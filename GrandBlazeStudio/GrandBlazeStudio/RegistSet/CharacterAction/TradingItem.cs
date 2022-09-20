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
        /// アイテムのトレード(対ＰＣ)
        /// </summary>
        /// <param name="Speed">速達か</param>
        private void TradingItem(bool Speed)
        {
            LibUnitBaseMini FixCharas = new LibUnitBaseMini();

            int ItemID = 0;
            bool IsCreatedItem = false;

            int status = -1;
            int StokNum = 0;
            string CountItemStok = "";

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                //ContinueDataEntity.ts_continue_tradeRow[] ContinueTradeRows = (ContinueDataEntity.ts_continue_tradeRow[])con.Entity.ts_continue_trade.Select("trade_entry>0 and trade_item_no>0 and trade_number>0 and trade_speed=" + Speed + " and entry_no=" + EntryNo);
                ContinueDataEntity.ts_continue_tradeRow[] ContinueTradeRows = (ContinueDataEntity.ts_continue_tradeRow[])con.Entity.ts_continue_trade.Select("trade_entry>0 and trade_item_no>0 and trade_number>0 and entry_no=" + EntryNo);

                if (ContinueTradeRows.Length == 0)
                {
                    // トレードを実行しない場合はスキップ
                    continue;
                }

                foreach (ContinueDataEntity.ts_continue_tradeRow TradeItemRow in ContinueTradeRows)
                {
                    CountItemStok = "";
                    int Tax = 0;
                    StokNum = TradeItemRow.trade_number;
                    status = -1;

                    if (!CharaMini.CheckInChara(TradeItemRow.trade_entry))
                    {
                        continue;
                    }

                    LibPlayer Receipt = CharaList.Find(chs => chs.EntryNo == TradeItemRow.trade_entry);

                    status = LibTrade.Item(Mine, TradeItemRow.trade_item_no, ref StokNum, Receipt, ref ItemID, ref IsCreatedItem, ref Tax, Speed);

                    if (StokNum > 1)
                    {
                        CountItemStok = StokNum + "個";
                    }

                    switch (status)
                    {
                        case Status.Trade.OK:
                            if (TradeItemRow.trade_message.Length > 0)
                            {
                                LibMessage.SenderMessage(Mine, Receipt, TradeItemRow.trade_message, Status.PlayerSysMemoType.TradeItem);
                            }
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeItem, LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "を" + CountItemStok + "、" + FixCharas.GetNickNameWithLink(Receipt.EntryNo, 1) + "にトレードした。", Status.MessageLevel.Normal);
                            LibPlayerMemo.AddSystemMessage(Receipt.EntryNo, Status.PlayerSysMemoType.TradeItem, FixCharas.GetNickNameWithLink(Mine.EntryNo, 1) + "から" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "を" + CountItemStok + "受け取った。", Status.MessageLevel.Normal);
                            if (Speed)
                            {
                                // 速達料金消費
                                Mine.HaveMoney -= Tax;
                                LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeItem, "※速達により、手数料" + LibResultText.CSSEscapeMoney(Tax, false) + "が消費されました。", Status.MessageLevel.Caution);
                            }
                            break;
                        case Status.Trade.NoHaveItem:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeItem, "所持していないアイテムが指定されたため、トレードできません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Trade.NoMineTrade:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeItem, "自分自身へはトレードできません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Trade.Equip:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeItem, LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "は装備しているためトレードできません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Trade.NoAddItem:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeItem, LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "を" + CountItemStok + "、" + FixCharas.GetNickNameWithLink(Receipt.EntryNo, 1) + "にトレードした。<br />しかし、このアイテムをもてないようだ…。", Status.MessageLevel.Caution);
                            LibPlayerMemo.AddSystemMessage(Receipt.EntryNo, Status.PlayerSysMemoType.TradeItem, FixCharas.GetNickNameWithLink(Mine.EntryNo, 1) + "から" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "を" + CountItemStok + "受け取った。<br />しかし、持てなかったので返却した…。", Status.MessageLevel.Caution);
                            break;
                        case Status.Trade.BindItem:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeItem, LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "はトレードできないアイテムです。", Status.MessageLevel.Caution);
                            break;
                        case Status.Trade.Bazzer:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeItem, LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "はバザーに出品中であるためトレードできません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Trade.NoTaxHaveMoney:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeItem, LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "を速達でトレードしようとしたが、手数料を払えないためトレードできなかった。", Status.MessageLevel.Caution);
                            break;
                        case Status.Trade.NoTarget:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeItem, LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, IsCreatedItem)) + "をトレードしようとしたが、トレード対象の冒険者(E-No." + TradeItemRow.trade_entry + ")が見つかりませんでした。", Status.MessageLevel.Caution);
                            break;
                    }
                }
            }
        }
    }
}
