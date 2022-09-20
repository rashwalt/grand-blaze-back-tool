using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using System.IO;
using CommonLibrary.Private;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// アイテムの購入
        /// </summary>
        private void BuyingItem()
        {
            int HaveNo = 0;
            int Bill = 0;

            int status;
            int StokNum = 0;
            int ItemID = 0;
            string CountItemStok = "";

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_shoppingRow[] ContinueShoppingRows = (ContinueDataEntity.ts_continue_shoppingRow[])con.Entity.ts_continue_shopping.Select("shop_act=0 and item_no>0 and item_count>0 and entry_no=" + EntryNo);

                if (ContinueShoppingRows.Length == 0)
                {
                    // ない場合はスキップ
                    continue;
                }

                foreach (ContinueDataEntity.ts_continue_shoppingRow BuyItemRow in ContinueShoppingRows)
                {
                    CountItemStok = "";

                    StokNum = BuyItemRow.item_count;
                    ItemID = BuyItemRow.item_no;

                    status = LibShop.BuyItem(Mine, ItemID, ref StokNum, ref HaveNo, ref Bill);

                    if (StokNum > 1)
                    {
                        CountItemStok = StokNum + "個";
                    }

                    Bill = Bill * -1;

                    switch (status)
                    {
                        case Status.Buy.OK:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.BuyingItem, LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + "を" + CountItemStok + "購入した。[" + LibResultText.CSSEscapeMoney(Bill, true) + "]", Status.MessageLevel.Normal);
                            break;
                        case Status.Buy.NoHaveItem:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.BuyingItem, "バッグに空きがないため、購入できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Buy.NoItem:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.BuyingItem, "指定されたアイテム番号:" + ItemID + "にアイテムは存在しません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Buy.NoShop:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.BuyingItem, "指定されたアイテム番号:" + ItemID + "のアイテムが扱われているお店が見つかりません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Buy.NoHaveMoney:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.BuyingItem, "お金が足りないため、購入できません。", Status.MessageLevel.Caution);
                            break;
                    }
                }
            }
        }
    }
}
