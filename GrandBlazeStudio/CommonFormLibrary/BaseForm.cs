using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonFormLibrary
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームのタイトルを設定
        /// </summary>
        [Description("コントロールに関連づけられたフォームタイトルを設定します。"),Category("表示"),Browsable(true)]
        public new string Text
        {
            set
            {
                base.Text = value;
                this.labelTitle.Text = value;
            }
            get
            {
                return base.Text;
            }
        }
    }
}