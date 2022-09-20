using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.Character;

namespace CommonLibrary
{
    public class LibHateRow
    {
        public LibUnitBase Target;

        /// <summary>
        /// ヘイト
        /// </summary>
        public int HateCount = 0;

        public bool Valid = true;

        public LibHateRow(LibUnitBase Hater, int Hates)
        {
            Target = Hater;
            HateCount = Hates;
            Valid = true;
        }
    }
}
