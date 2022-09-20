using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;
using CommonLibrary.DataAccess;

namespace DataEditForm.SetBonus
{
    public partial class SetBonusPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetBonusPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        public override void Cancel()
        {
            SetBonusEntity entity = LibSetBonus.Entity;
            entity.RejectChanges();
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
                dba.Update(LibSetBonus.Entity.mt_set_bonus_list);
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

            LibSetBonus.LoadSetBonus();
        }

        /// <summary>
        /// 画面表示
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
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            SetBonusEntity entity = LibSetBonus.Entity;

            return entity.GetChanges() != null;
        }

        private DataView SetView;

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            SetBonusEntity entity = LibSetBonus.Entity;
            SetView = new DataView(entity.mt_set_bonus_list);
            SetView.Sort = entity.mt_set_bonus_list.set_idColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = SetView;
            this.columnNo.DataPropertyName = entity.mt_set_bonus_list.set_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.mt_set_bonus_list.set_nameColumn.ColumnName;
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
        }

        /// <summary>
        /// 詳細表示
        /// </summary>
        /// <param name="SetID">表示対象ID</param>
        private void PrivateView(int SetID)
        {
            SetBonusEntity entity = LibSetBonus.Entity;

            if (SetID == 0)
            {
                SetID = LibInteger.GetNewUnderNum(entity.mt_set_bonus_list, entity.mt_set_bonus_list.set_idColumn.ColumnName);
            }

            // 表示

            SetBonusEntity.mt_set_bonus_listRow baseRow = entity.mt_set_bonus_list.FindByset_id(SetID);

            this.textBoxNo.Text = SetID.ToString();

            if (baseRow == null)
            {
                // 新規に行追加

                return;
            }

            this.textBoxName.Text = baseRow.set_name;

            // エフェクトリスト
            EffectListEntity.effect_listDataTable effectTable = new EffectListEntity.effect_listDataTable();
            LibEffect.Split(baseRow.set_effect, ref effectTable, true, 0);

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

            // 対応部位
            this.checkedListBoxEquip.SetItemChecked(0, baseRow.equip_main);
            this.checkedListBoxEquip.SetItemChecked(1, baseRow.equip_sub);
            this.checkedListBoxEquip.SetItemChecked(4, baseRow.equip_head);
            this.checkedListBoxEquip.SetItemChecked(5, baseRow.equip_body);
            this.checkedListBoxEquip.SetItemChecked(9, baseRow.equip_accesory);
        }

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="SetID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int SetID, int rowIndex)
        {
            SetBonusEntity entity = LibSetBonus.Entity;

            SetBonusEntity.mt_set_bonus_listRow setRow = entity.mt_set_bonus_list.FindByset_id(SetID);

            if (setRow == null) { return; }

            setRow.Delete();
        }

        /// <summary>
        /// データ複製
        /// </summary>
        /// <param name="SetID">対象ID</param>
        private void CopyData(int SetID)
        {
            SetBonusEntity entity = LibSetBonus.Entity;

            SetBonusEntity.mt_set_bonus_listRow setRow = entity.mt_set_bonus_list.FindByset_id(SetID);

            if (setRow == null) { return; }

            int newID = LibInteger.GetNewUnderNum(entity.mt_set_bonus_list, entity.mt_set_bonus_list.set_idColumn.ColumnName);

            // 上位から複製
            SetBonusEntity.mt_set_bonus_listRow newSetRow = entity.mt_set_bonus_list.Newmt_set_bonus_listRow();
            newSetRow.ItemArray = setRow.ItemArray;
            newSetRow.set_id = newID;

            entity.mt_set_bonus_list.Addmt_set_bonus_listRow(newSetRow);
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            SetBonusEntity entity = LibSetBonus.Entity;

            if (this.textBoxName.Text.Length == 0) { return; }

            SetBonusEntity.mt_set_bonus_listRow row = entity.mt_set_bonus_list.FindByset_id(int.Parse(this.textBoxNo.Text));
            bool isAdd = false;

            if (row == null)
            {
                // 新規追加
                row = entity.mt_set_bonus_list.Newmt_set_bonus_listRow();
                isAdd = true;
                row.set_id = int.Parse(this.textBoxNo.Text);
            }

            if (isAdd || row.set_name != this.textBoxName.Text) { row.set_name = this.textBoxName.Text; }

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

            if (isAdd || row.set_effect != effectString) { row.set_effect = effectString; }

            // 対応部位
            if (isAdd || row.equip_main != this.checkedListBoxEquip.GetItemChecked(0)) { row.equip_main = this.checkedListBoxEquip.GetItemChecked(0); }
            if (isAdd || row.equip_sub != this.checkedListBoxEquip.GetItemChecked(1)) { row.equip_sub = this.checkedListBoxEquip.GetItemChecked(1); }
            if (isAdd || row.equip_head != this.checkedListBoxEquip.GetItemChecked(4)) { row.equip_head = this.checkedListBoxEquip.GetItemChecked(2); }
            if (isAdd || row.equip_body != this.checkedListBoxEquip.GetItemChecked(5)) { row.equip_body = this.checkedListBoxEquip.GetItemChecked(3); }
            if (isAdd || row.equip_accesory != this.checkedListBoxEquip.GetItemChecked(9)) { row.equip_accesory = this.checkedListBoxEquip.GetItemChecked(4); }

            if (isAdd)
            {
                entity.mt_set_bonus_list.Addmt_set_bonus_listRow(row);
            }
        }
    }
}
