using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.Character;

namespace CommonLibrary
{
    /// <summary>
    /// ヘイト関連
    /// </summary>
    public class LibHate
    {
        private List<LibHateRow> HateList = new List<LibHateRow>();

        public LibHate()
        {
            HateList = new List<LibHateRow>();
        }

        /// <summary>
        /// リストへ追加
        /// </summary>
        /// <param name="Target">増加相手</param>
        /// <param name="Hate">増加ヘイト量</param>
        public void Add(LibUnitBase Target, int Hate)
        {
            bool IsNown = false;
            foreach (LibHateRow SelHateRow in HateList)
            {
                if (!SelHateRow.Valid)
                {
                    continue;
                }

                if (SelHateRow.Target.BattleID == Target.BattleID)
                {
                    IsNown = true;
                    SelHateRow.HateCount += Hate;
                    if (SelHateRow.HateCount < 0)
                    {
                        SelHateRow.HateCount = 0;
                    }
                    break;
                }
            }

            if (!IsNown)
            {
                LibHateRow NewHateRow = new LibHateRow(Target, Hate);
                HateList.Add(NewHateRow);
            }
        }

        /// <summary>
        /// トップヘイトを取得
        /// </summary>
        /// <param name="MinePartyBelong">自分の所属（敵対者を算出）</param>
        /// <returns>ヘイト</returns>
        public int GetTopHate(int MinePartyBelong)
        {
            if (HateList.Count == 0)
            {
                return 0;
            }

            LibHateRow SelectedRow = null;
            int MaxHate = 0;
            foreach (LibHateRow SelHateRow in HateList)
            {
                if (!SelHateRow.Valid)
                {
                    continue;
                }

                switch (MinePartyBelong)
                {
                    case Status.Belong.Friend:
                        if (SelHateRow.Target.PartyBelong == Status.Belong.Friend)
                        {
                            continue;
                        }
                        break;
                    case Status.Belong.Enemy:
                        if (SelHateRow.Target.PartyBelong == Status.Belong.Enemy)
                        {
                            continue;
                        }
                        break;
                }

                if (SelHateRow.HateCount >= MaxHate)
                {
                    MaxHate = SelHateRow.HateCount;
                    SelectedRow = SelHateRow;
                }
            }

            if (SelectedRow != null)
            {
                return SelectedRow.HateCount;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 一番ヘイトの高いものを取得
        /// </summary>
        /// <param name="MinePartyBelong">自分の所属（敵対者を算出）</param>
        /// <returns>トップヘイター</returns>
        public LibUnitBase GetTop(int MinePartyBelong)
        {
            if (HateList.Count == 0)
            {
                return null;
            }

            LibHateRow SelectedRow = null;
            int MaxHate = 0;
            foreach (LibHateRow SelHateRow in HateList)
            {
                if (!SelHateRow.Valid)
                {
                    continue;
                }

                switch (MinePartyBelong)
                {
                    case Status.Belong.Friend:
                        if (SelHateRow.Target.PartyBelong == Status.Belong.Friend)
                        {
                            continue;
                        }
                        break;
                    case Status.Belong.Enemy:
                        if (SelHateRow.Target.PartyBelong == Status.Belong.Enemy)
                        {
                            continue;
                        }
                        break;
                }

                if (SelHateRow.HateCount >= MaxHate)
                {
                    MaxHate = SelHateRow.HateCount;
                    SelectedRow = SelHateRow;
                }
            }

            if (SelectedRow != null)
            {
                return SelectedRow.Target;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 一番ヘイトの低いものを取得
        /// </summary>
        /// <param name="MinePartyBelong">自分の所属（敵対者を算出）</param>
        /// <returns>アンカーヘイター</returns>
        public LibUnitBase GetAnc(int MinePartyBelong)
        {
            if (HateList.Count == 0)
            {
                return null;
            }

            LibHateRow SelectedRow = null;
            int MaxHate = int.MaxValue;
            foreach (LibHateRow SelHateRow in HateList)
            {
                if (!SelHateRow.Valid)
                {
                    continue;
                }

                switch (MinePartyBelong)
                {
                    case Status.Belong.Friend:
                        if (SelHateRow.Target.PartyBelong == Status.Belong.Friend)
                        {
                            continue;
                        }
                        break;
                    case Status.Belong.Enemy:
                        if (SelHateRow.Target.PartyBelong == Status.Belong.Enemy)
                        {
                            continue;
                        }
                        break;
                }

                if (SelHateRow.HateCount <= MaxHate)
                {
                    MaxHate = SelHateRow.HateCount;
                    SelectedRow = SelHateRow;
                }
            }

            if (SelectedRow != null)
            {
                return SelectedRow.Target;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 対象をねらっているかどうか
        /// </summary>
        /// <param name="Target">対象</param>
        /// <returns>狙っている</returns>
        public bool Check(LibUnitBase Target)
        {
            bool IsNown = false;

            foreach (LibHateRow SelHateRow in HateList)
            {
                if (!SelHateRow.Valid)
                {
                    continue;
                }

                if (SelHateRow.Target.BattleID == Target.BattleID)
                {
                    IsNown = true;
                    break;
                }
            }

            return IsNown;
        }

        /// <summary>
        /// 味方をねらっているかどうか
        /// </summary>
        /// <param name="Mines">自分</param>
        /// <returns>狙っている</returns>
        public bool CheckFriends(LibUnitBase Mines)
        {
            bool IsNown = false;

            foreach (LibHateRow SelHateRow in HateList)
            {
                if (!SelHateRow.Valid)
                {
                    continue;
                }

                if (SelHateRow.Target.PartyBelong == Mines.PartyBelong && SelHateRow.Target.BattleID != Mines.BattleID)
                {
                    IsNown = true;
                    break;
                }
            }

            return IsNown;
        }

        /// <summary>
        /// 狙っている対象のヘイト獲得
        /// </summary>
        /// <param name="Target">対象</param>
        /// <returns>ヘイト</returns>
        public int GetHate(LibUnitBase Target)
        {
            int Hates = 0;

            foreach (LibHateRow SelHateRow in HateList)
            {
                if (!SelHateRow.Valid)
                {
                    continue;
                }

                if (SelHateRow.Target.BattleID == Target.BattleID)
                {
                    Hates = SelHateRow.HateCount;
                    break;
                }
            }

            return Hates;
        }

        /// <summary>
        /// ヘイトリストから削除
        /// </summary>
        /// <param name="Target">対象者</param>
        public void Delete(LibUnitBase Target)
        {
            foreach (LibHateRow SelHateRow in HateList)
            {
                if (!SelHateRow.Valid)
                {
                    continue;
                }

                if (SelHateRow.Target.BattleID == Target.BattleID)
                {
                    SelHateRow.HateCount = 0;
                    SelHateRow.Valid = false;
                }
            }
        }

        /// <summary>
        /// ヘイトリストのリセット
        /// </summary>
        /// <param name="Target">対象者</param>
        public void Reset(LibUnitBase Target)
        {
            foreach (LibHateRow SelHateRow in HateList)
            {
                if (!SelHateRow.Valid)
                {
                    continue;
                }

                if (SelHateRow.Target.BattleID == Target.BattleID)
                {
                    SelHateRow.HateCount = 0;
                }
            }
        }

        /// <summary>
        /// ヘイトリストから復活
        /// </summary>
        /// <param name="Target">対象者</param>
        public void Revivals(LibUnitBase Target)
        {
            foreach (LibHateRow SelHateRow in HateList)
            {
                if (SelHateRow.Target.BattleID == Target.BattleID)
                {
                    SelHateRow.Valid = true;
                }
            }
        }
    }
}
