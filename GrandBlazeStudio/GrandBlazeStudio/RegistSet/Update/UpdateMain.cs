using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using System.IO;
using CommonLibrary.DataAccess;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.SpecialEntity;

namespace GrandBlazeStudio.RegistSet.Update
{
    class UpdateMain
    {
        public UpdateMain()
        {
            // コンストラクタ
        }

        public void Do(bool IsNewPlayerReset)
        {
            StringBuilder Sql = new StringBuilder();

            // データの読み込み
            LibDBLocal dba = new LibDBLocal();
			List<string> sqlValues = new List<string>();

            CharacterDataEntity.ts_character_listDataTable CharacterTable = new CharacterDataEntity.ts_character_listDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + CharacterTable.TableName);
                dba.Fill(Sql.ToString(), CharacterTable);
            }

            CharacterDataEntity.ts_character_moving_markDataTable CharacterMovingTable = new CharacterDataEntity.ts_character_moving_markDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + CharacterMovingTable.TableName);
                dba.Fill(Sql.ToString(), CharacterMovingTable);
            }

            CharacterDataEntity.ts_character_have_itemDataTable CharacterHavingItemTable = new CharacterDataEntity.ts_character_have_itemDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + CharacterHavingItemTable.TableName);
                dba.Fill(Sql.ToString(), CharacterHavingItemTable);
            }

            CharacterDataEntity.ts_character_battle_abilityDataTable CharacterBattleTable = new CharacterDataEntity.ts_character_battle_abilityDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + CharacterBattleTable.TableName);
                dba.Fill(Sql.ToString(), CharacterBattleTable);
            }

            CharacterDataEntity.ts_character_install_levelDataTable CharacterInstallTable = new CharacterDataEntity.ts_character_install_levelDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + CharacterInstallTable.TableName);
                dba.Fill(Sql.ToString(), CharacterInstallTable);
            }

            CharacterDataEntity.ts_character_have_skillDataTable CharacterHavingSkillTable = new CharacterDataEntity.ts_character_have_skillDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + CharacterHavingSkillTable.TableName);
                dba.Fill(Sql.ToString(), CharacterHavingSkillTable);
            }

            CharacterDataEntity.ts_character_have_key_itemDataTable CharacterKeyItemTable = new CharacterDataEntity.ts_character_have_key_itemDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + CharacterKeyItemTable.TableName);
                dba.Fill(Sql.ToString(), CharacterKeyItemTable);
            }

            CharacterDataEntity.ts_character_questDataTable CharacterQuestTable = new CharacterDataEntity.ts_character_questDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + CharacterQuestTable.TableName);
                dba.Fill(Sql.ToString(), CharacterQuestTable);
            }

            CharacterDataEntity.ts_character_actionDataTable CharacterActionTable = new CharacterDataEntity.ts_character_actionDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + CharacterActionTable.TableName);
                dba.Fill(Sql.ToString(), CharacterActionTable);
            }

            CharacterDataEntity.ts_character_iconDataTable CharacterIconTable = new CharacterDataEntity.ts_character_iconDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + CharacterIconTable.TableName);
                dba.Fill(Sql.ToString(), CharacterIconTable);
            }

            CharacterDataEntity.ts_character_serifDataTable CharacterSerifTable = new CharacterDataEntity.ts_character_serifDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + CharacterSerifTable.TableName);
                dba.Fill(Sql.ToString(), CharacterSerifTable);
            }

            PartySettingEntity.ts_party_listDataTable PartyTable = new PartySettingEntity.ts_party_listDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + PartyTable.TableName);
                dba.Fill(Sql.ToString(), PartyTable);
            }

            PartySettingEntity.ts_character_belong_partyDataTable PartyBelongTable = new PartySettingEntity.ts_character_belong_partyDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + PartyBelongTable.TableName);
                dba.Fill(Sql.ToString(), PartyBelongTable);
            }

            DeletePlayerEntity.ts_delete_playersDataTable DeletePlayerTable = new DeletePlayerEntity.ts_delete_playersDataTable();
            {
                Sql.Clear();
                Sql.Append("select * from " + DeletePlayerTable.TableName + " where entry_no>0 and update_cnt=" + GrandBlazeStudio.Properties.Settings.Default.UpdateCnt);
                dba.Fill(Sql.ToString(), DeletePlayerTable);
            }

            dba.Close();

            LibDBWebServer dbm = new LibDBWebServer();
            try
            {
                dbm.Open();

                dbm.BeginTransaction();

                dbm.ExecuteNonQuery("SET FOREIGN_KEY_CHECKS=0;");
                if (IsNewPlayerReset)
                {
                    dbm.ExecuteNonQuery("TRUNCATE TABLE newgame_newgame");
                    dbm.ExecuteNonQuery("TRUNCATE TABLE character_character");
                }
                else
                {
                    //dbm.ExecuteNonQuery("DELETE FROM newgame_newgame where activate=1");
                    dbm.ExecuteNonQuery("DELETE FROM character_character where new_play>0");
                }

                //dbm.ExecuteNonQuery("DELETE FROM bazzer_items WHERE sell_comp=1");

                dbm.ExecuteNonQuery("TRUNCATE TABLE race_race");
                dbm.ExecuteNonQuery("TRUNCATE TABLE install_install");
                dbm.ExecuteNonQuery("TRUNCATE TABLE install_installskill");
                dbm.ExecuteNonQuery("TRUNCATE TABLE quest_mark");
                dbm.ExecuteNonQuery("TRUNCATE TABLE quest_quest");
                dbm.ExecuteNonQuery("TRUNCATE TABLE quest_fieldtype");
                dbm.ExecuteNonQuery("TRUNCATE TABLE quest_weather");
                dbm.ExecuteNonQuery("TRUNCATE TABLE quest_markweather");
                dbm.ExecuteNonQuery("TRUNCATE TABLE character_charactermovingmark");
                dbm.ExecuteNonQuery("TRUNCATE TABLE party_party");
                dbm.ExecuteNonQuery("TRUNCATE TABLE party_partybelong");
                dbm.ExecuteNonQuery("TRUNCATE TABLE item_item");
                dbm.ExecuteNonQuery("TRUNCATE TABLE item_itemtype");
                dbm.ExecuteNonQuery("TRUNCATE TABLE battleaction_battletarget");
                dbm.ExecuteNonQuery("TRUNCATE TABLE battleaction_battleaction");
                dbm.ExecuteNonQuery("TRUNCATE TABLE skill_skill");
                dbm.ExecuteNonQuery("TRUNCATE TABLE skill_skillcategory");
                dbm.ExecuteNonQuery("TRUNCATE TABLE character_characterbattle");
                dbm.ExecuteNonQuery("TRUNCATE TABLE character_characterinstall");
                dbm.ExecuteNonQuery("TRUNCATE TABLE skill_skillget");
                dbm.ExecuteNonQuery("TRUNCATE TABLE skill_skillgetlist");
                dbm.ExecuteNonQuery("TRUNCATE TABLE character_characterhavingitem");
                dbm.ExecuteNonQuery("TRUNCATE TABLE character_characterhavingskill");
                dbm.ExecuteNonQuery("TRUNCATE TABLE situation_situation");
                //dbm.ExecuteNonQuery("TRUNCATE TABLE keyitems");
                dbm.ExecuteNonQuery("TRUNCATE TABLE character_characterquest");
                dbm.ExecuteNonQuery("TRUNCATE TABLE character_characterkeyitem");
                dbm.ExecuteNonQuery("TRUNCATE TABLE character_characteraction");
                dbm.ExecuteNonQuery("TRUNCATE TABLE character_charactericon");
                dbm.ExecuteNonQuery("TRUNCATE TABLE character_characterserif");

                if (IsNewPlayerReset)
                {
                    dbm.ExecuteNonQuery("TRUNCATE TABLE continue_complete_continuecomplete");
                    dbm.ExecuteNonQuery("TRUNCATE TABLE continue_battleaction_continuebattleaction");
                    dbm.ExecuteNonQuery("ALTER TABLE continue_battleaction_continuebattleaction AUTO_INCREMENT = 1");
                    //dbm.ExecuteNonQuery("TRUNCATE TABLE continue_buy_bazzers");
                    //dbm.ExecuteNonQuery("TRUNCATE TABLE continue_create_items");
                    dbm.ExecuteNonQuery("TRUNCATE TABLE continue_equip_continueequip");
                    dbm.ExecuteNonQuery("TRUNCATE TABLE continue_icon_continueicon");
                    dbm.ExecuteNonQuery("TRUNCATE TABLE continue_main_continuemain");
                    dbm.ExecuteNonQuery("TRUNCATE TABLE continue_message_continuemessage");
                    dbm.ExecuteNonQuery("ALTER TABLE continue_message_continuemessage AUTO_INCREMENT = 1");
                    dbm.ExecuteNonQuery("TRUNCATE TABLE continue_serif_continueserif");
                    dbm.ExecuteNonQuery("ALTER TABLE continue_serif_continueserif AUTO_INCREMENT = 1");
                    dbm.ExecuteNonQuery("TRUNCATE TABLE continue_profile_continueprofile");
                    dbm.ExecuteNonQuery("TRUNCATE TABLE continue_trade_continueshopping");
                    dbm.ExecuteNonQuery("TRUNCATE TABLE continue_trade_continuetrade");
                    dbm.ExecuteNonQuery("ALTER TABLE continue_trade_continuetrade AUTO_INCREMENT = 1");
                }

                //dbm.ExecuteNonQuery("UPDATE bazzer_bazzer SET done=1 WHERE status=1 and done=0");

                foreach (DeletePlayerEntity.ts_delete_playersRow deleteRow in DeletePlayerTable.Rows)
                {
                    Sql.Clear();
                    Sql.AppendLine("delete from account_instantmessage");
                    Sql.AppendLine("where from_user_id=" + deleteRow.entry_no + " or user_id=" + deleteRow.entry_no);
                    dbm.ExecuteNonQuery(Sql.ToString());

                    Sql.Clear();
                    Sql.AppendLine("delete from account_userprofile");
                    Sql.AppendLine("where user_id=" + deleteRow.entry_no);
                    dbm.ExecuteNonQuery(Sql.ToString());

                    Sql.Clear();
                    Sql.AppendLine("delete from auth_user");
                    Sql.AppendLine("where id=" + deleteRow.entry_no);
                    dbm.ExecuteNonQuery(Sql.ToString());
                }
				
				sqlValues.Clear();
				Sql.Clear();
				Sql.AppendLine("insert into race_race values ");
                foreach (HumanRaceEntity.mt_race_listRow raceRow in LibRace.Entity.mt_race_list)
                {
					sqlValues.Add("( " + raceRow.race_id + "," + LibSql.EscapeString(raceRow.race_name) + "," + raceRow.up_hp + "," + raceRow.up_mp + "," + raceRow.up_str + "," + raceRow.up_agi + "," + raceRow.up_mag + "," + raceRow.up_unq + ")");
                }
				Sql.AppendLine(string.Join(",",sqlValues));
				dbm.ExecuteNonQuery(Sql.ToString());
				
				sqlValues.Clear();
				Sql.Clear();
				Sql.AppendLine("insert into install_install values ");
                foreach (InstallDataEntity.mt_install_class_listRow installRow in LibInstall.Entity.mt_install_class_list)
                {
					sqlValues.Add("( " + installRow.install_id + "," + LibSql.EscapeString(installRow.classname) + "," + installRow.up_hp + "," + installRow.up_mp + "," + installRow.up_str + "," + installRow.up_agi + "," + installRow.up_mag + "," + installRow.up_unq + "," + LibSql.EscapeString(installRow.class_comment) + ")");
                }
				Sql.AppendLine(string.Join(",",sqlValues));
				dbm.ExecuteNonQuery(Sql.ToString());
				
				sqlValues.Clear();
				Sql.Clear();
				Sql.AppendLine("insert into quest_quest values ");
                foreach (QuestDataEntity.quest_listRow questRow in LibQuest.Entity.mt_quest_list)
                {
                    if (!questRow.valid_fg) { continue; }

                    sqlValues.Add("( " + questRow.quest_id + "," + LibSql.EscapeString(questRow.quest_name) + "," + LibSql.EscapeString(questRow.quest_client) + "," + questRow.quest_type + "," + questRow.pick_level + "," + questRow.key_item_id + "," + questRow.offer_quest_id + "," + questRow.comp_quest_id + "," + questRow.sp_level + "," + questRow.class_id + "," + questRow.class_level + "," + LibSql.EscapeBoolChange(questRow.hide_fg) + "," + questRow.bc_count + ")");
                }

                // デフォルトデータ追加
				sqlValues.Add(" (0,'メンバーの決定に従う','',0,0,0,0,0,0,0,0,0,0)");

                // イベントの発生
                if (LibQuest.OfficialEventID > Status.OfficialEvent.NoEvent)
                {
                    sqlValues.Add("(-1,0," + LibSql.EscapeString(LibQuest.GetOfficialEventName()) + ",0,0)");
                }
				Sql.AppendLine(string.Join(",",sqlValues));
				dbm.ExecuteNonQuery(Sql.ToString());
				
				sqlValues.Clear();
				Sql.Clear();
				Sql.AppendLine("insert into quest_mark values ");
                foreach (QuestDataEntity.mt_mark_listRow markRow in LibQuest.Entity.mt_mark_list)
                {
					sqlValues.Add("( " + markRow.mark_id + "," + markRow.quest_id + "," + LibSql.EscapeString(markRow.mark_name) + "," + markRow.field_type + "," + LibSql.EscapeBoolChange(markRow.hide_mark) + ")");
                }
				Sql.AppendLine(string.Join(",",sqlValues));
				dbm.ExecuteNonQuery(Sql.ToString());
				
				sqlValues.Clear();
				Sql.Clear();
				Sql.AppendLine("insert into quest_fieldtype values ");
                foreach (FieldDataEntity.mt_field_type_listRow fieldRow in LibField.Entity.mt_field_type_list)
                {
					sqlValues.Add("( " + fieldRow.field_id + "," + LibSql.EscapeString(fieldRow.field_name) + ")");
                }
				Sql.AppendLine(string.Join(",",sqlValues));
				dbm.ExecuteNonQuery(Sql.ToString());
				
				sqlValues.Clear();
				Sql.Clear();
				Sql.AppendLine("insert into item_item values ");
                foreach (CommonItemEntity.item_listRow itemRow in LibItem.Entity.item_list)
                {
					sqlValues.Add("( " + itemRow.it_num + "," + LibSql.EscapeString(itemRow.it_name) + "," + itemRow.it_physics + "," + itemRow.it_sorcery + "," + itemRow.it_physics_parry + "," + itemRow.it_sorcery_parry + "," + itemRow.it_critical + "," + itemRow.it_metal + "," + itemRow.it_charge + "," + itemRow.it_range + "," + itemRow.it_type + "," + itemRow.it_sub_category + "," + itemRow.it_attack_type + "," + LibSql.EscapeString(LibComment.Item(itemRow.it_num, itemRow.it_creatable, "")) + "," + itemRow.it_fire + "," + itemRow.it_freeze + "," + itemRow.it_air + "," + itemRow.it_earth + "," + itemRow.it_water + "," + itemRow.it_thunder + "," + itemRow.it_holy + "," + itemRow.it_dark + "," + itemRow.it_slash + "," + itemRow.it_pierce + "," + itemRow.it_strike + "," + itemRow.it_break + "," + itemRow.it_ok_sex + "," + itemRow.it_ok_race + "," + LibSql.EscapeBoolChange(itemRow.it_both_hand) + "," + itemRow.it_use_item + "," + LibSql.EscapeString(itemRow.it_equip_install) + "," + itemRow.it_equip_parts + "," + LibSql.EscapeBoolChange(itemRow.it_rare) + "," + LibSql.EscapeBoolChange(itemRow.it_bind) + "," + LibSql.EscapeBoolChange(itemRow.it_quest) + "," + itemRow.it_shop + "," + itemRow.it_equip_level + "," + itemRow.it_target_area + "," + itemRow.it_price + "," + itemRow.it_seller + "," + itemRow.it_stack + ")");
                }
				Sql.AppendLine(string.Join(",",sqlValues));
				dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into item_itemtype values ");
                foreach (ItemTypeEntity.mt_item_typeRow itemTypeRow in LibItemType.Entity.mt_item_type)
                {
                    sqlValues.Add("( " + itemTypeRow.type_id + "," + LibSql.EscapeString(itemTypeRow.type) + "," + itemTypeRow.skill_id + "," + itemTypeRow.categ_div + "," + LibSql.EscapeBoolChange(itemTypeRow.database_view) + "," + itemTypeRow.stack_count + ")");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into battleaction_battletarget values ");
                foreach (ActionDataEntity.mt_target_listRow targetRow in LibAction.Entity.mt_target_list)
                {
                    if (!targetRow.upload_fg) { continue; }

                    sqlValues.Add("( " + targetRow.target_id + "," + LibSql.EscapeString(targetRow.target_act_name) + "," + LibSql.EscapeString(targetRow.target_act_comment) + "," + targetRow.view_no + "," + targetRow.target_type + "," + targetRow.target_no + ")");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into battleaction_battleaction values ");
                foreach (ActionDataEntity.mt_action_listRow actionRow in LibAction.Entity.mt_action_list)
                {
                    if (!actionRow.view_fg) { continue; }

                    sqlValues.Add("( " + actionRow.action_id + "," + LibSql.EscapeString(actionRow.action_name) + "," + LibSql.EscapeString(actionRow.action_comment) + ")");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into skill_skill values ");
                foreach (CommonSkillEntity.skill_listRow skillRow in LibSkill.Entity.skill_list)
                {
                    sqlValues.Add("( " + skillRow.sk_id + "," + LibSql.EscapeString(skillRow.sk_name) + "," + skillRow.sk_mp + "," + skillRow.sk_tp + "," + skillRow.sk_power + "," + skillRow.sk_damage_rate + "," + skillRow.sk_plus_score + "," + skillRow.sk_hit + "," + skillRow.sk_critical + "," + skillRow.sk_critical_type + "," + skillRow.sk_round + "," + skillRow.sk_range + "," + skillRow.sk_charge + "," + skillRow.sk_hate + "," + skillRow.sk_vhate + "," + skillRow.sk_dhate + "," + LibSql.EscapeBoolChange(skillRow.sk_antiair) + "," + skillRow.sk_target_restrict + "," + skillRow.sk_use_limit + "," + skillRow.sk_fire + "," + skillRow.sk_freeze + "," + skillRow.sk_air + "," + skillRow.sk_earth + "," + skillRow.sk_water + "," + skillRow.sk_thunder + "," + skillRow.sk_holy + "," + skillRow.sk_dark + "," + skillRow.sk_slash + "," + skillRow.sk_pierce + "," + skillRow.sk_strike + "," + skillRow.sk_break + "," + LibSql.EscapeString(skillRow.sk_effect) + "," + skillRow.sk_type + "," + skillRow.sk_atype + "," + skillRow.sk_damage_type + "," + skillRow.sk_target_area + "," + LibSql.EscapeString(LibComment.Skill(skillRow.sk_id)) + "," + skillRow.sk_target_party + "," + skillRow.sk_arts_category + ")");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into skill_skillcategory values ");
                foreach (SkillTypeEntity.mt_skill_categoryRow skillCategoryRow in LibSkillType.Entity.mt_skill_category)
                {
                    sqlValues.Add("( " + skillCategoryRow.category_id + "," + LibSql.EscapeString(skillCategoryRow.category_name) + "," + skillCategoryRow.type_id + ")");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into skill_skillgetlist values ");
                foreach (SkillGetEntity.mt_skill_get_listRow skillGetRow in LibSkill.GetEntity.mt_skill_get_list)
                {
                    sqlValues.Add("( " + skillGetRow.perks_id + "," + skillGetRow.tm_level + "," + skillGetRow.tm_race + "," + LibSql.EscapeString(LibComment.SkillCondition(skillGetRow.perks_id)) + "," + skillGetRow.perks_id + ")");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into install_installskill values ");
                int i = 1;
                foreach (InstallDataEntity.mt_install_class_skillRow installSkillRow in LibInstall.Entity.mt_install_class_skill)
                {
                    sqlValues.Add("( " + i.ToString() + "," + installSkillRow.install_id + "," + installSkillRow.install_level + "," + installSkillRow.perks_id + "," + installSkillRow.only_mode + ")");
                    i++;
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                //foreach (KeyItemEntity.mt_key_item_listRow keyRow in LibKeyItem.Entity.mt_key_item_list)
                //{
                //    Sql.Clear();
                //    Sql.AppendLine("insert into keyitems");
                //    Sql.AppendLine("values ( " + keyRow.key_id + "," + LibSql.EscapeString(keyRow.keyitem_name) + "," + LibSql.EscapeString(keyRow.keyitem_comment) + "," + keyRow.key_type + ")");
                //    dbm.ExecuteNonQuery(Sql.ToString());
                //}

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into situation_situation values ");
                foreach (SituationDataEntity.situation_listRow situationRow in LibSituation.Entity.mt_situation_list)
                {
                    sqlValues.Add("( " + situationRow.situation_no + "," + LibSql.EscapeString(situationRow.situation_text) + "," + LibSql.EscapeString(situationRow.situation_comment) + ")");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into quest_weather values ");
                foreach (WeatherDataEntity.mt_weather_listRow weatherRow in LibWeather.Entity.mt_weather_list)
                {
                    sqlValues.Add("( " + weatherRow.weather_id + "," + LibSql.EscapeString(weatherRow.weather_name) + ")");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into quest_markweather (mark_id, count, weather_id) values ");
                foreach (QuestMarkWeatherEntity.ts_mark_weather_scheduleRow weatherRow in LibQuest.WeatherEntity.ts_mark_weather_schedule)
                {
                    if (weatherRow.count_id < GrandBlazeStudio.Properties.Settings.Default.UpdateCnt)
                    {
                        continue;
                    }
                    sqlValues.Add("( " + weatherRow.mark_id + "," + weatherRow.count_id + "," + weatherRow.weather_id + ")");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into character_character");
                Sql.AppendLine("(user_id, continue_cnt, continue_bonus, account_status, new_play, last_update, new_gamer, character_name, image_url, image_width, image_height, image_link_url, image_copyright, nick_name, race_id, guardian_id, nation_id, have_money, blaze_chip, age, sex, max_item, max_bazzeritem, profile, unique_name, height, weight, familiar_name, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_listRow charaRow in CharacterTable)
                {
                    sqlValues.Add("( " + charaRow.entry_no + "," + charaRow.continue_cnt + "," + charaRow.continue_bonus + "," + charaRow.account_status + "," + charaRow.new_play + "," + LibSql.EscapeString(charaRow.last_update.ToShortDateString()) + "," + LibSql.EscapeBoolChange(charaRow.new_gamer) + "," + LibSql.EscapeString(charaRow.character_name) + "," + LibSql.EscapeString(charaRow.image_url) + "," + charaRow.image_width + "," + charaRow.image_height + "," + LibSql.EscapeString(charaRow.image_link_url) + "," + LibSql.EscapeString(charaRow.image_copyright) + "," + LibSql.EscapeString(charaRow.nick_name) + "," + charaRow.race_id + "," + charaRow.guardian_id + "," + charaRow.nation_id + "," + charaRow.have_money + "," + charaRow.blaze_chip + "," + charaRow.age + "," + charaRow.sex + "," + charaRow.max_item + "," + charaRow.max_bazzeritem + "," + LibSql.EscapeString(charaRow.profile) + "," + LibSql.EscapeString(charaRow.unique_name) + "," + charaRow.height + "," + charaRow.weight + "," + LibSql.EscapeString(charaRow.familiar_name) + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into character_charactermovingmark");
                Sql.AppendLine("(user_id, mark_id, instance, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_moving_markRow charaRow in CharacterMovingTable)
                {
                    sqlValues.Add("( " + charaRow.entry_no + "," + charaRow.mark_id + "," + LibSql.EscapeBoolChange(charaRow.instance) + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into character_characterhavingitem");
                Sql.AppendLine("(user_id, box_type, have_no, item_v_id, it_box_count, it_box_baz_count, equip_spot, is_new, created, it_name, it_comment, it_effect, it_price, it_seller, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_have_itemRow haveItemRow in CharacterHavingItemTable)
                {
                    sqlValues.Add("( " + haveItemRow.entry_no + "," + haveItemRow.box_type + "," + haveItemRow.have_no + "," + haveItemRow.it_num + "," + haveItemRow.it_box_count + "," + haveItemRow.it_box_baz_count + "," + haveItemRow.equip_spot + "," + LibSql.EscapeBoolChange(haveItemRow._new) + "," + LibSql.EscapeBoolChange(haveItemRow.created) + "," + LibSql.EscapeString(haveItemRow.it_name) + "," + LibSql.EscapeString(haveItemRow.it_comment) + "," + LibSql.EscapeString(haveItemRow.it_effect) + "," + haveItemRow.it_price + "," + haveItemRow.it_seller + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into character_characterbattle");
                Sql.AppendLine("(user_id, install, second_install, formation, level, exp, hp, mp, exp_unit, levelup_point, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_battle_abilityRow battleRow in CharacterBattleTable)
                {
                    sqlValues.Add("( " + battleRow.entry_no + "," + battleRow.install + "," + battleRow.second_install + "," + battleRow.formation + "," + battleRow.level + "," + battleRow.exp + "," + battleRow.hp + "," + battleRow.mp + "," + battleRow.exp_unit + "," + battleRow.levelup_point + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into character_characterinstall");
                Sql.AppendLine("(user_id, install_id, level, exp, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_install_levelRow installRow in CharacterInstallTable)
                {
                    sqlValues.Add("( " + installRow.entry_no + "," + installRow.install_id + "," + installRow.level + "," + installRow.exp + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into character_characterhavingskill");
                Sql.AppendLine("(user_id, skill_id, is_new, sc_flg, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_have_skillRow haveSkillRow in CharacterHavingSkillTable)
                {
                    sqlValues.Add("(" + haveSkillRow.entry_no + "," + haveSkillRow.sk_num + "," + LibSql.EscapeBoolChange(haveSkillRow._new) + "," + LibSql.EscapeBoolChange(haveSkillRow.sc_flg) + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into character_characterquest");
                Sql.AppendLine("(user_id, quest_id, clear_fg, step, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_questRow questRow in CharacterQuestTable)
                {
                    sqlValues.Add("( " + questRow.entry_no + "," + questRow.quest_id + "," + LibSql.EscapeBoolChange(questRow.clear_fg) + "," + questRow.quest_step + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into character_characterkeyitem");
                Sql.AppendLine("(user_id, keyitem_id, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_have_mt_key_item_listRow keyRow in CharacterKeyItemTable)
                {
                    sqlValues.Add("( " + keyRow.entry_no + "," + keyRow.key_item_id + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into character_characteraction");
                Sql.AppendLine("(user_id, action_no, action_target, action, perks_id, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_actionRow actionRow in CharacterActionTable)
                {
                    sqlValues.Add("( " + actionRow.entry_no + "," + actionRow.action_no + "," + actionRow.action_target + "," + actionRow.action + "," + actionRow.perks_id + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into character_charactericon");
                Sql.AppendLine("(user_id, icon_id, icon_url, icon_copyright, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_iconRow iconRow in CharacterIconTable)
                {
                    sqlValues.Add("( " + iconRow.entry_no + "," + iconRow.icon_id + "," + LibSql.EscapeString(iconRow.icon_url) + "," + LibSql.EscapeString(iconRow.icon_copyright) + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into character_characterserif");
                Sql.AppendLine("(user_id, word_no, situation_id, serif_text, perks_id, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_serifRow serifRow in CharacterSerifTable)
                {
                    sqlValues.Add("( " + serifRow.entry_no + "," + serifRow.serif_no + "," + serifRow.situation + "," + LibSql.EscapeString(serifRow.serif_text) + "," + serifRow.perks_id + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into party_party values ");
                foreach (PartySettingEntity.ts_party_listRow partyRow in PartyTable)
                {
                    sqlValues.Add("( " + partyRow.party_no + "," + LibSql.EscapeString(partyRow.pt_name) + "," + partyRow.mark_id + ")");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into party_partybelong");
                Sql.AppendLine("(user_id, party_id, reader, mark_id, created_at, updated_at) values ");
                foreach (PartySettingEntity.ts_character_belong_partyRow partyBelongRow in PartyBelongTable)
                {
                    sqlValues.Add("( " + partyBelongRow.entry_no + "," + partyBelongRow.party_no + "," + LibSql.EscapeBoolChange(partyBelongRow.reader) + "," + partyBelongRow.mark_id + ",now(),now())");
                }
                Sql.AppendLine(string.Join(",", sqlValues));
                dbm.ExecuteNonQuery(Sql.ToString());

                // 各登録者別に（全員のをやると時間がかかるので、対象者のみ）、習得可能スキルをリストして登録
                sqlValues.Clear();
                Sql.Clear();
                Sql.AppendLine("insert into  skill_skillget");
                Sql.AppendLine("(user_id, skill_id, created_at, updated_at) values ");
                foreach (CharacterDataEntity.ts_character_battle_abilityRow battleRow in CharacterBattleTable)
                {
                    if (battleRow.levelup_point > 0)
                    {
                        // キャラデータ取得
                        LibPlayer Chara = new LibPlayer(Status.Belong.Friend, battleRow.entry_no);

                        foreach (SkillGetEntity.mt_skill_get_listRow skillGetRow in LibSkill.GetEntity.mt_skill_get_list)
                        {
                            // 習得チェック

                            // レベルチェック
                            if (Chara.Level < skillGetRow.tm_level)
                            {
                                continue;
                            }

                            // インストールチェック
                            if (skillGetRow.tm_install > 0 && skillGetRow.tm_install_level > 0)
                            {
                                CommonUnitDataEntity.install_level_listRow characterIntallRow = Chara.InstallClassList.FindByinstall_id(skillGetRow.tm_install);
                                if (characterIntallRow == null || characterIntallRow.level < skillGetRow.tm_install_level)
                                {
                                    continue;
                                }
                            }

                            // ステータスチェック
                            if (Chara.STRBase < skillGetRow.tm_str)
                            {
                                continue;
                            }
                            if (Chara.AGIBase < skillGetRow.tm_agi)
                            {
                                continue;
                            }
                            if (Chara.MAGBase < skillGetRow.tm_mag)
                            {
                                continue;
                            }
                            if (Chara.UNQBase < skillGetRow.tm_unq)
                            {
                                continue;
                            }

                            // 種族
                            if (skillGetRow.tm_race > 0 && Chara.Race != skillGetRow.tm_race)
                            {
                                continue;
                            }

                            // 守護者
                            if (skillGetRow.tm_guardian > 0 && Chara.GuardianInt != skillGetRow.tm_guardian)
                            {
                                continue;
                            }

                            // 所持スキル
                            if (skillGetRow.tm_base_skill > 0)
                            {
                                if (!Chara.CheckHaveSkill(skillGetRow.tm_base_skill))
                                {
                                    continue;
                                }
                            }

                            // 登録実行
                            sqlValues.Add("( " + battleRow.entry_no + "," + skillGetRow.perks_id + ",now(),now())");
                        }
                    }
                }
                if (sqlValues.Count > 0)
                {
                    Sql.AppendLine(string.Join(",", sqlValues));
                    dbm.ExecuteNonQuery(Sql.ToString());
                }

                dbm.ExecuteNonQuery("SET FOREIGN_KEY_CHECKS=1;");

                dbm.Commit();
            }
            finally
            {
                dbm.Close();
            }
        }
    }
}
