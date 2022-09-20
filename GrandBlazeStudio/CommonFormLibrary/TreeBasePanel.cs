using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonFormLibrary
{
    public partial class TreeBasePanel : CommonFormLibrary.BasePanel
    {
        public TreeBasePanel()
        {
            InitializeComponent();
        }

        private bool isMouseDown = false;

        private void treeViewList_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = false;

            if (e.Button == MouseButtons.Right)
            {
                this.treeViewList.SelectedNode = this.treeViewList.GetNodeAt(e.X, e.Y);
                isMouseDown = true;

                return;
            }
        }

        private void treeViewList_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseDown && e.Button == MouseButtons.Right)
            {
                // コンテキスト表示
                this.contextMenuStripList.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
        }

        private void treeViewList_KeyDown(object sender, KeyEventArgs e)
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

        protected virtual void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
        }

        protected virtual void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
        }

        protected virtual void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
        }

        protected virtual void contextMenuStripList_Opening(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        /// 選択イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void treeViewList_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }
    }
}
