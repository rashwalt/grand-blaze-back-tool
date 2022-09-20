using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.SpecialEntity;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        /// <summary>
        /// 攻撃ダメージ量
        /// </summary>
        /// <param name="Mine">加攻撃者</param>
        /// <param name="Target">被攻撃者</param>
        /// <param name="ActionArts">攻撃行動種別</param>
        /// <param name="Drain">吸収量</param>
        /// <param name="IsCritical">クリティカル発生？</param>
        /// <param name="SacrificeHP">サクリファイス</param>
        /// <param name="ElementalDamageRatingType">属性相性種類</param>
        /// <param name="DamageType">ダメージタイプ</param>
        /// <returns>ダメージ量</returns>
        private int BattleDamage(LibUnitBase Mine, LibUnitBase Target, LibActionType ActionArts, ref int Drain, ref bool IsCritical, int SacrificeHP, ref int ElementalDamageRatingType, int DamageType, int Turn,bool IsVirtual)
        {
            int Attack = 0;
            int[] AttackAbility = { Status.BasicAbility.STR, Status.BasicAbility.STR };
            int DfeType = Status.DeffenceType.DFE;
            int DamageRating = 256;
            int PlusScore = 1;
            bool IsBreaker = false;
            bool IsDoubleHand = false;
            bool IsDisDefence = false;
            bool IsRangeWeapon = false;
            int DoubleHandType = 0;
            bool IsBaseAttackDamage = false;

            #region 攻撃力
            ItemTypeEntity.mt_item_typeRow MainAtkItemTypeRow = null;

            {
                if (Mine.GetType() == typeof(LibPlayer) || Mine.GetType() == typeof(LibGuest))
                {
                    MainAtkItemTypeRow = LibItemType.GetTypeRow(ActionArts.ItemType);
                }

                if (MainAtkItemTypeRow != null)
                {
                    AttackAbility[0] = MainAtkItemTypeRow.atk_ability1;
                    AttackAbility[1] = MainAtkItemTypeRow.atk_ability2;
                    DfeType = MainAtkItemTypeRow.dfe_bt_type;
                    DamageRating = MainAtkItemTypeRow.rating;
                    PlusScore = MainAtkItemTypeRow.plus_score;
                    IsBreaker = MainAtkItemTypeRow.random_damage;
                    IsDisDefence = MainAtkItemTypeRow.dfe_dis;
                    IsRangeWeapon = MainAtkItemTypeRow.range_weapon;
                }
            }

            if (Mine.EffectList.FindByeffect_id(4500) != null)
            {
                // 銃特性
                IsDisDefence = true;
            }

            switch (ActionArts.ActionBase)
            {
                case Status.ActionBaseType.MainAttack:
                    {
                        Attack = Mine.ATK;
                    }
                    break;
                case Status.ActionBaseType.SubAttack:
                    {
                        Attack = Mine.ATKSub;
                    }
                    break;
                case Status.ActionBaseType.MindAttack:
                    {
                        AttackAbility[0] = Status.BasicAbility.MAG;
                        AttackAbility[1] = Status.BasicAbility.UNQ;
                        Attack = Mine.ATK;
                        DfeType = Status.DeffenceType.MGR;
                        DamageRating = 256;
                        IsBreaker = false;
                        IsDisDefence = false;
                        IsBaseAttackDamage = true;
                    }
                    break;
                case Status.ActionBaseType.SorscialAttack:
                    {
                        AttackAbility[0] = Status.BasicAbility.MAG;
                        AttackAbility[1] = Status.BasicAbility.MAG;
                        Attack = Mine.ATK;
                        DfeType = Status.DeffenceType.MGR;
                        DamageRating = 256;
                        IsBreaker = false;
                        IsDisDefence = false;
                        IsBaseAttackDamage = true;
                    }
                    break;
                case Status.ActionBaseType.MagicSword:
                    {
                        AttackAbility[0] = Status.BasicAbility.UNQ;
                        AttackAbility[1] = Status.BasicAbility.MAG;
                        Attack = Mine.ATK;
                        DfeType = Status.DeffenceType.MGR;
                        DamageRating = 256;
                        IsBreaker = false;
                        IsDisDefence = false;
                        IsBaseAttackDamage = true;
                    }
                    break;
                case Status.ActionBaseType.BlessAttack:
                    {
                        Attack = Mine.ATK;
                        IsDisDefence = false;
                        IsBreaker = false;
                    }
                    break;
                case Status.ActionBaseType.ItemArts:
                    {
                        IsDisDefence = false;
                        IsBreaker = false;
                    }
                    break;
            }

            if (!ActionArts.IsNormalAttack)
            {
                if (ActionArts.Attack > 0)
                {
                    Attack = ActionArts.Attack;
                    IsDisDefence = false;
                    IsBreaker = false;
                    DamageRating = 256;
                }

                if (DamageType == Status.DamageType.PhysicalDamage || DamageType == Status.DamageType.PhysicalDrain)
                {
                    DfeType = Status.DeffenceType.DFE;
                }
                else if (DamageType == Status.DamageType.MagicalDamage || DamageType == Status.DamageType.MagicalDrain)
                {
                    DfeType = Status.DeffenceType.MGR;
                }
            }

            // 攻撃側の二刀流判定
            ItemTypeEntity.mt_item_typeRow SubAtkItemTypeRow = null;
            int SubATK = 0;
            if (Mine.GetType() == typeof(LibPlayer))
            {
                LibPlayer Player = (LibPlayer)Mine;
                CommonItemEntity.item_listRow SubAtkItemRow = Player.GetHaveItemEquiped(Status.EquipSpot.Sub);
                if (SubAtkItemRow != null)
                {
                    SubAtkItemTypeRow = LibItemType.GetTypeRow(SubAtkItemRow.it_type);
                }
                SubATK = Player.ATKSub;
            }
            else if (Mine.GetType() == typeof(LibGuest))
            {
                int ItemID = 0;
                LibGuest Guest = (LibGuest)Mine;
                ItemID = Guest.HaveItemS[0].equip_sub;
                SubAtkItemTypeRow = LibItemType.GetTypeRow(LibItem.GetType(ItemID, false));
                SubATK = Guest.ATKSub;
            }

            if (SubAtkItemTypeRow != null && SubAtkItemTypeRow.equip_spot == Status.EquipSpot.Main)
            {
                // 二刀流攻撃
                IsDoubleHand = true;

                // 二刀流タイプ判定
                if (Mine.EffectList.FindByeffect_id(892) != null)
                {
                    // 二刀流
                    DoubleHandType = 2;
                }
                else if (Mine.EffectList.FindByeffect_id(891) != null)
                {
                    // 両手利き
                    DoubleHandType = 1;
                }
            }

            // 二刀流の場合の攻撃力補正(物理アーツの場合のみ)
            if (IsDoubleHand &&
                !ActionArts.IsNormalAttack &&
                ActionArts.ActionBase == Status.ActionBaseType.MainAttack &&
                SubATK > 0)
            {
                if (DoubleHandType == 2)
                {
                    // 二刀流
                    Attack = (int)((decimal)Attack * 0.725m) + (int)((decimal)SubATK * 0.725m);
                }
                else if (DoubleHandType == 1)
                {
                    // 両手利き
                    Attack = (int)((decimal)Attack * 0.625m) + (int)((decimal)SubATK * 0.625m);
                }
            }

            // 二刀流ペナルティ(通常攻撃)
            if (IsDoubleHand &&
                ActionArts.IsNormalAttack)
            {
                if (DoubleHandType == 2)
                {
                    // 二刀流
                    Attack = (int)((decimal)Attack * 0.725m);
                }
                else if (DoubleHandType == 1)
                {
                    // 両手利き
                    Attack = (int)((decimal)Attack * 0.625m);
                }
            }

            // 魔法攻撃力アップ系の効果
            if (IsBaseAttackDamage)
            {
                {
                    EffectListEntity.effect_listRow row = Mine.EffectList.FindByeffect_id(752);

                    if (row != null)
                    {
                        Attack += (int)row.rank;
                    }
                }

                // 食事効果によるアップ
                {
                    EffectListEntity.effect_listRow row = Mine.EffectList.FindByeffect_id(1302);
                    if (row != null)
                    {
                        decimal PercentPlus = row.rank;

                        int PlusPoint = (int)((decimal)Attack * PercentPlus / 100m);

                        EffectListEntity.effect_listRow addinRow = Mine.EffectList.FindByeffect_id(1312);
                        if (addinRow != null && PlusPoint > addinRow.rank)
                        {
                            PlusPoint = (int)addinRow.rank;
                        }

                        Attack += PlusPoint;
                    }
                }
            }
            #endregion

            int Deffence = 0;

            #region 防御力
            switch (DfeType)
            {
                case Status.DeffenceType.DFE:
                    Deffence = Target.DFE;
                    break;
                case Status.DeffenceType.MGR:
                    Deffence = Target.MGR;
                    break;
            }

            // 防御力カット修正
            switch (DfeType)
            {
                case Status.DeffenceType.DFE:
                    {
                        EffectListEntity.effect_listRow EffectRow = ActionArts.EffectList.FindByeffect_id(710);
                        if (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis())
                        {
                            Deffence -= (int)(Deffence * EffectRow.rank / 100m);
                        }
                    }
                    break;
                case Status.DeffenceType.MGR:
                    {
                        EffectListEntity.effect_listRow EffectRow = ActionArts.EffectList.FindByeffect_id(711);
                        if (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis())
                        {
                            Deffence -= (int)(Deffence * EffectRow.rank / 100m);
                        }
                    }
                    break;
            }

            // 回復の場合、防御力は0として計算する。
            if (DamageType == Status.DamageType.Heal)
            {
                Deffence = 0;
            }
            #endregion

            bool CriticalHit = false;

            // クリティカル 発生率判定
            int CriticalProb = 0;
            int CriticalType = Status.CriticalType.Critical;

            switch (ActionArts.ActionBase)
            {
                case Status.ActionBaseType.MainAttack:
                    {
                        CriticalProb = Mine.MainWeapon.Critical;
                    }
                    break;
                case Status.ActionBaseType.SubAttack:
                    {
                        CriticalProb = Mine.SubWeapon.Critical;
                    }
                    break;
                case Status.ActionBaseType.MindAttack:
                case Status.ActionBaseType.SorscialAttack:
                case Status.ActionBaseType.MagicSword:
                    {
                        CriticalProb = 0;
                    }
                    break;
                case Status.ActionBaseType.BlessAttack:
                    {
                        CriticalProb = 0;
                    }
                    break;
                case Status.ActionBaseType.ItemArts:
                    {
                        CriticalProb = 0;
                    }
                    break;
            }

            ItemTypeEntity.mt_item_type_sub_categoryRow atkSubTypeRow = null;

            // クリティカルタイプ判定
            if (Mine.GetType() == typeof(LibPlayer) || Mine.GetType() == typeof(LibGuest))
            {
                atkSubTypeRow = LibItemType.GetSubCategoryRow(ActionArts.ItemSubType);
            }

            if (ActionArts.IsNormalAttack)
            {
                if (atkSubTypeRow != null)
                {
                    CriticalType = atkSubTypeRow.critical_type;
                }
            }
            else
            {
                CriticalType = ActionArts.CriticalType;
            }

            if (CriticalType == Status.CriticalType.Critical)
            {
                if (Mine.StatusEffect.Check(94))
                {
                    CriticalType = Status.CriticalType.MultiAct;
                }
            }

            // アーツのクリティカル率設定
            if (!ActionArts.IsNormalAttack)
            {
                CriticalProb = ActionArts.Critical;
            }

            if (Mine.GetType() == typeof(LibMonster))
            {
                // モンスターの場合の特別ルール
                if (((LibMonster)Mine).MultiAttackMaxCount == 1 && Mine.MainWeapon.Critical > 0)
                {
                    CriticalType = Status.CriticalType.Critical;
                }
                else
                {
                    CriticalType = Status.CriticalType.MultiAct;
                }
            }

            // クリティカル率アップ
            if (CriticalType == Status.CriticalType.Critical)
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(815);
                if (EffectRow != null)
                {
                    CriticalProb += (int)EffectRow.rank;
                }

                if (Mine.StatusEffect.Check(265))
                {
                    CriticalProb += 30;
                }

                if (Mine.StatusEffect.Check(93))
                {
                    CriticalProb = 100;
                }

                EffectListEntity.effect_listRow FineasEffectRow = Mine.EffectList.FindByeffect_id(2120);
                if (FineasEffectRow != null)
                {
                    // フィネス
                    CriticalProb += (int)FineasEffectRow.rank;
                }
            }

            if (Mine.StatusEffect.Check(258) && (ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack))
            {
                // 夢幻闘武
                CriticalProb = 200;
                CriticalType = Status.CriticalType.Critical;
            }

            // ソーサルクリティカル
            if (Mine.EffectList.FindByeffect_id(867) != null &&
                ActionArts.AttackType == Status.AttackType.Mystic &&
                !ActionArts.IsNormalAttack)
            {
                CriticalProb = 5;
                CriticalType = Status.CriticalType.Critical;
            }

            // リベンジクリティカル
            if (CriticalType == Status.CriticalType.Critical &&
                Mine.IsRevengeCritical)
            {
                CriticalProb = 200;
            }

            if (CriticalType == Status.CriticalType.Critical)
            {
                if (CriticalProb > LibInteger.GetRandBasis())
                {
                    // クリティカル発生
                    CriticalHit = true;
                    Target.IsRevengeCritical = true;

                    if (IsCritical == false)
                    {
                        IsCritical = true;
                    }
                }
            }

            // ダメージ計算開始
            int[] AttackAbilityData = new int[2];

            // 関連能力の抽出
            for (int i = 0; i < 2; i++)
            {
                switch (AttackAbility[i])
                {
                    case Status.BasicAbility.STR:
                        AttackAbilityData[i] = Mine.STR;
                        break;
                    case Status.BasicAbility.AGI:
                        AttackAbilityData[i] = Mine.AGI;
                        break;
                    case Status.BasicAbility.MAG:
                        AttackAbilityData[i] = Mine.MAG;
                        break;
                    case Status.BasicAbility.UNQ:
                        AttackAbilityData[i] = Mine.UNQ;
                        break;
                }
            }

            decimal ArtsPower = ActionArts.Power;

            // コンビネーション
            if (!ActionArts.IsNormalAttack &&
                ((decimal)Turn / 2m) == 0m &&
                Mine.EffectList.FindByeffect_id(2131) != null)
            {
                ArtsPower += 0.1m;
            }

            decimal DamageBasicRate = 1;

            // アーツの場合の特別処置
            if (!ActionArts.IsNormalAttack)
            {
                DamageBasicRate = (decimal)(ArtsPower * AttackAbilityData[0] * (Mine.Level + AttackAbilityData[1])) / (decimal)DamageRating + ActionArts.PlusScore;
            }
            else
            {
                DamageBasicRate = (decimal)(AttackAbilityData[0] * (Mine.Level + AttackAbilityData[1])) / (decimal)DamageRating + PlusScore;
            }

            decimal DamageMin = ((decimal)(Attack - Deffence) * DamageBasicRate);
            if (IsBreaker) { DamageMin = 0; }
            decimal DamageMax = (((decimal)Attack * 9m / 8m - (decimal)Deffence) * DamageBasicRate);
            if (DamageMin < 0)
            {
                DamageMin = 0;
            }
            if (DamageMax < 0)
            {
                DamageMax = 0;
            }

            // 防御無視
            if (IsDisDefence)
            {
                DamageMin = (decimal)Attack * (decimal)Attack;
                DamageMax = (decimal)Attack * (decimal)Attack * 81m / 64m;

                if (!ActionArts.IsNormalAttack)
                {
                    DamageMin = ArtsPower * DamageMin;
                    DamageMax = ArtsPower * DamageMax;
                }
            }

            // さらに変更
            if (!ActionArts.IsNormalAttack)
            {
                DamageMin *= ActionArts.DamageRate;
                DamageMax *= ActionArts.DamageRate;
            }

            // ブレスのダメージ算出
            if (ActionArts.ActionBase == Status.ActionBaseType.BlessAttack)
            {
                DamageMax = (decimal)Mine.HPNow / 4m;

                // ブレスダメージ上限
                EffectListEntity.effect_listRow EffectRow = ActionArts.EffectList.FindByeffect_id(735);
                if (EffectRow != null)
                {
                    if (EffectRow.rank < DamageMax)
                    {
                        DamageMax = EffectRow.rank;
                    }
                }

                DamageMin = DamageMax / 2m;
            }

            // アイテムのダメージ算出
            if (ActionArts.ActionBase == Status.ActionBaseType.ItemArts)
            {
                DamageMax = ActionArts.PlusScore;

                if (Mine.StatusEffect.Check(97) &&
                    ActionArts.AttackType == Status.AttackType.Item &&
                    DamageType == Status.DamageType.Heal &&
                    ActionArts.PlusScore > 0 &&
                    ActionArts.TargetArea == Status.TargetArea.Only)
                {
                    DamageMax = DamageMax / 2m;
                }


                DamageMin = DamageMax / 3m;
                IsDisDefence = false;
            }

            // サクリファイス
            if (SacrificeHP > 0)
            {
                DamageMax = SacrificeHP;

                DamageMin = DamageMax;
            }

            // 乱数処理
            int Damage = LibInteger.GetRand((int)DamageMin, (int)DamageMax);

            // ランダムでダメージアップ
            if (ActionArts.EffectList.FindByeffect_id(1065) != null)
            {
                decimal DamageRateRandom = (decimal)LibInteger.GetRandMax(50, 200) / 100m;
                Damage = (int)((decimal)Damage * DamageRateRandom);
            }

            #region 属性による影響
            decimal ElementalDamageRate = 1m;

            // 防御側属性効果
            foreach (string Elemental in LibConst.ElementalList)
            {
                // 攻撃側の属性判定
                int AttackElement = ActionArts.Elemental(Elemental);

                // 属性変更による効果
                BattleCommon.ElementalSpecial(ref AttackElement, Mine, ActionArts, Elemental);

                if (AttackElement <= 0)
                {
                    // 属性がない場合はスキップ
                    continue;
                }

                {
                    int DefenceElement = 0;

                    switch (Elemental)
                    {
                        case Status.Elemental.Fire:
                            DefenceElement = Target.DefenceElemental.Fire;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 1)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                        case Status.Elemental.Freeze:
                            DefenceElement = Target.DefenceElemental.Freeze;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 2)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                        case Status.Elemental.Air:
                            DefenceElement = Target.DefenceElemental.Air;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 3)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                        case Status.Elemental.Earth:
                            DefenceElement = Target.DefenceElemental.Earth;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 4)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                        case Status.Elemental.Water:
                            DefenceElement = Target.DefenceElemental.Water;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 5)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                        case Status.Elemental.Thunder:
                            DefenceElement = Target.DefenceElemental.Thunder;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 6)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                        case Status.Elemental.Holy:
                            DefenceElement = Target.DefenceElemental.Holy;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 7)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                        case Status.Elemental.Dark:
                            DefenceElement = Target.DefenceElemental.Dark;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 8)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                        case Status.Elemental.Slash:
                            DefenceElement = Target.DefenceElemental.Slash;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 9)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                        case Status.Elemental.Pierce:
                            DefenceElement = Target.DefenceElemental.Pierce;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 10)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                        case Status.Elemental.Strike:
                            DefenceElement = Target.DefenceElemental.Strike;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 11)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                        case Status.Elemental.Break:
                            DefenceElement = Target.DefenceElemental.Break;
                            if (Target.StatusEffect.Check(87) && (int)Target.StatusEffect.GetRank(87) == 12)
                            {
                                if (DefenceElement < 100)
                                {
                                    DefenceElement = 100;
                                }
                                Target.StatusEffect.Delete(87);
                            }
                            break;
                    }

                    if (DefenceElement < 0)
                    {
                        // 弱点効果
                        if (ElementalDamageRate > 0)
                        {
                            if (Target.StatusEffect.Check(25))
                            {
                                ElementalDamageRate *= 2m;
                            }
                            else
                            {
                                ElementalDamageRate *= 1.5m;
                            }
                        }

                        ElementalDamageRatingType = Math.Max(ElementalDamageRatingType, Status.ElementalRatingType.Weak);
                    }
                    else if (DefenceElement > 0 && DefenceElement < 100)
                    {
                        // 半減効果
                        if (ElementalDamageRate > 0)
                        {
                            ElementalDamageRate *= 0.5m;
                            ElementalDamageRatingType = Math.Max(ElementalDamageRatingType, Status.ElementalRatingType.Regist);
                        }
                    }
                    else if (DefenceElement == 100)
                    {
                        // 無効効果
                        ElementalDamageRate = 0;
                        ElementalDamageRatingType = Status.ElementalRatingType.Block;
                    }
                    else if (DefenceElement > 100)
                    {
                        // 吸収効果
                        ElementalDamageRate = -1;
                        ElementalDamageRatingType = Status.ElementalRatingType.Drain;
                    }
                }
            }

            // 属性強化
            foreach (string Elemental in LibConst.ElementalList)
            {
                // 攻撃側の属性判定
                int AttackElement = ActionArts.Elemental(Elemental);

                // 属性変更による効果
                BattleCommon.ElementalSpecial(ref AttackElement, Mine, ActionArts, Elemental);

                if (AttackElement <= 0)
                {
                    // 属性がない場合はスキップ
                    continue;
                }

                switch (Elemental)
                {
                    case Status.Elemental.Fire:
                        if (ActionArts.EffectList.FindByeffect_id(990) != null || Mine.EffectList.FindByeffect_id(990) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                    case Status.Elemental.Freeze:
                        if (ActionArts.EffectList.FindByeffect_id(991) != null || Mine.EffectList.FindByeffect_id(991) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                    case Status.Elemental.Air:
                        if (ActionArts.EffectList.FindByeffect_id(992) != null || Mine.EffectList.FindByeffect_id(992) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                    case Status.Elemental.Earth:
                        if (ActionArts.EffectList.FindByeffect_id(993) != null || Mine.EffectList.FindByeffect_id(993) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                    case Status.Elemental.Water:
                        if (ActionArts.EffectList.FindByeffect_id(994) != null || Mine.EffectList.FindByeffect_id(994) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                    case Status.Elemental.Thunder:
                        if (ActionArts.EffectList.FindByeffect_id(995) != null || Mine.EffectList.FindByeffect_id(995) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                    case Status.Elemental.Holy:
                        if (ActionArts.EffectList.FindByeffect_id(996) != null || Mine.EffectList.FindByeffect_id(996) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                    case Status.Elemental.Dark:
                        if (ActionArts.EffectList.FindByeffect_id(997) != null || Mine.EffectList.FindByeffect_id(997) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                    case Status.Elemental.Slash:
                        if (ActionArts.EffectList.FindByeffect_id(998) != null || Mine.EffectList.FindByeffect_id(998) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                    case Status.Elemental.Pierce:
                        if (ActionArts.EffectList.FindByeffect_id(999) != null || Mine.EffectList.FindByeffect_id(999) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                    case Status.Elemental.Strike:
                        if (ActionArts.EffectList.FindByeffect_id(1000) != null || Mine.EffectList.FindByeffect_id(1000) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                    case Status.Elemental.Break:
                        if (ActionArts.EffectList.FindByeffect_id(1001) != null || Mine.EffectList.FindByeffect_id(1001) != null)
                        {
                            ElementalDamageRate *= 1.5m;
                        }
                        break;
                }
            }

            if (ElementalDamageRate < 0)
            {
                ElementalDamageRate = -1;
            }

            Damage = (int)((decimal)Damage * ElementalDamageRate);
            #endregion

            // 防御
            if (DamageType != Status.DamageType.Heal &&
                Target.StatusEffect.Check(65))
            {
                // フューリー
                if (Mine.EffectList.FindByeffect_id(2137) != null)
                {
                    Damage = (int)((decimal)Damage * 0.75m);
                }
                else
                {
                    // 防御状態ならダメージ半減
                    Damage = (int)((decimal)Damage * 0.5m);
                }
            }

            // 暴走ステータス効果
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                DamageType != Status.DamageType.Heal &&
                Target.StatusEffect.Check(14))
            {
                // ダメージ増加
                Damage = (int)((decimal)Damage * 1.5m);
            }

            // クリティカル効果
            if (CriticalHit)
            {
                Damage *= 2;
            }

            #region キラー効果

            if (DamageType != Status.DamageType.Heal)
            {
                switch (Target.Category)
                {
                    case Status.Category.Aquan:
                        #region アクアンキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1200) != null || ActionArts.EffectList.FindByeffect_id(1200) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1250) != null || ActionArts.EffectList.FindByeffect_id(1250) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Amolf:
                        #region アモルフキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1201) != null || ActionArts.EffectList.FindByeffect_id(1201) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1251) != null || ActionArts.EffectList.FindByeffect_id(1251) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Alcana:
                        #region アルカナキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1202) != null || ActionArts.EffectList.FindByeffect_id(1202) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1252) != null || ActionArts.EffectList.FindByeffect_id(1252) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Undead:
                        #region アンデッドキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1203) != null || ActionArts.EffectList.FindByeffect_id(1203) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1253) != null || ActionArts.EffectList.FindByeffect_id(1253) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Vermin:
                        #region ヴァーミンキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1204) != null || ActionArts.EffectList.FindByeffect_id(1204) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1254) != null || ActionArts.EffectList.FindByeffect_id(1254) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Element:
                        #region エレメントキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1205) != null || ActionArts.EffectList.FindByeffect_id(1205) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1255) != null || ActionArts.EffectList.FindByeffect_id(1255) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Angel:
                        #region エンジェルキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1206) != null || ActionArts.EffectList.FindByeffect_id(1206) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1256) != null || ActionArts.EffectList.FindByeffect_id(1256) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Nothingness:
                        #region ナッシングネスキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1207) != null || ActionArts.EffectList.FindByeffect_id(1207) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1257) != null || ActionArts.EffectList.FindByeffect_id(1257) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Demon:
                        #region デーモンキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1208) != null || ActionArts.EffectList.FindByeffect_id(1208) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1258) != null || ActionArts.EffectList.FindByeffect_id(1258) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Dragon:
                        #region ドラゴンキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1209) != null || ActionArts.EffectList.FindByeffect_id(1209) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        if (CriticalHit && (Mine.EffectList.FindByeffect_id(1259) != null || ActionArts.EffectList.FindByeffect_id(1259) != null))
                        {
                            Damage = (int)((decimal)Damage * 1.5m);
                        }
                        #endregion
                        break;
                    case Status.Category.Bird:
                        #region バードキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1210) != null || ActionArts.EffectList.FindByeffect_id(1210) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1260) != null || ActionArts.EffectList.FindByeffect_id(1260) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Bio:
                        #region バイオキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1211) != null || ActionArts.EffectList.FindByeffect_id(1211) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1261) != null || ActionArts.EffectList.FindByeffect_id(1261) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Beast:
                        #region ビーストキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1212) != null || ActionArts.EffectList.FindByeffect_id(1212) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1262) != null || ActionArts.EffectList.FindByeffect_id(1262) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Plantid:
                        #region プラントイドキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1213) != null || ActionArts.EffectList.FindByeffect_id(1213) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1263) != null || ActionArts.EffectList.FindByeffect_id(1263) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Machine:
                        #region マシンキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1214) != null || ActionArts.EffectList.FindByeffect_id(1214) != null ||
                                (ActionArts.IsNormalAttack && Mine.StatusEffect.Check(95) && Mine.StatusEffect.GetRank(95) == 28002))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1264) != null || ActionArts.EffectList.FindByeffect_id(1264) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Human:
                        #region マンイーター
                        {
                            if (Mine.EffectList.FindByeffect_id(1215) != null || ActionArts.EffectList.FindByeffect_id(1215) != null ||
                                (ActionArts.IsNormalAttack && Mine.StatusEffect.Check(95) && Mine.StatusEffect.GetRank(95) == 28000))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1265) != null || ActionArts.EffectList.FindByeffect_id(1265) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        #region アナトミー
                        {
                            if (Mine.EffectList.FindByeffect_id(2111) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.05m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.Rizard:
                        #region リザードキラー
                        {
                            if (Mine.EffectList.FindByeffect_id(1216) != null || ActionArts.EffectList.FindByeffect_id(1216) != null)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                            if (CriticalHit && (Mine.EffectList.FindByeffect_id(1266) != null || ActionArts.EffectList.FindByeffect_id(1266) != null))
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                    case Status.Category.DemiHuman:
                        #region デミヒューマンキラー
                        {
                            if (ActionArts.IsNormalAttack && Mine.StatusEffect.Check(95) && Mine.StatusEffect.GetRank(95) == 28006)
                            {
                                Damage = (int)((decimal)Damage * 1.5m);
                            }
                        }
                        #endregion
                        break;
                }
            }

            #endregion

            #region 効果アップ系
            // 魔法回復量アップ
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(860);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    ActionArts.AttackType == Status.AttackType.Mystic &&
                    DamageType == Status.DamageType.Heal)
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // 魔法攻撃アップ
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(861);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    ActionArts.AttackType == Status.AttackType.Mystic &&
                    DamageType == Status.DamageType.MagicalDamage)
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // アイテムアーツプラス
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(863);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    ActionArts.AttackType == Status.AttackType.Item)
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // 魔法剣効果アップ
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(864);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    ActionArts.AttackType == Status.AttackType.MagicSword)
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // 忍術効果アップ
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(865);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    ActionArts.ArtsCategory == LibSkillType.FindByName("忍術"))
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // レンジアサルト
            if (IsRangeWeapon)
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(866);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                   (ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                    ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                    (DamageType == Status.DamageType.PhysicalDamage ||
                    DamageType == Status.DamageType.MagicalDamage))
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // 爆弾ダメージアップ
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(1120);
                if (EffectRow != null &&
                    ActionArts.ArtsCategory == LibSkillType.FindByName("爆弾"))
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // 薬草効果アップ
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(1121);
                if (EffectRow != null &&
                    ActionArts.ArtsCategory == LibSkillType.FindByName("薬草"))
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // 魔符迅雷
            if (Mine.StatusEffect.Check(256) &&
                ActionArts.ArtsCategory == LibSkillType.FindByName("忍術"))
            {
                Damage = (int)((decimal)Damage * 1.5m);
            }

            if (ActionArts.AttackType == Status.AttackType.Song)
            {
                // 効果アップ
                EffectListEntity.effect_listRow EffectSongRow = Mine.EffectList.FindByeffect_id(1190);
                if (EffectSongRow != null)
                {
                    Damage += (int)((decimal)Damage * (1 + EffectSongRow.rank) * 0.05m);
                }
            }
            if (ActionArts.AttackType == Status.AttackType.Dance)
            {
                // 効果アップ
                EffectListEntity.effect_listRow EffectDanceRow = Mine.EffectList.FindByeffect_id(1195);
                if (EffectDanceRow != null)
                {
                    Damage += (int)((decimal)Damage * (1 + EffectDanceRow.rank) * 0.05m);
                }
            }

            // フォースソウル
            if (Mine.StatusEffect.Check(257) &&
                (ActionArts.AttackType == Status.AttackType.Song ||
                ActionArts.AttackType == Status.AttackType.Dance))
            {
                Damage = (int)((decimal)Damage * 1.5m);
            }

            // 血の盟約
            if (Mine.StatusEffect.Check(69) &&
                (ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack))
            {
                int Blood = (int)((decimal)Mine.HPNow * 0.1m);

                if (Mine.HPNow > Blood)
                {
                    // HP消費
                    Mine.HPNow -= Blood;

                    Damage = (int)((decimal)Damage * 1.4m);
                }
            }

            // シノビノイン
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(2124);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    ActionArts.ArtsCategory == LibSkillType.FindByName("忍術"))
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // メタボリズム
            {
                EffectListEntity.effect_listRow EffectRow = Target.EffectList.FindByeffect_id(2127);
                if (EffectRow != null &&
                    ActionArts.DamageType == Status.DamageType.Heal &&
                    ActionArts.AttackType == Status.AttackType.Item)
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            #endregion

            // 魔法攻撃力アップ系
            if (IsBaseAttackDamage)
            {
                {
                    CharacterStatusListEntity.status_dataRow row = Mine.StatusEffect.Find(203);
                    if (row != null)
                    {
                        // 魔導のマドリガーレ
                        Damage += (int)((decimal)Damage * ((row.rank + 1m) * 0.05m));
                    }
                }

                {
                    CharacterStatusListEntity.status_dataRow row = Mine.StatusEffect.Find(232);
                    if (row != null)
                    {
                        // ダンス・アンフェイス
                        Damage -= (int)((decimal)Damage * ((row.rank + 1m) * 0.05m));
                    }
                }

                if (Mine.StatusEffect.Check(263))
                {
                    // オーバードライブ
                    Damage += (int)((decimal)Damage * 0.35m);
                }
            }

            // 気迫
            if (((ActionArts.ActionBase == Status.ActionBaseType.MainAttack && Mine.MainWeapon.AttackDamageType == Status.AttackType.Combat) ||
                (ActionArts.ActionBase == Status.ActionBaseType.SubAttack && Mine.SubWeapon.AttackDamageType == Status.AttackType.Combat)) &&
                Mine.StatusEffect.Check(63))
            {
                Damage += (int)((decimal)Damage * (decimal)Mine.StatusEffect.GetRank(63) / 100m);
                Mine.StatusEffect.Delete(63);
            }

            // シャープネス
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                Mine.StatusEffect.Check(71))
            {
                // ダメージ増加
                Damage += (int)((decimal)Damage * Mine.StatusEffect.GetRank(71) / 100);
            }

            // プロテクト
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                DamageType != Status.DamageType.Heal &&
                Target.StatusEffect.Check(73))
            {
                // ダメージ軽減
                Damage -= (int)((decimal)Damage * Target.StatusEffect.GetRank(73) / 100);
            }

            // アスティオン
            if ((ActionArts.ActionBase == Status.ActionBaseType.MindAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SorscialAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.MagicSword) &&
                Mine.StatusEffect.Check(74))
            {
                // ダメージ増加
                Damage += (int)((decimal)Damage * Mine.StatusEffect.GetRank(74) / 100);
            }

            // バリアー
            if ((ActionArts.ActionBase == Status.ActionBaseType.MindAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SorscialAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.MagicSword) &&
                DamageType != Status.DamageType.Heal &&
                Target.StatusEffect.Check(75))
            {
                // ダメージ軽減
                Damage -= (int)((decimal)Damage * Target.StatusEffect.GetRank(75) / 100);
            }

            // ウォー・クライ
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                Mine.StatusEffect.Check(90))
            {
                // ダメージ増加
                Damage += (int)((decimal)Damage * 0.2m);
            }
            // ファランクス
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                Mine.StatusEffect.Check(91))
            {
                // ダメージ増加
                Damage -= (int)((decimal)Damage * 0.2m);
            }

            // アームフォート
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.MindAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SorscialAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.MagicSword) &&
                Mine.StatusEffect.Check(85))
            {
                // ダメージ増加
                Damage -= (int)((decimal)Damage * 0.9m);
            }

            // アーマーブレイク
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                DamageType != Status.DamageType.Heal &&
                Target.StatusEffect.Check(27))
            {
                // ダメージ増加
                Damage += (int)((decimal)Damage * Target.StatusEffect.GetRank(27) / 100);
            }

            // バッドステータス数比例ダメージアップ
            if (ActionArts.EffectList.FindByeffect_id(868) != null ||
                Mine.StatusEffect.Check(70))
            {
                int BadStatusCount = Mine.StatusEffect.GetBadCount;
                if (BadStatusCount == 0)
                {
                    Damage -= (int)((decimal)Damage * 0.8m);
                }
                else if (BadStatusCount == 1)
                {
                    Damage -= (int)((decimal)Damage * 0.5m);
                }
                if (BadStatusCount >= 3)
                {
                    Damage += (int)((decimal)Damage * (0.2m * (decimal)(BadStatusCount - 2)));
                }
            }

            // バックスラッシュ
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(785);
                if (EffectRow != null &&
                    Target.PartyBelong != Mine.PartyBelong &&
                    Target.StatusEffect.Check(945) && Target.StatusEffect.GetRank(945) != Mine.BattleID)
                {
                    LibUnitBase TargetIsThis = EnemysLive.Find(TargetIs => TargetIs.BattleID == Target.StatusEffect.GetRank(945));
                    if (TargetIsThis != null && TargetIsThis.PartyBelong == Mine.PartyBelong)
                    {
                        // ダメージ増加
                        Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                    }
                }
            }

            // サイボーグ（攻撃時）
            if ((DamageType == Status.DamageType.PhysicalDamage ||
                DamageType == Status.DamageType.PhysicalDrain) &&
                Mine.EffectList.FindByeffect_id(2100) != null)
            {
                // ダメージ増加
                Damage = (int)((decimal)Damage * 1.2m);
            }
            // サイボーグ（防御時）
            if ((DamageType == Status.DamageType.MagicalDamage ||
                DamageType == Status.DamageType.MagicalDrain) &&
                Target.EffectList.FindByeffect_id(2100) != null)
            {
                // ダメージ増加
                Damage = (int)((decimal)Damage * 1.2m);
            }

            // エレメンタラー（攻撃時）
            if ((DamageType == Status.DamageType.MagicalDamage ||
                DamageType == Status.DamageType.MagicalDrain) &&
                Mine.EffectList.FindByeffect_id(2101) != null)
            {
                // ダメージ増加
                Damage = (int)((decimal)Damage * 1.2m);
            }
            // エレメンタラー（防御時）
            if ((DamageType == Status.DamageType.PhysicalDamage ||
                DamageType == Status.DamageType.PhysicalDrain) &&
                Target.EffectList.FindByeffect_id(2101) != null)
            {
                // ダメージ増加
                Damage = (int)((decimal)Damage * 1.2m);
            }

            // ファーストタッチダメージアップ
            // ファーストブレイク
            if (MainAtkItemTypeRow != null &&
                Mine.IsFirstAttachAttack &&
                (MainAtkItemTypeRow.type_id >= 12 &&
                MainAtkItemTypeRow.type_id <= 22) &&
                Mine.EffectList.FindByeffect_id(2109) != null)
            {
                // ダメージ増加
                Damage = (int)((decimal)Damage * 1.2m);
            }

            // サンドマン
            if (DamageType != Status.DamageType.Heal &&
                Mine.EffectList.FindByeffect_id(2122) != null &&
                Target.StatusEffect.Check(2))
            {
                // ダメージ増加
                Damage = (int)((decimal)Damage * 1.5m);
            }

            // 同じ属性攻撃アーツを使うと威力アップ
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(1620);
                if (EffectRow != null)
                {
                    if (DamageType != Status.DamageType.Heal &&
                        Mine.LastUsingSkillID == ActionArts.SkillID &&
                        (ActionArts.ArtsCategory == LibSkillType.FindByName("精霊魔法") ||
                        ActionArts.ArtsCategory == LibSkillType.FindByName("刀術")))
                    {
                        if (!IsVirtual)
                        {
                            Mine.UsingArtsEffectLv++;
                        }
                        Damage = (int)((decimal)Damage * (1m + Mine.UsingArtsEffectLv * EffectRow.rank / 100m));
                    }
                    else
                    {
                        if (!IsVirtual)
                        {
                            Mine.LastUsingSkillID = ActionArts.SkillID;
                            Mine.UsingArtsEffectLv = 0;
                        }
                    }
                }
            }

            // 同じ守護者への回復量アップ
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(1621);
                if (DamageType == Status.DamageType.Heal &&
                    EffectRow != null &&
                    (ActionArts.ArtsCategory == LibSkillType.FindByName("神聖魔法") ||
                    ActionArts.ArtsCategory == LibSkillType.FindByName("薬草")))
                {
                    Damage = (int)((decimal)Damage * (1m + 0.5m * EffectRow.rank));
                }
            }

            #region 天候＆地形による影響
            if (!ActionArts.IsNormalAttack && BattleCommon.CheckWeatherEffect(Mine) && BattleCommon.CheckWeatherEffect(Target))
            {
                bool IsWeatherDamageUp = false;
                bool IsWeatherDamageDown = false;

                bool IsElementalSiphon = false;
                if (Mine.StatusEffect.Check(268))
                {
                    IsElementalSiphon = true;
                }

                foreach (string Elemental in LibConst.ElementalList)
                {
                    // 物理属性の場合スキップ
                    switch (Elemental)
                    {
                        case Status.Elemental.Slash:
                        case Status.Elemental.Pierce:
                        case Status.Elemental.Strike:
                        case Status.Elemental.Break:
                            continue;
                    }

                    // 攻撃側の属性判定
                    int AttackElement = ActionArts.Elemental(Elemental);

                    // 属性変更による効果
                    BattleCommon.ElementalSpecial(ref AttackElement, Mine, ActionArts, Elemental);

                    if (AttackElement <= 0)
                    {
                        // 属性がない場合はスキップ
                        continue;
                    }

                    // 天候影響
                    switch (Elemental)
                    {
                        case Status.Elemental.Fire:
                            if (WeatherRow.fire > 0)
                            {
                                IsWeatherDamageUp = true;
                            }
                            if (WeatherRow.fire < 0)
                            {
                                IsWeatherDamageDown = true;
                            }
                            break;
                        case Status.Elemental.Freeze:
                            if (WeatherRow.freeze > 0)
                            {
                                IsWeatherDamageUp = true;
                            }
                            if (WeatherRow.freeze < 0)
                            {
                                IsWeatherDamageDown = true;
                            }
                            break;
                        case Status.Elemental.Air:
                            if (WeatherRow.air > 0)
                            {
                                IsWeatherDamageUp = true;
                            }
                            if (WeatherRow.air < 0)
                            {
                                IsWeatherDamageDown = true;
                            }
                            break;
                        case Status.Elemental.Earth:
                            if (WeatherRow.earth > 0)
                            {
                                IsWeatherDamageUp = true;
                            }
                            if (WeatherRow.earth < 0)
                            {
                                IsWeatherDamageDown = true;
                            }
                            break;
                        case Status.Elemental.Water:
                            if (WeatherRow.water > 0)
                            {
                                IsWeatherDamageUp = true;
                            }
                            if (WeatherRow.water < 0)
                            {
                                IsWeatherDamageDown = true;
                            }
                            break;
                        case Status.Elemental.Thunder:
                            if (WeatherRow.thunder > 0)
                            {
                                IsWeatherDamageUp = true;
                            }
                            if (WeatherRow.thunder < 0)
                            {
                                IsWeatherDamageDown = true;
                            }
                            break;
                        case Status.Elemental.Holy:
                            if (WeatherRow.holy > 0)
                            {
                                IsWeatherDamageUp = true;
                            }
                            if (WeatherRow.holy < 0)
                            {
                                IsWeatherDamageDown = true;
                            }
                            break;
                        case Status.Elemental.Dark:
                            if (WeatherRow.dark > 0)
                            {
                                IsWeatherDamageUp = true;
                            }
                            if (WeatherRow.dark < 0)
                            {
                                IsWeatherDamageDown = true;
                            }
                            break;
                    }

                    // 地形による影響
                    switch (Elemental)
                    {
                        case Status.Elemental.Fire:
                            if (FieldRow.fire > 0)
                            {
                                if (IsElementalSiphon)
                                {
                                    Damage = (int)((decimal)Damage * 1.5m);
                                }
                                else
                                {
                                    Damage = (int)((decimal)Damage * 1.2m);
                                }
                            }
                            if (FieldRow.fire < 0)
                            {
                                Damage = (int)((decimal)Damage * 0.8m);
                            }
                            break;
                        case Status.Elemental.Freeze:
                            if (FieldRow.freeze > 0)
                            {
                                if (IsElementalSiphon)
                                {
                                    Damage = (int)((decimal)Damage * 1.5m);
                                }
                                else
                                {
                                    Damage = (int)((decimal)Damage * 1.2m);
                                }
                            }
                            if (FieldRow.freeze < 0)
                            {
                                Damage = (int)((decimal)Damage * 0.8m);
                            }
                            break;
                        case Status.Elemental.Air:
                            if (FieldRow.air > 0)
                            {
                                if (IsElementalSiphon)
                                {
                                    Damage = (int)((decimal)Damage * 1.5m);
                                }
                                else
                                {
                                    Damage = (int)((decimal)Damage * 1.2m);
                                }
                            }
                            if (FieldRow.air < 0)
                            {
                                Damage = (int)((decimal)Damage * 0.8m);
                            }
                            break;
                        case Status.Elemental.Earth:
                            if (FieldRow.earth > 0)
                            {
                                if (IsElementalSiphon)
                                {
                                    Damage = (int)((decimal)Damage * 1.5m);
                                }
                                else
                                {
                                    Damage = (int)((decimal)Damage * 1.2m);
                                }
                            }
                            if (FieldRow.earth < 0)
                            {
                                Damage = (int)((decimal)Damage * 0.8m);
                            }
                            break;
                        case Status.Elemental.Water:
                            if (FieldRow.water > 0)
                            {
                                if (IsElementalSiphon)
                                {
                                    Damage = (int)((decimal)Damage * 1.5m);
                                }
                                else
                                {
                                    Damage = (int)((decimal)Damage * 1.2m);
                                }
                            }
                            if (FieldRow.water < 0)
                            {
                                Damage = (int)((decimal)Damage * 0.8m);
                            }
                            break;
                        case Status.Elemental.Thunder:
                            if (FieldRow.thunder > 0)
                            {
                                if (IsElementalSiphon)
                                {
                                    Damage = (int)((decimal)Damage * 1.5m);
                                }
                                else
                                {
                                    Damage = (int)((decimal)Damage * 1.2m);
                                }
                            }
                            if (FieldRow.thunder < 0)
                            {
                                Damage = (int)((decimal)Damage * 0.8m);
                            }
                            break;
                        case Status.Elemental.Holy:
                            if (FieldRow.holy > 0)
                            {
                                if (IsElementalSiphon)
                                {
                                    Damage = (int)((decimal)Damage * 1.5m);
                                }
                                else
                                {
                                    Damage = (int)((decimal)Damage * 1.2m);
                                }
                            }
                            if (FieldRow.holy < 0)
                            {
                                Damage = (int)((decimal)Damage * 0.8m);
                            }
                            break;
                        case Status.Elemental.Dark:
                            if (FieldRow.dark > 0)
                            {
                                if (IsElementalSiphon)
                                {
                                    Damage = (int)((decimal)Damage * 1.5m);
                                }
                                else
                                {
                                    Damage = (int)((decimal)Damage * 1.2m);
                                }
                            }
                            if (FieldRow.dark < 0)
                            {
                                Damage = (int)((decimal)Damage * 0.8m);
                            }
                            break;
                    }
                }

                if (IsWeatherDamageUp && IsWeatherDamageDown)
                {
                    decimal WeatherRate = 0.6m;

                    if (IsElementalSiphon)
                    {
                        WeatherRate = 0.9m;
                    }
                    else if (Mine.EffectList.FindByeffect_id(900) != null)
                    {
                        WeatherRate = 0.8m;
                    }

                    Damage = (int)((decimal)Damage * WeatherRate);
                }
                else if (IsWeatherDamageUp && !IsWeatherDamageDown)
                {
                    decimal WeatherRate = 1.2m;

                    if (IsElementalSiphon)
                    {
                        WeatherRate = 1.5m;
                    }
                    else if (Mine.EffectList.FindByeffect_id(900) != null)
                    {
                        WeatherRate = 1.4m;
                    }

                    Damage = (int)((decimal)Damage * WeatherRate);
                }
                else if (!IsWeatherDamageUp && IsWeatherDamageDown)
                {
                    decimal WeatherRate = 0.5m;

                    if (IsElementalSiphon)
                    {
                        WeatherRate = 0.7m;
                    }
                    if (Mine.EffectList.FindByeffect_id(900) != null)
                    {
                        WeatherRate = 0.6m;
                    }

                    Damage = (int)((decimal)Damage * WeatherRate);
                }
            }
            #endregion

            #region ダメージアップ、ダウン効果
            // 被物理ダメージダウン(遠隔も含む)
            {
                EffectListEntity.effect_listRow EffectRow = Target.EffectList.FindByeffect_id(720);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    DamageType != Status.DamageType.Heal &&
                    (ActionArts.AttackType == Status.AttackType.Combat ||
                    ActionArts.AttackType == Status.AttackType.Shoot))
                {
                    Damage -= (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // 被魔法ダメージダウン
            {
                EffectListEntity.effect_listRow EffectRow = Target.EffectList.FindByeffect_id(722);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    ActionArts.AttackType == Status.AttackType.Mystic &&
                    DamageType != Status.DamageType.Heal)
                {
                    Damage -= (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // 被ブレスダメージダウン
            {
                EffectListEntity.effect_listRow EffectRow = Target.EffectList.FindByeffect_id(724);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    ActionArts.AttackType == Status.AttackType.Bless &&
                    DamageType != Status.DamageType.Heal)
                {
                    Damage -= (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // 被物理ダメージアップ(遠隔も含む)
            {
                EffectListEntity.effect_listRow EffectRow = Target.EffectList.FindByeffect_id(725);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    DamageType != Status.DamageType.Heal &&
                    (ActionArts.AttackType == Status.AttackType.Combat ||
                    ActionArts.AttackType == Status.AttackType.Shoot))
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // 被魔法ダメージアップ
            {
                EffectListEntity.effect_listRow EffectRow = Target.EffectList.FindByeffect_id(727);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    ActionArts.AttackType == Status.AttackType.Mystic &&
                    DamageType != Status.DamageType.Heal)
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }

            // 被ブレスダメージアップ
            {
                EffectListEntity.effect_listRow EffectRow = Target.EffectList.FindByeffect_id(729);
                if (EffectRow != null &&
                    EffectRow.prob > LibInteger.GetRandBasis() &&
                    ActionArts.AttackType == Status.AttackType.Bless &&
                    DamageType != Status.DamageType.Heal)
                {
                    Damage += (int)((decimal)Damage * EffectRow.rank / 100m);
                }
            }
            #endregion

            #region 威力アップ
            // HPMax物理威力アップ
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                DamageType != Status.DamageType.Heal &&
                Mine.EffectList.FindByeffect_id(770) != null &&
                Mine.HPDamageRate == 100)
            {
                // ダメージ増加
                Damage = (int)((decimal)Damage * 1.2m);
            }

            // HPMax魔法威力アップ
            if ((ActionArts.ActionBase == Status.ActionBaseType.MindAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SorscialAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.MagicSword) &&
                DamageType != Status.DamageType.Heal &&
                Mine.EffectList.FindByeffect_id(771) != null &&
                Mine.HPDamageRate == 100)
            {
                // ダメージ増加
                Damage = (int)((decimal)Damage * 1.2m);
            }

            // 瀕死物理威力アップ
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                DamageType != Status.DamageType.Heal &&
                Mine.EffectList.FindByeffect_id(772) != null &&
                Mine.HPDamageRate < 20)
            {
                if (Mine.GetType() == typeof(LibPlayer))
                {
                    // ダメージ増加
                    Damage = (int)((decimal)Damage * 2m);
                }
                else
                {
                    // ダメージ増加
                    Damage = (int)((decimal)Damage * 1.2m);
                }
            }

            // 瀕死魔法威力アップ
            if ((ActionArts.ActionBase == Status.ActionBaseType.MindAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SorscialAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.MagicSword) &&
                DamageType != Status.DamageType.Heal &&
                Mine.EffectList.FindByeffect_id(773) != null &&
                Mine.HPDamageRate < 20)
            {
                if (Mine.GetType() == typeof(LibPlayer))
                {
                    // ダメージ増加
                    Damage = (int)((decimal)Damage * 2m);
                }
                else
                {
                    // ダメージ増加
                    Damage = (int)((decimal)Damage * 1.2m);
                }
            }

            // 瀕死物理防御アップ
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                DamageType != Status.DamageType.Heal &&
                Target.EffectList.FindByeffect_id(774) != null &&
                Target.HPDamageRate < 20)
            {
                if (Target.GetType() == typeof(LibPlayer))
                {
                    // ダメージ増加
                    Damage = (int)((decimal)Damage * 0.5m);
                }
                else
                {
                    // ダメージ増加
                    Damage = (int)((decimal)Damage * 1m / 3m);
                }
            }

            // 瀕死魔法防御アップ
            if ((ActionArts.ActionBase == Status.ActionBaseType.MindAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SorscialAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.MagicSword) &&
                DamageType != Status.DamageType.Heal &&
                Target.EffectList.FindByeffect_id(775) != null &&
                Target.HPDamageRate < 20)
            {
                if (Target.GetType() == typeof(LibPlayer))
                {
                    // ダメージ増加
                    Damage = (int)((decimal)Damage * 0.5m);
                }
                else
                {
                    // ダメージ増加
                    Damage = (int)((decimal)Damage * 1m / 3m);
                }
            }

            // レイジ
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                DamageType != Status.DamageType.Heal &&
                Mine.EffectList.FindByeffect_id(2121) != null &&
                Mine.HPDamageRate < 20)
            {
                if (Mine.GetType() == typeof(LibPlayer))
                {
                    // ダメージ増加
                    Damage = (int)((decimal)Damage * 1.2m);
                }
            }
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                DamageType != Status.DamageType.Heal &&
                Target.EffectList.FindByeffect_id(2121) != null &&
                Target.HPDamageRate < 20)
            {
                if (Target.GetType() == typeof(LibPlayer))
                {
                    // ダメージ増加
                    Damage = (int)((decimal)Damage * 0.8m);
                }
            }

            // アクセスコード
            if (Mine.EffectList.FindByeffect_id(2138) != null)
            {
                int DeathCount = this.Friends.Count - this.FriendsLive.Count;
                Damage = (int)((decimal)Damage + (decimal)Damage * (0.5m * (decimal)DeathCount));
            }

            #endregion

            // ハイアンドロー
            if (ActionArts.EffectList.FindByeffect_id(1061) != null)
            {
                switch (Target.HighAndLowHitCount)
                {
                    case 0:
                        Damage = 1;
                        break;
                    case 1:
                        Damage = 2;
                        break;
                    case 2:
                        Damage = 4;
                        break;
                    case 3:
                        Damage = 8;
                        break;
                    case 4:
                        Damage = 16;
                        break;
                    case 5:
                        Damage = 32;
                        break;
                    case 6:
                        Damage = 64;
                        break;
                    case 7:
                        Damage = 128;
                        break;
                    case 8:
                        Damage = 256;
                        break;
                    case 9:
                        Damage = 512;
                        break;
                    case 10:
                        Damage = 1024;
                        break;
                    case 11:
                        Damage = 2048;
                        break;
                    case 12:
                        Damage = 4096;
                        break;
                    case 13:
                        Damage = 8192;
                        break;
                    case 14:
                        Damage = 16384;
                        break;
                    case 15:
                        Damage = 32768;
                        break;
                    case 16:
                        Damage = 65536;
                        break;
                    case 17:
                        Damage = 131072;
                        break;
                    default:
                        Damage = 0;
                        break;
                }
            }

            // 固定ダメージ
            {
                EffectListEntity.effect_listRow effectRow = ActionArts.EffectList.FindByeffect_id(926);
                if (effectRow != null)
                {
                    Damage = (int)effectRow.rank;
                }
            }

            // ドリームルーレットのダメージ判定
            {
                EffectListEntity.effect_listRow effectRow = ActionArts.EffectList.FindByeffect_id(1060);
                if (effectRow != null)
                {
                    int[] DamageList = new int[] { 1, 2, 4, 8, 16, 32, 64, 128, 256, 521, 1024, 2048, 4096, 8192, 16384, 32768, 65536, 131072 };
                    Damage = DamageList[LibInteger.GetRand(DamageList.Length)];
                }
            }

            // ドラゴンズスケイル
            if (DamageType != Status.DamageType.Heal &&
                Target.StatusEffect.Check(269))
            {
                Damage -= (int)((decimal)Damage * 0.8m);
            }

            // ロックスキン
            if (DamageType != Status.DamageType.Heal &&
                Target.StatusEffect.Check(57))
            {
                int Rock = (int)Target.StatusEffect.GetRank(57);

                if (Damage < Rock)
                {
                    Target.StatusEffect.SetRank(57, Rock - Damage);
                    Rock = Damage;
                }
                else
                {
                    Target.StatusEffect.Delete(57);
                }

                Damage -= Rock;
            }

            // 防御無視耐性
            if (IsDisDefence &&
                Target.EffectList.FindByeffect_id(4501) != null)
            {
                Damage /= 8;
            }

            // 隊列補正
            if ((Mine.BattleFormation + Target.BattleFormation) > ActionArts.Range)
            {
                Damage /= 2;
            }

            // ダメージ最低保障
            {
                EffectListEntity.effect_listRow BasedEffectRow = Target.EffectList.FindByeffect_id(1330);
                if (DamageType != Status.DamageType.Heal &&
                    BasedEffectRow != null &&
                    Damage < (int)BasedEffectRow.rank)
                {
                    Damage = (int)BasedEffectRow.rank;
                }
            }

            // ダメージ最大制限
            {
                EffectListEntity.effect_listRow BasedEffectRow = Target.EffectList.FindByeffect_id(1331);
                if (DamageType != Status.DamageType.Heal &&
                    BasedEffectRow != null &&
                    Damage > (int)BasedEffectRow.rank)
                {
                    Damage = (int)BasedEffectRow.rank;
                }
            }

            // 聖障壁
            if ((ActionArts.ActionBase == Status.ActionBaseType.MainAttack ||
                ActionArts.ActionBase == Status.ActionBaseType.SubAttack) &&
                DamageType != Status.DamageType.Heal &&
                (ActionArts.AttackType == Status.AttackType.Combat ||
                ActionArts.AttackType == Status.AttackType.Shoot) &&
                Target.StatusEffect.Check(253))
            {
                // ダメージ０にする
                Damage = 0;
            }

            if (Damage < 0)
            {
                Damage = 0;
            }

            return Damage;
        }
    }
}
