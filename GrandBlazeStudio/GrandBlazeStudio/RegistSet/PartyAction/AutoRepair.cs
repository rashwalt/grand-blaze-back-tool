using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Character;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;

namespace GrandBlazeStudio.RegistSet.PartyAction
{
    partial class PartyActionMain
    {
        /// <summary>
        /// 自動回復
        /// </summary>
        private void AutoRepair()
        {
            foreach (LibPlayer Mine in CharaList)
            {
                int UpExp = Mine.GetExp;

                EffectListEntity.effect_listRow EffectExpRow = Mine.EffectList.FindByeffect_id(965);
                if (EffectExpRow != null)
                {
                    UpExp += (int)((decimal)UpExp * EffectExpRow.rank / 100m);
                }

                Mine.Exp += UpExp;
                Mine.InstallClassExp += UpExp;
                if (Mine.SecondryIntallClassID > 0)
                {
                    Mine.SecondryInstallClassExp += UpExp / 2;
                }

                // レベルアップ処理
                Mine.UpLevelExp();

                // 自動回復処理
                int CureRate = LibQuest.GetCureRate(LibParty.GetPartyMarkID(LibParty.GetPartyNo(Mine.EntryNo)));

                {
                    EffectListEntity.effect_listRow EffectRow = Mine.EffectList.FindByeffect_id(830);
                    if (EffectRow != null)
                    {
                        CureRate += (int)EffectRow.rank;
                    }
                }

                if (Mine.HPNow <= 0)
                {
                    Mine.HPNow = Mine.HPMax;
                    Mine.MPNow = Mine.MPMax;
                }

                if (CureRate > 0)
                {
                    Mine.HPNow += (int)((decimal)Mine.HPMax * (decimal)CureRate / 100m);
                    Mine.MPNow += (int)((decimal)Mine.MPMax * (decimal)CureRate / 100m);
                }
                Mine.TPNow = 0;

                Mine.Formation = Mine.FormationDefault;
                Mine.StatusEffect.Update(Mine.EntryNo);
                Mine.Update();
            }

            LibPlayerMemo.Update();
        }
    }
}
