namespace DataEditForm.Quest
{
    partial class MarkEditDialog
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.comboBoxQuest = new System.Windows.Forms.ComboBox();
            this.comboBoxFieldType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonWeather = new System.Windows.Forms.Button();
            this.textBoxWeather = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonPopMonster = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxTraps = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxKeyItem = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownCureRate = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownKeyLevel = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDownHackLevel = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTrapLevel = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownTrapHide = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDownPopMonsterLevel = new System.Windows.Forms.NumericUpDown();
            this.checkBoxHideMark = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.eventEditorP = new CommonFormLibrary.CommonPanel.EventEditorPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.eventEditorE = new CommonFormLibrary.CommonPanel.EventEditorPanel();
            this.buttonEditP = new System.Windows.Forms.Button();
            this.buttonEditE = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCureRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHackLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrapLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrapHide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPopMonsterLevel)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(677, 6);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(769, 6);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonEditE);
            this.panelMain.Controls.Add(this.buttonEditP);
            this.panelMain.Controls.Add(this.groupBox6);
            this.panelMain.Controls.Add(this.groupBox5);
            this.panelMain.Controls.Add(this.groupBox4);
            this.panelMain.Controls.Add(this.groupBox2);
            this.panelMain.Controls.Add(this.checkBoxHideMark);
            this.panelMain.Controls.Add(this.numericUpDownPopMonsterLevel);
            this.panelMain.Controls.Add(this.label14);
            this.panelMain.Controls.Add(this.numericUpDownCureRate);
            this.panelMain.Controls.Add(this.label9);
            this.panelMain.Controls.Add(this.buttonPopMonster);
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Controls.Add(this.comboBoxQuest);
            this.panelMain.Controls.Add(this.comboBoxFieldType);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.textBoxName);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.textBoxNo);
            this.panelMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelMain.Size = new System.Drawing.Size(867, 608);
            // 
            // textBoxName
            // 
            this.textBoxName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxName.Location = new System.Drawing.Point(98, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(329, 19);
            this.textBoxName.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "No.:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "名称:";
            // 
            // textBoxNo
            // 
            this.textBoxNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxNo.Location = new System.Drawing.Point(12, 24);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.Size = new System.Drawing.Size(80, 19);
            this.textBoxNo.TabIndex = 29;
            // 
            // comboBoxQuest
            // 
            this.comboBoxQuest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQuest.Enabled = false;
            this.comboBoxQuest.FormattingEnabled = true;
            this.comboBoxQuest.Location = new System.Drawing.Point(433, 23);
            this.comboBoxQuest.Name = "comboBoxQuest";
            this.comboBoxQuest.Size = new System.Drawing.Size(208, 20);
            this.comboBoxQuest.TabIndex = 36;
            // 
            // comboBoxFieldType
            // 
            this.comboBoxFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFieldType.FormattingEnabled = true;
            this.comboBoxFieldType.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.comboBoxFieldType.Location = new System.Drawing.Point(647, 24);
            this.comboBoxFieldType.Name = "comboBoxFieldType";
            this.comboBoxFieldType.Size = new System.Drawing.Size(201, 20);
            this.comboBoxFieldType.TabIndex = 35;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(647, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 34;
            this.label5.Text = "フィールドタイプ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(431, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 33;
            this.label3.Text = "所属クエスト:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonWeather);
            this.groupBox1.Controls.Add(this.textBoxWeather);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(15, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 41);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "マーク設定";
            // 
            // buttonWeather
            // 
            this.buttonWeather.Location = new System.Drawing.Point(227, 14);
            this.buttonWeather.Name = "buttonWeather";
            this.buttonWeather.Size = new System.Drawing.Size(61, 23);
            this.buttonWeather.TabIndex = 10;
            this.buttonWeather.Text = "天候設定";
            this.buttonWeather.UseVisualStyleBackColor = true;
            this.buttonWeather.Click += new System.EventHandler(this.buttonWeather_Click);
            // 
            // textBoxWeather
            // 
            this.textBoxWeather.Location = new System.Drawing.Point(43, 16);
            this.textBoxWeather.Name = "textBoxWeather";
            this.textBoxWeather.ReadOnly = true;
            this.textBoxWeather.Size = new System.Drawing.Size(178, 19);
            this.textBoxWeather.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "天候:";
            // 
            // buttonPopMonster
            // 
            this.buttonPopMonster.Location = new System.Drawing.Point(381, 61);
            this.buttonPopMonster.Name = "buttonPopMonster";
            this.buttonPopMonster.Size = new System.Drawing.Size(124, 23);
            this.buttonPopMonster.TabIndex = 39;
            this.buttonPopMonster.Text = "出現モンスター設定";
            this.buttonPopMonster.UseVisualStyleBackColor = true;
            this.buttonPopMonster.Click += new System.EventHandler(this.buttonPopMonster_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 12);
            this.label7.TabIndex = 46;
            this.label7.Text = "配置トラップ:";
            // 
            // comboBoxTraps
            // 
            this.comboBoxTraps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTraps.FormattingEnabled = true;
            this.comboBoxTraps.Location = new System.Drawing.Point(8, 29);
            this.comboBoxTraps.Name = "comboBoxTraps";
            this.comboBoxTraps.Size = new System.Drawing.Size(173, 20);
            this.comboBoxTraps.TabIndex = 47;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 12);
            this.label8.TabIndex = 48;
            this.label8.Text = "必要貴重品:";
            // 
            // comboBoxKeyItem
            // 
            this.comboBoxKeyItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyItem.FormattingEnabled = true;
            this.comboBoxKeyItem.Location = new System.Drawing.Point(8, 30);
            this.comboBoxKeyItem.Name = "comboBoxKeyItem";
            this.comboBoxKeyItem.Size = new System.Drawing.Size(156, 20);
            this.comboBoxKeyItem.TabIndex = 49;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(326, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 12);
            this.label9.TabIndex = 50;
            this.label9.Text = "回復量:";
            // 
            // numericUpDownCureRate
            // 
            this.numericUpDownCureRate.Location = new System.Drawing.Point(326, 102);
            this.numericUpDownCureRate.Name = "numericUpDownCureRate";
            this.numericUpDownCureRate.Size = new System.Drawing.Size(51, 19);
            this.numericUpDownCureRate.TabIndex = 51;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(171, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 12);
            this.label10.TabIndex = 52;
            this.label10.Text = "解錠Lv:";
            // 
            // numericUpDownKeyLevel
            // 
            this.numericUpDownKeyLevel.Location = new System.Drawing.Point(170, 30);
            this.numericUpDownKeyLevel.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownKeyLevel.Name = "numericUpDownKeyLevel";
            this.numericUpDownKeyLevel.Size = new System.Drawing.Size(44, 19);
            this.numericUpDownKeyLevel.TabIndex = 53;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(221, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 12);
            this.label11.TabIndex = 54;
            this.label11.Text = "解析Lv:";
            // 
            // numericUpDownHackLevel
            // 
            this.numericUpDownHackLevel.Location = new System.Drawing.Point(221, 30);
            this.numericUpDownHackLevel.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownHackLevel.Name = "numericUpDownHackLevel";
            this.numericUpDownHackLevel.Size = new System.Drawing.Size(44, 19);
            this.numericUpDownHackLevel.TabIndex = 55;
            // 
            // numericUpDownTrapLevel
            // 
            this.numericUpDownTrapLevel.Location = new System.Drawing.Point(238, 29);
            this.numericUpDownTrapLevel.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownTrapLevel.Name = "numericUpDownTrapLevel";
            this.numericUpDownTrapLevel.Size = new System.Drawing.Size(44, 19);
            this.numericUpDownTrapLevel.TabIndex = 59;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(238, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 12);
            this.label12.TabIndex = 58;
            this.label12.Text = "罠Lv:";
            // 
            // numericUpDownTrapHide
            // 
            this.numericUpDownTrapHide.Location = new System.Drawing.Point(187, 29);
            this.numericUpDownTrapHide.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownTrapHide.Name = "numericUpDownTrapHide";
            this.numericUpDownTrapHide.Size = new System.Drawing.Size(44, 19);
            this.numericUpDownTrapHide.TabIndex = 57;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(188, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 12);
            this.label13.TabIndex = 56;
            this.label13.Text = "隠蔽Lv:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(326, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 12);
            this.label14.TabIndex = 60;
            this.label14.Text = "出現最低敵数:";
            // 
            // numericUpDownPopMonsterLevel
            // 
            this.numericUpDownPopMonsterLevel.Location = new System.Drawing.Point(326, 64);
            this.numericUpDownPopMonsterLevel.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownPopMonsterLevel.Name = "numericUpDownPopMonsterLevel";
            this.numericUpDownPopMonsterLevel.Size = new System.Drawing.Size(49, 19);
            this.numericUpDownPopMonsterLevel.TabIndex = 61;
            // 
            // checkBoxHideMark
            // 
            this.checkBoxHideMark.AutoSize = true;
            this.checkBoxHideMark.Location = new System.Drawing.Point(383, 103);
            this.checkBoxHideMark.Name = "checkBoxHideMark";
            this.checkBoxHideMark.Size = new System.Drawing.Size(72, 16);
            this.checkBoxHideMark.TabIndex = 62;
            this.checkBoxHideMark.Text = "隠しマーク";
            this.checkBoxHideMark.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.comboBoxTraps);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.numericUpDownTrapHide);
            this.groupBox2.Controls.Add(this.numericUpDownTrapLevel);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(14, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(295, 55);
            this.groupBox2.TabIndex = 63;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "トラップ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.comboBoxKeyItem);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.numericUpDownKeyLevel);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.numericUpDownHackLevel);
            this.groupBox4.Location = new System.Drawing.Point(511, 50);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(337, 54);
            this.groupBox4.TabIndex = 64;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "進入条件";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.eventEditorP);
            this.groupBox5.Location = new System.Drawing.Point(15, 158);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(432, 450);
            this.groupBox5.TabIndex = 65;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "プロローグ";
            // 
            // eventEditorP
            // 
            this.eventEditorP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eventEditorP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventEditorP.Location = new System.Drawing.Point(3, 15);
            this.eventEditorP.Name = "eventEditorP";
            this.eventEditorP.Size = new System.Drawing.Size(426, 432);
            this.eventEditorP.TabIndex = 41;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.eventEditorE);
            this.groupBox6.Location = new System.Drawing.Point(453, 158);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(402, 449);
            this.groupBox6.TabIndex = 66;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "エピローグ";
            // 
            // eventEditorE
            // 
            this.eventEditorE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eventEditorE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventEditorE.Location = new System.Drawing.Point(3, 15);
            this.eventEditorE.Name = "eventEditorE";
            this.eventEditorE.Size = new System.Drawing.Size(396, 431);
            this.eventEditorE.TabIndex = 41;
            // 
            // buttonEditP
            // 
            this.buttonEditP.Location = new System.Drawing.Point(322, 129);
            this.buttonEditP.Name = "buttonEditP";
            this.buttonEditP.Size = new System.Drawing.Size(124, 23);
            this.buttonEditP.TabIndex = 67;
            this.buttonEditP.Text = "プロローグ外部編集";
            this.buttonEditP.UseVisualStyleBackColor = true;
            this.buttonEditP.Click += new System.EventHandler(this.buttonEditP_Click);
            // 
            // buttonEditE
            // 
            this.buttonEditE.Location = new System.Drawing.Point(724, 129);
            this.buttonEditE.Name = "buttonEditE";
            this.buttonEditE.Size = new System.Drawing.Size(124, 23);
            this.buttonEditE.TabIndex = 68;
            this.buttonEditE.Text = "エピローグ外部編集";
            this.buttonEditE.UseVisualStyleBackColor = true;
            this.buttonEditE.Click += new System.EventHandler(this.buttonEditE_Click);
            // 
            // MarkEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(867, 641);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MarkEditDialog";
            this.Text = "マーク設定";
            this.Load += new System.EventHandler(this.MarkEditDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCureRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHackLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrapLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrapHide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPopMonsterLevel)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNo;
        private System.Windows.Forms.ComboBox comboBoxQuest;
        private System.Windows.Forms.ComboBox comboBoxFieldType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxWeather;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonPopMonster;
        private System.Windows.Forms.Button buttonWeather;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxTraps;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxKeyItem;
        private System.Windows.Forms.NumericUpDown numericUpDownHackLevel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDownKeyLevel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownCureRate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownTrapLevel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDownTrapHide;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericUpDownPopMonsterLevel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBoxHideMark;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private CommonFormLibrary.CommonPanel.EventEditorPanel eventEditorE;
        private System.Windows.Forms.GroupBox groupBox5;
        private CommonFormLibrary.CommonPanel.EventEditorPanel eventEditorP;
        private System.Windows.Forms.Button buttonEditE;
        private System.Windows.Forms.Button buttonEditP;
    }
}
