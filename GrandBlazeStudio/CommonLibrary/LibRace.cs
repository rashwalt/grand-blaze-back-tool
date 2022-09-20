using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    public static class LibRace
    {
        public static HumanRaceEntity Entity;

        static LibRace()
        {
            Entity = new HumanRaceEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_race_list>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("race_id,");
                SelSql.AppendLine("race_name,");
                SelSql.AppendLine("up_hp,");
                SelSql.AppendLine("up_mp,");
                SelSql.AppendLine("up_str,");
                SelSql.AppendLine("up_agi,");
                SelSql.AppendLine("up_mag,");
                SelSql.AppendLine("up_unq");
                SelSql.AppendLine("FROM mt_race_list");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_race_list);
            }
        }

        /// <summary>
        /// 種族名取得
        /// </summary>
        /// <param name="RaceID">種族ID</param>
        /// <returns>種族名</returns>
        public static string GetRaceName(int RaceID)
        {
            HumanRaceEntity.mt_race_listRow row = Entity.mt_race_list.FindByrace_id(RaceID);

            if (row != null)
            {
                return row.race_name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 成長ランクリスト取得
        /// </summary>
        /// <param name="RaceID">種族ID</param>
        /// <returns>DataRow</returns>
        public static HumanRaceEntity.mt_race_listRow GetRow(int RaceID)
        {
            return Entity.mt_race_list.FindByrace_id(RaceID); ;
        }
    }
}
