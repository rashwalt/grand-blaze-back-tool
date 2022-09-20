using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// リスト管理
    /// </summary>
    public class LibList
    {
        /// <summary>
        /// リストHTMLファイル名取得
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>HTMLファイル名</returns>
        public static string GetListHTML(int EntryNo)
        {
            int ListNo = (int)((decimal)(EntryNo - 1) / 100.0M) + 1;

            return "list" + ListNo.ToString("0000") + ".html";
        }

        /// <summary>
        /// 最小エントリ取得
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>最小値</returns>
        public static int GetMinNo(int EntryNo)
        {
            int ListNo = (int)((decimal)(EntryNo - 1) / 100.0M);

            return ListNo * 100 + 1;
        }

        /// <summary>
        /// 最大エントリ取得
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>最大値</returns>
        public static int GetMaxNo(int EntryNo, int MaxNo)
        {
            int ListNo = (int)((decimal)(EntryNo - 1) / 100.0M);

            ListNo = ListNo * 100 + 100;

            return ListNo;
        }
    }
}
