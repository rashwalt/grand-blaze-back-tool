using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;
using CommonFormLibrary.CommonDialog;
using CommonFormLibrary.CommonPanel;
using System.IO;
using System.Diagnostics;

namespace DataEditForm.Skill
{
    public partial class SkillMainPanel : CommonFormLibrary.ListBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SkillMainPanel()
        {
            InitializeComponent();

            // データバインド
            SkillTypeEntity.mt_skill_categoryDataTable SkillCategoryTable = (SkillTypeEntity.mt_skill_categoryDataTable)LibSkillType.Entity.mt_skill_category.Copy();
            SkillCategoryTable.Addmt_skill_categoryRow(0, "なし", 0);

            SkillCategoryTable.DefaultView.Sort = LibSkillType.Entity.mt_skill_category.category_idColumn.ColumnName;

            this.comboBoxArtsCategory.DataSource = SkillCategoryTable.DefaultView;
            this.comboBoxArtsCategory.DisplayMember = LibSkillType.Entity.mt_skill_category.category_nameColumn.ColumnName;
            this.comboBoxArtsCategory.ValueMember = LibSkillType.Entity.mt_skill_category.category_idColumn.ColumnName;

            MonsterDataEntity.mt_monster_categoryDataTable MonsterCategoryTable = (MonsterDataEntity.mt_monster_categoryDataTable)LibMonsterData.Entity.mt_monster_category.Copy();
            MonsterCategoryTable.Addmt_monster_categoryRow(0, "なし");

            MonsterCategoryTable.DefaultView.Sort = LibMonsterData.Entity.mt_monster_category.category_idColumn.ColumnName;

            this.comboBoxOnTarget.DataSource = MonsterCategoryTable.DefaultView;
            this.comboBoxOnTarget.DisplayMember = LibMonsterData.Entity.mt_monster_category.category_nameColumn.ColumnName;
            this.comboBoxOnTarget.ValueMember = LibMonsterData.Entity.mt_monster_category.category_idColumn.ColumnName;

            this.textBoxNo.Text = "0";
        }

        private DataView _skillView;
        private EffectListEntity.effect_listDataTable _effectTable;

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
            CommonSkillEntity entity = LibSkill.Entity;
            entity.RejectChanges();
        }

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            CommonSkillEntity entity = LibSkill.Entity;
            _skillView = new DataView(entity.skill_list);
            _skillView.Sort = entity.skill_list.sk_idColumn.ColumnName;

            // リスト表示
            this.dataGridViewList.AutoGenerateColumns = false;
            this.dataGridViewList.DataSource = _skillView;
            this.columnNo.DataPropertyName = entity.skill_list.sk_idColumn.ColumnName;
            this.columnName.DataPropertyName = entity.skill_list.sk_nameColumn.ColumnName;
        }

        /// <summary>
        /// 詳細表示
        /// </summary>
        /// <param name="SkillID">表示対象ID</param>
        /// <param name="IsCreated">合成品か</param>
        private void PrivateView(int SkillID)
        {
            CommonSkillEntity entity = LibSkill.Entity;

            if (SkillID == 0)
            {
                // 新規ID発行
                DataView skillViewNumber = new DataView(entity.skill_list);

                SetNewNumberDialog dialog = new SetNewNumberDialog();
                dialog.SetNewNumber(LibInteger.GetNewUnderNum(skillViewNumber, entity.skill_list.sk_idColumn.ColumnName));
                dialog.ValidatingNumber += new EventHandler(Validate_Number);

                switch (dialog.ShowDialog(this))
                {
                    case DialogResult.OK:
                        SkillID = dialog.NewID;
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            if (SkillID <= 0) { return; }

            // 基本情報の表示

            CommonSkillEntity.skill_listRow baseRow = entity.skill_list.FindBysk_id(SkillID);

            this.SuspendLayout();

            this.textBoxNo.Text = SkillID.ToString();

            if (baseRow == null)
            {
                // 新規に行追加
                baseRow = entity.skill_list.Newskill_listRow();

                #region スキル新規設定
                baseRow.sk_id = SkillID;
                baseRow.sk_name = "";
                baseRow.sk_type = 0;
                baseRow.sk_atype = 0;
                baseRow.sk_mp = 0;
                baseRow.sk_tp = 0;
                baseRow.sk_range = 0;
                baseRow.sk_attack = 0;
                baseRow.sk_power = 0;
                baseRow.sk_damage_rate = 0;
                baseRow.sk_plus_score = 1;
                baseRow.sk_round = 1;
                baseRow.sk_hit = 0;
                baseRow.sk_critical = 0;
                baseRow.sk_critical_type = 0;
                baseRow.sk_hate = 1;
                baseRow.sk_vhate = 0;
                baseRow.sk_dhate = 0;
                baseRow.sk_charge = 0;
                baseRow.sk_antiair = false;
                baseRow.sk_damage_type = 0;
                baseRow.sk_arts_category = 0;
                baseRow.sk_target_restrict = 0;
                baseRow.sk_use_limit = 0;
                baseRow.sk_fire = 0;
                baseRow.sk_freeze = 0;
                baseRow.sk_air = 0;
                baseRow.sk_earth = 0;
                baseRow.sk_water = 0;
                baseRow.sk_thunder = 0;
                baseRow.sk_holy = 0;
                baseRow.sk_dark = 0;
                baseRow.sk_slash = 0;
                baseRow.sk_pierce = 0;
                baseRow.sk_strike = 0;
                baseRow.sk_break = 0;
                baseRow.sk_target_party = 0;
                baseRow.sk_target_area = 0;
                baseRow.sk_effect = "0,0,0,0,0,0";
                baseRow.sk_comment = "";
                #endregion

                entity.skill_list.Addskill_listRow(baseRow);
            }

            this.textBoxName.Text = baseRow.sk_name;
            this.comboBoxSkillType.SelectedIndex = baseRow.sk_type;
            this.comboBoxAttackType.SelectedIndex = baseRow.sk_atype;

            this.numericUpDownMPCost.Value = baseRow.sk_mp;
            this.numericUpDownTPCost.Value = baseRow.sk_tp;
            this.comboBoxRange.SelectedIndex = baseRow.sk_range;

            this.numericUpDownAttack.Value = baseRow.sk_attack;
            this.numericUpDownAtkPow.Value = baseRow.sk_power;
            this.numericUpDownDamageRate.Value = baseRow.sk_damage_rate;
            this.numericUpDownPlusScore.Value = baseRow.sk_plus_score;
            this.numericUpDownAtkCount.Value = baseRow.sk_round;

            this.numericUpDownHitRate.Value = baseRow.sk_hit;
            this.numericUpDownCritical.Value = baseRow.sk_critical;
            this.comboBoxCriticalType.SelectedIndex = baseRow.sk_critical_type;
            this.numericUpDownHate.Value = baseRow.sk_hate;
            this.numericUpDownVHate.Value = baseRow.sk_vhate;
            this.numericUpDownDHate.Value = baseRow.sk_dhate;

            this.numericUpDownCharge.Value = baseRow.sk_charge;
            this.comboBoxAntiAir.SelectedIndex = baseRow.sk_antiair ? 1 : 0;
            this.comboBoxDamageType.SelectedIndex = baseRow.sk_damage_type;
            this.comboBoxArtsCategory.SelectedValue = baseRow.sk_arts_category;

            this.comboBoxOnTarget.SelectedValue = baseRow.sk_target_restrict;
            this.comboBoxUseTerm.SelectedIndex = baseRow.sk_use_limit;

            this.numericUpDownFire.Value = baseRow.sk_fire;
            this.numericUpDownFreeze.Value = baseRow.sk_freeze;
            this.numericUpDownAir.Value = baseRow.sk_air;
            this.numericUpDownEarth.Value = baseRow.sk_earth;
            this.numericUpDownWater.Value = baseRow.sk_water;
            this.numericUpDownThunder.Value = baseRow.sk_thunder;
            this.numericUpDownHoly.Value = baseRow.sk_holy;
            this.numericUpDownDark.Value = baseRow.sk_dark;
            this.numericUpDownSlash.Value = baseRow.sk_slash;
            this.numericUpDownPierce.Value = baseRow.sk_pierce;
            this.numericUpDownStrike.Value = baseRow.sk_strike;
            this.numericUpDownBreak.Value = baseRow.sk_break;

            this.comboBoxTargetParty.SelectedIndex = baseRow.sk_target_party;
            this.comboBoxTargetArea.SelectedIndex = baseRow.sk_target_area;

            // エフェクトリスト
            _effectTable = new EffectListEntity.effect_listDataTable();
            LibEffect.Split(baseRow.sk_effect, ref _effectTable, true, 0);

            this.effectSettingPanel.Rows.Clear();

            foreach (EffectListEntity.effect_listRow EffectRow in _effectTable)
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

            this.textBoxSkillComment.Text = baseRow.sk_comment.ToString().Replace("<br />", "\r\n");

            string file = LibCommonLibrarySettings.EventScriptDir + "skillevent_" + SkillID.ToString("000000") + ".py";
            if (File.Exists(file))
            {
                this.labelUseEvent.Text = "○";
            }
            else
            {
                this.labelUseEvent.Text = "×";
            }

            CheckDouble();

            this.ResumeLayout();
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
            // コピー、削除を有効に(合成品は無理)
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
        /// データ削除
        /// </summary>
        /// <param name="SkillID">対象ID</param>
        /// <param name="rowIndex">行数</param>
        private void DeleteData(int SkillID, int rowIndex)
        {
            CommonSkillEntity entity = LibSkill.Entity;

            CommonSkillEntity.skill_listRow skillRow = entity.skill_list.FindBysk_id(SkillID);

            if (skillRow == null) { return; }

            skillRow.Delete();

            if (this.dataGridViewList.Rows.Count > 0 && this.dataGridViewList[0, 0].Value != null)
            {
                PrivateView((int)this.dataGridViewList[0, 0].Value);
            }
            else
            {
                PrivateView(0);
            }
        }

        /// <summary>
        /// 番号重複管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Validate_Number(object sender, EventArgs e)
        {
            CommonSkillEntity entity = LibSkill.Entity;
            SetNewNumberDialog dialog = (SetNewNumberDialog)sender;
            CommonSkillEntity.skill_listRow skillRow = entity.skill_list.FindBysk_id(dialog.NewID);

            if (skillRow != null)
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
        /// <param name="SkillID">対象ID</param>
        private void CopyData(int SkillID)
        {
            CommonSkillEntity entity = LibSkill.Entity;

            CommonSkillEntity.skill_listRow skillRow = entity.skill_list.FindBysk_id(SkillID);

            if (skillRow == null) { return; }

            // 新規ID発行
            DataView skillViewNumber = new DataView(entity.skill_list);

            SetNewNumberDialog dialog = new SetNewNumberDialog();
            dialog.SetNewNumber(LibInteger.GetNewUnderNum(skillViewNumber, entity.skill_list.sk_idColumn.ColumnName));
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
            CommonSkillEntity.skill_listRow newSkillRow = entity.skill_list.Newskill_listRow();
            newSkillRow.ItemArray = skillRow.ItemArray;
            newSkillRow.sk_id = NewID;

            entity.skill_list.Addskill_listRow(newSkillRow);
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            CommonSkillEntity entity = LibSkill.Entity;

            return entity.GetChanges() != null;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            UpdateEntity();
            LibSkill.Update();
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
            SkillFilterDialog dialog = new SkillFilterDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _skillView.RowFilter = dialog.FilterString;
            }
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            CommonSkillEntity entity = LibSkill.Entity;

            CommonSkillEntity.skill_listRow row = entity.skill_list.FindBysk_id(int.Parse(this.textBoxNo.Text));

            bool isNew = false;

            if (row == null)
            {
                row = entity.skill_list.Newskill_listRow();
                isNew = true;

                row.sk_id = int.Parse(this.textBoxNo.Text);
            }

            if (isNew || row.sk_name != this.textBoxName.Text) { row.sk_name = this.textBoxName.Text; }
            if (isNew || row.sk_type != this.comboBoxSkillType.SelectedIndex) { row.sk_type = this.comboBoxSkillType.SelectedIndex; }
            if (isNew || row.sk_atype != this.comboBoxAttackType.SelectedIndex) { row.sk_atype = this.comboBoxAttackType.SelectedIndex; }

            if (isNew || row.sk_mp != (int)this.numericUpDownMPCost.Value) { row.sk_mp = (int)this.numericUpDownMPCost.Value; }
            if (isNew || row.sk_tp != (int)this.numericUpDownTPCost.Value) { row.sk_tp = (int)this.numericUpDownTPCost.Value; }
            if (isNew || row.sk_range != this.comboBoxRange.SelectedIndex) { row.sk_range = this.comboBoxRange.SelectedIndex; }

            if (isNew || row.sk_attack != (int)this.numericUpDownAttack.Value) { row.sk_attack = (int)this.numericUpDownAttack.Value; }
            if (isNew || row.sk_power != this.numericUpDownAtkPow.Value) { row.sk_power = this.numericUpDownAtkPow.Value; }
            if (isNew || row.sk_damage_rate != this.numericUpDownDamageRate.Value) { row.sk_damage_rate = this.numericUpDownDamageRate.Value; }
            if (isNew || row.sk_plus_score != (int)this.numericUpDownPlusScore.Value) { row.sk_plus_score = (int)this.numericUpDownPlusScore.Value; }
            if (isNew || row.sk_round != (int)this.numericUpDownAtkCount.Value) { row.sk_round = (int)this.numericUpDownAtkCount.Value; }

            if (isNew || row.sk_hit != (int)this.numericUpDownHitRate.Value) { row.sk_hit = (int)this.numericUpDownHitRate.Value; }
            if (isNew || row.sk_critical != (int)this.numericUpDownCritical.Value) { row.sk_critical = (int)this.numericUpDownCritical.Value; }
            if (isNew || row.sk_critical_type != this.comboBoxCriticalType.SelectedIndex) { row.sk_critical_type = this.comboBoxCriticalType.SelectedIndex; }
            if (isNew || row.sk_hate != (int)this.numericUpDownHate.Value) { row.sk_hate = (int)this.numericUpDownHate.Value; }
            if (isNew || row.sk_vhate != (int)this.numericUpDownVHate.Value) { row.sk_vhate = (int)this.numericUpDownVHate.Value; }
            if (isNew || row.sk_dhate != (int)this.numericUpDownDHate.Value) { row.sk_dhate = (int)this.numericUpDownDHate.Value; }

            if (isNew || row.sk_charge != (int)this.numericUpDownCharge.Value) { row.sk_charge = (int)this.numericUpDownCharge.Value; }
            if (isNew || row.sk_antiair != (this.comboBoxAntiAir.SelectedIndex == 1 ? true : false)) { row.sk_antiair = this.comboBoxAntiAir.SelectedIndex == 1; }
            if (isNew || row.sk_damage_type != this.comboBoxDamageType.SelectedIndex) { row.sk_damage_type = this.comboBoxDamageType.SelectedIndex; }
            if (isNew || row.sk_arts_category != (int)this.comboBoxArtsCategory.SelectedValue) { row.sk_arts_category = (int)this.comboBoxArtsCategory.SelectedValue; }

            if (isNew || row.sk_target_restrict != (int)this.comboBoxOnTarget.SelectedValue) { row.sk_target_restrict = (int)this.comboBoxOnTarget.SelectedValue; }
            if (isNew || row.sk_use_limit != this.comboBoxUseTerm.SelectedIndex) { row.sk_use_limit = this.comboBoxUseTerm.SelectedIndex; }

            if (isNew || row.sk_fire != (int)this.numericUpDownFire.Value) { row.sk_fire = (int)this.numericUpDownFire.Value; }
            if (isNew || row.sk_freeze != (int)this.numericUpDownFreeze.Value) { row.sk_freeze = (int)this.numericUpDownFreeze.Value; }
            if (isNew || row.sk_air != (int)this.numericUpDownAir.Value) { row.sk_air = (int)this.numericUpDownAir.Value; }
            if (isNew || row.sk_earth != (int)this.numericUpDownEarth.Value) { row.sk_earth = (int)this.numericUpDownEarth.Value; }
            if (isNew || row.sk_water != (int)this.numericUpDownWater.Value) { row.sk_water = (int)this.numericUpDownWater.Value; }
            if (isNew || row.sk_thunder != (int)this.numericUpDownThunder.Value) { row.sk_thunder = (int)this.numericUpDownThunder.Value; }
            if (isNew || row.sk_holy != (int)this.numericUpDownHoly.Value) { row.sk_holy = (int)this.numericUpDownHoly.Value; }
            if (isNew || row.sk_dark != (int)this.numericUpDownDark.Value) { row.sk_dark = (int)this.numericUpDownDark.Value; }
            if (isNew || row.sk_slash != (int)this.numericUpDownSlash.Value) { row.sk_slash = (int)this.numericUpDownSlash.Value; }
            if (isNew || row.sk_pierce != (int)this.numericUpDownPierce.Value) { row.sk_pierce = (int)this.numericUpDownPierce.Value; }
            if (isNew || row.sk_strike != (int)this.numericUpDownStrike.Value) { row.sk_strike = (int)this.numericUpDownStrike.Value; }
            if (isNew || row.sk_break != (int)this.numericUpDownBreak.Value) { row.sk_break = (int)this.numericUpDownBreak.Value; }

            if (isNew || row.sk_target_party != this.comboBoxTargetParty.SelectedIndex) { row.sk_target_party = this.comboBoxTargetParty.SelectedIndex; }
            if (isNew || row.sk_target_area != this.comboBoxTargetArea.SelectedIndex) { row.sk_target_area = this.comboBoxTargetArea.SelectedIndex; }

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

            if (isNew || row.sk_effect != effectString) { row.sk_effect = effectString; }

            string inComment = this.textBoxSkillComment.Text.Replace("\r\n", "<br />");

            if (isNew || row.sk_comment != inComment) { row.sk_comment = inComment; }

            if (isNew)
            {
                entity.skill_list.Addskill_listRow(row);
            }
        }

        /// <summary>
        /// スキル種別変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSkillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxSkillType.SelectedIndex == 0 || this.comboBoxSkillType.SelectedIndex == 4)
            {
                this.comboBoxAttackType.Enabled = true;
            }
            else
            {
                this.comboBoxAttackType.Enabled = false;
            }
        }

        /// <summary>
        /// 使用イベント設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEvent_Click(object sender, EventArgs e)
        {
            string file = LibCommonLibrarySettings.EventScriptDir + "skillevent_" + int.Parse(this.textBoxNo.Text).ToString("000000") + ".py";

            if (!File.Exists(file))
            {
                File.WriteAllText(file, "", Encoding.UTF8);
            }

            string emeditor = LibCommonLibrarySettings.EditorPath;

            Process extProcess = new Process();
            extProcess.StartInfo.FileName = emeditor;
            extProcess.StartInfo.Arguments = file;
            extProcess.Start();

            this.labelUseEvent.Text = "○";
        }

        /// <summary>
        /// 重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckDouble();
        }

        private void CheckDouble()
        {
            CommonSkillEntity entity = LibSkill.Entity;
            DataView nameView = new DataView(entity.skill_list);
            nameView.RowFilter = entity.skill_list.sk_idColumn.ColumnName + "<>" + this.textBoxNo.Text + " and " + entity.skill_list.sk_nameColumn.ColumnName + "=" + LibSql.EscapeString(this.textBoxName.Text);
            if (nameView.Count > 0)
            {
                this.labelSkillNameCheck.Visible = true;
            }
            else
            {
                this.labelSkillNameCheck.Visible = false;
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            CheckDouble();
        }
    }
}
