using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;
using CommonLibrary.DataFormat.SpecialEntity;

namespace CommonLibrary
{
    /// <summary>
    /// スキル関連を扱うクラス
    /// </summary>
    public static class LibSkill
    {
        public static CommonSkillEntity Entity;
        public static SkillGetEntity GetEntity;

        static LibSkill()
        {
            LoadSkill();
        }

        static public void LoadSkill()
        {
            Entity = new CommonSkillEntity();
            GetEntity = new SkillGetEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder SelSql = new StringBuilder();
                #region TABLE <skill_list>
                SelSql.AppendLine("SELECT [sk_id]");
                SelSql.AppendLine("      ,[sk_name]");
                SelSql.AppendLine("      ,[sk_mp]");
                SelSql.AppendLine("      ,[sk_tp]");
                SelSql.AppendLine("      ,[sk_attack]");
                SelSql.AppendLine("      ,[sk_power]");
                SelSql.AppendLine("      ,[sk_damage_rate]");
                SelSql.AppendLine("      ,[sk_plus_score]");
                SelSql.AppendLine("      ,[sk_hit]");
                SelSql.AppendLine("      ,[sk_critical]");
                SelSql.AppendLine("      ,[sk_critical_type]");
                SelSql.AppendLine("      ,[sk_round]");
                SelSql.AppendLine("      ,[sk_range]");
                SelSql.AppendLine("      ,[sk_charge]");
                SelSql.AppendLine("      ,[sk_hate]");
                SelSql.AppendLine("      ,[sk_vhate]");
                SelSql.AppendLine("      ,[sk_dhate]");
                SelSql.AppendLine("      ,[sk_antiair]");
                SelSql.AppendLine("      ,[sk_target_restrict]");
                SelSql.AppendLine("      ,[sk_use_limit]");
                SelSql.AppendLine("      ,[sk_fire]");
                SelSql.AppendLine("      ,[sk_freeze]");
                SelSql.AppendLine("      ,[sk_air]");
                SelSql.AppendLine("      ,[sk_earth]");
                SelSql.AppendLine("      ,[sk_water]");
                SelSql.AppendLine("      ,[sk_thunder]");
                SelSql.AppendLine("      ,[sk_holy]");
                SelSql.AppendLine("      ,[sk_dark]");
                SelSql.AppendLine("      ,[sk_slash]");
                SelSql.AppendLine("      ,[sk_pierce]");
                SelSql.AppendLine("      ,[sk_strike]");
                SelSql.AppendLine("      ,[sk_break]");
                SelSql.AppendLine("      ,[sk_effect]");
                SelSql.AppendLine("      ,[sk_type]");
                SelSql.AppendLine("      ,[sk_atype]");
                SelSql.AppendLine("      ,[sk_damage_type]");
                SelSql.AppendLine("      ,[sk_target_area]");
                SelSql.AppendLine("      ,[sk_comment]");
                SelSql.AppendLine("      ,[sk_target_party]");
                SelSql.AppendLine("      ,[sk_arts_category]");
                SelSql.AppendLine("  FROM [GrandBlazeMaster].[dbo].[mt_skill_list]");
                SelSql.AppendLine("order by sk_id");
                #endregion

                dba.Fill(SelSql.ToString(), Entity.skill_list);

                SelSql = new StringBuilder();
                #region TABLE <mt_skill_get_list>
                SelSql.AppendLine("SELECT [perks_id]");
                SelSql.AppendLine("      ,[tm_install]");
                SelSql.AppendLine("      ,[tm_install_level]");
                SelSql.AppendLine("      ,[tm_level]");
                SelSql.AppendLine("      ,[tm_str]");
                SelSql.AppendLine("      ,[tm_agi]");
                SelSql.AppendLine("      ,[tm_mag]");
                SelSql.AppendLine("      ,[tm_unq]");
                SelSql.AppendLine("      ,[tm_race]");
                SelSql.AppendLine("      ,[tm_guardian]");
                SelSql.AppendLine("      ,[tm_base_skill]");
                SelSql.AppendLine("  FROM [mt_skill_get_list]");
                SelSql.AppendLine("  ORDER BY [tm_level]");
                #endregion

                dba.Fill(SelSql.ToString(), GetEntity.mt_skill_get_list);
            }
        }

        /// <summary>
        /// スキル名称取得
        /// </summary>
        /// <param name="SkillID">取得対象スキルNo</param>
        /// <returns>スキル名</returns>
        static public string GetSkillName(int SkillID)
        {
            CommonSkillEntity.skill_listRow row = Entity.skill_list.FindBysk_id(SkillID);

            if (row != null)
            {
                return row.sk_name;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// スキル種別取得
        /// </summary>
        /// <param name="SkillID">取得対象スキルNo</param>
        /// <returns>種別名</returns>
        static public string GetSkillTypeName(int SkillID)
        {
            CommonSkillEntity.skill_listRow row = Entity.skill_list.FindBysk_id(SkillID);

            if (row != null)
            {
                switch (row.sk_type)
                {
                    case Status.SkillType.Arts:
                        return "アーツ";
                    case Status.SkillType.Support:
                        return "サポート";
                    case Status.SkillType.Assist:
                        return "アシスト";
                    case Status.SkillType.Special:
                        return "スペシャル";
                    default:
                        throw new Exception("無効なスキル種別が選択されました。");
                }
            }
            else
            {
                throw new Exception("無効なスキル種別が選択されました。");
            }
        }

        /// <summary>
        /// 指定されたスキルのDataRowを取得
        /// </summary>
        /// <param name="SkillID">スキル番号</param>
        /// <returns>選択された スキルのDataRow</returns>
        static public CommonSkillEntity.skill_listRow GetSkillRow(int SkillID)
        {
            return Entity.skill_list.FindBysk_id(SkillID);
        }

        /// <summary>
        /// 指定されたスキルのエフェクトを取得
        /// </summary>
        /// <param name="SkillID">スキル番号</param>
        /// <returns>エフェクト</returns>
        static public EffectListEntity.effect_listDataTable GetEffectTable(int SkillID)
        {
            CommonSkillEntity.skill_listRow row = Entity.skill_list.FindBysk_id(SkillID);

            if (row == null)
            {
                return null;
            }

            EffectListEntity.effect_listDataTable EffectTable = new EffectListEntity.effect_listDataTable();
            LibEffect.Split(row.sk_effect, ref EffectTable);

            return EffectTable;
        }

        /// <summary>
        /// スキルの存在確認
        /// </summary>
        /// <param name="SkillID">スキル番号</param>
        /// <returns>ステータス</returns>
        static public bool CheckSkillNo(int SkillID)
        {
            if (SkillID < 10) { return false; }

            CommonSkillEntity.skill_listRow row = Entity.skill_list.FindBysk_id(SkillID);

            if (row == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 指定されたスキルを削除
        /// </summary>
        /// <param name="SkillID">スキル番号</param>
        /// <returns>選択された スキルのDataRow</returns>
        static public void Delete(int SkillID)
        {
            CommonSkillEntity.skill_listRow row = Entity.skill_list.FindBysk_id(SkillID);

            if (row != null)
            {
                row.Delete();
            }
        }

        /// <summary>
        /// 習得スキル番号の取得
        /// </summary>
        /// <param name="GetType">習得タイプ</param>
        /// <param name="GetterID">習得タイプ別ID</param>
        /// <param name="SPointMin">最小熟練度</param>
        /// <param name="SPointMax">最大熟練度</param>
        /// <returns>習得スキルテーブル</returns>
        static public DataTable GetSkillNo(int GetType, int GetterID, int SPointMin, int SPointMax)
        {
            GetEntity.mt_skill_get_list.DefaultView.RowFilter = "get_type=" + GetType + " and getperks_id=" + GetterID + " and spoint>=" + SPointMin + " and spoint<=" + SPointMax;

            return GetEntity.mt_skill_get_list.DefaultView.ToTable();
        }

        /// <summary>
        /// 指定文字列を条件テーブルに変換
        /// </summary>
        /// <param name="TermString">条件文字列</param>
        /// <param name="InTable">条件テーブル</param>
        public static void Split(string TermString, ref SkillTermEntity.tm_skill_listDataTable InTable)
        {
            string[] ArrayEf = TermString.Split('|');

            foreach (string Data in ArrayEf)
            {
                string[] DataRowArray = Data.Split(',');
                if (DataRowArray[0] != "0")
                {
                    SkillTermEntity.tm_skill_listRow NewStatusRow = InTable.Newtm_skill_listRow();

                    NewStatusRow.skill_id = int.Parse(DataRowArray[0]);
                    NewStatusRow.skill_rank = int.Parse(DataRowArray[1]);

                    InTable.Addtm_skill_listRow(NewStatusRow);
                }
            }
        }

        /// <summary>
        /// 条件テーブルを文字列に変換
        /// </summary>
        /// <param name="TermString">文字列</param>
        /// <param name="InTable">条件テーブル</param>
        public static void Join(ref string TermString, SkillTermEntity.tm_skill_listDataTable InTable)
        {
            StringBuilder EffectStringBuild = new StringBuilder(TermString);
            foreach (SkillTermEntity.tm_skill_listRow NewStatusRow in InTable)
            {
                if (EffectStringBuild.ToString().Length > 0)
                {
                    EffectStringBuild.Append("|");
                }
                EffectStringBuild.Append(NewStatusRow.skill_id.ToString());
                EffectStringBuild.Append("," + NewStatusRow.skill_rank.ToString());
            }

            if (EffectStringBuild.ToString().Length == 0)
            {
                EffectStringBuild.Append("0,0");
            }

            TermString = EffectStringBuild.ToString();
        }

        /// <summary>
        /// スキル登録
        /// </summary>
        static public void Update()
        {
            string UpSql;
            string InSql;
            string DelSql;
            string TableName = "";

            if (Entity.skill_list.GetChanges() == null)
            {
                return;
            }

            LibDBLocal DbAc = new LibDBLocal(Status.DataBaseAccessTarget.Master);
            try
            {
                DbAc.BeginTransaction();

                foreach (CommonSkillEntity.skill_listRow SkillRow in Entity.skill_list.GetChanges().Rows)
                {
                    TableName = "[GrandBlazeMaster].[dbo].[mt_skill_list]";

                    if (SkillRow.RowState == DataRowState.Added || SkillRow.RowState == DataRowState.Modified)
                    {
                        UpSql = LibSql.MakeUpSql(TableName, "sk_id=" + SkillRow.sk_id, SkillRow);
                        InSql = LibSql.MakeInSql(TableName, SkillRow);

                        if (DbAc.ExecuteNonQuery(UpSql) == 0)
                        {
                            DbAc.ExecuteNonQuery(InSql);
                        }
                    }
                    else if (SkillRow.RowState == DataRowState.Deleted)
                    {
                        DelSql = "DELETE FROM " + TableName + " WHERE sk_id=" + (int)SkillRow["sk_id", DataRowVersion.Original];

                        DbAc.ExecuteNonQuery(DelSql);
                    }
                }
                DbAc.Commit();
            }
            catch
            {
                DbAc.Rollback();
            }
            finally
            {
                DbAc.Close();
            }

            LoadSkill();
        }
    }
}
