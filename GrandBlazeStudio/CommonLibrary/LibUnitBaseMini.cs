using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataAccess;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary
{
    /// <summary>
    /// キャラクター管理クラス（最低限版）
    /// </summary>
    public class LibUnitBaseMini
    {
        public CharacterDataEntity.ts_character_listDataTable ChatacterTable = new CharacterDataEntity.ts_character_listDataTable();

        public LibUnitBaseMini()
        {
            using (LibDBLocal dba = new LibDBLocal())
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <ts_character_list>
                Sql.AppendLine("SELECT [entry_no]");
                Sql.AppendLine("      ,[continue_cnt]");
                Sql.AppendLine("      ,[continue_bonus]");
                Sql.AppendLine("      ,[account_status]");
                Sql.AppendLine("      ,[new_play]");
                Sql.AppendLine("      ,[last_update]");
                Sql.AppendLine("      ,[new_gamer]");
                Sql.AppendLine("      ,[character_name]");
                Sql.AppendLine("      ,[image_url]");
                Sql.AppendLine("      ,[image_width]");
                Sql.AppendLine("      ,[image_height]");
                Sql.AppendLine("      ,[image_link_url]");
                Sql.AppendLine("      ,[image_copyright]");
                Sql.AppendLine("      ,[nick_name]");
                Sql.AppendLine("      ,[race_id]");
                Sql.AppendLine("      ,[guardian_id]");
                Sql.AppendLine("      ,[nation_id]");
                Sql.AppendLine("      ,[have_money]");
                Sql.AppendLine("      ,[blaze_chip]");
                Sql.AppendLine("      ,[age]");
                Sql.AppendLine("      ,[sex]");
                Sql.AppendLine("      ,[max_item]");
                Sql.AppendLine("      ,[max_bazzeritem]");
                Sql.AppendLine("      ,[profile]");
                Sql.AppendLine("      ,[change_install]");
                Sql.AppendLine("      ,[unique_name]");
                Sql.AppendLine("  FROM [ts_character_list]");
                #endregion

                dba.Fill(Sql.ToString(), ChatacterTable);
            }
        }

        /// <summary>
        /// ニックネームを取得
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>ニックネーム</returns>
        public string GetNickName(int EntryNo)
        {
            CharacterDataEntity.ts_character_listRow row = ChatacterTable.FindByentry_no(EntryNo);

            return row.nick_name;
        }

        /// <summary>
        /// フルネームを取得
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>ニックネーム</returns>
        public string GetFullName(int EntryNo)
        {
            CharacterDataEntity.ts_character_listRow row = ChatacterTable.FindByentry_no(EntryNo);

            return row.character_name;
        }

        /// <summary>
        /// 性別を取得
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>性別</returns>
        public int GetSex(int EntryNo)
        {
            CharacterDataEntity.ts_character_listRow row = ChatacterTable.FindByentry_no(EntryNo);

            return row.sex;
        }

        /// <summary>
        /// キャラクター結果へのリンク
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <param name="ReturnPathCount">戻るカウント数</param>
        /// <returns>リンクURL</returns>
        public static string CharacterLink(int EntryNo, int ReturnPathCount)
        {
            string Ret = "";
            for (int i = 0; i < ReturnPathCount; i++)
            {
                Ret += "../";
            }
            return Ret + LibCommonLibrarySettings.Characters.Replace("\\", "") + "/" + CharacterHTML(EntryNo);
        }

        /// <summary>
        /// キャラクター結果へのリンク
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>リンクURL</returns>
        public static string CharacterLink(int EntryNo)
        {
            return CharacterLink(EntryNo, 0);
        }

        /// <summary>
        /// キャラクターファイルのファイル名
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>ファイル名</returns>
        public static string CharacterHTML(int EntryNo)
        {
            return "character" + EntryNo.ToString("0000") + ".html";
        }

        /// <summary>
        /// キャラクターの愛称とリンクを取得
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <returns></returns>
        public string GetNickNameWithLink(int EntryNo, int ReturnPathCount)
        {
            string NickName = GetNickName(EntryNo);
            string Link = CharacterLink(EntryNo, ReturnPathCount);

            return NickName + "[<a href=\"" + Link + "\">E-No." + EntryNo + "</a>]";
        }

        /// <summary>
        /// キャラクターの愛称とリンクを取得
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <returns></returns>
        public string GetNickNameWithLink(int EntryNo)
        {
            return GetNickNameWithLink(EntryNo, 0);
        }

        /// <summary>
        /// 指定エントリー番号のキャラクターが存在するか
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>存在する場合、真</returns>
        public bool CheckInChara(int EntryNo)
        {
            CharacterDataEntity.ts_character_listRow row = ChatacterTable.FindByentry_no(EntryNo);

            if (row != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 指定キャラクターは新規登録キャラクターか
        /// </summary>
        /// <param name="EntryNo">エントリー番号</param>
        /// <returns>新規の場合、真</returns>
        public bool CheckNewPlayer(int EntryNo)
        {
            CharacterDataEntity.ts_character_listRow row = ChatacterTable.FindByentry_no(EntryNo);

            if (row != null)
            {
                return row.new_play == LibCommonLibrarySettings.UpdateCnt;
            }

            return false;
        }

        /// <summary>
        /// キャラクター一覧取得
        /// </summary>
        /// <returns>キャラクターView</returns>
        public CharacterDataEntity.ts_character_listDataTable GetCharacters()
        {
            return ChatacterTable;
        }

        /// <summary>
        /// 最大エントリー取得
        /// </summary>
        public int GetMaxNo
        {
            get
            {
                ChatacterTable.DefaultView.RowFilter = "";
                ChatacterTable.DefaultView.Sort = "entry_no desc";
                int Max = (int)ChatacterTable.DefaultView[0]["entry_no"];
                ChatacterTable.DefaultView.Sort = "entry_no";

                return Max;
            }
        }

        public int Count()
        {
            return this.ChatacterTable.Count;
        }
    }
}
