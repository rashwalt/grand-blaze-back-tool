using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        /// <summary>
        /// イニシアチブ決定
        /// </summary>
        /// <param name="Mine">行動者</param>
        /// <param name="Turn">ターン</param>
        private void BattleInitiativeSet(LibUnitBase Mine, int Turn)
        {
            List<LibUnitBase> TempTargetBattle = new List<LibUnitBase>();

            // 行動の未決定化
            Mine.IsActionEnd = false;

            #region 敵と味方の判定
            Friends = new List<LibUnitBase>();
            Enemys = new List<LibUnitBase>();
            FriendsLive = new List<LibUnitBase>();
            EnemysLive = new List<LibUnitBase>();

            if (Mine.PartyBelong == Status.Belong.Enemy)
            {
                // モンスターの場合
                Friends = BattleCharacer.GetMonsters();
                Enemys = BattleCharacer.GetFriendry();

                FriendsLive = Friends.GetLive();
                EnemysLive = Enemys.GetLive();
            }
            else
            {
                // 味方の場合
                Friends = BattleCharacer.GetFriendry();
                Enemys = BattleCharacer.GetMonsters();

                FriendsLive = Friends.GetLive();
                EnemysLive = Enemys.GetLive();
            }
            #endregion

            #region 行動内容の確定(とターゲットの確定)
            bool ActionInfo = false;// 行動内容が設定されたか？
            int SelectedAction = Mine.ActionList[0].action;
            int SelectedActionSkillNo = 0;

            bool ActionSettings = false;
            CommonSkillEntity.skill_listRow ArtsRow;
            EffectListEntity.effect_listDataTable ArtsEffectTable = new EffectListEntity.effect_listDataTable();

            foreach (CommonUnitDataEntity.action_listRow ActionRow in Mine.ActionList)
            {
                // アクション実行確率
                if (Mine.GetType() == typeof(LibMonster))
                {
                    int actionProb = LibInteger.ChangeProbCombo(ActionRow.probability);

                    if (LibInteger.GetRandBasis() > actionProb)
                    {
                        // 実行しない
                        continue;
                    }
                }

                if (!ActionRow.Ismax_countNull() && ActionRow.max_count > 0 && ActionRow.use_count >= ActionRow.max_count)
                {
                    continue;
                }

                ActionSettings = true;

                // 行動タイミングの判定
                if (Mine.GetType() == typeof(LibMonster))
                {
                    int[] Timings = { ActionRow.timing1, ActionRow.timing2, ActionRow.timing3 };
                    bool SkilAction = true;

                    foreach (int Timing in Timings)
                    {
                        if (Timing == 0) { continue; }

                        #region タイミング
                        switch (Timing)
                        {
                            case 1:
                                {
                                    // いつでも
                                    SkilAction = false;
                                }
                                break;
                            case 2:
                                {
                                    // HP100%未満
                                    if (Mine.HPDamageRate < 100)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 3:
                                {
                                    // HP90%未満
                                    if (Mine.HPDamageRate < 90)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 4:
                                {
                                    // HP80%未満
                                    if (Mine.HPDamageRate < 80)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 5:
                                {
                                    // HP70%未満
                                    if (Mine.HPDamageRate < 70)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 6:
                                {
                                    // HP60%未満
                                    if (Mine.HPDamageRate < 60)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 7:
                                {
                                    // HP50%未満
                                    if (Mine.HPDamageRate < 50)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 8:
                                {
                                    // HP40%未満
                                    if (Mine.HPDamageRate < 40)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 9:
                                {
                                    // HP30%未満
                                    if (Mine.HPDamageRate < 30)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 10:
                                {
                                    // HP20%未満
                                    if (Mine.HPDamageRate < 20)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 11:
                                {
                                    // HP10%未満
                                    if (Mine.HPDamageRate < 10)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 12:
                                {
                                    // フレイムプレス使用回数1回
                                    if (Mine.GetType() == typeof(LibMonster) && ((LibMonster)Mine).FlamePressUseCount == 1)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 13:
                                {
                                    // フレイムプレス使用回数0回
                                    if (Mine.GetType() == typeof(LibMonster) && ((LibMonster)Mine).FlamePressUseCount == 0)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 14:
                                {
                                    // 同IDモンスターのHPが減少中
                                    foreach (LibMonster MemberMonster in FriendsLive)
                                    {
                                        if (MemberMonster.BattleID != Mine.BattleID && MemberMonster.HPDamageRate <= 50)
                                        {
                                            SkilAction = false;
                                            break;
                                        }
                                    }
                                }
                                break;
                            case 15:
                                {
                                    // HP90%以上
                                    if (Mine.HPDamageRate >= 90)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 16:
                                {
                                    // HP80%以上
                                    if (Mine.HPDamageRate >= 80)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 17:
                                {
                                    // HP70%以上
                                    if (Mine.HPDamageRate >= 70)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 18:
                                {
                                    // HP60%以上
                                    if (Mine.HPDamageRate >= 60)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 19:
                                {
                                    // HP50%以上
                                    if (Mine.HPDamageRate >= 50)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 20:
                                {
                                    // HP40%以上
                                    if (Mine.HPDamageRate >= 40)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 21:
                                {
                                    // HP30%以上
                                    if (Mine.HPDamageRate >= 30)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 22:
                                {
                                    // HP20%以上
                                    if (Mine.HPDamageRate >= 20)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 23:
                                {
                                    // HP10%以上
                                    if (Mine.HPDamageRate >= 10)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                            case 24:
                                {
                                    // 生命の賛歌使用直後
                                    if (Mine.GetType() == typeof(LibMonster) && ((LibMonster)Mine).IsUseLifeSong)
                                    {
                                        SkilAction = false;
                                    }
                                }
                                break;
                        }
                        #endregion
                    }

                    if (SkilAction)
                    {
                        // アクションを実行すべき時ではない場合はスキップ
                        continue;
                    }
                }

                // ターゲットの初期化
                TempTargetBattle = new List<LibUnitBase>();

                // ターゲット対象の判定
                int TargetType = 0;

                int ActionTarget = ActionRow.action_target;
                int Actions = ActionRow.action;

                // 暴走ならターゲット固定
                if (Mine.StatusEffect.Check(14))
                {
                    ActionTarget = 1;
                    Actions = Status.ActionType.MainAttack;
                }

                switch (LibAction.GetTargetType(ActionTarget))
                {
                    case 0:
                        TargetType = Status.TargetParty.Enemy;
                        break;
                    case 1:
                        TargetType = Status.TargetParty.Friend;
                        break;
                    case 2:
                        TargetType = Status.TargetParty.Mine;
                        break;
                }

                #region ターゲットの判定
                int Numbers = 0;
                switch (ActionTarget)
                {
                    #region ターゲット：敵
                    case 1:
                        // 目の前の敵(デフォルト)
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.BattleFormation == Status.Formation.Foward)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 2:
                        // 敵一体
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            TempTargetBattle.Add(Target);
                        }
                        break;
                    case 3:
                        // ヘイトの一番高い相手
                        LibUnitBase TopHater = Mine.HateList.GetTop(Mine.PartyBelong);
                        if (TopHater != null)
                        {
                            TempTargetBattle.Add(TopHater);
                        }
                        else
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 4:
                        // ヘイトの一番低い相手
                        LibUnitBase AncHater = Mine.HateList.GetAnc(Mine.PartyBelong);
                        if (AncHater != null)
                        {
                            TempTargetBattle.Add(AncHater);
                        }
                        else
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 5:
                        // 最も遠い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.BattleFormation == Status.Formation.Backs)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }

                        if (TempTargetBattle.Count == 0)
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.BattleFormation == Status.Formation.Foward)
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 6:
                        // 自分をねらう敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.OldSelectedTarget.IndexOf(Mine) >= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 7:
                        // 味方をねらう敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            foreach (LibUnitBase HateChara in Target.OldSelectedTarget)
                            {
                                if (HateChara.PartyBelong == Mine.PartyBelong && HateChara.BattleID != Mine.BattleID)
                                {
                                    TempTargetBattle.Add(Target);
                                    break;
                                }
                            }
                        }
                        break;
                    case 8:
                        // 最もHPの高い敵
                        Numbers = 0;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPNow > Numbers)
                            {
                                Numbers = Target.HPNow;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 9:
                        // 最もHPの低い敵
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPNow < Numbers)
                            {
                                Numbers = Target.HPNow;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 10:
                        // 最もMaxHPの高い敵
                        Numbers = 0;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPMax > Numbers)
                            {
                                Numbers = Target.HPMax;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 11:
                        // 最もMaxHPの低い敵
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPMax < Numbers)
                            {
                                Numbers = Target.HPMax;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 12:
                        // 最もTPの高い敵
                        Numbers = 0;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.TPNow > Numbers)
                            {
                                Numbers = Target.TPNow;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 13:
                        // 最もTPの低い敵
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.TPNow < Numbers)
                            {
                                Numbers = Target.TPNow;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 16:
                        // 最もMPの高い敵
                        Numbers = 0;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.MPNow > Numbers)
                            {
                                Numbers = Target.MPNow;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 17:
                        // 最もMPの低い敵
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.MPNow < Numbers)
                            {
                                Numbers = Target.MPNow;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 18:
                        // 最もMaxMPの高い敵
                        Numbers = 0;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.MPMax > Numbers)
                            {
                                Numbers = Target.MPMax;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 19:
                        // 最もMaxMPの低い敵
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.MPMax < Numbers)
                            {
                                Numbers = Target.MPMax;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 20:
                        // 最もレベルの高い敵
                        Numbers = 0;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.Level > Numbers)
                            {
                                Numbers = Target.Level;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 21:
                        // 最もレベルの低い敵
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.Level < Numbers)
                            {
                                Numbers = Target.Level;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 22:
                        // 最も物理攻撃の高い敵
                        Numbers = 0;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            int AttackPoint = Target.ATK;
                            if (AttackPoint > Numbers)
                            {
                                Numbers = AttackPoint;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 23:
                        // 最も物理攻撃の低い敵
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            int AttackPoint = Target.ATK;
                            if (AttackPoint < Numbers)
                            {
                                Numbers = AttackPoint;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 26:
                        // 最も物理防御の高い敵
                        Numbers = 0;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DFE > Numbers)
                            {
                                Numbers = Target.DFE;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 27:
                        // 最も物理防御の低い敵
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DFE < Numbers)
                            {
                                Numbers = Target.DFE;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 40:
                        // 最も魔法防御の高い敵
                        Numbers = 0;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.MGR > Numbers)
                            {
                                Numbers = Target.MGR;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 41:
                        // 最も魔法防御の低い敵
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.MGR < Numbers)
                            {
                                Numbers = Target.MGR;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 42:
                        // HP=100%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate == 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 43:
                        // HP>=70%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate >= 70)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 44:
                        // HP>=50%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate >= 50)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 45:
                        // HP>=30%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate >= 30)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 252:
                        // HP<100%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 253:
                        // HP<90%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate < 90)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 254:
                        // HP<80%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate < 80)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 255:
                        // HP<70%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate < 70)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 256:
                        // HP<60%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate < 60)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 257:
                        // HP<50%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate < 50)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 258:
                        // HP<40%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate < 40)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 259:
                        // HP<30%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate < 30)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 260:
                        // HP<20%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate < 20)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 261:
                        // HP<10%の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.HPDamageRate < 10)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 278:
                        // アンデッドの敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.CategoryName == "アンデッド")
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 279:
                        // マシンの敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.CategoryName == "マシン")
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 280:
                        // 残り>=10個なら敵へ
                        {
                            if (Mine.GetType() != typeof(LibPlayer))
                            {
                                break;
                            }

                            if (Actions == Status.ActionType.NoAction)
                            {
                                break;
                            }
                            else
                            {
                                CommonSkillEntity.skill_listRow ArtsRow2 = LibSkill.GetSkillRow(ActionRow.perks_id);

                                EffectListEntity.effect_listDataTable ArtsEffectTable2 = new EffectListEntity.effect_listDataTable();
                                LibEffect.Split(ArtsRow2.sk_effect, ref ArtsEffectTable2);
                                EffectListEntity.effect_listRow ArtsEffectRow = ArtsEffectTable2.FindByeffect_id(920);
                                if (ArtsEffectRow != null)
                                {
                                    if (!((LibPlayer)Mine).CheckHaveItem(Status.ItemBox.Normal, (int)ArtsEffectRow.rank, false, 10))
                                    {
                                        break;
                                    }
                                }
                            }

                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.BattleFormation == Status.Formation.Foward)
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 281:
                        // もし自分が「骨折」なら
                        {
                            if (Mine.StatusEffect.Check(9))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 282:
                        // もし自分が「沈黙」なら
                        {
                            if (Mine.StatusEffect.Check(8))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 283:
                        // もし自分が「シャープネス」なら
                        {
                            if (Mine.StatusEffect.Check(71))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 285:
                        // もし自分が「プロテクト」なら
                        {
                            if (Mine.StatusEffect.Check(73))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 286:
                        // もし自分が「アスティオン」なら
                        {
                            if (Mine.StatusEffect.Check(74))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 287:
                        // もし自分が「バリアー」」なら
                        {
                            if (Mine.StatusEffect.Check(75))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 288:
                        // もし自分のHP>=90%なら
                        {
                            if (Mine.HPDamageRate >= 90)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 289:
                        // もし自分のHP>=70%なら
                        {
                            if (Mine.HPDamageRate >= 70)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 290:
                        // もし自分のHP>=50%なら
                        {
                            if (Mine.HPDamageRate >= 50)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 291:
                        // もし自分のHP>=30%なら
                        {
                            if (Mine.HPDamageRate >= 30)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 292:
                        // もし自分のHP>=10%なら
                        {
                            if (Mine.HPDamageRate >= 10)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 293:
                        // もし自分のHP<90%なら
                        {
                            if (Mine.HPDamageRate < 90)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 294:
                        // もし自分のHP<70%なら
                        {
                            if (Mine.HPDamageRate < 70)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 295:
                        // もし自分のHP<50%なら
                        {
                            if (Mine.HPDamageRate < 50)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 296:
                        // もし自分のHP<30%なら
                        {
                            if (Mine.HPDamageRate < 30)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 297:
                        // もし自分のHP<10%なら
                        {
                            if (Mine.HPDamageRate < 10)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 298:
                        // もし自分のMP>=90%なら
                        {
                            if (Mine.MPDamageRate >= 90)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 299:
                        // もし自分のMP>=70%なら
                        {
                            if (Mine.MPDamageRate >= 70)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 300:
                        // もし自分のMP>=50%なら
                        {
                            if (Mine.MPDamageRate >= 50)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 301:
                        // もし自分のMP>=30%なら
                        {
                            if (Mine.MPDamageRate >= 30)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 302:
                        // もし自分のMP>=10%なら
                        {
                            if (Mine.MPDamageRate >= 10)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 303:
                        // もし自分のMP<90%なら
                        {
                            if (Mine.MPDamageRate < 90)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 304:
                        // もし自分のMP<70%なら
                        {
                            if (Mine.MPDamageRate < 70)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 305:
                        // もし自分のMP<50%なら
                        {
                            if (Mine.MPDamageRate < 50)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 306:
                        // もし自分のMP<30%なら
                        {
                            if (Mine.MPDamageRate < 30)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 307:
                        // もし自分のMP<10%なら
                        {
                            if (Mine.MPDamageRate < 10)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 308:
                        // もし自分のTP>=90%なら
                        {
                            if (Mine.TPDamageRate >= 90)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 309:
                        // もし自分のTP>=70%なら
                        {
                            if (Mine.TPDamageRate >= 70)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 310:
                        // もし自分のTP>=50%なら
                        {
                            if (Mine.TPDamageRate >= 50)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 311:
                        // もし自分のTP>=30%なら
                        {
                            if (Mine.TPDamageRate >= 30)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 312:
                        // もし自分のTP>=10%なら
                        {
                            if (Mine.TPDamageRate >= 10)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 313:
                        // もし自分のTP<90%なら
                        {
                            if (Mine.TPDamageRate < 90)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 314:
                        // もし自分のTP<70%なら
                        {
                            if (Mine.TPDamageRate < 70)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 315:
                        // もし自分のTP<50%なら
                        {
                            if (Mine.TPDamageRate < 50)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 316:
                        // もし自分のTP<30%なら
                        {
                            if (Mine.TPDamageRate < 30)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 317:
                        // もし自分のTP<10%なら
                        {
                            if (Mine.TPDamageRate < 10)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 46:
                        // 火属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Fire < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 47:
                        // 氷属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Freeze < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 48:
                        // 風属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Air < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 49:
                        // 土属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Earth < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 50:
                        // 水属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Water < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 51:
                        // 雷属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Thunder < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 52:
                        // 聖属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Holy < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 53:
                        // 闇属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Dark < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 54:
                        // 斬属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Slash < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 55:
                        // 突属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Pierce < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 56:
                        // 打属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Strike < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 57:
                        // 壊属性に弱い敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Break < 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 58:
                        // 火属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Fire < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 59:
                        // 氷属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Freeze < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 60:
                        // 風属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Air < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 61:
                        // 土属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Earth < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 62:
                        // 水属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Water < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 63:
                        // 雷属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Thunder < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 64:
                        // 聖属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Holy < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 65:
                        // 闇属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Dark < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 66:
                        // 斬属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Slash < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 67:
                        // 突属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Pierce < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 68:
                        // 打属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Strike < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 69:
                        // 壊属性を吸収しない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Break < 100)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 70:
                        // 火属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Fire <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 71:
                        // 氷属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Freeze <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 72:
                        // 風属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Air <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 73:
                        // 土属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Earth <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 74:
                        // 水属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Water <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 75:
                        // 雷属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Thunder <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 76:
                        // 聖属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Holy <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 77:
                        // 闇属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Dark <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 78:
                        // 斬属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Slash <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 79:
                        // 突属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Pierce <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 80:
                        // 打属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Strike <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 81:
                        // 壊属性に耐性のない敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.DefenceElemental.Break <= 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 82:
                        // 「睡眠」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(2))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 83:
                        // 「猛毒」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(3))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 85:
                        // 「呪詛」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(5))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 86:
                        // 「麻痺」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(6))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 87:
                        // 「暗闇」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(7))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 88:
                        // 「沈黙」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(8))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 89:
                        // 「骨折」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(9))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 90:
                        // 「病気」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(11))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 91:
                        // 「混乱」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(12))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 92:
                        // 「恐怖」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(13))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 93:
                        // 「暴走」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(14))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 94:
                        // 「逆転」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(15))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 95:
                        // 「スロウ」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(18))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 96:
                        // 「ヘイスト」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(53))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 97:
                        // 「リジェネ」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(50))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 98:
                        // 「マナライズ」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(51))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 99:
                        // 「リフレッシュ」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(52))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 103:
                        // 「ウォール」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(57))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 108:
                        // 「シャープネス」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(71))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 110:
                        // 「プロテクト」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(73))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 111:
                        // 「アスティオン」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(74))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 112:
                        // 「バリアー」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(75))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 113:
                        // 「魅了」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(20))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 319:
                        // 「土煙」の敵
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.StatusEffect.Check(79))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 320:
                        // もし自分が前列なら敵に
                        {
                            if (Mine.BattleFormation == Status.Formation.Foward)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 322:
                        // もし自分が後列なら敵に
                        {
                            if (Mine.BattleFormation == Status.Formation.Backs)
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 323:
                        // 集中攻撃
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (TempTargetBattle.Count == 0 || Target.TargetedCount == TempTargetBattle[0].TargetedCount)
                                {
                                    TempTargetBattle.Add(Target);
                                }
                                else if (Target.TargetedCount > TempTargetBattle[0].TargetedCount)
                                {
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 324:
                        // 分散攻撃
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (TempTargetBattle.Count == 0 || Target.TargetedCount < TempTargetBattle[0].TargetedCount)
                                {
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                                else if (Target.TargetedCount == TempTargetBattle[0].TargetedCount)
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 328:
                        // 「石化」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(10))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 329:
                        // 「ウォール」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(57))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 330:
                        // 「影縛」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(16))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 333:
                        // 「石化中」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(21))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 336:
                        // 「死霊の誘い」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(22))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 339:
                        // 「ブラッドロス」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(24))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 342:
                        // 「インスペクト」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(25))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 345:
                        // 「分身」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(62))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 348:
                        // もし自分が「分身」なら
                        {
                            if (Mine.StatusEffect.Check(62))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 349:
                        // もし自分が「気迫」なら
                        {
                            if (Mine.StatusEffect.Check(63))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 352:
                        // もし自分が「バーサーク」なら
                        {
                            if (Mine.StatusEffect.Check(68))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 355:
                        // もし自分が「血の盟約」なら
                        {
                            if (Mine.StatusEffect.Check(69))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 358:
                        // もし自分が「獅子奮迅」なら
                        {
                            if (Mine.StatusEffect.Check(70))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 361:
                        // もし自分が「ガーディア」なら
                        {
                            if (Mine.StatusEffect.Check(81))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 364:
                        // もし自分が「アームフォート」なら
                        {
                            if (Mine.StatusEffect.Check(85))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 367:
                        // もし自分が「ラストリゾート」なら
                        {
                            if (Mine.StatusEffect.Check(86))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 370:
                        // もし自分が「ウォークライ」なら
                        {
                            if (Mine.StatusEffect.Check(90))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 373:
                        // もし自分が「ファランクス」なら
                        {
                            if (Mine.StatusEffect.Check(91))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 376:
                        // もし自分が「ガーディアルアーツ」なら
                        {
                            if (Mine.StatusEffect.Check(92))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 379:
                        // もし自分が「ターゲットサイト」なら
                        {
                            if (Mine.StatusEffect.Check(93))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 382:
                        // もし自分が「意気衝天」なら
                        {
                            if (Mine.StatusEffect.Check(94))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 385:
                        // もし自分が「特殊装填」なら
                        {
                            if (Mine.StatusEffect.Check(95))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 388:
                        // もし自分が「秘密道具」なら
                        {
                            if (Mine.StatusEffect.Check(96))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 391:
                        // もし自分が「ワイドアイテム」なら
                        {
                            if (Mine.StatusEffect.Check(97))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 397:
                        // もし自分が「コレオグラフ」なら
                        {
                            if (Mine.StatusEffect.Check(101))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 418:
                        // 「ダンス・ポルカ」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(230))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 421:
                        // 「ダンス・レンゲ」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(231))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 424:
                        // 「ダンス・アンフェイス」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(232))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 427:
                        // 「ダンス・クエーカ」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(233))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 430:
                        // もし自分が「シャドウサーバント」なら
                        {
                            if (Mine.StatusEffect.Check(251))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 433:
                        // もし自分が「マナプール」なら
                        {
                            if (Mine.StatusEffect.Check(252))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 436:
                        // もし自分が「聖障壁」なら
                        {
                            if (Mine.StatusEffect.Check(253))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 439:
                        // もし自分が「フォースソウル」なら
                        {
                            if (Mine.StatusEffect.Check(257))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 442:
                        // もし自分が「夢幻闘武」なら
                        {
                            if (Mine.StatusEffect.Check(258))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 445:
                        // もし自分が「ブラッドスタンス」なら
                        {
                            if (Mine.StatusEffect.Check(259))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 448:
                        // もし自分が「森羅万象」なら
                        {
                            if (Mine.StatusEffect.Check(261))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 451:
                        // もし自分が「エアウィング」なら
                        {
                            if (Mine.StatusEffect.Check(264))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 454:
                        // もし自分が「リバースデスティニー」なら
                        {
                            if (Mine.StatusEffect.Check(265))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 457:
                        // もし自分が「ビーストフォーム」なら
                        {
                            if (Mine.StatusEffect.Check(266))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 460:
                        // もし自分が「フォレストガーヴ」なら
                        {
                            if (Mine.StatusEffect.Check(267))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 463:
                        // もし自分が「エレメンタルサイフォン」なら
                        {
                            if (Mine.StatusEffect.Check(268))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 466:
                        // もし自分が「ドラゴンズスケイル」なら
                        {
                            if (Mine.StatusEffect.Check(269))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 469:
                        // 「エンゲージ」の敵
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.StatusEffect.Check(945))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 472:
                        // もし自分が「ステルス」なら
                        {
                            if (Mine.StatusEffect.Check(946))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 475:
                        // もし自分が「アピール」なら
                        {
                            if (Mine.StatusEffect.Check(947))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 479:
                        // もし自分が「エピーズ」なら
                        {
                            if (Mine.StatusEffect.Check(948))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    #region ターゲット：味方
                    case 114:
                        // 目の前の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.BattleFormation == Status.Formation.Foward)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 115:
                        // 味方A
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.MemberNumber == 0)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 116:
                        // 味方B
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.MemberNumber == 1)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 117:
                        // 味方C
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.MemberNumber == 2)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 118:
                        // 味方D
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.MemberNumber == 3)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 119:
                        // 味方E
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.MemberNumber == 4)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 120:
                        // 味方F
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.MemberNumber == 5)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 121:
                        // 味方一人
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            TempTargetBattle.Add(Target);
                        }
                        break;
                    case 122:
                        // HP<100%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.HPDamageRateWithHeal < 100 && Target.HPDamageRateWithHeal < Prob)
                                {
                                    Prob = Target.HPDamageRateWithHeal;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 123:
                        // HP<90%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.HPDamageRateWithHeal < 90 && Target.HPDamageRateWithHeal < Prob)
                                {
                                    Prob = Target.HPDamageRateWithHeal;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 124:
                        // HP<80%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.HPDamageRateWithHeal < 80 && Target.HPDamageRateWithHeal < Prob)
                                {
                                    Prob = Target.HPDamageRateWithHeal;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 125:
                        // HP<70%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.HPDamageRateWithHeal < 70 && Target.HPDamageRateWithHeal < Prob)
                                {
                                    Prob = Target.HPDamageRateWithHeal;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 126:
                        // HP<60%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.HPDamageRateWithHeal < 60 && Target.HPDamageRateWithHeal < Prob)
                                {
                                    Prob = Target.HPDamageRateWithHeal;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 127:
                        // HP<50%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.HPDamageRateWithHeal < 50 && Target.HPDamageRateWithHeal < Prob)
                                {
                                    Prob = Target.HPDamageRateWithHeal;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 128:
                        // HP<40%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.HPDamageRateWithHeal < 40 && Target.HPDamageRateWithHeal < Prob)
                                {
                                    Prob = Target.HPDamageRateWithHeal;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 129:
                        // HP<30%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.HPDamageRateWithHeal < 30 && Target.HPDamageRateWithHeal < Prob)
                                {
                                    Prob = Target.HPDamageRateWithHeal;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 130:
                        // HP<20%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.HPDamageRateWithHeal < 20 && Target.HPDamageRateWithHeal < Prob)
                                {
                                    Prob = Target.HPDamageRateWithHeal;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 131:
                        // HP<10%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.HPDamageRateWithHeal < 10 && Target.HPDamageRateWithHeal < Prob)
                                {
                                    Prob = Target.HPDamageRateWithHeal;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 132:
                        // MP<100%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.MPDamageRate < 100 && Target.MPDamageRate < Prob)
                                {
                                    Prob = Target.MPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 133:
                        // MP<90%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.MPDamageRate < 90 && Target.MPDamageRate < Prob)
                                {
                                    Prob = Target.MPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 134:
                        // MP<80%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.MPDamageRate < 80 && Target.MPDamageRate < Prob)
                                {
                                    Prob = Target.MPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 135:
                        // MP<70%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.MPDamageRate < 70 && Target.MPDamageRate < Prob)
                                {
                                    Prob = Target.MPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 136:
                        // MP<60%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.MPDamageRate < 60 && Target.MPDamageRate < Prob)
                                {
                                    Prob = Target.MPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 137:
                        // MP<50%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.MPDamageRate < 50 && Target.MPDamageRate < Prob)
                                {
                                    Prob = Target.MPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 138:
                        // MP<40%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.MPDamageRate < 40 && Target.MPDamageRate < Prob)
                                {
                                    Prob = Target.MPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 139:
                        // MP<30%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.MPDamageRate < 30 && Target.MPDamageRate < Prob)
                                {
                                    Prob = Target.MPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 140:
                        // MP<20%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.MPDamageRate < 20 && Target.MPDamageRate < Prob)
                                {
                                    Prob = Target.MPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 141:
                        // MP<10%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.MPDamageRate < 10 && Target.MPDamageRate < Prob)
                                {
                                    Prob = Target.MPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 142:
                        // TP<100%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.TPDamageRate < 100 && Target.TPDamageRate < Prob)
                                {
                                    Prob = Target.TPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 143:
                        // TP<90%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.TPDamageRate < 90 && Target.TPDamageRate < Prob)
                                {
                                    Prob = Target.TPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 144:
                        // TP<80%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.TPDamageRate < 80 && Target.TPDamageRate < Prob)
                                {
                                    Prob = Target.TPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 145:
                        // TP<70%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.TPDamageRate < 70 && Target.TPDamageRate < Prob)
                                {
                                    Prob = Target.TPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 146:
                        // TP<60%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.TPDamageRate < 60 && Target.TPDamageRate < Prob)
                                {
                                    Prob = Target.TPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 147:
                        // TP<50%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.TPDamageRate < 50 && Target.TPDamageRate < Prob)
                                {
                                    Prob = Target.TPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 148:
                        // TP<40%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.TPDamageRate < 40 && Target.TPDamageRate < Prob)
                                {
                                    Prob = Target.TPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 149:
                        // TP<30%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.TPDamageRate < 30 && Target.TPDamageRate < Prob)
                                {
                                    Prob = Target.TPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 150:
                        // TP<20%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.TPDamageRate < 20 && Target.TPDamageRate < Prob)
                                {
                                    Prob = Target.TPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 151:
                        // TP<10%の味方
                        {
                            int Prob = 100;
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.TPDamageRate < 10 && Target.TPDamageRate < Prob)
                                {
                                    Prob = Target.TPDamageRate;
                                    TempTargetBattle.Clear();
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 152:
                        // 最もHPの低い味方
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.HPNow < Numbers)
                            {
                                Numbers = Target.HPNow;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 153:
                        // 最もTPの低い味方
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.TPNow < Numbers)
                            {
                                Numbers = Target.TPNow;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 154:
                        // 最もMPの低い味方
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.MPNow < Numbers)
                            {
                                Numbers = Target.MPNow;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 155:
                        // 最も武器の強い味方
                        Numbers = 0;
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.ATK > Numbers)
                            {
                                Numbers = Target.ATK;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 158:
                        // 最も物理防御の低い味方
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.DFE < Numbers)
                            {
                                Numbers = Target.DFE;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 159:
                        // 最も魔法防御の低い味方
                        Numbers = int.MaxValue;
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.MGR < Numbers)
                            {
                                Numbers = Target.MGR;
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 160:
                        // 「戦闘不能」の味方
                        foreach (LibUnitBase Target in Friends)
                        {
                            if (Target.StatusEffect.Check(1))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 161:
                        // 「睡眠」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(2))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 162:
                        // 「猛毒」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(3))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 164:
                        // 「呪詛」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(5))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 165:
                        // 「麻痺」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(6))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 166:
                        // 「暗闇」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(7))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 167:
                        // 「沈黙」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(8))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 168:
                        // 「骨折」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(9))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 169:
                        // 「石化」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(10))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 170:
                        // 「病気」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(11))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 171:
                        // 「混乱」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(12))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 172:
                        // 「恐怖」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(13))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 173:
                        // 「暴走」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(14))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 174:
                        // 「逆転」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(15))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 175:
                        // 「スロウ」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(18))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 176:
                        // 「ヘイスト」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(53))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 177:
                        // 「リジェネ」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(50))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 178:
                        // 「マナライズ」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(51))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 179:
                        // 「リフレッシュ」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(52))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 183:
                        // 「ウォール」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(57))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 188:
                        // 「シャープネス」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(71))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 190:
                        // 「プロテクト」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(73))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 191:
                        // 「アスティオン」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(74))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 192:
                        // 「バリアー」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(75))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 193:
                        // 「魅了」の味方
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.StatusEffect.Check(20))
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 318:
                        // 残り>=10個なら味方へ
                        {
                            if (Mine.GetType() != typeof(LibPlayer))
                            {
                                break;
                            }

                            if (Actions == Status.ActionType.NoAction)
                            {
                                break;
                            }
                            else
                            {
                                CommonSkillEntity.skill_listRow ArtsRow2 = LibSkill.GetSkillRow(ActionRow.perks_id);

                                EffectListEntity.effect_listDataTable ArtsEffectTable2 = new EffectListEntity.effect_listDataTable();
                                LibEffect.Split(ArtsRow2.sk_effect, ref ArtsEffectTable2);
                                EffectListEntity.effect_listRow ArtsEffectRow = ArtsEffectTable2.FindByeffect_id(920);
                                if (ArtsEffectRow != null)
                                {
                                    if (!((LibPlayer)Mine).CheckHaveItem(Status.ItemBox.Normal, (int)ArtsEffectRow.rank, false, 10))
                                    {
                                        break;
                                    }
                                }
                            }

                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 331:
                        // 「影縛」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(16))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 334:
                        // 「石化中」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(21))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 337:
                        // 「死霊の誘い」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(22))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 340:
                        // 「ブラッドロス」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(24))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 343:
                        // 「インスペクト」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(25))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 346:
                        // 「分身」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(62))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 350:
                        // 「バーサーク」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(68))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 353:
                        // 「血の盟約」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(69))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 356:
                        // 「獅子奮迅」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(70))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 359:
                        // 「ガーディア」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(81))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 362:
                        // 「アームフォート」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(85))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 365:
                        // 「ラストリゾート」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(86))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 368:
                        // 「ウォークライ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(90))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 371:
                        // 「ファランクス」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(91))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 374:
                        // 「ガーディアルアーツ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(92))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 377:
                        // 「ターゲットサイト」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(93))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 380:
                        // 「意気衝天」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(94))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 383:
                        // 「特殊装填」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(95))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 386:
                        // 「秘密道具」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(96))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 389:
                        // 「ワイドアイテム」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(97))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 394:
                        // もし自分が「スタンドバイミー」なら
                        {
                            if (Mine.StatusEffect.Check(100))
                            {
                                foreach (LibUnitBase Target in FriendsLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 392:
                        // 「スタンドバイミー」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(100))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 395:
                        // 「コレオグラフ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(101))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 400:
                        // もし自分が「防壁のプロローグ」なら
                        {
                            if (Mine.StatusEffect.Check(200))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 398:
                        // 「防壁のプロローグ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(200))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 403:
                        // もし自分が「勇猛のミンネザンク」なら
                        {
                            if (Mine.StatusEffect.Check(201))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 401:
                        // 「勇猛のミンネザンク」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(201))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 406:
                        // もし自分が「兵糧のピーオン」なら
                        {
                            if (Mine.StatusEffect.Check(202))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 404:
                        // 「兵糧のピーオン」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(202))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 409:
                        // もし自分が「魔導のマドリガーレ」なら
                        {
                            if (Mine.StatusEffect.Check(203))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 407:
                        // 「魔導のマドリガーレ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(203))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 412:
                        // もし自分が「妖術のスレノディ」なら
                        {
                            if (Mine.StatusEffect.Check(204))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 410:
                        // 「妖術のスレノディ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(204))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 415:
                        // もし自分が「逆襲のサンバ」なら
                        {
                            if (Mine.StatusEffect.Check(205))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 413:
                        // 「逆襲のサンバ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(205))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 416:
                        // 「ダンス・ポルカ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(230))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 419:
                        // 「ダンス・レンゲ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(231))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 422:
                        // 「ダンス・アンフェイス」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(232))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 425:
                        // 「ダンス・クエーカ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(233))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 428:
                        // 「シャドウサーバント」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(251))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 431:
                        // 「マナプール」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(252))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 434:
                        // 「聖障壁」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(253))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 437:
                        // 「フォースソウル」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(257))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 440:
                        // 「夢幻闘武」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(258))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 443:
                        // 「ブラッドスタンス」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(259))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 446:
                        // 「森羅万象」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(261))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 449:
                        // 「エアウィング」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(264))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 452:
                        // 「リバースデスティニー」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(265))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 455:
                        // 「ビーストフォーム」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(266))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 458:
                        // 「フォレストガーヴ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(267))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 461:
                        // 「エレメンタルサイフォン」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(268))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 464:
                        // 「ドラゴンズスケイル」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(269))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 467:
                        // 「エンゲージ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(945))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 470:
                        // 「ステルス」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(946))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 473:
                        // 「アピール」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(947))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 476:
                        // もし自分が「アピール」なら
                        {
                            if (Mine.StatusEffect.Check(947))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    case 477:
                        // 「エピーズ」の味方
                        {
                            foreach (LibUnitBase Target in FriendsLive)
                            {
                                if (Target.StatusEffect.Check(948))
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 480:
                        // もし自分が「エピーズ」なら
                        {
                            if (Mine.StatusEffect.Check(948))
                            {
                                foreach (LibUnitBase Target in EnemysLive)
                                {
                                    if (Target.BattleFormation == Status.Formation.Foward)
                                    {
                                        TempTargetBattle.Add(Target);
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    #region ターゲット：自分
                    case 194:
                        // 自分自身
                        TempTargetBattle.Add(Mine);
                        break;
                    case 195:
                        // HP<100%の自分
                        if (Mine.HPDamageRateWithHeal < 100)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 196:
                        // HP<90%の自分
                        if (Mine.HPDamageRateWithHeal < 90)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 197:
                        // HP<80%の自分
                        if (Mine.HPDamageRateWithHeal < 80)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 198:
                        // HP<70%の自分
                        if (Mine.HPDamageRateWithHeal < 70)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 199:
                        // HP<60%の自分
                        if (Mine.HPDamageRateWithHeal < 60)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 200:
                        // HP<50%の自分
                        if (Mine.HPDamageRateWithHeal < 50)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 201:
                        // HP<40%の自分
                        if (Mine.HPDamageRateWithHeal < 40)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 202:
                        // HP<30%の自分
                        if (Mine.HPDamageRateWithHeal < 30)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 203:
                        // HP<20%の自分
                        if (Mine.HPDamageRateWithHeal < 20)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 204:
                        // HP<10%の自分
                        if (Mine.HPDamageRateWithHeal < 10)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 205:
                        // MP<100%の自分
                        if (Mine.MPDamageRate < 100)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 206:
                        // MP<90%の自分
                        if (Mine.MPDamageRate < 90)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 207:
                        // MP<80%の自分
                        if (Mine.MPDamageRate < 80)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 208:
                        // MP<70%の自分
                        if (Mine.MPDamageRate < 70)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 209:
                        // MP<60%の自分
                        if (Mine.MPDamageRate < 60)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 210:
                        // MP<50%の自分
                        if (Mine.MPDamageRate < 50)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 211:
                        // MP<40%の自分
                        if (Mine.MPDamageRate < 40)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 212:
                        // MP<30%の自分
                        if (Mine.MPDamageRate < 30)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 213:
                        // MP<20%の自分
                        if (Mine.MPDamageRate < 20)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 214:
                        // MP<10%の自分
                        if (Mine.MPDamageRate < 10)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 215:
                        // TP<100%の自分
                        if (Mine.TPDamageRate < 100)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 216:
                        // TP<90%の自分
                        if (Mine.TPDamageRate < 90)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 217:
                        // TP<80%の自分
                        if (Mine.TPDamageRate < 80)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 218:
                        // TP<70%の自分
                        if (Mine.TPDamageRate < 70)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 219:
                        // TP<60%の自分
                        if (Mine.TPDamageRate < 60)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 220:
                        // TP<50%の自分
                        if (Mine.TPDamageRate < 50)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 221:
                        // TP<40%の自分
                        if (Mine.TPDamageRate < 40)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 222:
                        // TP<30%の自分
                        if (Mine.TPDamageRate < 30)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 223:
                        // TP<21%の自分
                        if (Mine.TPDamageRate < 21)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 224:
                        // TP<10%の自分
                        if (Mine.TPDamageRate < 10)
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 225:
                        // 「猛毒」の自分
                        if (Mine.StatusEffect.Check(3))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 227:
                        // 「呪詛」の自分
                        if (Mine.StatusEffect.Check(5))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 228:
                        // 「麻痺」の自分
                        if (Mine.StatusEffect.Check(6))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 229:
                        // 「暗闇」の自分
                        if (Mine.StatusEffect.Check(7))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 230:
                        // 「沈黙」の自分
                        if (Mine.StatusEffect.Check(8))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 231:
                        // 「骨折」の自分
                        if (Mine.StatusEffect.Check(9))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 232:
                        // 「病気」の自分
                        if (Mine.StatusEffect.Check(11))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 233:
                        // 「逆転」の自分
                        if (Mine.StatusEffect.Check(15))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 234:
                        // 「スロウ」の自分
                        if (Mine.StatusEffect.Check(18))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 235:
                        // 「ヘイスト」の自分
                        if (Mine.StatusEffect.Check(53))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 236:
                        // 「リジェネ」の自分
                        if (Mine.StatusEffect.Check(50))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 237:
                        // 「マナライズ」の自分
                        if (Mine.StatusEffect.Check(51))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 238:
                        // 「リフレッシュ」の自分
                        if (Mine.StatusEffect.Check(52))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 242:
                        // 「ウォール」の自分
                        if (Mine.StatusEffect.Check(57))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 247:
                        // 「シャープネス」の自分
                        if (Mine.StatusEffect.Check(71))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 249:
                        // 「プロテクト」の自分
                        if (Mine.StatusEffect.Check(73))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 250:
                        // 「アスティオン」の自分
                        if (Mine.StatusEffect.Check(74))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 251:
                        // 「バリアー」の自分
                        if (Mine.StatusEffect.Check(75))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 332:
                        // 「影縛」の自分
                        if (Mine.StatusEffect.Check(16))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 335:
                        // 「石化中」の自分
                        if (Mine.StatusEffect.Check(21))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 338:
                        // 「死霊の誘い」の自分
                        if (Mine.StatusEffect.Check(22))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 341:
                        // 「ブラッドロス」の自分
                        if (Mine.StatusEffect.Check(24))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 344:
                        // 「インスペクト」の自分
                        if (Mine.StatusEffect.Check(25))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 347:
                        // 「分身」の自分
                        if (Mine.StatusEffect.Check(25))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 351:
                        // 「バーサーク」の自分
                        if (Mine.StatusEffect.Check(68))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 354:
                        // 「血の盟約」の自分
                        if (Mine.StatusEffect.Check(69))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 357:
                        // 「獅子奮迅」の自分
                        if (Mine.StatusEffect.Check(70))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 360:
                        // 「ガーディア」の自分
                        if (Mine.StatusEffect.Check(81))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 363:
                        // 「アームフォート」の自分
                        if (Mine.StatusEffect.Check(85))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 366:
                        // 「ラストリゾート」の自分
                        if (Mine.StatusEffect.Check(86))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 369:
                        // 「ウォークライ」の自分
                        if (Mine.StatusEffect.Check(90))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 372:
                        // 「ファランクス」の自分
                        if (Mine.StatusEffect.Check(91))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 375:
                        // 「ガーディアルアーツ」の自分
                        if (Mine.StatusEffect.Check(92))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 378:
                        // 「ターゲットサイト」の自分
                        if (Mine.StatusEffect.Check(93))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 381:
                        // 「意気衝天」の自分
                        if (Mine.StatusEffect.Check(94))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 384:
                        // 「特殊装填」の自分
                        if (Mine.StatusEffect.Check(95))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 387:
                        // 「秘密道具」の自分
                        if (Mine.StatusEffect.Check(96))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 390:
                        // 「ワイドアイテム」の自分
                        if (Mine.StatusEffect.Check(97))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 393:
                        // 「スタンドバイミー」の自分
                        if (Mine.StatusEffect.Check(100))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 396:
                        // 「コレオグラフ」の自分
                        if (Mine.StatusEffect.Check(101))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 399:
                        // 「防壁のプロローグ」の自分
                        if (Mine.StatusEffect.Check(200))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 402:
                        // 「勇猛のミンネザンク」の自分
                        if (Mine.StatusEffect.Check(201))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 405:
                        // 「兵糧のピーオン」の自分
                        if (Mine.StatusEffect.Check(202))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 408:
                        // 「魔導のマドリガーレ」の自分
                        if (Mine.StatusEffect.Check(203))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 411:
                        // 「妖術のスレノディ」の自分
                        if (Mine.StatusEffect.Check(204))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 414:
                        // 「逆襲のサンバ」の自分
                        if (Mine.StatusEffect.Check(205))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 417:
                        // 「ダンス・ポルカ」の自分
                        if (Mine.StatusEffect.Check(230))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 420:
                        // 「ダンス・レンゲ」の自分
                        if (Mine.StatusEffect.Check(231))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 423:
                        // 「ダンス・アンフェイス」の自分
                        if (Mine.StatusEffect.Check(232))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 426:
                        // 「ダンス・クエーカ」の自分
                        if (Mine.StatusEffect.Check(233))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 429:
                        // 「シャドウサーバント」の自分
                        if (Mine.StatusEffect.Check(251))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 432:
                        // 「マナプール」の自分
                        if (Mine.StatusEffect.Check(252))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 435:
                        // 「聖障壁」の自分
                        if (Mine.StatusEffect.Check(253))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 438:
                        // 「フォースソウル」の自分
                        if (Mine.StatusEffect.Check(257))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 441:
                        // 「夢幻闘武」の自分
                        if (Mine.StatusEffect.Check(258))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 444:
                        // 「ブラッドスタンス」の自分
                        if (Mine.StatusEffect.Check(259))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 447:
                        // 「森羅万象」の自分
                        if (Mine.StatusEffect.Check(261))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 450:
                        // 「エアウィング」の自分
                        if (Mine.StatusEffect.Check(264))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 453:
                        // 「リバースデスティニー」の自分
                        if (Mine.StatusEffect.Check(265))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 456:
                        // 「ビーストフォーム」の自分
                        if (Mine.StatusEffect.Check(266))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 459:
                        // 「フォレストガーヴ」の自分
                        if (Mine.StatusEffect.Check(267))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 462:
                        // 「エレメンタルサイフォン」の自分
                        if (Mine.StatusEffect.Check(268))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 465:
                        // 「ドラゴンズスケイル」の自分
                        if (Mine.StatusEffect.Check(269))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 468:
                        // 「エンゲージ」の自分
                        if (Mine.StatusEffect.Check(945))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 471:
                        // 「ステルス」の自分
                        if (Mine.StatusEffect.Check(946))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 474:
                        // 「アピール」の自分
                        if (Mine.StatusEffect.Check(947))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    case 478:
                        // 「エピーズ」の自分
                        if (Mine.StatusEffect.Check(948))
                        {
                            TempTargetBattle.Add(Mine);
                        }
                        break;
                    #endregion
                    #region ターゲット：特殊
                    case 325:
                        // エリアルドを狙う
                        foreach (LibUnitBase Target in EnemysLive)
                        {
                            if (Target.GetType() == typeof(LibGuest) &&
                                (Target.EntryNo == 6 || Target.EntryNo == 7))
                            {
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }

                        if (TempTargetBattle.Count == 0)
                        {
                            foreach (LibUnitBase Target in EnemysLive)
                            {
                                if (Target.BattleFormation == Status.Formation.Foward)
                                {
                                    TempTargetBattle.Add(Target);
                                }
                            }
                        }
                        break;
                    case 326:
                        // ゲラールのHP70%以下
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.GetType() == typeof(LibMonster) &&
                                Target.EntryNo == 52 &&
                                Target.HPDamageRateWithHeal <= 70)
                            {
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    case 327:
                        // ゲラールに
                        foreach (LibUnitBase Target in FriendsLive)
                        {
                            if (Target.GetType() == typeof(LibMonster) &&
                                Target.EntryNo == 52)
                            {
                                TempTargetBattle.Clear();
                                TempTargetBattle.Add(Target);
                            }
                        }
                        break;
                    #endregion
                }
                #endregion

                // ステータス異常による変化
                // エンゲージ
                if (TargetType == Status.TargetParty.Enemy && Mine.StatusEffect.Check(945))
                {
                    LibUnitBase TargetIsThis = EnemysLive.Find(TargetIs => TargetIs.BattleID == Mine.StatusEffect.GetRank(945));
                    if (TargetIsThis != null)
                    {
                        TempTargetBattle.Clear();
                        TempTargetBattle.Add(TargetIsThis);
                    }
                }
                // ステルス
                TempTargetBattle.RemoveAll(Stels => Stels.StatusEffect.Check(946) == true);

                if (TempTargetBattle.Count == 0)
                {
                    // ターゲットが無意味なのでスキップ
                    continue;
                }

                #region アクション内容の正規化判定
                switch (Actions)
                {
                    case Status.ActionType.NoAction:
                        // 行動しない
                        TempTargetBattle = new List<LibUnitBase>();
                        TempTargetBattle.Add(Mine);// ターゲットは自分
                        break;
                    case Status.ActionType.MainAttack:
                        // 物理攻撃
                        if (Mine.GetType() != typeof(LibMonster))
                        {
                            // 物理禁止
                            if (FieldRow.not_attack)
                            {
                                ActionSettings = false;
                            }
                        }
                        break;
                    default:
                        // アーツその他
                        string EffectStr = "";
                        bool IsArtsAtk = false;
                        ArtsEffectTable.Clear();

                        // アーツを使用できるかチェック
                        if (Mine.GetType() == typeof(LibPlayer))
                        {
                            bool IsUsingOK = false;

                            // インストールクラス
                            {
                                InstallDataEntity.mt_install_class_skillRow InstallSkillRow = LibInstall.Entity.mt_install_class_skill.FindByinstall_idperks_id(((LibPlayer)Mine).IntallClassID, ActionRow.perks_id);
                                if (InstallSkillRow != null && InstallSkillRow.install_level <= ((LibPlayer)Mine).InstallClassLevel)
                                {
                                    // インストールクラスで使える！
                                    IsUsingOK = true;
                                }

                                if (((LibPlayer)Mine).SecondryInstallClassLevel > 0)
                                {
                                    InstallDataEntity.mt_install_class_skillRow SecondryInstallSkillRow = LibInstall.Entity.mt_install_class_skill.FindByinstall_idperks_id(((LibPlayer)Mine).SecondryIntallClassID, ActionRow.perks_id);
                                    if (SecondryInstallSkillRow != null && SecondryInstallSkillRow.install_level <= ((LibPlayer)Mine).SecondryInstallClassLevel)
                                    {
                                        // インストールクラスで使える！
                                        IsUsingOK = true;
                                    }
                                }
                            }

                            // 所持スキル
                            if (((LibPlayer)Mine).CheckHaveSkill(ActionRow.perks_id))
                            {
                                IsUsingOK = true;
                            }

                            if (!IsUsingOK)
                            {
                                ActionSettings = false;
                            }

                            // 現在のクラス、武器で使用可能なアーツかどうか
                            if (((LibPlayer)Mine).UsingSkillList.FindBysk_id(ActionRow.perks_id) == null)
                            {
                                ActionSettings = false;
                            }
                        }

                        ArtsRow = LibSkill.GetSkillRow(ActionRow.perks_id);

                        if (ArtsRow != null)
                        {
                            EffectStr = ArtsRow.sk_effect;

                            LibEffect.Split(EffectStr, ref ArtsEffectTable);

                            if (Actions == Status.ActionType.SpecialArtsAttack)
                            {
                                if (ArtsRow.sk_type != Status.SkillType.Special)
                                {
                                    // スペシャル以外の場合はスキップ
                                    ActionSettings = false;
                                }
                                if ((ArtsRow.sk_target_party != Status.TargetParty.Pet && ArtsRow.sk_target_party != Status.TargetParty.All && ArtsRow.sk_target_party == Status.TargetParty.Friend && (Status.TargetParty.Friend != TargetType && Status.TargetParty.Mine != TargetType)) ||
                                    (ArtsRow.sk_target_party != Status.TargetParty.Pet && ArtsRow.sk_target_party != Status.TargetParty.All && ArtsRow.sk_target_party != Status.TargetParty.Friend && ArtsRow.sk_target_party != TargetType) ||
                                    (ArtsRow.sk_target_party == Status.TargetParty.Pet && TargetType != Status.TargetParty.Friend))
                                {
                                    ActionSettings = false;
                                }

                                if (Mine.GetType() == typeof(LibPlayer) && ((LibPlayer)Mine).ContinueBonus < 4)
                                {
                                    ActionSettings = false;
                                }

                                if (Mine.IsSpecialUsed)
                                {
                                    ActionSettings = false;
                                }

                                if (Mine.GetType() == typeof(LibPlayer) && ((LibPlayer)Mine).IsInstallClassChanging)
                                {
                                    ActionSettings = false;
                                }

                                if (ArtsRow.sk_power > 0 || ArtsRow.sk_damage_rate > 0 || ArtsRow.sk_plus_score > 0)
                                {
                                    IsArtsAtk = true;
                                }
                            }
                            else
                            {
                                if (ArtsRow.sk_type != Status.SkillType.Arts)
                                {
                                    // アーツ以外の場合はスキップ
                                    ActionSettings = false;
                                }

                                if ((ArtsRow.sk_target_party != Status.TargetParty.Pet && ArtsRow.sk_target_party != Status.TargetParty.All && ArtsRow.sk_target_party == Status.TargetParty.Friend && (Status.TargetParty.Friend != TargetType && Status.TargetParty.Mine != TargetType)) ||
                                    (ArtsRow.sk_target_party != Status.TargetParty.Pet && ArtsRow.sk_target_party != Status.TargetParty.All && ArtsRow.sk_target_party != Status.TargetParty.Friend && ArtsRow.sk_target_party != TargetType) ||
                                    (ArtsRow.sk_target_party == Status.TargetParty.Pet && TargetType != Status.TargetParty.Friend))
                                {
                                    ActionSettings = false;
                                }

                                // コスト判定
                                int CostHP = 0;
                                int CostMP = ArtsRow.sk_mp;
                                int CostTP = ArtsRow.sk_tp;

                                // HP消費計算
                                {
                                    EffectListEntity.effect_listRow EffectRow = ArtsEffectTable.FindByeffect_id(855);
                                    if (EffectRow != null)
                                    {
                                        if ((int)EffectRow.sub_rank == 1)
                                        {
                                            CostHP = (int)((decimal)Mine.HPMax * EffectRow.rank / 100m);
                                        }
                                        else if ((int)EffectRow.sub_rank == 2)
                                        {
                                            CostHP = (int)((decimal)Mine.HPNow * EffectRow.rank / 100m);
                                        }
                                    }
                                }
                                // サクリファイス
                                {
                                    EffectListEntity.effect_listRow EffectRow = ArtsEffectTable.FindByeffect_id(856);
                                    if (EffectRow != null)
                                    {
                                        if ((int)EffectRow.sub_rank == 1)
                                        {
                                            CostHP = (int)((decimal)Mine.HPMax * EffectRow.rank / 100m);
                                        }
                                        else if ((int)EffectRow.sub_rank == 2)
                                        {
                                            CostHP = (int)((decimal)Mine.HPNow * EffectRow.rank / 100m);
                                        }
                                    }
                                }
                                // デヴォーション
                                {
                                    EffectListEntity.effect_listRow EffectRow = ArtsEffectTable.FindByeffect_id(857);
                                    if (EffectRow != null)
                                    {
                                        if ((int)EffectRow.sub_rank == 1)
                                        {
                                            CostHP = (int)((decimal)Mine.HPMax * EffectRow.rank / 100m);
                                        }
                                        else if ((int)EffectRow.sub_rank == 2)
                                        {
                                            CostHP = (int)((decimal)Mine.HPNow * EffectRow.rank / 100m);
                                        }
                                    }
                                }

                                // 消費量軽減
                                {
                                    // MP消費軽減
                                    EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(853);
                                    if (EffectRow != null)
                                    {
                                        CostMP -= (int)((decimal)CostMP * EffectRow.rank / 100m);
                                    }
                                }

                                // エレメンタルフィット
                                if (Mine.EffectList.FindByeffect_id(2134) != null &&
                                    ArtsRow.sk_arts_category == LibSkillType.FindByName("精霊魔法"))
                                {
                                    LibActionType ActionTypeTemp = new LibActionType(ArtsRow, Status.ActionBaseType.MainAttack, ArtsEffectTable, false, Actions);
                                    if (ActionTypeTemp.CheckElementalByFit(((LibPlayer)Mine).GuardianInt))
                                    {
                                        CostMP -= (int)((decimal)CostMP * 0.1m);
                                    }
                                }

                                // スペシャル効果
                                if (Mine.StatusEffect.Check(252))
                                {
                                    // マナプール
                                    CostMP = 0;
                                }
                                if (Mine.StatusEffect.Check(261))
                                {
                                    // 森羅万象
                                    CostTP = 0;
                                }

                                if (Mine.StatusEffect.Check(254) &&
                                    (ArtsRow.sk_atype == Status.AttackType.Mystic ||
                                    ArtsRow.sk_atype == Status.AttackType.MagicSword))
                                {
                                    // マナファストリング
                                    CostMP *= 2;
                                }

                                if ((CostMP > 0 && Mine.MPNow < CostMP) || (CostTP > 0 && Mine.TPNow < CostTP) || (CostHP > 0 && Mine.HPNow <= CostHP))
                                {
                                    ActionSettings = false;
                                }

                                if (Mine.GetType() != typeof(LibMonster))
                                {
                                    switch (ArtsRow.sk_atype)
                                    {
                                        case Status.AttackType.Combat:
                                        case Status.AttackType.Shoot:
                                        case Status.AttackType.Dance:
                                        case Status.AttackType.Item:
                                            // 物理禁止
                                            if (FieldRow.not_attack)
                                            {
                                                ActionSettings = false;
                                            }
                                            break;
                                        case Status.AttackType.Mystic:
                                        case Status.AttackType.Song:
                                        case Status.AttackType.Ninjutsu:
                                        case Status.AttackType.Summon:
                                            // 魔法禁止
                                            if (FieldRow.not_magic)
                                            {
                                                ActionSettings = false;
                                            }
                                            break;
                                        case Status.AttackType.MagicSword:
                                        case Status.AttackType.Bless:
                                            // 物理禁止
                                            if (FieldRow.not_attack)
                                            {
                                                ActionSettings = false;
                                            }
                                            // 魔法禁止
                                            if (FieldRow.not_magic)
                                            {
                                                ActionSettings = false;
                                            }
                                            break;
                                    }
                                }

                                // 武器種別
                                if (Mine.GetType() == typeof(LibPlayer) && ArtsRow.sk_arts_category > 0)
                                {
                                    int WeaponsType = LibSkillType.GetTypeID(ArtsRow.sk_arts_category);
                                    ItemTypeEntity.mt_item_typeRow TypeRow = LibItemType.GetTypeRow(WeaponsType);

                                    if (TypeRow != null && TypeRow.equip_spot == Status.EquipSpot.Main && Mine.MainWeapon.ItemType != WeaponsType)
                                    {
                                        ActionSettings = false;
                                    }
                                }

                                // 攻撃対象条件
                                if (ArtsRow.sk_target_restrict > 0)
                                {
                                    List<LibUnitBase> TargetLists = new List<LibUnitBase>();
                                    TargetLists = TempTargetBattle;

                                    int TargetRestrict = ArtsRow.sk_target_restrict;

                                    TempTargetBattle = new List<LibUnitBase>();// ターゲット初期化
                                    foreach (LibUnitBase Tg in TargetLists)
                                    {
                                        if (Tg.Category == TargetRestrict)
                                        {
                                            TempTargetBattle.Add(Tg);
                                        }
                                    }

                                    if (TempTargetBattle.Count == 0)
                                    {
                                        // いなくなってしまった
                                        ActionSettings = false;
                                    }
                                }

                                // ステータス異常判定
                                if (Mine.StatusEffect.Check(8) &&
                                    (ArtsRow.sk_atype == Status.AttackType.Mystic ||
                                    ArtsRow.sk_atype == Status.AttackType.Summon ||
                                    ArtsRow.sk_atype == Status.AttackType.MagicSword ||
                                    ArtsRow.sk_atype == Status.AttackType.Ninjutsu ||
                                    ArtsRow.sk_atype == Status.AttackType.Song))
                                {
                                    ActionSettings = false;
                                }

                                if (Mine.StatusEffect.Check(9) &&
                                    (ArtsRow.sk_atype == Status.AttackType.Item ||
                                    ArtsRow.sk_atype == Status.AttackType.Dance ||
                                    ArtsRow.sk_atype == Status.AttackType.Combat ||
                                    ArtsRow.sk_atype == Status.AttackType.Shoot ||
                                    ArtsRow.sk_atype == Status.AttackType.MagicSword))
                                {
                                    ActionSettings = false;
                                }

                                // 使用条件
                                switch (ArtsRow.sk_use_limit)
                                {
                                    case Status.SkillUseLimit.OneHanded:
                                        // 片手武器装備
                                        {
                                            int ItemID = 0;
                                            bool ItemCreated = false;
                                            if (Mine.GetType() == typeof(LibPlayer))
                                            {
                                                CommonItemEntity.item_listRow ItemRow = ((LibPlayer)Mine).GetHaveItemEquiped(Status.EquipSpot.Main);
                                                if (ItemRow == null)
                                                {
                                                    ActionSettings = false;
                                                }
                                                else if (ItemRow.it_both_hand != false || ItemRow.it_attack_type != Status.AttackType.Combat)
                                                {
                                                    ActionSettings = false;
                                                }
                                            }
                                            else if (Mine.GetType() == typeof(LibGuest))
                                            {
                                                ItemID = ((LibGuest)Mine).HaveItemS[0].equip_main;
                                                if (ItemID == -1)
                                                {
                                                    ActionSettings = false;
                                                }
                                                CommonItemEntity.item_listRow ItemRow = LibItem.GetItemRow(ItemID, ItemCreated);
                                                if (ItemRow.it_both_hand != false || ItemRow.it_attack_type != Status.AttackType.Combat)
                                                {
                                                    ActionSettings = false;
                                                }
                                            }
                                        }
                                        break;
                                    case Status.SkillUseLimit.TwoHanded:
                                        // 両手武器装備
                                        {
                                            int ItemID = 0;
                                            bool ItemCreated = false;
                                            if (Mine.GetType() == typeof(LibPlayer))
                                            {
                                                CommonItemEntity.item_listRow ItemRow = ((LibPlayer)Mine).GetHaveItemEquiped(Status.EquipSpot.Main);
                                                if (ItemRow == null)
                                                {
                                                    ActionSettings = false;
                                                }
                                                else if (ItemRow.it_both_hand != true || ItemRow.it_attack_type != Status.AttackType.Combat)
                                                {
                                                    ActionSettings = false;
                                                }
                                            }
                                            else if (Mine.GetType() == typeof(LibGuest))
                                            {
                                                ItemID = ((LibGuest)Mine).HaveItemS[0].equip_main;
                                                if (ItemID == -1)
                                                {
                                                    ActionSettings = false;
                                                }
                                                CommonItemEntity.item_listRow ItemRow = LibItem.GetItemRow(ItemID, ItemCreated);
                                                if (ItemRow.it_both_hand != true || ItemRow.it_attack_type != Status.AttackType.Combat)
                                                {
                                                    ActionSettings = false;
                                                }
                                            }
                                        }
                                        break;
                                    case Status.SkillUseLimit.RangeWeapon:
                                        // 遠隔武器
                                        {
                                            int ItemID = 0;
                                            bool ItemCreated = false;
                                            if (Mine.GetType() == typeof(LibPlayer))
                                            {
                                                CommonItemEntity.item_listRow ItemRow = ((LibPlayer)Mine).GetHaveItemEquiped(Status.EquipSpot.Main);
                                                if (ItemRow == null)
                                                {
                                                    ActionSettings = false;
                                                }
                                                else if (ItemRow.it_attack_type != Status.AttackType.Shoot)
                                                {
                                                    ActionSettings = false;
                                                }
                                            }
                                            else if (Mine.GetType() == typeof(LibGuest))
                                            {
                                                ItemID = ((LibGuest)Mine).HaveItemS[0].equip_main;
                                                if (ItemID == -1)
                                                {
                                                    ActionSettings = false;
                                                }
                                                CommonItemEntity.item_listRow ItemRow = LibItem.GetItemRow(ItemID, ItemCreated);
                                                if (ItemRow.it_attack_type != Status.AttackType.Shoot)
                                                {
                                                    ActionSettings = false;
                                                }
                                            }
                                        }
                                        break;
                                    case Status.SkillUseLimit.Shield:
                                        // 盾装備
                                        {
                                            int ItemID = 0;
                                            bool ItemCreated = false;
                                            if (Mine.GetType() == typeof(LibPlayer))
                                            {
                                                CommonItemEntity.item_listRow ItemRow = ((LibPlayer)Mine).GetHaveItemEquiped(Status.EquipSpot.Sub);
                                                if (ItemRow == null)
                                                {
                                                    ActionSettings = false;
                                                }
                                                else if (ItemRow.it_type != 33)
                                                {
                                                    ActionSettings = false;
                                                }
                                            }
                                            else if (Mine.GetType() == typeof(LibGuest))
                                            {
                                                ItemID = ((LibGuest)Mine).HaveItemS[0].equip_sub;
                                                if (ItemID == -1)
                                                {
                                                    ActionSettings = false;
                                                }
                                                CommonItemEntity.item_listRow ItemRow = LibItem.GetItemRow(ItemID, ItemCreated);
                                                if (ItemRow.it_type != 33)
                                                {
                                                    ActionSettings = false;
                                                }
                                            }
                                        }
                                        break;
                                }

                                if (ArtsRow.sk_power > 0 || ArtsRow.sk_damage_rate > 0 || ArtsRow.sk_plus_score > 0)
                                {
                                    IsArtsAtk = true;
                                }
                            }
                        }
                        else
                        {
                            ActionSettings = false;
                        }

                        // 素の攻撃力に無関係に判定するもの
                        if (ActionSettings)
                        {
                            #region エフェクトによる状況判定(常に判定)
                            foreach (EffectListEntity.effect_listRow ArtsEffectRow in ArtsEffectTable)
                            {
                                int EffectID = ArtsEffectRow.effect_id;
                                decimal Rank = ArtsEffectRow.rank;
                                decimal SubRank = ArtsEffectRow.sub_rank;
                                decimal Prob = ArtsEffectRow.prob;
                                int EndLimit = ArtsEffectRow.endlimit;

                                switch (EffectID)
                                {
                                    case 855:
                                        // HP消費（割合）
                                        {
                                            if ((int)SubRank == 1)
                                            {
                                                // 最大HP割合
                                                if (Mine.HPDamageRate <= (int)Rank)
                                                {
                                                    ActionSettings = false;
                                                }
                                            }
                                        }
                                        break;
                                    case 920:
                                        // 指定されたアイテムを持っているか？
                                        if (Mine.GetType() == typeof(LibPlayer) && !((LibPlayer)Mine).CheckHaveItem(Status.ItemBox.Normal, (int)Rank, false))
                                        {
                                            ActionSettings = false;
                                        }
                                        break;
                                    case 1040:
                                        if (Mine.GetType() == typeof(LibPlayer) && ((LibPlayer)Mine).ContinueBonus < 4)
                                        {
                                            ActionSettings = false;
                                        }

                                        if (Mine.IsSpecialUsed)
                                        {
                                            ActionSettings = false;
                                        }

                                        if (Mine.GetType() == typeof(LibPlayer) && ((LibPlayer)Mine).IsInstallClassChanging)
                                        {
                                            ActionSettings = false;
                                        }
                                        break;
                                }
                            }
                            #endregion
                        }

                        // 回復行動の無駄遣い防止
                        if (ArtsRow != null && ArtsRow.sk_damage_type == Status.DamageType.Heal && IsArtsAtk)
                        {
                            List<LibUnitBase> TargetLists = new List<LibUnitBase>();

                            foreach (LibUnitBase Tg in TempTargetBattle)
                            {
                                if (Tg.HPMax > (Tg.HPNow + Tg.ReceivedHeal))
                                {
                                    TargetLists.Add(Tg);
                                }
                            }

                            if (TargetLists.Count > 0)
                            {
                                TempTargetBattle = new List<LibUnitBase>();
                                foreach (LibUnitBase Tgst in TargetLists)
                                {
                                    TempTargetBattle.Add(Tgst);
                                }
                            }
                        }

                        if (ActionSettings && !IsArtsAtk)
                        {
                            #region エフェクトによる状況判定(攻撃力0の場合のみ)
                            foreach (EffectListEntity.effect_listRow ArtsEffectRow in ArtsEffectTable)
                            {
                                int EffectID = ArtsEffectRow.effect_id;
                                decimal Rank = ArtsEffectRow.rank;
                                decimal Prob = ArtsEffectRow.prob;
                                int EndLimit = ArtsEffectRow.endlimit;
                                bool IsBreak = false;

                                switch (EffectID)
                                {
                                    case 840:
                                    case 843:
                                        #region HP回復
                                        {
                                            List<LibUnitBase> TargetLists = new List<LibUnitBase>();

                                            // ステータスチェック
                                            foreach (LibUnitBase Tg in TempTargetBattle)
                                            {
                                                if (Tg.HPMax > (Tg.HPNow + Tg.ReceivedHeal))
                                                {
                                                    TargetLists.Add(Tg);
                                                }
                                            }

                                            if (TargetLists.Count == 0)
                                            {
                                                // いなくなってしまった
                                                ActionSettings = false;
                                            }
                                            else
                                            {
                                                TempTargetBattle = new List<LibUnitBase>();
                                                foreach (LibUnitBase Tgst in TargetLists)
                                                {
                                                    TempTargetBattle.Add(Tgst);
                                                }
                                            }
                                        }
                                        #endregion
                                        break;
                                    case 841:
                                    case 844:
                                        #region MP回復
                                        {
                                            List<LibUnitBase> TargetLists = new List<LibUnitBase>();

                                            // ステータスチェック
                                            foreach (LibUnitBase Tg in TempTargetBattle)
                                            {
                                                if (Tg.MPMax > (Tg.MPNow + Tg.ReceivedHealMP))
                                                {
                                                    TargetLists.Add(Tg);
                                                }
                                            }

                                            if (TargetLists.Count == 0)
                                            {
                                                // いなくなってしまった
                                                ActionSettings = false;
                                            }
                                            else
                                            {
                                                TempTargetBattle = new List<LibUnitBase>();
                                                foreach (LibUnitBase Tgst in TargetLists)
                                                {
                                                    TempTargetBattle.Add(Tgst);
                                                }
                                            }
                                        }
                                        #endregion
                                        break;
                                    case 842:
                                        #region TP回復
                                        {
                                            List<LibUnitBase> TargetLists = new List<LibUnitBase>();

                                            // ステータスチェック
                                            foreach (LibUnitBase Tg in TempTargetBattle)
                                            {
                                                if (Tg.TPMax > (Tg.TPNow + Tg.ReceivedHealTP))
                                                {
                                                    TargetLists.Add(Tg);
                                                }
                                            }

                                            if (TargetLists.Count == 0)
                                            {
                                                // いなくなってしまった
                                                ActionSettings = false;
                                            }
                                            else
                                            {
                                                TempTargetBattle = new List<LibUnitBase>();
                                                foreach (LibUnitBase Tgst in TargetLists)
                                                {
                                                    TempTargetBattle.Add(Tgst);
                                                }
                                            }
                                        }
                                        #endregion
                                        break;
                                    case 945:
                                    case 946:
                                    case 947:
                                    case 948:
                                        // エンゲージ、ステルス、アピール、エピーズ
                                        {
                                            List<LibUnitBase> TargetLists = new List<LibUnitBase>();
                                            TargetLists = TempTargetBattle;

                                            // ステータスチェック
                                            TempTargetBattle = new List<LibUnitBase>();// ターゲット初期化
                                            foreach (LibUnitBase Tg in TargetLists)
                                            {
                                                if (!Tg.StatusEffect.Check(EffectID) && !Tg.ReceivedStatusEffect.Check(EffectID))
                                                {
                                                    TempTargetBattle.Add(Tg);
                                                }
                                            }

                                            if (TempTargetBattle.Count == 0)
                                            {
                                                // いなくなってしまった
                                                ActionSettings = false;
                                            }

                                            if (TempTargetBattle.Count > 0)
                                            {
                                                // これ以降は調べない
                                                IsBreak = true;
                                            }
                                        }
                                        break;
                                    default:
                                        {
                                            List<LibUnitBase> TargetLists = new List<LibUnitBase>();
                                            TargetLists = TempTargetBattle;
                                            bool IsStatus = false;

                                            if (EffectID > 0 && EffectID < 300)
                                            {
                                                IsStatus = true;

                                                // ステータスチェック
                                                TempTargetBattle = new List<LibUnitBase>();// ターゲット初期化
                                                foreach (LibUnitBase Tg in TargetLists)
                                                {
                                                    if (!Tg.StatusEffect.Check(EffectID) && !Tg.ReceivedStatusEffect.Check(EffectID))
                                                    {
                                                        TempTargetBattle.Add(Tg);
                                                    }
                                                }
                                            }
                                            else if (EffectID > 300 && EffectID < 500)
                                            {
                                                IsStatus = true;

                                                // ステータスチェック
                                                TempTargetBattle = new List<LibUnitBase>();// ターゲット初期化
                                                foreach (LibUnitBase Tg in TargetLists)
                                                {
                                                    if (Tg.StatusEffect.Check((EffectID - 300)) && Tg.ReceivedStatusEffect.Check((EffectID - 300)))
                                                    {
                                                        TempTargetBattle.Add(Tg);
                                                    }
                                                }
                                            }
                                            else if (EffectID > 500 && EffectID < 700)
                                            {
                                                IsStatus = true;

                                                // オートステータス
                                                TempTargetBattle = new List<LibUnitBase>();// ターゲット初期化
                                                foreach (LibUnitBase Tg in TargetLists)
                                                {
                                                    if (!Tg.StatusEffect.Check(EffectID) && !Tg.ReceivedStatusEffect.Check(EffectID))
                                                    {
                                                        TempTargetBattle.Add(Tg);
                                                    }
                                                }
                                            }

                                            if (IsStatus && TempTargetBattle.Count == 0)
                                            {
                                                // いなくなってしまった
                                                ActionSettings = false;
                                            }

                                            if (TempTargetBattle.Count > 0)
                                            {
                                                // これ以降は調べない
                                                IsBreak = true;
                                            }
                                        }
                                        break;
                                }

                                if (IsBreak)
                                {
                                    break;
                                }
                            }
                            #endregion
                        }
                        break;
                }
                #endregion

                // ターゲットを一人だけにする
                if (TempTargetBattle.Count > 1)
                {
                    LibUnitBase SelectBattleCh = TempTargetBattle[LibInteger.GetRand(TempTargetBattle.Count)];

                    TempTargetBattle = new List<LibUnitBase>();
                    TempTargetBattle.Add(SelectBattleCh);
                }

                if (ActionSettings)
                {
                    SelectedAction = Actions;
                    SelectedActionSkillNo = ActionRow.perks_id;
                    ActionInfo = true;
                    if (!ActionRow.Ismax_countNull() && ActionRow.max_count > 0)
                    {
                        ActionRow.use_count++;
                    }
                    break;
                }
            }
            #endregion

            // 行動対象のバトルID設定
            if (TempTargetBattle.Count > 0)
            {
                TempTargetBattle[0].TargetedCount++;
                Mine.OldSelectedTarget = Mine.SelectedTarget;
                Mine.SelectedTarget = TempTargetBattle;
            }

            // 行動内容アーツDataTable
            List<LibActionType> ActionArtsRows = new List<LibActionType>();

            // 行動内容の設定
            if (ActionInfo)
            {
                Mine.SelectedActions = BattleActionSelect.Select(Mine, SelectedAction, false, 0, false, SelectedActionSkillNo);
            }
            else
            {
                Mine.SelectedActions = BattleActionSelect.Select(Mine, Status.ActionType.NoAction, false, 0, false, 0);
            }

            // 最初の相手の回復見込みを設定。クリティカルなどは考慮しない数値で計算する！
            if (Mine.SelectedTarget.Count > 0 && !Mine.SelectedActions[0].IsNormalAttack && Mine.SelectedActions[0].DamageType == Status.DamageType.Heal)
            {
                foreach (LibUnitBase unitBase in Mine.SelectedTarget)
                {
                    int HealDrain = 0;
                    bool HealCritial = false;
                    int ElemeltalType = 0;
                    int HealCount = BattleDamage(Mine, unitBase, Mine.SelectedActions[0], ref HealDrain, ref HealCritial, 0, ref ElemeltalType, Mine.SelectedActions[0].DamageType, Turn, true);
                    if (HealCritial) { HealCount /= 2; }
                    unitBase.ReceivedHeal += HealCount;
                }
            }
            // 狙われた見込みの状態変化判定
            if (Mine.SelectedTarget.Count > 0 && !Mine.SelectedActions[0].IsNormalAttack && !Mine.SelectedActions[0].IsAttackArts)
            {
                foreach (LibUnitBase unitBase in Mine.SelectedTarget)
                {
                    foreach (EffectListEntity.effect_listRow ArtsEffectRow in Mine.SelectedActions[0].EffectList)
                    {
                        int EffectID = ArtsEffectRow.effect_id;
                        decimal Rank = ArtsEffectRow.rank;
                        decimal SubRank = ArtsEffectRow.sub_rank;
                        decimal Prob = ArtsEffectRow.prob;
                        int EndLimit = ArtsEffectRow.endlimit;

                        if (Prob < 100)
                        {
                            continue;
                        }

                        switch (EffectID)
                        {
                            case 840:
                                #region HP回復（固定値）
                                {
                                    unitBase.ReceivedHeal += (int)Rank;
                                }
                                #endregion
                                break;
                            case 841:
                                #region MP回復（固定値）
                                {
                                    unitBase.ReceivedHealMP += (int)Rank;
                                }
                                #endregion
                                break;
                            case 842:
                                #region TP回復（固定値）
                                {
                                    unitBase.ReceivedHealTP += (int)Rank;
                                }
                                #endregion
                                break;
                            case 843:
                                #region HP回復（割合）
                                {
                                    int CureHPRates = (int)((decimal)unitBase.HPMax * Rank / 100m);
                                    unitBase.ReceivedHeal += CureHPRates;
                                }
                                #endregion
                                break;
                            case 844:
                                #region MP回復（割合）
                                {
                                    int CureMPRates = (int)((decimal)unitBase.MPMax * Rank / 100m);
                                    unitBase.ReceivedHealMP += CureMPRates;
                                }
                                #endregion
                                break;
                            default:
                                if (EffectID > 0 && EffectID < 300)
                                {
                                    unitBase.ReceivedStatusEffect.AddWithRegist(EffectID, (int)Rank, (int)SubRank, EndLimit, Mine.Level, true);
                                }
                                else if (EffectID > 300 && EffectID < 500)
                                {
                                    unitBase.ReceivedStatusEffect.Delete((EffectID - 300));
                                }
                                else if (EffectID > 500 && EffectID < 700)
                                {
                                    unitBase.ReceivedStatusEffect.Add(EffectID, (int)Rank, (int)SubRank, EndLimit, Mine.Level, true);
                                }
                                break;
                        }
                    }
                }
            }

            // チャージタイムの確定
            int Charge = 0;
            if ((Mine.SelectedActions[0].IsNormalAttack && Mine.SelectedActions[0].ActionType == Status.ActionType.MainAttack) || Mine.SelectedActions[0].ActionType == Status.ActionType.NoAction)
            {
                Charge = Mine.MainWeapon.ChargeTime;
                if (Mine.ATKSub > 0 && Mine.SubWeapon.ChargeTime > Charge)
                {
                    Charge = Mine.SubWeapon.ChargeTime;
                }
            }
            else
            {
                Charge = Mine.SelectedActions[0].Charge;
            }

            // 行動時間決定
            decimal Initiative = (28m / (decimal)Mine.AGI + (decimal)Charge / 10m + (decimal)LibInteger.GetRandMax(5) / 10m);

            // 行動時間短縮効果
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(1050);
                if (EffectRow != null)
                {
                    switch ((int)EffectRow.rank)
                    {
                        case 1:
                            Initiative *= 0.88m;
                            break;
                        case 2:
                            Initiative *= 0.76m;
                            break;
                        case 3:
                            Initiative *= 0.64m;
                            break;
                    }
                }
            }

            // アサシンシフト
            if (Turn == 0 &&
                Mine.EffectList.FindByeffect_id(2113) != null &&
                Mine.GetType() == typeof(LibPlayer) &&
                (Mine.MainWeapon.ItemType == 1 ||
                Mine.MainWeapon.ItemType == 2 ||
                Mine.MainWeapon.ItemType == 5 ||
                Mine.MainWeapon.ItemType == 8 ||
                Mine.MainWeapon.ItemType == 15))
            {
                Initiative *= 0.88m;
            }

            // 磁場効果
            if (FieldRow.magnet)
            {
                int MetalP = Mine.Metal;

                if (MetalP > 1)
                {
                    Initiative *= (decimal)MetalP;
                }
            }

            // ステータス、ターン効果
            // 暴走効果
            if (Mine.StatusEffect.Check(14))
            {
                Initiative *= 0.5m;
            }

            // 騎乗：ＣＴ軽減
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(5501);
                if (EffectRow != null &&
                    Mine.EffectList.FindByeffect_id(2118) != null)
                {
                    Initiative -= (int)(Initiative * EffectRow.rank / 100m);
                }
            }

            // ここまでで、数値の限界を設定する
            if (Initiative < 2m)
            {
                Initiative = 2;
            }
            else if (Initiative > (int.MaxValue - 2))
            {
                Initiative = int.MaxValue - 2;
            }

            {
                bool IsHaist = false;
                bool IsSlow = false;

                // ヘイスト効果
                if (Mine.StatusEffect.Check(53))
                {
                    IsHaist = true;
                }

                // スロウ効果
                if (Mine.StatusEffect.Check(18))
                {
                    IsSlow = true;
                }

                if (IsHaist && !IsSlow)
                {
                    Initiative = 1;
                }
                else if (!IsHaist && IsSlow)
                {
                    Initiative = int.MaxValue - 1;
                }

                // ファストトリック
                if (Turn == 0)
                {
                    EffectListEntity.effect_listRow FastTakeRow = Mine.EffectList.FindByeffect_id(705);
                    if (FastTakeRow != null && FastTakeRow.prob > LibInteger.GetRandBasis())
                    {
                        Initiative = 0;
                    }
                }
                {
                    // アーツのファストトリック
                    EffectListEntity.effect_listRow FastTakeRow = Mine.SelectedActions[0].EffectList.FindByeffect_id(705);
                    if (FastTakeRow != null && FastTakeRow.prob > LibInteger.GetRandBasis())
                    {
                        Initiative = 0;
                    }
                }

                // ファストアイテム
                if (Mine.SelectedActions[0].AttackType == Status.AttackType.Item &&
                    Mine.EffectList.FindByeffect_id(1051) != null)
                {
                    Initiative = 0;
                }

                if (Turn == 0)
                {
                    switch (BattleStyle)
                    {
                        case Status.BattleStyle.Preemptive:
                            if (Mine.PartyBelong == Status.Belong.Friend)
                            {
                                Initiative = 0;
                            }
                            else
                            {
                                Initiative = int.MaxValue;
                            }
                            break;
                        case Status.BattleStyle.Surprise:
                            if (Mine.PartyBelong == Status.Belong.Enemy)
                            {
                                Initiative = 0;
                            }
                            else
                            {
                                Initiative = int.MaxValue;
                            }
                            break;
                    }
                }
            }

            Mine.ChargeTime = (int)Initiative;

            // チェイン解除
            Mine.ChainList.Clear();

            // リベンジクリティカル初期化
            Mine.IsRevengeCritical = false;
        }
    }
}
