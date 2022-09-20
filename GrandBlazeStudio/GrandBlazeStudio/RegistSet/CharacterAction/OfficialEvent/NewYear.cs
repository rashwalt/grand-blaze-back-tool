using CommonLibrary;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandBlazeStudio.RegistSet.CharacterAction.OfficialEvent
{
    static class NewYear
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
                LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.QuestPrivateEntry, "新しい年を迎え、街はお祝いムード一色だった。<br />それは、新しい年に、仕事始めに冒険者ギルドへやってきた冒険者も例外ではない。<br />「おめでとうございま～す！新年の景品です～」<br />ギルドでやってくる冒険者に何かを配っているようだ…！", Status.MessageLevel.Normal);

                // 共通のアイテム配布
                string GetMessageCommon = "";
                int CommonItemNo = 27522;
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
                        Mine.AddItemWithMessage(19600, false, 1, ref GetMessageSex);
                        break;
                    case Status.Sex.Female:
                        Mine.AddItemWithMessage(19601, false, 1, ref GetMessageSex);
                        break;
                    case Status.Sex.Unknown:
                        Mine.AddItemWithMessage(19602, false, 1, ref GetMessageSex);
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
