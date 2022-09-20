using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using CommonLibrary;
using CommonLibrary.DataFormat.Format;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        private List<LibUnitBase> BattleCharacer = new List<LibUnitBase>();
        private List<LibUnitBase> BattleCharacerLive = new List<LibUnitBase>();
        private int QuestWeatherID;
        private WeatherDataEntity.mt_weather_listRow WeatherRow;
        private int BattleFieldID = 0;
        private int PlayerMemberNumber = 0;
        private int EnemyMemberCount = 0;
        private int SetCurrentBattleID = 0;
        private FieldDataEntity.mt_field_type_listRow FieldRow;
        private int BattleStyle = 0;
        private int PartyNo = 0;
        private int BaseLevel = 0;
        private Dictionary<int, int> monsterRaces = new Dictionary<int, int>();
        private List<LibUnitBase> Monsters = new List<LibUnitBase>();
        private List<LibUnitBase> NonPlayers = new List<LibUnitBase>();

        /// <summary>
        /// 戦闘メイン処理
        /// </summary>
        private void BattleMain()
        {
            StringBuilder MessageBuilder = new StringBuilder();
            LibPartyBattleSet battleset = new LibPartyBattleSet();
            int i = 0;
            int MarkID = 0;
            int MemberCount = 0;
            int CofferID = 0;

            foreach (DataRow PartyRow in PartyList.Rows)
            {
                PartyNo = (int)PartyRow["party_no"];
                string[] PartyMemberStr = LibParty.PartyMemberNoStr(PartyNo);
                int[] PartyMember = LibParty.PartyMemberNo(PartyNo);
                MemberCount = PartyMember.Length;

                ContinueDataEntity.ts_continue_mainRow[] ContinueMainRows = (ContinueDataEntity.ts_continue_mainRow[])con.Entity.ts_continue_main.Select("entry_no in (" + string.Join(",", PartyMemberStr) + ")");

                if (ContinueMainRows.Length == 0)
                {
                    // 誰も継続登録をしていない場合はスキップ
                    continue;
                }

                BattleCharacer = new List<LibUnitBase>();
                int MaxPartyLevel = 0;

                #region バトル参加キャラクターの選出(味方のみ)
                SetCurrentBattleID = 0;
                PlayerMemberNumber = 0;
                // 味方
                for (i = 0; i < PartyMember.Length; i++)
                {
                    SetCurrentBattleID++;
                    LibPlayer Member = CharaList.Find(chs => chs.EntryNo == PartyMember[i]);
                    Member.BattleID = SetCurrentBattleID;
                    Member.FormationDefault = Member.Formation;
                    Member.MemberNumber = PlayerMemberNumber;
                    BattleCharacer.Add(Member);

                    PlayerMemberNumber++;

                    if (Member.Level > MaxPartyLevel)
                    {
                        MaxPartyLevel = Member.Level;
                    }
                }
                #endregion

                // プレイヤーキャラクターの取得
                List<LibUnitBase> Players = new List<LibUnitBase>();
                Players =BattleCharacer.GetPlayers();

                if (!battleset.GetIsBattleStart(PartyNo))
                {
                    // 戦闘発生がない場合はスキップ
                    continue;
                }

                MarkID = battleset.GetMarkID(PartyNo);
                string QuestName = LibQuest.GetQuestMarkName(MarkID);
                QuestWeatherID = LibQuest.GetWeather(MarkID, GrandBlazeStudio.Properties.Settings.Default.UpdateCnt);
                BattleFieldID = LibQuest.GetFieldID(MarkID);
                CofferID = battleset.GetCofferID(PartyNo);
                WeatherRow = LibWeather.GetRow(QuestWeatherID);
                FieldRow = LibField.GetRow(BattleFieldID);
                int PopMonsterLevels = LibQuest.GetPopLevel(MarkID);
                QuestDataEntity.mt_mark_listRow MarkRow = LibQuest.GetMarkRow(MarkID);
                monsterRaces = new Dictionary<int, int>();

                MessageBuilder = new StringBuilder();
                MessageBuilder.AppendLine("<section id=\"battle\">");
                MessageBuilder.AppendLine("  <h2>バトル</h2>");
                MessageBuilder.AppendLine("  <h4 class=\"btarea\">バトルエリア：" + QuestName + "</h4>");
                MessageBuilder.AppendLine("  <dl class=\"areainfo\">");
                MessageBuilder.AppendLine("  <dt>フィールド</dt>");
                MessageBuilder.AppendLine("  <dd>" + LibField.GetName(BattleFieldID) + "</dd>");
                MessageBuilder.AppendLine("  <dt>天候</dt>");
                MessageBuilder.AppendLine("  <dd>" + WeatherRow.weather_name + "</dd>");
                MessageBuilder.AppendLine("  </dl>");

                int DefeatCondition = battleset.GetLoseStyle(PartyNo);
                int WinnerCondition = battleset.GetWinStyle(PartyNo);

                // 追加出現モンスターリスト
                // belongがモンスターなら敵、フレンドならNPC！
                ReinforceEntity.reinforceDataTable ReinforceData = new ReinforceEntity.reinforceDataTable();

                #region バトル参加キャラクターの選出(敵、NPC)
                // 最大ランダムモンスター出現数決定
                int MaxRandomMonster = MemberCount;
                if (MaxRandomMonster < PopMonsterLevels)
                {
                    MaxRandomMonster = PopMonsterLevels;
                }
                BaseLevel = 0;
                int BaseMinLevel = 255;

                // 平均レベル算出
                foreach (LibUnitBase Mine in Players)
                {
                    BaseLevel += Mine.Level;

                    if (BaseMinLevel > Mine.Level)
                    {
                        BaseMinLevel = Mine.Level;
                    }
                }

                BaseLevel = (int)Math.Round((decimal)BaseLevel / (decimal)Players.Count);

                // 敵
                string EnemyList = battleset.GetMonsterPopList(PartyNo);
                EnemyMemberCount = 0;
                // MEMO:　モンスターの出現ロジックは以下の通り
                /*
                 * 敵固定の場合：
                 * "78,80:6:1,81:2:-1"
                 * 上記の場合、No.78の敵が1体バトル開始時にいる
                 * 　No.81の敵が2ターンごとに1体出現する(無限なので勝利条件は注意)
                 * 　No.80の敵が6ターン後に1体出現する（出現したらそれだけで以後出現しない）
                 * 　
                 * ランダムモンスターの場合
                 * 総出現数は好戦度により上下する。
                 * 列として、「出現カウント」、「出現回数」を追加する。役割は上記とほぼ一緒
                 * 
                 * 敵No:敵種類(レアモンスターなど):出現カウント:出現回数(-1で無限)
                 */
                if (EnemyList.Length > 0)
                {
                    // 敵が固定されている場合
                    string[] FixEnemy = EnemyList.Split(',');
                    for (i = 0; i < FixEnemy.Length; i++)
                    {
                        SetCurrentBattleID++;
                        string[] EnemyOptions = FixEnemy[i].Split(':');
                        int EnemyNo = int.Parse(EnemyOptions[0]);
                        if (monsterRaces.ContainsKey(EnemyNo))
                        {
                            if (monsterRaces[EnemyNo] == 0)
                            {
                                monsterRaces[EnemyNo] = 2;
                            }
                            else
                            {
                                monsterRaces[EnemyNo]++;
                            }
                        }
                        else
                        {

                            monsterRaces[EnemyNo] = 0;
                        }

                        if (EnemyOptions.Length > 1)
                        {
                            // 増援
                            ReinforceEntity.reinforceRow ReinforceRow = ReinforceData.NewreinforceRow();
                            ReinforceRow.monster_id = EnemyNo;
                            ReinforceRow.belong = Status.Belong.Enemy;
                            ReinforceRow.time_count = int.Parse(EnemyOptions[1]);
                            ReinforceRow.limit_visitor = int.Parse(EnemyOptions[2]);
                            ReinforceRow.time_counter = 0;
                            ReinforceRow.last_time = 0;
                            ReinforceRow.last_battle_id = 0;
                            ReinforceRow.multi_count = monsterRaces[EnemyNo];
                            ReinforceData.AddreinforceRow(ReinforceRow);
                        }
                        else
                        {
                            LibMonster Member = null;
                            Member = new LibMonster(Status.Belong.Enemy, EnemyNo, BaseLevel);
                            Member.BattleID = SetCurrentBattleID;
                            Member.MemberNumber = EnemyMemberCount;
                            Member.MonsterMulti = monsterRaces[EnemyNo];
                            Member.TargetPartyCount = Players.Count;
                            Member.HPNow = Member.HPMax;
                            BattleCharacer.Add(Member);

                            EnemyMemberCount++;
                        }
                    }
                }
                else
                {
                    // 敵が固定されていない場合
                    DataTable PopMonster = LibQuest.GetPopMonsters(MarkID);
                    PopMonster.Columns.Add("count",typeof(int));
                    PopMonster.Columns.Add("del", typeof(bool));
                    PopMonster.Columns.Add("prob_min", typeof(int));
                    PopMonster.Columns.Add("prob_max", typeof(int));
                    int MaxProb = 0;

                    // 天候制限
                    foreach (DataRow PopRow in PopMonster.Rows)
                    {
                        PopRow["del"] = false;
                        PopRow["count"] = 0;

                        LibMonsterData.Entity.mt_monster_pop_weather.DefaultView.RowFilter = LibMonsterData.Entity.mt_monster_pop_weather.monster_idColumn.ColumnName + "=" + (int)PopRow["monster_id"];

                        if (LibMonsterData.Entity.mt_monster_pop_weather.DefaultView.Count > 0)
                        {
                            MonsterDataEntity.mt_monster_pop_weatherRow weatherPopRow = LibMonsterData.Entity.mt_monster_pop_weather.FindBymonster_idweather_id((int)PopRow["monster_id"], QuestWeatherID);

                            if (weatherPopRow == null)
                            {
                                PopRow["del"] = true;
                            }
                        }

                        if (!(bool)PopRow["del"])
                        {
                            PopRow["prob_min"] = MaxProb;
                            MaxProb += (int)PopRow["pop_prob"];
                            PopRow["prob_max"] = MaxProb;
                        }
                    }

                    DataView PopMonsterView = new DataView(PopMonster);

                    for (i = 0; i < MaxRandomMonster; i++)
                    {
                        SetCurrentBattleID++;

                        PopMonsterView.RowFilter = "del=false and (rare=0 or rare>count)";

                        if (PopMonsterView.Count == 0) { break; }

                        DataRow PopRow = SelectPopMonster(PopMonsterView, MaxProb);

                        int EnemyNo = (int)PopRow["monster_id"];

                        PopRow["count"] = (int)PopRow["count"] + 1;

                        if (monsterRaces.ContainsKey(EnemyNo))
                        {
                            if (monsterRaces[EnemyNo] == 0)
                            {
                                monsterRaces[EnemyNo] = 2;
                            }
                            else
                            {
                                monsterRaces[EnemyNo]++;
                            }
                        }
                        else
                        {

                            monsterRaces[EnemyNo] = 0;
                        }

                        if ((int)PopRow["time_count"] > 0 && (int)PopRow["limit_visitor"] > 0)
                        {
                            // 増援
                            ReinforceEntity.reinforceRow ReinforceRow = ReinforceData.NewreinforceRow();
                            ReinforceRow.monster_id = EnemyNo;
                            ReinforceRow.belong = Status.Belong.Enemy;
                            ReinforceRow.time_count = (int)PopRow["time_count"];
                            ReinforceRow.limit_visitor = (int)PopRow["limit_visitor"];
                            ReinforceRow.time_counter = 0;
                            ReinforceRow.last_time = 0;
                            ReinforceRow.last_battle_id = 0;
                            ReinforceRow.multi_count = monsterRaces[EnemyNo];
                            ReinforceData.AddreinforceRow(ReinforceRow);
                        }
                        else
                        {
                            LibMonster Member = null;
                            if (LibParty.CheckPartyOfficialEvent(PartyNo))
                            {
                                Member = new LibMonster(Status.Belong.Enemy, EnemyNo, BaseMinLevel);
                            }
                            else
                            {
                                Member = new LibMonster(Status.Belong.Enemy, EnemyNo, BaseLevel);
                            }
                            Member.BattleID = SetCurrentBattleID;
                            Member.MemberNumber = EnemyMemberCount;
                            Member.MonsterMulti = monsterRaces[EnemyNo];
                            Member.TargetPartyCount = Players.Count;
                            Member.HPNow = Member.HPMax;
                            BattleCharacer.Add(Member);

                            EnemyMemberCount++;
                        }
                    }
                }

                foreach (LibUnitBase MonsterMember in BattleCharacer)
                {
                    if (MonsterMember.PartyBelong == Status.Belong.Enemy)
                    {
                        if (MonsterMember.MonsterMulti == 0 && monsterRaces.ContainsKey(MonsterMember.EntryNo) && monsterRaces[MonsterMember.EntryNo] > 0)
                        {
                            MonsterMember.MonsterMulti = 1;
                        }
                    }
                }

                // NPC(ゲスト)
                string GuestList = battleset.GetGuestList(PartyNo);
                if (GuestList.Length > 0)
                {
                    string[] FixGuest = GuestList.Split(',');
                    for (i = 0; i < FixGuest.Length; i++)
                    {
                        string[] EnemyOptions = FixGuest[i].Split(':');

                        int GuestNo = int.Parse(EnemyOptions[0]);

                        if (EnemyOptions.Length > 1)
                        {
                            // 増援
                            ReinforceEntity.reinforceRow ReinforceRow = ReinforceData.NewreinforceRow();
                            ReinforceRow.monster_id = GuestNo;
                            ReinforceRow.belong = Status.Belong.Friend;
                            ReinforceRow.time_count = int.Parse(EnemyOptions[1]);
                            ReinforceRow.limit_visitor = int.Parse(EnemyOptions[2]);
                            ReinforceRow.time_counter = 0;
                            ReinforceData.AddreinforceRow(ReinforceRow);
                        }
                        else
                        {
                            SetCurrentBattleID++;
                            LibUnitBase Member = new LibGuest(Status.Belong.Friend, GuestNo, BaseLevel);
                            Member.BattleID = SetCurrentBattleID;
                            Member.MemberNumber = PlayerMemberNumber;
                            BattleCharacer.Add(Member);

                            PlayerMemberNumber++;
                        }
                    }
                }
                #endregion

                bool IsAnatomy = false;

                #region 初期ステータスの設定
                int CompanionCount = 6 - PartyMember.Length;

                // メンバーの増強
                List<LibUnitBase> TempCharacters = new List<LibUnitBase>();

                foreach (LibUnitBase Target in BattleCharacer)
                {
                    bool IsPetSummon = false;

                    foreach (EffectListEntity.effect_listRow EffectRow in Target.EffectList)
                    {
                        if (EffectRow.effect_div == Status.EffectDiv.Food) { continue; }

                        int EffectID = EffectRow.effect_id;
                        decimal Rank = EffectRow.rank;
                        decimal SubRank = EffectRow.sub_rank;
                        decimal Prob = EffectRow.prob;
                        int EndLimit = EffectRow.endlimit;
                        int EffectDiv = EffectRow.effect_div;

                        // 0の場合は-1扱い
                        if (EndLimit == 0) { EndLimit = -1; }

                        if (EffectID > 0 && EffectID < 300 && Prob == -1)
                        {
                            // オートステータス
                            Target.StatusEffect.Add(EffectID, (int)Rank, (int)SubRank, EndLimit, Target.Level, true);
                        }
                        else if (EffectID > 0 && EffectID < 300 && Prob > 0 && Prob >= LibInteger.GetRandBasis())
                        {
                            // オートステータス
                            Target.StatusEffect.Add(EffectID, (int)Rank, (int)SubRank, EndLimit, Target.Level, true);
                        }
                        else if (EffectID > 500 && EffectID < 700 && Prob == -1)
                        {
                            // オートステータス
                            Target.StatusEffect.Add(EffectID, (int)Rank, (int)SubRank, EndLimit, Target.Level, false);
                        }
                        else if (EffectID == 763)
                        {
                            // 初期TPボーナス
                            Target.TPNow = (int)Rank;
                        }
                        else if (EffectID == 953 && !IsPetSummon)
                        {
                            // ペット召喚
                            SetCurrentBattleID++;
                            int EnemyNo = (int)Rank;

                            if (Target.GetType() == typeof(LibPlayer))
                            {
                                ((LibPlayer)Target).PetCharacterBattleID = SetCurrentBattleID;
                            }
                            IsPetSummon = true;
                            string PetName = "";

                            if (Target.GetType() == typeof(LibMonster))
                            {
                                LibUnitBase Member = new LibMonster(Status.Belong.Enemy, EnemyNo, Target.Level, true);
                                Member.BattleID = SetCurrentBattleID;
                                Member.MemberNumber = EnemyMemberCount;
                                PetName = Member.NickName;
                                Member.NickName = Member.NickName + "(" + Target.NickName + ")";
                                TempCharacters.Add(Member);

                                EnemyMemberCount++;
                            }
                            else
                            {
                                LibUnitBase Member = new LibGuest(Status.Belong.Friend, EnemyNo, Target.Level);
                                Member.BattleID = SetCurrentBattleID;
                                Member.MemberNumber = PlayerMemberNumber;
                                PetName = Member.NickName;
                                Member.NickName = Member.NickName + "(" + Target.NickName + ")";
                                TempCharacters.Add(Member);

                                PlayerMemberNumber++;
                            }

                            MessageBuilder.AppendLine("<p>" + Target.NickName + "は、" + PetName + "を呼び出した！</p>");
                        }
                        else if (EffectID == 1600)
                        {
                            // セーフティ
                            Target.StatusEffect.Add(999, 1, 1, -1, Target.Level, false);
                        }
                        else if (EffectID == 2111)
                        {
                            // アナトミー
                            IsAnatomy = true;
                        }
                        else if (EffectID == 2119)
                        {
                            // プリパードネス
                            Target.TPNow += 10;
                        }
                    }

                    foreach (CharacterStatusListEntity.status_dataRow StatusRow in Target.StatusEffect.Rows)
                    {
                        int EffectID = StatusRow.status_id;
                        decimal Rank = StatusRow.rank;
                        decimal SubRank = StatusRow.sub_rank;
                        decimal Prob = 100;
                        int EndLimit = StatusRow.end_limit;

                        // 0の場合は-1扱い
                        if (EndLimit == 0) { EndLimit = -1; }

                        if (EffectID > 0 && EffectID < 300 && Prob == -1)
                        {
                            // オートステータス
                            Target.StatusEffect.Add(EffectID, (int)Rank, (int)SubRank, EndLimit, Target.Level, true);
                        }
                        else if (EffectID > 500 && EffectID < 700 && Prob == -1)
                        {
                            // オートステータス
                            Target.StatusEffect.Add(EffectID, (int)Rank, (int)SubRank, EndLimit, Target.Level, false);
                        }
                        else if (EffectID == 953 && !IsPetSummon)
                        {
                            // ペット召喚
                            SetCurrentBattleID++;
                            int EnemyNo = (int)Rank;

                            if (Target.GetType() == typeof(LibPlayer))
                            {
                                ((LibPlayer)Target).PetCharacterBattleID = SetCurrentBattleID;
                            }
                            IsPetSummon = true;
                            string PetName = "";

                            if (Target.PartyBelong == Status.Belong.Enemy)
                            {
                                LibUnitBase Member = new LibMonster(Status.Belong.Enemy, EnemyNo, Target.Level, true);
                                Member.BattleID = SetCurrentBattleID;
                                Member.MemberNumber = EnemyMemberCount;
                                PetName = Member.NickName;
                                Member.NickName = Member.NickName + "(" + Target.NickName + ")";
                                TempCharacters.Add(Member);

                                EnemyMemberCount++;
                            }
                            else
                            {
                                LibUnitBase Member = new LibGuest(Status.Belong.Friend, EnemyNo, Target.Level);
                                Member.BattleID = SetCurrentBattleID;
                                Member.MemberNumber = PlayerMemberNumber;
                                PetName = Member.NickName;
                                Member.NickName = Member.NickName + "(" + Target.NickName + ")";
                                TempCharacters.Add(Member);

                                PlayerMemberNumber++;
                            }

                            MessageBuilder.AppendLine("<p>" + Target.NickName + "は、" + PetName + "を呼び出した！</p>");
                        }
                    }
                }

                // 統合
                if (TempCharacters.Count > 0)
                {
                    BattleCharacer.AddRange(TempCharacters);
                }
                #endregion

                // モンスターの取得
                Monsters = new List<LibUnitBase>();
                Monsters = BattleCharacer.GetMonsters();

                // ノンプレイヤーの取得
                NonPlayers = new List<LibUnitBase>();
                NonPlayers = BattleCharacer.GetNonPlayers();

                #region 勝利＆敗北条件表示
                MessageBuilder.AppendLine("<dl class=\"object\">");
                MessageBuilder.AppendLine("<dt>勝利条件</dt>");
                switch (WinnerCondition)
                {
                    case Status.WinnerCondition.Annihilation:
                        MessageBuilder.AppendLine("  <dd>敵の全滅</dd>");
                        break;
                    case Status.WinnerCondition.BoneCollectorDefeat:
                        MessageBuilder.AppendLine("  <dd>ボーン・コレクターを倒せ！</dd>");
                        break;
                    case Status.WinnerCondition.UndeadMachineDefeat:
                        MessageBuilder.AppendLine("  <dd>謎の装置を無力化せよ！</dd>");
                        break;
                }
                MessageBuilder.AppendLine("<dt>敗北条件</dt>");
                switch (DefeatCondition)
                {
                    case Status.DefeatCondition.Annihilation:
                        MessageBuilder.AppendLine("  <dd>味方の全滅</dd>");
                        break;
                    case Status.DefeatCondition.FalkDefeat:
                        MessageBuilder.AppendLine("  <dd>味方の全滅、またはファルクの戦闘不能</dd>");
                        break;
                    case Status.DefeatCondition.TrunOver120:
                        MessageBuilder.AppendLine("  <dd>味方の全滅、または120カウントのオーバー</dd>");
                        break;
                }
                MessageBuilder.AppendLine("</dl>");
                #endregion

                #region 先制・バトル状況判定
                BattleStyle = battleset.GetBattleStyle(PartyNo);

                if (BattleStyle == -1)
                {
                    // 状態可変
                    decimal PreemptiveProb = 10;// 先制確率
                    decimal SurpriseProb = 10;// 不意打ち確率
                    decimal BackProb = 5; // バックアタック
                    decimal RoundProb = 5;// ラウンドアタック

                    // スキルによる変化（プレイヤーキャラクターのみ適用する）
                    foreach (LibUnitBase Mine in Players)
                    {
                        {
                            EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(700);
                            if (EffectRow != null)
                            {
                                SurpriseProb = 0;
                            }
                        }
                        {
                            EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(701);
                            if (EffectRow != null)
                            {
                                BackProb = 0;
                            }
                        }
                        {
                            EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(702);
                            if (EffectRow != null)
                            {
                                RoundProb = 0;
                            }
                        }
                        {
                            EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(706);
                            if (EffectRow != null)
                            {
                                PreemptiveProb = 50;
                            }
                        }
                    }

                    if (PreemptiveProb < 0)
                    {
                        PreemptiveProb = 0;
                    }
                    else if (PreemptiveProb > 100)
                    {
                        PreemptiveProb = 100;
                    }

                    if (SurpriseProb < 0)
                    {
                        SurpriseProb = 0;
                    }
                    else if (SurpriseProb > 100)
                    {
                        SurpriseProb = 100;
                    }

                    if (BackProb < 0)
                    {
                        BackProb = 0;
                    }
                    else if (BackProb > 100)
                    {
                        BackProb = 100;
                    }

                    if (RoundProb < 0)
                    {
                        RoundProb = 0;
                    }
                    else if (RoundProb > 100)
                    {
                        RoundProb = 100;
                    }

                    // 判定
                    if (LibInteger.GetRandBasis() <= PreemptiveProb)
                    {
                        // 先制攻撃
                        BattleStyle = Status.BattleStyle.Preemptive;
                    }
                    else if (LibInteger.GetRandBasis() <= SurpriseProb)
                    {
                        // 不意打ち
                        BattleStyle = Status.BattleStyle.Surprise;
                    }
                    else if (LibInteger.GetRandBasis() <= BackProb)
                    {
                        // バックアタック
                        BattleStyle = Status.BattleStyle.BackAttack;
                    }
                    else if (LibInteger.GetRandBasis() <= RoundProb)
                    {
                        // ラウンドアタック
                        BattleStyle = Status.BattleStyle.RoundAttack;
                    }
                }

                // 開始状況による補正
                // 先制と不意打ちは１ターン目の行動ができるかできないかに影響するので、ここでは設定しない。
                foreach (LibUnitBase Mine in BattleCharacer)
                {
                    switch (BattleStyle)
                    {
                        case Status.BattleStyle.BackAttack:
                            // バックアタック
                            switch (Mine.PartyBelong)
                            {
                                case Status.Belong.Friend:
                                    if (Mine.Formation == Status.Formation.Foward)
                                    {
                                        Mine.BattleFormation = Status.Formation.Backs;
                                    }
                                    else if (Mine.Formation == Status.Formation.Backs)
                                    {
                                        Mine.BattleFormation = Status.Formation.Foward;
                                    }
                                    break;
                            }
                            break;
                        case Status.BattleStyle.RoundAttack:
                            // ラウンドアタック
                            Mine.BattleFormation = LibInteger.GetRand(2);
                            break;
                    }
                }

                switch (BattleStyle)
                {
                    case Status.BattleStyle.Preemptive:
                        // 先制
                        MessageBuilder.AppendLine("<p class=\"battle_starts\">敵を発見！　先制攻撃のチャンス！</p>");
                        break;
                    case Status.BattleStyle.Surprise:
                        // 不意打ち
                        MessageBuilder.AppendLine("<p class=\"battle_starts\">モンスターが突然襲ってきた！</p>");
                        break;
                    case Status.BattleStyle.BackAttack:
                        // バックアタック
                        MessageBuilder.AppendLine("<p class=\"battle_starts\">背後からモンスターが襲いかかってきた！</p>");
                        break;
                    case Status.BattleStyle.RoundAttack:
                        // ラウンドアタック
                        MessageBuilder.AppendLine("<p class=\"battle_starts\">敵に周りを囲まれ乱戦状態に突入した！</p>");
                        break;
                    default:
                        // 通常
                        MessageBuilder.AppendLine("<p class=\"battle_starts\">敵が現れた！</p>");
                        break;
                }
                #endregion

                #region 敵出現メッセージ
                MessageBuilder.AppendLine("<ul class=\"encount\">");
                foreach (LibUnitBase Target in Monsters)
                {
                    string LevelColor = "level_equal";
                    if (Target.Level > (MaxPartyLevel + 3))
                    {
                        // つよすぎ
                        LevelColor = "level_over";
                    }
                    else if (Target.Level > MaxPartyLevel)
                    {
                        // かてるかもしれない
                        LevelColor = "level_upper";
                    }
                    else if (Target.Level < MaxPartyLevel)
                    {
                        // 楽な相手
                        LevelColor = "level_under";
                    }

                    MessageBuilder.AppendLine("<li><span class=\"" + LevelColor + "\">" + Target.NickName + "</span>が現れた！</li>");
                }
                MessageBuilder.AppendLine("</ul>");
                #endregion

                #region セリフ表示
                foreach (LibUnitBase Mine in BattleCharacer)
                {
                    string Serif = LibSerif.Serif(Mine, LibSituation.GetNo("戦闘開始"), null, QuestName, null);
                    if (Serif.Length > 0)
                    {
                        MessageBuilder.AppendLine(Serif);
                    }
                }
                #endregion

                // トラップ
                if (MarkRow.trap_level > 0 && MarkRow.mark_trap > 0)
                {
                    bool IsTrapShot = true;

                    foreach (LibPlayer Member in Players)
                    {
                        if (Member.FindTrap(MarkRow.trap_hide))
                        {
                            MessageBuilder.AppendLine("<dl class=\"trap\"><dt>" + Member.NickName + "はバトルフィールドに仕掛けられていた罠を発見した！</dt>");

                            if (Member.RemoveTrap(MarkRow.trap_level))
                            {
                                MessageBuilder.AppendLine("<dd>→罠の解除中 ... 成功！</dd>");
                                IsTrapShot = false;
                            }
                            else
                            {
                                MessageBuilder.AppendLine("<dd>→罠の解除中 ... 失敗！</dd>");
                            }

                            MessageBuilder.AppendLine("</dl>");
                        }
                    }

                    if(IsTrapShot){
                        foreach (LibPlayer Member in Players)
                        {
                            // トラップがある。
                            MessageBuilder.AppendLine("<dl class=\"trap\"><dt> トラップ【" + LibTrap.GetTrapName(MarkRow.mark_trap) + "】が発動した！</dt>");
                            if (LibTrap.GetHPDamagePercent(MarkRow.mark_trap) > 0)
                            {
                                int Damages = (int)((decimal)Member.HPMax * (decimal)LibTrap.GetHPDamagePercent(MarkRow.mark_trap) / 100m);
                                MessageBuilder.AppendLine("<dd> →" + Damages + "のダメージ！</dd>");
                                Member.HPNow -= Damages;
                                if (Member.HPNow <= 0)
                                {
                                    Member.Dead();
                                }
                            }
                            if (LibTrap.GetMPDamagePercent(MarkRow.mark_trap) > 0)
                            {
                                int Damages = (int)((decimal)Member.MPMax * (decimal)LibTrap.GetMPDamagePercent(MarkRow.mark_trap) / 100m);
                                MessageBuilder.AppendLine("<dd> →ＭＰに" + Damages + "のダメージ！</dd>");
                                Member.MPNow -= Damages;
                            }
                            foreach (EffectListEntity.effect_listRow EffectTrapRow in LibTrap.GetEffectList(MarkRow.mark_trap))
                            {
                                Member.StatusEffect.Add(EffectTrapRow.effect_id, (int)EffectTrapRow.rank, (int)EffectTrapRow.sub_rank, EffectTrapRow.endlimit, Member.Level, true);
                                MessageBuilder.AppendLine("<dd>→" + LibStatusList.GetName(EffectTrapRow.effect_id) + "になった！</dd>");
                            }

                            MessageBuilder.AppendLine("</dl>");
                        }
                    }
                }

                #region 戦闘開始
                int Turn = 0;
                int BattleResult = Status.BattleResult.Draw;

                BattleCharacerLive.Clear();
                BattleCharacerLive = BattleCharacer.GetLive();

                while (Turn <= 999)
                {
                    #region 隊列の補正
                    BattleCommon.FormationResetting(BattleCharacer);
                    #endregion

                    #region 簡易ステータスの表示
                    {
                        MessageBuilder.AppendLine("<p class=\"turn_obj\">Turn " + (Turn + 1).ToString() + "</p>");

                        #region プレイヤーキャラクター＆ゲスト
                        MessageBuilder.AppendLine("<table class=\"mini_status\">");
                        MessageBuilder.AppendLine("<tr>");
                        MessageBuilder.AppendLine("<th class=\"st_name\">Friends</th><th class=\"st_hp\">HP</th><th class=\"st_mp\">MP</th><th class=\"st_tp\">TP</th><th class=\"st_status\">Status</th>");
                        MessageBuilder.AppendLine("</tr>");

                        foreach (LibUnitBase Mine in Players)
                        {
                            if (Mine.BattleOut)
                            {
                                MessageBuilder.AppendLine("<tr class=\"battle_out\">");
                            }
                            else
                            {
                                MessageBuilder.AppendLine("<tr>");
                            }
                            MessageBuilder.Append("<td class=\"st_name\" title=\"" + Mine.CharacterName + "\">" + Mine.NickName + "<small>" + Mine.FormationName + "</small></td>");
                            MessageBuilder.Append("<td class=\"st_hp\">" + Mine.DecorateHP + "<div class=\"gauge\"><div class=\"gaugebar hp\" style=\"width:" + Mine.HPDamageRate + "%;\"></div></div></td>");
                            MessageBuilder.Append("<td class=\"st_mp\">" + Mine.DecorateMP + "<div class=\"gauge\"><div class=\"gaugebar mp\" style=\"width:" + Mine.MPDamageRate + "%;\"></div></div></td>");
                            MessageBuilder.Append("<td class=\"st_tp\">" + Mine.DecorateTP + "<div class=\"gauge\"><div class=\"gaugebar tp\" style=\"width:" + Mine.TPDamageRate + "%;\"></div></div></td>");
                            MessageBuilder.Append("<td class=\"st_status\">" + Mine.StatusEffect.ToString(false));
                            MessageBuilder.AppendLine("</tr>");
                        }

                        foreach (LibUnitBase Mine in NonPlayers)
                        {
                            if (Mine.BattleOut)
                            {
                                MessageBuilder.AppendLine("<tr class=\"battle_out\">");
                            }
                            else
                            {
                                MessageBuilder.AppendLine("<tr>");
                            }
                            MessageBuilder.Append("<td class=\"st_name guest_back\" title=\"" + Mine.CharacterName + "\">" + Mine.NickName + "<small>" + Mine.FormationName + "</small></td>");
                            MessageBuilder.Append("<td class=\"st_hp\">" + Mine.DecorateHP + "<div class=\"gauge\"><div class=\"gaugebar hp\" style=\"width:" + Mine.HPDamageRate + "%;\"></div></div></td>");
                            MessageBuilder.Append("<td class=\"st_mp\">" + Mine.DecorateMP + "<div class=\"gauge\"><div class=\"gaugebar mp\" style=\"width:" + Mine.MPDamageRate + "%;\"></div></div></td>");
                            MessageBuilder.Append("<td class=\"st_tp\">" + Mine.DecorateTP + "<div class=\"gauge\"><div class=\"gaugebar tp\" style=\"width:" + Mine.TPDamageRate + "%;\"></div></div></td>");
                            MessageBuilder.Append("<td class=\"st_status\">" + Mine.StatusEffect.ToString(false));
                            MessageBuilder.AppendLine("</tr>");
                        }

                        MessageBuilder.AppendLine("</table>");
                        #endregion

                        #region モンスター
                        MessageBuilder.AppendLine("<br />");
                        MessageBuilder.AppendLine("<table class=\"mini_status\">");
                        MessageBuilder.AppendLine("<tr>");
                        MessageBuilder.AppendLine("<th class=\"st_name\">Enemy</th><th class=\"st_hp\">HP</th><th class=\"st_mp\">MP</th><th class=\"st_tp\">TP</th><th class=\"st_status\">Status</th>");
                        MessageBuilder.AppendLine("</tr>");

                        foreach (LibUnitBase Mine in Monsters)
                        {
                            if (Mine.BattleOut)
                            {
                                continue;
                            }
                            else
                            {
                                MessageBuilder.AppendLine("<tr>");
                            }
                            MessageBuilder.Append("<td class=\"st_name\" title=\"" + Mine.CharacterName + "\">" + Mine.NickName + "<small>" + Mine.FormationName + "</small></td>");
                            if (IsAnatomy && Mine.Category == Status.Category.Human)
                            {
                                MessageBuilder.Append("<td class=\"st_hp\">" + Mine.DecorateHP + "<div class=\"gauge\"><div class=\"gaugebar hp\" style=\"width:" + Mine.HPDamageRate + "%;\"></div></div></td>");
                                MessageBuilder.Append("<td class=\"st_mp\">" + Mine.DecorateMP + "<div class=\"gauge\"><div class=\"gaugebar mp\" style=\"width:" + Mine.MPDamageRate + "%;\"></div></div></td>");
                                MessageBuilder.Append("<td class=\"st_tp\">" + Mine.DecorateTP + "<div class=\"gauge\"><div class=\"gaugebar tp\" style=\"width:" + Mine.TPDamageRate + "%;\"></div></div></td>");
                            }
                            else
                            {
                                MessageBuilder.Append("<td class=\"st_hp\"><div class=\"gauge\"><div class=\"gaugebar hp\" style=\"width:" + Mine.HPDamageRate + "%;\"></div></div></td>");
                                MessageBuilder.Append("<td class=\"st_mp\"><div class=\"gauge\"><div class=\"gaugebar mp\" style=\"width:" + Mine.MPDamageRate + "%;\"></div></div></td>");
                                MessageBuilder.Append("<td class=\"st_tp\"><div class=\"gauge\"><div class=\"gaugebar tp\" style=\"width:" + Mine.TPDamageRate + "%;\"></div></div></td>");
                            }
                            MessageBuilder.Append("<td class=\"st_status\">" + Mine.StatusEffect.ToString(false));
                            MessageBuilder.AppendLine("</tr>");
                        }

                        MessageBuilder.AppendLine("</table>");
                        #endregion
                    }
                    #endregion

                    // 揮発ヘイト
                    foreach (LibUnitBase Mine in BattleCharacerLive)
                    {
                        // 被回復量のリセット
                        Mine.ReceivedHeal = 0;
                        Mine.ReceivedHealMP = 0;
                        Mine.ReceivedHealTP = 0;

                        // 被ステータス変化の設定
                        Mine.StatusEffect.Copy(Mine.ReceivedStatusEffect);
                    }

                    #region イニシアチブ決定
                    foreach (LibUnitBase Mine in BattleCharacerLive)
                    {
                        BattleInitiativeSet(Mine, Turn);
                    }
                    #endregion

                    bool IsBattleEnd = false;

                    #region 戦闘行動
                    // 戦闘参加者をソートする。
                    List<LibUnitBase> BattleActors = new List<LibUnitBase>(BattleCharacerLive);
                    BattleActors.Sort((x, y) => x.ChargeTime - y.ChargeTime);

                    foreach (LibUnitBase Mine in BattleActors)
                    {
                        if (!Mine.BattleOut)
                        {
                            BattleAction(Mine, MessageBuilder, QuestName, Turn);
                        }

                        BattleCharacerLive.Clear();
                        BattleCharacerLive = LibBattleCharacter.GetLive(BattleCharacer);

                        if (CheckBattleEnd(ref BattleResult, DefeatCondition, WinnerCondition, Turn))
                        {
                            IsBattleEnd = true;
                            break;
                        }
                    }
                    #endregion

                    // ステータス時間減算
                    foreach (LibUnitBase Waiters in BattleCharacerLive)
                    {
                        Waiters.StatusEffect.HalfCount();

                        #region ステータス異常の解除
                        DataTable ClearStatusEffectList = Waiters.StatusEffect.GetClearStatusList();
                        foreach (DataRow StatusRow in ClearStatusEffectList.Rows)
                        {
                            string StatusName = LibStatusList.GetName((int)StatusRow["status_id"]);
                            if (StatusName.Length > 0)
                            {
                                MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Waiters.NickName) + "の" + StatusName + "の効果が切れた。</dd>");
                            }
                            Waiters.StatusEffect.Delete((int)StatusRow["status_id"]);
                        }
                        #endregion
                    }

                    // 戦闘終了判定
                    if (IsBattleEnd)
                    {
                        break;
                    }

                    // レインフォース！
                    int TimeOfMinute = Turn;
                    ReinforceData.DefaultView.RowFilter = "limit_visitor<>0";
                    bool IsRainforce = false;
                    foreach (DataRowView ReinforceViewRow in ReinforceData.DefaultView)
                    {
                        ReinforceEntity.reinforceRow ReinforceRow = (ReinforceEntity.reinforceRow)ReinforceViewRow.Row;

                        if (Turn < (ReinforceRow.last_time + ReinforceRow.time_count))
                        {
                            continue;
                        }

                        LibUnitBase RainforceUnit = BattleCharacerLive.Find(RaUn => RaUn.BattleID == ReinforceRow.last_battle_id);

                        if (RainforceUnit != null)
                        {
                            continue;
                        }

                        if (ReinforceRow.limit_visitor > 0)
                        {
                            ReinforceRow.limit_visitor--;
                        }

                        // 増援の追加
                        if (ReinforceRow.belong == Status.Belong.Friend)
                        {
                            // NPC
                            SetCurrentBattleID++;
                            LibUnitBase Member = new LibGuest(Status.Belong.Friend, ReinforceRow.monster_id, BaseLevel);
                            Member.BattleID = SetCurrentBattleID;
                            Member.MemberNumber = PlayerMemberNumber;
                            Member.MonsterMulti = ReinforceRow.multi_count;
                            BattleCharacer.Add(Member);
                            ReinforceRow.last_battle_id = SetCurrentBattleID;

                            PlayerMemberNumber++;

                            MessageBuilder.AppendLine("<dl class=\"btact guest\">");
                            MessageBuilder.AppendLine("<dt>" + Member.NickName + "が戦場に駆けつけた！</dt>");
                            MessageBuilder.AppendLine("</dl>");
                        }
                        else
                        {
                            // 敵
                            SetCurrentBattleID++;
                            LibUnitBase Member = new LibMonster(Status.Belong.Enemy, ReinforceRow.monster_id, BaseLevel);
                            Member.BattleID = SetCurrentBattleID;
                            Member.MemberNumber = EnemyMemberCount;
                            Member.MonsterMulti = ReinforceRow.multi_count;
                            Member.TargetPartyCount = Players.Count;
                            Member.HPNow = Member.HPMax;
                            BattleCharacer.Add(Member);
                            ReinforceRow.last_battle_id = SetCurrentBattleID;

                            EnemyMemberCount++;

                            MessageBuilder.AppendLine("<dl class=\"btact enemy\">");
                            MessageBuilder.AppendLine("<dt>" + Member.NickName + "が増援に来た！</dt>");
                            MessageBuilder.AppendLine("</dl>");
                        }

                        ReinforceRow.last_time = Turn;
                        IsRainforce = true;
                    }

                    if (IsRainforce)
                    {
                        // メンバーが増加したので再度メンバーリスト修正
                        BattleCharacerLive.Clear();
                        BattleCharacerLive = LibBattleCharacter.GetLive(BattleCharacer);

                        // モンスターの取得
                        Monsters.Clear();
                        Monsters = LibBattleCharacter.GetMonsters(BattleCharacer);

                        // ノンプレイヤーの取得
                        NonPlayers.Clear();
                        NonPlayers = LibBattleCharacter.GetNonPlayers(BattleCharacer);
                    }

                    Turn += 1;
                }

                #endregion

                #region 戦闘終了処理
                switch (BattleResult)
                {
                    case Status.BattleResult.Draw:
                        MessageBuilder.AppendLine("<p class=\"result_draw\">決着がつかない！ 撤退した…。</p>");
                        break;
                    case Status.BattleResult.Winner:
                        MessageBuilder.AppendLine("<p class=\"result_win\">戦闘に勝利した！</p>");

                        int RateMin = 100;

                        foreach (LibUnitBase Mine in NonPlayers)
                        {
                            if (Mine.HPDamageRate < RateMin)
                            {
                                RateMin = Mine.HPDamageRate;
                            }
                        }
                        foreach (LibUnitBase Mine in Players)
                        {
                            if (Mine.HPDamageRate < RateMin)
                            {
                                RateMin = Mine.HPDamageRate;
                            }
                        }

                        #region セリフ表示(NPC)
                        foreach (LibUnitBase Mine in NonPlayers)
                        {
                            // ターン経過によってセリフを変える
                            if (RateMin < 35)
                            {
                                MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("戦闘勝利・辛勝"), null, QuestName, null));
                            }
                            else if (RateMin > 80 && Turn < 2)
                            {
                                MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("戦闘勝利・圧勝"), null, QuestName, null));
                            }
                            else
                            {
                                MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("戦闘勝利・普通"), null, QuestName, null));
                            }
                        }
                        #endregion

                        #region セリフ表示
                        foreach (LibUnitBase Mine in Players)
                        {
                            // HP比率によってセリフを変える
                            if (RateMin < 35)
                            {
                                MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("戦闘勝利・辛勝"), null, QuestName, null));
                            }
                            else if (RateMin > 80 && Turn < 2)
                            {
                                MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("戦闘勝利・圧勝"), null, QuestName, null));
                            }
                            else
                            {
                                MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("戦闘勝利・普通"), null, QuestName, null));
                            }
                        }
                        #endregion
                        break;
                    case Status.BattleResult.Lose:
                        MessageBuilder.AppendLine("<p class=\"result_lose\">パーティは全滅した…。</p>");
                        break;
                }

                MessageBuilder.AppendLine("</section>");
                MessageBuilder.AppendLine("<section id=\"btresult\">");
                MessageBuilder.AppendLine("  <h2>バトルリザルト</h2>");

                // 戦闘不能者による変換率
                double ExperianceDeadRate = 1.0;
                int DeadCount = 0;

                foreach (LibUnitBase Mine in Players)
                {
                    if (Mine.BattleOut)
                    {
                        DeadCount++;
                    }
                }

                ExperianceDeadRate = 1.0 - (double)DeadCount * 0.05;

                // トレジャー入手確率上昇値
                int GetTresureUp = 0;

                // ギムルハント
                int GimlHant = 0;

                // ヒューマノイド系がいる
                bool IsManType = false;

                #region 経験値処理
                // 経験値算出
                int ExperiancePoint = 0;

                // 基本値算出
                int BlazeChips = 0;
                int ByeMonsterCount = Monsters.Count;
                foreach (LibUnitBase Target in Monsters)
                {
                    ExperiancePoint += Target.Exp;

                    // チップ入手
                    for (i = 0; i < 3; i++)
                    {
                        if (Target.PartyBelongDetail == Status.BelongDetail.Normal && 30 > LibInteger.GetRandBasis())
                        {
                            BlazeChips += LibInteger.GetRandMax(1, 3);
                        }
                    }

                    if (Target.Category == Status.Category.Human)
                    {
                        IsManType = true;
                    }
                }

                // 平均
                if (ByeMonsterCount > 0)
                {
                    BlazeChips = BlazeChips / ByeMonsterCount;
                }
                else
                {
                    ExperiancePoint = 0;
                    BlazeChips = 0;
                }

                // 経験値算出
                ExperiancePoint = (int)((double)ExperiancePoint * ExperianceDeadRate / (double)Players.Count);

                foreach (LibPlayer Mine in Players)
                {
                    // 取得経験値
                    int GetExp = ExperiancePoint;

                    if (BattleResult == Status.BattleResult.Lose)
                    {
                        // 勝利以外では経験値減少する
                        GetExp = (int)((double)GetExp * 0.1);
                    }

                    if (GetExp <= 0)
                    {
                        GetExp = 1;
                    }

                    // 財宝がでるときは報酬は他にない（引き分け含む）
                    if (LibParty.CheckPartyOfficialEvent(PartyNo))
                    {
                        GetExp = GetExp / 2;
                        BlazeChips = 0;
                    }
                    else if (CofferID > 0 || BattleResult == Status.BattleResult.Draw)
                    {
                        GetExp = 0;
                        BlazeChips = 0;
                    }

                    string GetChipText = "、" + BlazeChips + " のB.C.";
                    if (BattleResult != Status.BattleResult.Winner || BlazeChips == 0)
                    {
                        // 勝利でのみBCゲット
                        GetChipText = "";
                    }

                    MessageBuilder.AppendLine("<div class=\"exp_point\">" + Mine.NickName + "は " + GetExp + " のExp" + GetChipText + "を獲得！</div>");

                    if (LibConst.LevelLimit == Mine.Level)
                    {
                        MessageBuilder.AppendLine("<div class=\"level_up\">" + Mine.NickName + "はレベルがレベル制限まで到達しているため、これ以上レベルを上げることができません。</div>");
                    }

                    Mine.GetExp += GetExp;

                    if (BattleResult == Status.BattleResult.Winner)
                    {
                        Mine.BlazeChip += BlazeChips;
                    }

                    // ローマンスピリット
                    if (Mine.EffectList.FindByeffect_id(875) != null)
                    {
                        int RomanGiml = (int)((decimal)Mine.TempBattleScore.MaxAttackDamage * 0.1m * (decimal)LibInteger.GetRandMax(80, 120) / 100m);
                        MessageBuilder.AppendLine("<div class=\"level_up\">→どこからかおひねりが飛んできた！ " + LibResultText.CSSEscapeMoney(RomanGiml, false) + "を手に入れた！</div>");
                    }

                    // トレジャー入手確率アップ判定
                    EffectListEntity.effect_listRow TresureGetUpRow = Mine.EffectList.FindByeffect_id(871);
                    if (TresureGetUpRow != null && TresureGetUpRow.prob > LibInteger.GetRandBasis() && (int)TresureGetUpRow.rank > GetTresureUp)
                    {
                        GetTresureUp = (int)TresureGetUpRow.rank;
                    }

                    // ギムルハント判定
                    EffectListEntity.effect_listRow GimlHantRow = Mine.EffectList.FindByeffect_id(870);
                    if (GimlHantRow != null && (int)GimlHantRow.rank > GimlHant)
                    {
                        GimlHant = (int)GimlHantRow.rank;
                    }

                    // スカベンジャー
                    if (Mine.EffectList.FindByeffect_id(2114) != null && LibInteger.GetRandBasis() <= 10)
                    {
                        CommonItemEntity.item_listRow[] ScabengeItemRows = (CommonItemEntity.item_listRow[])LibItem.Entity.item_list.Select("it_type=67 and it_price<=" + Mine.LevelNormal * 15);
                        if (ScabengeItemRows.Length > 0)
                        {
                            CommonItemEntity.item_listRow ScabengeItemRow = ScabengeItemRows[LibInteger.GetRand(ScabengeItemRows.Length)];
                            int ScItemCount = 1;
                            int ScRestItemCount = 0;
                            if (Mine.AddItem(Status.ItemBox.Normal, ScabengeItemRow.it_num, ScabengeItemRow.it_creatable, ref ScItemCount, ref ScRestItemCount))
                            {
                                MessageBuilder.AppendLine("<div class=\"level_up\">→スカベンジャー！" + LibResultText.CSSEscapeItem(ScabengeItemRow.it_name) + "を見つけた！</div>");
                            }
                        }
                    }

                    // デッドスカベンジャー
                    if (IsManType && Mine.EffectList.FindByeffect_id(2130) != null)
                    {
                        Mine.HPNow += (int)((decimal)Mine.HPMax * 0.1m);
                        MessageBuilder.AppendLine("<div class=\"level_up\">→デッドスカベンジ！ムシャムシャ……ＨＰが回復！</div>");
                    }
                }

                #endregion

                #region 戦利品入手判定
                // 勝利時のみ判定あり
				// MEMO:
				/*
				 *  ドロップアイテムの判定は各キャラクターごとに独立して行うように変更
				 */
                if (BattleResult == Status.BattleResult.Winner)
                {
                    if (CofferID == 0)
                    {
                        foreach (LibUnitBase Target in Monsters)
                        {
                            if (Target.PartyBelong == Status.Belong.Friend) { continue; }

                            // 戦利品の確定
                            ((LibMonster)Target).HaveItem.DefaultView.RowFilter = "drop_type=0";
                            ((LibMonster)Target).HaveItem.DefaultView.Sort = "get_synx desc";

                            foreach (DataRowView HaveItemRow in ((LibMonster)Target).HaveItem.DefaultView)
                            {
                                int ProbSteal = 0;

                                #region 確率の設定
                                switch ((int)HaveItemRow["get_synx"])
                                {
                                    case 0:
                                        ProbSteal = 2;
                                        switch (GetTresureUp)
                                        {
                                            case 1:
                                                ProbSteal = 3;
                                                break;
                                            case 2:
                                                ProbSteal = 5;
                                                break;
                                            case 3:
                                                ProbSteal = 7;
                                                break;
                                            case 4:
                                                ProbSteal = 10;
                                                break;
                                        }
                                        break;
                                    case 1:
                                        ProbSteal = 10;
                                        switch (GetTresureUp)
                                        {
                                            case 1:
                                                ProbSteal = 13;
                                                break;
                                            case 2:
                                                ProbSteal = 16;
                                                break;
                                            case 3:
                                                ProbSteal = 20;
                                                break;
                                            case 4:
                                                ProbSteal = 25;
                                                break;
                                        }
                                        break;
                                    case 2:
                                        ProbSteal = 45;
                                        switch (GetTresureUp)
                                        {
                                            case 1:
                                                ProbSteal = 50;
                                                break;
                                            case 2:
                                                ProbSteal = 55;
                                                break;
                                            case 3:
                                                ProbSteal = 60;
                                                break;
                                            case 4:
                                                ProbSteal = 68;
                                                break;
                                        }
                                        break;
                                    case 3:
                                        ProbSteal = 70;
                                        switch (GetTresureUp)
                                        {
                                            case 1:
                                                ProbSteal = 75;
                                                break;
                                            case 2:
                                                ProbSteal = 80;
                                                break;
                                            case 3:
                                                ProbSteal = 85;
                                                break;
                                            case 4:
                                                ProbSteal = 95;
                                                break;
                                        }
                                        break;
                                    case 4:
                                        ProbSteal = 100;
                                        break;
                                    case 5:
                                        // 心得による入手
                                        ProbSteal = 0;
                                        {
                                            foreach (LibPlayer DropperPlayer in Players)
                                            {
                                                EffectListEntity.effect_listRow EffectMasterRow = DropperPlayer.EffectList.FindByeffect_id(872);

                                                if (EffectMasterRow == null)
                                                {
                                                    continue;
                                                }

                                                if ((int)EffectMasterRow.rank == ((LibMonster)Target).Category || (int)EffectMasterRow.sub_rank == ((LibMonster)Target).Category)
                                                {
                                                    ProbSteal = 5;
                                                }
                                            }
                                        }
                                        break;
                                }
                                #endregion

                                if (ProbSteal > LibInteger.GetRandBasis())
                                {
                                    // アイテム取得
                                    int ItemID = (int)HaveItemRow["it_num"];
                                    int ItemCount = (int)HaveItemRow["it_box_count"];

                                    if (ItemCount == 0) { continue; }

                                    // エクストラボーナス
                                    int ExtraBonusProb1 = 100;
                                    int ExtraBonusProb2 = 0;
                                    int ExtraBonusProb3 = 0;
                                    int ExtraBonusCount = 1;

                                    switch (GetTresureUp)
                                    {
                                        case 1:
                                            ExtraBonusProb1 = 65;
                                            ExtraBonusProb2 = 35;
                                            break;
                                        case 2:
                                            ExtraBonusProb1 = 30;
                                            ExtraBonusProb2 = 50;
                                            ExtraBonusProb3 = 20;
                                            break;
                                        case 3:
                                            ExtraBonusProb1 = 0;
                                            ExtraBonusProb2 = 60;
                                            ExtraBonusProb3 = 30;
                                            break;
                                        case 4:
                                            ExtraBonusProb1 = 0;
                                            ExtraBonusProb2 = 50;
                                            ExtraBonusProb3 = 40;
                                            break;
                                    }

                                    decimal ExtraProb = LibInteger.GetRandBasis();

                                    if (ExtraBonusProb1 >= ExtraProb)
                                    {
                                        ExtraBonusCount = 1;
                                    }
                                    else if ((ExtraBonusProb1 + ExtraBonusProb2) >= ExtraProb)
                                    {
                                        ExtraBonusCount = 2;
                                    }
                                    else if ((ExtraBonusProb1 + ExtraBonusProb2 + ExtraBonusProb3) >= ExtraProb)
                                    {
                                        ExtraBonusCount = 3;
                                    }
                                    else
                                    {
                                        ExtraBonusCount = 4;
                                    }

                                    // 入手量再計算
                                    if (ItemID > 0)
                                    {
                                        ItemCount *= ExtraBonusCount;
                                    }
                                    else
                                    {
                                        ItemCount += (int)((decimal)ItemCount * (decimal)GimlHant / 100m);
                                    }

                                    // 入手判定種類別処理
                                    if (PartyMember.Length == 1)
                                    {
                                        // 入手者
                                        LibPlayer GetCharacter;

                                        List<LibUnitBase> GetterCharas = new List<LibUnitBase>();
                                        foreach (LibPlayer GetTg in Players)
                                        {
                                            if (!GetTg.IsTresureGetting)
                                            {
                                                GetterCharas.Add(GetTg);
                                            }
                                        }

                                        if (GetterCharas.Count == 0)
                                        {
                                            GetCharacter = (LibPlayer)Players[LibInteger.GetRand(Players.Count)];
                                        }
                                        else
                                        {
                                            GetCharacter = (LibPlayer)GetterCharas[LibInteger.GetRand(GetterCharas.Count)];
                                        }

                                        GetCharacter.IsTresureGetting = true;

                                        if (ItemID > 0)
                                        {
                                            if (LibItem.CheckRare(ItemID, false))
                                            {
                                                // レアの場合は一個のみ
                                                ItemCount = 1;
                                            }

                                            string CountItemStok = "";
                                            int RestItemCount = 0;
                                            if (ItemCount > 1)
                                            {
                                                CountItemStok = ItemCount + "個";
                                            }

                                            MessageBuilder.AppendLine("<div class=\"have_item\">" + Target.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を" + CountItemStok + "持っていた！</div>");
                                            if (GetCharacter.AddItem(Status.ItemBox.Normal, ItemID, false, ref ItemCount, ref RestItemCount))
                                            {
                                                MessageBuilder.AppendLine("<div class=\"get_item\">→" + GetCharacter.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を" + CountItemStok + "手に入れた。</div>");
                                            }
                                            else
                                            {
                                                MessageBuilder.AppendLine("<div class=\"get_item\">→" + GetCharacter.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を持てなかったため、諦めた…。</div>");
                                            }
                                        }
                                        else
                                        {
                                            MessageBuilder.AppendLine("<div class=\"have_item\">" + Target.NickName + "は" + LibResultText.CSSEscapeMoney(ItemCount, false) + "を持っていた！</div>");
                                            MessageBuilder.AppendLine("<div class=\"get_item\">→" + GetCharacter.NickName + "は" + LibResultText.CSSEscapeMoney(ItemCount, false) + "を手に入れた。</div>");
                                            GetCharacter.HaveMoney += ItemCount;
                                        }
                                    }
                                    else
                                    {
                                        if (ItemID == 0)
                                        {
                                            // お金入手

                                            int GetCofferMoneyCount = ItemCount;

                                            MessageBuilder.AppendLine("<div class=\"have_item\">" + Target.NickName + "は" + LibResultText.CSSEscapeMoney(GetCofferMoneyCount, false) + "を持っていた！</div>");
                                            MessageBuilder.AppendLine("<div class=\"get_item\">→それぞれ、" + LibResultText.CSSEscapeMoney(GetCofferMoneyCount, false) + "を手に入れた。</div>");

                                            foreach (LibPlayer MoneyGetPlayers in Players)
                                            {
                                                MoneyGetPlayers.HaveMoney += GetCofferMoneyCount;
                                            }
                                        }
                                        else
                                        {
                                            // ルート登録
                                            bool IsRare = false;
                                            if (LibItem.CheckRare(ItemID, false))
                                            {
                                                // レアの場合は一個のみ
                                                ItemCount = 1;
                                                IsRare = true;
                                            }

                                            string CountItemStok = "";
                                            int RestItemCount = 0;
                                            if (ItemCount > 1)
                                            {
                                                CountItemStok = ItemCount + "個";
                                            }

                                            MessageBuilder.AppendLine("<div class=\"have_item\">" + Target.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を" + CountItemStok + "持っていた！</div>");

                                            // レアアイテムの場合のみ、誰か一人に渡す。他はすべて全員入手
                                            if (IsRare)
                                            {
                                                // 入手者
                                                LibPlayer GetCharacter;

                                                List<LibPlayer> GetterCharas = new List<LibPlayer>();
                                                foreach (LibPlayer GetTg in Players)
                                                {
                                                    if (GetTg.ContinueNoCount == 0)
                                                    {
                                                        // 継続登録してる人優先
                                                        GetterCharas.Add(GetTg);
                                                    }
                                                }

                                                if (GetterCharas.Count == 0)
                                                {
                                                    GetCharacter = (LibPlayer)Players[LibInteger.GetRand(Players.Count)];
                                                }
                                                else
                                                {
                                                    GetCharacter = GetterCharas[LibInteger.GetRand(GetterCharas.Count)];
                                                }

                                                if (GetCharacter.AddItem(Status.ItemBox.Normal, ItemID, false, ref ItemCount, ref RestItemCount))
                                                {
                                                    MessageBuilder.AppendLine("<div class=\"get_item\">→" + GetCharacter.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を" + CountItemStok + "手に入れた。</div>");
                                                }
                                                else
                                                {
                                                    MessageBuilder.AppendLine("<div class=\"get_item\">→" + GetCharacter.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を持てなかったため、諦めた…。</div>");
                                                }
                                            }
                                            else
                                            {
                                                MessageBuilder.AppendLine("<div class=\"get_item\">→それぞれ、" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を" + CountItemStok + "手に入れた。</div>");
                                                foreach (LibPlayer ItemGetPlayers in Players)
                                                {
                                                    ItemGetPlayers.AddItem(Status.ItemBox.Normal, ItemID, false, ref ItemCount, ref RestItemCount);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (CofferID > 0)
                    {
                        if (LibParty.CheckPartyOfficialEvent(PartyNo) && LibQuest.OfficialEventID == Status.OfficialEvent.Christmas)
                        {
                            MessageBuilder.AppendLine("<div class=\"have_item\">隠されていたプレゼントを発見した！</div>");

                            int PresentItemID = 10;

                            foreach (LibUnitBase Target in Monsters)
                            {
                                if (Target.PartyBelong == Status.Belong.Friend) { continue; }

                                // 入手アイテム確定
                                int Hate = Target.HateList.GetTopHate(Target.PartyBelong);

                                if (Hate < 1000)
                                {
                                    PresentItemID = 2003;
                                }
                                else if (Hate < 4000)
                                {
                                    PresentItemID = 4605;
                                }
                                else
                                {
                                    PresentItemID = 4606;
                                }

                                break;
                            }

                            MessageBuilder.AppendLine("<div class=\"have_item\">プレゼントには" + LibResultText.CSSEscapeItem(LibItem.GetItemName(PresentItemID, false)) + "が入っていた！</div>");
                            foreach (LibPlayer GetCharacter in Players)
                            {
                                int ItemCount = 1;
                                int RestItemCount = 0;

                                GetCharacter.IsTresureGetting = true;

                                if (LibItem.CheckRare(PresentItemID, false))
                                {
                                    // レアの場合は一個のみ
                                    ItemCount = 1;
                                }

                                string CountItemStok = "";
                                if (ItemCount > 1)
                                {
                                    CountItemStok = ItemCount + "個";
                                }

                                if (GetCharacter.AddItem(Status.ItemBox.Normal, PresentItemID, false, ref ItemCount, ref RestItemCount))
                                {
                                    MessageBuilder.AppendLine("<div class=\"get_item\">→" + GetCharacter.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(PresentItemID, false)) + "を" + CountItemStok + "手に入れた。</div>");
                                }
                                else
                                {
                                    MessageBuilder.AppendLine("<div class=\"get_item\">→" + GetCharacter.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(PresentItemID, false)) + "を持てなかったため、諦めた…。</div>");
                                }
                            }
                        }
                        else if (LibParty.CheckPartyOfficialEvent(PartyNo) && LibQuest.OfficialEventID == Status.OfficialEvent.SummerFestival)
                        {
                            MessageBuilder.AppendLine("<div class=\"have_item\">浴衣姿の女性<br />「お疲れ様でした！」<br /><br />浴衣姿の女性が、タオルと共に浴衣を差し出してきた。</div>");

                            foreach (LibPlayer GetCharacter in Players)
                            {
                                int ItemCount = 1;
                                int RestItemCount = 0;
                                int PresentItemID = 0;

                                bool IsHq = false;
                                if (GetCharacter.MPDamageRate <= 40 || GetCharacter.TPDamageRate <= 40)
                                {
                                    IsHq = true;
                                }

                                GetCharacter.IsTresureGetting = true;

                                string CountItemStok = "";
                                if (ItemCount > 1)
                                {
                                    CountItemStok = ItemCount + "個";
                                }

                                PresentItemID = 4525;
                                if (IsHq)
                                {
                                    PresentItemID = 4527;
                                }

                                if (GetCharacter.AddItem(Status.ItemBox.Normal, PresentItemID, false, ref ItemCount, ref RestItemCount))
                                {
                                    MessageBuilder.AppendLine("<div class=\"get_item\">→" + GetCharacter.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(PresentItemID, false)) + "を" + CountItemStok + "手に入れた。</div>");
                                }
                                else
                                {
                                    ItemCount = 1;
                                    GetCharacter.AddItem(Status.ItemBox.Box, PresentItemID, false, ref ItemCount, ref RestItemCount);
                                    MessageBuilder.AppendLine("<div class=\"get_item\">→" + GetCharacter.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(PresentItemID, false)) + "を持てなかったため、テンポラリボックスに入れた。</div>");
                                }

                                PresentItemID = 4526;
                                ItemCount = 1;
                                if (IsHq)
                                {
                                    PresentItemID = 4528;
                                }

                                if (GetCharacter.AddItem(Status.ItemBox.Normal, PresentItemID, false, ref ItemCount, ref RestItemCount))
                                {
                                    MessageBuilder.AppendLine("<div class=\"get_item\">→" + GetCharacter.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(PresentItemID, false)) + "を" + CountItemStok + "手に入れた。</div>");
                                }
                                else
                                {
                                    ItemCount = 1;
                                    GetCharacter.AddItem(Status.ItemBox.Box, PresentItemID, false, ref ItemCount, ref RestItemCount);
                                    MessageBuilder.AppendLine("<div class=\"get_item\">→" + GetCharacter.NickName + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(PresentItemID, false)) + "を持てなかったため、テンポラリボックスに入れた。</div>");
                                }
                            }
                        }
                    }
                }
                #endregion

                MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");

                #region 戦闘結果保存
                LibBattleResult.Update(PartyNo, MarkID, BattleResult);
                #endregion

                #endregion

                using (StreamWriter sw = new StreamWriter(LibConst.OutputFolderParty + LibParty.PartyHTML(PartyNo), true, LibConst.FileEncod))
                {
                    sw.Write(MessageBuilder.ToString());
                }
            }
        }

        /// <summary>
        /// 戦闘終了判定
        /// </summary>
        /// <param name="BattleResult">戦闘結果</param>
        /// <param name="DefeatCondition">敗北条件</param>
        /// <param name="WinnerCondition">勝利条件</param>
        /// <param name="Turn">ターン</param>
        /// <returns>終了した場合true</returns>
        private bool CheckBattleEnd(ref int BattleResult, int DefeatCondition, int WinnerCondition, int Turn)
        {
            List<LibUnitBase> PlayersLive = new List<LibUnitBase>();
            List<LibUnitBase> MonsterLive = new List<LibUnitBase>();
            List<LibUnitBase> Monsters = new List<LibUnitBase>();
            List<LibUnitBase> NPCLive = new List<LibUnitBase>();

            PlayersLive = LibBattleCharacter.GetPlayers(BattleCharacerLive);
            MonsterLive = LibBattleCharacter.GetMonsters(BattleCharacerLive);
            Monsters = LibBattleCharacter.GetMonsters(BattleCharacer);
            NPCLive = LibBattleCharacter.GetNonPlayersNotPet(BattleCharacerLive);

            // 敗北条件チェック
            switch (DefeatCondition)
            {
                case Status.DefeatCondition.Annihilation:
                    if (PlayersLive.Count == 0)
                    {
                        BattleResult = Status.BattleResult.Lose;
                        return true;
                    }
                    break;
                case Status.DefeatCondition.FalkDefeat:
                    if (NPCLive.Count == 0)
                    {
                        BattleResult = Status.BattleResult.Lose;
                        return true;
                    }
                    break;
                case Status.DefeatCondition.TrunOver120:
                    if (Turn >= 12)
                    {
                        BattleResult = Status.BattleResult.Lose;
                        return true;
                    }
                    break;
                case Status.DefeatCondition.EndOver80:
                    if (Turn >= 8)
                    {
                        BattleResult = Status.BattleResult.Draw;
                        return true;
                    }
                    break;
            }

            // どう転んでもPC全滅＝敗北
            if (PlayersLive.Count == 0)
            {
                BattleResult = Status.BattleResult.Lose;
                return true;
            }

            // NOTE:敗北条件にNPC1がやられた、など適時条件を追加していくこと。
            // 汎用性にこだわる必要は皆無。

            // 勝利条件チェック
            switch (WinnerCondition)
            {
                case Status.WinnerCondition.Annihilation:
                    if (MonsterLive.Count == 0)
                    {
                        BattleResult = Status.BattleResult.Winner;
                        return true;
                    }
                    break;
                case Status.WinnerCondition.BoneCollectorDefeat:
                    {
                        LibUnitBase BoneUnit = Monsters.Find(RaUn => RaUn.EntryNo == 205);

                        if (BoneUnit != null && BoneUnit.BattleOut)
                        {
                            BattleResult = Status.BattleResult.Winner;
                            return true;
                        }
                    }
                    break;
                case Status.WinnerCondition.UndeadMachineDefeat:
                    {
                        LibUnitBase Unit = Monsters.Find(RaUn => RaUn.EntryNo == 51);

                        if (Unit != null && Unit.BattleOut)
                        {
                            BattleResult = Status.BattleResult.Winner;
                            return true;
                        }
                    }
                    break;
            }

            return false;
        }

        private DataRow SelectPopMonster(DataView PopMonsterView, int MaxProb)
        {
            int SetProb = LibInteger.GetRandMax(1, MaxProb);
            DataRow PopRow = null;

            foreach (DataRowView PopMonsterViewRow in PopMonsterView)
            {
                if ((int)PopMonsterViewRow["prob_min"] <= SetProb && SetProb <= (int)PopMonsterViewRow["prob_max"])
                {
                    PopRow = PopMonsterViewRow.Row;
                    break;
                }
            }

            if (PopRow == null) { return SelectPopMonster(PopMonsterView, MaxProb); }
            else { return PopRow; }
        }
    }
}
