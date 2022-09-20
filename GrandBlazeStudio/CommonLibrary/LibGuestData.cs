using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    public static class LibGuestData
    {
        public static GuestDataEntity Entity;

        static LibGuestData()
        {
            LoadData();
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        public static void LoadData()
        {
            Entity = new GuestDataEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_guest_list>
                SelSql.AppendLine("SELECT [guest_id]");
                SelSql.AppendLine("      ,[character_name]");
                SelSql.AppendLine("      ,[nick_name]");
                SelSql.AppendLine("      ,[race_id]");
                SelSql.AppendLine("      ,[unique_name]");
                SelSql.AppendLine("      ,[belong_kb]");
                SelSql.AppendLine("  FROM [mt_guest_list]");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_guest_list);

                SelSql = new StringBuilder();
                #region TABLE <mt_guest_battle_ability>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("guest_id, ");
                SelSql.AppendLine("install, ");
                SelSql.AppendLine("second_install, ");
                SelSql.AppendLine("formation, ");
                SelSql.AppendLine("[level], ");
                SelSql.AppendLine("option_list, ");
                SelSql.AppendLine("level_edit, ");
                SelSql.AppendLine("atk_type, ");
                SelSql.AppendLine("dfe_type, ");
                SelSql.AppendLine("mgr_type ");
                SelSql.AppendLine("FROM mt_guest_battle_ability");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_guest_battle_ability);

                SelSql = new StringBuilder();
                #region TABLE <mt_guest_have_item>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("guest_id, ");
                SelSql.AppendLine("equip_main, ");
                SelSql.AppendLine("equip_sub, ");
                SelSql.AppendLine("equip_head, ");
                SelSql.AppendLine("equip_body, ");
                SelSql.AppendLine("equip_accesory1, ");
                SelSql.AppendLine("equip_accesory2 ");
                SelSql.AppendLine("FROM mt_guest_have_item");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_guest_have_item);

                SelSql = new StringBuilder();
                #region TABLE <mt_guest_action>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("guest_id, ");
                SelSql.AppendLine("action_no, ");
                SelSql.AppendLine("action_target, ");
                SelSql.AppendLine("action, ");
                SelSql.AppendLine("perks_id, ");
                SelSql.AppendLine("limit_level ");
                SelSql.AppendLine("FROM mt_guest_action");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_guest_action);

                SelSql = new StringBuilder();
                #region TABLE <mt_guest_serif>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("guest_id, ");
                SelSql.AppendLine("serif_no, ");
                SelSql.AppendLine("situation, ");
                SelSql.AppendLine("perks_id, ");
                SelSql.AppendLine("serif_text");
                SelSql.AppendLine("FROM mt_guest_serif");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_guest_serif);
            }
        }
    }
}
