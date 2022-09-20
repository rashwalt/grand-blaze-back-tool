using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    public static class LibStatusList
    {
        public static StatusListEntity Entity;

        static LibStatusList()
        {
            LoadStatusList();
        }

        public static void LoadStatusList()
        {
            Entity = new StatusListEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_status_list>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("status_id,");
                SelSql.AppendLine("status_name,");
                SelSql.AppendLine("status_text,");
                SelSql.AppendLine("status_dispel,");
                SelSql.AppendLine("status_clearnce,");
                SelSql.AppendLine("status_icon,");
                SelSql.AppendLine("status_good");
                SelSql.AppendLine("FROM mt_status_list");
                SelSql.AppendLine("ORDER BY status_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_status_list);
            }
        }

        /// <summary>
        /// ステータスの名称取得
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>ステータスの名称</returns>
        public static string GetName(int StatusID)
        {
            StatusListEntity.status_listRow row = Entity.mt_status_list.FindBystatus_id(StatusID);

            if (row == null)
            {
                return "";
            }

            return row.status_name;
        }

        /// <summary>
        /// ステータスのアイコン取得
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <param name="Rank">ランク</param>
        /// <param name="SubRank">サブランク</param>
        /// <returns>アイコン</returns>
        public static string GetIcon(int StatusID, int Rank, int SubRank)
        {
            StatusListEntity.status_listRow row = Entity.mt_status_list.FindBystatus_id(StatusID);

            if (row == null)
            {
                return "";
            }

            string IconText = row.status_icon;

            IconText = IconText.Replace("[status]", GetName(Rank));
            if (IconText.IndexOf("[elemental]") >= 0)
            {
                IconText = IconText.Replace("[elemental]", LibConst.ElementalListJpn[Rank - 1]);
            }

            return IconText;
        }

        /// <summary>
        /// ステータスがグッドステータスか
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>真ならグッドステータス</returns>
        public static bool CheckGoodStatus(int StatusID)
        {
            StatusListEntity.status_listRow row = Entity.mt_status_list.FindBystatus_id(StatusID);

            if (row == null)
            {
                return false;
            }

            return row.status_good;
        }

        /// <summary>
        /// ステータスがディスペル可能か
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>真ならグッドステータス</returns>
        public static bool CheckDispel(int StatusID)
        {
            StatusListEntity.status_listRow row = Entity.mt_status_list.FindBystatus_id(StatusID);

            if (row == null)
            {
                return false;
            }

            return row.status_dispel;
        }

        /// <summary>
        /// ステータスがクリアランス可能か
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>真ならグッドステータス</returns>
        public static bool CheckClearrance(int StatusID)
        {
            StatusListEntity.status_listRow row = Entity.mt_status_list.FindBystatus_id(StatusID);

            if (row == null)
            {
                return false;
            }

            return row.status_clearnce;
        }
    }
}
