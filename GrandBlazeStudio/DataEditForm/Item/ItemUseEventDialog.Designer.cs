namespace DataEditForm.Item
{
    partial class ItemUseEventDialog
    {
        /// <summary>
        /// 必要なデザイナー変数です。
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.eventEditorPanel = new CommonFormLibrary.CommonPanel.EventEditorPanel();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(526, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(618, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Size = new System.Drawing.Size(716, 462);
            // 
            // eventEditorPanel
            // 
            this.eventEditorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eventEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventEditorPanel.Location = new System.Drawing.Point(0, 0);
            this.eventEditorPanel.Name = "eventEditorPanel";
            this.eventEditorPanel.Size = new System.Drawing.Size(716, 462);
            this.eventEditorPanel.TabIndex = 0;
            // 
            // ItemUseEventDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 495);
            this.Controls.Add(this.eventEditorPanel);
            this.Name = "ItemUseEventDialog";
            this.ShowIcon = false;
            this.Text = "アイテム使用スクリプト";
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.Controls.SetChildIndex(this.eventEditorPanel, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private CommonFormLibrary.CommonPanel.EventEditorPanel eventEditorPanel;
    }
}