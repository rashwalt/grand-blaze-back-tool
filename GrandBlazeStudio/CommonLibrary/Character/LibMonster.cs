using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary.Character
{
    public partial class LibMonster : LibUnitBase
    {
        /// <summary>
        /// キャラクタ読み込みコンストラクタ(敵)
        /// </summary>
        /// <param name="Party">所属</param>
        /// <param name="Entry">エントリーNo</param>
        /// <param name="BaseLevel">味方平均レベル</param>
        public LibMonster(int Party, int Entry, int BaseLevel)
        {
            PartyBelong = Party;

            // データ読み込み＆セット(敵、NPC専用)
            ReadDataEnemy(Entry, BaseLevel, false);

            SetEquipData();

            HPNow = HPMax;
            MPNow = MPMax;
        }

        /// <summary>
        /// キャラクタ読み込みコンストラクタ(レベル:固定化)
        /// </summary>
        /// <param name="Party">所属</param>
        /// <param name="Entry">エントリーNo</param>
        /// <param name="BaseLevel">味方平均レベル</param>
        /// <param name="IsLevelFix">レベル固定か</param>
        public LibMonster(int Party, int Entry, int BaseLevel, bool IsLevelFix)
        {
            PartyBelong = Party;

            // データ読み込み＆セット(敵、NPC専用)
            ReadDataEnemy(Entry, BaseLevel, IsLevelFix);

            SetEquipData();

            HPNow = HPMax;
            MPNow = MPMax;
        }

        /// <summary>
        /// 装備数値設定
        /// </summary>
        private void SetEquipData()
        {
            // エフェクトリセット
            EffectList.DefaultView.RowFilter = "effect_div=" + Status.EffectDiv.Deffence;

            foreach (DataRowView EffectRow in EffectList.DefaultView)
            {
                EffectRow.Row.Delete();
            }
            EffectList.AcceptChanges();

            // メインウェポン特性
            MonsterDataEntity.mt_monster_elementRow MainWeaponElements = ElementTable.FindBymonster_idatkordfe(EntryNo, Status.ElementType.MainAttack);

            MainWeapon.ItemSubType = 7;
            MainWeapon.ItemType = 3;

            MainWeapon.AttackDamageType = Status.AttackType.Combat;
            MainWeapon.ChargeTime = MainWeaponElements.charge;
            LibEffect.Split(MainWeaponElements.status_list, ref MainWeapon.Effect);
            MainWeapon.Elemental.Fire = MainWeaponElements.fire;
            MainWeapon.Elemental.Freeze = MainWeaponElements.freeze;
            MainWeapon.Elemental.Air = MainWeaponElements.air;
            MainWeapon.Elemental.Earth = MainWeaponElements.earth;
            MainWeapon.Elemental.Water = MainWeaponElements.water;
            MainWeapon.Elemental.Thunder = MainWeaponElements.thunder;
            MainWeapon.Elemental.Holy = MainWeaponElements.holy;
            MainWeapon.Elemental.Dark = MainWeaponElements.dark;
            MainWeapon.Elemental.Slash = MainWeaponElements.slash;
            MainWeapon.Elemental.Pierce = MainWeaponElements.pierce;
            MainWeapon.Elemental.Strike = MainWeaponElements.strike;
            MainWeapon.Elemental.Break = MainWeaponElements._break;
            MainWeapon.Avoid = MainWeaponElements.avoid_physical;
            MainWeapon.Range = MainWeaponElements.range;
            MainWeapon.TargetArea = Status.TargetArea.Only;

            // サブウェポン特性
            MonsterDataEntity.mt_monster_elementRow SubWeaponElements = ElementTable.FindBymonster_idatkordfe(EntryNo, Status.ElementType.SubAttack);

            if (_sub_atk > 0)
            {
                SubWeapon.ItemSubType = 7;
                SubWeapon.ItemType = 3;

                SubWeapon.AttackDamageType = Status.AttackType.Combat;
                SubWeapon.ChargeTime = SubWeaponElements.charge;
                LibEffect.Split(SubWeaponElements.status_list, ref SubWeapon.Effect);
                SubWeapon.Elemental.Fire = SubWeaponElements.fire;
                SubWeapon.Elemental.Freeze = SubWeaponElements.freeze;
                SubWeapon.Elemental.Air = SubWeaponElements.air;
                SubWeapon.Elemental.Earth = SubWeaponElements.earth;
                SubWeapon.Elemental.Water = SubWeaponElements.water;
                SubWeapon.Elemental.Thunder = SubWeaponElements.thunder;
                SubWeapon.Elemental.Holy = SubWeaponElements.holy;
                SubWeapon.Elemental.Dark = SubWeaponElements.dark;
                SubWeapon.Elemental.Slash = SubWeaponElements.slash;
                SubWeapon.Elemental.Pierce = SubWeaponElements.pierce;
                SubWeapon.Elemental.Strike = SubWeaponElements.strike;
                SubWeapon.Elemental.Break = SubWeaponElements._break;
                SubWeapon.Avoid = 0;
                SubWeapon.Range = SubWeaponElements.range;
                SubWeapon.TargetArea = Status.TargetArea.Only;
            }
            else
            {
                ShieldAvoidPhysical = SubWeaponElements.avoid_physical;
                ShieldAvoidSorcery = SubWeaponElements.avoid_magical;
            }

            // 防御特性
            MonsterDataEntity.mt_monster_elementRow DefenceElements = ElementTable.FindBymonster_idatkordfe(EntryNo, Status.ElementType.Deffence);

            DefenceElemental.Fire = DefenceElements.fire;
            DefenceElemental.Freeze = DefenceElements.freeze;
            DefenceElemental.Air = DefenceElements.air;
            DefenceElemental.Earth = DefenceElements.earth;
            DefenceElemental.Water = DefenceElements.water;
            DefenceElemental.Thunder = DefenceElements.thunder;
            DefenceElemental.Holy = DefenceElements.holy;
            DefenceElemental.Dark = DefenceElements.dark;
            DefenceElemental.Slash = DefenceElements.slash;
            DefenceElemental.Pierce = DefenceElements.pierce;
            DefenceElemental.Strike = DefenceElements.strike;
            DefenceElemental.Break = DefenceElements._break;

            LibEffect.SplitAdd(DefenceElements.status_list, ref EffectList, Status.EffectDiv.Deffence);
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        /// <param name="Entry">モンスターID</param>
        /// <param name="BaseLevel">平均レベル</param>
        /// <param name="IsLevelFix">レベル固定か</param>
        private void ReadDataEnemy(int Entry, int BaseLevel, bool IsLevelFix)
        {
            StringBuilder Sql = new StringBuilder();

            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {

                #region 基本情報読み込み
                MonsterDataEntity.mt_monster_listDataTable CharacterBase = new MonsterDataEntity.mt_monster_listDataTable();

                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("monster_id, ");
                Sql.AppendLine("monster_name, ");
                Sql.AppendLine("category_id, ");
                Sql.AppendLine("formation, ");
                Sql.AppendLine("max_multi_act, ");
                Sql.AppendLine("multi_act_prob, ");
                Sql.AppendLine("target, ");
                Sql.AppendLine("belong_kb ");
                Sql.AppendLine("FROM [mt_monster_list]");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("monster_id = " + Entry);

                dba.Fill(Sql.ToString(), CharacterBase);
                MonsterDataEntity.mt_monster_listRow CharaBaseRow = CharacterBase[0];

                EntryNo = CharaBaseRow.monster_id;
                CharacterName = CharaBaseRow.monster_name;
                NickName = CharaBaseRow.monster_name;
                Category = CharaBaseRow.category_id;
                MultiAttackMaxCount = CharaBaseRow.max_multi_act;
                MainWeapon.Critical = CharaBaseRow.multi_act_prob;
                TargetType = CharaBaseRow.target;
                Formation = CharaBaseRow.formation;
                PartyBelongDetail = CharaBaseRow.belong_kb;

                #endregion

                #region キャラクターバトル情報読み込み

                MonsterDataEntity.mt_monster_battle_abilityDataTable CharacterBattle = new MonsterDataEntity.mt_monster_battle_abilityDataTable();

                Sql = new StringBuilder();
                Sql.AppendLine("SELECT monster_id");
                Sql.AppendLine(",[level]");
                Sql.AppendLine(",[hp]");
                Sql.AppendLine(",[mp]");
                Sql.AppendLine(",[str]");
                Sql.AppendLine(",[agi]");
                Sql.AppendLine(",[mag]");
                Sql.AppendLine(",[unq]");
                Sql.AppendLine(",[atk]");
                Sql.AppendLine(",[sub_atk]");
                Sql.AppendLine(",[dfe]");
                Sql.AppendLine(",[mgr]");
                Sql.AppendLine(",[avd]");
                Sql.AppendLine(",[exp]");
                Sql.AppendLine("FROM [mt_monster_battle_ability]");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("monster_id = " + Entry);
                if (IsLevelFix)
                {
                    Sql.AppendLine(" AND [level]=" + BaseLevel);
                }
                Sql.AppendLine("ORDER BY [level]");

                dba.Fill(Sql.ToString(), CharacterBattle);

                if (CharacterBattle.Count > 1)
                {
                    // レベル制限
                    int LevelMin = CharacterBattle[0].level;
                    int LevelMax = CharacterBattle[CharacterBattle.Count - 1].level;

                    if (LevelMin <= (BaseLevel - 3) && LevelMax >= (BaseLevel - 3))
                    {
                        LevelMin = (BaseLevel - 3);
                    }

                    if (LevelMax >= (BaseLevel + 2) && LevelMin <= (BaseLevel + 2))
                    {
                        LevelMax = (BaseLevel + 2);
                    }

                    Sql = new StringBuilder();
                    Sql.AppendLine("SELECT monster_id");
                    Sql.AppendLine(",[level]");
                    Sql.AppendLine(",[hp]");
                    Sql.AppendLine(",[mp]");
                    Sql.AppendLine(",[str]");
                    Sql.AppendLine(",[agi]");
                    Sql.AppendLine(",[mag]");
                    Sql.AppendLine(",[unq]");
                    Sql.AppendLine(",[atk]");
                    Sql.AppendLine(",[sub_atk]");
                    Sql.AppendLine(",[dfe]");
                    Sql.AppendLine(",[mgr]");
                    Sql.AppendLine(",[avd]");
                    Sql.AppendLine(",[exp]");
                    Sql.AppendLine("FROM [mt_monster_battle_ability]");
                    Sql.AppendLine("WHERE ");
                    Sql.AppendLine("monster_id = " + Entry);
                    Sql.AppendLine(" AND [level]=" + LibInteger.GetRandMax(LevelMin, LevelMax));
                    Sql.AppendLine("ORDER BY [level]");

                    dba.Fill(Sql.ToString(), CharacterBattle);
                }

                MonsterDataEntity.mt_monster_battle_abilityRow CharaBattleRow = CharacterBattle[LibInteger.GetRand(CharacterBattle.Count)];

                _level = CharaBattleRow.level;
                _max_hp = CharaBattleRow.hp;
                _max_mp = CharaBattleRow.mp;
                _str = CharaBattleRow.str;
                _agi = CharaBattleRow.agi;
                _mag = CharaBattleRow.mag;
                _unq = CharaBattleRow.unq;
                _atk = CharaBattleRow.atk;
                _sub_atk = CharaBattleRow.sub_atk;
                _armorDFE = CharaBattleRow.dfe;
                _armorMGR = CharaBattleRow.mgr;
                _avd = CharaBattleRow.avd;

                Exp = CharaBattleRow.exp;

                #endregion

                #region キャラクター行動情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("action_no, ");
                Sql.AppendLine("timing1, ");
                Sql.AppendLine("timing2, ");
                Sql.AppendLine("timing3, ");
                Sql.AppendLine("CASE action_target ");
                Sql.AppendLine(" WHEN 0 THEN " + TargetType + " ");
                Sql.AppendLine(" ELSE action_target ");
                Sql.AppendLine("END AS action_target, ");
                Sql.AppendLine("action, ");
                Sql.AppendLine("probability, ");
                Sql.AppendLine("max_count, ");
                Sql.AppendLine("perks_id ");
                Sql.AppendLine("FROM mt_monster_action");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("monster_id = " + Entry);
                Sql.AppendLine("ORDER BY action_no ");

                dba.Fill(Sql.ToString(), ActionList);
                #endregion

                #region キャラクター所持アイテム読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("0 as box_type, ");
                Sql.AppendLine("0 as have_no, ");
                Sql.AppendLine("drop_type, ");
                Sql.AppendLine("get_synx, ");
                Sql.AppendLine("it_num, ");
                Sql.AppendLine("it_box_count, ");
                Sql.AppendLine("0 as it_box_baz_count, ");
                Sql.AppendLine("0 as created, ");
                Sql.AppendLine("0 as equip_spot, ");
                Sql.AppendLine("0 as bind, ");
                Sql.AppendLine("0 as [new] ");
                Sql.AppendLine("FROM mt_monster_have_item");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("monster_id = " + Entry);

                dba.Fill(Sql.ToString(), HaveItem);
                #endregion

                #region キャラクターセリフ情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("serif_no, ");
                Sql.AppendLine("situation, ");
                Sql.AppendLine("perks_id, ");
                Sql.AppendLine("serif_text");
                Sql.AppendLine("FROM mt_monster_serif");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("monster_id = " + Entry);

                dba.Fill(Sql.ToString(), SerifList);
                #endregion

                #region キャラクターエレメント情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("monster_id,");
                Sql.AppendLine("atkordfe,");
                Sql.AppendLine("fire,");
                Sql.AppendLine("freeze,");
                Sql.AppendLine("air,");
                Sql.AppendLine("earth,");
                Sql.AppendLine("water,");
                Sql.AppendLine("thunder,");
                Sql.AppendLine("holy,");
                Sql.AppendLine("dark,");
                Sql.AppendLine("slash,");
                Sql.AppendLine("pierce,");
                Sql.AppendLine("strike,");
                Sql.AppendLine("[break],");
                Sql.AppendLine("status_list,");
                Sql.AppendLine("charge,");
                Sql.AppendLine("range,");
                Sql.AppendLine("avoid_physical,");
                Sql.AppendLine("avoid_magical");
                Sql.AppendLine("FROM mt_monster_element");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("monster_id = " + Entry);

                dba.Fill(Sql.ToString(), ElementTable);
                #endregion
            }
        }
    }
}
