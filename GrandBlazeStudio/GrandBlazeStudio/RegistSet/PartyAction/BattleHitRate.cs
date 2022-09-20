using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.Entity;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        /// <summary>
        /// 命中率算出
        /// </summary>
        /// <param name="Mine">加攻撃者</param>
        /// <param name="Target">被攻撃者</param>
        /// <param name="ActionArts">攻撃行動種別</param>
        /// <param name="IsDodgeOut">回避無視有無</param>
        /// <param name="IsCheckGuard">ガードチェック</param>
        /// <param name="IsCheckParry">パリィチェック</param>
        /// <param name="IsCheckDodge">ドッジチェック</param>
        /// <returns>命中率</returns>
        private int BattleHitRate(LibUnitBase Mine, LibUnitBase Target, LibActionType ActionArts, ref bool IsDodgeOut, ref bool IsCheckGuard, ref bool IsCheckParry, ref bool IsCheckDodge)
        {
            // デフォルトの命中力
            int AttackHit = ActionArts.HitRate;

            // 回避無視
            if (Mine.EffectList.FindByeffect_id(846) != null)
            {
                IsDodgeOut = false;
            }

            ItemTypeEntity.mt_item_typeRow typeRow = null;

            {
                if (Mine.GetType() == typeof(LibPlayer))
                {
                    LibPlayer Player = (LibPlayer)Mine;
                    CommonItemEntity.item_listRow ItemRow = Player.GetHaveItemEquiped(Status.EquipSpot.Main);
                    if (ItemRow != null)
                    {
                        typeRow = LibItemType.GetTypeRow(ItemRow.it_type);
                    }
                }
                else if (Mine.GetType() == typeof(LibGuest))
                {
                    int ItemID = 0;
                    LibGuest Guest = (LibGuest)Mine;
                    ItemID = Guest.HaveItemS[0].equip_main;
                    typeRow = LibItemType.GetTypeRow(LibItem.GetType(ItemID, false));
                }

                if (typeRow != null)
                {
                    IsCheckGuard = typeRow.check_guard;
                    IsCheckParry = typeRow.check_parry;
                    IsCheckDodge = typeRow.check_dodge;
                }
            }

            #region 天候による影響
            if (IsDodgeOut && BattleCommon.CheckWeatherEffect(Mine))
            {
                if (WeatherRow.bad_weather && typeRow != null)
                {
                    decimal BadWeatherHit = (decimal)typeRow.bad_weather_hit;

                    if (Mine.EffectList.FindByeffect_id(900) != null)
                    {
                        BadWeatherHit = BadWeatherHit * 1.5m;
                    }

                    AttackHit = (int)((decimal)AttackHit * BadWeatherHit / 100m);
                }
            }
            #endregion

            // スリンガー
            if (typeRow != null &&
                (typeRow.type_id == 15 ||
                typeRow.type_id == 16) &&
                ActionArts.SkillType == Status.SkillType.Arts &&
                Mine.EffectList.FindByeffect_id(2107) != null)
            {
                AttackHit += 20;
            }

            // ランボー
            if (typeRow != null &&
                (typeRow.type_id == 17 || 
                typeRow.type_id == 18)  &&
                ActionArts.SkillType == Status.SkillType.Arts &&
                Mine.EffectList.FindByeffect_id(2110) != null)
            {
                AttackHit += 20;
            }

            // ハイアンドロー
            if (ActionArts.EffectList.FindByeffect_id(1061) != null)
            {
                switch (Target.HighAndLowHitCount)
                {
                    case 0:
                        AttackHit = 85;
                        break;
                    case 1:
                        AttackHit = 85;
                        break;
                    case 2:
                        AttackHit = 84;
                        break;
                    case 3:
                        AttackHit = 83;
                        break;
                    case 4:
                        AttackHit = 81;
                        break;
                    case 5:
                        AttackHit = 79;
                        break;
                    case 6:
                        AttackHit = 76;
                        break;
                    case 7:
                        AttackHit = 73;
                        break;
                    case 8:
                        AttackHit = 69;
                        break;
                    case 9:
                        AttackHit = 65;
                        break;
                    case 10:
                        AttackHit = 60;
                        break;
                    case 11:
                        AttackHit = 55;
                        break;
                    case 12:
                        AttackHit = 49;
                        break;
                    case 13:
                        AttackHit = 43;
                        break;
                    case 14:
                        AttackHit = 36;
                        break;
                    case 15:
                        AttackHit = 29;
                        break;
                    case 16:
                        AttackHit = 21;
                        break;
                    case 17:
                        AttackHit = 13;
                        break;
                    default:
                        AttackHit = 0;
                        break;
                }

                IsDodgeOut = false;
            }

            // 暗闇
            if (Mine.StatusEffect.Check(7))
            {
                if (Mine.EffectList.FindByeffect_id(2102) != null)
                {
                    AttackHit = (int)((decimal)AttackHit * 2m / 3m);
                }
                else
                {
                    AttackHit /= 2;
                }
            }

            if (AttackHit < 0)
            {
                AttackHit = 0;
            }
            if (AttackHit > 100)
            {
                AttackHit = 100;
            }

            return AttackHit;
        }
    }
}
