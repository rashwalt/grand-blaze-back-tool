using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using System.IO;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// アイテムの装備
        /// </summary>
        private void EquipingItem()
        {
            int ItemID = 0;
            bool IsCreatedItem = false;

            int status;

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
                    if (EquipData[i] <= 0)
                    {
                        continue;
                    }

                    status = Mine.Equip(EquipSpots[i], EquipData[i], ref ItemID, ref IsCreatedItem);

                    switch (status)
                    {
                        case Status.Equip.OK:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "を装備した。", Status.MessageLevel.Normal);
                            break;
                        case Status.Equip.NoHaveItemError:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, "所持していないアイテムが指定されたため、" + LibConst.GetEquipSpotName(EquipSpots[i]) + "に装備できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.SpotError:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "は装備できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.MainWeaponIsTwoHands:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "は装備できません。<br />メインに装備している武器が両手用の武器です。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.SubWeaponIsTwoHands:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "は両手武器であるため、装備できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.MainEquiped:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "はすでにメインに装備しているため、装備できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.SubEquiped:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "はすでにサブに装備しているため、装備できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.HeadEquiped:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "はすでに頭部に装備しているため、装備できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.BodyEquiped:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "はすでに胴体に装備しているため、装備できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.AccessoryEquiped:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "はすでに装飾に装備しているため、装備できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.MainWeaponIsNoEquiped:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "は装備できません。<br />二刀流をする場合はメインに片手武器を装備してから、再度装備してください。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.SexMatch:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "は装備可能な性別ではないため、装備できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.RaceMatch:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "は装備可能な種族ではないため、装備できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.NotLevel:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "は装備できません。<br />レベルがたりません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.SubEquipGreatShield:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "は装備できません。<br />サブに装備している盾は両手用の武器と併用できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Equip.InstallMatch:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.EquipingItem, LibConst.GetEquipSpotName(EquipSpots[i]) + "に" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "は装備できません。<br />現在のインストールクラスでは装備できないアイテムです。", Status.MessageLevel.Caution);
                            break;
                    }
                }
            }
        }
    }
}
