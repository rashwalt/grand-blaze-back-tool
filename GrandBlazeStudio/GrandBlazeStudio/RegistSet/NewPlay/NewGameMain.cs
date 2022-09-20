using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using CommonLibrary.DataAccess;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.Entity;

namespace GrandBlazeStudio.RegistSet.NewPlay
{
    class NewGameMain
    {
        private CharacterDataEntity.ts_character_listDataTable CharacterTable = new CharacterDataEntity.ts_character_listDataTable();

        public NewGameMain()
        {
            // コンストラクタ
        }

        public void NewGameSet()
        {
            // 新規参加者を抽出
            NewGamerEntity.ts_newgame_playerDataTable NewPlayerTable = new NewGamerEntity.ts_newgame_playerDataTable();
            using (LibDBLocal dba = new LibDBLocal())
            {
                dba.Fill("SELECT * FROM ts_newgame_player", NewPlayerTable);
                dba.Fill("SELECT * FROM ts_character_list", CharacterTable);
            }

            foreach (NewGamerEntity.ts_newgame_playerRow NewPlayerRow in NewPlayerTable)
            {
                RegistNewPlayer(NewPlayerRow);
            }
        }

        private void RegistNewPlayer(NewGamerEntity.ts_newgame_playerRow NewRow)
        {
            // キャラクター管理クラス
            LibPlayer EditChara = new LibPlayer();

            // エントリーナンバーの算出
            //int EntryNo = LibInteger.GetNewUnderNum(CharacterTable, "entry_no");
            int EntryNo = NewRow.entry_no;
            EditChara.EntryNo = EntryNo;

            // 初期インストールクラスの設定
            foreach (InstallDataEntity.mt_install_class_listRow InstallRow in LibInstall.Entity.mt_install_class_list)
            {
                EditChara.AddInstallClass(InstallRow.install_id);
            }

            // 新規登録時の更新回数
            EditChara.NewPlayRegistUpdate = GrandBlazeStudio.Properties.Settings.Default.UpdateCnt;

            // 最終継続日時＝新規登録日時
            EditChara.LastUpdate = NewRow.time;

            // キャラクター名
            EditChara.CharacterName = NewRow.character_name;

            // キャラクター愛称
            EditChara.NickName = NewRow.nick_name;

            // キャラクター性別
            EditChara.SetSex(NewRow.sex);

            // キャラクター年齢
            EditChara.Age = NewRow.age;

            // キャラクター身長
            EditChara.Height = NewRow.height;

            // キャラクター体重
            EditChara.Weight = NewRow.weight;

            // キャラクター所属国家
            EditChara.SetNation(NewRow.nation);

            // キャラクター画像URL
            EditChara.ImageURL = NewRow.image_url;

            // キャラクター画像 横サイズ
            EditChara.ImageWidthSize = NewRow.image_width;

            // キャラクター画像 縦サイズ
            EditChara.ImageHeightSize = NewRow.image_height;

            // キャラクター画像 リンク先
            EditChara.ImageLinkURL = NewRow.image_link_url;

            // キャラクター画像 著作権者
            EditChara.ImageCopyright = NewRow.image_copyright;

            // キャラクターの初期インストールクラス
            EditChara.SetInstallClass(NewRow.install_class_no);

            // キャラクター種族
            EditChara.SetRace(NewRow.race);

            // キャラクター守護者
            EditChara.SetGuardian(NewRow.guardian);

            // キャラクターのユニーク名
            EditChara.UniqueName = NewRow.unique_name;

            int RestItemCount = 0;

            #region 装備品設定：武器
            int MainItemNo = 0;
            int MainItemCount = 1;

            switch (NewRow.main_weapon)
            {
                case 1:
                    // 格闘
                    EditChara.AddItem(Status.ItemBox.Normal, 10, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 2:
                    // 短剣
                    EditChara.AddItem(Status.ItemBox.Normal, 11, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 3:
                    // 片手剣
                    EditChara.AddItem(Status.ItemBox.Normal, 12, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 4:
                    // 片手斧
                    EditChara.AddItem(Status.ItemBox.Normal, 13, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 5:
                    // 片手刀
                    EditChara.AddItem(Status.ItemBox.Normal, 14, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 6:
                    // 片手鞭
                    EditChara.AddItem(Status.ItemBox.Normal, 15, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 7:
                    // 書物
                    EditChara.AddItem(Status.ItemBox.Normal, 16, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 8:
                    // 魔導器
                    EditChara.AddItem(Status.ItemBox.Normal, 17, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 9:
                    // 両手剣
                    EditChara.AddItem(Status.ItemBox.Normal, 18, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 10:
                    // 両手刀
                    EditChara.AddItem(Status.ItemBox.Normal, 19, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 11:
                    // 両手杖
                    EditChara.AddItem(Status.ItemBox.Normal, 20, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 12:
                    // 両手槍
                    EditChara.AddItem(Status.ItemBox.Normal, 21, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 13:
                    // 両手棒
                    EditChara.AddItem(Status.ItemBox.Normal, 22, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 14:
                    // 楽器
                    EditChara.AddItem(Status.ItemBox.Normal, 23, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 15:
                    // 投擲
                    EditChara.AddItem(Status.ItemBox.Normal, 24, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 16:
                    // 弓
                    EditChara.AddItem(Status.ItemBox.Normal, 25, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 17:
                    // 弩
                    EditChara.AddItem(Status.ItemBox.Normal, 26, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
                case 18:
                    // 銃
                    EditChara.AddItem(Status.ItemBox.Normal, 27, false, ref MainItemCount, ref MainItemNo, ref RestItemCount);
                    break;
            }

            // 武器の装備
            EditChara.Equip(Status.EquipSpot.Main, MainItemNo);
            #endregion

            #region 装備品設定：防具
            int BodyItemNo = 0;
            int HeadItemNo = 0;
            int ProtectCount = 1;

            switch ((int)NewRow["race"])
            {
                case Status.Race.Hume:
                    // ヒューム
                    EditChara.AddItem(Status.ItemBox.Normal, 71, false, ref ProtectCount, ref HeadItemNo, ref RestItemCount);
                    EditChara.AddItem(Status.ItemBox.Normal, 70, false, ref ProtectCount, ref BodyItemNo, ref RestItemCount);
                    break;
                case Status.Race.Elve:
                    // エルヴ
                    EditChara.AddItem(Status.ItemBox.Normal, 76, false, ref ProtectCount, ref HeadItemNo, ref RestItemCount);
                    EditChara.AddItem(Status.ItemBox.Normal, 75, false, ref ProtectCount, ref BodyItemNo, ref RestItemCount);
                    break;
                case Status.Race.Falurt:
                    // ファルート
                    EditChara.AddItem(Status.ItemBox.Normal, 81, false, ref ProtectCount, ref HeadItemNo, ref RestItemCount);
                    EditChara.AddItem(Status.ItemBox.Normal, 80, false, ref ProtectCount, ref BodyItemNo, ref RestItemCount);
                    break;
                case Status.Race.Lycanth:
                    // ライカンス
                    EditChara.AddItem(Status.ItemBox.Normal, 86, false, ref ProtectCount, ref HeadItemNo, ref RestItemCount);
                    EditChara.AddItem(Status.ItemBox.Normal, 85, false, ref ProtectCount, ref BodyItemNo, ref RestItemCount);
                    break;
                case Status.Race.Bartan:
                    // バルタン
                    EditChara.AddItem(Status.ItemBox.Normal, 91, false, ref ProtectCount, ref HeadItemNo, ref RestItemCount);
                    EditChara.AddItem(Status.ItemBox.Normal, 90, false, ref ProtectCount, ref BodyItemNo, ref RestItemCount);
                    break;
                case Status.Race.Draqh:
                    // ドラクォ
                    EditChara.AddItem(Status.ItemBox.Normal, 96, false, ref ProtectCount, ref HeadItemNo, ref RestItemCount);
                    EditChara.AddItem(Status.ItemBox.Normal, 95, false, ref ProtectCount, ref BodyItemNo, ref RestItemCount);
                    break;
            }

            EditChara.Equip(Status.EquipSpot.Body, BodyItemNo);
            EditChara.Equip(Status.EquipSpot.Head, HeadItemNo);
            #endregion

            #region 装備品設定：アクセサリ（守護者依存）
            int AcceItemNo = 0;
            int AcceCount = 1;

            switch (NewRow.guardian)
            {
                case 1:
                    // 修羅の炎帝イグニート
                    EditChara.AddItem(Status.ItemBox.Normal, 60, false, ref AcceCount, ref AcceItemNo, ref RestItemCount);
                    break;
                case 2:
                    // 氷花の乙女セルシウス
                    EditChara.AddItem(Status.ItemBox.Normal, 61, false, ref AcceCount, ref AcceItemNo, ref RestItemCount);
                    break;
                case 3:
                    // 風来の鬼神チャフリカ
                    EditChara.AddItem(Status.ItemBox.Normal, 62, false, ref AcceCount, ref AcceItemNo, ref RestItemCount);
                    break;
                case 4:
                    // 地獄の咆哮クツェルカン
                    EditChara.AddItem(Status.ItemBox.Normal, 63, false, ref AcceCount, ref AcceItemNo, ref RestItemCount);
                    break;
                case 5:
                    // 湧泉の真人カアシャック
                    EditChara.AddItem(Status.ItemBox.Normal, 64, false, ref AcceCount, ref AcceItemNo, ref RestItemCount);
                    break;
                case 6:
                    // 轟縛の雷帝イーヴァン
                    EditChara.AddItem(Status.ItemBox.Normal, 65, false, ref AcceCount, ref AcceItemNo, ref RestItemCount);
                    break;
                case 7:
                    // 閃光の翼士イシュタス
                    EditChara.AddItem(Status.ItemBox.Normal, 66, false, ref AcceCount, ref AcceItemNo, ref RestItemCount);
                    break;
                case 8:
                    // 漆黒の魔手アン・プトゥ
                    EditChara.AddItem(Status.ItemBox.Normal, 67, false, ref AcceCount, ref AcceItemNo, ref RestItemCount);
                    break;
            }

            EditChara.Equip(Status.EquipSpot.Accesory, AcceItemNo);
            #endregion

            // 初期行動内容
            EditChara.ActionSettingReset();
            EditChara.ActionSettings(1, 2, 1, 0);

            // エリア
            EditChara.SetMovingMark(1, true, false);

            // キャラクターデータ保存
            EditChara.Update();

            // パーティ設定
            LibParty.ReloadCharacterMini();

            int PartyNo = LibParty.SetNewParty(EntryNo, 1);
            LibParty.SetPartyAreaID(PartyNo, 1);
            LibParty.Update();
        }
    }
}
