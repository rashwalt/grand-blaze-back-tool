using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;

namespace CommonFormLibrary.CommonDialog
{
    public partial class SkillSelectDialog : CommonFormLibrary.BaseDialog
    {
        private DataView _perksView;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SkillSelectDialog()
        {
            InitializeComponent();

            // データバインド
            _perksView = new DataView(LibSkill.Entity.skill_list);
            _perksView.RowFilter = "";
            _perksView.Sort = LibSkill.Entity.skill_list.sk_idColumn.ColumnName;

            this.comboBoxItems.DataSource = _perksView;
            this.comboBoxItems.ValueMember = LibSkill.Entity.skill_list.sk_idColumn.ColumnName;
            this.comboBoxItems.DisplayMember = LibSkill.Entity.skill_list.sk_nameColumn.ColumnName;

            SkillTypeEntity.mt_skill_categoryDataTable SkillCategoryTable = (SkillTypeEntity.mt_skill_categoryDataTable)LibSkillType.Entity.mt_skill_category.Copy();
            SkillCategoryTable.Addmt_skill_categoryRow(0, "すべて", 0);
            SkillCategoryTable.DefaultView.Sort = LibSkillType.Entity.mt_skill_category.category_idColumn.ColumnName;

            this.comboBoxArtsCategory.DataSource = SkillCategoryTable.DefaultView;
            this.comboBoxArtsCategory.ValueMember = LibSkillType.Entity.mt_skill_category.category_idColumn.ColumnName;
            this.comboBoxArtsCategory.DisplayMember = LibSkillType.Entity.mt_skill_category.category_nameColumn.ColumnName;

            // 初期選択
            this.comboBoxFilterType.SelectedIndex = 0;
            this.comboBoxFilterLevel.SelectedIndex = 0;
        }

        /// <summary>
        /// データ設定
        /// </summary>
        /// <param name="SkillID">スキルID</param>
        public void SetData(int SkillID)
        {
            this.comboBoxItems.SelectedValue = SkillID;
        }

        public int SkillID
        {
            get { return (int)this.comboBoxItems.SelectedValue; }
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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #region フィルタ

        private void comboBoxFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterGenerate();
        }

        private void numericUpDownFilterLevel_ValueChanged(object sender, EventArgs e)
        {
            FilterGenerate();
        }

        private void comboBoxFilterLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterGenerate();
        }

        private void comboBoxArtsCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterGenerate();
        }

        private void FilterGenerate()
        {
            List<string> filter = new List<string>();

            if (this.comboBoxFilterType.SelectedIndex > 0)
            {
                filter.Add(LibSkill.Entity.skill_list.sk_typeColumn.ColumnName + "=" + (this.comboBoxFilterType.SelectedIndex - 1));
            }

            // カテゴリ
            if (this.comboBoxArtsCategory.SelectedIndex > 0)
            {
                filter.Add(LibSkill.Entity.skill_list.sk_arts_categoryColumn.ColumnName + "=" + (int)this.comboBoxArtsCategory.SelectedValue);
            }

            // ID
            if (this.comboBoxFilterLevel.SelectedIndex == 0)
            {
                filter.Add(LibSkill.Entity.skill_list.sk_idColumn.ColumnName + ">=" + this.numericUpDownFilterLevel.Value);
            }
            else
            {
                filter.Add(LibSkill.Entity.skill_list.sk_idColumn.ColumnName + "<=" + this.numericUpDownFilterLevel.Value);
            }

            _perksView.RowFilter = string.Join(" AND ", filter.ToArray());
        }
        #endregion

        /// <summary>
        /// 表示を再設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxItems_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((int)((DataRowView)e.ListItem)[LibSkill.Entity.skill_list.sk_idColumn.ColumnName]).ToString("00000") + ": " + e.Value;
        }
    }
}

