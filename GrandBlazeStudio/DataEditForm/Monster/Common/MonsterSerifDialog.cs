using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;
using DataEditForm.Monster.Common.SubDialog;

namespace DataEditForm.Monster.Common
{
    public partial class MonsterSerifDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public MonsterSerifDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="MonsterID">�����X�^�[ID</param>
        /// <param name="SerifTable">���f��e�[�u��</param>
        public MonsterSerifDialog(int MonsterID, MonsterDataEntity.mt_monster_serifDataTable SerifTable)
        {
            InitializeComponent();

            _monsterID = MonsterID;

            _table = SerifTable;
            _serifView = new DataView(_table);
            _serifView.RowFilter = SerifTable.monster_idColumn.ColumnName + "=" + _monsterID;
            _serifView.Sort = SerifTable.situationColumn.ColumnName + "," + SerifTable.serif_noColumn.ColumnName;
        }

        private int _monsterID = 0;
        private MonsterDataEntity.mt_monster_serifDataTable _table;
        private DataView _serifView;

        /// <summary>
        /// ��ʕ\��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonsterSerifDialog_Load(object sender, EventArgs e)
        {
            this.dataGridViewList.Rows.Clear();

            foreach (DataRowView rowView in _serifView)
            {
                MonsterDataEntity.mt_monster_serifRow row = (MonsterDataEntity.mt_monster_serifRow)rowView.Row;

                this.dataGridViewList.Rows.Add(
                    row.situation,
                    row.serif_text,
                    row.perks_id
                    );
            }
        }

        /// <summary>
        /// �L�[����
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
        /// �I���C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewList.SelectedCells.Count == 0)
            {
                toolStripMenuItemAdd_Click(this, EventArgs.Empty);
            }
            else
            {
                toolStripMenuItemEdit_Click(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// �Z���E�N���b�N
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.Button == MouseButtons.Right)
            {
                DataGridViewCell cell = this.dataGridViewList[e.ColumnIndex, e.RowIndex];
                // �Z���̑I����Ԃ𔽓]
                cell.Selected = true;
            }
        }

        /// <summary>
        /// �R���e�L�X�g���j���[�\��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // �R���e�L�X�g�\��
                this.contextMenuStripEdit.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
        }

        /// <summary>
        /// �\���`���̕ϊ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    // �V�`���G�[�V����
                    if (e.Value != null)
                    {
                        int SerifID = (int)e.Value;
                        e.Value = LibSituation.GetName(SerifID);

                        if ((int)this.dataGridViewList[2, e.RowIndex].Value > 0)
                        {
                            e.Value += "�y" + LibSkill.GetSkillName((int)this.dataGridViewList[2, e.RowIndex].Value) + "�z";
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// �ҏW�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewList.SelectedCells.Count == 0) { return; }

            int rowIndex = this.dataGridViewList.SelectedCells[0].RowIndex;

            SerifSettingDialog dialog = new SerifSettingDialog();

            int situationID = 0;
            if (this.dataGridViewList[0, rowIndex].Value != null)
            {
                situationID = (int)this.dataGridViewList[0, rowIndex].Value;
            }

            string serifText = "";
            if (this.dataGridViewList[1, rowIndex].Value != null)
            {
                serifText = this.dataGridViewList[1, rowIndex].Value.ToString();
            }

            int skillID = 0;
            if (this.dataGridViewList[2, rowIndex].Value != null)
            {
                skillID = (int)this.dataGridViewList[2, rowIndex].Value;
            }

            dialog.SetData(situationID, serifText, skillID);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.dataGridViewList[0, rowIndex].Value = dialog.SituationID;
                this.dataGridViewList[1, rowIndex].Value = dialog.SerifText;
                this.dataGridViewList[2, rowIndex].Value = dialog.SkillID;
            }
        }

        /// <summary>
        /// �ǉ��C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            SerifSettingDialog dialog = new SerifSettingDialog();

            dialog.SetData(0, "", 0);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.dataGridViewList.Rows.Add(dialog.SituationID, dialog.SerifText, dialog.SkillID);
            }
        }

        /// <summary>
        /// �폜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewList.SelectedCells.Count == 0) { return; }

            // �s�̍폜
            int rowIndex = this.dataGridViewList.SelectedCells[0].RowIndex;

            this.dataGridViewList.Rows.RemoveAt(rowIndex);
        }

        /// <summary>
        /// �ҏW�\���m�F
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripEdit_Opening(object sender, CancelEventArgs e)
        {
            if (this.dataGridViewList.SelectedCells.Count > 0)
            {
                this.toolStripMenuItemEdit.Enabled = true;
            }
        }

        /// <summary>
        /// �L�����Z��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // DELETE
            foreach (DataRowView viewRow in _serifView)
            {
                viewRow.Delete();
            }

            // INSERT
            int setNo = 1;
            foreach (DataGridViewRow row in this.dataGridViewList.Rows)
            {
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                {
                    continue;
                }

                MonsterDataEntity.mt_monster_serifRow newRow = _table.Newmt_monster_serifRow();

                newRow.monster_id = _monsterID;
                newRow.serif_no = setNo;
                newRow.situation = (int)row.Cells[0].Value;
                newRow.serif_text = row.Cells[1].Value.ToString();
                newRow.perks_id = (int)row.Cells[2].Value;

                _table.Addmt_monster_serifRow(newRow);

                setNo++;
            }

            this.Close();
        }
    }
}

