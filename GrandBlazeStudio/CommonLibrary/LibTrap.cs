using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// トラップ管理クラス
    /// </summary>
    public class LibTrap
    {
        public static TrapDataEntity Entity;

        static LibTrap()
        {
            LoadTrap();
        }

        public static void LoadTrap()
        {
            Entity = new TrapDataEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_trap_list>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("trap_id,");
                SelSql.AppendLine("trap_name,");
                SelSql.AppendLine("hp_damage,");
                SelSql.AppendLine("mp_damage,");
                SelSql.AppendLine("effect");
                SelSql.AppendLine("FROM mt_trap_list");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_trap_list);
            }
        }

        /// <summary>
        /// トラップ名称取得
        /// </summary>
        /// <param name="TrapID">トラップID</param>
        /// <returns>名称</returns>
        public static string GetTrapName(int TrapID)
        {
            TrapDataEntity.mt_trap_listRow row = Entity.mt_trap_list.FindBytrap_id(TrapID);

            if (row != null)
            {
                return row.trap_name;
            }

            return "";
        }

        /// <summary>
        /// HPダメージ取得
        /// </summary>
        /// <param name="TrapID">トラップID</param>
        /// <returns>HPダメージ率</returns>
        public static int GetHPDamagePercent(int TrapID)
        {
            TrapDataEntity.mt_trap_listRow row = Entity.mt_trap_list.FindBytrap_id(TrapID);

            if (row != null)
            {
                return row.hp_damage;
            }

            return 0;
        }

        /// <summary>
        /// MPダメージ取得
        /// </summary>
        /// <param name="TrapID">トラップID</param>
        /// <returns>MPダメージ率</returns>
        public static int GetMPDamagePercent(int TrapID)
        {
            TrapDataEntity.mt_trap_listRow row = Entity.mt_trap_list.FindBytrap_id(TrapID);

            if (row != null)
            {
                return row.mp_damage;
            }

            return 0;
        }

        /// <summary>
        /// エフェクト取得
        /// </summary>
        /// <param name="TrapID">トラップID</param>
        /// <returns>エフェクト</returns>
        public static EffectListEntity.effect_listDataTable GetEffectList(int TrapID)
        {
            TrapDataEntity.mt_trap_listRow row = Entity.mt_trap_list.FindBytrap_id(TrapID);

            EffectListEntity.effect_listDataTable EffectData = new EffectListEntity.effect_listDataTable();

            if (row != null)
            {
                LibEffect.Split(row.effect, ref EffectData);
            }

            return EffectData;
        }
    }
}
