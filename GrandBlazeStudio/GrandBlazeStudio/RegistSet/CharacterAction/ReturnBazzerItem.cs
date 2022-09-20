using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// バザーアイテムを返却
        /// </summary>
        private void ReturnBazzerItem()
        {
            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                // アイテムを追加
                Mine.HaveItem.DefaultView.RowFilter = "it_box_baz_count>0";

                foreach (DataRowView BazzerReturnRow in Mine.HaveItem.DefaultView)
                {
                    int ItemCount = (int)BazzerReturnRow["it_box_baz_count"];

                    BazzerReturnRow["it_box_count"] = (int)BazzerReturnRow["it_box_count"] + ItemCount;
                    BazzerReturnRow["it_box_baz_count"] = 0;
                }

                Mine.HaveItem.DefaultView.RowFilter = "it_box_count<=0";
                foreach (DataRowView BazzerReturnRow in Mine.HaveItem.DefaultView)
                {
                    Mine.RemoveItem((int)BazzerReturnRow["box_type"], (int)BazzerReturnRow["have_no"], 9999);
                }

                Mine.HaveItem.DefaultView.RowFilter = "";
            }
        }
    }
}
