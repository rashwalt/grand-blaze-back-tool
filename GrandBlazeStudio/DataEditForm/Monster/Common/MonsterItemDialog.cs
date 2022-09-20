using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;
using CommonFormLibrary.CommonPanel;

namespace DataEditForm.Monster.Common
{
    public partial class MonsterItemDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public MonsterItemDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="MonsterID">�����X�^�[ID</param>
        /// <param name="ItemTable">���f��e�[�u��</param>
        public MonsterItemDialog(int MonsterID, MonsterDataEntity.mt_monster_have_itemDataTable ItemTable)
        {
            InitializeComponent();

            _monsterID = MonsterID;

            _table = ItemTable;
        }

        private int _monsterID = 0;
        private MonsterDataEntity.mt_monster_have_itemDataTable _table;
        private DataView _itemDropView;
        private DataView _itemStielView;
        private DataView _itemPoacherView;

        /// <summary>
        /// ��ʕ\��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonsterItemDialog_Load(object sender, EventArgs e)
        {
            _itemDropView = new DataView(_table);
            _itemDropView.RowFilter = _table.monster_idColumn.ColumnName + "=" + _monsterID + " and " + _table.drop_typeColumn.ColumnName + "=0";
            _itemDropView.Sort = _table.get_synxColumn.ColumnName;

            this.itemSelectionPanelDrop.DataSource = _itemDropView;
            this.itemSelectionPanelDrop.DataPropertyNameProb = _table.get_synxColumn.ColumnName;
            this.itemSelectionPanelDrop.DataPropertyNameItemName = _table.it_numColumn.ColumnName;
            this.itemSelectionPanelDrop.DataPropertyNameCount = _table.it_box_countColumn.ColumnName;

            _itemStielView = new DataView(_table);
            _itemStielView.RowFilter = _table.monster_idColumn.ColumnName + "=" + _monsterID + " and " + _table.drop_typeColumn.ColumnName + "=1";
            _itemStielView.Sort = _table.get_synxColumn.ColumnName;

            this.itemSelectionPanelStiel.DataSource = _itemStielView;
            this.itemSelectionPanelStiel.DataPropertyNameProb = _table.get_synxColumn.ColumnName;
            this.itemSelectionPanelStiel.DataPropertyNameItemName = _table.it_numColumn.ColumnName;
            this.itemSelectionPanelStiel.DataPropertyNameCount = _table.it_box_countColumn.ColumnName;

            _itemPoacherView = new DataView(_table);
            _itemPoacherView.RowFilter = _table.monster_idColumn.ColumnName + "=" + _monsterID + " and " + _table.drop_typeColumn.ColumnName + "=2";
            _itemPoacherView.Sort = _table.get_synxColumn.ColumnName;

            this.itemSelectionPanelPoacher.DataSource = _itemPoacherView;
            this.itemSelectionPanelPoacher.DataPropertyNameProb = _table.get_synxColumn.ColumnName;
            this.itemSelectionPanelPoacher.DataPropertyNameItemName = _table.it_numColumn.ColumnName;
            this.itemSelectionPanelPoacher.DataPropertyNameCount = _table.it_box_countColumn.ColumnName;
        }

        /// <summary>
        /// �L�����Z��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// �폜�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemSelectionPanelDrop_DeleteClick(object sender, SelectionItemEventArgs e)
        {
            MonsterDataEntity.mt_monster_have_itemRow newRow = _table.FindBymonster_iddrop_typeget_synx(_monsterID, 0, e.Prob);

            if (newRow != null)
            {
                newRow.Delete();
            }
        }

        /// <summary>
        /// �ҏW�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemSelectionPanelDrop_EditClick(object sender, SelectionItemEventArgs e)
        {
            MonsterDataEntity.mt_monster_have_itemRow newRow = _table.FindBymonster_iddrop_typeget_synx(_monsterID, 0, e.Prob);

            if (newRow != null)
            {
                newRow.it_num = e.ItemID;
                newRow.it_box_count = e.ItemCount;
            }
            else
            {
                MonsterDataEntity.mt_monster_have_itemRow newThisRow = _table.Newmt_monster_have_itemRow();
                newThisRow.monster_id = _monsterID;
                newThisRow.drop_type = 0;
                newThisRow.it_num = e.ItemID;
                newThisRow.it_box_count = e.ItemCount;
                newThisRow.get_synx = e.Prob;

                _table.Addmt_monster_have_itemRow(newThisRow);
            }
        }

        /// <summary>
        /// �폜�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemSelectionPanelStiel_DeleteClick(object sender, SelectionItemEventArgs e)
        {
            MonsterDataEntity.mt_monster_have_itemRow newRow = _table.FindBymonster_iddrop_typeget_synx(_monsterID, 1, e.Prob);

            if (newRow != null)
            {
                newRow.Delete();
            }
        }

        /// <summary>
        /// �ҏW�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemSelectionPanelStiel_EditClick(object sender, SelectionItemEventArgs e)
        {
            MonsterDataEntity.mt_monster_have_itemRow newRow = _table.FindBymonster_iddrop_typeget_synx(_monsterID, 1, e.Prob);

            if (newRow != null)
            {
                newRow.it_num = e.ItemID;
                newRow.it_box_count = e.ItemCount;
            }
            else
            {
                MonsterDataEntity.mt_monster_have_itemRow newThisRow = _table.Newmt_monster_have_itemRow();
                newThisRow.monster_id = _monsterID;
                newThisRow.drop_type = 1;
                newThisRow.it_num = e.ItemID;
                newThisRow.it_box_count = e.ItemCount;
                newThisRow.get_synx = e.Prob;

                _table.Addmt_monster_have_itemRow(newThisRow);
            }
        }

        /// <summary>
        /// �폜�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemSelectionPanelPoacher_DeleteClick(object sender, SelectionItemEventArgs e)
        {
            MonsterDataEntity.mt_monster_have_itemRow newRow = _table.FindBymonster_iddrop_typeget_synx(_monsterID, 2, e.Prob);

            if (newRow != null)
            {
                newRow.Delete();
            }
        }

        /// <summary>
        /// �ҏW�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemSelectionPanelPoacher_EditClick(object sender, SelectionItemEventArgs e)
        {
            MonsterDataEntity.mt_monster_have_itemRow newRow = _table.FindBymonster_iddrop_typeget_synx(_monsterID, 2, e.Prob);

            if (newRow != null)
            {
                newRow.it_num = e.ItemID;
                newRow.it_box_count = e.ItemCount;
            }
            else
            {
                MonsterDataEntity.mt_monster_have_itemRow newThisRow = _table.Newmt_monster_have_itemRow();
                newThisRow.monster_id = _monsterID;
                newThisRow.drop_type = 2;
                newThisRow.it_num = e.ItemID;
                newThisRow.it_box_count = e.ItemCount;
                newThisRow.get_synx = e.Prob;

                _table.Addmt_monster_have_itemRow(newThisRow);
            }
        }

        /// <summary>
        /// �X�e�B�[���ɓ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSync_Click(object sender, EventArgs e)
        {
            if (_itemStielView.Count == 0)
            {
                foreach (DataRowView TresureRow in _itemDropView)
                {
                    if ((int)TresureRow["get_synx"] == 5)
                    {
                        continue;
                    }
                    MonsterDataEntity.mt_monster_have_itemRow newThisRow = _table.Newmt_monster_have_itemRow();
                    newThisRow.ItemArray = TresureRow.Row.ItemArray;
                    newThisRow.drop_type = 1;

                    _table.Addmt_monster_have_itemRow(newThisRow);
                }
            }
            else
            {
                MessageBox.Show("�X�e�B�[���ɂ̓A�C�e�������łɑ��݂��邽�߁A�����ł��܂���B", "�X�e�B�[���ɓ����G���[", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// �����ɓ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSync2_Click(object sender, EventArgs e)
        {
            if (_itemPoacherView.Count == 0)
            {
                foreach (DataRowView TresureRow in _itemDropView)
                {
                    if ((int)TresureRow["get_synx"] == 5)
                    {
                        continue;
                    }
                    MonsterDataEntity.mt_monster_have_itemRow newThisRow = _table.Newmt_monster_have_itemRow();
                    newThisRow.ItemArray = TresureRow.Row.ItemArray;
                    newThisRow.drop_type = 2;

                    _table.Addmt_monster_have_itemRow(newThisRow);
                }
            }
            else
            {
                MessageBox.Show("�����ɂ̓A�C�e�������łɑ��݂��邽�߁A�����ł��܂���B", "�����ɓ����G���[", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

