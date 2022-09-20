using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;
using DataEditForm.Monster.Common;

namespace DataEditForm.Monster.Data
{
    public partial class MonsterDataPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MonsterDataPanel()
        {
            InitializeComponent();

            // データバインド
            this.comboBoxCategory.DataSource = LibMonsterData.Entity.mt_monster_category;
            this.comboBoxCategory.DisplayMember = LibMonsterData.Entity.mt_monster_category.category_nameColumn.ColumnName;
            this.comboBoxCategory.ValueMember = LibMonsterData.Entity.mt_monster_category.category_idColumn.ColumnName;

            this.comboBoxTarget.DataSource = LibAction.Entity.mt_target_list;
            this.comboBoxTarget.DisplayMember = LibAction.Entity.mt_target_list.target_act_nameColumn.ColumnName;
            this.comboBoxTarget.ValueMember = LibAction.Entity.mt_target_list.target_idColumn.ColumnName;

            this.textBoxNo.Text = "0";

            this.checkedListBoxPopWeather.Items.Clear();
            foreach (WeatherDataEntity.mt_weather_listRow WeatherRow in LibWeather.Entity.mt_weather_list)
            {
                this.checkedListBoxPopWeather.Items.Add(WeatherRow.weather_id.ToString("000") + ": " + WeatherRow.weather_name, false);
            }
        }

        private DataView _monsterView;
        private DataTable MonsterPopTable = new DataTable();

        private int SelectedNo
        {
            get { return int.Parse(this.textBoxNo.Text); }
        }

        /// <summary>
        /// 初期表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Panel_Load(object sender, EventArgs e)
        {
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_mark_pop_monster>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("mt_mark_pop_monster.monster_id,");
                SelSql.AppendLine("mt_mark_pop_monster.mark_id,");
                SelSql.AppendLine("mt_quest_list.quest_name, ");
                SelSql.AppendLine("mt_mark_list.mark_name");
                SelSql.AppendLine("FROM mt_mark_pop_monster INNER JOIN (mt_mark_list INNER JOIN mt_quest_list ON mt_quest_list.quest_id = mt_mark_list.quest_id) ON mt_mark_pop_monster.mark_id = mt_mark_list.mark_id");
                #endregion

                MonsterPopTable = dba.GetTableData(SelSql.ToString());
            }

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
            MonsterDataEntity entity = LibMonsterData.Entity;
            entity.RejectChanges();
        }

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            MonsterDataEntity entity = LibMonsterData.Entity;
            _monsterView = new DataView(entity.mt_monster_list);
            _monsterView.RowFilter = "";
            _monsterView.Sort = entity.mt_monster_list.monster_idColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = _monsterView;
            this.columnNo.DataPropertyName = entity.mt_monster_list.monster_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.mt_monster_list.monster_nameColumn.ColumnName;
        }

        /// <summary>
        /// 詳細表示
        /// </summary>
        /// <param name="MonsterID">表示対象ID</param>
        private void PrivateView(int MonsterID)
        {
            if (MonsterID < 0) { return; }

            MonsterDataEntity entity = LibMonsterData.Entity;

            if (MonsterID == 0)
            {
                MonsterID = LibInteger.GetNewUnderNum(entity.mt_monster_list, entity.mt_monster_list.monster_idColumn.ColumnName);
            }

            // 基本情報の表示
            MonsterDataEntity.mt_monster_listRow baseRow = entity.mt_monster_list.FindBymonster_id(MonsterID);

            this.textBoxNo.Text = MonsterID.ToString();

            DataView MonsterAbility = new DataView(LibMonsterData.Entity.mt_monster_battle_ability);

            MonsterAbility.RowFilter = entity.mt_monster_battle_ability.monster_idColumn.ColumnName + "=" + MonsterID;
            MonsterAbility.Sort = entity.mt_monster_battle_ability.levelColumn.ColumnName;

            this.dataGridViewBattleAbility.AutoGenerateColumns = false;
            this.dataGridViewBattleAbility.DataSource = MonsterAbility;
            this.columnMonsterID.DataPropertyName = entity.mt_monster_battle_ability.monster_idColumn.ColumnName;
            this.columnLevel.DataPropertyName = entity.mt_monster_battle_ability.levelColumn.ColumnName;
            this.columnHP.DataPropertyName = entity.mt_monster_battle_ability.hpColumn.ColumnName;
            this.columnMP.DataPropertyName = entity.mt_monster_battle_ability.mpColumn.ColumnName;
            this.columnSTR.DataPropertyName = entity.mt_monster_battle_ability.strColumn.ColumnName;
            this.columnAGI.DataPropertyName = entity.mt_monster_battle_ability.agiColumn.ColumnName;
            this.columnMAG.DataPropertyName = entity.mt_monster_battle_ability.magColumn.ColumnName;
            this.columnUNQ.DataPropertyName = entity.mt_monster_battle_ability.unqColumn.ColumnName;
            this.columnATK.DataPropertyName = entity.mt_monster_battle_ability.atkColumn.ColumnName;
            this.columnSubATK.DataPropertyName = entity.mt_monster_battle_ability.sub_atkColumn.ColumnName;
            this.columnDFE.DataPropertyName = entity.mt_monster_battle_ability.dfeColumn.ColumnName;
            this.columnMGR.DataPropertyName = entity.mt_monster_battle_ability.mgrColumn.ColumnName;
            this.columnAVD.DataPropertyName = entity.mt_monster_battle_ability.avdColumn.ColumnName;
            this.columnEXP.DataPropertyName = entity.mt_monster_battle_ability.expColumn.ColumnName;

            if (baseRow == null)
            {
                // 新規に行追加
                this.textBoxName.Text = "";

                this.comboBoxCategory.SelectedIndex = 0;

                this.numericUpDownMaxMultiAction.Value = 1;
                this.numericUpDownMultiActionProb.Value = 0;

                this.comboBoxTarget.SelectedIndex = 0;
                this.comboBoxFormation.SelectedIndex = 0;
                this.comboBoxMonsterKb.SelectedIndex = 0;

                for (int i = 0; i < this.checkedListBoxPopWeather.Items.Count; i++)
                {
                    this.checkedListBoxPopWeather.SetItemChecked(i, false);
                }

                this.listBoxPop.Items.Clear();

                return;
            }

            this.textBoxName.Text = baseRow.monster_name;

            this.comboBoxCategory.SelectedValue = baseRow.category_id;

            this.numericUpDownMaxMultiAction.Value = baseRow.max_multi_act;
            this.numericUpDownMultiActionProb.Value = baseRow.multi_act_prob;

            this.comboBoxTarget.SelectedValue = baseRow.target;
            this.comboBoxFormation.SelectedIndex = baseRow.formation;
            this.comboBoxMonsterKb.SelectedIndex = baseRow.belong_kb;

            // 天候候補表示
            for (int i = 0; i < this.checkedListBoxPopWeather.Items.Count; i++)
            {
                string[] Weathers = this.checkedListBoxPopWeather.Items[i].ToString().Split(':');
                MonsterDataEntity.mt_monster_pop_weatherRow row = entity.mt_monster_pop_weather.FindBymonster_idweather_id(baseRow.monster_id, int.Parse(Weathers[0]));

                if (row != null)
                {
                    this.checkedListBoxPopWeather.SetItemChecked(i, true);
                }
                else
                {
                    this.checkedListBoxPopWeather.SetItemChecked(i, false);
                }
            }

            DataView MonsterPopView = new DataView(MonsterPopTable);
            MonsterPopView.RowFilter = "monster_id=" + baseRow.monster_id;

            this.listBoxPop.Items.Clear();
            foreach (DataRowView PopRow in MonsterPopView)
            {
                this.listBoxPop.Items.Add(PopRow["quest_name"].ToString() + "/" + PopRow["mark_name"].ToString());
            }
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

            if (this.dataGridViewList[0, 0].Value != null)
            {
                this.dataGridViewList.Rows[0].Selected = true;
                PrivateView((int)this.dataGridViewList[0, 0].Value);
            }
        }

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="MonsterID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int MonsterID, int rowIndex)
        {
            MonsterDataEntity entity = LibMonsterData.Entity;

            MonsterDataEntity.mt_monster_listRow monsterRow = entity.mt_monster_list.FindBymonster_id(MonsterID);

            if (monsterRow == null) { return; }

            monsterRow.Delete();

            entity.mt_monster_action.DefaultView.RowFilter = entity.mt_monster_action.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView actionRow in entity.mt_monster_action.DefaultView)
            {
                actionRow.Delete();
            }

            entity.mt_monster_battle_ability.DefaultView.RowFilter = entity.mt_monster_battle_ability.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView abilityRow in entity.mt_monster_battle_ability.DefaultView)
            {
                abilityRow.Delete();
            }

            entity.mt_monster_element.DefaultView.RowFilter = entity.mt_monster_element.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView elementRow in entity.mt_monster_element.DefaultView)
            {
                elementRow.Delete();
            }

            entity.mt_monster_have_item.DefaultView.RowFilter = entity.mt_monster_have_item.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView itemRow in entity.mt_monster_have_item.DefaultView)
            {
                itemRow.Delete();
            }

            entity.mt_monster_pop_weather.DefaultView.RowFilter = entity.mt_monster_pop_weather.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView popWeatherRow in entity.mt_monster_pop_weather.DefaultView)
            {
                popWeatherRow.Delete();
            }

            entity.mt_monster_serif.DefaultView.RowFilter = entity.mt_monster_action.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView serifRow in entity.mt_monster_serif.DefaultView)
            {
                serifRow.Delete();
            }
        }

        /// <summary>
        /// データ複製
        /// </summary>
        /// <param name="MonsterID">対象ID</param>
        private void CopyData(int MonsterID)
        {
            MonsterDataEntity entity = LibMonsterData.Entity;

            MonsterDataEntity.mt_monster_listRow monsterRow = entity.mt_monster_list.FindBymonster_id(MonsterID);

            if (monsterRow == null) { return; }

            // 新規ID発行
            int newID = LibInteger.GetNewUnderNum(entity.mt_monster_list, entity.mt_monster_list.monster_idColumn.ColumnName);

            // 複製
            MonsterDataEntity.mt_monster_listRow newMonsterRow = entity.mt_monster_list.Newmt_monster_listRow();
            newMonsterRow.ItemArray = monsterRow.ItemArray;
            newMonsterRow.monster_id = newID;

            entity.mt_monster_list.Addmt_monster_listRow(newMonsterRow);

            entity.mt_monster_have_item.DefaultView.RowFilter = entity.mt_monster_have_item.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView itemRow in entity.mt_monster_have_item.DefaultView)
            {
                MonsterDataEntity.mt_monster_have_itemRow newItemRow = entity.mt_monster_have_item.Newmt_monster_have_itemRow();
                newItemRow.ItemArray = itemRow.Row.ItemArray;
                newItemRow.monster_id = newID;

                entity.mt_monster_have_item.Addmt_monster_have_itemRow(newItemRow);
            }

            entity.mt_monster_battle_ability.DefaultView.RowFilter = entity.mt_monster_battle_ability.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView abilityRow in entity.mt_monster_battle_ability.DefaultView)
            {
                MonsterDataEntity.mt_monster_battle_abilityRow newAbilityRow = entity.mt_monster_battle_ability.Newmt_monster_battle_abilityRow();
                newAbilityRow.ItemArray = abilityRow.Row.ItemArray;
                newAbilityRow.monster_id = newID;

                entity.mt_monster_battle_ability.Addmt_monster_battle_abilityRow(newAbilityRow);
            }

            entity.mt_monster_element.DefaultView.RowFilter = entity.mt_monster_element.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView elementRow in entity.mt_monster_element.DefaultView)
            {
                MonsterDataEntity.mt_monster_elementRow newElementRow = entity.mt_monster_element.Newmt_monster_elementRow();
                newElementRow.ItemArray = elementRow.Row.ItemArray;
                newElementRow.monster_id = newID;

                entity.mt_monster_element.Addmt_monster_elementRow(newElementRow);
            }

            entity.mt_monster_action.DefaultView.RowFilter = entity.mt_monster_action.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView actionRow in entity.mt_monster_action.DefaultView)
            {
                MonsterDataEntity.mt_monster_actionRow newActionRow = entity.mt_monster_action.Newmt_monster_actionRow();
                newActionRow.ItemArray = actionRow.Row.ItemArray;
                newActionRow.monster_id = newID;

                entity.mt_monster_action.Addmt_monster_actionRow(newActionRow);
            }

            entity.mt_monster_serif.DefaultView.RowFilter = entity.mt_monster_serif.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView serifRow in entity.mt_monster_serif.DefaultView)
            {
                MonsterDataEntity.mt_monster_serifRow newSerifRow = entity.mt_monster_serif.Newmt_monster_serifRow();
                newSerifRow.ItemArray = serifRow.Row.ItemArray;
                newSerifRow.monster_id = newID;

                entity.mt_monster_serif.Addmt_monster_serifRow(newSerifRow);
            }

            entity.mt_monster_pop_weather.DefaultView.RowFilter = entity.mt_monster_pop_weather.monster_idColumn.ColumnName + "=" + MonsterID;
            foreach (DataRowView popWeatherRow in entity.mt_monster_pop_weather.DefaultView)
            {
                MonsterDataEntity.mt_monster_pop_weatherRow newPopWeatherRow = entity.mt_monster_pop_weather.Newmt_monster_pop_weatherRow();
                newPopWeatherRow.ItemArray = popWeatherRow.Row.ItemArray;
                newPopWeatherRow.monster_id = newID;

                entity.mt_monster_pop_weather.Addmt_monster_pop_weatherRow(newPopWeatherRow);
            }
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            MonsterDataEntity entity = LibMonsterData.Entity;

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

                dba.Update(LibMonsterData.Entity.mt_monster_list);

                dba.Update(LibMonsterData.Entity.mt_monster_have_item);
                dba.Update(LibMonsterData.Entity.mt_monster_battle_ability);
                dba.Update(LibMonsterData.Entity.mt_monster_element);
                dba.Update(LibMonsterData.Entity.mt_monster_action);
                dba.Update(LibMonsterData.Entity.mt_monster_serif);
                dba.Update(LibMonsterData.Entity.mt_monster_pop_weather);
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

            LibMonsterData.LoadMonster();
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            MonsterDataEntity entity = LibMonsterData.Entity;

            if (this.textBoxName.Text.Length == 0) { return; }

            MonsterDataEntity.mt_monster_listRow row = entity.mt_monster_list.FindBymonster_id(int.Parse(this.textBoxNo.Text));

            bool isNew = false;

            if (row == null)
            {
                row = entity.mt_monster_list.Newmt_monster_listRow();

                row.monster_id = int.Parse(this.textBoxNo.Text);

                isNew = true;
            }

            if (isNew || row.monster_name != this.textBoxName.Text) { row.monster_name = this.textBoxName.Text; }

            if (isNew || row.category_id != (int)this.comboBoxCategory.SelectedValue) { row.category_id = (int)this.comboBoxCategory.SelectedValue; }

            if (isNew || row.max_multi_act != (int)this.numericUpDownMaxMultiAction.Value) { row.max_multi_act = (int)this.numericUpDownMaxMultiAction.Value; }
            if (isNew || row.multi_act_prob != (int)this.numericUpDownMultiActionProb.Value) { row.multi_act_prob = (int)this.numericUpDownMultiActionProb.Value; }

            if (isNew || row.target != (int)this.comboBoxTarget.SelectedValue) { row.target = (int)this.comboBoxTarget.SelectedValue; }
            if (isNew || row.formation != this.comboBoxFormation.SelectedIndex) { row.formation = this.comboBoxFormation.SelectedIndex; }
            if (isNew || row.belong_kb != this.comboBoxMonsterKb.SelectedIndex) { row.belong_kb = this.comboBoxMonsterKb.SelectedIndex; }

            if (isNew)
            {
                entity.mt_monster_list.Addmt_monster_listRow(row);
            }

            List<int> WeatherListArray = new List<int>();

            for (int i = 0; i < this.checkedListBoxPopWeather.Items.Count; i++)
            {
                if (this.checkedListBoxPopWeather.GetItemChecked(i))
                {
                    string[] Weathers = this.checkedListBoxPopWeather.Items[i].ToString().Split(':');
                    WeatherListArray.Add(int.Parse(Weathers[0]));
                }
            }

            MonsterDataEntity.mt_monster_pop_weatherRow[] weatherRows = (MonsterDataEntity.mt_monster_pop_weatherRow[])LibMonsterData.Entity.mt_monster_pop_weather.Select(LibMonsterData.Entity.mt_monster_pop_weather.monster_idColumn.ColumnName + "=" + row.monster_id);

            foreach (MonsterDataEntity.mt_monster_pop_weatherRow weatherRow in weatherRows)
            {
                weatherRow.Delete();
            }

            for (int j = 0; j < WeatherListArray.Count; j++)
            {
                MonsterDataEntity.mt_monster_pop_weatherRow weatherNewRow = LibMonsterData.Entity.mt_monster_pop_weather.Newmt_monster_pop_weatherRow();
                weatherNewRow.monster_id = row.monster_id;
                weatherNewRow.weather_id = WeatherListArray[j];
                LibMonsterData.Entity.mt_monster_pop_weather.Addmt_monster_pop_weatherRow(weatherNewRow);
            }
        }

        /// <summary>
        /// フィルタ実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void toolStripMenuItemFilter_Click(object sender, EventArgs e)
        {
            MonsterDataFilterDialog dialog = new MonsterDataFilterDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _monsterView.RowFilter = dialog.FilterString;
            }
        }

        /// <summary>
        /// 戦闘行動設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCommonAction_Click(object sender, EventArgs e)
        {
            MonsterDataEntity entity = LibMonsterData.Entity;
            MonsterActionDialog dialog = new MonsterActionDialog(SelectedNo, entity.mt_monster_action);
            dialog.Show(this);
        }

        /// <summary>
        /// 属性設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCommonElement_Click(object sender, EventArgs e)
        {
            MonsterDataEntity entity = LibMonsterData.Entity;
            MonsterElementDialog dialog = new MonsterElementDialog(SelectedNo, entity.mt_monster_element);
            dialog.Show(this);
        }

        /// <summary>
        /// 所持アイテム設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCommonItemBox_Click(object sender, EventArgs e)
        {
            MonsterDataEntity entity = LibMonsterData.Entity;
            MonsterItemDialog dialog = new MonsterItemDialog(SelectedNo, entity.mt_monster_have_item);
            dialog.Show(this);
        }

        /// <summary>
        /// セリフ設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSerif_Click(object sender, EventArgs e)
        {
            MonsterDataEntity entity = LibMonsterData.Entity;
            MonsterSerifDialog dialog = new MonsterSerifDialog(SelectedNo, entity.mt_monster_serif);
            dialog.Show(this);
        }

        /// <summary>
        /// 規定値の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewBattleAbility_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if (this.dataGridViewBattleAbility.RowCount >= 2)
            {
                int NewRowLevel = (int)this.dataGridViewBattleAbility.Rows[this.dataGridViewBattleAbility.RowCount - 2].Cells["columnLevel"].Value;

                foreach (DataGridViewCell inCell in e.Row.Cells)
                {
                    if (inCell.ColumnIndex == 1)
                    {
                        //inCell.Value = NewRowLevel + 1;
                    }
                    else
                    {
                        inCell.Value = this.dataGridViewBattleAbility.Rows[this.dataGridViewBattleAbility.RowCount - 2].Cells[inCell.ColumnIndex].Value;
                    }
                }
            }

            e.Row.Cells["columnMonsterID"].Value = int.Parse(this.textBoxNo.Text);
        }

        /// <summary>
        /// キーエンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewBattleAbility_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewBattleAbility.SelectedRows.Count == 0 && this.dataGridViewBattleAbility.SelectedCells.Count > 0)
            {
                this.dataGridViewBattleAbility.BeginEdit(true);
            }
        }

        /// <summary>
        /// セルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewBattleAbility_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1)
            {
                this.dataGridViewBattleAbility.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                this.dataGridViewBattleAbility.EndEdit();
            }
            else if (this.dataGridViewBattleAbility.EditMode != DataGridViewEditMode.EditOnEnter)
            {
                this.dataGridViewBattleAbility.EditMode = DataGridViewEditMode.EditOnEnter;
                this.dataGridViewBattleAbility.BeginEdit(false);
            }
        }
    }
}
