namespace CommonFormLibrary
{
    partial class ListBaseForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.columnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.contextMenuStripList.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelList
            // 
            this.panelList.Controls.Add(this.dataGridViewList);
            this.panelList.Controls.SetChildIndex(this.dataGridViewList, 0);
            // 
            // dataGridViewList
            // 
            this.dataGridViewList.AllowUserToAddRows = false;
            this.dataGridViewList.AllowUserToDeleteRows = false;
            this.dataGridViewList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            this.dataGridViewList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.dataGridViewList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnNo,
            this.columnName});
            this.dataGridViewList.Location = new System.Drawing.Point(10, 37);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            this.dataGridViewList.RowHeadersVisible = false;
            this.dataGridViewList.RowTemplate.Height = 21;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(175, 564);
            this.dataGridViewList.TabIndex = 3;
            this.dataGridViewList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewList_CellMouseClick);
            this.dataGridViewList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewList_CellFormatting);
            this.dataGridViewList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewList_MouseUp);
            this.dataGridViewList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellClick);
            this.dataGridViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewList_KeyDown);
            // 
            // columnNo
            // 
            this.columnNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Format = "00000:";
            dataGridViewCellStyle2.NullValue = null;
            this.columnNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.columnNo.HeaderText = "No";
            this.columnNo.Name = "columnNo";
            this.columnNo.ReadOnly = true;
            this.columnNo.Width = 50;
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnName.HeaderText = "����";
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            // 
            // contextMenuStripList
            // 
            this.contextMenuStripList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAdd,
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemDelete,
            this.toolStripSeparator1,
            this.toolStripMenuItemFilter});
            this.contextMenuStripList.Name = "contextMenuStripList";
            this.contextMenuStripList.Size = new System.Drawing.Size(153, 120);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItemFilter
            // 
            this.toolStripMenuItemFilter.Name = "toolStripMenuItemFilter";
            this.toolStripMenuItemFilter.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemFilter.Text = "�t�B���^(&F)";
            this.toolStripMenuItemFilter.Click += new System.EventHandler(this.toolStripMenuItemFilter_Click);
            // 
            // ListBaseForm
            // 
            this.ClientSize = new System.Drawing.Size(851, 640);
            this.Name = "ListBaseForm";
            this.Controls.SetChildIndex(this.panelList, 0);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.contextMenuStripList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.DataGridView dataGridViewList;
        protected System.Windows.Forms.ContextMenuStrip contextMenuStripList;
        protected System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        protected System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilter;
        protected System.Windows.Forms.DataGridViewTextBoxColumn columnNo;
        protected System.Windows.Forms.DataGridViewTextBoxColumn columnName;
    }
}
