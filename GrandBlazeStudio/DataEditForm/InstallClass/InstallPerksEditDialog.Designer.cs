namespace DataEditForm.InstallClass
{
    partial class InstallSkillEditDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.buttonArtsSelect = new System.Windows.Forms.Button();
            this.textBoxArtsName = new System.Windows.Forms.TextBox();
            this.numericUpDownRank = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxOnlyMode = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRank)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.comboBoxOnlyMode);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.buttonArtsSelect);
            this.panelMain.Controls.Add(this.textBoxArtsName);
            this.panelMain.Controls.Add(this.numericUpDownRank);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Size = new System.Drawing.Size(375, 129);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "習得スキル:";
            // 
            // buttonArtsSelect
            // 
            this.buttonArtsSelect.Location = new System.Drawing.Point(327, 61);
            this.buttonArtsSelect.Name = "buttonArtsSelect";
            this.buttonArtsSelect.Size = new System.Drawing.Size(24, 19);
            this.buttonArtsSelect.TabIndex = 9;
            this.buttonArtsSelect.Text = "...";
            this.buttonArtsSelect.UseVisualStyleBackColor = true;
            this.buttonArtsSelect.Click += new System.EventHandler(this.buttonArtsSelect_Click);
            // 
            // textBoxArtsName
            // 
            this.textBoxArtsName.Location = new System.Drawing.Point(14, 61);
            this.textBoxArtsName.Name = "textBoxArtsName";
            this.textBoxArtsName.ReadOnly = true;
            this.textBoxArtsName.Size = new System.Drawing.Size(307, 19);
            this.textBoxArtsName.TabIndex = 8;
            // 
            // numericUpDownRank
            // 
            this.numericUpDownRank.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.numericUpDownRank.Location = new System.Drawing.Point(14, 24);
            this.numericUpDownRank.Name = "numericUpDownRank";
            this.numericUpDownRank.Size = new System.Drawing.Size(77, 19);
            this.numericUpDownRank.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "レベル:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "使用可能制限:";
            // 
            // comboBoxOnlyMode
            // 
            this.comboBoxOnlyMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOnlyMode.FormattingEnabled = true;
            this.comboBoxOnlyMode.Items.AddRange(new object[] {
            "すべて",
            "プライマリ",
            "セカンダリ",
            "スクロール",
            "精霊契約"});
            this.comboBoxOnlyMode.Location = new System.Drawing.Point(15, 98);
            this.comboBoxOnlyMode.Name = "comboBoxOnlyMode";
            this.comboBoxOnlyMode.Size = new System.Drawing.Size(141, 20);
            this.comboBoxOnlyMode.TabIndex = 12;
            // 
            // InstallSkillEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 162);
            this.Name = "InstallSkillEditDialog";
            this.Text = "習得スキル";
            this.Load += new System.EventHandler(this.InstallSkillEditDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonArtsSelect;
        private System.Windows.Forms.TextBox textBoxArtsName;
        private System.Windows.Forms.NumericUpDown numericUpDownRank;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxOnlyMode;
        private System.Windows.Forms.Label label3;
    }
}