using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data;

namespace CommonLibrary
{
    /// <summary>
    /// テキストを置換して整形する
    /// </summary>
    public class LibSql
    {

        public static string MakeUpSql(string table, string wheres, Hashtable SqlElm)
        {
            int i;
            string sql = "";
            string[] datals = new string[SqlElm.Count];

            i = 0;
            foreach (string key in SqlElm.Keys)
            {
                string inData = SqlElm[key].ToString();
                if (inData == "True") { inData = "1"; }
                else if (inData == "False") { inData = "0"; }
                datals[i] = "[" + key + "]=" + inData + "";
                i++;
            }

            sql = "" +
                "UPDATE " + table + " SET " +
                string.Join(",", datals) +
                " ";

            if (wheres.Length > 0)
            {
                sql = sql +
                    " WHERE " +
                    wheres;
            }

            return sql;
        }

        public static string MakeUpSql(string table, string wheres, DataRow SqlElm)
        {
            return MakeUpSql(table, wheres, SqlElm, false, new List<string>());
        }

        public static string MakeUpSql(string table, string wheres, DataRow SqlElm, bool EscapeBr)
        {
            return MakeUpSql(table, wheres, SqlElm, EscapeBr, new List<string>());
        }

        public static string MakeUpSql(string table, string wheres, DataRow SqlElm, List<string> OutofColumn)
        {
            return MakeUpSql(table, wheres, SqlElm, false, OutofColumn);
        }

        public static string MakeUpSql(string table, string wheres, DataRow SqlElm, bool EscapeBr, List<string> OutofColumn)
        {
            string sql = "";

            List<string> datals = new List<string>();

            foreach (DataColumn key in SqlElm.Table.Columns)
            {
                if (key.ColumnName.Length > 3 && key.ColumnName.Substring(0, 3) == "ns_") { continue; }
                if (OutofColumn.Exists(input => input == key.ColumnName)) { continue; }

                string Data = SqlElm[key.ColumnName].ToString();
                if (Data == "True") { Data = "1"; }
                else if (Data == "False") { Data = "0"; }

                bool EscapeBrs = EscapeBr;
                if (key.ColumnName.IndexOf("script") >= 0) { EscapeBrs = true; }

                if (SqlElm[key.ColumnName].GetType() == typeof(string) || SqlElm[key.ColumnName].GetType() == typeof(System.DBNull))
                {
                    Data = LibSql.EscapeString(Data, EscapeBrs);
                }
                else if (SqlElm[key.ColumnName].GetType() == typeof(DateTime))
                {
                    Data = LibSql.EscapeString(Data, EscapeBrs);
                }

                datals.Add("[" + key.ColumnName + "]=" + Data + "");
            }

            sql = "" +
                "UPDATE " + table + " SET " +
                string.Join(",", datals.ToArray()) +
                " ";

            if (wheres.Length > 0)
            {
                sql = sql +
                    " WHERE " +
                    wheres;
            }

            return sql;
        }

        public static string MakeInSql(string table, Hashtable SqlElm)
        {
            int i;
            string sql = "";
            string[] keyst = new string[SqlElm.Count];
            string[] values = new string[SqlElm.Count];

            i = 0;
            foreach (string key in SqlElm.Keys)
            {
                keyst[i] = "[" + key + "]";
                string inData = SqlElm[key].ToString();
                if (inData == "True") { inData = "1"; }
                else if (inData == "False") { inData = "0"; }
                values[i] = "" + inData + "";
                i++;
            }

            sql = "" +
                "INSERT INTO " + table + " (" +
                string.Join(",", keyst) +
                ") VALUES (" +
                string.Join(",", values) +
                ")";

            return sql;
        }

        public static string MakeInSql(string table, DataRow SqlElm)
        {
            return MakeInSql(table, SqlElm, false, new List<string>());
        }

        public static string MakeInSql(string table, DataRow SqlElm, bool EscapeBr)
        {
            return MakeInSql(table, SqlElm, EscapeBr, new List<string>());
        }

        public static string MakeInSql(string table, DataRow SqlElm, List<string> OutofColumn)
        {
            return MakeInSql(table, SqlElm, false, OutofColumn);
        }

        public static string MakeInSql(string table, DataRow SqlElm, bool EscapeBr, List<string> OutofColumn)
        {
            string sql = "";

            List<string> keyst = new List<string>();
            List<string> values = new List<string>();

            foreach (DataColumn key in SqlElm.Table.Columns)
            {
                if (key.ColumnName.Length > 3 && key.ColumnName.Substring(0, 3) == "ns_") { continue; }
                if (OutofColumn.Exists(input => input == key.ColumnName)) { continue; }

                string Data = SqlElm[key.ColumnName].ToString();
                if (Data == "True") { Data = "1"; }
                else if (Data == "False") { Data = "0"; }

                bool EscapeBrs = EscapeBr;
                if (key.ColumnName.IndexOf("script") >= 0) { EscapeBrs = true; }

                if (SqlElm[key.ColumnName].GetType() == typeof(string) || SqlElm[key.ColumnName].GetType() == typeof(System.DBNull))
                {
                    Data = LibSql.EscapeString(Data, EscapeBrs);
                }
                else if (SqlElm[key.ColumnName].GetType() == typeof(DateTime))
                {
                    Data = LibSql.EscapeString(Data, EscapeBrs);
                }

                keyst.Add("[" + key.ColumnName + "]");
                values.Add("" + Data + "");
            }

            sql = "" +
                "INSERT INTO " + table + " (" +
                string.Join(",", keyst.ToArray()) +
                ") VALUES (" +
                string.Join(",", values.ToArray()) +
                ")";

            return sql;
        }

        public static string EscapeString(string data)
        {
            return EscapeString(data, false);
        }

        public static string EscapeString(string data, bool EscapeBr)
        {
            if (data.Length == 0) { return "''"; }
            if (data == "True" || data == "False") { return data; }

            data = data.Replace("'", "''");
            //data = data.Replace("\\", "\\\\");
            if (!EscapeBr)
            {
                data = data.Replace("\r\n", "<br />");
            }

            data = "'" + data + "'";

            return data;
        }

        public static string EscapeStringLine(string data)
        {
            if (data.Length == 0) { return "''"; }
            if (data == "True" || data == "False") { return data; }

            data = data.Replace("'", "''");
            //data = data.Replace("\\", "\\\\");

            data = "'" + data + "'";

            return data;
        }

        public static string EscapeLike(string data)
        {
            data = data.Replace("[", "[[]");
            data = data.Replace("%", "[%]");
            data = data.Replace("_", "[_]");

            data = EscapeString(data);

            return data;
        }

        public static string EscapeBool(string data)
        {
            if (data.Length == 0) { return "false"; }
            if (data == "True" || data == "False") { return data; }
            if (data == "1") { return "true"; }

            return "false";
        }

        public static int EscapeBoolChange(bool data)
        {
            if (data) { return 1; }

            return 0;
        }

        public static string EscapeBool2(string data)
        {
            if (data.Length == 0) { return "0"; }
            if (data == "True") { return "1"; }
            if (data == "False") { return "0"; }
            if (data == "1") { return "1"; }

            return "0";
        }

        public static object EscapeWheres(object data)
        {
            if (data.ToString() == "True") { return "1"; }
            if (data.ToString() == "False") { return "0"; }

            int test = 0;
            if (!int.TryParse(data.ToString(), out test))
            {
                return EscapeString(data.ToString());
            }

            return data;
        }

        public static string ConvertMum(string num)
        {
            if (Regex.IsMatch(num, "^[\\+\\-]?\\d+(\\.\\d+)?([eE][\\+\\-]?\\d+)?$"))
            {
                return num;
            }

            return "0";
        }

        public static int ConvertBit(bool flag)
        {
            if (flag)
            {
                return 1;
            }

            return 0;
        }

        public static string Replace(string base_str, System.Data.DataRow rh_param2)
        {
            Hashtable rh_param = new Hashtable();

            foreach (System.Data.DataColumn col in rh_param2.Table.Columns)
            {
                rh_param.Add(col.Caption, rh_param2[col]);
            }

            // ステップ１
            string re_str = "";
            re_str = replace_step1(base_str, rh_param);

            return re_str;
        }

        public static string Replace(string base_str, System.Data.DataRowView rh_param2)
        {
            Hashtable rh_param = new Hashtable();

            foreach (System.Data.DataColumn col in rh_param2.Row.Table.Columns)
            {
                rh_param.Add(col.Caption, rh_param2.Row[col]);
            }

            // ステップ１
            string re_str = "";
            re_str = replace_step1(base_str, rh_param);

            return re_str;
        }

        public static string Replace(string base_str, Hashtable rh_param)
        {
            // ステップ１
            string re_str = replace_step1(base_str, rh_param);

            return re_str;
        }

        private static string replace_step1(string base_str, Hashtable rh_param)
        {
            //########################################################
            //# 置換対象ベース文字列を左から右へ順次置換していくイメージ

            StringBuilder re_str = new StringBuilder();
            re_str.Append("");// ←処理が済んだ部分が付け足されていく。
            while (true)
            {
                Match mt = Regex.Match(base_str, "(.*?)\\*(.{2})\\{(.+?)\\}(.*)");
                if (!mt.Success)
                {
                    //# 置換識別子にマッチするものがなければ、置換処理を終了
                    re_str.Append(base_str);
                    break;
                }
                //# 置換識別子以前のものは処理済として戻り値に追加
                re_str.Append(mt.Groups[1].ToString()); //# 「#?{XXX}」より前の処理対象外

                string operators = mt.Groups[2].ToString();	//# 「*??{XXX}」の「??」の部分(=識別子)
                //#	「IF/LK/ST/NM」のうちのどれか

                string key = mt.Groups[3].ToString();	//# 「*??{XXX}」の「XXX」の部分(=キー)
                base_str = mt.Groups[4].ToString();	//# 「*??{XXX}」より後の部分

                //# キーに対応する値をハッシュより取得
                object value_str = rh_param[key];

                //# 表示切替の場合
                if (operators == "IF")
                { //# 識別子はIF

                    //# のこりの部分を範囲終端「{XXX}#」以前と以降に
                    string unit = replace_step2(key, ref base_str);

                    //# 該当部分を有効にする場合は置換対象ベース文字列に戻す
                    if (base_str.Length > 0)
                    {
                        base_str = unit + base_str;
                    }
                }
                //# 単項の識別子の場合
                else
                { //# ←「LK/ST/NM」のうちどれか
                    //# 置換部分の処理
                    re_str.Append(replace_step3(operators, key, value_str));
                }
            }
            //# 置換された結果を返す。
            return re_str.ToString();
        }

        private static string replace_step2(string key, ref string base_str)
        {
            string unit = "";

            //# 同じ識別子とキーから、対応する終端を捜す。
            Match mt = Regex.Match(base_str, "(.*?)\\{$key\\}END\\*(.*)");
            if (mt.Success)
            {
                unit = mt.Groups[1].ToString();
                base_str = mt.Groups[2].ToString();
            }
            //# 対応する終端が無い場合、のこりを全て対象範囲とする。
            else
            {
                unit = base_str;
                base_str = "";
            }
            //# 切り出した対象範囲部分文字列を返す。
            return unit;
        }

        private static string replace_step3(string operators, string key, object value_str)
        {
            string value_str_s = value_str.ToString();

            //# 文字列エスケープ
            if (operators == "ST")
            {
                value_str_s = EscapeString(value_str_s);
            }
            //# 数値判定
            else if (operators == "NM")
            {
                value_str_s = ConvertMum(value_str_s);
            }
            //# LIKEエスケープ
            else if (operators == "LK")
            {
                value_str_s = EscapeLike(value_str_s);
            }
            //# BOOL
            else if (operators == "BL")
            {
                value_str_s = EscapeBool(value_str_s);
            }
            //# BOOL
            else if (operators == "BX")
            {
                value_str_s = EscapeBool2(value_str_s);
            }
            //# エスケープなし
            else if (operators == "NO")
            {
            }

            return value_str_s;
        }
    }
}
