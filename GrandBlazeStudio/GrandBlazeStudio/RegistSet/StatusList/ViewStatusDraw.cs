using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using System.IO;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.SpecialEntity;

namespace GrandBlazeStudio.RegistSet.StatusList
{
    partial class StatusListMain
    {
        private void ViewStatusDraw()
        {
            CharacterDataEntity.ts_character_listDataTable CharacterList = CharaMini.GetCharacters();
            int i;

            foreach (CharacterDataEntity.ts_character_listRow Character in CharacterList)
            {
                int EntryNo = Character.entry_no;

                LibPlayer Mine = new LibPlayer(Status.Belong.Friend, EntryNo);
                Mine.StatusEffect.LoadDefaultStatus(Mine.EntryNo);

                // HPなどの再調整
                if (Mine.HPNow > Mine.HPMax) { Mine.HPNow = Mine.HPMax; }
                if (Mine.MPNow > Mine.MPMax) { Mine.MPNow = Mine.MPMax; }
                if (Mine.TPNow > Mine.TPMax) { Mine.TPNow = Mine.TPMax; }

                int PartyNo = LibParty.GetPartyNo(EntryNo);

                DataTable PartyMembers = LibParty.GetPartyMembers(PartyNo);

                StringBuilder MessageBuilder = new StringBuilder();

                MessageBuilder.AppendLine("<!DOCTYPE HTML>");
                MessageBuilder.AppendLine("<html>");
                MessageBuilder.AppendLine("<head>");
                MessageBuilder.AppendLine("<meta charset=\"utf-8\">");
                MessageBuilder.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />");
                MessageBuilder.AppendLine("<title>No" + EntryNo + " " + Mine.CharacterName + " | Grand Blaze</title>");
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
                MessageBuilder.AppendLine("                ステータス");
                MessageBuilder.AppendLine("            </h4>");
                MessageBuilder.AppendLine("            <dl id=\"side-menu\" class=\"ui-right-icon\">");

                string CharacterHTML = LibUnitBaseMini.CharacterHTML(EntryNo);
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../status/" + CharacterHTML + "\">");
                MessageBuilder.AppendLine("                        アクションログ<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../status/" + CharacterHTML + "#status\">");
                MessageBuilder.AppendLine("                        ステータス<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../status/" + CharacterHTML + "#install\">");
                MessageBuilder.AppendLine("                        クラス<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../status/" + CharacterHTML + "#item_list\">");
                MessageBuilder.AppendLine("                        アイテム<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../status/" + CharacterHTML + "#eqitem\">");
                MessageBuilder.AppendLine("                        装備品<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../status/" + CharacterHTML + "#perks\">");
                MessageBuilder.AppendLine("                        スキル<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../status/" + CharacterHTML + "#battle\">");
                MessageBuilder.AppendLine("                        戦術<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../status/" + CharacterHTML + "#key\">");
                MessageBuilder.AppendLine("                        貴重品<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../status/" + CharacterHTML + "#quest\">");
                MessageBuilder.AppendLine("                        クエスト<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("                <dt>");
                MessageBuilder.AppendLine("                    <a href=\"../status/" + CharacterHTML + "#icon\">");
                MessageBuilder.AppendLine("                        アイコン<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("                    </a>");
                MessageBuilder.AppendLine("                </dt>");
                MessageBuilder.AppendLine("            </dl>");
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

                if (PartyMembers.Rows.Count > 1)
                {
                    MessageBuilder.AppendLine("            <h4>");
                    MessageBuilder.AppendLine("                パーティメンバー");
                    MessageBuilder.AppendLine("            </h4>");
                    MessageBuilder.AppendLine("            <dl id=\"side-menu\" class=\"ui-right-icon\">");

                    foreach (DataRow Member in PartyMembers.Rows)
                    {
                        if ((int)Member["entry_no"] != EntryNo)
                        {
                            CharacterDataEntity.ts_character_listRow MemberCharacter = CharacterList.FindByentry_no((int)Member["entry_no"]);
                            MessageBuilder.AppendLine("                <dt>");
                            MessageBuilder.AppendLine("                    <a href=\"../status/" + LibUnitBaseMini.CharacterHTML((int)Member["entry_no"]) + "\">");
                            MessageBuilder.AppendLine("                        " + MemberCharacter.nick_name + "<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                            MessageBuilder.AppendLine("                    </a>");
                            MessageBuilder.AppendLine("                </dt>");
                        }
                    }

                    MessageBuilder.AppendLine("            </dl>");
                }
                MessageBuilder.AppendLine("        </nav>");
                MessageBuilder.AppendLine("        <!--sidebar end-->");
                MessageBuilder.AppendLine("      </div>");
                MessageBuilder.AppendLine("      <div id=\"view-container\">");
                MessageBuilder.AppendLine("        <div id=\"breadcrumbs\">");
                MessageBuilder.AppendLine("            <!--bread start-->");
                MessageBuilder.AppendLine("            <a href=\"http://www.grand-blaze.com/\">トップ</a> &gt; <a href=\"../\">冒険の結果</a> &gt; <a href=\"../" + LibList.GetListHTML(EntryNo) + "\">冒険者一覧(" + LibList.GetMinNo(EntryNo) + " ～ " + LibList.GetMaxNo(EntryNo, CharaMini.GetMaxNo) + ")</a> &gt; Entry No:" + EntryNo + " " + Mine.CharacterName + "");
                MessageBuilder.AppendLine("            <!--bread end-->");
                MessageBuilder.AppendLine("        </div>");
                MessageBuilder.AppendLine("        <article id=\"wcontent\">");
                MessageBuilder.AppendLine("      <!--content start-->");

                MessageBuilder.AppendLine("<h1>Entry No:" + EntryNo + " " + Mine.CharacterName + "</h1>");

                DataTable ActionLogs = LibPlayerMemo.MessageList(EntryNo);

                MessageBuilder.AppendLine("<section id=\"action\">");
                MessageBuilder.AppendLine("	<h2>アクションログ</h2>");
                
                if (ActionLogs.Rows.Count == 0)
                {
                    MessageBuilder.AppendLine("<p>目立った行動は行っていないようです。</p>");
                }

                foreach (DataRow ActionLogRow in ActionLogs.Rows)
                {
                    MessageBuilder.AppendLine(LibPrivateAction.Message(ActionLogRow["memo"].ToString(), (int)ActionLogRow["memo_level"], true));
                }

                MessageBuilder.AppendLine("    <div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");

                MessageBuilder.AppendLine("<section id=\"status\">");
                MessageBuilder.AppendLine("  <h2>ステータス</h2>");
                MessageBuilder.AppendLine("  <h3>" + Mine.CharacterName + "</h3>");
                MessageBuilder.AppendLine("  <div class=\"stt\">");
                MessageBuilder.AppendLine("    <table class=\"stlist\" summary=\"バトルステータス（レベル）情報リストです。\">");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>クラス</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.IntallClassName + "&nbsp;Lv" + Mine.InstallClassLevelNormal + "");
                if (Mine.SecondryInstallClassLevel > 0)
                {
                    MessageBuilder.AppendLine("        / " + Mine.SecondryIntallClassName + "&nbsp;Lv" + Mine.SecondryInstallClassLevel);
                }
                MessageBuilder.AppendLine("        </td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("	    <th title=\"メインクラス経験値\">EXP(Main)</th>");
                MessageBuilder.AppendLine("	    <td>" + Mine.InstallClassExpNext + "/" + Mine.InstallClassMaxExpNext);
                MessageBuilder.AppendLine("		  <div class=\"gauge\">");
                MessageBuilder.AppendLine("          	<div class=\"gaugebar exp\" style=\"width:" + (int)((double)Mine.InstallClassExpNext / (double)Mine.InstallClassMaxExpNext * 100) + "%;\">");
                MessageBuilder.AppendLine("            </div>");
                MessageBuilder.AppendLine("           </div>");
                MessageBuilder.AppendLine("          </td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("	    <th title=\"サブクラス経験値\">EXP(Sub)</th>");
                if (Mine.SecondryInstallClassLevel > 0)
                {
                    MessageBuilder.AppendLine("	    <td>" + Mine.SecondryInstallClassExpNext + "/" + Mine.SecondryInstallClassMaxExpNext);
                    MessageBuilder.AppendLine("		  <div class=\"gauge\">");
                    MessageBuilder.AppendLine("          	<div class=\"gaugebar exp\" style=\"width:" + (int)((double)Mine.SecondryInstallClassExpNext / (double)Mine.SecondryInstallClassMaxExpNext * 100) + "%;\">");
                    MessageBuilder.AppendLine("            </div>");
                    MessageBuilder.AppendLine("           </div>");
                    MessageBuilder.AppendLine("          </td>");
                }
                else
                {
                    MessageBuilder.AppendLine("	    <td>--/--</td>");
                }
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("    </table>");
                MessageBuilder.AppendLine("    <table class=\"stlist\" summary=\"バトルステータス（ＨＰ、ＭＰ、ＴＰ）情報リストです。\">");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>HP</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.HPNow + "/" + Mine.HPMax);
                if (Mine.LevelUpHP > 0)
                {
                    MessageBuilder.AppendLine("<span class=\"lvupst\">(+" + Mine.LevelUpHP.ToString("G29") + ")</span>");
                }
                MessageBuilder.AppendLine("		  <div class=\"gauge\">");
                MessageBuilder.AppendLine("          	<div class=\"gaugebar hp\" style=\"width:" + Mine.HPDamageRate + "%;\">");
                MessageBuilder.AppendLine("            </div>");
                MessageBuilder.AppendLine("           </div>");
                MessageBuilder.AppendLine("        </td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>MP</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.MPNow + "/" + Mine.MPMax);
                if (Mine.LevelUpMP > 0)
                {
                    MessageBuilder.AppendLine("<span class=\"lvupst\">(+" + Mine.LevelUpMP.ToString("G29") + ")</span>");
                }
                MessageBuilder.AppendLine("		  <div class=\"gauge\">");
                MessageBuilder.AppendLine("          	<div class=\"gaugebar mp\" style=\"width:" + Mine.MPDamageRate + "%;\">");
                MessageBuilder.AppendLine("            </div>");
                MessageBuilder.AppendLine("           </div>");
                MessageBuilder.AppendLine("        </td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>TP</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.TPNow + "/" + Mine.TPMax);
                MessageBuilder.AppendLine("		  <div class=\"gauge\">");
                MessageBuilder.AppendLine("          	<div class=\"gaugebar tp\" style=\"width:" + Mine.TPDamageRate + "%;\">");
                MessageBuilder.AppendLine("            </div>");
                MessageBuilder.AppendLine("           </div></td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("    </table>");
                MessageBuilder.AppendLine("    <table class=\"stlist\" summary=\"バトルステータス（アドバンスアビリティ）情報リストです。\">");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>攻撃</th>");
                int CharacterATK = Mine.ATK;
                if (Mine.ATKSub > 0)
                {
                    if (Mine.EffectList.FindByeffect_id(892) != null)
                    {
                        // 二刀流
                        CharacterATK = (int)((decimal)Mine.ATK * 0.65m) + (int)((decimal)Mine.ATKSub * 0.65m);
                    }
                    else if (Mine.EffectList.FindByeffect_id(891) != null)
                    {
                        // 両手利き
                        CharacterATK = (int)((decimal)Mine.ATK * 0.55m) + (int)((decimal)Mine.ATKSub * 0.55m);
                    }
                }
                MessageBuilder.AppendLine("        <td>" + CharacterATK + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>防御</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.DFE + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>魔法防御</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.MGR + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("    </table>");
                MessageBuilder.AppendLine("    <table class=\"stlist\" summary=\"バトルステータス（ベーシックアビリティ）情報リストです。\">");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("          <th>力</th>");
                MessageBuilder.AppendLine("          <td>" + Mine.STRBase);
                if (Mine.STRPlus > 0)
                {
                    MessageBuilder.AppendLine("<span class=\"stup\">+" + Mine.STRPlus + "</span>");
                }
                else if (Mine.STRPlus < 0)
                {
                    MessageBuilder.AppendLine("<span class=\"stdown\">" + Mine.STRPlus + "</span>");
                }
                if (Mine.LevelUpSTR > 0)
                {
                    MessageBuilder.AppendLine("<span class=\"lvupst\">(+" + Mine.LevelUpSTR.ToString("G29") + ")</span>");
                }
                MessageBuilder.AppendLine("</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("          <th>敏捷</th>");
                MessageBuilder.AppendLine("          <td>" + Mine.AGIBase);
                if (Mine.AGIPlus > 0)
                {
                    MessageBuilder.AppendLine("<span class=\"stup\">+" + Mine.AGIPlus + "</span>");
                }
                else if (Mine.AGIPlus < 0)
                {
                    MessageBuilder.AppendLine("<span class=\"stdown\">" + Mine.AGIPlus + "</span>");
                }
                if (Mine.LevelUpAGI > 0)
                {
                    MessageBuilder.AppendLine("<span class=\"lvupst\">(+" + Mine.LevelUpAGI.ToString("G29") + ")</span>");
                }
                MessageBuilder.AppendLine("</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("          <th>魔力</th>");
                MessageBuilder.AppendLine("          <td>" + Mine.MAGBase);
                if (Mine.MAGPlus > 0)
                {
                    MessageBuilder.AppendLine("<span class=\"stup\">+" + Mine.MAGPlus + "</span>");
                }
                else if (Mine.MAGPlus < 0)
                {
                    MessageBuilder.AppendLine("<span class=\"stdown\">" + Mine.MAGPlus + "</span>");
                }
                if (Mine.LevelUpMAG > 0)
                {
                    MessageBuilder.AppendLine("<span class=\"lvupst\">(+" + Mine.LevelUpMAG.ToString("G29") + ")</span>");
                }
                MessageBuilder.AppendLine("</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("          <th>" + Mine.UniqueName + "</th>");
                MessageBuilder.AppendLine("          <td>" + Mine.UNQBase);
                if (Mine.UNQPlus > 0)
                {
                    MessageBuilder.AppendLine("<span class=\"stup\">+" + Mine.UNQPlus + "</span>");
                }
                else if (Mine.UNQPlus < 0)
                {
                    MessageBuilder.AppendLine("<span class=\"stdown\">" + Mine.UNQPlus + "</span>");
                }
                if (Mine.LevelUpUNQ > 0)
                {
                    MessageBuilder.AppendLine("<span class=\"lvupst\">(+" + Mine.LevelUpUNQ.ToString("G29") + ")</span>");
                }
                MessageBuilder.AppendLine("</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("    </table>");
                MessageBuilder.AppendLine(" </div>");
                MessageBuilder.AppendLine("	<div class=\"stt\">");

                string ALinkFoward = "";
                string ALinkBack = "";

                if (Mine.ImageLinkURL.Length > 0)
                {
                    ALinkFoward = "<a href=\"" + Mine.ImageLinkURL + "\">";
                    ALinkBack = "</a>";
                }

                if (Mine.ImageURL.Length == 0)
                {
                    //MessageBuilder.AppendLine("	  <p>" + ALinkFoward + "<img src=\"../parts/image/no_img.gif\" alt=\"キャラクターイラスト\" width=\"240\" height=\"280\" id=\"ch_img\" />" + ALinkBack + "</p>");
                    string ImageSexType = "m";
                    if (Mine.Sex == Status.Sex.Female) { ImageSexType = "f"; }
                    string ImageOfficialUrl = "../../static/images/result/op_";

                    switch (Mine.Race)
                    {
                        case Status.Race.Hume:
                            ImageOfficialUrl += "h" + ImageSexType + ".jpg";
                            break;
                        case Status.Race.Bartan:
                            ImageOfficialUrl += "b" + ImageSexType + ".jpg";
                            break;
                        case Status.Race.Draqh:
                            ImageOfficialUrl += "d" + ImageSexType + ".jpg";
                            break;
                        case Status.Race.Elve:
                            ImageOfficialUrl += "e" + ImageSexType + ".jpg";
                            break;
                        case Status.Race.Falurt:
                            ImageOfficialUrl += "f" + ImageSexType + ".jpg";
                            break;
                        case Status.Race.Lycanth:
                            ImageOfficialUrl += "l" + ImageSexType + ".jpg";
                            break;
                    }
                    MessageBuilder.AppendLine("	  <p>" + ALinkFoward + "<img src=\"" + ImageOfficialUrl + "\" alt=\"キャラクターイラスト\" width=\"240\" height=\"280\" id=\"ch_img\" />" + ALinkBack + "</p>");
                    MessageBuilder.AppendLine("	  <p>&copy;Grand Blaze Products.</p>");
                }
                else
                {
                    MessageBuilder.AppendLine("	  <p>" + ALinkFoward + "<img src=\"" + Mine.ImageURL + "\" alt=\"キャラクターイラスト\" width=\"" + Mine.ImageWidthSize + "\" height=\"" + Mine.ImageHeightSize + "\" id=\"ch_img\" />" + ALinkBack + "</p>");
                    MessageBuilder.AppendLine("	  <p>&copy;" + Mine.ImageCopyright + "</p>");
                }

                MessageBuilder.AppendLine("    </div>");
                MessageBuilder.AppendLine("  <div class=\"stt\">");
                MessageBuilder.AppendLine("    <table class=\"stlist\" summary=\"プライベート情報リストです。\">");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>Entry No</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.EntryNo + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>愛称</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.NickName + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th title=\"スキルポイント\">SP</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.LevelNormal + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("	    <th title=\"スキルポイント経験値\">EXP(SP)</th>");
                MessageBuilder.AppendLine("	    <td>" + Mine.ExpNext + "/" + Mine.MaxExpNext);
                MessageBuilder.AppendLine("		  <div class=\"gauge\">");
                MessageBuilder.AppendLine("          	<div class=\"gaugebar exp\" style=\"width:" + (int)((double)Mine.ExpNext / (double)Mine.MaxExpNext * 100) + "%;\">");
                MessageBuilder.AppendLine("            </div>");
                MessageBuilder.AppendLine("           </div>");
                MessageBuilder.AppendLine("           </td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>所属国</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.NationName + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>守護者</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.Guardian + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>種族</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.RaceName + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>性別</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.SexName + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>年齢</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.Age + "歳</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>身長</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.Height + "cm</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>体重</th>");
                MessageBuilder.AppendLine("        <td>" + Mine.Weight + "kg</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("    </table>");
                MessageBuilder.AppendLine("    <table class=\"stlist\" summary=\"所持金情報リストです。\">");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("	    <th>BC</th>");
                MessageBuilder.AppendLine("	    <td>" + Mine.BlazeChip.ToString("N0") + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("	    <th>所持金</th>");
                MessageBuilder.AppendLine("	    <td>" + Mine.HaveMoney.ToString("N0") + "G</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("    </table>");
                MessageBuilder.AppendLine("    </div>");
                MessageBuilder.AppendLine("	<table class=\"pflist\">");
                MessageBuilder.AppendLine("	  <tr>");
                MessageBuilder.AppendLine("        <th>プロフィール</th>");
                MessageBuilder.AppendLine("        <td class=\"profile\">" + Mine.Profile + "</td>");
                MessageBuilder.AppendLine("	  </tr>");
                MessageBuilder.AppendLine("	</table>");

                MessageBuilder.AppendLine("  <table class=\"ellist\">");
                MessageBuilder.AppendLine("    <tr>");
                MessageBuilder.AppendLine("     <th class=\"fire\">火</th>");
                MessageBuilder.AppendLine("     <th class=\"freeze\">氷</th>");
                MessageBuilder.AppendLine("     <th class=\"air\">風</th>");
                MessageBuilder.AppendLine("     <th class=\"earth\">土</th>");
                MessageBuilder.AppendLine("     <th class=\"water\">水</th>");
                MessageBuilder.AppendLine("     <th class=\"thunder\">雷</th>");
                MessageBuilder.AppendLine("     <th class=\"holy\">聖</th>");
                MessageBuilder.AppendLine("     <th class=\"dark\">闇</th>");
                MessageBuilder.AppendLine("     <th class=\"slash\">斬</th>");
                MessageBuilder.AppendLine("     <th class=\"pierce\">突</th>");
                MessageBuilder.AppendLine("     <th class=\"strike\">打</th>");
                MessageBuilder.AppendLine("     <th class=\"break\">壊</th>");
                MessageBuilder.AppendLine("    </tr>");
                MessageBuilder.AppendLine("    <tr>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Fire + "</td>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Freeze + "</td>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Air + "</td>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Earth + "</td>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Water + "</td>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Thunder + "</td>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Holy + "</td>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Dark + "</td>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Slash + "</td>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Pierce + "</td>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Strike + "</td>");
                MessageBuilder.AppendLine("     <td>" + Mine.DefenceElemental.Break + "</td>");
                MessageBuilder.AppendLine("    </tr>");
                MessageBuilder.AppendLine("  </table>");
                MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");

                MessageBuilder.AppendLine("<section id=\"install\">");
                MessageBuilder.AppendLine("  <h2>クラス</h2>");
                MessageBuilder.AppendLine("  <table class=\"iclist\" summary=\"インストールクラスのリストです。\">");

                // インストールクラス
                int InstallIndex = 0;
                foreach (CommonUnitDataEntity.install_level_listRow InstallClassRow in Mine.InstallClassList)
                {
                    int InstallNo = 0;
                    InstallNo = InstallClassRow.install_id;

                    InstallDataEntity.mt_install_class_listRow InstallRow = LibInstall.GetInstallRow(InstallNo);

                    int exp_next = 0;
                    int exp_max_next = 0;

                    if (InstallClassRow.level > 1)
                    {
                        exp_next = InstallClassRow.exp - LibExperience.GetMaxExp(InstallClassRow.level - 1);
                        exp_max_next = LibExperience.GetMaxExp(InstallClassRow.level) - LibExperience.GetMaxExp(InstallClassRow.level - 1);
                    }
                    else
                    {
                        exp_next = InstallClassRow.exp;
                        exp_max_next = LibExperience.GetMaxExp(InstallClassRow.level);
                    }

                    if (InstallIndex == 0 || InstallIndex % 3 == 0)
                    {
                        MessageBuilder.AppendLine("      <tr>");
                    }

                    MessageBuilder.AppendLine("        <th>" + InstallRow.classname + "</th>");
                    MessageBuilder.AppendLine("        <td>" + InstallClassRow.level + "</td>");
                    MessageBuilder.AppendLine("        <td class=\"exp\">" + exp_next + "/" + exp_max_next);

                    MessageBuilder.AppendLine("		  <div class=\"gauge\">");
                    MessageBuilder.AppendLine("          	<div class=\"gaugebar exp\" style=\"width:" + (int)((double)exp_next / (double)exp_max_next * 100) + "%;\">");
                    MessageBuilder.AppendLine("            </div>");
                    MessageBuilder.AppendLine("           </div></td>");

                    if ((InstallIndex + 1) % 3 == 0)
                    {
                        MessageBuilder.AppendLine("      </tr>");
                    }

                    InstallIndex++;
                }

                MessageBuilder.AppendLine("  </table>");
                MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");

                // アイテム(カバン)
                Mine.HaveItem.DefaultView.RowFilter = "box_type=" + Status.ItemBox.Normal;
                Mine.HaveItem.DefaultView.Sort = "have_no";
                int HaveItemCount = Mine.HaveItem.DefaultView.Count;

                MessageBuilder.AppendLine("<section id=\"item_list\">");
                MessageBuilder.AppendLine("  <h2>アイテム：カバン(" + HaveItemCount + "/" + Mine.MaxHaveItem + ")</h2>");
                MessageBuilder.AppendLine("  <div id=\"itemdata\" class=\"ui-tabs\">");
                MessageBuilder.AppendLine("  <ul class=\"ui-tabs-nav ui-helper-reset ui-helper-clearfix\">");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top ui-tabs-selected ui-state-active\"><a href=\"javascript:void(0);\" data-viewer=\"all\">すべて</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"new\">新規</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"main\">メイン</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"sub\">サブ</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"head\">頭部</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"body\">身体</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"accesory\">装飾</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"using\">道具</a></li>");
                MessageBuilder.AppendLine("  </ul>");
                MessageBuilder.AppendLine("<table class=\"itlist list_data\">");
                MessageBuilder.AppendLine("      <thead>");
                MessageBuilder.AppendLine("      <tr>");
                MessageBuilder.AppendLine("        <th class=\"no\">No</th>");
                MessageBuilder.AppendLine("        <th class=\"name\">アイテム名</th>");
                MessageBuilder.AppendLine("        <th class=\"type\">種別</th>");
                MessageBuilder.AppendLine("        <th class=\"price\">価値</th>");
                MessageBuilder.AppendLine("        <th class=\"number\">個数</th>");
                MessageBuilder.AppendLine("      </tr>");
                MessageBuilder.AppendLine("      </thead>");
                MessageBuilder.AppendLine("      <tbody>");

                CommonUnitDataEntity.have_item_listRow HaveItemRow = null;
                for (i = 1; i <= HaveItemCount; i++)
                {
                    int ItemNo = 0;
                    HaveItemRow = (CommonUnitDataEntity.have_item_listRow)Mine.HaveItem.DefaultView[i - 1].Row;
                    ItemNo = HaveItemRow.it_num;
                    bool IsCreated = HaveItemRow.created;

                    CommonItemEntity.item_listRow ItemRow = LibItem.GetItemRow(ItemNo, IsCreated);
                    ItemTypeEntity.mt_item_typeRow TypeRow = LibItemType.GetTypeRow(ItemRow.it_type);
                    ItemTypeEntity.mt_item_type_sub_categoryRow SubCategoryRow = LibItemType.GetSubCategoryRow(ItemRow.it_sub_category);

                    if (IsCreated)
                    {
                        ItemRow.it_name = HaveItemRow.it_name;
                        ItemRow.it_comment = HaveItemRow.it_comment;
                        ItemRow.it_effect = HaveItemRow.it_effect;
                        ItemRow.it_price = HaveItemRow.it_price;
                        ItemRow.it_seller = HaveItemRow.it_seller;
                    }

                    string ClassType = "main";

                    switch (TypeRow.categ_div)
                    {
                        case Status.TypeCategoryDiv.Main:
                            ClassType = "main";
                            break;
                        case Status.TypeCategoryDiv.Sub:
                            ClassType = "sub";
                            break;
                        case Status.TypeCategoryDiv.Head:
                            ClassType = "head";
                            break;
                        case Status.TypeCategoryDiv.Body:
                            ClassType = "body";
                            break;
                        case Status.TypeCategoryDiv.Accesory:
                            ClassType = "accesory";
                            break;
                        default:
                            ClassType = "using";
                            break;
                    }

                    string Equiped = "";
                    string Used = "";
                    string Creatable = "";
                    int StackCount = ItemRow.it_stack;
                    if (StackCount == 0) { StackCount = 99; }
                    if (ItemRow.it_rare) { StackCount = 1; }

                    string NewItems = "";
                    string NewTips = "";
                    if (HaveItemRow._new)
                    {
                        NewItems = " new";
                        NewTips = "<span class=\"newit\">New!</span>";
                    }

                    // 装備有無
                    if (HaveItemRow.equip_spot > 0)
                    {
                        Equiped = " equiped";
                    }

                    if (ItemRow.it_use_item != 0)
                    {
                        Used = " used";
                    }
                    if (IsCreated)
                    {
                        Creatable = " creatable";
                    }

                    MessageBuilder.AppendLine("      <tr class=\"" + ClassType + NewItems + "\">");
                    MessageBuilder.AppendLine("        <td class=\"no\">" + HaveItemRow.have_no + "<div class=\"toolhelp\">" + LibComment.Item(ItemNo, IsCreated, Mine.CharacterName) + "</div></td>");
                    MessageBuilder.AppendLine("        <td class=\"name" + Equiped + Used + Creatable + "\">" + ItemRow.it_name + NewTips + "</td>");
                    MessageBuilder.AppendLine("        <td class=\"type\">" + TypeRow.type + "</td>");
                    MessageBuilder.AppendLine("        <td class=\"price\">" + ItemRow.it_seller.ToString("#,0.#") + "</td>");
                    MessageBuilder.AppendLine("        <td class=\"number\">" + HaveItemRow.it_box_count + "<sub> / " + StackCount + "</sub></td>");
                    MessageBuilder.AppendLine("      </tr>");
                }

                MessageBuilder.AppendLine("  </tbody>");
                MessageBuilder.AppendLine("  </table>");

                if (HaveItemCount == 0)
                {
                    MessageBuilder.AppendLine("<p class=\"sysmes\">所持しているアイテムはありません。</p>");
                }
                MessageBuilder.AppendLine("  </div>");

                MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");

                // テンポラリボックス
                Mine.HaveItem.DefaultView.RowFilter = "box_type=" + Status.ItemBox.Box;
                Mine.HaveItem.DefaultView.Sort = "have_no";
                int BoxItemCount = Mine.HaveItem.DefaultView.Count;

                if (BoxItemCount > 0)
                {
                    MessageBuilder.AppendLine("<section>");
                    MessageBuilder.AppendLine("  <h2>アイテム：テンポラリボックス</h2>");

                    MessageBuilder.AppendLine("<table class=\"itlist list_data\">");
                    MessageBuilder.AppendLine("      <thead>");
                    MessageBuilder.AppendLine("      <tr>");
                    MessageBuilder.AppendLine("        <th class=\"no\">No</th>");
                    MessageBuilder.AppendLine("        <th class=\"name\">アイテム名</th>");
                    MessageBuilder.AppendLine("        <th class=\"type\">種別</th>");
                    MessageBuilder.AppendLine("        <th class=\"price\">価値</th>");
                    MessageBuilder.AppendLine("        <th class=\"number\">個数</th>");
                    MessageBuilder.AppendLine("      </tr>");
                    MessageBuilder.AppendLine("      </thead>");
                    MessageBuilder.AppendLine("      <tbody>");

                    // アイテム
                    CommonUnitDataEntity.have_item_listRow BoxItemRow = null;
                    for (i = 1; i <= BoxItemCount; i++)
                    {
                        int ItemNo = 0;
                        BoxItemRow = (CommonUnitDataEntity.have_item_listRow)Mine.HaveItem.DefaultView[i - 1].Row;
                        ItemNo = BoxItemRow.it_num;
                        bool IsCreated = BoxItemRow.created;

                        CommonItemEntity.item_listRow ItemRow = LibItem.GetItemRow(ItemNo, IsCreated);
                        ItemTypeEntity.mt_item_typeRow TypeRow = LibItemType.GetTypeRow(ItemRow.it_type);
                        ItemTypeEntity.mt_item_type_sub_categoryRow SubCategoryRow = LibItemType.GetSubCategoryRow(ItemRow.it_sub_category);

                        if (IsCreated)
                        {
                            ItemRow.it_name = HaveItemRow.it_name;
                            ItemRow.it_comment = HaveItemRow.it_comment;
                            ItemRow.it_effect = HaveItemRow.it_effect;
                            ItemRow.it_price = HaveItemRow.it_price;
                            ItemRow.it_seller = HaveItemRow.it_seller;
                        }

                        string ClassType = "main";

                        switch (TypeRow.categ_div)
                        {
                            case Status.TypeCategoryDiv.Main:
                                ClassType = "main";
                                break;
                            case Status.TypeCategoryDiv.Sub:
                                ClassType = "sub";
                                break;
                            case Status.TypeCategoryDiv.Head:
                                ClassType = "head";
                                break;
                            case Status.TypeCategoryDiv.Body:
                                ClassType = "body";
                                break;
                            case Status.TypeCategoryDiv.Accesory:
                                ClassType = "accesory";
                                break;
                            default:
                                ClassType = "using";
                                break;
                        }

                        string NewItems = "";
                        string NewTips = "";
                        if (HaveItemRow._new)
                        {
                            NewItems = " new";
                            NewTips = "<span class=\"newit\">New!</span>";
                        }

                        string Used = "";
                        string Creatable = "";
                        int StackCount = ItemRow.it_stack;
                        if (StackCount == 0) { StackCount = 99; }
                        if (ItemRow.it_rare) { StackCount = 1; }

                        if (ItemRow.it_use_item != 0)
                        {
                            Used = " used";
                        }
                        if (IsCreated)
                        {
                            Creatable = " creatable";
                        }

                        MessageBuilder.AppendLine("      <tr class=\"main_text " + ClassType + NewItems + "\">");
                        MessageBuilder.AppendLine("        <td class=\"no\">" + BoxItemRow.have_no + "<div class=\"toolhelp\">" + LibComment.Item(ItemNo, IsCreated, Mine.CharacterName) + "</div></td>");
                        MessageBuilder.AppendLine("        <td class=\"name" + Used + Creatable + "\">" + ItemRow.it_name + NewTips + "</td>");
                        MessageBuilder.AppendLine("        <td class=\"type\">" + TypeRow.type + "</td>");
                        MessageBuilder.AppendLine("        <td class=\"price\">" + ItemRow.it_seller.ToString("#,0.#") + "</td>");
                        MessageBuilder.AppendLine("        <td class=\"number\">" + BoxItemRow.it_box_count + "<sub> / " + StackCount + "</sub></td>");
                        MessageBuilder.AppendLine("      </tr>");
                    }

                    MessageBuilder.AppendLine("  </tbody>");
                    MessageBuilder.AppendLine("  </table>");

                    MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                    MessageBuilder.AppendLine("</section>");
                }

                MessageBuilder.AppendLine("<section id=\"eqitem\">");
                MessageBuilder.AppendLine("  <h2>装備アイテム</h2>");
                MessageBuilder.AppendLine("  <table class=\"eqlist\" summary=\"装備しているアイテムの一覧です。\">");
                MessageBuilder.AppendLine("    <tr>");
                MessageBuilder.AppendLine("      <th class=\"parts\">部位</th>");
                MessageBuilder.AppendLine("      <th class=\"name\">アイテム名</th>");
                MessageBuilder.AppendLine("      <th class=\"type\">種別</th>");
                MessageBuilder.AppendLine("    </tr>");

                MessageBuilder.AppendLine("    <tr>");
                MessageBuilder.AppendLine("      <td class=\"parts\">メイン</td>");

                CommonItemEntity.item_listRow MainItemRow = Mine.GetHaveItemEquiped(Status.EquipSpot.Main);
                if (MainItemRow != null)
                {
                    MessageBuilder.AppendLine("      <td class=\"name\">" + MainItemRow.it_name + "</td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">" + LibItemType.GetTypeName(MainItemRow.it_type) + "</td>");
                }
                else
                {
                    MessageBuilder.AppendLine("      <td class=\"name\" title=\"近接武器や遠隔武器を装備できます。\">素手</td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">格闘</td>");
                }

                MessageBuilder.AppendLine("    </tr>");

                MessageBuilder.AppendLine("    <tr>");
                MessageBuilder.AppendLine("      <td class=\"parts\">サブ</td>");

                CommonItemEntity.item_listRow SubItemRow = Mine.GetHaveItemEquiped(Status.EquipSpot.Sub);
                if (SubItemRow != null)
                {
                    MessageBuilder.AppendLine("      <td class=\"name\">" + SubItemRow.it_name + "</td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">" + LibItemType.GetTypeName(SubItemRow.it_type) + "</td>");
                }
                else
                {
                    MessageBuilder.AppendLine("      <td class=\"name\" title=\"片手用武器や盾を装備できます。\"><span class=\"none\">Empty</span></td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">&nbsp;</td>");
                }
                MessageBuilder.AppendLine("    </tr>");

                MessageBuilder.AppendLine("    <tr>");
                MessageBuilder.AppendLine("     <td class=\"parts\">頭部</td>");

                CommonItemEntity.item_listRow HeadItemRow = Mine.GetHaveItemEquiped(Status.EquipSpot.Head);
                if (HeadItemRow != null)
                {
                    MessageBuilder.AppendLine("      <td class=\"name\">" + HeadItemRow.it_name + "</td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">" + LibItemType.GetTypeName(HeadItemRow.it_type) + "</td>");
                }
                else
                {
                    MessageBuilder.AppendLine("      <td class=\"name\" title=\"頭部に装備できる防具を装備できます。\"><span class=\"none\">Empty</span></td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">&nbsp;</td>");
                }

                MessageBuilder.AppendLine("    </tr>");

                MessageBuilder.AppendLine("    <tr>");
                MessageBuilder.AppendLine("      <td class=\"parts\">身体</td>");

                CommonItemEntity.item_listRow BodyItemRow = Mine.GetHaveItemEquiped(Status.EquipSpot.Body);
                if (BodyItemRow != null)
                {
                    MessageBuilder.AppendLine("      <td class=\"name\">" + BodyItemRow.it_name + "</td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">" + LibItemType.GetTypeName(BodyItemRow.it_type) + "</td>");
                }
                else
                {
                    MessageBuilder.AppendLine("      <td class=\"name\" title=\"身体に装備できる防具を装備できます。\"><span class=\"none\">Empty</span></td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">&nbsp;</td>");
                }

                MessageBuilder.AppendLine("    </tr>");

                MessageBuilder.AppendLine("    <tr>");
                MessageBuilder.AppendLine("      <td class=\"parts\">装飾</td>");

                CommonItemEntity.item_listRow AcceItemRow = Mine.GetHaveItemEquiped(Status.EquipSpot.Accesory);
                if (AcceItemRow != null)
                {
                    MessageBuilder.AppendLine("      <td class=\"name\">" + AcceItemRow.it_name + "</td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">" + LibItemType.GetTypeName(AcceItemRow.it_type) + "</td>");
                }
                else
                {
                    MessageBuilder.AppendLine("      <td class=\"name\" title=\"装飾に装備できるアクセサリを装備できます。\"><span class=\"none\">Empty</span></td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">&nbsp;</td>");
                }

                MessageBuilder.AppendLine("   </tr>");
                MessageBuilder.AppendLine("  </table>");
                MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");

                MessageBuilder.AppendLine("<section id=\"perks\">");
                MessageBuilder.AppendLine("  <h2>スキル</h2>");
                MessageBuilder.AppendLine("  <div id=\"prvdata\" class=\"ui-tabs\">");
                MessageBuilder.AppendLine("  <ul class=\"ui-tabs-nav ui-helper-reset ui-helper-clearfix\">");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top ui-tabs-selected ui-state-active\"><a href=\"javascript:void(0);\" data-viewer=\"all\">すべて</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"arts\">アーツ</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"support\">サポート</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"special\">スペシャル</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"install\">クラス</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"private\">プライベート</a></li>");
                MessageBuilder.AppendLine("  </ul>");
                MessageBuilder.AppendLine("  <table class=\"sklist list_data\">");
                MessageBuilder.AppendLine("	<thead>");
                MessageBuilder.AppendLine("  <tr>");
                MessageBuilder.AppendLine("	  <th class=\"name\">スキル名</th>");
                MessageBuilder.AppendLine("	  <th class=\"mp\">MP</th>");
                MessageBuilder.AppendLine("	  <th class=\"tp\">TP</th>");
                MessageBuilder.AppendLine("	  <th class=\"type\">種別</th>");
                MessageBuilder.AppendLine("	 </tr>");
                MessageBuilder.AppendLine("	</thead>");
                MessageBuilder.AppendLine("	<tbody>");


                // スキル(SortSkillKeyNumでソートする）
                foreach (CommonSkillEntity.skill_listRow SkillRow in Mine.ClassHaveSkill)
                {
                    string ClassType = "special";
                    string SkillType = LibSkillType.GetName(SkillRow.sk_arts_category);
                    string UsingNone = "";

                    string SkillName = SkillRow.sk_name;

                    switch (SkillRow.sk_type)
                    {
                        case Status.SkillType.Arts:
                            ClassType = "arts";
                            SkillType = "アーツ";
                            break;
                        case Status.SkillType.Support:
                            ClassType = "support";
                            SkillType = "サポート";
                            break;
                        case Status.SkillType.Assist:
                            ClassType = "assist";
                            SkillType = "アシスト";
                            break;
                        case Status.SkillType.Special:
                            ClassType = "special";
                            SkillType = "スペシャル";
                            if (Mine.ContinueBonus < 4)
                            {
                                UsingNone = " notuse";
                            }
                            break;
                        default:
                            ClassType = "none";
                            SkillType = "不明";
                            break;
                    }

                    MessageBuilder.AppendLine("    <tr class=\"main_text install " + ClassType + "\">");

                    MessageBuilder.AppendLine("	  <td class=\"name" + UsingNone + "\">" + SkillName + "<div class=\"toolhelp\">" + LibComment.Skill(SkillRow.sk_id) + "</div></td>");
                    MessageBuilder.AppendLine("	  <td class=\"mp\">" + SkillRow.sk_mp + "</td>");
                    MessageBuilder.AppendLine("	  <td class=\"tp\">" + SkillRow.sk_tp + "</td>");
                    MessageBuilder.AppendLine("	  <td class=\"type\">クラス/" + SkillType + "</td>");
                    MessageBuilder.AppendLine("	</tr>");
                }


                // スキル(SortSkillKeyNumでソートする）
                foreach (CommonUnitDataEntity.have_skill_listRow HaveSkillRow in Mine.HaveSkill)
                {
                    int SkillNo = HaveSkillRow.sk_num;

                    CommonSkillEntity.skill_listRow SkillRow = LibSkill.GetSkillRow(SkillNo);

                    string ClassType = "special";
                    string SkillType = LibSkillType.GetName(SkillRow.sk_arts_category);
                    string UsingNone = "";

                    string SkillName = SkillRow.sk_name;

                    switch (SkillRow.sk_type)
                    {
                        case Status.SkillType.Arts:
                            ClassType = "arts";
                            SkillType = "アーツ";
                            break;
                        case Status.SkillType.Support:
                            ClassType = "support";
                            SkillType = "サポート";
                            break;
                        case Status.SkillType.Assist:
                            ClassType = "assist";
                            SkillType = "アシスト";
                            break;
                        case Status.SkillType.Special:
                            ClassType = "special";
                            SkillType = "スペシャル";
                            if (Mine.ContinueBonus < 4)
                            {
                                UsingNone = " notuse";
                            }
                            break;
                        default:
                            ClassType = "private";
                            SkillType = "不明";
                            break;
                    }

                    string NewItems = "";
                    string NewTips = "";
                    if (HaveSkillRow._new)
                    {
                        NewItems = " new";
                        NewTips = "<span class=\"newit\">New!</span>";
                    }

                    MessageBuilder.AppendLine("    <tr class=\"main_text private " + ClassType + NewItems + "\">");

                    MessageBuilder.AppendLine("	  <td class=\"name" + UsingNone + "\">" + SkillName + NewTips + "<div class=\"toolhelp\">" + LibComment.Skill(SkillRow.sk_id) + "</div></td>");
                    MessageBuilder.AppendLine("	  <td class=\"mp\">" + SkillRow.sk_mp + "</td>");
                    MessageBuilder.AppendLine("	  <td class=\"tp\">" + SkillRow.sk_tp + "</td>");
                    MessageBuilder.AppendLine("	  <td class=\"type\">プライベート/" + SkillType + "</td>");
                    MessageBuilder.AppendLine("	</tr>");
                }

                MessageBuilder.AppendLine("	</tbody>");
                MessageBuilder.AppendLine("  </table>");
                MessageBuilder.AppendLine("  </div>");
                if (Mine.HaveSkill.Count == 0)
                {
                    MessageBuilder.AppendLine("<p>所持しているスキルはありません。</p>");
                }
                MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");

                MessageBuilder.AppendLine("<section id=\"battle\">");
                MessageBuilder.AppendLine("  <h2>戦術</h2>");
                MessageBuilder.AppendLine("  <table class=\"atlist\" summary=\"バトル時の行動内容です。\">");
                MessageBuilder.AppendLine("    <tr>");
                MessageBuilder.AppendLine("      <th class=\"rank\">順位</th>");
                MessageBuilder.AppendLine("      <th class=\"target\">ターゲット</th>");
                MessageBuilder.AppendLine("      <th class=\"action\">アクション</th>");
                MessageBuilder.AppendLine("    </tr>");

                foreach (CommonUnitDataEntity.action_listRow ActionRow in Mine.ActionList)
                {
                    int ActionNo = 0;
                    ActionNo = ActionRow.action_no;

                    MessageBuilder.AppendLine("   <tr>");
                    MessageBuilder.AppendLine("      <td class=\"rank\">" + ActionNo + "</td>");

                    string Target = LibAction.GetActionTargetName(ActionRow.action_target);
                    int TargetNo = LibAction.GetTargetNo(ActionRow.action_target);
                    string TargetMemberName = "";
                    if (TargetNo > 0 && PartyMembers.Rows.Count >= TargetNo)
                    {
                        TargetMemberName = CharaMini.GetNickName((int)PartyMembers.Rows[TargetNo - 1]["entry_no"]);
                    }

                    Target = Target.Replace("<chara_name>", TargetMemberName);

                    MessageBuilder.AppendLine("      <td class=\"target\">" + LibResultText.EscapeHTML(Target) + "</td>");

                    switch (ActionRow.action)
                    {
                        case Status.ActionType.MainAttack:
                            MessageBuilder.AppendLine("      <td class=\"action\">攻撃</td>");
                            break;
                        case Status.ActionType.NoAction:
                            MessageBuilder.AppendLine("      <td class=\"action\">様子を見る</td>");
                            break;
                        case Status.ActionType.SpecialArtsAttack:
                            MessageBuilder.AppendLine("      <td class=\"action\">スペシャル「" + LibSkill.GetSkillName(ActionRow.perks_id) + "」を使用</td>");
                            break;
                        default:
                            MessageBuilder.AppendLine("      <td class=\"action\">スキル「" + LibSkill.GetSkillName(ActionRow.perks_id) + "」を使用</td>");
                            break;
                    }

                    MessageBuilder.AppendLine("   </tr>");
                }

                MessageBuilder.AppendLine("</table>");
                MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");

                MessageBuilder.AppendLine("<section id=\"key\">");
                MessageBuilder.AppendLine("   <h2>貴重品</h2>");
                MessageBuilder.AppendLine("  <div id=\"keydata\" class=\"ui-tabs\">");
                MessageBuilder.AppendLine("  <ul class=\"ui-tabs-nav ui-helper-reset ui-helper-clearfix\">");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top ui-tabs-selected ui-state-active\"><a href=\"javascript:void(0);\" data-viewer=\"all\">すべて</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"temp\">一時的</a></li>");
                MessageBuilder.AppendLine("  <li class=\"ui-state-default ui-corner-top\"><a href=\"javascript:void(0);\" data-viewer=\"never\">永久的</a></li>");
                MessageBuilder.AppendLine("  </ul>");
                MessageBuilder.AppendLine("   <table class=\"kylist list_data\" summary=\"貴重品の一覧です。\">");
                MessageBuilder.AppendLine("    <thead>");
                MessageBuilder.AppendLine("     <tr>");
                MessageBuilder.AppendLine("       <th class=\"name\">貴重品名</th>");
                MessageBuilder.AppendLine("       <th class=\"type\">種類</th>");
                MessageBuilder.AppendLine("     </tr>");
                MessageBuilder.AppendLine("    </thead>");
                MessageBuilder.AppendLine("    <tbody>");

                // 貴重品
                foreach (CommonUnitDataEntity.key_item_listRow KeyRow in Mine.KeyItemList)
                {
                    int HaveKeyItemNo = 0;
                    HaveKeyItemNo = KeyRow.key_item_id;
                    KeyItemEntity.mt_key_item_listRow KeyItemRow = LibKeyItem.GetKeyItemRow(HaveKeyItemNo);

                    string ClassType = "temp";
                    string TypeName = "一時的";

                    switch (KeyItemRow.key_type)
                    {
                        case Status.KeyItemType.Temporary:
                            ClassType = "temp";
                            TypeName = "一時的";
                            break;
                        case Status.KeyItemType.Never:
                            ClassType = "never";
                            TypeName = "永久的";
                            break;
                        default:
                            ClassType = "none";
                            break;
                    }

                    string NewItems = "";
                    string NewTips = "";
                    if (KeyRow._new)
                    {
                        NewItems = " new";
                        NewTips = "<span class=\"newit\">New!</span>";

                    }

                    MessageBuilder.AppendLine("     <tr class=\"main_text " + ClassType + NewItems + "\">");
                    MessageBuilder.AppendLine("       <td class=\"name\">" + KeyItemRow.keyitem_name + NewTips + "<div class=\"toolhelp\">" + KeyItemRow.keyitem_comment + "</div></td>");
                    MessageBuilder.AppendLine("       <td class=\"type\">" + TypeName + "</td>");
                    MessageBuilder.AppendLine("     </tr>");
                }

                MessageBuilder.AppendLine("  </tbody>");
                MessageBuilder.AppendLine("   </table>");
                MessageBuilder.AppendLine("   </div>");
                if (Mine.KeyItemList.Count == 0)
                {
                    MessageBuilder.AppendLine("<p>所持している貴重品はありません。</p>");
                }

                MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");


                MessageBuilder.AppendLine("<section id=\"quest\">");
                MessageBuilder.AppendLine("  <h2>クエスト</h2>");
                MessageBuilder.AppendLine("  <h3>現在引き受けているクエスト</h3>");
                MessageBuilder.AppendLine("  <table class=\"qmlist list_data\" summary=\"現在オファーしているクエストの一覧です。\">");
                MessageBuilder.AppendLine("    <thead>");
                MessageBuilder.AppendLine("    <tr>");
                MessageBuilder.AppendLine("      <th class=\"name\">クエスト名</th>");
                MessageBuilder.AppendLine("      <th class=\"offers\">依頼者</th>");
                MessageBuilder.AppendLine("      <th class=\"type\">種別</th>");
                MessageBuilder.AppendLine("    </tr>");
                MessageBuilder.AppendLine("    </thead>");
                MessageBuilder.AppendLine("    <tbody>");

                // オファークエスト
                Mine.QuestList.DefaultView.RowFilter = "clear_fg=false";
                int OfferQuestCount = Mine.QuestList.DefaultView.Count;
                CommonUnitDataEntity.quest_listRow OfferQuestRow = null;
                for (i = 1; i <= OfferQuestCount; i++)
                {
                    int OfferQuestNo = 0;
                    OfferQuestRow = (CommonUnitDataEntity.quest_listRow)Mine.QuestList.DefaultView[i - 1].Row;
                    OfferQuestNo = OfferQuestRow.quest_id;
                    QuestDataEntity.quest_listRow QuestRow = LibQuest.GetQuestRow(OfferQuestNo);
                    QuestDataEntity.mt_quest_stepRow NowQuestStepRow = LibQuest.GetQuestStepRow(OfferQuestNo, Mine.QuestStage(OfferQuestNo));
                    DataView QuestView = LibQuest.GetQuestStepList(OfferQuestNo, Mine.QuestStage(OfferQuestNo));

                    MessageBuilder.AppendLine("    <tr class=\"main_text\">");
                    MessageBuilder.AppendLine("      <td class=\"name\">" + QuestRow.quest_name + "<div class=\"toolhelp\">" + NowQuestStepRow.comment);
                    if (QuestView.Count > 0)
                    {
                        MessageBuilder.AppendLine("      <hr class=\"qs_hr\">");
                    }
                    foreach (DataRowView CommentRow in QuestView)
                    {
                        MessageBuilder.AppendLine(CommentRow["comment"].ToString() + "<br />");
                    }
                    MessageBuilder.AppendLine("</div></td>");
                    MessageBuilder.AppendLine("      <td class=\"offers\">" + QuestRow.quest_client + "</td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">" + LibQuest.QuestTypeName(QuestRow.quest_type) + "</td>");
                    MessageBuilder.AppendLine("    </tr>");
                }

                MessageBuilder.AppendLine("    </tbody>");
                MessageBuilder.AppendLine("  </table>");

                if (OfferQuestCount == 0)
                {
                    MessageBuilder.AppendLine("<p>引き受けているクエストはありません。</p>");
                }

                MessageBuilder.AppendLine("  <h3>コンプリート済のクエスト</h3>");
                MessageBuilder.AppendLine("  <p><a href=\"javascript:void(0)\" id=\"compque\">表示切替</a></p>");
                MessageBuilder.AppendLine("  <div id=\"compquelist\">");
                MessageBuilder.AppendLine("  <table class=\"qmlist list_data\" summary=\"コンプリート済のクエスト一覧です。\">");
                MessageBuilder.AppendLine("    <thead>");
                MessageBuilder.AppendLine("    <tr>");
                MessageBuilder.AppendLine("      <th class=\"name\">クエスト名</th>");
                MessageBuilder.AppendLine("      <th class=\"offers\">依頼者</th>");
                MessageBuilder.AppendLine("      <th class=\"type\">種別</th>");
                MessageBuilder.AppendLine("    </tr>");
                MessageBuilder.AppendLine("    </thead>");
                MessageBuilder.AppendLine("    <tbody>");

                // コンプリートクエスト
                Mine.QuestList.DefaultView.RowFilter = "clear_fg=true";
                int CompleteQuestCount = Mine.QuestList.DefaultView.Count;
                CommonUnitDataEntity.quest_listRow CompleteQuestRow = null;
                for (i = 1; i <= CompleteQuestCount; i++)
                {
                    int CompleteQuestNo = 0;
                    CompleteQuestRow = (CommonUnitDataEntity.quest_listRow)Mine.QuestList.DefaultView[i - 1].Row;
                    CompleteQuestNo = CompleteQuestRow.quest_id;
                    QuestDataEntity.quest_listRow QuestRow = LibQuest.GetQuestRow(CompleteQuestNo);
                    QuestDataEntity.mt_quest_stepRow NowQuestStepRow = LibQuest.GetQuestStepRow(CompleteQuestNo, Mine.QuestStage(CompleteQuestNo));
                    DataView QuestView = LibQuest.GetQuestStepList(CompleteQuestNo, Mine.QuestStage(CompleteQuestNo));

                    MessageBuilder.AppendLine("    <tr class=\"main_text\">");
                    MessageBuilder.AppendLine("      <td class=\"name\">" + QuestRow.quest_name + "<div class=\"toolhelp\">" + NowQuestStepRow.comment);
                    if (QuestView.Count > 0)
                    {
                        MessageBuilder.AppendLine("      <hr class=\"qs_hr\">");
                    }
                    foreach (DataRowView CommentRow in QuestView)
                    {
                        MessageBuilder.AppendLine(CommentRow["comment"].ToString() + "<br />");
                    }
                    MessageBuilder.AppendLine("</div></td>");
                    MessageBuilder.AppendLine("      <td class=\"offers\">" + QuestRow.quest_client + "</td>");
                    MessageBuilder.AppendLine("      <td class=\"type\">" + LibQuest.QuestTypeName(QuestRow.quest_type) + "</td>");
                    MessageBuilder.AppendLine("    </tr>");
                }

                MessageBuilder.AppendLine("    </tbody>");
                MessageBuilder.AppendLine("  </table>");
                MessageBuilder.AppendLine("  </div>");
                if (CompleteQuestCount == 0)
                {
                    MessageBuilder.AppendLine("<p>完了したクエストはありません。</p>");
                }

                MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");

                MessageBuilder.AppendLine("<section id=\"icon\">");
                MessageBuilder.AppendLine("   <h2>アイコンリスト</h2>");
                MessageBuilder.AppendLine("   <table class=\"inlist\" summary=\"設定されているアイコンの一覧です。\">");

                // アイコン
                int IconCount = Mine.IconList.Count;

                if ((Mine.IconList.Count % 10) > 0)
                {
                    IconCount += 10 - Mine.IconList.Count % 10;
                }

                for (i = 1; i <= IconCount; i++)
                {
                    if (i == 1 || 0 == ((i - 1) % 10))
                    {
                        MessageBuilder.AppendLine("     <tr>");
                    }

                    if (i > Mine.IconList.Count)
                    {
                        MessageBuilder.AppendLine("       <td class=\"icon\">&nbsp;</td>");
                    }
                    else
                    {
                        CommonUnitDataEntity.icon_listRow IconRow = Mine.IconList[i - 1];

                        MessageBuilder.AppendLine("       <td class=\"icon\">No." + IconRow.icon_id + "<br />" + Mine.GetIconUrl(IconRow.icon_id, Status.IconSize.M) + "</td>");
                    }

                    if (i == IconCount || 0 == (i % 10))
                    {
                        MessageBuilder.AppendLine("     </tr>");
                    }
                }

                MessageBuilder.AppendLine("   </table>");
                if (Mine.IconList.Count == 0)
                {
                    MessageBuilder.AppendLine("<p>設定されているアイコンはありません。</p>");
                }
                MessageBuilder.AppendLine("<div class=\"more\"><a href=\"#site-logo\">△TOP</a></div>");
                MessageBuilder.AppendLine("</section>");

                MessageBuilder.AppendLine("      <!--content end-->");
                MessageBuilder.AppendLine("      </article>");
                MessageBuilder.AppendLine("      </div>");
                MessageBuilder.AppendLine("  </div>");
                MessageBuilder.AppendLine("  <footer>");
                MessageBuilder.AppendLine("    <ul>");
                MessageBuilder.AppendLine("        <li><a href=\"/about/\">このサイトについて</a></li>");
                MessageBuilder.AppendLine("        <li><a href=\"/support/\">お問い合わせ</a></li>");
                MessageBuilder.AppendLine("    </ul>");
                MessageBuilder.AppendLine("    <small>");
                MessageBuilder.AppendLine("        &copy;2003-2012 Grand Blaze Game Master All Rights Reserved.");
                MessageBuilder.AppendLine("    </small>");
                MessageBuilder.AppendLine("  </footer>");
                MessageBuilder.AppendLine("  <!-- end .container --></div>");
                MessageBuilder.AppendLine("</body>");
                MessageBuilder.AppendLine("</html>");

                using (StreamWriter sw = new StreamWriter(LibConst.OutputFolderChara + LibUnitBaseMini.CharacterHTML(EntryNo), false, LibConst.FileEncod))
                {
                    sw.Write(MessageBuilder.ToString());
                }
            }
        }
    }
}
