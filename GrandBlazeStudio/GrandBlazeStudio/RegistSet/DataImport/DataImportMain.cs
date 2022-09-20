using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using CommonLibrary.DataAccess;

namespace GrandBlazeStudio.RegistSet.DataImport
{
    class DataImportMain
    {
        private LibDBWebServer mysqlDB = new LibDBWebServer();

        public void Imports()
        {
            LibDBLocal accessDB = new LibDBLocal();

            try
            {
                // データのリセット

                // 登録済みキャラクターリスト取得
                DataTable CharacterTable = new DataTable();
                CharacterTable = accessDB.GetTableData("SELECT entry_no FROM ts_character_list");
                DataView CharaView = new DataView(CharacterTable);

                accessDB.BeginTransaction();

                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_battle_action");// 戦術設定
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_battle_preparation");// 戦闘準備登録
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_buy_bazzer");// バザー購入設定
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_complete");// 各種登録コンプリート
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_create_item");// アイテム合成
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_icon");// アイコン設定
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_main");// 継続登録
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_message");// メッセージ送受信
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_official_event");// 公式イベント登録（システム側専用）
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_profile");// プロフィール設定
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_sell_bazzer");// バザー出品設定
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_serif");// キャラクターセリフ設定
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_shopping");// 取引登録
                accessDB.ExecuteNonQuery("DELETE FROM ts_continue_trade");// トレード設定
                accessDB.ExecuteNonQuery("DELETE FROM ts_newgame_player");// ニューゲーム

                accessDB.ExecuteNonQuery("DELETE FROM temp_battle_result");// 戦闘結果
                accessDB.ExecuteNonQuery("DELETE FROM ts_character_levelup_ability");// キャラクターレベルアップメモ
                accessDB.ExecuteNonQuery("DELETE FROM temp_party_battle_setting");// パーティバトル発生＆出現モンスター固定設定
                accessDB.ExecuteNonQuery("DELETE FROM ts_character_using_status");// キャラクター初期ステータス
                accessDB.ExecuteNonQuery("DELETE FROM temp_party_system_message");// パーティ用システムメッセージ
                accessDB.ExecuteNonQuery("DELETE FROM temp_character_system_message");// キャラクタシステムメッセージ

                accessDB.ExecuteNonQuery("UPDATE ts_character_battle_ability SET exp_unit=0");// 入手経験値初期化
                accessDB.ExecuteNonQuery("UPDATE ts_character_belong_party SET groom=0, bride=0");// ウェディングサポート用初期化
                accessDB.ExecuteNonQuery("UPDATE ts_character_have_key_item SET [new]=0");// 貴重品新規入手フラグ初期化
                accessDB.ExecuteNonQuery("UPDATE ts_character_have_item SET [new]=0");// アイテム新規入手フラグ初期化
                accessDB.ExecuteNonQuery("UPDATE ts_character_have_skill SET [new]=0");// スキル新規入手フラグ初期化

                string Sql;
                bool IsPassOK = false;

                #region インポート：新規登録
                DataTable NewGameTable = new DataTable();
                NewGameTable = GetDataByMySQL("`newgame_newgame` WHERE `activate`=1");
                DataView NewCharaView = new DataView(NewGameTable);
                foreach (DataRow DataInRow in NewGameTable.Rows)
                {
                    Sql = "INSERT INTO ts_newgame_player ( " +
                        "entry_no, " +
                        "character_name, nick_name, sex, age, height, weight, nation, " +
                        "image_url, image_width, image_height, image_link_url, image_copyright, " +
                        "install_class_no, race, guardian, " +
                        "main_weapon, unique_name, " +
                        "time, ip_address, host_address, agent " +
                        ") VALUES ( " +
                        "*NM{user_id}, " +
                        "*ST{character_name}, *ST{nick_name}, *NM{sex}, *NM{age}, *NM{height}, *NM{weight}, *NM{nation_id}, " +
                        "*ST{image_url}, *NM{image_width}, *NM{image_height}, *ST{image_link_url}, *ST{image_copyright}, " +
                        "*NM{install_class_id}, *NM{race_id}, *NM{guardian_id}, " +
                        "*NM{weapon_id}, *ST{unique_name}, " +
                        "*ST{created_at}, *ST{ip_address}, *ST{host_address}, *ST{user_agent}" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                #region インポート：各種登録有無
                DataTable CompTable = new DataTable();
                CompTable = GetDataByMySQL("continue_complete_continuecomplete");
                foreach (DataRow DataInRow in CompTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["user_id"];
                    NewCharaView.RowFilter = "user_id=" + (int)DataInRow["user_id"];
                    if (CharaView.Count > 0 || NewCharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    Sql = "INSERT INTO ts_continue_complete ( " +
                        "entry_no, " +
                        "category, " +
                        "time" +
                        ") VALUES( " +
                        "*NM{user_id}, " +
                        "*ST{category}, " +
                        "*ST{created_at}" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                #region インポート：継続登録
                DataTable ContinueTable = new DataTable();
                ContinueTable = GetDataByMySQL("continue_main_continuemain");
                foreach (DataRow DataInRow in ContinueTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["user_id"];
                    NewCharaView.RowFilter = "user_id=" + (int)DataInRow["user_id"];
                    if (CharaView.Count > 0 || NewCharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    Sql = "INSERT INTO ts_continue_main ( " +
                        "entry_no, " +
                        "party_secession, pcm_add_1, pcm_add_2, pcm_add_3, pcm_add_4, pcm_add_5, party_hope, option_comes_no, party_name, " +
                        "quest_id, mark_id, " +
                        "use_item_1, use_item_2, use_item_3, " +
                        "use_item_1_message, use_item_2_message, use_item_3_message, " +
                        "getting_private_skill, " +
                        "time, ip_address, host_address, agent, delete_fg " +
                        ") VALUES( " +
                        "*NM{user_id}, " +
                        "*NM{party_secession}, *NM{pcm_add_1}, *NM{pcm_add_2}, *NM{pcm_add_3}, *NM{pcm_add_4}, *NM{pcm_add_5}, *NM{party_hope}, *NM{option_comes_no}, *ST{party_name}, " +
                        "*NM{quest_id}, *NM{mark_id}, " +
                        "*NM{use_item_1}, *NM{use_item_2}, *NM{use_item_3}, " +
                        "*ST{use_item_1_message}, *ST{use_item_2_message}, *ST{use_item_3_message}, " +
                        "*NM{getting_private_skill}, " +
                        "*ST{created_at}, *ST{ip_address}, *ST{host_address}, *ST{user_agent}, 0" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                #region インポート：取引登録（店頭売買）
                DataTable MoneyTable = new DataTable();
                MoneyTable = GetDataByMySQL("continue_trade_continueshopping");
                foreach (DataRow DataInRow in MoneyTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["user_id"];
                    NewCharaView.RowFilter = "user_id=" + (int)DataInRow["user_id"];
                    if (CharaView.Count > 0 || NewCharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    Sql = "INSERT INTO ts_continue_shopping ( " +
                        "entry_no, " +
                        "shop_act, shopping_no, " +
                        "item_no, item_count, " +
                        "time, ip_address, host_address, agent " +
                        ") VALUES( " +
                        "*NM{user_id}, " +
                        "*NM{shop_act}, *NM{shopping_no}, " +
                        "*NM{item_no}, *NM{item_count}, " +
                        "*ST{created_at}, *ST{ip_address}, *ST{host_address}, *ST{user_agent}" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                #region インポート：取引登録（トレード）
                DataTable TradeTable = new DataTable();
                TradeTable = GetDataByMySQL("continue_trade_continuetrade");
                foreach (DataRow DataInRow in TradeTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["user_id"];
                    NewCharaView.RowFilter = "user_id=" + (int)DataInRow["user_id"];
                    if (CharaView.Count > 0 || NewCharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    Sql = "INSERT INTO ts_continue_trade ( " +
                        "entry_no," +
                        "trade_no, " +
                        "trade_entry, trade_item_no, trade_number, trade_message, trade_speed, " +
                        "time, ip_address, host_address, agent" +
                        ") VALUES( " +
                        "*NM{user_id}, " +
                        "*NM{trade_no}, " +
                        "*NM{trade_entry}, *NM{trade_item_no}, *NM{trade_number}, *ST{trade_message}, *BX{trade_speed}, " +
                        "*ST{created_at}, *ST{ip_address}, *ST{host_address}, *ST{user_agent}" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                #region インポート：戦闘準備登録
                DataTable EquipTable = new DataTable();
                EquipTable = GetDataByMySQL("continue_equip_continueequip");
                foreach (DataRow DataInRow in EquipTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["user_id"];
                    NewCharaView.RowFilter = "user_id=" + (int)DataInRow["user_id"];
                    if (CharaView.Count > 0 || NewCharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    Sql = "INSERT INTO ts_continue_battle_preparation ( " +
                        "entry_no, " +
                        "install, secondary_install, " +
                        "equip_main, equip_sub, equip_head, equip_body, equip_acce1, " +
                        "formation, " +
                        "time, ip_address, host_address, agent " +
                        ") VALUES( " +
                        "*NM{user_id}, " +
                        "*NM{install}, *NM{secondary_install}, " +
                        "*NM{equip_main}, *NM{equip_sub}, *NM{equip_head}, *NM{equip_body}, *NM{equip_acce1}, " +
                        "*ST{formation}, " +
                        "*ST{created_at}, *ST{ip_address}, *ST{host_address}, *ST{user_agent}" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                #region インポート：メッセージ送受信
                DataTable MessageTable = new DataTable();
                MessageTable = GetDataByMySQL("continue_message_continuemessage");
                foreach (DataRow DataInRow in MessageTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["user_id"];
                    NewCharaView.RowFilter = "user_id=" + (int)DataInRow["user_id"];
                    if (CharaView.Count > 0 || NewCharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    Sql = "INSERT INTO ts_continue_message ( " +
                        "entry_no, " +
                        "mes_no, " +
                        "message_target, message_entry, message_body, " +
                        "time, ip_address, host_address, agent " +
                        ") VALUES( " +
                        "*NM{user_id}, " +
                        "*NM{mes_no}, " +
                        "*NM{message_target}, *NM{message_entry}, *ST{message_body}, " +
                        "*ST{created_at}, *ST{ip_address}, *ST{host_address}, *ST{user_agent}" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                #region インポート：戦術設定
                DataTable BattleActionTable = new DataTable();
                BattleActionTable = GetDataByMySQL("continue_battleaction_continuebattleaction");
                foreach (DataRow DataInRow in BattleActionTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["user_id"];
                    NewCharaView.RowFilter = "user_id=" + (int)DataInRow["user_id"];
                    if (CharaView.Count > 0 || NewCharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    Sql = "INSERT INTO ts_continue_battle_action ( " +
                        "entry_no, " +
                        "action_no, " +
                        "action_target, action, perks_id, " +
                        "time, ip_address, host_address, agent" +
                        ") VALUES( " +
                        "*NM{user_id}, " +
                        "*NM{action_no}, " +
                        "*NM{action_target}, *NM{action}, *NM{perks_id}, " +
                        "*ST{created_at}, *ST{ip_address}, *ST{host_address}, *ST{user_agent}" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                #region インポート：セリフ設定
                DataTable CharaSerifTable = new DataTable();
                CharaSerifTable = GetDataByMySQL("continue_serif_continueserif");
                foreach (DataRow DataInRow in CharaSerifTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["user_id"];
                    NewCharaView.RowFilter = "user_id=" + (int)DataInRow["user_id"];
                    if (CharaView.Count > 0 || NewCharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    Sql = "INSERT INTO ts_continue_serif ( " +
                        "entry_no, " +
                        "word_no, " +
                        "situation, perks_id, serif_text, " +
                        "time, ip_address, host_address, agent" +
                        ") VALUES( " +
                        "*NM{user_id}, " +
                        "*NM{word_no}, " +
                        "*NM{situation_id}, *NM{perks_id}, *ST{serif_text}, " +
                        "*ST{created_at}, *ST{ip_address}, *ST{host_address}, *ST{user_agent}" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                //#region インポート：アイテム合成
                //DataTable ItemCreationTable = new DataTable();
                //ItemCreationTable = GetDataByMySQL("continue_create_items");
                //foreach (DataRow DataInRow in ItemCreationTable.Rows)
                //{
                //    CharaView.RowFilter = "entry_no=" + (int)DataInRow["user_id"];
                //    if (CharaView.Count > 0)
                //    {
                //        IsPassOK = true;
                //    }

                //    if (IsPassOK == false)
                //    {
                //        continue;
                //    }

                //    Sql = "INSERT INTO ts_continue_create_item ( " +
                //        "entry_no, creation_id, " +
                //        "base_item, " +
                //        "add_item1, " +
                //        "add_item2, " +
                //        "add_item3, " +
                //        "assist, " +
                //        "item_name, item_comment, " +
                //        "time, ip_address, host_address, agent" +
                //        ") VALUES( " +
                //        "*NM{user_id}, *NM{creation_id}, " +
                //        "*NM{base_item}, " +
                //        "*NM{add_item1}, " +
                //        "*NM{add_item2}, " +
                //        "*NM{add_item3}, " +
                //        "*NM{assist}, " +
                //        "*ST{item_name}, *ST{item_comment}, " +
                //        "*ST{created_at}, *ST{ip_address}, *ST{host_address}, *ST{user_agent}" +
                //        ")";
                //    Sql = LibSql.Replace(Sql, DataInRow);
                //    accessDB.ExecuteNonQuery(Sql);
                //}
                //#endregion

                #region インポート：プロフィール設定
                DataTable ProfileTable = new DataTable();
                ProfileTable = GetDataByMySQL("continue_profile_continueprofile");
                foreach (DataRow DataInRow in ProfileTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["user_id"];
                    NewCharaView.RowFilter = "user_id=" + (int)DataInRow["user_id"];
                    if (CharaView.Count > 0 || NewCharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    Sql = "INSERT INTO ts_continue_profile ( " +
                        "entry_no, " +
                        "nick_name, age, height, weight, profile, " +
                        "image_url, image_width, image_height, image_link_url, image_copyright, " +
                        "unique_name, account_status, " +
                        "time, ip_address, host_address, agent" +
                        ") VALUES( " +
                        "*NM{user_id}, " +
                        "*ST{nick_name}, *NM{age}, *NM{height}, *NM{weight}, *ST{profile}, " +
                        "*ST{image_url}, *NM{image_width}, *NM{image_height}, *ST{image_link_url}, *ST{image_copyright}, " +
                        "*ST{unique_name}, *NM{account_status}, " +
                        "*ST{created_at}, *ST{ip_address}, *ST{host_address}, *ST{user_agent}" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                #region インポート：アイコン設定
                DataTable IconTable = new DataTable();
                IconTable = GetDataByMySQL("continue_icon_continueicon");
                foreach (DataRow DataInRow in IconTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["user_id"];
                    NewCharaView.RowFilter = "user_id=" + (int)DataInRow["user_id"];
                    if (CharaView.Count > 0 || NewCharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    Sql = "INSERT INTO ts_continue_icon ( " +
                        "entry_no, " +
                        "icon_id, icon_url, icon_copyright, " +
                        "time, ip_address, host_address, agent" +
                        ") VALUES( " +
                        "*NM{user_id}, " +
                        "*NM{icon_id}, *ST{icon_url}, *ST{icon_copyright}, " +
                        "*ST{created_at}, *ST{ip_address}, *ST{host_address}, *ST{user_agent}" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                #region インポート：バザー購入設定
                DataTable BazzerTable = new DataTable();
                BazzerTable = GetDataByMySQL("bazzer_bazzer where status=1 and done=0");
                foreach (DataRow DataInRow in BazzerTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["buyer_id"];
                    if (CharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    //`bazzer_item_id`, `seller_no`, `have_no`, `it_id`, `it_created`, `price`, `set_count`

                    Sql = "INSERT INTO ts_continue_buy_bazzer ( " +
                        "entry_no, " +
                        "bazzer_id, seller_no, have_no, it_id, price, " +
                        "time, complete" +
                        ") VALUES( " +
                        "*NM{buyer_id}, " +
                        "*NM{id}, *NM{seller_id}, *NM{seller_having_no}, *NM{item_id}, *NM{price}, " +
                        "*ST{created_at}, 0" +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                #region インポート：バザー出品設定
                DataTable BazzerSellTable = new DataTable();
                BazzerSellTable = GetDataByMySQL("bazzer_bazzer where status<=1 and done=0");
                foreach (DataRow DataInRow in BazzerSellTable.Rows)
                {
                    CharaView.RowFilter = "entry_no=" + (int)DataInRow["seller_id"];
                    if (CharaView.Count > 0)
                    {
                        IsPassOK = true;
                    }

                    if (IsPassOK == false)
                    {
                        continue;
                    }

                    Sql = "INSERT INTO ts_continue_sell_bazzer ( " +
                        "id, entry_no, " +
                        "have_no, count, price" +
                        ") VALUES ( " +
                        "*NM{id}, *NM{seller_id}, " +
                        "*NM{seller_having_no}, 1, *NM{price} " +
                        ")";
                    Sql = LibSql.Replace(Sql, DataInRow);
                    accessDB.ExecuteNonQuery(Sql);
                }
                #endregion

                accessDB.Commit();
            }
            catch(Exception ex)
            {
                accessDB.Rollback();
            }
            finally
            {
                accessDB.Close();
            }

            LibInteger.SetNewSeed();

            LibPlayerMemo.LoadPlayerMemo();
            GrandBlazeStudio.Properties.Settings.Default.Seed = LibCommonLibrarySettings.Seed;
            GrandBlazeStudio.Properties.Settings.Default.Save();
        }

        private DataTable GetDataByMySQL(string TableName)
        {
            DataTable NewTable = new DataTable();

            mysqlDB.Open();
            NewTable = mysqlDB.GetTableData("SELECT * FROM " + TableName + ";");
            mysqlDB.Close();

            return NewTable;
        }
    }
}
