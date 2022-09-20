using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary.DataFormat.Entity;

namespace CommonLibrary.Character
{
    public partial class LibGuest : LibUnitBase
    {
        /// <summary>
        /// ユニーク能力名
        /// </summary>
        public string UniqueName = "";

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
        /// インストールクラスのレベル
        /// </summary>
        public int InstallClassLevel
        {
            get
            {
                return Level;
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
        /// サブインストールクラスのレベル
        /// </summary>
        public int SecondryInstallClassLevel
        {
            get
            {
                return Level;
            }
        }

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
        /// キャラクターの種族
        /// </summary>
        public override string RaceName
        {
            get
            {
                return LibRace.GetRaceName(_race);
            }
        }

        /// <summary>
        /// オプションリスト
        /// </summary>
        private string OptionList = "";

        /// <summary>
        /// 所持アイテム
        /// </summary>
        public GuestDataEntity.mt_guest_have_itemDataTable HaveItemS = new GuestDataEntity.mt_guest_have_itemDataTable();

        private int CustomAtk = 0;
        private int CustomDfe = 0;
        private int CustomMgr = 0;
    }
}
