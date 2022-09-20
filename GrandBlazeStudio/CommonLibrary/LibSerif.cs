using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CommonLibrary.Character;

namespace CommonLibrary
{
    public static class LibSerif
    {
        /// <summary>
        /// セリフ表示
        /// </summary>
        /// <param name="Mine">セリフを言う人物</param>
        /// <param name="Situation">シチュエーション</param>
        /// <param name="SkillID">スキルID</param>
        /// <param name="QuestName">クエスト名称</param>
        /// <param name="Target">ターゲット</param>
        /// <returns>セリフ内容</returns>
        public static string Serif(LibUnitBase Mine, int Situation, int? SkillID, string QuestName, LibUnitBase Target)
        {
            StringBuilder SerifMessage = new StringBuilder();

            Mine.SerifList.DefaultView.RowFilter = "situation=" + Situation;
            if (SkillID.HasValue)
            {
                Mine.SerifList.DefaultView.RowFilter += " and (perks_id=" + SkillID.Value + " or perks_id=0 or perks_id is null)";
            }

            int SerifCount = Mine.SerifList.DefaultView.Count;
            string ClassType = "bc-serif";

            if (Situation == LibSituation.GetNo("戦闘開始") ||
                Situation == LibSituation.GetNo("戦闘勝利・圧勝") ||
                Situation == LibSituation.GetNo("戦闘勝利・普通") ||
                Situation == LibSituation.GetNo("戦闘勝利・辛勝"))
            {
                ClassType = "sc-serif";
            }

            if (SerifCount > 0)
            {
                // セリフの取得
                string MessageText = Mine.SerifList.DefaultView[LibInteger.GetRand(SerifCount)]["serif_text"].ToString();
                MessageText = MessageText.Replace("<br>{nl}", "{nl}").Replace("{nl}<br>", "{nl}").Replace("<br />{nl}", "{nl}").Replace("{nl}<br />", "{nl}");
                string[] SerifTexts = MessageText.Split(new string[] { "{nl}" }, StringSplitOptions.None);

                foreach (string SerifText in SerifTexts)
                {
                    string ConvSerif = ConvertTextBySerif(Mine, SerifText, ClassType, QuestName, Target);
                    if (ConvSerif.Length > 0)
                    {
                        SerifMessage.AppendLine(ConvSerif);
                    }
                }
            }

            return SerifMessage.ToString();
        }

        /// <summary>
        /// 文字列をセリフ形式に変換
        /// </summary>
        /// <param name="Mine">セリフを言う人物</param>
        /// <param name="SerifText">文字列</param>
        /// <param name="ClassType">divクラス名称</param>
        /// <param name="QuestName">クエスト名称</param>
        /// <param name="Target">ターゲット</param>
        /// <returns>変換後文字列</returns>
        public static string ConvertTextBySerif(LibUnitBase Mine, string SerifText, string ClassType, string QuestName, LibUnitBase Target)
        {
            Regex regx = new Regex("^/[a-z]+ ");
            Match mat = regx.Match(SerifText.Trim());
            string SerifCommand = "/say";
            string SerifBody = SerifText.Trim();
            if (mat.Success)
            {
                SerifCommand = mat.Value.Trim();
                SerifBody = SerifBody.Remove(0, mat.Length);
            }

            string IconTag = "";
            string IconAddClass = "";
            int IconSize = 0;

            switch (SerifCommand)
            {
                case "/shout":
                case "/sh":
                    // シャウト
                    SerifSubCommand(Mine, ref SerifBody, ref IconTag, ref IconSize);
                    if (Target == null)
                    {
                        SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine);
                    }
                    else
                    {
                        SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine, Target);
                    }
                    IconAddClass = GetIconAddClass(IconTag, IconSize);
                    return "<dl class=\"" + ClassType + IconAddClass + "\"><dt>" + IconTag + "<span class=\"" + Mine.PartyColor + "\">" + Mine.NickName + "</span></dt><dd>「<span class=\"serif_shout\">" + SerifBody + "</span>」</dd></dl>";
                case "/low":
                case "/lo":
                    // ロウ
                    SerifSubCommand(Mine, ref SerifBody, ref IconTag, ref IconSize);
                    if (Target == null)
                    {
                        SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine);
                    }
                    else
                    {
                        SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine, Target);
                    }
                    IconAddClass = GetIconAddClass(IconTag, IconSize);
                    return "<dl class=\"" + ClassType + IconAddClass + "\"><dt>" + IconTag + "<span class=\"" + Mine.PartyColor + "\">" + Mine.NickName + "</span></dt><dd>「<span class=\"serif_low\">" + SerifBody + "</span>」</dd></dl>";
                case "/emotion":
                case "/em":
                    // エモーション
                    SerifSubCommand(Mine, ref SerifBody, ref IconTag, ref IconSize);
                    if (Target == null)
                    {
                        SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine);
                    }
                    else
                    {
                        SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine, Target);
                    }
                    IconAddClass = GetIconAddClass(IconTag, IconSize);
                    return "<p class=\"" + ClassType + IconAddClass + " serif_emotion\">" + IconTag + SerifBody + "</p>";
                case "/other":
                case "/o":
                    // アザー
                    {
                        if (SerifBody.IndexOf(" ") < 0)
                        {
                            break;
                        }
                        string SerifOther = SerifBody.Substring(0, SerifBody.IndexOf(" "));
                        SerifBody = SerifBody.Substring(SerifBody.IndexOf(" ")).Trim();
                        if (SerifBody.Length == 0)
                        {
                            break;
                        }

                        SerifSubCommand(Mine, ref SerifBody, ref IconTag, ref IconSize);

                        if (Target == null)
                        {
                            SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine);
                        }
                        else
                        {
                            SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine, Target);
                        }
                        IconAddClass = GetIconAddClass(IconTag, IconSize);
                        return "<dl class=\"" + ClassType + IconAddClass + "\"><dt>" + IconTag + "<span class=\"" + Mine.PartyColor + " serif_other\">" + SerifOther + "</span></dt><dd>「" + SerifBody + "」</dd></dl>";
                    }
                case "/othershout":
                case "/os":
                    // アザーシャウト
                    {
                        if (SerifBody.IndexOf(" ") < 0)
                        {
                            break;
                        }
                        string SerifOther = SerifBody.Substring(0, SerifBody.IndexOf(" "));
                        SerifBody = SerifBody.Substring(SerifBody.IndexOf(" ")).Trim();
                        if (SerifBody.Length == 0)
                        {
                            break;
                        }

                        SerifSubCommand(Mine, ref SerifBody, ref IconTag, ref IconSize);

                        if (Target == null)
                        {
                            SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine);
                        }
                        else
                        {
                            SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine, Target);
                        }
                        IconAddClass = GetIconAddClass(IconTag, IconSize);
                        return "<dl class=\"" + ClassType + IconAddClass + "\"><dt>" + IconTag + "<span class=\"" + Mine.PartyColor + " serif_other\">" + SerifOther + "</span></dt><dd>「<span class=\"serif_shout\">" + SerifBody + "</span>」</dd></dl>";
                    }
                case "/otherlow":
                case "/ol":
                    // アザーロウ
                    {
                        if (SerifBody.IndexOf(" ") < 0)
                        {
                            break;
                        }
                        string SerifOther = SerifBody.Substring(0, SerifBody.IndexOf(" "));
                        SerifBody = SerifBody.Substring(SerifBody.IndexOf(" ")).Trim();
                        if (SerifBody.Length == 0)
                        {
                            break;
                        }

                        SerifSubCommand(Mine, ref SerifBody, ref IconTag, ref IconSize);

                        if (Target == null)
                        {
                            SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine);
                        }
                        else
                        {
                            SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine, Target);
                        }
                        IconAddClass = GetIconAddClass(IconTag, IconSize);
                        return "<dl class=\"" + ClassType + IconAddClass + "\"><dt>" + IconTag + "<span class=\"" + Mine.PartyColor + " serif_other\">" + SerifOther + "</span></dt><dd>「<span class=\"serif_low\">" + SerifBody + "</span>」</dd></dl>";
                    }
                case "/say":
                case "/s":
                default:
                    // セイ
                    SerifSubCommand(Mine, ref SerifBody, ref IconTag, ref IconSize);

                    if (Target == null)
                    {
                        SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine);
                    }
                    else
                    {
                        SerifBody = LibMessage.ConvertMessage(SerifBody, QuestName, Mine, Target);
                    }
                    IconAddClass = GetIconAddClass(IconTag, IconSize);
                    return "<dl class=\"" + ClassType + IconAddClass + "\"><dt>" + IconTag + "<span class=\"" + Mine.PartyColor + "\">" + Mine.NickName + "</span></dt><dd>「" + SerifBody + "」</dd></dl>";
            }

            return "";
        }

        /// <summary>
        /// セリフのサブコマンド処理
        /// </summary>
        /// <param name="Mine">セリフを言う人物</param>
        /// <param name="SerifBody">セリフ本文</param>
        /// <param name="ImageUrl">画像URL格納先</param>
        private static void SerifSubCommand(LibUnitBase Mine, ref string SerifBody, ref string ImageUrl, ref int Size)
        {
            // サブコマンド
            Regex regxSub = new Regex("^[a-z0-9]+ ");
            Match matSub = regxSub.Match(SerifBody);
            string SubSerifCommand = "";
            if (matSub.Success)
            {
                SubSerifCommand = matSub.Value.Trim();
                SerifBody = SerifBody.Remove(0, matSub.Length);
            }

            if (SubSerifCommand.Length == 0)
            {
                return;
            }

            int IconNo = 1;
            string IconString = "";

            if (SubSerifCommand.Length > 0)
            {
                IconString = SubSerifCommand;
                IconString = IconString.Replace("sicon", "");
                IconString = IconString.Replace("licon", "");
                IconString = IconString.Replace("icon", "");
                if (int.TryParse(IconString, out IconNo))
                {
                    SubSerifCommand = SubSerifCommand.Replace(IconString, "");
                }
                else
                {
                    IconNo = 1;
                }
            }

            switch (SubSerifCommand.Trim())
            {
                case "icon":
                    ImageUrl = Mine.GetIconUrl(IconNo, Status.IconSize.M);
                    Size = Status.IconSize.M;
                    break;
                case "sicon":
                    ImageUrl = Mine.GetIconUrl(IconNo, Status.IconSize.S);
                    Size = Status.IconSize.S;
                    break;
                case "licon":
                    ImageUrl = Mine.GetIconUrl(IconNo, Status.IconSize.L);
                    Size = Status.IconSize.L;
                    break;
                default:
                    SerifBody = SubSerifCommand + " " + SerifBody;
                    break;
            }
        }

        /// <summary>
        /// アイコンのサイズ設定
        /// </summary>
        /// <param name="IconTag"></param>
        /// <param name="IconSize"></param>
        /// <returns></returns>
        private static string GetIconAddClass(string IconTag, int IconSize)
        {
            if (IconTag != "")
            {
                switch (IconSize)
                {
                    case Status.IconSize.M:
                        return " sic";
                    case Status.IconSize.L:
                        return " sicl";
                    case Status.IconSize.S:
                        return " sics";
                }
            }

            return "";
        }
    }
}
