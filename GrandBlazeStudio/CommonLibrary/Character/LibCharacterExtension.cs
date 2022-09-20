using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary.Character
{
    public static class LibUnitBaseExtension
    {
        /// <summary>
        /// 経験値取得(個人結果出す直前にこの処理を行う)
        /// </summary>
        /// <param name="Mine">拡張対象</param>
        public static void UpLevelExp(this LibPlayer Mine)
        {
            bool IsMineLevelUp = false;
            bool IsInstallLevelUp = false;
            bool IsSecondryInstallLevelUp = false;
            int BeforeInstallLevel = Mine.InstallClassLevelNormal;
            int BeforeSecondryInstallLevel = Mine.SecondryInstallClassLevelNormal;
            int EntryNo = Mine.EntryNo;

            bool IsLevelUp = false;

            int LvHP = Mine.HPMax;
            int LvCureHP = Mine.HPDamageRate;
            int LvMP = Mine.MPMax;
            int LvCureMP = Mine.MPDamageRate;
            int LvSTR = Mine.STR;
            int LvAGI = Mine.AGI;
            int LvMAG = Mine.MAG;
            int LvUNQ = Mine.UNQ;

            while (Mine.NextExp <= 0)
            {
                if (LibConst.LevelLimit > Mine.Level)
                {
                    Mine.LevelPlus();

                    IsLevelUp = true;
                    IsMineLevelUp = true;
                }
                else
                {
                    break;
                }
            }

            // インストールクラスのレベルアップ量
            while (Mine.InstallClassNExp <= 0)
            {
                if (LibConst.LevelLimit > Mine.InstallClassLevelNormal)
                {
                    Mine.InstallClassLevel++;

                    IsLevelUp = true;
                    IsInstallLevelUp = true;
                }
                else
                {
                    break;
                }
            }

            // セカンダリインストールクラスのレベルアップ量
            if (BeforeSecondryInstallLevel > 0)
            {
                while (Mine.SecondryInstallClassNExp <= 0)
                {
                    if (LibConst.LevelLimit > Mine.SecondryInstallClassLevelNormal)
                    {
                        Mine.SecondryInstallClassLevelNormal++;

                        IsLevelUp = true;
                        IsSecondryInstallLevelUp = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Mine.RefreshLevelUpAbility();

            if (IsLevelUp)
            {
                Mine.LevelUpHP += Mine.HPMax - LvHP;
                Mine.LevelUpMP += Mine.MPMax - LvMP;
                Mine.LevelUpSTR += Mine.STR - LvSTR;
                Mine.LevelUpAGI += Mine.AGI - LvAGI;
                Mine.LevelUpMAG += Mine.MAG - LvMAG;
                Mine.LevelUpUNQ += Mine.UNQ - LvUNQ;

                Mine.HPNow = (int)((decimal)LvCureHP / 100m * (decimal)Mine.HPMax);
                Mine.MPNow = (int)((decimal)LvCureMP / 100m * (decimal)Mine.MPMax);
            }

            if (IsLevelUp)
            {
                LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.LevelUpInformation, Mine.NickName + "はレベルアップ！", Status.MessageLevel.Normal);

                if (IsMineLevelUp)
                {
                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.LevelUpInformation, Mine.NickName + "のスキルポイントが" + Mine.LevelNormal + "になった！", Status.MessageLevel.Normal);
                }
                if (IsInstallLevelUp)
                {
                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.LevelUpInformation, Mine.IntallClassName + "のレベルが" + Mine.InstallClassLevelNormal + "になった！", Status.MessageLevel.Normal);

                    InstallDataEntity.mt_install_class_skillRow[] GetInstallSkillRows = LibInstall.GetSkillRows(Mine.IntallClassID, BeforeInstallLevel, Mine.InstallClassLevelNormal);

                    foreach (InstallDataEntity.mt_install_class_skillRow InstallSkillRow in GetInstallSkillRows)
                    {
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.LevelUpInformation, LibSkill.GetSkillName(InstallSkillRow.perks_id) + "を習得した！", Status.MessageLevel.Normal);
                    }
                }
                if (IsSecondryInstallLevelUp)
                {
                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.LevelUpInformation, Mine.SecondryIntallClassName + "のレベルが" + Mine.SecondryInstallClassLevelNormal + "になった！", Status.MessageLevel.Normal);

                    InstallDataEntity.mt_install_class_skillRow[] GetInstallSkillRows = LibInstall.GetSkillRows(Mine.SecondryIntallClassID, BeforeSecondryInstallLevel, Mine.SecondryInstallClassLevelNormal);

                    foreach (InstallDataEntity.mt_install_class_skillRow InstallSkillRow in GetInstallSkillRows)
                    {
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.LevelUpInformation, LibSkill.GetSkillName(InstallSkillRow.perks_id) + "を習得した！", Status.MessageLevel.Normal);
                    }
                }
            }
        }

        /// <summary>
        /// 数値をアルファベットに変換
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToAlphabet(this int self)
        {
            if (self <= 0) return "";

            int n = self % 26;
            n = (n == 0) ? 26 : n;
            string s = ((char)(n + 64)).ToString();
            if (self == n) return s;
            return ((self - n) / 26).ToAlphabet() + s;
        }
    }
}
