using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.VisualBasic;
using CommonLibrary.Integer;

namespace CommonLibrary
{
    /// <summary>
    /// 数値に関係するルーチンを集めたクラスです。
    /// </summary>
    public static class LibInteger
    {
        static LibInteger()
        {
            seed = LibCommonLibrarySettings.Seed;
        }

        /// <summary>
        /// 新規に入る最も若い番号を取得します。
        /// </summary>
        /// <param name="Numbers">探査対象数値配列</param>
        /// <returns>最も若い番号</returns>
        public static int GetNewUnderNum(int[] Numbers)
        {
            if (Numbers.Length == Numbers[Numbers.Length - 1])
            {
                return Numbers.Length;
            }
            else
            {
                int GetNum = Numbers.Length + 1;
                int oldNum = 0;
                foreach (int nowNum in Numbers)
                {
                    if ((oldNum + 1) != nowNum)
                    {
                        GetNum = oldNum + 1;
                        break;
                    }
                    oldNum = nowNum;
                }
                return GetNum;
            }
        }
        /// <summary>
        /// 新規に入る最も若い番号を取得します。
        /// </summary>
        /// <param name="NumberTable">探査対象テーブル</param>
        /// <param name="NumColumn">数値のカラム名</param>
        /// <returns></returns>
        public static int GetNewUnderNum(DataTable NumberTable, string NumColumn)
        {
            DataView NumverView = new DataView(NumberTable);
            NumverView.Sort = NumColumn;
            if (NumverView.Count == 0)
            {
                return 1;
            }
            else if (NumverView.Count == (int)NumverView[NumverView.Count - 1][NumColumn])
            {
                return NumverView.Count + 1;
            }
            else
            {
                int GetNum = NumverView.Count + 1;
                int oldNum = 0;
                foreach (DataRowView nowNumRow in NumverView)
                {
                    if ((oldNum + 1) != (int)nowNumRow[NumColumn])
                    {
                        GetNum = oldNum + 1;
                        break;
                    }
                    oldNum = (int)nowNumRow[NumColumn];
                }
                return GetNum;
            }
        }
        /// <summary>
        /// 新規に入る最も若い番号を取得します。
        /// </summary>
        /// <param name="NumberTable">探査対象テーブル</param>
        /// <param name="NumColumn">数値のカラム名</param>
        /// <returns></returns>
        public static int GetNewUnderNum(DataView NumberTable, string NumColumn)
        {
            DataView NumverView = NumberTable;
            NumverView.Sort = NumColumn;
            if (NumverView.Count == 0)
            {
                return 1;
            }
            else if (NumverView.Count == (int)NumverView[NumverView.Count - 1][NumColumn])
            {
                return NumverView.Count + 1;
            }
            else
            {
                int GetNum = NumverView.Count + 1;
                int oldNum = 0;
                foreach (DataRowView nowNumRow in NumverView)
                {
                    if ((oldNum + 1) != (int)nowNumRow[NumColumn])
                    {
                        GetNum = oldNum + 1;
                        break;
                    }
                    oldNum = (int)nowNumRow[NumColumn];
                }
                return GetNum;
            }
        }
        /// <summary>
        /// 新規に入る最も若い番号を取得します。
        /// </summary>
        /// <param name="NumberTable">探査対象テーブル</param>
        /// <param name="NumColumn">数値のカラム名</param>
        /// <returns></returns>
        public static int GetNewUnderNumEx(DataTable NumberTable, string NumColumn)
        {
            DataView NumverView = new DataView(NumberTable);
            NumverView.Sort = NumColumn;
            if (NumverView.Count == 0)
            {
                return 0;
            }
            else if ((NumverView.Count - 1) == (int)NumverView[NumverView.Count - 1][NumColumn])
            {
                return NumverView.Count;
            }
            else
            {
                int GetNum = NumverView.Count;
                int oldNum = -1;
                foreach (DataRowView nowNumRow in NumverView)
                {
                    if ((oldNum + 1) != (int)nowNumRow[NumColumn])
                    {
                        GetNum = oldNum + 1;
                        break;
                    }
                    oldNum = (int)nowNumRow[NumColumn];
                }
                return GetNum;
            }
        }

        /// <summary>
        /// 指定したカラムデータを数値に強制変換します
        /// </summary>
        /// <param name="Data">カラムの入ったDataRow</param>
        /// <param name="ColumnName">カラム名</param>
        /// <returns>強制変換された数値</returns>
        public static int GetDataRowInteger(DataRow Data, string ColumnName)
        {
            if (Data[ColumnName].GetType().FullName == "System.DBNull")
            {
                return 0;
            }
            else
            {
                return (int)Data[ColumnName];
            }
        }

        /// <summary>
        /// 文字列を半角に変換
        /// </summary>
        /// <param name="TextInt">変換対象</param>
        /// <returns>変換後の数字</returns>
        public static string ConvStr(string TextInt)
        {
            TextInt = Strings.StrConv(TextInt, VbStrConv.Narrow, 0);

            return TextInt;
        }

        private static uint seed = 0;
        public static MersenneTwister rand = new MersenneTwister();

        /// <summary>
        /// SEEDの初期化
        /// </summary>
        public static void SetNewSeed()
        {
            seed = (uint)System.Environment.TickCount;
            rand = new MersenneTwister(seed);
            LibCommonLibrarySettings.Seed = seed;
        }

        /// <summary>
        /// 0～Max-1までの乱数を取得
        /// </summary>
        /// <param name="Max">最大値</param>
        /// <returns>乱数</returns>
        public static int GetRand(int Max)
        {
            if (seed == 0)
            {
                seed = (uint)System.Environment.TickCount;
                rand = new MersenneTwister(seed);
            }
            else { seed++; }
            return rand.Next(Max);
        }
        /// <summary>
        /// 0～Maxまでの乱数を取得
        /// </summary>
        /// <param name="Max">最大値</param>
        /// <returns>乱数</returns>
        public static int GetRandMax(int Max)
        {
            Max++;
            return GetRand(Max);
        }

        /// <summary>
        /// ベイシス確率で乱数を取得
        /// </summary>
        /// <param name="Max">最大値</param>
        /// <returns>乱数</returns>
        public static decimal GetRandBasis()
        {
            return (decimal)GetRandMax(10000) / 100m;
        }

        /// <summary>
        /// Min～Max-1までの乱数を取得
        /// </summary>
        /// <param name="Min">最小値</param>
        /// <param name="Max">最大値</param>
        /// <returns>乱数</returns>
        public static int GetRand(int Min, int Max)
        {
            if (seed == 0)
            {
                seed = (uint)System.Environment.TickCount;
                rand = new MersenneTwister(seed);
            }
            else { seed++; }
            return rand.Next(Min, Max);
        }

        /// <summary>
        /// Min～Maxまでの乱数を取得
        /// </summary>
        /// <param name="Min">最小値</param>
        /// <param name="Max">最大値</param>
        /// <returns>乱数</returns>
        public static int GetRandMax(int Min, int Max)
        {
            Max++;
            return GetRand(Min, Max);
        }

        /// <summary>
        /// 正規分布の乱数取得
        /// </summary>
        /// <param name="Base">平均</param>
        /// <param name="Sigma">分布</param>
        /// <returns>乱数(平均+分布)</returns>
        public static double GetRandBoxMulur(double Base, double Sigma)
        {
            if (seed == 0)
            {
                seed = (uint)System.Environment.TickCount;
                rand = new MersenneTwister(seed);
            }
            else { seed++; }

            double x = rand.NextDouble();
            double y = rand.NextDouble();

            double resultA = Math.Sqrt((-2 * Math.Log(x))) * (Math.Cos(2 * Math.PI * y));

            return (resultA * Sigma) + Base;
        }

        /// <summary>
        /// 確率コンボから実際の確率表記を算出
        /// </summary>
        /// <param name="ComboIndex">確率コンボインデックス</param>
        /// <returns>実際の確率</returns>
        public static int ChangeProbCombo(int ComboIndex)
        {
            int Prob = 0;
            switch (ComboIndex)
            {
                case 0:
                    Prob = 1;
                    break;
                case 1:
                    Prob = 3;
                    break;
                case 2:
                    Prob = 25;
                    break;
                case 3:
                    Prob = 40;
                    break;
                case 4:
                    Prob = 100;
                    break;
            }

            return Prob;
        }

        /// <summary>
        /// 確率コンボから実際の確率表記を算出
        /// </summary>
        /// <param name="ComboIndex">確率コンボインデックス</param>
        /// <returns>実際の確率</returns>
        public static string ChangeProbComboStr(int ComboIndex)
        {
            string Prob = "";
            switch (ComboIndex)
            {
                case 0:
                    Prob = "超低";
                    break;
                case 1:
                    Prob = "低";
                    break;
                case 2:
                    Prob = "中";
                    break;
                case 3:
                    Prob = "高";
                    break;
                case 4:
                    Prob = "必";
                    break;
                case 5:
                    Prob = "心得";
                    break;
            }

            return Prob;
        }

        /// <summary>
        /// レベルからランクを算出
        /// </summary>
        /// <param name="Level">レベル</param>
        /// <returns>ランク</returns>
        public static decimal LevelToRank(int Level)
        {
            return (decimal)((Level - 1.0) / 6.0 + 1.0);
        }

        /// <summary>
        /// ランクからレベルを算出
        /// </summary>
        /// <param name="Rank">ランク</param>
        /// <returns>レベル</returns>
        public static int RankToLevel(int Rank)
        {
            return (int)(6.0 * ((double)Rank - 1.0) + 1.0);
        }

        /// <summary>
        /// 数値を漢数字にする
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string ConvertKansuji(int Number)
        {
            if (Number == 0)
            {
                return "〇";
            }

            string[] kl = new string[] { "", "十", "百", "千" };
            string[] tl = new string[] { "", "万", "億", "兆", "京" };
            string[] nl = new string[] { "", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            string str = "";
            int keta = 0;
            while (Number > 0)
            {
                int k = keta % 4;
                int n = (int)(Number % 10);

                if (k == 0 && Number % 10000 > 0)
                {
                    str = tl[keta / 4] + str;
                }

                if (k != 0 && n == 1)
                {
                    str = kl[k] + str;
                }
                else if (n != 0)
                {
                    str = nl[n] + kl[k] + str;
                }

                keta++;
                Number /= 10;
            }
            return str;
        }
    }
}
