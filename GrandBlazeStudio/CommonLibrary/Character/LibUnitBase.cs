using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataFormat.Format;

namespace CommonLibrary.Character
{
    public abstract class LibUnitBase
    {
        /// <summary>
        /// エントリーNo
        /// </summary>
        public int EntryNo = 0;

        /// <summary>
        /// キャラクター所属(味方、敵など)
        /// </summary>
        public int PartyBelong = Status.Belong.Friend;

        /// <summary>
        /// キャラクター所属(味方、敵など)、元々の所属
        /// </summary>
        public int BasePartyBelong = Status.Belong.Friend;

        /// <summary>
        /// キャラクター所属詳細
        /// </summary>
        public int PartyBelongDetail = Status.BelongDetail.Normal;

        /// <summary>
        /// パーティ表記カラー
        /// </summary>
        public string PartyColor
        {
            get
            {
                if (PartyBelong == Status.Belong.Friend && PartyBelongDetail == Status.BelongDetail.Normal)
                {
                    return "friend";
                }
                else if (PartyBelong == Status.Belong.Friend && PartyBelongDetail != Status.BelongDetail.Normal)
                {
                    return "guest";
                }
                else
                {
                    return "enemy";
                }
            }
        }

        /// <summary>
        /// パーティ内の順序
        /// </summary>
        public int MemberNumber = 0;

        /// <summary>
        /// パーティ内順序アルファベット
        /// </summary>
        public string MemberNumberID
        {
            get
            {
                return MemberNumber.ToAlphabet();
            }
        }

        /// <summary>
        /// スペシャルスキルの実行フラグ
        /// </summary>
        public bool IsSpecialUsed = false;

        public int MonsterMulti = 0;

        private string _characterName = "";

        /// <summary>
        /// キャラクター名
        /// </summary>
        public string CharacterName
        {
            get
            {
                if (MonsterMulti > 0)
                {
                    return _characterName + nameAlaha[MonsterMulti];
                }
                else
                {
                    return _characterName;
                }
            }
            set
            {
                _characterName = value;
            }
        }

        private string _nickName = "";

        /// <summary>
        /// キャラクター愛称
        /// </summary>
        public string NickName
        {
            get
            {
                if (MonsterMulti > 0)
                {
                    return _nickName + nameAlaha[MonsterMulti];
                }
                else
                {
                    return _nickName;
                }
            }
            set
            {
                _nickName = value;
            }
        }

        private string[] nameAlaha = { "", "Ａ", "Ｂ", "Ｃ", "Ｄ", "Ｅ", "Ｆ", "Ｇ", "Ｈ", "Ｉ", "Ｊ", "Ｋ", "Ｌ", "Ｍ", "Ｎ", "Ｏ", "Ｐ", "Ｑ", "Ｒ", "Ｓ", "Ｔ", "Ｕ", "Ｖ", "Ｗ", "Ｘ", "Ｙ", "Ｚ" };

        protected string _ImageURL = "";

        /// <summary>
        /// キャラクターの画像URL
        /// </summary>
        public string ImageURL
        {
            set
            {
                _ImageURL = value;
            }
            get
            {
                if (_ImageURL == "http://") { return ""; }
                else
                {
                    return _ImageURL;
                }
            }
        }

        /// <summary>
        /// キャラクターの画像 横サイズ(px)
        /// </summary>
        public int ImageWidthSize = 0;

        /// <summary>
        /// キャラクターの画像 縦サイズ(px)
        /// </summary>
        public int ImageHeightSize = 0;

        protected string _ImageLinkURL = "";

        /// <summary>
        /// キャラクターの画像リンクURL
        /// </summary>
        public string ImageLinkURL
        {
            set
            {
                _ImageLinkURL = value;
            }
            get
            {
                if (_ImageLinkURL == "http://") { return ""; }
                else
                {
                    return _ImageLinkURL;
                }
            }
        }

        /// <summary>
        /// キャラクターの画像著作権者
        /// </summary>
        public string ImageCopyright = "";

        /// <summary>
        /// アイコンリスト
        /// </summary>
        public CommonUnitDataEntity.icon_listDataTable IconList = new CommonUnitDataEntity.icon_listDataTable();

        /// <summary>
        /// アイコンURL取得
        /// </summary>
        /// <param name="IconID">アイコンID</param>
        /// <param name="IconSize">アイコンのサイズ</param>
        /// <returns>アイコンURL(タグいり)</returns>
        public string GetIconUrl(int IconID, int IconSize)
        {
            CommonUnitDataEntity.icon_listRow IconRow = IconList.FindByicon_id(IconID);

            if (IconRow == null || IconRow.icon_url == "http://")
            {
                return "";
            }

            StringBuilder IconUrl = new StringBuilder();
            IconUrl.Append("<img ");
            IconUrl.Append("src=\"" + IconRow.icon_url + "\" ");
            IconUrl.Append("alt=\"(C)" + IconRow.icon_copyright + "\" ");

            switch (IconSize)
            {
                case Status.IconSize.S:
                    IconUrl.Append("width=\"32\" height=\"32\" ");
                    break;
                case Status.IconSize.M:
                    IconUrl.Append("width=\"48\" height=\"48\" ");
                    break;
                case Status.IconSize.L:
                    IconUrl.Append("width=\"64\" height=\"64\" ");
                    break;
            }

            IconUrl.Append("class=\"ch_icon\" />");

            return IconUrl.ToString();
        }

        protected int _race = Status.Race.Hume;

        /// <summary>
        /// キャラクターの種族
        /// </summary>
        public virtual string RaceName
        {
            get
            {
                return LibRace.GetRaceName(_race);
            }
        }

        /// <summary>
        /// キャラクターの種族(数値)
        /// </summary>
        public int Race
        {
            get
            {
                return _race;
            }
        }

        /// <summary>
        /// 種族カテゴリ
        /// </summary>
        public int Category = Status.Category.Human;

        /// <summary>
        /// 種族カテゴリ名
        /// </summary>
        public string CategoryName
        {
            get
            {
                return LibMonsterData.GetCategoryName(Category);
            }
        }

        // 戦闘系

        public int FormationDefault = 0;

        protected int _formation = 0;

        /// <summary>
        /// 隊列
        /// </summary>
        public int Formation
        {
            get
            {
                return _formation;
            }
            set
            {
                _formation = value;
                if (_formation < 0) { _formation = 0; }
                else if (_formation > 1) { _formation = 1; }
                _battle_formation = _formation;
            }
        }

        protected int _battle_formation = 0;

        /// <summary>
        /// 戦闘隊列
        /// </summary>
        public int BattleFormation
        {
            get
            {
                return _battle_formation;
            }
            set
            {
                _battle_formation = value;
                if (_battle_formation < 0) { _battle_formation = 0; }
                else if (_battle_formation > 1) { _battle_formation = 1; }
            }
        }

        /// <summary>
        /// 隊列（日本語名）
        /// </summary>
        public string FormationName
        {
            get
            {
                switch (_battle_formation)
                {
                    case Status.Formation.Foward:
                        return "前列";
                    default:
                        return "後列";
                }
            }
        }

        protected int _level = 1;

        /// <summary>
        /// レベル(キャラクター)
        /// </summary>
        public int Level
        {
            get
            {
                // レベル制限
                {
                    CharacterStatusListEntity.status_dataRow row = StatusEffect.Find(955);

                    if (row != null && _level > row.rank)
                    {
                        return (int)row.rank;
                    }
                }

                return _level;
            }
        }

        protected int _exp = 0;

        /// <summary>
        /// 経験値
        /// </summary>
        public int Exp
        {
            get
            {
                return _exp;
            }
            set
            {
                _exp = value;
            }
        }

        protected int _now_hp = 0;

        /// <summary>
        /// 現在のＨＰ
        /// </summary>
        public int HPNow
        {
            get
            {
                return _now_hp;
            }
            set
            {
                if (StatusEffect.Check(11))
                {
                    if (value < _now_hp)
                    {
                        _now_hp = value;
                        if (_now_hp < 0) { _now_hp = 0; }
                        else if (_now_hp > HPMax) { _now_hp = HPMax; }
                    }
                }
                else
                {
                    _now_hp = value;
                    if (_now_hp < 0) { _now_hp = 0; }
                    else if (_now_hp > HPMax) { _now_hp = HPMax; }
                }
            }
        }

        protected decimal _max_hp = 0;// キャラクターの本来の最大ＨＰ（アイテムなどによる成長含む）

        /// <summary>
        /// 最大ＨＰ（基本値）
        /// </summary>
        public int HPMaxBase
        {
            get
            {
                int base_p = (int)_max_hp;

                return base_p;
            }
        }

        /// <summary>
        /// 最大ＨＰ
        /// </summary>
        public int HPMax
        {
            get
            {
                // 病気
                if (StatusEffect.Check(11))
                {
                    if (HPNow <= 0) { return 1; }
                    else { return HPNow; }
                }

                return HPMaxBase + HPMaxPlus;
            }
        }

        /// <summary>
        /// 最大ＨＰ増加分
        /// </summary>
        public int HPMaxPlus
        {
            get
            {
                int base_p = HPMaxBase;

                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(760);

                    if (row != null)
                    {
                        base_p += (int)row.rank;
                    }
                }

                // 相手パーティメンバーによるHP増加
                if (TargetPartyCount > 1)
                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(1335);

                    if (row != null)
                    {
                        base_p += (TargetPartyCount - 1) * base_p;
                    }
                }

                // 食事効果によるアップ
                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(1320);
                    if (row != null)
                    {
                        decimal PercentPlus = row.rank;

                        int PlusPoint = (int)((decimal)base_p * PercentPlus / 100m);

                        EffectListEntity.effect_listRow addinRow = EffectList.FindByeffect_id(1325);
                        if (addinRow != null && PlusPoint > addinRow.rank)
                        {
                            PlusPoint = (int)addinRow.rank;
                        }

                        base_p += PlusPoint;
                    }
                }

                return base_p - HPMaxBase;
            }
        }

        /// <summary>
        /// ＨＰ損傷率
        /// </summary>
        public int HPDamageRate
        {
            get
            {
                return (int)((decimal)HPNow / (decimal)HPMax * 100.0M);
            }
        }

        /// <summary>
        /// ＨＰ損傷率
        /// </summary>
        public int HPDamageRateWithHeal
        {
            get
            {
                return (int)(((decimal)HPNow + (decimal)ReceivedHeal) / (decimal)HPMax * 100.0M);
            }
        }

        /// <summary>
        /// 装飾されたHP/MaxHP数値
        /// </summary>
        public string DecorateHP
        {
            get
            {
                StringBuilder val = new StringBuilder();

                if (HPDamageRate == 0)
                {
                    val.Append("<span class=\"hp_die\">");
                }
                else if (HPDamageRate <= 50)
                {
                    val.Append("<span class=\"hp_caution\">");
                }
                else
                {
                    val.Append("<span class=\"hp_normal\">");
                }
                val.Append(HPNow + "/" + HPMax);
                val.Append("</span>");

                return val.ToString();
            }
        }

        protected int _now_mp = 0;

        /// <summary>
        /// 現在のＭＰ
        /// </summary>
        public int MPNow
        {
            get
            {
                return _now_mp;
            }
            set
            {
                _now_mp = value;
                if (_now_mp < 0) { _now_mp = 0; }
                else if (_now_mp > MPMax) { _now_mp = MPMax; }
            }
        }

        protected decimal _max_mp = 0;

        /// <summary>
        /// 最大ＭＰ（基本値）
        /// </summary>
        public int MPMaxBase
        {
            get
            {
                return (int)_max_mp;
            }
        }

        /// <summary>
        /// 最大ＭＰ
        /// </summary>
        public int MPMax
        {
            get
            {
                return MPMaxBase + MPMaxPlus;
            }
        }

        /// <summary>
        /// 最大ＭＰ増加分
        /// </summary>
        public int MPMaxPlus
        {
            get
            {
                int base_p = MPMaxBase;

                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(761);

                    if (row != null)
                    {
                        base_p += (int)row.rank;
                    }
                }

                // 食事効果によるアップ
                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(1321);
                    if (row != null)
                    {
                        decimal PercentPlus = row.rank;

                        int PlusPoint = (int)((decimal)base_p * PercentPlus / 100m);

                        EffectListEntity.effect_listRow addinRow = EffectList.FindByeffect_id(1326);
                        if (addinRow != null && PlusPoint > addinRow.rank)
                        {
                            PlusPoint = (int)addinRow.rank;
                        }

                        base_p += PlusPoint;
                    }
                }

                return base_p - MPMaxBase;
            }
        }

        /// <summary>
        /// ＭＰ損傷率
        /// </summary>
        public int MPDamageRate
        {
            get
            {
                return (int)((decimal)MPNow / (decimal)MPMax * 100.0M);
            }
        }

        /// <summary>
        /// 装飾されたMP/MaxMP数値
        /// </summary>
        public string DecorateMP
        {
            get
            {
                StringBuilder val = new StringBuilder();

                if (MPDamageRate == 0)
                {
                    val.Append("<span class=\"mp_die\">");
                }
                else if (MPDamageRate <= 50)
                {
                    val.Append("<span class=\"mp_caution\">");
                }
                else
                {
                    val.Append("<span class=\"mp_normal\">");
                }
                val.Append(MPNow + "/" + MPMax);
                val.Append("</span>");

                return val.ToString();
            }
        }

        protected int _now_tp = 0;

        /// <summary>
        /// 現在のＴＰ
        /// </summary>
        public int TPNow
        {
            get
            {
                return _now_tp;
            }
            set
            {
                _now_tp = value;
                if (_now_tp < 0) { _now_tp = 0; }
                else if (_now_tp > TPMax) { _now_tp = TPMax; }
            }
        }

        /// <summary>
        /// 最大ＴＰ
        /// </summary>
        public int TPMax
        {
            get
            {
                return Status.TP.Max + TPMaxPlus;
            }
        }

        /// <summary>
        /// 最大ＴＰ増加分
        /// </summary>
        public int TPMaxPlus
        {
            get
            {
                int base_p = Status.TP.Max;

                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(762);

                    if (row != null)
                    {
                        base_p += (int)row.rank;
                    }
                }

                // 食事効果によるアップ
                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(1322);
                    if (row != null)
                    {
                        decimal PercentPlus = row.rank;

                        int PlusPoint = (int)((decimal)base_p * PercentPlus / 100m);

                        EffectListEntity.effect_listRow addinRow = EffectList.FindByeffect_id(1327);
                        if (addinRow != null && PlusPoint > addinRow.rank)
                        {
                            PlusPoint = (int)addinRow.rank;
                        }

                        base_p += PlusPoint;
                    }
                }

                return base_p - Status.TP.Max;
            }
        }

        /// <summary>
        /// ＴＰ損傷率
        /// </summary>
        public int TPDamageRate
        {
            get
            {
                return (int)((decimal)TPNow / (decimal)TPMax * 100.0M);
            }
        }

        /// <summary>
        /// 装飾されたTP/MaxTP数値
        /// </summary>
        public string DecorateTP
        {
            get
            {
                StringBuilder val = new StringBuilder();

                //if (TPDamageRate == 0)
                //{
                //    val.Append("<span class=\"tp_die\">");
                //}
                //else if (TPDamageRate <= 50)
                //{
                //    val.Append("<span class=\"tp_caution\">");
                //}
                //else
                //{
                //    val.Append("<span class=\"tp_normal\">");
                //}
                val.Append("<span class=\"tp_normal\">");
                val.Append(TPNow + "/" + TPMax);
                val.Append("</span>");

                return val.ToString();
            }
        }

        protected decimal _str = 0;

        /// <summary>
        /// 力（基本値）
        /// </summary>
        public int STRBase
        {
            get
            {
                return (int)_str;
            }
        }

        /// <summary>
        /// 力
        /// </summary>
        public int STR
        {
            get
            {
                return STRBase + STRPlus;
            }
        }

        /// <summary>
        /// 増加分力
        /// </summary>
        public int STRPlus
        {
            get
            {
                int base_p = STRBase;

                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(740);

                    if (row != null)
                    {
                        base_p += (int)row.rank;
                    }
                }

                return base_p - STRBase;
            }
        }

        protected decimal _agi = 0;

        /// <summary>
        /// 敏捷（基本値）
        /// </summary>
        public int AGIBase
        {
            get
            {
                return (int)_agi;
            }
        }

        /// <summary>
        /// 敏捷
        /// </summary>
        public int AGI
        {
            get
            {
                return AGIBase + AGIPlus;
            }
        }

        /// <summary>
        /// 増加分敏捷
        /// </summary>
        public int AGIPlus
        {
            get
            {
                int base_p = AGIBase;

                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(742);

                    if (row != null)
                    {
                        base_p += (int)row.rank;
                    }
                }

                return base_p - AGIBase;
            }
        }

        protected decimal _mag = 0;

        /// <summary>
        /// 魔力（基本値）
        /// </summary>
        public int MAGBase
        {
            get
            {
                return (int)_mag;
            }
        }

        /// <summary>
        /// 魔力
        /// </summary>
        public int MAG
        {
            get
            {
                return MAGBase + MAGPlus;
            }
        }

        /// <summary>
        /// 増加分魔力
        /// </summary>
        public int MAGPlus
        {
            get
            {
                int base_p = MAGBase;

                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(743);

                    if (row != null)
                    {
                        base_p += (int)row.rank;
                    }
                }

                return base_p - MAGBase;
            }
        }

        protected decimal _unq = 0;

        /// <summary>
        /// ユニーク（基本値）
        /// </summary>
        public int UNQBase
        {
            get
            {
                return (int)_unq;
            }
        }

        /// <summary>
        /// ユニーク
        /// </summary>
        public int UNQ
        {
            get
            {
                return UNQBase + UNQPlus;
            }
        }

        /// <summary>
        /// 増加分ユニーク
        /// </summary>
        public int UNQPlus
        {
            get
            {
                int base_p = UNQBase;

                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(745);

                    if (row != null)
                    {
                        base_p += (int)row.rank;
                    }
                }

                return base_p - UNQBase;
            }
        }

        protected int _atk = 0;

        /// <summary>
        /// 物理攻撃力
        /// </summary>
        public int ATK
        {
            get
            {
                return GetATK(_atk);
            }
        }

        protected int _sub_atk = 0;

        /// <summary>
        /// 物理攻撃力(サブ)
        /// </summary>
        public int ATKSub
        {
            get
            {
                return GetATK(_sub_atk);
            }
        }

        /// <summary>
        /// ATK計算
        /// </summary>
        /// <param name="base_p">ベース</param>
        /// <returns></returns>
        private int GetATK(int base_p)
        {
            if (base_p == 0) { return 0; }

            {
                EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(2105);
                if (row != null)
                {
                    // ブラッディインパクト
                    base_p += (int)Math.Round((decimal)base_p * 0.05m, MidpointRounding.AwayFromZero);
                }
            }

            {
                EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(5503);

                if (row != null && EffectList.FindByeffect_id(2118) != null)
                {
                    // 騎乗：物理攻撃力アップ
                    base_p += (int)row.rank;
                }
            }

            {
                EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(750);

                if (row != null)
                {
                    base_p += (int)row.rank;
                }
            }

            {
                CharacterStatusListEntity.status_dataRow row = StatusEffect.Find(201);
                if (row != null)
                {
                    // 勇猛のミンネザンク
                    base_p += (int)((row.rank + 1m) * 0.05m);
                }
            }

            {
                CharacterStatusListEntity.status_dataRow row = StatusEffect.Find(230);
                if (row != null)
                {
                    // ダンス・ポルカ
                    base_p -= (int)((row.rank + 1m) * 0.05m);
                }
            }

            if (StatusEffect.Check(266))
            {
                // ビーストフォーム
                base_p += (int)((decimal)base_p * 0.2m);
            }

            if (StatusEffect.Check(80))
            {
                // バーサーク
                base_p += (int)((decimal)base_p * 0.25m);
            }

            if (StatusEffect.Check(86))
            {
                // ラストリゾート
                base_p += (int)((decimal)base_p * 0.35m);
            }

            if (StatusEffect.Check(263))
            {
                // オーバードライブ
                base_p += (int)((decimal)base_p * 0.5m);
            }

            // 食事効果によるアップ
            {
                EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(1300);
                if (row != null)
                {
                    decimal PercentPlus = row.rank;

                    int PlusPoint = (int)((decimal)base_p * PercentPlus / 100m);

                    EffectListEntity.effect_listRow addinRow = EffectList.FindByeffect_id(1310);
                    if (addinRow != null && PlusPoint > addinRow.rank)
                    {
                        PlusPoint = (int)addinRow.rank;
                    }

                    base_p += PlusPoint;
                }
            }

            return base_p;
        }

        /// <summary>
        /// 物理防御力
        /// </summary>
        public int DFE
        {
            get
            {
                int base_p = _armorDFE;

                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(753);

                    if (row != null)
                    {
                        base_p += (int)row.rank;
                    }
                }

                {
                    CharacterStatusListEntity.status_dataRow row = StatusEffect.Find(200);
                    if (row != null)
                    {
                        // 防壁のプロローグ
                        base_p += (int)((row.rank + 1m) * 0.05m);
                    }
                }

                {
                    CharacterStatusListEntity.status_dataRow row = StatusEffect.Find(231);
                    if (row != null)
                    {
                        // ダンス・レンゲ
                        base_p -= (int)((row.rank + 1m) * 0.05m);
                    }
                }

                if (StatusEffect.Check(80))
                {
                    // バーサーク
                    base_p -= (int)((decimal)base_p * 0.25m);
                }

                if (StatusEffect.Check(86))
                {
                    // ラストリゾート
                    base_p -= (int)((decimal)base_p * 0.35m);
                }

                if (StatusEffect.Check(263))
                {
                    // オーバードライブ
                    base_p += (int)((decimal)base_p * 0.5m);
                }

                // 食事効果によるアップ
                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(1303);
                    if (row != null)
                    {
                        decimal PercentPlus = row.rank;

                        int PlusPoint = (int)((decimal)base_p * PercentPlus / 100m);

                        EffectListEntity.effect_listRow addinRow = EffectList.FindByeffect_id(1313);
                        if (addinRow != null && PlusPoint > addinRow.rank)
                        {
                            PlusPoint = (int)addinRow.rank;
                        }

                        base_p += PlusPoint;
                    }
                }

                return base_p;
            }
        }

        /// <summary>
        /// 魔法防御力
        /// </summary>
        public int MGR
        {
            get
            {
                int base_p = _armorMGR;

                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(754);

                    if (row != null)
                    {
                        base_p += (int)row.rank;
                    }
                }

                {
                    CharacterStatusListEntity.status_dataRow row = StatusEffect.Find(204);
                    if (row != null)
                    {
                        // 妖術のスレノディ
                        base_p += (int)((row.rank + 1m) * 0.05m);
                    }
                }

                {
                    CharacterStatusListEntity.status_dataRow row = StatusEffect.Find(233);
                    if (row != null)
                    {
                        // ダンス・クエーカ
                        base_p -= (int)((row.rank + 1m) * 0.05m);
                    }
                }

                if (StatusEffect.Check(263))
                {
                    // オーバードライブ
                    base_p += (int)((decimal)base_p * 0.5m);
                }

                // 食事効果によるアップ
                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(1304);
                    if (row != null)
                    {
                        decimal PercentPlus = row.rank;

                        int PlusPoint = (int)((decimal)base_p * PercentPlus / 100m);

                        EffectListEntity.effect_listRow addinRow = EffectList.FindByeffect_id(1314);
                        if (addinRow != null && PlusPoint > addinRow.rank)
                        {
                            PlusPoint = (int)addinRow.rank;
                        }

                        base_p += PlusPoint;
                    }
                }

                return base_p;
            }
        }

        protected int _avd = 0;

        /// <summary>
        /// 回避
        /// </summary>
        public virtual int AVD
        {
            get
            {
                int base_p = _avd;

                // ドッジ発動率アップ
                {
                    EffectListEntity.effect_listRow row = EffectList.FindByeffect_id(811);

                    if (row != null)
                    {
                        base_p += (int)row.rank;
                    }
                }

                return base_p;
            }
        }

        /// <summary>
        /// メインウェポン
        /// </summary>
        public WeaponAbility MainWeapon = new WeaponAbility();

        /// <summary>
        /// サブウェポン
        /// </summary>
        public WeaponAbility SubWeapon = new WeaponAbility();

        /// <summary>
        /// 物理防具性能
        /// </summary>
        protected int _armorDFE = 0;

        /// <summary>
        /// 魔法防具性能
        /// </summary>
        protected int _armorMGR = 0;

        /// <summary>
        /// 盾回避(物理)
        /// </summary>
        public int ShieldAvoidPhysical = 0;

        /// <summary>
        /// 盾回避(魔法)
        /// </summary>
        public int ShieldAvoidSorcery = 0;

        /// <summary>
        /// エフェクト
        /// </summary>
        public EffectListEntity.effect_listDataTable EffectList = new EffectListEntity.effect_listDataTable();

        /// <summary>
        /// 属性防御
        /// </summary>
        public Elemental DefenceElemental = new Elemental();

        /// <summary>
        /// 所持アイテム
        /// </summary>
        public CommonUnitDataEntity.have_item_listDataTable HaveItem = new CommonUnitDataEntity.have_item_listDataTable();

        /// <summary>
        /// バトルアクション
        /// </summary>
        public CommonUnitDataEntity.action_listDataTable ActionList = new CommonUnitDataEntity.action_listDataTable();

        /// <summary>
        /// セリフ一覧
        /// </summary>
        public CommonUnitDataEntity.serif_listDataTable SerifList = new CommonUnitDataEntity.serif_listDataTable();

        /// <summary>
        /// ステータス異常・変化
        /// </summary>
        public LibStatus StatusEffect = new LibStatus();

        /// <summary>
        /// チャージタイム
        /// </summary>
        public int ChargeTime = 0;

        /// <summary>
        /// 戦闘離脱（戦闘不能のときにたつ）
        /// </summary>
        public bool BattleOut = false;

        /// <summary>
        /// ヘイトリスト
        /// </summary>
        public LibHate HateList = new LibHate();

        /// <summary>
        /// バトルID
        /// </summary>
        public int BattleID = 0;

        /// <summary>
        /// 連携属性リスト
        /// </summary>
        public LibChain ChainList = new LibChain();

        /// <summary>
        /// 開始段階の実行内容
        /// </summary>
        public List<LibActionType> SelectedActions = new List<LibActionType>();

        /// <summary>
        /// 開始段階のターゲット
        /// </summary>
        public List<LibUnitBase> SelectedTarget = new List<LibUnitBase>();

        /// <summary>
        /// 前ターンのターゲット
        /// </summary>
        public List<LibUnitBase> OldSelectedTarget = new List<LibUnitBase>();

        /// <summary>
        /// コンパニオンキャラクターバトルID
        /// </summary>
        public int CompanionBattleID = 0;

        /// <summary>
        /// 戦闘不能処理
        /// </summary>
        public void Dead()
        {
            HPNow = 0;
            MPNow = 0;
            TPNow = 0;

            StatusEffect.Clear();
            StatusEffect.Add(1, 1, 0, -1, 200, true);
            BattleOut = true;

            TargetedCount = 0;
        }

        /// <summary>
        /// 復活処理
        /// </summary>
        /// <param name="CureRate">回復率</param>
        public void Raise(int CureRate)
        {
            StatusEffect.Clear();

            HPNow = (int)((decimal)HPMax * (decimal)CureRate / 100m);
            MPNow = (int)((decimal)MPMax * (decimal)CureRate / 100m);
            TPNow = 0;
            BattleOut = false;
        }

        /// <summary>
        /// 金属値
        /// </summary>
        public int Metal = 0;

        /// <summary>
        /// ターン中に行動したか。
        /// </summary>
        public bool IsActionEnd = false;

        /// <summary>
        /// バトルスコア（その一戦だけで使う）
        /// </summary>
        public BattleScore TempBattleScore = new BattleScore();

        /// <summary>
        /// ハイアンドローヒットカウント
        /// </summary>
        public int HighAndLowHitCount = 0;

        /// <summary>
        /// 被ターゲット数（ターゲットされている数）
        /// </summary>
        public int TargetedCount = 0;

        /// <summary>
        /// その戦闘での攻撃されていない状態での攻撃か？
        /// </summary>
        public bool IsFirstAttachAttack = true;

        /// <summary>
        /// リベンジクリティカル発生条件確立
        /// </summary>
        public bool IsRevengeCritical = false;

        /// <summary>
        /// ファーストタッチ判定
        /// </summary>
        public bool IsFirstTouth = true;

        /// <summary>
        /// トドメを刺した数
        /// </summary>
        public int DestroyCount = 0;

        /// <summary>
        /// 発動したアーツ名称
        /// </summary>
        public string UsedArtsName = "";

        /// <summary>
        /// 被回復見込み
        /// </summary>
        public int ReceivedHeal = 0;

        /// <summary>
        /// 被回復見込み(MP)
        /// </summary>
        public int ReceivedHealMP = 0;

        /// <summary>
        /// 被回復見込み(TP)
        /// </summary>
        public int ReceivedHealTP = 0;

        /// <summary>
        /// 被ステータス異常・変化見込み
        /// </summary>
        public LibStatus ReceivedStatusEffect = new LibStatus();

        /// <summary>
        /// 相手パーティの数
        /// --モンスター限定--
        /// </summary>
        public int TargetPartyCount = 0;

        /// <summary>
        /// 最後に使ったアーツのID
        /// </summary>
        public int LastUsingSkillID = 0;

        /// <summary>
        /// 同じアーツを使うと効果アップ系のカウント
        /// </summary>
        public int UsingArtsEffectLv = 0;
    }
}
