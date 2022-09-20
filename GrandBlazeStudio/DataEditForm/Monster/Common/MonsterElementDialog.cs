using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;

namespace DataEditForm.Monster.Common
{
    public partial class MonsterElementDialog : CommonFormLibrary.BaseDialog
    {
        /// <summary>
        /// コンストラクタ（デザイン用）
        /// </summary>
        public MonsterElementDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="MonsterID">モンスターID</param>
        /// <param name="ElementTable">反映先テーブル</param>
        public MonsterElementDialog(int MonsterID, MonsterDataEntity.mt_monster_elementDataTable ElementTable)
        {
            InitializeComponent();

            _monsterID = MonsterID;

            _table = ElementTable;
        }

        private int _monsterID = 0;
        private MonsterDataEntity.mt_monster_elementDataTable _table;
        private EffectListEntity.effect_listDataTable _effectTable1;
        private EffectListEntity.effect_listDataTable _effectTable2;
        private EffectListEntity.effect_listDataTable _effectTable4;

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
        private void MonsterElementDialog_Load(object sender, EventArgs e)
        {
            #region 0.<メインウェポン>
            MonsterDataEntity.mt_monster_elementRow MainElementRow = _table.FindBymonster_idatkordfe(_monsterID, Status.ElementType.MainAttack);

            if (MainElementRow != null)
            {
                this.numericUpDownFire.Value = MainElementRow.fire;
                this.numericUpDownFreeze.Value = MainElementRow.freeze;
                this.numericUpDownAir.Value = MainElementRow.air;
                this.numericUpDownEarth.Value = MainElementRow.earth;
                this.numericUpDownWater.Value = MainElementRow.water;
                this.numericUpDownThunder.Value = MainElementRow.thunder;
                this.numericUpDownHoly.Value = MainElementRow.holy;
                this.numericUpDownDark.Value = MainElementRow.dark;
                this.numericUpDownSlash.Value = MainElementRow.slash;
                this.numericUpDownPierce.Value = MainElementRow.pierce;
                this.numericUpDownStrike.Value = MainElementRow.strike;
                this.numericUpDownBreak.Value = MainElementRow._break;

                this.numericUpDownCharge.Value = MainElementRow.charge;
                this.comboBoxRange.SelectedIndex = MainElementRow.range;
                this.numericUpDownWeaponAvoid.Value = MainElementRow.avoid_physical;

                // エフェクトリスト
                _effectTable1 = new EffectListEntity.effect_listDataTable();
                LibEffect.Split(MainElementRow.status_list, ref _effectTable1, true, 0);

                this.effectSettingPanel.Rows.Clear();

                foreach (EffectListEntity.effect_listRow EffectRow in _effectTable1)
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
            else
            {
                this.numericUpDownFire.Value = 0;
                this.numericUpDownFreeze.Value = 0;
                this.numericUpDownAir.Value = 0;
                this.numericUpDownEarth.Value = 0;
                this.numericUpDownWater.Value = 0;
                this.numericUpDownThunder.Value = 0;
                this.numericUpDownHoly.Value = 0;
                this.numericUpDownDark.Value = 0;
                this.numericUpDownSlash.Value = 0;
                this.numericUpDownPierce.Value = 0;
                this.numericUpDownStrike.Value = 0;
                this.numericUpDownBreak.Value = 0;

                this.numericUpDownCharge.Value = 35;
                this.comboBoxRange.SelectedIndex = 0;
                this.numericUpDownWeaponAvoid.Value = 0;

                this.effectSettingPanel.Rows.Clear();
            }
            #endregion

            #region 1.<サブウェポン>
            MonsterDataEntity.mt_monster_elementRow ShotElementRow = _table.FindBymonster_idatkordfe(_monsterID, Status.ElementType.SubAttack);

            if (ShotElementRow != null)
            {
                this.numericUpDownFire2.Value = ShotElementRow.fire;
                this.numericUpDownFreeze2.Value = ShotElementRow.freeze;
                this.numericUpDownAir2.Value = ShotElementRow.air;
                this.numericUpDownEarth2.Value = ShotElementRow.earth;
                this.numericUpDownWater2.Value = ShotElementRow.water;
                this.numericUpDownThunder2.Value = ShotElementRow.thunder;
                this.numericUpDownHoly2.Value = ShotElementRow.holy;
                this.numericUpDownDark2.Value = ShotElementRow.dark;
                this.numericUpDownSlash2.Value = ShotElementRow.slash;
                this.numericUpDownPierce2.Value = ShotElementRow.pierce;
                this.numericUpDownStrike2.Value = ShotElementRow.strike;
                this.numericUpDownBreak2.Value = ShotElementRow._break;

                this.numericUpDownCharge2.Value = ShotElementRow.charge;
                this.comboBoxRange2.SelectedIndex = ShotElementRow.range;

                this.numericUpDownAvoidPhysical.Value = ShotElementRow.avoid_physical;
                this.numericUpDownAvoidMagical.Value = ShotElementRow.avoid_magical;

                // エフェクトリスト
                _effectTable2 = new EffectListEntity.effect_listDataTable();
                LibEffect.Split(ShotElementRow.status_list, ref _effectTable2, true, 0);

                this.effectSettingPanel2.Rows.Clear();

                foreach (EffectListEntity.effect_listRow EffectRow in _effectTable2)
                {
                    int rowIndex = this.effectSettingPanel2.Rows.Add(
                        EffectRow.effect_id.ToString("0000") + ": " + EffectRow.name,
                        EffectRow.rank,
                        EffectRow.sub_rank,
                        EffectRow.prob,
                        EffectRow.endlimit,
                        EffectRow.hide_fg
                        );

                    DataGridViewRow row = this.effectSettingPanel2.Rows[rowIndex];
                    row.Tag = EffectRow;
                }
            }
            else
            {
                this.numericUpDownFire2.Value = 0;
                this.numericUpDownFreeze2.Value = 0;
                this.numericUpDownAir2.Value = 0;
                this.numericUpDownEarth2.Value = 0;
                this.numericUpDownWater2.Value = 0;
                this.numericUpDownThunder2.Value = 0;
                this.numericUpDownHoly2.Value = 0;
                this.numericUpDownDark2.Value = 0;
                this.numericUpDownSlash2.Value = 0;
                this.numericUpDownPierce2.Value = 0;
                this.numericUpDownStrike2.Value = 0;
                this.numericUpDownBreak2.Value = 0;

                this.numericUpDownCharge2.Value = 35;
                this.comboBoxRange2.SelectedIndex = 0;

                this.numericUpDownAvoidPhysical.Value = 0;
                this.numericUpDownAvoidMagical.Value =0;

                this.effectSettingPanel2.Rows.Clear();
            }
            #endregion

            #region 3.<防御>
            MonsterDataEntity.mt_monster_elementRow DeffenceElementRow = _table.FindBymonster_idatkordfe(_monsterID, Status.ElementType.Deffence);

            if (DeffenceElementRow != null)
            {
                this.numericUpDownFire4.Value = DeffenceElementRow.fire;
                this.numericUpDownFreeze4.Value = DeffenceElementRow.freeze;
                this.numericUpDownAir4.Value = DeffenceElementRow.air;
                this.numericUpDownEarth4.Value = DeffenceElementRow.earth;
                this.numericUpDownWater4.Value = DeffenceElementRow.water;
                this.numericUpDownThunder4.Value = DeffenceElementRow.thunder;
                this.numericUpDownHoly4.Value = DeffenceElementRow.holy;
                this.numericUpDownDark4.Value = DeffenceElementRow.dark;
                this.numericUpDownSlash4.Value = DeffenceElementRow.slash;
                this.numericUpDownPierce4.Value = DeffenceElementRow.pierce;
                this.numericUpDownStrike4.Value = DeffenceElementRow.strike;
                this.numericUpDownBreak4.Value = DeffenceElementRow._break;

                // エフェクトリスト
                _effectTable4 = new EffectListEntity.effect_listDataTable();
                LibEffect.Split(DeffenceElementRow.status_list, ref _effectTable4, true, 0);

                this.effectSettingPanel4.Rows.Clear();

                foreach (EffectListEntity.effect_listRow EffectRow in _effectTable4)
                {
                    int rowIndex = this.effectSettingPanel4.Rows.Add(
                        EffectRow.effect_id.ToString("0000") + ": " + EffectRow.name,
                        EffectRow.rank,
                        EffectRow.sub_rank,
                        EffectRow.prob,
                        EffectRow.endlimit,
                        EffectRow.hide_fg
                        );

                    DataGridViewRow row = this.effectSettingPanel4.Rows[rowIndex];
                    row.Tag = EffectRow;
                }
            }
            else
            {
                this.numericUpDownFire4.Value = 0;
                this.numericUpDownFreeze4.Value = 0;
                this.numericUpDownAir4.Value = 0;
                this.numericUpDownEarth4.Value = 0;
                this.numericUpDownWater4.Value = 0;
                this.numericUpDownThunder4.Value = 0;
                this.numericUpDownHoly4.Value = 0;
                this.numericUpDownDark4.Value = 0;
                this.numericUpDownSlash4.Value = 0;
                this.numericUpDownPierce4.Value = 0;
                this.numericUpDownStrike4.Value = 0;
                this.numericUpDownBreak4.Value = 0;

                this.effectSettingPanel4.Rows.Clear();
            }
            #endregion
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // メインウェポン
            {
                bool isNew = false;
                MonsterDataEntity.mt_monster_elementRow row = _table.FindBymonster_idatkordfe(_monsterID, Status.ElementType.MainAttack);

                if (row == null)
                {
                    row = _table.Newmt_monster_elementRow();
                    isNew = true;
                }

                if (isNew) { row.monster_id = _monsterID; }
                if (isNew) { row.atkordfe = Status.ElementType.MainAttack; }
                if (isNew || row.fire != (int)this.numericUpDownFire.Value) { row.fire = (int)this.numericUpDownFire.Value; }
                if (isNew || row.freeze != (int)this.numericUpDownFreeze.Value) { row.freeze = (int)this.numericUpDownFreeze.Value; }
                if (isNew || row.air != (int)this.numericUpDownAir.Value) { row.air = (int)this.numericUpDownAir.Value; }
                if (isNew || row.earth != (int)this.numericUpDownEarth.Value) { row.earth = (int)this.numericUpDownEarth.Value; }
                if (isNew || row.water != (int)this.numericUpDownWater.Value) { row.water = (int)this.numericUpDownWater.Value; }
                if (isNew || row.thunder != (int)this.numericUpDownThunder.Value) { row.thunder = (int)this.numericUpDownThunder.Value; }
                if (isNew || row.holy != (int)this.numericUpDownHoly.Value) { row.holy = (int)this.numericUpDownHoly.Value; }
                if (isNew || row.dark != (int)this.numericUpDownDark.Value) { row.dark = (int)this.numericUpDownDark.Value; }
                if (isNew || row.slash != (int)this.numericUpDownSlash.Value) { row.slash = (int)this.numericUpDownSlash.Value; }
                if (isNew || row.pierce != (int)this.numericUpDownPierce.Value) { row.pierce = (int)this.numericUpDownPierce.Value; }
                if (isNew || row.strike != (int)this.numericUpDownStrike.Value) { row.strike = (int)this.numericUpDownStrike.Value; }
                if (isNew || row._break != (int)this.numericUpDownBreak.Value) { row._break = (int)this.numericUpDownBreak.Value; }

                if (isNew || row.charge != (int)this.numericUpDownCharge.Value) { row.charge = (int)this.numericUpDownCharge.Value; }
                if (isNew || row.range != this.comboBoxRange.SelectedIndex) { row.range = this.comboBoxRange.SelectedIndex; }

                if (isNew || row.avoid_physical != (int)this.numericUpDownWeaponAvoid.Value) { row.avoid_physical = (int)this.numericUpDownWeaponAvoid.Value; }
                if (isNew) { row.avoid_magical = 0; }

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

                if (isNew || row.status_list != effectString) { row.status_list = effectString; }

                if (isNew) { _table.Addmt_monster_elementRow(row); }
            }

            // サブウェポン
            {
                bool isNew = false;
                MonsterDataEntity.mt_monster_elementRow row = _table.FindBymonster_idatkordfe(_monsterID, Status.ElementType.SubAttack);

                if (row == null)
                {
                    row = _table.Newmt_monster_elementRow();
                    isNew = true;
                }

                if (isNew) { row.monster_id = _monsterID; }
                if (isNew) { row.atkordfe = Status.ElementType.SubAttack; }
                if (isNew || row.fire != (int)this.numericUpDownFire2.Value) { row.fire = (int)this.numericUpDownFire2.Value; }
                if (isNew || row.freeze != (int)this.numericUpDownFreeze2.Value) { row.freeze = (int)this.numericUpDownFreeze2.Value; }
                if (isNew || row.air != (int)this.numericUpDownAir2.Value) { row.air = (int)this.numericUpDownAir2.Value; }
                if (isNew || row.earth != (int)this.numericUpDownEarth2.Value) { row.earth = (int)this.numericUpDownEarth2.Value; }
                if (isNew || row.water != (int)this.numericUpDownWater2.Value) { row.water = (int)this.numericUpDownWater2.Value; }
                if (isNew || row.thunder != (int)this.numericUpDownThunder2.Value) { row.thunder = (int)this.numericUpDownThunder2.Value; }
                if (isNew || row.holy != (int)this.numericUpDownHoly2.Value) { row.holy = (int)this.numericUpDownHoly2.Value; }
                if (isNew || row.dark != (int)this.numericUpDownDark2.Value) { row.dark = (int)this.numericUpDownDark2.Value; }
                if (isNew || row.slash != (int)this.numericUpDownSlash2.Value) { row.slash = (int)this.numericUpDownSlash2.Value; }
                if (isNew || row.pierce != (int)this.numericUpDownPierce2.Value) { row.pierce = (int)this.numericUpDownPierce2.Value; }
                if (isNew || row.strike != (int)this.numericUpDownStrike2.Value) { row.strike = (int)this.numericUpDownStrike2.Value; }
                if (isNew || row._break != (int)this.numericUpDownBreak2.Value) { row._break = (int)this.numericUpDownBreak2.Value; }

                if (isNew || row.charge != (int)this.numericUpDownCharge2.Value) { row.charge = (int)this.numericUpDownCharge2.Value; }
                if (isNew || row.range != this.comboBoxRange2.SelectedIndex) { row.range = this.comboBoxRange2.SelectedIndex; }

                if (isNew || row.avoid_physical != (int)this.numericUpDownAvoidPhysical.Value) { row.avoid_physical = (int)this.numericUpDownAvoidPhysical.Value; }
                if (isNew || row.avoid_magical != (int)this.numericUpDownAvoidMagical.Value) { row.avoid_magical = (int)this.numericUpDownAvoidMagical.Value; }

                // エフェクトリスト
                EffectListEntity.effect_listDataTable table = new EffectListEntity.effect_listDataTable();
                foreach (DataGridViewRow viewRow in this.effectSettingPanel2.Rows)
                {
                    EffectListEntity.effect_listRow EffectRow = (EffectListEntity.effect_listRow)viewRow.Tag;
                    EffectListEntity.effect_listRow newEffectRow = table.Neweffect_listRow();
                    newEffectRow.ItemArray = EffectRow.ItemArray;

                    table.Addeffect_listRow(newEffectRow);
                }

                string effectString = "";
                LibEffect.Join(ref effectString, table);

                if (isNew || row.status_list != effectString) { row.status_list = effectString; }

                if (isNew) { _table.Addmt_monster_elementRow(row); }
            }

            // 防御性
            {
                bool isNew = false;
                MonsterDataEntity.mt_monster_elementRow row = _table.FindBymonster_idatkordfe(_monsterID, Status.ElementType.Deffence);

                if (row == null)
                {
                    row = _table.Newmt_monster_elementRow();
                    isNew = true;
                }

                if (isNew) { row.monster_id = _monsterID; }
                if (isNew) { row.atkordfe = Status.ElementType.Deffence; }
                if (isNew || row.fire != (int)this.numericUpDownFire4.Value) { row.fire = (int)this.numericUpDownFire4.Value; }
                if (isNew || row.freeze != (int)this.numericUpDownFreeze4.Value) { row.freeze = (int)this.numericUpDownFreeze4.Value; }
                if (isNew || row.air != (int)this.numericUpDownAir4.Value) { row.air = (int)this.numericUpDownAir4.Value; }
                if (isNew || row.earth != (int)this.numericUpDownEarth4.Value) { row.earth = (int)this.numericUpDownEarth4.Value; }
                if (isNew || row.water != (int)this.numericUpDownWater4.Value) { row.water = (int)this.numericUpDownWater4.Value; }
                if (isNew || row.thunder != (int)this.numericUpDownThunder4.Value) { row.thunder = (int)this.numericUpDownThunder4.Value; }
                if (isNew || row.holy != (int)this.numericUpDownHoly4.Value) { row.holy = (int)this.numericUpDownHoly4.Value; }
                if (isNew || row.dark != (int)this.numericUpDownDark4.Value) { row.dark = (int)this.numericUpDownDark4.Value; }
                if (isNew || row.slash != (int)this.numericUpDownSlash4.Value) { row.slash = (int)this.numericUpDownSlash4.Value; }
                if (isNew || row.pierce != (int)this.numericUpDownPierce4.Value) { row.pierce = (int)this.numericUpDownPierce4.Value; }
                if (isNew || row.strike != (int)this.numericUpDownStrike4.Value) { row.strike = (int)this.numericUpDownStrike4.Value; }
                if (isNew || row._break != (int)this.numericUpDownBreak4.Value) { row._break = (int)this.numericUpDownBreak4.Value; }

                if (isNew) { row.charge = 0; }
                if (isNew) { row.range = 0; }

                if (isNew) { row.avoid_physical = 0; }
                if (isNew) { row.avoid_magical = 0; }

                // エフェクトリスト
                EffectListEntity.effect_listDataTable table = new EffectListEntity.effect_listDataTable();
                foreach (DataGridViewRow viewRow in this.effectSettingPanel4.Rows)
                {
                    EffectListEntity.effect_listRow EffectRow = (EffectListEntity.effect_listRow)viewRow.Tag;
                    EffectListEntity.effect_listRow newEffectRow = table.Neweffect_listRow();
                    newEffectRow.ItemArray = EffectRow.ItemArray;

                    table.Addeffect_listRow(newEffectRow);
                }

                string effectString = "";
                LibEffect.Join(ref effectString, table);

                if (isNew || row.status_list != effectString) { row.status_list = effectString; }

                if (isNew) { _table.Addmt_monster_elementRow(row); }
            }

            this.Close();
        }
    }
}

