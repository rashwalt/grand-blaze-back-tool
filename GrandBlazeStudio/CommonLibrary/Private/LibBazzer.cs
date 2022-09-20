using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary.Private
{
    /// <summary>
    /// バザー管理クラス
    /// </summary>
    public static class LibBazzer
    {
        /// <summary>
        /// バザー購入
        /// </summary>
        /// <param name="Buyer">購入者</param>
        /// <param name="HaveNo">購入するアイテム番号（販売者の所持番号）</param>
        /// <param name="ItemCount">購入するアイテム個数</param>
        /// <param name="Price">購入価格</param>
        /// <param name="Seller">売却者</param>
        /// <param name="ItemNo">バザー処理のアイテム</param>
        /// <param name="IsCreatedItem">バザー処理のアイテムの作成フラグ</param>
        /// <returns>ステータス</returns>
        public static int Buy(LibPlayer Buyer, int HaveNo, int BazzerItemNo, bool BazzerItemCreated, int ItemCount, int Price, LibPlayer Seller, ref int ItemNo, ref bool IsCreatedItem)
        {
            if (Buyer.EntryNo == Seller.EntryNo)
            {
                return Status.Bazzer.NoMineTrade;
            }

            CommonUnitDataEntity.have_item_listRow SelectedItemRow = Seller.HaveItem.FindBybox_typehave_nodrop_typeget_synx(Status.ItemBox.Normal, HaveNo, 0, 0);

            if (SelectedItemRow == null)
            {
                return Status.Bazzer.NoHaveItem;
            }

            int SellerHaveNo = SelectedItemRow.have_no;

            ItemNo = SelectedItemRow.it_num;
            IsCreatedItem = SelectedItemRow.created;

            if (BazzerItemNo != ItemNo && BazzerItemCreated != IsCreatedItem)
            {
                return Status.Bazzer.NoHaveItem;
            }

            int RestItemCount = 0;

            // 購入者がお金を持っている？
            if (Buyer.HaveMoney < Price)
            {
                // お金が足りません
                return Status.Bazzer.NoMoney;
            }

            // 個数の再設定
            if (ItemCount > SelectedItemRow.it_box_baz_count)
            {
                return Status.Bazzer.NoHaveItem;
            }

            SelectedItemRow.it_box_baz_count -= ItemCount;

            if (!Buyer.AddItem(Status.ItemBox.Normal, ItemNo, IsCreatedItem, ref ItemCount, ref RestItemCount))
            {
                ItemCount = RestItemCount;
                // アイテムボックスへ
                Buyer.AddItem(Status.ItemBox.Box, ItemNo, IsCreatedItem, ref ItemCount, ref RestItemCount);
            }

            Buyer.HaveMoney -= Price;

            return Status.Bazzer.OK;
        }
    }
}
