using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    public static class LibSetBonus
    {
        public static SetBonusEntity Entity;

        static LibSetBonus()
        {
            LoadSetBonus();
        }

        public static void LoadSetBonus()
        {
            Entity = new SetBonusEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <mt_set_bonus_list>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("set_id, ");
                Sql.AppendLine("set_name, ");
                Sql.AppendLine("set_effect, ");
                Sql.AppendLine("equip_main, ");
                Sql.AppendLine("equip_sub, ");
                Sql.AppendLine("equip_head, ");
                Sql.AppendLine("equip_body, ");
                Sql.AppendLine("equip_accesory ");
                Sql.AppendLine("FROM mt_set_bonus_list");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_set_bonus_list);
            }
        }

        /// <summary>
        /// セットボーナス名取得
        /// </summary>
        /// <param name="SetID">セットボーナスID</param>
        /// <returns>セットボーナス名</returns>
        public static string GetSetName(int SetID)
        {
            SetBonusEntity.mt_set_bonus_listRow row = Entity.mt_set_bonus_list.FindByset_id(SetID);

            if (row != null)
            {
                return row.set_name;
            }

            return "";
        }

        /// <summary>
        /// セットボーナスDataRow取得
        /// </summary>
        /// <param name="SetID">セットボーナスID</param>
        /// <returns>DataRow</returns>
        public static SetBonusEntity.mt_set_bonus_listRow GetDataRow(int SetID)
        {
            return Entity.mt_set_bonus_list.FindByset_id(SetID);
        }
    }
}
