using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// 天候管理クラス
    /// </summary>
    public static class LibWeather
    {
        public static WeatherDataEntity Entity;

        static LibWeather()
        {
            DataLoad();
        }

        static public void DataLoad()
        {
            Entity = new WeatherDataEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <mt_weather_list>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("weather_id,");
                Sql.AppendLine("weather_name,");
                Sql.AppendLine("fire, ");
                Sql.AppendLine("freeze, ");
                Sql.AppendLine("air, ");
                Sql.AppendLine("earth, ");
                Sql.AppendLine("water, ");
                Sql.AppendLine("thunder, ");
                Sql.AppendLine("holy, ");
                Sql.AppendLine("dark, ");
                Sql.AppendLine("bad_weather ");
                Sql.AppendLine("FROM mt_weather_list");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_weather_list);
            }
        }

        /// <summary>
        /// 天候名取得
        /// </summary>
        /// <param name="WeahterID">天候ID</param>
        /// <returns>天候名</returns>
        static public string GetWeatherName(int WeahterID)
        {
            WeatherDataEntity.mt_weather_listRow row = Entity.mt_weather_list.FindByweather_id(WeahterID);

            if (row == null)
            {
                return "";
            }

            return row.weather_name;
        }

        /// <summary>
        /// DataRow取得
        /// </summary>
        /// <param name="WeatherID">天候ID</param>
        /// <returns>DataRow</returns>
        static public WeatherDataEntity.mt_weather_listRow GetRow(int WeatherID)
        {
            return Entity.mt_weather_list.FindByweather_id(WeatherID);
        }
    }
}
