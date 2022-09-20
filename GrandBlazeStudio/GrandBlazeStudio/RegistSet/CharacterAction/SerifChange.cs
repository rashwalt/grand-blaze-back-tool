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
        /// セリフ変更
        /// </summary>
        private void SerifChange()
        {
            int status;
            bool IsOK = false;

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;
                ContinueDataEntity.ts_continue_serifRow[] ContinueSerifRows = (ContinueDataEntity.ts_continue_serifRow[])con.Entity.ts_continue_serif.Select("situation>0 and serif_text<>'' and entry_no=" + EntryNo);

                if (ContinueSerifRows.Length == 0)
                {
                    // セリフ設定を実行しない場合はスキップ
                    continue;
                }

                Mine.SerifSettingReset();

                foreach (ContinueDataEntity.ts_continue_serifRow SerifRow in ContinueSerifRows)
                {
                    status = Mine.SerifSettings(SerifRow.word_no, SerifRow.situation, SerifRow.serif_text, SerifRow.perks_id);

                    if (status == Status.SerifSetting.OK)
                    {
                        IsOK = true;
                    }
                }

                if (IsOK)
                {
                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.SerifChange, "セリフ内容を変更しました。", Status.MessageLevel.Normal);
                }
            }
        }
    }
}
