using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Format;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;

namespace CommonFormLibrary.CommonDialog
{
    public partial class SelectionItemDialog : CommonFormLibrary.BaseDialog
    {
        public SelectionItemDialog()
        {
            InitializeComponent();

            // データバインド
            List<ItemSelectNames> ItemList = new List<ItemSelectNames>();

            ItemList.Add(new ItemSelectNames(0, "00000: ギムル"));

            LibItem.Entity.item_list.DefaultView.RowFilter = LibItem.Entity.item_list.it_creatableColumn.ColumnName + "=false";
            DataView itemView = new DataView(LibItem.Entity.item_list.DefaultView.ToTable());
            itemView.RowFilter = "";
            itemView.Sort = LibItem.Entity.item_list.it_numColumn.ColumnName;

            foreach (DataRowView row in itemView)
            {
                if (!(bool)row["it_creatable"])
                {
                    ItemList.Add(new ItemSelectNames((int)row["it_num"], ((int)row["it_num"]).ToString("00000") + ": " + row["it_name"].ToString() + " 【" + ((decimal)row["it_price"]).ToString("N0") + "G】"));
                }
            }

            this.comboBoxItem.DataSource = ItemList;
            this.comboBoxItem.ValueMember = "ItemID";
            this.comboBoxItem.DisplayMember = "ViewName";
        }

        private bool isFiveProb = false;

        /// <summary>
        /// データ設定
        /// </summary>
        /// <param name="ItemID">アイテムID</param>
        /// <param name="Count">個数</param>
        /// <param name="Prob">確率</param>
        /// <param name="FiveProb">5段階確率か</param>
        public void SetData(int ItemID, int Count, int Prob, bool FiveProb)
        {
            this.comboBoxItem.SelectedValue = ItemID;
            this.numericUpDownCount.Value = Count;

            isFiveProb = FiveProb;

            if (Prob < 0)
            {
                this.labelProb.Visible = false;
                this.comboBoxProb.Visible = false;
                this.numericUpDownProb.Visible = false;
            }
            else
            {
                if (FiveProb)
                {
                    if (Prob < 0) { Prob = 4; }

                    this.labelProb.Text = "確率";
                    this.numericUpDownProb.Visible = false;
                    this.comboBoxProb.Visible = true;
                    this.comboBoxProb.SelectedIndex = Prob;
                }
                else
                {
                    if (Prob < 0) { Prob = 100; }

                    this.labelProb.Text = "確率(%)";
                    this.numericUpDownProb.Visible = true;
                    this.comboBoxProb.Visible = false;
                    this.numericUpDownProb.Value = Prob;
                }
            }
        }

        public void SetNoProb()
        {
            this.panelProb.Visible = false;
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

        public int Prob
        {
            get
            {
                if (isFiveProb)
                {
                    return this.comboBoxProb.SelectedIndex;
                }
                else
                {
                    return (int)this.numericUpDownProb.Value;
                }
            }
        }

        public int ItemID
        {
            get { return (int)this.comboBoxItem.SelectedValue; }
        }

        public int Count
        {
            get { return (int)this.numericUpDownCount.Value; }
        }

        /// <summary>
        /// インデックス変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxItem.SelectedIndex == 0)
            {
                this.labelCount.Text = "金額";
            }
            else
            {
                this.labelCount.Text = "個数";
            }
        }

        /// <summary>
        /// 選択ダイアログ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectItem_Click(object sender, EventArgs e)
        {
            ItemSelectDialog dialog = new ItemSelectDialog();
            dialog.SetData((int)this.comboBoxItem.SelectedValue);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.comboBoxItem.SelectedValue = dialog.ItemID;
            }
        }
    }
}

