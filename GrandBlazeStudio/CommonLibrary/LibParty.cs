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
    /// �p�[�e�B�Ǘ��N���X
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
        /// �V���ȃp�[�e�B�ԍ����擾
        /// </summary>
        /// <returns>�p�[�e�B�ԍ�</returns>
        public static int GetNewPartyNo()
        {
            return LibInteger.GetNewUnderNum(Entity.ts_party_list, "party_no");
        }

        /// <summary>
        /// �V���ȃp�[�e�B���쐬����
        /// </summary>
        /// <param name="EntryNo">�G���g���[No</param>
        /// <param name="AreaID">�G���AID</param>
        /// <returns>�p�[�e�B�ԍ�</returns>
        public static int SetNewParty(int EntryNo, int AreaID)
        {
            int NewPartyNo = GetNewPartyNo();

            string PartyName = GetDefaultPartName(NewPartyNo);

            RegistBelongParty(NewPartyNo, EntryNo, true, false, false, AreaID, false);
            RegistParty(NewPartyNo, PartyName, AreaID, false, false);

            string MineNickName = ch.GetNickName(EntryNo);

            LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.PartySetting, MineNickName + "�͐V���ɁA�u<a href=\"" + PartyLink(NewPartyNo, 1) + "\">" + PartyName + "</a>�v�Ƃ��ēo�^����܂����B", Status.MessageLevel.Normal);

            return NewPartyNo;
        }

        /// <summary>
        /// �����̃p�[�e�B�ɉ�������
        /// </summary>
        /// <param name="PartyNo"></param>
        /// <param name="EntryNo"></param>
        /// <param name="IsTemp">�ꎞ�p�[�e�B��</param>
        public static void SetInParty(int PartyNo, int EntryNo, bool IsTemp)
        {
            int AreaID = GetPartyMarkID(PartyNo);
            RegistBelongParty(PartyNo, EntryNo, false, null, null, AreaID, IsTemp);
        }

        /// <summary>
        /// �f�t�H���g�p�[�e�B�����擾
        /// </summary>
        /// <param name="NewPartyNo">�p�[�e�BNo</param>
        /// <returns>�p�[�e�B��</returns>
        public static string GetDefaultPartName(int NewPartyNo)
        {
            return "��" + NewPartyNo + "�p�[�e�B";
        }

        /// <summary>
        /// �p�[�e�B���̂�o�^
        /// </summary>
        /// <param name="PartyNo">�p�[�e�BNo</param>
        /// <param name="PartyName">�p�[�e�B��</param>
        public static void SetPartyName(int PartyNo, string PartyName)
        {
            int AreaID = GetPartyMarkID(PartyNo);
            RegistParty(PartyNo, PartyName, AreaID, null, null);
        }

        /// <summary>
        /// �p�[�e�B���[�_�[�ݒ�
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <param name="ReaderNo">���[�_�[E-No.</param>
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
                // �p�[�e�B�����łɑ��݂���ꍇ

                PartyRow.reader = true;
            }
        }

        /// <summary>
        /// �p�[�e�B���̓o�^
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <param name="PartyName">�p�[�e�B��</param>
        /// <param name="AreaID">�G���AID</param>
        /// <param name="IsWedding">�E�F�f�B���O�p�[�e�B��</param>
        /// <param name="IsTemp">�ꎞ�p�[�e�B��</param>
        public static void RegistParty(int PartyNo, string PartyName, int AreaID, bool? IsWedding, bool? IsTemp)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                // �p�[�e�B�����łɑ��݂���ꍇ

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
                // �V�K�p�[�e�B�̏ꍇ
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
        /// �p�[�e�B���ꎞ�p�[�e�B�����ɂ���
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        public static void SetTempParty(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                // �p�[�e�B�����łɑ��݂���ꍇ
                PartyRow.temp = true;
            }
        }

        /// <summary>
        /// �p�[�e�B���ꎞ�p�[�e�B��������
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        public static void ClearTempParty(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                // �p�[�e�B�����łɑ��݂���ꍇ
                PartyRow.temp = false;
            }
        }

        /// <summary>
        /// �p�[�e�B���̍폜
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        public static void DeleteParty(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                PartyRow.Delete();
            }
        }

        /// <summary>
        /// �����p�[�e�B���̓o�^
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <param name="EntryNo">�G���g���[No</param>
        /// <param name="IsPartyReader">�p�[�e�B���[�_�[�t���O</param>
        /// <param name="IsGroom">�V�Y��</param>
        /// <param name="IsBride">�V�w��</param>
        /// <param name="MarkID">�}�[�NID</param>
        /// <param name="IsTemp">�ꎞ�p�[�e�B��</param>
        public static void RegistBelongParty(int PartyNo, int EntryNo, bool IsPartyReader, bool? IsGroom, bool? IsBride, int MarkID, bool IsTemp)
        {
            PartySettingEntity.ts_character_belong_partyRow MemberRow = Entity.ts_character_belong_party.FindByentry_no(EntryNo);

            if (MemberRow != null)
            {
                // ���łɂǂ����ɏ������Ă���ꍇ
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
                // �V�K�p�[�e�B�̏ꍇ
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
        /// �����p�[�e�B���̍폜
        /// </summary>
        /// <param name="EntryNo">�G���g���[No</param>
        public static void DeleteBelongParty(int EntryNo)
        {
            PartySettingEntity.ts_character_belong_partyRow MemberRow = Entity.ts_character_belong_party.FindByentry_no(EntryNo);

            if (MemberRow != null)
            {
                MemberRow.Delete();
            }
        }

        /// <summary>
        /// �p�[�e�B���X�g
        /// </summary>
        /// <returns>�p�[�e�BDataView</returns>
        public static DataView PartyList()
        {
            return Entity.ts_party_list.DefaultView;
        }

        /// <summary>
        /// �p�[�e�B�l���̎擾
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�p�[�e�B�l��</returns>
        public static int PartyMemberCount(int PartyNo)
        {
            Entity.ts_character_belong_party.DefaultView.RowFilter = "party_no=" + PartyNo;

            return Entity.ts_character_belong_party.DefaultView.Count;
        }

        /// <summary>
        /// �p�[�e�B�����o�[�̔ԍ����X�g
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�����o�[No�z��</returns>
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
        /// �p�[�e�B�����o�[�̔ԍ����X�g
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�����o�[No�z��</returns>
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
        /// �p�[�e�B�����o�[�̎擾
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�p�[�e�B�����o�[�e�[�u��</returns>
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
        /// �p�[�e�B�����o�[�ɐV�K�L�����N�^�[�͂��邩
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>����ꍇtrue</returns>
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
        /// �p�[�e�B���[�_�[�K��
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>���[�_�[��E-No.</returns>
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
        /// �p�[�e�B�ԍ��̎擾
        /// </summary>
        /// <param name="EntryNo">�G���g���[�ԍ�</param>
        /// <returns>�p�[�e�B�ԍ�</returns>
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
        /// �p�[�e�B���̂̎擾
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�p�[�e�B����</returns>
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
        /// �p�[�e�B�����݂��邩
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>���݂���ꍇ�A�^</returns>
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
        /// �p�[�e�B�����݂���}�[�NID
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�}�[�NID</returns>
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
        /// �p�[�e�B�����݂���}�[�NID��ݒ�
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <param name="MarkID">�}�[�NID</param>
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
        /// �l�����݂���}�[�NID
        /// </summary>
        /// <param name="EntryNo">�G���g���[�ԍ�</param>
        /// <returns>�}�[�NID</returns>
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
        /// �I�t�B�V�����C�x���g�Q���t���O�����Ă�
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        public static void SetPartyOfficialEvent(int PartyNo)
        {
            PartySettingEntity.ts_party_listRow PartyRow = Entity.ts_party_list.FindByparty_no(PartyNo);

            if (PartyRow != null)
            {
                PartyRow.official_event = true;
            }
        }

        /// <summary>
        /// �I�t�B�V�����C�x���g�������ǂ���
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�����L��</returns>
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
        /// �p�[�e�B�V�X�e�����b�Z�[�W��ǋL����
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <param name="MemoType">�������</param>
        /// <param name="Memo">�������e</param>
        /// <param name="Level">���b�Z�[�W���x��</param>
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
        /// ���[�_�[�����_���ݒ�
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�ݒ肳�ꂽ���[�_�[</returns>
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
        /// �p�[�e�B�f�[�^�S�X�V
        /// </summary>
        public static void Update()
        {
            UpdatePartyData();
            UpdateBelongParty();
            ReadParty();
        }

        /// <summary>
        /// �p�[�e�B���̍X�V
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
        /// �����p�[�e�B���̍X�V
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
        /// �p�[�e�B�V�X�e�����b�Z�[�W���X�V
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
        /// �p�[�e�B�V�X�e�����b�Z�[�W�擾
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <param name="MemoType">�������</param>
        /// <returns>�Ґ�����DataView</returns>
        public static DataTable GetPartySystemMessage(int PartyNo, CommonLibrary.Status.PartySysMemoType MemoType)
        {
            Entity.temp_party_system_message.DefaultView.RowFilter = "party_no=" + PartyNo + " and memo_type=" + (int)MemoType;

            return Entity.temp_party_system_message.DefaultView.ToTable();
        }

        /// <summary>
        /// �p�[�e�B���ʂւ̃����N
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <param name="ReturnPathCount">�߂�J�E���g��</param>
        /// <returns>�����NURL</returns>
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
        /// �p�[�e�B���ʂւ̃����N
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�����NURL</returns>
        public static string PartyLink(int PartyNo)
        {
            return PartyLink(PartyNo, 0);
        }

        /// <summary>
        /// �p�[�e�B���ʃt�@�C���̃t�@�C����
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�t�@�C����</returns>
        public static string PartyHTML(int PartyNo)
        {
            return "party" + PartyNo.ToString("0000") + ".html";
        }

        /// <summary>
        /// �E�F�f�B���O�T�|�[�g�F�V�Y���̎擾
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�p�[�e�B�����o�[�e�[�u��</returns>
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
        /// �E�F�f�B���O�T�|�[�g�F�V�w���̎擾
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�p�[�e�B�����o�[�e�[�u��</returns>
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
        /// �E�F�f�B���O�T�|�[�g�F�V�YNo�擾
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�p�[�e�B�����o�[�e�[�u��</returns>
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
        /// �E�F�f�B���O�T�|�[�g�F�V�wNo�擾
        /// </summary>
        /// <param name="PartyNo">�p�[�e�B�ԍ�</param>
        /// <returns>�p�[�e�B�����o�[�e�[�u��</returns>
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
        /// �E�F�f�B���O�T�|�[�g�p�[�e�B�擾
        /// </summary>
        /// <returns>�p�[�e�B�����o�[</returns>
        public static DataTable GetWeddingParty()
        {
            Entity.ts_party_list.DefaultView.RowFilter = "wedding=true";

            return Entity.ts_party_list.DefaultView.ToTable();
        }

        /// <summary>
        /// �ꎞ�p�[�e�B�擾
        /// </summary>
        /// <returns>�p�[�e�B�����o�[</returns>
        public static DataTable GetTempParty()
        {
            Entity.ts_party_list.DefaultView.RowFilter = "temp=true";

            return Entity.ts_party_list.DefaultView.ToTable();
        }
    }
}
