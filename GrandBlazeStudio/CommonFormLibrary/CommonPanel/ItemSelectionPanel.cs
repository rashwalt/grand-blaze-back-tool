using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonFormLibrary.CommonDialog;
using CommonLibrary;

namespace CommonFormLibrary.CommonPanel
{
    public partial class ItemSelectionPanel : UserControl
    {
        public ItemSelectionPanel()
        {
            InitializeComponent();

            this.dataGridViewItemBox.AutoGenerateColumns = false;
        }

        private bool _probVisible = true;
        private bool _gimlVisible = true;
        private bool _probIsText = false;
        private bool _gimlInclude = false;

        /// <summary>
        /// 確率の表示/非表示
        /// </summary>
        public bool ProbVisible
        {
            get { return _probVisible; }
            set
            {
                _probVisible = value;

                this.columnProbability.Visible = _probVisible;
            }
        }

        /// <summary>
        /// ギムル系の有効/無効
        /// </summary>
        public bool GimlVisible
        {
            get { return _gimlVisible; }
            set
            {
                _gimlVisible = value;

                if (_gimlVisible)
                {
                    this.columnCount.HeaderText = "個数/金額";
                }
                else
                {
                    this.columnCount.HeaderText = "個数";
                }
            }
        }

        public bool GimlInclude
        {
            get { return _gimlInclude; }
            set { _gimlInclude = value; }
        }

        /// <summary>
        /// 確率を文字で表記
        /// </summary>
        public bool ProbIsText
        {
            get { return _probIsText; }
            set { _probIsText = value; }
        }

        /// <summary>
        /// データソース
        /// </summary>
        public object DataSource
        {
            get { return this.dataGridViewItemBox.DataSource; }
            set { this.dataGridViewItemBox.DataSource = value; }
        }

        /// <summary>
        /// 確率列表示カラム
        /// </summary>
        public string DataPropertyNameProb
        {
            get { return this.columnProbability.DataPropertyName; }
            set { this.columnProbability.DataPropertyName = value; }
        }

        /// <summary>
        /// アイテム名列表示カラム
        /// </summary>
        public string DataPropertyNameItemName
        {
            get { return this.columnItemName.DataPropertyName; }
            set { this.columnItemName.DataPropertyName = value; }
        }

        /// <summary>
        /// 個数列表示カラム
        /// </summary>
        public string DataPropertyNameCount
        {
            get { return this.columnCount.DataPropertyName; }
            set { this.columnCount.DataPropertyName = value; }
        }

        /// <summary>
        /// 専用イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SelectionItemEventHandler(object sender, SelectionItemEventArgs e);

        /// <summary>
        /// 編集イベント
        /// </summary>
        [Category("アクション"), Description("編集イベントの発生")]
        public event SelectionItemEventHandler EditClick;

        /// <summary>
        /// 削除イベント
        /// </summary>
        [Category("アクション"), Description("削除イベントの発生")]
        public event SelectionItemEventHandler DeleteClick;

        /// <summary>
        /// カレントセル解除
        /// </summary>
        public void Reset()
        {
            this.dataGridViewItemBox.CurrentCell = null;
        }

        /// <summary>
        /// 表示形式の変換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewItemBox_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    if (e.Value != null)
                    {
                        if (_probIsText)
                        {
                            e.Value = LibInteger.ChangeProbComboStr((int)e.Value);
                        }
                        else
                        {
                            e.Value = e.Value + "%";
                        }
                    }
                    break;
                case 1:
                    // アイテム？
                    if ((int)e.Value == 0)
                    {
                        e.Value = "00000: ギムル";
                    }
                    else
                    {
                        int ItemID = (int)e.Value;
                        e.Value = ItemID.ToString("00000") + ": " + LibItem.GetItemName(ItemID, false);

                        if (_gimlInclude)
                        {
                            decimal giml = LibItem.GetItemPrice(ItemID, false);
                            e.Value = e.Value + " 【" + giml.ToString("0.#") + "G】";
                        }
                    }
                    break;
                case 2:
                    // 個数
                    int Count = (int)e.Value;
                    if (this.dataGridViewItemBox[1, e.RowIndex].Value.ToString() == "0")
                    {
                        e.Value = Count.ToString("N0") + "G";
                    }
                    else
                    {
                        e.Value = Count.ToString() + "個";
                    }
                    break;
            }
        }

        /// <summary>
        /// 選択イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewItemBox_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewItemBox.SelectedCells.Count == 0)
            {
                AddItem();
            }
            else
            {
                EditItem();
            }
        }

        /// <summary>
        /// セル右クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewItemBox_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.Button == MouseButtons.Right)
            {
                DataGridViewCell cell = this.dataGridViewItemBox[e.ColumnIndex, e.RowIndex];
                // セルの選択状態を反転
                cell.Selected = true;
            }
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemItemDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        /// <summary>
        /// 編集イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemItemEdit_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        /// <summary>
        /// アイテムの削除
        /// </summary>
        private void DeleteItem()
        {
            if (this.dataGridViewItemBox.SelectedCells.Count == 0) { return; }

            int rowIndex = this.dataGridViewItemBox.SelectedCells[0].RowIndex;

            if (DeleteClick != null)
            {
                SelectionItemEventArgs e = new SelectionItemEventArgs();
                e.ItemID = (int)this.dataGridViewItemBox[1, rowIndex].Value;
                e.ItemCount = (int)this.dataGridViewItemBox[2, rowIndex].Value;
                if (this.dataGridViewItemBox[0, rowIndex].Value != null)
                {
                    e.Prob = (int)this.dataGridViewItemBox[0, rowIndex].Value;
                }
                else
                {
                    e.Prob = -1;
                }
                DeleteClick(this, e);
            }
        }

        /// <summary>
        /// アイテム編集
        /// </summary>
        private void EditItem()
        {
            if (this.dataGridViewItemBox.SelectedCells.Count == 0) { return; }

            int rowIndex = this.dataGridViewItemBox.SelectedCells[0].RowIndex;

            SelectionItemDialog dialog = new SelectionItemDialog();

            int prob = -1;
            if (this.dataGridViewItemBox[0, rowIndex].Value != null)
            {
                prob = (int)this.dataGridViewItemBox[0, rowIndex].Value;
            }

            if (!_probVisible)
            {
                dialog.SetNoProb();
            }

            dialog.SetData((int)this.dataGridViewItemBox[1, rowIndex].Value, (int)this.dataGridViewItemBox[2, rowIndex].Value, prob, _probIsText);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (EditClick != null)
                {
                    SelectionItemEventArgs e = new SelectionItemEventArgs();
                    e.ItemID = dialog.ItemID;
                    e.ItemCount = dialog.Count;
                    e.Prob = dialog.Prob;
                    EditClick(this, e);
                }
            }
        }

        /// <summary>
        /// アイテム追加
        /// </summary>
        private void AddItem()
        {
            SelectionItemDialog dialog = new SelectionItemDialog();

            if (!_probVisible)
            {
                dialog.SetNoProb();
            }

            dialog.SetData(0, 1, 0, _probIsText);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (EditClick != null)
                {
                    SelectionItemEventArgs e = new SelectionItemEventArgs();
                    e.ItemID = dialog.ItemID;
                    e.ItemCount = dialog.Count;
                    e.Prob = dialog.Prob;
                    EditClick(this, e);
                }
            }
        }

        /// <summary>
        /// 編集表示確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripEdit_Opening(object sender, CancelEventArgs e)
        {
            if (this.dataGridViewItemBox.SelectedCells.Count > 0)
            {
                this.toolStripMenuItemItemEdit.Enabled = true;
            }
        }

        /// <summary>
        /// 追加イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        /// <summary>
        /// コンテキストメニュー表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewItemBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // コンテキスト表示
                this.contextMenuStripEdit.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
        }

        /// <summary>
        /// キーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewItemBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Insert:
                    toolStripMenuItemAdd_Click(this, EventArgs.Empty);
                    break;
                case Keys.Delete:
                    toolStripMenuItemItemDelete_Click(this, EventArgs.Empty);
                    break;
            }
        }

        /// <summary>
        /// 行数
        /// </summary>
        public DataGridViewRowCollection Rows
        {
            get { return this.dataGridViewItemBox.Rows; }
        }
    }
}
