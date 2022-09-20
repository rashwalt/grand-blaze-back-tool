using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using CommonLibrary;
using System.Data;
using Microsoft.VisualBasic.FileIO;

namespace GrandBlazeStudio.RegistSet.Update
{
    public class ArchiverSetup
    {
        /// <summary>
        /// 圧縮開始＆終了処理
        /// </summary>
        public void Done()
        {
            CreateArchiveIndex();

            // 書庫ファイル名の設定
            string TarFileName = string.Format("result{0:0000}.tar.bz2", GrandBlazeStudio.Properties.Settings.Default.UpdateCnt);
            string BeforeTarFileName = "result.tar.bz2";

            File.Delete(GrandBlazeStudio.Properties.Settings.Default.BasePath + BeforeTarFileName);

            string lhaplus = GrandBlazeStudio.Properties.Settings.Default.LhaplusPath;

            Process extProcess = Process.Start(lhaplus, string.Format("/c:bzip2 {0}", "\"" + GrandBlazeStudio.Properties.Settings.Default.ResultBasePath + "\""));
            extProcess.WaitForExit();

            CreateArchiveIndex();

            // ファイルのリネーム
            if (File.Exists(GrandBlazeStudio.Properties.Settings.Default.PublicArchivePath + TarFileName))
            {
                File.Delete(GrandBlazeStudio.Properties.Settings.Default.PublicArchivePath + TarFileName);
            }

            File.Move(GrandBlazeStudio.Properties.Settings.Default.DesktopPath + ".tar.bz2", GrandBlazeStudio.Properties.Settings.Default.PublicArchivePath + TarFileName);

            // 移動先ファイル削除
            if (Directory.Exists(GrandBlazeStudio.Properties.Settings.Default.PublicResultPath + GrandBlazeStudio.Properties.Settings.Default.Characters))
            {
                FileSystem.DeleteDirectory(GrandBlazeStudio.Properties.Settings.Default.PublicResultPath + GrandBlazeStudio.Properties.Settings.Default.Characters, DeleteDirectoryOption.DeleteAllContents);
            }
            if (Directory.Exists(GrandBlazeStudio.Properties.Settings.Default.PublicResultPath + GrandBlazeStudio.Properties.Settings.Default.Partys))
            {
                FileSystem.DeleteDirectory(GrandBlazeStudio.Properties.Settings.Default.PublicResultPath + GrandBlazeStudio.Properties.Settings.Default.Partys, DeleteDirectoryOption.DeleteAllContents);
            }
            if (Directory.Exists(GrandBlazeStudio.Properties.Settings.Default.PublicResultPath + GrandBlazeStudio.Properties.Settings.Default.Privates))
            {
                FileSystem.DeleteDirectory(GrandBlazeStudio.Properties.Settings.Default.PublicResultPath + GrandBlazeStudio.Properties.Settings.Default.Privates, DeleteDirectoryOption.DeleteAllContents);
            }

            string[] fs = Directory.GetFiles(GrandBlazeStudio.Properties.Settings.Default.PublicResultPath, "*.html");
            foreach (string file in fs)
            {
                if (file.IndexOf("search") >= 0)
                {
                    continue;
                }
                File.Delete(file);
            }

            FileSystem.CopyDirectory(LibConst.OutputFolderChara, GrandBlazeStudio.Properties.Settings.Default.PublicResultPath + GrandBlazeStudio.Properties.Settings.Default.Characters, true);
            FileSystem.CopyDirectory(LibConst.OutputFolderParty, GrandBlazeStudio.Properties.Settings.Default.PublicResultPath + GrandBlazeStudio.Properties.Settings.Default.Partys, true);

            string[] fs2 = Directory.GetFiles(GrandBlazeStudio.Properties.Settings.Default.ResultBasePath, "*.html");
            foreach (string file in fs2)
            {
                File.Copy(file, file.Replace(GrandBlazeStudio.Properties.Settings.Default.ResultBasePath, GrandBlazeStudio.Properties.Settings.Default.PublicResultPath));
            }
        }

        /// <summary>
        /// 結果圧縮ファイルインデックス作成
        /// </summary>
        private void CreateArchiveIndex()
        {
            StringBuilder MessageBuilder = new StringBuilder();
            LibUnitBaseMini CharaMini = new LibUnitBaseMini();

            MessageBuilder.AppendLine("{% load file_extras %}");
            MessageBuilder.AppendLine("<!DOCTYPE HTML>");
            MessageBuilder.AppendLine("<html>");
            MessageBuilder.AppendLine("<head>");
            MessageBuilder.AppendLine("<meta charset=\"utf-8\">");
            MessageBuilder.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />");
            MessageBuilder.AppendLine("<title>ダウンロード | Grand Blaze</title>");
            MessageBuilder.AppendLine("<link href=\"/static/css/common.css\" rel=\"stylesheet\" type=\"text/css\">");
            MessageBuilder.AppendLine("<link href=\"/static/css/dark-hive/jquery-ui-1.9.0.custom.min.css\" rel=\"stylesheet\" type=\"text/css\">");
            MessageBuilder.AppendLine("<script src=\"http://code.jquery.com/jquery-1.8.2.js\"></script>");
            MessageBuilder.AppendLine("<script src=\"/static/js/jquery-ui-1.9.0.custom.min.js\"></script>");
            MessageBuilder.AppendLine("<script src=\"/static/js/jquery.formset.min.js\"></script>");
            MessageBuilder.AppendLine("<script src=\"/static/js/main.min.js\"></script>");
            MessageBuilder.AppendLine("<link href=\"/static/css/result.css\" rel=\"stylesheet\" type=\"text/css\">");
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
            MessageBuilder.AppendLine("    <a id=\"site-logo\" href=\"http://www.grand-blaze.com/\"><img src=\"/static/images/common/site_logo.png\" alt=\"Grand Blaze\" /></a>");
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

            if (CharaMini.Count() > 0)
            {
                for (int i = 1; i <= (int)((CharaMini.GetMaxNo - 1) / 100 + 1); i++)
                {
                    int Max = (i - 1) * 100 + 100;

                    MessageBuilder.AppendLine("		        <dt>");
                    MessageBuilder.AppendLine("		        	<a href=\"../list" + i.ToString("0000") + ".html\">");
                    MessageBuilder.AppendLine("		        		" + ((i - 1) * 100 + 1) + " ～ " + Max + "<span class=\"ui-icon ui-icon-carat-1-e\"></span>");
                    MessageBuilder.AppendLine("		        	</a>");
                    MessageBuilder.AppendLine("		        </dt>");
                }
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
            MessageBuilder.AppendLine("	  	<div id=\"breadcrumbs\">");
            MessageBuilder.AppendLine("	  		<!--bread start-->");
            MessageBuilder.AppendLine("	  		<a href=\"/\">トップ</a> &gt; <a href=\"../\">冒険の結果</a> &gt; ダウンロード");
            MessageBuilder.AppendLine("	  		<!--bread end-->");
            MessageBuilder.AppendLine("	  	</div>");
            MessageBuilder.AppendLine("	  	<article id=\"wcontent\">");
            MessageBuilder.AppendLine("	  <!--content start-->");

            MessageBuilder.AppendLine("<h1>ダウンロード</h1>");
            MessageBuilder.AppendLine("   <section>");
            MessageBuilder.AppendLine("   <h2>基本ファイル</h2>");
            MessageBuilder.AppendLine("   <p><a href=\"/static/tar/grandblaze.tar.bz2\">grandblaze.tar.bz2</a><br />");
            MessageBuilder.AppendLine("   基本ファイルを解凍した後にできる「static」フォルダと同じ階層に、<br />");
            MessageBuilder.AppendLine("   オープンベータ４結果ファイルの「result」フォルダをおいてください。<br />");
            MessageBuilder.AppendLine("   Last Update : {{\"/tar/grandblaze.tar.bz2\"|get_time}}</p>");
            MessageBuilder.AppendLine("   </section>");
            MessageBuilder.AppendLine("   <section>");
            MessageBuilder.AppendLine("   <h2>アーカイブス</h2>");
            MessageBuilder.AppendLine("   <p>今までの冒険の結果の圧縮ファイルです。</p>");
            MessageBuilder.AppendLine("   <ul class=\"sublink\">");
            for (int UpdateCount = GrandBlazeStudio.Properties.Settings.Default.UpdateCnt; UpdateCount > 0; UpdateCount--)
            {
                MessageBuilder.AppendLine("     <li><a href=\"/static/tar/archive/result" + string.Format("{0:0000}", UpdateCount) + ".tar.bz2\">第" + UpdateCount + "回</a> ({{\"/tar/archive/result" + string.Format("{0:0000}", UpdateCount) + ".tar.bz2\"|get_size}} Byte)</li>");
            }
            MessageBuilder.AppendLine("   </ul>");
            MessageBuilder.AppendLine("   </section>");
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

            using (StreamWriter sw = new StreamWriter(GrandBlazeStudio.Properties.Settings.Default.PublicArchiveIndexPath + "index.html", false, LibConst.FileEncod))
            {
                sw.Write(MessageBuilder.ToString());
            }
        }
    }
}
