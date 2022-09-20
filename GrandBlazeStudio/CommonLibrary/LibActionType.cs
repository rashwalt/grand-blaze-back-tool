using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary
{
    /// <summary>
    /// 戦闘行動種別
    /// </summary>
    public class LibActionType
    {
        private CommonSkillEntity.skill_listRow ActionRow;
        public int ActionBase = 0;
        public EffectListEntity.effect_listDataTable EffectList = new EffectListEntity.effect_listDataTable();
        public bool IsNormalAttack = false;
        public int ActionType = 0;

        /// <summary>
        /// アイテム種類
        /// </summary>
        public int ItemType = 0;

        /// <summary>
        /// アイテムサブカテゴリ
        /// </summary>
        public int ItemSubType = 0;

        /// <summary>
        /// 追加効果か？
        /// </summary>
        public bool AddAttack = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="InActionRow">格納スキルデータ</param>
        /// <param name="ActionBaseType">基準能力（メイン、遠隔通常など）</param>
        /// <param name="EffectInTable">エフェクト</param>
        /// <param name="NormalAttack">通常攻撃か</param>
        /// <param name="ActionTypeCode">アクションタイプコード</param>
        public LibActionType(CommonSkillEntity.skill_listRow InActionRow, int ActionBaseType, EffectListEntity.effect_listDataTable EffectInTable, bool NormalAttack, int ActionTypeCode)
        {
            ActionRow = InActionRow;
            ActionBase = ActionBaseType;
            EffectList = EffectInTable;
            IsNormalAttack = NormalAttack;
            ActionType = ActionTypeCode;
        }

        /// <summary>
        /// スキルID
        /// </summary>
        public int SkillID
        {
            get
            {
                return ActionRow.sk_id;
            }
        }

        /// <summary>
        /// ターゲット：パーティ
        /// </summary>
        public int TargetParty
        {
            get
            {
                return ActionRow.sk_target_party;
            }
            set
            {
                ActionRow.sk_target_party = value;
            }
        }

        /// <summary>
        /// ターゲット：エリア
        /// </summary>
        public int TargetArea
        {
            get
            {
                // エフェクトによるターゲット範囲拡張
                int EffectID = 0;

                switch (AttackType)
                {
                    case Status.AttackType.Item:
                        EffectID = 881;
                        break;
                    default:
                        EffectID = 882;
                        break;
                }

                EffectListEntity.effect_listRow EffectRow = EffectList.FindByeffect_id(EffectID);

                if (EffectRow != null && EffectRow.prob > LibInteger.GetRandBasis())
                {
                    switch ((int)EffectRow.rank)
                    {
                        case 1:
                            return Status.TargetArea.Circle1;
                        case 2:
                            return Status.TargetArea.Circle2;
                        case 3:
                            return Status.TargetArea.Line;
                        case 4:
                            return Status.TargetArea.All;
                        default:
                            return ActionRow.sk_target_area;
                    }
                }
                else
                {
                    return ActionRow.sk_target_area;
                }
            }
        }

        /// <summary>
        /// アーツ名称
        /// </summary>
        public string ArtsName
        {
            get
            {
                return ActionRow.sk_name;
            }
        }

        /// <summary>
        /// 基本威力
        /// </summary>
        public int Attack
        {
            get
            {
                return ActionRow.sk_attack;
            }
        }

        /// <summary>
        /// 攻撃・判定回数
        /// </summary>
        public int ActionCount
        {
            get
            {
                return ActionRow.sk_round;
            }
        }

        /// <summary>
        /// 攻撃タイプ
        /// </summary>
        public int AttackType
        {
            get
            {
                return ActionRow.sk_atype;
            }
        }

        /// <summary>
        /// 消費MP
        /// </summary>
        public int MP
        {
            get
            {
                return ActionRow.sk_mp;
            }
        }

        /// <summary>
        /// 消費TP
        /// </summary>
        public int TP
        {
            get
            {
                return ActionRow.sk_tp;
            }
        }

        /// <summary>
        /// ダメージ影響タイプ
        /// </summary>
        public int DamageType
        {
            get
            {
                return ActionRow.sk_damage_type;
            }
        }

        /// <summary>
        /// 対空効果
        /// </summary>
        public bool AntiAir
        {
            get
            {
                return ActionRow.sk_antiair;
            }
        }

        /// <summary>
        /// 属性
        /// </summary>
        /// <param name="ElementalString">属性種類</param>
        /// <returns>属性値</returns>
        public int Elemental(string ElementalString)
        {
            return (int)ActionRow["sk_" + ElementalString];
        }

        /// <summary>
        /// 属性保有
        /// </summary>
        /// <param name="ElementalString">属性種類</param>
        /// <returns>属性の有無</returns>
        public bool CheckElemental(string ElementalString)
        {
            return (int)ActionRow["sk_" + ElementalString] > 0;
        }

        /// <summary>
        /// スキル種別
        /// </summary>
        public int SkillType
        {
            get
            {
                return ActionRow.sk_type;
            }
        }

        /// <summary>
        /// アーツカテゴリ
        /// </summary>
        public int ArtsCategory
        {
            get
            {
                return ActionRow.sk_arts_category;
            }
        }

        /// <summary>
        /// 射程
        /// </summary>
        public int Range
        {
            get
            {
                return ActionRow.sk_range;
            }
        }

        /// <summary>
        /// 命中力
        /// </summary>
        public int HitRate
        {
            get
            {
                return ActionRow.sk_hit;
            }
        }

        /// <summary>
        /// 増加ヘイト
        /// </summary>
        public int Hate
        {
            get
            {
                return ActionRow.sk_hate;
            }
        }

        /// <summary>
        /// 回復ヘイト
        /// </summary>
        public int VHate
        {
            get
            {
                return ActionRow.sk_vhate;
            }
        }

        /// <summary>
        /// 減少ヘイト
        /// </summary>
        public int DHate
        {
            get
            {
                return ActionRow.sk_dhate;
            }
        }

        /// <summary>
        /// 待機時間係数
        /// </summary>
        public int Charge
        {
            get
            {
                return ActionRow.sk_charge;
            }
        }

        /// <summary>
        /// 攻撃性能
        /// </summary>
        public decimal Power
        {
            get
            {
                return ActionRow.sk_power;
            }
        }

        /// <summary>
        /// スコアプラス
        /// </summary>
        public int PlusScore
        {
            get
            {
                return ActionRow.sk_plus_score;
            }
        }

        /// <summary>
        /// 攻撃係数
        /// </summary>
        public decimal DamageRate
        {
            get
            {
                return ActionRow.sk_damage_rate;
            }
        }

        /// <summary>
        /// クリティカル性能
        /// </summary>
        public int Critical
        {
            get
            {
                return ActionRow.sk_critical;
            }
        }

        /// <summary>
        /// クリティカルタイプ
        /// </summary>
        public int CriticalType
        {
            get
            {
                return ActionRow.sk_critical_type;
            }
        }

        /// <summary>
        /// 攻撃アーツ？
        /// </summary>
        public bool IsAttackArts
        {
            get
            {
                return ActionRow.sk_power > 0 || ActionRow.sk_damage_rate > 0 || ActionRow.sk_plus_score > 0;
            }
        }

        /// <summary>
        /// 解除保有
        /// </summary>
        /// <param name="ElementalString">チェックエフェクト</param>
        /// <returns>エフェクトの有無</returns>
        public bool CheckEffect(int EffectID)
        {
            if (EffectList.FindByeffect_id(EffectID) != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 属性取得
        /// </summary>
        /// <param name="Elemental">属性値</param>
        /// <returns>その属性になっているかどうか</returns>
        public bool CheckElementalByFit(int Elemental)
        {
            switch (Elemental)
            {
                case 1:
                    if (this.CheckElemental(Status.Elemental.Fire))
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (this.CheckElemental(Status.Elemental.Freeze))
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (this.CheckElemental(Status.Elemental.Air))
                    {
                        return true;
                    }
                    break;
                case 4:
                    if (this.CheckElemental(Status.Elemental.Earth))
                    {
                        return true;
                    }
                    break;
                case 5:
                    if (this.CheckElemental(Status.Elemental.Water))
                    {
                        return true;
                    }
                    break;
                case 6:
                    if (this.CheckElemental(Status.Elemental.Thunder))
                    {
                        return true;
                    }
                    break;
                case 7:
                    if (this.CheckElemental(Status.Elemental.Holy))
                    {
                        return true;
                    }
                    break;
                case 8:
                    if (this.CheckElemental(Status.Elemental.Dark))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}
