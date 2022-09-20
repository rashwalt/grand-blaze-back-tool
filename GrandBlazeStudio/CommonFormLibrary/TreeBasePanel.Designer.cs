namespace CommonFormLibrary
{
    partial class TreeBasePanel
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeViewList = new System.Windows.Forms.TreeView();
            this.contextMenuStripList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panelList.SuspendLayout();
            this.contextMenuStripList.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelList
            // 
            this.panelList.Controls.Add(this.treeViewList);
            this.panelList.Controls.SetChildIndex(this.treeViewList, 0);
            // 
            // treeViewList
            // 
            this.treeViewList.FullRowSelect = true;
            this.treeViewList.HideSelection = false;
            this.treeViewList.Location = new System.Drawing.Point(10, 37);
            this.treeViewList.Name = "treeViewList";
            this.treeViewList.Size = new System.Drawing.Size(175, 564);
            this.treeViewList.TabIndex = 8;
            this.treeViewList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeViewList_MouseUp);
            this.treeViewList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewList_AfterSelect);
            this.treeViewList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewList_MouseDown);
            this.treeViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeViewList_KeyDown);
            // 
            // contextMenuStripList
            // 
            this.contextMenuStripList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAdd,
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemDelete});
            this.contextMenuStripList.Name = "contextMenuStripList";
            this.contextMenuStripList.Size = new System.Drawing.Size(153, 92);
            this.contextMenuStripList.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripList_Opening);
            // 
            // toolStripMenuItemAdd
            // 
            this.toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
            this.toolStripMenuItemAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.toolStripMenuItemAdd.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemAdd.Text = "追加(&A)";
            this.toolStripMenuItemAdd.Click += new System.EventHandler(this.toolStripMenuItemAdd_Click);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Enabled = false;
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemCopy.Text = "複製(&C)";
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Enabled = false;
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemDelete.Text = "削除(&D)";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // TreeBasePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "TreeBasePanel";
            this.Controls.SetChildIndex(this.panelList, 0);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelList.ResumeLayout(false);
            this.contextMenuStripList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TreeView treeViewList;
        protected System.Windows.Forms.ContextMenuStrip contextMenuStripList;
        protected System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdd;
        protected System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        protected System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
    }
}
