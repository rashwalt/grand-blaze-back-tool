using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data;

namespace CommonLibrary
{
    /// <summary>
    /// �e�L�X�g��u�����Đ��`����
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

            // �X�e�b�v�P
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

            // �X�e�b�v�P
            string re_str = "";
            re_str = replace_step1(base_str, rh_param);

            return re_str;
        }

        public static string Replace(string base_str, Hashtable rh_param)
        {
            // �X�e�b�v�P
            string re_str = replace_step1(base_str, rh_param);

            return re_str;
        }

        private static string replace_step1(string base_str, Hashtable rh_param)
        {
            //########################################################
            //# �u���Ώۃx�[�X�������������E�֏����u�����Ă����C���[�W

            StringBuilder re_str = new StringBuilder();
            re_str.Append("");// ���������ς񂾕������t��������Ă����B
            while (true)
            {
                Match mt = Regex.Match(base_str, "(.*?)\\*(.{2})\\{(.+?)\\}(.*)");
                if (!mt.Success)
                {
                    //# �u�����ʎq�Ƀ}�b�`������̂��Ȃ���΁A�u���������I��
                    re_str.Append(base_str);
                    break;
                }
                //# �u�����ʎq�ȑO�̂��̂͏����ςƂ��Ė߂�l�ɒǉ�
                re_str.Append(mt.Groups[1].ToString()); //# �u#?{XXX}�v���O�̏����ΏۊO

                string operators = mt.Groups[2].ToString();	//# �u*??{XXX}�v�́u??�v�̕���(=���ʎq)
                //#	�uIF/LK/ST/NM�v�̂����̂ǂꂩ

                string key = mt.Groups[3].ToString();	//# �u*??{XXX}�v�́uXXX�v�̕���(=�L�[)
                base_str = mt.Groups[4].ToString();	//# �u*??{XXX}�v����̕���

                //# �L�[�ɑΉ�����l���n�b�V�����擾
                object value_str = rh_param[key];

                //# �\���ؑւ̏ꍇ
                if (operators == "IF")
                { //# ���ʎq��IF

                    //# �̂���̕�����͈͏I�[�u{XXX}#�v�ȑO�ƈȍ~��
                    string unit = replace_step2(key, ref base_str);

                    //# �Y��������L���ɂ���ꍇ�͒u���Ώۃx�[�X������ɖ߂�
                    if (base_str.Length > 0)
                    {
                        base_str = unit + base_str;
                    }
                }
                //# �P���̎��ʎq�̏ꍇ
                else
                { //# ���uLK/ST/NM�v�̂����ǂꂩ
                    //# �u�������̏���
                    re_str.Append(replace_step3(operators, key, value_str));
                }
            }
            //# �u�����ꂽ���ʂ�Ԃ��B
            return re_str.ToString();
        }

        private static string replace_step2(string key, ref string base_str)
        {
            string unit = "";

            //# �������ʎq�ƃL�[����A�Ή�����I�[��{���B
            Match mt = Regex.Match(base_str, "(.*?)\\{$key\\}END\\*(.*)");
            if (mt.Success)
            {
                unit = mt.Groups[1].ToString();
                base_str = mt.Groups[2].ToString();
            }
            //# �Ή�����I�[�������ꍇ�A�̂����S�đΏ۔͈͂Ƃ���B
            else
            {
                unit = base_str;
                base_str = "";
            }
            //# �؂�o�����Ώ۔͈͕����������Ԃ��B
            return unit;
        }

        private static string replace_step3(string operators, string key, object value_str)
        {
            string value_str_s = value_str.ToString();

            //# ������G�X�P�[�v
            if (operators == "ST")
            {
                value_str_s = EscapeString(value_str_s);
            }
            //# ���l����
            else if (operators == "NM")
            {
                value_str_s = ConvertMum(value_str_s);
            }
            //# LIKE�G�X�P�[�v
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
            //# �G�X�P�[�v�Ȃ�
            else if (operators == "NO")
            {
            }

            return value_str_s;
        }
    }
}
