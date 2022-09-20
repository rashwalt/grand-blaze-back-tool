using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;
using System.IO;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace CommonLibrary
{
    public class LibLuaExec
    {
        private Lua lua = new Lua();
        private LibUnitBaseMini chm = new LibUnitBaseMini();
        private LibBattleResult btresult = new LibBattleResult();
        private LibContinue con = new LibContinue();
        public StringBuilder WriteString = new StringBuilder();
        public int PartyNos = 0;
        public int EntryNos = 0;
        public int MarkIDs = 0;
        private bool IsMoving = false;

        private List<LibPlayer> CharacterList;

        // TODO: Effect=3040＝盗賊技能　ランクが２なら、盗賊技能＋１として扱う。イベント処理。

        // バトル設定に、戦闘勝利時にオンになるイベントフラグの名称を設定する。valueは戦闘結果により変化 1:勝利 2:敗北 3:タイムオーバー

        public LibLuaExec(List<LibPlayer> CharactersDataList)
        {
            CharacterList = CharactersDataList;

            lua = new Lua();
            lua.RegisterFunction("ptmembers", this, this.GetType().GetMethod("PartyMemberCount", new Type[] { }));
            lua.RegisterFunction("getptmember", this, this.GetType().GetMethod("GetPartyMemberID", new Type[] { typeof(int) }));

            lua.RegisterFunction("getitemmsg", this, this.GetType().GetMethod("GetItemMsg", new Type[] { typeof(int), typeof(int) }));
            lua.RegisterFunction("getitem", this, this.GetType().GetMethod("GetItem", new Type[] { typeof(int), typeof(int), typeof(int) }));
            lua.RegisterFunction("getmoneymsg", this, this.GetType().GetMethod("GetMoneyMsg", new Type[] { typeof(int) }));
            lua.RegisterFunction("getmoneys", this, this.GetType().GetMethod("GetMoney", new Type[] { typeof(int), typeof(int) }));

            lua.RegisterFunction("orgup", this, this.GetType().GetMethod("UpOrganization", new Type[] { typeof(int), typeof(int), typeof(int) }));

            lua.RegisterFunction("checkhaveitem", this, this.GetType().GetMethod("CheckHaveItems", new Type[] { typeof(int), typeof(int), typeof(int) }));
            lua.RegisterFunction("checkhavekey", this, this.GetType().GetMethod("CheckHaveKeys", new Type[] { typeof(int), typeof(int) }));
            lua.RegisterFunction("checkinstallclass", this, this.GetType().GetMethod("CheckInstallClass2", new Type[] { typeof(int), typeof(int) }));
            lua.RegisterFunction("checkcontinue", this, this.GetType().GetMethod("CheckContinue", new Type[] { typeof(int) }));
            lua.RegisterFunction("checkequipitems", this, this.GetType().GetMethod("CheckEquipItems", new Type[] { typeof(int), typeof(int), typeof(int) }));
            lua.RegisterFunction("checkoffer", this, this.GetType().GetMethod("CheckOffer", new Type[] { typeof(int), typeof(int) }));
            lua.RegisterFunction("checkcomp", this, this.GetType().GetMethod("CheckComp", new Type[] { typeof(int), typeof(int) }));
            lua.RegisterFunction("checkfame", this, this.GetType().GetMethod("CheckFame", new Type[] { typeof(int), typeof(string), typeof(int) }));
            lua.RegisterFunction("checksex", this, this.GetType().GetMethod("CheckSex", new Type[] { typeof(int), typeof(int) }));

            lua.RegisterFunction("itemgetmsg", this, this.GetType().GetMethod("ItemGetWithMsg", new Type[] { typeof(int), typeof(int) }));
            lua.RegisterFunction("moneygetmsg", this, this.GetType().GetMethod("MoneyGetWithMsg", new Type[] { typeof(int) }));
            lua.RegisterFunction("keyitemgetmsg", this, this.GetType().GetMethod("KeyItemGetWithMsg", new Type[] { typeof(int) }));
            lua.RegisterFunction("installgetmsg", this, this.GetType().GetMethod("InstallGetWithMsg", new Type[] { typeof(int) }));
            lua.RegisterFunction("perksgetmsg", this, this.GetType().GetMethod("SkillGetWithMsg", new Type[] { typeof(int) }));
            lua.RegisterFunction("bagupmsg", this, this.GetType().GetMethod("BagUpWithMsg", new Type[] { typeof(int) }));
            lua.RegisterFunction("writeline", this, this.GetType().GetMethod("WriteLine", new Type[] { typeof(string) }));
            lua.RegisterFunction("monstername", this, this.GetType().GetMethod("GetMonsterName", new Type[] { typeof(int) }));
            lua.RegisterFunction("keyitemname", this, this.GetType().GetMethod("GetKeyItemName", new Type[] { typeof(int) }));
            lua.RegisterFunction("itemname", this, this.GetType().GetMethod("GetItemName", new Type[] { typeof(int) }));
            lua.RegisterFunction("questoffer", this, this.GetType().GetMethod("QuestOffer", new Type[] { typeof(int) }));
            lua.RegisterFunction("questcomp", this, this.GetType().GetMethod("QuestComplete", new Type[] { typeof(int) }));
            lua.RegisterFunction("checkblazechip", this, this.GetType().GetMethod("CheckBlazeChip", new Type[] { typeof(int) }));
            lua.RegisterFunction("checkquestoffer", this, this.GetType().GetMethod("CheckQuestOffer", new Type[] { typeof(int) }));
            lua.RegisterFunction("checkquestcomp", this, this.GetType().GetMethod("CheckQuestComp", new Type[] { typeof(int) }));
            lua.RegisterFunction("checkitem", this, this.GetType().GetMethod("CheckHaveItem", new Type[] { typeof(int), typeof(int) }));
            lua.RegisterFunction("checkkeyitem", this, this.GetType().GetMethod("CheckKeyItem", new Type[] { typeof(int) }));
            lua.RegisterFunction("checkkeyitemfull", this, this.GetType().GetMethod("CheckKeyItemFull", new Type[] { typeof(int) }));
            lua.RegisterFunction("checkinstall", this, this.GetType().GetMethod("CheckInstallClass", new Type[] { typeof(int) }));
            lua.RegisterFunction("checkequipitem", this, this.GetType().GetMethod("CheckEquipItem", new Type[] { typeof(int), typeof(int) }));
            lua.RegisterFunction("checkfames", this, this.GetType().GetMethod("CheckFames", new Type[] { typeof(string), typeof(int) }));
            lua.RegisterFunction("checkorg", this, this.GetType().GetMethod("CheckOrg", new Type[] { typeof(int) }));
            lua.RegisterFunction("tarent", this, this.GetType().GetMethod("TarentGet", new Type[] { typeof(int) }));
            lua.RegisterFunction("battle", this, this.GetType().GetMethod("BattleStart", new Type[] { typeof(string), typeof(string), typeof(int), typeof(int), typeof(int), typeof(int) }));
            lua.RegisterFunction("battleresult", this, this.GetType().GetMethod("BattleResults", new Type[] { }));
            lua.RegisterFunction("useitem", this, this.GetType().GetMethod("UsingItem", new Type[] { typeof(int), typeof(int) }));
            lua.RegisterFunction("useblazechip", this, this.GetType().GetMethod("UsingBlazeChip", new Type[] { typeof(int) }));
            lua.RegisterFunction("usekeyitem", this, this.GetType().GetMethod("UsingKeyItem", new Type[] { typeof(int) }));
            lua.RegisterFunction("usemoney", this, this.GetType().GetMethod("UsingMoney", new Type[] { typeof(int) }));
            lua.RegisterFunction("eventflag", this, this.GetType().GetMethod("EventFlag", new Type[] { typeof(int), typeof(int), typeof(bool) }));
            lua.RegisterFunction("move", this, this.GetType().GetMethod("MoveMark", new Type[] { typeof(int) }));
            lua.RegisterFunction("markname", this, this.GetType().GetMethod("GetMarkName", new Type[] { typeof(int) }));
            lua.RegisterFunction("mark", this, this.GetType().GetMethod("GetMarkOnlyName", new Type[] { typeof(int) }));
            lua.RegisterFunction("area", this, this.GetType().GetMethod("GetAreaName", new Type[] { typeof(int) }));
            lua.RegisterFunction("getlevel", this, this.GetType().GetMethod("GetLevel", new Type[] { }));
            lua.RegisterFunction("getlevelunder", this, this.GetType().GetMethod("GetLevelUnder", new Type[] { }));
            lua.RegisterFunction("getmoney", this, this.GetType().GetMethod("GetNowMoney", new Type[] { }));
            lua.RegisterFunction("getmoneyunder", this, this.GetType().GetMethod("GetMoneyUnder", new Type[] { }));
            lua.RegisterFunction("repair", this, this.GetType().GetMethod("Repair", new Type[] { }));
            lua.RegisterFunction("addstatus", this, this.GetType().GetMethod("StatusAdd", new Type[] { typeof(int), typeof(int), typeof(bool) }));

            lua.RegisterFunction("nowareatype", this, this.GetType().GetMethod("NowAreaType", new Type[] { }));

            lua.RegisterFunction("checkcompanion", this, this.GetType().GetMethod("CheckCompanion", new Type[] { }));
            lua.RegisterFunction("getcompanion", this, this.GetType().GetMethod("GetCompanion", new Type[] { typeof(int) }));
            lua.RegisterFunction("companionname", this, this.GetType().GetMethod("GetCompanionName", new Type[] { }));
            lua.RegisterFunction("companionnameentry", this, this.GetType().GetMethod("GetCompanionNameByEntry", new Type[] { typeof(int) }));

            // 第一引数：name はプレイヤー一人の名前（愛称）に変換。ソロ、またはプライベート時の場合の言い方
            // 第二引数：name はプレイヤー一人の名前（愛称）に変換。パーティ相手の場合
            lua.RegisterFunction("player", this, this.GetType().GetMethod("GetPlayerName", new Type[] { typeof(string), typeof(string) }));
            lua.RegisterFunction("playerex", this, this.GetType().GetMethod("GetPlayerNameWithSex", new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) }));
            lua.RegisterFunction("partyname", this, this.GetType().GetMethod("GetPartyName", new Type[] { }));

            // フィールドテクニック判定＆テクニックアップ！
            // テクニック上昇の数値を出す場合には、イベント後にまとめて出す方式にする
            lua.RegisterFunction("checktec", this, this.GetType().GetMethod("CheckTec", new Type[] { typeof(string), typeof(int) }));
            lua.RegisterFunction("gettecplayer", this, this.GetType().GetMethod("GetTecPlayerName", new Type[] { }));

            // ウェディングサポート用
            lua.RegisterFunction("groom", this, this.GetType().GetMethod("GetGroomName", new Type[] { }));// 新郎名
            lua.RegisterFunction("bride", this, this.GetType().GetMethod("GetBrideName", new Type[] { }));// 新婦名
            lua.RegisterFunction("groomno", this, this.GetType().GetMethod("GetGroomNo", new Type[] { }));// 新郎No
            lua.RegisterFunction("brideno", this, this.GetType().GetMethod("GetBrideNo", new Type[] { }));// 新婦No

            lua.RegisterFunction("getrand", this, this.GetType().GetMethod("GetRand", new Type[] { typeof(int) }));// 乱数

            // MEMO: イベントフラグは直接luaスクリプトで比較して結果を確定させる。
        }

        private LibPlayer SelectChara(int EntryNo)
        {
            if (CharacterList == null)
            {
                throw new Exception("キャラクター参照不可の部分で固有コマンドが実行されています。");
            }

            return CharacterList.Find(chs => chs.EntryNo == EntryNo);
        }

        public long GetRand(int Max)
        {
            return LibInteger.GetRand(Max);
        }

        public long GetValueAsLong(string Expr)
        {
            object[] res = lua.DoString("return " + Expr);

            return (long)(double)res[0];
        }

        public long GetValueAsLongNoZero(string Expr)
        {
            object[] res = lua.DoString("return " + Expr);

            long num = (long)(double)res[0];

            if (num < 0) { num = 0; }

            return num;
        }

        public double GetValueAsDouble(string Expr)
        {
            object[] res = lua.DoString("return " + Expr);

            return (double)res[0];
        }

        public void SetVariable(string Key, object Value)
        {
            lua[Key] = Value;
        }

        public object GetVariable(string Key)
        {
            return lua[Key];
        }

        public void Exec(string Expr)
        {
            lua.DoString(Expr + "\r\n");
        }

        public void DoFile(string file)
        {
            lua.DoFile(file);
        }

        public void Init()
        {
            WriteString = new StringBuilder();
        }

        public void EventExec(int PartyNo, int EntryNo, int EventID, ref string Output, int MarkID, ref bool IsMovingArea)
        {
            PartyNos = PartyNo;
            EntryNos = EntryNo;
            MarkIDs = MarkID;
            IsMoving = IsMovingArea;

            SetVariable("PartyNo", PartyNo);
            SetVariable("EntryNo", EntryNo);
            SetVariable("MarkID", MarkID);
            SetVariable("ResultCount", LibCommonLibrarySettings.UpdateCnt);

            string file = LibCommonLibrarySettings.EventScriptDir + "event_" + EventID.ToString("000000") + ".lua";

            lua.DoFile(file);

            Output = WriteString.ToString();
            IsMovingArea = IsMoving;
        }

        public void UseEventExec(int EntryNo, int ItemID, ref string Output, int MarkID)
        {
            PartyNos = 0;
            EntryNos = EntryNo;
            MarkIDs = MarkID;

            SetVariable("PartyNo", 0);
            SetVariable("EntryNo", EntryNo);
            SetVariable("MarkID", MarkID);
            SetVariable("ResultCount", LibCommonLibrarySettings.UpdateCnt);

            string file = LibCommonLibrarySettings.EventScriptDir + "itemevent_" + ItemID.ToString("000000") + ".lua";

            if (File.Exists(file))
            {
                lua.DoFile(file);

                Output = WriteString.ToString();
            }
            else
            {
                Output = "";
            }
        }

        private void StringWite(string Text)
        {
            if (WriteString.Length > 0 && Text.Length > 0)
            {
                WriteString.AppendLine("<hr class=\"event_hr\" />");
            }
            WriteString.AppendLine(Text);
        }

        public bool MarkCheckExec(int PartyNo, int EntryNo, int MarkID)
        {
            if (MarkID == -1) { return true; }

            WriteString = new StringBuilder();
            PartyNos = PartyNo;
            EntryNos = EntryNo;

            SetVariable("PartyNo", PartyNo);
            SetVariable("EntryNo", EntryNo);
            SetVariable("markmove", true);// 移動できない場合は markmove=falseに

            string file = LibCommonLibrarySettings.EventScriptDir + "mark_" + MarkID.ToString("000000") + ".lua";

            if (File.Exists(file))
            {
                lua.DoFile(file);

                return (bool)GetVariable("markmove");
            }
            else
            {
                return true;
            }
        }

        // ---------------------------
        // スクリプトアクセス用関数群
        // ---------------------------

        /// <summary>
        /// アイテム入手メッセージ
        /// </summary>
        /// <param name="ItemNo"></param>
        /// <param name="ItemCount"></param>
        /// <returns></returns>
        public string GetItemMsg(int ItemNo, int ItemCount)
        {
            StringBuilder Msg = new StringBuilder();

            string OverOneBox = "";
            if (ItemCount > 1)
            {
                OverOneBox = "[" + ItemCount + "個]";
            }

            Msg.Append(LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemNo, false)) + OverOneBox + "を手に入れた。<br />");

            return Msg.ToString();
        }

        /// <summary>
        /// アイテム入手
        /// </summary>
        /// <param name="EntryNo"></param>
        /// <param name="ItemNo"></param>
        /// <param name="ItemCount"></param>
        public void GetItem(int EntryNo, int ItemNo, int ItemCount)
        {
            LibPlayer ch = SelectChara(EntryNo);

            int refItemNo = 0;

            bool ReturnFlag = ch.AddItem(Status.ItemBox.Normal, ItemNo, false, ref ItemCount, ref refItemNo);

            if (!ReturnFlag)
            {
                // ボックスに保存
                ch.AddItem(Status.ItemBox.Box, ItemNo, false, ref ItemCount, ref refItemNo);
            }

            //ch.UpdateHaveItemData();
        }
        

        public string ItemGetWithMsg(int ItemNo, int ItemCount)
        {
            StringBuilder Msg = new StringBuilder();

            string OverOneBox = "";
            if (ItemCount > 1)
            {
                OverOneBox = "[" + ItemCount + "個]";
            }

            if (PartyNos > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_ItemGetWithMsg(Entrys[i], ItemNo, ItemCount))
                    {
                        GetCount++;
                    }
                    else
                    {
                        //表示
                        Msg.Append(chm.GetNickName(Entrys[i]) + "は" + LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemNo, false)) + OverOneBox + "を持てなかった為、諦めた……。<br />");
                    }
                }

                if (GetCount > 0)
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemNo, false)) + OverOneBox + "を手に入れた。<br />");
                }
            }
            else
            {
                if (_ItemGetWithMsg(EntryNos, ItemNo, ItemCount))
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemNo, false)) + OverOneBox + "を手に入れた。<br />");
                }
                else
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemNo, false)) + OverOneBox + "を持てなかった為、諦めた……。<br />");
                }
            }

            return Msg.ToString();
        }

        private bool _ItemGetWithMsg(int EntryNo, int ItemNo, int ItemCount)
        {
            LibPlayer ch = SelectChara(EntryNo);

            int refItemNo = 0;

            bool ReturnFlag = ch.AddItem(Status.ItemBox.Normal, ItemNo, false, ref ItemCount, ref refItemNo);

            if (!ReturnFlag)
            {
                // ボックスに保存
                ch.AddItem(Status.ItemBox.Box, ItemNo, false, ref ItemCount, ref refItemNo);
            }

            //ch.UpdateHaveItemData();

            return ReturnFlag;
        }

        public void ItemGet(int ItemNo, int ItemCount)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    _ItemGetWithMsg(Entrys[i], ItemNo, ItemCount);
                }
            }
            else
            {
                _ItemGetWithMsg(EntryNos, ItemNo, ItemCount);
            }
        }

        public void UpOrganization(int EntryNo, int OrgID, int OrgClassExp)
        {
            LibPlayer ch = SelectChara(EntryNo);

            int status = ch.SetOrgExp(OrgID);
        }

        public string KeyItemGetWithMsg(int ItemNo)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNos > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_KeyItemGetWithMsg(Entrys[i], ItemNo))
                    {
                        GetCount++;
                    }
                }

                if (GetCount > 0)
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeKeyItem(LibKeyItem.GetKeyItemName(ItemNo)) + "を手に入れた。<br />");
                }
            }
            else
            {
                if (_KeyItemGetWithMsg(EntryNos, ItemNo))
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeKeyItem(LibKeyItem.GetKeyItemName(ItemNo)) + "を手に入れた。<br />");
                }
            }

            return Msg.ToString();
        }

        private bool _KeyItemGetWithMsg(int EntryNo, int ItemNo)
        {
            LibPlayer ch = SelectChara(EntryNo);

            bool ReturnFlag = ch.AddKeyItem(ItemNo);

            //ch.UpdateKeyItemData();

            return ReturnFlag;
        }

        public string GetMoneyMsg(int Money)
        {
            StringBuilder Msg = new StringBuilder();

            Msg.Append(LibResultText.CSSEscapeMoney(Money, false) + "を手に入れた。<br />");

            return Msg.ToString();
        }

        public void GetMoney(int EntryNo, int Money)
        {
            LibPlayer ch = SelectChara(EntryNo);

            ch.HaveMoney += Money;

            //ch.UpdatePersonalData();
        }

        public string MoneyGetWithMsg(int Money)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNos > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_MoneyGetWithMsg(Entrys[i], Money))
                    {
                        GetCount++;
                    }
                }

                if (GetCount > 0)
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeMoney(Money, false) + "を手に入れた。<br />");
                }
            }
            else
            {
                if (_MoneyGetWithMsg(EntryNos, Money))
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeMoney(Money, false) + "を手に入れた。<br />");
                }
            }

            return Msg.ToString();
        }

        private bool _MoneyGetWithMsg(int EntryNo, int Money)
        {
            LibPlayer ch = SelectChara(EntryNo);

            ch.HaveMoney += Money;

            //ch.UpdatePersonalData();

            return true;
        }

        public string InstallGetWithMsg(int InstallNo)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNos > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_InstallGetWithMsg(Entrys[i], InstallNo))
                    {
                        GetCount++;
                    }
                }

                if (GetCount > 0)
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeInstallClass(LibInstall.GetInstallName(InstallNo)) + "を手に入れた。<br />");
                }
            }
            else
            {
                if (_InstallGetWithMsg(EntryNos, InstallNo))
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeInstallClass(LibInstall.GetInstallName(InstallNo)) + "を手に入れた。<br />");
                }
            }

            return Msg.ToString();
        }

        private bool _InstallGetWithMsg(int EntryNo, int InstallNo)
        {
            LibPlayer ch = SelectChara(EntryNo);

            ch.AddInstallClass(InstallNo);

            //ch.UpdateInstallClassData();

            return true;
        }

        public string SkillGetWithMsg(int SkillID)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNos > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_SkillGetWithMsg(Entrys[i], SkillID))
                    {
                        GetCount++;
                    }
                }

                if (GetCount > 0)
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeSkill(LibSkill.GetSkillName(SkillID, false)) + "を覚えた。<br />");
                }
            }
            else
            {
                if (_SkillGetWithMsg(EntryNos, SkillID))
                {
                    //表示
                    Msg.Append(LibResultText.CSSEscapeSkill(LibSkill.GetSkillName(SkillID, false)) + "を覚えた。<br />");
                }
            }

            return Msg.ToString();
        }

        private bool _SkillGetWithMsg(int EntryNo, int SkillID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            bool IsGet = ch.AddSkill(SkillID, false, false);

            //ch.UpdateHaveSkillData();

            return IsGet;
        }

        public string BagUpWithMsg(int BagupCount)
        {
            StringBuilder Msg = new StringBuilder();

            if (PartyNos > 0)
            {
                int GetCount = 0;
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_BagUpWithMsg(Entrys[i], BagupCount))
                    {
                        GetCount++;
                    }
                }

                if (GetCount > 0)
                {
                    //表示
                    Msg.Append("カバンの容量が最大" + BagupCount + "個に増えた。<br />");


                }
            }
            else
            {
                if (_BagUpWithMsg(EntryNos, BagupCount))
                {
                    //表示
                    Msg.Append("カバンの容量が最大" + BagupCount + "個に増えた。<br />");
                }
            }

            return Msg.ToString();
        }

        private bool _BagUpWithMsg(int EntryNo, int BagupCount)
        {
            LibPlayer ch = SelectChara(EntryNo);

            if (ch.MaxHaveItem < BagupCount)
            {
                ch.MaxHaveItem = BagupCount;
                //ch.UpdatePersonalData();

                return true;
            }

            return false;
        }

        public void QuestOffer(int QuestID)
        {
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    _QuestOffer(Entrys[i], QuestID);
                }
            }
            else
            {
                _QuestOffer(EntryNos, QuestID);
            }
        }

        private void _QuestOffer(int EntryNo, int QuestID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            ch.OfferQuest(QuestID);
        }

        public void QuestComplete(int QuestID)
        {
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    _QuestComplete(Entrys[i], QuestID);
                }
            }
            else
            {
                _QuestComplete(EntryNos, QuestID);
            }
        }

        private void _QuestComplete(int EntryNo, int QuestID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            ch.CompleteQuest(QuestID);
        }

        public void MessageShow(string Msg)
        {
            System.Windows.Forms.MessageBox.Show(Msg);
        }

        public void WriteLine(string Text)
        {
            Text = Text.Replace("\r\n", "<br />");
            Text = Text.Replace("\r", "<br />");
            Text = Text.Replace("\n", "<br />");
            StringWite(Text + "<br />\n");
        }

        public int PartyMemberCount()
        {
            if (PartyNos > 0)
            {
                return LibParty.PartyMemberNo(PartyNos).Length;
            }
            else
            {
                return LibParty.PartyMemberNo(LibParty.GetPartyNo(EntryNos)).Length;
            }
        }

        public int GetPartyMemberID(int Cnt)
        {
            return LibParty.PartyMemberNo(PartyNos)[Cnt-1];
        }

        public string GetMonsterName(int MonsterID)
        {
            return LibMonsterData.GetNickName(MonsterID);
        }

        public string GetKeyItemName(int KeyItemID)
        {
            return LibResultText.CSSEscapeKeyItem(LibKeyItem.GetKeyItemName(KeyItemID));
        }

        public string GetItemName(int ItemID)
        {
            return LibResultText.CSSEscapeItem(LibItem.GetItemName(ItemID, false));
        }

        public string GetGroomName()
        {
            return LibParty.GetGroomName(PartyNos);
        }

        public string GetBrideName()
        {
            return LibParty.GetBrideName(PartyNos);
        }

        public int GetGroomNo()
        {
            return LibParty.GetGroomNo(PartyNos);
        }

        public int GetBrideNo()
        {
            return LibParty.GetBrideNo(PartyNos);
        }

        public void TarentGet(int TarentID)
        {
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    _TarentGet(Entrys[i], TarentID);
                }
            }
            else
            {
                _TarentGet(EntryNos, TarentID);
            }
        }

        private void _TarentGet(int EntryNo, int TarentID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            ch.SetTarent(TarentID);

            //ch.UpdatePersonalData();
            //ch.UpdateTarentData();
        }

        public void BattleStart(string PopMonsters, string PopNonplayers, int Style, int WinStyle, int LoseStyle, int CofferID)
        {
            LibPartyBattleSet bt = new LibPartyBattleSet();
            bt.Update(PartyNos, PopMonsters, PopNonplayers, Style, WinStyle, LoseStyle, MarkIDs, CofferID);
        }

        public bool CheckQuestOffer(int QuestID)
        {
            bool Ok = false;
            if (PartyNos > 0)
            {                
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_CheckQuestOffer(Entrys[i], QuestID))
                    {
                        Ok = true;
                    }
                }
            }
            else
            {
                return _CheckQuestOffer(EntryNos, QuestID);
            }

            return Ok;
        }

        private bool _CheckQuestOffer(int EntryNo, int QuestID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckQuest(QuestID);
        }

        public bool CheckOffer(int EntryNo, int QuestID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckQuest(QuestID);
        }

        public bool CheckQuestComp(int QuestID)
        {
            bool Ok = true;
            if (PartyNos > 0)
            {                
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (!_CheckQuestComp(Entrys[i], QuestID))
                    {
                        Ok = false;
                    }
                }
            }
            else
            {
                return _CheckQuestComp(EntryNos, QuestID);
            }

            return Ok;
        }

        private bool _CheckQuestComp(int EntryNo, int QuestID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckQuestComp(QuestID);
        }

        public bool CheckComp(int EntryNo, int QuestID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckQuestComp(QuestID);
        }

        public bool CheckBlazeChip(int BlazeChipCount)
        {
            bool Ok = true;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (!_CheckBlazeChip(Entrys[i], BlazeChipCount))
                    {
                        Ok = false;
                    }
                }
            }
            else
            {
                return _CheckBlazeChip(EntryNos, BlazeChipCount);
            }

            return Ok;
        }

        private bool _CheckBlazeChip(int EntryNo, int BlazeChipCount)
        {
            LibPlayer ch = SelectChara(EntryNo);

            if (ch.BlazeChip >= BlazeChipCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckHaveItem(int ItemID, int ItemCount)
        {
            bool Ok = false;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_CheckHaveItem(Entrys[i], ItemID, ItemCount))
                    {
                        Ok = true;
                    }
                }
            }
            else
            {
                return _CheckHaveItem(EntryNos, ItemID, ItemCount);
            }

            return Ok;
        }

        private bool _CheckHaveItem(int EntryNo, int ItemID, int ItemCount)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckHaveItem(Status.ItemBox.Normal, ItemID, false, ItemCount);
        }

        public bool CheckHaveItems(int EntryNo, int ItemID, int ItemCount)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckHaveItem(Status.ItemBox.Normal, ItemID, false, ItemCount);
        }

        public bool CheckKeyItem(int ItemID)
        {
            bool Ok = false;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_CheckKeyItem(Entrys[i], ItemID))
                    {
                        Ok = true;
                    }
                }
            }
            else
            {
                return _CheckKeyItem(EntryNos, ItemID);
            }

            return Ok;
        }

        public bool CheckKeyItemFull(int ItemID)
        {
            bool Ok = true;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (!_CheckKeyItem(Entrys[i], ItemID))
                    {
                        Ok = false;
                    }
                }
            }
            else
            {
                return _CheckKeyItem(EntryNos, ItemID);
            }

            return Ok;
        }

        private bool _CheckKeyItem(int EntryNo, int ItemID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckKeyItem(ItemID);
        }

        public bool CheckHaveKeys(int EntryNo, int KeyItemID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckKeyItem(KeyItemID);
        }

        public bool CheckInstallClass(int InstallID)
        {
            bool Ok = false;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_CheckInstallClass(Entrys[i], InstallID))
                    {
                        Ok = true;
                    }
                }
            }
            else
            {
                return _CheckInstallClass(EntryNos, InstallID);
            }

            return Ok;
        }

        private bool _CheckInstallClass(int EntryNo, int InstallID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckHaveInstall(InstallID);
        }

        public bool CheckInstallClass2(int EntryNo, int InstallID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckHaveInstall(InstallID);
        }

        public bool CheckOrg(int OrgID)
        {
            bool Ok = false;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_CheckOrg(Entrys[i], OrgID))
                    {
                        Ok = true;
                    }
                }
            }
            else
            {
                return _CheckOrg(EntryNos, OrgID);
            }

            return Ok;
        }

        private bool _CheckOrg(int EntryNo, int OrgID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return (ch.GetOrgClass(OrgID) >= 0);
        }

        public bool CheckEquipItem(int EquipID, int ItemID)
        {
            bool Ok = false;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_CheckEquipItem(Entrys[i], EquipID, ItemID))
                    {
                        Ok = true;
                    }
                }
            }
            else
            {
                return _CheckEquipItem(EntryNos, EquipID, ItemID);
            }

            return Ok;
        }

        public bool CheckEquipItems(int EntryNo, int EquipID, int ItemID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckEquipItem(EquipID, ItemID);
        }

        private bool _CheckEquipItem(int EntryNo, int EquipID, int ItemID)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.CheckEquipItem(EquipID, ItemID);
        }

        public bool CheckFames(string FameType, int FameCount)
        {
            bool Ok = false;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_CheckFames(Entrys[i], FameType, FameCount))
                    {
                        Ok = true;
                    }
                }
            }
            else
            {
                return _CheckFames(EntryNos, FameType, FameCount);
            }

            return Ok;
        }

        public bool CheckFame(int EntryNo, string FameType, int FameCount)
        {
            LibPlayer ch = SelectChara(EntryNo);

            switch (FameType)
            {
                case "名声":
                    return (ch.FameGood >= FameCount);
                case "悪名":
                    return (ch.FameBad >= FameCount);
            }

            return false;
        }

        public bool CheckSex(int EntryNo, int SexType)
        {
            LibPlayer ch = SelectChara(EntryNo);

            if (ch.Sex == SexType)
            {
                return true;
            }

            return false;
        }

        private bool _CheckFames(int EntryNo, string FameType, int FameCount)
        {
            LibPlayer ch = SelectChara(EntryNo);

            switch (FameType)
            {
                case "名声":
                    return (ch.FameGood >= FameCount);
                case "悪名":
                    return (ch.FameBad >= FameCount);
            }

            return false;
        }

        public bool CheckContinue(int EntryNo)
        {
            ContinueDataEntity.ts_continue_mainRow ContinueMainRow = con.Entity.ts_continue_main.FindByentry_no(EntryNo);
            if (ContinueMainRow != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UsingItem(int ItemID, int ItemCount)
        {
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_CheckHaveItem(Entrys[i], ItemID, ItemCount))
                    {
                        bool Usings = _UsingItem(Entrys[i], ItemID, ItemCount);
                        if (Usings)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                _UsingItem(EntryNos, ItemID, ItemCount);
            }
        }

        private bool _UsingItem(int EntryNo, int ItemID, int ItemCount)
        {
            bool Usings = false;
            LibPlayer ch = SelectChara(EntryNo);
            Usings = ch.ItemNoRemoveItem(Status.ItemBox.Normal, ItemID, ItemCount);

            return Usings;
        }

        public void UsingKeyItem(int KeyItemID)
        {
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    _UsingKeyItem(Entrys[i], KeyItemID);
                }
            }
            else
            {
                _UsingKeyItem(EntryNos, KeyItemID);
            }
        }

        private void _UsingKeyItem(int EntryNo, int KeyItemID)
        {
            LibPlayer ch = SelectChara(EntryNo);
            ch.KeyItemNoRemoveKeyItem(KeyItemID);
        }

        public void UsingBlazeChip(int BlazeChipCount)
        {
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    _UsingBlazeChip(Entrys[i], BlazeChipCount);
                }
            }
            else
            {
                _UsingBlazeChip(EntryNos, BlazeChipCount);
            }
        }

        private void _UsingBlazeChip(int EntryNo, int BlazeChipCount)
        {
            LibPlayer ch = SelectChara(EntryNo);
            ch.BlazeChip -= BlazeChipCount;
        }

        public void UsingMoney(int MoneyCount)
        {
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    _UsingMoney(Entrys[i], MoneyCount);
                }
            }
            else
            {
                _UsingMoney(EntryNos, MoneyCount);
            }
        }

        private void _UsingMoney(int EntryNo, int MoneyCount)
        {
            LibPlayer ch = SelectChara(EntryNo);
            if (ch.HaveMoney < MoneyCount)
            {
                throw new Exception("所持金以上のお金が請求されました。");
            }

            ch.HaveMoney -= MoneyCount;
        }

        public void EventFlag(int FlagID, int FlagValue, bool Compulsion)
        {
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    _EventFlag(Entrys[i], FlagID, FlagValue, Compulsion);
                }
            }
            else
            {
                _EventFlag(EntryNos, FlagID, FlagValue, Compulsion);
            }
        }

        private void _EventFlag(int EntryNo, int FlagID, int FlagValue, bool Compulsion)
        {
            LibPlayer ch = SelectChara(EntryNo);
            ch.SetEventFlag(FlagID, FlagValue, Compulsion);
        }

        public void Repair()
        {
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    _Repair(Entrys[i]);
                }
            }
            else
            {
                _Repair(EntryNos);
            }
        }

        private void _Repair(int EntryNo)
        {
            LibPlayer ch = SelectChara(EntryNo);

            ch.HPNow = ch.HPMax;
            ch.MPNow = ch.MPMax;
        }

        private string CheckTecTargetName = "";

        public string GetTecPlayerName()
        {
            return CheckTecTargetName;
        }

        public bool CheckTec(string SkillName, int TecLimit)
        {
            bool Ok = false;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    if (_CheckTec(Entrys[i], SkillName, TecLimit))
                    {
                        Ok = true;
                    }
                }
            }
            else
            {
                return _CheckTec(EntryNos, SkillName, TecLimit);
            }

            return Ok;
        }

        private bool _CheckTec(int EntryNo, string SkillName, int TecLimit)
        {
            LibPlayer ch = SelectChara(EntryNo);

            int GetMaxRanks = TecLimit;
            string TecEnName = LibTechnique.GetTecNameEnByTecName(SkillName);
            int MineSkill = ch.TechniqueRow(TecEnName);

            int SearchProb = (int)((decimal)MineSkill / (decimal)GetMaxRanks * 100m);

            if (SearchProb < 5) { SearchProb = 5; }

            // 成功判定
            if (LibInteger.GetRandBasis() <= SearchProb)
            {
                // 失敗
                return false;
            }

            CheckTecTargetName = ch.NickName;

            return true;
        }

        public int BattleResults()
        {
            return btresult.GetResult(PartyNos, MarkIDs);
        }

        public void MoveMark(int MarkID)
        {
            LibParty.SetPartyAreaID(PartyNos, MarkID);
            LibParty.Update();

            if (LibArea.GetAreaID(MarkIDs) != LibArea.GetAreaID(MarkID))
            {
                IsMoving = true;
            }
        }

        public string GetMarkName(int MarkID)
        {
            return LibArea.GetAreaMarkName(MarkID);
        }

        public string GetMarkOnlyName(int MarkID)
        {
            return LibResultText.CSSEscapeMark(LibArea.GetMarkName(MarkID));
        }

        public string GetAreaName(int MarkID)
        {
            // 指定するのはマークID
            return LibResultText.CSSEscapeArea(LibArea.GetAreaName(MarkID));
        }

        public string GetPlayerName(string SoloName, string PartyName)
        {
            string Name = "";
            string CharaName = "";
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);

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
                CharaName = chm.GetNickName(EntryNos);
            }

            return Name.Replace("name", CharaName);
        }

        public string GetPlayerNameWithSex(string SoloMaleName, string SoloFemaleName, string PartyMaleName, string PartyFemaleName)
        {
            string Name = "";
            string CharaName = "";
            int TargetEntryNo = 0;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);

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
                if (chm.GetSex(EntryNos) == Status.Sex.Female)
                {
                    Name = SoloFemaleName;
                }
                else
                {
                    Name = SoloMaleName;
                }
                CharaName = chm.GetNickName(EntryNos);
            }

            return Name.Replace("name", CharaName);
        }

        public int GetLevel()
        {
            int MaxLevel = 0;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    int TargetLevel = _GetLevel(Entrys[i]);
                    MaxLevel = Math.Max(MaxLevel, TargetLevel);
                }

                return MaxLevel;
            }
            else
            {
                return _GetLevel(EntryNos);
            }
        }

        public int GetLevelUnder()
        {
            int MinLevel = int.MaxValue;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    int TargetLevel = _GetLevel(Entrys[i]);
                    MinLevel = Math.Min(MinLevel, TargetLevel);
                }

                return MinLevel;
            }
            else
            {
                return _GetLevel(EntryNos);
            }
        }

        private int _GetLevel(int EntryNo)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.Level;
        }

        public int GetNowMoney()
        {
            int MaxMoney = 0;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    int TargetMoney = _GetMoney(Entrys[i]);
                    MaxMoney = Math.Max(MaxMoney, TargetMoney);
                }

                return MaxMoney;
            }
            else
            {
                return _GetMoney(EntryNos);
            }
        }

        public int GetMoneyUnder()
        {
            int MaxMoney = int.MaxValue;
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    int TargetMoney = _GetMoney(Entrys[i]);
                    MaxMoney = Math.Min(MaxMoney, TargetMoney);
                }

                return MaxMoney;
            }
            else
            {
                return _GetMoney(EntryNos);
            }
        }

        private int _GetMoney(int EntryNo)
        {
            LibPlayer ch = SelectChara(EntryNo);

            return ch.HaveMoney;
        }

        public int NowAreaType()
        {
            return LibArea.GetAreaType(LibArea.GetAreaID(MarkIDs));
        }

        public bool CheckCompanion()
        {
            CompanionDataEntity.ts_companion_listRow row = LibCompanionData.Entity.ts_companion_list.FindBycompanion_id(EntryNos);
            return (row != null);
        }

        public void GetCompanion(int EntryNo)
        {
            CompanionDataEntity.ts_companion_listRow row = LibCompanionData.Entity.ts_companion_list.FindBycompanion_id(EntryNo);
            row.valid_fg = true;

            LibCompanionData.Update();
        }

        public string GetCompanionName()
        {
            CompanionDataEntity.ts_companion_listRow row = LibCompanionData.Entity.ts_companion_list.FindBycompanion_id(EntryNos);

            return row.nick_name;
        }

        public string GetCompanionNameByEntry(int EntryNo)
        {
            CompanionDataEntity.ts_companion_listRow row = LibCompanionData.Entity.ts_companion_list.FindBycompanion_id(EntryNo);

            return row.nick_name;
        }

        public void StatusAdd(int StatusID, int Rank, bool IsView)
        {
            if (PartyNos > 0)
            {
                int[] Entrys = LibParty.PartyMemberNo(PartyNos);
                for (int i = 0; i < Entrys.Length; i++)
                {
                    _StatusAdd(Entrys[i], StatusID, Rank, IsView);
                }
            }
            else
            {
                _StatusAdd(EntryNos, StatusID, Rank, IsView);
            }
        }

        private void _StatusAdd(int EntryNo, int StatusID, int Rank, bool IsView)
        {
            LibPlayer ch = SelectChara(EntryNo);

            ch.StatusEffect.Add(StatusID, Rank, 1, 0, 100, IsView);
            //ch.StatusEffect.Update(EntryNo);
        }

        public string GetPartyName()
        {
            if (PartyNos > 0)
            {
                return LibParty.GetPartyName(PartyNos);
            }
            else
            {
                return LibParty.GetPartyName(LibParty.GetPartyNo(EntryNos));
            }
        }
    }
}
