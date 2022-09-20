using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// パーティ移動
        /// </summary>
        private void PartyMoving()
        {
            MovingListEntity.moving_listDataTable MoveList = new MovingListEntity.moving_listDataTable();
            //LibLuaExec lua = new LibLuaExec(CharaList);
            LibScript script = new LibScript(CharaList);
            int MarkID = 0;

            // 移動は基本的に情報を入手しないと目的の場所へ移動できない。情報がない場合、公開マークのうち、階層が0のものからランダムの位置へ飛ばされる。

            foreach (DataRow PartyRow in PartyList.Rows)
            {
                int PartyNo = (int)PartyRow["party_no"];

                // ウェディングサポートではスキップ
                if (LibParty.CheckPartyOfficialEvent(PartyNo) && LibQuest.OfficialEventID == Status.OfficialEvent.WeddingSupport)
                {
                    continue;
                }

                string[] PartyMembers = LibParty.PartyMemberNoStr(PartyNo);

                ContinueDataEntity.ts_continue_mainRow[] ContinueMainRows = (ContinueDataEntity.ts_continue_mainRow[])con.Entity.ts_continue_main.Select("entry_no in (" + string.Join(",", PartyMembers) + ")");

                // スキップ
                if (ContinueMainRows.Length == 0)
                {
                    continue;
                }

                // 多数決
                MoveList.Clear();

                // すべて登録
                foreach (ContinueDataEntity.ts_continue_mainRow MoveRow in ContinueMainRows)
                {
                    if (MoveRow.quest_id == 0)
                    {
                        // 他の人に任せる場合はカウントしない
                        continue;
                    }

                    if (MoveRow.quest_id > 0)
                    {
                        MarkID = LibQuest.GetMarkID(MoveRow.quest_id, MoveRow.mark_id);
                        if (!LibQuest.CheckMark(MarkID))
                        {
                            // 存在しないエリアはカウントしない
                            continue;
                        }
                    }
                    else if (MoveRow.quest_id < 0)
                    {
                        MoveRow["mark_name"] = "";
                    }

                    MovingListEntity.moving_listRow MovingListRow = MoveList.FindByquest_idmark_id(MoveRow.quest_id, MoveRow.mark_id);
                    if (MovingListRow != null)
                    {
                        MovingListRow.count += 1;
                    }
                    else
                    {
                        MovingListEntity.moving_listRow MoveNewRow = MoveList.Newmoving_listRow();
                        MoveNewRow.quest_id = MoveRow.quest_id;
                        MoveNewRow.mark_id = MoveRow.mark_id;
                        MoveNewRow.count = 1;
                        MoveList.Addmoving_listRow(MoveNewRow);
                    }
                }

                MoveList.DefaultView.RowFilter = "";
                MoveList.DefaultView.Sort = "count desc";

                if (MoveList.DefaultView.Count == 0)
                {
                    LibParty.SetPartyAreaID(PartyNo, 0);
                    continue;
                }

                MoveList.DefaultView.RowFilter = "count=" + (int)MoveList.DefaultView[0]["count"];

                // ランダム
                MovingListEntity.moving_listRow SelectMarkRow = (MovingListEntity.moving_listRow)MoveList.DefaultView[LibInteger.GetRand(MoveList.DefaultView.Count)].Row;
                MarkID = LibQuest.GetMarkID(SelectMarkRow.quest_id, SelectMarkRow.mark_id);

                bool IsMemberMovingOK = false;

                foreach (string MemberEntry in PartyMembers)
                {
                    LibPlayer Member = CharaList.Find(Tg => Tg.EntryNo == int.Parse(MemberEntry));

                    if (Member.MovingOKMarks.FindByentry_nomark_id(Member.EntryNo, MarkID) != null)
                    {
                        IsMemberMovingOK = true;
                    }

                }

                if (!LibQuest.CheckHide(MarkID))
                {
                    IsMemberMovingOK = true;
                }

                // 移動判定
                if (IsMemberMovingOK)
                {
                    // クエスト共通進入チェック
                    QuestDataEntity.quest_listRow questRow = LibQuest.GetQuestRow(SelectMarkRow.quest_id);

                    bool IsInstallLevel = false;

                    foreach (string MemberEntry in PartyMembers)
                    {
                        LibPlayer Member = CharaList.Find(Tg => Tg.EntryNo == int.Parse(MemberEntry));

                        if (!LibQuest.CheckInnerMark(MarkID, Member))
                        {
                            IsMemberMovingOK = false;
                        }

                        // クエスト貴重品
                        if (questRow.key_item_id > 0 && !Member.CheckKeyItem(questRow.key_item_id))
                        {
                            IsMemberMovingOK = false;
                        }

                        // クエストオファー
                        if (questRow.offer_quest_id > 0 && !Member.CheckQuest(questRow.offer_quest_id))
                        {
                            IsMemberMovingOK = false;
                        }

                        // クエストコンプ
                        if (questRow.comp_quest_id > 0 && !Member.CheckQuestComp(questRow.comp_quest_id))
                        {
                            IsMemberMovingOK = false;
                        }

                        // 最低SP
                        if (questRow.sp_level > 0 && Member.Level < questRow.sp_level)
                        {
                            IsMemberMovingOK = false;
                        }

                        // クラスとレベル
                        if (questRow.class_id == 0 || questRow.class_level == 0 || Member.CheckInstallLevel(questRow.class_id, questRow.class_level))
                        {
                            IsInstallLevel = true;
                        }
                    }

                    if (!IsInstallLevel)
                    {
                        IsMemberMovingOK = false;
                    }
                }

                if (IsMemberMovingOK)
                {
                    if (MarkID < 0)
                    {
                        LibParty.SetPartyOfficialEvent(PartyNo);
                    }
                    else if (MarkID > 0)
                    {
                        // 移動先決定
                        LibParty.SetPartyAreaID(PartyNo, MarkID);
                    }

                    LibParty.Update();
                }
                else
                {
                    // 移動できなかった
                    LibParty.AddSystemMessage(PartyNo, Status.PartySysMemoType.MarkMissing, "条件を満たしていないため、移動できなかった。", Status.MessageLevel.Error);

                    LibParty.UpdateSystemMessage();

                    MarkID = LibQuest.GetDefaultMarkID(MarkID);
                    LibParty.AddSystemMessage(PartyNo, Status.PartySysMemoType.MarkMissing, "予定した位置とは違う場所に出てしまったようだ。", Status.MessageLevel.Caution);

                    // 移動先決定
                    LibParty.SetPartyAreaID(PartyNo, MarkID);

                    LibParty.Update();
                }
            }

        }
    }
}
