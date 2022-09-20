using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CommonLibrary.DataAccess
{
    /// <summary>
    /// ローカルデータベースアクセスのライブラリ
    /// </summary>
    public class LibDBLocal : IDisposable
    {
        private SqlConnection DbCon;
        private SqlDataAdapter DbAdap;
        private SqlCommand DbCmd;
        private SqlTransaction DbTran;

        public readonly string TranDataBaseName = "GrandBlazeData";
        public readonly string TestTranDataBaseName = "GrandBlazeTest";

        public readonly string MasterDataBaseName = "GrandBlazeMaster";

        public LibDBLocal()
        {
            Initialize(Status.DataBaseAccessTarget.Tran);
        }

        public LibDBLocal(int AccessTarget)
        {
            Initialize(AccessTarget);
        }

        private void Initialize(int AccessTarget)
        {
            string Conec = "";

            switch (AccessTarget)
            {
                case Status.DataBaseAccessTarget.Master:
                    if (CheckAttach(AccessTarget))
                    {
                        // アタッチ済
                        Conec = "Data Source=" + LibCommonLibrarySettings.DataBaseInstance + ";Initial Catalog=" + MasterDataBaseName + ";Integrated Security=True";
                    }
                    else
                    {
                        // アタッチ＆接続
                        Conec = "Server=" + LibCommonLibrarySettings.DataBaseInstance + "; AttachDbFilename=" + LibCommonLibrarySettings.DataBasePathMaster + "; Database=" + MasterDataBaseName + "; integrated security=true;";
                    }
                    break;
                case Status.DataBaseAccessTarget.SystemMaster:
                    {
                        Conec = "Data Source=" + LibCommonLibrarySettings.DataBaseInstance + ";Initial Catalog=master;Integrated Security=True";
                    }
                    break;
                default:
                    if (LibCommonLibrarySettings.IsProduction)
                    {
                        if (CheckAttach(AccessTarget))
                        {
                            // アタッチ済
                            Conec = "Data Source=" + LibCommonLibrarySettings.DataBaseInstance + ";Initial Catalog=" + TranDataBaseName + ";Integrated Security=True";
                        }
                        else
                        {
                            // アタッチ＆接続
                            Conec = "Server=" + LibCommonLibrarySettings.DataBaseInstance + "; AttachDbFilename=" + LibCommonLibrarySettings.DataBasePathMDF + "; Database=" + TranDataBaseName + "; integrated security=true;";
                        }
                    }
                    else
                    {
                        if (CheckAttach(AccessTarget))
                        {
                            // アタッチ済
                            Conec = "Data Source=" + LibCommonLibrarySettings.DataBaseInstance + ";Initial Catalog=" + TestTranDataBaseName + ";Integrated Security=True";
                        }
                        else
                        {
                            // アタッチ＆接続
                            Conec = "Server=" + LibCommonLibrarySettings.DataBaseInstance + "; AttachDbFilename=" + LibCommonLibrarySettings.TestDataBasePathMDF + "; Database=" + TestTranDataBaseName + "; integrated security=true;";
                        }
                    }
                    break;
            }

            DbCon = new SqlConnection(Conec);
            DbAdap = new SqlDataAdapter();

            try
            {
                DbCon.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public DataTable GetTableData(string Sql)
        {
            DataTable dt = new DataTable();
            DbAdap.SelectCommand = new SqlCommand(Sql, DbCon);
            if (DbTran != null)
            {
                DbAdap.SelectCommand.Transaction = DbTran;
            }
            DbAdap.Fill(dt);

            return dt;
        }

        public void Fill(string Sql, DataTable table)
        {
            try
            {
                DbAdap.SelectCommand = new SqlCommand(Sql, DbCon);
                if (DbTran != null)
                {
                    DbAdap.SelectCommand.Transaction = DbTran;
                }
                DbAdap.Fill(table);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void Close()
        {
            DbCon.Close();
            if (DbTran != null)
            {
                DbTran.Dispose();
            }
            DbCon.Dispose();
        }

        public void BeginTransaction()
        {
            DbCmd = DbCon.CreateCommand();
            DbTran = DbCon.BeginTransaction();

            DbCmd.Connection = DbCon;
            DbCmd.Transaction = DbTran;
        }

        public int ExecuteNonQuery(string sql)
        {
            try
            {
                DbCmd.CommandText = sql;
                return DbCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int ExecuteProcedure(string sql)
        {
            DbCmd = DbCon.CreateCommand();

            DbCmd.CommandText = sql;
            return DbCmd.ExecuteNonQuery();
        }

        public void Commit()
        {
            DbTran.Commit();
        }

        public void Rollback()
        {
            DbTran.Rollback();
        }

        public void Update(DataTable table)
        {
            Update(table, false);
        }

        public void Update(DataTable table, bool EscapeBr)
        {
            if (table.GetChanges() == null) { return; }

            string sql = "";

            string TableName = table.TableName;

            foreach (DataRow row in table.GetChanges().Rows)
            {
                StringBuilder KeyStrings = new StringBuilder();
                sql = "";

                foreach (DataColumn keyColumn in table.PrimaryKey)
                {
                    foreach (DataColumn Columns in table.Columns)
                    {
                        if (keyColumn.ColumnName == Columns.ColumnName)
                        {
                            if (KeyStrings.Length > 0) { KeyStrings.Append(" AND "); }

                            if (row.RowState == DataRowState.Deleted)
                            {
                                KeyStrings.Append(Columns.ColumnName + "=" + LibSql.EscapeWheres(row[Columns.ColumnName, DataRowVersion.Original]));
                            }
                            else
                            {
                                KeyStrings.Append(Columns.ColumnName + "=" + LibSql.EscapeWheres(row[Columns.ColumnName]));
                            }

                            continue;
                        }
                    }
                }

                bool IsDelete = false;
                switch (row.RowState)
                {
                    case DataRowState.Modified:
                        sql = LibSql.MakeUpSql(TableName, KeyStrings.ToString(), row, EscapeBr);
                        break;
                    case DataRowState.Added:
                        sql = LibSql.MakeInSql(TableName, row, EscapeBr);
                        break;
                    case DataRowState.Deleted:
                        sql = "DELETE FROM " + TableName + " WHERE " + KeyStrings.ToString();
                        IsDelete = true;
                        break;
                }

                if (sql.Length > 0)
                {
                    int rowcnt = ExecuteNonQuery(sql);
                    if (!IsDelete && rowcnt == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("影響件数が0件でした。");
                    }
                }
            }
        }

        public void UpdateByDeleteInsert(DataTable table, string where)
        {
            UpdateByDeleteInsert(table, where, false);
        }

        public void UpdateByDeleteInsert(DataTable table, string where, bool escape)
        {
            if (table.GetChanges() == null) { return; }

            string sql = "";

            string TableName = table.TableName;

            // DELETE
            sql = "DELETE FROM " + TableName + " WHERE " + where;
            ExecuteNonQuery(sql);

            foreach (DataRow row in table.Rows)
            {

                sql = LibSql.MakeInSql(TableName, row, escape);

                if (sql.Length > 0)
                {
                    ExecuteNonQuery(sql);
                }
            }
        }

        public void Dispose()
        {
            Close();
        }

        private bool CheckAttach(int AccessTarget)
        {
            bool isAttach = false;

            using (SqlConnection cn = new SqlConnection())
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cn.ConnectionString = @"Server=" + LibCommonLibrarySettings.DataBaseInstance + ";"
                                          + "Database=master;"
                                          + "integrated security=true";
                    cn.Open();
                    switch (AccessTarget)
                    {
                        case Status.DataBaseAccessTarget.Master:
                            cmd.CommandText =
                              "SELECT COUNT(*) FROM sysdatabases WHERE name='" + MasterDataBaseName + "'";
                            break;
                        default:
                            if (LibCommonLibrarySettings.IsProduction)
                            {
                                cmd.CommandText =
                                  "SELECT COUNT(*) FROM sysdatabases WHERE name='" + TranDataBaseName + "'";
                            }
                            else
                            {
                                cmd.CommandText =
                                  "SELECT COUNT(*) FROM sysdatabases WHERE name='" + TestTranDataBaseName + "'";
                            }
                            break;
                    }
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 1)
                    {
                        isAttach = true;
                    }
                    cn.Close();
                }
            }

            return isAttach;
        }
    }
}
