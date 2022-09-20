using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonFormLibrary.CommonDialog
{
    public partial class SetNewNumberDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetNewNumberDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 新規番号設定
        /// </summary>
        /// <param name="newID">新規番号</param>
        public void SetNewNumber(int newID)
        {
            this.numericUpDownNewID.Value = newID;
        }

        /// <summary>
        /// 番号検証
        /// </summary>
        public EventHandler ValidatingNumber;

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

        /// <summary>
        /// 新規ID
        /// </summary>
        public int NewID
        {
            get { return (int)this.numericUpDownNewID.Value; }
        }

        /// <summary>
        /// データ検証
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownNewID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ValidatingNumber != null)
            {
                ValidatingNumber(this, EventArgs.Empty);
            }
        }

        private void numericUpDownNewID_ValueChanged(object sender, EventArgs e)
        {
            if (ValidatingNumber != null)
            {
                ValidatingNumber(this, EventArgs.Empty);
            }
        }
    }
}

