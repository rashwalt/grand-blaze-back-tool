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
        /// 隊列の設定
        /// </summary>
        private void FormationSetting()
        {
            int status;

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_battle_preparationRow ContinueBattleRow = con.Entity.ts_continue_battle_preparation.FindByentry_no(EntryNo);

                if (ContinueBattleRow == null || ContinueBattleRow.formation == 0)
                {
                    // ない場合はスキップ
                    continue;
                }

                int Formation = ContinueBattleRow.formation - 1;

                status = Mine.SetFormation(Formation);

                switch (status)
                {
                    case Status.FormationSetting.OK:
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.FormationSetting, "隊列を" + Mine.FormationName + "に変更した。", Status.MessageLevel.Normal);
                        break;
                }
            }
        }
    }
}
