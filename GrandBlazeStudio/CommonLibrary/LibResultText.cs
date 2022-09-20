using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 結果表示文章整形クラス
    /// </summary>
    public class LibResultText
    {
        /// <summary>
        /// アイテム専用整形
        /// </summary>
        /// <param name="ItemName">アイテム名称</param>
        /// <returns>整形済みアイテム名称</returns>
        public static string CSSEscapeItem(string ItemName)
        {
            return "<span class=\"item_name\">" + ItemName + "</span>";
        }

        /// <summary>
        /// 金額専用整形
        /// </summary>
        /// <param name="Money">金額</param>
        /// <param name="IsSymbol">記号付き？</param>
        /// <returns>整形済み金額</returns>
        public static string CSSEscapeMoney(int Money, bool IsSymbol)
        {
            string Symbol = "";

            if (IsSymbol)
            {
                if (Money >= 0)
                {
                    Symbol = "+";
                }
            }

            return "<span class=\"money\">" + Symbol + Money.ToString("N0") + "</span>ギムル";
        }

        /// <summary>
        /// インストールクラス専用整形
        /// </summary>
        /// <param name="ClassName">インストールクラス名称</param>
        /// <returns>整形済みインストールクラス名称</returns>
        public static string CSSEscapeInstallClass(string ClassName)
        {
            return "<span class=\"mt_install_class_list\">" + ClassName + "</span>";
        }

        /// <summary>
        /// スキル専用整形
        /// </summary>
        /// <param name="SkillName">スキル名称</param>
        /// <returns>整形済みスキル名称</returns>
        public static string CSSEscapeSkill(string SkillName)
        {
            return "<span class=\"perks_name\">" + SkillName + "</span>";
        }

        /// <summary>
        /// 貴重品専用整形
        /// </summary>
        /// <param name="KeyItemName">貴重品名称</param>
        /// <returns>整形済み貴重品名称</returns>
        public static string CSSEscapeKeyItem(string KeyItemName)
        {
            return "<span class=\"key_item_name\">" + KeyItemName + "</span>";
        }

        /// <summary>
        /// 実行アーツ名称専用整形
        /// </summary>
        /// <param name="ArtsName">アーツ名称</param>
        /// <returns>整形済みアーツ名称</returns>
        public static string CSSEscapeActArts(string ArtsName)
        {
            return "<span class=\"act_arts_name\">" + ArtsName + "</span>";
        }

        /// <summary>
        /// エリア名称専用整形
        /// </summary>
        /// <param name="ArtsName">エリア名称</param>
        /// <returns>整形済みエリア名称</returns>
        public static string CSSEscapeArea(string AreaName)
        {
            return "<span class=\"area_name\">" + AreaName + "</span>";
        }

        /// <summary>
        /// マーク名称専用整形
        /// </summary>
        /// <param name="MarkName">マーク名称</param>
        /// <returns>整形済みマーク名称</returns>
        public static string CSSEscapeMark(string MarkName)
        {
            return "<span class=\"mark_name\">" + MarkName + "</span>";
        }

        /// <summary>
        /// クエスト名称専用整形
        /// </summary>
        /// <param name="ArtsName">クエスト名称</param>
        /// <returns>整形済みクエスト名称</returns>
        public static string CSSEscapeQuest(string AreaName)
        {
            return "<span class=\"quest_name\">" + AreaName + "</span>";
        }

        /// <summary>
        /// キャラ名称専用整形
        /// </summary>
        /// <param name="CharacterName">キャラ名称</param>
        /// <returns>整形済みクエスト名称</returns>
        public static string CSSEscapeChara(string CharacterName)
        {
            return "<span class=\"ch_name\">" + CharacterName + "</span>";
        }

        /// <summary>
        /// HTML表示用に各要素を変換
        /// </summary>
        /// <param name="Target">ターゲット</param>
        /// <returns>整形済み文字列</returns>
        public static string EscapeHTML(string Target)
        {
            Target = Target.Replace("&", "&amp;");
            Target = Target.Replace("<", "&lt;");
            Target = Target.Replace(">", "&gt;");
            Target = Target.Replace("\"", "&quot;");

            return Target;
        }
    }
}
