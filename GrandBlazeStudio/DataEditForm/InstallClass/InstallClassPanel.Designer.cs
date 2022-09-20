namespace DataEditForm.InstallClass
{
    partial class InstallClassPanel
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAbMax = new System.Windows.Forms.TextBox();
            this.comboBoxUpUNQ = new System.Windows.Forms.ComboBox();
            this.comboBoxUpMAG = new System.Windows.Forms.ComboBox();
            this.comboBoxUpAGI = new System.Windows.Forms.ComboBox();
            this.comboBoxUpSTR = new System.Windows.Forms.ComboBox();
            this.comboBoxUpMP = new System.Windows.Forms.ComboBox();
            this.comboBoxUpHP = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxInstallClassComment = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewSkill = new System.Windows.Forms.DataGridView();
            this.columnRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnEffectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOnlyMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStripEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain.SuspendLayout();
            this.panelList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSkill)).BeginInit();
            this.contextMenuStripEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dataGridViewSkill);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Controls.Add(this.textBoxInstallClassComment);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.textBoxName);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.textBoxNo);
            this.panelMain.Controls.Add(this.label1);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "クラス";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxAbMax);
            this.groupBox1.Controls.Add(this.comboBoxUpUNQ);
            this.groupBox1.Controls.Add(this.comboBoxUpMAG);
            this.groupBox1.Controls.Add(this.comboBoxUpAGI);
            this.groupBox1.Controls.Add(this.comboBoxUpSTR);
            this.groupBox1.Controls.Add(this.comboBoxUpMP);
            this.groupBox1.Controls.Add(this.comboBoxUpHP);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(8, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 100);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "能力成長";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 12);
            this.label3.TabIndex = 34;
            this.label3.Text = "ST合計:";
            // 
            // textBoxAbMax
            // 
            this.textBoxAbMax.Location = new System.Drawing.Point(231, 29);
            this.textBoxAbMax.Name = "textBoxAbMax";
            this.textBoxAbMax.ReadOnly = true;
            this.textBoxAbMax.Size = new System.Drawing.Size(49, 19);
            this.textBoxAbMax.TabIndex = 33;
            // 
            // comboBoxUpUNQ
            // 
            this.comboBoxUpUNQ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpUNQ.FormattingEnabled = true;
            this.comboBoxUpUNQ.Items.AddRange(new object[] {
            "G",
            "F",
            "E",
            "D",
            "C",
            "B",
            "A"});
            this.comboBoxUpUNQ.Location = new System.Drawing.Point(216, 66);
            this.comboBoxUpUNQ.Name = "comboBoxUpUNQ";
            this.comboBoxUpUNQ.Size = new System.Drawing.Size(64, 20);
            this.comboBoxUpUNQ.TabIndex = 32;
            this.comboBoxUpUNQ.SelectedIndexChanged += new System.EventHandler(this.comboBoxUp_SelectedIndexChanged);
            // 
            // comboBoxUpMAG
            // 
            this.comboBoxUpMAG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpMAG.FormattingEnabled = true;
            this.comboBoxUpMAG.Items.AddRange(new object[] {
            "G",
            "F",
            "E",
            "D",
            "C",
            "B",
            "A"});
            this.comboBoxUpMAG.Location = new System.Drawing.Point(146, 67);
            this.comboBoxUpMAG.Name = "comboBoxUpMAG";
            this.comboBoxUpMAG.Size = new System.Drawing.Size(64, 20);
            this.comboBoxUpMAG.TabIndex = 28;
            this.comboBoxUpMAG.SelectedIndexChanged += new System.EventHandler(this.comboBoxUp_SelectedIndexChanged);
            // 
            // comboBoxUpAGI
            // 
            this.comboBoxUpAGI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpAGI.FormattingEnabled = true;
            this.comboBoxUpAGI.Items.AddRange(new object[] {
            "G",
            "F",
            "E",
            "D",
            "C",
            "B",
            "A"});
            this.comboBoxUpAGI.Location = new System.Drawing.Point(76, 66);
            this.comboBoxUpAGI.Name = "comboBoxUpAGI";
            this.comboBoxUpAGI.Size = new System.Drawing.Size(64, 20);
            this.comboBoxUpAGI.TabIndex = 26;
            this.comboBoxUpAGI.SelectedIndexChanged += new System.EventHandler(this.comboBoxUp_SelectedIndexChanged);
            // 
            // comboBoxUpSTR
            // 
            this.comboBoxUpSTR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpSTR.FormattingEnabled = true;
            this.comboBoxUpSTR.Items.AddRange(new object[] {
            "G",
            "F",
            "E",
            "D",
            "C",
            "B",
            "A"});
            this.comboBoxUpSTR.Location = new System.Drawing.Point(6, 66);
            this.comboBoxUpSTR.Name = "comboBoxUpSTR";
            this.comboBoxUpSTR.Size = new System.Drawing.Size(64, 20);
            this.comboBoxUpSTR.TabIndex = 25;
            this.comboBoxUpSTR.SelectedIndexChanged += new System.EventHandler(this.comboBoxUp_SelectedIndexChanged);
            // 
            // comboBoxUpMP
            // 
            this.comboBoxUpMP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpMP.FormattingEnabled = true;
            this.comboBoxUpMP.Items.AddRange(new object[] {
            "G",
            "F",
            "E",
            "D",
            "C",
            "B",
            "A"});
            this.comboBoxUpMP.Location = new System.Drawing.Point(76, 29);
            this.comboBoxUpMP.Name = "comboBoxUpMP";
            this.comboBoxUpMP.Size = new System.Drawing.Size(64, 20);
            this.comboBoxUpMP.TabIndex = 23;
            this.comboBoxUpMP.SelectedIndexChanged += new System.EventHandler(this.comboBoxUp_SelectedIndexChanged);
            // 
            // comboBoxUpHP
            // 
            this.comboBoxUpHP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpHP.FormattingEnabled = true;
            this.comboBoxUpHP.Items.AddRange(new object[] {
            "G",
            "F",
            "E",
            "D",
            "C",
            "B",
            "A"});
            this.comboBoxUpHP.Location = new System.Drawing.Point(6, 29);
            this.comboBoxUpHP.Name = "comboBoxUpHP";
            this.comboBoxUpHP.Size = new System.Drawing.Size(64, 20);
            this.comboBoxUpHP.TabIndex = 22;
            this.comboBoxUpHP.SelectedIndexChanged += new System.EventHandler(this.comboBoxUp_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(214, 51);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 12);
            this.label16.TabIndex = 20;
            this.label16.Text = "UNQ:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(144, 53);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 12);
            this.label13.TabIndex = 14;
            this.label13.Text = "MAG:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(74, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 12);
            this.label12.TabIndex = 12;
            this.label12.Text = "AGI:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "STR:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(76, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "MP:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "HP:";
            // 
            // textBoxInstallClassComment
            // 
            this.textBoxInstallClassComment.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxInstallClassComment.Location = new System.Drawing.Point(6, 61);
            this.textBoxInstallClassComment.Multiline = true;
            this.textBoxInstallClassComment.Name = "textBoxInstallClassComment";
            this.textBoxInstallClassComment.Size = new System.Drawing.Size(412, 100);
            this.textBoxInstallClassComment.TabIndex = 44;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 12);
            this.label5.TabIndex = 43;
            this.label5.Text = "インストールクラス解説:";
            // 
            // textBoxName
            // 
            this.textBoxName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxName.Location = new System.Drawing.Point(92, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(326, 19);
            this.textBoxName.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 42;
            this.label2.Text = "名称:";
            // 
            // textBoxNo
            // 
            this.textBoxNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxNo.Location = new System.Drawing.Point(6, 24);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.ReadOnly = true;
            this.textBoxNo.Size = new System.Drawing.Size(80, 19);
            this.textBoxNo.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "No.:";
            // 
            // dataGridViewSkill
            // 
            this.dataGridViewSkill.AllowUserToAddRows = false;
            this.dataGridViewSkill.AllowUserToDeleteRows = false;
            this.dataGridViewSkill.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            this.dataGridViewSkill.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSkill.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSkill.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewSkill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewSkill.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewSkill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSkill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnRank,
            this.columnEffectName,
            this.columnOnlyMode});
            this.dataGridViewSkill.Location = new System.Drawing.Point(8, 285);
            this.dataGridViewSkill.MultiSelect = false;
            this.dataGridViewSkill.Name = "dataGridViewSkill";
            this.dataGridViewSkill.ReadOnly = true;
            this.dataGridViewSkill.RowHeadersVisible = false;
            this.dataGridViewSkill.RowTemplate.Height = 21;
            this.dataGridViewSkill.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSkill.Size = new System.Drawing.Size(415, 316);
            this.dataGridViewSkill.TabIndex = 156;
            this.dataGridViewSkill.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSkill_CellDoubleClick);
            this.dataGridViewSkill.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewSkill_CellFormatting);
            this.dataGridViewSkill.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewSkill_CellMouseClick);
            this.dataGridViewSkill.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewSkill_KeyDown);
            this.dataGridViewSkill.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewSkill_MouseUp);
            // 
            // columnRank
            // 
            this.columnRank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnRank.HeaderText = "レベル";
            this.columnRank.Name = "columnRank";
            this.columnRank.ReadOnly = true;
            this.columnRank.Width = 60;
            // 
            // columnEffectName
            // 
            this.columnEffectName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnEffectName.HeaderText = "スキル名";
            this.columnEffectName.Name = "columnEffectName";
            this.columnEffectName.ReadOnly = true;
            // 
            // columnOnlyMode
            // 
            this.columnOnlyMode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnOnlyMode.HeaderText = "専用";
            this.columnOnlyMode.Name = "columnOnlyMode";
            this.columnOnlyMode.ReadOnly = true;
            this.columnOnlyMode.Width = 60;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 12);
            this.label4.TabIndex = 155;
            this.label4.Text = "習得スキルリスト:";
            // 
            // contextMenuStripEdit
            // 
            this.contextMenuStripEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemItemEdit,
            this.toolStripMenuItemAdd,
            this.toolStripMenuItemItemDelete});
            this.contextMenuStripEdit.Name = "contextMenuStripEdit";
            this.contextMenuStripEdit.Size = new System.Drawing.Size(118, 70);
            this.contextMenuStripEdit.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripEdit_Opening);
            // 
            // toolStripMenuItemItemEdit
            // 
            this.toolStripMenuItemItemEdit.Enabled = false;
            this.toolStripMenuItemItemEdit.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripMenuItemItemEdit.Name = "toolStripMenuItemItemEdit";
            this.toolStripMenuItemItemEdit.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemItemEdit.Text = "編集(&E)";
            this.toolStripMenuItemItemEdit.Click += new System.EventHandler(this.toolStripMenuItemItemEdit_Click);
            // 
            // toolStripMenuItemAdd
            // 
            this.toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
            this.toolStripMenuItemAdd.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemAdd.Text = "追加(&A)";
            this.toolStripMenuItemAdd.Click += new System.EventHandler(this.toolStripMenuItemAdd_Click_1);
            // 
            // toolStripMenuItemItemDelete
            // 
            this.toolStripMenuItemItemDelete.Name = "toolStripMenuItemItemDelete";
            this.toolStripMenuItemItemDelete.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemItemDelete.Text = "削除(&D)";
            this.toolStripMenuItemItemDelete.Click += new System.EventHandler(this.toolStripMenuItemItemDelete_Click);
            // 
            // InstallClassPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "InstallClassPanel";
            this.Controls.SetChildIndex(this.panelList, 0);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSkill)).EndInit();
            this.contextMenuStripEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAbMax;
        private System.Windows.Forms.ComboBox comboBoxUpUNQ;
        private System.Windows.Forms.ComboBox comboBoxUpMAG;
        private System.Windows.Forms.ComboBox comboBoxUpAGI;
        private System.Windows.Forms.ComboBox comboBoxUpSTR;
        private System.Windows.Forms.ComboBox comboBoxUpMP;
        private System.Windows.Forms.ComboBox comboBoxUpHP;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxInstallClassComment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewSkill;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemItemEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemItemDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRank;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnEffectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOnlyMode;
    }
}
