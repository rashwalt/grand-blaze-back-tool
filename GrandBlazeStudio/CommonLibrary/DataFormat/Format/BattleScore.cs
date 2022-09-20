using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.DataFormat.Format
{
    /// <summary>
    /// バトルスコア
    /// </summary>
    public class BattleScore
    {
        private int _MaxAttackDamage = 0;

        /// <summary>
        /// 最大与ダメージ
        /// </summary>
        public int MaxAttackDamage
        {
            get
            {
                return _MaxAttackDamage;
            }
            set
            {
                if (value > _MaxAttackDamage)
                {
                    _MaxAttackDamage = value;
                }
            }
        }
    }
}
