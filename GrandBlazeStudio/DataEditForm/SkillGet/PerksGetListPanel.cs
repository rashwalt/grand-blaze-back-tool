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
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary.DataAccess;
using CommonFormLibrary.CommonDialog;

namespace DataEditForm.SkillGet
{
    public partial class SkillGetListPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SkillGetListPanel()
        {
            InitializeComponent();

            // データバインド
            InstallDataEntity.mt_install_class_listDataTable InstallTable = (InstallDataEntity.mt_install_class_listDataTable)LibInstall.Entity.mt_install_class_list.Copy();
            InstallTable.Addmt_install_class_listRow(0, "なし", 0, 0, 0, 0, 0, 0, "");

            InstallTable.DefaultView.Sort = LibInstall.Entity.mt_install_class_list.install_idColumn.ColumnName;

            this.comboBoxInstall.DataSource = InstallTable.DefaultView;
            this.comboBoxInstall.DisplayMember = LibInstall.Entity.mt_install_class_list.classnameColumn.ColumnName;
            this.comboBoxInstall.ValueMember = LibInstall.Entity.mt_install_class_list.install_idColumn.ColumnName;

            HumanRaceEntity.mt_race_listDataTable RaceTable = (HumanRaceEntity.mt_race_listDataTable)LibRace.Entity.mt_race_list.Copy();
            RaceTable.Addmt_race_listRow(0, "なし", 0, 0, 0, 0, 0, 0);

            RaceTable.DefaultView.Sort = LibRace.Entity.mt_race_list.race_idColumn.ColumnName;

            this.comboBoxRace.DataSource = RaceTable.DefaultView;
            this.comboBoxRace.DisplayMember = LibRace.Entity.mt_race_list.race_nameColumn.ColumnName;
            this.comboBoxRace.ValueMember = LibRace.Entity.mt_race_list.race_idColumn.ColumnName;

            this.textBoxNo.Text = "0";

            NothingFilter();
        }

        private DataView _perksView;

        /// <summary>
        /// 初期表示
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
            SkillGetEntity entity = LibSkill.GetEntity;
            entity.RejectChanges();
        }

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            SkillGetEntity entity = LibSkill.GetEntity;
            _perksView = new DataView(entity.mt_skill_get_list);
            _perksView.Sort = entity.mt_skill_get_list.perks_idColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = _perksView;
            this.columnNo.DataPropertyName = entity.mt_skill_get_list.perks_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.mt_skill_get_list.perks_idColumn.ColumnName;
        }

        /// <summary>
        /// 詳細表示
        /// </summary>
        /// <param name="SkillID">表示対象ID</param>
        private void PrivateView(int SkillID)
        {
            SkillGetEntity entity = LibSkill.GetEntity;

            if (SkillID == 0)
            {
                // 新規ID発行
                DataView skillViewNumber = new DataView(entity.mt_skill_get_list);

                SetNewNumberDialog dialog = new SetNewNumberDialog();
                dialog.SetNewNumber(LibInteger.GetNewUnderNum(skillViewNumber, entity.mt_skill_get_list.perks_idColumn.ColumnName));
                dialog.ValidatingNumber += new EventHandler(Validate_Number);

                switch (dialog.ShowDialog(this))
                {
                    case DialogResult.OK:
                        SkillID = dialog.NewID;
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            if (SkillID <= 0) { return; }

            // 基本情報の表示
            SkillGetEntity.mt_skill_get_listRow baseRow = entity.mt_skill_get_list.FindByperks_id(SkillID);

            this.textBoxNo.Text = SkillID.ToString();

            if (baseRow == null)
            {
                // 新規
                baseRow = entity.mt_skill_get_list.Newmt_skill_get_listRow();

                #region スキル新規設定
                baseRow.perks_id = SkillID;
                baseRow.tm_install = 0;
                baseRow.tm_install_level = 0;
                baseRow.tm_race = 0;
                baseRow.tm_guardian = 0;
                baseRow.tm_level = 0;
                baseRow.tm_str = 0;
                baseRow.tm_agi = 0;
                baseRow.tm_mag = 0;
                baseRow.tm_unq = 0;
                baseRow.tm_base_skill = 0;
                #endregion

                entity.mt_skill_get_list.Addmt_skill_get_listRow(baseRow);
            }

            //this.textBoxName.Text = LibSkill.GetSkillName(SkillID, false);

            this.comboBoxInstall.SelectedValue = baseRow.tm_install;
            this.numericUpDownInstallLevel.Value = baseRow.tm_install_level;
            this.comboBoxRace.SelectedValue = baseRow.tm_race;
            this.comboBoxGuardian.SelectedIndex = baseRow.tm_guardian;
            this.numericUpDownLevel.Value = baseRow.tm_level;
            this.numericUpDownSTR.Value = baseRow.tm_str;
            this.numericUpDownAGI.Value = baseRow.tm_agi;
            this.numericUpDownMAG.Value = baseRow.tm_mag;
            this.numericUpDownUNQ.Value = baseRow.tm_unq;
            this.textBoxBaseNo.Text = baseRow.tm_base_skill.ToString();
        }

        /// <summary>
        /// 番号重複管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Validate_Number(object sender, EventArgs e)
        {
            CommonSkillEntity entity = LibSkill.Entity;
            SetNewNumberDialog dialog = (SetNewNumberDialog)sender;
            CommonSkillEntity.skill_listRow skillRow = entity.skill_list.FindBysk_id(dialog.NewID);

            if (skillRow != null)
            {
                dialog.labelCaution.Visible = true;
            }
            else
            {
                dialog.labelCaution.Visible = false;
            }
        }

        /// <summary>
        /// リアルタイムにセル内容を変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dataGridViewList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = LibSkill.GetSkillName((int)e.Value);
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
            // 削除を有効に
            if (this.dataGridViewList.SelectedCells.Count > 0 && this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value != null)
            {
                this.toolStripMenuItemDelete.Enabled = true;
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
        /// <param name="SkillID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int SkillID, int rowIndex)
        {
            SkillGetEntity entity = LibSkill.GetEntity;

            SkillGetEntity.mt_skill_get_listRow perksRow = entity.mt_skill_get_list.FindByperks_id(SkillID);

            if (perksRow == null) { return; }

            perksRow.Delete();

            if (this.dataGridViewList.Rows.Count > 0 && this.dataGridViewList[0, 0].Value != null)
            {
                PrivateView((int)this.dataGridViewList[0, 0].Value);
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
            SkillGetEntity entity = LibSkill.GetEntity;

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

                dba.Update(LibSkill.GetEntity.mt_skill_get_list);
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

            LibSkill.LoadSkill();
        }

        private int SelectedNo
        {
            get { return int.Parse(this.textBoxNo.Text); }
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            SkillGetEntity entity = LibSkill.GetEntity;

            if (this.textBoxNo.Text == "0") { return; }

            SkillGetEntity.mt_skill_get_listRow row = entity.mt_skill_get_list.FindByperks_id(int.Parse(this.textBoxNo.Text));

            bool isNew = false;

            if (row == null)
            {
                row = entity.mt_skill_get_list.Newmt_skill_get_listRow();
                isNew = true;

                row.perks_id = int.Parse(this.textBoxNo.Text);
            }

            if (isNew || row.tm_install != (int)this.comboBoxInstall.SelectedValue) { row.tm_install = (int)this.comboBoxInstall.SelectedValue; }
            if (isNew || row.tm_install_level != (int)this.numericUpDownInstallLevel.Value) { row.tm_install_level = (int)this.numericUpDownInstallLevel.Value; }
            if (isNew || row.tm_race != (int)this.comboBoxRace.SelectedValue) { row.tm_race = (int)this.comboBoxRace.SelectedValue; }
            if (isNew || row.tm_guardian != this.comboBoxGuardian.SelectedIndex) { row.tm_guardian = this.comboBoxGuardian.SelectedIndex; }
            if (isNew || row.tm_level != (int)this.numericUpDownLevel.Value) { row.tm_level = (int)this.numericUpDownLevel.Value; }

            if (isNew || row.tm_str != (int)this.numericUpDownSTR.Value) { row.tm_str = (int)this.numericUpDownSTR.Value; }
            if (isNew || row.tm_agi != (int)this.numericUpDownAGI.Value) { row.tm_agi = (int)this.numericUpDownAGI.Value; }
            if (isNew || row.tm_mag != (int)this.numericUpDownMAG.Value) { row.tm_mag = (int)this.numericUpDownMAG.Value; }
            if (isNew || row.tm_unq != (int)this.numericUpDownUNQ.Value) { row.tm_unq = (int)this.numericUpDownUNQ.Value; }

            if (isNew || row.tm_base_skill != int.Parse(this.textBoxBaseNo.Text)) { row.tm_base_skill = int.Parse(this.textBoxBaseNo.Text); }

            if (isNew)
            {
                entity.mt_skill_get_list.Addmt_skill_get_listRow(row);
            }
        }

        /// <summary>
        /// アーツ選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonArtsSelect_Click(object sender, EventArgs e)
        {
            SkillSelectDialog dialog = new SkillSelectDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.textBoxNo.Text = dialog.SkillID.ToString();
            }
        }

        /// <summary>
        /// アーツ名称取得設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxNo_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxNo.Text == "0")
            {
                this.textBoxName.Text = "";
                return;
            }

            this.textBoxName.Text = LibSkill.GetSkillName(int.Parse(this.textBoxNo.Text));
        }

        /// <summary>
        /// アーツ選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBase_Click(object sender, EventArgs e)
        {
            SkillSelectDialog dialog = new SkillSelectDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.textBoxBaseNo.Text = dialog.SkillID.ToString();
            }
        }

        /// <summary>
        /// アーツ名称取得設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxBaseNo_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxBaseNo.Text == "0")
            {
                this.textBoxBaseName.Text = "";
                return;
            }

            this.textBoxBaseName.Text = LibSkill.GetSkillName(int.Parse(this.textBoxBaseNo.Text));
        }
    }
}
