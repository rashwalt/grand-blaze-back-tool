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

namespace DataEditForm.InstallClass
{
    public partial class InstallClassPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InstallClassPanel()
        {
            InitializeComponent();

            NothingFilter();
        }

        private DataView _perksView;

        /// <summary>
        /// キャンセル
        /// </summary>
        public override void Cancel()
        {
            InstallDataEntity entity = LibInstall.Entity;
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
                dba.Update(LibInstall.Entity.mt_install_class_list);
                dba.Update(LibInstall.Entity.mt_install_class_skill);
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

            LibInstall.LoadInstall();
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
            InstallDataEntity entity = LibInstall.Entity;

            return entity.GetChanges() != null;
        }

        private DataView InstallClassView;

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            InstallDataEntity entity = LibInstall.Entity;

            InstallClassView = new DataView(entity.mt_install_class_list);
            InstallClassView.Sort = entity.mt_install_class_list.install_idColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = InstallClassView;
            this.columnNo.DataPropertyName = entity.mt_install_class_list.install_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.mt_install_class_list.classnameColumn.ColumnName;
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
        /// <param name="InstallID">表示対象ID</param>
        private void PrivateView(int InstallID)
        {
            InstallDataEntity entity = LibInstall.Entity;

            if (InstallID == 0)
            {
                InstallID = LibInteger.GetNewUnderNum(entity.mt_install_class_list, entity.mt_install_class_list.install_idColumn.ColumnName);
            }

            // 表示

            InstallDataEntity.mt_install_class_listRow baseRow = entity.mt_install_class_list.FindByinstall_id(InstallID);

            this.textBoxNo.Text = InstallID.ToString();

            // スキルリスト
            _perksView = new DataView(entity.mt_install_class_skill);
            _perksView.RowFilter = entity.mt_install_class_skill.install_idColumn.ColumnName + "=" + InstallID;
            _perksView.Sort = entity.mt_install_class_skill.install_levelColumn.ColumnName + "," + entity.mt_install_class_skill.perks_idColumn.ColumnName;

            this.dataGridViewSkill.AutoGenerateColumns = false;
            this.dataGridViewSkill.DataSource = _perksView;
            this.columnEffectName.DataPropertyName = entity.mt_install_class_skill.perks_idColumn.ColumnName;
            this.columnRank.DataPropertyName = entity.mt_install_class_skill.install_levelColumn.ColumnName;
            this.columnOnlyMode.DataPropertyName = entity.mt_install_class_skill.only_modeColumn.ColumnName;

            if (baseRow == null)
            {
                // 新規に行追加

                return;
            }

            this.textBoxName.Text = baseRow.classname;

            this.textBoxInstallClassComment.Text = baseRow.class_comment;
            this.comboBoxUpHP.SelectedIndex = baseRow.up_hp;
            this.comboBoxUpMP.SelectedIndex = baseRow.up_mp;
            this.comboBoxUpSTR.SelectedIndex = baseRow.up_str;
            this.comboBoxUpAGI.SelectedIndex = baseRow.up_agi;
            this.comboBoxUpMAG.SelectedIndex = baseRow.up_mag;
            this.comboBoxUpUNQ.SelectedIndex = baseRow.up_unq;

            ResertAllPoints();
        }

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="InstallID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int InstallID, int rowIndex)
        {
            InstallDataEntity entity = LibInstall.Entity;

            InstallDataEntity.mt_install_class_listRow classRow = entity.mt_install_class_list.FindByinstall_id(InstallID);

            if (classRow == null) { return; }

            classRow.Delete();

            entity.mt_install_class_list.DefaultView.RowFilter = entity.mt_install_class_list.install_idColumn.ColumnName + "=" + InstallID;
            foreach (DataRowView perksRow in entity.mt_install_class_list.DefaultView)
            {
                perksRow.Delete();
            }
        }

        /// <summary>
        /// データ複製
        /// </summary>
        /// <param name="InstallID">対象ID</param>
        private void CopyData(int InstallID)
        {
            InstallDataEntity entity = LibInstall.Entity;

            InstallDataEntity.mt_install_class_listRow classRow = entity.mt_install_class_list.FindByinstall_id(InstallID);

            if (classRow == null) { return; }

            int newID = LibInteger.GetNewUnderNum(entity.mt_install_class_list, entity.mt_install_class_list.install_idColumn.ColumnName);

            // 上位から複製
            InstallDataEntity.mt_install_class_listRow newClassRow = entity.mt_install_class_list.Newmt_install_class_listRow();
            newClassRow.ItemArray = classRow.ItemArray;
            newClassRow.install_id = newID;

            entity.mt_install_class_list.Addmt_install_class_listRow(newClassRow);

            entity.mt_install_class_list.DefaultView.RowFilter = entity.mt_install_class_list.install_idColumn.ColumnName + "=" + InstallID;
            foreach (DataRowView itemRow in entity.mt_install_class_list.DefaultView)
            {
                InstallDataEntity.mt_install_class_listRow newItemRow = entity.mt_install_class_list.Newmt_install_class_listRow();
                newItemRow.ItemArray = itemRow.Row.ItemArray;
                newItemRow.install_id = newID;

                entity.mt_install_class_list.Addmt_install_class_listRow(newItemRow);
            }
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            InstallDataEntity entity = LibInstall.Entity;

            if (this.textBoxName.Text.Length == 0) { return; }

            InstallDataEntity.mt_install_class_listRow row = entity.mt_install_class_list.FindByinstall_id(int.Parse(this.textBoxNo.Text));
            bool isAdd = false;

            if (row == null)
            {
                // 新規追加
                row = entity.mt_install_class_list.Newmt_install_class_listRow();
                isAdd = true;
                row.install_id = int.Parse(this.textBoxNo.Text);
            }

            if (isAdd || row.classname != this.textBoxName.Text) { row.classname = this.textBoxName.Text; }

            if (isAdd || row.class_comment != this.textBoxInstallClassComment.Text) { row.class_comment = this.textBoxInstallClassComment.Text; }

            if (isAdd || row.up_hp != this.comboBoxUpHP.SelectedIndex) { row.up_hp = this.comboBoxUpHP.SelectedIndex; }
            if (isAdd || row.up_mp != this.comboBoxUpMP.SelectedIndex) { row.up_mp = this.comboBoxUpMP.SelectedIndex; }

            if (isAdd || row.up_str != this.comboBoxUpSTR.SelectedIndex) { row.up_str = this.comboBoxUpSTR.SelectedIndex; }
            if (isAdd || row.up_agi != this.comboBoxUpAGI.SelectedIndex) { row.up_agi = this.comboBoxUpAGI.SelectedIndex; }
            if (isAdd || row.up_mag != this.comboBoxUpMAG.SelectedIndex) { row.up_mag = this.comboBoxUpMAG.SelectedIndex; }
            if (isAdd || row.up_unq != this.comboBoxUpUNQ.SelectedIndex) { row.up_unq = this.comboBoxUpUNQ.SelectedIndex; }

            if (isAdd)
            {
                entity.mt_install_class_list.Addmt_install_class_listRow(row);
            }
        }

        /// <summary>
        /// ST合計算出
        /// </summary>
        private void ResertAllPoints()
        {
            int DataAll = this.comboBoxUpHP.SelectedIndex + this.comboBoxUpMP.SelectedIndex +
                this.comboBoxUpSTR.SelectedIndex + this.comboBoxUpAGI.SelectedIndex +
                this.comboBoxUpMAG.SelectedIndex + this.comboBoxUpUNQ.SelectedIndex;
            this.textBoxAbMax.Text = DataAll.ToString();
        }

        /// <summary>
        /// 習得スキル：フォーマット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewSkill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    e.Value = LibSkill.GetSkillName((int)e.Value);
                    break;
                case 2:
                    switch ((int)e.Value)
                    {
                        case 0:
                            e.Value = "すべて";
                            break;
                        case 1:
                            e.Value = "プライマリ";
                            break;
                        case 2:
                            e.Value = "セカンダリ";
                            break;
                        case 3:
                            e.Value = "スクロール";
                            break;
                        case 4:
                            e.Value = "精霊契約";
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 習得スキル：マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewSkill_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.Button == MouseButtons.Right)
            {
                DataGridViewCell cell = this.dataGridViewSkill[e.ColumnIndex, e.RowIndex];
                // セルの選択状態を反転
                cell.Selected = true;
            }
        }

        /// <summary>
        /// 習得スキル：ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewSkill_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewSkill.SelectedCells.Count == 0)
            {
                DialogView(0);
            }
            else
            {
                DialogView((int)this.dataGridViewSkill.SelectedCells[1].Value);
            }
        }

        /// <summary>
        /// 習得スキル：マウスアップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewSkill_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // コンテキスト表示
                this.contextMenuStripEdit.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
        }

        /// <summary>
        /// 習得スキル：キーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewSkill_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    DeleteItemSkill();
                    break;
            }
        }

        /// <summary>
        /// 習得スキル：編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemItemEdit_Click(object sender, EventArgs e)
        {
            DialogView(((InstallDataEntity.mt_install_class_skillRow)((DataRowView)(this.dataGridViewSkill.SelectedRows[0].DataBoundItem)).Row).perks_id);
        }

        /// <summary>
        /// 習得スキル：追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAdd_Click_1(object sender, EventArgs e)
        {
            DialogView(0);
        }

        /// <summary>
        /// 習得スキル：削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemItemDelete_Click(object sender, EventArgs e)
        {
            DeleteItemSkill();
        }

        /// <summary>
        /// ダイアログ表示
        /// </summary>
        /// <param name="SkillID">スキルID</param>
        private void DialogView(int SkillID)
        {
            InstallSkillEditDialog dialog = new InstallSkillEditDialog(int.Parse(this.textBoxNo.Text), SkillID);
            dialog.ShowDialog(this);
        }

        /// <summary>
        /// 習得スキル削除
        /// </summary>
        private void DeleteItemSkill()
        {
            InstallDataEntity entity = LibInstall.Entity;

            InstallDataEntity.mt_install_class_skillRow skillRow = entity.mt_install_class_skill.FindByinstall_idperks_id(int.Parse(this.textBoxNo.Text), ((InstallDataEntity.mt_install_class_skillRow)((DataRowView)(this.dataGridViewSkill.SelectedRows[0].DataBoundItem)).Row).perks_id);

            if (skillRow == null) { return; }

            skillRow.Delete();
        }

        private void comboBoxUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResertAllPoints();
        }

        private void contextMenuStripEdit_Opening(object sender, CancelEventArgs e)
        {
            if (this.dataGridViewSkill.SelectedRows.Count > 0)
            {
                this.toolStripMenuItemItemEdit.Enabled = true;
            }
            else
            {
                this.toolStripMenuItemItemEdit.Enabled = false;
            }
        }
    }
}
