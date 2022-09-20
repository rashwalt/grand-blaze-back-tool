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
        /// �����C�x���g��ID
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
        /// �N�G�X�g���A�}�[�N�����܂߂����̂��擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>�N�G�X�g�}�[�N����</returns>
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
                    return questRow.quest_name + "�y" + row.mark_name + "�z";
                }
            }
            else
            {
                return "���ׂĂ̂͂��܂�";
            }
        }

        /// <summary>
        /// �}�[�N���̂��擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>�}�[�N����</returns>
        public static string GetMarkName(int MarkID)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);

            if (row != null)
            {
                return row.mark_name;
            }
            else
            {
                return "���ׂĂ̂͂��܂�";
            }
        }

        /// <summary>
        /// �N�G�X�g���̂��擾
        /// </summary>
        /// <param name="MarkID">�G���AID</param>
        /// <returns>�G���A����</returns>
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
                return "�͂��܂�̏ꏊ";
            }
        }

        /// <summary>
        /// �N�G�X�gDataRow�擾
        /// </summary>
        /// <param name="QuestID">�N�G�X�gID</param>
        /// <returns>�N�G�X�gDataRow</returns>
        public static QuestDataEntity.quest_listRow GetQuestRow(int QuestID)
        {
            return Entity.mt_quest_list.FindByquest_id(QuestID);
        }

        /// <summary>
        /// �N�G�X�g���̎擾
        /// </summary>
        /// <param name="QuestID">�N�G�X�gID</param>
        /// <returns>����</returns>
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
        /// �N�G�X�g�X�e�b�vDataRow
        /// </summary>
        /// <param name="QuestID">�N�G�X�gID</param>
        /// <param name="QuestStep">�X�e�b�v</param>
        /// <returns>�X�e�b�vDataRow</returns>
        public static QuestDataEntity.mt_quest_stepRow GetQuestStepRow(int QuestID, int QuestStep)
        {
            return Entity.mt_quest_step.FindByquest_idquest_step(QuestID, QuestStep);
        }

        /// <summary>
        /// �N�G�X�g�X�e�b�vDataView
        /// </summary>
        /// <param name="QuestID">�N�G�X�gID</param>
        /// <param name="QuestStep">�X�e�b�v</param>
        /// <returns>�X�e�b�vDataRow</returns>
        public static DataView GetQuestStepList(int QuestID, int QuestStep)
        {
            DataView QuestView = new DataView(Entity.mt_quest_step);
            QuestView.RowFilter = "quest_id=" + QuestID + " and quest_step<" + QuestStep;
            QuestView.Sort = "quest_step desc";
            return QuestView;
        }

        /// <summary>
        /// �N�G�X�g�^�C�v���̎擾
        /// </summary>
        /// <param name="QuestType">�N�G�X�g�^�C�vID</param>
        /// <returns>����</returns>
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
        /// �}�[�N���݊m�F
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>���݉�</returns>
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
        /// �}�[�NID�擾
        /// </summary>
        /// <param name="QuestID">�N�G�X�gID</param>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>�}�[�NID</returns>
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
        /// �f�t�H���g�̃}�[�NID�擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>�}�[�NID</returns>
        public static int GetDefaultMarkID(int MarkID)
        {
            int QuestID = GetQuestID(MarkID);

            Entity.mt_mark_list.DefaultView.RowFilter = "quest_id=" + QuestID + " and hide_mark=false";

            return (int)Entity.mt_mark_list.DefaultView[LibInteger.GetRand(Entity.mt_mark_list.DefaultView.Count)]["mark_id"];
        }

        /// <summary>
        /// �N�G�X�gID�擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>�N�G�X�gID</returns>
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
        /// �V�󌈒聕�擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <param name="UpdateCount">�X�V��</param>
        /// <returns>�V��ID</returns>
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
        /// �V�󌈒聕�擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>�V��ID</returns>
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
        /// �o�g���t�B�[���h�擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>�o�g���t�B�[���h</returns>
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
        /// �o�������X�^�[���{���擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>�{��</returns>
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
        /// �o�������X�^�[���X�g�擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>�����X�^�[���X�g</returns>
        public static DataTable GetPopMonsters(int MarkID)
        {
            Entity.mt_mark_pop_monster.DefaultView.RowFilter = "mark_id=" + MarkID;

            return Entity.mt_mark_pop_monster.DefaultView.ToTable();
        }

        /// <summary>
        /// �V�󖼏̎擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
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
                WeatherBuilder.Append("���ׂ�");
            }

            return WeatherBuilder.ToString();
        }

        /// <summary>
        /// �w��}�[�N�̉B���ݒ�
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>�B���Ȃ�^</returns>
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
        /// �G���A�N���`�F�b�N
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <param name="Target">�N���`�F�b�N��</param>
        /// <returns>�N��OK�H</returns>
        public static bool CheckInnerMark(int MarkID, LibPlayer Target)
        {
            QuestDataEntity.mt_mark_listRow row = Entity.mt_mark_list.FindBymark_id(MarkID);

            if (row == null)
            {
                return false;
            }

            bool IsOK = true;

            // �M�d�i
            if (row.key_item_id > 0 && !Target.CheckKeyItem(row.key_item_id))
            {
                IsOK = false;
            }

            // �����x��
            if (row.key_level > 0 && !Target.RemoveKey(row.key_level))
            {
                IsOK = false;
            }

            // ��̓��x��
            if (row.hack_level > 0 && !Target.RemoveHack(row.hack_level))
            {
                IsOK = false;
            }
            

            return IsOK;
        }

        /// <summary>
        /// �}�[�N��DataRow�擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>DataRow</returns>
        public static QuestDataEntity.mt_mark_listRow GetMarkRow(int MarkID)
        {
            return Entity.mt_mark_list.FindBymark_id(MarkID);
        }

        /// <summary>
        /// �񕜃��[�g�̎擾
        /// </summary>
        /// <param name="MarkID">�}�[�NID</param>
        /// <returns>�񕜃��[�g</returns>
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
        /// �n���E�B�[���C�x���g���H�i�A�b�v�f�[�g�ł͎��T���������𔻕ʂ��邱�Ɓj
        /// </summary>
        public static bool IsHalloween
        {
            get
            {
                return (OfficialEventID == Status.OfficialEvent.Halloween);
            }
        }

        /// <summary>
        /// �J�Ò��̃C�x���g��
        /// </summary>
        /// <returns>�C�x���g��</returns>
        public static string GetOfficialEventName()
        {
            switch (OfficialEventID)
            {
                case Status.OfficialEvent.NewYear:
                    return "�����܂��Ă��߂łƂ��I";
                case Status.OfficialEvent.Hinamatsuri:
                    return "�ЂȂ܂�";
                case Status.OfficialEvent.Buosai:
                    return "���c��";
                case Status.OfficialEvent.SummerFestival:
                    return "�V����";
                case Status.OfficialEvent.Halloween:
                    return "Trick OR Treat";
                case Status.OfficialEvent.Christmas:
                    return "�Z���g�E�N���X�}�X";
                default:
                    return "";
            }
        }

        /// <summary>
        /// �C�x���g�X�N���v�g�擾
        /// </summary>
        /// <param name="EventType">�C�x���g�^�C�v</param>
        /// <param name="MarkID">�}�[�NID</param>
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
        /// �V��v�Z�ݒ�
        /// </summary>
        public static void CalcWeather(int UpdateCount)
        {
            // �ߋ��͍̂폜
            foreach (QuestMarkWeatherEntity.ts_mark_weather_scheduleRow markWeatherRow in WeatherEntity.ts_mark_weather_schedule.Select("count_id<" + (UpdateCount - 3)))
            {
                markWeatherRow.Delete();
            }

            foreach (QuestDataEntity.mt_mark_listRow markRow in Entity.mt_mark_list)
            {
                // 3��܂ł��邩�ȁH�Ȃ����ȁH
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
