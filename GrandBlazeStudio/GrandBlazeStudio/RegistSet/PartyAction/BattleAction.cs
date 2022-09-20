using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.SpecialEntity;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        private List<LibUnitBase> Friends = new List<LibUnitBase>();
        private List<LibUnitBase> Enemys = new List<LibUnitBase>();
        private List<LibUnitBase> FriendsLive = new List<LibUnitBase>();
        private List<LibUnitBase> EnemysLive = new List<LibUnitBase>();

        /// <summary>
        /// 戦闘行動
        /// </summary>
        /// <param name="Mine">実行者</param>
        /// <param name="MessageBuilder">メッセージ格納</param>
        /// <param name="QuestName">クエスト名</param>
        private void BattleAction(LibUnitBase Mine, StringBuilder MessageBuilder, string QuestName, int Turn)
        {
            bool IsActionAttack = true;// 攻撃を実行する。準備のみ、様子見の場合はスキップ

            MessageBuilder.AppendLine("<dl class=\"btact " + Mine.PartyColor + "\">");

            #region ステータス異常行動キャンセル判定
            if (Mine.StatusEffect.Check(2))
            {
                // 睡眠
                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は眠っている…。</dt>");
                IsActionAttack = false;
            }

            if (Mine.StatusEffect.Check(8) &&
                Mine.SelectedActions.Count > 0 &&
                !Mine.SelectedActions[0].IsNormalAttack &&
                (Mine.SelectedActions[0].AttackType == Status.AttackType.Mystic ||
                Mine.SelectedActions[0].AttackType == Status.AttackType.Summon ||
                Mine.SelectedActions[0].AttackType == Status.AttackType.MagicSword ||
                Mine.SelectedActions[0].AttackType == Status.AttackType.Ninjutsu ||
                Mine.SelectedActions[0].AttackType == Status.AttackType.Song))
            {
                // 沈黙
                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は沈黙している。</dt>");
                IsActionAttack = false;
            }

            if (Mine.StatusEffect.Check(9) &&
                Mine.SelectedActions.Count > 0 &&
                !Mine.SelectedActions[0].IsNormalAttack &&
                (Mine.SelectedActions[0].AttackType == Status.AttackType.Item ||
                Mine.SelectedActions[0].AttackType == Status.AttackType.Dance ||
                Mine.SelectedActions[0].AttackType == Status.AttackType.Combat ||
                Mine.SelectedActions[0].AttackType == Status.AttackType.MagicSword ||
                Mine.SelectedActions[0].AttackType == Status.AttackType.Shoot))
            {
                // 骨折
                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は骨折している。</dt>");
                IsActionAttack = false;
            }

            if (Mine.StatusEffect.Check(16) &&
                Mine.SelectedActions.Count > 0 &&
                Mine.SelectedActions[0].Range == Status.Range.Short &&
                Mine.SelectedActions[0].TargetParty != Status.TargetParty.Mine)
            {
                // 影縛
                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は身動きできない。</dt>");
                IsActionAttack = false;
            }

            if (Mine.StatusEffect.Check(10))
            {
                // 石化
                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は石になっている。</dt>");
                IsActionAttack = false;
            }

            if (Mine.StatusEffect.Check(12))
            {
                // 混乱
                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は混乱している。</dt>");
                switch (LibInteger.GetRandMax(15))
                {
                    case 0:
                        MessageBuilder.AppendLine("<dd>地面に「の」の字を描いていじけている！</dd>");
                        break;
                    case 1:
                        MessageBuilder.AppendLine("<dd>夕日に向かって吠えながら走り出した！</dd>");
                        break;
                    case 2:
                        MessageBuilder.AppendLine("<dd>ヒラリヒラリと不思議な踊りを踊り始めた！</dd>");
                        break;
                    case 3:
                        MessageBuilder.AppendLine("<dd>「実家に帰らせていただきます」という突然の宣言と共に突然荷物をまとめはじめた！</dd>");
                        break;
                    case 4:
                        MessageBuilder.AppendLine("<dd>意味もなく笑い出し、笑いすぎて咳き込んでいる！</dd>");
                        break;
                    case 5:
                        MessageBuilder.AppendLine("<dd>ホームラン宣言を行い、バッティングフォームにうつった！</dd>");
                        break;
                    case 6:
                        MessageBuilder.AppendLine("<dd>突如「フォォォーッ！」と叫び、腰を亜光速で動かし始めた！</dd>");
                        break;
                    case 7:
                        MessageBuilder.AppendLine("<dd>アクロバットを披露し始め、自分に酔っている！</dd>");
                        break;
                    case 8:
                        MessageBuilder.AppendLine("<dd>コサックダンスを踊り出した！</dd>");
                        break;
                    case 9:
                        MessageBuilder.AppendLine("<dd>感動の涙を流し始め大声で泣き叫んでいる！</dd>");
                        break;
                    case 10:
                        MessageBuilder.AppendLine("<dd>「ガーキだーいしょーーー！」　どうやら自分をガキ大将と勘違いしているようだ！</dd>");
                        break;
                    case 11:
                        MessageBuilder.AppendLine("<dd>突然、服を脱ぎだし「ちょっとだけよぉん」などと言い出した！</dd>");
                        break;
                    case 12:
                        MessageBuilder.AppendLine("<dd>盗んだバイクで走り出した！！</dd>");
                        break;
                    case 13:
                        MessageBuilder.AppendLine("<dd>とてつもなく寒いギャグを言い出した！　場の空気が凍っていることにまるで気付いていない！！</dd>");
                        break;
                    case 14:
                        MessageBuilder.AppendLine("<dd>敵の前に飛び出し、「僕はしにませぇぇん！」と言い出した！</dd>");
                        break;
                    case 15:
                        MessageBuilder.AppendLine("<dd>何かを念じている！　奥義を出そうとしているようだ……ぷぅ。　……うわっ、クサッ！！</dd>");
                        break;
                }
                IsActionAttack = false;
            }

            if (Mine.StatusEffect.Check(13))
            {
                // 恐怖
                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は恐怖で動けない。</dt>");
                IsActionAttack = false;
            }

            if (Mine.StatusEffect.Check(19))
            {
                // スタン
                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "はスタンしているため行動できない。</dt>");
                IsActionAttack = false;
                Mine.StatusEffect.Delete(19);
            }

            if (Mine.StatusEffect.Check(21))
            {
                if (35m > LibInteger.GetRandBasis())
                {
                    // 石化中
                    MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は石化してしまった！</dt>");
                    IsActionAttack = false;
                    Mine.StatusEffect.Delete(21);
                    Mine.StatusEffect.Add(10, 1, 1, -1, 99, true);
                }
                else
                {
                    MessageBuilder.AppendLine("<dt class=\"nob\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "の石化が進行している…。</dt>");
                }
            }

            if (Mine.StatusEffect.Check(22))
            {
                if (35m > LibInteger.GetRandBasis())
                {
                    // 死霊の誘い
                    MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は戦闘不能になってしまった！</dt>");
                    IsActionAttack = false;
                    Mine.StatusEffect.Delete(22);
                    MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "は倒れた…。</dd>");
                    BattleCommon.DeadMans(Mine, BattleCharacer);
                }
                else
                {
                    MessageBuilder.AppendLine("<dt class=\"nob\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "に死霊の誘いが迫っている…。</dt>");
                }
            }

            if (!IsActionAttack)
            {
                // 詠唱・準備中断処理（メッセージはいれない）
                Mine.ChargeTime = 0;
                Mine.SelectedActions.Clear();
            }
            #endregion

            #region 敵と味方の判定
            Friends = new List<LibUnitBase>();
            Enemys = new List<LibUnitBase>();
            FriendsLive = new List<LibUnitBase>();
            EnemysLive = new List<LibUnitBase>();

            if (Mine.PartyBelong == Status.Belong.Enemy)
            {
                // モンスターの場合
                Friends = LibBattleCharacter.GetMonsters(BattleCharacer);
                Enemys = LibBattleCharacter.GetFriendry(BattleCharacer);

                FriendsLive = LibBattleCharacter.GetLive(Friends);
                EnemysLive = LibBattleCharacter.GetLive(Enemys);
            }
            else
            {
                // 味方の場合
                Friends = LibBattleCharacter.GetFriendry(BattleCharacer);
                Enemys = LibBattleCharacter.GetMonsters(BattleCharacer);

                FriendsLive = LibBattleCharacter.GetLive(Friends);
                EnemysLive = LibBattleCharacter.GetLive(Enemys);
            }
            #endregion

            #region 行動内容の確定(とターゲットの確定)
            int SelectedAction = Mine.SelectedActions.Count > 0 ? Mine.SelectedActions[0].ActionType : Status.ActionType.Default;

            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(1100);
                if (EffectRow != null)
                {
                    // リシンク
                    BattleInitiativeSet(Mine, 2);
                    if ((int)EffectRow.rank != 2)
                    {
                        // rank=2の時はメッセージを出さない
                        MessageBuilder.AppendLine("<dt class=\"nob\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "のリシンク！</dt>");
                        SelectedAction = Mine.SelectedActions.Count > 0 ? Mine.SelectedActions[0].ActionType : Status.ActionType.Default;
                    }
                }
            }

            if (Mine.SelectedTarget.Count > 0 && Mine.SelectedTarget[0].BattleOut && SelectedAction != Status.ActionType.Default && Mine.SelectedActions[0].EffectList.FindByeffect_id(301) == null)
            {
                // 戦闘不能でした
                if (EnemysLive.Count > 0)
                {
                    // なのでリシンク
                    BattleInitiativeSet(Mine, 2);
                    MessageBuilder.AppendLine("<dt class=\"nob\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "はターゲットを変更！</dt>");
                    SelectedAction = Mine.SelectedActions.Count > 0 ? Mine.SelectedActions[0].ActionType : Status.ActionType.Default;
                }
                else
                {
                    IsActionAttack = false;
                }
            }
            #endregion

            // 行動内容アーツDataTable
            List<LibActionType> ActionArtsRows = new List<LibActionType>(Mine.SelectedActions);
            List<LibUnitBase> TempTargetBattle = new List<LibUnitBase>(Mine.SelectedTarget);

            #region ステータス異常行動キャンセル判定(麻痺)
            if (SelectedAction >= Status.ActionType.MainAttack && SelectedAction < Status.ActionType.ArtsAttack)
            {
                if (Mine.StatusEffect.Check(6) && 40m > LibInteger.GetRandBasis())
                {
                    // 麻痺
                    MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は麻痺している。</dt>");
                    IsActionAttack = false;
                }

                if (!IsActionAttack)
                {
                    // 詠唱・準備中断処理（メッセージはいれない）
                    Mine.SelectedActions.Clear();
                    ActionArtsRows.Clear();
                }
            }
            #endregion

            #region ターゲットいるの？
            if (TempTargetBattle.Count <= 0)
            {
                // ターゲット見つからず
                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "はアクションのターゲットが見つからない！</dt>");
                IsActionAttack = false;
                ActionArtsRows = new List<LibActionType>();
            }
            #endregion

            #region 行動内容の開始表示(の攻撃！etc) & 攻撃セリフ表示
            if (ActionArtsRows.Count > 0)
            {
                LibActionType DefaultAction = ActionArtsRows[0];
                int Actions = SelectedAction;

                string ArtsName = DefaultAction.ArtsName;

                switch (Actions)
                {
                    case Status.ActionType.NoAction:
                        MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は待機している…。</dt>");
                        IsActionAttack = false;
                        break;
                    case Status.ActionType.MainAttack:
                        MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "の攻撃！</dt>");
                        if (LibInteger.GetRandMax(100) <= 75)
                        {
                            MessageBuilder.AppendLine("<dd>" + LibSerif.Serif(Mine, LibSituation.GetNo("通常攻撃の実行時"), null, QuestName, TempTargetBattle[0]) + "</dd>");
                        }
                        break;
                    default:
                        // 実行

                        switch (DefaultAction.AttackType)
                        {
                            case Status.AttackType.Combat:
                            case Status.AttackType.Shoot:
                            case Status.AttackType.Bless:
                                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "の" + LibResultText.CSSEscapeActArts(ArtsName) + "！</dt>");
                                break;
                            case Status.AttackType.Mystic:
                            case Status.AttackType.Summon:
                            case Status.AttackType.Ninjutsu:
                            case Status.AttackType.MagicSword:
                                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "の" + LibResultText.CSSEscapeActArts(ArtsName) + "！</dt>");
                                break;
                            case Status.AttackType.Item:
                                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "の" + LibResultText.CSSEscapeActArts(ArtsName) + "！</dt>");
                                break;
                            case Status.AttackType.Dance:
                            case Status.AttackType.Song:
                                MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "の" + LibResultText.CSSEscapeActArts(ArtsName) + "！</dt>");
                                break;
                        }

                        #region セリフ
                        switch (Actions)
                        {
                            case Status.ActionType.ArtsAttack:
                                MessageBuilder.AppendLine("<dd>" + LibSerif.Serif(Mine, LibSituation.GetNo("アーツの発動"), DefaultAction.SkillID, QuestName, TempTargetBattle[0]) + "</dd>");
                                break;
                            case Status.ActionType.SpecialArtsAttack:
                                MessageBuilder.AppendLine("<dd>" + LibSerif.Serif(Mine, LibSituation.GetNo("スペシャル発動"), null, QuestName, TempTargetBattle[0]) + "</dd>");
                                break;
                        }
                        #endregion
                        break;
                }
            }
            else
            {
                if (IsActionAttack)
                {
                    // 様子を見ている
                    MessageBuilder.AppendLine("<dt>" + LibResultText.CSSEscapeChara(Mine.NickName) + "は待機している…。</dt>");
                }
            }
            #endregion

            #region 隊列の修正
            if (Mine.Formation != Mine.BattleFormation)
            {
                if (Mine.UNQ > LibInteger.GetRandBasis())
                {
                    Mine.BattleFormation = Mine.Formation;
                    MessageBuilder.AppendLine("<dd>乱されていた隊列を整えた！</dd>");
                }
            }
            #endregion

            // 攻撃の実行
            if (IsActionAttack)
            {
                BattleAttack(Mine, ActionArtsRows, TempTargetBattle, MessageBuilder, QuestName, Turn);
            }

            #region スリップ
            if (!Mine.BattleOut)
            {
                // 猛毒
                if (Mine.StatusEffect.Check(3))
                {
                    int PoisonDamage = (int)((decimal)Mine.HPMax * Mine.StatusEffect.GetRank(3) / 16m);

                    if (Mine.HPNow <= PoisonDamage)
                    {
                        PoisonDamage = Mine.HPNow;
                    }
                    Mine.HPNow -= PoisonDamage;

                    MessageBuilder.AppendLine("<dd>猛毒効果→" + LibResultText.CSSEscapeChara(Mine.NickName) + "に" + PoisonDamage + "のダメージ。</dd>");

                    if (Mine.HPNow <= 0)
                    {
                        MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "は倒れた…。</dd>");
                        BattleCommon.DeadMans(Mine, BattleCharacer);
                    }
                }

                // 呪詛
                if (Mine.StatusEffect.Check(5))
                {
                    int CurseMP = (int)((decimal)Mine.MPMax * Mine.StatusEffect.GetRank(5) / 16m);
                    int CurseTP = (int)((decimal)Mine.TPMax * Mine.StatusEffect.GetRank(5) / 16m); ;

                    if (Mine.MPNow <= CurseMP)
                    {
                        CurseMP = Mine.MPNow;
                    }
                    if (Mine.TPNow <= CurseTP)
                    {
                        CurseTP = Mine.TPNow;
                    }
                    Mine.MPNow -= CurseMP;
                    Mine.TPNow -= CurseTP;

                    MessageBuilder.AppendLine("<dd>呪詛効果→" + LibResultText.CSSEscapeChara(Mine.NickName) + "のＭＰに" + CurseMP + "のダメージ。</dd>");
                    MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Mine.NickName) + "のＴＰに" + CurseTP + "のダメージ。</dd>");
                }

                // ブラッドロス
                if (Mine.StatusEffect.Check(24))
                {
                    int BloodSrip = (int)((decimal)Mine.HPMax * Mine.StatusEffect.GetRank(24) / 32m);

                    if (Mine.HPNow <= BloodSrip)
                    {
                        BloodSrip = Mine.HPNow;
                    }
                    Mine.HPNow -= BloodSrip;

                    MessageBuilder.AppendLine("<dd>ブラッドロス効果→" + LibResultText.CSSEscapeChara(Mine.NickName) + "に" + BloodSrip + "のダメージ。</dd>");

                    if (Mine.HPNow <= 0)
                    {
                        MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "は倒れた…。</dd>");
                        BattleCommon.DeadMans(Mine, BattleCharacer);
                    }
                }

                // リジェネ
                if (Mine.StatusEffect.Check(50))
                {
                    decimal Rank = Mine.StatusEffect.GetRank(50);
                    int Rejeneration = LibInteger.GetRandMax((int)Math.Max(1m, (decimal)Mine.Level * 0.5m * (Rank - 1m)), (int)((decimal)Mine.Level * (1m + 0.5m * (Rank - 1m))));

                    Mine.HPNow += Rejeneration;

                    MessageBuilder.AppendLine("<dd>リジェネ効果→" + LibResultText.CSSEscapeChara(Mine.NickName) + "のＨＰが" + Rejeneration + "回復。</dd>");
                }

                // ビーストフォーム
                if (Mine.StatusEffect.Check(266))
                {
                    int Rejeneration = (int)((decimal)Mine.HPMax * 0.1m);

                    Mine.HPNow += Rejeneration;

                    MessageBuilder.AppendLine("<dd>傷が徐々に癒えていく……！→" + LibResultText.CSSEscapeChara(Mine.NickName) + "のＨＰが" + Rejeneration + "回復。</dd>");
                }

                // マナライズ
                if (Mine.StatusEffect.Check(51))
                {
                    decimal Rank = Mine.StatusEffect.GetRank(51);
                    int Manalize = LibInteger.GetRandMax((int)Math.Max(1m, (decimal)Mine.Level * 0.25m * (Rank - 1m)), (int)((decimal)Mine.Level * (0.5m + 0.25m * (Rank - 1m))));

                    Mine.MPNow += Manalize;

                    MessageBuilder.AppendLine("<dd>マナライズ効果→" + LibResultText.CSSEscapeChara(Mine.NickName) + "のＭＰが" + Manalize + "回復。</dd>");
                }

                // リフレッシュ
                if (Mine.StatusEffect.Check(52))
                {
                    decimal Rank = Mine.StatusEffect.GetRank(52);
                    int Refresh = LibInteger.GetRandMax((int)Math.Max(1m, (decimal)Mine.Level * 0.125m * (Rank - 1m)), (int)((decimal)Mine.Level * (0.25m + 0.125m * (Rank - 1m))));

                    Mine.TPNow += Refresh;

                    MessageBuilder.AppendLine("<dd>リフレッシュ効果→" + LibResultText.CSSEscapeChara(Mine.NickName) + "のＴＰが" + Refresh + "回復。</dd>");
                }

                // HPスリップ（フィールド効果）
                if (FieldRow.auto_hpsrip && !Mine.BattleOut)
                {
                    int PoisonDamage = (int)((decimal)Mine.HPMax * 1m / 16m);

                    if (Mine.HPNow <= PoisonDamage)
                    {
                        PoisonDamage = Mine.HPNow;
                    }
                    Mine.HPNow -= PoisonDamage;

                    MessageBuilder.AppendLine("<dd>フィールド効果【ＨＰスリップ】→" + LibResultText.CSSEscapeChara(Mine.NickName) + "に" + PoisonDamage + "のダメージ。</dd>");

                    if (Mine.HPNow <= 0)
                    {
                        MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "は倒れた…。</dd>");
                        BattleCommon.DeadMans(Mine, BattleCharacer);
                    }
                }
                // MPスリップ（フィールド効果）
                if (FieldRow.auto_mpsrip && !Mine.BattleOut)
                {
                    int PoisonDamage = (int)((decimal)Mine.MPMax * 1m / 16m);

                    if (Mine.MPNow <= PoisonDamage)
                    {
                        PoisonDamage = Mine.MPNow;
                    }
                    Mine.MPNow -= PoisonDamage;

                    MessageBuilder.AppendLine("<dd>フィールド効果【ＭＰスリップ】→" + LibResultText.CSSEscapeChara(Mine.NickName) + "に" + PoisonDamage + "のＭＰダメージ。</dd>");
                }
                // TPスリップ（フィールド効果）
                if (FieldRow.auto_tpsrip && !Mine.BattleOut)
                {
                    int PoisonDamage = (int)((decimal)Mine.TPMax * 1m / 16m);

                    if (Mine.TPNow <= PoisonDamage)
                    {
                        PoisonDamage = Mine.TPNow;
                    }
                    Mine.TPNow -= PoisonDamage;

                    MessageBuilder.AppendLine("<dd>フィールド効果【ＴＰスリップ】→" + LibResultText.CSSEscapeChara(Mine.NickName) + "に" + PoisonDamage + "のＴＰダメージ。</dd>");
                }
            }
            #endregion

            // チェイン解除
            Mine.ChainList.Clear();

            // 行動完了
            Mine.IsActionEnd = true;

            MessageBuilder.AppendLine("</dl>");
        }
    }
}
