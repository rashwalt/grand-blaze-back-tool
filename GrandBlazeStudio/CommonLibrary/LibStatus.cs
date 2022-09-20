using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// ステータス情報管理テーブル
    /// </summary>
    public class LibStatus
    {
        private CharacterStatusListEntity.status_dataDataTable StatusTable = new CharacterStatusListEntity.status_dataDataTable();

        /// <summary>
        /// 初期ステータス読込
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        public void LoadDefaultStatus(int EntryNo)
        {
            using (LibDBLocal dba = new LibDBLocal())
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <ts_character_using_status>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no,");
                SelSql.AppendLine("status_id,");
                SelSql.AppendLine("rank,");
                SelSql.AppendLine("sub_rank,");
                SelSql.AppendLine("end_limit,");
                SelSql.AppendLine("[level],");
                SelSql.AppendLine("[view]");
                SelSql.AppendLine("FROM ts_character_using_status");
                SelSql.AppendLine("WHERE");
                SelSql.AppendLine("entry_no=" + EntryNo);
                #endregion

                dba.Fill(SelSql.ToString(), StatusTable);
            }
        }

        /// <summary>
        /// バッドステータス数
        /// </summary>
        public int GetBadCount
        {
            get
            {
                int count = 0;

                foreach (DataRow StatusRow in StatusTable.Rows)
                {
                    if (StatusRow.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }

                    if ((bool)StatusRow["view"])
                    {
                        if (LibStatusList.GetName((int)StatusRow["status_id"]) != "" && !LibStatusList.CheckGoodStatus((int)StatusRow["status_id"]))
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
        }

        /// <summary>
        /// コピー！
        /// </summary>
        /// <param name="Pasted">貼り付け先</param>
        public void Copy(LibStatus Pasted)
        {
            Pasted.StatusTable.Clear();
            Pasted.StatusTable = (CharacterStatusListEntity.status_dataDataTable)this.StatusTable.Copy();
        }

        /// <summary>
        /// ステータスの追加
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <param name="Rank">ランク</param>
        /// <param name="SubRank">サブランク</param>
        /// <param name="EndLimit">終了カウント</param>
        /// <param name="Level">施行者レベル</param>
        /// <param name="View">表示設定</param>
        /// <returns>追加成功したか</returns>
        public bool Add(int StatusID, int Rank, int SubRank, int EndLimit, int Level, bool View)
        {
            CharacterStatusListEntity.status_dataRow row = StatusTable.FindBystatus_id(StatusID);

            if (row != null)
            {
                if (row.level >= Level)
                {
                    return false;
                }

                row.rank = Rank;
                row.sub_rank = SubRank;
                row.end_limit = EndLimit;
                row.level = Level;
                row.view = View;
            }
            else
            {
                CharacterStatusListEntity.status_dataRow AddRow = StatusTable.Newstatus_dataRow();

                AddRow.entry_no = 0;
                AddRow.status_id = StatusID;
                AddRow.rank = Rank;
                AddRow.sub_rank = SubRank;
                AddRow.end_limit = EndLimit;
                AddRow.level = Level;
                AddRow.view = View;

                StatusTable.Addstatus_dataRow(AddRow);
            }

            return true;
        }

        /// <summary>
        /// ステータスの追加（レジスト判定付き）
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <param name="Rank">ランク</param>
        /// <param name="SubRank">サブランク</param>
        /// <param name="EndLimit">終了カウント</param>
        /// <param name="Level">施行者レベル</param>
        /// <returns>追加成功したか</returns>
        public bool AddWithRegist(int StatusID, int Rank, int SubRank, int EndLimit, int Level, bool View)
        {
            if (!Regist(StatusID))
            {
                return Add(StatusID, Rank, SubRank, EndLimit, Level, View);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// レジスト判定
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>レジスト</returns>
        public bool Regist(int StatusID)
        {
            if (LibStatusList.CheckGoodStatus(StatusID)) { return false; }

            // セーフティのチェック
            switch (StatusID)
            {
                case 1:
                case 10:
                case 12:
                case 13:
                case 20:
                    if (Check(999))
                    {
                        return true;
                    }
                    break;
            }

            if (StatusID < 250 && Check(StatusID + 500))
            {
                if (LibInteger.GetRandBasis() <= GetRank(StatusID + 500))
                {
                    if (GetLimit(StatusID + 500) > 0)
                    {
                        Delete(StatusID + 500);
                    }

                    // レジスト成功！
                    return true;
                }
            }
            else if (StatusID < 250 && Check(88) && GetRank(88) == StatusID)
            {
                if (GetLimit(88) > 0)
                {
                    Delete(88);
                }

                // レジスト成功！
                return true;
            }

            return false;
        }

        /// <summary>
        /// 現在の状態異常一覧をリストにして出力
        /// </summary>
        /// <param name="IsView">すべて表示する場合はtrue</param>
        /// <returns>状態異常一覧</returns>
        public string ToString(bool IsView)
        {
            StringBuilder val = new StringBuilder();

            foreach (DataRow StatusRow in StatusTable.Rows)
            {
                if (StatusRow.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if ((bool)StatusRow["view"] || IsView)
                {
                    val.Append(LibStatusList.GetIcon((int)StatusRow["status_id"], (int)(decimal)StatusRow["rank"], (int)(decimal)StatusRow["sub_rank"]));
                }
            }

            if (val.ToString().Length == 0)
            {
                return "<span title=\"特別にステータス変化が発生していない状態。\">正常</span>";
            }
            else
            {
                return val.ToString();
            }
        }

        /// <summary>
        /// ステータスの影響下か
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>影響下なら真</returns>
        public bool Check(int StatusID)
        {
            CharacterStatusListEntity.status_dataRow row = StatusTable.FindBystatus_id(StatusID);

            if (row != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// ステータスDataRow取得
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>status_dataRow</returns>
        public CharacterStatusListEntity.status_dataRow Find(int StatusID)
        {
            return StatusTable.FindBystatus_id(StatusID);
        }

        /// <summary>
        /// ステータスのランクを取得
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>ランク</returns>
        public decimal GetRank(int StatusID)
        {
            CharacterStatusListEntity.status_dataRow row = StatusTable.FindBystatus_id(StatusID);

            if (row != null)
            {
                return row.rank;
            }

            return 0;
        }

        /// <summary>
        /// ステータスのサブランクを取得
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>サブランク</returns>
        public decimal GetSubRank(int StatusID)
        {
            CharacterStatusListEntity.status_dataRow row = StatusTable.FindBystatus_id(StatusID);

            if (row != null)
            {
                return row.sub_rank;
            }

            return 0;
        }

        /// <summary>
        /// ステータスのリミットを取得
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>リミット</returns>
        public int GetLimit(int StatusID)
        {
            CharacterStatusListEntity.status_dataRow row = StatusTable.FindBystatus_id(StatusID);

            if (row != null)
            {
                return row.end_limit;
            }

            return 0;
        }

        /// <summary>
        /// ステータスのランクを設定
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <param name="Rank">設定ランク</param>
        public void SetRank(int StatusID, int Rank)
        {
            CharacterStatusListEntity.status_dataRow row = StatusTable.FindBystatus_id(StatusID);

            if (row != null)
            {
                row.rank = Rank;
            }
        }

        /// <summary>
        /// ステータスのサブランクを設定
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <param name="SubRank">設定サブランク</param>
        public void SetSubRank(int StatusID, int SubRank)
        {
            CharacterStatusListEntity.status_dataRow row = StatusTable.FindBystatus_id(StatusID);

            if (row != null)
            {
                row.sub_rank = SubRank;
            }
        }

        /// <summary>
        /// ステータスの削除
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>削除成功？</returns>
        public bool Delete(int StatusID)
        {
            CharacterStatusListEntity.status_dataRow row = StatusTable.FindBystatus_id(StatusID);

            if (row != null)
            {
                row.Delete();
                StatusTable.AcceptChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 解除可能ステータスリスト
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetClearStatusList()
        {
            StatusTable.DefaultView.RowFilter = "end_limit=0";
            return StatusTable.DefaultView.ToTable();
        }

        /// <summary>
        /// カウントをマイナスする
        /// </summary>
        public void HalfCount()
        {
            foreach (CharacterStatusListEntity.status_dataRow StatusRow in StatusTable)
            {
                if (StatusRow.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (StatusRow.end_limit == -1)
                {
                    // 無限はスキップ
                    continue;
                }

                if (StatusRow.end_limit > 0)
                {
                    StatusRow.end_limit = StatusRow.end_limit - 1;

                    if (StatusRow.end_limit < 0)
                    {
                        StatusRow.end_limit = 0;
                    }
                }
            }
        }

        /// <summary>
        /// ステータス異常クリア
        /// </summary>
        public void Clear()
        {
            int cnt = 0;
            int StatusCnt = StatusTable.Count;
            for (int i = 0; i < StatusCnt; i++)
            {
                CharacterStatusListEntity.status_dataRow StatusRow = StatusTable[cnt];

                cnt++;
            }
            StatusTable.AcceptChanges();
        }

        public DataRowCollection Rows
        {
            get
            {
                return StatusTable.Rows;
            }
        }

        /// <summary>
        /// ステータスのディスペル
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>削除成功？</returns>
        public bool Dispel(ref int StatusID)
        {
            foreach (CharacterStatusListEntity.status_dataRow StatusRow in StatusTable)
            {
                if (StatusRow.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (LibStatusList.CheckDispel(StatusRow.status_id))
                {
                    StatusID = StatusRow.status_id;
                    StatusRow.Delete();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ステータスのクリアランス
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>削除成功？</returns>
        public bool Clearlance(ref int StatusID)
        {
            foreach (CharacterStatusListEntity.status_dataRow StatusRow in StatusTable)
            {
                if (StatusRow.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (LibStatusList.CheckClearrance(StatusRow.status_id))
                {
                    StatusID = StatusRow.status_id;
                    StatusRow.Delete();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ステータスのエスナ
        /// </summary>
        /// <param name="StatusID">ステータスID</param>
        /// <returns>削除成功？</returns>
        public bool Esna(ref int StatusID)
        {
            foreach (CharacterStatusListEntity.status_dataRow StatusRow in StatusTable)
            {
                if (StatusRow.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (StatusRow.status_id >= 900)
                {
                    continue;
                }

                if (!LibStatusList.CheckGoodStatus(StatusRow.status_id))
                {
                    // バッドステータスはすべて解消
                    StatusID = StatusRow.status_id;
                    StatusRow.Delete();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ステータスの登録
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        public void Update(int EntryNo)
        {
            string UpSql;
            string InSql;
            string DelSql;

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                DelSql = "DELETE FROM ts_character_using_status WHERE entry_no=" + EntryNo;
                dba.ExecuteNonQuery(DelSql);

                foreach (CharacterStatusListEntity.status_dataRow StatusRow in StatusTable)
                {
                    StatusRow.entry_no = EntryNo;
                    UpSql = LibSql.MakeUpSql("ts_character_using_status", "entry_no=" + EntryNo + " and status_id=" + StatusRow.status_id, StatusRow);
                    InSql = LibSql.MakeInSql("ts_character_using_status", StatusRow);

                    if (dba.ExecuteNonQuery(UpSql) == 0)
                    {
                        dba.ExecuteNonQuery(InSql);
                    }
                }
                dba.Commit();
            }
            catch
            {
                dba.Rollback();
            }
            finally
            {
                dba.Close();
            }
        }
    }
}
