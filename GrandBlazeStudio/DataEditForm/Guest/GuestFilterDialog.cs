using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;

namespace DataEditForm.Guest
{
    public partial class GuestFilterDialog : CommonFormLibrary.BaseDialog
    {
        public GuestFilterDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フィルタ文字列
        /// </summary>
        public string FilterString
        {
            get
            {
                List<string> filter = new List<string>();

                // ゲスト区分
                if (this.comboBoxFilterGuestKb.SelectedIndex > 0)
                {
                    filter.Add(LibGuestData.Entity.mt_guest_list.belong_kbColumn.ColumnName + "=" + (this.comboBoxFilterGuestKb.SelectedIndex - 1));
                }

                return string.Join(" AND ", filter.ToArray());
            }
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
        /// 初期表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuestFilterDialog_Load(object sender, EventArgs e)
        {
            DefalutFilterSet();
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
        /// 初期表示モード
        /// </summary>
        private void DefalutFilterSet()
        {
            this.comboBoxFilterGuestKb.SelectedIndex = 0;
        }
    }
}
