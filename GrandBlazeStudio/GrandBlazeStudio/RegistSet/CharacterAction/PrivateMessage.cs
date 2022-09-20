using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using System.IO;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// プライベートメッセージ
        /// </summary>
        private void PrivateMessage()
        {
            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_messageRow[] ContinueMessageRows = (ContinueDataEntity.ts_continue_messageRow[])con.Entity.ts_continue_message.Select("(entry_no=" + EntryNo + " or message_entry=" + EntryNo + ") and message_target=" + Status.MessageTarget.Private);

                if (ContinueMessageRows.Length == 0)
                {
                    // 自分に関係するメッセージがない場合はスキップ
                    continue;
                }

                string NickName = Mine.NickName;

                ContinueDataEntity.ts_continue_messageRow[] ContinueMessageSendRows = (ContinueDataEntity.ts_continue_messageRow[])con.Entity.ts_continue_message.Select("entry_no=" + EntryNo + " and message_target=" + Status.MessageTarget.Private);

                foreach (ContinueDataEntity.ts_continue_messageRow MessageSendRow in ContinueMessageSendRows)
                {
                    // 誰かに送信している場合
                    int TargetEntryNo = MessageSendRow.message_entry;

                    if (CharaMini.CheckInChara(TargetEntryNo) && (EntryNo != TargetEntryNo && TargetEntryNo != 0))
                    {
                        // 相手が存在し、自分以外の場合
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PrivateMessage, CharaMini.GetNickNameWithLink(TargetEntryNo, 1) + "に<a href=\"" + LibUnitBaseMini.CharacterLink(TargetEntryNo, 1) + "\">メッセージ</a>を送信しました。", Status.MessageLevel.Normal);
                    }
                    else if (EntryNo != TargetEntryNo && TargetEntryNo != 0)
                    {
                        // 相手が存在しない場合
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PrivateMessage, "送信先:E-No." + TargetEntryNo + "には登録されている冒険者がいないようです。<br />メッセージは次元の狭間に放り込まれました。", Status.MessageLevel.Caution);
                    }
                }

                ContinueDataEntity.ts_continue_messageRow[] ContinueMessageReceiveRows = (ContinueDataEntity.ts_continue_messageRow[])con.Entity.ts_continue_message.Select("(message_entry=" + EntryNo + " or (message_entry=0 and entry_no=" + EntryNo + ") ) and message_target=" + Status.MessageTarget.Private);

                foreach (ContinueDataEntity.ts_continue_messageRow MessageSendRow in ContinueMessageReceiveRows)
                {
                    LibUnitBase Sender = CharaList.Find(chs => chs.EntryNo == MessageSendRow.entry_no);
                    LibMessage.SenderMessage(Sender, Mine, MessageSendRow.message_body);
                }
            }
        }
    }
}
