using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// ランクから能力を計算するライブラリ
    /// </summary>
    public static class LibRankData
    {
        private static RankDataEntity Entity;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static LibRankData()
        {
            Entity = new RankDataEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_rank_list>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("rank_type,");
                SelSql.AppendLine("rank_level,");
                SelSql.AppendLine("rank_number_a,");
                SelSql.AppendLine("rank_number_b");
                SelSql.AppendLine("FROM mt_rank_list");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_rank_list);
            }
        }

        /// <summary>
        /// キャラクターのレベルとランクからHP数値を取得します
        /// </summary>
        /// <param name="Rank">HPのランク</param>
        /// <param name="Level">レベル</param>
        /// <returns></returns>
        public static decimal GetRankToHP(int Rank, int Level)
        {
            RankDataEntity.mt_rank_listRow row = Entity.mt_rank_list.FindByrank_typerank_level(Status.Rank.HP, Rank);

            int Upsink = 0;
            for (int i = 1; i <= Level; i++)
            {
                Upsink += LibExperience.HPBonus[i];
            }

            if (row != null)
            {
                decimal RankHP = 0;
                RankHP = (int)((decimal)(row.rank_number_b + Upsink) * row.rank_number_a);
                return RankHP;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// キャラクターのレベルとランクからMP数値を取得します
        /// </summary>
        /// <param name="Rank">MPのランク</param>
        /// <param name="Level">レベル</param>
        /// <returns></returns>
        public static decimal GetRankToMP(int Rank, int Level)
        {
            RankDataEntity.mt_rank_listRow row = Entity.mt_rank_list.FindByrank_typerank_level(Status.Rank.MP, Rank);

            int Upsink = 0;
            for (int i = 1; i <= Level; i++)
            {
                Upsink += LibExperience.MPBonus[i];
            }

            if (row != null)
            {
                decimal RankMP = 0;
                RankMP = (int)((decimal)(row.rank_number_b + Upsink) * row.rank_number_a);
                return RankMP;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// キャラクターのレベルとランクから基本能力数値を取得します
        /// </summary>
        /// <param name="Rank">基本能力のランク</param>
        /// <param name="Level">レベル</param>
        /// <returns></returns>
        public static decimal GetRankToSTR(int Rank, int Level)
        {
            RankDataEntity.mt_rank_listRow row = Entity.mt_rank_list.FindByrank_typerank_level(Status.Rank.STR, Rank);

            if (row != null)
            {
                decimal RankMPTP = 0;
                RankMPTP = row.rank_number_b + (int)(row.rank_number_a * (decimal)Level / 128m);
                return RankMPTP;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// キャラクターのレベルとランクから基本能力数値を取得します
        /// </summary>
        /// <param name="Rank">基本能力のランク</param>
        /// <param name="Level">レベル</param>
        /// <returns></returns>
        public static decimal GetRankToMAG(int Rank, int Level)
        {
            RankDataEntity.mt_rank_listRow row = Entity.mt_rank_list.FindByrank_typerank_level(Status.Rank.MAG, Rank);

            if (row != null)
            {
                decimal RankMPTP = 0;
                RankMPTP = row.rank_number_b + (int)((decimal)row.rank_number_a * (decimal)Level / 128m);
                return RankMPTP;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// キャラクターのレベルとランクから基本能力数値を取得します
        /// </summary>
        /// <param name="Rank">基本能力のランク</param>
        /// <param name="Level">レベル</param>
        /// <returns></returns>
        public static decimal GetRankToVIT(int Rank, int Level)
        {
            RankDataEntity.mt_rank_listRow row = Entity.mt_rank_list.FindByrank_typerank_level(Status.Rank.VIT, Rank);

            if (row != null)
            {
                decimal RankMPTP = 0;
                RankMPTP = row.rank_number_b + (int)((decimal)row.rank_number_a * (decimal)Level / 128m);
                return RankMPTP;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// キャラクターのレベルとランクから基本能力数値を取得します
        /// </summary>
        /// <param name="Rank">基本能力のランク</param>
        /// <param name="Level">レベル</param>
        /// <returns></returns>
        public static decimal GetRankToSPD(int Rank, int Level)
        {
            RankDataEntity.mt_rank_listRow row = Entity.mt_rank_list.FindByrank_typerank_level(Status.Rank.SPD, Rank);

            if (row != null)
            {
                decimal RankMPTP = 0;
                RankMPTP = row.rank_number_b + (int)((decimal)row.rank_number_a * (decimal)Level / 128m);
                return RankMPTP;
            }
            else
            {
                return 0;
            }
        }


    }
}
