using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;
using System.IO;
using DataEditForm.Quest.PopMonster;
using DataEditForm.Quest.Weather;
using System.Diagnostics;

namespace DataEditForm.Quest
{
    public partial class MarkEditDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MarkEditDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="MarkID">マークID</param>
        public MarkEditDialog(int MarkID)
        {
            InitializeComponent();

            _markID = MarkID;

            // データバインド
            this.comboBoxQuest.DataSource = LibQuest.Entity.mt_quest_list;
            this.comboBoxQuest.ValueMember = LibQuest.Entity.mt_quest_list.quest_idColumn.ColumnName;
            this.comboBoxQuest.DisplayMember = LibQuest.Entity.mt_quest_list.quest_nameColumn.ColumnName;

            this.comboBoxFieldType.DataSource = LibField.Entity.mt_field_type_list;
            this.comboBoxFieldType.ValueMember = LibField.Entity.mt_field_type_list.field_idColumn.ColumnName;
            this.comboBoxFieldType.DisplayMember = LibField.Entity.mt_field_type_list.field_nameColumn.ColumnName;

            KeyItemEntity.mt_key_item_listDataTable KeyTable = (KeyItemEntity.mt_key_item_listDataTable)LibKeyItem.Entity.mt_key_item_list.Copy();
            KeyTable.Addmt_key_item_listRow(0, "なし", "", 0);

            KeyTable.DefaultView.Sort = LibKeyItem.Entity.mt_key_item_list.key_idColumn.ColumnName;

            this.comboBoxKeyItem.DataSource = KeyTable.DefaultView;
            this.comboBoxKeyItem.ValueMember = LibKeyItem.Entity.mt_key_item_list.key_idColumn.ColumnName;
            this.comboBoxKeyItem.DisplayMember = LibKeyItem.Entity.mt_key_item_list.keyitem_nameColumn.ColumnName;

            TrapDataEntity.mt_trap_listDataTable TrapTable = (TrapDataEntity.mt_trap_listDataTable)LibTrap.Entity.mt_trap_list.Copy();
            TrapTable.Addmt_trap_listRow(0, "なし", 0, 0, "0,0,0,0,0");

            TrapTable.DefaultView.Sort = LibTrap.Entity.mt_trap_list.trap_idColumn.ColumnName;

            this.comboBoxTraps.DataSource = TrapTable.DefaultView;
            this.comboBoxTraps.DisplayMember = LibTrap.Entity.mt_trap_list.trap_nameColumn.ColumnName;
            this.comboBoxTraps.ValueMember = LibTrap.Entity.mt_trap_list.trap_idColumn.ColumnName;
        }

        private int _markID = 0;

        public int DefaultAreaID = 0;

        private string DataP = "";
        private string DataE = "";

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkEditDialog_Load(object sender, EventArgs e)
        {
            QuestDataEntity entity = LibQuest.Entity;

            QuestDataEntity.mt_mark_listRow row = entity.mt_mark_list.FindBymark_id(_markID);

            this.textBoxNo.Text = _markID.ToString();

            if (DefaultAreaID > 0)
            {
                this.comboBoxQuest.SelectedValue = DefaultAreaID;
            }

            if (row == null) { return; }

            this.textBoxName.Text = row.mark_name;
            this.comboBoxQuest.SelectedValue = row.quest_id;

            this.textBoxWeather.Text = LibQuest.GetWeatherNames(_markID);

            this.comboBoxFieldType.SelectedValue = row.field_type;

            this.comboBoxTraps.SelectedIndex = row.mark_trap;
            this.numericUpDownTrapHide.Value = row.trap_hide;
            this.numericUpDownTrapLevel.Value = row.trap_level;
            this.numericUpDownCureRate.Value = row.cure_rate;
            this.comboBoxKeyItem.SelectedValue = row.key_item_id;
            this.numericUpDownKeyLevel.Value = row.key_level;
            this.numericUpDownHackLevel.Value = row.hack_level;
            this.numericUpDownPopMonsterLevel.Value = row.pop_monster_level;
            this.checkBoxHideMark.Checked = row.hide_mark;

            string file_p = LibCommonLibrarySettings.EventScriptDir + "event_" + _markID.ToString("000000") + "_a.py";

            if (File.Exists(file_p))
            {
                DataP = File.ReadAllText(file_p, Encoding.UTF8);
            }

            this.eventEditorP.Text = DataP;

            string file_e = LibCommonLibrarySettings.EventScriptDir + "event_" + _markID.ToString("000000") + "_z.py";

            if (File.Exists(file_e))
            {
                DataE = File.ReadAllText(file_e, Encoding.UTF8);
            }

            this.eventEditorE.Text = DataE;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            QuestDataEntity entity = LibQuest.Entity;

            bool isNew = false;
            QuestDataEntity.mt_mark_listRow row = entity.mt_mark_list.FindBymark_id(_markID);

            if (row == null)
            {
                row = entity.mt_mark_list.Newmt_mark_listRow();
                isNew = true;
            }

            if (isNew) { row.mark_id = _markID; }

            if (isNew || row.mark_name != this.textBoxName.Text) { row.mark_name = this.textBoxName.Text; }

            if (isNew || row.quest_id != (int)this.comboBoxQuest.SelectedValue) { row.quest_id = (int)this.comboBoxQuest.SelectedValue; }

            if (isNew || row.field_type != (int)this.comboBoxFieldType.SelectedValue) { row.field_type = (int)this.comboBoxFieldType.SelectedValue; }

            if (isNew || row.mark_trap != this.comboBoxTraps.SelectedIndex) { row.mark_trap = this.comboBoxTraps.SelectedIndex; }

            if (isNew || row.trap_hide != (int)this.numericUpDownTrapHide.Value) { row.trap_hide = (int)this.numericUpDownTrapHide.Value; }

            if (isNew || row.trap_level != (int)this.numericUpDownTrapLevel.Value) { row.trap_level = (int)this.numericUpDownTrapLevel.Value; }

            if (isNew || row.cure_rate != (int)this.numericUpDownCureRate.Value) { row.cure_rate = (int)this.numericUpDownCureRate.Value; }

            if (isNew || row.key_item_id != (int)this.comboBoxKeyItem.SelectedValue) { row.key_item_id = (int)this.comboBoxKeyItem.SelectedValue; }

            if (isNew || row.key_level != (int)this.numericUpDownKeyLevel.Value) { row.key_level = (int)this.numericUpDownKeyLevel.Value; }

            if (isNew || row.hack_level != (int)this.numericUpDownHackLevel.Value) { row.hack_level = (int)this.numericUpDownHackLevel.Value; }

            if (isNew || row.pop_monster_level != (int)this.numericUpDownPopMonsterLevel.Value) { row.pop_monster_level = (int)this.numericUpDownPopMonsterLevel.Value; }

            if (isNew || row.hide_mark != this.checkBoxHideMark.Checked) { row.hide_mark = this.checkBoxHideMark.Checked; }

            if (isNew) { entity.mt_mark_list.Addmt_mark_listRow(row); }

            if (isNew || DataP.ToMD5() != this.eventEditorP.Text.ToMD5())
            {
                string file = LibCommonLibrarySettings.EventScriptDir + "event_" + int.Parse(this.textBoxNo.Text).ToString("000000") + "_a.py";
                File.WriteAllText(file, this.eventEditorP.Text, Encoding.UTF8);
            }

            if (isNew || DataE.ToMD5() != this.eventEditorE.Text.ToMD5())
            {
                string file = LibCommonLibrarySettings.EventScriptDir + "event_" + int.Parse(this.textBoxNo.Text).ToString("000000") + "_z.py";
                File.WriteAllText(file, this.eventEditorE.Text, Encoding.UTF8);
            }

            //if (this.eventEditorPanel.Text.Length > 0)
            //{
            //    string file = LibCommonLibrarySettings.EventScriptDir + "mark_" + _markID.ToString("000000") + ".lua";
            //    File.WriteAllText(file, this.eventEditorPanel.Text, Encoding.GetEncoding("Shift_JIS"));
            //}

            //if (isNew || row.mark_event.ToMD5() != this.eventEditorPanel.Text.ToMD5()) { row.mark_event = this.eventEditorPanel.Text; }

            this.Close();
        }

        /// <summary>
        /// 出現モンスター設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPopMonster_Click(object sender, EventArgs e)
        {
            PopMonsterSettingDialog dialog = new PopMonsterSettingDialog(_markID);

            dialog.ShowDialog(this);
        }

        /// <summary>
        /// 天候設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWeather_Click(object sender, EventArgs e)
        {
            WeatherSettingDialog dialog = new WeatherSettingDialog();
            dialog.MarkID = _markID;
            dialog.ShowDialog(this);

            this.textBoxWeather.Text = LibQuest.GetWeatherNames(_markID);
        }

        /// <summary>
        /// プロローグ編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditP_Click(object sender, EventArgs e)
        {
            string file = LibCommonLibrarySettings.EventScriptDir + "event_" + _markID.ToString("000000") + "_a.py";
            File.WriteAllText(file, this.eventEditorP.Text, Encoding.UTF8);

            string emeditor = LibCommonLibrarySettings.EditorPath;

            Process extProcess = new Process();
            extProcess.StartInfo.FileName = emeditor;
            extProcess.StartInfo.Arguments = file;
            extProcess.Start();
        }

        /// <summary>
        /// エピローグ編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditE_Click(object sender, EventArgs e)
        {
            string file = LibCommonLibrarySettings.EventScriptDir + "event_" + _markID.ToString("000000") + "_z.py";
            File.WriteAllText(file, this.eventEditorE.Text, Encoding.UTF8);

            string emeditor = LibCommonLibrarySettings.EditorPath;

            Process extProcess = new Process();
            extProcess.StartInfo.FileName = emeditor;
            extProcess.StartInfo.Arguments = file;
            extProcess.Start();
        }
    }
}

