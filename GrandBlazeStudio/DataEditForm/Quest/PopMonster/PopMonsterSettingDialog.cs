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
    public partial class PopMonsterSettingDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PopMonsterSettingDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="MarkID">マークID</param>
        public PopMonsterSettingDialog(int MarkID)
        {
            InitializeComponent();

            _markID = MarkID;

            // データバインド
            DataView popMonsterView = new DataView(LibQuest.Entity.mt_mark_pop_monster);
            popMonsterView.RowFilter = LibQuest.Entity.mt_mark_pop_monster.mark_idColumn.ColumnName + "=" + _markID;
            popMonsterView.Sort = LibQuest.Entity.mt_mark_pop_monster.monster_idColumn.ColumnName;

            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = popMonsterView;
            this.columnNo.DataPropertyName = LibQuest.Entity.mt_mark_pop_monster.monster_idColumn.ColumnName;
            this.columnName.DataPropertyName = LibQuest.Entity.mt_mark_pop_monster.monster_idColumn.ColumnName;
            this.columnRace.DataPropertyName = LibQuest.Entity.mt_mark_pop_monster.monster_idColumn.ColumnName;
            this.columnLevel.DataPropertyName = LibQuest.Entity.mt_mark_pop_monster.monster_idColumn.ColumnName;
            this.columnWeather.DataPropertyName = LibQuest.Entity.mt_mark_pop_monster.monster_idColumn.ColumnName;
            this.columnProb.DataPropertyName = LibQuest.Entity.mt_mark_pop_monster.pop_probColumn.ColumnName;
            this.columnRare.DataPropertyName = LibQuest.Entity.mt_mark_pop_monster.rareColumn.ColumnName;
        }

        private int _markID = 0;

        /// <summary>
        /// ショートカットキー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Insert:
                    toolStripMenuItemAdd_Click(this, EventArgs.Empty);
                    break;
                case Keys.Delete:
                    toolStripMenuItemDelete_Click(this, EventArgs.Empty);
                    break;
            }
        }

        /// <summary>
        /// セルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripMenuItemEdit_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// 右クリック選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // ヘッダ以外のセルか？
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    // 右クリックされたセル
                    DataGridViewCell cell = this.dataGridViewList[e.ColumnIndex, e.RowIndex];
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
        private void dataGridViewList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // コンテキスト表示
                this.contextMenuStrip.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
        }

        /// <summary>
        /// 編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewList.SelectedRows.Count == 0) { return; }

            int MonsterID = ((QuestDataEntity.mt_mark_pop_monsterRow)((DataRowView)(this.dataGridViewList.SelectedRows[0].DataBoundItem)).Row).monster_id;

            DialogView(MonsterID);
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            DialogView(0);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewList.SelectedRows.Count == 0) { return; }

            int MonsterID = ((QuestDataEntity.mt_mark_pop_monsterRow)((DataRowView)(this.dataGridViewList.SelectedRows[0].DataBoundItem)).Row).monster_id;

            QuestDataEntity entity = LibQuest.Entity;

            QuestDataEntity.mt_mark_pop_monsterRow monsterRow = entity.mt_mark_pop_monster.FindBymark_idmonster_id(_markID, MonsterID);

            if (monsterRow == null) { return; }

            monsterRow.Delete();


        }

        /// <summary>
        /// セル内容の変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) { return; }

            switch (e.ColumnIndex)
            {
                case 1:
                    e.Value = LibMonsterData.GetNickName((int)e.Value);
                    break;
                case 2:
                    e.Value = LibMonsterData.GetCategoryName(LibMonsterData.GetCategoryID((int)e.Value));
                    break;
                case 3:
                    {
                        DataView MonsterBattleView = new DataView(LibMonsterData.Entity.mt_monster_battle_ability);
                        MonsterBattleView.RowFilter = LibMonsterData.Entity.mt_monster_battle_ability.monster_idColumn.ColumnName + "=" + (int)e.Value;
                        string LevelColumn = LibMonsterData.Entity.mt_monster_battle_ability.levelColumn.ColumnName;
                        MonsterBattleView.Sort = LevelColumn;
                        if (MonsterBattleView.Count > 0)
                        {
                            e.Value = (int)MonsterBattleView[0][LevelColumn] + " ～ " + (int)MonsterBattleView[MonsterBattleView.Count - 1][LevelColumn];
                        }
                        else
                        {
                            e.Value = "？";
                        }
                    }
                    break;
                case 4:
                    // 天候
                    e.Value = LibMonsterData.GetWeatherNames((int)e.Value);
                    break;
                case 5:
                    e.Value = e.Value + "%";
                    break;
            }
        }

        /// <summary>
        /// ダイアログ表示
        /// </summary>
        /// <param name="MonsterID">モンスターID</param>
        private void DialogView(int MonsterID)
        {
            QuestDataEntity.mt_mark_pop_monsterRow MonsterRow = null;

            if (MonsterID > 0)
            {
                MonsterRow = (QuestDataEntity.mt_mark_pop_monsterRow)((DataRowView)(this.dataGridViewList.SelectedRows[0].DataBoundItem)).Row;
            }

            MonsterSettingDialog dialog = new MonsterSettingDialog(_markID, MonsterRow);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // 追加/変更
                QuestDataEntity entity = LibQuest.Entity;

                bool isNew = false;
                QuestDataEntity.mt_mark_pop_monsterRow row = entity.mt_mark_pop_monster.FindBymark_idmonster_id(_markID, dialog.MonsterID);

                if (row == null)
                {
                    row = entity.mt_mark_pop_monster.Newmt_mark_pop_monsterRow();
                    isNew = true;
                }

                if (isNew) { row.mark_id = _markID; }
                if (isNew) { row.monster_id = dialog.MonsterID; }

                if (isNew || row.rare != dialog.PopCount) { row.rare = dialog.PopCount; }
                if (isNew || row.pop_prob != dialog.Prob) { row.pop_prob = dialog.Prob; }

                if (isNew) { entity.mt_mark_pop_monster.Addmt_mark_pop_monsterRow(row); }
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            QuestDataEntity entity = LibQuest.Entity;
            entity.mt_mark_pop_monster.RejectChanges();

            this.Close();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

