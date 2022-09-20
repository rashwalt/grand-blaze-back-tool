using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// 貴重品管理クラス
    /// </summary>
    public static class LibKeyItem
    {
        public static KeyItemEntity Entity;

        static LibKeyItem()
        {
            LoadKeyItem();
        }

        public static void LoadKeyItem()
        {
            Entity = new KeyItemEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_key_item_list>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("key_id,");
                SelSql.AppendLine("keyitem_name,");
                SelSql.AppendLine("keyitem_comment,");
                SelSql.AppendLine("key_type");
                SelSql.AppendLine("FROM mt_key_item_list");
                SelSql.AppendLine("ORDER BY key_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_key_item_list);
            }
        }

        /// <summary>
        /// 貴重品名称取得
        /// </summary>
        /// <param name="KeyItemID">取得対象貴重品No</param>
        /// <returns>貴重品名</returns>
        public static string GetKeyItemName(int KeyItemID)
        {
            KeyItemEntity.mt_key_item_listRow row = Entity.mt_key_item_list.FindBykey_id(KeyItemID);

            if (row != null)
            {
                return row.keyitem_name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 貴重品DataRow取得
        /// </summary>
        /// <param name="KeyItemID">貴重品No</param>
        /// <returns>貴重品DataRow</returns>
        public static KeyItemEntity.mt_key_item_listRow GetKeyItemRow(int KeyItemID)
        {
            return Entity.mt_key_item_list.FindBykey_id(KeyItemID);
        }
    }
}
