namespace DataEditForm.SkillGet
{
    partial class SkillGetListPanel
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
            this.buttonArtsSelect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBase = new System.Windows.Forms.Button();
            this.textBoxBaseName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxBaseNo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBoxGuardian = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBoxRace = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownUNQ = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownMAG = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownAGI = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownSTR = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownLevel = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownInstallLevel = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxInstall = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUNQ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMAG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAGI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSTR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInstallLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Controls.Add(this.buttonArtsSelect);
            this.panelMain.Controls.Add(this.textBoxName);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.textBoxNo);
            this.panelMain.Controls.Add(this.label1);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "Ｐスキル";
            // 
            // textBoxName
            // 
            this.textBoxName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxName.Location = new System.Drawing.Point(92, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            this.textBoxName.Size = new System.Drawing.Size(326, 19);
            this.textBoxName.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "スキル:";
            // 
            // textBoxNo
            // 
            this.textBoxNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxNo.Location = new System.Drawing.Point(6, 24);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.Size = new System.Drawing.Size(80, 19);
            this.textBoxNo.TabIndex = 29;
            this.textBoxNo.TextChanged += new System.EventHandler(this.textBoxNo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "No.:";
            // 
            // buttonArtsSelect
            // 
            this.buttonArtsSelect.Location = new System.Drawing.Point(415, 24);
            this.buttonArtsSelect.Name = "buttonArtsSelect";
            this.buttonArtsSelect.Size = new System.Drawing.Size(24, 19);
            this.buttonArtsSelect.TabIndex = 33;
            this.buttonArtsSelect.Text = "...";
            this.buttonArtsSelect.UseVisualStyleBackColor = true;
            this.buttonArtsSelect.Click += new System.EventHandler(this.buttonArtsSelect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBase);
            this.groupBox1.Controls.Add(this.textBoxBaseName);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.textBoxBaseNo);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.comboBoxGuardian);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.comboBoxRace);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.numericUpDownUNQ);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.numericUpDownMAG);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.numericUpDownAGI);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numericUpDownSTR);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numericUpDownLevel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDownInstallLevel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxInstall);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 288);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "条件";
            // 
            // buttonBase
            // 
            this.buttonBase.Location = new System.Drawing.Point(415, 255);
            this.buttonBase.Name = "buttonBase";
            this.buttonBase.Size = new System.Drawing.Size(24, 19);
            this.buttonBase.TabIndex = 38;
            this.buttonBase.Text = "...";
            this.buttonBase.UseVisualStyleBackColor = true;
            this.buttonBase.Click += new System.EventHandler(this.buttonBase_Click);
            // 
            // textBoxBaseName
            // 
            this.textBoxBaseName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxBaseName.Location = new System.Drawing.Point(92, 255);
            this.textBoxBaseName.Name = "textBoxBaseName";
            this.textBoxBaseName.ReadOnly = true;
            this.textBoxBaseName.Size = new System.Drawing.Size(326, 19);
            this.textBoxBaseName.TabIndex = 35;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(90, 240);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 12);
            this.label15.TabIndex = 37;
            this.label15.Text = "名称:";
            // 
            // textBoxBaseNo
            // 
            this.textBoxBaseNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxBaseNo.Location = new System.Drawing.Point(6, 255);
            this.textBoxBaseNo.Name = "textBoxBaseNo";
            this.textBoxBaseNo.Size = new System.Drawing.Size(80, 19);
            this.textBoxBaseNo.TabIndex = 34;
            this.textBoxBaseNo.TextChanged += new System.EventHandler(this.textBoxBaseNo_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 240);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(76, 12);
            this.label16.TabIndex = 36;
            this.label16.Text = "前提スキルNo.:";
            // 
            // comboBoxGuardian
            // 
            this.comboBoxGuardian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGuardian.FormattingEnabled = true;
            this.comboBoxGuardian.Items.AddRange(new object[] {
            "なし",
            "修羅の炎帝イグニート",
            "氷花の乙女セルシウス",
            "風来の鬼神チャフリカ",
            "地獄の咆哮クツェルカン",
            "湧泉の真人カアシャック",
            "轟縛の雷帝イーヴァン",
            "閃光の翼士イシュタス",
            "漆黒の魔手アン・プトゥ"});
            this.comboBoxGuardian.Location = new System.Drawing.Point(8, 106);
            this.comboBoxGuardian.Name = "comboBoxGuardian";
            this.comboBoxGuardian.Size = new System.Drawing.Size(216, 20);
            this.comboBoxGuardian.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 91);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 12);
            this.label14.TabIndex = 24;
            this.label14.Text = "守護者:";
            // 
            // comboBoxRace
            // 
            this.comboBoxRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRace.FormattingEnabled = true;
            this.comboBoxRace.Location = new System.Drawing.Point(8, 68);
            this.comboBoxRace.Name = "comboBoxRace";
            this.comboBoxRace.Size = new System.Drawing.Size(216, 20);
            this.comboBoxRace.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 53);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 12);
            this.label13.TabIndex = 22;
            this.label13.Text = "種族:";
            // 
            // numericUpDownUNQ
            // 
            this.numericUpDownUNQ.Location = new System.Drawing.Point(84, 218);
            this.numericUpDownUNQ.Name = "numericUpDownUNQ";
            this.numericUpDownUNQ.Size = new System.Drawing.Size(70, 19);
            this.numericUpDownUNQ.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(82, 203);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 12);
            this.label12.TabIndex = 18;
            this.label12.Text = "UNQ:";
            // 
            // numericUpDownMAG
            // 
            this.numericUpDownMAG.Location = new System.Drawing.Point(8, 218);
            this.numericUpDownMAG.Name = "numericUpDownMAG";
            this.numericUpDownMAG.Size = new System.Drawing.Size(70, 19);
            this.numericUpDownMAG.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 203);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "MAG:";
            // 
            // numericUpDownAGI
            // 
            this.numericUpDownAGI.Location = new System.Drawing.Point(84, 181);
            this.numericUpDownAGI.Name = "numericUpDownAGI";
            this.numericUpDownAGI.Size = new System.Drawing.Size(70, 19);
            this.numericUpDownAGI.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(82, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "AGI:";
            // 
            // numericUpDownSTR
            // 
            this.numericUpDownSTR.Location = new System.Drawing.Point(8, 181);
            this.numericUpDownSTR.Name = "numericUpDownSTR";
            this.numericUpDownSTR.Size = new System.Drawing.Size(70, 19);
            this.numericUpDownSTR.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "STR:";
            // 
            // numericUpDownLevel
            // 
            this.numericUpDownLevel.Location = new System.Drawing.Point(8, 144);
            this.numericUpDownLevel.Name = "numericUpDownLevel";
            this.numericUpDownLevel.Size = new System.Drawing.Size(70, 19);
            this.numericUpDownLevel.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "キャラクターレベル:";
            // 
            // numericUpDownInstallLevel
            // 
            this.numericUpDownInstallLevel.Location = new System.Drawing.Point(255, 30);
            this.numericUpDownInstallLevel.Name = "numericUpDownInstallLevel";
            this.numericUpDownInstallLevel.Size = new System.Drawing.Size(56, 19);
            this.numericUpDownInstallLevel.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Lv:\r\n";
            // 
            // comboBoxInstall
            // 
            this.comboBoxInstall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInstall.FormattingEnabled = true;
            this.comboBoxInstall.Location = new System.Drawing.Point(8, 30);
            this.comboBoxInstall.Name = "comboBoxInstall";
            this.comboBoxInstall.Size = new System.Drawing.Size(216, 20);
            this.comboBoxInstall.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "インストールクラス:";
            // 
            // SkillGetListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SkillGetListPanel";
            this.Controls.SetChildIndex(this.panelList, 0);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUNQ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMAG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAGI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSTR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInstallLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonArtsSelect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownUNQ;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDownMAG;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownAGI;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownSTR;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownInstallLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxInstall;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxGuardian;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxRace;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonBase;
        private System.Windows.Forms.TextBox textBoxBaseName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxBaseNo;
        private System.Windows.Forms.Label label16;
    }
}
