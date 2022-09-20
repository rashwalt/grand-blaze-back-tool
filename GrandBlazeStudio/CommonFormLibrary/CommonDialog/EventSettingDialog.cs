using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonFormLibrary.CommonDialog
{
    public partial class EventSettingDialog : CommonFormLibrary.BaseDialog
    {
        public EventSettingDialog()
        {
            InitializeComponent();
        }

        public string Value
        {
            get
            {
                return this.eventEditorPanel.Text;
            }
            set
            {
                this.eventEditorPanel.Text = value;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
