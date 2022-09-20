namespace CommonFormLibrary.CommonPanel
{
    partial class EventEditorPanel
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
            this.azukiControl = new Sgry.Azuki.Windows.AzukiControl();
            this.SuspendLayout();
            // 
            // azukiControl
            // 
            this.azukiControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.azukiControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.azukiControl.DrawingOption = ((Sgry.Azuki.DrawingOption)((Sgry.Azuki.DrawingOption.DrawsEol | Sgry.Azuki.DrawingOption.ShowsLineNumber)));
            this.azukiControl.DrawsFullWidthSpace = false;
            this.azukiControl.DrawsTab = false;
            this.azukiControl.FirstVisibleLine = 0;
            this.azukiControl.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.azukiControl.HighlightsCurrentLine = false;
            this.azukiControl.Location = new System.Drawing.Point(0, 0);
            this.azukiControl.Name = "azukiControl";
            this.azukiControl.Size = new System.Drawing.Size(606, 533);
            this.azukiControl.TabIndex = 0;
            this.azukiControl.TabWidth = 8;
            this.azukiControl.ViewWidth = 4129;
            // 
            // EventEditorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.azukiControl);
            this.Name = "EventEditorPanel";
            this.Size = new System.Drawing.Size(606, 533);
            this.ResumeLayout(false);

        }

        #endregion

        private Sgry.Azuki.Windows.AzukiControl azukiControl;

    }
}
