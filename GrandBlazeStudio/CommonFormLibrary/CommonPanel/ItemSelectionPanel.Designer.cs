namespace CommonFormLibrary.CommonPanel
{
    partial class ItemSelectionPanel
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

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewItemBox = new System.Windows.Forms.DataGridView();
            this.columnProbability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItemBox)).BeginInit();
            this.contextMenuStripEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewItemBox
            // 
            this.dataGridViewItemBox.AllowUserToAddRows = false;
            this.dataGridViewItemBox.AllowUserToDeleteRows = false;
            this.dataGridViewItemBox.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            this.dataGridViewItemBox.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewItemBox.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewItemBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewItemBox.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewItemBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItemBox.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProbability,
            this.columnItemName,
            this.columnCount});
            this.dataGridViewItemBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewItemBox.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewItemBox.MultiSelect = false;
            this.dataGridViewItemBox.Name = "dataGridViewItemBox";
            this.dataGridViewItemBox.ReadOnly = true;
            this.dataGridViewItemBox.RowHeadersVisible = false;
            this.dataGridViewItemBox.RowTemplate.Height = 21;
            this.dataGridViewItemBox.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewItemBox.Size = new System.Drawing.Size(605, 556);
            this.dataGridViewItemBox.TabIndex = 1;
            this.dataGridViewItemBox.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewItemBox_CellMouseClick);
            this.dataGridViewItemBox.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItemBox_CellDoubleClick);
            this.dataGridViewItemBox.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewItemBox_CellFormatting);
            this.dataGridViewItemBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewItemBox_MouseUp);
            this.dataGridViewItemBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewItemBox_KeyDown);
            // 
            // columnProbability
            // 
            this.columnProbability.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.NullValue = null;
            this.columnProbability.DefaultCellStyle = dataGridViewCellStyle2;
            this.columnProbability.HeaderText = "確率";
            this.columnProbability.Name = "columnProbability";
            this.columnProbability.ReadOnly = true;
            this.columnProbability.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProbability.Width = 60;
            // 
            // columnItemName
            // 
            this.columnItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnItemName.HeaderText = "アイテム名";
            this.columnItemName.Name = "columnItemName";
            this.columnItemName.ReadOnly = true;
            // 
            // columnCount
            // 
            this.columnCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnCount.HeaderText = "個数/金額";
            this.columnCount.Name = "columnCount";
            this.columnCount.ReadOnly = true;
            this.columnCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // contextMenuStripEdit
            // 
            this.contextMenuStripEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemItemEdit,
            this.toolStripMenuItemAdd,
            this.toolStripMenuItemItemDelete});
            this.contextMenuStripEdit.Name = "contextMenuStripEdit";
            this.contextMenuStripEdit.Size = new System.Drawing.Size(133, 70);
            this.contextMenuStripEdit.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripEdit_Opening);
            // 
            // toolStripMenuItemItemEdit
            // 
            this.toolStripMenuItemItemEdit.Enabled = false;
            this.toolStripMenuItemItemEdit.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripMenuItemItemEdit.Name = "toolStripMenuItemItemEdit";
            this.toolStripMenuItemItemEdit.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItemItemEdit.Text = "編集(&E)";
            this.toolStripMenuItemItemEdit.Click += new System.EventHandler(this.toolStripMenuItemItemEdit_Click);
            // 
            // toolStripMenuItemAdd
            // 
            this.toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
            this.toolStripMenuItemAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.toolStripMenuItemAdd.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItemAdd.Text = "追加(&A)";
            this.toolStripMenuItemAdd.Click += new System.EventHandler(this.toolStripMenuItemAdd_Click);
            // 
            // toolStripMenuItemItemDelete
            // 
            this.toolStripMenuItemItemDelete.Name = "toolStripMenuItemItemDelete";
            this.toolStripMenuItemItemDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItemItemDelete.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItemItemDelete.Text = "削除(&D)";
            this.toolStripMenuItemItemDelete.Click += new System.EventHandler(this.toolStripMenuItemItemDelete_Click);
            // 
            // ItemSelectionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewItemBox);
            this.Name = "ItemSelectionPanel";
            this.Size = new System.Drawing.Size(605, 556);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItemBox)).EndInit();
            this.contextMenuStripEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewItemBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemItemEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemItemDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProbability;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCount;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdd;
    }
}
