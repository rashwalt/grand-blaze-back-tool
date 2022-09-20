using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using System.Drawing;
using CommonLibrary.DataFormat.SpecialEntity;

namespace CommonLibrary
{
    /// <summary>
    /// 解説文管理クラス
    /// </summary>
    public static class LibComment
    {
        /// <summary>
        /// アイテムのコメント生成
        /// </summary>
        /// <param name="ItemNo">編集先アイテムNo</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <returns>コメント</returns>
        public static string Item(int ItemNo, bool IsCreated, string EntryName)
        {
            StringBuilder AddComments = new StringBuilder();

            CommonItemEntity.item_listRow ItemRow = LibItem.GetItemRow(ItemNo, IsCreated);

            ItemTypeEntity.mt_item_typeRow TypeRow = LibItemType.GetTypeRow(ItemRow.it_type);

            ItemTypeEntity.mt_item_type_sub_categoryRow SubCategoryRow = LibItemType.GetSubCategoryRow(ItemRow.it_sub_category);

            bool isLevelView = false;

            int CateDiv = TypeRow.categ_div;

            if (CateDiv == 0)
            {
                AddComments.Append("威力:" + ItemRow.it_physics + " ＣＴ:" + ItemRow.it_charge + " 武器回避:" + ItemRow.it_physics_parry + " 必殺:" + ItemRow.it_critical + " 射程:" + GetRange(ItemRow.it_range) + " 属性:");

                string ElementAttack = "";
                if (ItemRow.it_fire > 0) { ElementAttack += "火"; }
                if (ItemRow.it_freeze > 0) { ElementAttack += "氷"; }
                if (ItemRow.it_air > 0) { ElementAttack += "風"; }
                if (ItemRow.it_earth > 0) { ElementAttack += "地"; }
                if (ItemRow.it_thunder > 0) { ElementAttack += "雷"; }
                if (ItemRow.it_water > 0) { ElementAttack += "水"; }
                if (ItemRow.it_holy > 0) { ElementAttack += "聖"; }
                if (ItemRow.it_dark > 0) { ElementAttack += "闇"; }
                if (ItemRow.it_slash > 0) { ElementAttack += "斬"; }
                if (ItemRow.it_pierce > 0) { ElementAttack += "突"; }
                if (ItemRow.it_strike > 0) { ElementAttack += "打"; }
                if (ItemRow.it_break > 0) { ElementAttack += "壊"; }

                if (ElementAttack.Length == 0)
                {
                    ElementAttack = "無";
                }
                AddComments.Append(ElementAttack);
            }
            if (CateDiv == 1)
            {
                AddComments.Append("回避:" + ItemRow.it_physics_parry + " 魔法回避:" + ItemRow.it_sorcery_parry);
            }
            if (CateDiv == 2 || CateDiv == 3 || CateDiv == 4)
            {
                AddComments.Append("防御:" + ItemRow.it_physics + " 魔法防御:" + ItemRow.it_sorcery);
            }
            if (CateDiv == 1 || CateDiv == 2 || CateDiv == 3 || CateDiv == 4)
            {
                string ElementAttack = "";
                if (ItemRow.it_fire < 0) { ElementAttack += "火"; }
                if (ItemRow.it_freeze < 0) { ElementAttack += "氷"; }
                if (ItemRow.it_air < 0) { ElementAttack += "風"; }
                if (ItemRow.it_earth < 0) { ElementAttack += "地"; }
                if (ItemRow.it_thunder < 0) { ElementAttack += "雷"; }
                if (ItemRow.it_water < 0) { ElementAttack += "水"; }
                if (ItemRow.it_holy < 0) { ElementAttack += "聖"; }
                if (ItemRow.it_dark < 0) { ElementAttack += "闇"; }
                if (ItemRow.it_slash < 0) { ElementAttack += "斬"; }
                if (ItemRow.it_pierce < 0) { ElementAttack += "突"; }
                if (ItemRow.it_strike < 0) { ElementAttack += "打"; }
                if (ItemRow.it_break < 0) { ElementAttack += "壊"; }

                if (ElementAttack.Length > 0)
                {
                    AddComments.Append(" 弱点:" + ElementAttack);
                }

                ElementAttack = "";
                if (ItemRow.it_fire < 100 && ItemRow.it_fire > 0) { ElementAttack += "火"; }
                if (ItemRow.it_freeze < 100 && ItemRow.it_freeze > 0) { ElementAttack += "氷"; }
                if (ItemRow.it_air < 100 && ItemRow.it_air > 0) { ElementAttack += "風"; }
                if (ItemRow.it_earth < 100 && ItemRow.it_earth > 0) { ElementAttack += "地"; }
                if (ItemRow.it_thunder < 100 && ItemRow.it_thunder > 0) { ElementAttack += "雷"; }
                if (ItemRow.it_water < 100 && ItemRow.it_water > 0) { ElementAttack += "水"; }
                if (ItemRow.it_holy < 100 && ItemRow.it_holy > 0) { ElementAttack += "聖"; }
                if (ItemRow.it_dark < 100 && ItemRow.it_dark > 0) { ElementAttack += "闇"; }
                if (ItemRow.it_slash < 100 && ItemRow.it_slash > 0) { ElementAttack += "斬"; }
                if (ItemRow.it_pierce < 100 && ItemRow.it_pierce > 0) { ElementAttack += "突"; }
                if (ItemRow.it_strike < 100 && ItemRow.it_strike > 0) { ElementAttack += "打"; }
                if (ItemRow.it_break < 100 && ItemRow.it_break > 0) { ElementAttack += "壊"; }

                if (ElementAttack.Length > 0)
                {
                    AddComments.Append(" 半減:" + ElementAttack);
                }

                ElementAttack = "";
                if (ItemRow.it_fire == 100) { ElementAttack += "火"; }
                if (ItemRow.it_freeze == 100) { ElementAttack += "氷"; }
                if (ItemRow.it_air == 100) { ElementAttack += "風"; }
                if (ItemRow.it_earth == 100) { ElementAttack += "地"; }
                if (ItemRow.it_thunder == 100) { ElementAttack += "雷"; }
                if (ItemRow.it_water == 100) { ElementAttack += "水"; }
                if (ItemRow.it_holy == 100) { ElementAttack += "聖"; }
                if (ItemRow.it_dark == 100) { ElementAttack += "闇"; }
                if (ItemRow.it_slash == 100) { ElementAttack += "斬"; }
                if (ItemRow.it_pierce == 100) { ElementAttack += "突"; }
                if (ItemRow.it_strike == 100) { ElementAttack += "打"; }
                if (ItemRow.it_break == 100) { ElementAttack += "壊"; }

                if (ElementAttack.Length > 0)
                {
                    AddComments.Append(" 無効:" + ElementAttack);
                }

                ElementAttack = "";
                if (ItemRow.it_fire > 100) { ElementAttack += "火"; }
                if (ItemRow.it_freeze > 100) { ElementAttack += "氷"; }
                if (ItemRow.it_air > 100) { ElementAttack += "風"; }
                if (ItemRow.it_earth > 100) { ElementAttack += "地"; }
                if (ItemRow.it_thunder > 100) { ElementAttack += "雷"; }
                if (ItemRow.it_water > 100) { ElementAttack += "水"; }
                if (ItemRow.it_holy > 100) { ElementAttack += "聖"; }
                if (ItemRow.it_dark > 100) { ElementAttack += "闇"; }
                if (ItemRow.it_slash > 100) { ElementAttack += "斬"; }
                if (ItemRow.it_pierce > 100) { ElementAttack += "突"; }
                if (ItemRow.it_strike > 100) { ElementAttack += "打"; }
                if (ItemRow.it_break > 100) { ElementAttack += "壊"; }

                if (ElementAttack.Length > 0)
                {
                    AddComments.Append(" 吸収:" + ElementAttack);
                }
            }
            if (CateDiv == 0 || CateDiv == 1 || CateDiv == 2 || CateDiv == 3 || CateDiv == 4)
            {
                switch (ItemRow.it_ok_sex)
                {
                    case 0:
                        AddComments.Append(" ");
                        break;
                    case 1:
                        AddComments.Append(" ♂");
                        break;
                    case 2:
                        AddComments.Append(" ♀");
                        break;
                }
                switch (ItemRow.it_ok_race)
                {
                    case 0:
                        AddComments.Append("");
                        break;
                    case 1:
                        AddComments.Append("Ｈ");
                        break;
                    case 2:
                        AddComments.Append("Ｅ");
                        break;
                    case 3:
                        AddComments.Append("Ｆ");
                        break;
                    case 4:
                        AddComments.Append("Ｒ");
                        break;
                    case 5:
                        AddComments.Append("Ｂ");
                        break;
                    case 6:
                        AddComments.Append("Ｄ");
                        break;
                }

                if (ItemRow.it_ok_sex == 0 && ItemRow.it_ok_race == 0)
                {
                    AddComments.Append("全種");
                }

                AddComments.Append(" Lv" + ItemRow.it_equip_level + "～");
                isLevelView = true;

                string[] InstallClassLists = ItemRow.it_equip_install.Split(',');
                string InstallComment = "";
                for (int i = 0; i < LibInstall.Entity.mt_install_class_list.Count; i++)
                {
                    if (InstallClassLists[i] == "0")
                    {
                        continue;
                    }

                    switch (i)
                    {
                        case 0:
                            InstallComment += "戦";
                            break;
                        case 1:
                            InstallComment += "サ";
                            break;
                        case 2:
                            InstallComment += "パ";
                            break;
                        case 3:
                            InstallComment += "モ";
                            break;
                        case 4:
                            InstallComment += "狩";
                            break;
                        case 5:
                            InstallComment += "ス";
                            break;
                        case 6:
                            InstallComment += "プ";
                            break;
                        case 7:
                            InstallComment += "機";
                            break;
                        case 8:
                            InstallComment += "マ";
                            break;
                        case 9:
                            InstallComment += "学";
                            break;
                        case 10:
                            InstallComment += "吟";
                            break;
                        case 11:
                            InstallComment += "バ";
                            break;
                    }
                }

                if (InstallComment.Length == LibInstall.Entity.mt_install_class_list.Count)
                {
                    InstallComment = "All Installs";
                }

                if (InstallComment.Length > 0)
                {
                    AddComments.Append(" " + InstallComment);
                }
            }

            if (CateDiv == 6)
            {
                if (ItemRow.it_equip_level > 0)
                {
                    // 素材
                    AddComments.Append("Lv" + (int)ItemRow["it_equip_level"] + "～");
                    isLevelView = true;
                }
            }

            if (isLevelView == false && ItemRow.it_use_item != 0 && ItemRow.it_equip_level > 0)
            {
                AddComments.Append("Lv" + (int)ItemRow["it_equip_level"] + "～");
            }

            if (ItemRow.it_rare)
            {
                AddComments.Append(" <span class=\"rare\">Rare</span>");
            }

            if (ItemRow.it_quest)
            {
                AddComments.Append(" <span class=\"quest\">Quest</span>");
            }

            if (ItemRow.it_bind)
            {
                AddComments.Append(" <span class=\"bind\">Bind</span>");
            }

            // 追加効果
            EffectListEntity.effect_listDataTable EffectTable = new EffectListEntity.effect_listDataTable();
            LibEffect.Split(ItemRow.it_effect, ref EffectTable, true, 0);

            if (EffectTable.Rows.Count > 0 && AddComments.Length > 0)
            {
                AddComments.Append("<br />");
            }

            foreach (DataRow EffectRow in EffectTable.Rows)
            {
                string EffectName = ConvEfName(EffectRow, ItemRow.it_name, true, ItemNo);
                if (EffectName.Length > 0)
                {
                    if (AddComments.Length > 0)
                    {
                        AddComments.Append(" ");
                    }
                    AddComments.Append(EffectName);
                }
            }

            string ItemComment = ItemRow.it_comment;

            if (EntryName.Length > 0 && !IsCreated)
            {
                ItemComment = ItemComment.Replace("[ringname]", EntryName);
            }

            if (AddComments.Length > 0)
            {
                AddComments.Append("<br />" + ItemComment);
            }
            else
            {
                AddComments = new StringBuilder(ItemComment);
            }

            return AddComments.ToString();
        }

        /// <summary>
        /// スキルのコメント生成
        /// </summary>
        /// <param name="SkillNo">スキルID</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <returns>解説文</returns>
        public static string Skill(int SkillNo)
        {
            StringBuilder AddComments = new StringBuilder();

            CommonSkillEntity.skill_listRow SkillRow = LibSkill.GetSkillRow(SkillNo);

            int Type = SkillRow.sk_type;

            switch (Type)
            {
                case Status.SkillType.Arts:
                case Status.SkillType.Special:
                    #region アーツ
                    if (SkillRow.sk_arts_category > 0)
                    {
                        AddComments.Append(LibSkillType.GetName(SkillRow.sk_arts_category) + " ");
                    }
                    AddComments.Append("ＣＴ:" + SkillRow.sk_charge);
                    AddComments.Append(" 射程:" + GetRange(SkillRow.sk_range) + " 対象(初期):" + GetTargetParty(SkillRow.sk_target_party) + " " + GetTargetArea(SkillRow.sk_target_area, true));
                    if (SkillRow.sk_antiair)
                    {
                        AddComments.Append(" 対空効果");
                    }
                    #endregion
                    break;
            }

            // 追加効果
            EffectListEntity.effect_listDataTable EffectTable = new EffectListEntity.effect_listDataTable();
            LibEffect.Split(SkillRow.sk_effect, ref EffectTable, true, 0);

            foreach (DataRow EffectRow in EffectTable.Rows)
            {
                string EffectName = ConvEfName(EffectRow, SkillRow.sk_name, false, 0);
            }

            if (AddComments.Length > 0)
            {
                AddComments.Append("<br />" + SkillRow.sk_comment);
            }
            else
            {
                AddComments = new StringBuilder(SkillRow.sk_comment);
            }

            return AddComments.ToString();
        }

        /// <summary>
        /// スキル習得の条件生成
        /// </summary>
        /// <param name="SkillNo">スキルID</param>
        /// <returns>条件文</returns>
        public static string SkillCondition(int SkillNo)
        {
            StringBuilder AddComments = new StringBuilder();

            CommonSkillEntity.skill_listRow SkillRow = LibSkill.GetSkillRow(SkillNo);
            SkillGetEntity.mt_skill_get_listRow SkillGetRow = LibSkill.GetEntity.mt_skill_get_list.FindByperks_id(SkillNo);

            if (SkillGetRow.tm_install > 0 && SkillGetRow.tm_install_level > 0)
            {
                AddComments.AppendLine(LibInstall.GetInstallName(SkillGetRow.tm_install) + "Lv:" + SkillGetRow.tm_install_level + "以上");
            }
            if (SkillGetRow.tm_race > 0)
            {
                AddComments.AppendLine("種族:" + LibRace.GetRaceName(SkillGetRow.tm_race) + "限定");
            }
            if (SkillGetRow.tm_guardian > 0)
            {
                AddComments.AppendLine("守護者:" + LibGuardian.GetName(SkillGetRow.tm_guardian) + "限定");
            }
            if (SkillGetRow.tm_base_skill > 0)
            {
                AddComments.AppendLine("スキル:" + LibSkill.GetSkillName(SkillGetRow.tm_base_skill) + "の習得");
            }
            if (SkillGetRow.tm_str > 0)
            {
                AddComments.AppendLine("力:" + SkillGetRow.tm_str + "以上");
            }
            if (SkillGetRow.tm_agi > 0)
            {
                AddComments.AppendLine("敏捷:" + SkillGetRow.tm_agi + "以上");
            }
            if (SkillGetRow.tm_mag > 0)
            {
                AddComments.AppendLine("魔力:" + SkillGetRow.tm_mag + "以上");
            }
            if (SkillGetRow.tm_unq > 0)
            {
                AddComments.AppendLine("ユニーク:" + SkillGetRow.tm_unq + "以上");
            }

            return AddComments.ToString();
        }

        /// <summary>
        /// 射程の文字列取得
        /// </summary>
        /// <param name="Range">射程数値</param>
        /// <returns>射程文字</returns>
        private static string GetRange(int Range)
        {
            switch (Range)
            {
                case 1:
                    return "M";
                case 2:
                    return "L";
                default:
                    return "S";
            }
        }

        /// <summary>
        /// ターゲット所属の文字列取得
        /// </summary>
        /// <param name="TargetParty"></param>
        /// <returns></returns>
        private static string GetTargetParty(int TargetParty)
        {
            switch (TargetParty)
            {
                case Status.TargetParty.Mine:
                    return "自分";
                case Status.TargetParty.Friend:
                    return "味方";
                case Status.TargetParty.Enemy:
                    return "敵";
                case Status.TargetParty.Pet:
                    return "ペット";
                case Status.TargetParty.All:
                    return "敵味方";
                default:
                    return "不明";
            }
        }

        /// <summary>
        /// ターゲットエリアの文字列取得
        /// </summary>
        /// <param name="TargetArea"></param>
        /// <returns></returns>
        private static string GetTargetArea(int TargetArea, bool IsComment)
        {
            string BeforeText = "";
            if (IsComment) { BeforeText = "範囲:"; }

            switch (TargetArea)
            {
                case Status.TargetArea.Only:
                    return BeforeText + "単体";
                case Status.TargetArea.Circle1:
                    return BeforeText + "範囲（小）";
                case Status.TargetArea.Circle2:
                    return BeforeText + "範囲（大）";
                case Status.TargetArea.Line:
                    return BeforeText + "隊列";
                case Status.TargetArea.All:
                    return BeforeText + "全体";
                case Status.TargetArea.Monster:
                    return BeforeText + "単体(ペット)";
                case Status.TargetArea.Papet:
                    return BeforeText + "単体(ファミリア)";
                case Status.TargetArea.Elemental:
                    return BeforeText + "単体(精霊)";
                default:
                    if (IsComment) { return ""; }
                    else { return "不明"; }
            }
        }

        /// <summary>
        /// エフェクト表示文字変換
        /// 特定のキャラクターなどはからまない
        /// </summary>
        /// <param name="EffectRow">エフェクトDataRow</param>
        /// <param name="Name">アイテム/スキルの名称</param>
        /// <param name="IsFoodConv">フードデータ置換するか</param>
        /// <param name="ItemID">アイテムID</param>
        /// <returns>変換後の名称</returns>
        public static string ConvEfName(DataRow EffectRow, string Name, bool IsFoodConv, int ItemID)
        {
            string EffectName = EffectRow["viewname"].ToString();
            int Rank = (int)(decimal)EffectRow["rank"];
            if (Rank >= 0)
            {
                EffectName = EffectName.Replace("[rank]", "+" + Rank.ToString());
            }
            else
            {
                EffectName = EffectName.Replace("[rank]", Rank.ToString());
            }
            EffectName = EffectName.Replace("[nrank]", Rank.ToString());
            int ProbRank100 = (int)Math.Round((decimal)Rank / 256M * 100M, 0, MidpointRounding.ToEven);
            if (ProbRank100 >= 0)
            {
                EffectName = EffectName.Replace("[rankp]", "+" + ProbRank100.ToString());
            }
            else
            {
                EffectName = EffectName.Replace("[rankp]", ProbRank100.ToString());
            }
            EffectName = EffectName.Replace("[nrankp]", ProbRank100.ToString());
            decimal ProbRank1000 = (decimal)ProbRank100 / 10m;
            if (ProbRank1000 < 1m)
            {
                EffectName = EffectName.Replace("[nrank10p]", ProbRank1000.ToString("0.0"));
            }
            else
            {
                EffectName = EffectName.Replace("[nrank10p]", ((int)Math.Round(ProbRank1000, 0, MidpointRounding.ToEven)).ToString());
            }
            EffectName = EffectName.Replace("[item]", LibItem.GetItemName(Rank, false));
            EffectName = EffectName.Replace("[citem]", LibItem.GetItemName(Rank, true));
            EffectName = EffectName.Replace("[perks]", LibSkill.GetSkillName(Rank));
            EffectName = EffectName.Replace("[monster]", LibMonsterData.GetNickName(Rank));
            EffectName = EffectName.Replace("[setbonus]", LibSetBonus.GetSetName(Rank));
            EffectName = EffectName.Replace("[key]", LibKeyItem.GetKeyItemName(Rank));
            EffectName = EffectName.Replace("[type]", LibItemType.GetTypeName(Rank));
            EffectName = EffectName.Replace("[install]", LibInstall.GetInstallName(Rank));
            EffectName = EffectName.Replace("[prob]", ((int)(decimal)EffectRow["prob"]).ToString());
            EffectName = EffectName.Replace("[limit]", ((int)EffectRow["endlimit"]).ToString());
            EffectName = EffectName.Replace("[name]", Name);
            EffectName = EffectName.Replace("[range]", GetRange(Rank));
            EffectName = EffectName.Replace("[targetp]", GetTargetParty(Rank));
            EffectName = EffectName.Replace("[targeta]", GetTargetArea(Rank, false));

            EffectName = EffectName.Replace("[rankelm]", GetElementRank(Rank));


            if ((int)(decimal)EffectRow["prob"] > 0 && (int)(decimal)EffectRow["prob"] < 100)
            {
                EffectName = "[R]" + EffectName;
            }

            return EffectName;
        }

        /// <summary>
        /// 属性耐性文字列
        /// </summary>
        /// <param name="Rank">ランク</param>
        /// <returns>耐性文字</returns>
        public static string GetElementRank(int Rank)
        {
            if (Rank < 0)
            {
                return "弱点";
            }
            else if (Rank == 0)
            {
                return "通常";
            }
            else if (Rank > 0 && Rank < 100)
            {
                return "半減";
            }
            else if (Rank == 100)
            {
                return "無効";
            }
            else
            {
                return "吸収";
            }
        }
    }
}
