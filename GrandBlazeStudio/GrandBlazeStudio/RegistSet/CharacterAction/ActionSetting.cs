using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using System.IO;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// 戦闘行動設定
        /// </summary>
        private void ActionSetting()
        {
            int status;
            bool IsOK = false;

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;
                ContinueDataEntity.ts_continue_battle_actionRow[] ContinueBattleActionRows = (ContinueDataEntity.ts_continue_battle_actionRow[])con.Entity.ts_continue_battle_action.Select("action_no>0 and action_target>0 and action>=0 and entry_no=" + EntryNo);

                if (ContinueBattleActionRows.Length == 0)
                {
                    // 戦闘行動設定を実行しない場合はスキップ
                    continue;
                }

                Mine.ActionSettingReset();

                foreach (ContinueDataEntity.ts_continue_battle_actionRow ActionRow in ContinueBattleActionRows)
                {
                    status = Mine.ActionSettings(ActionRow.action_no, ActionRow.action_target, ActionRow.action, ActionRow.perks_id);

                    if (status == Status.ActionSetting.OK)
                    {
                        IsOK = true;
                    }
                }

                if (IsOK)
                {
                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.BattleAction, "戦闘行動内容を変更しました。", Status.MessageLevel.Normal);
                }
            }
        }
    }
}
