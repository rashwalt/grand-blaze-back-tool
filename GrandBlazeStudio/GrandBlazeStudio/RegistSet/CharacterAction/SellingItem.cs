using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using System.IO;
using CommonLibrary.Private;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// アイテムの売却
        /// </summary>
        private void SellingItem()
        {
            string ItemName = "";
            int Bill = 0;

            int j = 0;
            int status;
            int ItemNo = 0;
            int StokNum = 0;
            string CountItemStok = "";
            List<int> EventItemNo = new List<int>();
            List<int> EventItemCount = new List<int>();
            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_shoppingRow[] ContinueShoppingRows = (ContinueDataEntity.ts_continue_shoppingRow[])con.Entity.ts_continue_shopping.Select("shop_act=1 and item_no>0 and item_count>0 and entry_no=" + EntryNo);

                if (ContinueShoppingRows.Length == 0)
                {
                    // ない場合はスキップ
                    continue;
                }

                foreach (ContinueDataEntity.ts_continue_shoppingRow SellItemRow in ContinueShoppingRows)
                {
                    CountItemStok = "";
                    ItemName = "";

                    ItemNo = SellItemRow.item_no;
                    StokNum = SellItemRow.item_count;

                    status = LibShop.SellItem(Mine, ItemNo, ref StokNum, ref ItemName, ref Bill, ref EventItemNo, ref EventItemCount);

                    if (StokNum > 1)
                    {
                        CountItemStok = StokNum + "個";
                    }

                    switch (status)
                    {
                        case Status.Sell.OK:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.SellingItem, LibResultText.CSSEscapeItem(ItemName) + "を" + CountItemStok + "売却した。[" + LibResultText.CSSEscapeMoney(Bill, true) + "]", Status.MessageLevel.Normal);
                            break;
                        case Status.Sell.NoHaveItem:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.SellingItem, "所持していないアイテムを売却しようとしました。", Status.MessageLevel.Caution);
                            break;
                        case Status.Sell.Equip:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.SellingItem, LibResultText.CSSEscapeItem(ItemName) + "は装備しているため売却できません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Sell.Bazzer:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.SellingItem, LibResultText.CSSEscapeItem(ItemName) + "はバザーに出品中であるため売却できません。", Status.MessageLevel.Caution);
                            break;
                    }
                }
            }
        }
    }
}
