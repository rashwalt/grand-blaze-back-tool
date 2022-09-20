using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using System.IO;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        /// <summary>
        /// 戦闘前のイベント
        /// </summary>
        private void QuestBeforeEntry()
        {
            LibScript script = new LibScript(CharaList);
            int MarkID = 0;

            foreach (DataRow PartyRow in PartyList.Rows)
            {
                int PartyNo = (int)PartyRow["party_no"];
                string[] PartyMembers = LibParty.PartyMemberNoStr(PartyNo);

                ContinueDataEntity.ts_continue_mainRow[] ContinueMainRows = (ContinueDataEntity.ts_continue_mainRow[])con.Entity.ts_continue_main.Select("entry_no in (" + string.Join(",", PartyMembers) + ")");

                if (ContinueMainRows.Length == 0 && LibParty.CheckNewPlayer(PartyNo) == false)
                {
                    // 誰も継続登録がされていない場合はスキップ
                    using (StreamWriter sw = new StreamWriter(LibConst.OutputFolderParty + LibParty.PartyHTML(PartyNo), true, LibConst.FileEncod))
                    {
                        sw.WriteLine("<section>");
                        sw.WriteLine("<p>継続登録を確認できなかったため、クエスト、およびバトルがスキップされました。</p>");
                        sw.WriteLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                        sw.WriteLine("</section>");
                    }
                    continue;
                }

                MarkID = LibParty.GetPartyMarkID(PartyNo);

                if (LibParty.CheckPartyOfficialEvent(PartyNo))
                {
                    switch (LibQuest.OfficialEventID)
                    {
                        case Status.OfficialEvent.NewYear:
                            // 新年イベント
                            MarkID = 29;
                            break;
                        case Status.OfficialEvent.Hinamatsuri:
                            // ひなまつり
                            MarkID = 30;
                            break;
                        case Status.OfficialEvent.Buosai:
                            // 武皇祭
                            MarkID = 31;
                            break;
                        case Status.OfficialEvent.SummerFestival:
                            // 天龍祭
                            MarkID = 32;
                            break;
                        case Status.OfficialEvent.Halloween:
                            // ハロウィーンイベント
                            MarkID = 28;
                            break;
                        case Status.OfficialEvent.Christmas:
                            // セント・クリスマス
                            MarkID = 33;
                            break;
                        case Status.OfficialEvent.WeddingSupport:
                            // ウェディングサポート
                            MarkID = 53;
                            break;
                    }
                }

                if (LibParty.CheckNewPlayer(PartyNo))
                {
                    // 新規キャラクターがいる場合
                    MarkID = -1;
                }

                if (MarkID > 0)
                {
                    foreach (string MemberEntry in PartyMembers)
                    {
                        LibPlayer Member = CharaList.Find(Tg => Tg.EntryNo == int.Parse(MemberEntry));

                        // 踏破した！
                        Member.SetMovingMark(MarkID, true, true);
                    }
                }

                string EventScript = "";
                if (MarkID != 0)
                {
                    EventScript = LibQuest.GetEventScript(Status.EventPopType.BattleBefore, MarkID);
                }
                bool IsMovingQuest = false;
                StringBuilder Event = new StringBuilder();

                //sct.Init();

                if (EventScript.Length > 0)
                {
                    Event.Append(script.Exec(0, PartyNo, MarkID, ref IsMovingQuest, EventScript));
                }

                if (Event.Length > 0)
                {
                    using (StreamWriter sw = new StreamWriter(LibConst.OutputFolderParty + LibParty.PartyHTML(PartyNo), true, LibConst.FileEncod))
                    {
                        sw.WriteLine("<section>");
                        sw.WriteLine("<h2>パーティイベント：プロローグ</h2>");

                        sw.WriteLine("<h4 class=\"mark_name\">" + LibQuest.GetQuestMarkName(MarkID) + "</h4>");
                        sw.WriteLine("<div class=\"before_quest\">");
                        sw.WriteLine(Event.ToString());
                        sw.WriteLine("</div>");

                        sw.WriteLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                        sw.WriteLine("</section>");
                    }
                }
            }
        }

        /// <summary>
        /// 戦闘後のイベント
        /// </summary>
        private void QuestAfterEntry()
        {
            //LibLuaExec sct = new LibLuaExec(CharaList);
            LibScript script = new LibScript(CharaList);
            int MarkID = 0;

            foreach (DataRow PartyRow in PartyList.Rows)
            {
                int PartyNo = (int)PartyRow["party_no"];
                string[] PartyMembers = LibParty.PartyMemberNoStr(PartyNo);

                ContinueDataEntity.ts_continue_mainRow[] ContinueMainRows = (ContinueDataEntity.ts_continue_mainRow[])con.Entity.ts_continue_main.Select("entry_no in (" + string.Join(",", PartyMembers) + ")");

                if (ContinueMainRows.Length == 0)
                {
                    // 誰も継続登録がされていない場合はスキップ
                    continue;
                }

                MarkID = LibParty.GetPartyMarkID(PartyNo);

                if (LibParty.CheckPartyOfficialEvent(PartyNo))
                {
                    switch (LibQuest.OfficialEventID)
                    {
                        case Status.OfficialEvent.NewYear:
                            // 新年イベント
                            MarkID = 29;
                            break;
                        case Status.OfficialEvent.Hinamatsuri:
                            // ひなまつり
                            MarkID = 30;
                            break;
                        case Status.OfficialEvent.Buosai:
                            // 武皇祭
                            MarkID = 31;
                            break;
                        case Status.OfficialEvent.SummerFestival:
                            // 天龍祭
                            MarkID = 32;
                            break;
                        case Status.OfficialEvent.Halloween:
                            // ハロウィーンイベント
                            MarkID = 28;
                            break;
                        case Status.OfficialEvent.Christmas:
                            // セント・クリスマス
                            MarkID = 33;
                            break;
                    }
                }

                if (LibParty.CheckNewPlayer(PartyNo))
                {
                    // 新規キャラクターがいる場合
                    MarkID = -1;
                    continue;
                }
                
                string EventScript = "";
                if (MarkID > 0)
                {
                    EventScript = LibQuest.GetEventScript(Status.EventPopType.BattleAfter, MarkID);
                }
                StringBuilder Event = new StringBuilder();
                bool IsQuestqMoving = false;

                //sct.Init();

                if (EventScript.Length > 0)
                {
                    Event.Append(script.Exec(0, PartyNo, MarkID, ref IsQuestqMoving, EventScript));
                }

                if (Event.Length > 0)
                {
                    using (StreamWriter sw = new StreamWriter(LibConst.OutputFolderParty + LibParty.PartyHTML(PartyNo), true, LibConst.FileEncod))
                    {
                        sw.WriteLine("<section id=\"epilog\">");
                        sw.WriteLine("  <h2>パーティイベント：エピローグ</h2>");

                        sw.WriteLine("<h4 class=\"mark_name\">" + LibQuest.GetQuestMarkName(MarkID) + "</h4>");
                        sw.WriteLine("<div class=\"after_quest\">");
                        sw.WriteLine(Event.ToString());
                        sw.WriteLine("</div>");

                        sw.WriteLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                        sw.WriteLine("</section>");
                    }
                }
            }
        }
    }
}
