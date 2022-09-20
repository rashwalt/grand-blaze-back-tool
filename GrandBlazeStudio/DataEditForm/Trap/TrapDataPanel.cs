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

namespace DataEditForm.Trap
{
    public partial class TrapDataPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TrapDataPanel()
        {
            InitializeComponent();

            NothingFilter();
        }

        private EffectListEntity.effect_listDataTable _optionTable;

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
        /// キャンセル
        /// </summary>
        public override void Cancel()
        {
            TrapDataEntity entity = LibTrap.Entity;
            entity.RejectChanges();
        }

        private DataView TrapView;

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            TrapDataEntity entity = LibTrap.Entity;
            TrapView = new DataView(entity.mt_trap_list);
            TrapView.Sort = entity.mt_trap_list.trap_idColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = TrapView;
            this.columnNo.DataPropertyName = entity.mt_trap_list.trap_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.mt_trap_list.trap_nameColumn.ColumnName;
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
        /// <param name="TrapID">表示対象ID</param>
        private void PrivateView(int TrapID)
        {
            TrapDataEntity entity = LibTrap.Entity;

            if (TrapID == 0)
            {
                TrapID = LibInteger.GetNewUnderNum(entity.mt_trap_list, entity.mt_trap_list.trap_idColumn.ColumnName);
            }

            // 表示

            TrapDataEntity.mt_trap_listRow baseRow = entity.mt_trap_list.FindBytrap_id(TrapID);

            this.textBoxNo.Text = TrapID.ToString();

            if (baseRow == null)
            {
                // 新規に行追加
                baseRow = entity.mt_trap_list.Addmt_trap_listRow(TrapID, "", 0, 0, "0,0,0,0,0,0");
            }

            this.textBoxName.Text = baseRow.trap_name;

            this.numericUpDownHPDamageRate.Value = baseRow.hp_damage;
            this.numericUpDownMPDamageRate.Value = baseRow.mp_damage;

            _optionTable = new EffectListEntity.effect_listDataTable();
            LibEffect.Split(baseRow.effect, ref _optionTable, true, 0);

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
        }

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="TrapID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int TrapID, int rowIndex)
        {
            TrapDataEntity entity = LibTrap.Entity;

            TrapDataEntity.mt_trap_listRow tarentRow = entity.mt_trap_list.FindBytrap_id(TrapID);

            if (tarentRow == null) { return; }

            tarentRow.Delete();
        }

        /// <summary>
        /// データ複製
        /// </summary>
        /// <param name="TrapID">対象ID</param>
        private void CopyData(int TrapID)
        {
            TrapDataEntity entity = LibTrap.Entity;

            TrapDataEntity.mt_trap_listRow tarentRow = entity.mt_trap_list.FindBytrap_id(TrapID);

            if (tarentRow == null) { return; }

            int newID = LibInteger.GetNewUnderNum(entity.mt_trap_list, entity.mt_trap_list.trap_idColumn.ColumnName);

            // 上位から複製
            TrapDataEntity.mt_trap_listRow newTrapRow = entity.mt_trap_list.Newmt_trap_listRow();
            newTrapRow.ItemArray = tarentRow.ItemArray;
            newTrapRow.trap_id = newID;

            entity.mt_trap_list.Addmt_trap_listRow(newTrapRow);
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            TrapDataEntity entity = LibTrap.Entity;

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
                dba.Update(LibTrap.Entity.mt_trap_list);
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

            LibTrap.LoadTrap();
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            TrapDataEntity entity = LibTrap.Entity;

            if (this.textBoxName.Text.Length == 0) { return; }

            TrapDataEntity.mt_trap_listRow row = entity.mt_trap_list.FindBytrap_id(int.Parse(this.textBoxNo.Text));
            bool isAdd = false;

            if (row == null)
            {
                // 新規追加
                row = entity.mt_trap_list.Newmt_trap_listRow();
                isAdd = true;
                row.trap_id = int.Parse(this.textBoxNo.Text);
            }

            if (isAdd || row.trap_name != this.textBoxName.Text) { row.trap_name = this.textBoxName.Text; }

            if (isAdd || row.hp_damage != (int)this.numericUpDownHPDamageRate.Value) { row.hp_damage = (int)this.numericUpDownHPDamageRate.Value; }
            if (isAdd || row.mp_damage != (int)this.numericUpDownMPDamageRate.Value) { row.mp_damage = (int)this.numericUpDownMPDamageRate.Value; }

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

            if (isAdd || row.effect != effectString) { row.effect = effectString; }

            if (isAdd)
            {
                entity.mt_trap_list.Addmt_trap_listRow(row);
            }
        }
    }
}
