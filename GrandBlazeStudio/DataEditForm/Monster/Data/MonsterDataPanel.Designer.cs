namespace DataEditForm.Monster.Data
{
    partial class MonsterDataPanel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.checkedListBoxPopWeather = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxPop = new System.Windows.Forms.ListBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBoxTarget = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSerif = new System.Windows.Forms.Button();
            this.buttonCommonItemBox = new System.Windows.Forms.Button();
            this.buttonCommonElement = new System.Windows.Forms.Button();
            this.buttonCommonAction = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownMaxMultiAction = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMultiActionProb = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBoxFormation = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridViewBattleAbility = new System.Windows.Forms.DataGridView();
            this.columnMonsterID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnHP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSTR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAGI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnMAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUNQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnATK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSubATK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDFE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnMGR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAVD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnEXP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxMonsterKb = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            this.panelList.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxMultiAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMultiActionProb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBattleAbility)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.comboBoxMonsterKb);
            this.panelMain.Controls.Add(this.label8);
            this.panelMain.Controls.Add(this.dataGridViewBattleAbility);
            this.panelMain.Controls.Add(this.label7);
            this.panelMain.Controls.Add(this.comboBoxFormation);
            this.panelMain.Controls.Add(this.label16);
            this.panelMain.Controls.Add(this.label17);
            this.panelMain.Controls.Add(this.comboBoxTarget);
            this.panelMain.Controls.Add(this.numericUpDownMultiActionProb);
            this.panelMain.Controls.Add(this.numericUpDownMaxMultiAction);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.checkedListBoxPopWeather);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.listBoxPop);
            this.panelMain.Controls.Add(this.label19);
            this.panelMain.Controls.Add(this.groupBox2);
            this.panelMain.Controls.Add(this.label9);
            this.panelMain.Controls.Add(this.label6);
            this.panelMain.Controls.Add(this.comboBoxCategory);
            this.panelMain.Controls.Add(this.textBoxName);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.textBoxNo);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "モンスター";
            // 
            // checkedListBoxPopWeather
            // 
            this.checkedListBoxPopWeather.CheckOnClick = true;
            this.checkedListBoxPopWeather.FormattingEnabled = true;
            this.checkedListBoxPopWeather.Location = new System.Drawing.Point(425, 440);
            this.checkedListBoxPopWeather.Name = "checkedListBoxPopWeather";
            this.checkedListBoxPopWeather.Size = new System.Drawing.Size(122, 158);
            this.checkedListBoxPopWeather.TabIndex = 74;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(423, 425);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 12);
            this.label3.TabIndex = 73;
            this.label3.Text = "出現天候条件:";
            // 
            // listBoxPop
            // 
            this.listBoxPop.FormattingEnabled = true;
            this.listBoxPop.ItemHeight = 12;
            this.listBoxPop.Location = new System.Drawing.Point(6, 440);
            this.listBoxPop.Name = "listBoxPop";
            this.listBoxPop.Size = new System.Drawing.Size(415, 160);
            this.listBoxPop.TabIndex = 70;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(4, 425);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(55, 12);
            this.label19.TabIndex = 69;
            this.label19.Text = "出現場所:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 84);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 12);
            this.label16.TabIndex = 1;
            this.label16.Text = "ターゲット:";
            // 
            // comboBoxTarget
            // 
            this.comboBoxTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTarget.FormattingEnabled = true;
            this.comboBoxTarget.Location = new System.Drawing.Point(6, 99);
            this.comboBoxTarget.Name = "comboBoxTarget";
            this.comboBoxTarget.Size = new System.Drawing.Size(153, 20);
            this.comboBoxTarget.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSerif);
            this.groupBox2.Controls.Add(this.buttonCommonItemBox);
            this.groupBox2.Controls.Add(this.buttonCommonElement);
            this.groupBox2.Controls.Add(this.buttonCommonAction);
            this.groupBox2.Location = new System.Drawing.Point(432, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 87);
            this.groupBox2.TabIndex = 66;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "個別データ設定";
            // 
            // buttonSerif
            // 
            this.buttonSerif.Location = new System.Drawing.Point(96, 47);
            this.buttonSerif.Name = "buttonSerif";
            this.buttonSerif.Size = new System.Drawing.Size(75, 23);
            this.buttonSerif.TabIndex = 12;
            this.buttonSerif.Text = "セリフ設定";
            this.buttonSerif.UseVisualStyleBackColor = true;
            this.buttonSerif.Click += new System.EventHandler(this.buttonSerif_Click);
            // 
            // buttonCommonItemBox
            // 
            this.buttonCommonItemBox.Location = new System.Drawing.Point(96, 18);
            this.buttonCommonItemBox.Name = "buttonCommonItemBox";
            this.buttonCommonItemBox.Size = new System.Drawing.Size(75, 23);
            this.buttonCommonItemBox.TabIndex = 5;
            this.buttonCommonItemBox.Text = "所有品設定";
            this.buttonCommonItemBox.UseVisualStyleBackColor = true;
            this.buttonCommonItemBox.Click += new System.EventHandler(this.buttonCommonItemBox_Click);
            // 
            // buttonCommonElement
            // 
            this.buttonCommonElement.Location = new System.Drawing.Point(6, 47);
            this.buttonCommonElement.Name = "buttonCommonElement";
            this.buttonCommonElement.Size = new System.Drawing.Size(75, 23);
            this.buttonCommonElement.TabIndex = 4;
            this.buttonCommonElement.Text = "属性設定";
            this.buttonCommonElement.UseVisualStyleBackColor = true;
            this.buttonCommonElement.Click += new System.EventHandler(this.buttonCommonElement_Click);
            // 
            // buttonCommonAction
            // 
            this.buttonCommonAction.Location = new System.Drawing.Point(6, 18);
            this.buttonCommonAction.Name = "buttonCommonAction";
            this.buttonCommonAction.Size = new System.Drawing.Size(75, 23);
            this.buttonCommonAction.TabIndex = 2;
            this.buttonCommonAction.Text = "行動設定";
            this.buttonCommonAction.UseVisualStyleBackColor = true;
            this.buttonCommonAction.Click += new System.EventHandler(this.buttonCommonAction_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(163, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 12);
            this.label9.TabIndex = 64;
            this.label9.Text = "隊列:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 60;
            this.label6.Text = "種族カテゴリ:";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(6, 60);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(127, 20);
            this.comboBoxCategory.TabIndex = 59;
            // 
            // textBoxName
            // 
            this.textBoxName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxName.Location = new System.Drawing.Point(92, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(329, 19);
            this.textBoxName.TabIndex = 52;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 53;
            this.label1.Text = "No.:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 54;
            this.label2.Text = "名称:";
            // 
            // textBoxNo
            // 
            this.textBoxNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxNo.Location = new System.Drawing.Point(6, 24);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.ReadOnly = true;
            this.textBoxNo.Size = new System.Drawing.Size(80, 19);
            this.textBoxNo.TabIndex = 51;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(290, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 12);
            this.label5.TabIndex = 76;
            this.label5.Text = "連撃数:";
            // 
            // numericUpDownMaxMultiAction
            // 
            this.numericUpDownMaxMultiAction.Location = new System.Drawing.Point(291, 61);
            this.numericUpDownMaxMultiAction.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownMaxMultiAction.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxMultiAction.Name = "numericUpDownMaxMultiAction";
            this.numericUpDownMaxMultiAction.Size = new System.Drawing.Size(64, 19);
            this.numericUpDownMaxMultiAction.TabIndex = 16;
            this.numericUpDownMaxMultiAction.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownMultiActionProb
            // 
            this.numericUpDownMultiActionProb.Location = new System.Drawing.Point(361, 61);
            this.numericUpDownMultiActionProb.Name = "numericUpDownMultiActionProb";
            this.numericUpDownMultiActionProb.Size = new System.Drawing.Size(60, 19);
            this.numericUpDownMultiActionProb.TabIndex = 77;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(361, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 12);
            this.label17.TabIndex = 78;
            this.label17.Text = "連撃率:";
            // 
            // comboBoxFormation
            // 
            this.comboBoxFormation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormation.FormattingEnabled = true;
            this.comboBoxFormation.Items.AddRange(new object[] {
            "前列",
            "後列"});
            this.comboBoxFormation.Location = new System.Drawing.Point(165, 99);
            this.comboBoxFormation.Name = "comboBoxFormation";
            this.comboBoxFormation.Size = new System.Drawing.Size(119, 20);
            this.comboBoxFormation.TabIndex = 79;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 12);
            this.label7.TabIndex = 80;
            this.label7.Text = "能力:";
            // 
            // dataGridViewBattleAbility
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            this.dataGridViewBattleAbility.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewBattleAbility.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewBattleAbility.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewBattleAbility.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBattleAbility.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnMonsterID,
            this.columnLevel,
            this.columnHP,
            this.columnMP,
            this.columnSTR,
            this.columnAGI,
            this.columnMAG,
            this.columnUNQ,
            this.columnATK,
            this.columnSubATK,
            this.columnDFE,
            this.columnMGR,
            this.columnAVD,
            this.columnEXP});
            this.dataGridViewBattleAbility.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewBattleAbility.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dataGridViewBattleAbility.Location = new System.Drawing.Point(8, 138);
            this.dataGridViewBattleAbility.MultiSelect = false;
            this.dataGridViewBattleAbility.Name = "dataGridViewBattleAbility";
            this.dataGridViewBattleAbility.RowTemplate.Height = 21;
            this.dataGridViewBattleAbility.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBattleAbility.Size = new System.Drawing.Size(637, 284);
            this.dataGridViewBattleAbility.TabIndex = 81;
            this.dataGridViewBattleAbility.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBattleAbility_CellClick);
            this.dataGridViewBattleAbility.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBattleAbility_CellEnter);
            this.dataGridViewBattleAbility.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewBattleAbility_DefaultValuesNeeded);
            // 
            // columnMonsterID
            // 
            this.columnMonsterID.HeaderText = "ID";
            this.columnMonsterID.Name = "columnMonsterID";
            this.columnMonsterID.Visible = false;
            // 
            // columnLevel
            // 
            this.columnLevel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnLevel.HeaderText = "Lv";
            this.columnLevel.Name = "columnLevel";
            this.columnLevel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnLevel.Width = 40;
            // 
            // columnHP
            // 
            this.columnHP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnHP.HeaderText = "HP";
            this.columnHP.Name = "columnHP";
            this.columnHP.Width = 70;
            // 
            // columnMP
            // 
            this.columnMP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnMP.HeaderText = "MP";
            this.columnMP.Name = "columnMP";
            this.columnMP.Width = 70;
            // 
            // columnSTR
            // 
            this.columnSTR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnSTR.HeaderText = "STR";
            this.columnSTR.Name = "columnSTR";
            this.columnSTR.Width = 40;
            // 
            // columnAGI
            // 
            this.columnAGI.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnAGI.HeaderText = "AGI";
            this.columnAGI.Name = "columnAGI";
            this.columnAGI.Width = 40;
            // 
            // columnMAG
            // 
            this.columnMAG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnMAG.HeaderText = "MAG";
            this.columnMAG.Name = "columnMAG";
            this.columnMAG.Width = 40;
            // 
            // columnUNQ
            // 
            this.columnUNQ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnUNQ.HeaderText = "UNQ";
            this.columnUNQ.Name = "columnUNQ";
            this.columnUNQ.Width = 40;
            // 
            // columnATK
            // 
            this.columnATK.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnATK.HeaderText = "ATK";
            this.columnATK.Name = "columnATK";
            this.columnATK.Width = 40;
            // 
            // columnSubATK
            // 
            this.columnSubATK.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnSubATK.HeaderText = "SATK";
            this.columnSubATK.Name = "columnSubATK";
            this.columnSubATK.Width = 40;
            // 
            // columnDFE
            // 
            this.columnDFE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnDFE.HeaderText = "DFE";
            this.columnDFE.Name = "columnDFE";
            this.columnDFE.Width = 40;
            // 
            // columnMGR
            // 
            this.columnMGR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnMGR.HeaderText = "MGR";
            this.columnMGR.Name = "columnMGR";
            this.columnMGR.Width = 40;
            // 
            // columnAVD
            // 
            this.columnAVD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnAVD.HeaderText = "AVD";
            this.columnAVD.Name = "columnAVD";
            this.columnAVD.Width = 40;
            // 
            // columnEXP
            // 
            this.columnEXP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnEXP.HeaderText = "EXP";
            this.columnEXP.Name = "columnEXP";
            this.columnEXP.Width = 70;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(289, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 12);
            this.label8.TabIndex = 82;
            this.label8.Text = "モンスター区分:";
            // 
            // comboBoxMonsterKb
            // 
            this.comboBoxMonsterKb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMonsterKb.FormattingEnabled = true;
            this.comboBoxMonsterKb.Items.AddRange(new object[] {
            "通常",
            "レア",
            "ボス"});
            this.comboBoxMonsterKb.Location = new System.Drawing.Point(291, 99);
            this.comboBoxMonsterKb.Name = "comboBoxMonsterKb";
            this.comboBoxMonsterKb.Size = new System.Drawing.Size(98, 20);
            this.comboBoxMonsterKb.TabIndex = 83;
            // 
            // MonsterDataPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MonsterDataPanel";
            this.Controls.SetChildIndex(this.panelList, 0);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelList.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxMultiAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMultiActionProb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBattleAbility)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxPopWeather;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxPop;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBoxTarget;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSerif;
        private System.Windows.Forms.Button buttonCommonItemBox;
        private System.Windows.Forms.Button buttonCommonElement;
        private System.Windows.Forms.Button buttonCommonAction;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numericUpDownMultiActionProb;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxMultiAction;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxFormation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridViewBattleAbility;
        private System.Windows.Forms.ComboBox comboBoxMonsterKb;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMonsterID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnHP;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMP;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSTR;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAGI;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUNQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnATK;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSubATK;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDFE;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMGR;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAVD;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnEXP;
    }
}
