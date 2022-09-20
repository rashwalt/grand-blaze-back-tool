namespace CommonFormLibrary.CommonDialog
{
    partial class ItemSelectDialog
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
            this.comboBoxItems = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
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
            this.panelMain.SuspendLayout();
            this.groupBoxFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(140, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(232, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBoxFilter);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.comboBoxItems);
            this.panelMain.Size = new System.Drawing.Size(332, 243);
            // 
            // comboBoxItems
            // 
            this.comboBoxItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItems.FormattingEnabled = true;
            this.comboBoxItems.Location = new System.Drawing.Point(14, 27);
            this.comboBoxItems.Name = "comboBoxItems";
            this.comboBoxItems.Size = new System.Drawing.Size(287, 20);
            this.comboBoxItems.TabIndex = 0;
            this.comboBoxItems.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboBoxItems_Format);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "アイテム:";
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Controls.Add(this.comboBoxFilterMoney);
            this.groupBoxFilter.Controls.Add(this.numericUpDownFilterMoney);
            this.groupBoxFilter.Controls.Add(this.label5);
            this.groupBoxFilter.Controls.Add(this.comboBoxFilterLevel);
            this.groupBoxFilter.Controls.Add(this.numericUpDownFilterLevel);
            this.groupBoxFilter.Controls.Add(this.label4);
            this.groupBoxFilter.Controls.Add(this.comboBoxFilterShop);
            this.groupBoxFilter.Controls.Add(this.label3);
            this.groupBoxFilter.Controls.Add(this.comboBoxFilterType);
            this.groupBoxFilter.Controls.Add(this.label2);
            this.groupBoxFilter.Location = new System.Drawing.Point(14, 53);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(304, 176);
            this.groupBoxFilter.TabIndex = 2;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "フィルタ";
            // 
            // comboBoxFilterMoney
            // 
            this.comboBoxFilterMoney.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterMoney.FormattingEnabled = true;
            this.comboBoxFilterMoney.Items.AddRange(new object[] {
            "以上",
            "以下"});
            this.comboBoxFilterMoney.Location = new System.Drawing.Point(73, 143);
            this.comboBoxFilterMoney.Name = "comboBoxFilterMoney";
            this.comboBoxFilterMoney.Size = new System.Drawing.Size(96, 20);
            this.comboBoxFilterMoney.TabIndex = 9;
            this.comboBoxFilterMoney.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterMoney_SelectedIndexChanged);
            // 
            // numericUpDownFilterMoney
            // 
            this.numericUpDownFilterMoney.Location = new System.Drawing.Point(9, 143);
            this.numericUpDownFilterMoney.Name = "numericUpDownFilterMoney";
            this.numericUpDownFilterMoney.Size = new System.Drawing.Size(58, 19);
            this.numericUpDownFilterMoney.TabIndex = 8;
            this.numericUpDownFilterMoney.ValueChanged += new System.EventHandler(this.numericUpDownFilterMoney_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "販売価格:";
            // 
            // comboBoxFilterLevel
            // 
            this.comboBoxFilterLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterLevel.FormattingEnabled = true;
            this.comboBoxFilterLevel.Items.AddRange(new object[] {
            "以上",
            "以下"});
            this.comboBoxFilterLevel.Location = new System.Drawing.Point(73, 106);
            this.comboBoxFilterLevel.Name = "comboBoxFilterLevel";
            this.comboBoxFilterLevel.Size = new System.Drawing.Size(96, 20);
            this.comboBoxFilterLevel.TabIndex = 6;
            this.comboBoxFilterLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterLevel_SelectedIndexChanged);
            // 
            // numericUpDownFilterLevel
            // 
            this.numericUpDownFilterLevel.Location = new System.Drawing.Point(9, 106);
            this.numericUpDownFilterLevel.Name = "numericUpDownFilterLevel";
            this.numericUpDownFilterLevel.Size = new System.Drawing.Size(58, 19);
            this.numericUpDownFilterLevel.TabIndex = 5;
            this.numericUpDownFilterLevel.ValueChanged += new System.EventHandler(this.numericUpDownFilterLevel_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 12);
            this.label4.TabIndex = 4;
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
            this.comboBoxFilterShop.Location = new System.Drawing.Point(9, 68);
            this.comboBoxFilterShop.Name = "comboBoxFilterShop";
            this.comboBoxFilterShop.Size = new System.Drawing.Size(160, 20);
            this.comboBoxFilterShop.TabIndex = 3;
            this.comboBoxFilterShop.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterShop_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "販売区分:";
            // 
            // comboBoxFilterType
            // 
            this.comboBoxFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterType.FormattingEnabled = true;
            this.comboBoxFilterType.Location = new System.Drawing.Point(9, 30);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(160, 20);
            this.comboBoxFilterType.TabIndex = 1;
            this.comboBoxFilterType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "種別:";
            // 
            // ItemSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(332, 276);
            this.Name = "ItemSelectDialog";
            this.Text = "アイテム選択";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.ComboBox comboBoxFilterType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxItems;
        private System.Windows.Forms.ComboBox comboBoxFilterShop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxFilterLevel;
        private System.Windows.Forms.NumericUpDown numericUpDownFilterLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxFilterMoney;
        private System.Windows.Forms.NumericUpDown numericUpDownFilterMoney;
        private System.Windows.Forms.Label label5;
    }
}
