using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Script
{
    /// <summary>
    /// 名称関連取得系
    /// </summary>
    public static class ScName
    {
        /// <summary>
        /// エリア名称取得
        /// </summary>
        /// <param name="AreaID">エリアID</param>
        /// <returns>名称</returns>
        public static string ByArea(int AreaID)
        {
            return LibResultText.CSSEscapeArea(LibArea.GetAreaName(AreaID));
        }

        /// <summary>
        /// マーク名称取得
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <returns>名称</returns>
        public static string ByMark(int MarkID)
        {
            return LibResultText.CSSEscapeMark(LibQuest.GetMarkName(MarkID));
        }

        /// <summary>
        /// クエスト名称取得
        /// </summary>
        /// <param name="QuestID">クエストID</param>
        /// <returns>名称</returns>
        public static string ByQuest(int QuestID)
        {
            return LibResultText.CSSEscapeQuest(LibQuest.GetQuestName(QuestID));
        }

        /// <summary>
        /// モンスター名称取得
        /// </summary>
        /// <param name="MonsterID">モンスターID</param>
        /// <returns>名称</returns>
        public static string ByMonster(int MonsterID)
        {
            return LibMonsterData.GetNickName(MonsterID);
        }

        /// <summary>
        /// アイテム名称取得
        /// </summary>
        /// <param name="ItemID">アイテムID</param>
        /// <returns>名称</returns>
        public static string ByItem(int ItemID)
        {
            return LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false));
        }

        /// <summary>
        /// 貴重品名称取得
        /// </summary>
        /// <param name="KeyID">貴重品ID</param>
        /// <returns>名称</returns>
        public static string ByKeyItem(int KeyID)
        {
            return LibResultText.CSSEscapeKeyItem(LibKeyItem.GetKeyItemName(KeyID));
        }

        /// <summary>
        /// お金入手メッセージ取得
        /// </summary>
        /// <param name="Money">お金</param>
        /// <returns>メッセージ</returns>
        public static string MsgMoney(int Money)
        {
            StringBuilder Msg = new StringBuilder();

            Msg.Append(LibResultText.CSSEscapeMoney(Money, false) + "を手に入れた。");

            return Msg.ToString();
        }

        /// <summary>
        /// EXP入手メッセージ取得
        /// </summary>
        /// <param name="Exp">EXP</param>
        /// <returns>メッセージ</returns>
        public static string MsgExp(int Exp)
        {
            StringBuilder Msg = new StringBuilder();

            Msg.Append(Exp + " Expを手に入れた！");

            return Msg.ToString();
        }

        /// <summary>
        /// アイテム入手メッセージ取得
        /// </summary>
        /// <param name="ItemID">アイテムID</param>
        /// <param name="ItemCount">入手個数</param>
        /// <returns>メッセージ</returns>
        public static string MsgItem(int ItemID, int ItemCount)
        {
            StringBuilder Msg = new StringBuilder();

            string OverOneBox = "";
            if (ItemCount > 1)
            {
                OverOneBox = "[" + ItemCount + "個]";
            }

            Msg.Append(LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + OverOneBox + "を手に入れた。");

            return Msg.ToString();
        }

        /// <summary>
        /// 貴重品入手メッセージ取得
        /// </summary>
        /// <param name="KeyItemID">アイテムID</param>
        /// <returns>メッセージ</returns>
        public static string MsgKeyItem(int KeyItemID)
        {
            StringBuilder Msg = new StringBuilder();

            Msg.Append("貴重品「" + LibResultText.CSSEscapeKeyItem(LibKeyItem.GetKeyItemName(KeyItemID)) + "」を手に入れた。");

            return Msg.ToString();
        }
    }
}
