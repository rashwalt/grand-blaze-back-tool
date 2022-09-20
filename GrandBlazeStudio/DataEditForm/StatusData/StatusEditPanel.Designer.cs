namespace DataEditForm.StatusData
{
    partial class StatusEditPanel
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
            this.checkBoxGood = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxClearlance = new System.Windows.Forms.CheckBox();
            this.checkBoxDispel = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxIcon = new System.Windows.Forms.TextBox();
            this.textBoxStatusComment = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.checkBoxGood);
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Controls.Add(this.groupBox2);
            this.panelMain.Controls.Add(this.textBoxStatusComment);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.textBoxName);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.textBoxNo);
            this.panelMain.Controls.Add(this.label1);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "ステータス";
            // 
            // checkBoxGood
            // 
            this.checkBoxGood.AutoSize = true;
            this.checkBoxGood.Location = new System.Drawing.Point(125, 165);
            this.checkBoxGood.Name = "checkBoxGood";
            this.checkBoxGood.Size = new System.Drawing.Size(94, 16);
            this.checkBoxGood.TabIndex = 44;
            this.checkBoxGood.Text = "グッドステータス";
            this.checkBoxGood.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxClearlance);
            this.groupBox1.Controls.Add(this.checkBoxDispel);
            this.groupBox1.Location = new System.Drawing.Point(6, 147);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(113, 68);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "解除設定";
            // 
            // checkBoxClearlance
            // 
            this.checkBoxClearlance.AutoSize = true;
            this.checkBoxClearlance.Location = new System.Drawing.Point(6, 40);
            this.checkBoxClearlance.Name = "checkBoxClearlance";
            this.checkBoxClearlance.Size = new System.Drawing.Size(98, 16);
            this.checkBoxClearlance.TabIndex = 1;
            this.checkBoxClearlance.Text = "クリアランス有効";
            this.checkBoxClearlance.UseVisualStyleBackColor = true;
            // 
            // checkBoxDispel
            // 
            this.checkBoxDispel.AutoSize = true;
            this.checkBoxDispel.Location = new System.Drawing.Point(6, 18);
            this.checkBoxDispel.Name = "checkBoxDispel";
            this.checkBoxDispel.Size = new System.Drawing.Size(94, 16);
            this.checkBoxDispel.TabIndex = 0;
            this.checkBoxDispel.Text = "ディスペル有効";
            this.checkBoxDispel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxIcon);
            this.groupBox2.Location = new System.Drawing.Point(6, 221);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(412, 141);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "アイコン設定";
            // 
            // textBoxIcon
            // 
            this.textBoxIcon.AcceptsReturn = true;
            this.textBoxIcon.Location = new System.Drawing.Point(6, 18);
            this.textBoxIcon.Multiline = true;
            this.textBoxIcon.Name = "textBoxIcon";
            this.textBoxIcon.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxIcon.Size = new System.Drawing.Size(400, 117);
            this.textBoxIcon.TabIndex = 0;
            // 
            // textBoxStatusComment
            // 
            this.textBoxStatusComment.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxStatusComment.Location = new System.Drawing.Point(6, 61);
            this.textBoxStatusComment.Multiline = true;
            this.textBoxStatusComment.Name = "textBoxStatusComment";
            this.textBoxStatusComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxStatusComment.Size = new System.Drawing.Size(412, 80);
            this.textBoxStatusComment.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 12);
            this.label4.TabIndex = 40;
            this.label4.Text = "ステータス解説:";
            // 
            // textBoxName
            // 
            this.textBoxName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxName.Location = new System.Drawing.Point(92, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(326, 19);
            this.textBoxName.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 39;
            this.label2.Text = "名称:";
            // 
            // textBoxNo
            // 
            this.textBoxNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxNo.Location = new System.Drawing.Point(6, 24);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.ReadOnly = true;
            this.textBoxNo.Size = new System.Drawing.Size(80, 19);
            this.textBoxNo.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "No.:";
            // 
            // StatusEditPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "StatusEditPanel";
            this.Controls.SetChildIndex(this.panelList, 0);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxGood;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxClearlance;
        private System.Windows.Forms.CheckBox checkBoxDispel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxIcon;
        private System.Windows.Forms.TextBox textBoxStatusComment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNo;
        private System.Windows.Forms.Label label1;
    }
}
