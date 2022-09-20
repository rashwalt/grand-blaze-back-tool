using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using System.IO;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// 装備の解除
        /// </summary>
        private void RemoveEquipItem()
        {
            int ItemID = 0;
            bool IsCreatedItem = false;

            int status;

            bool SubUnEquip = false;

            int[] EquipSpots = { Status.EquipSpot.Main, Status.EquipSpot.Sub, Status.EquipSpot.Head, Status.EquipSpot.Body, Status.EquipSpot.Accesory };

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_battle_preparationRow ContinueEquipItemRow = con.Entity.ts_continue_battle_preparation.FindByentry_no(EntryNo);

                if (ContinueEquipItemRow == null)
                {
                    // 登録がない場合はスキップ
                    continue;
                }

                int[] EquipData = { ContinueEquipItemRow.equip_main, ContinueEquipItemRow.equip_sub, ContinueEquipItemRow.equip_head, ContinueEquipItemRow.equip_body, ContinueEquipItemRow.equip_acce1 };

                for (int i = 0; i < EquipSpots.Length; i++)
                {
                    if (EquipData[i] >= 0)
                    {
                        continue;
                    }

                    ItemID = 0;
                    IsCreatedItem = false;

                    status = Mine.EquipRemove(EquipSpots[i], ref ItemID, ref IsCreatedItem);
                    if (status == Status.Equip.OK)
                    {
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.RemoveEquip, LibConst.GetEquipSpotName(EquipSpots[i]) + "に装備していた" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "を外した。", Status.MessageLevel.Normal);
                    }

                    SubUnEquip = false;
                    if (EquipSpots[i] == Status.EquipSpot.Main)
                    {
                        CommonItemEntity.item_listRow MainRow = LibItem.GetItemRow(ItemID, IsCreatedItem);
                        if (MainRow != null)
                        {
                            bool IsSubCreatedItem = false;
                            int SubItemNo = 0;
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
                                status = Mine.EquipRemove(Status.EquipSpot.Sub, ref ItemID, ref IsCreatedItem);
                                if (status == Status.Equip.OK)
                                {
                                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.RemoveEquip, "サブに装備していた" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "を外した。", Status.MessageLevel.Normal);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
