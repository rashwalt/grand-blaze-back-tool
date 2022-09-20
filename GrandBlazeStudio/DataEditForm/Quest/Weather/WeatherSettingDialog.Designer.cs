namespace DataEditForm.Quest.Weather
{
    partial class WeatherSettingDialog
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
            this.checkedListBoxWeather = new System.Windows.Forms.CheckedListBox();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(50, 7);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(142, 7);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.checkedListBoxWeather);
            this.panelMain.Size = new System.Drawing.Size(240, 275);
            // 
            // checkedListBoxWeather
            // 
            this.checkedListBoxWeather.CheckOnClick = true;
            this.checkedListBoxWeather.ColumnWidth = 100;
            this.checkedListBoxWeather.FormattingEnabled = true;
            this.checkedListBoxWeather.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxWeather.Name = "checkedListBoxWeather";
            this.checkedListBoxWeather.Size = new System.Drawing.Size(216, 256);
            this.checkedListBoxWeather.TabIndex = 23;
            // 
            // WeatherSettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(240, 308);
            this.Name = "WeatherSettingDialog";
            this.Text = "天候設定";
            this.Load += new System.EventHandler(this.WeatherSettingDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxWeather;
    }
}
