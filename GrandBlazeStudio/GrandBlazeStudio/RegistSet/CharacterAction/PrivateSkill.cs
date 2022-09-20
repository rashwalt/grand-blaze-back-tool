using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary;
using CommonLibrary.DataFormat.SpecialEntity;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// プライベートスキル習得
        /// </summary>
        private void PrivateSkill()
        {
            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;
                ContinueDataEntity.ts_continue_mainRow ContinueMainRow = con.Entity.ts_continue_main.FindByentry_no(EntryNo);

                if (ContinueMainRow == null || ContinueMainRow.getting_private_skill == 0)
                {
                    // スキル習得を実行しない場合はスキップ
                    continue;
                }

                // 条件判定
                if (Mine.LevelUpPoint <= 0)
                {
                    continue;
                }

                // 習得条件の精査
                SkillGetEntity.mt_skill_get_listRow skillGetRow = LibSkill.GetEntity.mt_skill_get_list.FindByperks_id(ContinueMainRow.getting_private_skill);

                if (skillGetRow == null)
                {
                    continue;
                }

                // レベルチェック
                if (Mine.Level < skillGetRow.tm_level)
                {
                    continue;
                }

                // インストールチェック
                if (skillGetRow.tm_install > 0 && skillGetRow.tm_install_level > 0)
                {
                    CommonUnitDataEntity.install_level_listRow characterIntallRow = Mine.InstallClassList.FindByinstall_id(skillGetRow.tm_install);
                    if (characterIntallRow == null || characterIntallRow.level < skillGetRow.tm_install_level)
                    {
                        continue;
                    }
                }

                // ステータスチェック
                if (Mine.STRBase < skillGetRow.tm_str)
                {
                    continue;
                }
                if (Mine.AGIBase < skillGetRow.tm_agi)
                {
                    continue;
                }
                if (Mine.MAGBase < skillGetRow.tm_mag)
                {
                    continue;
                }
                if (Mine.UNQBase < skillGetRow.tm_unq)
                {
                    continue;
                }

                // 種族
                if (skillGetRow.tm_race > 0 && Mine.Race != skillGetRow.tm_race)
                {
                    continue;
                }

                // 守護者
                if (skillGetRow.tm_guardian > 0 && Mine.GuardianInt != skillGetRow.tm_guardian)
                {
                    continue;
                }

                // 所持スキル
                if (skillGetRow.tm_base_skill > 0)
                {
                    if (!Mine.CheckHaveSkill(skillGetRow.tm_base_skill))
                    {
                        continue;
                    }
                }

                if (Mine.AddSkill(ContinueMainRow.getting_private_skill, false))
                {
                    Mine.LevelUpPoint -= 1;
                    if (Mine.LevelUpPoint < 0) { Mine.LevelUpPoint = 0; }
                    LibPlayerMemo.AddSystemMessage(Mine.EntryNo, Status.PlayerSysMemoType.PrivateSkill, "スキル：" + LibResultText.CSSEscapeSkill(LibSkill.GetSkillName(ContinueMainRow.getting_private_skill)) + "を習得！", Status.MessageLevel.Normal);

                    EffectListEntity.effect_listDataTable EffectTable = LibSkill.GetEffectTable(ContinueMainRow.getting_private_skill);
                    if (EffectTable.FindByeffect_id(2117) != null)
                    {
                        // レベルゲイン
                        if (LibConst.LevelLimit > Mine.Level)
                        {
                            Mine.Exp += Mine.MaxExpNext;
                            Mine.LevelPlus();
                            LibPlayerMemo.AddSystemMessage(Mine.EntryNo, Status.PlayerSysMemoType.PrivateSkill, "→スキル「" + LibResultText.CSSEscapeSkill(LibSkill.GetSkillName(ContinueMainRow.getting_private_skill)) + "」の効果により、SPが" + 1 + "ポイントアップ！", Status.MessageLevel.Normal);
                        }
                    }
                }
            }
        }
    }
}
