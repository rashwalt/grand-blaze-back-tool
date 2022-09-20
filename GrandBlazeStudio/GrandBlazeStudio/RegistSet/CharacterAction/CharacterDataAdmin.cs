using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CommonLibrary;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.Entity;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// キャラクターデータのロード
        /// </summary>
        private void CharacterDataLoad()
        {
            // データの一括ロード
            foreach (DataRow row in CharaTable.Rows)
            {
                LibPlayer player = new LibPlayer(Status.Belong.Friend, (int)row["entry_no"]);
                player.GetExp = 0;
                CharaList.Add(player);
            }
        }

        /// <summary>
        /// 一括保存
        /// </summary>
        private void CharacterDataSave()
        {
            foreach (LibPlayer Data in CharaList)
            {
                Data.Update();
                Data.StatusEffect.Update(Data.EntryNo);
            }

            LibPlayerMemo.Update();
        }
    }
}
