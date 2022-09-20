using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;

namespace DataEditForm.Quest.PopMonster
{
    public partial class MonsterSettingDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MonsterSettingDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <param name="MonsterRow">出現モンスターRow</param>
        public MonsterSettingDialog(int MarkID, QuestDataEntity.mt_mark_pop_monsterRow MonsterRow)
        {
            InitializeComponent();

            _selectedRow = MonsterRow;

            // データバインド
            this.comboBoxMonsList.DataSource = LibMonsterData.Entity.mt_monster_list;
            this.comboBoxMonsList.ValueMember = LibMonsterData.Entity.mt_monster_list.monster_idColumn.ColumnName;
            this.comboBoxMonsList.DisplayMember = LibMonsterData.Entity.mt_monster_list.monster_nameColumn.ColumnName;
        }

        private QuestDataEntity.mt_mark_pop_monsterRow _selectedRow;

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonsterSettingDialog_Load(object sender, EventArgs e)
        {
            if (_selectedRow == null) { return; }

            // 読み込み
            this.comboBoxMonsList.SelectedValue = _selectedRow.monster_id;

            this.numericUpDownPopCountMax.Value = _selectedRow.rare;
            this.numericUpDownPopProb.Value = _selectedRow.pop_prob;
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
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public int MonsterID
        {
            get { return (int)this.comboBoxMonsList.SelectedValue; }
        }

        public int PopCount
        {
            get { return (int)this.numericUpDownPopCountMax.Value; }
        }

        public int Prob
        {
            get { return (int)this.numericUpDownPopProb.Value; }
        }

        private void comboBoxMonsList_Format(object sender, ListControlConvertEventArgs e)
        {
            DataView MonsterBattleView = new DataView(LibMonsterData.Entity.mt_monster_battle_ability);
            MonsterBattleView.RowFilter = LibMonsterData.Entity.mt_monster_battle_ability.monster_idColumn.ColumnName + "=" + (int)((DataRowView)e.ListItem)[LibMonsterData.Entity.mt_monster_list.monster_idColumn.ColumnName];
            string LevelColumn = LibMonsterData.Entity.mt_monster_battle_ability.levelColumn.ColumnName;
            MonsterBattleView.Sort = LevelColumn;
            if (MonsterBattleView.Count > 0)
            {
                e.Value = ((int)((DataRowView)e.ListItem)[LibMonsterData.Entity.mt_monster_list.monster_idColumn.ColumnName]).ToString("00000") + ": " + e.Value+" (Lv."+(int)MonsterBattleView[0][LevelColumn] + " ～ " + (int)MonsterBattleView[MonsterBattleView.Count - 1][LevelColumn]+")";
            }
            else
            {
                e.Value = ((int)((DataRowView)e.ListItem)[LibMonsterData.Entity.mt_monster_list.monster_idColumn.ColumnName]).ToString("00000") + ": " + e.Value;
            }
        }
    }
}

