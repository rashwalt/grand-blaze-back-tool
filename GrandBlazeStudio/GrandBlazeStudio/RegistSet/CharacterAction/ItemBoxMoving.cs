using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.SpecialEntity;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// アイテムボックスからの移動
        /// </summary>
        private void ItemBoxMoving()
        {
            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                Mine.HaveItem.DefaultView.RowFilter = "box_type=" + Status.ItemBox.Box;

                if (Mine.HaveItem.DefaultView.Count == 0)
                {
                    continue;
                }

                int[] BoxItems=Mine.GetBoxItemNumbers();

                // ボックスからの移動
                foreach (int BoxID in BoxItems)
                {
                    if (Mine.CheckHaveItemMax(Status.ItemBox.Normal))
                    {
                        // いっぱいなのでスキップ
                        continue;
                    }

                    CommonUnitDataEntity.have_item_listRow ItemRow = Mine.GetHaveItemItemRow(Status.ItemBox.Box, BoxID);
                    int ItemNo = ItemRow.it_num;
                    bool IsCreated = ItemRow.created;
                    int ItemCount = ItemRow.it_box_count;
                    int RestItemCount = 0;

                    // 移動開始
                    if (Mine.AddItem(Status.ItemBox.Normal, ItemNo, IsCreated, ref ItemCount, ref RestItemCount))
                    {
                        Mine.RemoveItem(Status.ItemBox.Box, BoxID, ItemCount);
                    }
                }
            }
        }
    }
}
