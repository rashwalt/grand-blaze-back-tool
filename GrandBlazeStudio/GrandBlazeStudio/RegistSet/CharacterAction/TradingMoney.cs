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
        /// お金のトレード
        /// </summary>
        /// <param name="Speed">速達か</param>
        private void TradingMoney(bool Speed)
        {
            LibUnitBaseMini FixCharas = new LibUnitBaseMini();

            int status = -1;
            int Moneys = 0;

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;
                //ContinueDataEntity.ts_continue_tradeRow[] ContinueTradeRows = (ContinueDataEntity.ts_continue_tradeRow[])con.Entity.ts_continue_trade.Select("trade_entry>0 and trade_item_no=0 and trade_number>0 and trade_speed=" + Speed + " and entry_no=" + EntryNo);
                ContinueDataEntity.ts_continue_tradeRow[] ContinueTradeRows = (ContinueDataEntity.ts_continue_tradeRow[])con.Entity.ts_continue_trade.Select("trade_entry>0 and trade_item_no=0 and trade_number>0 and entry_no=" + EntryNo);

                if (ContinueTradeRows.Length == 0)
                {
                    // トレードを実行しない場合はスキップ
                    continue;
                }

                foreach (ContinueDataEntity.ts_continue_tradeRow TradeItemRow in ContinueTradeRows)
                {
                    Moneys = TradeItemRow.trade_number;
                    int Tax = 0;

                    if (!FixCharas.CheckInChara(TradeItemRow.trade_entry))
                    {
                        continue;
                    }

                    LibPlayer Receipt = CharaList.Find(chs => chs.EntryNo == TradeItemRow.trade_entry);

                    status = LibTrade.Money(Mine, ref Moneys, Receipt, ref Tax, Speed);

                    switch (status)
                    {
                        case Status.Trade.OK:
                            if (TradeItemRow.trade_message.Length > 0)
                            {
                                LibMessage.SenderMessage(Mine, Receipt, TradeItemRow.trade_message, Status.PlayerSysMemoType.TradeMoney);
                            }
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeMoney, LibResultText.CSSEscapeMoney(Moneys, false) + "を" + FixCharas.GetNickNameWithLink(Receipt.EntryNo, 1) + "にトレードした。", Status.MessageLevel.Normal);
                            LibPlayerMemo.AddSystemMessage(Receipt.EntryNo, Status.PlayerSysMemoType.TradeMoney, FixCharas.GetNickNameWithLink(Mine.EntryNo, 1) + "から" + LibResultText.CSSEscapeMoney(Moneys, false) + "を受け取った。", Status.MessageLevel.Normal);
                            if (Speed)
                            {
                                // 速達料金消費
                                Mine.HaveMoney -= Tax;
                                LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeMoney, "※速達により、手数料" + LibResultText.CSSEscapeMoney(Tax, false) + "が消費されました。", Status.MessageLevel.Caution);
                            }
                            break;
                        case Status.Trade.NoMineTrade:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeMoney, LibResultText.CSSEscapeMoney(Moneys, false) + "を自分にはトレードできません。", Status.MessageLevel.Caution);
                            break;
                        case Status.Trade.NoMoney:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeMoney, LibResultText.CSSEscapeMoney(Moneys, false) + "を" + FixCharas.GetNickNameWithLink(Receipt.EntryNo, 1) + "にトレードした。<br />しかし、渡せるだけのお金がない。", Status.MessageLevel.Caution);
                            break;
                        case Status.Trade.NoTaxHaveMoney:
                            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.TradeMoney, LibResultText.CSSEscapeMoney(Moneys, false) + "を速達でトレードしようとしたが、手数料を払えないためトレードできなかった。", Status.MessageLevel.Caution);
                            break;
                    }
                }
            }
        }
    }
}
