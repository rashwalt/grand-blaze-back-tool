using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using System.Collections;
using CommonLibrary.DataFormat.Entity;
using CommonFormLibrary.CommonDialog;
using System.IO;
using System.Diagnostics;

namespace DataEditForm.Item
{
    public partial class ItemMainPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemMainPanel()
        {
            InitializeComponent();

            _subCategoryView = new DataView(LibItemType.Entity.mt_item_type_sub_category);
            _subCategoryView.RowFilter = "";

            // データバインド
            this.comboBoxItemType.DataSource = LibItemType.Entity.mt_item_type;
            this.comboBoxItemType.DisplayMember = LibItemType.Entity.mt_item_type.typeColumn.ColumnName;
            this.comboBoxItemType.ValueMember = LibItemType.Entity.mt_item_type.type_idColumn.ColumnName;

            this.comboBoxItemSubCategory.DataSource = _subCategoryView;
            this.comboBoxItemSubCategory.DisplayMember = LibItemType.Entity.mt_item_type_sub_category.sub_categoryColumn.ColumnName;
            this.comboBoxItemSubCategory.ValueMember = LibItemType.Entity.mt_item_type_sub_category.category_idColumn.ColumnName;

            this.comboBoxHalfSell.SelectedIndex = 0;

            _filter = new Hashtable();

            this.textBoxNo.Text = "0";
        }

        private DataView _subCategoryView;
        private DataView _itemView;
        private Hashtable _filter;
        private bool IsView = false;

        /// <summary>
        /// 初期表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Panel_Load(object sender, EventArgs e)
        {
            LoadData();
            if (this.dataGridViewList.SelectedCells.Count > 0 && this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value != null)
            {
                PrivateView((int)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value, (bool)((DataRowView)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].OwningRow.DataBoundItem)[LibItem.Entity.item_list.it_creatableColumn.ColumnName]);
            }
            else
            {
                PrivateView(0, false);
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        public override void Cancel()
        {
            CommonItemEntity entity = LibItem.Entity;
            entity.RejectChanges();
        }

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            CommonItemEntity entity = LibItem.Entity;
            _itemView = new DataView(entity.item_list);
            _itemView.RowFilter = entity.item_list.it_creatableColumn.ColumnName + "=false";
            _itemView.Sort = entity.item_list.it_numColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = _itemView;
            this.columnNo.DataPropertyName = entity.item_list.it_numColumn.ColumnName;
            this.columnName.DataPropertyName = entity.item_list.it_nameColumn.ColumnName;

            // インストールクラス
            this.checkedListBoxInstall.Items.Clear();
            foreach (InstallDataEntity.mt_install_class_listRow InstallRow in LibInstall.Entity.mt_install_class_list)
            {
                this.checkedListBoxInstall.Items.Add(InstallRow.classname);
            }
        }

        private string ViewText = "";

        /// <summary>
        /// 詳細表示
        /// </summary>
        /// <param name="ItemID">表示対象ID</param>
        /// <param name="IsCreated">合成品か</param>
        private void PrivateView(int ItemID, bool IsCreated)
        {
            CommonItemEntity entity = LibItem.Entity;

            if (ItemID == 0)
            {
                // 新規ID発行
                DataView itemViewNumber = new DataView(entity.item_list);
                itemViewNumber.RowFilter = entity.item_list.it_creatableColumn.ColumnName + "=false";

                SetNewNumberDialog dialog = new SetNewNumberDialog();
                dialog.SetNewNumber(LibInteger.GetNewUnderNum(itemViewNumber, entity.item_list.it_numColumn.ColumnName));
                dialog.ValidatingNumber += new EventHandler(Validate_Number);

                switch (dialog.ShowDialog(this))
                {
                    case DialogResult.OK:
                        ItemID = dialog.NewID;
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            if (ItemID <= 0) { return; }

            // 基本情報の表示

            CommonItemEntity.item_listRow baseRow = entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            this.SuspendLayout();
            IsView = true;

            this.textBoxNo.Text = ItemID.ToString();

            if (baseRow == null)
            {
                // 新規に行追加
                baseRow = entity.item_list.Newitem_listRow();

                #region 新規設定
                baseRow.it_num = ItemID;
                baseRow.it_name = "";
                baseRow.it_physics = 0;
                baseRow.it_sorcery = 0;
                baseRow.it_physics_parry = 0;
                baseRow.it_sorcery_parry = 0;
                baseRow.it_critical = 0;
                baseRow.it_metal = 0;
                baseRow.it_charge = 0;
                baseRow.it_range = 0;
                baseRow.it_type = 0;
                baseRow.it_sub_category = 0;
                baseRow.it_attack_type = 0;
                baseRow.it_comment = "";
                baseRow.it_fire = 0;
                baseRow.it_freeze = 0;
                baseRow.it_air = 0;
                baseRow.it_earth = 0;
                baseRow.it_water = 0;
                baseRow.it_thunder = 0;
                baseRow.it_holy = 0;
                baseRow.it_dark = 0;
                baseRow.it_slash = 0;
                baseRow.it_pierce = 0;
                baseRow.it_strike = 0;
                baseRow.it_break = 0;
                baseRow.it_effect = "0,0,0,0,0,0";
                baseRow.it_ok_sex = 0;
                baseRow.it_ok_race = 0;
                baseRow.it_both_hand = false;
                baseRow.it_use_item = 0;
                baseRow.it_equip_install = "";
                baseRow.it_equip_parts = 0;
                baseRow.it_rare = false;
                baseRow.it_bind = false;
                baseRow.it_quest = false;
                baseRow.it_shop = 0;
                baseRow.it_equip_level = 0;
                baseRow.it_target_area = 0;
                baseRow.it_price = 0;
                baseRow.it_seller = 0;
                baseRow.it_stack = 0;
                baseRow.it_creatable = false;
                baseRow.use_script = "";
                baseRow.use_battle = false;
                #endregion

                entity.item_list.Additem_listRow(baseRow);
            }

            this.textBoxName.Text = baseRow.it_name;
            this.comboBoxItemType.SelectedValue = baseRow.it_type;
            this.comboBoxItemSubCategory.SelectedValue = baseRow.it_sub_category;
            this.comboBoxAttackType.SelectedIndex = baseRow.it_attack_type + 1;

            this.numericUpDownMoney.Value = baseRow.it_price;
            this.numericUpDownSeller.Value = baseRow.it_seller;

            this.numericUpDownPhysics.Value = baseRow.it_physics;
            this.numericUpDownSorcery.Value = baseRow.it_sorcery;
            this.numericUpDownPhysicsParry.Value = baseRow.it_physics_parry;
            this.numericUpDownSorceryParry.Value = baseRow.it_sorcery_parry;
            this.numericUpDownCritical.Value = baseRow.it_critical;
            this.numericUpDownMetal.Value = baseRow.it_metal;
            this.comboBoxRange.SelectedIndex = baseRow.it_range;
            this.numericUpDownCharge.Value = baseRow.it_charge;

            this.numericUpDownFire.Value = baseRow.it_fire;
            this.numericUpDownFreeze.Value = baseRow.it_freeze;
            this.numericUpDownAir.Value = baseRow.it_air;
            this.numericUpDownEarth.Value = baseRow.it_earth;
            this.numericUpDownWater.Value = baseRow.it_water;
            this.numericUpDownThunder.Value = baseRow.it_thunder;
            this.numericUpDownHoly.Value = baseRow.it_holy;
            this.numericUpDownDark.Value = baseRow.it_dark;
            this.numericUpDownSlash.Value = baseRow.it_slash;
            this.numericUpDownPierce.Value = baseRow.it_pierce;
            this.numericUpDownStrike.Value = baseRow.it_strike;
            this.numericUpDownBreak.Value = baseRow.it_break;

            // エフェクトリスト
            EffectListEntity.effect_listDataTable effectTable = new EffectListEntity.effect_listDataTable();
            LibEffect.Split(baseRow.it_effect, ref effectTable, true, 0);

            this.effectSettingPanel.Rows.Clear();

            foreach (EffectListEntity.effect_listRow EffectRow in effectTable)
            {
                int rowIndex = this.effectSettingPanel.Rows.Add(
                    EffectRow.effect_id.ToString("0000") + ": " + EffectRow.name,
                    EffectRow.rank,
                    EffectRow.sub_rank,
                    EffectRow.prob,
                    EffectRow.endlimit,
                    EffectRow.hide_fg
                    );

                DataGridViewRow row = this.effectSettingPanel.Rows[rowIndex];
                row.Tag = EffectRow;
            }

            this.textBoxItemComment.Text = baseRow.it_comment.Replace("<br />", "\r\n");

            // 装備箇所
            this.comboBoxEquipParts.SelectedIndex = baseRow.it_equip_parts;

            // ターゲット
            this.comboBoxTargetArea.SelectedIndex = baseRow.it_target_area;

            // 装備制限
            this.comboBoxSex.SelectedIndex = baseRow.it_ok_sex;
            this.comboBoxRace.SelectedIndex = baseRow.it_ok_race;
            this.numericUpDownLevel.Value = baseRow.it_equip_level;

            // 装備可能インストールクラス
            string[] EquipInstallClassList = baseRow.it_equip_install.Split(',');
            for (int i = 0; i < this.checkedListBoxInstall.Items.Count; i++)
            {
                this.checkedListBoxInstall.SetItemChecked(i, false);
            }

            foreach (string ClassList in EquipInstallClassList)
            {
                if (ClassList != "0" && ClassList.Length > 0)
                {
                    this.checkedListBoxInstall.SetItemChecked(int.Parse(ClassList) - 1, true);
                }
            }

            // その他設定
            this.checkBoxBothWeapon.Checked = baseRow.it_both_hand;
            this.checkBoxRare.Checked = baseRow.it_rare;
            this.checkBoxQuest.Checked = baseRow.it_quest;
            this.numericUpDownUseItem.Value = baseRow.it_use_item;
            this.checkBoxBind.Checked = baseRow.it_bind;
            this.numericUpDownStack.Value = baseRow.it_stack;
            this.comboBoxShop.SelectedIndex = baseRow.it_shop;
            this.checkBoxUseBattle.Checked = baseRow.use_battle;

            this.checkBoxCreatable.Checked = baseRow.it_creatable;

            ViewText = baseRow.use_script;

            if (ViewText.Length > 0)
            {
                this.labelUseEvent.Text = "○";
            }
            else
            {
                this.labelUseEvent.Text = "×";
            }

            CheckDouble();

            this.ResumeLayout();
            IsView = false;
        }

        /// <summary>
        /// リアルタイムにセル内容を変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dataGridViewList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((bool)((DataRowView)this.dataGridViewList[0, e.RowIndex].OwningRow.DataBoundItem)[LibItem.Entity.item_list.it_creatableColumn.ColumnName])
            {
                e.CellStyle.ForeColor = Color.Tomato;
            }
        }

        /// <summary>
        /// カテゴリ反映
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxItemType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.comboBoxItemType.SelectedValue == null || this.comboBoxItemType.SelectedValue.GetType() != typeof(int)) { return; }

            _subCategoryView.RowFilter = LibItemType.Entity.mt_item_type_sub_category.type_idColumn.ColumnName + "=" + (int)this.comboBoxItemType.SelectedValue;

            this.comboBoxItemSubCategory.Enabled = false;
            if (_subCategoryView.Count > 0)
            {
                this.comboBoxItemSubCategory.Enabled = true;
            }

            ItemTypeEntity.mt_item_typeRow row = LibItemType.Entity.mt_item_type.FindBytype_id((int)this.comboBoxItemType.SelectedValue);

            if (row != null)
            {
                // 装備箇所
                this.comboBoxEquipParts.SelectedIndex = row.equip_spot;

                // その他設定
                this.checkBoxBothWeapon.Checked = row.both_hand;
                this.numericUpDownUseItem.Value = row.use_item;
                this.numericUpDownStack.Value = row.stack_count;

                // ターゲット
                this.comboBoxTargetArea.SelectedIndex = row.target_area;
            }
        }

        /// <summary>
        /// 選択：詳細表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dataGridViewList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            base.dataGridViewList_CellMouseClick(sender, e);

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && this.dataGridViewList[0, e.RowIndex].Value != null)
            {
                UpdateEntity();
                PrivateView((int)this.dataGridViewList[0, e.RowIndex].Value, (bool)((DataRowView)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].OwningRow.DataBoundItem)[LibItem.Entity.item_list.it_creatableColumn.ColumnName]);
            }
        }

        /// <summary>
        /// コンテキストメニュー判定（リスト）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void contextMenuStripList_Opening(object sender, CancelEventArgs e)
        {
            // コピー、削除を有効に(合成品は無理)
            if (this.dataGridViewList.SelectedCells.Count > 0 && this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value != null && !(bool)((DataRowView)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].OwningRow.DataBoundItem)[LibItem.Entity.item_list.it_creatableColumn.ColumnName])
            {
                this.toolStripMenuItemCopy.Enabled = true;
                this.toolStripMenuItemDelete.Enabled = true;
            }
        }

        /// <summary>
        /// 複製
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewList.SelectedCells.Count > 0 && this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value != null)
            {
                // 複製実行
                CopyData((int)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value, (bool)((DataRowView)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].OwningRow.DataBoundItem)[LibItem.Entity.item_list.it_creatableColumn.ColumnName]);
            }
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            PrivateView(0, false);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewList.SelectedCells.Count > 0 && this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value != null)
            {
                DeleteData((int)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value, (bool)((DataRowView)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].OwningRow.DataBoundItem)[LibItem.Entity.item_list.it_creatableColumn.ColumnName], this.dataGridViewList.SelectedCells[0].RowIndex);
            }
        }

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="ItemID">対象ID</param>
        /// <param name="IsCreated">合成品か</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int ItemID, bool IsCreated, int rowIndex)
        {
            CommonItemEntity entity = LibItem.Entity;

            CommonItemEntity.item_listRow itemRow = entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (itemRow == null) { return; }

            itemRow.Delete();

            if (this.dataGridViewList.SelectedCells.Count > 0 && this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value != null)
            {
                PrivateView((int)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value, (bool)((DataRowView)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].OwningRow.DataBoundItem)[LibItem.Entity.item_list.it_creatableColumn.ColumnName]);
            }
            else
            {
                PrivateView(0, false);
            }
        }

        /// <summary>
        /// 番号重複管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Validate_Number(object sender, EventArgs e)
        {
            CommonItemEntity entity = LibItem.Entity;
            SetNewNumberDialog dialog = (SetNewNumberDialog)sender;
            CommonItemEntity.item_listRow itemRow = entity.item_list.FindByit_numit_creatable(dialog.NewID, false);

            if (itemRow != null)
            {
                dialog.labelCaution.Visible = true;
            }
            else
            {
                dialog.labelCaution.Visible = false;
            }
        }

        /// <summary>
        /// データ複製
        /// </summary>
        /// <param name="ItemID">対象ID</param>
        /// <param name="IsCreated">合成品か</param>
        private void CopyData(int ItemID, bool IsCreated)
        {
            CommonItemEntity entity = LibItem.Entity;

            if (IsCreated) { return; }

            CommonItemEntity.item_listRow itemRow = entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (itemRow == null) { return; }

            // 新規ID発行
            DataView itemViewNumber = new DataView(entity.item_list);
            itemViewNumber.RowFilter = entity.item_list.it_creatableColumn.ColumnName + "=false";

            SetNewNumberDialog dialog = new SetNewNumberDialog();
            dialog.SetNewNumber(LibInteger.GetNewUnderNum(itemViewNumber, entity.item_list.it_numColumn.ColumnName));
            dialog.ValidatingNumber += new EventHandler(Validate_Number);

            int NewID = 0;

            switch (dialog.ShowDialog(this))
            {
                case DialogResult.OK:
                    NewID = dialog.NewID;
                    break;
                case DialogResult.Cancel:
                    return;
            }

            // 上位から複製
            CommonItemEntity.item_listRow newItemRow = entity.item_list.Newitem_listRow();
            newItemRow.ItemArray = itemRow.ItemArray;
            newItemRow.it_num = NewID;

            if (entity.item_list.FindByit_numit_creatable(NewID, false) != null)
            {
                MessageBox.Show("重複するNoのアイテムが存在します。", "重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            entity.item_list.Additem_listRow(newItemRow);
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            CommonItemEntity entity = LibItem.Entity;

            return entity.GetChanges() != null;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            UpdateEntity();
            LibItem.Update();
        }

        private int SelectedNo
        {
            get { return int.Parse(this.textBoxNo.Text); }
        }

        /// <summary>
        /// フィルタ実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void toolStripMenuItemFilter_Click(object sender, EventArgs e)
        {
            ItemFilterDialog dialog = new ItemFilterDialog();
            if (_filter.Count > 0)
            {
                dialog.GetFilterSelected = _filter;
            }
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _filter = dialog.GetFilterSelected;
                _itemView.RowFilter = dialog.FilterString;
            }
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            CommonItemEntity entity = LibItem.Entity;

            CommonItemEntity.item_listRow row = entity.item_list.FindByit_numit_creatable(int.Parse(this.textBoxNo.Text), this.checkBoxCreatable.Checked);

            bool isNew = false;
            if (row == null)
            {
                row = entity.item_list.Newitem_listRow();
                isNew = true;
            }

            if (isNew) { row.it_num = int.Parse(this.textBoxNo.Text); }
            if (isNew) { row.it_creatable = this.checkBoxCreatable.Checked; }

            if (isNew || row.it_name != this.textBoxName.Text) { row.it_name = this.textBoxName.Text; }
            if (isNew || row.it_type != (int)this.comboBoxItemType.SelectedValue) { row.it_type = (int)this.comboBoxItemType.SelectedValue; }

            int subCategory = 0;
            if (this.comboBoxItemSubCategory.SelectedValue != null) { subCategory = (int)this.comboBoxItemSubCategory.SelectedValue; }

            if (isNew || row.it_sub_category != subCategory) { row.it_sub_category = subCategory; }

            if (isNew || row.it_attack_type != (this.comboBoxAttackType.SelectedIndex - 1)) { row.it_attack_type = this.comboBoxAttackType.SelectedIndex - 1; }

            if (isNew || row.it_price != this.numericUpDownMoney.Value) { row.it_price = this.numericUpDownMoney.Value; }
            if (isNew || row.it_seller != this.numericUpDownSeller.Value) { row.it_seller = this.numericUpDownSeller.Value; }

            if (isNew || row.it_physics != (int)this.numericUpDownPhysics.Value) { row.it_physics = (int)this.numericUpDownPhysics.Value; }
            if (isNew || row.it_sorcery != (int)this.numericUpDownSorcery.Value) { row.it_sorcery = (int)this.numericUpDownSorcery.Value; }
            if (isNew || row.it_physics_parry != (int)this.numericUpDownPhysicsParry.Value) { row.it_physics_parry = (int)this.numericUpDownPhysicsParry.Value; }
            if (isNew || row.it_sorcery_parry != (int)this.numericUpDownSorceryParry.Value) { row.it_sorcery_parry = (int)this.numericUpDownSorceryParry.Value; }
            if (isNew || row.it_critical != (int)this.numericUpDownCritical.Value) { row.it_critical = (int)this.numericUpDownCritical.Value; }
            if (isNew || row.it_metal != (int)this.numericUpDownMetal.Value) { row.it_metal = (int)this.numericUpDownMetal.Value; }
            if (isNew || row.it_range != this.comboBoxRange.SelectedIndex) { row.it_range = this.comboBoxRange.SelectedIndex; }
            if (isNew || row.it_charge != (int)this.numericUpDownCharge.Value) { row.it_charge = (int)this.numericUpDownCharge.Value; }

            if (isNew || row.it_fire != (int)this.numericUpDownFire.Value) { row.it_fire = (int)this.numericUpDownFire.Value; }
            if (isNew || row.it_freeze != (int)this.numericUpDownFreeze.Value) { row.it_freeze = (int)this.numericUpDownFreeze.Value; }
            if (isNew || row.it_air != (int)this.numericUpDownAir.Value) { row.it_air = (int)this.numericUpDownAir.Value; }
            if (isNew || row.it_earth != (int)this.numericUpDownEarth.Value) { row.it_earth = (int)this.numericUpDownEarth.Value; }
            if (isNew || row.it_water != (int)this.numericUpDownWater.Value) { row.it_water = (int)this.numericUpDownWater.Value; }
            if (isNew || row.it_thunder != (int)this.numericUpDownThunder.Value) { row.it_thunder = (int)this.numericUpDownThunder.Value; }
            if (isNew || row.it_holy != (int)this.numericUpDownHoly.Value) { row.it_holy = (int)this.numericUpDownHoly.Value; }
            if (isNew || row.it_dark != (int)this.numericUpDownDark.Value) { row.it_dark = (int)this.numericUpDownDark.Value; }
            if (isNew || row.it_slash != (int)this.numericUpDownSlash.Value) { row.it_slash = (int)this.numericUpDownSlash.Value; }
            if (isNew || row.it_pierce != (int)this.numericUpDownPierce.Value) { row.it_pierce = (int)this.numericUpDownPierce.Value; }
            if (isNew || row.it_strike != (int)this.numericUpDownStrike.Value) { row.it_strike = (int)this.numericUpDownStrike.Value; }
            if (isNew || row.it_break != (int)this.numericUpDownBreak.Value) { row.it_break = (int)this.numericUpDownBreak.Value; }

            // エフェクトリスト
            EffectListEntity.effect_listDataTable table = new EffectListEntity.effect_listDataTable();
            foreach (DataGridViewRow viewRow in this.effectSettingPanel.Rows)
            {
                EffectListEntity.effect_listRow EffectRow = (EffectListEntity.effect_listRow)viewRow.Tag;
                EffectListEntity.effect_listRow newEffectRow = table.Neweffect_listRow();
                newEffectRow.ItemArray = EffectRow.ItemArray;

                table.Addeffect_listRow(newEffectRow);
            }

            string effectString = "";
            LibEffect.Join(ref effectString, table);

            if (isNew || row.it_effect != effectString) { row.it_effect = effectString; }

            string inComment = this.textBoxItemComment.Text.Replace("\r\n", "<br />");

            if (isNew || row.it_comment != inComment) { row.it_comment = inComment; }

            // 装備箇所
            if (isNew || row.it_equip_parts != this.comboBoxEquipParts.SelectedIndex) { row.it_equip_parts = this.comboBoxEquipParts.SelectedIndex; }

            // ターゲット
            if (isNew || row.it_target_area != this.comboBoxTargetArea.SelectedIndex) { row.it_target_area = this.comboBoxTargetArea.SelectedIndex; }

            // 装備制限
            if (isNew || row.it_ok_sex != this.comboBoxSex.SelectedIndex) { row.it_ok_sex = this.comboBoxSex.SelectedIndex; }
            if (isNew || row.it_ok_race != this.comboBoxRace.SelectedIndex) { row.it_ok_race = this.comboBoxRace.SelectedIndex; }
            if (isNew || row.it_equip_level != (int)this.numericUpDownLevel.Value) { row.it_equip_level = (int)this.numericUpDownLevel.Value; }

            // 装備可能インストールクラス
            string Installs = "";
            List<string> InstallLists = new List<string>();
            for (int i = 0; i < this.checkedListBoxInstall.Items.Count; i++)
            {
                if (this.checkedListBoxInstall.GetItemChecked(i))
                {
                    InstallLists.Add((i + 1).ToString());
                }
                else
                {
                    InstallLists.Add("0");
                }
            }
            Installs = string.Join(",", InstallLists.ToArray());

            if (isNew || row.it_equip_install != Installs) { row.it_equip_install = Installs; }

            // その他設定
            if (isNew || row.it_both_hand != this.checkBoxBothWeapon.Checked) { row.it_both_hand = this.checkBoxBothWeapon.Checked; }
            if (isNew || row.it_rare != this.checkBoxRare.Checked) { row.it_rare = this.checkBoxRare.Checked; }
            if (isNew || row.it_quest != this.checkBoxQuest.Checked) { row.it_quest = this.checkBoxQuest.Checked; }
            if (isNew || row.it_use_item != (int)this.numericUpDownUseItem.Value) { row.it_use_item = (int)this.numericUpDownUseItem.Value; }
            if (isNew || row.it_bind != this.checkBoxBind.Checked) { row.it_bind = this.checkBoxBind.Checked; }
            if (isNew || row.it_stack != (int)this.numericUpDownStack.Value) { row.it_stack = (int)this.numericUpDownStack.Value; }
            if (isNew || row.it_shop != this.comboBoxShop.SelectedIndex) { row.it_shop = this.comboBoxShop.SelectedIndex; }
            if (isNew || row.use_battle != this.checkBoxUseBattle.Checked) { row.use_battle = this.checkBoxUseBattle.Checked; }

            if (isNew || row.use_script.ToMD5() != ViewText.ToMD5()) { row.use_script = ViewText; }

            if (isNew)
            {
                entity.item_list.Additem_listRow(row);
            }
        }

        /// <summary>
        /// サブカテゴリ設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxItemSubCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            if (IsView) { return; }
            if (!this.comboBoxItemSubCategory.Enabled) { return; }
            if (this.comboBoxItemSubCategory.SelectedValue == null) { return; }
            if (this.comboBoxItemSubCategory.SelectedValue.GetType() != typeof(int)) { return; }

            ItemTypeEntity.mt_item_type_sub_categoryRow row = LibItemType.Entity.mt_item_type_sub_category.FindBycategory_id((int)this.comboBoxItemSubCategory.SelectedValue);

            if (row != null)
            {
                this.comboBoxRange.SelectedIndex = row.range;

                this.numericUpDownFire.Value = row.it_fire;
                this.numericUpDownFreeze.Value = row.it_freeze;
                this.numericUpDownAir.Value = row.it_air;
                this.numericUpDownEarth.Value = row.it_earth;
                this.numericUpDownWater.Value = row.it_water;
                this.numericUpDownThunder.Value = row.it_thunder;
                this.numericUpDownHoly.Value = row.it_holy;
                this.numericUpDownDark.Value = row.it_dark;
                this.numericUpDownSlash.Value = row.it_slash;
                this.numericUpDownPierce.Value = row.it_pierce;
                this.numericUpDownStrike.Value = row.it_strike;
                this.numericUpDownBreak.Value = row.it_break;

                this.numericUpDownCharge.Value = row.charge;
                this.numericUpDownCritical.Value = row.it_critical;
                this.numericUpDownPhysicsParry.Value = row.it_avoid;

                this.comboBoxAttackType.SelectedIndex = row.it_attack_type + 1;

                // エフェクトリスト
                EffectListEntity.effect_listDataTable EffectTable = new EffectListEntity.effect_listDataTable();
                LibEffect.Split(row.effects, ref EffectTable);

                this.effectSettingPanel.Rows.Clear();

                foreach (EffectListEntity.effect_listRow EffectRow in EffectTable)
                {
                    int rowIndex = this.effectSettingPanel.Rows.Add(
                        EffectRow.effect_id.ToString("0000") + ": " + EffectRow.name,
                        EffectRow.rank,
                        EffectRow.sub_rank,
                        EffectRow.prob,
                        EffectRow.endlimit,
                        EffectRow.hide_fg
                        );

                    DataGridViewRow EffectViewRow = this.effectSettingPanel.Rows[rowIndex];
                    EffectViewRow.Tag = EffectRow;
                }
            }
        }

        /// <summary>
        /// 売却倍率変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxHalfSell_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsView) { return; }
            this.numericUpDownSeller.Value = (int)(this.numericUpDownMoney.Value / (this.comboBoxHalfSell.SelectedIndex + 1));
        }

        /// <summary>
        /// イベント設定（外部エディタ起動）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEvent_Click(object sender, EventArgs e)
        {
            //string file = LibCommonLibrarySettings.EventScriptDir + "itemevent_" + int.Parse(this.textBoxNo.Text).ToString("000000") + ".lua";

            //if (!File.Exists(file))
            //{
            //    File.WriteAllText(file, "", Encoding.GetEncoding("Shift_JIS"));
            //}

            //string emeditor = LibCommonLibrarySettings.EditorPath;

            //Process extProcess = new Process();
            //extProcess.StartInfo.FileName = emeditor;
            //extProcess.StartInfo.Arguments = file;
            //extProcess.Start();

            ItemUseEventDialog dialog = new ItemUseEventDialog();
            dialog.ScriptText = ViewText;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ViewText = dialog.ScriptText;

                this.labelUseEvent.Text = "○";
            }
        }

        private void linkLabelAllSelect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < this.checkedListBoxInstall.Items.Count; i++)
            {
                this.checkedListBoxInstall.SetItemChecked(i, true);
            }
        }

        private void linkLabelAllDeselect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < this.checkedListBoxInstall.Items.Count; i++)
            {
                this.checkedListBoxInstall.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// 重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            CheckDouble();
        }

        private void CheckDouble()
        {
            CommonItemEntity entity = LibItem.Entity;
            DataView nameView = new DataView(entity.item_list);
            nameView.RowFilter = entity.item_list.it_numColumn.ColumnName + "<>" + this.textBoxNo.Text + " and " + entity.item_list.it_nameColumn.ColumnName + "=" + LibSql.EscapeString(this.textBoxName.Text);
            if (nameView.Count > 0)
            {
                this.labelItemNameCheck.Visible = true;
            }
            else
            {
                this.labelItemNameCheck.Visible = false;
            }
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckDouble();
        }
    }
}
