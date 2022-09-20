using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary.Character
{
    public partial class LibMonster : LibUnitBase
    {
        /// <summary>
        /// 最大連撃回数
        /// </summary>
        public int MultiAttackMaxCount = 1;

        /// <summary>
        /// ターゲット種別
        /// </summary>
        public int TargetType = 0;

        /// <summary>
        /// エレメントテーブル
        /// </summary>
        private MonsterDataEntity.mt_monster_elementDataTable ElementTable = new MonsterDataEntity.mt_monster_elementDataTable();

        /// <summary>
        /// 回避
        /// </summary>
        public override int AVD
        {
            get
            {
                if (base.AVD == 0)
                {
                    return 0;
                }

                // 回避特性があるか？
                if (EffectList.FindByeffect_id(845) != null)
                {
                    return base.AVD + 25;
                }
                else
                {
                    return base.AVD;
                }
            }
        }

        /// <summary>
        /// スティールされたかどうか
        /// </summary>
        public bool IsSteal = false;

        //特殊
        /// <summary>
        /// フレイムプレス使用回数
        /// </summary>
        public int FlamePressUseCount = 0;

        /// <summary>
        /// 生命の賛歌使用直後？
        /// </summary>
        public bool IsUseLifeSong = false;
    }
}
