using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;

namespace DataEditForm.Monster.Data
{
    public partial class MonsterDataFilterDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MonsterDataFilterDialog()
        {
            InitializeComponent();

            // データバインド

            MonsterDataEntity.mt_monster_categoryDataTable RaceTable = (MonsterDataEntity.mt_monster_categoryDataTable)LibMonsterData.Entity.mt_monster_category.Copy();
            RaceTable.Addmt_monster_categoryRow(-1, "すべて");

            RaceTable.DefaultView.Sort = LibMonsterData.Entity.mt_monster_category.category_idColumn.ColumnName;

            this.comboBoxRace.DataSource = RaceTable.DefaultView;
            this.comboBoxRace.DisplayMember = LibMonsterData.Entity.mt_monster_category.category_nameColumn.ColumnName;
            this.comboBoxRace.ValueMember = LibMonsterData.Entity.mt_monster_category.category_idColumn.ColumnName;

            QuestDataEntity.quest_listDataTable QuestTable = (QuestDataEntity.quest_listDataTable)LibQuest.Entity.mt_quest_list.Copy();
            QuestTable.Addquest_listRow(-1, "すべて", "", 0, 0, false, true, 0, 0, 0, 0, 0, 0, 0);

            QuestTable.DefaultView.Sort = LibQuest.Entity.mt_quest_list.quest_idColumn.ColumnName;

            this.comboBoxQuest.DataSource = QuestTable.DefaultView;
            this.comboBoxQuest.DisplayMember = LibQuest.Entity.mt_quest_list.quest_nameColumn.ColumnName;
            this.comboBoxQuest.ValueMember = LibQuest.Entity.mt_quest_list.quest_idColumn.ColumnName;
        }

        /// <summary>
        /// フィルタ文字列
        /// </summary>
        public string FilterString
        {
            get
            {
                List<string> filter = new List<string>();

                // 種族
                if (this.comboBoxRace.SelectedIndex > 0)
                {
                    filter.Add(LibMonsterData.Entity.mt_monster_list.category_idColumn.ColumnName + "=" + this.comboBoxRace.SelectedValue);
                }

                // 出現エリア/マーク
                if (this.comboBoxQuest.SelectedIndex > 0)
                {
                    List<string> monsterIds = new List<string>();

                    if (this.comboBoxMark.SelectedIndex > 0)
                    {
                        // マーク限定
                        LibQuest.Entity.mt_mark_pop_monster.DefaultView.RowFilter = "mark_id=" + (int)this.comboBoxMark.SelectedValue;

                        foreach (DataRowView popRow in LibQuest.Entity.mt_mark_pop_monster.DefaultView)
                        {
                            monsterIds.Add(popRow["monster_id"].ToString());
                        }
                    }
                    else
                    {
                        // クエスト限定
                        QuestDataEntity.quest_listRow questRow = LibQuest.Entity.mt_quest_list.FindByquest_id((int)this.comboBoxQuest.SelectedValue);
                        foreach (QuestDataEntity.mt_mark_listRow markRow in questRow.Getmt_mark_listRows())
                        {
                            LibQuest.Entity.mt_mark_pop_monster.DefaultView.RowFilter = "mark_id=" + markRow.mark_id;

                            foreach (DataRowView popRow in LibQuest.Entity.mt_mark_pop_monster.DefaultView)
                            {
                                monsterIds.Add(popRow["monster_id"].ToString());
                            }
                        }
                    }

                    if (monsterIds.Count > 0)
                    {
                        filter.Add("monster_id in (" + string.Join(",", monsterIds.ToArray()) + ")");
                    }
                    else
                    {
                        filter.Add("monster_id=-1");
                    }
                }

                return string.Join(" AND ", filter.ToArray());
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
        /// 初期表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonsterDataFilterDialog_Load(object sender, EventArgs e)
        {
            DefalutFilterSet();
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
        /// 初期表示モード
        /// </summary>
        private void DefalutFilterSet()
        {
            this.comboBoxRace.SelectedIndex = 0;
        }

        /// <summary>
        /// マーク限定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxArea_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.comboBoxQuest.SelectedIndex == 0) { this.comboBoxMark.Enabled = false; return; }
            else { this.comboBoxMark.Enabled = true; }

            // マーク限定

            QuestDataEntity.mt_mark_listDataTable MarkTable = (QuestDataEntity.mt_mark_listDataTable)LibQuest.Entity.mt_mark_list.Copy();
            QuestDataEntity.mt_mark_listRow newRow = MarkTable.Newmt_mark_listRow();
            newRow.mark_id = -1;
            newRow.quest_id = (int)this.comboBoxQuest.SelectedValue;
            newRow.field_type = 0;
            newRow.mark_name = "すべて";

            MarkTable.Addmt_mark_listRow(newRow);

            MarkTable.DefaultView.Sort = LibQuest.Entity.mt_mark_list.mark_idColumn.ColumnName;

            this.comboBoxMark.DataSource = MarkTable.DefaultView;
            this.comboBoxMark.DisplayMember = LibQuest.Entity.mt_mark_list.mark_nameColumn.ColumnName;
            this.comboBoxMark.ValueMember = LibQuest.Entity.mt_mark_list.mark_idColumn.ColumnName;
        }
    }
}

