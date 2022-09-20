using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.StatusList
{
    partial class StatusListMain
    {
        private void ListDraw()
        {
            LibContinue con = new LibContinue();
            string PartyMessage = "";

            int MaxCount = (int)((CharaMini.GetMaxNo - 1) / 100 + 1);

            for (int i = 1; i <= MaxCount; i++)
            {
                StringBuilder MessageBuilder = new StringBuilder();
                StringBuilder MessageBuilder2 = new StringBuilder();

                int Min = (i - 1) * 100 + 1;
                int Max = (i - 1) * 100 + 100;

                MessageBuilder.AppendLine("        <div id=\"breadcrumbs\">");
                MessageBuilder.AppendLine("            <!--bread start-->");
                MessageBuilder.AppendLine("            <a href=\"/\">トップ</a> &gt; <a href=\"./\">冒険の結果</a> &gt; 冒険者一覧(" + Min + " ～ " + Max + ")");
                MessageBuilder.AppendLine("            <!--bread end-->");
                MessageBuilder.AppendLine("        </div>");
                MessageBuilder.AppendLine("        <article id=\"wcontent\">");
                MessageBuilder.AppendLine("      <!--content start-->");
                MessageBuilder.AppendLine("      ");
                MessageBuilder.AppendLine("<h1>冒険の結果</h1>");
                MessageBuilder.AppendLine("<section>");
                MessageBuilder.AppendLine("<h2>冒険者一覧(" + Min + " ～ " + Max + ")</h2>");
                MessageBuilder.AppendLine("<div id=\"player-comment\" class=\"ui-tabs\"><ul class=\"ui-tabs-nav ui-helper-reset ui-helper-clearfix\">");
                MessageBuilder.AppendLine("<li class=\"ui-state-default ui-corner-top ui-tabs-selected ui-state-active\"><a href=\"javascript:void(0);\">キャラクター一覧</a></li><li class=\"ui-state-default ui-corner-top\"><a href=\"lcom" + i.ToString("0000") + ".html\">リストメッセージ</a></li>");
                MessageBuilder.AppendLine("</ul></div>");
                MessageBuilder.AppendLine("<table class=\"player-list\" summary=\"冒険者の一覧です。\">");
                MessageBuilder.AppendLine("  <tr>");
                MessageBuilder.AppendLine("    <th scope=\"col\">E-No.</th>");
                MessageBuilder.AppendLine("    <th scope=\"col\">名前</th>");
                MessageBuilder.AppendLine("    <th scope=\"col\">パーティ</th>");
                MessageBuilder.AppendLine("  </tr>");

                MessageBuilder2.AppendLine("        <div id=\"breadcrumbs\">");
                MessageBuilder2.AppendLine("            <!--bread start-->");
                MessageBuilder2.AppendLine("            <a href=\"/\">トップ</a> &gt; <a href=\"./\">冒険の結果</a> &gt; 冒険者一覧(" + Min + " ～ " + Max + ")");
                MessageBuilder2.AppendLine("            <!--bread end-->");
                MessageBuilder2.AppendLine("        </div>");
                MessageBuilder2.AppendLine("        <article id=\"wcontent\">");
                MessageBuilder2.AppendLine("      <!--content start-->");
                MessageBuilder2.AppendLine("      ");
                MessageBuilder2.AppendLine("<h1>冒険の結果</h1>");
                MessageBuilder2.AppendLine("<section>");
                MessageBuilder2.AppendLine("<h2>冒険者一覧(" + Min + " ～ " + Max + ")</h2>");
                MessageBuilder2.AppendLine("<div id=\"player-comment\" class=\"ui-tabs\"><ul class=\"ui-tabs-nav ui-helper-reset ui-helper-clearfix\">");
                MessageBuilder2.AppendLine("<li class=\"ui-state-default ui-corner-top\"><a href=\"list" + i.ToString("0000") + ".html\">キャラクター一覧</a></li><li class=\"ui-state-default ui-corner-top ui-tabs-selected ui-state-active\"><a href=\"javascript:void(0);\">リストメッセージ</a></li>");
                MessageBuilder2.AppendLine("</ul></div>");
                MessageBuilder2.AppendLine("<table class=\"player-list\" summary=\"冒険者の一覧です。\">");
                MessageBuilder2.AppendLine("  <tr>");
                MessageBuilder2.AppendLine("    <th scope=\"col\">E-No.</th>");
                MessageBuilder2.AppendLine("    <th scope=\"col\">名前</th>");
                MessageBuilder2.AppendLine("    <th scope=\"col\">メッセージ</th>");
                MessageBuilder2.AppendLine("  </tr>");

                for (int j = Min; j <= Max; j++)
                {

                    if (CharaMini.CheckInChara(j))
                    {
                        int PartyNo = LibParty.GetPartyNo(j);

                        ContinueDataEntity.ts_continue_messageRow[] ContinueMessageRows = (ContinueDataEntity.ts_continue_messageRow[])con.Entity.ts_continue_message.Select("entry_no=" + j + " and message_target=" + Status.MessageTarget.List);


                        MessageBuilder.AppendLine("  <tr>");
                        MessageBuilder.AppendLine("    <td>" + j + "</td>");
                        MessageBuilder.AppendLine("    <td><a href=\"" + LibUnitBaseMini.CharacterLink(j) + "\">" + CharaMini.GetFullName(j) + "</a></td>");
                        MessageBuilder.AppendLine("    <td><a href=\"" + LibParty.PartyLink(PartyNo) + "\">P-No." + PartyNo + " " + LibParty.GetPartyName(PartyNo) + "</a></td>");
                        MessageBuilder.AppendLine("  </tr>");


                        // 送信している場合
                        if (ContinueMessageRows.Length > 0)
                        {
                            int MarkID = LibParty.GetPartyMarkID(PartyNo);
                            string QuestName = LibQuest.GetQuestMarkName(MarkID);

                            LibPlayer Mine = new LibPlayer(Status.Belong.Friend, j);

                            MessageBuilder2.AppendLine("  <tr>");
                            MessageBuilder2.AppendLine("    <td>" + j + "</td>");
                            MessageBuilder2.AppendLine("    <td><a href=\"" + LibUnitBaseMini.CharacterLink(j) + "\">" + CharaMini.GetFullName(j) + "</a></td>");
                            MessageBuilder2.AppendLine("    <td>");

                            foreach (ContinueDataEntity.ts_continue_messageRow MesRow in ContinueMessageRows)
                            {
                                string Base = MesRow.message_body.Replace("<br>{nl}", "{nl}").Replace("{nl}<br>", "{nl}").Replace("<br />{nl}", "{nl}").Replace("{nl}<br />", "{nl}");
                                PartyMessage = LibMessage.ConvertMessage(Base, QuestName, Mine);
                                PartyMessage = LibMessage.ListSerif(PartyMessage);

                                MessageBuilder2.AppendLine(PartyMessage);
                            }
                            MessageBuilder2.AppendLine("    </td>");
                            MessageBuilder2.AppendLine("  </tr>");
                        }
                    }
                }

                MessageBuilder.AppendLine("</table>");
                MessageBuilder.AppendLine("</section>");

                MessageBuilder2.AppendLine("</table>");
                MessageBuilder2.AppendLine("</section>");

                using (StreamWriter sw = new StreamWriter(GrandBlazeStudio.Properties.Settings.Default.ResultBasePath + "list" + i.ToString("0000") + ".html", false, LibConst.FileEncod))
                {
                    sw.Write(GetOutLine(MessageBuilder.ToString()));
                }

                using (StreamWriter sw = new StreamWriter(GrandBlazeStudio.Properties.Settings.Default.ResultBasePath + "lcom" + i.ToString("0000") + ".html", false, LibConst.FileEncod))
                {
                    sw.Write(GetOutLine(MessageBuilder2.ToString()));
                }
            }
        }

        private void NewPlayerDraw()
        {
            CharacterDataEntity.ts_character_listDataTable CharacterList = CharaMini.GetCharacters();
            CharacterList.DefaultView.RowFilter = "new_gamer=true";
            CharacterList.DefaultView.Sort = "entry_no";
            DataTable NewPlayerTable = CharacterList.DefaultView.ToTable();

            int MaxCount = NewPlayerTable.Rows.Count;

            StringBuilder MessageBuilder = new StringBuilder();

            MessageBuilder.AppendLine("        <div id=\"breadcrumbs\">");
            MessageBuilder.AppendLine("            <!--bread start-->");
            MessageBuilder.AppendLine("            <a href=\"/\">トップ</a> &gt; <a href=\"./\">冒険の結果</a> &gt; 新規登録者一覧");
            MessageBuilder.AppendLine("            <!--bread end-->");
            MessageBuilder.AppendLine("        </div>");
            MessageBuilder.AppendLine("        <article id=\"wcontent\">");
            MessageBuilder.AppendLine("      <!--content start-->");
            MessageBuilder.AppendLine("      ");
            MessageBuilder.AppendLine("<h1>冒険の結果</h1>");
            MessageBuilder.AppendLine("<section>");
            MessageBuilder.AppendLine("<h2>新規登録者一覧</h2>");

            if (MaxCount > 0)
            {
                MessageBuilder.AppendLine("<table class=\"player-list\" summary=\"冒険者の一覧です。\">");
                MessageBuilder.AppendLine("  <tr>");
                MessageBuilder.AppendLine("    <th scope=\"col\">E-No.</th>");
                MessageBuilder.AppendLine("    <th scope=\"col\">名前</th>");
                MessageBuilder.AppendLine("    <th scope=\"col\">パーティ</th>");
                MessageBuilder.AppendLine("  </tr>");

                for (int j = 0; j < MaxCount; j++)
                {
                    int EntryNo = (int)NewPlayerTable.Rows[j]["entry_no"];
                    int PartyNo = LibParty.GetPartyNo(EntryNo);

                    MessageBuilder.AppendLine("  <tr>");
                    MessageBuilder.AppendLine("    <td>" + EntryNo + "</td>");
                    MessageBuilder.AppendLine("    <td><a href=\"" + LibUnitBaseMini.CharacterLink(EntryNo) + "\">" + CharaMini.GetFullName(EntryNo) + "</a></td>");
                    MessageBuilder.AppendLine("    <td><a href=\"" + LibParty.PartyLink(PartyNo) + "\">P-No." + PartyNo + " " + LibParty.GetPartyName(PartyNo) + "</a></td>");
                    MessageBuilder.AppendLine("  </tr>");
                }

                MessageBuilder.AppendLine("</table>");
            }
            else
            {
                MessageBuilder.AppendLine("<p>今回の更新で、新規登録者はいませんでした。</p>");
            }
            MessageBuilder.AppendLine("</section>");

            using (StreamWriter sw = new StreamWriter(GrandBlazeStudio.Properties.Settings.Default.ResultBasePath + "newplayer.html", false, LibConst.FileEncod))
            {
                sw.Write(GetOutLine(MessageBuilder.ToString()));
            }
        }
    }
}
