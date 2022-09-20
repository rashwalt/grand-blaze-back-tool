using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CommonLibrary
{
    /// <summary>
    /// 処理ステータス管理クラス
    /// </summary>
    public class Status
    {
        /// <summary>
        /// 共通ステータス
        /// </summary>
        public class Common
        {
            /// <summary>
            /// 成功
            /// </summary>
            public const int OK = 0;

            /// <summary>
            /// データが見つかりません
            /// </summary>
            public const int NotFound = 3;

            /// <summary>
            /// 引数エラー
            /// </summary>
            public const int ArgumentError = 255;
        }

        /// <summary>
        /// 装備関連処理ステータス
        /// </summary>
        public class Equip
        {
            /// <summary>
            /// 通常成功
            /// </summary>
            public const int OK = 0;

            /// <summary>
            /// 指定アイテム未所持エラー
            /// </summary>
            public const int NoHaveItemError = 1;

            /// <summary>
            /// 装備箇所不一致エラー
            /// </summary>
            public const int SpotError = 2;

            /// <summary>
            /// サブウェポンが解除された
            /// </summary>
            public const int RemoveSubWeapon = 3;

            /// <summary>
            /// メインが両手武器です
            /// </summary>
            public const int MainWeaponIsTwoHands = 5;

            /// <summary>
            /// サブが両手武器です
            /// </summary>
            public const int SubWeaponIsTwoHands = 6;

            /// <summary>
            /// メインに装備済み
            /// </summary>
            public const int MainEquiped = 7;

            /// <summary>
            /// サブに装備済み
            /// </summary>
            public const int SubEquiped = 8;

            /// <summary>
            /// 頭部に装備済み
            /// </summary>
            public const int HeadEquiped = 9;

            /// <summary>
            /// 胴体に装備済み
            /// </summary>
            public const int BodyEquiped = 10;

            /// <summary>
            /// アクセサリに装備済み
            /// </summary>
            public const int AccessoryEquiped = 15;

            /// <summary>
            /// メインに装備していない
            /// </summary>
            public const int MainWeaponIsNoEquiped = 19;

            /// <summary>
            /// 性別が異なるため装備できない
            /// </summary>
            public const int SexMatch = 22;

            /// <summary>
            /// 種族が異なる
            /// </summary>
            public const int RaceMatch = 23;

            /// <summary>
            /// サブに大盾装備中です
            /// </summary>
            public const int SubEquipGreatShield = 26;

            /// <summary>
            /// 装備できるレベルではない
            /// </summary>
            public const int NotLevel = 27;

            /// <summary>
            /// 装備できないインストールクラス
            /// </summary>
            public const int InstallMatch = 28;
        }

        /// <summary>
        /// 隊列関連
        /// </summary>
        public class Formation
        {
            /// <summary>
            /// 前衛
            /// </summary>
            public const int Foward = 0;

            /// <summary>
            /// 後衛
            /// </summary>
            public const int Backs = 1;
        }

        /// <summary>
        /// プレイヤー側種族(mt_race_listと連動するように注意)
        /// </summary>
        public class Race
        {
            /// <summary>
            /// ヒューム
            /// </summary>
            public const int Hume = 1;

            /// <summary>
            /// エルヴ
            /// </summary>
            public const int Elve = 2;

            /// <summary>
            /// ファルート
            /// </summary>
            public const int Falurt = 3;

            /// <summary>
            /// ライカンス
            /// </summary>
            public const int Lycanth = 4;

            /// <summary>
            /// バルタン
            /// </summary>
            public const int Bartan = 5;

            /// <summary>
            /// ドラクォ
            /// </summary>
            public const int Draqh = 6;
        }

        /// <summary>
        /// 性別
        /// </summary>
        public class Sex
        {
            /// <summary>
            /// ？(またはすべて)
            /// </summary>
            public const int Unknown = 0;

            /// <summary>
            /// 男
            /// </summary>
            public const int Male = 1;

            /// <summary>
            /// 女
            /// </summary>
            public const int Female = 2;
        }

        /// <summary>
        /// ランクタイプ名称
        /// </summary>
        public class Rank
        {
            /// <summary>
            /// 基本STR値成長
            /// </summary>
            public const string STR = "StatusSTR";

            /// <summary>
            /// 基本MAG値成長
            /// </summary>
            public const string MAG = "StatusMAG";

            /// <summary>
            /// 基本VIT値成長
            /// </summary>
            public const string VIT = "StatusVIT";

            /// <summary>
            /// 基本SPD値成長
            /// </summary>
            public const string SPD = "StatusSPD";

            /// <summary>
            /// ＨＰ成長
            /// </summary>
            public const string HP = "StatusHP";

            /// <summary>
            /// ＭＰ成長
            /// </summary>
            public const string MP = "StatusMP";
        }

        /// <summary>
        /// 所属クラス
        /// </summary>
        public class Belong
        {
            /// <summary>
            /// 味方
            /// </summary>
            public const int Friend = 0;

            /// <summary>
            /// 敵
            /// </summary>
            public const int Enemy = 1;
        }

        /// <summary>
        /// 所属詳細
        /// </summary>
        public class BelongDetail
        {
            /// <summary>
            /// 詳細区分：通常(味方はＰＣ、敵は雑魚モンスター)
            /// </summary>
            public const int Normal = 0;
            // LibMonster   : 通常モンスター（Belong==Friendの場合はあやつりモンスター）
            // LibPlayer    : プレイヤー
            // LibGuest     : ゲストキャラクター。
            // LibCompanion : コンパニオン

            /// <summary>
            /// 詳細区分：レア
            /// </summary>
            public const int Rare = 1;
            // LibMonster   : レアモンスター
            // LibPlayer    : None
            // LibGuest     : None
            // LibCompanion : None

            /// <summary>
            /// 詳細区分：ボス
            /// </summary>
            public const int Boss = 2;
            // LibMonster   : ボスモンスター
            // LibPlayer    : None
            // LibGuest     : None
            // LibCompanion : None

            /// <summary>
            /// 詳細区分：ペット
            /// </summary>
            public const int Animal = 3;
            // LibMonster   : None
            // LibPlayer    : None
            // LibGuest     : ペットモンスター
            // LibCompanion : None

            /// <summary>
            /// 詳細区分：精霊
            /// </summary>
            public const int Elemental = 4;
            // LibMonster   : None
            // LibPlayer    : None
            // LibGuest     : 精霊
            // LibCompanion : None

            /// <summary>
            /// 詳細区分：ファミリア
            /// </summary>
            public const int Familiar = 5;
            // LibMonster   : None
            // LibPlayer    : None
            // LibGuest     : ファミリア
            // LibCompanion : None
        }

        /// <summary>
        /// 射程
        /// </summary>
        public class Range
        {
            /// <summary>
            /// 近距離
            /// </summary>
            public const int Short = 0;

            /// <summary>
            /// 中距離
            /// </summary>
            public const int Middle = 1;

            /// <summary>
            /// 遠距離
            /// </summary>
            public const int Long = 2;
        }

        /// <summary>
        /// 属性テーブル種別
        /// </summary>
        public class ElementType
        {
            /// <summary>
            /// メイン攻撃
            /// </summary>
            public const int MainAttack = 0;

            /// <summary>
            /// サブ攻撃
            /// </summary>
            public const int SubAttack = 1;

            /// <summary>
            /// 防御属性
            /// </summary>
            public const int Deffence = 3;
        }

        /// <summary>
        /// 表示メッセージレベル(表示の色が変化する
        /// </summary>
        public class MessageLevel
        {
            /// <summary>
            /// 通常表記
            /// </summary>
            public const int Normal = 0;

            /// <summary>
            /// 警告表記
            /// </summary>
            public const int Caution = 1;

            /// <summary>
            /// エラー表記
            /// </summary>
            public const int Error = 2;

            /// <summary>
            /// レベル上昇
            /// </summary>
            public const int LevelUp = 4;
        }

        /// <summary>
        /// アカウント状態
        /// </summary>
        public class Account
        {
            /// <summary>
            /// 通常
            /// </summary>
            public const int Normal = 0;

            /// <summary>
            /// 凍結
            /// </summary>
            public const int Freeze = 1;

            /// <summary>
            /// メールアドレス不正
            /// </summary>
            public const int MailFailed = 2;

            /// <summary>
            /// 注意
            /// </summary>
            public const int YellowCard = 3;

            /// <summary>
            /// 警告
            /// </summary>
            public const int RedCard = 4;

            /// <summary>
            /// 強制退会処分
            /// </summary>
            public const int Out = 5;

            /// <summary>
            /// アカウントバン
            /// </summary>
            public const int Ban = 6;
        }

        /// <summary>
        /// アカウントステータス
        /// </summary>
        public class AccountStatus
        {
            /// <summary>
            /// アクティブ
            /// </summary>
            public const int Active = 0;

            /// <summary>
            /// フリーズ
            /// </summary>
            public const int Freeze = 1;

            /// <summary>
            /// 削除
            /// </summary>
            public const int Delete = 2;
        }

        /// <summary>
        /// メッセージ対象
        /// </summary>
        public class MessageTarget
        {
            /// <summary>
            /// プライベート
            /// </summary>
            public const int Private = 0;

            /// <summary>
            /// パーティ
            /// </summary>
            public const int Party = 1;

            /// <summary>
            /// リスト
            /// </summary>
            public const int List = 2;
        }

        /// <summary>
        /// 売却結果状態
        /// </summary>
        public class Sell
        {
            /// <summary>
            /// 通常成功
            /// </summary>
            public const int OK = 0;

            /// <summary>
            /// 所持していないアイテムを指定しました
            /// </summary>
            public const int NoHaveItem = 1;

            /// <summary>
            /// 装備中は売却できない
            /// </summary>
            public const int Equip = 2;

            /// <summary>
            /// バザーに出品中のものは売却できない
            /// </summary>
            public const int Bazzer = 3;
        }

        /// <summary>
        /// トレード結果状態
        /// </summary>
        public class Trade
        {
            /// <summary>
            /// 通常成功
            /// </summary>
            public const int OK = 0;

            /// <summary>
            /// 所持していないアイテムを指定しました
            /// </summary>
            public const int NoHaveItem = 1;

            /// <summary>
            /// 装備中はトレードできない
            /// </summary>
            public const int Equip = 2;

            /// <summary>
            /// アイテムをもてなかった
            /// </summary>
            public const int NoAddItem = 3;

            /// <summary>
            /// お金が足りない
            /// </summary>
            public const int NoMoney = 4;

            /// <summary>
            /// バインドアイテムだった
            /// </summary>
            public const int BindItem = 5;

            /// <summary>
            /// 自分にはトレードできない
            /// </summary>
            public const int NoMineTrade = 6;

            /// <summary>
            /// バザーに出品中のものはトレードできない
            /// </summary>
            public const int Bazzer = 7;

            /// <summary>
            /// 手数料を払えない
            /// </summary>
            public const int NoTaxHaveMoney = 8;

            /// <summary>
            /// 対象が存在しない
            /// </summary>
            public const int NoTarget = 11;
        }

        /// <summary>
        /// 装備箇所
        /// </summary>
        public class EquipSpot
        {
            /// <summary>
            /// メイン
            /// </summary>
            public const int Main = 1;

            /// <summary>
            /// サブ
            /// </summary>
            public const int Sub = 2;

            /// <summary>
            /// 頭部
            /// </summary>
            public const int Head = 3;

            /// <summary>
            /// 胴体
            /// </summary>
            public const int Body = 4;

            /// <summary>
            /// アクセサリ
            /// </summary>
            public const int Accesory = 5;
        }

        /// <summary>
        /// 購入処理
        /// </summary>
        public class Buy
        {
            /// <summary>
            /// 成功
            /// </summary>
            public const int OK = 0;

            /// <summary>
            /// アイテムが存在しない
            /// </summary>
            public const int NoItem = 1;

            /// <summary>
            /// ショップに販売していない
            /// </summary>
            public const int NoShop = 2;

            /// <summary>
            /// これ以上もてない
            /// </summary>
            public const int NoHaveItem = 3;

            /// <summary>
            /// お金がたりない
            /// </summary>
            public const int NoHaveMoney = 4;
        }

        /// <summary>
        /// インストール処理
        /// </summary>
        public class Install
        {
            /// <summary>
            /// 成功
            /// </summary>
            public const int OK = 0;

            /// <summary>
            /// 習得していない
            /// </summary>
            public const int NoGetting = 1;
        }

        /// <summary>
        /// エフェクト装備元区分
        /// </summary>
        public class EffectDiv
        {
            /// <summary>
            /// 防具
            /// </summary>
            public const int Deffence = 1;

            /// <summary>
            /// サポートスキル
            /// </summary>
            public const int SupportSkill = 2;

            /// <summary>
            /// 食事
            /// </summary>
            public const int Food = 3;
        }

        /// <summary>
        /// エフェクト追加上書き区分
        /// </summary>
        public class EffectFix
        {
            /// <summary>
            /// 固定
            /// </summary>
            public const int Const = 0;

            /// <summary>
            /// 追加
            /// </summary>
            public const int Add = 1;

            /// <summary>
            /// 上書
            /// </summary>
            public const int Paste = 2;
        }

        /// <summary>
        /// スキル合成
        /// </summary>
        public class CreationSkill
        {
            /// <summary>
            /// 成功
            /// </summary>
            public const int OK = 0;

            /// <summary>
            /// 指定されたスキルが重複している
            /// </summary>
            public const int ComboSkillsBad = 2;

            /// <summary>
            /// ベーススキルを所持していない
            /// </summary>
            public const int NoHaveBaseSkills = 3;

            /// <summary>
            /// アドイン１スキルを所持していない
            /// </summary>
            public const int NoHaveAddInSkills1 = 4;

            /// <summary>
            /// アドイン２スキルを所持していない
            /// </summary>
            public const int NoHaveAddInSkills2 = 5;

            /// <summary>
            /// アドイン３スキルを所持していない
            /// </summary>
            public const int NoHaveAddInSkills3 = 6;

            /// <summary>
            /// スキル合成の資格がない
            /// </summary>
            public const int NoHaveKeyItem = 7;
        }

        /// <summary>
        /// ダメージ算出タイプ
        /// </summary>
        public class DamageType
        {
            /// <summary>
            /// ダメージ算出なし
            /// </summary>
            public const int None = 0;

            /// <summary>
            /// 回復
            /// </summary>
            public const int Heal = 1;

            /// <summary>
            /// 物理吸収
            /// </summary>
            public const int PhysicalDrain = 2;

            /// <summary>
            /// 魔法吸収
            /// </summary>
            public const int MagicalDrain = 3;

            /// <summary>
            /// 物理ダメージ
            /// </summary>
            public const int PhysicalDamage = 4;

            /// <summary>
            /// 魔法ダメージ
            /// </summary>
            public const int MagicalDamage = 5;
        }

        /// <summary>
        /// ターゲット勢力
        /// </summary>
        public class TargetParty
        {
            /// <summary>
            /// 敵
            /// </summary>
            public const int Enemy = 1;

            /// <summary>
            /// 味方
            /// </summary>
            public const int Friend = 2;

            /// <summary>
            /// 自分
            /// </summary>
            public const int Mine = 3;

            /// <summary>
            /// ペット
            /// </summary>
            public const int Pet = 4;

            /// <summary>
            /// 敵味方すべて
            /// </summary>
            public const int All = 5;
        }


        /// <summary>
        /// ターゲット範囲
        /// </summary>
        public class TargetArea
        {
            /// <summary>
            /// 単体
            /// </summary>
            public const int Only = 1;

            /// <summary>
            /// 範囲１
            /// </summary>
            public const int Circle1 = 2;

            /// <summary>
            /// 範囲２
            /// </summary>
            public const int Circle2 = 3;

            /// <summary>
            /// 隊列
            /// </summary>
            public const int Line = 4;

            /// <summary>
            /// 全体
            /// </summary>
            public const int All = 5;

            /// <summary>
            /// モンスター
            /// </summary>
            public const int Monster = 6;

            /// <summary>
            /// 精霊
            /// </summary>
            public const int Elemental = 7;

            /// <summary>
            /// 使い魔
            /// </summary>
            public const int Papet = 8;
        }

        /// <summary>
        /// アイテム合成
        /// </summary>
        public class CreationItem
        {
            /// <summary>
            /// 通常成功
            /// </summary>
            public const int NormalQuality = 0;

            /// <summary>
            /// 高品質成功
            /// </summary>
            public const int HighQuality = 1;

            /// <summary>
            /// 難しすぎて合成できない
            /// </summary>
            public const int VeryDifficulty = 6;

            /// <summary>
            /// 素材がたりない
            /// </summary>
            public const int MaterialInsufficient = 7;

            /// <summary>
            /// 材料が間違っている
            /// </summary>
            public const int NotingRecipe = 9;

            /// <summary>
            /// アイテム所持数がいっぱい
            /// </summary>
            public const int MaxHaveItem = 11;

            /// <summary>
            /// 指定された追加材が不正
            /// </summary>
            public const int AddinError = 13;
        }

        /// <summary>
        /// 作成区分
        /// </summary>
        public class CreationDiv
        {
            /// <summary>
            /// 未所属
            /// </summary>
            public const int NoDiv = -1;

            /// <summary>
            /// 近接武器
            /// </summary>
            public const int ProximityWeapon = 0;

            /// <summary>
            /// 遠隔武器
            /// </summary>
            public const int RangeWeapon = 1;

            /// <summary>
            /// 防具（盾、アクセサリ以外）
            /// </summary>
            public const int ArmorProtection = 2;

            /// <summary>
            /// 盾
            /// </summary>
            public const int Shield = 3;

            /// <summary>
            /// 装飾品
            /// </summary>
            public const int Accessory = 4;

            /// <summary>
            /// 食品
            /// </summary>
            public const int Food = 5;
        }

        /// <summary>
        /// 行動内容設定
        /// </summary>
        public class ActionSetting
        {
            /// <summary>
            /// 完了
            /// </summary>
            public const int OK = 0;
        }

        /// <summary>
        /// 隊列設定
        /// </summary>
        public class FormationSetting
        {
            /// <summary>
            /// 完了
            /// </summary>
            public const int OK = 0;
        }

        /// <summary>
        /// セリフ設定
        /// </summary>
        public class SerifSetting
        {
            /// <summary>
            /// 完了
            /// </summary>
            public const int OK = 0;
        }

        /// <summary>
        /// イベント発生箇所
        /// </summary>
        public class EventPopType
        {
            /// <summary>
            /// 戦闘前
            /// </summary>
            public const int BattleBefore = 1;

            /// <summary>
            /// 戦闘後
            /// </summary>
            public const int BattleAfter = 2;
        }

        /// <summary>
        /// 特性種類
        /// </summary>
        public class SkillType
        {
            /// <summary>
            /// カウンター
            /// </summary>
            public const int Counter = -2;

            /// <summary>
            /// 通常攻撃
            /// </summary>
            public const int Normal = -1;

            /// <summary>
            /// アーツ
            /// </summary>
            public const int Arts = 0;

            /// <summary>
            /// パッシブ
            /// </summary>
            public const int Support = 1;

            /// <summary>
            /// アシスト
            /// </summary>
            public const int Assist = 2;

            /// <summary>
            /// スペシャル
            /// </summary>
            public const int Special = 3;
        }

        /// <summary>
        /// 習得タイプ
        /// </summary>
        public class GettingType
        {
            /// <summary>
            /// インストール
            /// </summary>
            public const int Install = 0;

            /// <summary>
            /// テクニック
            /// </summary>
            public const int Skill = 1;
        }

        /// <summary>
        /// 攻撃種別
        /// </summary>
        public class AttackType
        {
            /// <summary>
            /// 未定義
            /// </summary>
            public const int None = -1;

            /// <summary>
            /// 近接
            /// </summary>
            public const int Combat = 0;

            /// <summary>
            /// 遠隔
            /// </summary>
            public const int Shoot = 1;

            /// <summary>
            /// 魔法
            /// </summary>
            public const int Mystic = 2;

            /// <summary>
            /// 呪歌
            /// </summary>
            public const int Song = 3;

            /// <summary>
            /// 舞踏
            /// </summary>
            public const int Dance = 4;

            /// <summary>
            /// 忍術
            /// </summary>
            public const int Ninjutsu = 5;

            /// <summary>
            /// 道具
            /// </summary>
            public const int Item = 6;

            /// <summary>
            /// 召喚
            /// </summary>
            public const int Summon = 7;

            /// <summary>
            /// 魔法剣
            /// </summary>
            public const int MagicSword = 8;

            /// <summary>
            /// ブレス
            /// </summary>
            public const int Bless = 9;
        }

        /// <summary>
        /// 素材種別
        /// </summary>
        public class MaterialType
        {
            /// <summary>
            /// 宝箱
            /// </summary>
            public const int Tresure = 0;

            /// <summary>
            /// 採掘
            /// </summary>
            public const int Mining = 1;

            /// <summary>
            /// 伐採
            /// </summary>
            public const int Felling = 2;

            /// <summary>
            /// 採集
            /// </summary>
            public const int Collection = 3;

            /// <summary>
            /// 釣り
            /// </summary>
            public const int Fishing = 4;
        }

        /// <summary>
        /// 合成用素材種別
        /// </summary>
        public class CreateMaterialType
        {
            /// <summary>
            /// 通常
            /// </summary>
            public const int Normal = 0;

            /// <summary>
            /// 追加素材
            /// </summary>
            public const int Addin = 1;

            /// <summary>
            /// 基礎アイテム
            /// </summary>
            public const int Base = 2;
        }

        /// <summary>
        /// トレジャーオープン
        /// </summary>
        public class TresureOpen
        {
            /// <summary>
            /// 成功
            /// </summary>
            public const int OK = 0;

            /// <summary>
            /// 鍵の解錠失敗
            /// </summary>
            public const int KeyUnLockMiss = 1;

            /// <summary>
            /// トラップの解除失敗
            /// </summary>
            public const int TrapUnLockMiss = 2;

            /// <summary>
            /// 素材入手不可能な場所
            /// </summary>
            public const int NoMaterialArea = 3;

            /// <summary>
            /// 素材を入手できなかった
            /// </summary>
            public const int NoGetMaterial = 5;
        }

        /// <summary>
        /// ランクレベル
        /// </summary>
        public class RankLevel
        {
            /// <summary>
            /// A
            /// </summary>
            public const int A = 6;

            /// <summary>
            /// B
            /// </summary>
            public const int B = 5;

            /// <summary>
            /// C
            /// </summary>
            public const int C = 4;

            /// <summary>
            /// D
            /// </summary>
            public const int D = 3;

            /// <summary>
            /// E
            /// </summary>
            public const int E = 2;

            /// <summary>
            /// F
            /// </summary>
            public const int F = 1;

            /// <summary>
            /// G(None)
            /// </summary>
            public const int G = 0;
        }

        /// <summary>
        /// バトル初期状態
        /// </summary>
        public class BattleStyle
        {
            /// <summary>
            /// 通常
            /// </summary>
            public const int Normal = 0;

            /// <summary>
            /// 先制
            /// </summary>
            public const int Preemptive = 1;

            /// <summary>
            /// バックアタック
            /// </summary>
            public const int BackAttack = 2;

            /// <summary>
            /// ラウンドアタック
            /// </summary>
            public const int RoundAttack = 3;

            /// <summary>
            /// 不意打ち
            /// </summary>
            public const int Surprise = 4;
        }

        /// <summary>
        /// パーソナル特性
        /// </summary>
        public class PersonalAbility
        {
            /// <summary>
            /// なし
            /// </summary>
            public const int None = 0;

            /// <summary>
            /// サイボーグ
            /// </summary>
            public const int Cyborg = 1;

            /// <summary>
            /// エレメンタライズ
            /// </summary>
            public const int Elementalize = 2;

            /// <summary>
            /// フィジカルシフト
            /// </summary>
            public const int PhysicalShift = 3;

            /// <summary>
            /// メンタルシフト
            /// </summary>
            public const int MentalShift = 4;
        }

        /// <summary>
        /// スキル使用条件
        /// </summary>
        public class SkillUseLimit
        {
            /// <summary>
            /// なし
            /// </summary>
            public const int None = 0;

            /// <summary>
            /// 片手武器装備
            /// </summary>
            public const int OneHanded = 1;

            /// <summary>
            /// 両手武器装備
            /// </summary>
            public const int TwoHanded = 2;

            /// <summary>
            /// 盾装備
            /// </summary>
            public const int Shield = 3;

            /// <summary>
            /// 射撃装備
            /// </summary>
            public const int RangeWeapon = 4;
        }

        /// <summary>
        /// 行動内容
        /// </summary>
        public class ActionType
        {
            /// <summary>
            /// None
            /// </summary>
            public const int Default = -1;

            /// <summary>
            /// 行動しない
            /// </summary>
            public const int NoAction = 0;

            /// <summary>
            /// メインウェポン攻撃
            /// </summary>
            public const int MainAttack = 1;

            /// <summary>
            /// アーツ
            /// </summary>
            public const int ArtsAttack = 2;

            /// <summary>
            /// スペシャルアーツ
            /// </summary>
            public const int SpecialArtsAttack = 3;

            /// <summary>
            /// 指定された番号のアーツを発動(任意)
            /// </summary>
            public const int SelectionArtsAttack = 4;

            /// <summary>
            /// アイテムの使用
            /// </summary>
            public const int UseItem = 6;
        }

        /// <summary>
        /// 行動基準種別
        /// </summary>
        public class ActionBaseType
        {
            /// <summary>
            /// メイン武器
            /// </summary>
            public const int MainAttack = 0;

            /// <summary>
            /// サブ武器
            /// </summary>
            public const int SubAttack = 1;

            /// <summary>
            /// 精霊魔法攻撃(魔導攻撃)
            /// </summary>
            public const int SorscialAttack = 5;

            /// <summary>
            /// 神聖魔法攻撃(魔導攻撃)
            /// </summary>
            public const int MindAttack = 6;

            /// <summary>
            /// ブレス攻撃
            /// </summary>
            public const int BlessAttack = 7;

            /// <summary>
            /// 魔法剣
            /// </summary>
            public const int MagicSword = 8;

            /// <summary>
            /// アイテムアーツ
            /// </summary>
            public const int ItemArts = 9;
        }

        /// <summary>
        /// 属性
        /// </summary>
        public class Elemental
        {
            /// <summary>
            /// 火
            /// </summary>
            public const string Fire = "fire";

            /// <summary>
            /// 氷
            /// </summary>
            public const string Freeze = "freeze";

            /// <summary>
            /// 風
            /// </summary>
            public const string Air = "air";

            /// <summary>
            /// 土
            /// </summary>
            public const string Earth = "earth";

            /// <summary>
            /// 水
            /// </summary>
            public const string Water = "water";

            /// <summary>
            /// 雷
            /// </summary>
            public const string Thunder = "thunder";

            /// <summary>
            /// 聖
            /// </summary>
            public const string Holy = "holy";

            /// <summary>
            /// 闇
            /// </summary>
            public const string Dark = "dark";

            /// <summary>
            /// 斬
            /// </summary>
            public const string Slash = "slash";

            /// <summary>
            /// 突
            /// </summary>
            public const string Pierce = "pierce";

            /// <summary>
            /// 打
            /// </summary>
            public const string Strike = "strike";

            /// <summary>
            /// 壊
            /// </summary>
            public const string Break = "break";
        }

        /// <summary>
        /// 種族カテゴリ
        /// </summary>
        public class Category
        {
            /// <summary>
            /// アクアン
            /// </summary>
            public const int Aquan = 1;

            /// <summary>
            /// アモルフ
            /// </summary>
            public const int Amolf = 2;

            /// <summary>
            /// アルカナ
            /// </summary>
            public const int Alcana = 3;

            /// <summary>
            /// アンデッド
            /// </summary>
            public const int Undead = 4;

            /// <summary>
            /// ヴァーミン
            /// </summary>
            public const int Vermin = 5;

            /// <summary>
            /// エレメント
            /// </summary>
            public const int Element = 6;

            /// <summary>
            /// エンジェル
            /// </summary>
            public const int Angel = 7;

            /// <summary>
            /// ナッシングネス
            /// </summary>
            public const int Nothingness = 8;

            /// <summary>
            /// デーモン
            /// </summary>
            public const int Demon = 9;

            /// <summary>
            /// ドラゴン
            /// </summary>
            public const int Dragon = 10;

            /// <summary>
            /// バード
            /// </summary>
            public const int Bird = 11;

            /// <summary>
            /// バイオ
            /// </summary>
            public const int Bio = 12;

            /// <summary>
            /// ビースト
            /// </summary>
            public const int Beast = 13;

            /// <summary>
            /// プラントイド
            /// </summary>
            public const int Plantid = 14;

            /// <summary>
            /// マシン
            /// </summary>
            public const int Machine = 15;

            /// <summary>
            /// ヒューマノイド
            /// </summary>
            public const int Human = 16;

            /// <summary>
            /// リザード
            /// </summary>
            public const int Rizard = 17;

            /// <summary>
            /// デミヒューマン
            /// </summary>
            public const int DemiHuman = 18;
        }

        /// <summary>
        /// 連携発動アーツ番号
        /// </summary>
        public class ChainArts
        {
            /// <summary>
            /// なし
            /// </summary>
            public const int None = 0;

            /// <summary>
            /// ダブルブレイド
            /// </summary>
            public const int DoubleBlade = 2500;

            /// <summary>
            /// バリアントガード
            /// </summary>
            public const int VariantGuard = 2501;

            /// <summary>
            /// スタンブロー
            /// </summary>
            public const int StunBrow = 2502;

            /// <summary>
            /// パワードフィスト
            /// </summary>
            public const int PowerdFist = 2503;

            /// <summary>
            /// アストラルフォース
            /// </summary>
            public const int AstralForce = 2504;

            /// <summary>
            /// スウィフリーモーガン
            /// </summary>
            public const int SwiflyMorgan = 2505;

            /// <summary>
            /// ブラックグラビティ
            /// </summary>
            public const int BlackGravity = 2506;

            /// <summary>
            /// ホワイトアウト
            /// </summary>
            public const int WhiteOut = 2507;
        }

        /// <summary>
        /// 戦闘結果
        /// </summary>
        public class BattleResult
        {
            /// <summary>
            /// ＰＣ勝利
            /// </summary>
            public const int Winner = 0;

            /// <summary>
            /// 引き分け
            /// </summary>
            public const int Draw = 1;

            /// <summary>
            /// ＰＣ敗北
            /// </summary>
            public const int Lose = 2;
        }

        /// <summary>
        /// アイテムリスト用カテゴリ
        /// </summary>
        public class TypeCategoryDiv
        {
            /// <summary>
            /// メイン
            /// </summary>
            public const int Main = 0;

            /// <summary>
            /// サブ
            /// </summary>
            public const int Sub = 1;

            /// <summary>
            /// 頭部
            /// </summary>
            public const int Head = 2;

            /// <summary>
            /// 身体
            /// </summary>
            public const int Body = 3;

            /// <summary>
            /// 装飾
            /// </summary>
            public const int Accesory = 4;

            /// <summary>
            /// 道具
            /// </summary>
            public const int Items = 5;

            /// <summary>
            /// 素材
            /// </summary>
            public const int Material = 6;

            /// <summary>
            /// 食品
            /// </summary>
            public const int Food = 7;

            /// <summary>
            /// 矢弾
            /// </summary>
            public const int Ammo = 8;

            /// <summary>
            /// スクロール
            /// </summary>
            public const int Scroll = 9;

            /// <summary>
            /// アーツ素材
            /// </summary>
            public const int Arts = 10;
        }

        /// <summary>
        /// クエスト種別
        /// </summary>
        public class QuestType
        {
            /// <summary>
            /// クエスト
            /// </summary>
            public const int Quest = 0;

            /// <summary>
            /// ミッション
            /// </summary>
            public const int Mission = 1;
        }

        /// <summary>
        /// アイコンサイズ
        /// </summary>
        public class IconSize
        {
            /// <summary>
            /// サイズ：S
            /// </summary>
            public const int S = 1;

            /// <summary>
            /// サイズ：M
            /// </summary>
            public const int M = 2;

            /// <summary>
            /// サイズ：L
            /// </summary>
            public const int L = 3;
        }

        /// <summary>
        /// アイコン設定
        /// </summary>
        public class IconSetting
        {
            /// <summary>
            /// 正常終了
            /// </summary>
            public const int OK = 0;
        }

        /// <summary>
        /// 組織設定
        /// </summary>
        public class OrganizationSetting
        {
            /// <summary>
            /// 昇格終了
            /// </summary>
            public const int OK = 0;

            /// <summary>
            /// 昇格せずに終了
            /// </summary>
            public const int NoUpOK = 1;
        }

        /// <summary>
        /// 敗北条件
        /// </summary>
        public class DefeatCondition
        {
            /// <summary>
            /// 全滅
            /// </summary>
            public const int Annihilation = 0;

            /// <summary>
            /// ファルクの戦闘不能
            /// </summary>
            public const int FalkDefeat = 1;

            /// <summary>
            /// 120カウントのターンオーバー
            /// </summary>
            public const int TrunOver120 = 2;

            /// <summary>
            /// 80カウントで強制終了
            /// </summary>
            public const int EndOver80 = 3;
        }

        /// <summary>
        /// 勝利条件
        /// </summary>
        public class WinnerCondition
        {
            /// <summary>
            /// 全滅
            /// </summary>
            public const int Annihilation = 0;

            /// <summary>
            /// ボーン・コレクターを倒せ！
            /// </summary>
            public const int BoneCollectorDefeat = 1;

            /// <summary>
            /// 謎の装置を無力化せよ！
            /// </summary>
            public const int UndeadMachineDefeat = 2;
        }

        /// <summary>
        /// 公式イベントID
        /// </summary>
        public class OfficialEvent
        {
            /// <summary>
            /// イベントなし
            /// </summary>
            public const int NoEvent = 0;

            /// <summary>
            /// あけましておめでとう！
            /// </summary>
            public const int NewYear = 1;

            /// <summary>
            /// ひなまつり
            /// </summary>
            public const int Hinamatsuri = 2;

            /// <summary>
            /// 武皇祭(こどもの日)
            /// </summary>
            public const int Buosai = 3;

            /// <summary>
            /// 夏祭り
            /// </summary>
            public const int SummerFestival = 4;

            /// <summary>
            /// ハロウィーン
            /// </summary>
            public const int Halloween = 5;

            /// <summary>
            /// クリスマス
            /// </summary>
            public const int Christmas = 6;

            /// <summary>
            /// ウェディングサポート
            /// </summary>
            public const int WeddingSupport = 7;
        }

        /// <summary>
        /// パーティシステムメッセージ種別
        /// </summary>
        public enum PartySysMemoType
        {
            None = 0,

            /// <summary>
            /// マークの侵入条件を満たしていない
            /// </summary>
            MarkMissing,
        }

        /// <summary>
        /// プレイヤーシステムメッセージ種別
        /// </summary>
        public enum PlayerSysMemoType
        {
            None = 0,

            /// <summary>
            /// マーク発見
            /// </summary>
            MarkFinder,

            /// <summary>
            /// プロフィール設定
            /// </summary>
            ProfileSetting,

            /// <summary>
            /// パーティ編成
            /// </summary>
            PartySetting,

            /// <summary>
            /// 各種登録内容確認
            /// </summary>
            CheckContinue,

            /// <summary>
            /// 各種警告の表示
            /// </summary>
            CautionView,

            /// <summary>
            /// プライベートスキル習得
            /// </summary>
            PrivateSkill,

            /// <summary>
            /// アイコン設定
            /// </summary>
            IconSettings,

            /// <summary>
            /// メッセージ設定
            /// </summary>
            PrivateMessage,

            /// <summary>
            /// メッセージ受信
            /// </summary>
            ReceiveMessage,

            /// <summary>
            /// バザーアイテム売却ロック
            /// </summary>
            SellingBazzerItemLock,

            /// <summary>
            /// 装備解除
            /// </summary>
            RemoveEquip,

            /// <summary>
            /// アイテム売却
            /// </summary>
            SellingItem,

            /// <summary>
            /// アイテム取引
            /// </summary>
            TradeItem,

            /// <summary>
            /// お金取引
            /// </summary>
            TradeMoney,

            /// <summary>
            /// アイテム購入
            /// </summary>
            BuyingItem,

            /// <summary>
            /// バザーアイテム購入
            /// </summary>
            BuyingBazzerItem,

            /// <summary>
            /// リーダー設定
            /// </summary>
            ReaderSetting,

            /// <summary>
            /// インストール
            /// </summary>
            InstallSetting,

            /// <summary>
            /// アイテム装備
            /// </summary>
            EquipingItem,

            /// <summary>
            /// アイテム使用
            /// </summary>
            UsingItem,

            /// <summary>
            /// 戦闘行動内容
            /// </summary>
            BattleAction,

            /// <summary>
            /// 隊列変更
            /// </summary>
            FormationSetting,

            /// <summary>
            /// 台詞変更
            /// </summary>
            SerifChange,

            /// <summary>
            /// アカウント設定
            /// </summary>
            AccountSetting,

            /// <summary>
            /// 個人結果用クエスト進行
            /// </summary>
            QuestPrivateEntry,

            /// <summary>
            /// レベルアップインフォメーション（レベルアップの内容は個人結果にまとめる）
            /// </summary>
            LevelUpInformation,
        }

        /// <summary>
        /// バザー結果状態
        /// </summary>
        public class Bazzer
        {
            /// <summary>
            /// 通常成功
            /// </summary>
            public const int OK = 0;

            /// <summary>
            /// アイテムを持っていない
            /// </summary>
            public const int NoHaveItem = 1;

            /// <summary>
            /// お金が足りない
            /// </summary>
            public const int NoMoney = 3;

            /// <summary>
            /// 自分にはトレードできない
            /// </summary>
            public const int NoMineTrade = 4;

            /// <summary>
            /// バインドアイテム！？
            /// </summary>
            public const int BindItem = 5;
        }

        /// <summary>
        /// アイテムボックスタイプ
        /// </summary>
        public class ItemBox
        {
            /// <summary>
            /// 通常ボックス（カバン）
            /// </summary>
            public const int Normal = 0;

            /// <summary>
            /// テンポラリボックス（イベントで入手できたアイテムはここにストックされる）
            /// </summary>
            public const int Box = 1;
        }

        /// <summary>
        /// コッファーステータス
        /// </summary>
        public class Coffer
        {
            /// <summary>
            /// 正常終了
            /// </summary>
            public const int OK = 0;
        }
        
        /// <summary>
        /// 貴重品種別
        /// </summary>
        public class KeyItemType
        {
            /// <summary>
            /// 一時的
            /// </summary>
            public const int Temporary = 0;

            /// <summary>
            /// 永久的
            /// </summary>
            public const int Never = 1;
        }

        /// <summary>
        /// 種別カウント用 タイプ
        /// </summary>
        public class TypeCountType
        {
            /// <summary>
            /// 作成したアイテムの種別のカウント（作成数）
            /// </summary>
            public const int CreateItemType = 0;

            /// <summary>
            /// 作成品の能力種類
            /// </summary>
            public const int CreateItemStatus = 1;

            /// <summary>
            /// ディギング
            /// </summary>
            public const int Digging = 2;
        }

        /// <summary>
        /// 攻撃武器種類（メイン、サブ区別用）
        /// </summary>
        public class AttackWeaponType
        {
            /// <summary>
            /// メインウェポン
            /// </summary>
            public const int Main = 0;

            /// <summary>
            /// サブウェポン
            /// </summary>
            public const int Sub = 1;
        }

        /// <summary>
        /// データベースアクセス先
        /// </summary>
        public class DataBaseAccessTarget
        {
            /// <summary>
            /// トランザクション
            /// </summary>
            public const int Tran = 0;

            /// <summary>
            /// マスター
            /// </summary>
            public const int Master = 1;

            /// <summary>
            /// システムマスター
            /// </summary>
            public const int SystemMaster = 2;
        }

        /// <summary>
        /// 取り扱いショップ
        /// </summary>
        public class Shopping
        {
            /// <summary>
            /// 取り扱いなし(非売品)
            /// </summary>
            public const int None = 0;

            /// <summary>
            /// 販売品
            /// </summary>
            public const int Online = 1;
        }

        /// <summary>
        /// 基本アビリティ
        /// </summary>
        public class BasicAbility
        {
            /// <summary>
            /// 力
            /// </summary>
            public const int STR = 1;

            /// <summary>
            /// 敏捷
            /// </summary>
            public const int AGI = 3;

            /// <summary>
            /// 魔力
            /// </summary>
            public const int MAG = 4;

            /// <summary>
            /// ユニーク
            /// </summary>
            public const int UNQ = 6;
        }

        /// <summary>
        /// 防御側数値種別
        /// </summary>
        public class DeffenceType
        {
            /// <summary>
            /// 防御
            /// </summary>
            public const int DFE = 0;

            /// <summary>
            /// 魔法防御
            /// </summary>
            public const int MGR = 1;
        }

        /// <summary>
        /// クリティカル/連撃種別
        /// </summary>
        public class CriticalType
        {
            /// <summary>
            /// 連撃
            /// </summary>
            public const int MultiAct = 0;

            /// <summary>
            /// クリティカル
            /// </summary>
            public const int Critical = 1;
        }

        /// <summary>
        /// 工房
        /// </summary>
        public class CreateEnvironment
        {
            /// <summary>
            /// 野外
            /// </summary>
            public const int OutSide = 0;

            /// <summary>
            /// 普通の工房
            /// </summary>
            public const int NormalStudio = 1;

            /// <summary>
            /// 高級な工房
            /// </summary>
            public const int HighStudio = 2;

            /// <summary>
            /// 最高級の工房
            /// </summary>
            public const int GodStudio = 3;
        }

        /// <summary>
        /// バインドタイプ
        /// </summary>
        public class BindType
        {
            /// <summary>
            /// なし
            /// </summary>
            public const int None = 0;

            /// <summary>
            /// 入手時
            /// </summary>
            public const int PickUp = 1;
        }

        /// <summary>
        /// エリアタイプ
        /// </summary>
        public class AreaType
        {
            /// <summary>
            /// タウン
            /// </summary>
            public const int Town = 0;

            /// <summary>
            /// ダンジョン
            /// </summary>
            public const int Dungeon = 1;

            /// <summary>
            /// フィールド
            /// </summary>
            public const int Field = 2;
        }

        /// <summary>
        /// 属性相性種類
        /// </summary>
        public class ElementalRatingType
        {
            /// <summary>
            /// なし
            /// </summary>
            public const int None = 0;

            /// <summary>
            /// 弱点
            /// </summary>
            public const int Weak = 1;

            /// <summary>
            /// 半減
            /// </summary>
            public const int Regist = 2;

            /// <summary>
            /// 無効
            /// </summary>
            public const int Block = 3;

            /// <summary>
            /// 吸収
            /// </summary>
            public const int Drain = 4;
        }

        /// <summary>
        /// 使用可能制限
        /// </summary>
        public class OnlyMode
        {
            /// <summary>
            /// いつでも
            /// </summary>
            public const int Always = 0;

            /// <summary>
            /// プライマリインストール時のみ
            /// </summary>
            public const int Primary = 1;

            /// <summary>
            /// セカンダリインストール時のみ
            /// </summary>
            public const int Secondary = 2;

            /// <summary>
            /// スクロール使用後から
            /// </summary>
            public const int ScrollUsing = 3;

            /// <summary>
            /// 精霊との契約
            /// </summary>
            public const int UsingElemental = 4;
        }

        /// <summary>
        /// TP最大値
        /// </summary>
        public class TP
        {
            /// <summary>
            /// TP最大値
            /// </summary>
            public const int Max = 200;
        }

        // 配列定数
    }
}
