using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        private void CheckContinue()
        {
            foreach (LibPlayer Chara in CharaList)
            {
                int EntryNo = Chara.EntryNo;

                StringBuilder MessageBuilder = new StringBuilder();

                MessageBuilder.AppendLine("<section>");
                MessageBuilder.AppendLine("<h2>登録状況</h2>");
                MessageBuilder.AppendLine("<ul class=\"submenu\">");

                #region 継続登録
                {
                    MessageBuilder.Append("  <li>Continue ... ");

                    ContinueDataEntity.ts_continue_completeRow ContinueCompleteRow = con.Entity.ts_continue_complete.FindByentry_nocategory(EntryNo, "continue");

                    if (ContinueCompleteRow != null)
                    {
                        MessageBuilder.Append("Complete OK!");
                    }
                    else
                    {
                        if (Chara.IsNewPlayer)
                        {
                            MessageBuilder.Append("New Player.");
                        }
                        else
                        {
                            MessageBuilder.Append("None.");
                        }
                    }

                    MessageBuilder.AppendLine("</li>");
                }
                #endregion

                #region 取引登録
                {
                    ContinueDataEntity.ts_continue_completeRow ContinueCompleteRow = con.Entity.ts_continue_complete.FindByentry_nocategory(EntryNo, "trade");

                    if (ContinueCompleteRow != null)
                    {
                        MessageBuilder.AppendLine("  <li>The Trade ... Complete OK!</li>");
                    }
                }
                #endregion

                #region 戦闘準備登録
                {
                    ContinueDataEntity.ts_continue_completeRow ContinueCompleteRow = con.Entity.ts_continue_complete.FindByentry_nocategory(EntryNo, "equip");

                    if (ContinueCompleteRow != null)
                    {
                        MessageBuilder.AppendLine("  <li>Battle Equipment ... Complete OK!</li>");
                    }
                }
                #endregion

                #region 戦術登録
                {
                    ContinueDataEntity.ts_continue_completeRow ContinueCompleteRow = con.Entity.ts_continue_complete.FindByentry_nocategory(EntryNo, "action");

                    if (ContinueCompleteRow != null)
                    {
                        MessageBuilder.AppendLine("  <li>Battle Action ... Complete OK!</li>");
                    }
                }
                #endregion

                #region メッセージ登録
                {
                    ContinueDataEntity.ts_continue_completeRow ContinueCompleteRow = con.Entity.ts_continue_complete.FindByentry_nocategory(EntryNo, "message");

                    if (ContinueCompleteRow != null)
                    {
                        MessageBuilder.AppendLine("  <li>Message Send ... Complete OK!</li>");
                    }
                }
                #endregion

                #region セリフ登録
                {
                    ContinueDataEntity.ts_continue_completeRow ContinueCompleteRow = con.Entity.ts_continue_complete.FindByentry_nocategory(EntryNo, "serif");

                    if (ContinueCompleteRow != null)
                    {
                        MessageBuilder.AppendLine("  <li>Serif Setting ... Complete OK!</li>");
                    }
                }
                #endregion

                #region アイテム合成登録
                {
                    ContinueDataEntity.ts_continue_completeRow ContinueCompleteRow = con.Entity.ts_continue_complete.FindByentry_nocategory(EntryNo, "itemc");

                    if (ContinueCompleteRow != null)
                    {
                        MessageBuilder.AppendLine("  <li>Item Creation ... Complete OK!</li>");
                    }
                }
                #endregion

                #region キャラクター設定登録
                {
                    ContinueDataEntity.ts_continue_completeRow ContinueCompleteRow = con.Entity.ts_continue_complete.FindByentry_nocategory(EntryNo, "profile");

                    if (ContinueCompleteRow != null)
                    {
                        MessageBuilder.AppendLine("  <li>Character Profile ... Complete OK!</li>");
                    }
                }
                #endregion

                #region アカウント設定登録
                {
                    ContinueDataEntity.ts_continue_completeRow ContinueCompleteRow = con.Entity.ts_continue_complete.FindByentry_nocategory(EntryNo, "account");

                    if (ContinueCompleteRow != null)
                    {
                        MessageBuilder.AppendLine("  <li>Character Account ... Complete OK!</li>");
                    }
                }
                #endregion

                #region アイコン設定登録
                {
                    ContinueDataEntity.ts_continue_completeRow ContinueCompleteRow = con.Entity.ts_continue_complete.FindByentry_nocategory(EntryNo, "icon");

                    if (ContinueCompleteRow != null)
                    {
                        MessageBuilder.AppendLine("  <li>Character Icon ... Complete OK!</li>");
                    }
                }
                #endregion

                MessageBuilder.AppendLine("</ul>");
                MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");

                using (StreamWriter sw = new StreamWriter(LibConst.OutputFolderChara + LibUnitBaseMini.CharacterHTML(EntryNo), true, LibConst.FileEncod))
                {
                    sw.Write(MessageBuilder.ToString());
                }
            }
        }
    }
}
