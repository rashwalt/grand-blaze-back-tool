using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using System.IO;
using CommonLibrary.DataFormat.Entity;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        /// <summary>
        /// パーティ結果用ヘッダー
        /// </summary>
        private void BattleHeadder()
        {
            foreach (DataRow Party in PartyList.Rows)
            {
                int PartyNo = (int)Party["party_no"];

                DataTable PartyMembers = LibParty.GetPartyMembers(PartyNo);

                StringBuilder MessageBuilder = new StringBuilder();

                MessageBuilder.AppendLine("<!DOCTYPE HTML>");
                MessageBuilder.AppendLine("<html>");
                MessageBuilder.AppendLine("<head>");
                MessageBuilder.AppendLine("<meta charset=\"utf-8\">");
                MessageBuilder.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />");
                MessageBuilder.AppendLine("<title>P-No." + PartyNo + " " + Party["pt_name"].ToString() + " | Grand Blaze</title>");
                MessageBuilder.AppendLine("<link href=\"../../static/css/common.css\" rel=\"stylesheet\" type=\"text/css\">");
                MessageBuilder.AppendLine("<link href=\"../../static/css/dark-hive/jquery-ui-1.9.0.custom.min.css\" rel=\"stylesheet\" type=\"text/css\">");
                MessageBuilder.AppendLine("<script src=\"http://code.jquery.com/jquery-1.8.2.js\"></script>");
                MessageBuilder.AppendLine("<script src=\"../../static/js/jquery-ui-1.9.0.custom.min.js\"></script>");
                MessageBuilder.AppendLine("<script src=\"../../static/js/jquery.formset.min.js\"></script>");
                MessageBuilder.AppendLine("<script src=\"../../static/js/main.min.js\"></script>");
                MessageBuilder.AppendLine("<link href=\"../../static/css/result.css\" rel=\"stylesheet\" type=\"text/css\">");
                MessageBuilder.AppendLine("<!--[if lt IE 9]>");
                MessageBuilder.AppendLine("<script src=\"http://html5shiv.googlecode.com/svn/trunk/html5.js\"></script>");
                MessageBuilder.AppendLine("<script src=\"http://ie7-js.googlecode.com/svn/version/2.1(beta4)/IE9.js\"></script>");
                MessageBuilder.AppendLine("<![endif]-->");
                MessageBuilder.AppendLine("<script type=\"text/javascript\">");
                MessageBuilder.AppendLine("  var _gaq = _gaq || [];");
                MessageBuilder.AppendLine("  _gaq.push(['_setAccount', 'UA-30449583-1']);");
                MessageBuilder.AppendLine("  _gaq.push(['_trackPageview']);");
                MessageBuilder.AppendLine("  (function() {");
                MessageBuilder.AppendLine("    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;");
                MessageBuilder.AppendLine("    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';");
                MessageBuilder.AppendLine("    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);");
                MessageBuilder.AppendLine("  })();");
                MessageBuilder.AppendLine("</script>");
                MessageBuilder.AppendLine("</head>");
                MessageBuilder.AppendLine("");
                MessageBuilder.AppendLine("<body class=\"adventure-result\">");
                MessageBuilder.AppendLine("");
                MessageBuilder.AppendLine("<div id=\"container\">");
                MessageBuilder.AppendLine("  <header>");
                MessageBuilder.AppendLine("    <a id=\"site-logo\" href=\"http://www.grand-blaze.com/\"><img src=\"../../static/images/common/site_logo.png\" alt=\"Grand Blaze\" /></a>");
                MessageBuilder.AppendLine("    <div id=\"header-right\">");
                MessageBuilder.AppendLine("        <!--{% include 'account/login_parts.html' %}-->");
                MessageBuilder.AppendLine("        <div id=\"searchcontrol\">");
                MessageBuilder.AppendLine("            <form action=\"/search/\" id=\"cse-search-box\">");
                MessageBuilder.AppendLine("              <div>");
                MessageBuilder.AppendLine("                <input type=\"hidden\" name=\"cx\" value=\"018114392011501256481:1jv92qvio2i\" />");
                MessageBuilder.AppendLine("                <input type=\"hidden\" name=\"cof\" value=\"FORID:11\" />");
                MessageBuilder.AppendLine("                <input type=\"hidden\" name=\"ie\" value=\"UTF-8\" />");
                MessageBuilder.AppendLine("                <input type=\"text\" name=\"q\" id=\"q\" autocomplete=\"off\" size=\"31\" />");
                MessageBuilder.AppendLine("                <button type=\"submit\" id=\"search-submit\" name=\"sa\" class=\"ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only\" role=\"button\" aria-disabled=\"false\" title=\"検索\"><span class=\"ui-button-icon-primary ui-icon ui-icon-search\"></span><span class=\"ui-button-text\">検索</span></button>");
                MessageBuilder.AppendLine("              </div>");
                MessageBuilder.AppendLine("            </form>");
                MessageBuilder.AppendLine("            <script type=\"text/javascript\" src=\"http://www.google.com/cse/brand?form=cse-search-box&lang=ja\"></script>");
                MessageBuilder.AppendLine("       </div>");
                MessageBuilder.AppendLine("    </div>");
                MessageBuilder.AppendLine("  </header>");
                MessageBuilder.AppendLine("  <nav>");
                MessageBuilder.AppendLine("      <ul id=\"global-navi\">");
                MessageBuilder.AppendLine("        <li id=\"page-top\"><a href=\"http://www.grand-blaze.com/\">トップ</a></li>");
                MessageBuilder.AppendLine("        <li id=\"registration\"><a href=\"/continue/\">各種登録</a></li>");
                MessageBuilder.AppendLine("        <li id=\"play-guide\"><a href=\"/playguide/\">プレイガイド</a></li>");
                MessageBuilder.AppendLine("        <li id=\"adventure-result\"><a href=\"/result/\">冒険の結果</a></li>");
                MessageBuilder.AppendLine("        <li id=\"forum\"><a href=\"/forum/\">フォーラム</a></li>");
                MessageBuilder.AppendLine("        <li id=\"world-guide\"><a href=\"/worldguide/\">世界背景</a></li>");
                MessageBuilder.AppendLine("      </ul>");
                MessageBuilder.AppendLine("  </nav>");
                MessageBuilder.AppendLine("  <div id=\"main-view\">");
                MessageBuilder.AppendLine("      <div id=\"sidebar1\">");
                MessageBuilder.AppendLine("        <!--sidebar start-->");
                MessageBuilder.AppendLine("        <nav>");
                MessageBuilder.AppendLine("            <h4>");
                MessageBuilder.AppendLine("                パーティ");
                MessageBuilder.AppendLine("            </h4>");
                MessageBuilder.AppendLine("            <dl id=\"side-menu\" class=\"ui-right-icon\">");
                string PartyHTML = LibParty.PartyHTML(PartyNo);
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../party/" + PartyHTML + "\">");
                MessageBuilder.AppendLine("                        プロローグ<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../party/" + PartyHTML + "#battle\">");
                MessageBuilder.AppendLine("                        バトル<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../party/" + PartyHTML + "#btresult\">");
                MessageBuilder.AppendLine("                        バトルリザルト<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../party/" + PartyHTML + "#epilog\">");
                MessageBuilder.AppendLine("                        エピローグ<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("            </dl>");

                if (PartyMembers.Rows.Count > 0)
                {
                    MessageBuilder.AppendLine("            <h4>");
                    MessageBuilder.AppendLine("                パーティメンバー");
                    MessageBuilder.AppendLine("            </h4>");
                    MessageBuilder.AppendLine("            <dl id=\"side-menu\" class=\"ui-right-icon\">");

                    foreach (DataRow Member in PartyMembers.Rows)
                    {
                        MessageBuilder.AppendLine("                <dt>");
                        MessageBuilder.AppendLine("                    <a href=\"../status/" + LibUnitBaseMini.CharacterHTML((int)Member["entry_no"]) + "\">");
                        MessageBuilder.AppendLine("                        " + CharaMini.GetNickName((int)Member["entry_no"]) + "<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                        MessageBuilder.AppendLine("                    </a>");
                        MessageBuilder.AppendLine("                </dt>");
                    }

                    MessageBuilder.AppendLine("            </dl>");
                }
                MessageBuilder.AppendLine("        </nav>");
                MessageBuilder.AppendLine("        <!--sidebar end-->");
                MessageBuilder.AppendLine("      </div>");
                MessageBuilder.AppendLine("      <div id=\"view-container\">");
                MessageBuilder.AppendLine("        <div id=\"breadcrumbs\">");
                MessageBuilder.AppendLine("	  		<!--bread start-->");
                MessageBuilder.AppendLine("	  		<a href=\"/\">トップ</a> &gt; <a href=\"../\">冒険の結果</a> &gt; P-No." + PartyNo + " " + Party["pt_name"].ToString() + "");
                MessageBuilder.AppendLine("	  		<!--bread end-->");
                MessageBuilder.AppendLine("	  	</div>");
                MessageBuilder.AppendLine("	  	<article id=\"wcontent\">");
                MessageBuilder.AppendLine("	  <!--content start-->");

                MessageBuilder.AppendLine("<h1>P-No." + PartyNo + " " + Party["pt_name"].ToString() + "</h1>");
                MessageBuilder.AppendLine("<section>");
                MessageBuilder.AppendLine("<h2>行き先判定</h2>");
                MessageBuilder.AppendLine("  <ul class=\"submenu\">");

                foreach (DataRow Member in PartyMembers.Rows)
                {
                    int EntryNo = (int)Member["entry_no"];
                    ContinueDataEntity.ts_continue_mainRow ContinueMainRow = con.Entity.ts_continue_main.FindByentry_no(EntryNo);

                    if (CharaMini.CheckNewPlayer(EntryNo))
                    {
                        MessageBuilder.AppendLine("    <li>" + CharaMini.GetNickNameWithLink(EntryNo, 1) + ": - Welcome to \"Grand Blaze\" -</li>");
                    }
                    else
                    {
                        if (ContinueMainRow != null)
                        {
                            if (ContinueMainRow.quest_id == 0)
                            {
                                MessageBuilder.AppendLine("    <li>" + CharaMini.GetNickNameWithLink(EntryNo, 1) + "は、パーティメンバーの決定に従うようだ…。</li>");
                            }
                            else if (ContinueMainRow.quest_id == -1)
                            {
                                MessageBuilder.AppendLine("    <li>" + CharaMini.GetNickNameWithLink(EntryNo, 1) + "は、イベントへの参加を希望した。</li>");
                            }
                            else
                            {
                                MessageBuilder.AppendLine("    <li>" + CharaMini.GetNickNameWithLink(EntryNo, 1) + "は、クエスト「" + LibQuest.GetQuestName(ContinueMainRow.quest_id) + "」、マーク「" + LibQuest.GetMarkName(ContinueMainRow.mark_id) + "」を希望した。</li>");
                            }
                        }
                        else
                        {
                            MessageBuilder.AppendLine("    <li>" + CharaMini.GetNickNameWithLink(EntryNo, 1) + "の継続登録を確認できませんでした…。</li>");
                        }
                    }
                }
                MessageBuilder.AppendLine("  </ul>");

                DataTable PartySystemMessage = LibParty.GetPartySystemMessage(PartyNo, Status.PartySysMemoType.MarkMissing);

                foreach (DataRow SystemRow in PartySystemMessage.Rows)
                {
                    MessageBuilder.AppendLine(LibPrivateAction.Message(SystemRow["memo"].ToString(), (int)SystemRow["memo_level"]));
                }

                MessageBuilder.AppendLine("</section>");

                using (StreamWriter sw = new StreamWriter(LibConst.OutputFolderParty + LibParty.PartyHTML(PartyNo), true, LibConst.FileEncod))
                {
                    sw.Write(MessageBuilder.ToString());
                }
            }
        }

        private void BattleFooter()
        {
            foreach (DataRow Party in PartyList.Rows)
            {
                int PartyNo = (int)Party["party_no"];

                DataTable PartyMembers = LibParty.GetPartyMembers(PartyNo);

                StringBuilder MessageBuilder = new StringBuilder();

                MessageBuilder.AppendLine("	  <!--content end-->");
                MessageBuilder.AppendLine("      </article>");
                MessageBuilder.AppendLine("	  </div>");
                MessageBuilder.AppendLine("  </div>");
                MessageBuilder.AppendLine("  <footer>");
                MessageBuilder.AppendLine("  	<ul>");
                MessageBuilder.AppendLine("  		<li><a href=\"/about/\">このサイトについて</a></li>");
                MessageBuilder.AppendLine("  		<li><a href=\"/support/\">お問い合わせ</a></li>");
                MessageBuilder.AppendLine("  	</ul>");
                MessageBuilder.AppendLine("    <small>");
                MessageBuilder.AppendLine("		&copy;2003-2012 Grand Blaze Game Master All Rights Reserved.");
                MessageBuilder.AppendLine("    </small>");
                MessageBuilder.AppendLine("  </footer>");
                MessageBuilder.AppendLine("  <!-- end .container --></div>");
                MessageBuilder.AppendLine("</body>");
                MessageBuilder.AppendLine("</html>");

                using (StreamWriter sw = new StreamWriter(LibConst.OutputFolderParty + LibParty.PartyHTML(PartyNo), true, LibConst.FileEncod))
                {
                    sw.Write(MessageBuilder.ToString());
                }
            }
        }
    }
}
