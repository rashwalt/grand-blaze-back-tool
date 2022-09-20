namespace DataEditForm.Trap
{
    partial class TrapDataPanel
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownHPDamageRate = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMPDamageRate = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.effectSettingPanel = new CommonFormLibrary.CommonPanel.EffectSettingPanel();
            this.panelMain.SuspendLayout();
            this.panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHPDamageRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMPDamageRate)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.effectSettingPanel);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.numericUpDownMPDamageRate);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.numericUpDownHPDamageRate);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.textBoxName);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.textBoxNo);
            this.panelMain.Controls.Add(this.label1);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "トラップ";
            // 
            // textBoxName
            // 
            this.textBoxName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxName.Location = new System.Drawing.Point(92, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(326, 19);
            this.textBoxName.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "名称:";
            // 
            // textBoxNo
            // 
            this.textBoxNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxNo.Location = new System.Drawing.Point(6, 24);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.ReadOnly = true;
            this.textBoxNo.Size = new System.Drawing.Size(80, 19);
            this.textBoxNo.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "No.:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 12);
            this.label3.TabIndex = 37;
            this.label3.Text = "HPダメージ(%):";
            // 
            // numericUpDownHPDamageRate
            // 
            this.numericUpDownHPDamageRate.Location = new System.Drawing.Point(6, 61);
            this.numericUpDownHPDamageRate.Name = "numericUpDownHPDamageRate";
            this.numericUpDownHPDamageRate.Size = new System.Drawing.Size(86, 19);
            this.numericUpDownHPDamageRate.TabIndex = 38;
            // 
            // numericUpDownMPDamageRate
            // 
            this.numericUpDownMPDamageRate.Location = new System.Drawing.Point(98, 61);
            this.numericUpDownMPDamageRate.Name = "numericUpDownMPDamageRate";
            this.numericUpDownMPDamageRate.Size = new System.Drawing.Size(86, 19);
            this.numericUpDownMPDamageRate.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "MPダメージ(%):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "エフェクト:";
            // 
            // effectSettingPanel
            // 
            this.effectSettingPanel.Location = new System.Drawing.Point(6, 98);
            this.effectSettingPanel.Name = "effectSettingPanel";
            this.effectSettingPanel.Size = new System.Drawing.Size(536, 317);
            this.effectSettingPanel.TabIndex = 42;
            // 
            // TrapDataPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "TrapDataPanel";
            this.Controls.SetChildIndex(this.panelList, 0);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHPDamageRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMPDamageRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownHPDamageRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownMPDamageRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private CommonFormLibrary.CommonPanel.EffectSettingPanel effectSettingPanel;
    }
}
