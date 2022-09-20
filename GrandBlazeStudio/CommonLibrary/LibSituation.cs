using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// シチュエーション管理クラス
    /// </summary>
    public static class LibSituation
    {
        public static SituationDataEntity Entity;

        static LibSituation()
        {
            Entity = new SituationDataEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_situation_list>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("situation_no,");
                SelSql.AppendLine("situation_text,");
                SelSql.AppendLine("situation_comment");
                SelSql.AppendLine("FROM mt_situation_list");
                SelSql.AppendLine("ORDER BY situation_no");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_situation_list);
            }
        }

        /// <summary>
        /// 名称から番号取得
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static int GetNo(string Name)
        {
            Entity.mt_situation_list.DefaultView.RowFilter = "situation_text = " + LibSql.EscapeString(Name);

            if (Entity.mt_situation_list.DefaultView.Count > 0)
            {
                return (int)Entity.mt_situation_list.DefaultView[0]["situation_no"];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// シチュエーション名称取得
        /// </summary>
        /// <param name="SituationNo">番号</param>
        /// <returns>名称</returns>
        public static string GetName(int SituationNo)
        {
            SituationDataEntity.situation_listRow row = Entity.mt_situation_list.FindBysituation_no(SituationNo);

            if (row != null)
            {
                return row.situation_text;
            }
            else
            {
                return "";
            }
        }
    }
}
