using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonFormLibrary.CommonDialog;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;

namespace DataEditForm.InstallClass
{
    public partial class InstallSkillEditDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InstallSkillEditDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="InstallID">インストールID</param>
        /// <param name="InstallSkillID">スキルID</param>
        public InstallSkillEditDialog(int InstallID, int InstallSkillID)
        {
            InitializeComponent();

            _InstallID = InstallID;
            _InstallSkillID = InstallSkillID;
        }

        private int _selectSkillID = 0;
        private int _InstallID = 0;
        private int _InstallSkillID = 0;

        /// <summary>
        /// アーツ選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonArtsSelect_Click(object sender, EventArgs e)
        {
            SkillSelectDialog dialog = new SkillSelectDialog();
            dialog.SetData(_selectSkillID);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _selectSkillID = dialog.SkillID;

                this.textBoxArtsName.Text = LibSkill.GetSkillName(_selectSkillID);
            }
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstallSkillEditDialog_Load(object sender, EventArgs e)
        {
            InstallDataEntity entity = LibInstall.Entity;

            // 基本情報の表示

            InstallDataEntity.mt_install_class_skillRow baseRow = entity.mt_install_class_skill.FindByinstall_idperks_id(_InstallID, _InstallSkillID);

            if (baseRow != null)
            {
                this.numericUpDownRank.Value = baseRow.install_level;
                this.comboBoxOnlyMode.SelectedIndex = baseRow.only_mode;
                _selectSkillID = baseRow.perks_id;
            }
            else
            {
                this.numericUpDownRank.Value = 1;
                this.comboBoxOnlyMode.SelectedIndex = 0;
                _selectSkillID = 0;
            }

            this.textBoxArtsName.Text = LibSkill.GetSkillName(_InstallSkillID);
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            InstallDataEntity entity = LibInstall.Entity;

            InstallDataEntity.mt_install_class_skillRow baseRow = entity.mt_install_class_skill.FindByinstall_idperks_id(_InstallID, _selectSkillID);

            if (baseRow != null)
            {
                // 既存データの変更
                baseRow.install_level = (int)this.numericUpDownRank.Value;
                baseRow.only_mode = this.comboBoxOnlyMode.SelectedIndex;
            }
            else
            {
                baseRow = entity.mt_install_class_skill.Newmt_install_class_skillRow();

                baseRow.install_id = _InstallID;
                baseRow.install_level = (int)this.numericUpDownRank.Value;
                baseRow.perks_id = _selectSkillID;
                baseRow.only_mode = this.comboBoxOnlyMode.SelectedIndex;

                entity.mt_install_class_skill.Addmt_install_class_skillRow(baseRow);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
