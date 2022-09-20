namespace DataEditForm.Skill
{
    partial class SkillFilterDialog
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
            this.buttonReset = new System.Windows.Forms.Button();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownFilterLevel = new System.Windows.Forms.NumericUpDown();
            this.comboBoxFilterLevel = new System.Windows.Forms.ComboBox();
            this.comboBoxArtsCategory = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(20, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(112, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.comboBoxArtsCategory);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.buttonReset);
            this.panelMain.Controls.Add(this.comboBoxFilterLevel);
            this.panelMain.Controls.Add(this.numericUpDownFilterLevel);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.comboBoxFilterType);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Size = new System.Drawing.Size(210, 159);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(132, 126);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 36;
            this.buttonReset.Text = "リセット";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // comboBoxFilterType
            // 
            this.comboBoxFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterType.FormattingEnabled = true;
            this.comboBoxFilterType.Items.AddRange(new object[] {
            "すべて",
            "アーツ",
            "サポート",
            "フィールド",
            "スペシャル",
            "アシスト"});
            this.comboBoxFilterType.Location = new System.Drawing.Point(14, 24);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(160, 20);
            this.comboBoxFilterType.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "種別:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "ID:";
            // 
            // numericUpDownFilterLevel
            // 
            this.numericUpDownFilterLevel.Location = new System.Drawing.Point(14, 100);
            this.numericUpDownFilterLevel.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownFilterLevel.Name = "numericUpDownFilterLevel";
            this.numericUpDownFilterLevel.Size = new System.Drawing.Size(58, 19);
            this.numericUpDownFilterLevel.TabIndex = 30;
            // 
            // comboBoxFilterLevel
            // 
            this.comboBoxFilterLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterLevel.FormattingEnabled = true;
            this.comboBoxFilterLevel.Items.AddRange(new object[] {
            "以上",
            "以下"});
            this.comboBoxFilterLevel.Location = new System.Drawing.Point(78, 100);
            this.comboBoxFilterLevel.Name = "comboBoxFilterLevel";
            this.comboBoxFilterLevel.Size = new System.Drawing.Size(96, 20);
            this.comboBoxFilterLevel.TabIndex = 31;
            // 
            // comboBoxArtsCategory
            // 
            this.comboBoxArtsCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArtsCategory.FormattingEnabled = true;
            this.comboBoxArtsCategory.Location = new System.Drawing.Point(14, 62);
            this.comboBoxArtsCategory.Name = "comboBoxArtsCategory";
            this.comboBoxArtsCategory.Size = new System.Drawing.Size(159, 20);
            this.comboBoxArtsCategory.TabIndex = 46;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 12);
            this.label3.TabIndex = 45;
            this.label3.Text = "アーツカテゴリ:";
            // 
            // SkillFilterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(210, 192);
            this.Name = "SkillFilterDialog";
            this.Text = "スキルフィルタ";
            this.Load += new System.EventHandler(this.SkillFilterDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ComboBox comboBoxFilterType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxFilterLevel;
        private System.Windows.Forms.NumericUpDown numericUpDownFilterLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxArtsCategory;
        private System.Windows.Forms.Label label3;
    }
}
