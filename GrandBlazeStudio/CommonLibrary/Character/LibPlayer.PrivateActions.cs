using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary.Character
{
    public partial class LibPlayer : LibUnitBase
    {
        /// <summary>
        /// 戦闘行動の設定
        /// </summary>
        /// <param name="ActionNo">アクション順番</param>
        /// <param name="ActionTarget">アクションターゲット</param>
        /// <param name="Actions">アクション内容</param>
        /// <param name="SkillID">スキルID</param>
        /// <param name="SkillCreated">スキル作成フラグ</param>
        /// <returns>ステータス</returns>
        public int ActionSettings(int ActionNo, int ActionTarget, int Actions, int SkillID)
        {
            CommonUnitDataEntity.action_listRow ActionRow = ActionList.Newaction_listRow();

            ActionRow.action_no = ActionNo;
            ActionRow.action_target = ActionTarget;
            ActionRow.action = Actions;
            ActionRow.probability = 100;
            ActionRow.max_count = 0;
            ActionRow.timing1 = 0;
            ActionRow.timing2 = 0;
            ActionRow.timing3 = 0;
            ActionRow.perks_id = SkillID;

            ActionList.Addaction_listRow(ActionRow);

            return Status.ActionSetting.OK;
        }

        /// <summary>
        /// 戦闘行動内容をリセット
        /// </summary>
        public void ActionSettingReset()
        {
            foreach (CommonUnitDataEntity.action_listRow ActionRow in ActionList)
            {
                ActionRow.Delete();
            }
        }

        /// <summary>
        /// 隊列変更
        /// </summary>
        /// <param name="Formations">変更隊列</param>
        /// <returns>ステータス</returns>
        public int SetFormation(int Formations)
        {
            switch (Formations)
            {
                case Status.Formation.Foward:
                case Status.Formation.Backs:
                    break;
                default:
                    return Status.Common.NotFound;
            }

            Formation = Formations;

            return Status.FormationSetting.OK;
        }

        /// <summary>
        /// セリフ内容をリセット
        /// </summary>
        public void SerifSettingReset()
        {
            for (int i = SerifList.Count - 1; i >= 0; i--)
            {
                SerifList[i].Delete();
            }
        }

        /// <summary>
        /// セリフ設定
        /// </summary>
        /// <param name="WordNo">セリフ順番</param>
        /// <param name="Situation">シチュエーションID</param>
        /// <param name="SeriText">セリフ本文</param>
        /// <returns>ステータス</returns>
        public int SerifSettings(int WordNo, int Situation, string SeriText, int PerksID)
        {
            CommonUnitDataEntity.serif_listRow SerifRow = SerifList.Newserif_listRow();

            SerifRow.serif_no = WordNo;
            SerifRow.situation = Situation;
            SerifRow.serif_text = SeriText;
            SerifRow.perks_id = PerksID;  

            SerifList.Addserif_listRow(SerifRow);

            return Status.SerifSetting.OK;
        }

        /// <summary>
        /// インストールクラスのレベル習得
        /// </summary>
        /// <param name="InstallID">インストールクラスID</param>
        /// <returns>レベル</returns>
        public int GetInstallClassLevel(int InstallID)
        {
            CommonUnitDataEntity.install_level_listRow InstallRow = InstallClassList.FindByinstall_id(InstallID);

            if (InstallRow != null)
            {
                return InstallRow.level;
            }

            return -1;
        }

        /// <summary>
        /// インストールクラスの習得
        /// </summary>
        /// <param name="InstallID">インストールクラスID</param>
        /// <returns>習得できたか</returns>
        public bool AddInstallClass(int InstallID)
        {
            string InstallName = LibInstall.GetInstallName(InstallID);
            if (InstallName.Length == 0)
            {
                // 存在しない場合ここで終わり
                return false;
            }

            bool IsOK = false;

            if (CheckHaveInstall(InstallID))
            {
                // すでに持っている場合、なにもしない
                IsOK = true;
            }
            else
            {
                // 持っていない場合、新たに設定する
                CommonUnitDataEntity.install_level_listRow EditRow = InstallClassList.Newinstall_level_listRow();

                EditRow.install_id = InstallID;
                EditRow.level = 1;
                EditRow.exp = 0;

                InstallClassList.Addinstall_level_listRow(EditRow);

                IsOK = true;
            }

            return IsOK;
        }

        /// <summary>
        /// インストールクラスを持っているかどうか
        /// </summary>
        /// <param name="InstallID">インストールクラスID</param>
        /// <returns>所持しているか</returns>
        public bool CheckHaveInstall(int InstallID)
        {
            CommonUnitDataEntity.install_level_listRow InstallRow = InstallClassList.FindByinstall_id(InstallID);

            if (InstallRow != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// インストールクラスのレベルが指定値以上か
        /// </summary>
        /// <param name="InstallID">インストールクラスID</param>
        /// <param name="Level">レベル</param>
        /// <returns>所持しているか</returns>
        public bool CheckInstallLevel(int InstallID, int Level)
        {
            CommonUnitDataEntity.install_level_listRow InstallRow = InstallClassList.FindByinstall_id(InstallID);

            if (InstallRow != null && InstallRow.level >= Level)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// クエスト・ミッションをオファー
        /// </summary>
        /// <param name="QuestID">クエスト・ミッションID</param>
        public bool OfferQuest(int QuestID)
        {
            CommonUnitDataEntity.quest_listRow QuestRow = QuestList.FindByquest_id(QuestID);

            if (QuestRow == null)
            {
                // 持っていない場合、新たに設定する
                CommonUnitDataEntity.quest_listRow EditRow = QuestList.Newquest_listRow();

                EditRow.quest_id = QuestID;
                EditRow.clear_fg = false;
                EditRow.quest_step = 0;

                QuestList.Addquest_listRow(EditRow);

                return true;
            }

            return false;
        }

        /// <summary>
        /// クエスト・ミッションをステップアップ
        /// </summary>
        /// <param name="QuestID">クエスト・ミッションID</param>
        /// <param name="StageCount">クエストステージ</param>
        public void SetQuestStage(int QuestID, int StageCount)
        {
            CommonUnitDataEntity.quest_listRow QuestRow = QuestList.FindByquest_id(QuestID);

            if (QuestRow != null)
            {
                if (QuestRow.clear_fg)
                {
                    // クリア済みの場合はここで終了させる
                    return;
                }

                // ステップアップ
                QuestRow.quest_step = StageCount;
            }
            else
            {
                // 持っていない場合、例外
                throw new Exception("オファーされていないクエストがステップアップされようとしています。");
            }
        }

        /// <summary>
        /// クエスト・ミッションをコンプリート
        /// </summary>
        /// <param name="QuestID">クエスト・ミッションID</param>
        public void CompleteQuest(int QuestID)
        {
            CommonUnitDataEntity.quest_listRow QuestRow = QuestList.FindByquest_id(QuestID);

            if (QuestRow != null)
            {
                if (QuestRow.clear_fg)
                {
                    // クリア済みの場合はここで終了させる
                    return;
                }

                // コンプリート
                QuestRow.clear_fg = true;
            }
            else
            {
                // 持っていない場合、例外
                throw new Exception("オファーされていないクエストがコンプリートされようとしています。");
            }
        }

        /// <summary>
        /// クエスト・ミッションを受けているか？(オファー)
        /// </summary>
        /// <param name="QuestID">クエスト・ミッションID</param>
        /// <returns>クエストを受けている？</returns>
        public bool CheckQuest(int QuestID)
        {
            CommonUnitDataEntity.quest_listRow QuestRow = QuestList.FindByquest_id(QuestID);

            if (QuestRow != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// クエスト・ミッションを受けているか？(コンプリート)
        /// </summary>
        /// <param name="QuestID">クエスト・ミッションID</param>
        /// <returns>クエストを受けている？</returns>
        public bool CheckQuestComp(int QuestID)
        {
            CommonUnitDataEntity.quest_listRow QuestRow = QuestList.FindByquest_id(QuestID);

            if (QuestRow != null && QuestRow.clear_fg)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// クエストステップ数取得
        /// </summary>
        /// <param name="QuestID">クエストID</param>
        /// <returns>ステップ数</returns>
        public int QuestStage(int QuestID)
        {
            CommonUnitDataEntity.quest_listRow QuestRow = QuestList.FindByquest_id(QuestID);

            if (QuestRow != null)
            {
                return QuestRow.quest_step;
            }

            return -1;
        }

        /// <summary>
        /// イベントフラグを設定
        /// </summary>
        /// <param name="FlagID">フラグID</param>
        /// <param name="FlagValue">フラグ数値</param>
        /// <param name="Compulsion">強制の場合はtrue</param>
        public void SetEventFlag(int FlagID, int FlagValue, bool Compulsion)
        {
            CommonUnitDataEntity.event_flagRow EventFlagRow = EventFlag.FindByflag_id(FlagID);

            if (EventFlagRow == null)
            {
                // 持っていない場合、新たに設定する
                CommonUnitDataEntity.event_flagRow EditRow = EventFlag.Newevent_flagRow();

                EditRow.flag_id = FlagID;
                EditRow.flag_value = FlagValue;

                EventFlag.Addevent_flagRow(EditRow);
            }
            else
            {
                if (Compulsion)
                {
                    EventFlagRow.flag_value = FlagValue;
                }
                else
                {
                    if (FlagValue > EventFlagRow.flag_value)
                    {
                        EventFlagRow.flag_value = FlagValue;
                    }
                }
            }

        }

        /// <summary>
        /// イベントフラグを取得
        /// </summary>
        /// <param name="FlagID">フラグID</param>
        public int GetEventFlag(int FlagID)
        {
            CommonUnitDataEntity.event_flagRow EventFlagRow = EventFlag.FindByflag_id(FlagID);

            if (EventFlagRow != null)
            {
                return EventFlagRow.flag_value;
            }

            return 0;
        }

        /// <summary>
        /// アイコン内容をリセット
        /// </summary>
        public void IconSettingReset()
        {
            for (int i = IconList.Count - 1; i >= 0; i--)
            {
                IconList[i].Delete();
            }
        }

        /// <summary>
        /// アイコン追加
        /// </summary>
        /// <param name="IconID">アイコンID</param>
        /// <param name="IconUrl">アイコンURL</param>
        /// <param name="IconCopy">アイコン著作者</param>
        /// <returns>ステータス</returns>
        public int SetIcon(int IconID, string IconUrl, string IconCopy)
        {
            CommonUnitDataEntity.icon_listRow EditRow = IconList.Newicon_listRow();

            EditRow.icon_id = IconID;
            EditRow.icon_url = IconUrl;
            EditRow.icon_copyright = IconCopy;

            IconList.Addicon_listRow(EditRow);

            return Status.IconSetting.OK;
        }

        /// <summary>
        /// 所持スカウト技能
        /// </summary>
        /// <returns>スカウト技能の基本値</returns>
        private int ScoutRank()
        {
            EffectListEntity.effect_listRow scoutRow = EffectList.FindByeffect_id(3040);
            if (scoutRow != null)
            {
                switch ((int)scoutRow.rank)
                {
                    case 1:
                        return 5;
                    case 2:
                        return 15;
                }
            }

            return 0;
        }

        /// <summary>
        /// トラップ発見
        /// </summary>
        /// <param name="HideLevel">隠蔽Lv</param>
        /// <returns>発見の有無</returns>
        public bool FindTrap(int HideLevel)
        {
            int TrapBaseLevel = ScoutRank();

            EffectListEntity.effect_listRow EffectRow = EffectList.FindByeffect_id(3000);
            if (EffectRow != null)
            {
                TrapBaseLevel += (int)EffectRow.rank;
            }

            return TrapBaseLevel >= HideLevel;
        }

        /// <summary>
        /// トラップ解除
        /// </summary>
        /// <param name="TrapLevel">罠Lv</param>
        /// <returns>解除の成否</returns>
        public bool RemoveTrap(int TrapLevel)
        {
            int TrapBaseLevel = ScoutRank();

            EffectListEntity.effect_listRow EffectRow = EffectList.FindByeffect_id(3001);
            if (EffectRow != null)
            {
                TrapBaseLevel += (int)EffectRow.rank;
            }

            return TrapBaseLevel >= TrapLevel;
        }

        /// <summary>
        /// 移動可能マーク設定
        /// </summary>
        /// <param name="MarkID">マークID</param>
        /// <param name="IsClear">踏破したか</param>
        /// <returns>新規マーク入手か</returns>
        public bool SetMovingMark(int MarkID, bool IsClear, bool IsLogging)
        {
            CharacterDataEntity.ts_character_moving_markRow row = MovingOKMarks.FindByentry_nomark_id(EntryNo, MarkID);

            bool IsChange = false;

            if (row != null)
            {
                if (!row.instance)
                {
                    row.instance = IsClear;
                    IsChange = true;
                }
            }
            else
            {
                // 持っていない場合、新たに設定する
                row = MovingOKMarks.Newts_character_moving_markRow();

                row.entry_no = EntryNo;
                row.mark_id = MarkID;
                row.instance = IsClear;

                MovingOKMarks.Addts_character_moving_markRow(row);

                IsChange = true;
            }

            if (IsClear && IsChange && IsLogging)
            {
                // 追加で経験値ゲット
                GetExp += 10;
                LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.MarkFinder, "新たなマークに到達！ (<span class=\"stup\">+10</span> Exp)", Status.MessageLevel.Normal);
            }

            return IsChange;
        }

        /// <summary>
        /// カギ解除
        /// </summary>
        /// <param name="KeyLevel">鍵Lv</param>
        /// <returns>解除の成否</returns>
        public bool RemoveKey(int KeyLevel)
        {
            int KeyBaseLevel = ScoutRank();

            EffectListEntity.effect_listRow EffectRow = EffectList.FindByeffect_id(3080);
            if (EffectRow != null)
            {
                KeyBaseLevel += (int)EffectRow.rank;
            }

            return KeyBaseLevel >= KeyLevel;
        }

        /// <summary>
        /// ハック解除
        /// </summary>
        /// <param name="HackLevel">解析Lv</param>
        /// <returns>解除の成否</returns>
        public bool RemoveHack(int HackLevel)
        {
            int HackBaseLevel = ScoutRank();

            EffectListEntity.effect_listRow EffectRow = EffectList.FindByeffect_id(3081);
            if (EffectRow != null)
            {
                HackBaseLevel += (int)EffectRow.rank;
            }

            return HackBaseLevel >= HackLevel;
        }
    }
}
