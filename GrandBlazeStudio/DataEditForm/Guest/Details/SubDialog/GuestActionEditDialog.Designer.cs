namespace DataEditForm.Guest.Details.SubDialog
{
    partial class GuestActionEditDialog
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonArtsSelect = new System.Windows.Forms.Button();
            this.textBoxArtsName = new System.Windows.Forms.TextBox();
            this.comboBoxAction = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxTargetTypeMine = new System.Windows.Forms.ComboBox();
            this.radioButtonTargetTypeMine = new System.Windows.Forms.RadioButton();
            this.comboBoxTargetTypeFriend = new System.Windows.Forms.ComboBox();
            this.radioButtonTargetTypeFriend = new System.Windows.Forms.RadioButton();
            this.comboBoxTargetTypeEnemy = new System.Windows.Forms.ComboBox();
            this.radioButtonTargetTypeEnemy = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDownLimitLevel = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLimitLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(247, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(339, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBox3);
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Controls.Add(this.groupBox2);
            this.panelMain.Size = new System.Drawing.Size(437, 248);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonArtsSelect);
            this.groupBox2.Controls.Add(this.textBoxArtsName);
            this.groupBox2.Controls.Add(this.comboBoxAction);
            this.groupBox2.Location = new System.Drawing.Point(12, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 71);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "行動内容";
            // 
            // buttonArtsSelect
            // 
            this.buttonArtsSelect.Location = new System.Drawing.Point(382, 44);
            this.buttonArtsSelect.Name = "buttonArtsSelect";
            this.buttonArtsSelect.Size = new System.Drawing.Size(24, 19);
            this.buttonArtsSelect.TabIndex = 2;
            this.buttonArtsSelect.Text = "...";
            this.buttonArtsSelect.UseVisualStyleBackColor = true;
            this.buttonArtsSelect.Click += new System.EventHandler(this.buttonArtsSelect_Click);
            // 
            // textBoxArtsName
            // 
            this.textBoxArtsName.Location = new System.Drawing.Point(76, 44);
            this.textBoxArtsName.Name = "textBoxArtsName";
            this.textBoxArtsName.ReadOnly = true;
            this.textBoxArtsName.Size = new System.Drawing.Size(307, 19);
            this.textBoxArtsName.TabIndex = 1;
            // 
            // comboBoxAction
            // 
            this.comboBoxAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAction.FormattingEnabled = true;
            this.comboBoxAction.Location = new System.Drawing.Point(76, 18);
            this.comboBoxAction.Name = "comboBoxAction";
            this.comboBoxAction.Size = new System.Drawing.Size(331, 20);
            this.comboBoxAction.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxTargetTypeMine);
            this.groupBox1.Controls.Add(this.radioButtonTargetTypeMine);
            this.groupBox1.Controls.Add(this.comboBoxTargetTypeFriend);
            this.groupBox1.Controls.Add(this.radioButtonTargetTypeFriend);
            this.groupBox1.Controls.Add(this.comboBoxTargetTypeEnemy);
            this.groupBox1.Controls.Add(this.radioButtonTargetTypeEnemy);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 98);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "行動対象";
            // 
            // comboBoxTargetTypeMine
            // 
            this.comboBoxTargetTypeMine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetTypeMine.Enabled = false;
            this.comboBoxTargetTypeMine.FormattingEnabled = true;
            this.comboBoxTargetTypeMine.Location = new System.Drawing.Point(76, 66);
            this.comboBoxTargetTypeMine.Name = "comboBoxTargetTypeMine";
            this.comboBoxTargetTypeMine.Size = new System.Drawing.Size(331, 20);
            this.comboBoxTargetTypeMine.TabIndex = 5;
            // 
            // radioButtonTargetTypeMine
            // 
            this.radioButtonTargetTypeMine.AutoSize = true;
            this.radioButtonTargetTypeMine.Location = new System.Drawing.Point(6, 70);
            this.radioButtonTargetTypeMine.Name = "radioButtonTargetTypeMine";
            this.radioButtonTargetTypeMine.Size = new System.Drawing.Size(47, 16);
            this.radioButtonTargetTypeMine.TabIndex = 4;
            this.radioButtonTargetTypeMine.Text = "自分";
            this.radioButtonTargetTypeMine.UseVisualStyleBackColor = true;
            this.radioButtonTargetTypeMine.CheckedChanged += new System.EventHandler(this.radioButtonTargetTypeMine_CheckedChanged);
            // 
            // comboBoxTargetTypeFriend
            // 
            this.comboBoxTargetTypeFriend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetTypeFriend.Enabled = false;
            this.comboBoxTargetTypeFriend.FormattingEnabled = true;
            this.comboBoxTargetTypeFriend.Location = new System.Drawing.Point(76, 40);
            this.comboBoxTargetTypeFriend.Name = "comboBoxTargetTypeFriend";
            this.comboBoxTargetTypeFriend.Size = new System.Drawing.Size(331, 20);
            this.comboBoxTargetTypeFriend.TabIndex = 3;
            // 
            // radioButtonTargetTypeFriend
            // 
            this.radioButtonTargetTypeFriend.AutoSize = true;
            this.radioButtonTargetTypeFriend.Location = new System.Drawing.Point(6, 44);
            this.radioButtonTargetTypeFriend.Name = "radioButtonTargetTypeFriend";
            this.radioButtonTargetTypeFriend.Size = new System.Drawing.Size(47, 16);
            this.radioButtonTargetTypeFriend.TabIndex = 2;
            this.radioButtonTargetTypeFriend.Text = "味方";
            this.radioButtonTargetTypeFriend.UseVisualStyleBackColor = true;
            this.radioButtonTargetTypeFriend.CheckedChanged += new System.EventHandler(this.radioButtonTargetTypeFriend_CheckedChanged);
            // 
            // comboBoxTargetTypeEnemy
            // 
            this.comboBoxTargetTypeEnemy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetTypeEnemy.Enabled = false;
            this.comboBoxTargetTypeEnemy.FormattingEnabled = true;
            this.comboBoxTargetTypeEnemy.Location = new System.Drawing.Point(76, 14);
            this.comboBoxTargetTypeEnemy.Name = "comboBoxTargetTypeEnemy";
            this.comboBoxTargetTypeEnemy.Size = new System.Drawing.Size(331, 20);
            this.comboBoxTargetTypeEnemy.TabIndex = 1;
            // 
            // radioButtonTargetTypeEnemy
            // 
            this.radioButtonTargetTypeEnemy.AutoSize = true;
            this.radioButtonTargetTypeEnemy.Location = new System.Drawing.Point(6, 18);
            this.radioButtonTargetTypeEnemy.Name = "radioButtonTargetTypeEnemy";
            this.radioButtonTargetTypeEnemy.Size = new System.Drawing.Size(35, 16);
            this.radioButtonTargetTypeEnemy.TabIndex = 0;
            this.radioButtonTargetTypeEnemy.Text = "敵";
            this.radioButtonTargetTypeEnemy.UseVisualStyleBackColor = true;
            this.radioButtonTargetTypeEnemy.CheckedChanged += new System.EventHandler(this.radioButtonTargetTypeEnemy_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDownLimitLevel);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 193);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(413, 48);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "実行";
            // 
            // numericUpDownLimitLevel
            // 
            this.numericUpDownLimitLevel.Location = new System.Drawing.Point(76, 18);
            this.numericUpDownLimitLevel.Name = "numericUpDownLimitLevel";
            this.numericUpDownLimitLevel.Size = new System.Drawing.Size(76, 19);
            this.numericUpDownLimitLevel.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "下限レベル:";
            // 
            // GuestActionEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 281);
            this.Name = "GuestActionEditDialog";
            this.Text = "バトルアクション";
            this.panelMain.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLimitLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonArtsSelect;
        private System.Windows.Forms.TextBox textBoxArtsName;
        private System.Windows.Forms.ComboBox comboBoxAction;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxTargetTypeMine;
        private System.Windows.Forms.RadioButton radioButtonTargetTypeMine;
        private System.Windows.Forms.ComboBox comboBoxTargetTypeFriend;
        private System.Windows.Forms.RadioButton radioButtonTargetTypeFriend;
        private System.Windows.Forms.ComboBox comboBoxTargetTypeEnemy;
        private System.Windows.Forms.RadioButton radioButtonTargetTypeEnemy;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDownLimitLevel;
        private System.Windows.Forms.Label label1;
    }
}