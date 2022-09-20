using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.SpecialEntity;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// バザーアイテムをロック
        /// </summary>
        private void BazzerItemLock()
        {
            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_sell_bazzerRow[] BazzerSellRows = (ContinueDataEntity.ts_continue_sell_bazzerRow[])con.Entity.ts_continue_sell_bazzer.Select("entry_no=" + EntryNo);

                // アイテムを追加

                foreach (ContinueDataEntity.ts_continue_sell_bazzerRow BazzerRockRow in BazzerSellRows)
                {
                    int ItemNo = 0;
                    bool IsCreated = false;
                    bool SubUnEquip = false;
                    int ItemCount = BazzerRockRow.count;

                    CommonUnitDataEntity.have_item_listRow ItemRow = Mine.GetHaveItemItemRow(Status.ItemBox.Normal, BazzerRockRow.have_no);

                    if (ItemRow == null)
                    {
                        continue;
                    }

                    ItemNo = ItemRow.it_num;
                    IsCreated = ItemRow.created;


                    CommonItemEntity.item_listRow ItemDataRow = LibItem.GetItemRow(ItemRow.it_num, ItemRow.created);

                    // バインド？
                    if (ItemDataRow.it_bind)
                    {
                        continue;
                    }

                    // 装備解除
                    {
                        int EqItemID = 0;
                        bool EqCreated = false;

                        if (ItemRow.equip_spot == Status.EquipSpot.Main)
                        {
                            Mine.EquipRemove(Status.EquipSpot.Main, ref EqItemID, ref EqCreated);

                            CommonItemEntity.item_listRow MainRow = LibItem.GetItemRow(EqItemID, EqCreated);
                            if (MainRow != null)
                            {
                                int SubItemNo = 0;
                                bool IsSubCreatedItem = false;
                                CommonItemEntity.item_listRow SubRow = Mine.GetHaveItemEquiped(Status.EquipSpot.Sub);
                                if (SubRow != null)
                                {
                                    SubItemNo = SubRow.it_num;
                                    IsSubCreatedItem = SubRow.it_creatable;
                                    if (!MainRow.it_both_hand)
                                    {
                                        if (SubRow.it_equip_parts == Status.EquipSpot.Main)
                                        {
                                            SubUnEquip = true;
                                        }
                                    }
                                }

                                if (SubUnEquip)
                                {
                                    Mine.EquipRemove(Status.EquipSpot.Sub, ref SubItemNo, ref IsSubCreatedItem);
                                }
                            }
                        }
                        if (ItemRow.equip_spot == Status.EquipSpot.Sub)
                        {
                            Mine.EquipRemove(Status.EquipSpot.Sub, ref EqItemID, ref EqCreated);
                        }
                        if (ItemRow.equip_spot == Status.EquipSpot.Head)
                        {
                            Mine.EquipRemove(Status.EquipSpot.Head, ref EqItemID, ref EqCreated);
                        }
                        if (ItemRow.equip_spot == Status.EquipSpot.Body)
                        {
                            Mine.EquipRemove(Status.EquipSpot.Body, ref EqItemID, ref EqCreated);
                        }
                        if (ItemRow.equip_spot >= Status.EquipSpot.Accesory)
                        {
                            Mine.EquipRemove(ItemRow.equip_spot, ref EqItemID, ref EqCreated);
                        }
                    }

                    // 手数料の徴収
                    int BasePrice = Math.Max((int)ItemRow.it_seller, BazzerRockRow.price);
                    
                    int Tax = 0;

                    // スキルによる割引
                    if (Mine.EffectList.FindByeffect_id(2108) != null)
                    {
                        if (BasePrice < 1000)
                        {
                            Tax = 0;
                        }
                        else if (BasePrice < 10000)
                        {
                            Tax = (int)((decimal)BazzerRockRow.price * 0.01m) + 1;
                        }
                        else if (BasePrice < 50000)
                        {
                            Tax = (int)((decimal)BazzerRockRow.price * 0.01m) + 50;
                        }
                        else
                        {
                            Tax = (int)((decimal)BazzerRockRow.price * 0.01m) + 100;
                        }
                    }
                    else
                    {
                        if (BasePrice < 200)
                        {
                            Tax = 0;
                        }
                        else if (BasePrice < 1000)
                        {
                            Tax = (int)((decimal)BazzerRockRow.price * 0.01m) + 1;
                        }
                        else if (BasePrice < 10000)
                        {
                            Tax = (int)((decimal)BazzerRockRow.price * 0.01m) + 50;
                        }
                        else
                        {
                            Tax = (int)((decimal)BazzerRockRow.price * 0.01m) + 100;
                        }
                    }

                    Mine.HaveMoney -= Tax;

                    if (Tax > 0)
                    {
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.SellingBazzerItemLock, "バザー出品手数料として" + LibResultText.CSSEscapeMoney(Tax, false) + "が手持ちから引き下ろされました。", Status.MessageLevel.Normal);
                    }

                    ItemRow.it_box_count -= ItemCount;
                    ItemRow.it_box_baz_count += ItemCount;
                }
            }
        }
    }
}
