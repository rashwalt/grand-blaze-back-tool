using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    /// <summary>
    /// エフェクト管理クラス
    /// </summary>
    public static class LibEffect
    {
        public static EffectEntity Entity;

        static LibEffect()
        {
            Entity = new EffectEntity();

            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <mt_effect_list>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("effect_id, ");
                Sql.AppendLine("ef_name, ");
                Sql.AppendLine("ef_memo, ");
                Sql.AppendLine("ef_viewname, ");
                Sql.AppendLine("rank_min, ");
                Sql.AppendLine("rank_max, ");
                Sql.AppendLine("rank_default, ");
                Sql.AppendLine("rank_fix, ");
                Sql.AppendLine("sub_rank_min, ");
                Sql.AppendLine("sub_rank_max, ");
                Sql.AppendLine("sub_rank_default, ");
                Sql.AppendLine("sub_rank_fix, ");
                Sql.AppendLine("prob_min, ");
                Sql.AppendLine("prob_max, ");
                Sql.AppendLine("prob_default, ");
                Sql.AppendLine("prob_fix, ");
                Sql.AppendLine("limit_min, ");
                Sql.AppendLine("limit_max, ");
                Sql.AppendLine("limit_default, ");
                Sql.AppendLine("limit_fix");
                Sql.AppendLine("FROM mt_effect_list");
                Sql.AppendLine("ORDER BY effect_id ");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_effect_list);
            }
        }

        /// <summary>
        /// 指定エフェクト最大ランク取得
        /// </summary>
        /// <param name="EffectID">エフェクトID</param>
        /// <return>最大ランク</return>
        public static decimal GetMaxRank(int EffectID)
        {
            EffectEntity.mt_effect_listRow row = Entity.mt_effect_list.FindByeffect_id(EffectID);

            if (row != null)
            {
                return row.rank_max;
            }

            return 0;
        }

        /// <summary>
        /// 指定エフェクト名称取得
        /// </summary>
        /// <param name="EffectID">エフェクトID</param>
        /// <return>名称</return>
        public static string GetName(int EffectID)
        {
            EffectEntity.mt_effect_listRow row = Entity.mt_effect_list.FindByeffect_id(EffectID);

            if (row != null)
            {
                return row.ef_name;
            }

            return "";
        }

        /// <summary>
        /// 指定エフェクトID取得
        /// </summary>
        /// <param name="EffectName">エフェクト名</param>
        /// <return>最大ランク</return>
        public static int GetEffectID(string EffectName)
        {
            Entity.mt_effect_list.DefaultView.RowFilter = Entity.mt_effect_list.ef_nameColumn.ColumnName + "=" + LibSql.EscapeString(EffectName);

            if (Entity.mt_effect_list.DefaultView.Count > 0)
            {
                return (int)Entity.mt_effect_list.DefaultView[0]["effect_id"];
            }

            return 0;
        }

        /// <summary>
        /// 指定文字列をエフェクトテーブルに変換
        /// </summary>
        /// <param name="EffectString">エフェクト文字列</param>
        /// <param name="InTable">エフェクトテーブル</param>
        public static void Split(string EffectString, ref EffectListEntity.effect_listDataTable InTable)
        {
            InTable = new EffectListEntity.effect_listDataTable();
            Split(EffectString, ref InTable, false, 0);
        }

        /// <summary>
        /// 指定文字列をエフェクトテーブルに追加
        /// </summary>
        /// <param name="EffectString">エフェクト文字列</param>
        /// <param name="InTable">エフェクトテーブル</param>
        public static void SplitAdd(string EffectString, ref EffectListEntity.effect_listDataTable InTable)
        {
            SplitAdd(EffectString, ref InTable, false, 0);
        }

        /// <summary>
        /// 指定文字列をエフェクトテーブルに追加
        /// </summary>
        /// <param name="EffectString">エフェクト文字列</param>
        /// <param name="InTable">エフェクトテーブル</param>
        /// <param name="EffectDiv">エフェクト区分</param>
        public static void SplitAdd(string EffectString, ref EffectListEntity.effect_listDataTable InTable, int EffectDiv)
        {
            SplitAdd(EffectString, ref InTable, false, EffectDiv);
        }

        /// <summary>
        /// 指定文字列をエフェクトテーブルに変換
        /// </summary>
        /// <param name="EffectString">エフェクト文字列</param>
        /// <param name="InTable">エフェクトテーブル</param>
        /// <param name="IsInName">エフェクトテーブルにエフェクト名を含めるかのフラグ</param>
        /// <param name="EffectDiv">エフェクト区分</param>
        public static void Split(string EffectString, ref EffectListEntity.effect_listDataTable InTable, bool IsInName, int EffectDiv)
        {
            string[] ArrayEf = EffectString.Split('|');

            foreach (string Data in ArrayEf)
            {
                string[] DataRowArray = Data.Split(',');
                if (DataRowArray[0] != "0")
                {
                    EffectListEntity.effect_listRow NewStatusRow = InTable.Neweffect_listRow();

                    NewStatusRow.effect_id = int.Parse(DataRowArray[0]);
                    if (IsInName)
                    {
                        EffectEntity.mt_effect_listRow row = Entity.mt_effect_list.FindByeffect_id(NewStatusRow.effect_id);
                        if (row != null)
                        {
                            NewStatusRow.name = row.ef_name;
                            NewStatusRow.memo = row.ef_memo;
                            if (row.Isef_viewnameNull())
                            {
                                NewStatusRow.viewname = "";
                            }
                            else
                            {
                                NewStatusRow.viewname = row.ef_viewname;
                            }
                        }
                        else
                        {
                            NewStatusRow.name = "";
                            NewStatusRow.memo = "";
                            NewStatusRow.viewname = "";
                        }
                    }
                    else
                    {
                        NewStatusRow.name = "";
                        NewStatusRow.memo = "";
                        NewStatusRow.viewname = "";
                    }
                    NewStatusRow.rank = decimal.Parse(DataRowArray[1]);
                    NewStatusRow.sub_rank = decimal.Parse(DataRowArray[2]);
                    NewStatusRow.prob = decimal.Parse(DataRowArray[3]);
                    NewStatusRow.endlimit = int.Parse(DataRowArray[4]);
                    NewStatusRow.effect_div = EffectDiv;
                    NewStatusRow.hide_fg = false;
                    if (DataRowArray.Length > 5)
                    {
                        NewStatusRow.hide_fg = int.Parse(DataRowArray[5]) == 1;
                    }

                    InTable.Addeffect_listRow(NewStatusRow);
                }
            }
        }

        /// <summary>
        /// 指定文字列をエフェクトテーブルに変換・追加
        /// </summary>
        /// <param name="EffectString">エフェクト文字列</param>
        /// <param name="InTable">エフェクトテーブル</param>
        /// <param name="IsInName">エフェクトテーブルにエフェクト名を含めるかのフラグ</param>
        /// <param name="EffectDiv">エフェクト区分</param>
        public static void SplitAdd(string EffectString, ref EffectListEntity.effect_listDataTable InTable, bool IsInName, int EffectDiv)
        {
            string[] ArrayEf = EffectString.Split('|');

            foreach (string Data in ArrayEf)
            {
                string[] DataRowArray = Data.Split(',');
                if (DataRowArray[0] != "0")
                {
                    EffectListEntity.effect_listRow InStatusRow = InTable.FindByeffect_id(int.Parse(DataRowArray[0]));

                    if (InStatusRow == null)
                    {
                        EffectListEntity.effect_listRow NewStatusRow = InTable.Neweffect_listRow();

                        NewStatusRow.effect_id = int.Parse(DataRowArray[0]);
                        if (IsInName)
                        {
                            EffectEntity.mt_effect_listRow EffectDataRow = Entity.mt_effect_list.FindByeffect_id(NewStatusRow.effect_id);
                            if (EffectDataRow != null)
                            {
                                NewStatusRow.name = EffectDataRow.ef_name;
                                NewStatusRow.memo = EffectDataRow.ef_memo;
                                NewStatusRow.viewname = EffectDataRow.ef_viewname;
                            }
                            else
                            {
                                NewStatusRow.name = "";
                                NewStatusRow.memo = "";
                                NewStatusRow.viewname = "";
                            }
                        }
                        else
                        {
                            NewStatusRow.name = "";
                            NewStatusRow.memo = "";
                            NewStatusRow.viewname = "";
                        }
                        NewStatusRow.rank = decimal.Parse(DataRowArray[1]);
                        NewStatusRow.sub_rank = decimal.Parse(DataRowArray[2]);
                        NewStatusRow.prob = decimal.Parse(DataRowArray[3]);
                        NewStatusRow.endlimit = int.Parse(DataRowArray[4]);
                        NewStatusRow.effect_div = EffectDiv;
                        NewStatusRow.hide_fg = false;
                        if (DataRowArray.Length > 4)
                        {
                            NewStatusRow.hide_fg = int.Parse(DataRowArray[5]) == 1;
                        }

                        InTable.Addeffect_listRow(NewStatusRow);
                    }
                    else
                    {
                        EffectEntity.mt_effect_listRow EffectDataRow = Entity.mt_effect_list.FindByeffect_id(int.Parse(DataRowArray[0]));

                        // ランク
                        switch (EffectDataRow.rank_fix)
                        {
                            case Status.EffectFix.Add:
                                InStatusRow.rank = InStatusRow.rank + decimal.Parse(DataRowArray[1]);

                                if (InStatusRow.rank > EffectDataRow.rank_max)
                                {
                                    InStatusRow.rank = EffectDataRow.rank_max;
                                }
                                if (InStatusRow.rank < EffectDataRow.rank_min)
                                {
                                    InStatusRow.rank = EffectDataRow.rank_min;
                                }
                                break;
                            case Status.EffectFix.Paste:
                                if (decimal.Parse(DataRowArray[1]) > InStatusRow.rank)
                                {
                                    InStatusRow.rank = decimal.Parse(DataRowArray[1]);
                                }
                                break;
                        }

                        // サブランク
                        switch (EffectDataRow.sub_rank_fix)
                        {
                            case Status.EffectFix.Add:
                                InStatusRow.sub_rank = InStatusRow.sub_rank + decimal.Parse(DataRowArray[2]);

                                if (InStatusRow.sub_rank > EffectDataRow.sub_rank_max)
                                {
                                    InStatusRow.sub_rank = EffectDataRow.sub_rank_max;
                                }
                                if (InStatusRow.sub_rank < EffectDataRow.sub_rank_min)
                                {
                                    InStatusRow.sub_rank = EffectDataRow.sub_rank_min;
                                }
                                break;
                            case Status.EffectFix.Paste:
                                if (decimal.Parse(DataRowArray[2]) > InStatusRow.sub_rank)
                                {
                                    InStatusRow.sub_rank = decimal.Parse(DataRowArray[2]);
                                }
                                break;
                        }

                        // 確率
                        switch (EffectDataRow.prob_fix)
                        {
                            case Status.EffectFix.Add:
                                InStatusRow.prob = InStatusRow.prob + decimal.Parse(DataRowArray[3]);

                                if (InStatusRow.prob > EffectDataRow.prob_max)
                                {
                                    InStatusRow.prob = EffectDataRow.prob_max;
                                }
                                if (InStatusRow.prob < EffectDataRow.prob_min)
                                {
                                    InStatusRow.prob = EffectDataRow.prob_min;
                                }
                                break;
                            case Status.EffectFix.Paste:
                                if (decimal.Parse(DataRowArray[3]) > InStatusRow.prob)
                                {
                                    InStatusRow.prob = decimal.Parse(DataRowArray[3]);
                                }
                                break;
                        }

                        // エンドリミット
                        switch (EffectDataRow.limit_fix)
                        {
                            case Status.EffectFix.Add:
                                InStatusRow.endlimit = InStatusRow.endlimit + int.Parse(DataRowArray[4]);

                                if (InStatusRow.endlimit > EffectDataRow.limit_max)
                                {
                                    InStatusRow.endlimit = EffectDataRow.limit_max;
                                }
                                if (InStatusRow.endlimit < EffectDataRow.limit_min)
                                {
                                    InStatusRow.endlimit = EffectDataRow.limit_min;
                                }
                                break;
                            case Status.EffectFix.Paste:
                                if (int.Parse(DataRowArray[4]) > InStatusRow.endlimit)
                                {
                                    InStatusRow.endlimit = int.Parse(DataRowArray[4]);
                                }
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// エフェクトテーブルを文字列に変換
        /// </summary>
        /// <param name="EffectString">文字列</param>
        /// <param name="InTable">エフェクトテーブル</param>
        public static void Join(ref string EffectString, EffectListEntity.effect_listDataTable InTable)
        {
            StringBuilder EffectStringBuild = new StringBuilder(EffectString);
            foreach (EffectListEntity.effect_listRow NewStatusRow in InTable)
            {
                if (EffectStringBuild.ToString().Length > 0)
                {
                    EffectStringBuild.Append("|");
                }
                EffectStringBuild.Append(NewStatusRow.effect_id.ToString());
                EffectStringBuild.Append("," + NewStatusRow.rank.ToString());
                EffectStringBuild.Append("," + NewStatusRow.sub_rank.ToString());
                EffectStringBuild.Append("," + NewStatusRow.prob.ToString());
                EffectStringBuild.Append("," + NewStatusRow.endlimit.ToString());
                if (NewStatusRow.hide_fg)
                {
                    EffectStringBuild.Append(",1");
                }
                else
                {
                    EffectStringBuild.Append(",0");
                }
            }

            if (EffectStringBuild.ToString().Length == 0)
            {
                EffectStringBuild.Append("0,0,0,0,0,0");
            }

            EffectString = EffectStringBuild.ToString();
        }

        /// <summary>
        /// エフェクトテーブルに追加
        /// </summary>
        /// <param name="effect_id">エフェクトID</param>
        /// <param name="rank">ランク(Lv)</param>
        /// <param name="sub_rank">サブランク</param>
        /// <param name="prob">確率</param>
        /// <param name="endlimit">終了カウント</param>
        /// <param name="eff_div">エフェクト区分</param>
        /// <param name="InTable">追加先エフェクトテーブル</param>
        public static void Add(int effect_id, int rank, int sub_rank, int prob, int endlimit, int eff_div, bool is_hide, ref EffectListEntity.effect_listDataTable InTable)
        {
            if (effect_id > 0)
            {
                EffectListEntity.effect_listRow NewStatusRow = InTable.Neweffect_listRow();

                NewStatusRow.effect_id = effect_id;
                NewStatusRow.name = "";
                NewStatusRow.memo = "";
                NewStatusRow.viewname = "";
                NewStatusRow.rank = rank;
                NewStatusRow.sub_rank = sub_rank;
                NewStatusRow.prob = prob;
                NewStatusRow.endlimit = endlimit;
                NewStatusRow.effect_div = eff_div;
                NewStatusRow.hide_fg = is_hide;

                InTable.Addeffect_listRow(NewStatusRow);
            }
        }

        /// <summary>
        /// エフェクト追加
        /// </summary>
        /// <param name="MainEffect">追加先テーブル</param>
        /// <param name="AddEffect">追加するエフェクト一覧</param>
        /// <param name="RankFix">ランク修正</param>
        /// <param name="ProbFix">確率修正</param>
        public static void AddEffectTable(ref EffectListEntity.effect_listDataTable MainEffect, EffectListEntity.effect_listDataTable AddEffect, decimal RankFix, decimal ProbFix)
        {
            foreach (EffectListEntity.effect_listRow AddEffectRow in AddEffect)
            {
                EffectEntity.mt_effect_listRow row = Entity.mt_effect_list.FindByeffect_id(AddEffectRow.effect_id);

                if (row == null)
                {
                    continue;
                }

                EffectListEntity.effect_listRow MainEffectRow = MainEffect.FindByeffect_id(AddEffectRow.effect_id);

                AddEffectRow.rank = (int)((decimal)AddEffectRow.rank * RankFix);
                AddEffectRow.prob = (int)((decimal)AddEffectRow.prob * ProbFix);

                if (MainEffectRow == null)
                {
                    EffectListEntity.effect_listRow NewRow = MainEffect.Neweffect_listRow();
                    NewRow.effect_id = AddEffectRow.effect_id;
                    NewRow.name = AddEffectRow.name;
                    NewRow.memo = AddEffectRow.memo;
                    NewRow.viewname = AddEffectRow.viewname;
                    NewRow.rank = AddEffectRow.rank;
                    NewRow.sub_rank = AddEffectRow.sub_rank;
                    NewRow.prob = AddEffectRow.prob;
                    NewRow.endlimit = AddEffectRow.endlimit;
                    NewRow.effect_div = AddEffectRow.effect_div;
                    NewRow.hide_fg = AddEffectRow.hide_fg;

                    MainEffect.Addeffect_listRow(NewRow);
                }
                else
                {
                    // ランク
                    switch (row.rank_fix)
                    {
                        case Status.EffectFix.Add:
                            MainEffectRow.rank = MainEffectRow.rank + AddEffectRow.rank;

                            if (MainEffectRow.rank > row.rank_max)
                            {
                                MainEffectRow.rank = row.rank_max;
                            }
                            if (MainEffectRow.rank < row.rank_min)
                            {
                                MainEffectRow.rank = row.rank_min;
                            }
                            break;
                        case Status.EffectFix.Paste:
                            if (AddEffectRow.rank > MainEffectRow.rank)
                            {
                                MainEffectRow.rank = AddEffectRow.rank;
                            }
                            break;
                    }

                    // サブランク
                    switch (row.sub_rank_fix)
                    {
                        case Status.EffectFix.Add:
                            MainEffectRow.sub_rank = MainEffectRow.sub_rank + AddEffectRow.sub_rank;

                            if (MainEffectRow.sub_rank > row.sub_rank_max)
                            {
                                MainEffectRow.sub_rank = row.sub_rank_max;
                            }
                            if (MainEffectRow.sub_rank < row.sub_rank_min)
                            {
                                MainEffectRow.sub_rank = row.sub_rank_min;
                            }
                            break;
                        case Status.EffectFix.Paste:
                            if (AddEffectRow.sub_rank > MainEffectRow.sub_rank)
                            {
                                MainEffectRow.sub_rank = AddEffectRow.sub_rank;
                            }
                            break;
                    }

                    // 確率
                    switch (row.prob_fix)
                    {
                        case Status.EffectFix.Add:
                            MainEffectRow.prob = MainEffectRow.prob + AddEffectRow.prob;

                            if (MainEffectRow.prob > row.prob_max)
                            {
                                MainEffectRow.prob = row.prob_max;
                            }
                            if (MainEffectRow.prob < row.prob_min)
                            {
                                MainEffectRow.prob = row.prob_min;
                            }
                            break;
                        case Status.EffectFix.Paste:
                            if (AddEffectRow.prob > MainEffectRow.prob)
                            {
                                MainEffectRow.prob = AddEffectRow.prob;
                            }
                            break;
                    }

                    // エンドリミット
                    switch (row.limit_fix)
                    {
                        case Status.EffectFix.Add:
                            MainEffectRow.endlimit = MainEffectRow.endlimit + AddEffectRow.endlimit;

                            if (MainEffectRow.endlimit > row.limit_max)
                            {
                                MainEffectRow.endlimit = row.limit_max;
                            }
                            if (MainEffectRow.endlimit < row.limit_min)
                            {
                                MainEffectRow.endlimit = row.limit_min;
                            }
                            break;
                        case Status.EffectFix.Paste:
                            if (AddEffectRow.endlimit > MainEffectRow.endlimit)
                            {
                                MainEffectRow.endlimit = AddEffectRow.endlimit;
                            }
                            break;
                    }
                }
            }
        }
    }
}
