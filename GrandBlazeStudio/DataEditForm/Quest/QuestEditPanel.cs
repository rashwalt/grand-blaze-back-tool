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

namespace DataEditForm.Quest
{
    public partial class QuestEditPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public QuestEditPanel()
        {
            InitializeComponent();

            this.comboBoxType.DataSource = LibQuest.Entity.mt_quest_type;
            this.comboBoxType.ValueMember = LibQuest.Entity.mt_quest_type.quest_typeColumn.ColumnName;
            this.comboBoxType.DisplayMember = LibQuest.Entity.mt_quest_type.quest_type_nameColumn.ColumnName;

            KeyItemEntity.mt_key_item_listDataTable KeyItem = (KeyItemEntity.mt_key_item_listDataTable)LibKeyItem.Entity.mt_key_item_list.Copy();
            KeyItem.Addmt_key_item_listRow(0, "未設定", "", 0);
            KeyItem.DefaultView.Sort = LibKeyItem.Entity.mt_key_item_list.key_idColumn.ColumnName;
            this.comboBoxKeyItem.DataSource = KeyItem.DefaultView;
            this.comboBoxKeyItem.ValueMember = LibKeyItem.Entity.mt_key_item_list.key_idColumn.ColumnName;
            this.comboBoxKeyItem.DisplayMember = LibKeyItem.Entity.mt_key_item_list.keyitem_nameColumn.ColumnName;

            QuestDataEntity.quest_listDataTable questData = (QuestDataEntity.quest_listDataTable)LibQuest.Entity.mt_quest_list.Copy();
            questData.Addquest_listRow(0, "未設定", "", 0, 0, false, true, 0, 0, 0, 0, 0, 0, 0);
            questData.DefaultView.Sort = LibQuest.Entity.mt_quest_list.quest_idColumn.ColumnName;
            this.comboBoxOfferQuest.DataSource = questData.DefaultView;
            this.comboBoxOfferQuest.ValueMember = LibQuest.Entity.mt_quest_list.quest_idColumn.ColumnName;
            this.comboBoxOfferQuest.DisplayMember = LibQuest.Entity.mt_quest_list.quest_nameColumn.ColumnName;

            QuestDataEntity.quest_listDataTable questData2 = (QuestDataEntity.quest_listDataTable)LibQuest.Entity.mt_quest_list.Copy();
            questData2.Addquest_listRow(0, "未設定", "", 0, 0, false, true, 0, 0, 0, 0, 0, 0, 0);
            questData2.DefaultView.Sort = LibQuest.Entity.mt_quest_list.quest_idColumn.ColumnName;
            this.comboBoxCompQuest.DataSource = questData2.DefaultView;
            this.comboBoxCompQuest.ValueMember = LibQuest.Entity.mt_quest_list.quest_idColumn.ColumnName;
            this.comboBoxCompQuest.DisplayMember = LibQuest.Entity.mt_quest_list.quest_nameColumn.ColumnName;

            InstallDataEntity.mt_install_class_listDataTable InstallClass = (InstallDataEntity.mt_install_class_listDataTable)LibInstall.Entity.mt_install_class_list.Copy();
            InstallClass.Addmt_install_class_listRow(0, "未設定", 0, 0, 0, 0, 0, 0, "");
            InstallClass.DefaultView.Sort = LibInstall.Entity.mt_install_class_list.install_idColumn.ColumnName;
            this.comboBoxClass.DataSource = InstallClass.DefaultView;
            this.comboBoxClass.ValueMember = LibInstall.Entity.mt_install_class_list.install_idColumn.ColumnName;
            this.comboBoxClass.DisplayMember = LibInstall.Entity.mt_install_class_list.classnameColumn.ColumnName;

            NothingFilter();
        }

        private DataView _questView;

        /// <summary>
        /// マーク表示の最適化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxMark_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.Value != null)
            {
                QuestDataEntity.mt_mark_listRow row = (QuestDataEntity.mt_mark_listRow)((DataRowView)e.ListItem).Row;
                e.Value = LibQuest.GetQuestMarkName(row.mark_id);
            }
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
            QuestDataEntity entity = LibQuest.Entity;
            entity.RejectChanges();
        }

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            QuestDataEntity entity = LibQuest.Entity;
            _questView = new DataView(entity.mt_quest_list);
            _questView.RowFilter = "";
            _questView.Sort = entity.mt_quest_list.quest_idColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = _questView;
            this.columnNo.DataPropertyName = entity.mt_quest_list.quest_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.mt_quest_list.quest_nameColumn.ColumnName;
        }

        /// <summary>
        /// 詳細表示
        /// </summary>
        /// <param name="QuestID">表示対象ID</param>
        private void PrivateView(int QuestID)
        {
            if (QuestID < -1) { return; }

            QuestDataEntity entity = LibQuest.Entity;

            if (QuestID == 0)
            {
                int TempQuestID = LibInteger.GetNewUnderNum(entity.mt_quest_list, entity.mt_quest_list.quest_idColumn.ColumnName);

                SetNewNumberDialog dialog = new SetNewNumberDialog();
                dialog.SetNewNumber(TempQuestID);
                dialog.ValidatingNumber += new EventHandler(Validate_Number);

                switch (dialog.ShowDialog(this))
                {
                    case DialogResult.OK:
                        QuestID = dialog.NewID;
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            // 表示
            QuestDataEntity.quest_listRow baseRow = entity.mt_quest_list.FindByquest_id(QuestID);

            this.textBoxNo.Text = QuestID.ToString();

            if (baseRow == null)
            {
                // 新規に行追加
                baseRow = entity.mt_quest_list.Addquest_listRow(QuestID, "", "", 0, 1, false, true, 0, 0, 0, 0, 0, 0, 0);
            }

            this.textBoxName.Text = baseRow.quest_name;

            this.textBoxClientName.Text = baseRow.quest_client;
            this.comboBoxType.SelectedValue = baseRow.quest_type;
            this.numericUpDownPickLevel.Value = baseRow.pick_level;

            DataView stepView = new DataView(entity.mt_quest_step);
            stepView.RowFilter = entity.mt_quest_step.quest_idColumn.ColumnName + "=" + QuestID;
            stepView.Sort = entity.mt_quest_step.quest_stepColumn.ColumnName;

            this.comboBoxKeyItem.SelectedValue = baseRow.key_item_id;
            this.comboBoxOfferQuest.SelectedValue = baseRow.offer_quest_id;
            this.comboBoxCompQuest.SelectedValue = baseRow.comp_quest_id;
            this.comboBoxClass.SelectedValue = baseRow.class_id;
            this.numericUpDownClassLevel.Value = baseRow.class_level;
            this.numericUpDownSP.Value = baseRow.sp_level;
            this.numericUpDownBC.Value = baseRow.bc_count;

            this.checkBoxHide.Checked = baseRow.hide_fg;
            this.checkBoxValid.Checked = baseRow.valid_fg;

            this.dataGridViewComment.AutoGenerateColumns = false;
            this.dataGridViewComment.DataSource = stepView;
            this.dataGridViewComment.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewComment.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.columnStep.DataPropertyName = LibQuest.Entity.mt_quest_step.quest_stepColumn.ColumnName;
            this.columnComment.DataPropertyName = LibQuest.Entity.mt_quest_step.commentColumn.ColumnName;

            DataView markView = new DataView(entity.mt_mark_list);
            markView.RowFilter = entity.mt_mark_list.quest_idColumn.ColumnName + "=" + QuestID;
            markView.Sort = entity.mt_mark_list.mark_idColumn.ColumnName;

            this.dataGridViewMarkList.AutoGenerateColumns = false;
            this.dataGridViewMarkList.DataSource = markView;
            this.columnMarkName.DataPropertyName = LibQuest.Entity.mt_mark_list.mark_nameColumn.ColumnName;
            this.columnType.DataPropertyName = LibQuest.Entity.mt_mark_list.field_typeColumn.ColumnName;
            this.columnHideMark.DataPropertyName = LibQuest.Entity.mt_mark_list.hide_markColumn.ColumnName;

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
        /// データ削除
        /// </summary>
        /// <param name="QuestID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int QuestID, int rowIndex)
        {
            QuestDataEntity entity = LibQuest.Entity;

            QuestDataEntity.quest_listRow questRow = entity.mt_quest_list.FindByquest_id(QuestID);

            if (questRow == null) { return; }

            questRow.Delete();
        }

        /// <summary>
        /// データ複製
        /// </summary>
        /// <param name="QuestID">対象ID</param>
        private void CopyData(int QuestID)
        {
            QuestDataEntity entity = LibQuest.Entity;

            QuestDataEntity.quest_listRow questRow = entity.mt_quest_list.FindByquest_id(QuestID);

            if (questRow == null) { return; }

            // 新規ID発行
            int newID = LibInteger.GetNewUnderNum(entity.mt_quest_list, entity.mt_quest_list.quest_idColumn.ColumnName);

            // 複製
            QuestDataEntity.quest_listRow newQuestRow = entity.mt_quest_list.Newquest_listRow();
            newQuestRow.ItemArray = questRow.ItemArray;
            newQuestRow.quest_id = newID;

            entity.mt_quest_list.Addquest_listRow(newQuestRow);
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            QuestDataEntity entity = LibQuest.Entity;

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
                dba.Update(LibQuest.Entity.mt_quest_list);
                dba.Update(LibQuest.Entity.mt_quest_step);

                dba.Update(LibQuest.Entity.mt_mark_list);
                dba.Update(LibQuest.Entity.mt_mark_pop_monster);
                dba.Update(LibQuest.Entity.mt_mark_weather);

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

            LibQuest.LoadQuestList();
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            QuestDataEntity entity = LibQuest.Entity;

            QuestDataEntity.quest_listRow row = entity.mt_quest_list.FindByquest_id(int.Parse(this.textBoxNo.Text));

            if (row == null) { return; }

            if (row.quest_name != this.textBoxName.Text) { row.quest_name = this.textBoxName.Text; }

            if (row.quest_client != this.textBoxClientName.Text) { row.quest_client = this.textBoxClientName.Text; }

            if (row.quest_type != (int)this.comboBoxType.SelectedValue) { row.quest_type = (int)this.comboBoxType.SelectedValue; }

            if (row.pick_level != (int)this.numericUpDownPickLevel.Value) { row.pick_level = (int)this.numericUpDownPickLevel.Value; }

            if (row.key_item_id != (int)this.comboBoxKeyItem.SelectedValue) { row.key_item_id = (int)this.comboBoxKeyItem.SelectedValue; }

            if (row.offer_quest_id != (int)this.comboBoxOfferQuest.SelectedValue) { row.offer_quest_id = (int)this.comboBoxOfferQuest.SelectedValue; }

            if (row.comp_quest_id != (int)this.comboBoxCompQuest.SelectedValue) { row.comp_quest_id = (int)this.comboBoxCompQuest.SelectedValue; }

            if (row.class_id != (int)this.comboBoxClass.SelectedValue) { row.class_id = (int)this.comboBoxClass.SelectedValue; }

            if (row.class_level != (int)this.numericUpDownClassLevel.Value) { row.class_level = (int)this.numericUpDownClassLevel.Value; }

            if (row.sp_level != (int)this.numericUpDownSP.Value) { row.sp_level = (int)this.numericUpDownSP.Value; }

            if (row.bc_count != (int)this.numericUpDownBC.Value) { row.bc_count = (int)this.numericUpDownBC.Value; }

            if (row.hide_fg != this.checkBoxHide.Checked) { row.hide_fg = this.checkBoxHide.Checked; }

            if (row.valid_fg != this.checkBoxValid.Checked) { row.valid_fg = this.checkBoxValid.Checked; }

        }

        /// <summary>
        /// コメントキーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewComment_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Insert:
                    toolStripMenuItemComAdd_Click(this, EventArgs.Empty);
                    break;
                case Keys.Delete:
                    toolStripMenuItemComDelete_Click(this, EventArgs.Empty);
                    break;
            }
        }

        /// <summary>
        /// コメントダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewComment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripMenuItemComEdit_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// コメント右クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewComment_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // ヘッダ以外のセルか？
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    // 右クリックされたセル
                    DataGridViewCell cell = this.dataGridViewComment[e.ColumnIndex, e.RowIndex];
                    // セルの選択
                    cell.Selected = true;
                }
            }
        }

        /// <summary>
        /// コメント・コンテキスト表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewComment_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // コンテキスト表示
                this.contextMenuStripComment.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
        }

        /// <summary>
        /// コメント表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewComment_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) { return; }

            switch (e.ColumnIndex)
            {
                case 1:
                    // 改行変換
                    e.Value = e.Value.ToString().Replace("<br />", "\r\n");
                    break;
            }
        }

        /// <summary>
        /// コメント編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemComEdit_Click(object sender, EventArgs e)
        {
            int QuestStep = ((QuestDataEntity.mt_quest_stepRow)((DataRowView)this.dataGridViewComment.SelectedRows[0].DataBoundItem).Row).quest_step;
            DialogViewComment(QuestStep);
        }

        /// <summary>
        /// コメント追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemComAdd_Click(object sender, EventArgs e)
        {
            DialogViewComment(-1);
        }

        /// <summary>
        /// コメント削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemComDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewComment.SelectedRows.Count == 0) { return; }

            int QuestStep = ((QuestDataEntity.mt_quest_stepRow)((DataRowView)this.dataGridViewComment.SelectedRows[0].DataBoundItem).Row).quest_step;

            QuestDataEntity entity = LibQuest.Entity;

            QuestDataEntity.mt_quest_stepRow stepRow = entity.mt_quest_step.FindByquest_idquest_step(int.Parse(this.textBoxNo.Text), QuestStep);

            if (stepRow == null) { return; }

            stepRow.Delete();
        }

        /// <summary>
        /// ダイアログ表示(コメント)
        /// </summary>
        /// <param name="AreaID">エリアID</param>
        private void DialogViewComment(int QuestStep)
        {
            QuestCommentDialog dialog = new QuestCommentDialog(QuestStep);
            dialog.QuestID = int.Parse(this.textBoxNo.Text);
            dialog.ShowDialog(this);
        }

        /// <summary>
        /// ショートカットキー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewMarkList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Insert:
                    toolStripMenuItemMarkAdd_Click(this, EventArgs.Empty);
                    break;
                case Keys.Delete:
                    toolStripMenuItemMarkDelete_Click(this, EventArgs.Empty);
                    break;
            }
        }

        /// <summary>
        /// マークダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewMarkList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripMenuItemMarkEdit_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// 右クリック選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewMarkList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // ヘッダ以外のセルか？
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    // 右クリックされたセル
                    DataGridViewCell cell = this.dataGridViewMarkList[e.ColumnIndex, e.RowIndex];
                    // セルの選択
                    cell.Selected = true;
                }
            }
        }

        /// <summary>
        /// コンテキストメニュー表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewMarkList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // コンテキスト表示
                this.contextMenuStripMark.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
        }

        /// <summary>
        /// 編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemMarkEdit_Click(object sender, EventArgs e)
        {
            int MarkID = ((QuestDataEntity.mt_mark_listRow)((DataRowView)this.dataGridViewMarkList.SelectedRows[0].DataBoundItem).Row).mark_id;
            DialogView(MarkID);
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemMarkAdd_Click(object sender, EventArgs e)
        {
            DialogView(0);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemMarkDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewMarkList.SelectedRows.Count == 0) { return; }

            int MarkID = ((QuestDataEntity.mt_mark_listRow)((DataRowView)(this.dataGridViewMarkList.SelectedRows[0].DataBoundItem)).Row).mark_id;

            QuestDataEntity entity = LibQuest.Entity;

            QuestDataEntity.mt_mark_listRow markRow = entity.mt_mark_list.FindBymark_id(MarkID);

            if (markRow == null) { return; }

            entity.mt_mark_pop_monster.DefaultView.RowFilter = entity.mt_mark_pop_monster.mark_idColumn.ColumnName + "=" + MarkID;
            foreach (DataRowView monsterRow in entity.mt_mark_pop_monster.DefaultView)
            {
                monsterRow.Delete();
            }

            markRow.Delete();
        }

        /// <summary>
        /// ダイアログ表示
        /// </summary>
        /// <param name="MarkID">マークID</param>
        private void DialogView(int MarkID)
        {
            bool IsNew = false;

            if (MarkID == 0)
            {
                MarkID = LibInteger.GetNewUnderNumEx(LibQuest.Entity.mt_mark_list, LibQuest.Entity.mt_mark_list.mark_idColumn.ColumnName);
                IsNew = true;
            }

            MarkEditDialog dialog = new MarkEditDialog(MarkID);
            if (IsNew)
            {
                dialog.DefaultAreaID = int.Parse(this.textBoxNo.Text);
            }
            dialog.ShowDialog(this);
        }

        /// <summary>
        /// 表示内容の整形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewMarkList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) { return; }

            switch (e.ColumnIndex)
            {
                case 2:
                    // タイプ
                    e.Value = LibField.GetName((int)e.Value);
                    break;
            }
        }

        /// <summary>
        /// 番号重複管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Validate_Number(object sender, EventArgs e)
        {
            QuestDataEntity entity = LibQuest.Entity;
            SetNewNumberDialog dialog = (SetNewNumberDialog)sender;
            QuestDataEntity.quest_listRow questRow = entity.mt_quest_list.FindByquest_id(dialog.NewID);

            if (questRow != null)
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
