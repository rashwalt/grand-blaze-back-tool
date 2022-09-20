using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;

namespace GrandBlazeStudio.RegistSet.StatusList
{
    partial class StatusListMain
    {
        private void TopListDraw()
        {
            StringBuilder MessageBuilder = new StringBuilder();

            MessageBuilder.AppendLine("	  	<div id=\"breadcrumbs\">");
            MessageBuilder.AppendLine("	  		<!--bread start-->");
            MessageBuilder.AppendLine("	  		<a href=\"/\">トップ</a> &gt; 冒険の結果");
            MessageBuilder.AppendLine("	  		<!--bread end-->");
            MessageBuilder.AppendLine("	  	</div>");
            MessageBuilder.AppendLine("	  	<article id=\"wcontent\">");
            MessageBuilder.AppendLine("	  <!--content start-->");
            MessageBuilder.AppendLine("        <h1>冒険の結果</h1>");
            MessageBuilder.AppendLine("        <section>");
            MessageBuilder.AppendLine("            <h2>冒険者一覧</h2>");
            MessageBuilder.AppendLine("            <table class=\"player-list\" summary=\"冒険者一覧の番号別リストへのリンクリストです。\">");

            int MaxCount = (int)((CharaMini.GetMaxNo - 1) / 100 + 1);
            int Plus = 0;

            while (true)
            {
                if ((MaxCount + Plus) % 4 == 0)
                {
                    break;
                }
                Plus++;
            }

            for (int i = 1; i <= (MaxCount + Plus); i++)
            {
                int Max = (i - 1) * 100 + 100;

                if (i == 1 || 1.0 == ((double)(i - 1) / 4.0))
                {
                    MessageBuilder.AppendLine("  <tr>");
                }

                if (i > MaxCount)
                {
                    MessageBuilder.AppendLine("    <td>&nbsp;</td>");
                }
                else
                {
                    MessageBuilder.AppendLine("    <td><a href=\"list" + i.ToString("0000") + ".html\">" + ((i - 1) * 100 + 1) + " ～ " + Max + "</a></td>");
                }

                if (i == (MaxCount + Plus) || 1.0 == ((double)i / 4.0))
                {
                    MessageBuilder.AppendLine("  </tr>");
                }
            }

            MessageBuilder.AppendLine("            </table>");
            MessageBuilder.AppendLine("        </section>");
            MessageBuilder.AppendLine("        <section>");
            MessageBuilder.AppendLine("            <h2>リストメッセージ</h2>");
            MessageBuilder.AppendLine("            <table class=\"player-list\" summary=\"リストメッセージ一覧の番号別リストへのリンクリストです。\">");

            for (int i = 1; i <= (MaxCount + Plus); i++)
            {
                int Max = (i - 1) * 100 + 100;

                if (i == 1 || 1.0 == ((double)(i - 1) / 4.0))
                {
                    MessageBuilder.AppendLine("  <tr>");
                }

                if (i > MaxCount)
                {
                    MessageBuilder.AppendLine("    <td>&nbsp;</td>");
                }
                else
                {
                    MessageBuilder.AppendLine("    <td><a href=\"lcom" + i.ToString("0000") + ".html\">" + ((i - 1) * 100 + 1) + " ～ " + Max + "</a></td>");
                }

                if (i == (MaxCount + Plus) || 1.0 == ((double)i / 4.0))
                {
                    MessageBuilder.AppendLine("  </tr>");
                }
            }

            MessageBuilder.AppendLine("            </table>");
            MessageBuilder.AppendLine("        </section>");
            MessageBuilder.AppendLine("        ");
            MessageBuilder.AppendLine("        <section>");
            MessageBuilder.AppendLine("            <h2>リザルトツール</h2>");
            MessageBuilder.AppendLine("            <dl class=\"subtext\">");
            MessageBuilder.AppendLine("              <dt><a href=\"newplayer.html\">新規登録者一覧</a></dt>");
            MessageBuilder.AppendLine("              <dd>新規に登録されたキャラクターの一覧です。</dd>");
            MessageBuilder.AppendLine("              <dt><a href=\"/result/search/\">キャラクター検索</a></dt>");
            MessageBuilder.AppendLine("              <dd>キャラクターを検索します。</dd>");
            MessageBuilder.AppendLine("            </dl>");
            MessageBuilder.AppendLine("        </section>");
            MessageBuilder.AppendLine("        ");
            MessageBuilder.AppendLine("        <section>");
            MessageBuilder.AppendLine("            <h2>ダウンロード</h2>");
            MessageBuilder.AppendLine("            <dl class=\"subtext\">");
            MessageBuilder.AppendLine("              <dt><a href=\"/result/download/\">ダウンロード</a></dt>");
            MessageBuilder.AppendLine("              <dd>結果の圧縮ファイルが置かれています。</dd>");
            MessageBuilder.AppendLine("            </dl>");
            MessageBuilder.AppendLine("        ");
            MessageBuilder.AppendLine("        </section>");

            using (StreamWriter sw = new StreamWriter(GrandBlazeStudio.Properties.Settings.Default.ResultBasePath + "index.html", false, LibConst.FileEncod))
            {
                sw.Write(GetOutLine(MessageBuilder.ToString()));
            }
        }

        private string GetOutLine(string BodyText)
        {
            StringBuilder MessageBuilder = new StringBuilder();

            MessageBuilder.AppendLine("<!DOCTYPE HTML>");
            MessageBuilder.AppendLine("<html>");
            MessageBuilder.AppendLine("<head>");
            MessageBuilder.AppendLine("<meta charset=\"utf-8\">");
            MessageBuilder.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />");
            MessageBuilder.AppendLine("<title>冒険の結果 | Grand Blaze</title>");
            MessageBuilder.AppendLine("<link href=\"../static/css/common.css\" rel=\"stylesheet\" type=\"text/css\">");
            MessageBuilder.AppendLine("<link href=\"../static/css/dark-hive/jquery-ui-1.9.0.custom.min.css\" rel=\"stylesheet\" type=\"text/css\">");
            MessageBuilder.AppendLine("<script src=\"http://code.jquery.com/jquery-1.8.2.js\"></script>");
            MessageBuilder.AppendLine("<script src=\"../static/js/jquery-ui-1.9.0.custom.min.js\"></script>");
            MessageBuilder.AppendLine("<script src=\"../static/js/jquery.formset.min.js\"></script>");
            MessageBuilder.AppendLine("<script src=\"../static/js/main.min.js\"></script>");
            MessageBuilder.AppendLine("<link href=\"../static/css/result.css\" rel=\"stylesheet\" type=\"text/css\">");
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
            MessageBuilder.AppendLine("    <a id=\"site-logo\" href=\"http://www.grand-blaze.com/\"><img src=\"../static/images/common/site_logo.png\" alt=\"Grand Blaze\" /></a>");
            MessageBuilder.AppendLine("    <div id=\"header-right\">");
            MessageBuilder.AppendLine("        <!--{% include 'account/login_parts.html' %}-->");
            MessageBuilder.AppendLine("		<div id=\"searchcontrol\">");
            MessageBuilder.AppendLine("			<form action=\"/search/\" id=\"cse-search-box\">");
            MessageBuilder.AppendLine("			  <div>");
            MessageBuilder.AppendLine("			    <input type=\"hidden\" name=\"cx\" value=\"018114392011501256481:1jv92qvio2i\" />");
            MessageBuilder.AppendLine("			    <input type=\"hidden\" name=\"cof\" value=\"FORID:11\" />");
            MessageBuilder.AppendLine("			    <input type=\"hidden\" name=\"ie\" value=\"UTF-8\" />");
            MessageBuilder.AppendLine("			    <input type=\"text\" name=\"q\" id=\"q\" autocomplete=\"off\" size=\"31\" />");
            MessageBuilder.AppendLine("			    <button type=\"submit\" id=\"search-submit\" name=\"sa\" class=\"ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only\" role=\"button\" aria-disabled=\"false\" title=\"検索\"><span class=\"ui-button-icon-primary ui-icon ui-icon-search\"></span><span class=\"ui-button-text\">検索</span></button>");
            MessageBuilder.AppendLine("			  </div>");
            MessageBuilder.AppendLine("			</form>");
            MessageBuilder.AppendLine("			<script type=\"text/javascript\" src=\"http://www.google.com/cse/brand?form=cse-search-box&lang=ja\"></script>");
            MessageBuilder.AppendLine("	   </div>");
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
            MessageBuilder.AppendLine("	  <div id=\"sidebar1\">");
            MessageBuilder.AppendLine("	  	<!--sidebar start-->");
            MessageBuilder.AppendLine("	    <nav>");
            MessageBuilder.AppendLine("	    	<h4>");
            MessageBuilder.AppendLine("	    		冒険者一覧");
            MessageBuilder.AppendLine("	    	</h4>");
            MessageBuilder.AppendLine("	    	<dl id=\"side-menu\" class=\"ui-right-icon\">");

            for (int i = 1; i <= (int)((CharaMini.GetMaxNo - 1) / 100 + 1); i++)
            {
                int Max = (i - 1) * 100 + 100;

                MessageBuilder.AppendLine("		        <dt>");
                MessageBuilder.AppendLine("		        	<a href=\"list" + i.ToString("0000") + ".html\">");
                MessageBuilder.AppendLine("		        		" + ((i - 1) * 100 + 1) + " ～ " + Max + "<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("		        	</a>");
                MessageBuilder.AppendLine("		        </dt>");
            }

            MessageBuilder.AppendLine("			</dl>");
            MessageBuilder.AppendLine("	    	<h4>");
            MessageBuilder.AppendLine("	    		リストメッセージ");
            MessageBuilder.AppendLine("	    	</h4>");
            MessageBuilder.AppendLine("	    	<dl id=\"side-menu\" class=\"ui-right-icon\">");

            for (int i = 1; i <= (int)((CharaMini.GetMaxNo - 1) / 100 + 1); i++)
            {
                int Max = (i - 1) * 100 + 100;

                MessageBuilder.AppendLine("		        <dt>");
                MessageBuilder.AppendLine("		        	<a href=\"lcom" + i.ToString("0000") + ".html\">");
                MessageBuilder.AppendLine("		        		" + ((i - 1) * 100 + 1) + " ～ " + Max + "<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                MessageBuilder.AppendLine("		        	</a>");
                MessageBuilder.AppendLine("		        </dt>");
            }

            MessageBuilder.AppendLine("			</dl>");
            MessageBuilder.AppendLine("	    	<h4>");
            MessageBuilder.AppendLine("	    		リザルトツール");
            MessageBuilder.AppendLine("	    	</h4>");
            MessageBuilder.AppendLine("	    	<dl id=\"side-menu\" class=\"ui-right-icon\">");
            MessageBuilder.AppendLine("		        <dt>");
            MessageBuilder.AppendLine("		        	<a href=\"newplayer.html\">");
            MessageBuilder.AppendLine("		        		新規登録者一覧<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
            MessageBuilder.AppendLine("		        	</a>");
            MessageBuilder.AppendLine("		        </dt>");
            MessageBuilder.AppendLine("		        <dt>");
            MessageBuilder.AppendLine("		        	<a href=\"/result/search/\">");
            MessageBuilder.AppendLine("		        		キャラクター検索<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
            MessageBuilder.AppendLine("		        	</a>");
            MessageBuilder.AppendLine("		        </dt>");
            MessageBuilder.AppendLine("			</dl>");
            MessageBuilder.AppendLine("	    	<h4>");
            MessageBuilder.AppendLine("	    		ダウンロード");
            MessageBuilder.AppendLine("	    	</h4>");
            MessageBuilder.AppendLine("	    	<dl id=\"side-menu\" class=\"ui-right-icon\">");
            MessageBuilder.AppendLine("		        <dt>");
            MessageBuilder.AppendLine("		        	<a href=\"/result/download/\">");
            MessageBuilder.AppendLine("		        		ダウンロード<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
            MessageBuilder.AppendLine("		        	</a>");
            MessageBuilder.AppendLine("		        </dt>");
            MessageBuilder.AppendLine("			</dl>");
            MessageBuilder.AppendLine("	    </nav>");
            MessageBuilder.AppendLine("	    <!--sidebar end-->");
            MessageBuilder.AppendLine("	  </div>");
            MessageBuilder.AppendLine("	  <div id=\"view-container\">");
            MessageBuilder.AppendLine(BodyText);
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

            return MessageBuilder.ToString();
        }
    }
}
