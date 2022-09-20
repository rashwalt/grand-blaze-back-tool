using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.DataFormat.Format
{
    /// <summary>
    /// アイテム選択用のクラス
    /// </summary>
    public class ItemSelectNames
    {
        private int _ItemID = 0;
        private string _ViewName = "";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="InItemID"></param>
        /// <param name="InViewName"></param>
        public ItemSelectNames(int InItemID, string InViewName)
        {
            _ItemID = InItemID;
            _ViewName = InViewName;
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
        /// 表示名
        /// </summary>
        public string ViewName
        {
            get { return _ViewName; }
            set { _ViewName = value; }
        }
    }
}
