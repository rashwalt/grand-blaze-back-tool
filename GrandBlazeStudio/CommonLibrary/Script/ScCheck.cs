using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary.Script
{
    /// <summary>
    /// チェック関連
    /// </summary>
    public static class ScCheck
    {
        public static int PartyNo = 0;
        public static int EntryNo = 0;
        public static int MarkID = 0;
        public static List<LibPlayer> CharaList;

        /// <summary>
        /// キャラクター選択
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <returns></returns>
        private static LibPlayer SelectChara(int EntryNo)
        {
            return CharaList.Find(chs => chs.EntryNo == EntryNo);
        }

        /// <summary>
        /// クエストオファー状況確認
        /// </summary>
        /// <param name="QuestID"></param>
        /// <returns></returns>
        public static bool QuestOffer(int QuestID)
        {
            bool Ok = true;
            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (!QuestOfferPrivate(Entrys[i], QuestID))
                    {
                        Ok = false;
                    }
                }
            }
            else
            {
                return QuestOfferPrivate(EntryNo, QuestID);
            }

            return Ok;
        }

        /// <summary>
        /// クエストオファー状況確認(プライベート)
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="QuestID"></param>
        /// <returns></returns>
        public static bool QuestOfferPrivate(int EntryNo, int QuestID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckQuest(QuestID);
        }

        /// <summary>
        /// クエストコンプ状況確認
        /// </summary>
        /// <param name="QuestID"></param>
        /// <returns></returns>
        public static bool QuestComp(int QuestID)
        {
            bool Ok = true;
            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (!QuestCompPrivate(Entrys[i], QuestID))
                    {
                        Ok = false;
                    }
                }
            }
            else
            {
                return QuestCompPrivate(EntryNo, QuestID);
            }

            return Ok;
        }

        /// <summary>
        /// クエストコンプ状況確認(プライベート)
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="QuestID"></param>
        /// <returns></returns>
        public static bool QuestCompPrivate(int EntryNo, int QuestID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckQuestComp(QuestID);
        }

        /// <summary>
        /// アイテム所有確認
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ItemCount"></param>
        /// <returns></returns>
        public static bool HaveItem(int ItemID, int ItemCount)
        {
            bool Ok = false;
            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (HaveItemPrivate(Entrys[i], ItemID, ItemCount))
                    {
                        Ok = true;
                    }
                }
            }
            else
            {
                return HaveItemPrivate(EntryNo, ItemID, ItemCount);
            }

            return Ok;
        }

        /// <summary>
        /// アイテム所有確認(プライベート)
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ItemCount"></param>
        /// <returns></returns>
        public static bool HaveItemPrivate(int EntryNo, int ItemID, int ItemCount)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckHaveItem(Status.ItemBox.Normal, ItemID, false, ItemCount);
        }

        /// <summary>
        /// 貴重品所有確認
        /// </summary>
        /// <param name="KeyID"></param>
        /// <param name="IsFullHaving">全所持</param>
        /// <returns></returns>
        public static bool KeyItem(int KeyID, bool IsFullHaving)
        {
            bool Ok = false;
            if (IsFullHaving) { Ok = true; }
            
            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (IsFullHaving)
                    {
                        if (!KeyItemPrivate(Entrys[i], KeyID))
                        {
                            Ok = false;
                        }
                    }
                    else
                    {
                        if (KeyItemPrivate(Entrys[i], KeyID))
                        {
                            Ok = true;
                        }
                    }
                }
            }
            else
            {
                return KeyItemPrivate(EntryNo, KeyID);
            }

            return Ok;
        }

        /// <summary>
        /// 貴重品所有確認(プライベート)
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public static bool KeyItemPrivate(int EntryNo, int KeyID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckKeyItem(KeyID);
        }

        /// <summary>
        /// フィールド判定
        /// </summary>
        /// <param name="SkillType">スキルタイプ</param>
        /// <param name="RequireValue">必要な値</param>
        /// <param name="WithMessage">入手メッセージ有無</param>
        /// <returns>メッセージ</returns>
        public static bool Field(int SkillType, int RequireValue)
        {
            StringBuilder Msg = new StringBuilder();
            bool Ok = false;

            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (FieldPrivate(Entrys[i], SkillType, RequireValue))
                    {
                        Ok = true;
                    }
                }
            }
            else
            {
                return FieldPrivate(EntryNo, SkillType, RequireValue);
            }

            return Ok;
        }

        /// <summary>
        /// フィールドの判定実態
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="SkillType"></param>
        /// <param name="RequireValue"></param>
        /// <returns></returns>
        public static bool FieldPrivate(int EntryNo, int SkillType, int RequireValue)
        {
            LibPlayer Chara = SelectChara(EntryNo);

            switch (SkillType)
            {
                case 1:
                    // 鍵の解除
                    {
                        if (Chara.RemoveKey(RequireValue))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case 2:
                    // 罠発見
                    {
                        if (Chara.FindTrap(RequireValue))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case 3:
                    // 罠解除
                    {
                        if (Chara.RemoveTrap(RequireValue))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case 4:
                    // ハッキング
                    {
                        if (Chara.RemoveHack(RequireValue))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case 5:
                    // TODO:ネゴシエーション
                    return false;
            }

            return false;
        }

        /// <summary>
        /// SP確認
        /// </summary>
        /// <param name="SPLevel"></param>
        /// <returns></returns>
        public static bool SP(int SPLevel)
        {
            bool Ok = true;

            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (!SPPrivate(Entrys[i], SPLevel))
                    {
                        Ok = false;
                    }
                }
            }
            else
            {
                return SPPrivate(EntryNo, SPLevel);
            }

            return Ok;
        }

        /// <summary>
        /// SP確認(プライベート)
        /// </summary>
        /// <param name="SPLevel"></param>
        /// <returns></returns>
        public static bool SPPrivate(int EntryNo, int SPLevel)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.LevelNormal >= SPLevel;
        }

        /// <summary>
        /// クラスのレベル確認
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static bool ClassLevel(int ClassID, int Level)
        {
            bool Ok = false;

            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (!ClassLevelPrivate(Entrys[i], ClassID, Level))
                    {
                        Ok = true;
                    }
                }
            }
            else
            {
                return ClassLevelPrivate(EntryNo, ClassID, Level);
            }

            return Ok;
        }

        /// <summary>
        /// クラスレベル確認(プライベート)
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static bool ClassLevelPrivate(int EntryNo, int ClassID, int Level)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckInstallLevel(ClassID, Level);
        }

        /// <summary>
        /// 所持BC確認
        /// </summary>
        /// <param name="BlazeChipCount"></param>
        /// <returns></returns>
        public static bool BC(int BlazeChipCount)
        {
            bool Ok = true;

            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (!BCPrivate(Entrys[i], BlazeChipCount))
                    {
                        Ok = false;
                    }
                }
            }
            else
            {
                return BCPrivate(EntryNo, BlazeChipCount);
            }

            return Ok;
        }

        /// <summary>
        /// 所持BC確認(プライベート)
        /// </summary>
        /// <param name="BlazeChipCount"></param>
        /// <returns></returns>
        public static bool BCPrivate(int EntryNo, int BlazeChipCount)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.BlazeChip >= BlazeChipCount;
        }
    }
}
