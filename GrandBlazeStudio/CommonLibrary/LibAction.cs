using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace CommonLibrary
{
    public static class LibAction
    {
        public static ActionDataEntity Entity;

        static LibAction()
        {
            Entity = new ActionDataEntity();
            using (LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master))
            {
                StringBuilder Sql = new StringBuilder();
                #region TABLE <mt_target_list>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("* ");
                Sql.AppendLine("FROM mt_target_list");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_target_list);

                Sql = new StringBuilder();
                #region TABLE <mt_action_list>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("* ");
                Sql.AppendLine("FROM mt_action_list");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_action_list);

                Sql = new StringBuilder();
                #region TABLE <mt_timing_list>
                Sql.AppendLine("SELECT");
                Sql.AppendLine("* ");
                Sql.AppendLine("FROM mt_timing_list");
                #endregion

                dba.Fill(Sql.ToString(), Entity.mt_timing_list);
            }
        }

        /// <summary>
        /// ターゲット選択名称取得
        /// </summary>
        /// <param name="ActTargetID">ターゲット選択ID</param>
        /// <returns>名称</returns>
        public static string GetActionTargetName(int ActTargetID)
        {
            ActionDataEntity.mt_target_listRow row = Entity.mt_target_list.FindBytarget_id(ActTargetID);

            if (row == null)
            {
                return "";
            }

            return row.target_act_name;
        }

        /// <summary>
        /// ターゲットタイプ取得
        /// </summary>
        /// <param name="ActTargetID">取得対象アクション</param>
        /// <returns>ターゲットタイプ</returns>
        public static int GetTargetType(int ActTargetID)
        {
            ActionDataEntity.mt_target_listRow row = Entity.mt_target_list.FindBytarget_id(ActTargetID);

            if (row == null)
            {
                return -1;
            }

            return row.target_type;
        }

        /// <summary>
        /// ターゲットNo取得
        /// </summary>
        /// <param name="ActTargetID">取得対象アクション</param>
        /// <returns>ターゲットNo</returns>
        public static int GetTargetNo(int ActTargetID)
        {
            ActionDataEntity.mt_target_listRow row = Entity.mt_target_list.FindBytarget_id(ActTargetID);

            if (row == null)
            {
                return -1;
            }

            return row.target_no;
        }

        /// <summary>
        /// アクション名称取得
        /// </summary>
        /// <param name="ActionID">ターゲット選択ID</param>
        /// <returns>名称</returns>
        public static string GetActionName(int ActionID)
        {
            ActionDataEntity.mt_action_listRow row = Entity.mt_action_list.FindByaction_id(ActionID);

            if (row == null)
            {
                return "";
            }

            return row.action_name;
        }

        /// <summary>
        /// タイミング名称取得
        /// </summary>
        /// <param name="TimingID">タイミング選択ID</param>
        /// <returns>名称</returns>
        public static string GetTimingName(int TimingID)
        {
            ActionDataEntity.mt_timing_listRow row = Entity.mt_timing_list.FindBytiming_id(TimingID);

            if (row == null)
            {
                return "";
            }

            return row.timing_name;
        }
    }
}
