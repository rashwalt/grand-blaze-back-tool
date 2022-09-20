namespace GrandBlazeStudio.MainFormItem
{
    partial class EventJoinForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.columnEntryNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnGroom = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnBride = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(574, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(666, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dataGridView);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Size = new System.Drawing.Size(763, 521);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "ウェディングサポートが選択されています。\r\nイベントに参加するPCを設定してください。";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnEntryNo,
            this.columnGroom,
            this.columnBride});
            this.dataGridView.Location = new System.Drawing.Point(14, 36);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.Size = new System.Drawing.Size(738, 477);
            this.dataGridView.TabIndex = 1;
            // 
            // columnEntryNo
            // 
            this.columnEntryNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.columnEntryNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.columnEntryNo.Frozen = true;
            this.columnEntryNo.HeaderText = "E-No.";
            this.columnEntryNo.Name = "columnEntryNo";
            this.columnEntryNo.Width = 59;
            // 
            // columnGroom
            // 
            this.columnGroom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.columnGroom.HeaderText = "新郎フラグ";
            this.columnGroom.Name = "columnGroom";
            this.columnGroom.Width = 60;
            // 
            // columnBride
            // 
            this.columnBride.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.columnBride.HeaderText = "新婦フラグ";
            this.columnBride.Name = "columnBride";
            this.columnBride.Width = 60;
            // 
            // EventJoinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(763, 554);
            this.Name = "EventJoinForm";
            this.Text = "イベント参加設定";
            this.Load += new System.EventHandler(this.EventJoinForm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnEntryNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnGroom;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnBride;
    }
}
