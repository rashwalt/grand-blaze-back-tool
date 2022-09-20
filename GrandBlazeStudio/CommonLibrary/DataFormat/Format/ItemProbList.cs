using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.DataFormat.Format
{
    /// <summary>
    /// アイテムの確率とＩＤと個数管理クラス
    /// </summary>
    public class ItemProbList
    {
        private int _MinProb = 0;
        private int _MaxProb = 0;
        private int _ItemID = 0;
        private int _ItemCount = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="InMinProb"></param>
        /// <param name="InMaxProb"></param>
        /// <param name="InItemID"></param>
        /// <param name="InItemCount"></param>
        public ItemProbList(int InMinProb, int InMaxProb, int InItemID, int InItemCount)
        {
            _MinProb = InMinProb;
            _MaxProb = InMaxProb;
            _ItemID = InItemID;
            _ItemCount = InItemCount;
        }

        /// <summary>
        /// 確率(下限)
        /// </summary>
        public int MinProb
        {
            get { return _MinProb; }
            set { _MinProb = value; }
        }

        /// <summary>
        /// 確率(上限)
        /// </summary>
        public int MaxProb
        {
            get { return _MaxProb; }
            set { _MaxProb = value; }
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
