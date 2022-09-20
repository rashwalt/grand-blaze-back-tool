using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net;

namespace CommonLibrary.DataAccess
{
    /// <summary>
    /// Webサーバー用データベースアクセスのライブラリ
    /// </summary>
    public class LibDBWebServer
    {
        // MySQLに接続。ODBC経由
        private MySqlConnection DbCon;
        private MySqlDataAdapter DbAdap;
        private MySqlCommand DbCmd;
        private MySqlTransaction DbTran;

        public LibDBWebServer()
        {
            string Conn = @"Server=*.*.*.*;Database=grand_blaze;User=grbdb;Password=*****;Character Set=sjis;";
            string ConnTest = "Server=127.0.0.1;Database=grand_blaze;User ID=root;Password=password;Character Set=sjis;";
            if (LibCommonLibrarySettings.IsProduction)
            {
                DbCon = new MySqlConnection(Conn);
            }
            else
            {
                DbCon = new MySqlConnection(ConnTest);
            }
            DbAdap = new MySqlDataAdapter();
        }

        public void Open()
        {
            DbCon.Open();
        }

        public DataTable GetTableData(string Sql)
        {
            System.Data.DataTable dt = new DataTable();
            DbAdap.SelectCommand = new MySqlCommand(Sql, DbCon);
            DbAdap.Fill(dt);

            return dt;
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
            DbCmd.CommandText = sql;
            return DbCmd.ExecuteNonQuery();
        }

        public void Commit()
        {
            DbTran.Commit();
        }

        public void Close()
        {
            DbCon.Close();
        }
    }
}
