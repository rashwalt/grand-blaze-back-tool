namespace DataEditForm.Quest
{
    partial class QuestEditPanel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxClientName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.dataGridViewComment = new System.Windows.Forms.DataGridView();
            this.columnStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripComment = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemComEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemComAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemComDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownPickLevel = new System.Windows.Forms.NumericUpDown();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.dataGridViewMarkList = new System.Windows.Forms.DataGridView();
            this.columnMarkName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnHideMark = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.contextMenuStripMark = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemMarkEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMarkAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMarkDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxValid = new System.Windows.Forms.CheckBox();
            this.checkBoxHide = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownBC = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownClassLevel = new System.Windows.Forms.NumericUpDown();
            this.comboBoxClass = new System.Windows.Forms.ComboBox();
            this.numericUpDownSP = new System.Windows.Forms.NumericUpDown();
            this.comboBoxCompQuest = new System.Windows.Forms.ComboBox();
            this.comboBoxOfferQuest = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyItem = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComment)).BeginInit();
            this.contextMenuStripComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPickLevel)).BeginInit();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMarkList)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.contextMenuStripMark.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClassLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSP)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBox3);
            this.panelMain.Controls.Add(this.checkBoxHide);
            this.panelMain.Controls.Add(this.checkBoxValid);
            this.panelMain.Controls.Add(this.groupBox2);
            this.panelMain.Controls.Add(this.groupBox);
            this.panelMain.Controls.Add(this.numericUpDownPickLevel);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.comboBoxType);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.textBoxClientName);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.textBoxName);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.textBoxNo);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "クエスト";
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(447, 24);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(200, 20);
            this.comboBoxType.TabIndex = 35;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(445, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 12);
            this.label5.TabIndex = 34;
            this.label5.Text = "タイプ:";
            // 
            // textBoxClientName
            // 
            this.textBoxClientName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxClientName.Location = new System.Drawing.Point(6, 61);
            this.textBoxClientName.Name = "textBoxClientName";
            this.textBoxClientName.Size = new System.Drawing.Size(212, 19);
            this.textBoxClientName.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "依頼者:";
            // 
            // textBoxName
            // 
            this.textBoxName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxName.Location = new System.Drawing.Point(92, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(349, 19);
            this.textBoxName.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 28;
            this.label1.Text = "No.:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "名称:";
            // 
            // textBoxNo
            // 
            this.textBoxNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxNo.Location = new System.Drawing.Point(6, 24);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.ReadOnly = true;
            this.textBoxNo.Size = new System.Drawing.Size(80, 19);
            this.textBoxNo.TabIndex = 29;
            // 
            // dataGridViewComment
            // 
            this.dataGridViewComment.AllowUserToAddRows = false;
            this.dataGridViewComment.AllowUserToDeleteRows = false;
            this.dataGridViewComment.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            this.dataGridViewComment.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewComment.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewComment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewComment.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewComment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnStep,
            this.columnComment});
            this.dataGridViewComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewComment.Location = new System.Drawing.Point(3, 15);
            this.dataGridViewComment.MultiSelect = false;
            this.dataGridViewComment.Name = "dataGridViewComment";
            this.dataGridViewComment.ReadOnly = true;
            this.dataGridViewComment.RowHeadersVisible = false;
            this.dataGridViewComment.RowTemplate.Height = 21;
            this.dataGridViewComment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewComment.Size = new System.Drawing.Size(294, 395);
            this.dataGridViewComment.TabIndex = 41;
            this.dataGridViewComment.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComment_CellDoubleClick);
            this.dataGridViewComment.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewComment_CellFormatting);
            this.dataGridViewComment.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewComment_CellMouseClick);
            this.dataGridViewComment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewComment_KeyDown);
            this.dataGridViewComment.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewComment_MouseUp);
            // 
            // columnStep
            // 
            this.columnStep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnStep.FillWeight = 60F;
            this.columnStep.HeaderText = "進捗";
            this.columnStep.Name = "columnStep";
            this.columnStep.ReadOnly = true;
            this.columnStep.Width = 60;
            // 
            // columnComment
            // 
            this.columnComment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnComment.DefaultCellStyle = dataGridViewCellStyle2;
            this.columnComment.FillWeight = 80F;
            this.columnComment.HeaderText = "コメント";
            this.columnComment.Name = "columnComment";
            this.columnComment.ReadOnly = true;
            // 
            // contextMenuStripComment
            // 
            this.contextMenuStripComment.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemComEdit,
            this.toolStripMenuItemComAdd,
            this.toolStripMenuItemComDelete});
            this.contextMenuStripComment.Name = "contextMenuStripMark";
            this.contextMenuStripComment.Size = new System.Drawing.Size(144, 70);
            // 
            // toolStripMenuItemComEdit
            // 
            this.toolStripMenuItemComEdit.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripMenuItemComEdit.Name = "toolStripMenuItemComEdit";
            this.toolStripMenuItemComEdit.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemComEdit.Text = "編集(&E)";
            this.toolStripMenuItemComEdit.Click += new System.EventHandler(this.toolStripMenuItemComEdit_Click);
            // 
            // toolStripMenuItemComAdd
            // 
            this.toolStripMenuItemComAdd.Name = "toolStripMenuItemComAdd";
            this.toolStripMenuItemComAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.toolStripMenuItemComAdd.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemComAdd.Text = "追加(&A)";
            this.toolStripMenuItemComAdd.Click += new System.EventHandler(this.toolStripMenuItemComAdd_Click);
            // 
            // toolStripMenuItemComDelete
            // 
            this.toolStripMenuItemComDelete.Name = "toolStripMenuItemComDelete";
            this.toolStripMenuItemComDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItemComDelete.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemComDelete.Text = "削除(&D)";
            this.toolStripMenuItemComDelete.Click += new System.EventHandler(this.toolStripMenuItemComDelete_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 12);
            this.label3.TabIndex = 42;
            this.label3.Text = "推奨レベル:";
            // 
            // numericUpDownPickLevel
            // 
            this.numericUpDownPickLevel.Location = new System.Drawing.Point(224, 62);
            this.numericUpDownPickLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPickLevel.Name = "numericUpDownPickLevel";
            this.numericUpDownPickLevel.Size = new System.Drawing.Size(82, 19);
            this.numericUpDownPickLevel.TabIndex = 43;
            this.numericUpDownPickLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.dataGridViewMarkList);
            this.groupBox.Location = new System.Drawing.Point(6, 188);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(341, 413);
            this.groupBox.TabIndex = 59;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "関連マーク";
            // 
            // dataGridViewMarkList
            // 
            this.dataGridViewMarkList.AllowUserToAddRows = false;
            this.dataGridViewMarkList.AllowUserToDeleteRows = false;
            this.dataGridViewMarkList.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            this.dataGridViewMarkList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewMarkList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMarkList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewMarkList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewMarkList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewMarkList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMarkList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnMarkName,
            this.columnHideMark,
            this.columnType});
            this.dataGridViewMarkList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMarkList.Location = new System.Drawing.Point(3, 15);
            this.dataGridViewMarkList.MultiSelect = false;
            this.dataGridViewMarkList.Name = "dataGridViewMarkList";
            this.dataGridViewMarkList.ReadOnly = true;
            this.dataGridViewMarkList.RowHeadersVisible = false;
            this.dataGridViewMarkList.RowTemplate.Height = 21;
            this.dataGridViewMarkList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMarkList.Size = new System.Drawing.Size(335, 395);
            this.dataGridViewMarkList.TabIndex = 2;
            this.dataGridViewMarkList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMarkList_CellDoubleClick);
            this.dataGridViewMarkList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewMarkList_CellFormatting);
            this.dataGridViewMarkList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewMarkList_CellMouseClick);
            this.dataGridViewMarkList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewMarkList_KeyDown);
            this.dataGridViewMarkList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewMarkList_MouseUp);
            // 
            // columnMarkName
            // 
            this.columnMarkName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnMarkName.HeaderText = "マーク名";
            this.columnMarkName.Name = "columnMarkName";
            this.columnMarkName.ReadOnly = true;
            this.columnMarkName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // columnHideMark
            // 
            this.columnHideMark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnHideMark.FillWeight = 40F;
            this.columnHideMark.HeaderText = "隠し";
            this.columnHideMark.Name = "columnHideMark";
            this.columnHideMark.ReadOnly = true;
            this.columnHideMark.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnHideMark.Width = 40;
            // 
            // columnType
            // 
            this.columnType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnType.FillWeight = 70F;
            this.columnType.HeaderText = "タイプ";
            this.columnType.Name = "columnType";
            this.columnType.ReadOnly = true;
            this.columnType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnType.Width = 70;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewComment);
            this.groupBox2.Location = new System.Drawing.Point(353, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 413);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "依頼進捗テキスト";
            // 
            // contextMenuStripMark
            // 
            this.contextMenuStripMark.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMarkEdit,
            this.toolStripMenuItemMarkAdd,
            this.toolStripMenuItemMarkDelete});
            this.contextMenuStripMark.Name = "contextMenuStripMark";
            this.contextMenuStripMark.Size = new System.Drawing.Size(153, 92);
            // 
            // toolStripMenuItemMarkEdit
            // 
            this.toolStripMenuItemMarkEdit.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripMenuItemMarkEdit.Name = "toolStripMenuItemMarkEdit";
            this.toolStripMenuItemMarkEdit.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemMarkEdit.Text = "編集(&E)";
            this.toolStripMenuItemMarkEdit.Click += new System.EventHandler(this.toolStripMenuItemMarkEdit_Click);
            // 
            // toolStripMenuItemMarkAdd
            // 
            this.toolStripMenuItemMarkAdd.Name = "toolStripMenuItemMarkAdd";
            this.toolStripMenuItemMarkAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.toolStripMenuItemMarkAdd.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemMarkAdd.Text = "追加(&A)";
            this.toolStripMenuItemMarkAdd.Click += new System.EventHandler(this.toolStripMenuItemMarkAdd_Click);
            // 
            // toolStripMenuItemMarkDelete
            // 
            this.toolStripMenuItemMarkDelete.Name = "toolStripMenuItemMarkDelete";
            this.toolStripMenuItemMarkDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuItemMarkDelete.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemMarkDelete.Text = "削除(&D)";
            this.toolStripMenuItemMarkDelete.Click += new System.EventHandler(this.toolStripMenuItemMarkDelete_Click);
            // 
            // checkBoxValid
            // 
            this.checkBoxValid.AutoSize = true;
            this.checkBoxValid.Location = new System.Drawing.Point(527, 46);
            this.checkBoxValid.Name = "checkBoxValid";
            this.checkBoxValid.Size = new System.Drawing.Size(85, 16);
            this.checkBoxValid.TabIndex = 61;
            this.checkBoxValid.Text = "公開エリア？";
            this.checkBoxValid.UseVisualStyleBackColor = true;
            // 
            // checkBoxHide
            // 
            this.checkBoxHide.AutoSize = true;
            this.checkBoxHide.Location = new System.Drawing.Point(527, 65);
            this.checkBoxHide.Name = "checkBoxHide";
            this.checkBoxHide.Size = new System.Drawing.Size(79, 16);
            this.checkBoxHide.TabIndex = 62;
            this.checkBoxHide.Text = "隠しクエスト";
            this.checkBoxHide.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.numericUpDownBC);
            this.groupBox3.Controls.Add(this.numericUpDownClassLevel);
            this.groupBox3.Controls.Add(this.comboBoxClass);
            this.groupBox3.Controls.Add(this.numericUpDownSP);
            this.groupBox3.Controls.Add(this.comboBoxCompQuest);
            this.groupBox3.Controls.Add(this.comboBoxOfferQuest);
            this.groupBox3.Controls.Add(this.comboBoxKeyItem);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(9, 86);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(481, 96);
            this.groupBox3.TabIndex = 63;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "受領条件";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(233, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "必要BC:";
            // 
            // numericUpDownBC
            // 
            this.numericUpDownBC.Location = new System.Drawing.Point(235, 68);
            this.numericUpDownBC.Name = "numericUpDownBC";
            this.numericUpDownBC.Size = new System.Drawing.Size(45, 19);
            this.numericUpDownBC.TabIndex = 13;
            // 
            // numericUpDownClassLevel
            // 
            this.numericUpDownClassLevel.Location = new System.Drawing.Point(133, 68);
            this.numericUpDownClassLevel.Name = "numericUpDownClassLevel";
            this.numericUpDownClassLevel.Size = new System.Drawing.Size(45, 19);
            this.numericUpDownClassLevel.TabIndex = 12;
            // 
            // comboBoxClass
            // 
            this.comboBoxClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClass.FormattingEnabled = true;
            this.comboBoxClass.Location = new System.Drawing.Point(6, 68);
            this.comboBoxClass.Name = "comboBoxClass";
            this.comboBoxClass.Size = new System.Drawing.Size(121, 20);
            this.comboBoxClass.TabIndex = 11;
            // 
            // numericUpDownSP
            // 
            this.numericUpDownSP.Location = new System.Drawing.Point(184, 68);
            this.numericUpDownSP.Name = "numericUpDownSP";
            this.numericUpDownSP.Size = new System.Drawing.Size(45, 19);
            this.numericUpDownSP.TabIndex = 2;
            // 
            // comboBoxCompQuest
            // 
            this.comboBoxCompQuest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCompQuest.FormattingEnabled = true;
            this.comboBoxCompQuest.Location = new System.Drawing.Point(303, 30);
            this.comboBoxCompQuest.Name = "comboBoxCompQuest";
            this.comboBoxCompQuest.Size = new System.Drawing.Size(164, 20);
            this.comboBoxCompQuest.TabIndex = 10;
            // 
            // comboBoxOfferQuest
            // 
            this.comboBoxOfferQuest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOfferQuest.FormattingEnabled = true;
            this.comboBoxOfferQuest.Location = new System.Drawing.Point(133, 30);
            this.comboBoxOfferQuest.Name = "comboBoxOfferQuest";
            this.comboBoxOfferQuest.Size = new System.Drawing.Size(164, 20);
            this.comboBoxOfferQuest.TabIndex = 8;
            // 
            // comboBoxKeyItem
            // 
            this.comboBoxKeyItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyItem.FormattingEnabled = true;
            this.comboBoxKeyItem.Location = new System.Drawing.Point(6, 30);
            this.comboBoxKeyItem.Name = "comboBoxKeyItem";
            this.comboBoxKeyItem.Size = new System.Drawing.Size(121, 20);
            this.comboBoxKeyItem.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(301, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "完遂済クエスト:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(184, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "最低SP:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "クラス:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(131, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "受領済クエスト:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "貴重品:";
            // 
            // QuestEditPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "QuestEditPanel";
            this.Controls.SetChildIndex(this.panelList, 0);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComment)).EndInit();
            this.contextMenuStripComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPickLevel)).EndInit();
            this.groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMarkList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStripMark.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClassLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxClientName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNo;
        private System.Windows.Forms.DataGridView dataGridViewComment;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripComment;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemComEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemComAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemComDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnComment;
        private System.Windows.Forms.NumericUpDown numericUpDownPickLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.DataGridView dataGridViewMarkList;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMarkName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnHideMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnType;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMark;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMarkEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMarkAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMarkDelete;
        private System.Windows.Forms.CheckBox checkBoxHide;
        private System.Windows.Forms.CheckBox checkBoxValid;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownClassLevel;
        private System.Windows.Forms.ComboBox comboBoxClass;
        private System.Windows.Forms.NumericUpDown numericUpDownSP;
        private System.Windows.Forms.ComboBox comboBoxCompQuest;
        private System.Windows.Forms.ComboBox comboBoxOfferQuest;
        private System.Windows.Forms.ComboBox comboBoxKeyItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownBC;
    }
}
