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
using CommonFormLibrary.CommonDialog;

namespace DataEditForm.StatusData
{
    public partial class StatusEditPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StatusEditPanel()
        {
            InitializeComponent();

            NothingFilter();
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        public override void Cancel()
        {
            StatusListEntity entity = LibStatusList.Entity;
            entity.RejectChanges();
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
                dba.Update(LibStatusList.Entity.mt_status_list);
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

            LibStatusList.LoadStatusList();
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
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            StatusListEntity entity = LibStatusList.Entity;

            return entity.GetChanges() != null;
        }

        private DataView StatusView;

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            StatusListEntity entity = LibStatusList.Entity;
            StatusView = new DataView(entity.mt_status_list);
            StatusView.Sort = entity.mt_status_list.status_idColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = StatusView;
            this.columnNo.DataPropertyName = entity.mt_status_list.status_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.mt_status_list.status_nameColumn.ColumnName;
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
        /// <param name="StatusID">表示対象ID</param>
        private void PrivateView(int StatusID)
        {
            StatusListEntity entity = LibStatusList.Entity;

            if (StatusID == 0)
            {
                // 新規ID発行
                SetNewNumberDialog dialog = new SetNewNumberDialog();
                dialog.SetNewNumber(LibInteger.GetNewUnderNum(entity.mt_status_list, entity.mt_status_list.status_idColumn.ColumnName));
                dialog.ValidatingNumber += new EventHandler(Validate_Number);

                switch (dialog.ShowDialog(this))
                {
                    case DialogResult.OK:
                        StatusID = dialog.NewID;
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            // 表示

            StatusListEntity.status_listRow baseRow = entity.mt_status_list.FindBystatus_id(StatusID);

            this.textBoxNo.Text = StatusID.ToString();

            if (baseRow == null)
            {
                // 新規に行追加

                entity.mt_status_list.Addstatus_listRow(StatusID, "", "", false, false, "", false);
            }

            this.textBoxName.Text = baseRow.status_name;

            this.textBoxStatusComment.Text = baseRow.status_text;
            this.checkBoxDispel.Checked = baseRow.status_dispel;
            this.checkBoxClearlance.Checked = baseRow.status_clearnce;
            this.textBoxIcon.Text = baseRow.status_icon;
            this.checkBoxGood.Checked = baseRow.status_good;
        }

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="StatusID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int StatusID, int rowIndex)
        {
            StatusListEntity entity = LibStatusList.Entity;

            StatusListEntity.status_listRow statusRow = entity.mt_status_list.FindBystatus_id(StatusID);

            if (statusRow == null) { return; }

            statusRow.Delete();
        }

        /// <summary>
        /// データ複製
        /// </summary>
        /// <param name="StatusID">対象ID</param>
        private void CopyData(int StatusID)
        {
            StatusListEntity entity = LibStatusList.Entity;

            StatusListEntity.status_listRow statusRow = entity.mt_status_list.FindBystatus_id(StatusID);

            if (statusRow == null) { return; }

            int newID = LibInteger.GetNewUnderNum(entity.mt_status_list, entity.mt_status_list.status_idColumn.ColumnName);

            // 上位から複製
            StatusListEntity.status_listRow newStatusRow = entity.mt_status_list.Newstatus_listRow();
            newStatusRow.ItemArray = statusRow.ItemArray;
            newStatusRow.status_id = newID;

            entity.mt_status_list.Addstatus_listRow(newStatusRow);
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            StatusListEntity entity = LibStatusList.Entity;

            if (this.textBoxName.Text.Length == 0) { return; }

            StatusListEntity.status_listRow row = entity.mt_status_list.FindBystatus_id(int.Parse(this.textBoxNo.Text));
            bool isAdd = false;

            if (row == null)
            {
                // 新規追加
                row = entity.mt_status_list.Newstatus_listRow();
                isAdd = true;
                row.status_id = int.Parse(this.textBoxNo.Text);
            }

            if (isAdd || row.status_name != this.textBoxName.Text) { row.status_name = this.textBoxName.Text; }

            if (isAdd || row.status_text != this.textBoxStatusComment.Text) { row.status_text = this.textBoxStatusComment.Text; }

            if (isAdd || row.status_dispel != this.checkBoxDispel.Checked) { row.status_dispel = this.checkBoxDispel.Checked; }
            if (isAdd || row.status_clearnce != this.checkBoxClearlance.Checked) { row.status_clearnce = this.checkBoxClearlance.Checked; }

            if (isAdd || row.status_icon != this.textBoxIcon.Text) { row.status_icon = this.textBoxIcon.Text; }

            if (isAdd || row.status_good != this.checkBoxGood.Checked) { row.status_good = this.checkBoxGood.Checked; }

            if (isAdd)
            {
                entity.mt_status_list.Addstatus_listRow(row);
            }
        }

        /// <summary>
        /// 番号重複管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Validate_Number(object sender, EventArgs e)
        {
            StatusListEntity entity = LibStatusList.Entity;
            SetNewNumberDialog dialog = (SetNewNumberDialog)sender;
            StatusListEntity.status_listRow statusRow = entity.mt_status_list.FindBystatus_id(dialog.NewID);

            if (statusRow != null)
            {
                dialog.labelCaution.Visible = true;
            }
            else
            {
                dialog.labelCaution.Visible = false;
            }
        }
    }
}
