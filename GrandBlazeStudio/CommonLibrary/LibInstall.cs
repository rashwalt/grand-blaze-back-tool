using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    public static class LibInstall
    {
        public static InstallDataEntity Entity;

        static LibInstall()
        {
            LoadInstall();
        }

        public static void LoadInstall()
        {
            Entity = new InstallDataEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_install_class_list>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("install_id,");
                SelSql.AppendLine("classname,");
                SelSql.AppendLine("up_hp,");
                SelSql.AppendLine("up_mp,");
                SelSql.AppendLine("up_str,");
                SelSql.AppendLine("up_agi,");
                SelSql.AppendLine("up_mag,");
                SelSql.AppendLine("up_unq,");
                SelSql.AppendLine("class_comment");
                SelSql.AppendLine("FROM mt_install_class_list");
                SelSql.AppendLine("ORDER BY install_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_install_class_list);

                SelSql = new StringBuilder();
                #region TABLE <mt_install_class_skill>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("install_id,");
                SelSql.AppendLine("perks_id,");
                SelSql.AppendLine("install_level,");
                SelSql.AppendLine("only_mode");
                SelSql.AppendLine("FROM mt_install_class_skill");
                SelSql.AppendLine("ORDER BY install_id,install_level,perks_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_install_class_skill);
            }
        }

        /// <summary>
        /// インストールクラスの名前を取得
        /// </summary>
        /// <param name="InstallID">インストールクラスID</param>
        /// <returns>インストールクラスの名前</returns>
        public static string GetInstallName(int InstallID)
        {
            InstallDataEntity.mt_install_class_listRow row = Entity.mt_install_class_list.FindByinstall_id(InstallID);

            if (row != null)
            {
                return row.classname;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// インストールクラスのDataRow
        /// </summary>
        /// <param name="InstallID">インストールクラスID</param>
        /// <returns>インストールクラスのDataRow</returns>
        public static InstallDataEntity.mt_install_class_listRow GetInstallRow(int InstallID)
        {
            return Entity.mt_install_class_list.FindByinstall_id(InstallID);
        }

        /// <summary>
        /// インストールクラスの使用可能スキルリスト
        /// </summary>
        /// <param name="InstallID"></param>
        /// <param name="InstallLevel"></param>
        /// <returns></returns>
        public static InstallDataEntity.mt_install_class_skillRow[] GetSkillRows(int InstallID, int InstallLevel)
        {
            return (InstallDataEntity.mt_install_class_skillRow[])Entity.mt_install_class_skill.Select("install_id=" + InstallID + " and install_level<=" + InstallLevel);
        }

        /// <summary>
        /// インストールクラスの使用可能スキルリスト最小と最大指定
        /// </summary>
        /// <param name="InstallID"></param>
        /// <param name="InstallLevelMin"></param>
        /// <param name="InstallLevelMax"></param>
        /// <returns></returns>
        public static InstallDataEntity.mt_install_class_skillRow[] GetSkillRows(int InstallID, int InstallLevelMin, int InstallLevelMax)
        {
            return (InstallDataEntity.mt_install_class_skillRow[])Entity.mt_install_class_skill.Select("install_id=" + InstallID + " and install_level>" + InstallLevelMin + " and install_level<=" + InstallLevelMax);
        }

        /// <summary>
        /// インストールクラスのレベルアップに必要な経験値
        /// </summary>
        /// <param name="Level">インストールクラスレベル</param>
        /// <returns>経験値</returns>
        public static int MaxExp(int Level)
        {
            switch (Level)
            {
                case 0:
                    return 3;
                case 1:
                    return 6;
                default:
                    return 10;
            }
        }
    }
}
