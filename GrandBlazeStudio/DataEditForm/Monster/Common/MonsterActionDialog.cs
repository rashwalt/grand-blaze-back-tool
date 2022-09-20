using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Entity;
using DataEditForm.Monster.Common.SubDialog;
using CommonLibrary;

namespace DataEditForm.Monster.Common
{
    public partial class MonsterActionDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ（デザイン用）
        /// </summary>
        public MonsterActionDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="MonsterID">モンスターID</param>
        /// <param name="ActionTable">反映先テーブル</param>
        /// <param name="TargetDefault">デフォルトターゲット</param>
        public MonsterActionDialog(int MonsterID, MonsterDataEntity.mt_monster_actionDataTable ActionTable)
        {
            InitializeComponent();

            _monsterID = MonsterID;

            _table = ActionTable;
            _actionView = new DataView(_table);
            _actionView.RowFilter = ActionTable.monster_idColumn.ColumnName + "=" + _monsterID;
            _actionView.Sort = ActionTable.action_noColumn.ColumnName;

            this.dataGridViewList.CurrentCell = null;
        }

        private int _monsterID = 0;
        private MonsterDataEntity.mt_monster_actionDataTable _table;
        private DataView _actionView;
        private int _newIndex = 0;

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonsterActionDialog_Load(object sender, EventArgs e)
        {
            this.dataGridViewList.Rows.Clear();

            foreach (DataRowView rowView in _actionView)
            {
                MonsterDataEntity.mt_monster_actionRow row = (MonsterDataEntity.mt_monster_actionRow)rowView.Row;

                int rowIndex = this.dataGridViewList.Rows.Add(
                    row.timing1,
                    row.timing2,
                    row.timing3,
                    row.action_target,
                    row.action,
                    row.probability,
                    row.max_count,
                    row.perks_id
                    );

                DataGridViewRow GridRow = this.dataGridViewList.Rows[rowIndex];
                GridRow.Tag = row;
            }

            _newIndex = this.dataGridViewList.Rows.Add();

            DataGridViewRow newGridRow = this.dataGridViewList.Rows[_newIndex];
            MonsterDataEntity.mt_monster_actionRow newRows = _table.Newmt_monster_actionRow();
            newRows.monster_id = _monsterID;
            newRows.action_no = -1;
            newRows.timing1 = 1;
            newRows.timing2 = 0;
            newRows.timing3 = 0;
            newRows.action_target = 0;
            newRows.action = 0;
            newRows.probability = 4;
            newRows.max_count = 0;
            newRows.perks_id = 0;
            newGridRow.Tag = newRows;
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
        /// 編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            DialogView();
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            DialogView();
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewList.SelectedRows.Count > 0)
            {
                this.dataGridViewList.Rows.RemoveAt(this.dataGridViewList.SelectedRows[0].Index);
            }
        }

        /// <summary>
        /// ダイアログ表示
        /// </summary>
        private void DialogView()
        {
            if (this.dataGridViewList.SelectedRows.Count > 0)
            {
                bool isAdd = this.dataGridViewList.SelectedRows[0].Index == _newIndex;

                MonsterActionEditDialog dialog = new MonsterActionEditDialog(_monsterID, (MonsterDataEntity.mt_monster_actionRow)this.dataGridViewList.SelectedRows[0].Tag);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    MonsterDataEntity.mt_monster_actionRow Row = dialog.Row;
                    this.dataGridViewList.SelectedRows[0].Cells[0].Value = Row.timing1;
                    this.dataGridViewList.SelectedRows[0].Cells[1].Value = Row.timing2;
                    this.dataGridViewList.SelectedRows[0].Cells[2].Value = Row.timing3;
                    this.dataGridViewList.SelectedRows[0].Cells[3].Value = Row.action_target;
                    this.dataGridViewList.SelectedRows[0].Cells[4].Value = Row.action;
                    this.dataGridViewList.SelectedRows[0].Cells[5].Value = Row.probability;
                    this.dataGridViewList.SelectedRows[0].Cells[6].Value = Row.max_count;
                    this.dataGridViewList.SelectedRows[0].Cells[7].Value = Row.perks_id;
                    this.dataGridViewList.SelectedRows[0].Tag = dialog.Row;

                    if (isAdd)
                    {
                        _newIndex = this.dataGridViewList.Rows.Add();

                        DataGridViewRow newGridRow = this.dataGridViewList.Rows[_newIndex];
                        MonsterDataEntity.mt_monster_actionRow newRows = _table.Newmt_monster_actionRow();
                        newRows.monster_id = _monsterID;
                        newRows.action_no = -1;
                        newRows.timing1 = 1;
                        newRows.timing2 = 0;
                        newRows.timing3 = 0;
                        newRows.action_target = 0;
                        newRows.action = 0;
                        newRows.probability = 4;
                        newRows.max_count = 0;
                        newRows.perks_id = 0;
                        newGridRow.Tag = newRows;
                    }
                }
            }
        }

        /// <summary>
        /// 表示書式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        {
                            // 条件1
                            e.Value = LibAction.GetTimingName((int)e.Value);
                        }
                        break;
                    case 1:
                        {
                            // 条件2
                            e.Value = LibAction.GetTimingName((int)e.Value);
                        }
                        break;
                    case 2:
                        {
                            // 条件3
                            e.Value = LibAction.GetTimingName((int)e.Value);
                        }
                        break;
                    case 3:
                        {
                            // 対象
                            e.Value = LibAction.GetActionTargetName((int)e.Value);
                        }
                        break;
                    case 4:
                        {
                            // アクション
                            string ActionName = LibAction.GetActionName((int)e.Value);
                            if ((int)e.Value == 2)
                            {
                                MonsterDataEntity.mt_monster_actionRow Row = (MonsterDataEntity.mt_monster_actionRow)this.dataGridViewList.Rows[e.RowIndex].Tag;
                                e.Value ="「" + LibSkill.GetSkillName(Row.perks_id) + "」を発動";
                            }
                            else
                            {
                                e.Value = ActionName;
                            }
                        }
                        break;
                    case 5:
                        {
                            // 確率
                            e.Value = LibInteger.ChangeProbComboStr((int)e.Value);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// UP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewList.SelectedRows.Count > 0)
            {
                DataGridViewRow row = (DataGridViewRow)this.dataGridViewList.SelectedRows[0];
                int rowIndex = this.dataGridViewList.SelectedRows[0].Index;

                this.dataGridViewList.Rows.Insert(rowIndex - 1, 1);

                for (int j = 0; j < this.dataGridViewList.Columns.Count; j++)
                {
                    this.dataGridViewList.Rows[rowIndex - 1].Cells[j].Value = row.Cells[j].Value;
                }

                this.dataGridViewList.Rows[rowIndex - 1].Tag = row.Tag;

                this.dataGridViewList.Rows.Remove(row);
                this.dataGridViewList.Rows[rowIndex - 1].Selected = true;

                dataGridViewList_CellClick(this, null);
            }
        }

        /// <summary>
        /// DOWN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewList.SelectedRows.Count > 0)
            {
                DataGridViewRow row = (DataGridViewRow)this.dataGridViewList.SelectedRows[0];
                int rowIndex = this.dataGridViewList.SelectedRows[0].Index;

                this.dataGridViewList.Rows.Insert(rowIndex + 2, 1);

                for (int j = 0; j < this.dataGridViewList.Columns.Count; j++)
                {
                    this.dataGridViewList.Rows[rowIndex + 2].Cells[j].Value = row.Cells[j].Value;
                }

                this.dataGridViewList.Rows[rowIndex + 2].Tag = row.Tag;

                this.dataGridViewList.Rows.Remove(row);
                this.dataGridViewList.Rows[rowIndex + 1].Selected = true;

                dataGridViewList_CellClick(this, null);
            }
        }

        /// <summary>
        /// ボタン制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewList.SelectedRows.Count > 0)
            {
                int rowIndex = this.dataGridViewList.SelectedRows[0].Index;
                this.buttonUp.Enabled = true;
                this.buttonDown.Enabled = true;

                if (rowIndex == 0)
                {
                    this.buttonUp.Enabled = false;
                }

                if (rowIndex == (this.dataGridViewList.Rows.Count - 1))
                {
                    this.buttonDown.Enabled = false;
                }
            }
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // DELETE
            foreach (DataRowView viewRow in _actionView)
            {
                viewRow.Delete();
            }

            // INSERT
            int actionNo = 1;
            foreach (DataGridViewRow row in this.dataGridViewList.Rows)
            {
                if (row.Cells[0].Value == null)
                {
                    continue;
                }

                MonsterDataEntity.mt_monster_actionRow newRow = _table.Newmt_monster_actionRow();

                newRow.monster_id = _monsterID;
                newRow.action_no = actionNo;
                newRow.timing1 = (int)row.Cells[0].Value;
                newRow.timing2 = (int)row.Cells[1].Value;
                newRow.timing3 = (int)row.Cells[2].Value;
                newRow.action_target = (int)row.Cells[3].Value;
                newRow.action = (int)row.Cells[4].Value;
                newRow.probability = (int)row.Cells[5].Value;
                newRow.max_count = (int)row.Cells[6].Value;
                newRow.perks_id = (int)row.Cells[7].Value;

                _table.Addmt_monster_actionRow(newRow);

                actionNo++;
            }

            this.Close();
        }
    }
}

