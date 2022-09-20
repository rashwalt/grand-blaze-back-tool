using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonFormLibrary
{
    public partial class BasePanel : UserControl
    {
        public BasePanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 保存処理
        /// </summary>
        public virtual void Save()
        {
        }

        /// <summary>
        /// キャンセル処理
        /// </summary>
        public virtual void Cancel()
        {
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <returns>変更があるならtrue</returns>
        public virtual bool CheckModify()
        {
            return true;
        }

        protected virtual void Panel_Load(object sender, EventArgs e)
        {

        }

        public string TitleName
        {
            get
            {
                return this.labelTitle.Text;
            }
        }
    }
}
