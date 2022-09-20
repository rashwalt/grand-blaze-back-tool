namespace CommonFormLibrary.CommonDialog
{
    partial class SelectionEffectDialog
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
            this.comboBoxEffect = new System.Windows.Forms.ComboBox();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.numericUpDownFilterEffectIDMax = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownFilterEffectIDMin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownRank = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownProb = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownEndLimit = new System.Windows.Forms.NumericUpDown();
            this.checkBoxFilterValid = new System.Windows.Forms.CheckBox();
            this.buttonPerfectDeffence = new System.Windows.Forms.Button();
            this.numericUpDownSubRank = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxHide = new System.Windows.Forms.CheckBox();
            this.panelMain.SuspendLayout();
            this.groupBoxFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterEffectIDMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterEffectIDMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubRank)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(304, 7);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(396, 7);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.checkBoxHide);
            this.panelMain.Controls.Add(this.numericUpDownSubRank);
            this.panelMain.Controls.Add(this.label8);
            this.panelMain.Controls.Add(this.buttonPerfectDeffence);
            this.panelMain.Controls.Add(this.checkBoxFilterValid);
            this.panelMain.Controls.Add(this.numericUpDownEndLimit);
            this.panelMain.Controls.Add(this.label7);
            this.panelMain.Controls.Add(this.label6);
            this.panelMain.Controls.Add(this.numericUpDownProb);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.numericUpDownRank);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.groupBoxFilter);
            this.panelMain.Controls.Add(this.comboBoxEffect);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Size = new System.Drawing.Size(494, 213);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "エフェクト:";
            // 
            // comboBoxEffect
            // 
            this.comboBoxEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEffect.FormattingEnabled = true;
            this.comboBoxEffect.Location = new System.Drawing.Point(12, 24);
            this.comboBoxEffect.Name = "comboBoxEffect";
            this.comboBoxEffect.Size = new System.Drawing.Size(263, 20);
            this.comboBoxEffect.TabIndex = 1;
            this.comboBoxEffect.SelectedValueChanged += new System.EventHandler(this.comboBoxEffect_SelectedValueChanged);
            this.comboBoxEffect.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboBoxEffect_Format);
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Controls.Add(this.numericUpDownFilterEffectIDMax);
            this.groupBoxFilter.Controls.Add(this.label3);
            this.groupBoxFilter.Controls.Add(this.numericUpDownFilterEffectIDMin);
            this.groupBoxFilter.Controls.Add(this.label2);
            this.groupBoxFilter.Enabled = false;
            this.groupBoxFilter.Location = new System.Drawing.Point(281, 31);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(200, 50);
            this.groupBoxFilter.TabIndex = 2;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "エフェクト：フィルタ";
            // 
            // numericUpDownFilterEffectIDMax
            // 
            this.numericUpDownFilterEffectIDMax.Location = new System.Drawing.Point(120, 19);
            this.numericUpDownFilterEffectIDMax.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownFilterEffectIDMax.Name = "numericUpDownFilterEffectIDMax";
            this.numericUpDownFilterEffectIDMax.Size = new System.Drawing.Size(56, 19);
            this.numericUpDownFilterEffectIDMax.TabIndex = 3;
            this.numericUpDownFilterEffectIDMax.Value = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownFilterEffectIDMax.ValueChanged += new System.EventHandler(this.numericUpDownFilterEffectIDMax_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "〜";
            // 
            // numericUpDownFilterEffectIDMin
            // 
            this.numericUpDownFilterEffectIDMin.Location = new System.Drawing.Point(35, 19);
            this.numericUpDownFilterEffectIDMin.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownFilterEffectIDMin.Name = "numericUpDownFilterEffectIDMin";
            this.numericUpDownFilterEffectIDMin.Size = new System.Drawing.Size(56, 19);
            this.numericUpDownFilterEffectIDMin.TabIndex = 1;
            this.numericUpDownFilterEffectIDMin.ValueChanged += new System.EventHandler(this.numericUpDownFilterEffectIDMin_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "ランク:";
            // 
            // numericUpDownRank
            // 
            this.numericUpDownRank.DecimalPlaces = 4;
            this.numericUpDownRank.Location = new System.Drawing.Point(12, 72);
            this.numericUpDownRank.Name = "numericUpDownRank";
            this.numericUpDownRank.Size = new System.Drawing.Size(103, 19);
            this.numericUpDownRank.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "確率:";
            // 
            // numericUpDownProb
            // 
            this.numericUpDownProb.DecimalPlaces = 4;
            this.numericUpDownProb.Location = new System.Drawing.Point(12, 146);
            this.numericUpDownProb.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericUpDownProb.Name = "numericUpDownProb";
            this.numericUpDownProb.Size = new System.Drawing.Size(103, 19);
            this.numericUpDownProb.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(121, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "持続カウント:";
            // 
            // numericUpDownEndLimit
            // 
            this.numericUpDownEndLimit.Location = new System.Drawing.Point(12, 183);
            this.numericUpDownEndLimit.Name = "numericUpDownEndLimit";
            this.numericUpDownEndLimit.Size = new System.Drawing.Size(103, 19);
            this.numericUpDownEndLimit.TabIndex = 9;
            // 
            // checkBoxFilterValid
            // 
            this.checkBoxFilterValid.AutoSize = true;
            this.checkBoxFilterValid.Location = new System.Drawing.Point(281, 8);
            this.checkBoxFilterValid.Name = "checkBoxFilterValid";
            this.checkBoxFilterValid.Size = new System.Drawing.Size(81, 16);
            this.checkBoxFilterValid.TabIndex = 4;
            this.checkBoxFilterValid.Text = "フィルタ有効";
            this.checkBoxFilterValid.UseVisualStyleBackColor = true;
            this.checkBoxFilterValid.CheckedChanged += new System.EventHandler(this.checkBoxFilterValid_CheckedChanged);
            // 
            // buttonPerfectDeffence
            // 
            this.buttonPerfectDeffence.Location = new System.Drawing.Point(407, 89);
            this.buttonPerfectDeffence.Name = "buttonPerfectDeffence";
            this.buttonPerfectDeffence.Size = new System.Drawing.Size(75, 23);
            this.buttonPerfectDeffence.TabIndex = 10;
            this.buttonPerfectDeffence.Text = "完全防御";
            this.buttonPerfectDeffence.UseVisualStyleBackColor = true;
            this.buttonPerfectDeffence.Click += new System.EventHandler(this.buttonPerfectDeffence_Click);
            // 
            // numericUpDownSubRank
            // 
            this.numericUpDownSubRank.DecimalPlaces = 4;
            this.numericUpDownSubRank.Location = new System.Drawing.Point(12, 109);
            this.numericUpDownSubRank.Name = "numericUpDownSubRank";
            this.numericUpDownSubRank.Size = new System.Drawing.Size(103, 19);
            this.numericUpDownSubRank.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "サブランク:";
            // 
            // checkBoxHide
            // 
            this.checkBoxHide.AutoSize = true;
            this.checkBoxHide.Location = new System.Drawing.Point(190, 51);
            this.checkBoxHide.Name = "checkBoxHide";
            this.checkBoxHide.Size = new System.Drawing.Size(84, 16);
            this.checkBoxHide.TabIndex = 18;
            this.checkBoxHide.Text = "説明非表示";
            this.checkBoxHide.UseVisualStyleBackColor = true;
            // 
            // SelectionEffectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(494, 246);
            this.Name = "SelectionEffectDialog";
            this.Text = "エフェクト設定";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterEffectIDMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterEffectIDMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubRank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.NumericUpDown numericUpDownFilterEffectIDMax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownFilterEffectIDMin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxEffect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownRank;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownEndLimit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownProb;
        private System.Windows.Forms.CheckBox checkBoxFilterValid;
        private System.Windows.Forms.Button buttonPerfectDeffence;
        private System.Windows.Forms.NumericUpDown numericUpDownSubRank;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxHide;
    }
}
