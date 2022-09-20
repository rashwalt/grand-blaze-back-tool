namespace CommonFormLibrary.CommonDialog
{
    partial class EventSettingDialog
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
            this.eventEditorPanel = new CommonFormLibrary.CommonPanel.EventEditorPanel();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(633, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(725, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.eventEditorPanel);
            this.panelMain.Size = new System.Drawing.Size(823, 609);
            // 
            // eventEditorPanel
            // 
            this.eventEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventEditorPanel.Location = new System.Drawing.Point(0, 0);
            this.eventEditorPanel.Name = "eventEditorPanel";
            this.eventEditorPanel.Size = new System.Drawing.Size(823, 609);
            this.eventEditorPanel.TabIndex = 0;
            // 
            // EventSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(823, 642);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "EventSettingDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Auto;
            this.Text = "イベントエディタ";
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CommonFormLibrary.CommonPanel.EventEditorPanel eventEditorPanel;
    }
}
