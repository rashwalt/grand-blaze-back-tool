namespace DataEditForm.Item
{
    partial class ItemFilterDialog
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
            this.comboBoxFilterMoney = new System.Windows.Forms.ComboBox();
            this.numericUpDownFilterMoney = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxFilterLevel = new System.Windows.Forms.ComboBox();
            this.numericUpDownFilterLevel = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxFilterShop = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.comboBoxFilterSellType = new System.Windows.Forms.ComboBox();
            this.comboBoxSubCategory = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(12, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(104, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.comboBoxSubCategory);
            this.panelMain.Controls.Add(this.label6);
            this.panelMain.Controls.Add(this.comboBoxFilterSellType);
            this.panelMain.Controls.Add(this.buttonReset);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.comboBoxFilterMoney);
            this.panelMain.Controls.Add(this.numericUpDownFilterMoney);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.comboBoxFilterLevel);
            this.panelMain.Controls.Add(this.numericUpDownFilterLevel);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.comboBoxFilterShop);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.comboBoxFilterType);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Size = new System.Drawing.Size(203, 291);
            // 
            // comboBoxFilterMoney
            // 
            this.comboBoxFilterMoney.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterMoney.FormattingEnabled = true;
            this.comboBoxFilterMoney.Items.AddRange(new object[] {
            "以上",
            "以下"});
            this.comboBoxFilterMoney.Location = new System.Drawing.Point(78, 175);
            this.comboBoxFilterMoney.Name = "comboBoxFilterMoney";
            this.comboBoxFilterMoney.Size = new System.Drawing.Size(96, 20);
            this.comboBoxFilterMoney.TabIndex = 19;
            // 
            // numericUpDownFilterMoney
            // 
            this.numericUpDownFilterMoney.Location = new System.Drawing.Point(14, 175);
            this.numericUpDownFilterMoney.Name = "numericUpDownFilterMoney";
            this.numericUpDownFilterMoney.Size = new System.Drawing.Size(58, 19);
            this.numericUpDownFilterMoney.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "販売価格:";
            // 
            // comboBoxFilterLevel
            // 
            this.comboBoxFilterLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterLevel.FormattingEnabled = true;
            this.comboBoxFilterLevel.Items.AddRange(new object[] {
            "以上",
            "以下"});
            this.comboBoxFilterLevel.Location = new System.Drawing.Point(78, 138);
            this.comboBoxFilterLevel.Name = "comboBoxFilterLevel";
            this.comboBoxFilterLevel.Size = new System.Drawing.Size(96, 20);
            this.comboBoxFilterLevel.TabIndex = 16;
            // 
            // numericUpDownFilterLevel
            // 
            this.numericUpDownFilterLevel.Location = new System.Drawing.Point(14, 138);
            this.numericUpDownFilterLevel.Name = "numericUpDownFilterLevel";
            this.numericUpDownFilterLevel.Size = new System.Drawing.Size(58, 19);
            this.numericUpDownFilterLevel.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "装備レベル:";
            // 
            // comboBoxFilterShop
            // 
            this.comboBoxFilterShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterShop.FormattingEnabled = true;
            this.comboBoxFilterShop.Items.AddRange(new object[] {
            "すべて",
            "販売品のみ",
            "非売品のみ"});
            this.comboBoxFilterShop.Location = new System.Drawing.Point(14, 100);
            this.comboBoxFilterShop.Name = "comboBoxFilterShop";
            this.comboBoxFilterShop.Size = new System.Drawing.Size(160, 20);
            this.comboBoxFilterShop.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "販売区分:";
            // 
            // comboBoxFilterType
            // 
            this.comboBoxFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterType.FormattingEnabled = true;
            this.comboBoxFilterType.Location = new System.Drawing.Point(14, 24);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(160, 20);
            this.comboBoxFilterType.TabIndex = 11;
            this.comboBoxFilterType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "種別:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "合成:";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(125, 259);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 23;
            this.buttonReset.Text = "リセット";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // comboBoxFilterSellType
            // 
            this.comboBoxFilterSellType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterSellType.FormattingEnabled = true;
            this.comboBoxFilterSellType.Items.AddRange(new object[] {
            "システム",
            "合成品",
            "すべて"});
            this.comboBoxFilterSellType.Location = new System.Drawing.Point(14, 212);
            this.comboBoxFilterSellType.Name = "comboBoxFilterSellType";
            this.comboBoxFilterSellType.Size = new System.Drawing.Size(160, 20);
            this.comboBoxFilterSellType.TabIndex = 24;
            // 
            // comboBoxSubCategory
            // 
            this.comboBoxSubCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSubCategory.FormattingEnabled = true;
            this.comboBoxSubCategory.Location = new System.Drawing.Point(14, 62);
            this.comboBoxSubCategory.Name = "comboBoxSubCategory";
            this.comboBoxSubCategory.Size = new System.Drawing.Size(160, 20);
            this.comboBoxSubCategory.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "サブカテゴリ:";
            // 
            // ItemFilterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(203, 324);
            this.Name = "ItemFilterDialog";
            this.Text = "アイテムフィルタ";
            this.Load += new System.EventHandler(this.ItemFilterDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxFilterMoney;
        private System.Windows.Forms.NumericUpDown numericUpDownFilterMoney;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxFilterLevel;
        private System.Windows.Forms.NumericUpDown numericUpDownFilterLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxFilterShop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxFilterType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFilterSellType;
        private System.Windows.Forms.ComboBox comboBoxSubCategory;
        private System.Windows.Forms.Label label6;
    }
}
