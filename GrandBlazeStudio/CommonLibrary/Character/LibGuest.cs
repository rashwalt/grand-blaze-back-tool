using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataAccess;
using CommonLibrary.DataFormat.Entity;
using System.Data;
using CommonLibrary.DataFormat.SpecialEntity;

namespace CommonLibrary.Character
{
    public partial class LibGuest : LibUnitBase
    {
        /// <summary>
        /// キャラクタ読み込みコンストラクタ
        /// </summary>
        /// <param name="Party">所属</param>
        /// <param name="GuestID">ゲストID</param>
        /// <param name="BaseLevel">基礎レベル</param>
        public LibGuest(int Party, int GuestID, int BaseLevel)
        {
            PartyBelong = Party;

            // データ読み込み＆セット
            ReadDataGuest(GuestID, BaseLevel);

            SetEquipData();
            SetEquipSkillData();

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

            _armorDFE = 0;
            _armorMGR = 0;
            Metal = 0;

            int ATKWeaponNo = HaveItemS[0].equip_main;
            if (ATKWeaponNo > 0)
            {
                CommonItemEntity.item_listRow ItemMainRow = LibItem.GetItemRow(ATKWeaponNo, false);

                _atk = ItemMainRow.it_physics;
                MainWeapon.AttackDamageType = ItemMainRow.it_attack_type;
                MainWeapon.Avoid = ItemMainRow.it_physics_parry;
                MainWeapon.ChargeTime = ItemMainRow.it_charge;
                MainWeapon.Critical = ItemMainRow.it_critical;
                LibEffect.Split(ItemMainRow.it_effect, ref MainWeapon.Effect);
                MainWeapon.Elemental.Fire = ItemMainRow.it_fire;
                MainWeapon.Elemental.Freeze = ItemMainRow.it_freeze;
                MainWeapon.Elemental.Air = ItemMainRow.it_air;
                MainWeapon.Elemental.Earth = ItemMainRow.it_earth;
                MainWeapon.Elemental.Water = ItemMainRow.it_water;
                MainWeapon.Elemental.Thunder = ItemMainRow.it_thunder;
                MainWeapon.Elemental.Holy = ItemMainRow.it_holy;
                MainWeapon.Elemental.Dark = ItemMainRow.it_dark;
                MainWeapon.Elemental.Slash = ItemMainRow.it_slash;
                MainWeapon.Elemental.Pierce = ItemMainRow.it_pierce;
                MainWeapon.Elemental.Strike = ItemMainRow.it_strike;
                MainWeapon.Elemental.Break = ItemMainRow.it_break;
                MainWeapon.ItemSubType = ItemMainRow.it_sub_category;
                MainWeapon.ItemType = ItemMainRow.it_type;
                Metal += ItemMainRow.it_metal;
                MainWeapon.Range = ItemMainRow.it_range;
                MainWeapon.TargetArea = ItemMainRow.it_target_area;

                LibEffect.SplitAdd(ItemMainRow.it_effect, ref EffectList, Status.EffectDiv.Deffence);
            }
            else
            {
                _atk = 11;

                MainWeapon.AttackDamageType = Status.AttackType.Combat;
                MainWeapon.Avoid = 0;
                MainWeapon.ChargeTime = 29;
                MainWeapon.Critical = 5;
                MainWeapon.Effect = new EffectListEntity.effect_listDataTable();
                MainWeapon.Elemental.Fire = 0;
                MainWeapon.Elemental.Freeze = 0;
                MainWeapon.Elemental.Air = 0;
                MainWeapon.Elemental.Earth = 0;
                MainWeapon.Elemental.Water = 0;
                MainWeapon.Elemental.Thunder = 0;
                MainWeapon.Elemental.Holy = 0;
                MainWeapon.Elemental.Dark = 0;
                MainWeapon.Elemental.Slash = 0;
                MainWeapon.Elemental.Pierce = 0;
                MainWeapon.Elemental.Strike = 100;
                MainWeapon.Elemental.Break = 0;
                MainWeapon.ItemSubType = 0;
                MainWeapon.ItemType = 0;
                MainWeapon.Range = Status.Range.Short;
                MainWeapon.TargetArea = Status.TargetArea.Only;
            }

            int ATKSubWeaponNo = HaveItemS[0].equip_sub;
            if (ATKSubWeaponNo > 0)
            {
                CommonItemEntity.item_listRow ItemSubRow = LibItem.GetItemRow(ATKSubWeaponNo, false);

                if (ItemSubRow.it_both_hand == false && ItemSubRow.it_equip_parts==Status.EquipSpot.Main)
                {
                    _sub_atk = ItemSubRow.it_physics;

                    SubWeapon.AttackDamageType = ItemSubRow.it_attack_type;
                    SubWeapon.Avoid = ItemSubRow.it_physics_parry;
                    SubWeapon.ChargeTime = ItemSubRow.it_charge;
                    SubWeapon.Critical = ItemSubRow.it_critical;
                    LibEffect.Split(ItemSubRow.it_effect, ref SubWeapon.Effect);
                    SubWeapon.Elemental.Fire = ItemSubRow.it_fire;
                    SubWeapon.Elemental.Freeze = ItemSubRow.it_freeze;
                    SubWeapon.Elemental.Air = ItemSubRow.it_air;
                    SubWeapon.Elemental.Earth = ItemSubRow.it_earth;
                    SubWeapon.Elemental.Water = ItemSubRow.it_water;
                    SubWeapon.Elemental.Thunder = ItemSubRow.it_thunder;
                    SubWeapon.Elemental.Holy = ItemSubRow.it_holy;
                    SubWeapon.Elemental.Dark = ItemSubRow.it_dark;
                    SubWeapon.Elemental.Slash = ItemSubRow.it_slash;
                    SubWeapon.Elemental.Pierce = ItemSubRow.it_pierce;
                    SubWeapon.Elemental.Strike = ItemSubRow.it_strike;
                    SubWeapon.Elemental.Break = ItemSubRow.it_break;
                    SubWeapon.ItemSubType = ItemSubRow.it_sub_category;
                    SubWeapon.ItemType = ItemSubRow.it_type;
                    Metal += ItemSubRow.it_metal;
                    SubWeapon.Range = ItemSubRow.it_range;
                    SubWeapon.TargetArea = ItemSubRow.it_target_area;

                    if (ItemSubRow.it_range < MainWeapon.Range)
                    {
                        MainWeapon.Range = ItemSubRow.it_range;
                    }
                }
                else if (ItemSubRow.it_equip_parts == Status.EquipSpot.Sub)
                {
                    ShieldAvoidPhysical = ItemSubRow.it_physics_parry;
                    ShieldAvoidSorcery = ItemSubRow.it_sorcery_parry;
                    Metal += ItemSubRow.it_metal;

                    LibEffect.SplitAdd(ItemSubRow.it_effect, ref EffectList, Status.EffectDiv.Deffence);
                }
            }

            int HeadProtectNo = HaveItemS[0].equip_head;
            if (HeadProtectNo > 0)
            {
                CommonItemEntity.item_listRow ItemHeadRow = LibItem.GetItemRow(HeadProtectNo, false);

                _armorDFE += ItemHeadRow.it_physics;
                _armorMGR += ItemHeadRow.it_sorcery;
                Metal += ItemHeadRow.it_metal;

                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Fire, ItemHeadRow.it_fire);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Freeze, ItemHeadRow.it_freeze);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Air, ItemHeadRow.it_air);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Earth, ItemHeadRow.it_earth);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Water, ItemHeadRow.it_water);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Thunder, ItemHeadRow.it_thunder);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Holy, ItemHeadRow.it_holy);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Dark, ItemHeadRow.it_dark);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Slash, ItemHeadRow.it_slash);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Pierce, ItemHeadRow.it_pierce);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Strike, ItemHeadRow.it_strike);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Break, ItemHeadRow.it_break);

                LibEffect.SplitAdd(ItemHeadRow.it_effect, ref EffectList, Status.EffectDiv.Deffence);
            }

            int BodyProtectNo = HaveItemS[0].equip_body;
            if (BodyProtectNo > 0)
            {
                CommonItemEntity.item_listRow ItemBodyRow = LibItem.GetItemRow(BodyProtectNo, false);

                _armorDFE += ItemBodyRow.it_physics;
                _armorMGR += ItemBodyRow.it_sorcery;
                Metal += ItemBodyRow.it_metal;

                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Fire, ItemBodyRow.it_fire);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Freeze, ItemBodyRow.it_freeze);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Air, ItemBodyRow.it_air);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Earth, ItemBodyRow.it_earth);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Water, ItemBodyRow.it_water);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Thunder, ItemBodyRow.it_thunder);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Holy, ItemBodyRow.it_holy);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Dark, ItemBodyRow.it_dark);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Slash, ItemBodyRow.it_slash);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Pierce, ItemBodyRow.it_pierce);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Strike, ItemBodyRow.it_strike);
                CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Break, ItemBodyRow.it_break);

                LibEffect.SplitAdd(ItemBodyRow.it_effect, ref EffectList, Status.EffectDiv.Deffence);
            }

            // アクセサリ設定
            int[] AcceNos = { HaveItemS[0].equip_accesory1, HaveItemS[0].equip_accesory2 };
            for (int i = 0; i < 2; i++)
            {
                int AccesoryProtectNo = AcceNos[i];

                if (AccesoryProtectNo > 0)
                {
                    CommonItemEntity.item_listRow ItemAcceRow = LibItem.GetItemRow(AccesoryProtectNo, false);

                    _armorDFE += ItemAcceRow.it_physics;
                    _armorMGR += ItemAcceRow.it_sorcery;
                    Metal += ItemAcceRow.it_metal;

                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Fire, ItemAcceRow.it_fire);
                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Freeze, ItemAcceRow.it_freeze);
                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Air, ItemAcceRow.it_air);
                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Earth, ItemAcceRow.it_earth);
                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Water, ItemAcceRow.it_water);
                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Thunder, ItemAcceRow.it_thunder);
                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Holy, ItemAcceRow.it_holy);
                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Dark, ItemAcceRow.it_dark);
                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Slash, ItemAcceRow.it_slash);
                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Pierce, ItemAcceRow.it_pierce);
                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Strike, ItemAcceRow.it_strike);
                    CommonLibrary.DataFormat.Format.Elemental.SettingElemental(ref DefenceElemental.Break, ItemAcceRow.it_break);

                    LibEffect.SplitAdd(ItemAcceRow.it_effect, ref EffectList, Status.EffectDiv.Deffence);
                }

            }
        }

        /// <summary>
        /// 装備スキル設定
        /// </summary>
        private void SetEquipSkillData()
        {
            // エフェクトリセット
            EffectList.DefaultView.RowFilter = "effect_div=" + Status.EffectDiv.SupportSkill;

            foreach (DataRowView EffectRow in EffectList.DefaultView)
            {
                EffectRow.Row.Delete();
            }
            EffectList.AcceptChanges();

            // インストールクラスによるサポートアビリティ
            InstallDataEntity.mt_install_class_skillRow[] InstallSkillRows = LibInstall.GetSkillRows(IntallClassID, InstallClassLevel);

            foreach (InstallDataEntity.mt_install_class_skillRow InstallSkill in InstallSkillRows)
            {
                if (InstallSkill.only_mode == Status.OnlyMode.Secondary) { continue; }

                CommonSkillEntity.skill_listRow SkillRow = LibSkill.GetSkillRow(InstallSkill.perks_id);

                if (SkillRow.sk_type == Status.SkillType.Support)
                {
                    LibEffect.SplitAdd(SkillRow.sk_effect, ref EffectList, Status.EffectDiv.SupportSkill);
                }
            }

            LibEffect.SplitAdd(OptionList, ref EffectList, Status.EffectDiv.SupportSkill);

            // スキル効果の属性耐性影響
            foreach (EffectListEntity.effect_listRow EffectRow in EffectList)
            {
                decimal Rank = EffectRow.rank;
                decimal SubRank = EffectRow.sub_rank;

                switch (EffectRow.effect_id)
                {
                    case 970:
                        #region 属性相性：火
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Fire < (int)Rank)
                            {
                                DefenceElemental.Fire = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                    case 971:
                        #region 属性相性：氷
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Freeze < (int)Rank)
                            {
                                DefenceElemental.Freeze = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                    case 972:
                        #region 属性相性：風
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Air < (int)Rank)
                            {
                                DefenceElemental.Air = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                    case 973:
                        #region 属性相性：土
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Earth < (int)Rank)
                            {
                                DefenceElemental.Earth = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                    case 974:
                        #region 属性相性：水
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Water < (int)Rank)
                            {
                                DefenceElemental.Water = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                    case 975:
                        #region 属性相性：雷
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Thunder < (int)Rank)
                            {
                                DefenceElemental.Thunder = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                    case 976:
                        #region 属性相性：聖
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Holy < (int)Rank)
                            {
                                DefenceElemental.Holy = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                    case 977:
                        #region 属性相性：闇
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Dark < (int)Rank)
                            {
                                DefenceElemental.Dark = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                    case 978:
                        #region 属性相性：斬
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Slash < (int)Rank)
                            {
                                DefenceElemental.Slash = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                    case 979:
                        #region 属性相性：突
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Pierce < (int)Rank)
                            {
                                DefenceElemental.Pierce = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                    case 980:
                        #region 属性相性：打
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Strike < (int)Rank)
                            {
                                DefenceElemental.Strike = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                    case 981:
                        #region 属性相性：壊
                        {
                            if ((int)SubRank == 1 || DefenceElemental.Break < (int)Rank)
                            {
                                DefenceElemental.Break = (int)Rank;
                            }
                        }
                        #endregion
                        break;
                }
            }

            int upSt = (int)(((decimal)Level - 1m) / 6m * 5m);
            if (upSt < 0) { upSt = 0; }
            if (upSt >= 40) { upSt = (int)(((decimal)Level - 1m) / 6m * 6m); }
            if (upSt >= 90) { upSt = (int)(((decimal)Level - 1m) / 6m * 7m); }
            if (upSt >= 150) { upSt = (int)(((decimal)Level - 1m) / 6m * 8m); }
            if (upSt >= 220) { upSt = (int)(((decimal)Level - 1m) / 6m * 9m); }

            int upSt2 = (int)(((decimal)Level - 1m) / 6m * 0.25m + ((decimal)Level - 1m) / 6m + 6m);

            if (CustomAtk > 0)
            {
                _atk = 14 + upSt;

                switch (CustomAtk)
                {
                    case 2:
                        _atk = (int)((decimal)_atk * 1.3m);
                        break;
                    case 3:
                        _atk = (int)((decimal)_atk * 1.6m);
                        break;
                    case 4:
                        _atk = (int)((decimal)_atk * 2m);
                        break;
                    case 5:
                        _atk = (int)((decimal)_atk * 2.2m);
                        break;
                    case 6:
                        _atk = (int)((decimal)_atk * 2.5m);
                        break;
                }
            }
            if (CustomDfe > 0)
            {
                _armorDFE = upSt2;

                switch (CustomDfe)
                {
                    case 1:
                        _armorDFE = (int)((decimal)_atk * 0.66m);
                        break;
                    case 2:
                        _armorDFE = (int)((decimal)_atk * 0.75m);
                        break;
                    case 3:
                        _armorDFE = (int)((decimal)_atk * 0.85m);
                        break;
                    case 5:
                        _armorDFE = (int)((decimal)_atk * 1.3m);
                        break;
                    case 6:
                        _armorDFE = (int)((decimal)_atk * 1.6m);
                        break;
                }
            }
            if (CustomMgr > 0)
            {
                _armorMGR = upSt2;

                switch (CustomMgr)
                {
                    case 1:
                        _armorMGR = (int)((decimal)_atk * 0.66m);
                        break;
                    case 2:
                        _armorMGR = (int)((decimal)_atk * 0.75m);
                        break;
                    case 3:
                        _armorMGR = (int)((decimal)_atk * 0.85m);
                        break;
                    case 5:
                        _armorMGR = (int)((decimal)_atk * 1.3m);
                        break;
                    case 6:
                        _armorMGR = (int)((decimal)_atk * 1.6m);
                        break;
                }
            }
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        /// <param name="GuestID">ゲストID</param>
        /// <param name="BaseLevel">基礎レベル</param>
        private void ReadDataGuest(int GuestID, int BaseLevel)
        {
            StringBuilder Sql = new StringBuilder();

            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                #region キャラクター基本情報読み込み
                GuestDataEntity.mt_guest_listDataTable CharacterBase = new GuestDataEntity.mt_guest_listDataTable();

                Sql = new StringBuilder();
                Sql.AppendLine("SELECT [guest_id]");
                Sql.AppendLine("      ,[character_name]");
                Sql.AppendLine("      ,[nick_name]");
                Sql.AppendLine("      ,[race_id]");
                Sql.AppendLine("      ,[unique_name]");
                Sql.AppendLine("      ,[belong_kb]");
                Sql.AppendLine("  FROM [mt_guest_list]");
                Sql.AppendLine("  WHERE ");
                Sql.AppendLine("       [guest_id] = " + GuestID);

                dba.Fill(Sql.ToString(), CharacterBase);

                if (CharacterBase.Count > 0)
                {
                    GuestDataEntity.mt_guest_listRow CharaBaseRow = CharacterBase[0];

                    EntryNo = CharaBaseRow.guest_id;
                    CharacterName = CharaBaseRow.character_name;
                    NickName = CharaBaseRow.nick_name;
                    _race = CharaBaseRow.race_id;
                    UniqueName = CharaBaseRow.unique_name;
                    PartyBelongDetail = CharaBaseRow.belong_kb;
                }

                #endregion

                #region キャラクターバトル情報読み込み
                GuestDataEntity.mt_guest_battle_abilityDataTable CharacterBattle = new GuestDataEntity.mt_guest_battle_abilityDataTable();

                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("guest_id, ");
                Sql.AppendLine("install, ");
                Sql.AppendLine("second_install, ");
                Sql.AppendLine("formation, ");
                Sql.AppendLine("[level], ");
                Sql.AppendLine("option_list, ");
                Sql.AppendLine("level_edit, ");
                Sql.AppendLine("atk_type, ");
                Sql.AppendLine("dfe_type, ");
                Sql.AppendLine("mgr_type ");
                Sql.AppendLine("FROM mt_guest_battle_ability");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("guest_id = " + GuestID);

                dba.Fill(Sql.ToString(), CharacterBattle);
                if (CharacterBattle.Rows.Count > 0)
                {
                    GuestDataEntity.mt_guest_battle_abilityRow CharaBattleRow = CharacterBattle[0];

                    IntallClassID = CharaBattleRow.install;
                    SecondryIntallClassID = CharaBattleRow.second_install;
                    Formation = CharaBattleRow.formation;
                    _level = CharaBattleRow.level;
                    if (_level <= 0) { _level = BaseLevel + CharaBattleRow.level_edit; }

                    // 能力の設定
                    InstallDataEntity.mt_install_class_listRow InstallRow = LibInstall.GetInstallRow(IntallClassID);

                    // 種族による能力追加
                    HumanRaceEntity.mt_race_listRow RaceRow = LibRace.GetRow(_race);

                    int AbBaseLevel = (InstallClassLevel + Level) / 2;

                    _max_hp = (int)LibRankData.GetRankToHP(InstallRow.up_hp, AbBaseLevel);
                    _max_mp = (int)LibRankData.GetRankToMP(InstallRow.up_mp, AbBaseLevel);
                    _str = (int)LibRankData.GetRankToSTR(InstallRow.up_str, AbBaseLevel);
                    _agi = (int)LibRankData.GetRankToSPD(InstallRow.up_agi, AbBaseLevel);
                    _mag = (int)LibRankData.GetRankToMAG(InstallRow.up_mag, AbBaseLevel);
                    _unq = (int)LibRankData.GetRankToVIT(InstallRow.up_unq, AbBaseLevel);

                    // オプション設定
                    OptionList = CharaBattleRow.option_list;

                    CustomAtk = CharaBattleRow.atk_type;
                    CustomDfe = CharaBattleRow.dfe_type;
                    CustomMgr = CharaBattleRow.mgr_type;
                }

                #endregion

                #region キャラクター行動情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("action_no, ");
                Sql.AppendLine("action_target, ");
                Sql.AppendLine("action, ");
                Sql.AppendLine("perks_id ");
                Sql.AppendLine("FROM mt_guest_action");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("guest_id = " + GuestID);
                Sql.AppendLine("and ");
                Sql.AppendLine("limit_level <= " + _level);
                Sql.AppendLine("ORDER BY action_no ");

                dba.Fill(Sql.ToString(), ActionList);
                #endregion

                #region キャラクター所持アイテム読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("guest_id, ");
                Sql.AppendLine("equip_main, ");
                Sql.AppendLine("equip_sub, ");
                Sql.AppendLine("equip_head, ");
                Sql.AppendLine("equip_body, ");
                Sql.AppendLine("equip_accesory1, ");
                Sql.AppendLine("equip_accesory2 ");
                Sql.AppendLine("FROM mt_guest_have_item");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("guest_id = " + GuestID);

                dba.Fill(Sql.ToString(), HaveItemS);

                #endregion

                #region キャラクターセリフ情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("serif_no, ");
                Sql.AppendLine("situation, ");
                Sql.AppendLine("perks_id, ");
                Sql.AppendLine("serif_text");
                Sql.AppendLine("FROM mt_guest_serif");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("guest_id = " + GuestID);

                dba.Fill(Sql.ToString(), SerifList);
                #endregion

                Category = Status.Category.Human;
            }
        }
    }
}
