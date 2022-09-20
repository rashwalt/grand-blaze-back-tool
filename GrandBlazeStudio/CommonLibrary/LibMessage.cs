using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary
{
    /// <summary>
    /// セリフ関連クラス
    /// </summary>
    public class LibMessage
    {
        /// <summary>
        /// リストセリフの取得
        /// </summary>
        /// <param name="Serif">表示セリフ</param>
        /// <returns>表示セリフ</returns>
        public static string ListSerif(string Serif)
        {
            StringBuilder StringMessage = new StringBuilder();

            StringMessage.Append("<div class=\"list_message\">");
            StringMessage.Append(Serif);
            StringMessage.Append("</div>");

            return StringMessage.ToString();
        }

        /// <summary>
        /// セリフ変換
        /// </summary>
        /// <param name="Msg">メッセージ本文</param>
        /// <param name="QuestName">クエスト名称</param>
        /// <param name="Mine">セリフの主役</param>
        /// <param name="TargetChara">セリフのターゲット</param>
        /// <returns>変換後のメッセージ本文</returns>
        public static string ConvertMessage(string Msg, string QuestName, LibUnitBase Mine, LibUnitBase TargetChara)
        {
            //{t}  	行動の対象（攻撃目標となっている敵など）の名前（愛称）を指す代名詞です。複数が対象の場合は展開されません。
            Msg = Regex.Replace(Msg, "{t}", TargetChara.NickName);

            return ConvertMessage(Msg, QuestName, Mine);
        }

        /// <summary>
        /// セリフ変換
        /// </summary>
        /// <param name="Msg">メッセージ本文</param>
        /// <param name="QuestName">クエスト名称</param>
        /// <param name="Mine">セリフの主役</param>
        /// <returns>変換後のメッセージ本文</returns>
        public static string ConvertMessage(string Msg, string QuestName, LibUnitBase Mine)
        {
            //{hp} 	自分のHPを 現在値/最大値 で表示します。
            Msg = Regex.Replace(Msg, "{hp}", Mine.HPNow + "/" + Mine.HPMax);
            //{hpp} 	自分のHPを％で表示します。
            Msg = Regex.Replace(Msg, "{hpp}", Mine.HPDamageRate + "%");
            //{hppr} 	自分のHPの減少量を％で表示します。
            Msg = Regex.Replace(Msg, "{hppr}", (100 - Mine.HPDamageRate) + "%");
            //{mp} 	自分のMPを 現在値/最大値 で表示します。
            Msg = Regex.Replace(Msg, "{mp}", Mine.MPNow + "/" + Mine.MPMax);
            //{mpp} 	自分のMPを％で表示します。
            Msg = Regex.Replace(Msg, "{mpp}", Mine.MPDamageRate + "%");
            //{mppr} 	自分のMPの減少量を％で表示します。
            Msg = Regex.Replace(Msg, "{mppr}", (100 - Mine.MPDamageRate) + "%");
            //{tp} 	自分のTPを 現在値/最大値 で表示します。
            Msg = Regex.Replace(Msg, "{tp}", Mine.TPNow + "/" + Mine.TPMax);
            //{tpp} 	自分のTPを％で表示します。
            Msg = Regex.Replace(Msg, "{tpp}", Mine.TPDamageRate + "%");
            //{tppr} 	自分のTPの減少量を％で表示します。
            Msg = Regex.Replace(Msg, "{tppr}", (100 - Mine.TPDamageRate) + "%");
            //{me} 	自分の愛称を指す代名詞です。
            Msg = Regex.Replace(Msg, "{me}", Mine.NickName);
            //{dc} 	トドメを刺した数
            Msg = Regex.Replace(Msg, "{dc}", Mine.DestroyCount.ToString());
            //{dcj} 	トドメを刺した数(漢数字)
            Msg = Regex.Replace(Msg, "{dcj}", LibInteger.ConvertKansuji(Mine.DestroyCount));
            if (Mine.GetType() == typeof(LibPlayer))
            {
                //{main} 	装備中のアイテム名称(メイン)
                CommonItemEntity.item_listRow MainItemRow = ((LibPlayer)Mine).GetHaveItemEquiped(Status.EquipSpot.Main);
                Msg = Regex.Replace(Msg, "{main}", MainItemRow != null ? MainItemRow.it_name : "");
                //{sub} 	装備中のアイテム名称(サブ)
                CommonItemEntity.item_listRow SubItemRow = ((LibPlayer)Mine).GetHaveItemEquiped(Status.EquipSpot.Sub);
                Msg = Regex.Replace(Msg, "{sub}", SubItemRow != null ? SubItemRow.it_name : "");
                //{head} 	装備中のアイテム名称(頭部)
                CommonItemEntity.item_listRow HeadItemRow = ((LibPlayer)Mine).GetHaveItemEquiped(Status.EquipSpot.Head);
                Msg = Regex.Replace(Msg, "{head}", HeadItemRow != null ? HeadItemRow.it_name : "");
                //{body} 	装備中のアイテム名称(身体)
                CommonItemEntity.item_listRow BodyItemRow = ((LibPlayer)Mine).GetHaveItemEquiped(Status.EquipSpot.Body);
                Msg = Regex.Replace(Msg, "{body}", BodyItemRow != null ? BodyItemRow.it_name : "");
                //{acce} 	装備中のアイテム名称(装飾)
                CommonItemEntity.item_listRow AcceItemRow = ((LibPlayer)Mine).GetHaveItemEquiped(Status.EquipSpot.Accesory);
                Msg = Regex.Replace(Msg, "{acce}", AcceItemRow != null ? AcceItemRow.it_name : "");
            }
            //{arts} 	使ったアーツの名称
            Msg = Regex.Replace(Msg, "{arts}", Mine.UsedArtsName);

            return ConvertMessage(Msg, QuestName);
        }

        /// <summary>
        /// セリフ変換
        /// </summary>
        /// <param name="Msg">メッセージ本文</param>
        /// <param name="QuestName">クエスト名称</param>
        /// <returns>変換後のメッセージ本文</returns>
        public static string ConvertMessage(string Msg, string QuestName)
        {
            //{pos} 	現在のクエスト名称を指す代名詞です。
            Msg = Regex.Replace(Msg, "{pos}", QuestName);
            //{br} 	指定された場所で改行します。
            Msg = Regex.Replace(Msg, "{br}", "\n");
            //{random} 	1～999の数字をランダムで表示します。
            Msg = Regex.Replace(Msg, "{random}", LibInteger.GetRandMax(1, 999).ToString());
            ////{omikuji} 運勢をランダムで表示します。
            //string[] Unsei = new string[] { "大吉", "吉", "中吉", "小吉", "凶" };
            //int UnseiCnt = LibInteger.GetRand(Unsei.Length);
            //Msg = Regex.Replace(Msg, "{omikuji}", Unsei[UnseiCnt]);

            return ConvMessageNoHtml(Msg);
        }

        private static Dictionary<string, string> RangeKeyPrams = new Dictionary<string, string> {
            { "b", "strong" },
            { "i", "em" },
            { "u", "u" },
            { "s", "strike" }, 
            { "sh", "span class=\"serif_shout\"" },
            { "lo", "span class=\"serif_low\"" },
            { "gray", "span class=\"serif_gray\"" },
            { "silver", "span class=\"serif_silver\"" },
            { "maroon", "span class=\"serif_maroon\"" },
            { "red", "span class=\"serif_red\"" },
            { "olive", "span class=\"serif_olive\"" },
            { "yellow", "span class=\"serif_yellow\"" },
            { "green", "span class=\"serif_green\"" },
            { "lime", "span class=\"serif_lime\"" },
            { "teal", "span class=\"serif_teal\"" },
            { "aqua", "span class=\"serif_aqua\"" },
            { "navy", "span class=\"serif_navy\"" },
            { "blue", "span class=\"serif_blue\"" },
            { "purple", "span class=\"serif_purple\"" },
            { "fuchsia", "span class=\"serif_fuchsia\"" },
        };

        /// <summary>
        /// 徐々に置換
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        private static string ReplaceRange1(string Msg)
        {
            //########################################################
            //# 置換対象ベース文字列を左から右へ順次置換していくイメージ

            StringBuilder re_str = new StringBuilder();
            re_str.Append("");// ←処理が済んだ部分が付け足されていく。
            while (true)
            {
                Match mt = Regex.Match(Msg, "(?<before>.*?){(?<tag>.+?)}(?<after>.*)");
                if (!mt.Success)
                {
                    //# 置換識別子にマッチするものがなければ、置換処理を終了
                    re_str.Append(Msg);
                    break;
                }
                //# 置換識別子以前のものは処理済として戻り値に追加
                re_str.Append(mt.Groups["before"].ToString()); //# 「{XXX}」より前の処理対象外

                string key = mt.Groups["tag"].ToString();	//# 「{XXX}」の「XXX」の部分(=キー)
                Msg = mt.Groups["after"].ToString();	//# 「{XXX}」より後の部分

                if (RangeKeyPrams.ContainsKey(key))
                {
                    //# のこりの部分を範囲終端「{XXX}#」以前と以降に
                    string unit = ReplaceRange2(key, ref Msg);

                    //# 該当部分を有効にする場合は置換対象ベース文字列に戻す
                    Msg = unit + Msg;
                }
                else
                {
                    re_str.Append(Msg);
                    break;
                }
            }
            //# 置換された結果を返す。
            return re_str.ToString();
        }

        private static string ReplaceRange2(string key, ref string base_str)
        {
            string unit = "";
            string start_tag = RangeKeyPrams[key];
            string end_tag = start_tag;
            if (start_tag.IndexOf(" ") >= 0)
            {
                string[] splitter = start_tag.Split(' ');
                end_tag = splitter[0];
            }

            //# 同じ識別子とキーから、対応する終端を捜す。
            Match mt = Regex.Match(base_str, "(?<before>.*?){/" + key + "}(?<after>.*)");
            if (mt.Success)
            {
                unit = mt.Groups["before"].ToString();
                base_str = mt.Groups["after"].ToString();
                unit = "<" + start_tag + ">" + unit + "</" + end_tag + ">";
            }
            //# 対応する終端が無い場合、のこりを全て対象範囲とする。
            else
            {
                unit = "<" + start_tag + ">" + base_str + "</" + end_tag + ">";
                base_str = "";
            }
            //# 切り出した対象範囲部分文字列を返す。
            return unit;
        }

        /// <summary>
        /// セリフの中に含まれたHTMLタグを無効にする
        /// </summary>
        /// <param name="Msg">メッセージ本文</param>
        /// <returns>変換後のメッセージ本文</returns>
        public static string ConvMessageNoHtml(string Msg)
        {
            // 代名詞以外は無効に
            Msg = Regex.Replace(Msg, "<br />", "\n");
            Msg = Regex.Replace(Msg, "<", "&lt;");
            Msg = Regex.Replace(Msg, ">", "&gt;");
            Msg = Regex.Replace(Msg, "\n", "<br />");

            // 範囲文字列置換
            Msg = ReplaceRange1(Msg);

            return Msg;
        }

        /// <summary>
        /// メッセージの受信
        /// </summary>
        /// <param name="Sender">送信者</param>
        /// <param name="Receipt">受信者</param>
        /// <param name="MessageText">メッセージ内容</param>
        public static void SenderMessage(LibUnitBase Sender, LibUnitBase Receipt, string MessageText)
        {
            SenderMessage(Sender, Receipt, MessageText, Status.PlayerSysMemoType.ReceiveMessage);
        }

        /// <summary>
        /// メッセージの受信
        /// </summary>
        /// <param name="Sender">送信者</param>
        /// <param name="Receipt">受信者</param>
        /// <param name="MessageText">メッセージ内容</param>
        /// <param name="MessageSelect">メッセージ</param>
        public static void SenderMessage(LibUnitBase Sender, LibUnitBase Receipt, string MessageText, Status.PlayerSysMemoType MessageSelect)
        {
            int SenderEntryNo = Sender.EntryNo;

            LibUnitBaseMini CharaMini = new LibUnitBaseMini();

            // 送信時点での現在位置を取得
            int AreaMarkID = LibParty.GetPartyMarkID(LibParty.GetPartyNo(SenderEntryNo));
            string QuestName = LibQuest.GetQuestMarkName(AreaMarkID);

            StringBuilder PrivateMessage = new StringBuilder();

            if (Receipt.EntryNo == SenderEntryNo)
            {
                // 独り言の場合
                LibPlayerMemo.AddSystemMessage(Receipt.EntryNo, MessageSelect, "自分宛に送ったメッセージを受信しました。", Status.MessageLevel.Normal);
            }
            else
            {
                // 誰かからの送信の場合
                LibPlayerMemo.AddSystemMessage(Receipt.EntryNo, MessageSelect, CharaMini.GetNickNameWithLink(SenderEntryNo, 1) + "からメッセージを受信しました。", Status.MessageLevel.Normal);
            }

            MessageText = MessageText.Replace("<br>{nl}", "{nl}").Replace("{nl}<br>", "{nl}").Replace("<br />{nl}", "{nl}").Replace("{nl}<br />", "{nl}");
            string[] MessageList = MessageText.Split(new string[] { "{nl}" }, StringSplitOptions.None);

            foreach (string Text in MessageList)
            {
                PrivateMessage.Append(LibSerif.ConvertTextBySerif(Sender, Text.Replace("<br />", "\n"), "private_serif", QuestName, Receipt));
            }

            LibPlayerMemo.AddSystemMessage(Receipt.EntryNo, MessageSelect, PrivateMessage.ToString(), Status.MessageLevel.Normal);
        }
    }
}
