using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using System.Collections;

namespace DataEditForm.Item
{
    public partial class ItemFilterDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemFilterDialog()
        {
            InitializeComponent();

            ItemTypeEntity.mt_item_typeDataTable TypeTable = (ItemTypeEntity.mt_item_typeDataTable)LibItemType.Entity.mt_item_type.Copy();
            TypeTable.Addmt_item_typeRow(-1, "すべて", 0, 0, false, 0, 0, 0, 0, true, true, true, 0, 0, 0, 256, 1, false, 0, 0, false, false, false, false);

            TypeTable.DefaultView.Sort = LibItemType.Entity.mt_item_type.type_idColumn.ColumnName;

            this.comboBoxFilterType.DataSource = TypeTable.DefaultView;
            this.comboBoxFilterType.ValueMember = TypeTable.type_idColumn.ColumnName;
            this.comboBoxFilterType.DisplayMember = TypeTable.typeColumn.ColumnName;

            _SubCategoryTable = (ItemTypeEntity.mt_item_type_sub_categoryDataTable)LibItemType.Entity.mt_item_type_sub_category.Copy();
            _SubCategoryTable.Addmt_item_type_sub_categoryRow(-1, -1, "すべて", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0,0,0,0,0,0", "0,0,0,0,0,0", "0,0,0,0,0,0", 0, 0, 0);

            _SubCategoryView = new DataView(_SubCategoryTable);

            _SubCategoryView.Sort = LibItemType.Entity.mt_item_type_sub_category.category_idColumn.ColumnName;

            this.comboBoxSubCategory.DataSource = _SubCategoryView;
            this.comboBoxSubCategory.DisplayMember = _SubCategoryTable.sub_categoryColumn.ColumnName;
            this.comboBoxSubCategory.ValueMember = _SubCategoryTable.category_idColumn.ColumnName;

            _filters = new Hashtable();
        }

        private ItemTypeEntity.mt_item_type_sub_categoryDataTable _SubCategoryTable = null;
        private DataView _SubCategoryView = null;
        private Hashtable _filters;

        /// <summary>
        /// フィルタ文字列
        /// </summary>
        public string FilterString
        {
            get
            {
                List<string> filter = new List<string>();

                if (this.comboBoxFilterType.SelectedIndex > 0)
                {
                    filter.Add(LibItem.Entity.item_list.it_typeColumn.ColumnName + "=" + this.comboBoxFilterType.SelectedValue);
                }

                if (this.comboBoxSubCategory.Enabled && this.comboBoxSubCategory.SelectedIndex > 0)
                {
                    filter.Add(LibItem.Entity.item_list.it_sub_categoryColumn.ColumnName + "=" + this.comboBoxSubCategory.SelectedValue);
                }

                switch (this.comboBoxFilterShop.SelectedIndex)
                {
                    case 1:
                        // 販売品
                        filter.Add(LibItem.Entity.item_list.it_shopColumn.ColumnName + "=true");
                        break;
                    case 2:
                        // 非売品
                        filter.Add(LibItem.Entity.item_list.it_shopColumn.ColumnName + "=false");
                        break;
                }

                // レベル
                if (this.comboBoxFilterLevel.SelectedIndex == 0)
                {
                    filter.Add(LibItem.Entity.item_list.it_equip_levelColumn.ColumnName + ">=" + this.numericUpDownFilterLevel.Value);
                }
                else
                {
                    filter.Add(LibItem.Entity.item_list.it_equip_levelColumn.ColumnName + "<=" + this.numericUpDownFilterLevel.Value);
                }

                // お金
                if (this.comboBoxFilterMoney.SelectedIndex == 0)
                {
                    filter.Add(LibItem.Entity.item_list.it_priceColumn.ColumnName + ">=" + this.numericUpDownFilterMoney.Value);
                }
                else
                {
                    filter.Add(LibItem.Entity.item_list.it_priceColumn.ColumnName + "<=" + this.numericUpDownFilterMoney.Value);
                }

                // システム販売
                switch (this.comboBoxFilterSellType.SelectedIndex)
                {
                    case 0:
                        filter.Add(LibItem.Entity.item_list.it_creatableColumn.ColumnName + "=false");
                        break;
                    case 1:
                        filter.Add(LibItem.Entity.item_list.it_creatableColumn.ColumnName + "=true");
                        break;
                }

                return string.Join(" AND ", filter.ToArray());
            }
        }

        /// <summary>
        /// フィルタ設定
        /// </summary>
        public Hashtable GetFilterSelected
        {
            get
            {
                Hashtable hash = new Hashtable();

                hash["comboBoxFilterType"] = this.comboBoxFilterType.SelectedIndex;
                hash["comboBoxSubCategoryEnable"] = this.comboBoxSubCategory.Enabled;
                hash["comboBoxSubCategory"] = this.comboBoxSubCategory.SelectedIndex;
                hash["comboBoxFilterShop"] = this.comboBoxFilterShop.SelectedIndex;
                hash["comboBoxFilterLevel"] = this.comboBoxFilterLevel.SelectedIndex;
                hash["numericUpDownFilterLevel"] = this.numericUpDownFilterLevel.Value;
                hash["comboBoxFilterMoney"] = this.comboBoxFilterMoney.SelectedIndex;
                hash["numericUpDownFilterMoney"] = this.numericUpDownFilterMoney.Value;
                hash["comboBoxFilterSellType"] = this.comboBoxFilterSellType.SelectedIndex;

                return hash;
            }
            set
            {
                _filters = value;
            }
        }

        /// <summary>
        /// フィルタ条件リセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            DefalutFilterSet();
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemFilterDialog_Load(object sender, EventArgs e)
        {
            DefalutFilterSet();
        }

        /// <summary>
        /// 初期表示モード
        /// </summary>
        private void DefalutFilterSet()
        {
            if (_filters.Count == 0)
            {
                this.comboBoxFilterShop.SelectedIndex = 0;
                this.comboBoxFilterType.SelectedIndex = 0;
                this.comboBoxFilterMoney.SelectedIndex = 0;
                this.comboBoxFilterLevel.SelectedIndex = 0;
                this.numericUpDownFilterLevel.Value = 0;
                this.comboBoxFilterSellType.SelectedIndex = 0;
                this.numericUpDownFilterMoney.Value = 0;
                this.comboBoxSubCategory.Enabled = false;
            }
            else
            {
                this.comboBoxFilterType.SelectedIndex = (int)_filters["comboBoxFilterType"];
                this.comboBoxSubCategory.Enabled = (bool)_filters["comboBoxSubCategoryEnable"];
                if (this.comboBoxSubCategory.Enabled)
                {
                    this.comboBoxSubCategory.SelectedIndex = (int)_filters["comboBoxSubCategory"];
                }
                this.comboBoxFilterShop.SelectedIndex = (int)_filters["comboBoxFilterShop"];
                this.comboBoxFilterLevel.SelectedIndex = (int)_filters["comboBoxFilterLevel"];
                this.numericUpDownFilterLevel.Value = (decimal)_filters["numericUpDownFilterLevel"];
                this.comboBoxFilterMoney.SelectedIndex = (int)_filters["comboBoxFilterMoney"];
                this.numericUpDownFilterMoney.Value = (decimal)_filters["numericUpDownFilterMoney"];
                this.comboBoxFilterSellType.SelectedIndex = (int)_filters["comboBoxFilterSellType"];
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
        /// タイプ変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxFilterType.SelectedValue == null) { return; }
            if (this.comboBoxFilterType.SelectedValue.GetType() != typeof(int)) { return; }

            _SubCategoryView.RowFilter = _SubCategoryTable.type_idColumn.ColumnName + "=-1 or " + _SubCategoryTable.type_idColumn.ColumnName + "=" + (int)this.comboBoxFilterType.SelectedValue;

            _SubCategoryView.Sort = LibItemType.Entity.mt_item_type_sub_category.category_idColumn.ColumnName;

            this.comboBoxSubCategory.Enabled = false;
            if (_SubCategoryView.Count > 0)
            {
                this.comboBoxSubCategory.Enabled = true;
            }
        }
    }
}

