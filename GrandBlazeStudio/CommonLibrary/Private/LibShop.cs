using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.SpecialEntity;

namespace CommonLibrary.Private
{
    /// <summary>
    /// アイテム販売クラス
    /// </summary>
    public static class LibShop
    {
        /// <summary>
        /// アイテムの購入
        /// </summary>
        /// <param name="Customer">購入者</param>
        /// <param name="ItemNo">購入アイテム番号</param>
        /// <param name="StokNum">購入数</param>
        /// <param name="HaveNo">購入したアイテム所持番号(参照)</param>
        /// <param name="Bill">購入価格合計</param>
        /// <returns></returns>
        public static int BuyItem(LibPlayer Customer, int ItemNo, ref int StokNum, ref int HaveNo, ref int Bill)
        {
            Bill = 0;
            HaveNo = 0;

            if (!LibItem.CheckItemID(ItemNo, false))
            {
                // 存在しないアイテム番号の場合、終わり
                return Status.Buy.NoItem;
            }

            // 購入アイテム取得
            CommonItemEntity.item_listRow ItemRow = LibItem.GetItemRow(ItemNo, false);

            // 店売りしてるアイテムか
            // 購入金額も算出する
            if (ItemRow.it_shop != Status.Shopping.Online)
            {
                return Status.Buy.NoShop;
            }

            Bill = (int)Math.Ceiling(ItemRow.it_price * (decimal)StokNum);

            {
                EffectListEntity.effect_listRow EffectRow = Customer.EffectList.FindByeffect_id(1450);
                if (EffectRow != null)
                {
                    Bill -= (int)((decimal)Bill * EffectRow.rank / 100m);
                }
            }

            if (Customer.HaveMoney < Bill)
            {
                // お金がたりない
                return Status.Buy.NoHaveMoney;
            }

            // アイテムの増加
            if (Customer.AddItem(Status.ItemBox.Normal, ItemNo, false, ref StokNum, ref HaveNo))
            {
                // 入手できる場合のみ購入金額を減少
                Customer.HaveMoney -= Bill;
            }
            else
            {
                // 持てなかった場合
                return Status.Buy.NoHaveItem;
            }

            return Status.Buy.OK;
        }

        /// <summary>
        /// アイテムの売却
        /// </summary>
        /// <param name="Customer">売却者</param>
        /// <param name="HaveNo">売却アイテム所持番号</param>
        /// <param name="StokNum">売却数</param>
        /// <param name="ItemName">売却したアイテム名(参照)</param>
        /// <param name="Bill">売却価格合計</param>
        /// <param name="EventGetItemNo">イベント入手アイテムNo</param>
        /// <param name="EventGetItemCount">イベント入手アイテム個数</param>
        /// <returns></returns>
        public static int SellItem(LibPlayer Customer, int HaveNo, ref int StokNum, ref string ItemName, ref int Bill, ref List<int> EventGetItemNo, ref List<int> EventGetItemCount)
        {
            Bill = 0;
            int ItemNo = 0;
            bool IsCreatedItem = false;
            CommonItemEntity.item_listRow customerItemRow = Customer.GetHaveItemItemNum(Status.ItemBox.Normal, HaveNo);
            EventGetItemNo = new List<int>();
            EventGetItemCount = new List<int>();

            if (customerItemRow == null)
            {
                // もっていない場合、終わり
                return Status.Sell.NoHaveItem;
            }
            ItemNo = customerItemRow.it_num;
            IsCreatedItem = customerItemRow.it_creatable;

            CommonUnitDataEntity.have_item_listRow SelectedItemRow = Customer.GetHaveItemItemRow(Status.ItemBox.Normal, HaveNo);
            int HavingBoxCount = SelectedItemRow.it_box_count;

            // 売却する個数の確定
            if (StokNum > HavingBoxCount)
            {
                // 指定された個数が所持数以上の場合
                StokNum = HavingBoxCount;
            }

            if (StokNum == 0)
            {
                return Status.Sell.Bazzer;
            }

            int EquipedCount = 0;

            // アイテムを装備しているか
            if (SelectedItemRow.equip_spot > 0)
            {
                EquipedCount++;
            }

            if (EquipedCount > StokNum)
            {
                // アイテムを装備している場合、売却できない
                return Status.Sell.Equip;
            }

            CommonItemEntity.item_listRow ItemRow = LibItem.GetItemRow(ItemNo, IsCreatedItem);
            ItemName = ItemRow.it_name;

            EffectListEntity.effect_listDataTable EffectTable = new EffectListEntity.effect_listDataTable();
            LibEffect.Split(ItemRow.it_effect, ref EffectTable);

            EffectListEntity.effect_listRow EffectRow = EffectTable.FindByeffect_id(4505);

            // 売却金額の算出
            Bill = (int)Math.Floor(ItemRow.it_seller * (decimal)StokNum);

            // アイテムの除去
            if (Customer.RemoveItem(Status.ItemBox.Normal, HaveNo, StokNum))
            {
                // イベント判定
                if (EffectRow != null)
                {
                    // 個数確認
                    decimal EffectProb = EffectRow.prob / 10.0m;
                    int EffectCount = (int)(EffectProb * (decimal)StokNum);

                    if (EffectCount > 0)
                    {
                        EventGetItemNo.Add((int)EffectRow.rank);
                        EventGetItemCount.Add(EffectCount);
                    }
                }

                // 除去できる場合のみ売却金額を増加
                Customer.HaveMoney += Bill;

                // お宝カウント
                if (ItemRow.it_type == 67)
                {
                    LibSelledTreasure.AddCount(ItemRow.it_num, StokNum);
                }
            }

            return Status.Sell.OK;
        }
    }
}
