namespace CommonFormLibrary.CommonDialog
{
    partial class SkillSelectDialog
    {
        /// <summary>
        /// �K�v�ȃf�U�C�i�ϐ��ł��B
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// �g�p���̃��\�[�X�����ׂăN���[���A�b�v���܂��B
        /// </summary>
        /// <param name="disposing">�}�l�[�W ���\�[�X���j�������ꍇ true�A�j������Ȃ��ꍇ�� false �ł��B</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h

        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.comboBoxArtsCategory = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxFilterLevel = new System.Windows.Forms.ComboBox();
            this.numericUpDownFilterLevel = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxItems = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            this.groupBoxFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(131, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(223, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBoxFilter);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.comboBoxItems);
            this.panelMain.Size = new System.Drawing.Size(320, 200);
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Controls.Add(this.comboBoxArtsCategory);
            this.groupBoxFilter.Controls.Add(this.label2);
            this.groupBoxFilter.Controls.Add(this.comboBoxFilterLevel);
            this.groupBoxFilter.Controls.Add(this.numericUpDownFilterLevel);
            this.groupBoxFilter.Controls.Add(this.label4);
            this.groupBoxFilter.Controls.Add(this.comboBoxFilterType);
            this.groupBoxFilter.Controls.Add(this.label3);
            this.groupBoxFilter.Location = new System.Drawing.Point(5, 50);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(304, 141);
            this.groupBoxFilter.TabIndex = 8;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "�t�B���^";
            // 
            // comboBoxArtsCategory
            // 
            this.comboBoxArtsCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArtsCategory.FormattingEnabled = true;
            this.comboBoxArtsCategory.Location = new System.Drawing.Point(9, 68);
            this.comboBoxArtsCategory.Name = "comboBoxArtsCategory";
            this.comboBoxArtsCategory.Size = new System.Drawing.Size(159, 20);
            this.comboBoxArtsCategory.TabIndex = 44;
            this.comboBoxArtsCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxArtsCategory_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 43;
            this.label2.Text = "�A�[�c�J�e�S��:";
            // 
            // comboBoxFilterLevel
            // 
            this.comboBoxFilterLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterLevel.FormattingEnabled = true;
            this.comboBoxFilterLevel.Items.AddRange(new object[] {
            "�ȏ�",
            "�ȉ�"});
            this.comboBoxFilterLevel.Location = new System.Drawing.Point(73, 110);
            this.comboBoxFilterLevel.Name = "comboBoxFilterLevel";
            this.comboBoxFilterLevel.Size = new System.Drawing.Size(96, 20);
            this.comboBoxFilterLevel.TabIndex = 42;
            this.comboBoxFilterLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterLevel_SelectedIndexChanged);
            // 
            // numericUpDownFilterLevel
            // 
            this.numericUpDownFilterLevel.Location = new System.Drawing.Point(9, 110);
            this.numericUpDownFilterLevel.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownFilterLevel.Name = "numericUpDownFilterLevel";
            this.numericUpDownFilterLevel.Size = new System.Drawing.Size(58, 19);
            this.numericUpDownFilterLevel.TabIndex = 41;
            this.numericUpDownFilterLevel.ValueChanged += new System.EventHandler(this.numericUpDownFilterLevel_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 12);
            this.label4.TabIndex = 40;
            this.label4.Text = "ID:";
            // 
            // comboBoxFilterType
            // 
            this.comboBoxFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterType.FormattingEnabled = true;
            this.comboBoxFilterType.Items.AddRange(new object[] {
            "���ׂ�",
            "�A�[�c",
            "�T�|�[�g",
            "�A�V�X�g",
            "�X�y�V����"});
            this.comboBoxFilterType.Location = new System.Drawing.Point(8, 30);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(160, 20);
            this.comboBoxFilterType.TabIndex = 39;
            this.comboBoxFilterType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 12);
            this.label3.TabIndex = 38;
            this.label3.Text = "���:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "�X�L��:";
            // 
            // comboBoxItems
            // 
            this.comboBoxItems.DropDownHeight = 186;
            this.comboBoxItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItems.FormattingEnabled = true;
            this.comboBoxItems.IntegralHeight = false;
            this.comboBoxItems.Location = new System.Drawing.Point(5, 24);
            this.comboBoxItems.Name = "comboBoxItems";
            this.comboBoxItems.Size = new System.Drawing.Size(287, 20);
            this.comboBoxItems.TabIndex = 6;
            this.comboBoxItems.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboBoxItems_Format);
            // 
            // SkillSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(320, 233);
            this.Name = "SkillSelectDialog";
            this.Text = "�X�L���I��";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxItems;
        private System.Windows.Forms.ComboBox comboBoxFilterLevel;
        private System.Windows.Forms.NumericUpDown numericUpDownFilterLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxFilterType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxArtsCategory;
        private System.Windows.Forms.Label label2;
    }
}
