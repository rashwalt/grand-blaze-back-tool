using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;
using CommonLibrary.Character;
using System.IO;

namespace CommonLibrary
{
    public static class LibQuest
    {
        public static QuestDataEntity Entity;
        public static QuestMarkWeatherEntity WeatherEntity;

        static LibQuest()
        {
            LoadQuestList();
        }

        /// <summary>
        /// 公式イベントのID
        /// </summary>
        public static int OfficialEventID = 0;

        public static void LoadQuestList()
        {
            Entity = new QuestDataEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <mt_quest_list>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("quest_id, ");
                Sql.AppendLine("quest_name, ");
                Sql.AppendLine("quest_client, ");
                Sql.AppendLine("quest_type, ");
                Sql.AppendLine("pick_level, ");
                Sql.AppendLine("key_item_id, ");
                Sql.AppendLine("offer_quest_id, ");
                Sql.AppendLine("comp_quest_id, ");
                Sql.AppendLine("sp_level, ");
                Sql.AppendLine("class_id, ");
                Sql.AppendLine("class_level, ");
                Sql.AppendLine("bc_count, ");
                Sql.AppendLine("hide_fg, ");
                Sql.AppendLine("valid_fg ");
                Sql.AppendLine("FROM mt_quest_list");
                Sql.AppendLine("ORDER BY quest_id ");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_quest_list);

                Sql = new StringBuilder();
                #region TABLE <mt_quest_type>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("quest_type, ");
                Sql.AppendLine("quest_type_name ");
                Sql.AppendLine("FROM mt_quest_type");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_quest_type);

                Sql = new StringBuilder();
                #region TABLE <mt_quest_step>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("quest_id, ");
                Sql.AppendLine("quest_step, ");
                Sql.AppendLine("comment ");
                Sql.AppendLine("FROM mt_quest_step");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_quest_step);

                Sql = new StringBuilder();
                #region TABLE <mt_mark_list>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("mark_id, ");
                Sql.AppendLine("quest_id, ");
                Sql.AppendLine("mark_name, ");
                Sql.AppendLine("field_type, ");
                Sql.AppendLine("mark_trap, ");
                Sql.AppendLine("trap_hide,");
                Sql.AppendLine("trap_level,");
                Sql.AppendLine("cure_rate, ");
                Sql.AppendLine("key_item_id, ");
                Sql.AppendLine("key_level, ");
                Sql.AppendLine("hack_level, ");
                Sql.AppendLine("pop_monster_level, ");
                Sql.AppendLine("hide_mark ");
                Sql.AppendLine("FROM mt_mark_list");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_mark_list);

                Sql = new StringBuilder();
                #region TABLE <mt_mark_weather>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("mark_id, ");
                Sql.AppendLine("weather_id ");
                Sql.AppendLine("FROM mt_mark_weather");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_mark_weather);

                Sql = new StringBuilder();
                #region TABLE <mt_mark_pop_monster>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("mark_id, ");
                Sql.AppendLine("monster_id,");
                Sql.AppendLine("rare, ");
                Sql.AppendLine("pop_prob, ");
                Sql.AppendLine("time_count, ");
                Sql.AppendLine("limit_visitor ");
                Sql.AppendLine("FROM mt_mark_pop_monster");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_mark_pop_monster);
            }

            LoadMarkWeatherSelect();
        }

        public static void LoadMarkWeatherSelect()
        {
            WeatherEntity = new QuestMarkWeatherEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Tran))
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <ts_mark_weather_schedule>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("mark_id, ");
                Sql.AppendLine("count_id, ");
                Sql.AppendLine("weather_id");
                Sql.AppendLine("FROM ts_mark_weather_schedule");
                #endregion

                dba.Fill(Sql.ToString(), WeatherEntity.ts_mark_weather_schedule);
            }
        }

        /// <summary>
        /// クエスト名、マーク名を含めた名称を取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>クエストマーク名称</returns>
        public static string GetQuestMarkName(int MarkID)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);
            if (row != null)
            {
                QuestDataEntity.quest_listRow questRow = Entity.mt_quest_list.FindByquest_id(row.quest_id);

                if (questRow.quest_id <= 0)
                {
                    return row.mark_name;
                }
                else
                {
                    return questRow.quest_name + "【" + row.mark_name + "】";
                }
            }
            else
            {
                return "すべてのはじまり";
            }
        }

        /// <summary>
        /// マーク名称を取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>マーク名称</returns>
        public static string GetMarkName(int MarkID)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);

            if (row != null)
            {
                return row.mark_name;
            }
            else
            {
                return "すべてのはじまり";
            }
        }

        /// <summary>
        /// クエスト名称を取得
        /// </summary>
        /// <param name="MarkID">エリアID</param>
        /// <returns>エリア名称</returns>
        public static string GetQuestNameByMarkID(int MarkID)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);
            if (row != null)
            {
                QuestDataEntity.quest_listRow questRow = Entity.mt_quest_list.FindByquest_id(row.quest_id);

                return questRow.quest_name;
            }
            else
            {
                return "はじまりの場所";
            }
        }

        /// <summary>
        /// クエストDataRow取得
        /// </summary>
        /// <param name="QuestID">クエストID</param>
        /// <returns>クエストDataRow</returns>
        public static QuestDataEntity.quest_listRow GetQuestRow(int QuestID)
        {
            return Entity.mt_quest_list.FindByquest_id(QuestID);
        }

        /// <summary>
        /// クエスト名称取得
        /// </summary>
        /// <param name="QuestID">クエストID</param>
        /// <returns>名称</returns>
        public static string GetQuestName(int QuestID)
        {
            QuestDataEntity.quest_listRow row = Entity.mt_quest_list.FindByquest_id(QuestID);

            if (row != null)
            {
                return row.quest_name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// クエストステップDataRow
        /// </summary>
        /// <param name="QuestID">クエストID</param>
        /// <param name="QuestStep">ステップ</param>
        /// <returns>ステップDataRow</returns>
        public static QuestDataEntity.mt_quest_stepRow GetQuestStepRow(int QuestID, int QuestStep)
        {
            return Entity.mt_quest_step.FindByquest_idquest_step(QuestID, QuestStep);
        }

        /// <summary>
        /// クエストステップDataView
        /// </summary>
        /// <param name="QuestID">クエストID</param>
        /// <param name="QuestStep">ステップ</param>
        /// <returns>ステップDataRow</returns>
        public static DataView GetQuestStepList(int QuestID, int QuestStep)
        {
            DataView QuestView = new DataView(Entity.mt_quest_step);
            QuestView.RowFilter = "quest_id=" + QuestID + " and quest_step<" + QuestStep;
            QuestView.Sort = "quest_step desc";
            return QuestView;
        }

        /// <summary>
        /// クエストタイプ名称取得
        /// </summary>
        /// <param name="QuestType">クエストタイプID</param>
        /// <returns>名称</returns>
        public static string QuestTypeName(int QuestType)
        {
            QuestDataEntity.quest_typeRow row = Entity.mt_quest_type.FindByquest_type(QuestType);

            if (row != null)
            {
                return row.quest_type_name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// マーク存在確認
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>存在可否</returns>
        public static bool CheckMark(int MarkID)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);

            if (row != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// マークID取得
        /// </summary>
        /// <param name="QuestID">クエストID</param>
        /// <param name="MarkID">マークID</param>
        /// <returns>マークID</returns>
        public static int GetMarkID(int QuestID, int MarkID)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);
            if (row != null && row.quest_id == QuestID)
            {
                return row.mark_id;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// デフォルトのマークID取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>マークID</returns>
        public static int GetDefaultMarkID(int MarkID)
        {
            int QuestID = GetQuestID(MarkID);

            Entity.mt_mark_list.DefaultView.RowFilter = "quest_id=" + QuestID + " and hide_mark=false";

            return (int)Entity.mt_mark_list.DefaultView[LibInteger.GetRand(Entity.mt_mark_list.DefaultView.Count)]["mark_id"];
        }

        /// <summary>
        /// クエストID取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>クエストID</returns>
        public static int GetQuestID(int MarkID)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);

            if (row == null)
            {
                return 0;
            }

            return row.quest_id;
        }

        /// <summary>
        /// 天候決定＆取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <param name="UpdateCount">更新回数</param>
        /// <returns>天候ID</returns>
        public static int GetWeather(int MarkID, int UpdateCount)
        {
            QuestMarkWeatherEntity.ts_mark_weather_scheduleRow markWeatherRow = WeatherEntity.ts_mark_weather_schedule.FindBymark_idcount_id(MarkID, UpdateCount);

            if (markWeatherRow == null)
            {
                return 1;
            }

            return markWeatherRow.weather_id;
        }

        /// <summary>
        /// 天候決定＆取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>天候ID</returns>
        public static int GetWeatherRandom(int MarkID)
        {
            DataView markWeatherView = new DataView(LibQuest.Entity.mt_mark_weather);
            markWeatherView.RowFilter = LibQuest.Entity.mt_mark_weather.mark_idColumn.ColumnName + "=" + MarkID;

            if (markWeatherView.Count == 0)
            {
                return 1;
            }

            return (int)markWeatherView[LibInteger.GetRand(markWeatherView.Count)]["weather_id"];
        }

        /// <summary>
        /// バトルフィールド取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>バトルフィールド</returns>
        public static int GetFieldID(int MarkID)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);

            if (row == null)
            {
                return 0;
            }

            return row.field_type;
        }

        /// <summary>
        /// 出現モンスター数倍率取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>倍率</returns>
        public static int GetPopLevel(int MarkID)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);

            if (row == null)
            {
                return 0;
            }

            return (int)row.pop_monster_level;
        }

        /// <summary>
        /// 出現モンスターリスト取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>モンスターリスト</returns>
        public static DataTable GetPopMonsters(int MarkID)
        {
            Entity.mt_mark_pop_monster.DefaultView.RowFilter = "mark_id=" + MarkID;

            return Entity.mt_mark_pop_monster.DefaultView.ToTable();
        }

        /// <summary>
        /// 天候名称取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        static public string GetWeatherNames(int MarkID)
        {
            StringBuilder WeatherBuilder = new StringBuilder();

            DataView markWeatherView = new DataView(Entity.mt_mark_weather);
            markWeatherView.RowFilter = Entity.mt_mark_weather.mark_idColumn.ColumnName + "=" + MarkID;

            foreach (DataRowView weatherRow in markWeatherView)
            {
                QuestDataEntity.mt_mark_weatherRow weatherThisRow = (QuestDataEntity.mt_mark_weatherRow)weatherRow.Row;

                if (WeatherBuilder.Length > 0) { WeatherBuilder.Append(", "); }
                WeatherBuilder.Append(LibWeather.GetWeatherName(weatherThisRow.weather_id));
            }

            if (markWeatherView.Count == 0)
            {
                WeatherBuilder.Append("すべて");
            }

            return WeatherBuilder.ToString();
        }

        /// <summary>
        /// 指定マークの隠し設定
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>隠しなら真</returns>
        public static bool CheckHide(int MarkID)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);

            if (row == null)
            {
                return true;
            }

            return row.hide_mark;
        }

        /// <summary>
        /// エリア侵入チェック
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <param name="Target">侵入チェック者</param>
        /// <returns>侵入OK？</returns>
        public static bool CheckInnerMark(int MarkID, LibPlayer Target)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);

            if (row == null)
            {
                return false;
            }

            bool IsOK = true;

            // 貴重品
            if (row.key_item_id > 0 && !Target.CheckKeyItem(row.key_item_id))
            {
                IsOK = false;
            }

            // 鍵レベル
            if (row.key_level > 0 && !Target.RemoveKey(row.key_level))
            {
                IsOK = false;
            }

            // 解析レベル
            if (row.hack_level > 0 && !Target.RemoveHack(row.hack_level))
            {
                IsOK = false;
            }
            

            return IsOK;
        }

        /// <summary>
        /// マークのDataRow取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>DataRow</returns>
        public static QuestDataEntity.mt_mark_listRow GetMarkRow(int MarkID)
        {
            return Entity.mt_mark_list.FindBymark_id(MarkID);
        }

        /// <summary>
        /// 回復レートの取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>回復レート</returns>
        public static int GetCureRate(int MarkID)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);

            if (row == null)
            {
                return 0;
            }

            return row.cure_rate;
        }

        /// <summary>
        /// ハロウィーンイベントか？（アップデートでは次週がそうかを判別すること）
        /// </summary>
        public static bool IsHalloween
        {
            get
            {
                return (OfficialEventID == Status.OfficialEvent.Halloween);
            }
        }

        /// <summary>
        /// 開催中のイベント名
        /// </summary>
        /// <returns>イベント名</returns>
        public static string GetOfficialEventName()
        {
            switch (OfficialEventID)
            {
                case Status.OfficialEvent.NewYear:
                    return "あけましておめでとう！";
                case Status.OfficialEvent.Hinamatsuri:
                    return "ひなまつり";
                case Status.OfficialEvent.Buosai:
                    return "武皇祭";
                case Status.OfficialEvent.SummerFestival:
                    return "天龍祭";
                case Status.OfficialEvent.Halloween:
                    return "Trick OR Treat";
                case Status.OfficialEvent.Christmas:
                    return "セント・クリスマス";
                default:
                    return "";
            }
        }

        /// <summary>
        /// イベントスクリプト取得
        /// </summary>
        /// <param name="EventType">イベントタイプ</param>
        /// <param name="MarkID">マークID</param>
        public static string GetEventScript(int EventType, int MarkID)
        {
            switch (EventType)
            {
                case Status.EventPopType.BattleBefore:
                    {
                        string EventScriptFile = LibCommonLibrarySettings.EventScriptDir + "event_" + MarkID.ToString("000000") + "_a.py";
                        return File.ReadAllText(EventScriptFile, Encoding.UTF8);
                    }
                case Status.EventPopType.BattleAfter:
                    {
                        string EventScriptFile = LibCommonLibrarySettings.EventScriptDir + "event_" + MarkID.ToString("000000") + "_z.py";
                        return File.ReadAllText(EventScriptFile, Encoding.UTF8);
                    }
            }
            return "";
        }

        /// <summary>
        /// 天候計算設定
        /// </summary>
        public static void CalcWeather(int UpdateCount)
        {
            // 過去のは削除
            foreach (QuestMarkWeatherEntity.ts_mark_weather_scheduleRow markWeatherRow in WeatherEntity.ts_mark_weather_schedule.Select("count_id<" + (UpdateCount - 3)))
            {
                markWeatherRow.Delete();
            }

            foreach (QuestDataEntity.mt_mark_listRow markRow in Entity.mt_mark_list)
            {
                // 3個先まであるかな？ないかな？
                for (int countPlus = 0; countPlus < 4; countPlus++)
                {
                    QuestMarkWeatherEntity.ts_mark_weather_scheduleRow markWeatherRow = WeatherEntity.ts_mark_weather_schedule.FindBymark_idcount_id(markRow.mark_id, UpdateCount + countPlus);
                    if (markWeatherRow == null)
                    {
                        QuestMarkWeatherEntity.ts_mark_weather_scheduleRow newMarkWeatherRow = WeatherEntity.ts_mark_weather_schedule.Newts_mark_weather_scheduleRow();
                        newMarkWeatherRow.mark_id = markRow.mark_id;
                        newMarkWeatherRow.count_id = UpdateCount + countPlus;
                        newMarkWeatherRow.weather_id = LibQuest.GetWeatherRandom(markRow.mark_id);
                        WeatherEntity.ts_mark_weather_schedule.Addts_mark_weather_scheduleRow(newMarkWeatherRow);
                    }
                }
            }


            LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Tran);

            try
            {
                dba.BeginTransaction();
                dba.Update(WeatherEntity.ts_mark_weather_schedule);

                dba.Commit();
            }
            catch
            {
                dba.Rollback();
            }
            finally
            {
                dba.Dispose();
            }

            LibQuest.LoadMarkWeatherSelect();
        }
    }
}
