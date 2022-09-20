using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;

namespace CommonLibrary.DataFormat.Format
{
    public class WeaponAbility
    {
        public WeaponAbility()
        {
            Elemental.Strike = 100;
        }


        /// <summary>
        /// 回避性能
        /// </summary>
        public int Avoid = 0;

        /// <summary>
        /// 射程
        /// </summary>
        public int Range = Status.Range.Short;

        /// <summary>
        /// チャージタイム
        /// </summary>
        public int ChargeTime = 20;

        /// <summary>
        /// アイテム種類
        /// </summary>
        public int ItemType = 0;

        /// <summary>
        /// アイテムサブカテゴリ
        /// </summary>
        public int ItemSubType = 0;

        /// <summary>
        /// 攻撃種別
        /// </summary>
        public int AttackDamageType = Status.AttackType.Combat;

        /// <summary>
        /// クリティカル/連撃値
        /// </summary>
        public int Critical = 0;

        /// <summary>
        /// 属性
        /// </summary>
        public Elemental Elemental = new Elemental();

        /// <summary>
        /// ターゲットエリア
        /// </summary>
        public int TargetArea = Status.TargetArea.Only;

        /// <summary>
        /// エフェクト
        /// </summary>
        public EffectListEntity.effect_listDataTable Effect = new EffectListEntity.effect_listDataTable();

    }
}
