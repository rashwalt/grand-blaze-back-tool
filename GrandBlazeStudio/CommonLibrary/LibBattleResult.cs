using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using CommonLibrary.DataAccess;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary
{
    public static class LibBattleResult
    {
        public static BattleResultEntity Entity = new BattleResultEntity();

        static LibBattleResult()
        {
            Load();
        }

        public static void Load()
        {
            Entity = new BattleResultEntity();
            using (LibDBLocal dba = new LibDBLocal())
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <temp_battle_result>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("party_no, ");
                Sql.AppendLine("mark_id, ");
                Sql.AppendLine("result");
                Sql.AppendLine("FROM temp_battle_result");
                #endregion

                dba.Fill(Sql.ToString(), Entity.temp_battle_result);
            }
        }

        public static void Update(int PartyNo, int MarkID, int BattleResult)
        {
            BattleResultEntity.temp_battle_resultRow row = Entity.temp_battle_result.Newtemp_battle_resultRow();
            row.party_no = PartyNo;
            row.mark_id = MarkID;
            row.result = BattleResult;
            Entity.temp_battle_result.Addtemp_battle_resultRow(row);
        }

        public static int GetResult(int PartyNo, int MarkID)
        {
            BattleResultEntity.temp_battle_resultRow row = Entity.temp_battle_result.FindByparty_no(PartyNo);

            if (row != null && row.mark_id == MarkID)
            {
                return row.result;
            }

            return -1;
        }
    }
}
