using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        /// <summary>
        /// 攻撃を実行する
        /// </summary>
        /// <param name="Mine">実行者</param>
        /// <param name="ActionArtsRows">行動内容（カウンターの場合はそれを設定）</param>
        /// <param name="TempTargetBattle">攻撃対象リスト</param>
        /// <param name="MessageBuilder">メッセージ格納</param>
        /// <param name="AreaName">エリア名</param>
        private void BattleAttack(LibUnitBase Mine, List<LibActionType> ActionArtsRows, List<LibUnitBase> TempTargetBattle, StringBuilder MessageBuilder, string AreaName, int Turn)
        {
            int i = 0;

            // 予ダメージによるセリフ表示有無
            bool OnceSerif = true;

            List<LibUnitBase> SelectTarget = new List<LibUnitBase>();// 攻撃対象（本番）
            foreach (LibActionType ActionArtsRow in ActionArtsRows)
            {
                List<LibUnitBase> TargetModel = new List<LibUnitBase>();
                SelectTarget.Clear();

                int DamageType = ActionArtsRow.DamageType;

                Mine.UsedArtsName = ActionArtsRow.ArtsName;

                #region ブラッドスタンスによる攻撃吸収への変換
                if (Mine.StatusEffect.Check(259) && DamageType == Status.DamageType.PhysicalDamage)
                {
                    // ＨＰダメージを吸収に変換
                    DamageType = Status.DamageType.PhysicalDrain;
                }
                #endregion

                #region シャドーポット効果
                if (ActionArtsRow.EffectList.FindByeffect_id(1062) != null)
                {
                    if (LibInteger.GetRandMax(100) <= 50)
                    {
                        DamageType = Status.DamageType.PhysicalDamage;
                    }
                }
                #endregion

                #region 魅了の効果
                if (Mine.StatusEffect.Check(20))
                {
                    switch (ActionArtsRow.TargetParty)
                    {
                        case Status.TargetParty.Friend:
                            ActionArtsRow.TargetParty = Status.TargetParty.Enemy;
                            break;
                        case Status.TargetParty.Enemy:
                            ActionArtsRow.TargetParty = Status.TargetParty.Friend;
                            break;
                    }
                }
                #endregion

                #region ターゲットの選定
                switch (ActionArtsRow.TargetParty)
                {
                    case Status.TargetParty.Mine:
                    case Status.TargetParty.Friend:
                        if (Mine.PartyBelong == Status.Belong.Enemy)
                        {
                            TargetModel = LibBattleCharacter.GetMonsters(BattleCharacer);
                        }
                        else
                        {
                            TargetModel = LibBattleCharacter.GetFriendryNotPet(BattleCharacerLive);
                        }
                        break;
                    case Status.TargetParty.Enemy:
                        if (Mine.PartyBelong == Status.Belong.Enemy)
                        {
                            TargetModel = LibBattleCharacter.GetFriendry(BattleCharacerLive);
                        }
                        else
                        {
                            TargetModel = LibBattleCharacter.GetMonsters(BattleCharacer);
                        }
                        break;
                    case Status.TargetParty.Pet:
                        if (Mine.PartyBelong == Status.Belong.Enemy)
                        {
                            TargetModel = LibBattleCharacter.GetMonsters(BattleCharacer);
                        }
                        else
                        {
                            TargetModel = LibBattleCharacter.GetFriendryPet(BattleCharacer);
                        }
                        break;
                    case Status.TargetParty.All:
                        TargetModel = BattleCharacer;
                        break;
                }

                int TargetArea = ActionArtsRow.TargetArea;

                if (Mine.StatusEffect.Check(97) &&
                    ActionArtsRow.AttackType == Status.AttackType.Item &&
                    DamageType == Status.DamageType.Heal &&
                    ActionArtsRow.PlusScore > 0 &&
                    TargetArea == Status.TargetArea.Only)
                {
                    TargetArea = Status.TargetArea.All;
                }

                if (Mine.StatusEffect.Check(100) &&
                    ActionArtsRow.AttackType == Status.AttackType.Song)
                {
                    TargetArea = Status.TargetArea.Only;
                }

                if (Mine.StatusEffect.Check(101) &&
                    ActionArtsRow.AttackType == Status.AttackType.Dance)
                {
                    TargetArea = Status.TargetArea.Only;
                }

                switch (TargetArea)
                {
                    case Status.TargetArea.Only:
                        if (ActionArtsRow.TargetParty == Status.TargetParty.Enemy && TempTargetBattle[0].StatusEffect.Check(64))
                        {
                            // かばう（ラウンド）
                            LibUnitBase SelectCharacters = BattleCharacerLive.Find(TempRoundTarget => TempRoundTarget.BattleID == TempTargetBattle[0].StatusEffect.GetRank(64));

                            if (SelectCharacters != null)
                            {
                                SelectTarget.Add(SelectCharacters);
                                MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(SelectCharacters.NickName) + "の庇う！</dd>");
                            }
                        }
                        else
                        {
                            if (ActionArtsRow.TargetParty == Status.TargetParty.Mine)
                            {
                                SelectTarget.Add(Mine);
                            }
                            else
                            {
                                bool IsFrontGuard = false;
                                if (ActionArtsRow.TargetParty == Status.TargetParty.Enemy && TempTargetBattle[0].Formation == Status.Formation.Backs)
                                {
                                    bool IsFullProtect = false;
                                    LibUnitBase protectUnit = null;

                                    foreach (LibUnitBase unitParty in EnemysLive)
                                    {
                                        if (unitParty.EffectList.FindByeffect_id(847) != null && unitParty.Formation == Status.Formation.Foward)
                                        {
                                            IsFrontGuard = true;
                                            if (unitParty.StatusEffect.Check(65))
                                            {
                                                // 鉄壁中は確率アップ
                                                IsFullProtect = true;
                                            }
                                            protectUnit = unitParty;
                                            break;
                                        }
                                    }

                                    if (protectUnit != null && IsFrontGuard && (LibInteger.GetRandBasis() <= 10 || (IsFullProtect && LibInteger.GetRandBasis() <= 80)))
                                    {
                                        // フロントガード
                                        SelectTarget.Add(protectUnit);
                                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(protectUnit.NickName) + "のフロントガード！</dd>");
                                    }
                                    else
                                    {
                                        IsFrontGuard = false;
                                    }
                                }

                                if (!IsFrontGuard)
                                {
                                    SelectTarget.Add(TempTargetBattle[0]);
                                }
                            }
                        }
                        break;
                    case Status.TargetArea.Circle1:
                        SelectTarget.Add(TempTargetBattle[0]);

                        // さらに確率で追加される
                        for (i = 0; i < 2; i++)
                        {
                            if (LibInteger.GetRand(256) <= (204 - (i * 52)))
                            {
                                LibUnitBase Added = TargetModel[LibInteger.GetRand(TargetModel.Count)];
                                if (SelectTarget[0].BattleID != Added.BattleID)
                                {
                                    SelectTarget.Add(Added);
                                }
                            }
                        }
                        break;
                    case Status.TargetArea.Circle2:
                        SelectTarget.Add(TempTargetBattle[0]);

                        // さらに確率で追加される
                        for (i = 0; i < 4; i++)
                        {
                            if (LibInteger.GetRand(256) <= (204 - (i * 52)))
                            {
                                LibUnitBase Added = TargetModel[LibInteger.GetRand(TargetModel.Count)];
                                if (SelectTarget[0].BattleID != Added.BattleID)
                                {
                                    SelectTarget.Add(Added);
                                }
                            }
                        }
                        break;
                    case Status.TargetArea.Line:
                        foreach (LibUnitBase Target in TargetModel)
                        {
                            if (Target.BattleFormation == TempTargetBattle[0].BattleFormation)
                            {
                                SelectTarget.Add(Target);
                            }
                        }
                        break;
                    case Status.TargetArea.All:
                        SelectTarget = TargetModel;
                        break;
                    case Status.TargetArea.Monster:
                        if (Mine.GetType() == typeof(LibPlayer))
                        {
                            foreach (LibUnitBase Target in TargetModel)
                            {
                                if ((Target.PartyBelongDetail == Status.BelongDetail.Animal ||
                                    Target.PartyBelongDetail == Status.BelongDetail.Normal) &&
                                    Target.BattleID == ((LibPlayer)Mine).PetCharacterBattleID)
                                {
                                    // Status.BelongDetail.Normal はペットにされたモンスター
                                    SelectTarget.Add(Target);
                                }
                            }
                        }
                        break;
                    case Status.TargetArea.Elemental:
                        if (Mine.GetType() == typeof(LibPlayer))
                        {
                            foreach (LibUnitBase Target in TargetModel)
                            {
                                if (Target.PartyBelongDetail == Status.BelongDetail.Elemental &&
                                    Target.BattleID == ((LibPlayer)Mine).PetCharacterBattleID)
                                {
                                    SelectTarget.Add(Target);
                                }
                            }
                        }
                        break;
                    case Status.TargetArea.Papet:
                        if (Mine.GetType() == typeof(LibPlayer))
                        {
                            foreach (LibUnitBase Target in TargetModel)
                            {
                                if (Target.PartyBelongDetail == Status.BelongDetail.Familiar &&
                                    Target.BattleID == ((LibPlayer)Mine).PetCharacterBattleID)
                                {
                                    SelectTarget.Add(Target);
                                }
                            }
                        }
                        break;
                }
                #endregion

                int SacrificeHP = 0;
                int DevotionHP = 0;

                #region MP・TPの消費
                {
                    int CostHP = 0;
                    int CostMP = ActionArtsRow.MP;
                    int CostTP = ActionArtsRow.TP;

                    // HP消費計算
                    {
                        EffectListEntity.effect_listRow EffectRow = ActionArtsRow.EffectList.FindByeffect_id(855);
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
                        EffectListEntity.effect_listRow EffectRow = ActionArtsRow.EffectList.FindByeffect_id(856);
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
                        SacrificeHP = CostHP * 2;
                    }
                    // デヴォーション
                    {
                        EffectListEntity.effect_listRow EffectRow = ActionArtsRow.EffectList.FindByeffect_id(857);
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
                        DevotionHP = CostHP;
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
                    {
                        // コンサーブマインド
                        EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(852);
                        if (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis())
                        {
                            CostMP -= (int)((decimal)CostMP * EffectRow.rank / 100m);
                            MessageBuilder.AppendLine("<dd>コンサーブマインドの効果が発動！</dd>");
                        }
                    }

                    // エレメンタルフィット
                    if (Mine.EffectList.FindByeffect_id(2134) != null &&
                        ActionArtsRow.ArtsCategory == LibSkillType.FindByName("精霊魔法") &&
                        ActionArtsRow.CheckElementalByFit(((LibPlayer)Mine).GuardianInt))
                    {
                        CostMP -= (int)((decimal)CostMP * 0.1m);
                        MessageBuilder.AppendLine("<dd>エレメンタルフィットの効果が発動！</dd>");
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
                        (ActionArtsRow.AttackType == Status.AttackType.Mystic ||
                        ActionArtsRow.AttackType == Status.AttackType.MagicSword))
                    {
                        // マナファストリング
                        CostMP *= 2;
                    }

                    // 消費実行
                    if (CostHP > Mine.HPNow)
                    {
                        MessageBuilder.AppendLine("<dd class=\"caution\">HPが足りない！</dd>");
                        break;
                    }
                    if (CostMP > Mine.MPNow)
                    {
                        MessageBuilder.AppendLine("<dd class=\"caution\">MPが足りない！</dd>");
                        break;
                    }
                    if (CostTP > Mine.TPNow)
                    {
                        MessageBuilder.AppendLine("<dd class=\"caution\">TPが足りない！</dd>");
                        break;
                    }

                    Mine.MPNow -= CostMP;
                    Mine.TPNow -= CostTP;
                    Mine.HPNow -= CostHP;
                }
                #endregion

                #region 不安定による発動失敗
                {
                    EffectListEntity.effect_listRow EffectRow = ActionArtsRow.EffectList.FindByeffect_id(2000);
                    if (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis())
                    {
                        MessageBuilder.AppendLine("<dd>集まりかけたエネルギーが霧散してしまった…。</dd>");
                        break;
                    }
                }
                #endregion

                #region スペシャルスキルの継続回数リセット
                if (ActionArtsRow.SkillType == Status.SkillType.Special ||
                    ActionArtsRow.EffectList.FindByeffect_id(1040) != null)
                {
                    Mine.IsSpecialUsed = true;
                    if (Mine.GetType() == typeof(LibPlayer))
                    {
                        ((LibPlayer)Mine).ContinueBonus = 0;
                    }
                }
                #endregion

                #region 攻撃対象別の判定実行
                foreach (LibUnitBase Targeted in SelectTarget)
                {
                    // ターゲットの生存確認
                    if (Targeted.BattleOut)
                    {
                        // 復活効果
                        EffectListEntity.effect_listRow RaiseRow = ActionArtsRow.EffectList.FindByeffect_id(300);
                        if (RaiseRow != null)
                        {
                            if (Targeted.GetType() == typeof(LibPlayer))
                            {
                                MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "は戦列に復帰した。</dd>");

                                BattleCommon.Revivals(Targeted, BattleCharacer, (int)RaiseRow.rank);
                            }
                            else
                            {
                                MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                    #region 各種フラグ宣言
                    // 吸収量
                    int DrainPoint = 0;

                    // 分身消費数
                    int UseUtsusemiCount = 0;

                    // パリィ発動回数
                    int ParryCount = 0;

                    // ガード発動回数
                    int ShieldGuardCount = 0;

                    // ドッジ発動回数
                    int DodgeCount = 0;

                    // 回避無視かどうか
                    bool IsDodgeOut = true;
                    #endregion

                    // ダメージ総量
                    int Damage = 0;

                    // クリティカル発動
                    bool IsCritical = false;

                    // 命中したか
                    bool IsHit = false;

                    // ミスの表示を行うか
                    bool IsMissMessage = true;

                    // 攻撃ヒット回数
                    int HitCount = 0;

                    // 混乱解除
                    bool IsConfuseClear = false;

                    // 睡眠解除
                    bool IsSleepClear = false;

                    // ガードチェック？
                    bool IsCheckGuard = true;

                    // パリィチェック？
                    bool IsCheckParry = true;

                    // ドッジチェック？
                    bool IsCheckDodge = true;

                    // 連撃回数
                    int MultiActAttackCount = 0;

                    // ターゲットレイヴ
                    bool IsTargetRave = false;

                    // 属性相性種類
                    int ElementalRatingType = Status.ElementalRatingType.None;

                    // ドリームルーレットによる効果判定
                    {
                        EffectListEntity.effect_listRow EffectRow = ActionArtsRow.EffectList.FindByeffect_id(1060);
                        if (EffectRow != null)
                        {
                            int[] DamageTypeList = new int[] { Status.DamageType.None, Status.DamageType.Heal, Status.DamageType.MagicalDamage };
                            DamageType = DamageTypeList[LibInteger.GetRand(DamageTypeList.Length)];
                        }
                    }

                    // シャドウサーバント
                    if ((ActionArtsRow.ActionBase == Status.ActionBaseType.MainAttack ||
                        ActionArtsRow.ActionBase == Status.ActionBaseType.SubAttack) &&
                        (DamageType == Status.DamageType.PhysicalDamage ||
                        DamageType == Status.DamageType.PhysicalDrain) &&
                        ActionArtsRow.AttackType == Status.AttackType.Combat &&
                        ActionArtsRow.TargetParty == Status.TargetParty.Enemy &&
                        Targeted.StatusEffect.Check(251))
                    {
                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の影が攻撃を受け止めた。</dd>");

                        // 以降の判定を回避
                        continue;
                    }

                    // 土煙
                    if ((DamageType == Status.DamageType.PhysicalDamage ||
                        DamageType == Status.DamageType.PhysicalDrain ||
                        DamageType == Status.DamageType.MagicalDamage ||
                        DamageType == Status.DamageType.MagicalDrain) &&
                        ActionArtsRow.TargetParty == Status.TargetParty.Enemy &&
                        !ActionArtsRow.CheckEffect(379) &&
                        Targeted.StatusEffect.Check(79))
                    {
                        MessageBuilder.AppendLine("<dd>土煙の影響で、" + LibResultText.CSSEscapeChara(Targeted.NickName) + "に攻撃が届かない！</dd>");

                        // 以降の判定を回避
                        continue;
                    }

                    // 上空
                    if ((Targeted.EffectList.FindByeffect_id(825) != null ||
                        Targeted.StatusEffect.Check(264)) &&
                        !ActionArtsRow.AntiAir)
                    {
                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "は飛行しているため、攻撃が届かない！</dd>");

                        // 以降の判定を回避
                        continue;
                    }

                    //// 射程
                    //if ((Mine.BattleFormation + Targeted.BattleFormation) > ActionArtsRow.Range)
                    //{
                    //    MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "は遠い場所にいるため、攻撃が届かない！</dd>");

                    //    // 以降の判定を回避
                    //    continue;
                    //}

                    // 攻撃・判定回数
                    int AttackCount = ActionArtsRow.ActionCount;

                    if (Mine.StatusEffect.Check(255) &&
                        ActionArtsRow.ArtsCategory == LibSkillType.FindByName("壊魔術"))
                    {
                        // コンセントレーション
                        AttackCount *= 2;
                    }
                    if (Mine.StatusEffect.Check(254) &&
                        (ActionArtsRow.AttackType == Status.AttackType.Mystic ||
                        ActionArtsRow.AttackType == Status.AttackType.MagicSword))
                    {
                        // マナファストリング
                        AttackCount *= 2;
                    }

                    #region 連撃 発生率判定
                    int CriticalProb = 0;
                    int CriticalType = Status.CriticalType.Critical;

                    switch (ActionArtsRow.ActionBase)
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
                        atkSubTypeRow = LibItemType.GetSubCategoryRow(ActionArtsRow.ItemSubType);
                    }

                    if (ActionArtsRow.IsNormalAttack)
                    {
                        if (atkSubTypeRow != null)
                        {
                            CriticalType = atkSubTypeRow.critical_type;
                        }
                    }
                    else
                    {
                        CriticalType = ActionArtsRow.CriticalType;
                    }

                    if (CriticalType == Status.CriticalType.Critical)
                    {
                        if (Mine.StatusEffect.Check(94))
                        {
                            CriticalType = Status.CriticalType.MultiAct;
                        }
                    }

                    // アーツのクリティカル率設定
                    if (!ActionArtsRow.IsNormalAttack)
                    {
                        CriticalProb = ActionArtsRow.Critical;
                    }

                    // 連撃アップ
                    if (CriticalType == Status.CriticalType.MultiAct)
                    {
                        EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(816);
                        if (EffectRow != null)
                        {
                            CriticalProb = (int)((decimal)CriticalProb * (1m + EffectRow.rank * 0.1m));
                        }
                        else if (Mine.StatusEffect.Check(68))
                        {
                            switch ((int)Mine.StatusEffect.GetRank(68))
                            {
                                case 1:
                                    CriticalProb = (int)((decimal)CriticalProb * 1.2m);
                                    break;
                                case 2:
                                    CriticalProb = (int)((decimal)CriticalProb * 1.5m);
                                    break;
                                case 3:
                                    CriticalProb = (int)((decimal)CriticalProb * 1.8m);
                                    break;
                                case 4:
                                    CriticalProb += 100;
                                    IsTargetRave = true;
                                    break;
                            }
                        }
                        else if (Mine.StatusEffect.Check(94))
                        {
                            CriticalProb = (int)((decimal)CriticalProb * 1.5m);
                        }
                        else
                        {
                            CriticalProb = (int)((decimal)CriticalProb * 0.7m);
                        }

                        // ラッシュアワー
                        EffectListEntity.effect_listRow EffectRushRow = Mine.EffectList.FindByeffect_id(2123);
                        if (EffectRushRow != null)
                        {
                            CriticalProb += (int)EffectRushRow.rank;
                        }
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

                    if (Mine.StatusEffect.Check(258) && (ActionArtsRow.ActionBase == Status.ActionBaseType.MainAttack ||
                        ActionArtsRow.ActionBase == Status.ActionBaseType.SubAttack))
                    {
                        // 夢幻闘武
                        CriticalProb = 200;
                        CriticalType = Status.CriticalType.Critical;
                    }

                    // ソーサルクリティカル
                    if (Mine.EffectList.FindByeffect_id(867) != null &&
                        ActionArtsRow.AttackType == Status.AttackType.Mystic &&
                        !ActionArtsRow.IsNormalAttack)
                    {
                        CriticalProb = 5;
                        CriticalType = Status.CriticalType.Critical;
                    }

                    if (CriticalType == Status.CriticalType.MultiAct)
                    {
                        while (CriticalProb > (int)LibInteger.GetRandBasis())
                        {
                            if (Mine.GetType() == typeof(LibMonster))
                            {
                                // モンスターの場合の特別ルール
                                // 連撃の場合の最大回数
                                if ((MultiActAttackCount + 1) >= ((LibMonster)Mine).MultiAttackMaxCount)
                                {
                                    break;
                                }
                            }

                            // 連撃発生
                            MultiActAttackCount++;

                            // ウェポンブレリー
                            if (Mine.EffectList.FindByeffect_id(818) != null)
                            {
                                Mine.TPNow += LibInteger.GetRandMax(2, 5);
                            }

                            if (IsTargetRave)
                            {
                                CriticalProb -= 100;
                                IsTargetRave = false;
                            }
                        }
                    }
                    #endregion

                    int AttackActionCount = AttackCount + MultiActAttackCount;

                    // 複数回攻撃
                    for (i = 0; i < AttackActionCount; i++)
                    {
                        IsDodgeOut = true;
                        IsCheckGuard = true;
                        IsCheckParry = true;
                        IsCheckDodge = true;

                        // 命中率の計算
                        int HitRate = BattleHitRate(Mine, Targeted, ActionArtsRow, ref IsDodgeOut, ref IsCheckGuard, ref IsCheckParry, ref IsCheckDodge);

                        // ダメージ判定なしの場合、複数回攻撃判定はスキップ
                        if (DamageType == Status.DamageType.None)
                        {
                            IsHit = true;

                            break;
                        }

                        // 回避の判定
                        #region ステータスによる回避
                        // 分身
                        if (Targeted.StatusEffect.Check(62) &&
                            ActionArtsRow.EffectList.FindByeffect_id(1610) == null &&
                            SelectTarget.Count == 1 &&
                            DamageType != Status.DamageType.Heal &&
                            ActionArtsRow.TargetParty != Status.TargetParty.Friend &&
                            ActionArtsRow.TargetParty != Status.TargetParty.Mine &&
                            ActionArtsRow.TargetParty != Status.TargetParty.Pet)
                        {
                            // 分身
                            int Rank = (int)Targeted.StatusEffect.GetRank(62) - 1;

                            UseUtsusemiCount++;

                            if (Rank == 0)
                            {
                                // 全部消費したら削除する
                                Targeted.StatusEffect.Delete(62);
                            }
                            else
                            {
                                Targeted.StatusEffect.SetRank(62, Rank);
                            }

                            // 以降の判定を回避
                            continue;
                        }
                        #endregion

                        // 盾・ドッジ・武器回避による回避
                        if (IsDodgeOut && HitRate > 0)
                        {
                            // 盾
                            if (IsCheckGuard)
                            {
                                EffectListEntity.effect_listRow ShieldEffectRow = Targeted.EffectList.FindByeffect_id(810);
                                switch (ActionArtsRow.ActionBase)
                                {
                                    case Status.ActionBaseType.MainAttack:
                                    case Status.ActionBaseType.SubAttack:
                                        {
                                            decimal Avoid = (decimal)Targeted.ShieldAvoidPhysical;
                                            if (ShieldEffectRow != null && Targeted.ShieldAvoidPhysical > 0)
                                            {
                                                Avoid += ShieldEffectRow.rank * 5m;
                                            }

                                            decimal AddRate = 1.0m;

                                            if (Targeted.StatusEffect.Check(81))
                                            {
                                                // ガーディア
                                                if (Targeted.EffectList.FindByeffect_id(2125) != null)
                                                {
                                                    // ガーディアプラス
                                                    Avoid += 0.3m;
                                                }
                                                else
                                                {
                                                    Avoid += 0.2m;
                                                }
                                            }

                                            if (Targeted.StatusEffect.Check(55))
                                            {
                                                Avoid += 0.5m;
                                            }

                                            if (AddRate > 1.0m)
                                            {
                                                Avoid *= AddRate;
                                            }

                                            if (Targeted.ShieldAvoidPhysical > 0 &&
                                                ActionArtsRow.DamageType != Status.DamageType.Heal &&
                                                !Mine.StatusEffect.Check(265) &&
                                                (Avoid / (decimal)HitRate * 100m) > LibInteger.GetRandBasis())
                                            {
                                                ShieldGuardCount++;
                                                continue;
                                            }
                                        }
                                        break;
                                    case Status.ActionBaseType.ItemArts:
                                    case Status.ActionBaseType.BlessAttack:
                                    case Status.ActionBaseType.MagicSword:
                                    case Status.ActionBaseType.MindAttack:
                                    case Status.ActionBaseType.SorscialAttack:
                                        {
                                            decimal Avoid = (decimal)Targeted.ShieldAvoidSorcery;
                                            if (ShieldEffectRow != null && Targeted.ShieldAvoidSorcery > 0)
                                            {
                                                Avoid += ShieldEffectRow.rank * 5m;
                                            }

                                            decimal AddRate = 1.0m;

                                            if (Targeted.StatusEffect.Check(81))
                                            {
                                                // ガーディア
                                                if (Targeted.EffectList.FindByeffect_id(2125) != null)
                                                {
                                                    // ガーディアプラス
                                                    Avoid += 0.3m;
                                                }
                                                else
                                                {
                                                    Avoid += 0.2m;
                                                }
                                            }

                                            if (Targeted.StatusEffect.Check(55))
                                            {
                                                Avoid += 0.5m;
                                            }

                                            if (AddRate > 1.0m)
                                            {
                                                Avoid *= AddRate;
                                            }

                                            if (Targeted.ShieldAvoidSorcery > 0 &&
                                                ActionArtsRow.DamageType != Status.DamageType.Heal &&
                                                !Mine.StatusEffect.Check(265) &&
                                                (Avoid / (decimal)HitRate * 100m) > LibInteger.GetRandBasis())
                                            {
                                                ShieldGuardCount++;
                                                continue;
                                            }
                                        }
                                        break;
                                }
                            }

                            // 武器回避
                            {
                                decimal Avoid = (decimal)Targeted.MainWeapon.Avoid;
                                if ((decimal)Targeted.MainWeapon.Avoid > 0 &&
                                    Targeted.StatusEffect.Check(92))
                                {
                                    // ガーディアルアーツ
                                    Avoid += 30;
                                }

                                {
                                    // ベアズパリィ
                                    EffectListEntity.effect_listRow EffectParryRow = Targeted.EffectList.FindByeffect_id(2116);
                                    if ((decimal)Targeted.MainWeapon.Avoid > 0 &&
                                        EffectParryRow != null)
                                    {
                                        Avoid += (int)EffectParryRow.rank;
                                    }
                                }

                                if (Targeted.StatusEffect.Check(55))
                                {
                                    Avoid *= 1.5m;
                                }

                                if (IsCheckParry &&
                                    !Mine.StatusEffect.Check(265) &&
                                    ActionArtsRow.DamageType != Status.DamageType.Heal &&
                                    (ActionArtsRow.ActionBase == Status.ActionBaseType.MainAttack ||
                                    ActionArtsRow.ActionBase == Status.ActionBaseType.SubAttack) &&
                                    (Avoid / (decimal)HitRate * 100m) > LibInteger.GetRandBasis())
                                {
                                    ParryCount++;
                                    continue;
                                }
                            }

                            // サブ武器回避
                            if (Targeted.ATKSub > 0)
                            {
                                decimal Avoid = (decimal)Targeted.SubWeapon.Avoid;
                                if ((decimal)Targeted.SubWeapon.Avoid > 0 &&
                                    Targeted.StatusEffect.Check(92))
                                {
                                    // ガーディアルアーツ
                                    Avoid += 30;
                                }

                                {
                                    // ベアズパリィ
                                    EffectListEntity.effect_listRow EffectParryRow = Targeted.EffectList.FindByeffect_id(2116);
                                    if ((decimal)Targeted.SubWeapon.Avoid > 0 &&
                                        EffectParryRow != null)
                                    {
                                        Avoid += (int)EffectParryRow.rank;
                                    }
                                }

                                if (Targeted.StatusEffect.Check(55))
                                {
                                    Avoid *= 1.5m;
                                }

                                if (IsCheckParry &&
                                    !Mine.StatusEffect.Check(265) &&
                                    ActionArtsRow.DamageType != Status.DamageType.Heal &&
                                    (ActionArtsRow.ActionBase == Status.ActionBaseType.MainAttack ||
                                    ActionArtsRow.ActionBase == Status.ActionBaseType.SubAttack) &&
                                    (Avoid / (decimal)HitRate * 100m) > LibInteger.GetRandBasis())
                                {
                                    ParryCount++;
                                    continue;
                                }
                            }

                            // ドッジ回避
                            {
                                decimal DodgeAvoid = (decimal)Targeted.AVD;

                                if (Targeted.StatusEffect.Check(267))
                                {
                                    DodgeAvoid += 50;
                                }

                                {
                                    // 騎乗：ドッジ成功率アップ
                                    EffectListEntity.effect_listRow EffectRow = Targeted.EffectList.FindByeffect_id(5502);
                                    if (EffectRow != null && Targeted.EffectList.FindByeffect_id(2118) != null)
                                    {
                                        DodgeAvoid += (int)EffectRow.rank;
                                    }
                                }

                                {
                                    //ドッジエクリプス
                                    EffectListEntity.effect_listRow EffectRow = Targeted.EffectList.FindByeffect_id(2126);
                                    if (EffectRow != null)
                                    {
                                        DodgeAvoid += (int)EffectRow.rank;
                                    }
                                }

                                if (IsCheckDodge &&
                                    !Mine.StatusEffect.Check(265) &&
                                    ActionArtsRow.DamageType != Status.DamageType.Heal &&
                                    (ActionArtsRow.ActionBase == Status.ActionBaseType.MainAttack ||
                                    ActionArtsRow.ActionBase == Status.ActionBaseType.SubAttack) &&
                                    (DodgeAvoid / (decimal)HitRate * 100m) > LibInteger.GetRandBasis())
                                {
                                    DodgeCount++;
                                    if (Targeted.StatusEffect.Check(267))
                                    {
                                        Targeted.TPNow += 5;
                                    }
                                    continue;
                                }
                            }
                        }

                        if (HitRate > LibInteger.GetRandBasis() ||
                            ActionArtsRow.ActionBase == Status.ActionBaseType.MindAttack ||
                            ActionArtsRow.ActionBase == Status.ActionBaseType.BlessAttack ||
                            ActionArtsRow.ActionBase == Status.ActionBaseType.SorscialAttack ||
                            ActionArtsRow.ActionBase == Status.ActionBaseType.MagicSword ||
                            DamageType == Status.DamageType.Heal)
                        {
                            IsHit = true;

                            // ダメージの計算
                            Damage += BattleDamage(Mine, Targeted, ActionArtsRow, ref DrainPoint, ref IsCritical, SacrificeHP, ref ElementalRatingType, DamageType, Turn, false);

                            HitCount++;

                            #region ヒットアンドローカウント
                            if (ActionArtsRow.EffectList.FindByeffect_id(1061) != null)
                            {
                                Targeted.HighAndLowHitCount++;
                            }
                            #endregion

                            // 混乱、睡眠解除
                            if (ActionArtsRow.ActionBase == Status.ActionBaseType.MainAttack ||
                                ActionArtsRow.ActionBase == Status.ActionBaseType.SubAttack ||
                                DamageType == Status.DamageType.Heal)
                            {
                                if (Targeted.StatusEffect.Check(2)) { IsSleepClear = true; }
                                if (Targeted.StatusEffect.Check(12)) { IsConfuseClear = true; }
                                Targeted.StatusEffect.Delete(2);
                                Targeted.StatusEffect.Delete(12);
                            }
                        }
                        else
                        {
                            // ハイアンドロー
                            if (ActionArtsRow.EffectList.FindByeffect_id(1061) != null)
                            {
                                Targeted.HighAndLowHitCount = 0;
                            }
                        }
                    }

                    // ファーストタッチのリセット
                    Targeted.IsFirstAttachAttack = false;

                    #region 回避、吸収の表示

                    // 吸収
                    if (DrainPoint > 0)
                    {
                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "は" + DrainPoint + "吸収した。</dd>");
                        Targeted.HPNow += DrainPoint;
                    }

                    // 分身
                    if (UseUtsusemiCount > 0 && Damage == 0)
                    {
                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の分身が" + UseUtsusemiCount + "枚、攻撃を受けて消えた。</dd>");
                        IsHit = false;
                        IsMissMessage = false;
                    }

                    // パリィ
                    if (ParryCount > 0)
                    {
                        MessageBuilder.AppendLine(LibSerif.Serif(Targeted, LibSituation.GetNo("パリィが発動"), null, AreaName, Mine));
                        MessageBuilder.AppendLine("<dd>パリィ! " + LibResultText.CSSEscapeChara(Targeted.NickName) + "は攻撃を受け流した！</dd>");
                        IsHit = false;
                        IsMissMessage = false;
                    }

                    // ガード
                    if (ShieldGuardCount > 0)
                    {
                        MessageBuilder.AppendLine(LibSerif.Serif(Targeted, LibSituation.GetNo("ガードが発動"), null, AreaName, Mine));
                        MessageBuilder.AppendLine("<dd>ガード！ " + LibResultText.CSSEscapeChara(Targeted.NickName) + "は盾で攻撃を防いだ！</dd>");
                        IsHit = false;
                        IsMissMessage = false;
                    }

                    // ドッジ
                    if (DodgeCount > 0)
                    {
                        MessageBuilder.AppendLine(LibSerif.Serif(Targeted, LibSituation.GetNo("ドッジが発動"), null, AreaName, Mine));
                        MessageBuilder.AppendLine("<dd>ドッジ！" + LibResultText.CSSEscapeChara(Targeted.NickName) + "は攻撃を避けた！</dd>");
                        IsHit = false;
                        IsMissMessage = false;
                    }
                    #endregion

                    #region 予ダメージによるセリフ表示
                    if (IsMissMessage && 75 > LibInteger.GetRandBasis() &&
                        DamageType != Status.DamageType.Heal &&
                        DamageType > Status.DamageType.None &&
                        OnceSerif)
                    {
                        OnceSerif = false;

                        if (IsHit)
                        {
                            if (IsCritical)
                            {
                                #region クリティカル
                                switch (ActionArtsRow.ActionType)
                                {
                                    case Status.ActionType.MainAttack:
                                        MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("通常攻撃がクリティカルヒットした時"), null, AreaName, Targeted));
                                        break;
                                    case Status.ActionType.ArtsAttack:
                                        MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("アーツがクリティカルした時"), ActionArtsRow.SkillID, AreaName, Targeted));
                                        break;
                                    case Status.ActionType.SpecialArtsAttack:
                                        MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("スペシャルがクリティカルヒットした時"), null, AreaName, Targeted));
                                        break;
                                }
                                #endregion
                            }
                            else
                            {
                                #region ダメージ
                                switch (ActionArtsRow.ActionType)
                                {
                                    case Status.ActionType.MainAttack:
                                        MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("通常攻撃がヒットした時"), null, AreaName, Targeted));
                                        break;
                                    case Status.ActionType.ArtsAttack:
                                        MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("アーツがヒットした時"), ActionArtsRow.SkillID, AreaName, Targeted));
                                        break;
                                    case Status.ActionType.SpecialArtsAttack:
                                        MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("スペシャルがヒットした時"), null, AreaName, Targeted));
                                        break;
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            #region ミス
                            switch (ActionArtsRow.ActionType)
                            {
                                case Status.ActionType.MainAttack:
                                    MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("通常攻撃がミスした時"), null, AreaName, Targeted));
                                    break;
                                case Status.ActionType.ArtsAttack:
                                    MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("アーツがミスした時"), ActionArtsRow.SkillID, AreaName, Targeted));
                                    break;
                                case Status.ActionType.SpecialArtsAttack:
                                    MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("スペシャルがミスした時"), null, AreaName, Targeted));
                                    break;
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region ダメージ表示、減算
                    string AddDamageText = "";

                    if (ActionArtsRow.AddAttack)
                    {
                        AddDamageText = "→追加効果！ ";

                        if (ElementalRatingType != Status.ElementalRatingType.None)
                        {
                            AddDamageText = AddDamageText + BattleCommon.GetAddMessageElementals(ElementalRatingType);
                        }

                        if (IsCritical)
                        {
                            AddDamageText = AddDamageText + "<span class=\"act_critical\">クリティカル！</span> ";
                        }
                    }
                    else
                    {
                        if (ElementalRatingType != Status.ElementalRatingType.None)
                        {
                            AddDamageText = AddDamageText + BattleCommon.GetAddMessageElementals(ElementalRatingType);
                        }

                        if (IsHit && IsCritical)
                        {
                            AddDamageText = "<span class=\"act_critical\">クリティカル！</span> ";
                        }
                    }

                    if (IsCritical)
                    {
                        // ダイハード
                        if (Mine.EffectList.FindByeffect_id(817) != null)
                        {
                            Mine.TPNow += LibInteger.GetRandMax(3, 8);
                        }
                    }

                    int HPDamage = Damage;

                    if (!IsHit)
                    {
                        if (IsMissMessage)
                        {
                            // 攻撃を回避
                            if (DamageType != Status.DamageType.Heal &&
                                DamageType > Status.DamageType.None &&
                                75 > LibInteger.GetRandBasis())
                            {
                                MessageBuilder.AppendLine(LibSerif.Serif(Targeted, LibSituation.GetNo("敵の攻撃が当たらなかった"), null, AreaName, Mine));
                            }

                            MessageBuilder.AppendLine("<dd>" + AddDamageText + Targeted.NickName + "にミス。</dd>");
                        }
                    }
                    else
                    {
                        string HitCountString = "";

                        if (HitCount > 1)
                        {
                            HitCountString = HitCount + "Hit！ ";
                        }

                        // セリフの表示
                        if (DamageType == Status.DamageType.Heal)
                        {
                            // 回復の場合
                            if (Mine.BattleID != Targeted.BattleID && 75 > LibInteger.GetRandBasis())
                            {
                                MessageBuilder.AppendLine(LibSerif.Serif(Targeted, LibSituation.GetNo("HPを回復された"), null, AreaName, Mine));
                            }
                        }
                        else if (DamageType > Status.DamageType.None)
                        {
                            // ダメージの場合

                            if (IsCritical)
                            {
                                if (75 > LibInteger.GetRandBasis() && Damage > 0)
                                {
                                    MessageBuilder.AppendLine(LibSerif.Serif(Targeted, LibSituation.GetNo("クリティカルを食らった"), null, AreaName, Mine));
                                }
                            }
                            else
                            {
                                if (75 > LibInteger.GetRandBasis() && Damage > 0)
                                {
                                    MessageBuilder.AppendLine(LibSerif.Serif(Targeted, LibSituation.GetNo("ダメージを受けた"), null, AreaName, Mine));
                                }
                            }
                        }

                        // 各種ダメージ処理
                        // MEMO: MPやTPに影響のあるものはエフェクト処理として別で処理する！

                        switch (DamageType)
                        {
                            case Status.DamageType.PhysicalDamage:
                            case Status.DamageType.MagicalDamage:
                                // ダメージ
                                if (Targeted.StatusEffect.Check(15))
                                {
                                    // 逆転
                                    MessageBuilder.AppendLine("<dd>" + HitCountString + AddDamageText + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＨＰが" + HPDamage + "回復。</dd>");
                                    Targeted.HPNow += HPDamage;
                                }
                                else
                                {
                                    MessageBuilder.AppendLine("<dd>" + HitCountString + AddDamageText + LibResultText.CSSEscapeChara(Targeted.NickName) + "に" + HPDamage + "のダメージ。</dd>");
                                    Targeted.HPNow -= HPDamage;
                                }

                                // HP減少による自動発動
                                {
                                    EffectListEntity.effect_listRow autoDownEffectRow = Targeted.EffectList.FindByeffect_id(4000);
                                    if (autoDownEffectRow != null && Targeted.HPDamageRate <= autoDownEffectRow.rank && Targeted.EffectList.FindByeffect_id((int)autoDownEffectRow.sub_rank) == null)
                                    {
                                        EffectEntity.mt_effect_listRow defRow = LibEffect.Entity.mt_effect_list.FindByeffect_id((int)autoDownEffectRow.sub_rank);
                                        LibEffect.Add((int)autoDownEffectRow.sub_rank, (int)defRow.rank_default, (int)defRow.sub_rank_default, (int)defRow.prob_default, defRow.limit_default, 0, true, ref Targeted.EffectList);
                                    }
                                }
                                break;
                            case Status.DamageType.PhysicalDrain:
                            case Status.DamageType.MagicalDrain:
                                // 吸収
                                if (Targeted.StatusEffect.Check(15) || Mine.StatusEffect.Check(15))
                                {
                                    MessageBuilder.AppendLine("<dd>" + HitCountString + AddDamageText + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＨＰが" + HPDamage + "吸収された。</dd>");
                                    Targeted.HPNow += HPDamage;
                                    Mine.HPNow -= HPDamage;
                                }
                                else
                                {
                                    MessageBuilder.AppendLine("<dd>" + HitCountString + AddDamageText + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＨＰを" + HPDamage + "吸収した。</dd>");
                                    Targeted.HPNow -= HPDamage;
                                    Mine.HPNow += HPDamage;
                                }

                                // HP減少による自動発動
                                {
                                    EffectListEntity.effect_listRow autoDownEffectRow = Targeted.EffectList.FindByeffect_id(4000);
                                    if (autoDownEffectRow != null && Targeted.HPDamageRate <= autoDownEffectRow.rank && Targeted.EffectList.FindByeffect_id((int)autoDownEffectRow.sub_rank) == null)
                                    {
                                        EffectEntity.mt_effect_listRow defRow = LibEffect.Entity.mt_effect_list.FindByeffect_id((int)autoDownEffectRow.sub_rank);
                                        LibEffect.Add((int)autoDownEffectRow.sub_rank, (int)defRow.rank_default, (int)defRow.sub_rank_default, (int)defRow.prob_default, defRow.limit_default, 0, true, ref Targeted.EffectList);
                                    }
                                }
                                break;
                            case Status.DamageType.Heal:
                                // HP回復
                                if (Targeted.StatusEffect.Check(15))
                                {
                                    // 逆転
                                    MessageBuilder.AppendLine("<dd>" + HitCountString + AddDamageText + LibResultText.CSSEscapeChara(Targeted.NickName) + "に" + HPDamage + "のダメージ。</dd>");
                                    Targeted.HPNow -= HPDamage;
                                }
                                else
                                {
                                    // 回復できるまでの量に強制変換
                                    if (HPDamage > (Targeted.HPMax - Targeted.HPNow)) { HPDamage = (Targeted.HPMax - Targeted.HPNow); }

                                    MessageBuilder.AppendLine("<dd>" + HitCountString + AddDamageText + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＨＰが" + HPDamage + "回復。</dd>");

                                    Targeted.HPNow += HPDamage;
                                }
                                break;
                        }

                        // デヴォーション効果発揮
                        if (DevotionHP > 0)
                        {
                            if (Targeted.StatusEffect.Check(15))
                            {
                                // 逆転
                                MessageBuilder.AppendLine("<dd>" + HitCountString + AddDamageText + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＭＰに" + DevotionHP + "のダメージ。</dd>");
                                Targeted.MPNow -= DevotionHP;
                            }
                            else
                            {
                                // 回復できるまでの量に強制変換
                                if (DevotionHP > (Targeted.MPMax - Targeted.MPNow)) { DevotionHP = (Targeted.MPMax - Targeted.MPNow); }

                                MessageBuilder.AppendLine("<dd>" + HitCountString + AddDamageText + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＭＰが" + DevotionHP + "回復。</dd>");

                                Targeted.MPNow += DevotionHP;
                            }
                        }
                    }
                    #endregion

                    #region ヘイト増減
                    if (ActionArtsRow.SkillType != Status.SkillType.Counter)
                    {
                        decimal GetHateRate = 1.0m;

                        //// 獲得ヘイト、隊列による補正
                        //switch (Mine.BattleFormation)
                        //{
                        //    case Status.Formation.Backs:
                        //        GetHateRate = 0.5m;
                        //        break;
                        //}

                        decimal HateRateChange = 0;

                        // 獲得ヘイト補正
                        {
                            EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(940);
                            if (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis())
                            {
                                // 敵対心アップ
                                HateRateChange += EffectRow.rank / 100m;
                            }
                        }
                        {
                            EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(941);
                            if (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis())
                            {
                                // 敵対心ダウン
                                HateRateChange -= EffectRow.rank / 100m;
                            }
                        }

                        if (HateRateChange > 0.5m) { HateRateChange = 0.5m; }
                        else if (HateRateChange < -0.5m) { HateRateChange = -0.5m; }

                        if (Mine.StatusEffect.Check(947) && !Mine.StatusEffect.Check(948))
                        {
                            HateRateChange = 0.5m;
                        }
                        if (!Mine.StatusEffect.Check(947) && Mine.StatusEffect.Check(948))
                        {
                            HateRateChange = -0.5m;
                        }

                        GetHateRate += HateRateChange;

                        if (Targeted.PartyBelong != Mine.PartyBelong)
                        {
                            // 敵対行動によるヘイト変動

                            // 敵を対象とした行動での増加ヘイト
                            int EnemyAttackHate = (int)((decimal)(ActionArtsRow.Hate * AttackActionCount) * GetHateRate);
                            Targeted.HateList.Add(Mine, EnemyAttackHate);

                            // 対象外の相手へのヘイト
                            foreach (LibUnitBase TargetOut in EnemysLive)
                            {
                                if (TargetOut.BattleID != Targeted.BattleID)
                                {
                                    continue;
                                }

                                TargetOut.HateList.Add(Mine, (int)((decimal)EnemyAttackHate * 0.2m));
                            }

                            // 自分以外に対するヘイト変動
                            int FriendAttackDownHate = ActionArtsRow.VHate * AttackActionCount;

                            foreach (LibUnitBase TargetOut in FriendsLive)
                            {
                                if (TargetOut.BattleID != Mine.BattleID)
                                {
                                    continue;
                                }

                                int DownHate = FriendAttackDownHate * -1;

                                decimal DownGetHateRate = 1.0m;

                                {
                                    EffectListEntity.effect_listRow EffectRow = Targeted.EffectList.FindByeffect_id(942);
                                    if (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis())
                                    {
                                        // 減少ヘイトダウン
                                        DownGetHateRate -= EffectRow.rank / 100m;
                                    }
                                }
                                {
                                    EffectListEntity.effect_listRow EffectRow = Targeted.EffectList.FindByeffect_id(943);
                                    if (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis())
                                    {
                                        // 減少ヘイトアップ
                                        DownGetHateRate += EffectRow.rank / 100m;
                                    }
                                }

                                Targeted.HateList.Add(TargetOut, (int)Math.Round((decimal)DownHate * DownGetHateRate, MidpointRounding.AwayFromZero));
                            }

                            // ファーストタッチ処理
                            if (Targeted.IsFirstTouth)
                            {
                                Targeted.HateList.Add(Mine, 10);
                            }

                            Targeted.IsFirstTouth = false;
                        }
                        else
                        {
                            // 回復行動によるヘイト変動
                            int FriendCureHate = (int)((decimal)(ActionArtsRow.DHate * AttackActionCount) * GetHateRate);

                            foreach (LibUnitBase TargetOut in EnemysLive)
                            {
                                TargetOut.HateList.Add(Mine, FriendCureHate);
                            }
                        }
                    }
                    #endregion

                    #region 戦闘不能
                    if (Targeted.HPNow == 0 && !Targeted.BattleOut)
                    {
                        // 最新の生存者取得
                        List<LibUnitBase> LiveEnemyList = new List<LibUnitBase>();
                        LiveEnemyList = LibBattleCharacter.GetLive(Enemys);

                        if (LiveEnemyList.Count == 1)
                        {
                            // 敵を全滅
                            MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("全滅させた"), null, AreaName, Targeted));
                        }
                        else
                        {
                            // トドメのみ
                            MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("トドメを刺した"), null, AreaName, Targeted));
                        }

                        switch (Targeted.PartyBelong)
                        {
                            case Status.Belong.Friend:
                                if (Targeted.EffectList.FindByeffect_id(2105) != null)
                                {
                                    MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Targeted.NickName) + "ははじけ飛んだ…。</dd>");
                                }
                                else
                                {
                                    MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Targeted.NickName) + "は倒された…。</dd>");
                                }
                                break;
                            case Status.Belong.Enemy:
                                if (Mine.EffectList.FindByeffect_id(2105) != null)
                                {
                                    MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Targeted.NickName) + "がはじけ飛んだ！</dd>");
                                }
                                else
                                {
                                    MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Targeted.NickName) + "を倒した！</dd>");
                                }
                                break;
                        }

                        MessageBuilder.AppendLine(LibSerif.Serif(Targeted, LibSituation.GetNo("戦闘不能時"), null, AreaName, Mine));

                        foreach (LibUnitBase FriendShip in EnemysLive)
                        {
                            if (FriendShip.BattleID != Targeted.BattleID)
                            {
                                StringBuilder addMessage = new StringBuilder();
                                switch (Targeted.MemberNumber)
                                {
                                    case 0:
                                        addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方A<chara_name>が倒れた時"), null, AreaName, Targeted));
                                        break;
                                    case 1:
                                        addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方B<chara_name>が倒れた時"), null, AreaName, Targeted));
                                        break;
                                    case 2:
                                        addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方C<chara_name>が倒れた時"), null, AreaName, Targeted));
                                        break;
                                    case 3:
                                        addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方D<chara_name>が倒れた時"), null, AreaName, Targeted));
                                        break;
                                    case 4:
                                        addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方E<chara_name>が倒れた時"), null, AreaName, Targeted));
                                        break;
                                    case 5:
                                        addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方F<chara_name>が倒れた時"), null, AreaName, Targeted));
                                        break;
                                }

                                if (addMessage.Length == 0)
                                {
                                    addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方が倒れた時"), null, AreaName, Targeted));
                                }

                                MessageBuilder.AppendLine(addMessage.ToString());
                            }
                        }

                        Mine.DestroyCount++;
                        BattleCommon.DeadMans(Targeted, BattleCharacer);
                    }
                    #endregion

                    // 与えたダメージカウント
                    Mine.TempBattleScore.MaxAttackDamage = Damage;

                    // リヴァイバル
                    if (Targeted.BattleOut && Targeted.StatusEffect.Check(78))
                    {
                        // 発動
                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "のリヴァイバル！</dd>");
                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "は生き返った。</dd>");

                        BattleCommon.Revivals(Targeted, BattleCharacer, 50);
                    }

                    if (IsSleepClear)
                    {
                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の" + LibStatusList.GetName(2) + "の効果が切れた。</dd>");
                    }
                    if (IsConfuseClear)
                    {
                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の" + LibStatusList.GetName(12) + "の効果が切れた。</dd>");
                    }

                    #region 追加効果判定
                    bool IsAdditionalEffectHit = false;
                    decimal EffectHitRate = (decimal)ActionArtsRow.HitRate;

                    if (DamageType == Status.DamageType.None)
                    {
                        // 命中率変更
                        EffectHitRate = EffectHitRate + ((decimal)Mine.UNQ - (decimal)Targeted.UNQ);

                        if (EffectHitRate < 0)
                        {
                            // バリアー
                            if (Targeted.StatusEffect.Check(75))
                            {
                                EffectHitRate = 0;
                            }
                            else
                            {
                                EffectHitRate = 100;
                            }
                        }
                        else
                        {
                            // バリアー
                            if (Targeted.StatusEffect.Check(75))
                            {
                                EffectHitRate *= 0.5m;
                            }

                            // アスティオン
                            if (Mine.StatusEffect.Check(74))
                            {
                                EffectHitRate *= 1.5m;
                            }

                            // HPMax魔法威力アップ
                            if (Mine.EffectList.FindByeffect_id(771) != null && Mine.HPMax == Mine.HPNow)
                            {
                                EffectHitRate *= 2m;
                            }

                            // 瀕死魔法威力アップ
                            if (Mine.EffectList.FindByeffect_id(773) != null && Mine.HPDamageRate <= 20)
                            {
                                EffectHitRate *= 2m;
                            }

                            // 瀕死魔法防御アップ
                            if (Targeted.EffectList.FindByeffect_id(775) != null && Targeted.HPDamageRate <= 20)
                            {
                                EffectHitRate *= 0.5m;
                            }
                        }

                        // 状態異常効果アップ
                        if (Targeted.EffectList.FindByeffect_id(765) != null)
                        {
                            EffectHitRate = 100;
                        }
                    }

                    // バリアチェンジはエフェクト：「属性相性変化：火」などで対応。このエフェクトは直接属性相性を書き換える（Rankで上書き）

                    if (Targeted.BattleOut == false && IsHit)
                    {
                        foreach (EffectListEntity.effect_listRow EffectRow in ActionArtsRow.EffectList)
                        {
                            int EffectID = EffectRow.effect_id;
                            decimal Rank = EffectRow.rank;
                            decimal SubRank = EffectRow.sub_rank;
                            decimal Prob = EffectRow.prob;
                            int EndLimit = EffectRow.endlimit;
                            int EffectDiv = EffectRow.effect_div;

                            // 違う状態異常アーツを使うと命中アップ
                            {
                                EffectListEntity.effect_listRow MineEffectRow = Mine.EffectList.FindByeffect_id(1622);
                                if (MineEffectRow != null &&
                                    DamageType == Status.DamageType.None)
                                {
                                    if (Mine.LastUsingSkillID != ActionArtsRow.SkillID)
                                    {
                                        Mine.UsingArtsEffectLv++;
                                        Prob += (int)(Mine.UsingArtsEffectLv * EffectRow.rank);
                                    }
                                    else
                                    {
                                        Mine.LastUsingSkillID = ActionArtsRow.SkillID;
                                        Mine.UsingArtsEffectLv = 0;
                                    }
                                }
                            }

                            // 状態変化発生の判定
                            if (Prob < LibInteger.GetRandBasis())
                            {
                                // 発生しない
                                continue;
                            }

                            IsAdditionalEffectHit = true;

                            switch (EffectID)
                            {
                                case 0:
                                    break;
                                case 1:
                                    #region 即死
                                    if (!Targeted.StatusEffect.Regist(EffectID) && EffectHitRate > LibInteger.GetRandBasis())
                                    {
                                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "に即死効果！</dd>");
                                        MessageBuilder.AppendLine("<dd class=\"act_dead\">" + Mine.NickName + "は" + LibResultText.CSSEscapeChara(Targeted.NickName) + "を倒した！</dd>");
                                        BattleCommon.DeadMans(Targeted, BattleCharacer);
                                    }
                                    else
                                    {
                                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                    }
                                    #endregion
                                    break;
                                case 715:
                                    #region ディスペル
                                    {
                                        bool IsDispel = false;

                                        for (i = 0; i < Rank; i++)
                                        {
                                            int StatusID = 0;
                                            if (EffectHitRate > LibInteger.GetRandBasis() && Targeted.StatusEffect.Dispel(ref StatusID))
                                            {
                                                IsDispel = true;
                                                MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の" + LibStatusList.GetName(StatusID) + "の効果を消し去った。</dd>");
                                            }
                                        }

                                        if (!IsDispel)
                                        {
                                            MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                        }
                                    }
                                    #endregion
                                    break;
                                case 716:
                                    #region クリアランス
                                    {
                                        bool IsClearance = false;

                                        for (i = 0; i < Rank; i++)
                                        {
                                            int StatusID = 0;
                                            if (EffectHitRate > LibInteger.GetRandBasis() && Targeted.StatusEffect.Clearlance(ref StatusID))
                                            {
                                                IsClearance = true;
                                                MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の" + LibStatusList.GetName(StatusID) + "の効果を打ち消した。</dd>");
                                            }
                                        }

                                        if (!IsClearance)
                                        {
                                            MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                        }
                                    }
                                    #endregion
                                    break;
                                case 717:
                                    #region エスナ
                                    {
                                        bool IsEsna = false;

                                        for (i = 0; i < Rank; i++)
                                        {
                                            int StatusID = 0;
                                            if (EffectHitRate > LibInteger.GetRandBasis() && Targeted.StatusEffect.Esna(ref StatusID))
                                            {
                                                IsEsna = true;
                                                MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の" + LibStatusList.GetName(StatusID) + "の効果を解消した。</dd>");
                                            }
                                        }

                                        if (!IsEsna)
                                        {
                                            MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                        }
                                    }
                                    #endregion
                                    break;
                                case 800:
                                    #region スティール
                                    if (Mine.GetType() == typeof(LibPlayer) && Targeted.GetType() == typeof(LibMonster) && !((LibMonster)Targeted).IsSteal)
                                    {
                                        bool IsStealOn = false;

                                        // モンスターの場合
                                        ((LibMonster)Targeted).HaveItem.DefaultView.RowFilter = "drop_type=1";
                                        ((LibMonster)Targeted).HaveItem.DefaultView.Sort = "get_synx desc";

                                        if (Targeted.HaveItem.DefaultView.Count == 0 || (Targeted.HaveItem.DefaultView.Count == 1 && (int)Targeted.HaveItem.DefaultView[0]["it_num"] == 0 && (int)Targeted.HaveItem.DefaultView[0]["it_box_count"] == 0))
                                        {
                                            // 何も盗めない
                                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "は何も持っていない！</dd>");

                                            break;
                                        }

                                        // スティールアップがあるか
                                        bool IsStealUp = false;
                                        if (Mine.EffectList.FindByeffect_id(802) != null)
                                        {
                                            IsStealUp = true;
                                        }

                                        // 複数同時に盗むか
                                        bool IsStealDouble = false;
                                        if (Mine.EffectList.FindByeffect_id(803) != null)
                                        {
                                            IsStealDouble = true;
                                        }

                                        foreach (DataRowView HaveItemRow in Targeted.HaveItem.DefaultView)
                                        {
                                            int ProbSteal = 0;

                                            // 確率の設定
                                            switch ((int)HaveItemRow["get_synx"])
                                            {
                                                case 0:
                                                    ProbSteal = 1;
                                                    if (IsStealUp) { ProbSteal = 3; }
                                                    break;
                                                case 1:
                                                    ProbSteal = 3;
                                                    if (IsStealUp) { ProbSteal = 6; }
                                                    break;
                                                case 2:
                                                    ProbSteal = 10;
                                                    if (IsStealUp) { ProbSteal = 30; }
                                                    break;
                                                case 3:
                                                    ProbSteal = 55;
                                                    if (IsStealUp) { ProbSteal = 80; }
                                                    break;
                                                case 4:
                                                    ProbSteal = 100;
                                                    break;
                                            }

                                            if (ProbSteal > LibInteger.GetRandBasis())
                                            {
                                                // アイテム取得
                                                int ItemID = (int)HaveItemRow["it_num"];
                                                int ItemCount = (int)HaveItemRow["it_box_count"];
                                                int RestItemCount = 0;

                                                if (ItemCount > 0)
                                                {
                                                    IsStealOn = true;
                                                    if (ItemID > 0)
                                                    {
                                                        if (LibItem.CheckRare(ItemID, false))
                                                        {
                                                            // レアの場合は一個のみ
                                                            ItemCount = 1;
                                                        }

                                                        string CountItemStok = "";
                                                        if (ItemCount > 1)
                                                        {
                                                            CountItemStok = ItemCount + "個";
                                                        }

                                                        if (((LibPlayer)Mine).AddItem(Status.ItemBox.Normal, ItemID, false, ref ItemCount, ref RestItemCount))
                                                        {
                                                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "から" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を" + CountItemStok + "盗んだ。</dd>");
                                                        }
                                                        else
                                                        {
                                                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "から" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を" + CountItemStok + "盗んだが、これ以上アイテムを持てない！</dd>");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "から" + LibResultText.CSSEscapeMoney(ItemCount, false) + "を盗んだ。</dd>");
                                                        ((LibPlayer)Mine).HaveMoney += ItemCount;
                                                    }
                                                }
                                                else
                                                {
                                                    continue;
                                                }

                                                // 一度ぬすんだものはもう盗めない
                                                HaveItemRow.Delete();

                                                if (!IsStealDouble)
                                                {
                                                    break;
                                                }
                                            }
                                        }

                                        if (IsStealOn)
                                        {
                                            ((LibMonster)Targeted).IsSteal = true;
                                        }
                                        else
                                        {
                                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "から盗むのに失敗した。</dd>");
                                        }
                                    }
                                    else
                                    {
                                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "は何も持っていない。</dd>");
                                    }
                                    #endregion
                                    break;
                                case 820:
                                    #region 追加アーツ
                                    if (!Mine.StatusEffect.Check(77) || !ActionArtsRow.IsNormalAttack || ActionArtsRow.ActionBase != Status.ActionBaseType.MainAttack)
                                    {
                                        List<LibActionType> CounterActionList = new List<LibActionType>();
                                        List<LibUnitBase> CounterTargetList = new List<LibUnitBase>();

                                        CounterActionList = BattleActionSelect.Select(Mine, Status.ActionType.SelectionArtsAttack, false, (int)Rank, true, 0);

                                        if (SubRank == 1)
                                        {
                                            CounterTargetList.Add(Targeted);
                                        }
                                        else
                                        {
                                            CounterTargetList.Add(Mine);
                                        }

                                        BattleAttack(Mine, CounterActionList, CounterTargetList, MessageBuilder, AreaName, Turn);
                                    }
                                    #endregion
                                    break;
                                case 840:
                                    #region HP回復（固定値）
                                    {
                                        Targeted.HPNow += (int)Rank;
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＨＰが" + (int)Rank + "回復！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 841:
                                    #region MP回復（固定値）
                                    {
                                        Targeted.MPNow += (int)Rank;
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＭＰが" + (int)Rank + "回復！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 842:
                                    #region TP回復（固定値）
                                    {
                                        Targeted.TPNow += (int)Rank;
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＴＰが" + (int)Rank + "上昇！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 843:
                                    #region HP回復（割合）
                                    {
                                        int CureHPRates = (int)((decimal)Targeted.HPMax * Rank / 100m);
                                        Targeted.HPNow += CureHPRates;
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＨＰが" + CureHPRates + "回復！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 844:
                                    #region MP回復（割合）
                                    {
                                        int CureMPRates = (int)((decimal)Targeted.MPMax * Rank / 100m);
                                        Targeted.MPNow += CureMPRates;
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＭＰが" + CureMPRates + "回復！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 854:
                                    #region MP吸収
                                    {
                                        int MPDrainMin = Mine.Level / 2;
                                        int MPDrainMax = (int)((decimal)Mine.Level * Rank);
                                        int MPDrain = LibInteger.GetRandMax(MPDrainMin, MPDrainMax);
                                        Targeted.MPNow -= MPDrain;
                                        Mine.MPNow += MPDrain;
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "からＭＰを" + MPDrain + "吸収！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 858:
                                    #region HP吸収
                                    {
                                        int HPDrain = (int)((decimal)Damage * Rank / 100m);
                                        Targeted.HPNow -= HPDrain;
                                        Mine.HPNow += HPDrain;
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "からＨＰを" + HPDrain + "吸収！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 873:
                                    #region 密漁
                                    if (Mine.GetType() == typeof(LibPlayer) && Targeted.GetType() == typeof(LibMonster) && ((LibMonster)Targeted).HPDamageRate < 20)
                                    {
                                        // そもそもアイテムを持っているか
                                        bool IsPoacherOn = false;

                                        // モンスターの場合
                                        ((LibMonster)Targeted).HaveItem.DefaultView.RowFilter = "drop_type=2";
                                        ((LibMonster)Targeted).HaveItem.DefaultView.Sort = "get_synx desc";

                                        if (Targeted.HaveItem.DefaultView.Count == 0 || (Targeted.HaveItem.DefaultView.Count == 1 && (int)Targeted.HaveItem.DefaultView[0]["get_item_no"] == 0 && (int)Targeted.HaveItem.DefaultView[0]["item_count"] == 0))
                                        {
                                            // 何もなし
                                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった！</dd>");

                                            break;
                                        }

                                        foreach (DataRowView HaveItemRow in Targeted.HaveItem.DefaultView)
                                        {
                                            int ProbSteal = 0;

                                            // 確率の設定
                                            switch ((int)HaveItemRow["get_synx"])
                                            {
                                                case 0:
                                                    ProbSteal = 1;
                                                    break;
                                                case 1:
                                                    ProbSteal = 3;
                                                    break;
                                                case 2:
                                                    ProbSteal = 10;
                                                    break;
                                                case 3:
                                                    ProbSteal = 55;
                                                    break;
                                                case 4:
                                                    ProbSteal = 100;
                                                    break;
                                            }

                                            if (ProbSteal > LibInteger.GetRandBasis())
                                            {
                                                // アイテム取得
                                                int ItemID = (int)HaveItemRow["get_item_no"];
                                                int ItemCount = (int)HaveItemRow["item_count"];
                                                int RestItemCount = 0;

                                                if (ItemCount > 0)
                                                {
                                                    IsPoacherOn = true;
                                                    if (ItemID > 0)
                                                    {
                                                        if (LibItem.CheckRare(ItemID, false))
                                                        {
                                                            // レアの場合は一個のみ
                                                            ItemCount = 1;
                                                        }

                                                        string CountItemStok = "";
                                                        if (ItemCount > 1)
                                                        {
                                                            CountItemStok = ItemCount + "個";
                                                        }

                                                        if (((LibPlayer)Mine).AddItem(Status.ItemBox.Normal, ItemID, false, ref ItemCount, ref RestItemCount))
                                                        {
                                                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "から" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を" + CountItemStok + "手に入れた。</dd>");
                                                        }
                                                        else
                                                        {
                                                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "から" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を" + CountItemStok + "手に入れたが、これ以上アイテムを持てない！</dd>");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "から" + LibResultText.CSSEscapeMoney(ItemCount, false) + "を手に入れた。</dd>");
                                                        ((LibPlayer)Mine).HaveMoney += ItemCount;
                                                    }
                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                            }
                                        }

                                        if (!IsPoacherOn)
                                        {
                                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                        }
                                        else
                                        {
                                            // 一撃死
                                            MessageBuilder.AppendLine("<dd class=\"act_dead\">" + Mine.NickName + "は" + LibResultText.CSSEscapeChara(Targeted.NickName) + "を捕獲した！</dd>");
                                            BattleCommon.DeadMans(Targeted, BattleCharacer);
                                        }
                                    }
                                    else
                                    {
                                        MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                    }
                                    #endregion
                                    break;
                                case 874:
                                    #region ファストコイン
                                    if (Mine.GetType() == typeof(LibPlayer) && Targeted.GetType() == typeof(LibMonster))
                                    {
                                        // ダメージ量の0.1倍のギムルゲット
                                        int FastCoin = (int)((decimal)Damage * 0.1m);

                                        if (FastCoin > 0)
                                        {
                                            // 何もなし
                                            MessageBuilder.AppendLine("<dd>さらに" + LibResultText.CSSEscapeMoney(FastCoin, true) + "を拾った！</dd>");
                                            ((LibPlayer)Mine).HaveMoney += FastCoin;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 915:
                                    #region ノックバック
                                    if (Targeted.BattleFormation != Status.Formation.Backs && EffectHitRate > LibInteger.GetRandBasis())
                                    {
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "にノックバックの効果。</dd>");
                                        Targeted.BattleFormation++;
                                    }
                                    else
                                    {
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                    }
                                    #endregion
                                    break;
                                case 925:
                                    #region 割合ダメージ
                                    {
                                        if (SubRank == 0)
                                        {
                                            int WDamage = (int)((decimal)Targeted.HPNow * Rank / 100m);
                                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "に" + WDamage + "のダメージ。</dd>");
                                            Targeted.HPNow -= WDamage;
                                        }
                                        else
                                        {
                                            int WDamage = (int)((decimal)Targeted.MPNow * Rank / 100m);
                                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＭＰに" + WDamage + "のダメージ。</dd>");
                                            Targeted.MPNow -= WDamage;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 930:
                                    #region ＨＰ交換
                                    int MineHP = Mine.HPNow;
                                    Mine.HPNow = Targeted.HPNow;
                                    Targeted.HPNow = MineHP;
                                    MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "とＨＰを交換した！</dd>");
                                    #endregion
                                    break;
                                case 931:
                                    #region ＭＰ交換
                                    int MineMP = Mine.MPNow;
                                    Mine.MPNow = Targeted.MPNow;
                                    Targeted.MPNow = MineMP;
                                    MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "とＭＰを交換した！</dd>");
                                    #endregion
                                    break;
                                case 932:
                                    #region ＴＰ交換
                                    int MineTP = Mine.TPNow;
                                    Mine.TPNow = Targeted.TPNow;
                                    Targeted.TPNow = MineTP;
                                    MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "とＴＰを交換した！</dd>");
                                    #endregion
                                    break;
                                case 945:
                                    #region エンゲージ
                                    if (!Targeted.StatusEffect.Check(946))
                                    {
                                        {
                                            EffectListEntity.effect_listRow ProvokerEffectRow = Mine.EffectList.FindByeffect_id(2128);
                                            if (ProvokerEffectRow != null)
                                            {
                                                // プロヴォーカー
                                                EndLimit += (int)EffectRow.rank;
                                            }
                                        }

                                        Targeted.StatusEffect.Add(945, Mine.BattleID, 0, EndLimit, Mine.Level, true);
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "は" + Mine.NickName + "にエンゲージされた！</dd>");
                                    }
                                    else
                                    {
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "はステルス状態であるため、エンゲージできない！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 946:
                                    #region ステルス
                                    if (!Targeted.StatusEffect.Check(945))
                                    {
                                        Targeted.StatusEffect.Add(946, 1, 0, 4, Mine.Level, true);
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "のステルス！</dd>");
                                    }
                                    else
                                    {
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "はエンゲージされているため、ステルス状態になれない！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 947:
                                    #region 挑発
                                    {
                                        Targeted.StatusEffect.Add(947, Mine.BattleID, 0, 4, Mine.Level, true);
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "にアピール！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 948:
                                    #region エピーズ
                                    {
                                        Targeted.StatusEffect.Add(948, 1, 0, 4, Mine.Level, true);
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "にエピーズ！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 970:
                                    #region 属性相性：火
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Fire < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Fire = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 971:
                                    #region 属性相性：氷
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Freeze < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Freeze = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 972:
                                    #region 属性相性：風
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Air < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Air = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 973:
                                    #region 属性相性：土
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Earth < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Earth = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 974:
                                    #region 属性相性：水
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Water < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Water = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 975:
                                    #region 属性相性：雷
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Thunder < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Thunder = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 976:
                                    #region 属性相性：聖
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Holy < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Holy = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 977:
                                    #region 属性相性：闇
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Dark < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Dark = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 978:
                                    #region 属性相性：斬
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Slash < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Slash = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 979:
                                    #region 属性相性：突
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Pierce < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Pierce = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 980:
                                    #region 属性相性：打
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Strike < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Strike = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 981:
                                    #region 属性相性：壊
                                    {
                                        if ((int)SubRank == 1 || Targeted.DefenceElemental.Break < (int)Rank)
                                        {
                                            Targeted.DefenceElemental.Break = (int)Rank;
                                        }
                                    }
                                    #endregion
                                    break;
                                case 1063:
                                    #region サーチギムル
                                    if (Mine.GetType() == typeof(LibPlayer))
                                    {
                                        int MaxGiml = 100 + Mine.Level * 2;

                                        int GetGiml = LibInteger.GetRand(MaxGiml) + 1;

                                        ((LibPlayer)Mine).HaveMoney += GetGiml;
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeMoney(GetGiml, false) + "を見つけた！</dd>");
                                    }
                                    #endregion
                                    break;
                                case 1110:
                                    #region 増援
                                    if (Mine.GetType() == typeof(LibMonster))
                                    {
                                        int RainMonsterID = Mine.EntryNo;
                                        if (Rank > 0) { RainMonsterID = (int)Rank; }

                                        string NickName = "";

                                        for (int RainCount = 0; RainCount < (int)SubRank; RainCount++)
                                        {
                                            // 敵
                                            if (monsterRaces.ContainsKey(RainMonsterID))
                                            {
                                                if (monsterRaces[RainMonsterID] == 0)
                                                {
                                                    monsterRaces[RainMonsterID] = 2;
                                                }
                                                else
                                                {
                                                    monsterRaces[RainMonsterID]++;
                                                }
                                            }
                                            else
                                            {

                                                monsterRaces[RainMonsterID] = 0;
                                            }

                                            SetCurrentBattleID++;
                                            LibUnitBase Member = new LibMonster(Status.Belong.Enemy, RainMonsterID, BaseLevel);
                                            Member.BattleID = SetCurrentBattleID;
                                            Member.MemberNumber = EnemyMemberCount;
                                            Member.MonsterMulti = monsterRaces[RainMonsterID];
                                            BattleCharacer.Add(Member);

                                            EnemyMemberCount++;

                                            NickName = Member.NickName;
                                        }

                                        string RainCountString = "";
                                        if ((int)SubRank > 1)
                                        {
                                            RainCountString = (int)SubRank + "体、";
                                        }

                                        MessageBuilder.AppendLine("<dd>→" + NickName + "が" + RainCountString + "新たに出現した！</dd>");

                                        // メンバーが増加したので再度メンバーリスト修正
                                        BattleCharacerLive.Clear();
                                        BattleCharacerLive = LibBattleCharacter.GetLive(BattleCharacer);

                                        // モンスターの取得
                                        Monsters.Clear();
                                        Monsters = LibBattleCharacter.GetMonsters(BattleCharacer);

                                        // ノンプレイヤーの取得
                                        NonPlayers.Clear();
                                        NonPlayers = LibBattleCharacter.GetNonPlayers(BattleCharacer);
                                    }
                                    #endregion
                                    break;
                                case 2001:
                                    #region 自爆・崩壊
                                    string BombClearText = "";
                                    switch ((int)Rank)
                                    {
                                        case 1:
                                            BombClearText = "ははじけとんだ！";
                                            break;
                                    }
                                    MessageBuilder.AppendLine("<dd>" + Mine.NickName + BombClearText + "</dd>");
                                    MessageBuilder.AppendLine("<dd class=\"act_dead\">" + Mine.NickName + "は倒れた。</dd>");
                                    BattleCommon.DeadMans(Mine, BattleCharacer);
                                    #endregion
                                    break;
                                default:
                                    #region ステータス変化
                                    if ((ActionArtsRow.AttackType == Status.AttackType.Song || ActionArtsRow.AttackType == Status.AttackType.Dance) && EffectID >= 200 && EffectID < 250)
                                    {
                                        // 基礎レベルの算出
                                        if (Mine.GetType() == typeof(LibPlayer))
                                        {
                                            if (((LibPlayer)Mine).IntallClassID == 17)
                                            {
                                                Rank += ((LibPlayer)Mine).InstallClassLevel / 10;
                                            }
                                            if (((LibPlayer)Mine).SecondryIntallClassID == 17)
                                            {
                                                Rank += ((LibPlayer)Mine).SecondryInstallClassLevel / 10;
                                            }
                                        }

                                        // エフェクトによる強化
                                        if (ActionArtsRow.AttackType == Status.AttackType.Song)
                                        {
                                            // 効果アップ
                                            EffectListEntity.effect_listRow EffectSongRow = Mine.EffectList.FindByeffect_id(1190);
                                            if (EffectSongRow != null)
                                            {
                                                Rank += (int)EffectSongRow.rank;
                                            }

                                            // スタンドバイミー
                                            if (Mine.StatusEffect.Check(100))
                                            {
                                                EndLimit *= 2;
                                            }
                                        }
                                        if (ActionArtsRow.AttackType == Status.AttackType.Dance)
                                        {
                                            // 効果アップ
                                            EffectListEntity.effect_listRow EffectDanceRow = Mine.EffectList.FindByeffect_id(1195);
                                            if (EffectDanceRow != null)
                                            {
                                                Rank += (int)EffectDanceRow.rank;
                                            }

                                            // コレオグラフ
                                            if (Mine.StatusEffect.Check(101))
                                            {
                                                EndLimit *= 2;
                                            }
                                        }

                                        // ステータスによる変化
                                        if ((ActionArtsRow.AttackType == Status.AttackType.Song && Mine.StatusEffect.Check(257)) ||
                                            (ActionArtsRow.AttackType == Status.AttackType.Dance && Mine.StatusEffect.Check(257)))
                                        {
                                            Rank += 3;
                                        }
                                    }

                                    switch (EffectID)
                                    {
                                        case 3:
                                            // 猛毒だったら…？
                                            // ブラッドポイズン
                                            if (ActionArtsRow.SkillID == 4200 &&
                                                Mine.EffectList.FindByeffect_id(2133) != null)
                                            {
                                                Rank++;
                                            }
                                            break;
                                        case 19:
                                            // スタン
                                            if (Targeted.IsActionEnd)
                                            {
                                                MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                                continue;
                                            }
                                            break;
                                        case 64:
                                            {
                                                // 庇う
                                                Rank = Mine.BattleID;
                                            }
                                            break;
                                    }

                                    if (EffectID > 1 && EffectID < 250 && LibStatusList.CheckGoodStatus(EffectID) && Targeted.EffectList.FindByeffect_id(2104) != null)
                                    {
                                        EndLimit += 2;
                                    }

                                    // 自分自身へのグッドステータス効果時間延長
                                    {
                                        EffectListEntity.effect_listRow MineEffectRow = Mine.EffectList.FindByeffect_id(1623);
                                        if (EffectID > 1 && EffectID < 300 && LibStatusList.CheckGoodStatus(EffectID) && MineEffectRow != null && Mine.BattleID == Targeted.BattleID)
                                        {
                                            EndLimit += (int)MineEffectRow.rank;
                                        }
                                    }

                                    {
                                        EffectListEntity.effect_listRow TargetRegistEffectRow = Targeted.EffectList.FindByeffect_id(EffectID + 6000);
                                        if (EffectID > 1 && EffectID < 300 && !LibStatusList.CheckGoodStatus(EffectID) && TargetRegistEffectRow != null)
                                        {
                                            EndLimit -= Math.Max(1, (int)((decimal)EndLimit * TargetRegistEffectRow.rank / 100m));
                                        }
                                    }

                                    if (Rank > 0)
                                    {
                                        if (EffectID > 1 && EffectID < 300)
                                        {
                                            int HitRand = (int)LibInteger.GetRandBasis();

                                            // ステータス追加
                                            if ((LibStatusList.CheckGoodStatus(EffectID) || EffectHitRate >= HitRand) && Targeted.StatusEffect.AddWithRegist(EffectID, (int)Rank, (int)SubRank, EndLimit, Mine.Level, true))
                                            {
                                                MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "に" + LibStatusList.GetName(EffectID) + "の効果。</dd>");
                                            }
                                            else
                                            {
                                                if (LibStatusList.CheckGoodStatus(EffectID))
                                                {
                                                    MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                                }
                                                else
                                                {
                                                    MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "は" + LibStatusList.GetName(EffectID) + "の効果をレジスト！</dd>");
                                                }
                                            }
                                        }
                                        else if (EffectID > 500 && EffectID < 700)
                                        {
                                            // ステータス追加
                                            if (Targeted.StatusEffect.Add(EffectID, (int)Rank, (int)SubRank, EndLimit, Mine.Level, true))
                                            {
                                                MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "に" + LibStatusList.GetName(EffectID) + "の効果。</dd>");
                                            }
                                            else
                                            {
                                                MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                            }
                                        }
                                        else if (EffectID > 301 && EffectID < 500)
                                        {
                                            EffectID -= 300;
                                            // ステータス解除
                                            if (Targeted.StatusEffect.Delete(EffectID))
                                            {
                                                MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の" + LibStatusList.GetName(EffectID) + "が解除された。</dd>");
                                            }
                                            else
                                            {
                                                MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBuilder.AppendLine("<dd>→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                                    }
                                    #endregion
                                    break;
                            }
                        }
                    }

                    if (Targeted.BattleOut == false && Damage == 0 && IsHit && !IsAdditionalEffectHit && ActionArtsRow.EffectList.Count > 0)
                    {
                        if ((Mine.PartyBelong == Status.Belong.Friend && Targeted.PartyBelong == Status.Belong.Friend) ||
                            (Mine.PartyBelong == Status.Belong.Enemy && Targeted.PartyBelong == Status.Belong.Enemy))
                        {
                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "には効果がなかった。</dd>");
                        }
                        else
                        {
                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "にミス。</dd>");
                        }
                    }
                    #endregion

                    #region カウンター

                    // カウンター
                    {
                        EffectListEntity.effect_listRow EffectRow = Targeted.EffectList.FindByeffect_id(790);
                        if ((ActionArtsRow.ActionBase == Status.ActionBaseType.MainAttack ||
                            ActionArtsRow.ActionBase == Status.ActionBaseType.SubAttack) &&
                            DamageType != Status.DamageType.Heal &&
                            (EffectRow != null && ((decimal)Targeted.UNQ / 2.5m) > LibInteger.GetRandBasis()) &&
                            ActionArtsRow.SkillType == Status.SkillType.Normal)
                        {
                            List<LibActionType> CounterActionList = new List<LibActionType>();
                            List<LibUnitBase> CounterTargetList = new List<LibUnitBase>();

                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "のカウンター！</dd>");
                            MessageBuilder.AppendLine(LibSerif.Serif(Targeted, LibSituation.GetNo("カウンターが発動"), null, AreaName, Mine));

                            CounterActionList = BattleActionSelect.Select(Targeted, Status.ActionType.MainAttack, true, 0, false, 0);
                            CounterTargetList.Add(Mine);

                            BattleAttack(Targeted, CounterActionList, CounterTargetList, MessageBuilder, AreaName, Turn);
                        }
                    }

                    // 反撃スキル
                    {
                        EffectListEntity.effect_listRow EffectRow = Targeted.EffectList.FindByeffect_id(792);
                        if ((ActionArtsRow.ActionBase == Status.ActionBaseType.MainAttack ||
                            ActionArtsRow.ActionBase == Status.ActionBaseType.SubAttack) &&
                            DamageType != Status.DamageType.Heal &&
                            (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis()) &&
                            ActionArtsRow.SkillType == Status.SkillType.Normal)
                        {
                            List<LibActionType> CounterActionList = new List<LibActionType>();
                            List<LibUnitBase> CounterTargetList = new List<LibUnitBase>();

                            int SkillNo = (int)EffectRow.rank;

                            CounterActionList = BattleActionSelect.Select(Targeted, Status.ActionType.SelectionArtsAttack, true, SkillNo, false, 0);
                            CounterTargetList.Add(Mine);

                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の" + CounterActionList[0].ArtsName + "！</dd>");

                            BattleAttack(Targeted, CounterActionList, CounterTargetList, MessageBuilder, AreaName, Turn);
                        }
                    }

                    // 弱点魔法を喰らったら反撃
                    {
                        EffectListEntity.effect_listRow EffectRow = Targeted.EffectList.FindByeffect_id(793);
                        if (ElementalRatingType == Status.ElementalRatingType.Weak &&
                            DamageType != Status.DamageType.Heal &&
                            (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis()))
                        {
                            List<LibActionType> CounterActionList = new List<LibActionType>();
                            List<LibUnitBase> CounterTargetList = new List<LibUnitBase>();

                            int SkillNo = (int)EffectRow.rank;

                            CounterActionList = BattleActionSelect.Select(Targeted, Status.ActionType.SelectionArtsAttack, true, SkillNo, false, 0);
                            CounterTargetList.Add(Mine);

                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の" + CounterActionList[0].ArtsName + "！</dd>");

                            BattleAttack(Targeted, CounterActionList, CounterTargetList, MessageBuilder, AreaName, Turn);
                        }
                    }

                    // 逆襲のサンバ
                    {
                        if ((ActionArtsRow.ActionBase == Status.ActionBaseType.MainAttack ||
                            ActionArtsRow.ActionBase == Status.ActionBaseType.SubAttack) &&
                            DamageType != Status.DamageType.Heal &&
                            (Targeted.StatusEffect.Check(205) && Targeted.StatusEffect.GetSubRank(205) > LibInteger.GetRandBasis()) &&
                            ActionArtsRow.SkillType == Status.SkillType.Normal)
                        {
                            List<LibActionType> CounterActionList = new List<LibActionType>();
                            List<LibUnitBase> CounterTargetList = new List<LibUnitBase>();

                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の逆襲のサンバによるカウンター！</dd>");
                            MessageBuilder.AppendLine(LibSerif.Serif(Targeted, LibSituation.GetNo("カウンターが発動"), null, AreaName, Mine));

                            CounterActionList = BattleActionSelect.Select(Targeted, Status.ActionType.MainAttack, true, 0, false, 0);
                            CounterTargetList.Add(Mine);

                            BattleAttack(Targeted, CounterActionList, CounterTargetList, MessageBuilder, AreaName, Turn);
                        }
                    }

                    // 反撃準備
                    {
                        if ((ActionArtsRow.ActionBase == Status.ActionBaseType.MainAttack ||
                            ActionArtsRow.ActionBase == Status.ActionBaseType.SubAttack) &&
                            DamageType != Status.DamageType.Heal &&
                            (Targeted.StatusEffect.Check(76) && Targeted.StatusEffect.GetSubRank(76) > LibInteger.GetRandBasis()) &&
                            ActionArtsRow.SkillType == Status.SkillType.Normal)
                        {
                            List<LibActionType> CounterActionList = new List<LibActionType>();
                            List<LibUnitBase> CounterTargetList = new List<LibUnitBase>();

                            int SkillNo = (int)Targeted.StatusEffect.GetRank(76);

                            CounterActionList = BattleActionSelect.Select(Targeted, Status.ActionType.SelectionArtsAttack, true, SkillNo, false, 0);
                            CounterTargetList.Add(Mine);

                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "の" + CounterActionList[0].ArtsName + "！</dd>");

                            BattleAttack(Targeted, CounterActionList, CounterTargetList, MessageBuilder, AreaName, Turn);
                        }
                    }

                    // 被ダメージ返し
                    {
                        EffectListEntity.effect_listRow EffectRow = Targeted.EffectList.FindByeffect_id(791);
                        if (DamageType != Status.DamageType.Heal &&
                            EffectRow != null &&
                            Damage > 0)
                        {
                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Targeted.NickName) + "のリベンジスパイク！</dd>");

                            int RevengeSpikeDamage = (int)((decimal)Damage * 0.05m);

                            MessageBuilder.AppendLine("<dd>" + LibResultText.CSSEscapeChara(Mine.NickName) + "に" + RevengeSpikeDamage + "のダメージ。</dd>");
                            Mine.HPNow -= RevengeSpikeDamage;

                            if (Mine.HPNow == 0 && !Mine.BattleOut)
                            {
                                switch (Mine.PartyBelong)
                                {
                                    case Status.Belong.Friend:
                                        if (Targeted.EffectList.FindByeffect_id(2105) != null)
                                        {
                                            MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "ははじけ飛んだ…。</dd>");
                                        }
                                        else
                                        {
                                            MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "は倒された…。</dd>");
                                        }
                                        break;
                                    case Status.Belong.Enemy:
                                        if (Mine.EffectList.FindByeffect_id(2105) != null)
                                        {
                                            MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "がはじけ飛んだ！</dd>");
                                        }
                                        else
                                        {
                                            MessageBuilder.AppendLine("<dd class=\"act_dead\">" + LibResultText.CSSEscapeChara(Mine.NickName) + "を倒した！</dd>");
                                        }
                                        break;
                                }

                                MessageBuilder.AppendLine(LibSerif.Serif(Mine, LibSituation.GetNo("戦闘不能時"), null, AreaName, Mine));

                                foreach (LibUnitBase FriendShip in FriendsLive)
                                {
                                    if (FriendShip.BattleID != Mine.BattleID)
                                    {
                                        StringBuilder addMessage = new StringBuilder();
                                        switch (Mine.MemberNumber)
                                        {
                                            case 0:
                                                addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方A<chara_name>が倒れた時"), null, AreaName, Mine));
                                                break;
                                            case 1:
                                                addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方B<chara_name>が倒れた時"), null, AreaName, Mine));
                                                break;
                                            case 2:
                                                addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方C<chara_name>が倒れた時"), null, AreaName, Mine));
                                                break;
                                            case 3:
                                                addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方D<chara_name>が倒れた時"), null, AreaName, Mine));
                                                break;
                                            case 4:
                                                addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方E<chara_name>が倒れた時"), null, AreaName, Mine));
                                                break;
                                            case 5:
                                                addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方F<chara_name>が倒れた時"), null, AreaName, Mine));
                                                break;
                                        }

                                        if (addMessage.Length == 0)
                                        {
                                            addMessage.Append(LibSerif.Serif(FriendShip, LibSituation.GetNo("味方が倒れた時"), null, AreaName, Mine));
                                        }

                                        MessageBuilder.AppendLine(addMessage.ToString());
                                    }
                                }

                                Targeted.DestroyCount++;
                                BattleCommon.DeadMans(Mine, BattleCharacer);
                            }
                        }
                    }
                    #endregion

                    #region 兵糧のピーオン
                    if (ActionArtsRow.AttackType == Status.AttackType.Song && Targeted.StatusEffect.Check(202))
                    {
                        decimal Rank = Targeted.StatusEffect.GetRank(202);
                        int Rejeneration = LibInteger.GetRandMax((int)Math.Max(1m, (decimal)Targeted.Level * 0.5m * (Rank - 1m)), (int)((decimal)Targeted.Level * (1m + 0.5m * (Rank - 1m))));

                        Targeted.HPNow += Rejeneration;

                        MessageBuilder.AppendLine("<dd>兵糧のピーオン効果→" + LibResultText.CSSEscapeChara(Targeted.NickName) + "のＨＰが" + Rejeneration + "回復。</dd>");
                    }
                    #endregion

                    #region 魔法剣効果
                    if (IsHit && Mine.StatusEffect.Check(77) && ActionArtsRow.IsNormalAttack && ActionArtsRow.ActionBase == Status.ActionBaseType.MainAttack)
                    {
                        List<LibActionType> CounterActionList = new List<LibActionType>();
                        List<LibUnitBase> CounterTargetList = new List<LibUnitBase>();

                        CounterActionList = BattleActionSelect.Select(Mine, Status.ActionType.SelectionArtsAttack, false, (int)Mine.StatusEffect.GetRank(77), true, 0);

                        CounterTargetList.Add(Targeted);

                        BattleAttack(Mine, CounterActionList, CounterTargetList, MessageBuilder, AreaName, Turn);
                    }
                    #endregion

                    // ミスだったので、次の相手へ
                    if (!IsHit)
                    {
                        continue;
                    }
                }
                #endregion

                #region アイテムの消費
                {
                    EffectListEntity.effect_listRow EffectRow = ActionArtsRow.EffectList.FindByeffect_id(920);
                    if (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis() && Mine.GetType() == typeof(LibPlayer))
                    {
                        // 消費実行
                        int Rank = (int)EffectRow.rank;

                        // アイテム消費
                        bool IsItemUsing = true;

                        {
                            EffectListEntity.effect_listRow EffectItemArtsRow = Mine.EffectList.FindByeffect_id(885);
                            if (EffectItemArtsRow != null && EffectItemArtsRow.prob > LibInteger.GetRandBasis() && ActionArtsRow.AttackType == Status.AttackType.Item)
                            {
                                IsItemUsing = false;
                            }
                        }

                        if (Mine.StatusEffect.Check(96))
                        {
                            IsItemUsing = false;
                        }

                        // アイテムの消費
                        if (IsItemUsing)
                        {
                            ((LibPlayer)Mine).HaveItem.DefaultView.RowFilter = "box_type=" + Status.ItemBox.Normal + " and it_num=" + Rank + " and created=false";

                            if (((LibPlayer)Mine).HaveItem.DefaultView.Count > 0)
                            {
                                ((LibPlayer)Mine).RemoveItem(Status.ItemBox.Normal, (int)((LibPlayer)Mine).HaveItem.DefaultView[0]["have_no"], 1);
                            }
                        }
                    }
                }
                #endregion

                // フレイムプレス使用回数カウント
                if (Mine.GetType() == typeof(LibMonster) && ActionArtsRow.SkillID == 5023)
                {
                    ((LibMonster)Mine).FlamePressUseCount += 1;
                }

                // 生命の賛歌チェック
                if (Mine.GetType() == typeof(LibMonster))
                {
                    if (ActionArtsRow.SkillID == 5194)
                    {
                        ((LibMonster)Mine).IsUseLifeSong = true;
                    }
                    else
                    {
                        ((LibMonster)Mine).IsUseLifeSong = false;
                    }
                }
            }

            // 攻撃者のTPアップ
            Mine.TPNow += LibInteger.GetRandMax(3, 7);

            // タクティカルプラス
            {
                EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(2129);
                if (EffectRow != null)
                {
                    Mine.TPNow += (int)EffectRow.rank;
                }
            }

            // ファーストタッチのリセット
            Mine.IsFirstAttachAttack = false;

            Mine.UsedArtsName = "";
        }
    }
}
