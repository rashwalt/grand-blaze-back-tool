namespace DataEditForm.Guest.Details
{
    partial class GuestSerifDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStripEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.columnSituation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSerif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSkillID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSkillCreated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.contextMenuStripEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(522, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(614, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dataGridViewList);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Size = new System.Drawing.Size(712, 439);
            // 
            // dataGridViewList
            // 
            this.dataGridViewList.AllowUserToAddRows = false;
            this.dataGridViewList.AllowUserToDeleteRows = false;
            this.dataGridViewList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            this.dataGridViewList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSituation,
            this.columnSerif,
            this.columnSkillID,
            this.columnSkillCreated});
            this.dataGridViewList.Location = new System.Drawing.Point(14, 24);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            this.dataGridViewList.RowHeadersVisible = false;
            this.dataGridViewList.RowTemplate.Height = 21;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(686, 409);
            this.dataGridViewList.TabIndex = 6;
            this.dataGridViewList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellDoubleClick);
            this.dataGridViewList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewList_CellFormatting);
            this.dataGridViewList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewList_CellMouseClick);
            this.dataGridViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewList_KeyDown);
            this.dataGridViewList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewList_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "セリフ一覧:";
            // 
            // contextMenuStripEdit
            // 
            this.contextMenuStripEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEdit,
            this.toolStripMenuItemAdd,
            this.toolStripMenuItemDelete});
            this.contextMenuStripEdit.Name = "contextMenuStripEdit";
            this.contextMenuStripEdit.Size = new System.Drawing.Size(133, 70);
            // 
            // toolStripMenuItemEdit
            // 
            this.toolStripMenuItemEdit.Enabled = false;
            this.toolStripMenuItemEdit.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
            this.toolStripMenuItemEdit.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItemEdit.Text = "編集(&E)";
            this.toolStripMenuItemEdit.Click += new System.EventHandler(this.toolStripMenuItemEdit_Click);
            // 
            // toolStripMenuItemAdd
            // 
            this.toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
            this.toolStripMenuItemAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.toolStripMenuItemAdd.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItemAdd.Text = "追加(&A)";
            this.toolStripMenuItemAdd.Click += new System.EventHandler(this.toolStripMenuItemAdd_Click);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItemDelete.Text = "削除(&D)";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // columnSituation
            // 
            this.columnSituation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnSituation.HeaderText = "シチュエーション";
            this.columnSituation.Name = "columnSituation";
            this.columnSituation.ReadOnly = true;
            this.columnSituation.Width = 260;
            // 
            // columnSerif
            // 
            this.columnSerif.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnSerif.DefaultCellStyle = dataGridViewCellStyle2;
            this.columnSerif.HeaderText = "セリフ";
            this.columnSerif.Name = "columnSerif";
            this.columnSerif.ReadOnly = true;
            // 
            // columnSkillID
            // 
            this.columnSkillID.HeaderText = "スキルID";
            this.columnSkillID.Name = "columnSkillID";
            this.columnSkillID.ReadOnly = true;
            this.columnSkillID.Visible = false;
            // 
            // columnSkillCreated
            // 
            this.columnSkillCreated.HeaderText = "合成フラグ";
            this.columnSkillCreated.Name = "columnSkillCreated";
            this.columnSkillCreated.ReadOnly = true;
            this.columnSkillCreated.Visible = false;
            // 
            // GuestSerifDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 472);
            this.Name = "GuestSerifDialog";
            this.Text = "ゲストセリフ";
            this.Load += new System.EventHandler(this.GuestSerifDialog_Load);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.contextMenuStripEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSituation;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSerif;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSkillID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnSkillCreated;
    }
}