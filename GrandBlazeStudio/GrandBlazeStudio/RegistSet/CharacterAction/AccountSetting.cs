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
        /// アカウント設定
        /// </summary>
        private void AccountSetting()
        {
            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_profileRow ContinueProfileRow = con.Entity.ts_continue_profile.FindByentry_no(EntryNo);

                if (ContinueProfileRow == null)
                {
                    // ない場合はスキップ
                    continue;
                }

                // アカウントステータス変更
                if (ContinueProfileRow.account_status == Status.AccountStatus.Freeze)
                {
                    Mine.AccountStatus = Status.Account.Freeze;

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.AccountSetting, "アカウントを凍結しました。<br />再開する場合には、継続登録を行ってください。<br />自動で解凍手続きが行われます。", Status.MessageLevel.Caution);
                }
            }
        }
    }
}
