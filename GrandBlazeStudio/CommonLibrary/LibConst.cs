using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 定数管理クラス
    /// </summary>
    public class LibConst
    {
        /// <summary>
        /// 許容未継続回数
        /// </summary>
        public const int NoContinueCountMax = 14;

        /// <summary>
        /// キャラクター結果出力先
        /// </summary>
        public static string OutputFolderChara = LibCommonLibrarySettings.ResultBasePath + LibCommonLibrarySettings.Characters;

        /// <summary>
        /// キャラクター結果出力先(バックアップ)
        /// </summary>
        public static string OutputFolderCharaBackup = LibCommonLibrarySettings.ResultBackupPath + LibCommonLibrarySettings.Characters;

        /// <summary>
        /// パーティ結果出力先
        /// </summary>
        public static string OutputFolderParty = LibCommonLibrarySettings.ResultBasePath + LibCommonLibrarySettings.Partys;

        /// <summary>
        /// プライベート結果出力先
        /// </summary>
        public static string OutputFolderPrivate = LibCommonLibrarySettings.ResultBasePath + LibCommonLibrarySettings.Privates;

        /// <summary>
        /// 既定のファイルエンコード
        /// </summary>
        public static Encoding FileEncod = Encoding.UTF8;

        /// <summary>
        /// キャラクタセット Shift-JIS
        /// </summary>
        public const int Shift_JIS = 932;

        /// <summary>
        /// 素材消失確率
        /// </summary>
        public const int LostProbability = 112;

        /// <summary>
        /// HQランクアップ確率
        /// </summary>
        public const int HighQualityRankUp = 96;

        /// <summary>
        /// 属性リスト
        /// </summary>
        public static readonly string[] ElementalList ={ "fire", "freeze", "air", "earth", "water", "thunder", "holy", "dark", "slash", "pierce", "strike", "break" };

        /// <summary>
        /// 属性リスト(日本語)
        /// </summary>
        public static readonly string[] ElementalListJpn = { "火", "氷", "風", "土", "水", "雷", "聖", "闇", "斬", "突", "打", "壊" };

        /// <summary>
        /// レベル制限
        /// </summary>
        public static readonly int LevelLimit = 50;

        /// <summary>
        /// 性別名称取得
        /// </summary>
        /// <param name="SexID"></param>
        /// <returns></returns>
        public static string GetSexName(int SexID)
        {
            switch (SexID)
            {
                case Status.Sex.Male:
                    return "男";
                case Status.Sex.Female:
                    return "女";
                default:
                    return "？";
            }
        }

        /// <summary>
        /// 装備部位名取得
        /// </summary>
        /// <param name="EquipSpotID">装備部位</param>
        /// <returns>部位名</returns>
        public static string GetEquipSpotName(int EquipSpotID)
        {
            switch (EquipSpotID)
            {
                case Status.EquipSpot.Main:
                    return "メイン";
                case Status.EquipSpot.Sub:
                    return "サブ";
                case Status.EquipSpot.Head:
                    return "頭部";
                case Status.EquipSpot.Body:
                    return "胴体";
                default:
                    return "装飾";
            }
        }
    }
}
