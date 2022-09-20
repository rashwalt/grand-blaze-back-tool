namespace DataEditForm.Monster.Common
{
    partial class MonsterActionDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.columnTiming1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTiming2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTiming3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnMaxCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSkillID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(688, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(780, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonUp);
            this.panelMain.Controls.Add(this.buttonDown);
            this.panelMain.Controls.Add(this.dataGridViewList);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Size = new System.Drawing.Size(878, 257);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "アクションリスト:";
            // 
            // dataGridViewList
            // 
            this.dataGridViewList.AllowUserToAddRows = false;
            this.dataGridViewList.AllowUserToDeleteRows = false;
            this.dataGridViewList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            this.dataGridViewList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnTiming1,
            this.columnTiming2,
            this.columnTiming3,
            this.columnTarget,
            this.columnAction,
            this.columnProb,
            this.columnMaxCount,
            this.columnSkillID});
            this.dataGridViewList.Location = new System.Drawing.Point(14, 24);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            this.dataGridViewList.RowHeadersVisible = false;
            this.dataGridViewList.RowTemplate.Height = 21;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(852, 196);
            this.dataGridViewList.TabIndex = 1;
            this.dataGridViewList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellClick);
            this.dataGridViewList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellDoubleClick);
            this.dataGridViewList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewList_CellFormatting);
            this.dataGridViewList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewList_CellMouseClick);
            this.dataGridViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewList_KeyDown);
            this.dataGridViewList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewList_MouseUp);
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(810, 226);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(25, 23);
            this.buttonUp.TabIndex = 14;
            this.buttonUp.Text = "▲";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Location = new System.Drawing.Point(841, 226);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(25, 23);
            this.buttonDown.TabIndex = 13;
            this.buttonDown.Text = "▼";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEdit,
            this.toolStripMenuItemAdd,
            this.toolStripMenuItemDelete});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(153, 92);
            // 
            // toolStripMenuItemEdit
            // 
            this.toolStripMenuItemEdit.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
            this.toolStripMenuItemEdit.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemEdit.Text = "編集(&E)";
            this.toolStripMenuItemEdit.Click += new System.EventHandler(this.toolStripMenuItemEdit_Click);
            // 
            // toolStripMenuItemAdd
            // 
            this.toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
            this.toolStripMenuItemAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.toolStripMenuItemAdd.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemAdd.Text = "追加(&A)";
            this.toolStripMenuItemAdd.Click += new System.EventHandler(this.toolStripMenuItemAdd_Click);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemDelete.Text = "削除(&D)";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // columnTiming1
            // 
            this.columnTiming1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnTiming1.FillWeight = 130F;
            this.columnTiming1.HeaderText = "条件1";
            this.columnTiming1.Name = "columnTiming1";
            this.columnTiming1.ReadOnly = true;
            this.columnTiming1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnTiming1.Width = 130;
            // 
            // columnTiming2
            // 
            this.columnTiming2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnTiming2.FillWeight = 130F;
            this.columnTiming2.HeaderText = "条件2";
            this.columnTiming2.Name = "columnTiming2";
            this.columnTiming2.ReadOnly = true;
            this.columnTiming2.Width = 130;
            // 
            // columnTiming3
            // 
            this.columnTiming3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnTiming3.FillWeight = 130F;
            this.columnTiming3.HeaderText = "条件3";
            this.columnTiming3.Name = "columnTiming3";
            this.columnTiming3.ReadOnly = true;
            this.columnTiming3.Width = 130;
            // 
            // columnTarget
            // 
            this.columnTarget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnTarget.HeaderText = "対象";
            this.columnTarget.Name = "columnTarget";
            this.columnTarget.ReadOnly = true;
            // 
            // columnAction
            // 
            this.columnAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnAction.FillWeight = 75.7874F;
            this.columnAction.HeaderText = "行動";
            this.columnAction.Name = "columnAction";
            this.columnAction.ReadOnly = true;
            this.columnAction.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // columnProb
            // 
            this.columnProb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnProb.FillWeight = 70F;
            this.columnProb.HeaderText = "実行率";
            this.columnProb.Name = "columnProb";
            this.columnProb.ReadOnly = true;
            this.columnProb.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnProb.Width = 70;
            // 
            // columnMaxCount
            // 
            this.columnMaxCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnMaxCount.FillWeight = 60F;
            this.columnMaxCount.HeaderText = "回数";
            this.columnMaxCount.Name = "columnMaxCount";
            this.columnMaxCount.ReadOnly = true;
            this.columnMaxCount.Width = 60;
            // 
            // columnSkillID
            // 
            this.columnSkillID.HeaderText = "スキルID";
            this.columnSkillID.Name = "columnSkillID";
            this.columnSkillID.ReadOnly = true;
            this.columnSkillID.Visible = false;
            // 
            // MonsterActionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(878, 290);
            this.Name = "MonsterActionDialog";
            this.Text = "モンスター行動設定";
            this.Load += new System.EventHandler(this.MonsterActionDialog_Load);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTiming1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTiming2;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTiming3;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProb;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMaxCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSkillID;
    }
}
