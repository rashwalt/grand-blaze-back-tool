using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Format;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// エリア情報管理クラス
    /// </summary>
    public static class LibArea
    {
        public static AreaMarkEntity Entity;

        static LibArea()
        {
            LoadArea();
        }

        public static void LoadArea()
        {
            Entity = new AreaMarkEntity();

            // 国名設定
            Entity.mt_nation_list.Addnation_listRow(1, "ファーネルド連邦");
            Entity.mt_nation_list.Addnation_listRow(2, "ワルド帝国");
            Entity.mt_nation_list.Addnation_listRow(3, "ネルヴァリア王国");
            Entity.mt_nation_list.Addnation_listRow(4, "ダナクス諸侯連合");
            Entity.mt_nation_list.AcceptChanges();

            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <mt_area_list>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("* ");
                Sql.AppendLine("FROM mt_area_list");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_area_list);
            }
        }

        /// <summary>
        /// エリア名称を取得
        /// </summary>
        /// <param name="AreaID">エリアID</param>
        /// <returns>エリア名称</returns>
        public static string GetAreaName(int AreaID)
        {
            AreaMarkEntity.mt_area_listRow row = Entity.mt_area_list.FindByarea_id(AreaID);

            if (row != null)
            {
                return row.area_name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 国名取得
        /// </summary>
        /// <param name="NationID">国名ID</param>
        /// <returns>国名</returns>
        public static string GetNationName(int NationID)
        {
            AreaMarkEntity.nation_listRow row = Entity.mt_nation_list.FindBynation_id(NationID);

            if (row != null)
            {
                return row.nation_name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// エリアの所属国を取得
        /// </summary>
        /// <param name="AreaID">エリアID</param>
        /// <returns>エリア名称</returns>
        public static int GetNationID(int AreaID)
        {
            AreaMarkEntity.mt_area_listRow row = Entity.mt_area_list.FindByarea_id(AreaID);

            if (row != null)
            {
                return row.nation_id;
            }
            else
            {
                return -1;
            }
        }
    }
}
