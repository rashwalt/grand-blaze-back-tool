using CommonLibrary;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandBlazeStudio.RegistSet.CharacterAction.OfficialEvent
{
    static class Halloween
    {
        public static void Private(List<LibPlayer> CharaList, LibContinue con, LibUnitBaseMini CharaMini)
        {
            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_mainRow ContinueMainRow = con.Entity.ts_continue_main.FindByentry_no(EntryNo);

                if (ContinueMainRow == null && !CharaMini.CheckNewPlayer(EntryNo))
                {
                    // ない場合はスキップ
                    continue;
                }

                // ミニイベント。
                LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.QuestPrivateEntry, "街は祭りの様相を呈していた。<br />様々な飾り付けがされ、子供たちが時にお菓子を配り、時に街の家中からお菓子を手に入れる。<br />そんな状況だからか、子供たちからお菓子と一緒に何かの小包をもらったのも、きっと祭りの一環だったのだろう。<br />ただその小包に挟まれた小さな紙片に「よき正義を」と一文が書かれていることを除けば……。<br />さっきのはロビンという名の人物だったのか。それとも……。<br />冒険をはじめたばかりの身には、何かが起きつつあるのかという漠然とした予感のようなものが浮かぶだけだった……。", Status.MessageLevel.Normal);

                // 共通のアイテム配布
                string GetMessageCommon = "";
                int CommonItemNo = 27520;
                int CommonItenCount = 1;
                Mine.AddItemWithMessage(CommonItemNo, false, CommonItenCount, ref GetMessageCommon);
                if (GetMessageCommon.Length > 0)
                {
                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.QuestPrivateEntry, GetMessageCommon, Status.MessageLevel.Normal);
                }

                // 性別別のアイテム配布
                string GetMessageSex = "";
                switch (Mine.Sex)
                {
                    case Status.Sex.Male:
                        Mine.AddItemWithMessage(20506, false, 1, ref GetMessageSex);
                        break;
                    case Status.Sex.Female:
                        Mine.AddItemWithMessage(20507, false, 1, ref GetMessageSex);
                        break;
                    case Status.Sex.Unknown:
                        Mine.AddItemWithMessage(20508, false, 1, ref GetMessageSex);
                        break;
                }
                if (GetMessageSex.Length > 0)
                {
                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.QuestPrivateEntry, GetMessageSex, Status.MessageLevel.Normal);
                }
            }
        }
    }
}
