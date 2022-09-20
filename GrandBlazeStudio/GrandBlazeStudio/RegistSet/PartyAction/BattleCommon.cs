using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        /// <summary>
        /// 戦闘共有クラス
        /// </summary>
        public class BattleCommon
        {
            /// <summary>
            /// 隊列の再編
            /// </summary>
            /// <param name="BattleCharacters">再編対象</param>
            public static void FormationResetting(List<LibUnitBase> BattleCharacters)
            {
                bool Foward = false;
                int i;
                List<LibUnitBase> Friendrys = BattleCharacters.GetFriendry();
                List<LibUnitBase> Monsters = BattleCharacters.GetMonsters();

                for (i = 0; i < 1; i++)
                {
                    Foward = false;

                    foreach (LibUnitBase Mine in Friendrys)
                    {
                        if (Mine.BattleFormation == Status.Formation.Foward && Mine.BattleOut == false)
                        {
                            Foward = true;
                            break;
                        }
                    }

                    if (!Foward)
                    {
                        foreach (LibUnitBase Mine in Friendrys)
                        {
                            Mine.BattleFormation--;
                            Mine.Formation = Mine.BattleFormation;
                        }
                    }
                }

                for (i = 0; i < 1; i++)
                {
                    Foward = false;

                    foreach (LibUnitBase Mine in Monsters)
                    {
                        if (Mine.BattleFormation == Status.Formation.Foward && Mine.BattleOut == false)
                        {
                            Foward = true;
                            break;
                        }
                    }

                    if (!Foward)
                    {
                        foreach (LibUnitBase Mine in Monsters)
                        {
                            Mine.BattleFormation--;
                            Mine.Formation = Mine.BattleFormation;
                        }
                    }
                }
            }

            /// <summary>
            /// キャラクターの死亡
            /// </summary>
            /// <param name="Target">死亡者</param>
            /// <param name="BattleCharacter">戦闘参加者リスト</param>
            public static void DeadMans(LibUnitBase Target, List<LibUnitBase> BattleCharacter)
            {
                // 死亡処理
                Target.Dead();

                // 隊列
                FormationResetting(BattleCharacter);

                // ヘイトリスト削除
                foreach (LibUnitBase Mine in BattleCharacter)
                {
                    if (!Mine.BattleOut)
                    {
                        Mine.HateList.Delete(Target);
                    }
                }

                // ペット解除
                if (Target.GetType() == typeof(LibPlayer) && ((LibPlayer)Target).PetCharacterBattleID > 0)
                {
                    LibUnitBase Pets = BattleCharacter.Find(PetTarget => PetTarget.BattleID == ((LibPlayer)Target).PetCharacterBattleID);
                    if (Pets != null && !Pets.BattleOut)
                    {
                        BattleCommon.DeadMans(Pets, BattleCharacter);
                    }

                    ((LibPlayer)Target).PetCharacterBattleID = 0;
                }
                if (Target.CompanionBattleID > 0)
                {
                    LibUnitBase Companions = BattleCharacter.Find(PetTarget => PetTarget.BattleID == Target.CompanionBattleID);
                    if (Companions != null && !Companions.BattleOut)
                    {
                        BattleCommon.DeadMans(Companions, BattleCharacter);
                    }
                }
            }

            /// <summary>
            /// キャラクターの復活
            /// </summary>
            /// <param name="Target">復活者</param>
            /// <param name="BattleCharacter">戦闘参加者リスト</param>
            /// <param name="CureRate">回復率</param>
            public static void Revivals(LibUnitBase Target, List<LibUnitBase> BattleCharacter, int CureRate)
            {
                // 復活処理
                Target.Raise(CureRate);

                // ヘイトリスト復活
                foreach (LibUnitBase Mine in BattleCharacter)
                {
                    if (!Mine.BattleOut)
                    {
                        Mine.HateList.Revivals(Target);
                    }
                }
            }

            /// <summary>
            /// 天候効果が発揮されるか判定
            /// </summary>
            /// <param name="Target">効果発揮者</param>
            /// <param name="Weather">天候</param>
            /// <returns>発揮される場合は真</returns>
            public static bool CheckWeatherEffect(LibUnitBase Target)
            {
                // 天候＆地形影響無視
                if (Target.EffectList.FindByeffect_id(905) != null)
                {
                    return false;
                }

                return true;
            }

            /// <summary>
            /// 回復ヘイトベース
            /// </summary>
            /// <param name="TargetLevel">対象レベル</param>
            /// <returns>エニティ</returns>
            public static int GetHealHateRate(int TargetLevel)
            {
                int[] CureBase = new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 20, 21, 
                    21, 22, 22, 23, 23, 24, 24, 25, 25, 26, 26, 27, 27, 28, 28, 29, 29, 30, 
                    30, 31, 31, 32, 32, 33, 33, 34, 34, 35, 35, 36, 36, 37, 37, 38, 38, 39, 
                    39, 40, 40, 41, 41, 42, 43, 43, 44, 44, 45, 46, 46, 47, 47, 48, 49, 49, 
                    50, 50, 51, 52, 52, 53, 53, 54, 55 };

                return CureBase[TargetLevel];
            }

            /// <summary>
            /// 属性相性値によるメッセージ取得
            /// </summary>
            /// <param name="ElementalRatingType">属性相性種類</param>
            /// <returns>メッセージ</returns>
            public static string GetAddMessageElementals(int ElementalRatingType)
            {
                switch (ElementalRatingType)
                {
                    case Status.ElementalRatingType.Weak:
                        return "Weak! ";
                    case Status.ElementalRatingType.Regist:
                        return "Regist! ";
                    case Status.ElementalRatingType.Block:
                        return "Block! ";
                    case Status.ElementalRatingType.Drain:
                        return "Drain! ";
                    default:
                        return "";
                }
            }

            /// <summary>
            /// 特殊装填による属性変化
            /// </summary>
            /// <param name="AttackElement">攻撃属性値</param>
            /// <param name="Mine">使用者</param>
            /// <param name="ActionArts">使用アーツ</param>
            /// <param name="ElementalType">属性</param>
            /// <returns>属性値</returns>
            public static void ElementalSpecial(ref int AttackElement, LibUnitBase Mine, LibActionType ActionArts, string ElementalType)
            {
                if (ActionArts.IsNormalAttack && Mine.StatusEffect.Check(95))
                {
                    switch ((int)Mine.StatusEffect.GetRank(95))
                    {
                        case 28001:
                            // バーンスプレッダー
                            if (ElementalType == Status.Elemental.Fire)
                            {
                                AttackElement = 100;
                            }
                            else
                            {
                                AttackElement = 0;
                            }
                            break;
                        case 28003:
                            // フロストランサー
                            if (ElementalType == Status.Elemental.Freeze)
                            {
                                AttackElement = 100;
                            }
                            else
                            {
                                AttackElement = 0;
                            }
                            break;
                        case 28004:
                            // チョークカイザー
                            if (ElementalType == Status.Elemental.Air)
                            {
                                AttackElement = 100;
                            }
                            else
                            {
                                AttackElement = 0;
                            }
                            break;
                        case 28005:
                            // セイスミッカー
                            if (ElementalType == Status.Elemental.Earth)
                            {
                                AttackElement = 100;
                            }
                            else
                            {
                                AttackElement = 0;
                            }
                            break;
                    }
                }
            }
        }
    }
}
