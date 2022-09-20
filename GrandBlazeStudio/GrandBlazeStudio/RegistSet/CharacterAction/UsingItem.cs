using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using System.IO;
using CommonLibrary.DataFormat.Format;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.SpecialEntity;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// アイテムの使用
        /// </summary>
        private void UsingItem()
        {
            int MarkID;

            //LibLuaExec sct = new LibLuaExec(CharaList);
            LibScript script = new LibScript(CharaList);

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_mainRow ContinueMainRow = con.Entity.ts_continue_main.FindByentry_no(EntryNo);

                if (ContinueMainRow == null || (ContinueMainRow.use_item_1 == 0 && ContinueMainRow.use_item_2 == 0 && ContinueMainRow.use_item_3 == 0))
                {
                    // ない場合はスキップ
                    continue;
                }

                MarkID = ContinueMainRow.mark_id;

                int[] UsingItemNoList = { ContinueMainRow.use_item_1, ContinueMainRow.use_item_2, ContinueMainRow.use_item_3 };
                string[] UsingItemMessageList = { ContinueMainRow.use_item_1_message, ContinueMainRow.use_item_2_message, ContinueMainRow.use_item_3_message };
                int LastUsingHaveItemID = 0;

                for (int ItemNoCount = 0; ItemNoCount < UsingItemNoList.Length; ItemNoCount++)
                {
                    bool UseItem = true;// 消費するか？
                    int ItemNo = UsingItemNoList[ItemNoCount];
                    string Message = UsingItemMessageList[ItemNoCount];

                    if (ItemNo <= 0)
                    {
                        continue;
                    }

                    CommonUnitDataEntity.have_item_listRow HaveItemRow = Mine.GetHaveItemItemRow(Status.ItemBox.Normal, ItemNo);

                    CommonItemEntity.item_listRow ItemUsingRow = Mine.GetHaveItemItemNum(Status.ItemBox.Normal, ItemNo);

                    if (ItemUsingRow == null)
                    {
                        // もっていない番号の場合スキップ
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, "無効なアイテムが指定されました。", Status.MessageLevel.Error);
                        continue;
                    }

                    string ItemName = ItemUsingRow.it_name;

                    if (ItemUsingRow.it_use_item == 0)
                    {
                        // 使用不可能な場合スキップ
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, LibResultText.CSSEscapeItem(ItemName) + "は使うことができません。", Status.MessageLevel.Error);
                        continue;
                    }

                    if (ItemUsingRow.it_use_item == -1)
                    {
                        // 消費しない
                        UseItem = false;
                    }

                    if (ItemUsingRow.it_equip_level > 0 && ItemUsingRow.it_equip_level > Mine.Level)
                    {
                        // 使用不可能な場合スキップ
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, LibResultText.CSSEscapeItem(ItemName) + "は現在のレベルでは使うことができません。", Status.MessageLevel.Error);
                        continue;
                    }

                    string[] Install = ItemUsingRow.it_equip_install.Split(',');
                    bool IsInstallOk = false;
                    for (int v = 0; v < Install.Length; v++)
                    {
                        if (int.Parse(Install[v]) == Mine.IntallClassID)
                        {
                            IsInstallOk = true;
                        }
                    }
                    if (!IsInstallOk)
                    {
                        // 使用不可能な場合スキップ
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, LibResultText.CSSEscapeItem(ItemName) + "は現在のインストールクラスでは使うことができません。", Status.MessageLevel.Error);
                        continue;
                    }

                    EffectListEntity.effect_listDataTable ItemUsingEffectTable = new EffectListEntity.effect_listDataTable();
                    LibEffect.Split(ItemUsingRow.it_effect, ref ItemUsingEffectTable);

                    // 装備限定
                    {
                        if (ItemUsingEffectTable.FindByeffect_id(1505) != null && HaveItemRow.equip_spot == 0)
                        {
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, LibResultText.CSSEscapeItem(ItemName) + "を装備していないため使うことができません。", Status.MessageLevel.Error);
                            continue;
                        }
                    }

                    // 無限アイテムの場合、同じ所持番号は連続で使えないよ？
                    if (LastUsingHaveItemID > 0 && LastUsingHaveItemID == ItemNo)
                    {
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, LibResultText.CSSEscapeItem(ItemName) + "は連続で使えないようだ…。", Status.MessageLevel.Caution);
                        continue;
                    }

                    if (Message.Length > 0)
                    {
                        LibMessage.SenderMessage(Mine, Mine, Message, Status.PlayerSysMemoType.UsingItem);
                    }

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, LibResultText.CSSEscapeItem(ItemName) + "を使った。", Status.MessageLevel.Normal);

                    foreach (EffectListEntity.effect_listRow EffectRow in ItemUsingEffectTable.Rows)
                    {
                        int EffectID = EffectRow.effect_id;
                        decimal Rank = EffectRow.rank;
                        decimal SubRank = EffectRow.sub_rank;
                        decimal Prob = EffectRow.prob;
                        int EndLimit = EffectRow.endlimit;

                        if (LibInteger.GetRandBasis() > Prob)
                        {
                            // 先に確率で判定。失敗の場合スキップ
                            continue;
                        }

                        switch (EffectID)
                        {
                            case 840:
                                // HP回復
                                int RepairHP = (int)Rank;
                                if (RepairHP > 0)
                                {
                                    Mine.HPNow += RepairHP;
                                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, "→HPが" + RepairHP + "回復した。", Status.MessageLevel.Normal);
                                }
                                break;
                            case 841:
                                // MP回復
                                int RepairMP = (int)Rank;
                                if (RepairMP > 0)
                                {
                                    Mine.MPNow += RepairMP;
                                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, "→MPが" + RepairMP + "回復した。", Status.MessageLevel.Normal);
                                }
                                break;
                            case 843:
                                // HP回復(割合)
                                int RepairHPRate = (int)((decimal)Mine.HPMax * Rank / 100m);
                                if (RepairHPRate > 0)
                                {
                                    Mine.HPNow += RepairHPRate;
                                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, "→HPが" + RepairHPRate + "回復した。", Status.MessageLevel.Normal);
                                }
                                break;
                            case 844:
                                // MP回復(割合)
                                int RepairMPRate = (int)((decimal)Mine.MPMax * Rank / 100m);
                                if (RepairMPRate > 0)
                                {
                                    Mine.MPNow += RepairMPRate;
                                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, "→MPが" + RepairMPRate + "回復した。", Status.MessageLevel.Normal);
                                }
                                break;
                            case 953:
                                // ペット呼び出し
                                {
                                    // ステータス追加
                                    Mine.StatusEffect.Add(EffectID, (int)Rank, (int)SubRank, EndLimit, Mine.Level, false);
                                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, "→ペットを呼び出しています…。", Status.MessageLevel.Normal);
                                }
                                break;
                            case 954:
                                // コンパニオン呼び出し
                                {
                                    // ステータス追加
                                    Mine.StatusEffect.Add(EffectID, (int)Rank, (int)SubRank, EndLimit, Mine.Level, false);
                                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, "→コンパニオンを呼び出しています…。", Status.MessageLevel.Normal);
                                }
                                break;
                            case 1500:
                                // スキル習得
                                {
                                    bool IsScroll = false;
                                    if (SubRank == 2)
                                    {
                                        IsScroll = true;
                                    }
                                    if (Mine.AddSkill((int)Rank, IsScroll))
                                    {
                                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, "→" + LibResultText.CSSEscapeSkill(LibSkill.GetSkillName((int)Rank)) + "を覚えた。", Status.MessageLevel.Normal);
                                    }
                                }
                                break;
                            case 1501:
                                // 貴重品入手
                                if (Mine.AddKeyItem((int)Rank))
                                {
                                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, "→" + LibResultText.CSSEscapeKeyItem(LibKeyItem.GetKeyItemName((int)Rank)) + "を手に入れた。", Status.MessageLevel.Normal);
                                }
                                break;
                            case 1502:
                            case 1505:
                                // アイテム入手
                                {
                                    bool IsCreated = false;

                                    // 個数
                                    int GetCount = (int)SubRank;

                                    string TextMsg = "";
                                    Mine.AddItemWithMessage((int)Rank, IsCreated, GetCount, ref TextMsg);
                                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, TextMsg, Status.MessageLevel.Normal);
                                }
                                break;
                            case 1510:
                                // SPアップ
                                {
                                    if (LibConst.LevelLimit > Mine.Level)
                                    {
                                        Mine.Exp += Mine.MaxExpNext;
                                        Mine.LevelPlus();
                                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, "→" + Mine.NickName + "のスキルポイントが" + Mine.LevelNormal + "になった！", Status.MessageLevel.Normal);
                                    }
                                }
                                break;
                            default:
                                if ((EffectID > 0 && EffectID < 300) || (EffectID > 500 && EffectID < 700))
                                {
                                    // ステータス追加
                                    Mine.StatusEffect.Add(EffectID, (int)Rank, (int)SubRank, EndLimit, Mine.Level, true);
                                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, "→" + LibStatusList.GetName(EffectID) + "の効果を得た。", Status.MessageLevel.Normal);
                                }
                                break;
                        }
                    }

                    if (UseItem)
                    {
                        // 使用アイテムの消失
                        Mine.RemoveItem(Status.ItemBox.Normal, ItemNo, 1);
                    }
                    else
                    {
                        LastUsingHaveItemID = ItemNo;
                    }

                    // 使用イベントの発生
                    string Event = "";
                    //sct.Init();
                    //sct.UseEventExec(Mine.EntryNo, UseItemID, ref Event, MarkID);
                    Event = script.UseExec(Mine.EntryNo, MarkID, ItemUsingRow.use_script);

                    if (Event.Length > 0)
                    {
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.UsingItem, Event, Status.MessageLevel.Normal);
                    }
                }
            }
        }
    }
}
