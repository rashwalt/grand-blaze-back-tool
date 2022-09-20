using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    public static class LibGuardian
    {
        /// <summary>
        /// ガーディアン名取得
        /// </summary>
        /// <param name="GuardianID">ガーディアンID</param>
        /// <returns>ガーディアン名</returns>
        public static string GetName(int GuardianID)
        {
            switch (GuardianID)
            {
                case 1:
                    return "修羅の炎帝イグニート";
                case 2:
                    return "氷花の乙女セルシウス";
                case 3:
                    return "風来の鬼神チャフリカ";
                case 4:
                    return "地獄の咆哮クツェルカン";
                case 5:
                    return "湧泉の真人カアシャック";
                case 6:
                    return "轟縛の雷帝イーヴァン";
                case 7:
                    return "閃光の翼士イシュタス";
                case 8:
                    return "漆黒の魔手アン・プトゥ";
                default:
                    return "";
            }
        }
    }
}
