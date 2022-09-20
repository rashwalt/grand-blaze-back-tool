using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// アイテム関連を扱うクラス
    /// </summary>
    public static class LibItem
    {
        public static CommonItemEntity Entity;

        static LibItem()
        {
            LoadItem();
        }

        /// <summary>
        /// データ読込
        /// </summary>
        static public void LoadItem()
        {
            Entity = new CommonItemEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <item_list>
                SelSql.AppendLine("SELECT [it_num]");
                SelSql.AppendLine("      ,[it_name]");
                SelSql.AppendLine("      ,[it_physics]");
                SelSql.AppendLine("      ,[it_sorcery]");
                SelSql.AppendLine("      ,[it_physics_parry]");
                SelSql.AppendLine("      ,[it_sorcery_parry]");
                SelSql.AppendLine("      ,[it_critical]");
                SelSql.AppendLine("      ,[it_metal]");
                SelSql.AppendLine("      ,[it_charge]");
                SelSql.AppendLine("      ,[it_range]");
                SelSql.AppendLine("      ,[it_type]");
                SelSql.AppendLine("      ,[it_sub_category]");
                SelSql.AppendLine("      ,[it_attack_type]");
                SelSql.AppendLine("      ,[it_comment]");
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
                SelSql.AppendLine("      ,[it_effect]");
                SelSql.AppendLine("      ,[it_ok_sex]");
                SelSql.AppendLine("      ,[it_ok_race]");
                SelSql.AppendLine("      ,[it_both_hand]");
                SelSql.AppendLine("      ,[it_use_item]");
                SelSql.AppendLine("      ,[it_equip_install]");
                SelSql.AppendLine("      ,[it_equip_parts]");
                SelSql.AppendLine("      ,[it_rare]");
                SelSql.AppendLine("      ,[it_bind]");
                SelSql.AppendLine("      ,[it_quest]");
                SelSql.AppendLine("      ,[it_shop]");
                SelSql.AppendLine("      ,[it_equip_level]");
                SelSql.AppendLine("      ,[it_target_area]");
                SelSql.AppendLine("      ,[it_price]");
                SelSql.AppendLine("      ,[it_seller]");
                SelSql.AppendLine("      ,convert(bit, 0) as it_creatable");
                SelSql.AppendLine("      ,[it_stack]");
                SelSql.AppendLine("      ,'' as it_base_item");
                SelSql.AppendLine("      ,[use_script]");
                SelSql.AppendLine("      ,[use_battle]");
                SelSql.AppendLine("  FROM [mt_item_list]");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.item_list);
            }
        }

        /// <summary>
        /// アイテム名称取得
        /// </summary>
        /// <param name="ItemID">取得対象アイテムNo</param>
        /// <param name="IsCreated">作成したアイテムか</param>
        /// <returns>アイテム名</returns>
        static public string GetItemName(int ItemID, bool IsCreated)
        {
            CommonItemEntity.item_listRow row = Entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (row != null)
            {
                return row.it_name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// アイテム種別取得
        /// </summary>
        /// <param name="ItemID">取得対象アイテムNo</param>
        /// <param name="IsCreated">作成したアイテムか</param>
        /// <returns>種別ID</returns>
        static public int GetType(int ItemID, bool IsCreated)
        {
            CommonItemEntity.item_listRow row = Entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (row != null)
            {
                return row.it_type;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// アイテムサブ種別取得
        /// </summary>
        /// <param name="ItemID">取得対象アイテムNo</param>
        /// <param name="IsCreated">作成したアイテムか</param>
        /// <returns>サブカテゴリID</returns>
        static public int GetSubType(int ItemID, bool IsCreated)
        {
            CommonItemEntity.item_listRow row = Entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (row != null)
            {
                return row.it_sub_category;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// アイテム価格取得
        /// </summary>
        /// <param name="ItemID">取得対象アイテムNo</param>
        /// <param name="IsCreated">作成したアイテムか</param>
        /// <returns>アイテム価格</returns>
        static public decimal GetItemPrice(int ItemID, bool IsCreated)
        {
            CommonItemEntity.item_listRow row = Entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (row != null)
            {
                return row.it_price;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 指定されたアイテムのDataRowを取得
        /// </summary>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成したアイテムか</param>
        /// <returns>選択された アイテムのDataRow</returns>
        static public CommonItemEntity.item_listRow GetItemRow(int ItemID, bool IsCreated)
        {
            return Entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);
        }

        /// <summary>
        /// 指定されたアイテムのエフェクトを取得
        /// </summary>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成したアイテムか</param>
        /// <returns>エフェクト</returns>
        static public EffectListEntity.effect_listDataTable GetEffectTable(int ItemID, bool IsCreated)
        {
            CommonItemEntity.item_listRow row = Entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (row == null)
            {
                return null;
            }

            EffectListEntity.effect_listDataTable EffectTable = new EffectListEntity.effect_listDataTable();
            LibEffect.Split(row.it_effect, ref EffectTable);

            return EffectTable;
        }

        /// <summary>
        /// アイテムの存在確認
        /// </summary>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成したアイテムか</param>
        /// <returns>ステータス</returns>
        static public bool CheckItemID(int ItemID, bool IsCreated)
        {
            CommonItemEntity.item_listRow row = Entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (row == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// バインドタイプ取得
        /// </summary>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <returns>バインドタイプ</returns>
        static public bool GetBindType(int ItemID, bool IsCreated)
        {
            CommonItemEntity.item_listRow row = Entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (row != null)
            {
                return row.it_bind;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// レア属性か
        /// </summary>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <returns>レア属性</returns>
        static public bool CheckRare(int ItemID, bool IsCreated)
        {
            CommonItemEntity.item_listRow row = Entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (row != null)
            {
                return row.it_rare;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// スタック数取得
        /// </summary>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <returns>スタック数</returns>
        static public int GetStackCount(int ItemID, bool IsCreated)
        {
            CommonItemEntity.item_listRow row = Entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (row != null)
            {
                return row.it_stack;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// アイテム完全削除
        /// </summary>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        static public void Delete(int ItemID, bool IsCreated)
        {
            CommonItemEntity.item_listRow row = Entity.item_list.FindByit_numit_creatable(ItemID, IsCreated);

            if (row != null)
            {
                row.Delete();
            }
        }

        /// <summary>
        /// アイテム登録
        /// </summary>
        static public void Update()
        {
            Update(false);
        }

        /// <summary>
        /// アイテム登録
        /// </summary>
        static public void Update(bool EscapeBr)
        {
            if (Entity.item_list.GetChanges() == null) { return; }

            string UpSql;
            string InSql;
            string DelSql;
            string TableName = "";

            LibDBLocal DbAc = new LibDBLocal(Status.DataBaseAccessTarget.Tran);
            try
            {
                DbAc.BeginTransaction();

                foreach (DataRow ItemRow in Entity.item_list.GetChanges().Rows)
                {
                    TableName = "[GrandBlazeMaster].[dbo].[mt_item_list]";

                    if (ItemRow.RowState == DataRowState.Deleted)
                    {
                        if ((bool)ItemRow["it_creatable", DataRowVersion.Original])
                        {
                            TableName = "[GrandBlazeData].[dbo].[ts_created_item_list]";
                        }
                    }
                    else
                    {
                        if ((bool)ItemRow["it_creatable"])
                        {
                            TableName = "[GrandBlazeData].[dbo].[ts_created_item_list]";
                        }
                        else
                        {
                            ItemRow["it_base_item"] = "0";
                        }
                    }

                    if (ItemRow.RowState == DataRowState.Added || ItemRow.RowState == DataRowState.Modified)
                    {
                        List<string> OutString = new List<string> { "it_creatable" };
                        if (TableName == "[GrandBlazeData].[dbo].[ts_created_item_list]") { OutString.Add("it_shop"); OutString.Add("use_script"); }
                        else { OutString.Add("it_base_item"); }

                        UpSql = LibSql.MakeUpSql(TableName, "it_num=" + (int)ItemRow["it_num"], ItemRow, EscapeBr, OutString);
                        InSql = LibSql.MakeInSql(TableName, ItemRow, EscapeBr, OutString);

                        if (DbAc.ExecuteNonQuery(UpSql) == 0)
                        {
                            DbAc.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (ItemRow.RowState == DataRowState.Deleted)
                    {
                        DelSql = "DELETE FROM " + TableName + " WHERE it_num=" + (int)ItemRow["it_num", DataRowVersion.Original];

                        DbAc.ExecuteNonQuery(DelSql);
                    }
                }
                DbAc.Commit();
            }
            catch
            {
                DbAc.Rollback();
            }
            finally
            {
                DbAc.Dispose();
            }

            LoadItem();
        }
    }
}
