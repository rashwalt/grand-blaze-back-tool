using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    class BattleActionSelect
    {
        /// <summary>
        /// 行動内容の設定
        /// </summary>
        /// <param name="Mine">行動実行者</param>
        /// <param name="ActionType">デフォルト行動種類</param>
        /// <param name="IsCounter">カウンター攻撃？</param>
        /// <param name="SelectionArtsID">指定アーツ番号</param>
        /// <param name="AddAttack">追加効果か？</param>
        /// <param name="SelectedActionSkillNo">選択されたスキルID</param>
        /// <param name="SelectedActionSkillCreated">選択されたスキル種別</param>
        /// <returns>行動内容のDataRowList</returns>
        public static List<LibActionType> Select(LibUnitBase Mine, int ActionType, bool IsCounter, int SelectionArtsID, bool AddAttack, int SelectedActionSkillNo)
        {
            List<LibActionType> ActionSelectRows = new List<LibActionType>();

            // デフォルトの内容を設定
            SelectDetail(Mine, ActionType, SelectionArtsID, ActionSelectRows, AddAttack, IsCounter, SelectedActionSkillNo);

            if (ActionSelectRows.Count == 0)
            {
                // なにもしない
                return ActionSelectRows;
            }

            return ActionSelectRows;
        }

        /// <summary>
        /// 行動内容の設定（詳細）
        /// </summary>
        /// <param name="Mine">行動実行者</param>
        /// <param name="ActionTypeCode">デフォルト行動種類</param>
        /// <param name="SelectionArtsID">指定アーツ番号</param>
        /// <param name="ActionSelectRows">行動内容のDataRowList</param>
        /// <param name="AddAttack">追加効果か？</param>
        /// <param name="Counters">カウンターか</param>
        /// <param name="SelectedActionSkillNo">選択されたスキルID</param>
        /// <param name="SelectedActionSkillCreated">選択されたスキル種別</param>
        private static void SelectDetail(LibUnitBase Mine, int ActionTypeCode, int SelectionArtsID, List<LibActionType> ActionSelectRows, bool AddAttack, bool Counters, int SelectedActionSkillNo)
        {
            // 基本攻撃などもスキルとして登録管理する
            int i = 0;
            int max_i = 0;

            // 暴走の場合、行動を変換
            if (Mine.StatusEffect.Check(14))
            {
                ActionTypeCode = Status.ActionType.MainAttack;
            }
            CommonSkillEntity.skill_listDataTable dummyTable = new CommonSkillEntity.skill_listDataTable();

            switch (ActionTypeCode)
            {
                case Status.ActionType.NoAction:
                    // 行動しない
                    {
                        CommonSkillEntity.skill_listRow ActionRow = dummyTable.Newskill_listRow();
                        ActionRow.ItemArray = LibSkill.GetSkillRow(5).ItemArray;

                        EffectListEntity.effect_listDataTable EffectTable = new EffectListEntity.effect_listDataTable();

                        LibActionType ActionType = new LibActionType(ActionRow, 0, EffectTable, true, ActionTypeCode);

                        ActionSelectRows.Add(ActionType);
                    }
                    return;
                case Status.ActionType.MainAttack:
                    // 通常攻撃
                    max_i = 1;
                    if (Mine.ATKSub > 0)
                    {
                        max_i++;
                    }

                    for (i = 0; i < max_i; i++)
                    {
                        CommonSkillEntity.skill_listRow ActionRow = dummyTable.Newskill_listRow();
                        ActionRow.ItemArray = LibSkill.GetSkillRow(5).ItemArray;

                        int ActionBaseType = 0;
                        int MainWeapType = 0;
                        int ItemType = 0;
                        int SubType = 0;

                        if (i == 0)
                        {
                            ActionBaseType = Status.ActionBaseType.MainAttack;
                            ActionRow.sk_fire = Mine.MainWeapon.Elemental.Fire;
                            ActionRow.sk_freeze = Mine.MainWeapon.Elemental.Freeze;
                            ActionRow.sk_air = Mine.MainWeapon.Elemental.Air;
                            ActionRow.sk_earth = Mine.MainWeapon.Elemental.Earth;
                            ActionRow.sk_water = Mine.MainWeapon.Elemental.Water;
                            ActionRow.sk_thunder = Mine.MainWeapon.Elemental.Thunder;
                            ActionRow.sk_holy = Mine.MainWeapon.Elemental.Holy;
                            ActionRow.sk_dark = Mine.MainWeapon.Elemental.Dark;
                            ActionRow.sk_slash = Mine.MainWeapon.Elemental.Slash;
                            ActionRow.sk_pierce = Mine.MainWeapon.Elemental.Pierce;
                            ActionRow.sk_strike = Mine.MainWeapon.Elemental.Strike;
                            ActionRow.sk_break = Mine.MainWeapon.Elemental.Break;

                            ActionRow.sk_range = Mine.MainWeapon.Range;
                            ActionRow.sk_target_area = Mine.MainWeapon.TargetArea;

                            MainWeapType = Mine.MainWeapon.ItemType;

                            ActionRow.sk_antiair = LibItemType.GetAntiAir(Mine.MainWeapon.ItemType);

                            ItemType = Mine.MainWeapon.ItemType;
                            SubType = Mine.MainWeapon.ItemSubType;
                        }
                        else
                        {
                            ActionBaseType = Status.ActionBaseType.SubAttack;
                            ActionRow.sk_fire = Mine.SubWeapon.Elemental.Fire;
                            ActionRow.sk_freeze = Mine.SubWeapon.Elemental.Freeze;
                            ActionRow.sk_air = Mine.SubWeapon.Elemental.Air;
                            ActionRow.sk_earth = Mine.SubWeapon.Elemental.Earth;
                            ActionRow.sk_water = Mine.SubWeapon.Elemental.Water;
                            ActionRow.sk_thunder = Mine.SubWeapon.Elemental.Thunder;
                            ActionRow.sk_holy = Mine.SubWeapon.Elemental.Holy;
                            ActionRow.sk_dark = Mine.SubWeapon.Elemental.Dark;
                            ActionRow.sk_slash = Mine.SubWeapon.Elemental.Slash;
                            ActionRow.sk_pierce = Mine.SubWeapon.Elemental.Pierce;
                            ActionRow.sk_strike = Mine.SubWeapon.Elemental.Strike;
                            ActionRow.sk_break = Mine.SubWeapon.Elemental.Break;

                            ActionRow.sk_range = Mine.SubWeapon.Range;
                            ActionRow.sk_target_area = Mine.SubWeapon.TargetArea;

                            ActionRow.sk_antiair = LibItemType.GetAntiAir(Mine.SubWeapon.ItemType);

                            ItemType = Mine.SubWeapon.ItemType;
                            SubType = Mine.SubWeapon.ItemSubType;
                        }

                        // 射程アップ
                        {
                            EffectListEntity.effect_listRow RangeUpRow = Mine.EffectList.FindByeffect_id(880);
                            if (RangeUpRow != null && ActionRow.sk_range < (int)RangeUpRow.rank)
                            {
                                ActionRow.sk_range = (int)RangeUpRow.rank;
                            }
                        }

                        EffectListEntity.effect_listDataTable EffectTable = new EffectListEntity.effect_listDataTable();
                        LibEffect.Split(ActionRow.sk_effect, ref EffectTable);

                        ActionRow.sk_type = Status.SkillType.Normal;

                        if (Counters)
                        {
                            ActionRow.sk_type = Status.SkillType.Counter;
                        }

                        ActionRow.sk_round = 1;

                        if (Mine.MainWeapon.Effect.Count > 0)
                        {
                            LibEffect.AddEffectTable(ref EffectTable, Mine.MainWeapon.Effect, 1m, 1m);
                        }

                        LibActionType ActionType = new LibActionType(ActionRow, ActionBaseType, EffectTable, true, ActionTypeCode);
                        ActionType.AddAttack = AddAttack;
                        ActionType.ItemType = ItemType;
                        ActionType.ItemSubType = SubType;

                        ActionSelectRows.Add(ActionType);
                    }

                    break;
                case Status.ActionType.UseItem:
                    // アイテム使用
                    {
                        int UseItemNo = SelectedActionSkillNo;

                        CommonSkillEntity.skill_listRow ActionRow = dummyTable.Newskill_listRow();
                        ActionRow.ItemArray = LibSkill.GetSkillRow(6).ItemArray;

                        int ActionBaseType = 0;
                        int MainWeapType = 0;

                        CommonItemEntity.item_listRow itemRow = LibItem.GetItemRow(UseItemNo, false);

                        ActionBaseType = Status.ActionBaseType.ItemArts;

                        ActionRow.sk_target_area = itemRow.it_target_area;

                        MainWeapType = Mine.MainWeapon.ItemType;

                        EffectListEntity.effect_listDataTable EffectTable = new EffectListEntity.effect_listDataTable();
                        LibEffect.Split(ActionRow.sk_effect, ref EffectTable);

                        // アイテム消費を入れ込む
                        LibEffect.Add(920, UseItemNo, 0, 100, 0, 0, true, ref EffectTable);

                        ActionRow.sk_type = Status.SkillType.Normal;

                        LibActionType ActionType = new LibActionType(ActionRow, ActionBaseType, EffectTable, true, ActionTypeCode);
                        ActionType.AddAttack = AddAttack;

                        ActionSelectRows.Add(ActionType);
                    }
                    break;
                default:
                    // アーツその他
                    {
                        int SkillNo = 0;
                        if (ActionTypeCode == Status.ActionType.SpecialArtsAttack)
                        {
                            SkillNo = SelectedActionSkillNo;
                        }
                        else if (ActionTypeCode == Status.ActionType.SelectionArtsAttack)
                        {
                            SkillNo = SelectionArtsID;
                        }
                        else
                        {
                            SkillNo = SelectedActionSkillNo;
                        }

                        CommonSkillEntity.skill_listRow ActionRow = dummyTable.Newskill_listRow();
                        ActionRow.ItemArray = LibSkill.GetSkillRow(SkillNo).ItemArray;

                        EffectListEntity.effect_listDataTable EffectTable = new EffectListEntity.effect_listDataTable();
                        LibEffect.Split(ActionRow.sk_effect, ref EffectTable);

                        // ギャンブルキャスト
                        {
                            EffectListEntity.effect_listRow EffectRow = EffectTable.FindByeffect_id(1064);
                            if (EffectRow != null)
                            {
                                int SkillMin = 0;
                                int SkillMax = 0;
                                switch ((int)EffectRow.rank)
                                {
                                    case 1:
                                        SkillMin = 4000;
                                        SkillMax = 4005;
                                        break;
                                    case 2:
                                        SkillMin = 4100;
                                        SkillMax = 4404;
                                        break;
                                    case 3:
                                        SkillMin = 4414;
                                        SkillMax = 4419;
                                        break;
                                    case 4:
                                        SkillMin = 4106;
                                        SkillMax = 4111;
                                        break;
                                }

                                int DreamPoint = LibInteger.GetRandMax(SkillMin, SkillMax);

                                ActionRow = LibSkill.GetSkillRow(DreamPoint);

                                EffectTable = new EffectListEntity.effect_listDataTable();
                                LibEffect.Split(ActionRow.sk_effect, ref EffectTable);
                            }
                        }

                        // 射程アップ
                        {
                            EffectListEntity.effect_listRow RangeUpRow = Mine.EffectList.FindByeffect_id(880);
                            if (RangeUpRow != null && ActionRow.sk_range < (int)RangeUpRow.rank)
                            {
                                ActionRow.sk_range = (int)RangeUpRow.rank;
                            }
                        }

                        if (EffectTable.FindByeffect_id(920) != null)
                        {
                            EffectListEntity.effect_listRow RangeUp2Row = Mine.EffectList.FindByeffect_id(2103);
                            if (RangeUp2Row != null && ActionRow.sk_range < Status.Range.Long)
                            {
                                ActionRow.sk_range++;
                            }
                        }

                        int ActionBaseType = 0;

                        switch (ActionRow.sk_atype)
                        {
                            case Status.AttackType.Combat:
                            case Status.AttackType.Shoot:
                                ActionBaseType = Status.ActionBaseType.MainAttack;
                                break;
                            case Status.AttackType.Mystic:
                                if (ActionRow.sk_arts_category == LibSkillType.FindByName("神聖魔法"))
                                {
                                    ActionBaseType = Status.ActionBaseType.MindAttack;
                                }
                                else
                                {
                                    ActionBaseType = Status.ActionBaseType.SorscialAttack;
                                }
                                break;
                            case Status.AttackType.Summon:
                                ActionBaseType = Status.ActionBaseType.SorscialAttack;
                                break;
                            case Status.AttackType.Song:
                                ActionBaseType = Status.ActionBaseType.MindAttack;
                                break;
                            case Status.AttackType.Dance:
                                ActionBaseType = Status.ActionBaseType.MainAttack;
                                break;
                            case Status.AttackType.Ninjutsu:
                                ActionBaseType = Status.ActionBaseType.SorscialAttack;
                                break;
                            case Status.AttackType.Item:
                                ActionBaseType = Status.ActionBaseType.ItemArts;
                                break;
                            case Status.AttackType.MagicSword:
                                ActionBaseType = Status.ActionBaseType.MagicSword;
                                break;
                            case Status.AttackType.Bless:
                                ActionBaseType = Status.ActionBaseType.BlessAttack;
                                break;
                        }

                        if (Counters)
                        {
                            ActionRow.sk_type = Status.SkillType.Counter;
                        }

                        LibActionType ActionType = new LibActionType(ActionRow, ActionBaseType, EffectTable, false, ActionTypeCode);
                        ActionType.AddAttack = AddAttack;
                        ActionType.ItemType = Mine.MainWeapon.ItemType;
                        ActionType.ItemSubType = Mine.MainWeapon.ItemSubType;

                        bool PersonalActionOK = true;

                        // 現在のクラス、武器で使用可能なアーツかどうか
                        if (Mine.GetType() == typeof(LibPlayer))
                        {
                            if (((LibPlayer)Mine).UsingSkillList.FindBysk_id(SkillNo) == null)
                            {
                                PersonalActionOK = false;
                            }
                        }

                        if (PersonalActionOK)
                        {
                            ActionSelectRows.Add(ActionType);
                        }
                    }
                    break;
            }
        }
    }
}
