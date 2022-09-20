using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary.Private
{
    /// <summary>
    /// トレード管理クラス
    /// </summary>
    public static class LibTrade
    {
        /// <summary>
        /// アイテムトレード
        /// </summary>
        /// <param name="Sender">送付者</param>
        /// <param name="HaveNo">トレードするアイテム名</param>
        /// <param name="TradeItemCount">トレードするアイテム個数</param>
        /// <param name="Receipt">トレード相手エントリー番号</param>
        /// <param name="ItemNo">トレード処理のアイテム</param>
        /// <param name="IsCreatedItem">トレード処理のアイテムの作成フラグ</param>
        /// <param name="Tax">手数料</param>
        /// <param name="Speed">速達で送るか</param>
        /// <returns>ステータス</returns>
        public static int Item(LibPlayer Sender, int HaveNo, ref int TradeItemCount, LibPlayer Receipt, ref int ItemNo, ref bool IsCreatedItem, ref int Tax, bool Speed)
        {
            CommonItemEntity.item_listRow senderItemRow = Sender.GetHaveItemItemNum(Status.ItemBox.Normal, HaveNo);

            if (senderItemRow == null)
            {
                // もっていない場合、終わり
                ItemNo = 0;
                return Status.Trade.NoHaveItem;
            }

            ItemNo = senderItemRow.it_num;
            IsCreatedItem = senderItemRow.it_creatable;

            if (Receipt == null)
            {
                return Status.Trade.NoTarget;
            }

            if (Sender.EntryNo == Receipt.EntryNo)
            {
                return Status.Trade.NoMineTrade;
            }

            CommonUnitDataEntity.have_item_listRow SelectedItemRow = Sender.GetHaveItemItemRow(Status.ItemBox.Normal, HaveNo);

            CommonItemEntity.item_listRow itemRow = Sender.GetHaveItemItemNum(Status.ItemBox.Normal, HaveNo);

            // トレードする個数の確定
            if (TradeItemCount > SelectedItemRow.it_box_count)
            {
                // 指定された個数が所持数以上の場合
                TradeItemCount = SelectedItemRow.it_box_count;
            }

            if (TradeItemCount == 0)
            {
                return Status.Trade.Bazzer;
            }

            int RestItemCount = 0;

            // アイテムを装備しているか
            if (SelectedItemRow.equip_spot > 0)
            {
                // アイテムを装備している場合、トレードできない
                return Status.Trade.Equip;
            }

            if (itemRow.it_bind || itemRow.it_quest)
            {
                // バインドアイテムだったのでおわり
                return Status.Trade.BindItem;
            }

            //// 手数料の計算
            //if (Speed)
            //{
            //    Tax = (int)(LibItem.GetItemPrice(ItemNo, IsCreatedItem) * (decimal)TradeItemCount) / 100 * 1 + 50;

            //    if (Sender.EffectList.FindByeffect_id(2108) != null)
            //    {
            //        Tax -= (int)Math.Round((decimal)Tax * 0.1m, MidpointRounding.AwayFromZero);
            //    }

            //    if (Sender.HaveMoney < Tax)
            //    {
            //        return Status.Trade.NoTaxHaveMoney;
            //    }
            //}

            if (Receipt.AddItem(Status.ItemBox.Normal, ItemNo, IsCreatedItem, ref TradeItemCount, ref RestItemCount))
            {
                // アイテムが入手できたら
                Sender.RemoveItem(Status.ItemBox.Normal, HaveNo, TradeItemCount);
            }
            else
            {
                // アイテムをもてなかったので終わり
                return Status.Trade.NoAddItem;
            }

            return Status.Trade.OK;
        }

        /// <summary>
        /// マネートレード
        /// </summary>
        /// <param name="Sender">送付者</param>
        /// <param name="Money">金額</param>
        /// <param name="Receipt">トレード相手</param>
        /// <param name="Tax">手数料</param>
        /// <param name="Speed">速達で送るか</param>
        /// <returns>ステータス</returns>
        public static int Money(LibPlayer Sender, ref int Money, LibPlayer Receipt, ref int Tax, bool Speed)
        {
            // トレードするだけのお金を持っている？
            if (Sender.HaveMoney < Money)
            {
                // お金が足りません
                return Status.Trade.NoMoney;
            }

            if (Sender.EntryNo == Receipt.EntryNo)
            {
                return Status.Trade.NoMineTrade;
            }

            //// 手数料の計算
            //if (Speed)
            //{
            //    Tax = Money / 100 * 1 + 50;

            //    if (Sender.EffectList.FindByeffect_id(2108) != null)
            //    {
            //        Tax -= (int)Math.Round((decimal)Tax * 0.1m, MidpointRounding.AwayFromZero);
            //    }

            //    if (Sender.HaveMoney < (Tax + Money))
            //    {
            //        return Status.Trade.NoTaxHaveMoney;
            //    }
            //}

            // 相手のお金を先に増やす
            Receipt.HaveMoney += Money;

            Sender.HaveMoney -= Money;

            return Status.Trade.OK;
        }
    }
}
