namespace DataEditForm.Quest.Weather
{
    partial class WeatherSettingDialog
    {
        /// <summary>
        /// �K�v�ȃf�U�C�i�ϐ��ł��B
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// �g�p���̃��\�[�X�����ׂăN���[���A�b�v���܂��B
        /// </summary>
        /// <param name="disposing">�}�l�[�W ���\�[�X���j�������ꍇ true�A�j������Ȃ��ꍇ�� false �ł��B</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h

        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
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
            this.Text = "�V��ݒ�";
            this.Load += new System.EventHandler(this.WeatherSettingDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxWeather;
    }
}
