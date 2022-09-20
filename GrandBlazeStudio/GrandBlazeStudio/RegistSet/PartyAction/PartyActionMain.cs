using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using System.IO;
using CommonLibrary.Character;
using CommonLibrary.DataAccess;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        private LibContinue con = new LibContinue();
        private DataTable PartyList;
        private LibUnitBaseMini CharaMini = new LibUnitBaseMini();

        private List<LibPlayer> CharaList = new List<LibPlayer>();

        public PartyActionMain()
        {
            // コンストラクタ
            PartyList = new DataTable();
        }

        public void Action(string OnlyPartyNo)
        {
            if (OnlyPartyNo.Length > 0)
            {
                LibParty.PartyList().RowFilter = "party_no in (" + OnlyPartyNo + ")";
            }

            PartyList = LibParty.PartyList().ToTable();

            // 処理対象フォルダ内ファイル一括削除
            if (Directory.Exists(LibConst.OutputFolderParty))
            {
                Directory.Delete(LibConst.OutputFolderParty, true);
            }
            Directory.CreateDirectory(LibConst.OutputFolderParty);

            // データの一括ロード
            foreach (DataRow row in CharaMini.ChatacterTable.Rows)
            {
                CharaList.Add(new LibPlayer(Status.Belong.Friend, (int)row["entry_no"]));
            }
            LibBattleResult.Load();

            // ヘッダー
            BattleHeadder();

            // パーティメッセージ
            PartyMessage();

            // 戦闘前クエスト
            QuestBeforeEntry();

            // 戦闘開始
            BattleMain();

            // 戦闘後クエスト
            QuestAfterEntry();

            // 自動回復
            AutoRepair();

            // フッター
            BattleFooter();

            {
                LibDBLocal dba = new LibDBLocal();

                try
                {
                    dba.BeginTransaction();
                    dba.Update(LibBattleResult.Entity.temp_battle_result);
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

                LibBattleResult.Load();
            }
        }
    }
}
