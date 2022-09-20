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
        /// インストールクラスの設定
        /// </summary>
        private void InstallClassSetting()
        {
            int status;

            int InstallClassNo = 0;
            int SecondaryInstallClassNo = 0;

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_battle_preparationRow ContinueBattleRow = con.Entity.ts_continue_battle_preparation.FindByentry_no(EntryNo);

                if (ContinueBattleRow == null || (ContinueBattleRow.install == 0 && ContinueBattleRow.secondary_install == 0))
                {
                    // ない場合はスキップ
                    continue;
                }

                status = -1;

                InstallClassNo = ContinueBattleRow.install;

                if (InstallClassNo > 0 && Mine.IntallClassID != InstallClassNo)
                {
                    status = Mine.SetInstallClass(InstallClassNo);
                    Mine.IsInstallClassChanging = true;
                }

                switch (status)
                {
                    case Status.Install.OK:
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.InstallSetting, LibResultText.CSSEscapeInstallClass(LibInstall.GetInstallName(InstallClassNo)) + "をインストールした。<br />※装備が再設定されました。装備品を再確認してください。", Status.MessageLevel.Normal);
                        break;
                    case Status.Install.NoGetting:
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.InstallSetting, "習得していないインストールクラスをインストールしようとしました。", Status.MessageLevel.Caution);
                        break;
                }

                if (ContinueBattleRow.secondary_install > 0 && Mine.CheckKeyItem(3))
                {
                    SecondaryInstallClassNo = ContinueBattleRow.secondary_install;

                    if (SecondaryInstallClassNo > 0 && Mine.SecondryIntallClassID != SecondaryInstallClassNo)
                    {
                        status = Mine.SetSecondaryInstallClass(SecondaryInstallClassNo);
                        Mine.IsInstallClassChanging = true;
                    }

                    switch (status)
                    {
                        case Status.Install.OK:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.InstallSetting, LibResultText.CSSEscapeInstallClass(LibInstall.GetInstallName(SecondaryInstallClassNo)) + "をサブにインストールした。", Status.MessageLevel.Normal);
                            break;
                        case Status.Install.NoGetting:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.InstallSetting, "習得していないインストールクラスをサブにインストールしようとしました。", Status.MessageLevel.Caution);
                            break;
                    }
                }
            }
        }
    }
}
