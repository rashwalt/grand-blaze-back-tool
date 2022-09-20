using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using CommonLibrary.Private;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        public void SendMoneyFromBazzer()
        {
            con.ReLoadBazzer();

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;
                ContinueDataEntity.ts_continue_buy_bazzerRow[] BazzerRows = (ContinueDataEntity.ts_continue_buy_bazzerRow[])con.Entity.ts_continue_buy_bazzer.Select("complete=true and seller_no=" + EntryNo);

                if (BazzerRows.Length == 0)
                {
                    // バザー購入が実行されていない or 確定していない場合はスキップ
                    continue;
                }

                foreach (ContinueDataEntity.ts_continue_buy_bazzerRow BazzerSendedRow in BazzerRows)
                {
                    Mine.HaveMoney += BazzerSendedRow.price;
                }
            }
        }
    }
}
