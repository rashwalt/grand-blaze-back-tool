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

namespace DataEditForm.KeyItem
{
    public partial class KeyItemPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KeyItemPanel()
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
            KeyItemEntity entity = LibKeyItem.Entity;
            entity.RejectChanges();
        }

        private DataView KeyView;

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            KeyItemEntity entity = LibKeyItem.Entity;

            KeyView = new DataView(entity.mt_key_item_list);
            KeyView.Sort = entity.mt_key_item_list.key_idColumn.ColumnName;
            
            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = KeyView;
            this.columnNo.DataPropertyName = entity.mt_key_item_list.key_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.mt_key_item_list.keyitem_nameColumn.ColumnName;
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
        /// <param name="KeyItemID">表示対象ID</param>
        private void PrivateView(int KeyItemID)
        {
            KeyItemEntity entity = LibKeyItem.Entity;

            if (KeyItemID == 0)
            {
                KeyItemID = LibInteger.GetNewUnderNum(entity.mt_key_item_list, entity.mt_key_item_list.key_idColumn.ColumnName);
            }

            // 表示

            KeyItemEntity.mt_key_item_listRow baseRow = entity.mt_key_item_list.FindBykey_id(KeyItemID);

            this.textBoxNo.Text = KeyItemID.ToString();

            if (baseRow == null)
            {
                return;
            }

            this.textBoxName.Text = baseRow.keyitem_name;
            this.textBoxComment.Text = baseRow.keyitem_comment.Replace("<br />", "\r\n");
            this.comboBoxKeyType.SelectedIndex = baseRow.key_type;
        }

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="KeyItemID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int KeyItemID, int rowIndex)
        {
            KeyItemEntity entity = LibKeyItem.Entity;

            KeyItemEntity.mt_key_item_listRow keyRow = entity.mt_key_item_list.FindBykey_id(KeyItemID);

            if (keyRow == null) { return; }

            keyRow.Delete();
        }

        /// <summary>
        /// データ複製
        /// </summary>
        /// <param name="KeyItemID">対象ID</param>
        private void CopyData(int KeyItemID)
        {
            KeyItemEntity entity = LibKeyItem.Entity;

            KeyItemEntity.mt_key_item_listRow keyRow = entity.mt_key_item_list.FindBykey_id(KeyItemID);

            if (keyRow == null) { return; }

            int newID = LibInteger.GetNewUnderNum(entity.mt_key_item_list, entity.mt_key_item_list.key_idColumn.ColumnName);

            // 上位から複製
            KeyItemEntity.mt_key_item_listRow newKeyRow = entity.mt_key_item_list.Newmt_key_item_listRow();
            newKeyRow.ItemArray = keyRow.ItemArray;
            newKeyRow.key_id = newID;

            entity.mt_key_item_list.Addmt_key_item_listRow(newKeyRow);
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            KeyItemEntity entity = LibKeyItem.Entity;

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
                dba.Update(LibKeyItem.Entity.mt_key_item_list);
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

            LibKeyItem.LoadKeyItem();
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            KeyItemEntity entity = LibKeyItem.Entity;

            if (this.textBoxName.Text.Length == 0) { return; }

            KeyItemEntity.mt_key_item_listRow row = entity.mt_key_item_list.FindBykey_id(int.Parse(this.textBoxNo.Text));
            bool isAdd = false;

            if (row == null)
            {
                // 新規追加
                row = entity.mt_key_item_list.Newmt_key_item_listRow();
                isAdd = true;
                row.key_id = int.Parse(this.textBoxNo.Text);
            }

            if (isAdd || row.keyitem_name != this.textBoxName.Text) { row.keyitem_name = this.textBoxName.Text; }

            string comment = this.textBoxComment.Text.Replace("\r\n", "<br />");
            if (isAdd || row.keyitem_comment != comment) { row.keyitem_comment = comment; }
            if (isAdd || row.key_type != this.comboBoxKeyType.SelectedIndex) { row.key_type = this.comboBoxKeyType.SelectedIndex; }

            if (isAdd)
            {
                entity.mt_key_item_list.Addmt_key_item_listRow(row);
            }
        }
    }
}
