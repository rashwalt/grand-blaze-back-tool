using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;

namespace DataEditForm.Monster.Common.SubDialog
{
    public partial class SerifSettingDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SerifSettingDialog()
        {
            InitializeComponent();

            // オートバインド
            this.comboBoxSituation.DataSource = LibSituation.Entity.mt_situation_list;
            this.comboBoxSituation.DisplayMember = LibSituation.Entity.mt_situation_list.situation_textColumn.ColumnName;
            this.comboBoxSituation.ValueMember = LibSituation.Entity.mt_situation_list.situation_noColumn.ColumnName;

            CommonSkillEntity.skill_listDataTable SkillTable = (CommonSkillEntity.skill_listDataTable)LibSkill.Entity.skill_list.Copy();

            CommonSkillEntity.skill_listRow baseRow = SkillTable.Newskill_listRow();

            #region スキル新規設定
            baseRow.sk_id = 0;
            baseRow.sk_name = "指定なし";
            baseRow.sk_type = 0;
            baseRow.sk_atype = 0;
            baseRow.sk_mp = 0;
            baseRow.sk_tp = 0;
            baseRow.sk_range = 0;
            baseRow.sk_power = 0;
            baseRow.sk_damage_rate = 0;
            baseRow.sk_plus_score = 1;
            baseRow.sk_round = 1;
            baseRow.sk_hit = 0;
            baseRow.sk_critical = 0;
            baseRow.sk_critical_type = 0;
            baseRow.sk_hate = 1;
            baseRow.sk_vhate = 0;
            baseRow.sk_charge = 0;
            baseRow.sk_antiair = false;
            baseRow.sk_damage_type = 0;
            baseRow.sk_arts_category = 0;
            baseRow.sk_target_restrict = 0;
            baseRow.sk_use_limit = 0;
            baseRow.sk_fire = 0;
            baseRow.sk_freeze = 0;
            baseRow.sk_air = 0;
            baseRow.sk_earth = 0;
            baseRow.sk_water = 0;
            baseRow.sk_thunder = 0;
            baseRow.sk_holy = 0;
            baseRow.sk_dark = 0;
            baseRow.sk_slash = 0;
            baseRow.sk_pierce = 0;
            baseRow.sk_strike = 0;
            baseRow.sk_break = 0;
            baseRow.sk_target_party = 0;
            baseRow.sk_target_area = 0;
            baseRow.sk_effect = "0,0,0,0,0,0";
            baseRow.sk_comment = "";
            #endregion

            SkillTable.Addskill_listRow(baseRow);

            _skillView = new DataView(SkillTable);
            _skillView.Sort = LibSkill.Entity.skill_list.sk_idColumn.ColumnName;

            this.comboBoxArts.DataSource = _skillView;
            this.comboBoxArts.ValueMember = LibSkill.Entity.skill_list.sk_idColumn.ColumnName;
            this.comboBoxArts.DisplayMember = LibSkill.Entity.skill_list.sk_nameColumn.ColumnName;
        }

        private DataView _skillView;

        /// <summary>
        /// 初期設定
        /// </summary>
        /// <param name="situationID">シチュエーションID</param>
        /// <param name="serifText">セリフ</param>
        /// <param name="SkillID">スキルID</param>
        public void SetData(int situationID, string serifText, int SkillID)
        {
            this.comboBoxSituation.SelectedValue = situationID;
            this.textBoxSerifText.Text = serifText;
            this.comboBoxArts.SelectedValue = SkillID;
        }

        public int SituationID
        {
            get { return (int)this.comboBoxSituation.SelectedValue; }
        }

        public string SerifText
        {
            get { return this.textBoxSerifText.Text; }
        }

        public int SkillID
        {
            get { return (int)this.comboBoxArts.SelectedValue; }
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
            // 入力チェック
            if (this.textBoxSerifText.Text.Length == 0)
            {
                MessageBox.Show("セリフ内容が入力されていません。", "入力チェック", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

