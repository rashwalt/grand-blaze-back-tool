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
        /// フィルタはない
        /// </summary>
        public void NothingFilter()
        {
            this.toolStripSeparator1.Visible = false;
            this.toolStripMenuItemFilter.Visible = false;
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
                this.contextMenuStripList.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
        }

        /// <summary>
        /// ショートカットキーイベント
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

