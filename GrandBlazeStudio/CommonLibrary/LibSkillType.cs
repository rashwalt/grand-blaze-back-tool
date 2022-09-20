using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// スキル種別
    /// </summary>
    public static class LibSkillType
    {
        public static SkillTypeEntity Entity;

        static LibSkillType()
        {
            Entity = new SkillTypeEntity();

            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_skill_category>
                SelSql.AppendLine("SELECT [category_id]");
                SelSql.AppendLine("      ,[category_name]");
                SelSql.AppendLine("      ,[type_id]");
                SelSql.AppendLine("  FROM [mt_skill_category]");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_skill_category);
            }
        }

        /// <summary>
        /// 名称でID取得
        /// </summary>
        /// <param name="CategoryName">名称</param>
        /// <returns>ID</returns>
        public static int FindByName(string CategoryName)
        {
            SkillTypeEntity.mt_skill_categoryRow[] SkillCateRow = (SkillTypeEntity.mt_skill_categoryRow[])Entity.mt_skill_category.Select(Entity.mt_skill_category.category_nameColumn.ColumnName + "=" + LibSql.EscapeString(CategoryName));

            if (SkillCateRow.Length != 1) { throw new Exception("不正なカテゴリ名称が指定されました"); }

            return SkillCateRow[0].category_id;
        }

        /// <summary>
        /// 名称取得
        /// </summary>
        /// <param name="CategoryID">カテゴリID</param>
        /// <returns>名称</returns>
        public static string GetName(int CategoryID)
        {
            SkillTypeEntity.mt_skill_categoryRow CategoryRow = Entity.mt_skill_category.FindBycategory_id(CategoryID);

            if (CategoryRow != null)
            {
                return CategoryRow.category_name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// アイテム種別ID取得
        /// </summary>
        /// <param name="CategoryID">カテゴリID</param>
        /// <returns>種別ID</returns>
        public static int GetTypeID(int CategoryID)
        {
            SkillTypeEntity.mt_skill_categoryRow CategoryRow = Entity.mt_skill_category.FindBycategory_id(CategoryID);

            if (CategoryRow != null)
            {
                return CategoryRow.type_id;
            }
            else
            {
                return 0;
            }
        }
    }
}
