namespace CommonFormLibrary
{
    partial class TreeBaseForm
    {
        /// <summary>
        /// �K�v�ȃf�U�C�i�ϐ��ł��B
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// �g�p���̃��\�[�X�����ׂăN���[���A�b�v���܂��B
        /// </summary>
        /// <param name="disposing">�}�l�[�W ���\�[�X���j�������ꍇ true�A�j������Ȃ��ꍇ�� false �ł��B</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h

        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
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
            this.treeViewList.TabIndex = 0;
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
            this.toolStripMenuItemAdd.Text = "�ǉ�(&A)";
            this.toolStripMenuItemAdd.Click += new System.EventHandler(this.toolStripMenuItemAdd_Click);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Enabled = false;
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemCopy.Text = "����(&C)";
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Enabled = false;
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemDelete.Text = "�폜(&D)";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // TreeBaseForm
            // 
            this.ClientSize = new System.Drawing.Size(851, 640);
            this.Name = "TreeBaseForm";
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
