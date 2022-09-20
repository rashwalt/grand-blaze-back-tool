using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sgry.Azuki;

namespace CommonFormLibrary.CommonPanel
{
    public partial class EventEditorPanel : UserControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EventEditorPanel()
        {
            InitializeComponent();

            // コントロール初期化
            this.azukiControl.ColorScheme.BackColor = Color.White;
            this.azukiControl.ColorScheme.ForeColor = Color.Black;
            this.azukiControl.ColorScheme.LineNumberBack = Color.White;
            this.azukiControl.ColorScheme.LineNumberFore = Color.Black;
            this.azukiControl.ColorScheme.SetColor(CharClass.Keyword2, Color.FromArgb(43, 145, 175), Color.White);
            this.azukiControl.ColorScheme.SetColor(CharClass.String, Color.Maroon, Color.White);
            this.azukiControl.ColorScheme.SetColor(CharClass.Keyword, Color.Blue, Color.White);
            this.azukiControl.ColorScheme.SetColor(CharClass.Number, Color.Black, Color.White);
            this.azukiControl.ColorScheme.SetColor(CharClass.Selecter, Color.FromArgb(51, 153, 255), Color.Black);
            this.azukiControl.AutoIndentHook = AutoIndentHooks.GenericHook;
            this.azukiControl.Document.Highlighter = Sgry.Azuki.Highlighter.Highlighters.Ruby;
            this.azukiControl.TabWidth = 4;
        }

        /// <summary>
        /// コントロールのテキスト
        /// </summary>
        public override string Text
        {
            get
            {
                return this.azukiControl.Text;
            }
            set
            {
                this.azukiControl.Text = value;
            }
        }
    }
}
