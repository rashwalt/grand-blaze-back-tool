namespace DataEditForm.Guest
{
    partial class GuestFilterDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxFilterGuestKb = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(61, 7);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(153, 7);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.comboBoxFilterGuestKb);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Size = new System.Drawing.Size(248, 55);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ゲスト区分:";
            // 
            // comboBoxFilterGuestKb
            // 
            this.comboBoxFilterGuestKb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterGuestKb.FormattingEnabled = true;
            this.comboBoxFilterGuestKb.Items.AddRange(new object[] {
            "すべて",
            "ゲスト",
            "レア(ゲスト無効)",
            "ボス(ゲスト無効)",
            "アニマル",
            "精霊",
            "ファミリア"});
            this.comboBoxFilterGuestKb.Location = new System.Drawing.Point(14, 24);
            this.comboBoxFilterGuestKb.Name = "comboBoxFilterGuestKb";
            this.comboBoxFilterGuestKb.Size = new System.Drawing.Size(225, 20);
            this.comboBoxFilterGuestKb.TabIndex = 68;
            // 
            // GuestFilterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 88);
            this.Name = "GuestFilterDialog";
            this.Text = "ゲストフィルタ";
            this.Load += new System.EventHandler(this.GuestFilterDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFilterGuestKb;
    }
}