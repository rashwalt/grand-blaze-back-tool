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
        /// スキルを入手
        /// </summary>
        /// <param name="SkillID">スキル番号</param>
        /// <param name="IsScrolUse">スクロールなどによる習得か</param>
        /// <returns>成功したか否か</returns>
        public bool AddSkill(int SkillID, bool IsScrolUse)
        {
            bool IsOK = false;

            if (HaveSkill.FindBysk_num(SkillID) != null)
            {
                // すでに持っている場合、なにもしない
                IsOK = false;
            }
            else
            {
                // 持っていない場合、新たに設定する
                CommonUnitDataEntity.have_skill_listRow EditRow = HaveSkill.Newhave_skill_listRow();

                EditRow.sk_num = SkillID;
                EditRow._new = true;
                EditRow.sc_flg = IsScrolUse;

                HaveSkill.Addhave_skill_listRow(EditRow);

                IsOK = true;

                CommonSkillEntity.skill_listRow skillRow = LibSkill.GetSkillRow(SkillID);

                if (skillRow.sk_type == Status.SkillType.Support)
                {
                    LibEffect.SplitAdd(skillRow.sk_effect, ref EffectList, Status.EffectDiv.SupportSkill);
                }
            }

            return IsOK;

        }

        /// <summary>
        /// スキル所持チェック
        /// </summary>
        /// <param name="SkillNo">スキル番号</param>
        /// <returns>所持しているか</returns>
        public bool CheckHaveSkill(int SkillNo)
        {
            if (HaveSkill.FindBysk_num(SkillNo) == null)
            {
                return false;
            }

            return true;
        }
    }
}
