using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using System.Collections;
using System.Text.RegularExpressions;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// モンスター基本情報扱い
    /// </summary>
    public static class LibMonsterData
    {
        public static MonsterDataEntity Entity;

        static LibMonsterData()
        {
            LoadMonster();
        }

        public static void LoadMonster()
        {
            Entity = new MonsterDataEntity();

            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_monster_list>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("monster_id, ");
                SelSql.AppendLine("monster_name, ");
                SelSql.AppendLine("category_id, ");
                SelSql.AppendLine("formation, ");
                SelSql.AppendLine("max_multi_act, ");
                SelSql.AppendLine("multi_act_prob, ");
                SelSql.AppendLine("target, ");
                SelSql.AppendLine("belong_kb ");
                SelSql.AppendLine("FROM [mt_monster_list]");
                SelSql.AppendLine("ORDER BY monster_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_monster_list);

                SelSql = new StringBuilder();
                #region TABLE <mt_monster_battle_ability>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("monster_id, ");
                SelSql.AppendLine("level, ");
                SelSql.AppendLine("hp, ");
                SelSql.AppendLine("mp, ");
                SelSql.AppendLine("str, ");
                SelSql.AppendLine("agi, ");
                SelSql.AppendLine("mag, ");
                SelSql.AppendLine("unq, ");
                SelSql.AppendLine("atk, ");
                SelSql.AppendLine("sub_atk, ");
                SelSql.AppendLine("dfe, ");
                SelSql.AppendLine("mgr, ");
                SelSql.AppendLine("avd, ");
                SelSql.AppendLine("exp ");
                SelSql.AppendLine("FROM [mt_monster_battle_ability]");
                SelSql.AppendLine("ORDER BY monster_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_monster_battle_ability);

                SelSql = new StringBuilder();
                #region TABLE <mt_monster_have_item>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("monster_id,");
                SelSql.AppendLine("drop_type, ");
                SelSql.AppendLine("get_synx, ");
                SelSql.AppendLine("it_num, ");
                SelSql.AppendLine("it_box_count");
                SelSql.AppendLine("FROM mt_monster_have_item");
                SelSql.AppendLine("ORDER BY monster_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_monster_have_item);

                SelSql = new StringBuilder();
                #region TABLE <mt_monster_element>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("monster_id,");
                SelSql.AppendLine("atkordfe,");
                SelSql.AppendLine("fire,");
                SelSql.AppendLine("freeze,");
                SelSql.AppendLine("air,");
                SelSql.AppendLine("earth,");
                SelSql.AppendLine("water,");
                SelSql.AppendLine("thunder,");
                SelSql.AppendLine("holy,");
                SelSql.AppendLine("dark,");
                SelSql.AppendLine("slash,");
                SelSql.AppendLine("pierce,");
                SelSql.AppendLine("strike,");
                SelSql.AppendLine("[break],");
                SelSql.AppendLine("status_list,");
                SelSql.AppendLine("charge,");
                SelSql.AppendLine("range,");
                SelSql.AppendLine("avoid_physical,");
                SelSql.AppendLine("avoid_magical");
                SelSql.AppendLine("FROM mt_monster_element");
                SelSql.AppendLine("ORDER BY monster_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_monster_element);

                SelSql = new StringBuilder();
                #region TABLE <mt_monster_action>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("monster_id,");
                SelSql.AppendLine("action_no, ");
                SelSql.AppendLine("timing1, ");
                SelSql.AppendLine("timing2, ");
                SelSql.AppendLine("timing3, ");
                SelSql.AppendLine("action_target, ");
                SelSql.AppendLine("action, ");
                SelSql.AppendLine("probability, ");
                SelSql.AppendLine("max_count,");
                SelSql.AppendLine("perks_id");
                SelSql.AppendLine("FROM mt_monster_action");
                SelSql.AppendLine("ORDER BY monster_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_monster_action);

                SelSql = new StringBuilder();
                #region TABLE <mt_monster_serif>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("monster_id, ");
                SelSql.AppendLine("serif_no, ");
                SelSql.AppendLine("situation, ");
                SelSql.AppendLine("perks_id, ");
                SelSql.AppendLine("serif_text ");
                SelSql.AppendLine("FROM mt_monster_serif");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_monster_serif);

                SelSql = new StringBuilder();
                #region TABLE <mt_monster_pop_weather>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("monster_id, ");
                SelSql.AppendLine("weather_id "); ;
                SelSql.AppendLine("FROM mt_monster_pop_weather");
                SelSql.AppendLine("ORDER BY monster_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_monster_pop_weather);

                SelSql = new StringBuilder();
                #region TABLE <mt_monster_category>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("category_id, ");
                SelSql.AppendLine("category_name "); ;
                SelSql.AppendLine("FROM mt_monster_category");
                SelSql.AppendLine("ORDER BY category_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_monster_category);
            }
        }

        /// <summary>
        /// ニックネームを取得
        /// </summary>
        /// <param name="MonsterID">モンスターID</param>
        /// <returns>ニックネーム</returns>
        public static string GetNickName(int MonsterID)
        {
            MonsterDataEntity.mt_monster_listRow row = Entity.mt_monster_list.FindBymonster_id(MonsterID);

            if (row == null)
            {
                return "";
            }

            return row.monster_name;
        }

        /// <summary>
        /// モンスターのDataRowを取得
        /// </summary>
        /// <param name="MonsterID">モンスターID</param>
        /// <returns>DataRow</returns>
        public static MonsterDataEntity.mt_monster_listRow GetMonsterRow(int MonsterID)
        {
            return Entity.mt_monster_list.FindBymonster_id(MonsterID);
        }

        /// <summary>
        /// カテゴリ名取得
        /// </summary>
        /// <param name="RaceID">カテゴリID</param>
        /// <returns>カテゴリ名</returns>
        public static string GetCategoryName(int CategoryID)
        {
            MonsterDataEntity.mt_monster_categoryRow row = Entity.mt_monster_category.FindBycategory_id(CategoryID);

            if (row != null)
            {
                return row.category_name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 天候名称取得
        /// </summary>
        /// <param name="MonsterID">モンスターID</param>
        static public string GetWeatherNames(int MonsterID)
        {
            StringBuilder WeatherBuilder = new StringBuilder();

            DataView WeatherView = new DataView(Entity.mt_monster_pop_weather);
            WeatherView.RowFilter = Entity.mt_monster_pop_weather.monster_idColumn.ColumnName + "=" + MonsterID;

            foreach (DataRowView weatherRow in WeatherView)
            {
                MonsterDataEntity.mt_monster_pop_weatherRow weatherThisRow = (MonsterDataEntity.mt_monster_pop_weatherRow)weatherRow.Row;

                if (WeatherBuilder.Length > 0) { WeatherBuilder.Append(", "); }
                WeatherBuilder.Append(LibWeather.GetWeatherName(weatherThisRow.weather_id));
            }

            if (WeatherView.Count == 0)
            {
                WeatherBuilder.Append("すべて");
            }

            return WeatherBuilder.ToString();
        }

        /// <summary>
        /// カテゴリID取得
        /// </summary>
        /// <param name="MonsterID">モンスターID</param>
        /// <returns>カテゴリID</returns>
        public static int GetCategoryID(int MonsterID)
        {
            MonsterDataEntity.mt_monster_listRow row = Entity.mt_monster_list.FindBymonster_id(MonsterID);

            if (row != null)
            {
                return row.category_id;
            }
            else
            {
                return 0;
            }
        }
    }
}
