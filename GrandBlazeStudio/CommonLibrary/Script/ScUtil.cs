using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLibrary.Script
{
    /// <summary>
    /// ユーティリティ関連
    /// </summary>
    public static class ScUtil
    {
        public static StringBuilder PutString = new StringBuilder();

        /// <summary>
        /// 表示文章を出力
        /// </summary>
        /// <param name="text"></param>
        public static void Print(string text)
        {
            MatchEvaluator eva = new MatchEvaluator(MsgMatchEvaluator);
            Regex MsgTag = new Regex(@"\{\{\s*msg\s+(\S+)\s+(\S+)\s*\}\}\r?\n?");
            Regex MsgEndTag = new Regex(@"\{\{\s*endmsg\s*\}\}\r?\n?");
            text = MsgTag.Replace(text, eva);
            text = MsgEndTag.Replace(text, @"</dd></dl>");
            PutString.AppendLine(text);
        }

        /// <summary>
        /// メッセージタグの変換
        /// </summary>
        /// <param name="match">マッチング文字列</param>
        /// <returns>変換後文字</returns>
        private static string MsgMatchEvaluator(Match match)
        {
            if (match.Groups.Count <= 1) { return ""; }

            string HeaderStrting = match.Groups[2].Value.Trim(' ', '\'').Length > 0 ? @"<img src='" + match.Groups[2].Value.Trim(' ', '\'') + @"' />" : "";

            return string.Format(@"<dl class='sc-serif'><dt>{0}{1}</dt><dd>", HeaderStrting, match.Groups[1].Value.Trim(' ', '\''));
        }

        public static int Random(int Min, int Max)
        {
            return LibInteger.GetRandMax(Min, Max);
        }
    }
}
