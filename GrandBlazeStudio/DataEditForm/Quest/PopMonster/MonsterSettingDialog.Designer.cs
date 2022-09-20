namespace DataEditForm.Quest.PopMonster
{
    partial class MonsterSettingDialog
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
            this.numericUpDownPopProb = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownPopCountMax = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxMonsList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPopProb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPopCountMax)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(97, 7);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(189, 7);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.comboBoxMonsList);
            this.panelMain.Size = new System.Drawing.Size(283, 129);
            // 
            // numericUpDownPopProb
            // 
            this.numericUpDownPopProb.Location = new System.Drawing.Point(137, 40);
            this.numericUpDownPopProb.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownPopProb.Name = "numericUpDownPopProb";
            this.numericUpDownPopProb.Size = new System.Drawing.Size(120, 19);
            this.numericUpDownPopProb.TabIndex = 14;
            this.numericUpDownPopProb.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "出現率:";
            // 
            // numericUpDownPopCountMax
            // 
            this.numericUpDownPopCountMax.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.numericUpDownPopCountMax.Location = new System.Drawing.Point(136, 15);
            this.numericUpDownPopCountMax.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDownPopCountMax.Name = "numericUpDownPopCountMax";
            this.numericUpDownPopCountMax.Size = new System.Drawing.Size(120, 19);
            this.numericUpDownPopCountMax.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "出現個体数(0は無限):";
            // 
            // comboBoxMonsList
            // 
            this.comboBoxMonsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMonsList.FormattingEnabled = true;
            this.comboBoxMonsList.Location = new System.Drawing.Point(12, 24);
            this.comboBoxMonsList.Name = "comboBoxMonsList";
            this.comboBoxMonsList.Size = new System.Drawing.Size(263, 20);
            this.comboBoxMonsList.TabIndex = 8;
            this.comboBoxMonsList.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboBoxMonsList_Format);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "出現モンスター:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownPopCountMax);
            this.groupBox1.Controls.Add(this.numericUpDownPopProb);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 71);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出現条件";
            // 
            // MonsterSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(283, 162);
            this.Name = "MonsterSettingDialog";
            this.Text = "モンスター出現設定";
            this.Load += new System.EventHandler(this.MonsterSettingDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPopProb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPopCountMax)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownPopCountMax;
        private System.Windows.Forms.NumericUpDown numericUpDownPopProb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxMonsList;
    }
}
