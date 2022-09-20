using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// パーティ行動関連描画クラス
    /// </summary>
    public class LibPartyAction
    {
        /// <summary>
        /// システムメッセージ表示（セリフ以外）
        /// </summary>
        /// <param name="Mes">メッセージ内容</param>
        /// <param name="Level">メッセージレベル</param>
        /// <param name="IsParagraph">段落フラグ</param>
        public static string Message(string Mes, int Level, bool IsParagraph)
        {
            StringBuilder StringMessage = new StringBuilder();

            // div属性クラスを標準に設定
            string DivClass;

            switch (Level)
            {
                case Status.MessageLevel.Caution:
                    DivClass = "sys_cat";
                    break;
                case Status.MessageLevel.Error:
                    DivClass = "sys_err";
                    break;
                default:
                    DivClass = "sys_mes";
                    break;
            }

            string Syntax = "div";

            if (IsParagraph)
            {
                Syntax = "p";
            }

            StringMessage.Append("<" + Syntax + " class=\"" + DivClass + "\">");
            StringMessage.Append(Mes);
            StringMessage.Append("</" + Syntax + ">");

            return StringMessage.ToString();
        }

        /// <summary>
        /// システムメッセージ表示（セリフ以外）
        /// </summary>
        /// <param name="Mes">メッセージ内容</param>
        /// <param name="Level">メッセージレベル</param>
        public static string Message(string Mes, int Level)
        {
            return Message(Mes, Level, true);
        }
    }
}
