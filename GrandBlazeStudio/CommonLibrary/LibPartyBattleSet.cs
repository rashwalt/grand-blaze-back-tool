using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    public class LibPartyBattleSet
    {
        private DataTable PartyBattleSetTable = new DataTable();
        private DataView PartyBattleSetView;

        public LibPartyBattleSet()
        {
            using (LibDBLocal dba = new LibDBLocal())
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <temp_party_battle_setting>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("party_no, ");
                Sql.AppendLine("battle_monster, ");
                Sql.AppendLine("battle_npc, ");
                Sql.AppendLine("battle_style, ");
                Sql.AppendLine("battle_win_type, ");
                Sql.AppendLine("battle_lose_type, ");
                Sql.AppendLine("mark_id, ");
                Sql.AppendLine("coffer_id ");
                Sql.AppendLine("FROM temp_party_battle_setting");
                #endregion

                PartyBattleSetTable = dba.GetTableData(Sql.ToString());
                PartyBattleSetView = new DataView(PartyBattleSetTable);
            }
        }

        /// <summary>
        /// バトルが発生するかの選別
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>バトル発生フラグ</returns>
        public bool GetIsBattleStart(int PartyNo)
        {
            PartyBattleSetView.RowFilter = "party_no=" + PartyNo;

            if (PartyBattleSetView.Count == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 出現する固定モンスター取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>固定モンスターリスト カンマ区切り (1,2,3...)</returns>
        public string GetMonsterPopList(int PartyNo)
        {
            PartyBattleSetView.RowFilter = "party_no=" + PartyNo;

            if (PartyBattleSetView.Count == 0)
            {
                return "";
            }

            return PartyBattleSetView[0]["battle_monster"].ToString();
        }

        /// <summary>
        /// 出現する固定ゲスト取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>固定ゲストリスト カンマ区切り (1,2,3...)</returns>
        public string GetGuestList(int PartyNo)
        {
            PartyBattleSetView.RowFilter = "party_no=" + PartyNo;

            if (PartyBattleSetView.Count == 0)
            {
                return "";
            }

            return PartyBattleSetView[0]["battle_npc"].ToString();
        }

        /// <summary>
        /// バトルの開始状態取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>開始状態</returns>
        public int GetBattleStyle(int PartyNo)
        {
            PartyBattleSetView.RowFilter = "party_no=" + PartyNo;

            if (PartyBattleSetView.Count == 0)
            {
                return -1;
            }

            return (int)PartyBattleSetView[0]["battle_style"];
        }

        /// <summary>
        /// バトルの勝利条件取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>勝利条件</returns>
        public int GetWinStyle(int PartyNo)
        {
            PartyBattleSetView.RowFilter = "party_no=" + PartyNo;

            if (PartyBattleSetView.Count == 0)
            {
                return -1;
            }

            return (int)PartyBattleSetView[0]["battle_win_type"];
        }

        /// <summary>
        /// バトルの敗北条件取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>敗北条件</returns>
        public int GetLoseStyle(int PartyNo)
        {
            PartyBattleSetView.RowFilter = "party_no=" + PartyNo;

            if (PartyBattleSetView.Count == 0)
            {
                return -1;
            }

            return (int)PartyBattleSetView[0]["battle_lose_type"];
        }

        /// <summary>
        /// バトルの発生ポイント取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>発生ポイント(マークID)</returns>
        public int GetMarkID(int PartyNo)
        {
            PartyBattleSetView.RowFilter = "party_no=" + PartyNo;

            if (PartyBattleSetView.Count == 0)
            {
                return -1;
            }

            return (int)PartyBattleSetView[0]["mark_id"];
        }

        /// <summary>
        /// バトルの財宝ID取得
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <returns>財宝ID</returns>
        public int GetCofferID(int PartyNo)
        {
            PartyBattleSetView.RowFilter = "party_no=" + PartyNo;

            if (PartyBattleSetView.Count == 0)
            {
                return -1;
            }

            return (int)PartyBattleSetView[0]["coffer_id"];
        }

        /// <summary>
        /// バトルの開始状態設定
        /// </summary>
        /// <param name="PartyNo">パーティ番号</param>
        /// <param name="PopMonsters">出現固定モンスター</param>
        /// <param name="PopNonplayers">出現固定NPC</param>
        /// <param name="Style">戦闘開始時状態（-1:戦闘時判定 0:通常 1:先制攻撃 2:バックアタック 3:ラウンドアタック 4:不意打ち）</param>
        /// <param name="WinStyle">勝利条件</param>
        /// <param name="LoseStyle">敗北条件</param>
        /// <param name="MarkID">バトル発生ポイント</param>
        /// <param name="CofferID">財宝ID</param>
        public void Update(int PartyNo, string PopMonsters, string PopNonplayers, int Style, int WinStyle, int LoseStyle, int MarkID, int CofferID)
        {
            string UpSql;
            string InSql;

            LibDBLocal dba = new LibDBLocal();
            Hashtable EditTable = new Hashtable();
            try
            {
                dba.BeginTransaction();
                EditTable["party_no"] = PartyNo;
                EditTable["battle_monster"] = LibSql.EscapeString(PopMonsters);
                EditTable["battle_npc"] = LibSql.EscapeString(PopNonplayers);
                EditTable["battle_style"] = Style;
                EditTable["battle_win_type"] = WinStyle;
                EditTable["battle_lose_type"] = LoseStyle;
                EditTable["mark_id"] = MarkID;
                EditTable["coffer_id"] = CofferID;

                UpSql = LibSql.MakeUpSql("temp_party_battle_setting", "party_no=" + (int)EditTable["party_no"], EditTable);
                InSql = LibSql.MakeInSql("temp_party_battle_setting", EditTable);

                if (dba.ExecuteNonQuery(UpSql) == 0)
                {
                    dba.ExecuteNonQuery(InSql);
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
