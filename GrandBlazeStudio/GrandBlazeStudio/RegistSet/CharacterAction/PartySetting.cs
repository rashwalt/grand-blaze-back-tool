using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// パーティ設定
        /// </summary>
        private void PartySetting()
        {
            int i;
            int AreaID = 0;

            // ウェディングパーティの解散
            DataTable WeddingPartyTable = LibParty.GetWeddingParty();
            foreach (PartySettingEntity.ts_party_listRow WeddingRow in WeddingPartyTable.Rows)
            {
                int PartyNo = WeddingRow.party_no;
                DataTable WeddingMembers = LibParty.GetPartyMembers(PartyNo);

                foreach (DataRow WeddingMenberRow in WeddingMembers.Rows)
                {
                    if (LibParty.PartyMemberCount(PartyNo) > 1)
                    {
                        AreaID = LibParty.GetEntryAreaID((int)WeddingMenberRow["entry_no"]);
                        // 人数が一人以上（パーティの場合）のみ
                        int NewPartyNo = LibParty.SetNewParty((int)WeddingMenberRow["entry_no"], AreaID);
                        LibParty.SetPartyAreaID(NewPartyNo, AreaID);
                    }
                    else
                    {
                        AreaID = LibParty.GetEntryAreaID((int)WeddingMenberRow["entry_no"]);
                        int NewPartyNo = LibParty.GetPartyNo((int)WeddingMenberRow["entry_no"]);
                        LibParty.SetPartyAreaID(NewPartyNo, AreaID);
                    }
                }
            }

            // 一時パーティの解散
            DataTable TempPartyTable = LibParty.GetTempParty();
            foreach (PartySettingEntity.ts_party_listRow TempRow in TempPartyTable.Rows)
            {
                int PartyNo = (int)TempRow["party_no"];
                DataTable TempMembers = LibParty.GetPartyMembers(PartyNo);

                foreach (DataRow TempMenberRow in TempMembers.Rows)
                {
                    if (LibParty.PartyMemberCount(PartyNo) > 1)
                    {
                        AreaID = LibParty.GetEntryAreaID((int)TempMenberRow["entry_no"]);
                        // 人数が一人以上（パーティの場合）のみ
                        int NewPartyNo = LibParty.SetNewParty((int)TempMenberRow["entry_no"], AreaID);
                        LibParty.SetPartyAreaID(NewPartyNo, AreaID);
                    }
                    else
                    {
                        AreaID = LibParty.GetEntryAreaID((int)TempMenberRow["entry_no"]);
                        int NewPartyNo = LibParty.GetPartyNo((int)TempMenberRow["entry_no"]);
                        LibParty.ClearTempParty(NewPartyNo);
                        LibParty.SetPartyAreaID(NewPartyNo, AreaID);
                    }
                }
            }

            // 離脱実行
            ContinueDataEntity.ts_continue_mainRow[] ContinueMainGoodbyeRows = (ContinueDataEntity.ts_continue_mainRow[])con.Entity.ts_continue_main.Select("party_secession=1");

            foreach (ContinueDataEntity.ts_continue_mainRow SecessionRow in ContinueMainGoodbyeRows)
            {
                int PartyNo = LibParty.GetPartyNo(SecessionRow.entry_no);

                if (LibParty.PartyMemberCount(PartyNo) > 1)
                {
                    AreaID = LibParty.GetPartyMarkID(PartyNo);
                    // 人数が一人以上（パーティの場合）のみ
                    int NewPartyNo = LibParty.SetNewParty(SecessionRow.entry_no, AreaID);
                    LibParty.SetPartyAreaID(NewPartyNo, AreaID);
                }
            }

            // 未継続回数の超過
            foreach (LibPlayer Target in CharaList.FindAll(Del => Del.ContinueNoCount == 13))
            {
                int PartyNo = LibParty.GetPartyNo(Target.EntryNo);

                if (LibParty.PartyMemberCount(PartyNo) > 1)
                {
                    AreaID = LibParty.GetPartyMarkID(PartyNo);
                    // 人数が一人以上（パーティの場合）のみ
                    LibPlayerMemo.AddSystemMessage(Target.EntryNo, Status.PlayerSysMemoType.PartySetting, Target.NickName + "の未継続回数が13回に達したため、パーティからの自動離脱が行われます。", Status.MessageLevel.Caution);
                    int NewPartyNo = LibParty.SetNewParty(Target.EntryNo, AreaID);
                    LibParty.SetPartyAreaID(NewPartyNo, AreaID);
                }
            }

            // イベント用パーティ離脱
            if (LibQuest.OfficialEventID == Status.OfficialEvent.WeddingSupport)
            {
                // パーティ離脱実行

                foreach (ContinueDataEntity.ts_continue_official_eventRow SecessionRow in con.Entity.ts_continue_official_event)
                {
                    int PartyNo = LibParty.GetPartyNo(SecessionRow.entry_no);

                    if (LibParty.PartyMemberCount(PartyNo) > 1)
                    {
                        AreaID = LibParty.GetPartyMarkID(PartyNo);
                        // 人数が一人以上（パーティの場合）のみ
                        int NewPartyNo = LibParty.GetNewPartyNo();

                        string PartyName = "ザ・ウェディング";

                        LibParty.RegistParty(NewPartyNo, PartyName, 0, true, true);
                        LibParty.RegistBelongParty(NewPartyNo, SecessionRow.entry_no, true, SecessionRow.groom, SecessionRow.bride, AreaID, true);
                    }
                }

                // パーティ加入実行

                int PartyReader = 0;

                con.Entity.ts_continue_official_event.DefaultView.RowFilter = "groom=true";

                if (con.Entity.ts_continue_official_event.DefaultView.Count > 0)
                {
                    PartyReader = (int)con.Entity.ts_continue_official_event.DefaultView[0]["entry_no"];

                    con.Entity.ts_continue_official_event.DefaultView.RowFilter = "groom=false";
                    con.Entity.ts_continue_official_event.DefaultView.Sort = "bride,entry_no";

                    int NowPartyNo = LibParty.GetPartyNo(PartyReader);
                    LibParty.RegistBelongParty(NowPartyNo, PartyReader, true, true, false, 0, true);

                    foreach (DataRowView AddInRow in con.Entity.ts_continue_official_event.DefaultView)
                    {
                        LibParty.SetInParty(NowPartyNo, (int)AddInRow["entry_no"], true);
                    }

                    LibParty.SetPartyOfficialEvent(NowPartyNo);
                    LibParty.SetPartyName(NowPartyNo, "ザ・ウェディング");
                }

                con.UpdateWeddingPartyNothings();
                con = new LibContinue();// 再読み込み
            }

            // 加入設定
            ContinueDataEntity.ts_continue_mainRow[] ContinueMainComeonRows = (ContinueDataEntity.ts_continue_mainRow[])con.Entity.ts_continue_main.Select("pcm_add_1>0 or pcm_add_2>0 or pcm_add_3>0 or pcm_add_4>0 or pcm_add_5>0");

            foreach (ContinueDataEntity.ts_continue_mainRow AddInRow in ContinueMainComeonRows)
            {
                // 人数確認
                int EntryNo = AddInRow.entry_no;
                int NowPartyNo = LibParty.GetPartyNo(AddInRow.entry_no);
                int PartyMemberCount = LibParty.PartyMemberCount(NowPartyNo);
                string MineName = CharaMini.GetNickNameWithLink(EntryNo, 1);
                bool IsTemp = false;

                if (PartyMemberCount == 0)
                {
                    // 0人の場合はエラーとしてスキップ
                    continue;
                }

                for (i = 1; i <= 5; i++)
                {
                    int TargetEntryNo = (int)AddInRow["pcm_add_" + i];
                    if (TargetEntryNo <= 0)
                    {
                        // 誘う相手を指定していない場合スキップ
                        continue;
                    }

                    if (TargetEntryNo == EntryNo)
                    {
                        // 自分自身を誘っている。
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, "自分自身を誘うことはできない！", Status.MessageLevel.Error);
                        continue;
                    }

                    if (!CharaMini.CheckInChara(TargetEntryNo))
                    {
                        // 誘う相手が存在しない場合
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, "E-No." + TargetEntryNo + "には、登録されている冒険者が存在しなかった…。<br />※ヒント: 誘う相手のE-No.を間違えていませんか？ 誘う相手のE-No.を再度確認してみましょう！", Status.MessageLevel.Error);
                        continue;
                    }

                    // 誘おうとしている相手の名前取得
                    string TargetNickName = CharaMini.GetNickNameWithLink(TargetEntryNo, 1);

                    int TargetPartyNo = LibParty.GetPartyNo(TargetEntryNo);
                    if (TargetPartyNo == NowPartyNo)
                    {
                        // 誘う相手が既に同じパーティか
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, TargetNickName + "をパーティに誘えませんでした。<br />相手はすでにパーティの一員です。", Status.MessageLevel.Error);
                        continue;
                    }

                    if (PartyMemberCount == 6)
                    {
                        // フルメンバーの場合
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, TargetNickName + "をパーティに誘えない！<br />パーティはすでにフルメンバーであるため、新たに誘うことができなかった…。", Status.MessageLevel.Error);
                        continue;
                    }

                    // 誘う相手の登録状況確認
                    ContinueDataEntity.ts_continue_mainRow ContinueMainReciveRow = con.Entity.ts_continue_main.FindByentry_no(TargetEntryNo);

                    if (ContinueMainReciveRow == null)
                    {
                        // 誘う相手が登録していない
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, TargetNickName + "をパーティに誘った。<br />しかし、相手は継続登録書をギルドに提出していないため、ギルドにパーティ認定されなかった。", Status.MessageLevel.Caution);
                        LibPlayerMemo.AddSystemMessage(TargetEntryNo, Status.PlayerSysMemoType.PartySetting, MineName + "からパーティに誘われた。<br />しかし、継続登録書をギルドに提出していないため、ギルドにパーティ認定されなかった。", Status.MessageLevel.Caution);
                        continue;
                    }

                    if (ContinueMainReciveRow.party_hope != 1)
                    {
                        // 誘う相手が誘い受入れしていない場合
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, TargetNickName + "をパーティに誘ったが、断られた。<br />誰からの誘いも断るつもりのようだ。", Status.MessageLevel.Caution);
                        LibPlayerMemo.AddSystemMessage(TargetEntryNo, Status.PlayerSysMemoType.PartySetting, MineName + "からパーティに誘われたが、断った。<br />※パーティ参加希望を出していません。", Status.MessageLevel.Caution);
                        continue;
                    }

                    if (ContinueMainReciveRow.option_comes_no > 0 && ContinueMainReciveRow.option_comes_no != EntryNo)
                    {
                        // 誘われる相手を指定している場合
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, TargetNickName + "をパーティに誘ったが、断られた。<br />誰かの誘いを待っているようだ。", Status.MessageLevel.Caution);
                        LibPlayerMemo.AddSystemMessage(TargetEntryNo, Status.PlayerSysMemoType.PartySetting, MineName + "からパーティに誘われたが、断った。<br />※指定した人物以外の誘いでした。", Status.MessageLevel.Caution);
                        continue;
                    }

                    if (CharaMini.CheckNewPlayer(EntryNo))
                    {
                        // 自分が新規登録者の場合、相手も新規登録者じゃないとだめ
                        if (!CharaMini.CheckNewPlayer(TargetEntryNo))
                        {
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, TargetNickName + "をパーティに誘ったが、認可が下りなかった。<br />※新規登録者は同じ新規登録者の場合のみ、誘うことが出来ます。", Status.MessageLevel.Caution);
                            LibPlayerMemo.AddSystemMessage(TargetEntryNo, Status.PlayerSysMemoType.PartySetting, MineName + "からパーティに誘われたが、認可が下りなかった。<br />※新規登録者は同じ新規登録者の場合のみ、誘われることが出来ます。", Status.MessageLevel.Caution);
                            continue;
                        }
                    }
                    else
                    {
                        // 自分が新規登録者でない場合、新規登録者は誘えない
                        if (CharaMini.CheckNewPlayer(TargetEntryNo))
                        {
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, TargetNickName + "をパーティに誘ったが、認可が下りなかった。<br />※新規登録者は同じ新規登録者の場合のみ、誘うことが出来ます。", Status.MessageLevel.Caution);
                            LibPlayerMemo.AddSystemMessage(TargetEntryNo, Status.PlayerSysMemoType.PartySetting, MineName + "からパーティに誘われたが、認可が下りなかった。<br />※新規登録者は同じ新規登録者の場合のみ、誘われることが出来ます。", Status.MessageLevel.Caution);
                            continue;
                        }
                    }

                    // 相手を誘い入れる
                    LibParty.SetInParty(NowPartyNo, TargetEntryNo, IsTemp);
                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, TargetNickName + "をパーティに誘った。<br />" + TargetNickName + "がパーティメンバーになりました。", Status.MessageLevel.Normal);
                    LibPlayerMemo.AddSystemMessage(TargetEntryNo, Status.PlayerSysMemoType.PartySetting, MineName + "からパーティに誘われた。<br />" + MineName + "のパーティメンバーになりました。", Status.MessageLevel.Normal);

                    if (IsTemp)
                    {
                        LibParty.SetTempParty(NowPartyNo);
                    }
                }
            }

            // ランダムパーティの結成
            ContinueDataEntity.ts_continue_mainRow[] ContinueMainRandomRows = (ContinueDataEntity.ts_continue_mainRow[])con.Entity.ts_continue_main.Select("party_hope=2");
            DataTable RandomPartyMember = new DataTable();
            RandomPartyMember.Columns.Add("entry_no", typeof(int));
            RandomPartyMember.Columns.Add("random", typeof(int));
            RandomPartyMember.Columns.Add("is_party", typeof(bool));
            RandomPartyMember.Columns.Add("target_mark", typeof(int));

            DataTable RandomPartyMemberNewbie = new DataTable();
            RandomPartyMemberNewbie.Columns.Add("entry_no", typeof(int));
            RandomPartyMemberNewbie.Columns.Add("random", typeof(int));
            RandomPartyMemberNewbie.Columns.Add("is_party", typeof(bool));

            foreach (ContinueDataEntity.ts_continue_mainRow RandRow in ContinueMainRandomRows)
            {
                int NowPartyNo = LibParty.GetPartyNo(RandRow.entry_no);
                int PartyMemberCount = LibParty.PartyMemberCount(NowPartyNo);

                if (PartyMemberCount == 1)
                {
                    if (CharaMini.CheckNewPlayer(RandRow.entry_no))
                    {
                        DataRow NewEntryRow = RandomPartyMemberNewbie.NewRow();

                        NewEntryRow["entry_no"] = RandRow.entry_no;
                        NewEntryRow["random"] = LibInteger.GetRandMax(256);
                        NewEntryRow["is_party"] = false;

                        RandomPartyMemberNewbie.Rows.Add(NewEntryRow);
                    }
                    else
                    {
                        DataRow NewEntryRow = RandomPartyMember.NewRow();

                        NewEntryRow["entry_no"] = RandRow.entry_no;
                        NewEntryRow["random"] = LibInteger.GetRandMax(256);
                        NewEntryRow["is_party"] = false;
                        NewEntryRow["target_mark"] = RandRow.mark_id;

                        RandomPartyMember.Rows.Add(NewEntryRow);
                    }
                }
            }

            // 自動補充
            ContinueDataEntity.ts_continue_mainRow[] ContinueMainRandomFreeJoinRows = (ContinueDataEntity.ts_continue_mainRow[])con.Entity.ts_continue_main.Select("party_hope=3");
            DataTable RandomPartyFreeJoins = new DataTable();
            DataColumn primaryKey = RandomPartyFreeJoins.Columns.Add("party_no", typeof(int));
            RandomPartyFreeJoins.Columns.Add("entry_no", typeof(int));
            RandomPartyFreeJoins.Columns.Add("target_mark", typeof(int));
            RandomPartyFreeJoins.Columns.Add("now_count", typeof(int));
            RandomPartyFreeJoins.Columns.Add("max_count", typeof(int));
            RandomPartyFreeJoins.Columns.Add("overman", typeof(bool));
            RandomPartyFreeJoins.PrimaryKey = new DataColumn[] { primaryKey };

            foreach (ContinueDataEntity.ts_continue_mainRow RandRow in ContinueMainRandomFreeJoinRows)
            {
                int PartyNo = LibParty.GetPartyNo(RandRow.entry_no);
                DataRow FreeJoinRow = RandomPartyFreeJoins.Rows.Find(PartyNo);
                if (FreeJoinRow != null)
                {
                    FreeJoinRow["now_count"] = (int)FreeJoinRow["now_count"] + 1;
                    if ((int)FreeJoinRow["now_count"] > ((int)FreeJoinRow["max_count"] / 2))
                    {
                        FreeJoinRow["overman"] = true;
                    }

                    if ((int)FreeJoinRow["target_mark"] == 0 && RandRow.mark_id > 0)
                    {
                        FreeJoinRow["target_mark"] = RandRow.mark_id;
                    }
                }
                else
                {
                    int MaxCount = LibParty.PartyMemberCount(PartyNo);
                    DataRow NewEntryRow = RandomPartyFreeJoins.NewRow();

                    NewEntryRow["party_no"] = PartyNo;
                    NewEntryRow["entry_no"] = RandRow.entry_no;
                    NewEntryRow["target_mark"] = RandRow.mark_id;
                    NewEntryRow["now_count"] = 1;
                    NewEntryRow["max_count"] = MaxCount;
                    if (MaxCount == 1)
                    {
                        NewEntryRow["overman"] = true;
                    }
                    else
                    {
                        NewEntryRow["overman"] = false;
                    }

                    RandomPartyFreeJoins.Rows.Add(NewEntryRow);
                }
            }


            if (ContinueMainRandomRows.Length > 0)
            {
                DataView RandomPartyView = new DataView(RandomPartyMember);
                DataView RandomPartyJoinView = new DataView(RandomPartyMember);
                RandomPartyView.Sort = "target_mark desc, random";

                int MemberCount = 1;

                foreach (DataRow RandomPartyRow in RandomPartyFreeJoins.Select("overman=true"))
                {
                    int NowPartyNo = (int)RandomPartyRow["party_no"];

                    RandomPartyJoinView.RowFilter = "entry_no<>" + (int)RandomPartyRow["entry_no"] + " and target_mark=" + (int)RandomPartyRow["target_mark"];
                    RandomPartyJoinView.Sort = "random";

                    MemberCount = (int)RandomPartyRow["max_count"];

                    foreach (DataRowView JoinRow in RandomPartyJoinView)
                    {
                        if ((bool)JoinRow["is_party"])
                        {
                            continue;
                        }

                        int EntryNo = (int)RandomPartyRow["entry_no"];
                        string MineName = CharaMini.GetNickNameWithLink(EntryNo, 1);

                        int TargetEntryNo = (int)JoinRow["entry_no"];
                        string TargetNickName = CharaMini.GetNickNameWithLink(TargetEntryNo, 1);

                        LibParty.SetInParty(NowPartyNo, TargetEntryNo, false);
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, TargetNickName + "がランダムパーティに参加してきた。<br />" + TargetNickName + "がパーティメンバーになりました。", Status.MessageLevel.Normal);
                        LibPlayerMemo.AddSystemMessage(TargetEntryNo, Status.PlayerSysMemoType.PartySetting, MineName + "のランダムパーティに参加を希望した。<br />" + MineName + "のパーティメンバーになりました。", Status.MessageLevel.Normal);

                        JoinRow["is_party"] = true;
                        MemberCount++;

                        if (MemberCount >= 6)
                        {
                            break;
                        }
                    }
                }

                MemberCount = 1;

                foreach (DataRowView RandomPartyRow in RandomPartyView)
                {
                    if ((bool)RandomPartyRow["is_party"] == false)
                    {
                        int NowPartyNo = LibParty.GetPartyNo((int)RandomPartyRow["entry_no"]);

                        RandomPartyJoinView.RowFilter = "entry_no<>" + (int)RandomPartyRow["entry_no"] + " and target_mark in (" + (int)RandomPartyRow["target_mark"] + ",0)";
                        RandomPartyJoinView.Sort = "random";

                        MemberCount = 1;

                        foreach (DataRowView JoinRow in RandomPartyJoinView)
                        {
                            if ((bool)JoinRow["is_party"])
                            {
                                continue;
                            }

                            int EntryNo = (int)RandomPartyRow["entry_no"];
                            string MineName = CharaMini.GetNickNameWithLink(EntryNo, 1);

                            int TargetEntryNo = (int)JoinRow["entry_no"];
                            string TargetNickName = CharaMini.GetNickNameWithLink(TargetEntryNo, 1);

                            LibParty.SetInParty(NowPartyNo, TargetEntryNo, false);
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, TargetNickName + "がランダムパーティに参加してきた。<br />" + TargetNickName + "がパーティメンバーになりました。", Status.MessageLevel.Normal);
                            LibPlayerMemo.AddSystemMessage(TargetEntryNo, Status.PlayerSysMemoType.PartySetting, MineName + "のランダムパーティに参加を希望した。<br />" + MineName + "のパーティメンバーになりました。", Status.MessageLevel.Normal);

                            JoinRow["is_party"] = true;
                            RandomPartyRow["is_party"] = true;
                            MemberCount++;

                            if (MemberCount >= 6)
                            {
                                break;
                            }
                        }
                    }
                }

                DataView RandomPartyNewbieView = new DataView(RandomPartyMemberNewbie);
                DataView RandomPartyNewbieJoinView = new DataView(RandomPartyMemberNewbie);
                RandomPartyNewbieView.Sort = "random";

                MemberCount = 1;

                foreach (DataRowView RandomPartyRow in RandomPartyNewbieView)
                {
                    if ((bool)RandomPartyRow["is_party"] == false)
                    {
                        int NowPartyNo = LibParty.GetPartyNo((int)RandomPartyRow["entry_no"]);

                        RandomPartyNewbieJoinView.RowFilter = "entry_no<>" + (int)RandomPartyRow["entry_no"];
                        RandomPartyNewbieJoinView.Sort = "random";

                        MemberCount = 1;

                        foreach (DataRowView JoinRow in RandomPartyNewbieJoinView)
                        {
                            if ((bool)JoinRow["is_party"])
                            {
                                continue;
                            }

                            int EntryNo = (int)RandomPartyRow["entry_no"];
                            string MineName = CharaMini.GetNickNameWithLink(EntryNo, 1);

                            int TargetEntryNo = (int)JoinRow["entry_no"];
                            string TargetNickName = CharaMini.GetNickNameWithLink(TargetEntryNo, 1);

                            LibParty.SetInParty(NowPartyNo, TargetEntryNo, false);
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, TargetNickName + "がランダムパーティに参加してきた。<br />" + TargetNickName + "がパーティメンバーになりました。", Status.MessageLevel.Normal);
                            LibPlayerMemo.AddSystemMessage(TargetEntryNo, Status.PlayerSysMemoType.PartySetting, MineName + "のランダムパーティに参加を希望した。<br />" + MineName + "のパーティメンバーになりました。", Status.MessageLevel.Normal);

                            JoinRow["is_party"] = true;
                            RandomPartyRow["is_party"] = true;
                            MemberCount++;

                            if (MemberCount >= 6)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            // イベント編成
            ContinueDataEntity.ts_continue_mainRow[] ContinueMainLastEventRows = (ContinueDataEntity.ts_continue_mainRow[])con.Entity.ts_continue_main.Select("mark_id=501");

            if (ContinueMainLastEventRows.Length > 0)
            {
                LibParty.RegistParty(999, "グランドブレイズ", 501, false, false);
                int NewPartyNo = 999;

                foreach (ContinueDataEntity.ts_continue_mainRow RandomPartyRow in ContinueMainLastEventRows)
                {
                    string MineName = CharaMini.GetNickNameWithLink(RandomPartyRow.entry_no, 1);
                    LibParty.SetInParty(NewPartyNo, RandomPartyRow.entry_no, false);
                    LibPlayerMemo.AddSystemMessage(RandomPartyRow.entry_no, Status.PlayerSysMemoType.PartySetting, MineName + "はイベントパーティに参加しました。<br />" + MineName + "がパーティメンバーになりました。", Status.MessageLevel.Normal);

                }
            }

            // メンバーのいないパーティ削除
            LibParty.PartyList().RowFilter = "";
            LibParty.PartyList().Sort = "party_no";
            int MaxParty = LibParty.PartyList().Count;
            int j = 0;
            i = 0;
            while (i < MaxParty)
            {
                i++;
                LibParty.PartyList().RowFilter = "";
                LibParty.PartyList().Sort = "party_no";
                DataRowView Partys = LibParty.PartyList()[j];
                if (LibParty.PartyMemberCount((int)Partys["party_no"]) <= 0)
                {
                    LibParty.DeleteParty((int)Partys["party_no"]);
                }
                else
                {
                    j++;
                }
            }

            // パーティ名変更
            DataTable PartyList = LibParty.PartyList().ToTable();
            foreach (DataRow PartyRow in PartyList.Rows)
            {
                string[] PartyMember = LibParty.PartyMemberNoStr((int)PartyRow["party_no"]);

                if ((int)PartyRow["party_no"] == 999)
                {
                    continue;
                }
                if (PartyMember == null || PartyMember.Length == 0)
                {
                    // メンバーがいない場合、スキップ
                    LibParty.DeleteParty((int)PartyRow["party_no"]);
                    continue;
                }

                string PartyMemberStr = string.Join(",", PartyMember);

                ContinueDataEntity.ts_continue_mainRow[] ContinueMainRows = (ContinueDataEntity.ts_continue_mainRow[])con.Entity.ts_continue_main.Select("entry_no in (" + PartyMemberStr + ")");

                List<string> PartyNameList = new List<string>();

                foreach (ContinueDataEntity.ts_continue_mainRow MemberContinueRow in ContinueMainRows)
                {
                    if (MemberContinueRow.party_name.Length > 0)
                    {
                        PartyNameList.Add(MemberContinueRow.party_name);
                    }
                }

                if (PartyNameList.Count > 0)
                {
                    // 入力されたパーティ名からランダムで選択
                    string NewPartyName = PartyNameList[LibInteger.GetRand(PartyNameList.Count)];
                    LibParty.SetPartyName((int)PartyRow["party_no"], NewPartyName);
                }
            }

            // ここまでの変更を登録
            LibParty.Update();
        }
    }
}
