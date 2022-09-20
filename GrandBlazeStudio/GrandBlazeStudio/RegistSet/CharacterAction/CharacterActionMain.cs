using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using System.IO;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.Entity;
using Microsoft.VisualBasic.FileIO;
using CommonLibrary.DataFormat.SpecialEntity;
using GrandBlazeStudio.RegistSet.CharacterAction.OfficialEvent;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        private LibContinue con = new LibContinue();
        private LibUnitBaseMini CharaMini = new LibUnitBaseMini();
        private DataTable PartyList = new DataTable();
        private DataTable CharaTable;
        private string PrivateNoListWith = "";

        private List<LibPlayer> CharaList = new List<LibPlayer>();

        public void Action(string PrivateNoList)
        {
            CharaMini = new LibUnitBaseMini();
            PartyList = LibParty.PartyList().ToTable();
            CharacterDataEntity.ts_character_listDataTable CharaView = CharaMini.GetCharacters();
            PrivateNoListWith = PrivateNoList;

            if (PrivateNoList.Length > 0)
            {
                CharaView.DefaultView.RowFilter = "entry_no in (" + PrivateNoList + ")";
            }
            CharaTable = CharaView.DefaultView.ToTable();

            // 処理対象フォルダ内ファイル一括削除
            if (Directory.Exists(LibConst.OutputFolderChara))
            {
                Directory.Delete(LibConst.OutputFolderChara, true);
            }
            if (Directory.Exists(LibConst.OutputFolderCharaBackup))
            {
                Directory.Delete(LibConst.OutputFolderCharaBackup, true);
            }
            Directory.CreateDirectory(LibConst.OutputFolderChara);

            // 天候設定
            LibQuest.CalcWeather(GrandBlazeStudio.Properties.Settings.Default.UpdateCnt);

            // データの取得
            CharacterDataLoad();

            // アカウントの設定確認
            CheckAccount();

            LibParty.ReadParty();

            // プロフィール変更
            ProfileSetting();

            // パーティ編成
            PartySetting();
            PartyList = LibParty.PartyList().ToTable();

            // 登録確認
            //CheckContinue();

            // 警告表示
            CautionView();

            // プライベートスキルの習得
            PrivateSkill();

            // アイコン設定
            IconSettings();

            // プライベートメッセージ
            PrivateMessage();

            // バザーアイテム出品
            BazzerItemLock();

            // アイテムボックスのカバンへの移動
            ItemBoxMoving();

            // 装備解除
            RemoveEquipItem();

            // アイテム売却
            SellingItem();

            // マネートレード
            TradingMoney(false);

            // アイテム購入
            BuyingItem();

            // バザーアイテム購入
            BuyingBazzerItem();

            // バザーアイテムの返却
            ReturnBazzerItem();

            // パーティ移動実行
            PartyMoving();

            // インストールクラスのインストール
            InstallClassSetting();

            // アイテムトレード
            TradingItem(false);

            // バザーのお金送付
            SendMoneyFromBazzer();

            // レベルアップ
            foreach (LibPlayer Mine in CharaList)
            {
                ContinueDataEntity.ts_continue_mainRow ContinueMainRow = con.Entity.ts_continue_main.FindByentry_no(Mine.EntryNo);

                if (ContinueMainRow == null || ContinueMainRow.mark_id != 501 || Mine.InstallClassLevelNormal >= 10)
                {
                    // スキップ
                    continue;
                }

                // インストールクラスのレベルアップ量
                while (Mine.InstallClassLevelNormal < 10)
                {
                    Mine.InstallClassLevel++;
                    Mine.InstallClassExp += Mine.InstallClassNExp;
                }

                Mine.Update();
            }


            // 装備の実行
            EquipingItem();

            // アイテム使用
            UsingItem();

            // 戦術
            ActionSetting();

            // 隊列変更
            FormationSetting();

            // セリフ変更
            SerifChange();

            // アカウント設定変更
            AccountSetting();

            // イベント
            if (LibQuest.OfficialEventID == Status.OfficialEvent.Halloween)
            {
                Halloween.Private(CharaList, con, CharaMini);
            }
            else if (LibQuest.OfficialEventID == Status.OfficialEvent.NewYear)
            {
                NewYear.Private(CharaList, con, CharaMini);
            }

            // 一括保存
            CharacterDataSave();
            LibSelledTreasure.Update();
        }
    }
}
