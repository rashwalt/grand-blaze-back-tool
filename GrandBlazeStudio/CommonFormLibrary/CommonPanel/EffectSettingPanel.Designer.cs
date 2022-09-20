namespace CommonFormLibrary.CommonPanel
{
    partial class EffectSettingPanel
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
            this.dataGridViewEffect = new System.Windows.Forms.DataGridView();
            this.contextMenuStripEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.columnEffectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSubRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnEndLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnHide = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEffect)).BeginInit();
            this.contextMenuStripEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewEffect
            // 
            this.dataGridViewEffect.AllowUserToAddRows = false;
            this.dataGridViewEffect.AllowUserToDeleteRows = false;
            this.dataGridViewEffect.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            this.dataGridViewEffect.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewEffect.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEffect.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewEffect.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewEffect.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewEffect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEffect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnEffectName,
            this.columnRank,
            this.columnSubRank,
            this.columnProb,
            this.columnEndLimit,
            this.columnHide});
            this.dataGridViewEffect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEffect.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewEffect.MultiSelect = false;
            this.dataGridViewEffect.Name = "dataGridViewEffect";
            this.dataGridViewEffect.ReadOnly = true;
            this.dataGridViewEffect.RowHeadersVisible = false;
            this.dataGridViewEffect.RowTemplate.Height = 21;
            this.dataGridViewEffect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEffect.Size = new System.Drawing.Size(394, 131);
            this.dataGridViewEffect.TabIndex = 16;
            this.dataGridViewEffect.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEffect_CellMouseClick);
            this.dataGridViewEffect.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEffect_CellDoubleClick);
            this.dataGridViewEffect.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewEffect_MouseUp);
            this.dataGridViewEffect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewEffect_KeyDown);
            // 
            // contextMenuStripEdit
            // 
            this.contextMenuStripEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemItemEdit,
            this.toolStripMenuItemAdd,
            this.toolStripMenuItemItemDelete});
            this.contextMenuStripEdit.Name = "contextMenuStripEdit";
            this.contextMenuStripEdit.Size = new System.Drawing.Size(186, 70);
            this.contextMenuStripEdit.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripEdit_Opening);
            // 
            // toolStripMenuItemItemEdit
            // 
            this.toolStripMenuItemItemEdit.Enabled = false;
            this.toolStripMenuItemItemEdit.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripMenuItemItemEdit.Name = "toolStripMenuItemItemEdit";
            this.toolStripMenuItemItemEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.toolStripMenuItemItemEdit.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemItemEdit.Text = "編集(&E)";
            this.toolStripMenuItemItemEdit.Click += new System.EventHandler(this.toolStripMenuItemItemEdit_Click);
            // 
            // toolStripMenuItemAdd
            // 
            this.toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
            this.toolStripMenuItemAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.toolStripMenuItemAdd.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemAdd.Text = "追加(&A)";
            this.toolStripMenuItemAdd.Click += new System.EventHandler(this.toolStripMenuItemItemAdd_Click);
            // 
            // toolStripMenuItemItemDelete
            // 
            this.toolStripMenuItemItemDelete.Name = "toolStripMenuItemItemDelete";
            this.toolStripMenuItemItemDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItemItemDelete.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemItemDelete.Text = "削除(&D)";
            this.toolStripMenuItemItemDelete.Click += new System.EventHandler(this.toolStripMenuItemItemDelete_Click);
            // 
            // columnEffectName
            // 
            this.columnEffectName.HeaderText = "エフェクト名";
            this.columnEffectName.Name = "columnEffectName";
            this.columnEffectName.ReadOnly = true;
            // 
            // columnRank
            // 
            this.columnRank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnRank.HeaderText = "ランク";
            this.columnRank.Name = "columnRank";
            this.columnRank.ReadOnly = true;
            this.columnRank.Width = 55;
            // 
            // columnSubRank
            // 
            this.columnSubRank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnSubRank.HeaderText = "ｻﾌﾞﾗﾝｸ";
            this.columnSubRank.Name = "columnSubRank";
            this.columnSubRank.ReadOnly = true;
            this.columnSubRank.Width = 65;
            // 
            // columnProb
            // 
            this.columnProb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnProb.HeaderText = "確率";
            this.columnProb.Name = "columnProb";
            this.columnProb.ReadOnly = true;
            this.columnProb.Width = 55;
            // 
            // columnEndLimit
            // 
            this.columnEndLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnEndLimit.HeaderText = "ｶｳﾝﾄ";
            this.columnEndLimit.Name = "columnEndLimit";
            this.columnEndLimit.ReadOnly = true;
            this.columnEndLimit.Width = 60;
            // 
            // columnHide
            // 
            this.columnHide.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnHide.HeaderText = "隠し";
            this.columnHide.Name = "columnHide";
            this.columnHide.ReadOnly = true;
            this.columnHide.Width = 35;
            // 
            // EffectSettingPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewEffect);
            this.Name = "EffectSettingPanel";
            this.Size = new System.Drawing.Size(394, 131);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEffect)).EndInit();
            this.contextMenuStripEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEffect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemItemEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemItemDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnEffectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRank;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSubRank;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProb;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnEndLimit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnHide;
    }
}
