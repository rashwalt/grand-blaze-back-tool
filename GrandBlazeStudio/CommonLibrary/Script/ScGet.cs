using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Character;

namespace CommonLibrary.Script
{
    /// <summary>
    /// 入手系
    /// </summary>
    public static class ScGet
    {
        public static int PartyNo = 0;
        public static int EntryNo = 0;
        public static int MarkID = 0;
        public static List<LibPlayer> CharaList;
        private static LibUnitBaseMini chm = new LibUnitBaseMini();

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
        /// 貴重品入手
        /// </summary>
        /// <param name="KeyID">貴重品ID</param>
        /// <param name="WithMessage">入手メッセージ有無</param>
        /// <returns>メッセージ</returns>
        public static string KeyItem(int KeyID, bool WithMessage)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNo > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (KeyItemPrivate(Entrys[i], KeyID))
                    {
                        GetCount++;
                    }
                }

                if (GetCount > 0)
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append("貴重品「" + LibResultText.CSSEscapeKeyItem(LibKeyItem.GetKeyItemName(KeyID)) + "」を手に入れた。");
                    }
                }
            }
            else
            {
                if (KeyItemPrivate(EntryNo, KeyID))
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append("貴重品「" + LibResultText.CSSEscapeKeyItem(LibKeyItem.GetKeyItemName(KeyID)) + "」を手に入れた。");
                    }
                }
            }

            return Msg.ToString();
        }

        /// <summary>
        /// 貴重品の入手実態
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public static bool KeyItemPrivate(int EntryNo, int KeyID)
        {
            LibPlayer Chara = SelectChara(EntryNo);

            return Chara.AddKeyItem(KeyID);
        }

        /// <summary>
        /// クエストのオファー
        /// </summary>
        /// <param name="QuestID">クエストID</param>
        /// <param name="WithMessage">入手メッセージ有無</param>
        public static string QuestOffer(int QuestID, bool WithMessage)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNo > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_QuestOffer(Entrys[i], QuestID))
                    {
                        GetCount++;
                    }
                }

                if (GetCount > 0)
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append("クエスト「" + LibResultText.CSSEscapeQuest(LibQuest.GetQuestName(QuestID)) + "」をオファーしました。");
                    }
                }
            }
            else
            {
                if (_QuestOffer(EntryNo, QuestID))
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append("クエスト「" + LibResultText.CSSEscapeQuest(LibQuest.GetQuestName(QuestID)) + "」をオファーしました。");
                    }
                }
            }

            return Msg.ToString();
        }

        /// <summary>
        /// クエストのオファー実態
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="QuestID"></param>
        private static bool _QuestOffer(int EntryNo, int QuestID)
        {
            LibPlayer Chara = SelectChara(EntryNo);

            return Chara.OfferQuest(QuestID);
        }

        /// <summary>
        /// クエストのコンプリート
        /// </summary>
        /// <param name="QuestID">クエストID</param>
        /// <param name="WithMessage">入手メッセージ有無</param>
        public static string QuestComplete(int QuestID, bool WithMessage)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNo > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_QuestComplete(Entrys[i], QuestID))
                    {
                        GetCount++;
                    }
                }

                if (GetCount > 0)
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append("=== Congratulations! ===<br />クエスト「" + LibResultText.CSSEscapeQuest(LibQuest.GetQuestName(QuestID)) + "」をコンプリートしました！");
                    }
                }
            }
            else
            {
                if (_QuestComplete(EntryNo, QuestID))
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append("=== Congratulations! ===<br />クエスト「" + LibResultText.CSSEscapeQuest(LibQuest.GetQuestName(QuestID)) + "」をコンプリートしました！");
                    }
                }
            }

            return Msg.ToString();
        }

        /// <summary>
        /// クエストのコンプリート実態
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="QuestID"></param>
        private static bool _QuestComplete(int EntryNo, int QuestID)
        {
            LibPlayer Chara = SelectChara(EntryNo);

            Chara.CompleteQuest(QuestID);

            return true;
        }

        /// <summary>
        /// マーク情報入手
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <param name="WithMessage">入手メッセージ有無</param>
        /// <returns>メッセージ</returns>
        public static string Mark(int InMarkID, bool WithMessage)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNo > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_MarkGet(Entrys[i], InMarkID))
                    {
                        GetCount++;
                    }
                }

                if (GetCount > 0)
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append(LibResultText.CSSEscapeQuest(LibQuest.GetQuestNameByMarkID(MarkID)) + "のマーク「" + LibResultText.CSSEscapeMark(LibQuest.GetMarkName(InMarkID)) + "」に行けるようになった！");
                    }
                }
            }
            else
            {
                if (_MarkGet(EntryNo, InMarkID))
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append(LibResultText.CSSEscapeQuest(LibQuest.GetQuestNameByMarkID(MarkID)) + "のマーク「" + LibResultText.CSSEscapeMark(LibQuest.GetMarkName(InMarkID)) + "」に行けるようになった！");
                    }
                }
            }

            return Msg.ToString();
        }

        /// <summary>
        /// マークの入手実態
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        private static bool _MarkGet(int EntryNo, int InMarkID)
        {
            LibPlayer Chara = SelectChara(EntryNo);

            return Chara.SetMovingMark(InMarkID, false, true);
        }

        /// <summary>
        /// バトル開始
        /// </summary>
        /// <param name="PopMonsters"></param>
        /// <param name="PopNonplayers"></param>
        /// <param name="Style"></param>
        /// <param name="WinStyle"></param>
        /// <param name="LoseStyle"></param>
        /// <param name="CofferID"></param>
        public static void BattleStart(string PopMonsters, string PopNonplayers, int BattleStartStyle, int WinStyle, int LoseStyle, int CofferID)
        {
            LibPartyBattleSet bt = new LibPartyBattleSet();
            bt.Update(PartyNo, PopMonsters, PopNonplayers, BattleStartStyle, WinStyle, LoseStyle, MarkID, CofferID);
        }

        /// <summary>
        /// パーティメンバーリスト
        /// </summary>
        /// <returns></returns>
        public static int[] PartyMember()
        {
            if (PartyNo > 0)
            {
                return LibParty.PartyMemberNo(PartyNo);
            }
            else
            {
                return LibParty.PartyMemberNo(LibParty.GetPartyNo(EntryNo));
            }
        }

        /// <summary>
        /// バトル結果
        /// </summary>
        /// <returns></returns>
        public static int BattleResult()
        {
            return LibBattleResult.GetResult(PartyNo, MarkID);
        }

        /// <summary>
        /// クエストのステージ取得
        /// </summary>
        /// <param name="QuestID">クエストID</param>
        public static int QuestStage(int QuestID)
        {
            int Step = 1000;
            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    int GetStep = QuestStagePrivate(Entrys[i], QuestID);
                    if (GetStep < Step)
                    {
                        Step = GetStep;
                    }
                }
            }
            else
            {
                Step = QuestStagePrivate(EntryNo, QuestID);
            }

            return Step;
        }

        /// <summary>
        /// クエストのステージ数
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="QuestID"></param>
        public static int QuestStagePrivate(int EntryNo, int QuestID)
        {
            LibPlayer Chara = SelectChara(EntryNo);

            return Chara.QuestStage(QuestID);
        }

        /// <summary>
        /// クエストステージ設定
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="QuestID"></param>
        /// <param name="StageCount"></param>
        public static void QuestStageSet(int EntryNo, int QuestID, int StageCount)
        {
            LibPlayer Chara = SelectChara(EntryNo);

            Chara.SetQuestStage(QuestID, StageCount);
        }

        /// <summary>
        /// お金入手
        /// </summary>
        /// <param name="Money">入手金</param>
        /// <param name="WithMessage">入手メッセージ有無</param>
        /// <returns>メッセージ</returns>
        public static string Money(int Money, bool WithMessage)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNo > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (MoneyPrivate(Entrys[i], Money))
                    {
                        GetCount++;
                    }
                }

                if (GetCount > 0)
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append(LibResultText.CSSEscapeMoney(Money, false) + "を手に入れた。<br />");
                    }
                }
            }
            else
            {
                if (MoneyPrivate(EntryNo, Money))
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append(LibResultText.CSSEscapeMoney(Money, false) + "を手に入れた。<br />");
                    }
                }
            }

            return Msg.ToString();
        }

        /// <summary>
        /// お金の入手実態
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="Money"></param>
        /// <returns></returns>
        public static bool MoneyPrivate(int EntryNo, int Money)
        {
            LibPlayer Chara = SelectChara(EntryNo);

            Chara.HaveMoney += Money;

            return true;
        }

        /// <summary>
        /// 経験値アップ
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="Exp"></param>
        /// <returns></returns>
        public static void ExpUp(int EntryNo, int Exp)
        {
            LibPlayer Chara = SelectChara(EntryNo);

            Chara.GetExp += Exp;
        }

        /// <summary>
        /// アイテムの消費
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ItemCount"></param>
        public static void UsingItem(int ItemID, int ItemCount)
        {
            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (ScCheck.HaveItemPrivate(Entrys[i], ItemID, ItemCount))
                    {
                        bool Usings = UsingItemPrivate(Entrys[i], ItemID, ItemCount);
                        if (Usings)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                UsingItemPrivate(EntryNo, ItemID, ItemCount);
            }
        }

        /// <summary>
        /// アイテムの消費(プライベート)
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="ItemID"></param>
        /// <param name="ItemCount"></param>
        /// <returns></returns>
        public static bool UsingItemPrivate(int EntryNo, int ItemID, int ItemCount)
        {
            bool Usings = false;
            LibPlayer ch = SelectChara(EntryNo);
            Usings = ch.ItemNoRemoveItem(Status.ItemBox.Normal, ItemID, ItemCount);

            return Usings;
        }

        /// <summary>
        /// 貴重品の消費
        /// </summary>
        /// <param name="KeyID"></param>
        public static void UsingKeyItem(int KeyID)
        {
            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    UsingKeyItemPrivate(Entrys[i], KeyID);
                }
            }
            else
            {
                UsingKeyItemPrivate(EntryNo, KeyID);
            }
        }

        /// <summary>
        /// 貴重品の消費(プライベート)
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public static void UsingKeyItemPrivate(int EntryNo, int KeyID)
        {
            LibPlayer ch = SelectChara(EntryNo);
            ch.KeyItemNoRemoveKeyItem(KeyID);
        }

        /// <summary>
        /// パーティ人数による名称選択
        /// </summary>
        /// <param name="SoloName"></param>
        /// <param name="PartyName"></param>
        /// <returns></returns>
        public static string GetPlayerName(string SoloName, string PartyName)
        {
            string Name = "";
            string CharaName = "";
            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);

                if (Entrys.Length == 1)
                {
                    Name = SoloName;
                    CharaName = chm.GetNickName(Entrys[0]);
                }
                else
                {
                    Name = PartyName;
                    CharaName = chm.GetNickName(Entrys[LibInteger.GetRand(Entrys.Length)]);
                }
            }
            else
            {
                Name = SoloName;
                CharaName = chm.GetNickName(EntryNo);
            }

            return Name.Replace("name", CharaName);
        }

        /// <summary>
        /// パーティ人数による名称選択(性別判定付)
        /// </summary>
        /// <param name="SoloMaleName"></param>
        /// <param name="SoloFemaleName"></param>
        /// <param name="PartyMaleName"></param>
        /// <param name="PartyFemaleName"></param>
        /// <returns></returns>
        public static string GetPlayerNameWithSex(string SoloMaleName, string SoloFemaleName, string PartyMaleName, string PartyFemaleName)
        {
            string Name = "";
            string CharaName = "";
            int TargetEntryNo = 0;
            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);

                if (Entrys.Length == 1)
                {
                    TargetEntryNo = Entrys[0];
                    if (chm.GetSex(TargetEntryNo) == Status.Sex.Female)
                    {
                        Name = SoloFemaleName;
                    }
                    else
                    {
                        Name = SoloMaleName;
                    }
                    CharaName = chm.GetNickName(TargetEntryNo);
                }
                else
                {
                    TargetEntryNo = Entrys[LibInteger.GetRand(Entrys.Length)];
                    if (chm.GetSex(TargetEntryNo) == Status.Sex.Female)
                    {
                        Name = PartyFemaleName;
                    }
                    else
                    {
                        Name = PartyMaleName;
                    }
                    CharaName = chm.GetNickName(TargetEntryNo);
                }
            }
            else
            {
                if (chm.GetSex(EntryNo) == Status.Sex.Female)
                {
                    Name = SoloFemaleName;
                }
                else
                {
                    Name = SoloMaleName;
                }
                CharaName = chm.GetNickName(EntryNo);
            }

            return Name.Replace("name", CharaName);
        }

        /// <summary>
        /// アイテム入手
        /// </summary>
        /// <param name="ItemID">アイテムID</param>
        /// <param name="ItemCount">アイテム個数</param>
        /// <param name="WithMessage">入手メッセージ有無</param>
        /// <returns>メッセージ</returns>
        public static string Item(int ItemID, int ItemCount, bool WithMessage)
        {
            StringBuilder Msg = new StringBuilder();

            string OverOneBox = "";
            if (ItemCount > 1)
            {
                OverOneBox = "[" + ItemCount + "個]";
            }

            if (PartyNo > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (ItemPrivate(Entrys[i], ItemID, ItemCount))
                    {
                        GetCount++;
                    }
                    else if (WithMessage)
                    {
                        //表示
                        Msg.Append(chm.GetNickName(Entrys[i]) + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + OverOneBox + "を持てなかった為、諦めた……。<br />");
                    }
                }

                if (GetCount > 0)
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append(LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + OverOneBox + "を手に入れた。<br />");
                    }
                }
            }
            else
            {
                if (ItemPrivate(EntryNo, ItemID, ItemCount))
                {
                    if (WithMessage)
                    {
                        //表示
                        Msg.Append(LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + OverOneBox + "を手に入れた。<br />");
                    }
                }
                else if (WithMessage)
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false)) + OverOneBox + "を持てなかった為、諦めた……。<br />");
                }
            }

            return Msg.ToString();
        }

        /// <summary>
        /// アイテムの入手実態
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="ItemID"></param>
        /// <param name="ItemCount"></param>
        /// <returns></returns>
        public static bool ItemPrivate(int EntryNo, int ItemID, int ItemCount)
        {
            LibPlayer Chara = SelectChara(EntryNo);

            int refItemNo = 0;

            bool ReturnFlag = Chara.AddItem(Status.ItemBox.Normal, ItemID, false, ref ItemCount, ref refItemNo);

            if (!ReturnFlag)
            {
                // ボックスに保存
                Chara.AddItem(Status.ItemBox.Box, ItemID, false, ref ItemCount, ref refItemNo);
            }

            return ReturnFlag;
        }

        /// <summary>
        /// 体力魔力全快
        /// </summary>
        public static string RepairAll(bool WithMessage)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    RepairAllPrivate(Entrys[i]);
                }
                if (WithMessage)
                {
                    //表示
                    Msg.Append("HPとMPが全回復した！<br />");
                }
            }
            else
            {
                RepairAllPrivate(EntryNo);

                if (WithMessage)
                {
                    //表示
                    Msg.Append("HPとMPが全回復した！<br />");
                }
            }

            return Msg.ToString();
        }

        /// <summary>
        /// 体力魔力全快(プライベート)
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <returns></returns>
        public static void RepairAllPrivate(int EntryNo)
        {
            LibPlayer ch = SelectChara(EntryNo);
            ch.HPNow = ch.HPMax;
            ch.MPNow = ch.MPMax;
        }

        /// <summary>
        /// BCの消費
        /// </summary>
        /// <param name="BlazeChipCount"></param>
        public static void UsingBC(int BlazeChipCount)
        {
            if (PartyNo > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNo);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    UsingBCPrivate(Entrys[i], BlazeChipCount);
                }
            }
            else
            {
                UsingBCPrivate(EntryNo, BlazeChipCount);
            }
        }

        /// <summary>
        /// 貴重品の消費(プライベート)
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        public static void UsingBCPrivate(int EntryNo, int BlazeChipCount)
        {
            LibPlayer ch = SelectChara(EntryNo);
            ch.BlazeChip -= BlazeChipCount;
        }

        /// <summary>
        /// キャラ名称取得
        /// </summary>
        /// <param name="CharaID">キャラID</param>
        /// <returns>名称</returns>
        public static string ByCharaName(int CharaID)
        {
            LibPlayer ch = SelectChara(CharaID);
            return ch.NickName;
        }
    }
}
