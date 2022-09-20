namespace CommonFormLibrary.CommonDialog
{
    partial class SelectionItemDialog
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
            this.comboBoxItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSelectItem = new System.Windows.Forms.Button();
            this.panelProb = new System.Windows.Forms.Panel();
            this.comboBoxProb = new System.Windows.Forms.ComboBox();
            this.numericUpDownProb = new System.Windows.Forms.NumericUpDown();
            this.labelProb = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.numericUpDownCount = new System.Windows.Forms.NumericUpDown();
            this.panelMain.SuspendLayout();
            this.panelProb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(152, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(244, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.numericUpDownCount);
            this.panelMain.Controls.Add(this.labelCount);
            this.panelMain.Controls.Add(this.panelProb);
            this.panelMain.Controls.Add(this.buttonSelectItem);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.comboBoxItem);
            this.panelMain.Size = new System.Drawing.Size(342, 152);
            // 
            // comboBoxItem
            // 
            this.comboBoxItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItem.FormattingEnabled = true;
            this.comboBoxItem.Location = new System.Drawing.Point(12, 24);
            this.comboBoxItem.Name = "comboBoxItem";
            this.comboBoxItem.Size = new System.Drawing.Size(251, 20);
            this.comboBoxItem.TabIndex = 0;
            this.comboBoxItem.SelectedIndexChanged += new System.EventHandler(this.comboBoxItem_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "アイテム:";
            // 
            // buttonSelectItem
            // 
            this.buttonSelectItem.Location = new System.Drawing.Point(269, 24);
            this.buttonSelectItem.Name = "buttonSelectItem";
            this.buttonSelectItem.Size = new System.Drawing.Size(61, 20);
            this.buttonSelectItem.TabIndex = 2;
            this.buttonSelectItem.Text = "選択...";
            this.buttonSelectItem.UseVisualStyleBackColor = true;
            this.buttonSelectItem.Click += new System.EventHandler(this.buttonSelectItem_Click);
            // 
            // panelProb
            // 
            this.panelProb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
            this.panelProb.Controls.Add(this.comboBoxProb);
            this.panelProb.Controls.Add(this.numericUpDownProb);
            this.panelProb.Controls.Add(this.labelProb);
            this.panelProb.Location = new System.Drawing.Point(12, 87);
            this.panelProb.Name = "panelProb";
            this.panelProb.Size = new System.Drawing.Size(318, 50);
            this.panelProb.TabIndex = 3;
            // 
            // comboBoxProb
            // 
            this.comboBoxProb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProb.FormattingEnabled = true;
            this.comboBoxProb.Items.AddRange(new object[] {
            "超低",
            "低",
            "中",
            "高",
            "必",
            "心得"});
            this.comboBoxProb.Location = new System.Drawing.Point(5, 18);
            this.comboBoxProb.Name = "comboBoxProb";
            this.comboBoxProb.Size = new System.Drawing.Size(78, 20);
            this.comboBoxProb.TabIndex = 2;
            this.comboBoxProb.Visible = false;
            // 
            // numericUpDownProb
            // 
            this.numericUpDownProb.Location = new System.Drawing.Point(5, 19);
            this.numericUpDownProb.Name = "numericUpDownProb";
            this.numericUpDownProb.Size = new System.Drawing.Size(100, 19);
            this.numericUpDownProb.TabIndex = 1;
            // 
            // labelProb
            // 
            this.labelProb.AutoSize = true;
            this.labelProb.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelProb.Location = new System.Drawing.Point(3, 4);
            this.labelProb.Name = "labelProb";
            this.labelProb.Size = new System.Drawing.Size(48, 12);
            this.labelProb.TabIndex = 0;
            this.labelProb.Text = "確率(%)";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(12, 47);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(31, 12);
            this.labelCount.TabIndex = 4;
            this.labelCount.Text = "個数:";
            // 
            // numericUpDownCount
            // 
            this.numericUpDownCount.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.numericUpDownCount.Location = new System.Drawing.Point(12, 62);
            this.numericUpDownCount.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDownCount.Name = "numericUpDownCount";
            this.numericUpDownCount.Size = new System.Drawing.Size(105, 19);
            this.numericUpDownCount.TabIndex = 5;
            // 
            // SelectionItemDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(342, 185);
            this.Name = "SelectionItemDialog";
            this.Text = "アイテム確率設定";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelProb.ResumeLayout(false);
            this.panelProb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxItem;
        private System.Windows.Forms.NumericUpDown numericUpDownCount;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Panel panelProb;
        private System.Windows.Forms.Button buttonSelectItem;
        private System.Windows.Forms.NumericUpDown numericUpDownProb;
        private System.Windows.Forms.Label labelProb;
        private System.Windows.Forms.ComboBox comboBoxProb;
    }
}
