using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using CommonLibrary.DataAccess;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary
{
    /// <summary>
    /// パーティ管理クラス
    /// </summary>
    public static class LibParty
    {
        private static PartySettingEntity Entity = new PartySettingEntity();
        private static LibUnitBaseMini ch = new LibUnitBaseMini();

        static LibParty()
        {
            ReadParty();
            ReadMessage();
        }

        public static void ReadParty()
        {
            Entity.ts_party_list.Clear();
            Entity.ts_character_belong_party.Clear();

            using (LibDBLocal dba = new LibDBLocal())
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <ts_party_list>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("party_no, ");
                Sql.AppendLine("pt_name, ");
                Sql.AppendLine("mark_id, ");
                Sql.AppendLine("wedding, ");
                Sql.AppendLine("temp, ");
                Sql.AppendLine("official_event ");
                Sql.AppendLine("FROM ts_party_list");
                #endregion

                dba.Fill(Sql.ToString(), Entity.ts_party_list);

                Sql = new StringBuilder();
                #region TABLE <ts_character_belong_party>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("entry_no, ");
                Sql.AppendLine("party_no, ");
                Sql.AppendLine("reader,");
                Sql.AppendLine("groom,");
                Sql.AppendLine("bride,");
                Sql.AppendLine("mark_id");
                Sql.AppendLine("FROM ts_character_belong_party");
                #endregion

                dba.Fill(Sql.ToString(), Entity.ts_character_belong_party);
            }
        }

        public static void ReadMessage()
        {
            Entity.temp_party_system_message.Clear();

            using (LibDBLocal dba = new LibDBLocal())
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <temp_party_system_message>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("party_no, ");
                Sql.AppendLine("memo_type, ");
                Sql.AppendLine("memo_id, ");
                Sql.AppendLine("memo, ");
                Sql.AppendLine("memo_level");
                Sql.AppendLine("FROM temp_party_system_message");
                #endregion

                dba.Fill(Sql.ToString(), Entity.temp_party_system_message);
            }
        }

        public static void ReloadCharacterMini()
        {
            ch = new LibUnitBaseMini();
        }

        /// <summary>
        /// 新たなパーティ番号を取得
        /// </summary>
        /// <returns>パーティ番号</returns>
        public static int GetNewPartyNo()
        {
            return LibInteger.GetNewUnderNum(Entity.ts_party_list, "party_no");
        }

        /// <summary>
        /// 新たなパーティを作成する
        /// </summary>
        /// <param name="EntryNo">エントリーNo</param>
        /// <param name="AreaID">エリアID</param>
        /// <returns>パーティ番号</returns>
        public static int SetNewParty(int EntryNo, int AreaID)
        {
            int NewPartyNo = GetNewPartyNo();

            string PartyName = GetDefaultPartName(NewPartyNo);

            RegistBelongParty(NewPartyNo, EntryNo, true, false, false, AreaID, false);
            RegistParty(NewPartyNo, PartyName, AreaID, false, false);

            string MineNickName = ch.GetNickName(EntryNo);

            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, MineNickName + "は新たに、「<a href=\"" + PartyLink(NewPartyNo, 1) + "\">" + PartyName + "</a>」として登録されました。", Status.MessageLevel.Normal);

            return NewPartyNo;
        }

        /// <summary>
        /// 既存のパーティに加入する
        /// </summary>
        /// <param name="PartyNo"></param>
        /// <param name="EntryNo"></param>
        /// <param name="IsTemp">一時パーティか</param>
        public static void SetInParty(int PartyNo, int EntryNo, bool IsTemp)
        {
            int AreaID = GetPartyMarkID(PartyNo);
            RegistBelongParty(PartyNo, EntryNo, false, null, null, AreaID, IsTemp);
        }

        /// <summary>
        /// デフォルトパーティ名を取得
        /// </summary>
        /// <param name="NewPartyNo">パーティNo</param>
        /// <returns>パーティ名</returns>
        public static string GetDefaultPartName(int NewPartyNo)
        {
            return "第" + NewPartyNo + "パーティ";
        }

        /// <summary>
        /// パーティ名称を登録
        /// </summary>
        /// <param name="PartyNo">パーティNo</param>
        /// <param name="PartyName">パーティ名</param>
        public static void SetPartyName(int PartyNo, string PartyName)
        {
            int AreaID = GetPartyMarkID(PartyNo);
            RegistParty(PartyNo, PartyName, AreaID, null, null);
        }

        /// <summary>
        /// パーティリーダー設定
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <param name="ReaderNo">リーダーE-No.</param>
        public static void SetPartyReader(int PartyNo, int ReaderNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo + " and reader=true";

            if (Entity.ts_character_belong_party.DefaultView.Count > 0)
            {
                Entity.ts_character_belong_party.DefaultView[0]["reader"] = false;
            }

            PartySettingEntity.ts_character_belong_partyRow PartyRow = Entity.ts_character_belong_party.FindByentry_no(ReaderNo);

            if (PartyRow != null)
            {
                // パーティがすでに存在する場合

                PartyRow.reader = true;
            }
        }

        /// <summary>
        /// パーティ情報の登録
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <param name="PartyName">パーティ名</param>
        /// <param name="AreaID">エリアID</param>
        /// <param name="IsWedding">ウェディングパーティか</param>
        /// <param name="IsTemp">一時パーティか</param>
        public static void RegistParty(int PartyNo, string PartyName, int AreaID, bool? IsWedding, bool? IsTemp)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                // パーティがすでに存在する場合

                PartyRow.pt_name = PartyName;
                if (AreaID > 0)
                {
                    PartyRow.mark_id = AreaID;
                }
                if (IsWedding.HasValue)
                {
                    PartyRow.wedding = IsWedding.Value;
                }
                if (IsTemp.HasValue)
                {
                    PartyRow.temp = IsTemp.Value;
                }
            }
            else
            {
                // 新規パーティの場合
                PartyRow = Entity.ts_party_list.Newts_party_listRow();

                if (!IsWedding.HasValue) { IsWedding = false; }
                if (!IsTemp.HasValue) { IsTemp = false; }

                PartyRow.party_no = PartyNo;
                PartyRow.pt_name = PartyName;
                PartyRow.mark_id = AreaID;
                PartyRow.wedding = IsWedding.Value;
                PartyRow.temp = IsTemp.Value;
                PartyRow.official_event = false;

                Entity.ts_party_list.Addts_party_listRow(PartyRow);
            }

            if (IsWedding.HasValue && !IsWedding.Value && IsTemp.HasValue && !IsTemp.Value)
            {
                Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo;

                foreach (DataRowView MemberRow in Entity.ts_character_belong_party.DefaultView)
                {
                    MemberRow["mark_id"] = AreaID;
                }
            }
        }

        /// <summary>
        /// パーティを一時パーティ扱いにする
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        public static void SetTempParty(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                // パーティがすでに存在する場合
                PartyRow.temp = true;
            }
        }

        /// <summary>
        /// パーティを一時パーティ解除する
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        public static void ClearTempParty(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                // パーティがすでに存在する場合
                PartyRow.temp = false;
            }
        }

        /// <summary>
        /// パーティ情報の削除
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        public static void DeleteParty(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                PartyRow.Delete();
            }
        }

        /// <summary>
        /// 加入パーティ情報の登録
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <param name="EntryNo">エントリーNo</param>
        /// <param name="IsPartyReader">パーティリーダーフラグ</param>
        /// <param name="IsGroom">新郎か</param>
        /// <param name="IsBride">新婦か</param>
        /// <param name="MarkID">マークID</param>
        /// <param name="IsTemp">一時パーティか</param>
        public static void RegistBelongParty(int PartyNo, int EntryNo, bool IsPartyReader, bool? IsGroom, bool? IsBride, int MarkID, bool IsTemp)
        {
            PartySettingEntity.ts_character_belong_partyRow MemberRow = Entity.ts_character_belong_party.FindByentry_no(EntryNo);

            if (MemberRow != null)
            {
                // すでにどこかに所属している場合
                MemberRow.party_no = PartyNo;
                MemberRow.reader = IsPartyReader;
                if (IsGroom.HasValue) { MemberRow.groom = IsGroom.Value; }
                if (IsBride.HasValue) { MemberRow.bride = IsBride.Value; }
                if (!IsTemp)
                {
                    MemberRow.mark_id = MarkID;
                }
            }
            else
            {
                // 新規パーティの場合
                MemberRow = Entity.ts_character_belong_party.Newts_character_belong_partyRow();

                if (!IsGroom.HasValue) { IsGroom = false; }
                if (!IsBride.HasValue) { IsBride = false; }

                MemberRow.entry_no = EntryNo;
                MemberRow.party_no = PartyNo;
                MemberRow.reader = IsPartyReader;
                MemberRow.groom = IsGroom.Value;
                MemberRow.bride = IsBride.Value;
                MemberRow.mark_id = MarkID;

                Entity.ts_character_belong_party.Addts_character_belong_partyRow(MemberRow);
            }
        }

        /// <summary>
        /// 加入パーティ情報の削除
        /// </summary>
        /// <param name="EntryNo">エントリーNo</param>
        public static void DeleteBelongParty(int EntryNo)
        {
            PartySettingEntity.ts_character_belong_partyRow MemberRow = Entity.ts_character_belong_party.FindByentry_no(EntryNo);

            if (MemberRow != null)
            {
                MemberRow.Delete();
            }
        }

        /// <summary>
        /// パーティリスト
        /// </summary>
        /// <returns>パーティDataView</returns>
        public static DataView PartyList()
        {
            return Entity.ts_party_list.DefaultView;
        }

        /// <summary>
        /// パーティ人数の取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>パーティ人数</returns>
        public static int PartyMemberCount(int PartyNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo;

            return Entity.ts_character_belong_party.DefaultView.Count;
        }

        /// <summary>
        /// パーティメンバーの番号リスト
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>メンバーNo配列</returns>
        public static int[] PartyMemberNo(int PartyNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo;
            Entity.ts_character_belong_party.DefaultView.Sort = "entry_no";

            if (Entity.ts_character_belong_party.DefaultView.Count == 0)
            {
                return null;
            }

            int[] EntryNos = new int[Entity.ts_character_belong_party.DefaultView.Count];

            int i = 0;

            foreach (DataRowView PartyMemberRow in Entity.ts_character_belong_party.DefaultView)
            {
                EntryNos[i] = (int)PartyMemberRow["entry_no"];
                i++;
            }

            return EntryNos;
        }

        /// <summary>
        /// パーティメンバーの番号リスト
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>メンバーNo配列</returns>
        public static string[] PartyMemberNoStr(int PartyNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo;

            if (Entity.ts_character_belong_party.DefaultView.Count == 0)
            {
                return null;
            }

            string[] EntryNos = new string[Entity.ts_character_belong_party.DefaultView.Count];

            int i = 0;

            foreach (DataRowView PartyMemberRow in Entity.ts_character_belong_party.DefaultView)
            {
                EntryNos[i] = PartyMemberRow["entry_no"].ToString();
                i++;
            }

            return EntryNos;
        }

        /// <summary>
        /// パーティメンバーの取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>パーティメンバーテーブル</returns>
        public static DataTable GetPartyMembers(int PartyNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo;

            if (Entity.ts_character_belong_party.DefaultView.Count == 0)
            {
                return null;
            }

            return Entity.ts_character_belong_party.DefaultView.ToTable();
        }

        /// <summary>
        /// パーティメンバーに新規キャラクターはいるか
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>いる場合true</returns>
        public static bool CheckNewPlayer(int PartyNo)
        {
            DataTable table = GetPartyMembers(PartyNo);

            foreach (DataRow row in table.Rows)
            {
                if (ch.CheckNewPlayer((int)row["entry_no"]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// パーティリーダー習得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>リーダーのE-No.</returns>
        public static int GetPartyReader(int PartyNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo + " and reader=true";

            if (Entity.ts_character_belong_party.DefaultView.Count > 0)
            {
                return (int)Entity.ts_character_belong_party.DefaultView[0]["entry_no"];
            }

            return 0;
        }

        /// <summary>
        /// パーティ番号の取得
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>パーティ番号</returns>
        public static int GetPartyNo(int EntryNo)
        {
            PartySettingEntity.ts_character_belong_partyRow MemberRow = Entity.ts_character_belong_party.FindByentry_no(EntryNo);

            if (MemberRow != null)
            {
                return MemberRow.party_no;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// パーティ名称の取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>パーティ名称</returns>
        public static string GetPartyName(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                return PartyRow.pt_name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// パーティが存在するか
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>存在する場合、真</returns>
        public static bool CheckParty(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// パーティが現在いるマークID
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>マークID</returns>
        public static int GetPartyMarkID(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                return PartyRow.mark_id;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// パーティが現在いるマークIDを設定
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <param name="MarkID">マークID</param>
        public static void SetPartyAreaID(int PartyNo, int MarkID)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                PartyRow.mark_id = MarkID;

                Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo;

                foreach (DataRowView MemberRow in Entity.ts_character_belong_party.DefaultView)
                {
                    MemberRow["mark_id"] = MarkID;
                }
            }
        }

        /// <summary>
        /// 個人が現在いるマークID
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>マークID</returns>
        public static int GetEntryAreaID(int EntryNo)
        {
            PartySettingEntity.ts_character_belong_partyRow MemberRow = Entity.ts_character_belong_party.FindByentry_no(EntryNo);

            if (MemberRow != null)
            {
                return MemberRow.mark_id;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// オフィシャルイベント参加フラグをたてる
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        public static void SetPartyOfficialEvent(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                PartyRow.official_event = true;
            }
        }

        /// <summary>
        /// オフィシャルイベント発生かどうか
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>発生有無</returns>
        public static bool CheckPartyOfficialEvent(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null && PartyRow.official_event)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// パーティシステムメッセージを追記する
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <param name="MemoType">メモ種別</param>
        /// <param name="Memo">メモ内容</param>
        /// <param name="Level">メッセージレベル</param>
        public static void AddSystemMessage(int PartyNo, CommonLibrary.Status.PartySysMemoType MemoType, string Memo, int Level)
        {
            PartySettingEntity.temp_party_system_messageRow SystemMessageRow = Entity.temp_party_system_message.Newtemp_party_system_messageRow();

            Entity.temp_party_system_message.DefaultView.RowFilter = "party_no=" + PartyNo;

            int NewMemoNo = LibInteger.GetNewUnderNum(Entity.temp_party_system_message.DefaultView, "memo_id");

            SystemMessageRow.party_no = PartyNo;
            SystemMessageRow.memo_type = (int)MemoType;
            SystemMessageRow.memo_id = NewMemoNo;
            SystemMessageRow.memo = Memo;
            SystemMessageRow.memo_level = Level;

            Entity.temp_party_system_message.Addtemp_party_system_messageRow(SystemMessageRow);
        }

        /// <summary>
        /// リーダーランダム設定
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>設定されたリーダー</returns>
        public static int SetReaderByRandom(int PartyNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo;

            foreach (DataRowView row in Entity.ts_character_belong_party.DefaultView)
            {
                row["reader"] = false;
            }

            int Target = LibInteger.GetRand(Entity.ts_character_belong_party.DefaultView.Count);

            Entity.ts_character_belong_party.DefaultView[Target]["reader"] = true;

            return (int)Entity.ts_character_belong_party.DefaultView[Target]["entry_no"];
        }

        /// <summary>
        /// パーティデータ全更新
        /// </summary>
        public static void Update()
        {
            UpdatePartyData();
            UpdateBelongParty();
            ReadParty();
        }

        /// <summary>
        /// パーティ情報の更新
        /// </summary>
        public static void UpdatePartyData()
        {
            string UpSql;
            string InSql;
            string DelSql;

            if (Entity.ts_party_list.GetChanges() == null)
            {
                return;
            }

            LibDBLocal dba = new LibDBLocal();

            try
            {
                dba.BeginTransaction();

                foreach (DataRow PartyRow in Entity.ts_party_list.GetChanges().Rows)
                {
                    if (PartyRow.RowState == DataRowState.Added || PartyRow.RowState == DataRowState.Modified)
                    {
                        UpSql = LibSql.MakeUpSql("ts_party_list", "party_no=" + (int)PartyRow["party_no"], PartyRow);
                        InSql = LibSql.MakeInSql("ts_party_list", PartyRow);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (PartyRow.RowState == DataRowState.Deleted)
                    {
                        DelSql = "DELETE FROM ts_party_list WHERE party_no=" + (int)PartyRow["party_no", DataRowVersion.Original];

                        dba.ExecuteNonQuery(DelSql);
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

        /// <summary>
        /// 加入パーティ情報の更新
        /// </summary>
        private static void UpdateBelongParty()
        {
            if (Entity.ts_character_belong_party.GetChanges() == null)
            {
                return;
            }

            string UpSql;
            string InSql;
            string DelSql;

            LibDBLocal dba = new LibDBLocal();

            try
            {
                dba.BeginTransaction();
                foreach (DataRow PartyMemberRow in Entity.ts_character_belong_party.GetChanges().Rows)
                {
                    if (PartyMemberRow.RowState == DataRowState.Added || PartyMemberRow.RowState == DataRowState.Modified)
                    {
                        Hashtable EditHash = new Hashtable();

                        EditHash["entry_no"] = PartyMemberRow["entry_no"];
                        EditHash["party_no"] = PartyMemberRow["party_no"];
                        EditHash["reader"] = PartyMemberRow["reader"];
                        EditHash["groom"] = PartyMemberRow["groom"];
                        EditHash["bride"] = PartyMemberRow["bride"];
                        EditHash["mark_id"] = PartyMemberRow["mark_id"];

                        UpSql = LibSql.MakeUpSql("ts_character_belong_party", "entry_no=" + (int)PartyMemberRow["entry_no"], EditHash);
                        InSql = LibSql.MakeInSql("ts_character_belong_party", EditHash);

                        if (dba.ExecuteNonQuery(UpSql) == 0)
                        {
                            dba.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (PartyMemberRow.RowState == DataRowState.Deleted)
                    {
                        DelSql = "DELETE FROM ts_character_belong_party WHERE entry_no=" + (int)PartyMemberRow["entry_no", DataRowVersion.Original];

                        dba.ExecuteNonQuery(DelSql);
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

        /// <summary>
        /// パーティシステムメッセージを更新
        /// </summary>
        public static void UpdateSystemMessage()
        {
            string UpSql;
            string InSql;

            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();
                foreach (DataRow PartyMemoRow in Entity.temp_party_system_message.Rows)
                {
                    UpSql = LibSql.MakeUpSql("temp_party_system_message", "party_no=" + (int)PartyMemoRow["party_no"] + " and memo_type=" + (int)PartyMemoRow["memo_type"] + " and memo_id=" + (int)PartyMemoRow["memo_id"], PartyMemoRow);
                    InSql = LibSql.MakeInSql("temp_party_system_message", PartyMemoRow);

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

            ReadMessage();
        }

        /// <summary>
        /// パーティシステムメッセージ取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <param name="MemoType">メモ種別</param>
        /// <returns>編成メモDataView</returns>
        public static DataTable GetPartySystemMessage(int PartyNo, CommonLibrary.Status.PartySysMemoType MemoType)
        {
            Entity.temp_party_system_message.DefaultView.RowFilter = "party_no=" + PartyNo + " and memo_type=" + (int)MemoType;

            return Entity.temp_party_system_message.DefaultView.ToTable();
        }

        /// <summary>
        /// パーティ結果へのリンク
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <param name="ReturnPathCount">戻るカウント数</param>
        /// <returns>リンクURL</returns>
        public static string PartyLink(int PartyNo, int ReturnPathCount)
        {
            string Ret = "";
            for (int i = 0; i < ReturnPathCount; i++)
            {
                Ret += "../";
            }
            return Ret + LibCommonLibrarySettings.Partys.Replace("\\", "") + "/" + PartyHTML(PartyNo);
        }

        /// <summary>
        /// パーティ結果へのリンク
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>リンクURL</returns>
        public static string PartyLink(int PartyNo)
        {
            return PartyLink(PartyNo, 0);
        }

        /// <summary>
        /// パーティ結果ファイルのファイル名
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>ファイル名</returns>
        public static string PartyHTML(int PartyNo)
        {
            return "party" + PartyNo.ToString("0000") + ".html";
        }

        /// <summary>
        /// ウェディングサポート：新郎名称取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>パーティメンバーテーブル</returns>
        public static string GetGroomName(int PartyNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo + " and groom=true";

            if (Entity.ts_character_belong_party.DefaultView.Count == 0)
            {
                return "";
            }

            return ch.GetNickName((int)Entity.ts_character_belong_party.DefaultView[0]["entry_no"]);
        }

        /// <summary>
        /// ウェディングサポート：新婦名称取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>パーティメンバーテーブル</returns>
        public static string GetBrideName(int PartyNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo + " and bride=true";

            if (Entity.ts_character_belong_party.DefaultView.Count == 0)
            {
                return "";
            }

            return ch.GetNickName((int)Entity.ts_character_belong_party.DefaultView[0]["entry_no"]);
        }


        /// <summary>
        /// ウェディングサポート：新郎No取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>パーティメンバーテーブル</returns>
        public static int GetGroomNo(int PartyNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo + " and groom=true";

            if (Entity.ts_character_belong_party.DefaultView.Count == 0)
            {
                return 0;
            }

            return (int)Entity.ts_character_belong_party.DefaultView[0]["entry_no"];
        }

        /// <summary>
        /// ウェディングサポート：新婦No取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>パーティメンバーテーブル</returns>
        public static int GetBrideNo(int PartyNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo + " and bride=true";

            if (Entity.ts_character_belong_party.DefaultView.Count == 0)
            {
                return 0;
            }

            return (int)Entity.ts_character_belong_party.DefaultView[0]["entry_no"];
        }

        /// <summary>
        /// ウェディングサポートパーティ取得
        /// </summary>
        /// <returns>パーティメンバー</returns>
        public static DataTable GetWeddingParty()
        {
            Entity.ts_party_list.DefaultView.RowFilter = "wedding=true";

            return Entity.ts_party_list.DefaultView.ToTable();
        }

        /// <summary>
        /// 一時パーティ取得
        /// </summary>
        /// <returns>パーティメンバー</returns>
        public static DataTable GetTempParty()
        {
            Entity.ts_party_list.DefaultView.RowFilter = "temp=true";

            return Entity.ts_party_list.DefaultView.ToTable();
        }
    }
}
