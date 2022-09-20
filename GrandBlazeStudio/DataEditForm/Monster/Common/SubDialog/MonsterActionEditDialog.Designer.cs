namespace DataEditForm.Monster.Common.SubDialog
{
    partial class MonsterActionEditDialog
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonArtsSelect = new System.Windows.Forms.Button();
            this.textBoxArtsName = new System.Windows.Forms.TextBox();
            this.comboBoxAction = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDownMaxCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxProb = new System.Windows.Forms.ComboBox();
            this.comboBoxTiming1 = new System.Windows.Forms.ComboBox();
            this.comboBoxTiming2 = new System.Windows.Forms.ComboBox();
            this.comboBoxTiming3 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBoxTargetTypeMine = new System.Windows.Forms.ComboBox();
            this.radioButtonTargetTypeMine = new System.Windows.Forms.RadioButton();
            this.comboBoxTargetTypeFriend = new System.Windows.Forms.ComboBox();
            this.radioButtonTargetTypeFriend = new System.Windows.Forms.RadioButton();
            this.comboBoxTargetTypeEnemy = new System.Windows.Forms.ComboBox();
            this.radioButtonTargetTypeEnemy = new System.Windows.Forms.RadioButton();
            this.radioButtonTargetTypeNone = new System.Windows.Forms.RadioButton();
            this.panelMain.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(247, 7);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(339, 7);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBox4);
            this.panelMain.Controls.Add(this.groupBox3);
            this.panelMain.Controls.Add(this.groupBox2);
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Size = new System.Drawing.Size(437, 401);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonArtsSelect);
            this.groupBox2.Controls.Add(this.textBoxArtsName);
            this.groupBox2.Controls.Add(this.comboBoxAction);
            this.groupBox2.Location = new System.Drawing.Point(12, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 71);
            this.groupBox2.TabIndex = 1;
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDownMaxCount);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.comboBoxProb);
            this.groupBox3.Location = new System.Drawing.Point(12, 320);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(413, 72);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "実行";
            // 
            // numericUpDownMaxCount
            // 
            this.numericUpDownMaxCount.Location = new System.Drawing.Point(76, 44);
            this.numericUpDownMaxCount.Name = "numericUpDownMaxCount";
            this.numericUpDownMaxCount.Size = new System.Drawing.Size(76, 19);
            this.numericUpDownMaxCount.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "回数:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "確率:";
            // 
            // comboBoxProb
            // 
            this.comboBoxProb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProb.FormattingEnabled = true;
            this.comboBoxProb.Items.AddRange(new object[] {
            "超低",
            "低",
            "中",
            "高",
            "必"});
            this.comboBoxProb.Location = new System.Drawing.Point(76, 18);
            this.comboBoxProb.Name = "comboBoxProb";
            this.comboBoxProb.Size = new System.Drawing.Size(76, 20);
            this.comboBoxProb.TabIndex = 2;
            // 
            // comboBoxTiming1
            // 
            this.comboBoxTiming1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTiming1.FormattingEnabled = true;
            this.comboBoxTiming1.Location = new System.Drawing.Point(76, 14);
            this.comboBoxTiming1.Name = "comboBoxTiming1";
            this.comboBoxTiming1.Size = new System.Drawing.Size(331, 20);
            this.comboBoxTiming1.TabIndex = 1;
            // 
            // comboBoxTiming2
            // 
            this.comboBoxTiming2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTiming2.FormattingEnabled = true;
            this.comboBoxTiming2.Location = new System.Drawing.Point(76, 40);
            this.comboBoxTiming2.Name = "comboBoxTiming2";
            this.comboBoxTiming2.Size = new System.Drawing.Size(331, 20);
            this.comboBoxTiming2.TabIndex = 3;
            // 
            // comboBoxTiming3
            // 
            this.comboBoxTiming3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTiming3.FormattingEnabled = true;
            this.comboBoxTiming3.Location = new System.Drawing.Point(76, 66);
            this.comboBoxTiming3.Name = "comboBoxTiming3";
            this.comboBoxTiming3.Size = new System.Drawing.Size(331, 20);
            this.comboBoxTiming3.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxTiming3);
            this.groupBox1.Controls.Add(this.comboBoxTiming2);
            this.groupBox1.Controls.Add(this.comboBoxTiming1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 98);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "条件";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButtonTargetTypeNone);
            this.groupBox4.Controls.Add(this.comboBoxTargetTypeMine);
            this.groupBox4.Controls.Add(this.radioButtonTargetTypeMine);
            this.groupBox4.Controls.Add(this.comboBoxTargetTypeFriend);
            this.groupBox4.Controls.Add(this.radioButtonTargetTypeFriend);
            this.groupBox4.Controls.Add(this.comboBoxTargetTypeEnemy);
            this.groupBox4.Controls.Add(this.radioButtonTargetTypeEnemy);
            this.groupBox4.Location = new System.Drawing.Point(12, 116);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(413, 121);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "行動対象";
            // 
            // comboBoxTargetTypeMine
            // 
            this.comboBoxTargetTypeMine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetTypeMine.Enabled = false;
            this.comboBoxTargetTypeMine.FormattingEnabled = true;
            this.comboBoxTargetTypeMine.Location = new System.Drawing.Point(76, 89);
            this.comboBoxTargetTypeMine.Name = "comboBoxTargetTypeMine";
            this.comboBoxTargetTypeMine.Size = new System.Drawing.Size(331, 20);
            this.comboBoxTargetTypeMine.TabIndex = 5;
            // 
            // radioButtonTargetTypeMine
            // 
            this.radioButtonTargetTypeMine.AutoSize = true;
            this.radioButtonTargetTypeMine.Location = new System.Drawing.Point(6, 93);
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
            this.comboBoxTargetTypeFriend.Location = new System.Drawing.Point(76, 63);
            this.comboBoxTargetTypeFriend.Name = "comboBoxTargetTypeFriend";
            this.comboBoxTargetTypeFriend.Size = new System.Drawing.Size(331, 20);
            this.comboBoxTargetTypeFriend.TabIndex = 3;
            // 
            // radioButtonTargetTypeFriend
            // 
            this.radioButtonTargetTypeFriend.AutoSize = true;
            this.radioButtonTargetTypeFriend.Location = new System.Drawing.Point(6, 67);
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
            this.comboBoxTargetTypeEnemy.Location = new System.Drawing.Point(76, 37);
            this.comboBoxTargetTypeEnemy.Name = "comboBoxTargetTypeEnemy";
            this.comboBoxTargetTypeEnemy.Size = new System.Drawing.Size(331, 20);
            this.comboBoxTargetTypeEnemy.TabIndex = 1;
            // 
            // radioButtonTargetTypeEnemy
            // 
            this.radioButtonTargetTypeEnemy.AutoSize = true;
            this.radioButtonTargetTypeEnemy.Location = new System.Drawing.Point(6, 41);
            this.radioButtonTargetTypeEnemy.Name = "radioButtonTargetTypeEnemy";
            this.radioButtonTargetTypeEnemy.Size = new System.Drawing.Size(35, 16);
            this.radioButtonTargetTypeEnemy.TabIndex = 0;
            this.radioButtonTargetTypeEnemy.Text = "敵";
            this.radioButtonTargetTypeEnemy.UseVisualStyleBackColor = true;
            this.radioButtonTargetTypeEnemy.CheckedChanged += new System.EventHandler(this.radioButtonTargetTypeEnemy_CheckedChanged);
            // 
            // radioButtonTargetTypeNone
            // 
            this.radioButtonTargetTypeNone.AutoSize = true;
            this.radioButtonTargetTypeNone.Location = new System.Drawing.Point(6, 18);
            this.radioButtonTargetTypeNone.Name = "radioButtonTargetTypeNone";
            this.radioButtonTargetTypeNone.Size = new System.Drawing.Size(59, 16);
            this.radioButtonTargetTypeNone.TabIndex = 6;
            this.radioButtonTargetTypeNone.TabStop = true;
            this.radioButtonTargetTypeNone.Text = "未設定";
            this.radioButtonTargetTypeNone.UseVisualStyleBackColor = true;
            // 
            // MonsterActionEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(437, 434);
            this.Name = "MonsterActionEditDialog";
            this.Text = "バトルアクション";
            this.panelMain.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxAction;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxProb;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxTiming3;
        private System.Windows.Forms.ComboBox comboBoxTiming2;
        private System.Windows.Forms.ComboBox comboBoxTiming1;
        private System.Windows.Forms.Button buttonArtsSelect;
        private System.Windows.Forms.TextBox textBoxArtsName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBoxTargetTypeMine;
        private System.Windows.Forms.RadioButton radioButtonTargetTypeMine;
        private System.Windows.Forms.ComboBox comboBoxTargetTypeFriend;
        private System.Windows.Forms.RadioButton radioButtonTargetTypeFriend;
        private System.Windows.Forms.ComboBox comboBoxTargetTypeEnemy;
        private System.Windows.Forms.RadioButton radioButtonTargetTypeEnemy;
        private System.Windows.Forms.RadioButton radioButtonTargetTypeNone;
    }
}
