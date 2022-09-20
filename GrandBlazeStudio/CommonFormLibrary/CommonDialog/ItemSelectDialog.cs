using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Format;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;

namespace CommonFormLibrary.CommonDialog
{
    public partial class ItemSelectDialog : CommonFormLibrary.BaseDialog
    {
        private DataView _itemView;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ItemSelectDialog()
        {
            InitializeComponent();

            // �f�[�^�o�C���h
            LibItem.Entity.item_list.DefaultView.RowFilter = LibItem.Entity.item_list.it_creatableColumn.ColumnName + "=false";
            _itemView = new DataView(LibItem.Entity.item_list.DefaultView.ToTable());
            _itemView.RowFilter = "";
            _itemView.Sort = LibItem.Entity.item_list.it_numColumn.ColumnName;

            this.comboBoxItems.DataSource = _itemView;
            this.comboBoxItems.ValueMember = LibItem.Entity.item_list.it_numColumn.ColumnName;
            this.comboBoxItems.DisplayMember = LibItem.Entity.item_list.it_nameColumn.ColumnName;

            ItemTypeEntity.mt_item_typeDataTable TypeTable = (ItemTypeEntity.mt_item_typeDataTable)LibItemType.Entity.mt_item_type.Copy();
            TypeTable.Addmt_item_typeRow(-1, "���ׂ�", 0, 0, false, 0, 0, 0, 0, true, true, true, 0, 0, 0, 256, 1, false, 0, 0, false, false, false, false);

            TypeTable.DefaultView.Sort = LibItemType.Entity.mt_item_type.type_idColumn.ColumnName;

            this.comboBoxFilterType.DataSource = TypeTable.DefaultView;
            this.comboBoxFilterType.ValueMember = TypeTable.type_idColumn.ColumnName;
            this.comboBoxFilterType.DisplayMember = TypeTable.typeColumn.ColumnName;

            // �����I��
            this.comboBoxFilterShop.SelectedIndex = 0;
            this.comboBoxFilterType.SelectedIndex = 0;
            this.comboBoxFilterMoney.SelectedIndex = 0;
            this.comboBoxFilterLevel.SelectedIndex = 0;
        }

        /// <summary>
        /// �f�[�^�ݒ�
        /// </summary>
        /// <param name="ItemID">�A�C�e��ID</param>
        public void SetData(int ItemID)
        {
            this.comboBoxItems.SelectedValue = ItemID;
        }

        public int ItemID
        {
            get { return (int)this.comboBoxItems.SelectedValue; }
        }

        /// <summary>
        /// �L�����Z��
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

        #region �t�B���^
        private void comboBoxFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterGenerate();
        }

        private void comboBoxFilterShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterGenerate();
        }

        private void numericUpDownFilterLevel_ValueChanged(object sender, EventArgs e)
        {
            FilterGenerate();
        }

        private void comboBoxFilterLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterGenerate();
        }

        private void numericUpDownFilterMoney_ValueChanged(object sender, EventArgs e)
        {
            FilterGenerate();
        }

        private void comboBoxFilterMoney_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterGenerate();
        }

        private void FilterGenerate()
        {
            List<string> filter = new List<string>();

            if (this.comboBoxFilterType.SelectedIndex > 0)
            {
                filter.Add(LibItem.Entity.item_list.it_typeColumn.ColumnName + "=" + this.comboBoxFilterType.SelectedValue);
            }

            switch (this.comboBoxFilterShop.SelectedIndex)
            {
                case 1:
                    // �̔��i
                    filter.Add(LibItem.Entity.item_list.it_shopColumn.ColumnName + "=true");
                    break;
                case 2:
                    // �񔄕i
                    filter.Add(LibItem.Entity.item_list.it_shopColumn.ColumnName + "=false");
                    break;
            }

            // ���x��
            if (this.comboBoxFilterLevel.SelectedIndex == 0)
            {
                filter.Add(LibItem.Entity.item_list.it_equip_levelColumn.ColumnName + ">=" + this.numericUpDownFilterLevel.Value);
            }
            else
            {
                filter.Add(LibItem.Entity.item_list.it_equip_levelColumn.ColumnName + "<=" + this.numericUpDownFilterLevel.Value);
            }

            // ����
            if (this.comboBoxFilterMoney.SelectedIndex == 0)
            {
                filter.Add(LibItem.Entity.item_list.it_priceColumn.ColumnName + ">=" + this.numericUpDownFilterMoney.Value);
            }
            else
            {
                filter.Add(LibItem.Entity.item_list.it_priceColumn.ColumnName + "<=" + this.numericUpDownFilterMoney.Value);
            }

            _itemView.RowFilter = string.Join(" AND ", filter.ToArray());
        }
        #endregion

        /// <summary>
        /// �\�����Đݒ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxItems_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((int)((DataRowView)e.ListItem)[LibItem.Entity.item_list.it_numColumn.ColumnName]).ToString("00000") + ": " + e.Value;
        }
    }
}

