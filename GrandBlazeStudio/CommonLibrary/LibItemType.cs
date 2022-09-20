using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    public static class LibItemType
    {
        public static ItemTypeEntity Entity;

        static LibItemType()
        {
            Entity = new ItemTypeEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <mt_item_type>
                SelSql.AppendLine("SELECT [type_id]");
                SelSql.AppendLine("      ,[type]");
                SelSql.AppendLine("      ,[equip_spot]");
                SelSql.AppendLine("      ,[use_item]");
                SelSql.AppendLine("      ,[both_hand]");
                SelSql.AppendLine("      ,[target_area]");
                SelSql.AppendLine("      ,[categ_div]");
                SelSql.AppendLine("      ,[stack_count]");
                SelSql.AppendLine("      ,[bad_weather_hit]");
                SelSql.AppendLine("      ,[check_guard]");
                SelSql.AppendLine("      ,[check_parry]");
                SelSql.AppendLine("      ,[check_dodge]");
                SelSql.AppendLine("      ,[atk_ability1]");
                SelSql.AppendLine("      ,[atk_ability2]");
                SelSql.AppendLine("      ,[dfe_bt_type]");
                SelSql.AppendLine("      ,[rating]");
                SelSql.AppendLine("      ,[plus_score]");
                SelSql.AppendLine("      ,[random_damage]");
                SelSql.AppendLine("      ,[material_type]");
                SelSql.AppendLine("      ,[skill_id]");
                SelSql.AppendLine("      ,[anti_air]");
                SelSql.AppendLine("      ,[dfe_dis]");
                SelSql.AppendLine("      ,[database_view]");
                SelSql.AppendLine("      ,[range_weapon]");
                SelSql.AppendLine("  FROM [mt_item_type]");
                #endregion
                
                dba.Fill(SelSql.ToString(), Entity.mt_item_type);

                SelSql = new StringBuilder();
                #region TABLE <mt_item_type_sub_category>
                SelSql.AppendLine("SELECT [category_id]");
                SelSql.AppendLine("      ,[type_id]");
                SelSql.AppendLine("      ,[sub_category]");
                SelSql.AppendLine("      ,[discharge]");
                SelSql.AppendLine("      ,[range_id]");
                SelSql.AppendLine("      ,[it_avoid]");
                SelSql.AppendLine("      ,[it_fire]");
                SelSql.AppendLine("      ,[it_freeze]");
                SelSql.AppendLine("      ,[it_air]");
                SelSql.AppendLine("      ,[it_earth]");
                SelSql.AppendLine("      ,[it_water]");
                SelSql.AppendLine("      ,[it_thunder]");
                SelSql.AppendLine("      ,[it_holy]");
                SelSql.AppendLine("      ,[it_dark]");
                SelSql.AppendLine("      ,[it_slash]");
                SelSql.AppendLine("      ,[it_pierce]");
                SelSql.AppendLine("      ,[it_strike]");
                SelSql.AppendLine("      ,[it_break]");
                SelSql.AppendLine("      ,[it_attack_type]");
                SelSql.AppendLine("      ,[charge]");
                SelSql.AppendLine("      ,[range]");
                SelSql.AppendLine("      ,[effects]");
                SelSql.AppendLine("      ,[fd_effects]");
                SelSql.AppendLine("      ,[cad_effects]");
                SelSql.AppendLine("      ,[it_critical]");
                SelSql.AppendLine("      ,[critical_type]");
                SelSql.AppendLine("      ,[create_skill_id]");
                SelSql.AppendLine("  FROM [mt_item_type_sub_category]");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.mt_item_type_sub_category);
            }
        }

        public static int GetCharge(int SubCategoryID)
        {
            ItemTypeEntity.mt_item_type_sub_categoryRow row = Entity.mt_item_type_sub_category.FindBycategory_id(SubCategoryID);
            if (row == null) { return 0; }

            return row.charge;
        }

        /// <summary>
        /// 種別DataRow取得
        /// </summary>
        /// <param name="TypeID">種別ID</param>
        /// <returns>種別DataRow</returns>
        public static ItemTypeEntity.mt_item_typeRow GetTypeRow(int TypeID)
        {
            return Entity.mt_item_type.FindBytype_id(TypeID);
        }

        /// <summary>
        /// サブ種別DataRow取得
        /// </summary>
        /// <param name="SubCategoryID">サブ種別ID</param>
        /// <returns>サブ種別DataRow</returns>
        public static ItemTypeEntity.mt_item_type_sub_categoryRow GetSubCategoryRow(int SubCategoryID)
        {
            return Entity.mt_item_type_sub_category.FindBycategory_id(SubCategoryID);
        }

        /// <summary>
        /// 種別番号取得
        /// </summary>
        /// <param name="TypeName">種別名</param>
        /// <returns>番号</returns>
        public static int GetTypeNo(string TypeName)
        {
            Entity.mt_item_type.DefaultView.RowFilter = Entity.mt_item_type.typeColumn.ColumnName + "=" + LibSql.EscapeString(TypeName);

            if (Entity.mt_item_type.DefaultView.Count > 0)
            {
                return (int)Entity.mt_item_type.DefaultView[0][Entity.mt_item_type.type_idColumn.ColumnName];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 種別番号取得
        /// </summary>
        /// <param name="SubCategoryID">サブ種別ID</param>
        /// <returns>番号</returns>
        public static int GetSub2TypeNo(int SubCategoryID)
        {
            ItemTypeEntity.mt_item_type_sub_categoryRow row = Entity.mt_item_type_sub_category.FindBycategory_id(SubCategoryID);

            if (row != null)
            {
                return row.type_id;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// アイテムの種別取得
        /// </summary>
        /// <param name="TypeID">タイプID</param>
        /// <returns>種別名</returns>
        public static string GetTypeName(int TypeID)
        {
            ItemTypeEntity.mt_item_typeRow row = Entity.mt_item_type.FindBytype_id(TypeID);

            if (row != null)
            {
                return row.type;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// アイテムのマテリアル種別取得
        /// </summary>
        /// <param name="TypeID">タイプID</param>
        /// <returns>マテリアル種別</returns>
        public static int GetMaterialType(int TypeID)
        {
            ItemTypeEntity.mt_item_typeRow row = Entity.mt_item_type.FindBytype_id(TypeID);

            if (row != null)
            {
                return row.material_type;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 対空効果取得
        /// </summary>
        /// <param name="TypeID">タイプID</param>
        /// <returns>対空効果</returns>
        public static bool GetAntiAir(int TypeID)
        {
            ItemTypeEntity.mt_item_typeRow row = Entity.mt_item_type.FindBytype_id(TypeID);

            if (row != null)
            {
                return row.anti_air;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 合成スキルID取得
        /// </summary>
        /// <param name="SubCategoryID">サブ種別ID</param>
        /// <returns>スキルID</returns>
        public static int GetCreateSkillID(int SubCategoryID)
        {
            ItemTypeEntity.mt_item_type_sub_categoryRow row = Entity.mt_item_type_sub_category.FindBycategory_id(SubCategoryID);

            if (row != null && row.create_skill_id > 0)
            {
                return row.create_skill_id;
            }
            else
            {
                throw new Exception("合成スキルIDが設定されていない！？");
            }
        }

        /// <summary>
        /// テクニックIDから種類ID
        /// </summary>
        /// <param name="TecID">テクニックID</param>
        /// <returns>種類ID</returns>
        public static int GetTypeIDByTecID(int TecID)
        {
            Entity.mt_item_type.DefaultView.RowFilter = Entity.mt_item_type.skill_idColumn.ColumnName + "=" + TecID;

            if (Entity.mt_item_type.DefaultView.Count > 0)
            {
                return (int)Entity.mt_item_type.DefaultView[0][Entity.mt_item_type.type_idColumn.ColumnName];
            }
            else
            {
                return 0;
            }
        }
    }
}
