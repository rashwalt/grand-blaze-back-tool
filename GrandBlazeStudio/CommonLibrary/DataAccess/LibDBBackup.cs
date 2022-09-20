using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.DataAccess
{
    public static class LibDBBackup
    {
        /// <summary>
        /// バックアップの実行
        /// </summary>
        /// <param name="backNo">バックアップID</param>
        public static void Done(int backNo, int UpdateCount)
        {
            LibDBLocal DbAc = new LibDBLocal(Status.DataBaseAccessTarget.SystemMaster);
            try
            {
                StringBuilder sql = new StringBuilder();

                if (LibCommonLibrarySettings.IsProduction)
                {
                    sql.Append("backup database " + DbAc.TranDataBaseName + " to disk='" + LibCommonLibrarySettings.DataBaseBackupPath + "gbd_" + backNo + ".bak' with init, NAME='第" + UpdateCount + "回 No-" + backNo + "'");
                }
                else
                {
                    sql.Append("backup database " + DbAc.TestTranDataBaseName + " to disk='" + LibCommonLibrarySettings.DataBaseBackupPath + "gbd_" + backNo + ".bak' with init, NAME='第" + UpdateCount + "回 No-" + backNo + "'");
                }
                DbAc.ExecuteProcedure(sql.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DbAc.Dispose();
            }
        }

        /// <summary>
        /// デタッチ
        /// </summary>
        public static void Detouch()
        {
            LibDBLocal DbAc = new LibDBLocal(Status.DataBaseAccessTarget.SystemMaster);
            try
            {
                StringBuilder sql = new StringBuilder();

                if (LibCommonLibrarySettings.IsProduction)
                {
                    sql.Append("sp_detach_db '" + DbAc.TranDataBaseName + "'");
                }
                else
                {
                    sql.Append("sp_detach_db '" + DbAc.TestTranDataBaseName + "'");
                }
                DbAc.ExecuteProcedure(sql.ToString());
            }
            catch
            {
            }
            finally
            {
                DbAc.Dispose();
            }
        }
    }
}
