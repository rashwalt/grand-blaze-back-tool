using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using System.IO;
using System.Text.RegularExpressions;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        /// <summary>
        /// パーティメッセージ
        /// </summary>
        private void PartyMessage()
        {
            StringBuilder MessageBuilder = new StringBuilder();
            string PartyMessage = "";
            string QuestName = "";

            foreach (DataRow PartyRow in PartyList.Rows)
            {
                int PartyNo = (int)PartyRow["party_no"];
                string[] PartyMembers = LibParty.PartyMemberNoStr(PartyNo);
                MessageBuilder = new StringBuilder();

                ContinueDataEntity.ts_continue_messageRow[] ContionueMessageRows = (ContinueDataEntity.ts_continue_messageRow[])con.Entity.ts_continue_message.Select("entry_no in (" + string.Join(",", PartyMembers) + ") and message_target=" + Status.MessageTarget.Party);

                if (ContionueMessageRows.Length == 0)
                {
                    // メッセージがない場合はスキップ
                    continue;
                }

                QuestName = LibQuest.GetQuestMarkName(LibParty.GetPartyMarkID(PartyNo));

                foreach (ContinueDataEntity.ts_continue_messageRow MessageSendRow in ContionueMessageRows)
                {
                    // 送信している場合
                    LibPlayer Mine = CharaList.Find(chs => chs.EntryNo == MessageSendRow.entry_no);

                    MessageBuilder.AppendLine("<div class=\"party_message_unit\">");
                    MessageBuilder.AppendLine("<div class=\"p_mess_unit\">" + Mine.NickName + " からのパーティメッセージ</div>");

                    PartyMessage = "";
                    string Base = MessageSendRow.message_body;

                    Base = Base.Replace("<br>{nl}", "{nl}").Replace("{nl}<br>", "{nl}").Replace("<br />{nl}", "{nl}").Replace("{nl}<br />", "{nl}");
                    string[] MessageList = Base.Split(new string[] { "{nl}" }, StringSplitOptions.None);

                    foreach (string Text in MessageList)
                    {
                        PartyMessage += LibSerif.ConvertTextBySerif(Mine, Text.Replace("<br />", "\n"), "party_message", QuestName, null);
                    }

                    MessageBuilder.AppendLine(PartyMessage);
                    MessageBuilder.AppendLine("</div>");
                }

                using (StreamWriter sw = new StreamWriter(LibConst.OutputFolderParty + LibParty.PartyHTML(PartyNo), true, LibConst.FileEncod))
                {
                    sw.Write(MessageBuilder.ToString());
                }
            }
        }
    }
}
