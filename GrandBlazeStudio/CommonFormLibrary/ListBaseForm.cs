using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonFormLibrary
{
    public partial class ListBaseForm : BaseForm
    {
        public ListBaseForm()
        {
            InitializeComponent();
        }

        protected virtual void dataGridViewList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // �w�b�_�ȊO�̃Z�����H
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    // �E�N���b�N���ꂽ�Z��
                    DataGridViewCell cell = this.dataGridViewList[e.ColumnIndex, e.RowIndex];
                    // �Z���̑I��
                    cell.Selected = true;
                }
            }
        }

        protected virtual void dataGridViewList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        protected virtual void contextMenuStripList_Opening(object sender, CancelEventArgs e)
        {
        }

        protected virtual void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
        }

        protected virtual void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
        }

        protected virtual void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
        }

        protected virtual void toolStripMenuItemFilter_Click(object sender, EventArgs e)
        {
        }

        protected virtual void dataGridViewList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }

        /// <summary>
        /// �t�B���^�͂Ȃ�
        /// </summary>
        public void NothingFilter()
        {
            this.toolStripSeparator1.Visible = false;
            this.toolStripMenuItemFilter.Visible = false;
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
                this.contextMenuStripList.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
        }

        /// <summary>
        /// �V���[�g�J�b�g�L�[�C�x���g
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

    }
}

