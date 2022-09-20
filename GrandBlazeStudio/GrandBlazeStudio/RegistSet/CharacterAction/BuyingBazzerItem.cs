using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using CommonLibrary.Private;
using System.IO;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// バザーアイテム購入
        /// </summary>
        private void BuyingBazzerItem()
        {
            LibUnitBaseMini FixCharas = new LibUnitBaseMini();

            int ItemID = 0;
            bool IsCreatedItem = false;

            int status;

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_buy_bazzerRow[] ContinueBuyBazzerRows = (ContinueDataEntity.ts_continue_buy_bazzerRow[])con.Entity.ts_continue_buy_bazzer.Select("entry_no=" + EntryNo);

                if (ContinueBuyBazzerRows.Length == 0)
                {
                    // バザー購入を実行しない場合はスキップ
                    continue;
                }

                foreach (ContinueDataEntity.ts_continue_buy_bazzerRow BazzerBuyRow in ContinueBuyBazzerRows)
                {
                    if (FixCharas.CheckInChara(BazzerBuyRow.seller_no))
                    {
                        LibPlayer Seller = CharaList.Find(chs => chs.EntryNo == BazzerBuyRow.seller_no);

                        status = LibBazzer.Buy(Mine, BazzerBuyRow.have_no, BazzerBuyRow.it_id, false, 1, BazzerBuyRow.price, Seller, ref ItemID, ref IsCreatedItem);

                        int BuyPrice = BazzerBuyRow.price;
                        int SellPrice = BazzerBuyRow.price;

                        switch (status)
                        {
                            case Status.Bazzer.OK:
                                LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.BuyingBazzerItem, LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "を、バザーから落札した。[" + LibResultText.CSSEscapeMoney(BuyPrice * -1, true) + "]", Status.MessageLevel.Normal);
                                LibPlayerMemo.AddSystemMessage(Seller.EntryNo, Status.PlayerSysMemoType.BuyingBazzerItem, "バザーに出品していた" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "が、売れました。[" + LibResultText.CSSEscapeMoney(SellPrice, true) + "]", Status.MessageLevel.Normal);
                                con.UpdateBazzerComplete(EntryNo, BazzerBuyRow.seller_no, BazzerBuyRow.bazzer_id);
                                break;
                            case Status.Bazzer.NoMineTrade:
                                LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.BuyingBazzerItem, "バザーに出品中のアイテムは自分では落札できません。", Status.MessageLevel.Caution);
                                break;
                            case Status.Bazzer.NoMoney:
                                LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.BuyingBazzerItem, LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "を、バザーから落札しようとしたが、お金が足りない。", Status.MessageLevel.Caution);
                                break;
                            case Status.Bazzer.NoHaveItem:
                                LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.BuyingBazzerItem, "バザーから落札しようとしたが、落札できるアイテムを出品者がもっていない。", Status.MessageLevel.Caution);
                                break;
                        }
                    }
                    else
                    {
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.BuyingBazzerItem, "登録されていない冒険者が出品していたため、落札できませんでした。", Status.MessageLevel.Caution);
                    }
                }
            }
        }
    }
}
