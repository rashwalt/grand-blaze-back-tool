using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.Character;

namespace CommonLibrary
{
    /// <summary>
    /// 戦闘用キャラクターリスト取得クラス
    /// </summary>
    public static class LibBattleCharacter
    {
        /// <summary>
        /// プレイヤーキャラクターだけを取得
        /// </summary>
        /// <param name="TargetList">取得対象リスト</param>
        /// <returns>取得リスト</returns>
        public static List<LibUnitBase> GetPlayers(this List<LibUnitBase> TargetList)
        {
            return TargetList.FindAll(CharacterPoint => CharacterPoint.GetType() == typeof(LibPlayer));
        }

        /// <summary>
        /// ノンプレイヤーキャラクターだけを取得
        /// </summary>
        /// <param name="TargetList">取得対象リスト</param>
        /// <returns>取得リスト</returns>
        public static List<LibUnitBase> GetNonPlayers(this List<LibUnitBase> TargetList)
        {
            return TargetList.FindAll(CharacterPoint => CharacterPoint.GetType() == typeof(LibGuest) ||
                    (CharacterPoint.GetType() == typeof(LibMonster) && CharacterPoint.PartyBelong == Status.Belong.Friend));
        }

        /// <summary>
        /// ノンプレイヤーキャラクター（ペット含まない）だけを取得
        /// </summary>
        /// <param name="TargetList">取得対象リスト</param>
        /// <returns>取得リスト</returns>
        public static List<LibUnitBase> GetNonPlayersNotPet(this List<LibUnitBase> TargetList)
        {
            return TargetList.FindAll(CharacterPoint => CharacterPoint.GetType() == typeof(LibGuest) && CharacterPoint.PartyBelongDetail == Status.BelongDetail.Normal);
        }

        /// <summary>
        /// 味方（NPC含む）だけを取得
        /// </summary>
        /// <param name="TargetList">取得対象リスト</param>
        /// <returns>取得リスト</returns>
        public static List<LibUnitBase> GetFriendry(this List<LibUnitBase> TargetList)
        {
            return TargetList.FindAll(CharacterPoint => CharacterPoint.PartyBelong == Status.Belong.Friend);
        }

        /// <summary>
        /// 味方（ペット）だけを取得
        /// </summary>
        /// <param name="TargetList">取得対象リスト</param>
        /// <returns>取得リスト</returns>
        public static List<LibUnitBase> GetFriendryPet(this List<LibUnitBase> TargetList)
        {
            return TargetList.FindAll(CharacterPoint => (CharacterPoint.GetType() == typeof(LibGuest) && CharacterPoint.PartyBelongDetail != Status.BelongDetail.Normal) ||
                (CharacterPoint.GetType() == typeof(LibMonster) && CharacterPoint.PartyBelong == Status.Belong.Friend));
        }

        /// <summary>
        /// 味方（ペット含まない）だけを取得
        /// </summary>
        /// <param name="TargetList">取得対象リスト</param>
        /// <returns>取得リスト</returns>
        public static List<LibUnitBase> GetFriendryNotPet(this List<LibUnitBase> TargetList)
        {
            return TargetList.FindAll(CharacterPoint => CharacterPoint.GetType() == typeof(LibPlayer) ||
                (CharacterPoint.GetType() == typeof(LibGuest) && CharacterPoint.PartyBelongDetail == Status.BelongDetail.Normal));
        }

        /// <summary>
        /// 敵だけを取得
        /// </summary>
        /// <param name="TargetList">取得対象リスト</param>
        /// <returns>取得リスト</returns>
        public static List<LibUnitBase> GetMonsters(this List<LibUnitBase> TargetList)
        {
            return TargetList.FindAll(CharacterPoint => CharacterPoint.PartyBelong == Status.Belong.Enemy);
        }

        /// <summary>
        /// 生存者のみを帰す
        /// </summary>
        /// <param name="TargetList">取得対象リスト</param>
        /// <returns>取得リスト</returns>
        public static List<LibUnitBase> GetLive(this List<LibUnitBase> TargetList)
        {
            return TargetList.FindAll(CharacterPoint => !CharacterPoint.BattleOut);
        }
    }
}
