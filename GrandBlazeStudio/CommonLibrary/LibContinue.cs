using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// åpë±ìoò^ÉfÅ[É^ä«óùÉNÉâÉX
    /// </summary>
    public class LibContinue
    {
        public ContinueDataEntity Entity;

        public LibContinue()
        {
            Entity = new ContinueDataEntity();
            using (LibDBLocal dba = new LibDBLocal())
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <ts_continue_main>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("party_secession, ");
                SelSql.AppendLine("pcm_add_1, ");
                SelSql.AppendLine("pcm_add_2, ");
                SelSql.AppendLine("pcm_add_3, ");
                SelSql.AppendLine("pcm_add_4, ");
                SelSql.AppendLine("pcm_add_5, ");
                SelSql.AppendLine("party_hope, ");
                SelSql.AppendLine("option_comes_no, ");
                SelSql.AppendLine("party_name, ");
                SelSql.AppendLine("quest_id, ");
                SelSql.AppendLine("mark_id, ");
                SelSql.AppendLine("use_item_1, ");
                SelSql.AppendLine("use_item_2, ");
                SelSql.AppendLine("use_item_3, ");
                SelSql.AppendLine("getting_private_skill, ");
                SelSql.AppendLine("use_item_1_message, ");
                SelSql.AppendLine("use_item_2_message, ");
                SelSql.AppendLine("use_item_3_message, ");
                SelSql.AppendLine("time, ");
                SelSql.AppendLine("ip_address, ");
                SelSql.AppendLine("host_address, ");
                SelSql.AppendLine("agent, ");
                SelSql.AppendLine("delete_fg ");
                SelSql.AppendLine("FROM ts_continue_main");
                SelSql.AppendLine("WHERE delete_fg=0");
                SelSql.AppendLine("ORDER BY entry_no");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_main);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_complete>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("category, ");
                SelSql.AppendLine("time ");
                SelSql.AppendLine("FROM ts_continue_complete");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_complete);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_shopping>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("shop_act, ");
                SelSql.AppendLine("shopping_no, ");
                SelSql.AppendLine("item_no, ");
                SelSql.AppendLine("item_count, ");
                SelSql.AppendLine("time, ");
                SelSql.AppendLine("ip_address, ");
                SelSql.AppendLine("host_address, ");
                SelSql.AppendLine("agent ");
                SelSql.AppendLine("FROM ts_continue_shopping");
                SelSql.AppendLine("ORDER BY entry_no,shop_act,shopping_no");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_shopping);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_battle_preparation>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("install, ");
                SelSql.AppendLine("secondary_install, ");
                SelSql.AppendLine("equip_main, ");
                SelSql.AppendLine("equip_sub, ");
                SelSql.AppendLine("equip_head, ");
                SelSql.AppendLine("equip_body, ");
                SelSql.AppendLine("equip_acce1, ");
                SelSql.AppendLine("equip_acce2, ");
                SelSql.AppendLine("formation, ");
                SelSql.AppendLine("time, ");
                SelSql.AppendLine("ip_address, ");
                SelSql.AppendLine("host_address, ");
                SelSql.AppendLine("agent ");
                SelSql.AppendLine("FROM ts_continue_battle_preparation");
                SelSql.AppendLine("ORDER BY entry_no");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_battle_preparation);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_message>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("mes_no, ");
                SelSql.AppendLine("message_target, ");
                SelSql.AppendLine("message_entry, ");
                SelSql.AppendLine("message_body, ");
                SelSql.AppendLine("time, ");
                SelSql.AppendLine("ip_address, ");
                SelSql.AppendLine("host_address, ");
                SelSql.AppendLine("agent ");
                SelSql.AppendLine("FROM ts_continue_message");
                SelSql.AppendLine("ORDER BY entry_no,mes_no");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_message);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_trade>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("trade_no, ");
                SelSql.AppendLine("trade_entry, ");
                SelSql.AppendLine("trade_item_no, ");
                SelSql.AppendLine("trade_number, ");
                SelSql.AppendLine("trade_message, ");
                SelSql.AppendLine("trade_speed, ");
                SelSql.AppendLine("time, ");
                SelSql.AppendLine("ip_address, ");
                SelSql.AppendLine("host_address, ");
                SelSql.AppendLine("agent ");
                SelSql.AppendLine("FROM ts_continue_trade");
                SelSql.AppendLine("ORDER BY entry_no,trade_no");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_trade);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_battle_action>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("action_no, ");
                SelSql.AppendLine("action_target, ");
                SelSql.AppendLine("action, ");
                SelSql.AppendLine("perks_id, ");
                SelSql.AppendLine("time, ");
                SelSql.AppendLine("ip_address, ");
                SelSql.AppendLine("host_address, ");
                SelSql.AppendLine("agent ");
                SelSql.AppendLine("FROM ts_continue_battle_action");
                SelSql.AppendLine("ORDER BY entry_no,action_no");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_battle_action);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_serif>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("word_no, ");
                SelSql.AppendLine("situation, ");
                SelSql.AppendLine("perks_id, ");
                SelSql.AppendLine("serif_text, ");
                SelSql.AppendLine("time, ");
                SelSql.AppendLine("ip_address, ");
                SelSql.AppendLine("host_address, ");
                SelSql.AppendLine("agent ");
                SelSql.AppendLine("FROM ts_continue_serif");
                SelSql.AppendLine("ORDER BY entry_no,word_no");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_serif);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_profile>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("nick_name, ");
                SelSql.AppendLine("age, ");
                SelSql.AppendLine("height, ");
                SelSql.AppendLine("weight, ");
                SelSql.AppendLine("profile, ");
                SelSql.AppendLine("image_url, ");
                SelSql.AppendLine("image_width, ");
                SelSql.AppendLine("image_height, ");
                SelSql.AppendLine("image_link_url, ");
                SelSql.AppendLine("image_copyright, ");
                SelSql.AppendLine("unique_name, ");
                SelSql.AppendLine("account_status, ");
                SelSql.AppendLine("time, ");
                SelSql.AppendLine("ip_address, ");
                SelSql.AppendLine("host_address, ");
                SelSql.AppendLine("agent ");
                SelSql.AppendLine("FROM ts_continue_profile");
                SelSql.AppendLine("ORDER BY entry_no");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_profile);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_icon>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("icon_id, ");
                SelSql.AppendLine("icon_url, ");
                SelSql.AppendLine("icon_copyright, ");
                SelSql.AppendLine("time, ");
                SelSql.AppendLine("ip_address, ");
                SelSql.AppendLine("host_address, ");
                SelSql.AppendLine("agent ");
                SelSql.AppendLine("FROM ts_continue_icon");
                SelSql.AppendLine("ORDER BY entry_no,icon_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_icon);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_buy_bazzer>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("bazzer_id, ");
                SelSql.AppendLine("seller_no, ");
                SelSql.AppendLine("have_no, ");
                SelSql.AppendLine("it_id, ");
                SelSql.AppendLine("price, ");
                SelSql.AppendLine("time, ");
                SelSql.AppendLine("complete ");
                SelSql.AppendLine("FROM ts_continue_buy_bazzer");
                SelSql.AppendLine("ORDER BY entry_no,time");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_buy_bazzer);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_sell_bazzer>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("id, ");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("have_no, ");
                SelSql.AppendLine("[count], ");
                SelSql.AppendLine("[price] ");
                SelSql.AppendLine("FROM ts_continue_sell_bazzer");
                SelSql.AppendLine("ORDER BY id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_sell_bazzer);

                SelSql = new StringBuilder();
                #region TABLE <ts_continue_official_event>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("groom, ");
                SelSql.AppendLine("bride ");
                SelSql.AppendLine("FROM ts_continue_official_event");
                SelSql.AppendLine("ORDER BY entry_no,groom,bride");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_official_event);
            }
        }


        public void UpdateBazzerComplete(int EntryNo, int TargetNo, int BzId)
        {
            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                dba.ExecuteNonQuery("UPDATE ts_continue_buy_bazzer SET complete=1 WHERE entry_no=" + EntryNo + " and seller_no=" + TargetNo + " and bazzer_id=" + BzId);

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

        public void ReLoadBazzer()
        {
            using (LibDBLocal dba = new LibDBLocal())
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <ts_continue_buy_bazzer>
                SelSql.AppendLine("SELECT");
                SelSql.AppendLine("entry_no, ");
                SelSql.AppendLine("bazzer_id, ");
                SelSql.AppendLine("seller_no, ");
                SelSql.AppendLine("have_no, ");
                SelSql.AppendLine("it_id, ");
                SelSql.AppendLine("it_created, ");
                SelSql.AppendLine("price, ");
                SelSql.AppendLine("set_count, ");
                SelSql.AppendLine("time, ");
                SelSql.AppendLine("ip_address, ");
                SelSql.AppendLine("host_address, ");
                SelSql.AppendLine("agent, ");
                SelSql.AppendLine("complete ");
                SelSql.AppendLine("FROM ts_continue_buy_bazzer");
                SelSql.AppendLine("ORDER BY entry_no");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.ts_continue_buy_bazzer);
            }
        }

        public void UpdateWeddingPartyNothings()
        {
            LibDBLocal dba = new LibDBLocal();
            try
            {
                dba.BeginTransaction();

                foreach (ContinueDataEntity.ts_continue_official_eventRow eventRow in Entity.ts_continue_official_event)
                {
                    dba.ExecuteNonQuery("UPDATE ts_continue_main SET party_secession=0,pcm_add_1=0,pcm_add_2=0,pcm_add_3=0,pcm_add_4=0,pcm_add_5=0,party_hope=0,option_comes_no=0,party_name='',area_name_no=0 WHERE entry_no=" + eventRow.entry_no);
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
