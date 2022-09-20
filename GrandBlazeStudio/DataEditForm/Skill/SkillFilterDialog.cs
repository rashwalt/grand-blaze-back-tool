using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;

namespace DataEditForm.Skill
{
    public partial class SkillFilterDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SkillFilterDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フィルタ文字列
        /// </summary>
        public string FilterString
        {
            get
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

                return string.Join(" AND ", filter.ToArray());
            }
        }

        /// <summary>
        /// フィルタ条件リセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            DefalutFilterSet();
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkillFilterDialog_Load(object sender, EventArgs e)
        {
            DefalutFilterSet();
        }

        /// <summary>
        /// 初期表示モード
        /// </summary>
        private void DefalutFilterSet()
        {
            this.comboBoxFilterType.SelectedIndex = 0;
            this.comboBoxFilterLevel.SelectedIndex = 0;
            this.numericUpDownFilterLevel.Value = 0;
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
    }
}

