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
    public partial class SelectionEffectDialog : CommonFormLibrary.BaseDialog
    {
        private DataView _effectView;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SelectionEffectDialog()
        {
            InitializeComponent();

            // データバインド
            _effectView = new DataView(LibEffect.Entity.mt_effect_list);
            _effectView.RowFilter = "";
            _effectView.Sort = LibEffect.Entity.mt_effect_list.effect_idColumn.ColumnName;

            this.comboBoxEffect.DataSource = _effectView;
            this.comboBoxEffect.ValueMember = LibEffect.Entity.mt_effect_list.effect_idColumn.ColumnName;
            this.comboBoxEffect.DisplayMember = LibEffect.Entity.mt_effect_list.ef_nameColumn.ColumnName;

            this.comboBoxEffect.SelectedIndex = 0;
        }

        /// <summary>
        /// データ設定
        /// </summary>
        /// <param name="EffectID">エフェクトID</param>
        /// <param name="Rank">ランク</param>
        /// <param name="SubRank">サブランク</param>
        /// <param name="Prob">確率</param>
        /// <param name="EndLimit">持続カウント</param>
        /// <param name="IsHide">説明を表示するか</param>
        public void SetData(int EffectID, decimal Rank, decimal SubRank, decimal Prob, int EndLimit, bool IsHide)
        {
            this.comboBoxEffect.SelectedValue = EffectID;
            setAutoValidNumeric(this.numericUpDownRank, Rank);
            setAutoValidNumeric(this.numericUpDownSubRank, SubRank);
            setAutoValidNumeric(this.numericUpDownProb, Prob);
            setAutoValidNumeric(this.numericUpDownEndLimit, EndLimit);
            this.checkBoxHide.Checked = IsHide;
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
        /// 表示を再設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEffect_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((int)((DataRowView)e.ListItem)[LibEffect.Entity.mt_effect_list.effect_idColumn.ColumnName]).ToString("00000") + ": " + e.Value;
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

        /// <summary>
        /// フィルタの有効無効切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxFilterValid_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBoxFilter.Enabled = this.checkBoxFilterValid.Checked;

            Filter();
        }

        /// <summary>
        /// フィルタ実行
        /// </summary>
        private void Filter()
        {
            if (this.groupBoxFilter.Enabled)
            {
                _effectView.RowFilter = LibEffect.Entity.mt_effect_list.effect_idColumn.ColumnName + ">=" + (int)this.numericUpDownFilterEffectIDMin.Value + " and " + LibEffect.Entity.mt_effect_list.effect_idColumn.ColumnName + "<=" + (int)this.numericUpDownFilterEffectIDMax.Value;
            }
            else
            {
                _effectView.RowFilter = "";
            }
        }

        /// <summary>
        /// 値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEffect_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.comboBoxEffect.ValueMember.Length == 0) { return; }

            int EffectID = (int)this.comboBoxEffect.SelectedValue;

            EffectEntity.mt_effect_listRow row = LibEffect.Entity.mt_effect_list.FindByeffect_id(EffectID);

            if (row != null)
            {
                this.numericUpDownRank.Minimum = -99999;
                this.numericUpDownRank.Maximum = 99999;

                this.numericUpDownRank.Value = row.rank_default;
                this.numericUpDownRank.Maximum = row.rank_max;
                this.numericUpDownRank.Minimum = row.rank_min;

                this.numericUpDownSubRank.Minimum = -99999;
                this.numericUpDownSubRank.Maximum = 99999;

                this.numericUpDownSubRank.Value = row.sub_rank_default;
                this.numericUpDownSubRank.Maximum = row.sub_rank_max;
                this.numericUpDownSubRank.Minimum = row.sub_rank_min;

                this.numericUpDownProb.Minimum = -99999;
                this.numericUpDownProb.Maximum = 99999;

                this.numericUpDownProb.Value = row.prob_default;
                this.numericUpDownProb.Maximum = row.prob_max;
                this.numericUpDownProb.Minimum = row.prob_min;

                this.numericUpDownEndLimit.Minimum = -99999;
                this.numericUpDownEndLimit.Maximum = 99999;

                this.numericUpDownEndLimit.Value = row.limit_default;
                this.numericUpDownEndLimit.Maximum = row.limit_max;
                this.numericUpDownEndLimit.Minimum = row.limit_min;

                this.checkBoxHide.Checked = false;
            }
        }

        public int EffectID
        {
            get { return (int)this.comboBoxEffect.SelectedValue; }
        }

        public decimal Rank
        {
            get { return (int)this.numericUpDownRank.Value; }
        }

        public decimal SubRank
        {
            get { return (int)this.numericUpDownSubRank.Value; }
        }

        public decimal Prob
        {
            get { return (int)this.numericUpDownProb.Value; }
        }

        public int EndLimit
        {
            get { return (int)this.numericUpDownEndLimit.Value; }
        }

        public bool IsHide
        {
            get { return this.checkBoxHide.Checked; }
        }

        private void numericUpDownFilterEffectIDMin_ValueChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void numericUpDownFilterEffectIDMax_ValueChanged(object sender, EventArgs e)
        {
            Filter();
        }

        /// <summary>
        /// 完全防御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPerfectDeffence_Click(object sender, EventArgs e)
        {
            this.numericUpDownRank.Value = 100;
            this.numericUpDownSubRank.Value = 1;
            this.numericUpDownEndLimit.Value = -1;
            this.numericUpDownProb.Value = -1;
        }

        private void setAutoValidNumeric(NumericUpDown target, decimal input)
        {
            if (input > target.Maximum)
            {
                target.Value = target.Maximum;
            }
            else if (input < target.Minimum)
            {
                target.Value = target.Minimum;
            }
            else
            {
                target.Value = input;
            }
        }
    }
}

