namespace DataEditForm.Quest
{
    partial class QuestCommentDialog
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
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownStep = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStep)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(417, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(509, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.numericUpDownStep);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.textBoxComment);
            this.panelMain.Size = new System.Drawing.Size(607, 355);
            // 
            // textBoxComment
            // 
            this.textBoxComment.AcceptsReturn = true;
            this.textBoxComment.AcceptsTab = true;
            this.textBoxComment.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxComment.Location = new System.Drawing.Point(14, 52);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxComment.Size = new System.Drawing.Size(581, 297);
            this.textBoxComment.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "進捗:";
            // 
            // numericUpDownStep
            // 
            this.numericUpDownStep.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.numericUpDownStep.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownStep.Location = new System.Drawing.Point(49, 12);
            this.numericUpDownStep.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownStep.Name = "numericUpDownStep";
            this.numericUpDownStep.Size = new System.Drawing.Size(82, 19);
            this.numericUpDownStep.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "コメント:";
            // 
            // QuestCommentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 388);
            this.Name = "QuestCommentDialog";
            this.Text = "コメント編集";
            this.Load += new System.EventHandler(this.QuestCommentDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStep)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownStep;
        private System.Windows.Forms.Label label1;
    }
}