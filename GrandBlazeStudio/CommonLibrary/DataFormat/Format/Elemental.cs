using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.DataFormat.Format
{
    /// <summary>
    /// 属性値設定クラス
    /// </summary>
    public class Elemental
    {
        /// <summary>
        /// 火
        /// </summary>
        public int Fire = 0;

        /// <summary>
        /// 氷
        /// </summary>
        public int Freeze = 0;

        /// <summary>
        /// 風
        /// </summary>
        public int Air = 0;

        /// <summary>
        /// 土
        /// </summary>
        public int Earth = 0;

        /// <summary>
        /// 雷
        /// </summary>
        public int Thunder = 0;

        /// <summary>
        /// 水
        /// </summary>
        public int Water = 0;

        /// <summary>
        /// 聖
        /// </summary>
        public int Holy = 0;

        /// <summary>
        /// 闇
        /// </summary>
        public int Dark = 0;

        /// <summary>
        /// 斬
        /// </summary>
        public int Slash = 0;

        /// <summary>
        /// 突
        /// </summary>
        public int Pierce = 0;

        /// <summary>
        /// 打
        /// </summary>
        public int Strike = 0;

        /// <summary>
        /// 壊
        /// </summary>
        public int Break = 0;

        /// <summary>
        /// 属性設定
        /// </summary>
        /// <param name="Defence">防御属性</param>
        /// <param name="Setin">入れる属性値</param>
        /// <returns>設定値</returns>
        public static void SettingElemental(ref int Defence, int Setin)
        {
            if (Setin < 0 && Defence <= 0) { Defence = Setin; }
            else if (Setin > 0 && Defence < Setin) { Defence = Setin; }
        }

        /// <summary>
        /// 属性を単純な数値に変換
        /// </summary>
        /// <param name="Elemental"></param>
        /// <returns></returns>
        public static int ConvertElementalToInt(int Elemental)
        {
            if (Elemental < 0)
            {
                // 弱点
                return -1;
            }
            else if (Elemental == 0)
            {
                // 通常
                return 0;
            }
            else if (Elemental > 0 && Elemental < 100)
            {
                // 半減
                return 1;
            }
            else if (Elemental == 100)
            {
                // 無効
                return 2;
            }
            else
            {
                // 吸収
                return 3;
            }
        }

        /// <summary>
        /// 属性を単純な属性を変換
        /// </summary>
        /// <param name="Elemental"></param>
        /// <returns></returns>
        public static int ConvertIntToElemental(int Elemental)
        {
            if (Elemental < 0)
            {
                // 弱点
                return -100;
            }
            else if (Elemental == 0)
            {
                // 通常
                return 0;
            }
            else if (Elemental == 1)
            {
                // 半減
                return 50;
            }
            else if (Elemental == 2)
            {
                // 無効
                return 100;
            }
            else
            {
                // 吸収
                return 200;
            }
        }
    }
}
