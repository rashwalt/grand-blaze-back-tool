using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// フィールド管理クラス
    /// </summary>
    public static class LibField
    {
        public static FieldDataEntity Entity;

        static LibField()
        {
            DataLoad();
        }

        /// <summary>
        /// データロード
        /// </summary>
        public static void DataLoad()
        {
            Entity = new FieldDataEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <mt_field_type_list>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("field_id, ");
                Sql.AppendLine("field_name, ");
                Sql.AppendLine("not_attack, ");
                Sql.AppendLine("not_magic, ");
                Sql.AppendLine("auto_hpsrip, ");
                Sql.AppendLine("auto_mpsrip, ");
                Sql.AppendLine("auto_tpsrip, ");
                Sql.AppendLine("magnet, ");
                Sql.AppendLine("fire, ");
                Sql.AppendLine("freeze, ");
                Sql.AppendLine("air, ");
                Sql.AppendLine("earth, ");
                Sql.AppendLine("water, ");
                Sql.AppendLine("thunder, ");
                Sql.AppendLine("holy, ");
                Sql.AppendLine("dark ");
                Sql.AppendLine("FROM mt_field_type_list");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_field_type_list);
            }
        }

        /// <summary>
        /// フィールドタイプの名称取得
        /// </summary>
        /// <param name="FieldID">フィールドID</param>
        /// <returns>名称</returns>
        public static string GetName(int FieldID)
        {
            FieldDataEntity.mt_field_type_listRow row = Entity.mt_field_type_list.FindByfield_id(FieldID);

            if (row != null)
            {
                return row.field_name;
            }
            else
            {
                return "不明";
            }
        }

        /// <summary>
        /// フィールドのDataRow取得
        /// </summary>
        /// <param name="FieldID">フィールドID</param>
        /// <returns>DataRow</returns>
        public static FieldDataEntity.mt_field_type_listRow GetRow(int FieldID)
        {
            return Entity.mt_field_type_list.FindByfield_id(FieldID);
        }
    }
}
