using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    public static class LibSelledTreasure
    {
        public static SelledTreasureEntity Entity;

        static LibSelledTreasure()
        {
            LoadItem();
        }

        /// <summary>
        /// データ読込
        /// </summary>
        static public void LoadItem()
        {
            Entity = new SelledTreasureEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Tran))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <ts_selled_treasure>
                SelSql.AppendLine("SELECT [it_num]");
                SelSql.AppendLine("      ,[count]");
                SelSql.AppendLine("  FROM [ts_selled_treasure]");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_selled_treasure);
            }
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="ItemID">売却物アイテムID</param>
        /// <param name="Count">売却個数</param>
        /// <returns>アイテム名</returns>
        static public void AddCount(int ItemID, int Count)
        {
            SelledTreasureEntity.ts_selled_treasureRow row = Entity.ts_selled_treasure.FindByit_num(ItemID);

            if (row != null)
            {
                row.count += Count;
            }
            else
            {
                SelledTreasureEntity.ts_selled_treasureRow newRow = Entity.ts_selled_treasure.Newts_selled_treasureRow();
                newRow.it_num = ItemID;
                newRow.count = Count;
                Entity.ts_selled_treasure.Addts_selled_treasureRow(newRow);
            }
        }

        static public void Update()
        {
            LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Tran);

            try
            {
                dba.BeginTransaction();
                dba.Update(Entity.ts_selled_treasure);
                dba.Commit();
            }
            catch
            {
                dba.Rollback();
            }
            finally
            {
                dba.Dispose();
            }

            LoadItem();
        }
    }
}
