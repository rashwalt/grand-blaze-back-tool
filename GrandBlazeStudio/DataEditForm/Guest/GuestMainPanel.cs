using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataAccess;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using DataEditForm.Guest.Details;

namespace DataEditForm.Guest
{
    public partial class GuestMainPanel : CommonFormLibrary.ListBasePanel
    {
        public GuestMainPanel()
        {
            InitializeComponent();

            // データバインド
            this.comboBoxRaceType.DataSource = LibRace.Entity.mt_race_list;
            this.comboBoxRaceType.DisplayMember = LibRace.Entity.mt_race_list.race_nameColumn.ColumnName;
            this.comboBoxRaceType.ValueMember = LibRace.Entity.mt_race_list.race_idColumn.ColumnName;

            this.comboBoxInstall.DataSource = LibInstall.Entity.mt_install_class_list;
            this.comboBoxInstall.DisplayMember = LibInstall.Entity.mt_install_class_list.classnameColumn.ColumnName;
            this.comboBoxInstall.ValueMember = LibInstall.Entity.mt_install_class_list.install_idColumn.ColumnName;

            // 装備品
            CommonItemEntity.item_listDataTable ItemTable = (CommonItemEntity.item_listDataTable)LibItem.Entity.item_list.Copy();
            ItemTable.Additem_listRow(0, "装備なし", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, false, 0, "", -1, false, false, false, 0, 0, 0, 0, 0, false, 0, "", false);

            DataView EquipMain = new DataView(ItemTable);
            EquipMain.RowFilter = LibItem.Entity.item_list.it_equip_partsColumn.ColumnName + " in (-1, " + Status.EquipSpot.Main + ") and it_creatable=false";
            EquipMain.Sort = LibItem.Entity.item_list.it_numColumn.ColumnName;
            this.comboBoxEquipMain.DataSource = EquipMain;
            this.comboBoxEquipMain.DisplayMember = LibItem.Entity.item_list.it_nameColumn.ColumnName;
            this.comboBoxEquipMain.ValueMember = LibItem.Entity.item_list.it_numColumn.ColumnName;

            DataView EquipSub = new DataView(ItemTable);
            EquipSub.RowFilter = LibItem.Entity.item_list.it_equip_partsColumn.ColumnName + " in (-1, " + Status.EquipSpot.Sub + ") and it_creatable=false";
            EquipSub.Sort = LibItem.Entity.item_list.it_numColumn.ColumnName;
            this.comboBoxEquipSub.DataSource = EquipSub;
            this.comboBoxEquipSub.DisplayMember = LibItem.Entity.item_list.it_nameColumn.ColumnName;
            this.comboBoxEquipSub.ValueMember = LibItem.Entity.item_list.it_numColumn.ColumnName;

            DataView EquipHead = new DataView(ItemTable);
            EquipHead.RowFilter = LibItem.Entity.item_list.it_equip_partsColumn.ColumnName + " in (-1, " + Status.EquipSpot.Head + ") and it_creatable=false";
            EquipHead.Sort = LibItem.Entity.item_list.it_numColumn.ColumnName;
            this.comboBoxEquipHead.DataSource = EquipHead;
            this.comboBoxEquipHead.DisplayMember = LibItem.Entity.item_list.it_nameColumn.ColumnName;
            this.comboBoxEquipHead.ValueMember = LibItem.Entity.item_list.it_numColumn.ColumnName;

            DataView EquipBody = new DataView(ItemTable);
            EquipBody.RowFilter = LibItem.Entity.item_list.it_equip_partsColumn.ColumnName + " in (-1, " + Status.EquipSpot.Body + ") and it_creatable=false";
            EquipBody.Sort = LibItem.Entity.item_list.it_numColumn.ColumnName;
            this.comboBoxEquipBody.DataSource = EquipBody;
            this.comboBoxEquipBody.DisplayMember = LibItem.Entity.item_list.it_nameColumn.ColumnName;
            this.comboBoxEquipBody.ValueMember = LibItem.Entity.item_list.it_numColumn.ColumnName;

            DataView EquipAcce = new DataView(ItemTable);
            EquipAcce.RowFilter = LibItem.Entity.item_list.it_equip_partsColumn.ColumnName + " in (-1, " + Status.EquipSpot.Accesory + ") and it_creatable=false";
            EquipAcce.Sort = LibItem.Entity.item_list.it_numColumn.ColumnName;
            this.comboBoxEquipAcce1.DataSource = EquipAcce;
            this.comboBoxEquipAcce1.DisplayMember = LibItem.Entity.item_list.it_nameColumn.ColumnName;
            this.comboBoxEquipAcce1.ValueMember = LibItem.Entity.item_list.it_numColumn.ColumnName;

            this.textBoxNo.Text = "0";
        }

        private DataView _guestView;
        private EffectListEntity.effect_listDataTable _optionTable;

        private int SelectedNo
        {
            get { return int.Parse(this.textBoxNo.Text); }
        }

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
                PrivateView((int)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value);
            }
            else
            {
                PrivateView(0);
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        public override void Cancel()
        {
            GuestDataEntity entity = LibGuestData.Entity;
            entity.RejectChanges();
        }

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            GuestDataEntity entity = LibGuestData.Entity;
            _guestView = new DataView(entity.mt_guest_list);
            _guestView.RowFilter = "";
            _guestView.Sort = entity.mt_guest_list.guest_idColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = _guestView;
            this.columnNo.DataPropertyName = entity.mt_guest_list.guest_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.mt_guest_list.character_nameColumn.ColumnName;
        }

        /// <summary>
        /// 詳細表示
        /// </summary>
        /// <param name="GuestID">表示対象ID</param>
        private void PrivateView(int GuestID)
        {
            if (GuestID < 0) { return; }

            GuestDataEntity entity = LibGuestData.Entity;

            if (GuestID == 0)
            {
                GuestID = LibInteger.GetNewUnderNum(entity.mt_guest_list, entity.mt_guest_list.guest_idColumn.ColumnName);
            }

            // 基本情報の表示
            GuestDataEntity.mt_guest_listRow baseRow = entity.mt_guest_list.FindByguest_id(GuestID);

            this.textBoxNo.Text = GuestID.ToString();

            if (baseRow == null)
            {
                // 新規に行追加
                this.textBoxName.Text = "";

                this.textBoxNickName.Text = "";
                this.comboBoxRaceType.SelectedIndex = 0;
                this.textBoxUniqueName.Text = "";
                this.comboBoxGuestKb.SelectedIndex = 0;

                this.comboBoxInstall.SelectedIndex = 0;
                this.comboBoxFormation.SelectedIndex = 0;
                this.numericUpDownLevel.Value = 1;
                this.numericUpDownLevelEdit.Value = 0;

                this.effectSettingPanel.Rows.Clear();

                this.comboBoxEquipMain.SelectedIndex = 0;
                this.comboBoxEquipSub.SelectedIndex = 0;
                this.comboBoxEquipHead.SelectedIndex = 0;
                this.comboBoxEquipBody.SelectedIndex = 0;
                this.comboBoxEquipAcce1.SelectedIndex = 0;

                this.comboBoxAtkType.SelectedIndex = 0;
                this.comboBoxDfeType.SelectedIndex = 0;
                this.comboBoxMgrType.SelectedIndex = 0;

                return;
            }

            this.textBoxName.Text = baseRow.character_name;

            this.textBoxNickName.Text = baseRow.nick_name;
            this.comboBoxRaceType.SelectedValue = baseRow.race_id;
            this.textBoxUniqueName.Text = baseRow.unique_name;
            this.comboBoxGuestKb.SelectedIndex = baseRow.belong_kb;

            // バトル情報の表示
            GuestDataEntity.mt_guest_battle_abilityRow battleRow = entity.mt_guest_battle_ability.FindByguest_id(GuestID);

            this.comboBoxInstall.SelectedValue = battleRow.install;
            this.comboBoxFormation.SelectedIndex = battleRow.formation;
            this.numericUpDownLevel.Value = battleRow.level;
            this.numericUpDownLevelEdit.Value = battleRow.level_edit;

            _optionTable = new EffectListEntity.effect_listDataTable();
            LibEffect.Split(battleRow.option_list, ref _optionTable, true, 0);

            this.effectSettingPanel.Rows.Clear();

            foreach (EffectListEntity.effect_listRow EffectRow in _optionTable)
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

            // 所持品情報の表示
            GuestDataEntity.mt_guest_have_itemRow itemRow = entity.mt_guest_have_item.FindByguest_id(GuestID);

            this.comboBoxEquipMain.SelectedValue = itemRow.equip_main;
            this.comboBoxEquipSub.SelectedValue = itemRow.equip_sub;
            this.comboBoxEquipHead.SelectedValue = itemRow.equip_head;
            this.comboBoxEquipBody.SelectedValue = itemRow.equip_body;
            this.comboBoxEquipAcce1.SelectedValue = itemRow.equip_accesory1;

            this.comboBoxAtkType.SelectedIndex = battleRow.atk_type;
            this.comboBoxDfeType.SelectedIndex = battleRow.dfe_type;
            this.comboBoxMgrType.SelectedIndex = battleRow.mgr_type;
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
                PrivateView((int)this.dataGridViewList[0, e.RowIndex].Value);
            }
        }

        /// <summary>
        /// コンテキストメニュー判定（リスト）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void contextMenuStripList_Opening(object sender, CancelEventArgs e)
        {
            // コピー、削除を有効に
            if (this.dataGridViewList.SelectedCells.Count > 0 && this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value != null)
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
                CopyData((int)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value);
            }
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            PrivateView(0);
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
                DeleteData((int)this.dataGridViewList[0, this.dataGridViewList.SelectedCells[0].RowIndex].Value, this.dataGridViewList.SelectedCells[0].RowIndex);
            }

            if (this.dataGridViewList[0, 0].Value != null)
            {
                this.dataGridViewList.Rows[0].Selected = true;
                PrivateView((int)this.dataGridViewList[0, 0].Value);
            }
        }

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="GuestID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int GuestID, int rowIndex)
        {
            GuestDataEntity entity = LibGuestData.Entity;

            GuestDataEntity.mt_guest_listRow monsterRow = entity.mt_guest_list.FindByguest_id(GuestID);

            if (monsterRow == null) { return; }

            monsterRow.Delete();

            entity.mt_guest_action.DefaultView.RowFilter = entity.mt_guest_action.guest_idColumn.ColumnName + "=" + GuestID;
            foreach (DataRowView actionRow in entity.mt_guest_action.DefaultView)
            {
                actionRow.Delete();
            }

            entity.mt_guest_battle_ability.DefaultView.RowFilter = entity.mt_guest_battle_ability.guest_idColumn.ColumnName + "=" + GuestID;
            foreach (DataRowView abilityRow in entity.mt_guest_battle_ability.DefaultView)
            {
                abilityRow.Delete();
            }

            entity.mt_guest_have_item.DefaultView.RowFilter = entity.mt_guest_have_item.guest_idColumn.ColumnName + "=" + GuestID;
            foreach (DataRowView itemRow in entity.mt_guest_have_item.DefaultView)
            {
                itemRow.Delete();
            }

            entity.mt_guest_serif.DefaultView.RowFilter = entity.mt_guest_action.guest_idColumn.ColumnName + "=" + GuestID;
            foreach (DataRowView serifRow in entity.mt_guest_serif.DefaultView)
            {
                serifRow.Delete();
            }
        }

        /// <summary>
        /// データ複製
        /// </summary>
        /// <param name="GuestID">対象ID</param>
        private void CopyData(int GuestID)
        {
            GuestDataEntity entity = LibGuestData.Entity;

            GuestDataEntity.mt_guest_listRow monsterRow = entity.mt_guest_list.FindByguest_id(GuestID);

            if (monsterRow == null) { return; }

            // 新規ID発行
            int newID = LibInteger.GetNewUnderNum(entity.mt_guest_list, entity.mt_guest_list.guest_idColumn.ColumnName);

            // 複製
            GuestDataEntity.mt_guest_listRow newGuestRow = entity.mt_guest_list.Newmt_guest_listRow();
            newGuestRow.ItemArray = monsterRow.ItemArray;
            newGuestRow.guest_id = newID;

            entity.mt_guest_list.Addmt_guest_listRow(newGuestRow);

            entity.mt_guest_have_item.DefaultView.RowFilter = entity.mt_guest_have_item.guest_idColumn.ColumnName + "=" + GuestID;
            foreach (DataRowView itemRow in entity.mt_guest_have_item.DefaultView)
            {
                GuestDataEntity.mt_guest_have_itemRow newItemRow = entity.mt_guest_have_item.Newmt_guest_have_itemRow();
                newItemRow.ItemArray = itemRow.Row.ItemArray;
                newItemRow.guest_id = newID;

                entity.mt_guest_have_item.Addmt_guest_have_itemRow(newItemRow);
            }

            entity.mt_guest_battle_ability.DefaultView.RowFilter = entity.mt_guest_battle_ability.guest_idColumn.ColumnName + "=" + GuestID;
            foreach (DataRowView abilityRow in entity.mt_guest_battle_ability.DefaultView)
            {
                GuestDataEntity.mt_guest_battle_abilityRow newAbilityRow = entity.mt_guest_battle_ability.Newmt_guest_battle_abilityRow();
                newAbilityRow.ItemArray = abilityRow.Row.ItemArray;
                newAbilityRow.guest_id = newID;

                entity.mt_guest_battle_ability.Addmt_guest_battle_abilityRow(newAbilityRow);
            }

            entity.mt_guest_action.DefaultView.RowFilter = entity.mt_guest_action.guest_idColumn.ColumnName + "=" + GuestID;
            foreach (DataRowView actionRow in entity.mt_guest_action.DefaultView)
            {
                GuestDataEntity.mt_guest_actionRow newActionRow = entity.mt_guest_action.Newmt_guest_actionRow();
                newActionRow.ItemArray = actionRow.Row.ItemArray;
                newActionRow.guest_id = newID;

                entity.mt_guest_action.Addmt_guest_actionRow(newActionRow);
            }

            entity.mt_guest_serif.DefaultView.RowFilter = entity.mt_guest_serif.guest_idColumn.ColumnName + "=" + GuestID;
            foreach (DataRowView serifRow in entity.mt_guest_serif.DefaultView)
            {
                GuestDataEntity.mt_guest_serifRow newSerifRow = entity.mt_guest_serif.Newmt_guest_serifRow();
                newSerifRow.ItemArray = serifRow.Row.ItemArray;
                newSerifRow.guest_id = newID;

                entity.mt_guest_serif.Addmt_guest_serifRow(newSerifRow);
            }
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            GuestDataEntity entity = LibGuestData.Entity;

            return entity.GetChanges() != null;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            UpdateEntity();

            LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master);

            try
            {
                dba.BeginTransaction();

                dba.Update(LibGuestData.Entity.mt_guest_list);

                dba.Update(LibGuestData.Entity.mt_guest_have_item);
                dba.Update(LibGuestData.Entity.mt_guest_battle_ability);
                dba.Update(LibGuestData.Entity.mt_guest_action);
                dba.Update(LibGuestData.Entity.mt_guest_serif);
                dba.Commit();
            }
            catch
            {
                dba.Rollback();
            }
            finally
            {
                dba.Dispose();
            }

            LibGuestData.LoadData();
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            GuestDataEntity entity = LibGuestData.Entity;

            if (this.textBoxName.Text.Length == 0) { return; }

            GuestDataEntity.mt_guest_listRow row = entity.mt_guest_list.FindByguest_id(int.Parse(this.textBoxNo.Text));
            GuestDataEntity.mt_guest_battle_abilityRow battleRow = entity.mt_guest_battle_ability.FindByguest_id(int.Parse(this.textBoxNo.Text));
            GuestDataEntity.mt_guest_have_itemRow itemRow = entity.mt_guest_have_item.FindByguest_id(int.Parse(this.textBoxNo.Text));

            bool isNew = false;

            if (row == null)
            {
                row = entity.mt_guest_list.Newmt_guest_listRow();

                row.guest_id = int.Parse(this.textBoxNo.Text);

                battleRow = entity.mt_guest_battle_ability.Newmt_guest_battle_abilityRow();

                battleRow.guest_id = int.Parse(this.textBoxNo.Text);

                itemRow = entity.mt_guest_have_item.Newmt_guest_have_itemRow();

                itemRow.guest_id = int.Parse(this.textBoxNo.Text);

                isNew = true;
            }

            if (isNew || row.character_name != this.textBoxName.Text) { row.character_name = this.textBoxName.Text; }

            if (isNew || row.nick_name != this.textBoxNickName.Text) { row.nick_name = this.textBoxNickName.Text; }
            if (isNew || row.race_id != (int)this.comboBoxRaceType.SelectedValue) { row.race_id = (int)this.comboBoxRaceType.SelectedValue; }
            if (isNew || row.unique_name != this.textBoxUniqueName.Text) { row.unique_name = this.textBoxUniqueName.Text; }
            if (isNew || row.belong_kb != this.comboBoxGuestKb.SelectedIndex) { row.belong_kb = this.comboBoxGuestKb.SelectedIndex; }

            if (isNew || battleRow.install != (int)this.comboBoxInstall.SelectedValue) { battleRow.install = (int)this.comboBoxInstall.SelectedValue; }
            if (isNew || battleRow.formation != this.comboBoxFormation.SelectedIndex) { battleRow.formation = this.comboBoxFormation.SelectedIndex; }
            if (isNew || battleRow.level != (int)this.numericUpDownLevel.Value) { battleRow.level = (int)this.numericUpDownLevel.Value; }
            if (isNew || battleRow.level_edit != (int)this.numericUpDownLevelEdit.Value) { battleRow.level_edit = (int)this.numericUpDownLevelEdit.Value; }

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

            if (isNew || battleRow.option_list != effectString) { battleRow.option_list = effectString; }

            if (isNew || itemRow.equip_main != (int)this.comboBoxEquipMain.SelectedValue) { itemRow.equip_main = (int)this.comboBoxEquipMain.SelectedValue; }
            if (isNew || itemRow.equip_sub != (int)this.comboBoxEquipSub.SelectedValue) { itemRow.equip_sub = (int)this.comboBoxEquipSub.SelectedValue; }
            if (isNew || itemRow.equip_head != (int)this.comboBoxEquipHead.SelectedValue) { itemRow.equip_head = (int)this.comboBoxEquipHead.SelectedValue; }
            if (isNew || itemRow.equip_body != (int)this.comboBoxEquipBody.SelectedValue) { itemRow.equip_body = (int)this.comboBoxEquipBody.SelectedValue; }
            if (isNew || itemRow.equip_accesory1 != (int)this.comboBoxEquipAcce1.SelectedValue) { itemRow.equip_accesory1 = (int)this.comboBoxEquipAcce1.SelectedValue; }

            if (isNew || battleRow.atk_type != this.comboBoxAtkType.SelectedIndex) { battleRow.atk_type = this.comboBoxAtkType.SelectedIndex; }
            if (isNew || battleRow.dfe_type != this.comboBoxDfeType.SelectedIndex) { battleRow.dfe_type = this.comboBoxDfeType.SelectedIndex; }
            if (isNew || battleRow.mgr_type != this.comboBoxMgrType.SelectedIndex) { battleRow.mgr_type = this.comboBoxMgrType.SelectedIndex; }

            if (isNew)
            {
                entity.mt_guest_list.Addmt_guest_listRow(row);
                entity.mt_guest_battle_ability.Addmt_guest_battle_abilityRow(battleRow);
                entity.mt_guest_have_item.Addmt_guest_have_itemRow(itemRow);
            }
        }

        /// <summary>
        /// フィルタ実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void toolStripMenuItemFilter_Click(object sender, EventArgs e)
        {
            GuestFilterDialog dialog = new GuestFilterDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _guestView.RowFilter = dialog.FilterString;
            }
        }

        /// <summary>
        /// 戦闘行動設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCommonAction_Click(object sender, EventArgs e)
        {
            GuestDataEntity entity = LibGuestData.Entity;
            GuestActionDialog dialog = new GuestActionDialog(SelectedNo, entity.mt_guest_action);
            dialog.Show(this);
        }

        /// <summary>
        /// セリフ設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSerif_Click(object sender, EventArgs e)
        {
            GuestDataEntity entity = LibGuestData.Entity;
            GuestSerifDialog dialog = new GuestSerifDialog(SelectedNo, entity.mt_guest_serif);
            dialog.Show(this);
        }

        /// <summary>
        /// 規定値の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewElementalArts_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["columnGuestNo"].Value = int.Parse(this.textBoxNo.Text);
        }
    }
}
