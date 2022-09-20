namespace DataEditForm.Monster.Common.SubDialog
{
    partial class SerifSettingDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSituation = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSerifText = new System.Windows.Forms.TextBox();
            this.comboBoxArts = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(339, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(431, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.comboBoxArts);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.textBoxSerifText);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.comboBoxSituation);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Size = new System.Drawing.Size(529, 421);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "シチュエーション:";
            // 
            // comboBoxSituation
            // 
            this.comboBoxSituation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSituation.FormattingEnabled = true;
            this.comboBoxSituation.Location = new System.Drawing.Point(14, 24);
            this.comboBoxSituation.Name = "comboBoxSituation";
            this.comboBoxSituation.Size = new System.Drawing.Size(275, 20);
            this.comboBoxSituation.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "セリフ:";
            // 
            // textBoxSerifText
            // 
            this.textBoxSerifText.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxSerifText.Location = new System.Drawing.Point(14, 100);
            this.textBoxSerifText.Multiline = true;
            this.textBoxSerifText.Name = "textBoxSerifText";
            this.textBoxSerifText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSerifText.Size = new System.Drawing.Size(503, 313);
            this.textBoxSerifText.TabIndex = 3;
            this.textBoxSerifText.WordWrap = false;
            // 
            // comboBoxArts
            // 
            this.comboBoxArts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArts.FormattingEnabled = true;
            this.comboBoxArts.Location = new System.Drawing.Point(14, 62);
            this.comboBoxArts.Name = "comboBoxArts";
            this.comboBoxArts.Size = new System.Drawing.Size(275, 20);
            this.comboBoxArts.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "アーツ条件:";
            // 
            // SerifSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(529, 454);
            this.Name = "SerifSettingDialog";
            this.Text = "セリフ設定";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSerifText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxSituation;
        private System.Windows.Forms.ComboBox comboBoxArts;
        private System.Windows.Forms.Label label3;
    }
}
