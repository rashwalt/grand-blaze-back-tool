using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;

namespace DataEditForm.Quest.Weather
{
    public partial class WeatherSettingDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WeatherSettingDialog()
        {
            InitializeComponent();
        }

        private int _targetMarkID;

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int MarkID
        {
            get
            {
                return _targetMarkID;
            }
            set
            {
                _targetMarkID = value;
            }
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeatherSettingDialog_Load(object sender, EventArgs e)
        {
            this.checkedListBoxWeather.Items.Clear();
            foreach (WeatherDataEntity.mt_weather_listRow WeatherRow in LibWeather.Entity.mt_weather_list)
            {
                bool isCheck = false;

                QuestDataEntity.mt_mark_weatherRow weatherThisRow = LibQuest.Entity.mt_mark_weather.FindBymark_idweather_id(_targetMarkID, WeatherRow.weather_id);

                if (weatherThisRow != null) { isCheck = true; }

                this.checkedListBoxWeather.Items.Add(WeatherRow.weather_id.ToString("000") + ": " + WeatherRow.weather_name, isCheck);
            }
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            int i = 0;

            List<int> WeatherListArray = new List<int>();

            for (i = 0; i < this.checkedListBoxWeather.Items.Count; i++)
            {
                if (this.checkedListBoxWeather.GetItemChecked(i))
                {
                    string[] Weathers = this.checkedListBoxWeather.Items[i].ToString().Split(':');
                    WeatherListArray.Add(int.Parse(Weathers[0]));
                }
            }

            QuestDataEntity.mt_mark_weatherRow[] weatherRows = (QuestDataEntity.mt_mark_weatherRow[])LibQuest.Entity.mt_mark_weather.Select(LibQuest.Entity.mt_mark_weather.mark_idColumn.ColumnName + "=" + _targetMarkID);

            foreach (QuestDataEntity.mt_mark_weatherRow weatherRow in weatherRows)
            {
                weatherRow.Delete();
            }

            // 登録
            for (int j = 0; j < WeatherListArray.Count; j++)
            {
                QuestDataEntity.mt_mark_weatherRow weatherNewRow = LibQuest.Entity.mt_mark_weather.Newmt_mark_weatherRow();
                weatherNewRow.mark_id = _targetMarkID;
                weatherNewRow.weather_id = WeatherListArray[j];
                LibQuest.Entity.mt_mark_weather.Addmt_mark_weatherRow(weatherNewRow);
            }

            this.Close();
        }
    }
}

