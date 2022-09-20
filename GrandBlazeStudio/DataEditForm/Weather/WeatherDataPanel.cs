using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;
using CommonLibrary.DataAccess;

namespace DataEditForm.Weather
{
    public partial class WeatherDataPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WeatherDataPanel()
        {
            InitializeComponent();

            NothingFilter();
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Panel_Load(object sender, EventArgs e)
        {
            LoadData();
            if (this.dataGridViewList.SelectedCells.Count > 0 && this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value != null)
            {
                PrivateView((int)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value);
            }
            else
            {
                PrivateView(0);
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        public override void Cancel()
        {
            WeatherDataEntity entity = LibWeather.Entity;
            entity.RejectChanges();
        }

        private DataView WeatherView;

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            WeatherDataEntity entity = LibWeather.Entity;
            WeatherView = new DataView(entity.mt_weather_list);
            WeatherView.Sort = entity.mt_weather_list.weather_idColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = WeatherView;
            this.columnNo.DataPropertyName = entity.mt_weather_list.weather_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.mt_weather_list.weather_nameColumn.ColumnName;
        }

        /// <summary>
        /// 選択：詳細表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dataGridViewList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            base.dataGridViewList_CellMouseClick(sender, e);

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && this.dataGridViewList[0, e.RowIndex].Value != null)
            {
                UpdateEntity();
                PrivateView((int)this.dataGridViewList[0, e.RowIndex].Value);
            }
        }

        /// <summary>
        /// コンテキストメニュー判定（リスト）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void contextMenuStripList_Opening(object sender, CancelEventArgs e)
        {
            // コピー、削除を有効に
            if (this.dataGridViewList.SelectedCells.Count > 0 && this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value != null)
            {
                this.toolStripMenuItemCopy.Enabled = true;
                this.toolStripMenuItemDelete.Enabled = true;
            }
        }

        /// <summary>
        /// 複製
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewList.SelectedCells.Count > 0 && this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value != null)
            {
                // 複製実行
                CopyData((int)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value);
            }
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            PrivateView(0);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewList.SelectedCells.Count > 0 && this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value != null)
            {
                DeleteData((int)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value, this.dataGridViewList.SelectedCells[0].RowIndex);
            }
        }

        /// <summary>
        /// 詳細表示
        /// </summary>
        /// <param name="WeatherID">表示対象ID</param>
        private void PrivateView(int WeatherID)
        {
            WeatherDataEntity entity = LibWeather.Entity;

            if (WeatherID == 0)
            {
                WeatherID = LibInteger.GetNewUnderNum(entity.mt_weather_list, entity.mt_weather_list.weather_idColumn.ColumnName);
            }

            // 表示

            WeatherDataEntity.mt_weather_listRow baseRow = entity.mt_weather_list.FindByweather_id(WeatherID);

            this.textBoxNo.Text = WeatherID.ToString();

            if (baseRow == null)
            {
                // 新規に行追加
                baseRow = entity.mt_weather_list.Addmt_weather_listRow(WeatherID, "", 0, 0, 0, 0, 0, 0, 0, 0, false);
            }

            this.textBoxName.Text = baseRow.weather_name;

            this.comboBoxElementFire.SelectedIndex = baseRow.fire + 1;
            this.comboBoxElementFreeze.SelectedIndex = baseRow.freeze + 1;
            this.comboBoxElementAir.SelectedIndex = baseRow.air + 1;
            this.comboBoxElementEarth.SelectedIndex = baseRow.earth + 1;
            this.comboBoxElementWater.SelectedIndex = baseRow.water + 1;
            this.comboBoxElementThunder.SelectedIndex = baseRow.thunder + 1;
            this.comboBoxElementHoly.SelectedIndex = baseRow.holy + 1;
            this.comboBoxElementDark.SelectedIndex = baseRow.dark + 1;

            this.checkBoxBadWeather.Checked = baseRow.bad_weather;
        }

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="WeatherID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int WeatherID, int rowIndex)
        {
            WeatherDataEntity entity = LibWeather.Entity;

            WeatherDataEntity.mt_weather_listRow tarentRow = entity.mt_weather_list.FindByweather_id(WeatherID);

            if (tarentRow == null) { return; }

            tarentRow.Delete();
        }

        /// <summary>
        /// データ複製
        /// </summary>
        /// <param name="WeatherID">対象ID</param>
        private void CopyData(int WeatherID)
        {
            WeatherDataEntity entity = LibWeather.Entity;

            WeatherDataEntity.mt_weather_listRow tarentRow = entity.mt_weather_list.FindByweather_id(WeatherID);

            if (tarentRow == null) { return; }

            int newID = LibInteger.GetNewUnderNum(entity.mt_weather_list, entity.mt_weather_list.weather_idColumn.ColumnName);

            // 上位から複製
            WeatherDataEntity.mt_weather_listRow newWeatherRow = entity.mt_weather_list.Newmt_weather_listRow();
            newWeatherRow.ItemArray = tarentRow.ItemArray;
            newWeatherRow.weather_id = newID;

            entity.mt_weather_list.Addmt_weather_listRow(newWeatherRow);
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            WeatherDataEntity entity = LibWeather.Entity;

            return entity.GetChanges() != null;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            UpdateEntity();

            LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master);

            try
            {
                dba.BeginTransaction();
                dba.Update(LibWeather.Entity.mt_weather_list);
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

            LibWeather.DataLoad();
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            WeatherDataEntity entity = LibWeather.Entity;

            if (this.textBoxName.Text.Length == 0) { return; }

            WeatherDataEntity.mt_weather_listRow row = entity.mt_weather_list.FindByweather_id(int.Parse(this.textBoxNo.Text));
            bool isAdd = false;

            if (row == null)
            {
                // 新規追加
                row = entity.mt_weather_list.Newmt_weather_listRow();
                isAdd = true;
                row.weather_id = int.Parse(this.textBoxNo.Text);
            }

            if (isAdd || row.weather_name != this.textBoxName.Text) { row.weather_name = this.textBoxName.Text; }

            if (isAdd || (row.fire + 1) != this.comboBoxElementFire.SelectedIndex) { row.fire = this.comboBoxElementFire.SelectedIndex - 1; }
            if (isAdd || (row.freeze + 1) != this.comboBoxElementFreeze.SelectedIndex) { row.freeze = this.comboBoxElementFreeze.SelectedIndex - 1; }
            if (isAdd || (row.air + 1) != this.comboBoxElementAir.SelectedIndex) { row.air = this.comboBoxElementAir.SelectedIndex - 1; }
            if (isAdd || (row.earth + 1) != this.comboBoxElementEarth.SelectedIndex) { row.earth = this.comboBoxElementEarth.SelectedIndex - 1; }
            if (isAdd || (row.water + 1) != this.comboBoxElementWater.SelectedIndex) { row.water = this.comboBoxElementWater.SelectedIndex - 1; }
            if (isAdd || (row.thunder + 1) != this.comboBoxElementThunder.SelectedIndex) { row.thunder = this.comboBoxElementThunder.SelectedIndex - 1; }
            if (isAdd || (row.holy + 1) != this.comboBoxElementHoly.SelectedIndex) { row.holy = this.comboBoxElementHoly.SelectedIndex - 1; }
            if (isAdd || (row.dark + 1) != this.comboBoxElementDark.SelectedIndex) { row.dark = this.comboBoxElementDark.SelectedIndex - 1; }

            if (isAdd || row.bad_weather != this.checkBoxBadWeather.Checked) { row.bad_weather = this.checkBoxBadWeather.Checked; }

            if (isAdd)
            {
                entity.mt_weather_list.Addmt_weather_listRow(row);
            }
        }
    }
}
