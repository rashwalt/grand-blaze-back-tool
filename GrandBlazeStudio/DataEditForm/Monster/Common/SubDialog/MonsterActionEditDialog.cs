using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonFormLibrary.CommonDialog;

namespace DataEditForm.Monster.Common.SubDialog
{
    public partial class MonsterActionEditDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MonsterActionEditDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="MonsterID">モンスターID</param>
        /// <param name="inRow">対象DataRow</param>
        public MonsterActionEditDialog(int MonsterID, MonsterDataEntity.mt_monster_actionRow inRow)
        {
            InitializeComponent();

            _monsterID = MonsterID;

            // オートバインド
            DataView TimingOutOne = new DataView(LibAction.Entity.mt_timing_list);
            TimingOutOne.RowFilter = LibAction.Entity.mt_timing_list.timing_idColumn.ColumnName + ">0";
            TimingOutOne.Sort = LibAction.Entity.mt_timing_list.timing_idColumn.ColumnName;
            this.comboBoxTiming1.DataSource = TimingOutOne;
            this.comboBoxTiming1.DisplayMember = LibAction.Entity.mt_timing_list.timing_nameColumn.ColumnName;
            this.comboBoxTiming1.ValueMember = LibAction.Entity.mt_timing_list.timing_idColumn.ColumnName;

            DataView TimingOutTwo = new DataView(LibAction.Entity.mt_timing_list);
            TimingOutTwo.Sort = LibAction.Entity.mt_timing_list.timing_idColumn.ColumnName;
            this.comboBoxTiming2.DataSource = TimingOutTwo;
            this.comboBoxTiming2.DisplayMember = LibAction.Entity.mt_timing_list.timing_nameColumn.ColumnName;
            this.comboBoxTiming2.ValueMember = LibAction.Entity.mt_timing_list.timing_idColumn.ColumnName;

            DataView TimingOutThree = new DataView(LibAction.Entity.mt_timing_list);
            TimingOutThree.Sort = LibAction.Entity.mt_timing_list.timing_idColumn.ColumnName;
            this.comboBoxTiming3.DataSource = TimingOutThree;
            this.comboBoxTiming3.DisplayMember = LibAction.Entity.mt_timing_list.timing_nameColumn.ColumnName;
            this.comboBoxTiming3.ValueMember = LibAction.Entity.mt_timing_list.timing_idColumn.ColumnName;

            this.comboBoxAction.DataSource = LibAction.Entity.mt_action_list;
            this.comboBoxAction.DisplayMember = LibAction.Entity.mt_action_list.action_nameColumn.ColumnName;
            this.comboBoxAction.ValueMember = LibAction.Entity.mt_action_list.action_idColumn.ColumnName;

            DataView enemyView = new DataView(LibAction.Entity.mt_target_list);
            enemyView.RowFilter = LibAction.Entity.mt_target_list.target_typeColumn.ColumnName + "=0";
            this.comboBoxTargetTypeEnemy.DataSource = enemyView;
            this.comboBoxTargetTypeEnemy.DisplayMember = LibAction.Entity.mt_target_list.target_act_nameColumn.ColumnName;
            this.comboBoxTargetTypeEnemy.ValueMember = LibAction.Entity.mt_target_list.target_idColumn.ColumnName;

            DataView friendView = new DataView(LibAction.Entity.mt_target_list);
            friendView.RowFilter = LibAction.Entity.mt_target_list.target_typeColumn.ColumnName + "=1";
            this.comboBoxTargetTypeFriend.DataSource = friendView;
            this.comboBoxTargetTypeFriend.DisplayMember = LibAction.Entity.mt_target_list.target_act_nameColumn.ColumnName;
            this.comboBoxTargetTypeFriend.ValueMember = LibAction.Entity.mt_target_list.target_idColumn.ColumnName;

            DataView mineView = new DataView(LibAction.Entity.mt_target_list);
            mineView.RowFilter = LibAction.Entity.mt_target_list.target_typeColumn.ColumnName + "=2";
            this.comboBoxTargetTypeMine.DataSource = mineView;
            this.comboBoxTargetTypeMine.DisplayMember = LibAction.Entity.mt_target_list.target_act_nameColumn.ColumnName;
            this.comboBoxTargetTypeMine.ValueMember = LibAction.Entity.mt_target_list.target_idColumn.ColumnName;

            int Target = inRow.action_target;

            if (Target == 0)
            {
                this.radioButtonTargetTypeNone.Checked = true;
            }
            else
            {
                ActionDataEntity.mt_target_listRow row = LibAction.Entity.mt_target_list.FindBytarget_id(Target);

                if (row != null)
                {
                    switch (row.target_type)
                    {
                        case 0:
                            this.radioButtonTargetTypeEnemy.Checked = true;
                            this.comboBoxTargetTypeEnemy.SelectedValue = inRow.action_target;
                            break;
                        case 1:
                            this.radioButtonTargetTypeFriend.Checked = true;
                            this.comboBoxTargetTypeFriend.SelectedValue = inRow.action_target;
                            break;
                        case 2:
                            this.radioButtonTargetTypeMine.Checked = true;
                            this.comboBoxTargetTypeMine.SelectedValue = inRow.action_target;
                            break;
                    }
                }
                else
                {
                    this.radioButtonTargetTypeEnemy.Checked = true;
                }
            }

            Row = inRow;

            if (Row == null)
            {
                this.comboBoxAction.SelectedIndex = 0;
                this.comboBoxProb.SelectedIndex = 4;
                this.numericUpDownMaxCount.Value = 0;
                this.comboBoxTiming1.SelectedIndex = 0;
                this.comboBoxTiming2.SelectedIndex = 0;
                this.comboBoxTiming3.SelectedIndex = 0;
                return;
            }

            this.comboBoxAction.SelectedValue = Row.action;
            this.comboBoxProb.SelectedIndex = Row.probability;
            this.numericUpDownMaxCount.Value = Row.max_count;
            this.comboBoxTiming1.SelectedValue = Row.timing1;
            this.comboBoxTiming2.SelectedValue = Row.timing2;
            this.comboBoxTiming3.SelectedValue = Row.timing3;

            this.textBoxArtsName.Text = LibSkill.GetSkillName(Row.perks_id);
        }

        private int _monsterID = 0;
        public MonsterDataEntity.mt_monster_actionRow Row = null;

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
            // DataRowに反映
            Row.timing1 = (int)this.comboBoxTiming1.SelectedValue;
            Row.timing2 = (int)this.comboBoxTiming2.SelectedValue;
            Row.timing3 = (int)this.comboBoxTiming3.SelectedValue;
            Row.action_target = SelectedTarget;
            Row.action = (int)this.comboBoxAction.SelectedValue;
            Row.probability = this.comboBoxProb.SelectedIndex;
            Row.max_count = (int)this.numericUpDownMaxCount.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// アーツ設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonArtsSelect_Click(object sender, EventArgs e)
        {
            SkillSelectDialog dialog = new SkillSelectDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                Row.perks_id = dialog.SkillID;

                this.textBoxArtsName.Text = LibSkill.GetSkillName(Row.perks_id);
            }
        }

        /// <summary>
        /// ターゲット：敵 有効変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonTargetTypeEnemy_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxTargetTypeEnemy.Enabled = this.radioButtonTargetTypeEnemy.Checked;
        }

        /// <summary>
        /// ターゲット：味方 有効変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonTargetTypeFriend_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxTargetTypeFriend.Enabled = this.radioButtonTargetTypeFriend.Checked;
        }

        /// <summary>
        /// ターゲット：自分 有効変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonTargetTypeMine_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxTargetTypeMine.Enabled = this.radioButtonTargetTypeMine.Checked;
        }

        /// <summary>
        /// 変更ターゲット
        /// </summary>
        private int SelectedTarget
        {
            get
            {
                if (this.radioButtonTargetTypeNone.Checked)
                {
                    return 0;
                }
                if (this.radioButtonTargetTypeEnemy.Checked)
                {
                    return (int)this.comboBoxTargetTypeEnemy.SelectedValue;
                }
                if (this.radioButtonTargetTypeFriend.Checked)
                {
                    return (int)this.comboBoxTargetTypeFriend.SelectedValue;
                }
                if (this.radioButtonTargetTypeMine.Checked)
                {
                    return (int)this.comboBoxTargetTypeMine.SelectedValue;
                }

                return 0;
            }
        }
    }
}

