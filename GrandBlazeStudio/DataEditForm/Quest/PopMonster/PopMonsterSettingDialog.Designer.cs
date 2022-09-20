namespace DataEditForm.Quest.PopMonster
{
    partial class PopMonsterSettingDialog
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
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.columnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnRace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnWeather = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnRare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(323, 7);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(415, 7);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dataGridViewList);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Size = new System.Drawing.Size(507, 227);
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
            this.columnNo,
            this.columnName,
            this.columnRace,
            this.columnLevel,
            this.columnWeather,
            this.columnProb,
            this.columnRare});
            this.dataGridViewList.Location = new System.Drawing.Point(5, 24);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            this.dataGridViewList.RowHeadersVisible = false;
            this.dataGridViewList.RowTemplate.Height = 21;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(496, 196);
            this.dataGridViewList.TabIndex = 3;
            this.dataGridViewList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellDoubleClick);
            this.dataGridViewList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewList_CellFormatting);
            this.dataGridViewList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewList_CellMouseClick);
            this.dataGridViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewList_KeyDown);
            this.dataGridViewList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewList_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "出現モンスター:";
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
            // columnNo
            // 
            this.columnNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnNo.Frozen = true;
            this.columnNo.HeaderText = "No";
            this.columnNo.Name = "columnNo";
            this.columnNo.ReadOnly = true;
            this.columnNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnNo.Width = 35;
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnName.Frozen = true;
            this.columnName.HeaderText = "名称";
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            this.columnName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnName.Width = 102;
            // 
            // columnRace
            // 
            this.columnRace.HeaderText = "種族";
            this.columnRace.Name = "columnRace";
            this.columnRace.ReadOnly = true;
            this.columnRace.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // columnLevel
            // 
            this.columnLevel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnLevel.HeaderText = "Lv";
            this.columnLevel.Name = "columnLevel";
            this.columnLevel.ReadOnly = true;
            this.columnLevel.Width = 60;
            // 
            // columnWeather
            // 
            this.columnWeather.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnWeather.FillWeight = 70F;
            this.columnWeather.HeaderText = "天候";
            this.columnWeather.Name = "columnWeather";
            this.columnWeather.ReadOnly = true;
            // 
            // columnProb
            // 
            this.columnProb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnProb.FillWeight = 70F;
            this.columnProb.HeaderText = "出現率";
            this.columnProb.Name = "columnProb";
            this.columnProb.ReadOnly = true;
            this.columnProb.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnProb.Width = 70;
            // 
            // columnRare
            // 
            this.columnRare.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnRare.HeaderText = "出現数";
            this.columnRare.Name = "columnRare";
            this.columnRare.ReadOnly = true;
            this.columnRare.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnRare.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnRare.Width = 60;
            // 
            // PopMonsterSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(507, 260);
            this.Name = "PopMonsterSettingDialog";
            this.Text = "ポップモンスター設定";
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRace;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnWeather;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProb;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRare;
    }
}
