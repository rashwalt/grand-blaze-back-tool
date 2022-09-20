using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;
using CommonFormLibrary.CommonDialog;

namespace CommonFormLibrary.CommonPanel
{
    public partial class EffectSettingPanel : UserControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EffectSettingPanel()
        {
            InitializeComponent();
        }

        private EffectListEntity.effect_listDataTable _effectTable = new EffectListEntity.effect_listDataTable();

        /// <summary>
        /// コンテキストメニュー表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEffect_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // コンテキスト表示
                this.contextMenuStripEdit.Show(Control.MousePosition.X, Control.MousePosition.Y);
            }
        }

        /// <summary>
        /// 行
        /// </summary>
        public DataGridViewRowCollection Rows
        {
            get
            {
                return this.dataGridViewEffect.Rows;
            }
        }

        /// <summary>
        /// セル右クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEffect_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.Button == MouseButtons.Right)
            {
                DataGridViewCell cell = this.dataGridViewEffect[e.ColumnIndex, e.RowIndex];
                // セルの選択状態を反転
                cell.Selected = true;
            }
        }

        /// <summary>
        /// 選択イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEffect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewEffect.SelectedCells.Count == 0)
            {
                AddItem();
            }
            else
            {
                EditItem();
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
        /// エフェクト追加
        /// </summary>
        /// <param name="EffectID">エフェクトID</param>
        /// <param name="Rank">ランク</param>
        /// <param name="SubRank">サブランク</param>
        /// <param name="Prob">確率</param>
        /// <param name="EndLimit">持続カウント</param>
        /// <param name="IsHide">説明を表示するか</param>
        public void EffectAdd(int EffectID, decimal Rank, decimal SubRank, decimal Prob, int EndLimit, bool IsHide)
        {
            bool isAdd = true;

            foreach (DataGridViewRow row in this.dataGridViewEffect.Rows)
            {
                EffectListEntity.effect_listRow EffectRow = (EffectListEntity.effect_listRow)row.Tag;

                if (EffectRow.effect_id == EffectID)
                {
                    // 編集
                    EffectRow.rank = Rank;
                    EffectRow.sub_rank = SubRank;
                    EffectRow.prob = Prob;
                    EffectRow.endlimit = EndLimit;
                    EffectRow.hide_fg = IsHide;

                    row.Cells[1].Value = Rank;
                    row.Cells[2].Value = SubRank;
                    row.Cells[3].Value = Prob;
                    row.Cells[4].Value = EndLimit;
                    row.Cells[5].Value = IsHide;

                    row.Tag = EffectRow;

                    isAdd = false;
                }
            }

            if (isAdd)
            {
                // 追加
                EffectListEntity.effect_listRow EffectNewRow = _effectTable.Neweffect_listRow();
                EffectNewRow.effect_id = EffectID;
                EffectNewRow.rank = Rank;
                EffectNewRow.sub_rank = SubRank;
                EffectNewRow.prob = Prob;
                EffectNewRow.endlimit = EndLimit;
                EffectNewRow.name = LibEffect.GetName(EffectID);
                EffectNewRow.effect_div = 0;
                EffectNewRow.memo = "";
                EffectNewRow.viewname = "";
                EffectNewRow.hide_fg = IsHide;

                int rowIndex = this.dataGridViewEffect.Rows.Add(
                    EffectNewRow.effect_id.ToString("0000") + ": " + EffectNewRow.name,
                    EffectNewRow.rank,
                    EffectNewRow.sub_rank,
                    EffectNewRow.prob,
                    EffectNewRow.endlimit,
                    EffectNewRow.hide_fg
                    );

                DataGridViewRow newRow = this.dataGridViewEffect.Rows[rowIndex];
                newRow.Tag = EffectNewRow;
            }
        }

        /// <summary>
        /// アイテム追加
        /// </summary>
        private void AddItem()
        {
            SelectionEffectDialog dialog = new SelectionEffectDialog();

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                EffectAdd(dialog.EffectID, dialog.Rank, dialog.SubRank, dialog.Prob, dialog.EndLimit, dialog.IsHide);
            }
        }

        /// <summary>
        /// 編集表示確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripEdit_Opening(object sender, CancelEventArgs e)
        {
            if (this.dataGridViewEffect.SelectedCells.Count > 0)
            {
                this.toolStripMenuItemItemEdit.Enabled = true;
            }
        }

        /// <summary>
        /// 追加イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemItemAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        /// <summary>
        /// アイテムの削除
        /// </summary>
        private void DeleteItem()
        {
            if (this.dataGridViewEffect.SelectedCells.Count == 0) { return; }

            int rowIndex = this.dataGridViewEffect.SelectedCells[0].RowIndex;

            this.dataGridViewEffect.Rows.RemoveAt(rowIndex);
        }

        /// <summary>
        /// アイテム編集
        /// </summary>
        private void EditItem()
        {
            if (this.dataGridViewEffect.SelectedCells.Count == 0) { return; }

            int rowIndex = this.dataGridViewEffect.SelectedCells[0].RowIndex;

            EffectListEntity.effect_listRow EffectRow = (EffectListEntity.effect_listRow)this.dataGridViewEffect.Rows[rowIndex].Tag;

            SelectionEffectDialog dialog = new SelectionEffectDialog();

            dialog.SetData(EffectRow.effect_id, EffectRow.rank, EffectRow.sub_rank, EffectRow.prob, EffectRow.endlimit, EffectRow.hide_fg);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                EffectAdd(dialog.EffectID, dialog.Rank, dialog.SubRank, dialog.Prob, dialog.EndLimit, dialog.IsHide);
            }
        }

        /// <summary>
        /// キー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEffect_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    DeleteItem();
                    break;
                case Keys.Insert:
                    AddItem();
                    break;
            }
        }
    }
}
