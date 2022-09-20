namespace CommonFormLibrary.CommonDialog
{
    partial class SetNewNumberDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownNewID = new System.Windows.Forms.NumericUpDown();
            this.labelCaution = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewID)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(6, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(98, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.labelCaution);
            this.panelMain.Controls.Add(this.numericUpDownNewID);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Size = new System.Drawing.Size(194, 88);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // numericUpDownNewID
            // 
            this.numericUpDownNewID.Location = new System.Drawing.Point(12, 24);
            this.numericUpDownNewID.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownNewID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNewID.Name = "numericUpDownNewID";
            this.numericUpDownNewID.Size = new System.Drawing.Size(111, 19);
            this.numericUpDownNewID.TabIndex = 1;
            this.numericUpDownNewID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNewID.ValueChanged += new System.EventHandler(this.numericUpDownNewID_ValueChanged);
            this.numericUpDownNewID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDownNewID_KeyPress);
            // 
            // labelCaution
            // 
            this.labelCaution.AutoSize = true;
            this.labelCaution.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCaution.ForeColor = System.Drawing.Color.Red;
            this.labelCaution.Location = new System.Drawing.Point(22, 59);
            this.labelCaution.Name = "labelCaution";
            this.labelCaution.Size = new System.Drawing.Size(162, 12);
            this.labelCaution.TabIndex = 2;
            this.labelCaution.Text = "重複しているIDが存在します。";
            this.labelCaution.Visible = false;
            // 
            // SetNewNumberDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(194, 121);
            this.Name = "SetNewNumberDialog";
            this.Text = "新規番号決定";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewID)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownNewID;
        public System.Windows.Forms.Label labelCaution;
    }
}
