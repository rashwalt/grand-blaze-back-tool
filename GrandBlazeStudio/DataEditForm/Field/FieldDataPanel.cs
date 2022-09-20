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

namespace DataEditForm.Field
{
    public partial class FieldDataPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FieldDataPanel()
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

        private DataView FieldView;

        /// <summary>
        /// キャンセル
        /// </summary>
        public override void Cancel()
        {
            FieldDataEntity entity = LibField.Entity;
            entity.RejectChanges();
        }

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            FieldDataEntity entity = LibField.Entity;

            FieldView = new DataView(entity.mt_field_type_list);
            FieldView.Sort = entity.mt_field_type_list.field_idColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = FieldView;
            this.columnNo.DataPropertyName = entity.mt_field_type_list.field_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.mt_field_type_list.field_nameColumn.ColumnName;
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
        /// <param name="FieldID">表示対象ID</param>
        private void PrivateView(int FieldID)
        {
            FieldDataEntity entity = LibField.Entity;

            if (FieldID == 0)
            {
                FieldID = LibInteger.GetNewUnderNum(entity.mt_field_type_list, entity.mt_field_type_list.field_idColumn.ColumnName);
            }

            // 表示

            FieldDataEntity.mt_field_type_listRow baseRow = entity.mt_field_type_list.FindByfield_id(FieldID);

            this.textBoxNo.Text = FieldID.ToString();

            if (baseRow == null)
            {
                // 新規に行追加
                entity.mt_field_type_list.Addmt_field_type_listRow(FieldID, "", false, false, false, false, false, false, 0, 0, 0, 0, 0, 0, 0, 0);
            }

            this.textBoxName.Text = baseRow.field_name;

            this.checkBoxNotAttack.Checked = baseRow.not_attack;
            this.checkBoxNotMagic.Checked = baseRow.not_magic;
            this.checkBoxMagnet.Checked = baseRow.magnet;

            this.checkBoxHPSrip.Checked = baseRow.auto_hpsrip;
            this.checkBoxMPSrip.Checked = baseRow.auto_mpsrip;
            this.checkBoxTPSrip.Checked = baseRow.auto_tpsrip;

            this.comboBoxElementFire.SelectedIndex = baseRow.fire + 1;
            this.comboBoxElementFreeze.SelectedIndex = baseRow.freeze + 1;
            this.comboBoxElementAir.SelectedIndex = baseRow.air + 1;
            this.comboBoxElementEarth.SelectedIndex = baseRow.earth + 1;
            this.comboBoxElementWater.SelectedIndex = baseRow.water + 1;
            this.comboBoxElementThunder.SelectedIndex = baseRow.thunder + 1;
            this.comboBoxElementHoly.SelectedIndex = baseRow.holy + 1;
            this.comboBoxElementDark.SelectedIndex = baseRow.dark + 1;

        }

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="FieldID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int FieldID, int rowIndex)
        {
            FieldDataEntity entity = LibField.Entity;

            FieldDataEntity.mt_field_type_listRow tarentRow = entity.mt_field_type_list.FindByfield_id(FieldID);

            if (tarentRow == null) { return; }

            tarentRow.Delete();
        }

        /// <summary>
        /// データ複製
        /// </summary>
        /// <param name="FieldID">対象ID</param>
        private void CopyData(int FieldID)
        {
            FieldDataEntity entity = LibField.Entity;

            FieldDataEntity.mt_field_type_listRow tarentRow = entity.mt_field_type_list.FindByfield_id(FieldID);

            if (tarentRow == null) { return; }

            int newID = LibInteger.GetNewUnderNum(entity.mt_field_type_list, entity.mt_field_type_list.field_idColumn.ColumnName);

            // 上位から複製
            FieldDataEntity.mt_field_type_listRow newFieldRow = entity.mt_field_type_list.Newmt_field_type_listRow();
            newFieldRow.ItemArray = tarentRow.ItemArray;
            newFieldRow.field_id = newID;

            entity.mt_field_type_list.Addmt_field_type_listRow(newFieldRow);
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            FieldDataEntity entity = LibField.Entity;

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
                dba.Update(LibField.Entity.mt_field_type_list);
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

            LibField.DataLoad();
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            FieldDataEntity entity = LibField.Entity;

            if (this.textBoxName.Text.Length == 0) { return; }

            FieldDataEntity.mt_field_type_listRow row = entity.mt_field_type_list.FindByfield_id(int.Parse(this.textBoxNo.Text));
            bool isAdd = false;

            if (row == null)
            {
                // 新規追加
                row = entity.mt_field_type_list.Newmt_field_type_listRow();
                isAdd = true;
                row.field_id = int.Parse(this.textBoxNo.Text);
            }

            if (isAdd || row.field_name != this.textBoxName.Text) { row.field_name = this.textBoxName.Text; }

            if (isAdd || row.not_attack != this.checkBoxNotAttack.Checked) { row.not_attack = this.checkBoxNotAttack.Checked; }
            if (isAdd || row.not_magic != this.checkBoxNotMagic.Checked) { row.not_magic = this.checkBoxNotMagic.Checked; }
            if (isAdd || row.magnet != this.checkBoxMagnet.Checked) { row.magnet = this.checkBoxMagnet.Checked; }

            if (isAdd || row.auto_hpsrip != this.checkBoxHPSrip.Checked) { row.auto_hpsrip = this.checkBoxHPSrip.Checked; }
            if (isAdd || row.auto_mpsrip != this.checkBoxMPSrip.Checked) { row.auto_mpsrip = this.checkBoxMPSrip.Checked; }
            if (isAdd || row.auto_tpsrip != this.checkBoxTPSrip.Checked) { row.auto_tpsrip = this.checkBoxTPSrip.Checked; }

            if (isAdd || (row.fire + 1) != this.comboBoxElementFire.SelectedIndex) { row.fire = this.comboBoxElementFire.SelectedIndex - 1; }
            if (isAdd || (row.freeze + 1) != this.comboBoxElementFreeze.SelectedIndex) { row.freeze = this.comboBoxElementFreeze.SelectedIndex - 1; }
            if (isAdd || (row.air + 1) != this.comboBoxElementAir.SelectedIndex) { row.air = this.comboBoxElementAir.SelectedIndex - 1; }
            if (isAdd || (row.earth + 1) != this.comboBoxElementEarth.SelectedIndex) { row.earth = this.comboBoxElementEarth.SelectedIndex - 1; }
            if (isAdd || (row.water + 1) != this.comboBoxElementWater.SelectedIndex) { row.water = this.comboBoxElementWater.SelectedIndex - 1; }
            if (isAdd || (row.thunder + 1) != this.comboBoxElementThunder.SelectedIndex) { row.thunder = this.comboBoxElementThunder.SelectedIndex - 1; }
            if (isAdd || (row.holy + 1) != this.comboBoxElementHoly.SelectedIndex) { row.holy = this.comboBoxElementHoly.SelectedIndex - 1; }
            if (isAdd || (row.dark + 1) != this.comboBoxElementDark.SelectedIndex) { row.dark = this.comboBoxElementDark.SelectedIndex - 1; }

            if (isAdd)
            {
                entity.mt_field_type_list.Addmt_field_type_listRow(row);
            }
        }
    }
}
