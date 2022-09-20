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
        /// アイコンの設定
        /// </summary>
        private void IconSettings()
        {
            int status;

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;
                ContinueDataEntity.ts_continue_iconRow[] ContinueIconRows = (ContinueDataEntity.ts_continue_iconRow[])con.Entity.ts_continue_icon.Select("icon_copyright<>'' and icon_url<>'' and entry_no=" + EntryNo);

                ContinueDataEntity.ts_continue_completeRow ContinueCompleteRow = con.Entity.ts_continue_complete.FindByentry_nocategory(EntryNo, "icon");

                if (ContinueCompleteRow == null)
                {
                    continue;
                }

                if (ContinueIconRows.Length == 0 && Mine.IconList.Count == 0)
                {
                    // アイコン設定を実行しない場合はスキップ
                    continue;
                }

                Mine.IconSettingReset();

                int IconCount = 0;

                foreach (ContinueDataEntity.ts_continue_iconRow IconRow in ContinueIconRows)
                {
                    status = Mine.SetIcon(IconRow.icon_id, IconRow.icon_url, IconRow.icon_copyright);
                    IconCount++;

                    if (IconCount >= 50)
                    {
                        break;
                    }
                }

                LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.IconSettings, "アイコンの設定を変更した。", Status.MessageLevel.Normal);
            }
        }
    }
}
