using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.DataFormat.Format
{
    /// <summary>
    /// アイテムのＩＤと個数管理クラス
    /// </summary>
    public class ItemList
    {
        private int _ItemID = 0;
        private int _ItemCount = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="InItemID"></param>
        /// <param name="InItemCount"></param>
        public ItemList(int InItemID, int InItemCount)
        {
            _ItemID = InItemID;
            _ItemCount = InItemCount;
        }

        /// <summary>
        /// アイテムID
        /// </summary>
        public int ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }

        /// <summary>
        /// 個数
        /// </summary>
        public int ItemCount
        {
            get { return _ItemCount; }
            set { _ItemCount = value; }
        }
    }
}
