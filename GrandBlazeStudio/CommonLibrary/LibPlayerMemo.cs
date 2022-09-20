using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;
using System.Data;

namespace CommonLibrary
{
    public static class LibPlayerMemo
    {
        public static PlayerMemoEntity Entity;

        static LibPlayerMemo()
        {
            LoadPlayerMemo();
        }

        static public void LoadPlayerMemo()
        {
            Entity = new PlayerMemoEntity();
            using (LibDBLocal dba = new LibDBLocal())
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <temp_character_system_message>
                SelSql.AppendLine("SELECT [entry_no]");
                SelSql.AppendLine("      ,[memo_type]");
                SelSql.AppendLine("      ,[memo_id]");
                SelSql.AppendLine("      ,[memo]");
                SelSql.AppendLine("      ,[memo_level]");
                SelSql.AppendLine("  FROM [temp_character_system_message]");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.temp_character_system_message);
            }
        }

        /// <summary>
        /// システムメッセージを追記する
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <param name="MemoType">メモ種別</param>
        /// <param name="Memo">メモ内容</param>
        /// <param name="Level">メッセージレベル</param>
        public static void AddSystemMessage(int EntryNo, CommonLibrary.Status.PlayerSysMemoType MemoType, string Memo, int Level)
        {
            if (Memo.Length == 0) { return; }

            PlayerMemoEntity.temp_character_system_messageRow SystemMessageRow = Entity.temp_character_system_message.Newtemp_character_system_messageRow();

            int NewMemoNo = LibInteger.GetNewUnderNum(Entity.temp_character_system_message, "memo_id");

            SystemMessageRow.entry_no = EntryNo;
            SystemMessageRow.memo_type = (int)MemoType;
            SystemMessageRow.memo_id = NewMemoNo;
            SystemMessageRow.memo = Memo;
            SystemMessageRow.memo_level = Level;

            Entity.temp_character_system_message.Addtemp_character_system_messageRow(SystemMessageRow);
        }

        /// <summary>
        /// システムメッセージのリスト
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>メッセージリスト</returns>
        public static DataTable MessageList(int EntryNo)
        {
            DataView MessageView = new DataView(Entity.temp_character_system_message);
            MessageView.RowFilter = "entry_no=" + EntryNo;
            MessageView.Sort="memo_type,memo_id";
            return MessageView.ToTable();
        }

        /// <summary>
        /// 登録
        /// </summary>
        static public void Update()
        {
            string UpSql;
            string InSql;
            string DelSql;
            string TableName = "";

            if (Entity.temp_character_system_message.GetChanges() == null)
            {
                return;
            }

            LibDBLocal DbAc = new LibDBLocal();
            try
            {
                DbAc.BeginTransaction();

                foreach (PlayerMemoEntity.temp_character_system_messageRow MesseageRow in Entity.temp_character_system_message.GetChanges().Rows)
                {
                    TableName = "temp_character_system_message";

                    if (MesseageRow.RowState == DataRowState.Added || MesseageRow.RowState == DataRowState.Modified)
                    {
                        UpSql = LibSql.MakeUpSql(TableName, "entry_no=" + MesseageRow.entry_no + " and memo_type=" + MesseageRow.memo_type + " and memo_id=" + MesseageRow.memo_id, MesseageRow);
                        InSql = LibSql.MakeInSql(TableName, MesseageRow);

                        if (DbAc.ExecuteNonQuery(UpSql) == 0)
                        {
                            DbAc.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (MesseageRow.RowState == DataRowState.Deleted)
                    {
                        DelSql = "DELETE FROM " + TableName + " WHERE entry_no=" + (int)MesseageRow["entry_no", DataRowVersion.Original] + " and memo_type=" + (int)MesseageRow["memo_type", DataRowVersion.Original] + " and memo_id=" + (int)MesseageRow["memo_id", DataRowVersion.Original];

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
                DbAc.Close();
            }

            LoadPlayerMemo();
        }
    }
}
