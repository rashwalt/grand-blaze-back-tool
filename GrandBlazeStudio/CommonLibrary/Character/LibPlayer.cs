using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataFormat.Entity;
using System.Data;
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary.DataAccess;

namespace CommonLibrary.Character
{
    /// <summary>
    /// プレイヤーキャラクタークラス
    /// </summary>
    public partial class LibPlayer : LibUnitBase
    {
        /// <summary>
        /// 新規作成用コンストラクタ
        /// </summary>
        public LibPlayer()
        {
            PartyBelong = Status.Belong.Friend;
            PartyBelongDetail = Status.BelongDetail.Normal;
            IsNewPlayer = true;
        }

        /// <summary>
        /// キャラクタ読み込みコンストラクタ
        /// </summary>
        /// <param name="Party">所属</param>
        /// <param name="Entry">エントリーNo</param>
        public LibPlayer(int Party, int Entry)
        {
            PartyBelong = Party;
            PartyBelongDetail = Status.BelongDetail.Normal;

            // データ読み込み＆セット
            ReadDataFriend(Entry);

            SetEquipData();
            SetEquipSkillData();

            if (HPNow > HPMax)
            {
                HPNow = HPMax;
            }
            if (MPNow > MPMax)
            {
                MPNow = MPMax;
            }
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

            SetBonusEntity.mt_set_bonus_listDataTable TemporaryCheckSetBonusList = new SetBonusEntity.mt_set_bonus_listDataTable();

            CommonItemEntity.item_listRow ItemMainRow = GetHaveItemEquiped(Status.EquipSpot.Main);
            if (ItemMainRow != null)
            {
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

                EffectListEntity.effect_listDataTable AddEffectList = new EffectListEntity.effect_listDataTable();
                LibEffect.Split(ItemMainRow.it_effect, ref AddEffectList, false, Status.EffectDiv.Deffence);

                EffectListEntity.effect_listRow EffectRow = AddEffectList.FindByeffect_id(1400);
                if (EffectRow != null)
                {
                    int TargetSetID = (int)EffectRow.rank;
                    SetBonusEntity.mt_set_bonus_listRow row = TemporaryCheckSetBonusList.FindByset_id(TargetSetID);
                    if (row != null)
                    {
                        row.equip_main = true;
                    }
                    else
                    {
                        TemporaryCheckSetBonusList.Addmt_set_bonus_listRow(TargetSetID, "", "", true, false, false, false, false);
                    }
                }

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

            CommonItemEntity.item_listRow ItemSubRow = GetHaveItemEquiped(Status.EquipSpot.Sub);
            if (ItemSubRow != null)
            {
                if (ItemSubRow.it_both_hand == false && ItemSubRow.it_equip_parts == Status.EquipSpot.Main)
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

                    LibEffect.SplitAdd(ItemSubRow.it_effect, ref EffectList, Status.EffectDiv.Deffence);
                }
                else if (ItemSubRow.it_equip_parts == Status.EquipSpot.Sub)
                {
                    ShieldAvoidPhysical = ItemSubRow.it_physics_parry;
                    ShieldAvoidSorcery = ItemSubRow.it_sorcery_parry;
                    Metal += ItemSubRow.it_metal;

                    LibEffect.SplitAdd(ItemSubRow.it_effect, ref EffectList, Status.EffectDiv.Deffence);
                }

                EffectListEntity.effect_listDataTable AddEffectList = new EffectListEntity.effect_listDataTable();
                LibEffect.Split(ItemSubRow.it_effect, ref AddEffectList, false, Status.EffectDiv.Deffence);

                EffectListEntity.effect_listRow EffectRow = AddEffectList.FindByeffect_id(1400);
                if (EffectRow != null)
                {
                    int TargetSetID = (int)EffectRow.rank;
                    SetBonusEntity.mt_set_bonus_listRow row = TemporaryCheckSetBonusList.FindByset_id(TargetSetID);
                    if (row != null)
                    {
                        row.equip_sub = true;
                    }
                    else
                    {
                        TemporaryCheckSetBonusList.Addmt_set_bonus_listRow(TargetSetID, "", "", false, true, false, false, false);
                    }
                }
            }

            CommonItemEntity.item_listRow ItemHeadRow = GetHaveItemEquiped(Status.EquipSpot.Head);
            if (ItemHeadRow != null)
            {
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

                EffectListEntity.effect_listDataTable AddEffectList = new EffectListEntity.effect_listDataTable();
                LibEffect.Split(ItemHeadRow.it_effect, ref AddEffectList, false, Status.EffectDiv.Deffence);

                EffectListEntity.effect_listRow EffectRow = AddEffectList.FindByeffect_id(1400);
                if (EffectRow != null)
                {
                    int TargetSetID = (int)EffectRow.rank;
                    SetBonusEntity.mt_set_bonus_listRow row = TemporaryCheckSetBonusList.FindByset_id(TargetSetID);
                    if (row != null)
                    {
                        row.equip_head = true;
                    }
                    else
                    {
                        TemporaryCheckSetBonusList.Addmt_set_bonus_listRow(TargetSetID, "", "", false, false, true, false, false);
                    }
                }

                LibEffect.SplitAdd(ItemHeadRow.it_effect, ref EffectList, Status.EffectDiv.Deffence);
            }

            CommonItemEntity.item_listRow ItemBodyRow = GetHaveItemEquiped(Status.EquipSpot.Body);
            if (ItemBodyRow != null)
            {
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

                EffectListEntity.effect_listDataTable AddEffectList = new EffectListEntity.effect_listDataTable();
                LibEffect.Split(ItemBodyRow.it_effect, ref AddEffectList, false, Status.EffectDiv.Deffence);

                EffectListEntity.effect_listRow EffectRow = AddEffectList.FindByeffect_id(1400);
                if (EffectRow != null)
                {
                    int TargetSetID = (int)EffectRow.rank;
                    SetBonusEntity.mt_set_bonus_listRow row = TemporaryCheckSetBonusList.FindByset_id(TargetSetID);
                    if (row != null)
                    {
                        row.equip_body = true;
                    }
                    else
                    {
                        TemporaryCheckSetBonusList.Addmt_set_bonus_listRow(TargetSetID, "", "", false, false, false, true, false);
                    }
                }

                LibEffect.SplitAdd(ItemBodyRow.it_effect, ref EffectList, Status.EffectDiv.Deffence);
            }

            // アクセサリ設定
            CommonItemEntity.item_listRow ItemAcceRow = GetHaveItemEquiped(Status.EquipSpot.Accesory);
            if (ItemAcceRow != null)
            {
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

                EffectListEntity.effect_listDataTable AddEffectList = new EffectListEntity.effect_listDataTable();
                LibEffect.Split(ItemAcceRow.it_effect, ref AddEffectList, false, Status.EffectDiv.Deffence);

                EffectListEntity.effect_listRow EffectRow = AddEffectList.FindByeffect_id(1400);
                if (EffectRow != null)
                {
                    int TargetSetID = (int)EffectRow.rank;
                    SetBonusEntity.mt_set_bonus_listRow row = TemporaryCheckSetBonusList.FindByset_id(TargetSetID);
                    if (row != null)
                    {
                        row.equip_accesory = true;
                    }
                    else
                    {
                        TemporaryCheckSetBonusList.Addmt_set_bonus_listRow(TargetSetID, "", "", false, false, false, false, true);
                    }
                }

                LibEffect.SplitAdd(ItemAcceRow.it_effect, ref EffectList, Status.EffectDiv.Deffence);

            }

            // セットボーナスデータ追加
            foreach (SetBonusEntity.mt_set_bonus_listRow TemporaryRow in TemporaryCheckSetBonusList)
            {
                // 候補取得
                SetBonusEntity.mt_set_bonus_listRow TargetBonusRow = LibSetBonus.GetDataRow(TemporaryRow.set_id);

                if (TargetBonusRow == null) { continue; }

                // 条件判定
                if (TargetBonusRow.equip_main == TemporaryRow.equip_main &&
                    TargetBonusRow.equip_sub == TemporaryRow.equip_sub &&
                    TargetBonusRow.equip_head == TemporaryRow.equip_head &&
                    TargetBonusRow.equip_body == TemporaryRow.equip_body &&
                    TargetBonusRow.equip_accesory == TemporaryRow.equip_accesory)
                {
                    LibEffect.SplitAdd(TargetBonusRow.set_effect, ref EffectList, Status.EffectDiv.Deffence);
                }
            }
        }

        /// <summary>
        /// 装備スキル設定
        /// </summary>
        private void SetEquipSkillData()
        {
            // 実際に使用可能なスキルは以下のもの
            // インストールのスキル
            // プライベートスキル（スクロールフラグ: offのもののみ）
            // テクニックスキル
            // インストールで習得可能だが何らかの条件で使用OKになっているもの（壊魔士の魔法とか。
            // 　この場合、インストールスキルリストには「スクロール=3」として設定されており、
            // 　キャラクターのスキルとしてスキルIDが登録されていること（フラグ「スクロール（sc_flg）がon」になっていることが必要）

            // エフェクトリセット
            EffectList.DefaultView.RowFilter = "effect_div=" + Status.EffectDiv.SupportSkill;

            foreach (DataRowView EffectRow in EffectList.DefaultView)
            {
                EffectRow.Row.Delete();
            }
            EffectList.AcceptChanges();

            // インストールクラスによるスキル
            InstallDataEntity.mt_install_class_skillRow[] InstallSkillRows = LibInstall.GetSkillRows(IntallClassID, InstallClassLevel);

            foreach (InstallDataEntity.mt_install_class_skillRow InstallSkill in InstallSkillRows)
            {
                if (InstallSkill.only_mode == Status.OnlyMode.Secondary) { continue; }
                if (InstallSkill.only_mode == Status.OnlyMode.ScrollUsing)
                {
                    CommonUnitDataEntity.have_skill_listRow HaveSkillByClassSkill = HaveSkill.FindBysk_num(InstallSkill.perks_id);
                    if (HaveSkillByClassSkill == null || !HaveSkillByClassSkill.sc_flg)
                    {
                        continue;
                    }
                }

                CommonSkillEntity.skill_listRow SkillRow = LibSkill.GetSkillRow(InstallSkill.perks_id);

                if (SkillRow.sk_type == Status.SkillType.Support)
                {
                    LibEffect.SplitAdd(SkillRow.sk_effect, ref EffectList, Status.EffectDiv.SupportSkill);
                }

                CommonSkillEntity.skill_listRow NewSkillRow = UsingSkillList.Newskill_listRow();
                NewSkillRow.ItemArray = SkillRow.ItemArray;
                UsingSkillList.Addskill_listRow(NewSkillRow);
                CommonSkillEntity.skill_listRow NewSkillRow2 = ClassHaveSkill.Newskill_listRow();
                NewSkillRow2.ItemArray = SkillRow.ItemArray;
                ClassHaveSkill.Addskill_listRow(NewSkillRow2);
            }

            if (SecondryIntallClassID > 0)
            {
                InstallDataEntity.mt_install_class_skillRow[] SecondaryInstallSkillRows = LibInstall.GetSkillRows(SecondryIntallClassID, SecondryInstallClassLevel);

                foreach (InstallDataEntity.mt_install_class_skillRow InstallSkill in SecondaryInstallSkillRows)
                {
                    if (InstallSkill.only_mode == Status.OnlyMode.Primary) { continue; }
                    if (InstallSkill.only_mode == Status.OnlyMode.ScrollUsing)
                    {
                        CommonUnitDataEntity.have_skill_listRow HaveSkillByClassSkill = HaveSkill.FindBysk_num(InstallSkill.perks_id);
                        if (HaveSkillByClassSkill == null || !HaveSkillByClassSkill.sc_flg)
                        {
                            continue;
                        }
                    }

                    CommonSkillEntity.skill_listRow SkillRow = LibSkill.GetSkillRow(InstallSkill.perks_id);

                    if (SkillRow.sk_type == Status.SkillType.Support)
                    {
                        LibEffect.SplitAdd(SkillRow.sk_effect, ref EffectList, Status.EffectDiv.SupportSkill);
                    }

                    if (UsingSkillList.FindBysk_id(SkillRow.sk_id) == null)
                    {
                        CommonSkillEntity.skill_listRow NewSkillRow = UsingSkillList.Newskill_listRow();
                        NewSkillRow.ItemArray = SkillRow.ItemArray;
                        UsingSkillList.Addskill_listRow(NewSkillRow);
                    }
                    if (ClassHaveSkill.FindBysk_id(SkillRow.sk_id) == null)
                    {
                        CommonSkillEntity.skill_listRow NewSkillRow2 = ClassHaveSkill.Newskill_listRow();
                        NewSkillRow2.ItemArray = SkillRow.ItemArray;
                        ClassHaveSkill.Addskill_listRow(NewSkillRow2);
                    }
                }
            }

            // プライベートスキル取得
            foreach (CommonUnitDataEntity.have_skill_listRow HaveSkillRow in HaveSkill)
            {
                if (HaveSkillRow.sc_flg)
                {
                    continue;
                }
                CommonSkillEntity.skill_listRow SkillRow = LibSkill.GetSkillRow(HaveSkillRow.sk_num);

                if (SkillRow.sk_type == Status.SkillType.Support)
                {
                    LibEffect.SplitAdd(SkillRow.sk_effect, ref EffectList, Status.EffectDiv.SupportSkill);
                }

                CommonSkillEntity.skill_listRow NewSkillRow = UsingSkillList.Newskill_listRow();
                NewSkillRow.ItemArray = SkillRow.ItemArray;
                UsingSkillList.Addskill_listRow(NewSkillRow);
            }

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

            // 属性変化
            foreach (EffectListEntity.effect_listRow EffectRow in EffectList)
            {
                decimal Rank = EffectRow.rank;
                decimal SubRank = EffectRow.sub_rank;

                switch (EffectRow.effect_id)
                {
                    case 1010:
                        #region 火属性変化
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Fire);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Fire = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                    case 1011:
                        #region 属性相性：氷
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Freeze);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Freeze = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                    case 1012:
                        #region 属性相性：風
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Air);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Air = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                    case 1013:
                        #region 属性相性：土
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Earth);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Earth = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                    case 1014:
                        #region 属性相性：水
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Water);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Water = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                    case 1015:
                        #region 属性相性：雷
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Thunder);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Thunder = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                    case 1016:
                        #region 属性相性：聖
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Holy);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Holy = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                    case 1017:
                        #region 属性相性：闇
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Dark);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Dark = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                    case 1018:
                        #region 属性相性：斬
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Slash);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Slash = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                    case 1019:
                        #region 属性相性：突
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Pierce);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Pierce = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                    case 1020:
                        #region 属性相性：打
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Strike);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Strike = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                    case 1021:
                        #region 属性相性：壊
                        {
                            int ElementalNum = CommonLibrary.DataFormat.Format.Elemental.ConvertElementalToInt(DefenceElemental.Break);
                            ElementalNum += (int)Rank;
                            DefenceElemental.Break = CommonLibrary.DataFormat.Format.Elemental.ConvertIntToElemental(ElementalNum);
                        }
                        #endregion
                        break;
                }
            }
        }

        /// <summary>
        /// 性別を数値で設定
        /// </summary>
        /// <param name="SexInt">性別数値</param>
        public void SetSex(int SexInt)
        {
            _sex = SexInt;
        }

        /// <summary>
        /// 所属国を数値で設定
        /// </summary>
        /// <param name="NationInt">国家No</param>
        public void SetNation(int NationInt)
        {
            _nation = NationInt;
        }

        /// <summary>
        /// インストールクラスを数値で設定
        /// </summary>
        /// <param name="InstallNo">インストールID</param>
        /// <returns>ステータス</returns>
        public int SetInstallClass(int InstallNo)
        {
            // 習得しているか判定
            CommonUnitDataEntity.install_level_listRow row = InstallClassList.FindByinstall_id(InstallNo);

            if (row == null)
            {
                // 習得していない
                return Status.Install.NoGetting;
            }

            IntallClassID = InstallNo;

            // 装備の再設定
            int ItemID = 0;
            bool ItemCreated = false;
            int HaveNo = 0;
            int[] EquipSpotsList = { Status.EquipSpot.Main, Status.EquipSpot.Sub, Status.EquipSpot.Head, Status.EquipSpot.Body, Status.EquipSpot.Accesory };
            for(int i=0;i<EquipSpotsList.Length;i++)
            {
                EquipRemove(EquipSpotsList[i], ref ItemID, ref ItemCreated, ref HaveNo);
                Equip(EquipSpotsList[i], HaveNo);
            }

            return Status.Install.OK;
        }

        /// <summary>
        /// サブインストールクラスを数値で設定
        /// </summary>
        /// <param name="InstallNo">インストールID</param>
        /// <returns>ステータス</returns>
        public int SetSecondaryInstallClass(int InstallNo)
        {
            // 習得しているか判定
            CommonUnitDataEntity.install_level_listRow row = InstallClassList.FindByinstall_id(InstallNo);

            if (row == null)
            {
                // 習得していない
                return Status.Install.NoGetting;
            }

            SecondryIntallClassID = InstallNo;

            return Status.Install.OK;
        }

        /// <summary>
        /// 種族を数値で設定
        /// </summary>
        /// <param name="RaceID">種族No</param>
        public void SetRace(int RaceID)
        {
            _race = RaceID;
        }

        /// <summary>
        /// 守護者を数値で設定
        /// </summary>
        /// <param name="GuardianID">守護者No</param>
        public void SetGuardian(int GuardianID)
        {
            _guardian = GuardianID;
        }

        /// <summary>
        /// キャラクターデータ全更新
        /// </summary>
        public void Update()
        {
            UpdatePersonalData();
            UpdateBattleData();
            UpdateActionData();
            UpdateHaveItemData();
            UpdateHaveSkillData();
            UpdateLevelUpData();
            UpdateEventFlagData();
            UpdateInstallClassData();
            UpdateQuestData();
            UpdateSerifData();
            UpdateKeyItemData();
            UpdateIconData();
            UpdateRecordData();
            UpdateMovingMarks();
        }

        /// <summary>
        /// キャラクター個人データ登録処理
        /// </summary>
        public void UpdatePersonalData()
        {
            CharacterDataEntity.ts_character_listDataTable Table = new CharacterDataEntity.ts_character_listDataTable();
            CharacterDataEntity.ts_character_listRow Row = Table.Newts_character_listRow();

            Row.entry_no = EntryNo;
            Row.continue_cnt = ContinueNoCount;
            Row.continue_bonus = ContinueBonus;
            Row.account_status = AccountStatus;
            Row.new_play = NewPlayRegistUpdate;
            Row.last_update = LastUpdate;
            Row.new_gamer = IsNewPlayer;
            Row.character_name = CharacterName;
            Row.image_url = ImageURL;
            Row.image_width = ImageWidthSize;
            Row.image_height = ImageHeightSize;
            Row.image_link_url = ImageLinkURL;
            Row.image_copyright = ImageCopyright;
            Row.nick_name = NickName;
            Row.race_id = _race;
            Row.guardian_id = _guardian;
            Row.nation_id = _nation;
            Row.have_money = HaveMoney;
            Row.blaze_chip = BlazeChip;
            Row.age = Age;
            Row.sex = _sex;
            Row.height = Height;
            Row.weight = Weight;
            Row.max_item = _maxHaveItem;
            Row.max_bazzeritem = MaxBazzerItem;
            Row.profile = Profile;
            Row.change_install = IsInstallClassChanging;
            Row.unique_name = UniqueName;
            Row.familiar_name = FamiliarName;

            string UpSql = LibSql.MakeUpSql("ts_character_list", "entry_no=" + EntryNo, Row);
            string InSql = LibSql.MakeInSql("ts_character_list", Row);

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();
                if (dba.ExecuteNonQuery(UpSql) == 0)
                {
                    dba.ExecuteNonQuery(InSql);
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }
        }

        /// <summary>
        /// キャラクターバトルデータ登録処理
        /// </summary>
        public void UpdateBattleData()
        {
            CharacterDataEntity.ts_character_battle_abilityDataTable Table = new CharacterDataEntity.ts_character_battle_abilityDataTable();
            CharacterDataEntity.ts_character_battle_abilityRow Row = Table.Newts_character_battle_abilityRow();

            Row.entry_no = EntryNo;
            Row.install = IntallClassID;
            Row.second_install = SecondryIntallClassID;
            Row.formation = Formation;
            Row.level = Level;
            Row.exp = _exp;
            Row.hp = _now_hp;
            Row.mp = _now_mp;
            Row.exp_unit = GetExp;
            Row.levelup_point = LevelUpPoint;

            string UpSql = LibSql.MakeUpSql("ts_character_battle_ability", "entry_no=" + EntryNo, Row);
            string InSql = LibSql.MakeInSql("ts_character_battle_ability", Row);

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();
                if (dba.ExecuteNonQuery(UpSql) == 0)
                {
                    dba.ExecuteNonQuery(InSql);
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }
        }

        /// <summary>
        /// キャラクター行動情報登録処理
        /// </summary>
        public void UpdateActionData()
        {
            if (ActionList.GetChanges() == null)
            {
                return;
            }

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                CharacterDataEntity.ts_character_actionDataTable Table = new CharacterDataEntity.ts_character_actionDataTable();

                foreach (DataRow ActionRow in ActionList.GetChanges().Rows)
                {
                    if (ActionRow.RowState == DataRowState.Added || ActionRow.RowState == DataRowState.Modified)
                    {
                        CommonUnitDataEntity.action_listRow ActionFormatRow = (CommonUnitDataEntity.action_listRow)ActionRow;
                        CharacterDataEntity.ts_character_actionRow Row = Table.Newts_character_actionRow();

                        Row.entry_no = EntryNo;
                        Row.action_no = ActionFormatRow.action_no;
                        Row.action_target = ActionFormatRow.action_target;
                        Row.action = ActionFormatRow.action;
                        Row.perks_id = ActionFormatRow.perks_id;

                        string UpSql = LibSql.MakeUpSql("ts_character_action", "entry_no=" + EntryNo + " and action_no=" + ActionFormatRow.action_no, Row);
                        string InSql = LibSql.MakeInSql("ts_character_action", Row);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (ActionRow.RowState == DataRowState.Deleted)
                    {
                        string DelSql = "DELETE FROM ts_character_action WHERE entry_no=" + EntryNo + " and action_no=" + (int)ActionRow["action_no", DataRowVersion.Original];

                        dba.ExecuteNonQuery(DelSql);
                    }
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }
        }

        /// <summary>
        /// キャラクター所持アイテム登録処理
        /// </summary>
        public void UpdateHaveItemData()
        {
            if (HaveItem.Rows.Count == 0 || HaveItem.GetChanges() == null)
            {
                return;
            }

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                CharacterDataEntity.ts_character_have_itemDataTable Table = new CharacterDataEntity.ts_character_have_itemDataTable();

                foreach (DataRow HaveItemRow in HaveItem.GetChanges().Rows)
                {
                    if (HaveItemRow.RowState == DataRowState.Added || HaveItemRow.RowState == DataRowState.Modified)
                    {
                        CommonUnitDataEntity.have_item_listRow HaveItemFormatRow = (CommonUnitDataEntity.have_item_listRow)HaveItemRow;
                        CharacterDataEntity.ts_character_have_itemRow Row = Table.Newts_character_have_itemRow();

                        Row.entry_no = EntryNo;
                        Row.box_type = HaveItemFormatRow.box_type;
                        Row.have_no = HaveItemFormatRow.have_no;
                        Row.it_num = HaveItemFormatRow.it_num;
                        Row.it_box_count = HaveItemFormatRow.it_box_count;
                        Row.it_box_baz_count = HaveItemFormatRow.it_box_baz_count;
                        Row.created = HaveItemFormatRow.created;
                        Row.equip_spot = HaveItemFormatRow.equip_spot;
                        Row._new = HaveItemFormatRow._new;
                        Row.it_name = HaveItemFormatRow.it_name;
                        Row.it_comment = HaveItemFormatRow.it_comment;
                        Row.it_effect = HaveItemFormatRow.it_effect;
                        Row.it_price = HaveItemFormatRow.it_price;
                        Row.it_seller = HaveItemFormatRow.it_seller;

                        string UpSql = LibSql.MakeUpSql("ts_character_have_item", "entry_no=" + EntryNo + " and have_no=" + HaveItemFormatRow.have_no + " and box_type=" + HaveItemFormatRow.box_type, Row);
                        string InSql = LibSql.MakeInSql("ts_character_have_item", Row);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (HaveItemRow.RowState == DataRowState.Deleted)
                    {
                        string DelSql = "DELETE FROM ts_character_have_item WHERE entry_no=" + EntryNo + " and have_no=" + (int)HaveItemRow["have_no", DataRowVersion.Original] + " and box_type=" + (int)HaveItemRow["box_type", DataRowVersion.Original];

                        dba.ExecuteNonQuery(DelSql);
                    }
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }
        }

        /// <summary>
        /// キャラクター所持スキル登録処理
        /// </summary>
        public void UpdateHaveSkillData()
        {
            if (HaveSkill.Rows.Count == 0 || HaveSkill.GetChanges() == null)
            {
                return;
            }

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                CharacterDataEntity.ts_character_have_skillDataTable Table = new CharacterDataEntity.ts_character_have_skillDataTable();

                foreach (DataRow HaveSkillRow in HaveSkill.GetChanges().Rows)
                {
                    if (HaveSkillRow.RowState == DataRowState.Added || HaveSkillRow.RowState == DataRowState.Modified)
                    {
                        CommonUnitDataEntity.have_skill_listRow HaveSkillFormatRow = (CommonUnitDataEntity.have_skill_listRow)HaveSkillRow;
                        CharacterDataEntity.ts_character_have_skillRow Row = Table.Newts_character_have_skillRow();

                        Row.entry_no = EntryNo;
                        Row.sk_num = HaveSkillFormatRow.sk_num;
                        Row._new = HaveSkillFormatRow._new;

                        string UpSql = LibSql.MakeUpSql("ts_character_have_skill", "entry_no=" + EntryNo + " and sk_num=" + HaveSkillFormatRow.sk_num, Row);
                        string InSql = LibSql.MakeInSql("ts_character_have_skill", Row);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (HaveSkillRow.RowState == DataRowState.Deleted)
                    {
                        string DelSql = "DELETE FROM ts_character_have_skill WHERE entry_no=" + EntryNo + " and sk_num=" + (int)HaveSkillRow["sk_num", DataRowVersion.Original] + " and created=" + LibSql.ConvertBit((bool)HaveSkillRow["created", DataRowVersion.Original]);

                        dba.ExecuteNonQuery(DelSql);
                    }
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }
        }

        /// <summary>
        /// キャラクターレベルアップ情報登録処理
        /// </summary>
        public void UpdateLevelUpData()
        {
            CharacterDataEntity.ts_character_levelup_abilityDataTable Table = new CharacterDataEntity.ts_character_levelup_abilityDataTable();
            CharacterDataEntity.ts_character_levelup_abilityRow Row = Table.Newts_character_levelup_abilityRow();

            Row.entry_no = EntryNo;
            Row.hp = LevelUpHP;
            Row.mp = LevelUpMP;
            Row.str = LevelUpSTR;
            Row.agi = LevelUpAGI;
            Row.mag = LevelUpMAG;
            Row.unq = LevelUpUNQ;

            string UpSql = LibSql.MakeUpSql("ts_character_levelup_ability", "entry_no=" + EntryNo, Row);
            string InSql = LibSql.MakeInSql("ts_character_levelup_ability", Row);

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();
                if (dba.ExecuteNonQuery(UpSql) == 0)
                {
                    dba.ExecuteNonQuery(InSql);
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }
        }

        /// <summary>
        /// キャラクターフラグ情報登録処理
        /// </summary>
        public void UpdateEventFlagData()
        {
            if (EventFlag.Rows.Count == 0 || EventFlag.GetChanges() == null)
            {
                return;
            }

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                CharacterDataEntity.ts_character_event_flagDataTable Table = new CharacterDataEntity.ts_character_event_flagDataTable();

                foreach (DataRow EventFlagRow in EventFlag.GetChanges().Rows)
                {
                    if (EventFlagRow.RowState == DataRowState.Added || EventFlagRow.RowState == DataRowState.Modified)
                    {
                        CommonUnitDataEntity.event_flagRow EventFormatRow = (CommonUnitDataEntity.event_flagRow)EventFlagRow;
                        CharacterDataEntity.ts_character_event_flagRow Row = Table.Newts_character_event_flagRow();

                        Row.entry_no = EntryNo;
                        Row.flag_id = EventFormatRow.flag_id;
                        Row.flag_value = EventFormatRow.flag_value;

                        string UpSql = LibSql.MakeUpSql("ts_character_event_flag", "entry_no=" + EntryNo + " and flag_id=" + EventFormatRow.flag_id, Row);
                        string InSql = LibSql.MakeInSql("ts_character_event_flag", Row);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (EventFlagRow.RowState == DataRowState.Deleted)
                    {
                        string DelSql = "DELETE FROM ts_character_event_flag WHERE entry_no=" + EntryNo + " and flag_id=" + (int)EventFlagRow["flag_id", DataRowVersion.Original];

                        dba.ExecuteNonQuery(DelSql);
                    }
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }
        }

        /// <summary>
        /// キャラクターインストールクラス登録処理
        /// </summary>
        public void UpdateInstallClassData()
        {
            if (InstallClassList.GetChanges() == null)
            {
                return;
            }

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                CharacterDataEntity.ts_character_install_levelDataTable Table = new CharacterDataEntity.ts_character_install_levelDataTable();

                foreach (DataRow InstallClassRow in InstallClassList.GetChanges().Rows)
                {
                    if (InstallClassRow.RowState == DataRowState.Added || InstallClassRow.RowState == DataRowState.Modified)
                    {
                        CommonUnitDataEntity.install_level_listRow InstallClassFormatRow = (CommonUnitDataEntity.install_level_listRow)InstallClassRow;
                        CharacterDataEntity.ts_character_install_levelRow Row = Table.Newts_character_install_levelRow();

                        Row.entry_no = EntryNo;
                        Row.install_id = InstallClassFormatRow.install_id;
                        Row.level = InstallClassFormatRow.level;
                        Row.exp = InstallClassFormatRow.exp;

                        string UpSql = LibSql.MakeUpSql("ts_character_install_level", "entry_no=" + EntryNo + " and install_id=" + InstallClassFormatRow.install_id, Row);
                        string InSql = LibSql.MakeInSql("ts_character_install_level", Row);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (InstallClassRow.RowState == DataRowState.Deleted)
                    {
                        string DelSql = "DELETE FROM ts_character_install_level WHERE entry_no=" + EntryNo + " and install_class_no=" + (int)InstallClassRow["install_class_no", DataRowVersion.Original];

                        dba.ExecuteNonQuery(DelSql);
                    }
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }
        }

        /// <summary>
        /// キャラクタークエスト情報登録処理
        /// </summary>
        public void UpdateQuestData()
        {
            if (QuestList.Rows.Count == 0 || QuestList.GetChanges() == null)
            {
                return;
            }

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                CharacterDataEntity.ts_character_questDataTable Table = new CharacterDataEntity.ts_character_questDataTable();

                foreach (DataRow QuestRow in QuestList.GetChanges().Rows)
                {
                    if (QuestRow.RowState == DataRowState.Added || QuestRow.RowState == DataRowState.Modified)
                    {
                        CommonUnitDataEntity.quest_listRow QuestFormatRow = (CommonUnitDataEntity.quest_listRow)QuestRow;
                        CharacterDataEntity.ts_character_questRow Row = Table.Newts_character_questRow();

                        Row.entry_no = EntryNo;
                        Row.quest_id = QuestFormatRow.quest_id;
                        Row.clear_fg = QuestFormatRow.clear_fg;
                        Row.quest_step = QuestFormatRow.quest_step;

                        string UpSql = LibSql.MakeUpSql("ts_character_quest", "entry_no=" + EntryNo + " and quest_id=" + QuestFormatRow.quest_id, Row);
                        string InSql = LibSql.MakeInSql("ts_character_quest", Row);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (QuestRow.RowState == DataRowState.Deleted)
                    {
                        string DelSql = "DELETE FROM ts_character_quest WHERE entry_no=" + EntryNo + " and quest_id=" + (int)QuestRow["quest_id", DataRowVersion.Original];

                        dba.ExecuteNonQuery(DelSql);
                    }
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }
        }

        /// <summary>
        /// キャラクターセリフ情報登録処理
        /// </summary>
        public void UpdateSerifData()
        {
            if (SerifList.Rows.Count == 0 || SerifList.GetChanges() == null)
            {
                return;
            }

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                CharacterDataEntity.ts_character_serifDataTable Table = new CharacterDataEntity.ts_character_serifDataTable();

                string DelSql = "DELETE FROM ts_character_serif WHERE entry_no=" + EntryNo;

                dba.ExecuteNonQuery(DelSql);

                foreach (DataRow SerifRow in SerifList.GetChanges().Rows)
                {
                    if (SerifRow.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }
                    CommonUnitDataEntity.serif_listRow SerifFormatRow = (CommonUnitDataEntity.serif_listRow)SerifRow;
                    CharacterDataEntity.ts_character_serifRow Row = Table.Newts_character_serifRow();

                    Row.entry_no = EntryNo;
                    Row.serif_no = SerifFormatRow.serif_no;
                    Row.situation = SerifFormatRow.situation;
                    Row.serif_text = SerifFormatRow.serif_text;
                    Row.perks_id = SerifFormatRow.perks_id;

                    string UpSql = LibSql.MakeUpSql("ts_character_serif", "entry_no=" + EntryNo + " and serif_no=" + SerifFormatRow.serif_no + " and situation=" + SerifFormatRow.situation, Row);
                    string InSql = LibSql.MakeInSql("ts_character_serif", Row);

                    if (dba.ExecuteNonQuery(UpSql) == 0)
                    {
                        dba.ExecuteNonQuery(InSql);
                    }
                }
                dba.Commit();
            }
            catch(Exception ex)
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }
        }

        /// <summary>
        /// キャラクター貴重品情報登録処理
        /// </summary>
        public void UpdateKeyItemData()
        {
            if (KeyItemList.Rows.Count == 0 || KeyItemList.GetChanges() == null)
            {
                return;
            }

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                CharacterDataEntity.ts_character_have_key_itemDataTable Table = new CharacterDataEntity.ts_character_have_key_itemDataTable();

                foreach (DataRow KeyItemRow in KeyItemList.GetChanges().Rows)
                {
                    if (KeyItemRow.RowState == DataRowState.Added || KeyItemRow.RowState == DataRowState.Modified)
                    {
                        CommonUnitDataEntity.key_item_listRow KeyItemFormatRow = (CommonUnitDataEntity.key_item_listRow)KeyItemRow;
                        CharacterDataEntity.ts_character_have_mt_key_item_listRow Row = Table.Newts_character_have_mt_key_item_listRow();

                        Row.entry_no = EntryNo;
                        Row.key_item_id = KeyItemFormatRow.key_item_id;
                        Row._new = KeyItemFormatRow._new;

                        string UpSql = LibSql.MakeUpSql("ts_character_have_key_item", "entry_no=" + EntryNo + " and key_item_id=" + KeyItemFormatRow.key_item_id, Row);
                        string InSql = LibSql.MakeInSql("ts_character_have_key_item", Row);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (KeyItemRow.RowState == DataRowState.Deleted)
                    {
                        string DelSql = "DELETE FROM ts_character_have_key_item WHERE entry_no=" + EntryNo + " and key_item_id=" + (int)KeyItemRow["key_item_id", DataRowVersion.Original];

                        dba.ExecuteNonQuery(DelSql);
                    }
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }
        }

        /// <summary>
        /// キャラクターアイコン情報登録処理
        /// </summary>
        public bool UpdateIconData()
        {
            if (IconList.Rows.Count == 0 || IconList.GetChanges() == null)
            {
                return false;
            }

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                CharacterDataEntity.ts_character_iconDataTable Table = new CharacterDataEntity.ts_character_iconDataTable();

                foreach (DataRow IconRow in IconList.GetChanges().Rows)
                {
                    if (IconRow.RowState == DataRowState.Added || IconRow.RowState == DataRowState.Modified)
                    {
                        CommonUnitDataEntity.icon_listRow IconFormatRow = (CommonUnitDataEntity.icon_listRow)IconRow;
                        CharacterDataEntity.ts_character_iconRow Row = Table.Newts_character_iconRow();

                        Row.entry_no = EntryNo;
                        Row.icon_id = IconFormatRow.icon_id;
                        Row.icon_url = IconFormatRow.icon_url;
                        Row.icon_copyright = IconFormatRow.icon_copyright;

                        string UpSql = LibSql.MakeUpSql("ts_character_icon", "entry_no=" + EntryNo + " and icon_id=" + IconFormatRow.icon_id, Row);
                        string InSql = LibSql.MakeInSql("ts_character_icon", Row);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (IconRow.RowState == DataRowState.Deleted)
                    {
                        string DelSql = "DELETE FROM ts_character_icon WHERE entry_no=" + EntryNo + " and icon_id=" + (int)IconRow["icon_id", DataRowVersion.Original];

                        dba.ExecuteNonQuery(DelSql);
                    }
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }

            return true;
        }

        /// <summary>
        /// キャラクターレコード登録処理
        /// </summary>
        public bool UpdateRecordData()
        {
            if (Record.Rows.Count == 0 || Record.GetChanges() == null)
            {
                return false;
            }

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                foreach (DataRow RecordRow in Record.GetChanges().Rows)
                {
                    if (RecordRow.RowState == DataRowState.Added || RecordRow.RowState == DataRowState.Modified)
                    {
                        string UpSql = LibSql.MakeUpSql("ts_character_record", "entry_no=" + EntryNo, RecordRow);
                        string InSql = LibSql.MakeInSql("ts_character_record", RecordRow);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (RecordRow.RowState == DataRowState.Deleted)
                    {
                        string DelSql = "DELETE FROM ts_character_record WHERE entry_no=" + EntryNo;

                        dba.ExecuteNonQuery(DelSql);
                    }
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }

            return true;
        }

        /// <summary>
        /// キャラクター移動可能マーク登録処理
        /// </summary>
        public bool UpdateMovingMarks()
        {
            if (MovingOKMarks.Rows.Count == 0 || MovingOKMarks.GetChanges() == null)
            {
                return false;
            }

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                foreach (CharacterDataEntity.ts_character_moving_markRow MovingMarkRow in MovingOKMarks.GetChanges().Rows)
                {
                    if (MovingMarkRow.RowState == DataRowState.Added || MovingMarkRow.RowState == DataRowState.Modified)
                    {
                        string UpSql = LibSql.MakeUpSql("ts_character_moving_mark", "entry_no=" + EntryNo + " and mark_id=" + MovingMarkRow.mark_id, MovingMarkRow);
                        string InSql = LibSql.MakeInSql("ts_character_moving_mark", MovingMarkRow);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (MovingMarkRow.RowState == DataRowState.Deleted)
                    {
                        string DelSql = "DELETE FROM ts_character_moving_mark WHERE entry_no=" + EntryNo + " and mark_id=" + (int)MovingMarkRow["mark_id", DataRowVersion.Original];

                        dba.ExecuteNonQuery(DelSql);
                    }
                }
                dba.Commit();
            }
            catch
            {
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }

            return true;
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        /// <param name="Entry">エントリーNo</param>
        private void ReadDataFriend(int Entry)
        {
            StringBuilder Sql = new StringBuilder();

            using (LibDBLocal dba = new LibDBLocal())
            {
                #region キャラクター基本情報読み込み
                CharacterDataEntity.ts_character_listDataTable CharacterBase = new CharacterDataEntity.ts_character_listDataTable();

                Sql = new StringBuilder();
                Sql.AppendLine("SELECT [entry_no]");
                Sql.AppendLine("      ,[continue_cnt]");
                Sql.AppendLine("      ,[continue_bonus]");
                Sql.AppendLine("      ,[account_status]");
                Sql.AppendLine("      ,[new_play]");
                Sql.AppendLine("      ,[last_update]");
                Sql.AppendLine("      ,[new_gamer]");
                Sql.AppendLine("      ,[character_name]");
                Sql.AppendLine("      ,[image_url]");
                Sql.AppendLine("      ,[image_width]");
                Sql.AppendLine("      ,[image_height]");
                Sql.AppendLine("      ,[image_link_url]");
                Sql.AppendLine("      ,[image_copyright]");
                Sql.AppendLine("      ,[nick_name]");
                Sql.AppendLine("      ,[race_id]");
                Sql.AppendLine("      ,[guardian_id]");
                Sql.AppendLine("      ,[nation_id]");
                Sql.AppendLine("      ,[have_money]");
                Sql.AppendLine("      ,[blaze_chip]");
                Sql.AppendLine("      ,[age]");
                Sql.AppendLine("      ,[sex]");
                Sql.AppendLine("      ,[height]");
                Sql.AppendLine("      ,[weight]");
                Sql.AppendLine("      ,[max_item]");
                Sql.AppendLine("      ,[max_bazzeritem]");
                Sql.AppendLine("      ,[profile]");
                Sql.AppendLine("      ,[change_install]");
                Sql.AppendLine("      ,[unique_name]");
                Sql.AppendLine("      ,[familiar_name]");
                Sql.AppendLine("  FROM [ts_character_list]");
                Sql.AppendLine("  WHERE ");
                Sql.AppendLine("       [entry_no] = " + Entry);

                dba.Fill(Sql.ToString(), CharacterBase);

                if (CharacterBase.Count > 0)
                {
                    CharacterDataEntity.ts_character_listRow CharaBaseRow = CharacterBase[0];

                    EntryNo = CharaBaseRow.entry_no;
                    ContinueNoCount = CharaBaseRow.continue_cnt;
                    ContinueBonus = CharaBaseRow.continue_bonus;
                    AccountStatus = CharaBaseRow.account_status;
                    NewPlayRegistUpdate = CharaBaseRow.new_play;
                    LastUpdate = CharaBaseRow.last_update;
                    IsNewPlayer = CharaBaseRow.new_gamer;
                    CharacterName = CharaBaseRow.character_name;
                    ImageURL = CharaBaseRow.image_url;
                    ImageWidthSize = CharaBaseRow.image_width;
                    ImageHeightSize = CharaBaseRow.image_height;
                    ImageLinkURL = CharaBaseRow.image_link_url;
                    ImageCopyright = CharaBaseRow.image_copyright;
                    NickName = CharaBaseRow.nick_name;
                    _race = CharaBaseRow.race_id;
                    _guardian = CharaBaseRow.guardian_id;
                    _nation = CharaBaseRow.nation_id;
                    HaveMoney = CharaBaseRow.have_money;
                    BlazeChip = CharaBaseRow.blaze_chip;
                    Age = CharaBaseRow.age;
                    _sex = CharaBaseRow.sex;
                    Height = CharaBaseRow.height;
                    Weight = CharaBaseRow.weight;
                    _maxHaveItem = CharaBaseRow.max_item;
                    MaxBazzerItem = CharaBaseRow.max_bazzeritem;
                    Profile = CharaBaseRow.profile;
                    IsInstallClassChanging = CharaBaseRow.change_install;
                    UniqueName = CharaBaseRow.unique_name;
                    FamiliarName = CharaBaseRow.familiar_name;
                }

                #endregion

                #region キャラクターインストールクラス情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("install_id, ");
                Sql.AppendLine("[level], ");
                Sql.AppendLine("exp");
                Sql.AppendLine("FROM ts_character_install_level");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), InstallClassList);
                #endregion

                #region キャラクターバトル情報読み込み
                CharacterDataEntity.ts_character_battle_abilityDataTable CharacterBattle = new CharacterDataEntity.ts_character_battle_abilityDataTable();

                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("entry_no, ");
                Sql.AppendLine("install, ");
                Sql.AppendLine("second_install, ");
                Sql.AppendLine("formation, ");
                Sql.AppendLine("[level], ");
                Sql.AppendLine("exp, ");
                Sql.AppendLine("hp, ");
                Sql.AppendLine("mp, ");
                Sql.AppendLine("exp_unit, ");
                Sql.AppendLine("levelup_point ");
                Sql.AppendLine("FROM ts_character_battle_ability");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), CharacterBattle);
                if (CharacterBattle.Rows.Count > 0)
                {
                    CharacterDataEntity.ts_character_battle_abilityRow CharaBattleRow = CharacterBattle[0];

                    IntallClassID = CharaBattleRow.install;
                    SecondryIntallClassID = CharaBattleRow.second_install;
                    Formation = CharaBattleRow.formation;
                    _level = CharaBattleRow.level;
                    _exp = CharaBattleRow.exp;

                    // 能力の設定
                    InstallDataEntity.mt_install_class_listRow InstallRow = LibInstall.GetInstallRow(IntallClassID);

                    int AbBaseLevel = (InstallClassLevel + Level) / 2;

                    _max_hp = (int)LibRankData.GetRankToHP(InstallRow.up_hp, AbBaseLevel);
                    _max_mp = (int)LibRankData.GetRankToMP(InstallRow.up_mp, AbBaseLevel);
                    _str = (int)LibRankData.GetRankToSTR(InstallRow.up_str, AbBaseLevel);
                    _agi = (int)LibRankData.GetRankToSPD(InstallRow.up_agi, AbBaseLevel);
                    _mag = (int)LibRankData.GetRankToMAG(InstallRow.up_mag, AbBaseLevel);
                    _unq = (int)LibRankData.GetRankToVIT(InstallRow.up_unq, AbBaseLevel);

                    _now_hp = (int)CharaBattleRow.hp;
                    _now_mp = (int)CharaBattleRow.mp;

                    GetExp = CharaBattleRow.exp_unit;
                    LevelUpPoint = CharaBattleRow.levelup_point;
                }

                #endregion

                #region キャラクター行動情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("action_no, ");
                Sql.AppendLine("action_target, ");
                Sql.AppendLine("action, ");
                Sql.AppendLine("perks_id ");
                Sql.AppendLine("FROM ts_character_action");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);
                Sql.AppendLine("ORDER BY action_no ");

                dba.Fill(Sql.ToString(), ActionList);
                #endregion

                #region キャラクター所持アイテム読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("0 as drop_type, ");
                Sql.AppendLine("0 as get_synx, ");
                Sql.AppendLine("box_type, ");
                Sql.AppendLine("have_no, ");
                Sql.AppendLine("it_num, ");
                Sql.AppendLine("it_box_count, ");
                Sql.AppendLine("it_box_baz_count, ");
                Sql.AppendLine("created, ");
                Sql.AppendLine("equip_spot, ");
                Sql.AppendLine("[new], ");
                Sql.AppendLine("it_name, ");
                Sql.AppendLine("it_comment, ");
                Sql.AppendLine("it_effect, ");
                Sql.AppendLine("it_price, ");
                Sql.AppendLine("it_seller ");
                Sql.AppendLine("FROM ts_character_have_item");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), HaveItem);

                #endregion

                #region キャラクター所持スキル読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("sk_num, ");
                Sql.AppendLine("[new], ");
                Sql.AppendLine("sc_flg ");
                Sql.AppendLine("FROM ts_character_have_skill");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), HaveSkill);
                #endregion

                #region キャラクターレベルアップ情報読み込み
                CharacterDataEntity.ts_character_levelup_abilityDataTable LevelUpTable = new CharacterDataEntity.ts_character_levelup_abilityDataTable();
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("entry_no, ");
                Sql.AppendLine("hp, ");
                Sql.AppendLine("mp, ");
                Sql.AppendLine("str, ");
                Sql.AppendLine("agi, ");
                Sql.AppendLine("mag, ");
                Sql.AppendLine("unq");
                Sql.AppendLine("FROM ts_character_levelup_ability");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), LevelUpTable);
                if (LevelUpTable.Count > 0)
                {
                    CharacterDataEntity.ts_character_levelup_abilityRow LevelUpRow = LevelUpTable[0];

                    LevelUpHP = LevelUpRow.hp;
                    LevelUpMP = LevelUpRow.mp;
                    LevelUpSTR = LevelUpRow.str;
                    LevelUpAGI = LevelUpRow.agi;
                    LevelUpMAG = LevelUpRow.mag;
                    LevelUpUNQ = LevelUpRow.unq;
                }

                #endregion

                #region キャラクターフラグ情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("flag_id, ");
                Sql.AppendLine("flag_value");
                Sql.AppendLine("FROM ts_character_event_flag");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), EventFlag);
                #endregion

                #region キャラクタークエスト情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("quest_id, ");
                Sql.AppendLine("clear_fg, ");
                Sql.AppendLine("quest_step ");
                Sql.AppendLine("FROM ts_character_quest");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), QuestList);
                #endregion

                #region キャラクターセリフ情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("serif_no, ");
                Sql.AppendLine("situation, ");
                Sql.AppendLine("perks_id, ");
                Sql.AppendLine("serif_text");
                Sql.AppendLine("FROM ts_character_serif");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), SerifList);
                #endregion

                #region キャラクター貴重品情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("key_item_id,");
                Sql.AppendLine("[new] ");
                Sql.AppendLine("FROM ts_character_have_key_item");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), KeyItemList);
                #endregion

                #region キャラクターアイコン情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT");
                Sql.AppendLine("icon_id, ");
                Sql.AppendLine("icon_url, ");
                Sql.AppendLine("icon_copyright");
                Sql.AppendLine("FROM ts_character_icon");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), IconList);
                #endregion

                #region キャラクターレコード情報読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT [entry_no]");
                Sql.AppendLine("      ,[party_message]");
                Sql.AppendLine("      ,[private_message]");
                Sql.AppendLine("      ,[list_message]");
                Sql.AppendLine("      ,[party_join]");
                Sql.AppendLine("      ,[alliance_join]");
                Sql.AppendLine("      ,[battle]");
                Sql.AppendLine("      ,[death]");
                Sql.AppendLine("      ,[busterd]");
                Sql.AppendLine("      ,[max_atk_damage]");
                Sql.AppendLine("      ,[max_def_damage]");
                Sql.AppendLine("      ,[atk_damage_total]");
                Sql.AppendLine("      ,[dfe_damage_total]");
                Sql.AppendLine("      ,[heal_total]");
                Sql.AppendLine("      ,[continue_total]");
                Sql.AppendLine("      ,[create_item_total]");
                Sql.AppendLine("  FROM [ts_character_record]");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), Record);
                #endregion

                #region キャラクター移動可能マーク読み込み
                Sql = new StringBuilder();
                Sql.AppendLine("SELECT [entry_no]");
                Sql.AppendLine("      ,[mark_id]");
                Sql.AppendLine("      ,[instance]");
                Sql.AppendLine("  FROM [ts_character_moving_mark]");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + Entry);

                dba.Fill(Sql.ToString(), MovingOKMarks);
                #endregion

                Category = Status.Category.Human;

                StatusEffect.LoadDefaultStatus(Entry);
            }
        }

        /// <summary>
        /// 削除処理実行
        /// </summary>
        public void Delete(int UpdateCnt)
        {
            // パーティ登録の削除

            int PartyNo = LibParty.GetPartyNo(EntryNo);
            int PartyMemberCount = LibParty.PartyMemberCount(PartyNo);
            if (PartyMemberCount == 1)
            {
                LibParty.DeleteParty(PartyNo);
            }
            bool IsReader = LibParty.GetPartyReader(PartyNo) == EntryNo;
            LibParty.DeleteBelongParty(EntryNo);
            if (PartyMemberCount > 1 && IsReader)
            {
                // リーダーの再設定
                LibParty.SetReaderByRandom(PartyNo);
            }
            LibParty.Update();

            StringBuilder Sql = new StringBuilder();

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                #region キャラクター基本情報
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_list");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクターパーティ情報
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM [ts_character_belong_party]");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクターバトル情報
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_battle_ability");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクター行動情報
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_action");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクター所持アイテム
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_have_item");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクター所持スキル
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_have_skill");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクターレベルアップ情報
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_levelup_ability");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクターフラグ情報
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_event_flag");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクターインストールクラス情報
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_install_level");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクタークエスト情報
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_quest");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクターセリフ情報
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_serif");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクター貴重品情報
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_have_key_item");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクターアイコン情報
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_icon");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクターレコード
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_record");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region キャラクター移動可能マーク
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_character_moving_mark");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region TABLE <ts_continue_main>
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_continue_main");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region TABLE <ts_continue_message>
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_continue_message");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region TABLE <ts_continue_trade>
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_continue_trade");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region TABLE <ts_continue_battle_action>
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_continue_battle_action");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region TABLE <ts_continue_create_item>
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_continue_create_item");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region TABLE <ts_continue_serif>
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_continue_serif");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region TABLE <ts_continue_profile>
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_continue_profile");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region TABLE <ts_continue_battle_preparation>
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_continue_battle_preparation");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region TABLE <ts_continue_shopping>
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE");
                Sql.AppendLine("FROM ts_continue_shopping");
                Sql.AppendLine("WHERE ");
                Sql.AppendLine("entry_no = " + EntryNo);

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                #region DELETE PLAYER
                Sql = new StringBuilder();
                Sql.AppendLine("DELETE FROM ts_delete_players");
                Sql.AppendLine("WHERE entry_no=" + EntryNo + " and update_cnt=" + UpdateCnt + "");

                dba.ExecuteNonQuery(Sql.ToString());

                Sql = new StringBuilder();
                Sql.AppendLine("INSERT INTO ts_delete_players");
                Sql.AppendLine("values (" + EntryNo + "," + UpdateCnt + ",GETDATE() )");

                dba.ExecuteNonQuery(Sql.ToString());
                #endregion

                dba.Commit();
            }
            catch (Exception ex)
            {
                dba.Rollback();
                throw new Exception("データベース更新に失敗しました。");
            }
            finally
            {
                dba.Close();
            }

            IsValid = false;
        }

        /// <summary>
        /// レベルアップによる能力再計算
        /// </summary>
        public void RefreshLevelUpAbility()
        {
            // 能力の設定
            InstallDataEntity.mt_install_class_listRow InstallRow = LibInstall.GetInstallRow(IntallClassID);

            _max_hp = (int)LibRankData.GetRankToHP(InstallRow.up_hp, InstallClassLevel);
            _max_mp = (int)LibRankData.GetRankToMP(InstallRow.up_mp, InstallClassLevel);
            _str = (int)LibRankData.GetRankToSTR(InstallRow.up_str, InstallClassLevel);
            _agi = (int)LibRankData.GetRankToSPD(InstallRow.up_agi, InstallClassLevel);
            _mag = (int)LibRankData.GetRankToMAG(InstallRow.up_mag, InstallClassLevel);
            _unq = (int)LibRankData.GetRankToVIT(InstallRow.up_unq, InstallClassLevel);
        }
    }
}
