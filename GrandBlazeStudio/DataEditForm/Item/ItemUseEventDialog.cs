using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonFormLibrary;

namespace DataEditForm.Item
{
    public partial class ItemUseEventDialog : BaseDialog
    {
        public ItemUseEventDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        public string ScriptText
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
    }
}
