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
        // アカウント関連設定

        /// <summary>
        /// 未継続回数
        /// </summary>
        public int ContinueNoCount = 0;

        /// <summary>
        /// 連続継続回数
        /// </summary>
        public int ContinueBonus = 0;

        /// <summary>
        /// アカウントステータス
        /// </summary>
        public int AccountStatus = 0;

        /// <summary>
        /// 新規登録かどうか
        /// </summary>
        public bool IsNewPlayer = false;

        /// <summary>
        /// 最終継続日時
        /// </summary>
        public DateTime LastUpdate = DateTime.Now;

        // ここまで

        /// <summary>
        /// 新規登録時の更新回数
        /// </summary>
        public int NewPlayRegistUpdate = 0;

        private int _sex = Status.Sex.Male;

        public int Sex
        {
            get
            {
                return _sex;
            }
        }

        /// <summary>
        /// キャラクターの性別
        /// </summary>
        public string SexName
        {
            get
            {
                return LibConst.GetSexName(_sex);
            }
        }

        /// <summary>
        /// キャラクターの年齢
        /// </summary>
        public int Age = 0;

        /// <summary>
        /// 身長
        /// </summary>
        public int Height = 0;

        /// <summary>
        /// 体重
        /// </summary>
        public int Weight = 0;

        private int _nation = 0;

        /// <summary>
        /// キャラクターの所属国家
        /// </summary>
        public string NationName
        {
            get
            {
                return LibArea.GetNationName(_nation);
            }
        }

        /// <summary>
        /// キャラクターの種族
        /// </summary>
        public override string RaceName
        {
            get
            {
                return LibRace.GetRaceName(_race);
            }
        }

        private int _guardian = 0;

        /// <summary>
        /// キャラクターの守護者
        /// </summary>
        public string Guardian
        {
            get
            {

                return LibGuardian.GetName(_guardian);
            }
        }

        /// <summary>
        /// 守護者(数値)
        /// </summary>
        public int GuardianInt
        {
            get
            {
                return _guardian;
            }
        }

        private int _have_money = 0;

        /// <summary>
        /// 所持金額
        /// </summary>
        public int HaveMoney
        {
            get
            {
                return _have_money;
            }
            set
            {
                _have_money = value;
                if (_have_money > 99999999) { _have_money = 99999999; }
                else if (_have_money < 0) { _have_money = 0; }
            }
        }

        private int _blaze_chip = 0;

        /// <summary>
        /// ブレイズチップ
        /// </summary>
        public int BlazeChip
        {
            get
            {
                return _blaze_chip;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("ブレイズチップはマイナスにはなりません。");
                }

                _blaze_chip = value;
                if (_blaze_chip > 9999) { _blaze_chip = 9999; }
            }
        }

        /// <summary>
        /// キャラクタープロフィール
        /// </summary>
        public string Profile = "";

        /// <summary>
        /// データ有効性
        /// </summary>
        public bool IsValid = true;

        // 最大所持数など

        private int _maxHaveItem = 50;

        /// <summary>
        /// 最大所持可能アイテム数
        /// </summary>
        public int MaxHaveItem
        {
            get
            {
                int Max = _maxHaveItem;

                EffectListEntity.effect_listRow EffectRow = this.EffectList.FindByeffect_id(2115);
                if (EffectRow != null)
                {
                    Max += (int)EffectRow.rank;
                }

                return Max;
            }
        }

        /// <summary>
        /// 最大出品可能アイテム数
        /// </summary>
        public int MaxBazzerItem = 7;

        /// <summary>
        /// インストールクラスの変更有無
        /// </summary>
        public bool IsInstallClassChanging = false;

        /// <summary>
        /// レベルアップによるスキル習得回数
        /// </summary>
        public int LevelUpPoint = 0;

        /// <summary>
        /// ユニーク能力名
        /// </summary>
        public string UniqueName = "";

        // 戦闘系

        /// <summary>
        /// 素のキャラクターレベル
        /// </summary>
        public int LevelNormal
        {
            get
            {
                return _level;
            }
        }

        /// <summary>
        /// インストール中のインストールクラスID
        /// </summary>
        public int IntallClassID = 0;

        /// <summary>
        /// キャラクターのインストールクラス
        /// </summary>
        public string IntallClassName
        {
            get
            {
                return LibInstall.GetInstallName(IntallClassID);
            }
        }

        /// <summary>
        /// インストールクラスの素のレベル
        /// </summary>
        public int InstallClassLevelNormal
        {
            get
            {
                return GetInstallClassLevel(IntallClassID);
            }
        }

        /// <summary>
        /// インストールクラスのレベル
        /// </summary>
        public int InstallClassLevel
        {
            get
            {
                int insLevel = GetInstallClassLevel(IntallClassID);

                // レベル制限
                {
                    CharacterStatusListEntity.status_dataRow row = StatusEffect.Find(955);

                    if (row != null && insLevel > row.rank)
                    {
                        return (int)row.rank;
                    }
                }

                return insLevel;
            }
            set
            {
                CommonUnitDataEntity.install_level_listRow InstallRow = InstallClassList.FindByinstall_id(IntallClassID);
                InstallRow.level = value;
            }
        }

        /// <summary>
        /// インストールクラスのEXP
        /// </summary>
        public int InstallClassExp
        {
            get
            {
                CommonUnitDataEntity.install_level_listRow InstallRow = InstallClassList.FindByinstall_id(IntallClassID);
                return InstallRow.exp;
            }
            set
            {
                CommonUnitDataEntity.install_level_listRow InstallRow = InstallClassList.FindByinstall_id(IntallClassID);
                InstallRow.exp = value;
            }
        }

        /// <summary>
        /// インストールクラスのNEXP
        /// </summary>
        public int InstallClassNExp
        {
            get
            {
                return InstallClassMaxExp - InstallClassExp;
            }
        }

        /// <summary>
        /// インストールクラスのMaxEXP
        /// </summary>
        public int InstallClassMaxExp
        {
            get
            {
                return LibExperience.GetMaxExp(InstallClassLevelNormal);
            }
        }

        /// <summary>
        /// インストールクラスのEXPNext
        /// </summary>
        public int InstallClassExpNext
        {
            get
            {
                if (InstallClassLevel > 1)
                {
                    return InstallClassExp - LibExperience.GetMaxExp(InstallClassLevelNormal - 1);
                }
                else
                {
                    return InstallClassExp;
                }
            }
        }

        /// <summary>
        /// インストールクラスのMaxEXP
        /// </summary>
        public int InstallClassMaxExpNext
        {
            get
            {
                if (InstallClassLevel > 1)
                {
                    return LibExperience.GetMaxExp(InstallClassLevel) - LibExperience.GetMaxExp(InstallClassLevelNormal - 1);
                }
                else
                {
                    return LibExperience.GetMaxExp(InstallClassLevel);
                }
            }
        }

        /// <summary>
        /// インストール中のサブインストールクラスID
        /// </summary>
        public int SecondryIntallClassID = 0;

        /// <summary>
        /// キャラクターのサブインストールクラス
        /// </summary>
        public string SecondryIntallClassName
        {
            get
            {
                return LibInstall.GetInstallName(SecondryIntallClassID);
            }
        }

        /// <summary>
        /// サブインストールクラスの素のレベル
        /// </summary>
        public int SecondryInstallClassLevelNormal
        {
            get
            {
                return GetInstallClassLevel(SecondryIntallClassID);
            }
            set
            {
                CommonUnitDataEntity.install_level_listRow InstallRow = InstallClassList.FindByinstall_id(SecondryIntallClassID);
                InstallRow.level = value;
            }
        }

        /// <summary>
        /// サブインストールクラスのレベル
        /// </summary>
        public int SecondryInstallClassLevel
        {
            get
            {
                int insLevel = GetInstallClassLevel(SecondryIntallClassID);

                if (insLevel > InstallClassLevel / 2)
                {
                    insLevel = InstallClassLevel / 2;
                }

                // レベル制限
                {
                    CharacterStatusListEntity.status_dataRow row = StatusEffect.Find(955);

                    if (row != null && insLevel > row.rank)
                    {
                        return (int)row.rank;
                    }
                }

                return insLevel;
            }
            set
            {
                CommonUnitDataEntity.install_level_listRow InstallRow = InstallClassList.FindByinstall_id(SecondryIntallClassID);
                InstallRow.level = value;
            }
        }

        /// <summary>
        /// サブインストールクラスのEXP
        /// </summary>
        public int SecondryInstallClassExp
        {
            get
            {
                CommonUnitDataEntity.install_level_listRow InstallRow = InstallClassList.FindByinstall_id(SecondryIntallClassID);
                return InstallRow.exp;
            }
            set
            {
                CommonUnitDataEntity.install_level_listRow InstallRow = InstallClassList.FindByinstall_id(SecondryIntallClassID);
                InstallRow.exp = value;
            }
        }

        /// <summary>
        /// サブインストールクラスのNEXP
        /// </summary>
        public int SecondryInstallClassNExp
        {
            get
            {
                return SecondryInstallClassMaxExp - SecondryInstallClassExp;
            }
        }

        /// <summary>
        /// サブインストールクラスのMaxEXP
        /// </summary>
        public int SecondryInstallClassMaxExp
        {
            get
            {
                return LibExperience.GetMaxExp(SecondryInstallClassLevelNormal);
            }
        }

        /// <summary>
        /// サブインストールクラスのEXPNext
        /// </summary>
        public int SecondryInstallClassExpNext
        {
            get
            {
                if (SecondryInstallClassLevel > 1)
                {
                    return SecondryInstallClassExp - LibExperience.GetMaxExp(SecondryInstallClassLevelNormal - 1);
                }
                else
                {
                    return SecondryInstallClassExp;
                }
            }
        }

        /// <summary>
        /// サブインストールクラスのMaxEXP
        /// </summary>
        public int SecondryInstallClassMaxExpNext
        {
            get
            {
                if (SecondryInstallClassLevel > 1)
                {
                    return LibExperience.GetMaxExp(SecondryInstallClassLevel) - LibExperience.GetMaxExp(SecondryInstallClassLevel - 1);
                }
                else
                {
                    return LibExperience.GetMaxExp(SecondryInstallClassLevel);
                }
            }
        }

        /// <summary>
        /// レベル数値を上昇させる
        /// </summary>
        public void LevelPlus()
        {
            _level++;
            if (_level % 2 == 0)
            {
                LevelUpPoint++;
            }
        }

        /// <summary>
        /// 最大経験値量
        /// </summary>
        public int MaxExp
        {
            get
            {
                return LibExperience.GetMaxExp(Level);
            }
        }

        /// <summary>
        /// 次のレベルまであと(累計で計算)
        /// </summary>
        public int NextExp
        {
            get
            {
                return MaxExp - Exp;
            }
        }

        /// <summary>
        /// EXPNext
        /// </summary>
        public int ExpNext
        {
            get
            {
                if (Level > 1)
                {
                    return Exp - LibExperience.GetMaxExp(Level - 1);
                }
                else
                {
                    return Exp;
                }
            }
        }

        /// <summary>
        /// MaxEXP
        /// </summary>
        public int MaxExpNext
        {
            get
            {
                if (Level > 1)
                {
                    return LibExperience.GetMaxExp(Level) - LibExperience.GetMaxExp(Level - 1);
                }
                else
                {
                    return LibExperience.GetMaxExp(Level);
                }
            }
        }

        #region レベルアップ能力

        /// <summary>
        /// レベルアップ時増加HP量
        /// </summary>
        public decimal LevelUpHP = 0;

        /// <summary>
        /// レベルアップ時増加MP量
        /// </summary>
        public decimal LevelUpMP = 0;

        /// <summary>
        /// レベルアップ時増加力量
        /// </summary>
        public decimal LevelUpSTR = 0;

        /// <summary>
        /// レベルアップ時増加敏捷量
        /// </summary>
        public decimal LevelUpAGI = 0;

        /// <summary>
        /// レベルアップ時増加魔力量
        /// </summary>
        public decimal LevelUpMAG = 0;

        /// <summary>
        /// レベルアップ時増加ユニーク量
        /// </summary>
        public decimal LevelUpUNQ = 0;

        #endregion

        /// <summary>
        /// 所持スキル
        /// </summary>
        public CommonUnitDataEntity.have_skill_listDataTable HaveSkill = new CommonUnitDataEntity.have_skill_listDataTable();

        /// <summary>
        /// クラス所持スキル
        /// </summary>
        public CommonSkillEntity.skill_listDataTable ClassHaveSkill = new CommonSkillEntity.skill_listDataTable();

        /// <summary>
        /// イベントフラグ
        /// </summary>
        public CommonUnitDataEntity.event_flagDataTable EventFlag = new CommonUnitDataEntity.event_flagDataTable();

        /// <summary>
        /// ペットキャラクターバトルID
        /// </summary>
        public int PetCharacterBattleID = 0;

        /// <summary>
        /// インストールクラス一覧
        /// </summary>
        public CommonUnitDataEntity.install_level_listDataTable InstallClassList = new CommonUnitDataEntity.install_level_listDataTable();

        /// <summary>
        /// クエスト一覧
        /// </summary>
        public CommonUnitDataEntity.quest_listDataTable QuestList = new CommonUnitDataEntity.quest_listDataTable();

        /// <summary>
        /// 貴重品一覧
        /// </summary>
        public CommonUnitDataEntity.key_item_listDataTable KeyItemList = new CommonUnitDataEntity.key_item_listDataTable();

        /// <summary>
        /// レコード
        /// </summary>
        public CharacterDataEntity.ts_character_recordDataTable Record = new CharacterDataEntity.ts_character_recordDataTable();

        /// <summary>
        /// 回避
        /// </summary>
        public override int AVD
        {
            get
            {
                // 回避特性があるか？
                if (EffectList.FindByeffect_id(845) != null)
                {
                    return 30;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// おたから入手
        /// </summary>
        public bool IsTresureGetting = false;

        /// <summary>
        /// 移動可能マークリスト（侵入済みならinstance=true）
        /// </summary>
        public CharacterDataEntity.ts_character_moving_markDataTable MovingOKMarks = new CharacterDataEntity.ts_character_moving_markDataTable();

        /// <summary>
        /// 入手経験値総量
        /// </summary>
        public int GetExp = 0;

        /// <summary>
        /// ファミリアの名前
        /// </summary>
        public string FamiliarName = "";

        /// <summary>
        /// 使用可能なアーツ・スキル
        /// </summary>
        public CommonSkillEntity.skill_listDataTable UsingSkillList = new CommonSkillEntity.skill_listDataTable();

        /// <summary>
        /// 消費した矢弾の数
        /// </summary>
        public int UsedAmmoCount = 0;

        /// <summary>
        /// 消費した矢弾のID
        /// </summary>
        public int UsedAmmoID = 0;
    }
}
