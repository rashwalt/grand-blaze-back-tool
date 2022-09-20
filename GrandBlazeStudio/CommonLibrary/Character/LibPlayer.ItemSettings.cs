using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.DataFormat.SpecialEntity;
using CommonLibrary.DataFormat.Entity;
using System.Data;

namespace CommonLibrary.Character
{
    public partial class LibPlayer : LibUnitBase
    {
        /// <summary>
        /// 装備アイテムのアイテム番号取得
        /// </summary>
        /// <param name="EquipSpots">装備箇所</param>
        /// <returns>itemRow</returns>
        public CommonItemEntity.item_listRow GetHaveItemEquiped(int EquipSpots)
        {
            HaveItem.DefaultView.RowFilter = "equip_spot=" + EquipSpots;

            if (HaveItem.DefaultView.Count == 0)
            {
                return null;
            }

            CommonUnitDataEntity.have_item_listRow Row = (CommonUnitDataEntity.have_item_listRow)HaveItem.DefaultView[0].Row;

            CommonItemEntity.item_listRow itemRow = LibItem.GetItemRow(Row.it_num, Row.created);

            if (Row.created)
            {
                itemRow.it_name = Row.it_name;
                itemRow.it_comment = Row.it_comment;
                itemRow.it_effect = Row.it_effect;
                itemRow.it_price = Row.it_price;
                itemRow.it_seller = Row.it_seller;
            }
            return itemRow;
        }

        /// <summary>
        /// 所持アイテムのアイテム番号と作成フラグ取得
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="HaveNo">所持番号</param>
        /// <returns>itemRow</returns>
        public CommonItemEntity.item_listRow GetHaveItemItemNum(int BoxType, int HaveNo)
        {
            CommonUnitDataEntity.have_item_listRow Row = HaveItem.FindBybox_typehave_nodrop_typeget_synx(BoxType, HaveNo, 0, 0);

            if (Row == null)
            {
                return null;
            }

            CommonItemEntity.item_listRow itemRow = LibItem.GetItemRow(Row.it_num, Row.created);

            if (Row.created)
            {
                itemRow.it_name = Row.it_name;
                itemRow.it_comment = Row.it_comment;
                itemRow.it_effect = Row.it_effect;
                itemRow.it_price = Row.it_price;
                itemRow.it_seller = Row.it_seller;
            }
            return itemRow;
        }

        /// <summary>
        /// 所持アイテムのアイテムDataRow
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="HaveNo">所持番号</param>
        /// <returns>アイテムDataRow</returns>
        public CommonUnitDataEntity.have_item_listRow GetHaveItemItemRow(int BoxType, int HaveNo)
        {
            return HaveItem.FindBybox_typehave_nodrop_typeget_synx(BoxType, HaveNo,0,0);
        }

        /// <summary>
        /// アイテムを入手
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <param name="ItemCount">個数</param>
        /// <returns>成功したか否か</returns>
        public bool AddItem(int BoxType, int ItemID, bool IsCreated, ref int ItemCount, ref int RestItemCount)
        {
            int ItemNo = 0;
            return AddItem(BoxType, ItemID, IsCreated, ref ItemCount, ref ItemNo, true, ref RestItemCount);
        }

        /// <summary>
        /// アイテムを入手
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <param name="ItemCount">個数</param>
        /// <returns>成功したか否か</returns>
        public bool AddItem(int BoxType, int ItemID, bool IsCreated, ref int ItemCount, ref int ItemNo, ref int RestItemCount)
        {
            return AddItem(BoxType, ItemID, IsCreated, ref ItemCount, ref ItemNo, true, ref RestItemCount);
        }

        /// <summary>
        /// アイテムを入手
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <param name="ItemCount">個数</param>
        /// <param name="ItemNo">取得したアイテム番号が入る</param>
        /// <param name="CheckMax">所持数を確認するかどうか</param>
        /// <param name="RestItemCount">残りアイテム数</param>
        /// <returns>成功したか否か</returns>
        public bool AddItem(int BoxType, int ItemID, bool IsCreated, ref int ItemCount, ref int ItemNo, bool CheckMax, ref int RestItemCount)
        {
            bool IsOK = false;

            CommonItemEntity.item_listRow ItemRow = LibItem.GetItemRow(ItemID, IsCreated);

            int StackCount = ItemRow.it_stack;
            if (StackCount == 0)
            {
                StackCount = 99;
                EffectListEntity.effect_listRow EffectRow = EffectList.FindByeffect_id(2112);
                if (EffectRow != null)
                {
                    switch ((int)EffectRow.rank)
                    {
                        case 1:
                            StackCount = 150;
                            break;
                        case 2:
                            StackCount = 200;
                            break;
                        case 3:
                            StackCount = 255;
                            break;
                    }
                }
            }

            bool IsRareItem = ItemRow.it_rare;

            if (IsRareItem) { StackCount = 1; }

            HaveItem.DefaultView.RowFilter = "box_type=" + BoxType + " and it_num=" + ItemID + " and created=" + IsCreated + " and it_box_count<" + StackCount;
            HaveItem.DefaultView.Sort = "it_box_count desc";

            int BaseItemCount = ItemCount;

            // スタック数による確認
            DataView HaveItemMax = HaveItem.DefaultView;

            foreach (DataRowView HaveItemRowView in HaveItemMax)
            {
                if (!IsRareItem)
                {
                    CommonUnitDataEntity.have_item_listRow HaveItemRow = HaveItem.FindBybox_typehave_nodrop_typeget_synx(BoxType, (int)HaveItemRowView["have_no"], 0, 0);

                    int AddCount = BaseItemCount;

                    if ((HaveItemRow.it_box_count + AddCount) > StackCount)
                    {
                        AddCount = StackCount - HaveItemRow.it_box_count;
                        if (AddCount < 0) { AddCount = 0; }
                    }

                    BaseItemCount -= AddCount;

                    if (AddCount > 0)
                    {
                        HaveItemRow.it_box_count += AddCount;
                        HaveItemRow._new = true;

                        ItemNo = (int)HaveItemRowView["have_no"];
                        IsOK = true;
                    }
                }

                if (BaseItemCount == 0)
                {
                    break;
                }
            }

            while (BaseItemCount > 0)
            {
                if (CheckMax && CheckHaveItemMax(BoxType))
                {
                    break;
                }

                // レアアイテムをすでに持っていないかチェック
                if (IsRareItem)
                {
                    HaveItem.DefaultView.RowFilter = "box_type=" + BoxType + " and it_num=" + ItemID + " and created=" + IsCreated;
                    if (HaveItem.DefaultView.Count > 0)
                    {
                        break;
                    }
                }

                int AddCount = BaseItemCount;

                if (AddCount > StackCount)
                {
                    AddCount = StackCount;
                    if (AddCount < 0) { AddCount = 0; }
                }

                BaseItemCount -= AddCount;

                // 持っていない場合、新たに設定する
                CommonUnitDataEntity.have_item_listRow EditRow = HaveItem.Newhave_item_listRow();

                HaveItem.DefaultView.RowFilter = "box_type=" + BoxType;
                int NewID = LibInteger.GetNewUnderNum(HaveItem.DefaultView, "have_no");
                ItemNo = NewID;

                EditRow.box_type = BoxType;
                EditRow.have_no = NewID;
                EditRow.it_num = ItemID;
                EditRow.it_box_count = AddCount;
                EditRow.it_box_baz_count = 0;
                EditRow.created = IsCreated;
                EditRow.equip_spot = 0;
                EditRow.drop_type = 0;
                EditRow.get_synx = 0;
                EditRow._new = true;
                EditRow.it_name = "";
                EditRow.it_comment = "";
                EditRow.it_effect = "0,0,0,0,0,0";
                EditRow.it_price = 0;
                EditRow.it_seller = 0;

                HaveItem.Addhave_item_listRow(EditRow);

                ItemNo = NewID;
                IsOK = true;
            }

            ItemCount -= BaseItemCount;
            RestItemCount = BaseItemCount;

            return IsOK;
        }

        /// <summary>
        /// アイテムを除去
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="HaveNo">所有アイテム番号</param>
        /// <param name="ItemCount">個数</param>
        /// <param name="IsCreatedNoDelete">作成アイテムでも削除するかどうかのフラグ</param>
        /// <returns>成功したか否か</returns>
        public bool RemoveItem(int BoxType, int HaveNo, int ItemCount)
        {
            int ItemNo = 0;
            bool IsCreated = false;
            return RemoveItem(BoxType, HaveNo, ItemCount, ref ItemNo, ref IsCreated);
        }

        /// <summary>
        /// アイテムを除去
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="HaveNo">所有アイテム番号</param>
        /// <param name="ItemCount">個数</param>
        /// <param name="ItemNo">削除したアイテム番号が入る</param>
        /// <param name="IsCreated">削除アイテム作成フラグ</param>
        /// <returns>成功したか否か</returns>
        public bool RemoveItem(int BoxType, int HaveNo, int ItemCount, ref int ItemNo, ref bool IsCreated)
        {
            CommonUnitDataEntity.have_item_listRow HaveItemRow = HaveItem.FindBybox_typehave_nodrop_typeget_synx(BoxType, HaveNo, 0, 0);

            if (HaveItemRow == null)
            {
                return false;
            }

            ItemNo = HaveItemRow.it_num;
            IsCreated = HaveItemRow.created;

            if (HaveItemRow.it_box_count > ItemCount)
            {
                // 除去個数が所持数より少ない場合、その分だけ引く
                HaveItemRow.it_box_count -= ItemCount;
            }
            else
            {
                if (HaveItemRow.it_box_baz_count == 0)
                {
                    // 除去個数が所持数より多い（または同数）場合、そのアイテムを完全除去
                    HaveItemRow.Delete();
                }
                else
                {
                   HaveItemRow.it_box_count = 0;
                }
            }

            return true;
        }

        /// <summary>
        /// アイテムを除去(アイテム番号指定)
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="ItemNo">アイテム番号</param>
        /// <param name="ItemCount">個数</param>
        /// <returns>成功したか否か</returns>
        public bool ItemNoRemoveItem(int BoxType, int ItemNo, int ItemCount)
        {
            HaveItem.DefaultView.RowFilter = "box_type=" + BoxType + " and it_num=" + ItemNo + " and created=false";

            if (HaveItem.DefaultView.Count == 0)
            {
                return false;
            }

            CommonUnitDataEntity.have_item_listRow HaveItemRow = (CommonUnitDataEntity.have_item_listRow)HaveItem.DefaultView[0].Row;

            if (HaveItemRow.it_box_count > ItemCount)
            {
                // 除去個数が所持数より少ない場合、その分だけ引く
                HaveItemRow.it_box_count -= ItemCount;
            }
            else
            {
                // 除去個数が所持数より多い（または同数）場合、そのアイテムを完全除去
                HaveItemRow.Delete();
            }

            return true;
        }

        /// <summary>
        /// アイテムを持っているか判定
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <returns>持っている場合、trueを返す。ない場合はfalse</returns>
        public bool CheckHaveItem(int BoxType, int ItemID, bool IsCreated)
        {
            HaveItem.DefaultView.RowFilter = "box_type=" + BoxType + " and it_num=" + ItemID + " and created=" + IsCreated;

            if (HaveItem.DefaultView.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// アイテムを持っているか判定
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <param name="ItemCount">アイテム個数</param>
        /// <returns>持っている場合、trueを返す。ない場合はfalse</returns>
        public bool CheckHaveItem(int BoxType, int ItemID, bool IsCreated, int ItemCount)
        {
            HaveItem.DefaultView.RowFilter = "box_type=" + BoxType + " and it_num=" + ItemID + " and created=" + IsCreated + " and it_box_count>=" + ItemCount;

            if (HaveItem.DefaultView.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// アイテムの個数取得
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="ItemID">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <returns>個数</returns>
        public int GetHaveItemCount(int BoxType, int ItemID, bool IsCreated)
        {
            HaveItem.DefaultView.RowFilter = "box_type=" + BoxType + " and it_num=" + ItemID + " and created=" + IsCreated;

            if (HaveItem.DefaultView.Count > 0)
            {
                return (int)HaveItem.DefaultView[0]["it_box_count"];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// アイテムを装備しているか判定
        /// </summary>
        /// <param name="EquipID">装備部位</param>
        /// <param name="ItemID">アイテム番号</param>
        /// <returns>装備している場合、true</returns>
        public bool CheckEquipItem(int EquipID, int ItemID)
        {
            switch (EquipID)
            {
                case Status.EquipSpot.Main:
                    HaveItem.DefaultView.RowFilter = "equip_main=true";
                    break;
                case Status.EquipSpot.Sub:
                    HaveItem.DefaultView.RowFilter = "equip_sub=true";
                    break;
                case Status.EquipSpot.Head:
                    HaveItem.DefaultView.RowFilter = "equip_head=true";
                    break;
                case Status.EquipSpot.Body:
                    HaveItem.DefaultView.RowFilter = "equip_body=true";
                    break;
                default:
                    HaveItem.DefaultView.RowFilter = "equip_accesory=true";
                    break;
            }

            HaveItem.DefaultView.RowFilter += " and it_num=" + ItemID + " and created=false and box_type=" + Status.ItemBox.Normal;

            if (HaveItem.DefaultView.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// キーアイテムを持っているか判定
        /// </summary>
        /// <param name="KeyItemID">アイテム番号</param>
        /// <returns>持っている場合、番号を返す。ない場合は0</returns>
        public bool CheckKeyItem(int KeyItemID)
        {
            CommonUnitDataEntity.key_item_listRow KeyItemRow = KeyItemList.FindBykey_item_id(KeyItemID);

            if (KeyItemRow != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 指定したアイテムを指定した番号に装備する
        /// </summary>
        /// <param name="EquipID">装備箇所ID</param>
        /// <param name="HaveNo">所持アイテムNo</param>
        /// <returns>処理ステータス</returns>
        public int Equip(int EquipID, int HaveNo)
        {
            int ItemID = 0;
            bool IsCreated = false;
            return Equip(EquipID, HaveNo, ref ItemID, ref IsCreated);
        }

        /// <summary>
        /// 指定したアイテムを指定した番号に装備する
        /// </summary>
        /// <param name="EquipID">装備箇所ID</param>
        /// <param name="HaveNo">所持アイテムNo</param>
        /// <param name="ItemID">アイテム番号(参照)</param>
        /// <param name="IsCreated">アイテム作成フラグ(参照)</param>
        /// <returns>処理ステータス</returns>
        public int Equip(int EquipID, int HaveNo, ref int ItemID, ref bool IsCreated)
        {
            ItemID = 0;

            if (HaveNo < 1)
            {
                return Status.Common.ArgumentError;
            }

            CommonItemEntity.item_listRow ItemRow = GetHaveItemItemNum(Status.ItemBox.Normal, HaveNo);

            if (ItemRow == null)
            {
                return Status.Equip.NoHaveItemError;
            }

            ItemID = ItemRow.it_num;
            IsCreated = ItemRow.it_creatable;

            // 装備条件判定
            int status = EquipCondition(EquipID, HaveNo);

            if (status != Status.Equip.OK)
            {
                return status;
            }

            // 以前の装備を自動で解除
            int RemoveItemID = 0;
            bool RemoveItemIsCreated = false;
            this.EquipRemove(EquipID, ref RemoveItemID, ref RemoveItemIsCreated);

            CommonUnitDataEntity.have_item_listRow HaveItemRow = GetHaveItemItemRow(Status.ItemBox.Normal, HaveNo);

            // 種別取得
            ItemTypeEntity.mt_item_type_sub_categoryRow SubCategoryRow = LibItemType.GetSubCategoryRow(ItemRow.it_sub_category);

            // データ登録処理
            HaveItemRow.equip_spot = EquipID;

            SetEquipData();

            return Status.Equip.OK;
        }
        

        /// <summary>
        /// 装備の解除
        /// </summary>
        /// <param name="EquipID">装備箇所ID</param>
        /// <param name="ItemID">アイテム番号(参照)</param>
        /// <param name="IsCreated">作成フラグ(参照)</param>
        /// <returns>処理ステータス</returns>
        public int EquipRemove(int EquipID, ref int ItemID, ref bool IsCreated)
        {
            int HaveNo = 0;
            return EquipRemove(EquipID, ref ItemID, ref IsCreated, ref HaveNo);
        }

        /// <summary>
        /// 装備の解除
        /// </summary>
        /// <param name="EquipID">装備箇所ID</param>
        /// <param name="ItemID">アイテム番号(参照)</param>
        /// <param name="IsCreated">作成フラグ(参照)</param>
        /// <param name="HaveNo">所持番号(参照)</param>
        /// <returns>処理ステータス</returns>
        public int EquipRemove(int EquipID, ref int ItemID, ref bool IsCreated, ref int HaveNo)
        {
            HaveItem.DefaultView.RowFilter = "equip_spot=" + EquipID;

            if (HaveItem.DefaultView.Count == 0)
            {
                // 所持していないのでエラー
                ItemID = -1;
                IsCreated = false;
                HaveNo = -1;
                return Status.Common.ArgumentError;
            }

            ItemID = (int)HaveItem.DefaultView[0]["it_num"];
            IsCreated = (bool)HaveItem.DefaultView[0]["created"];
            HaveNo = (int)HaveItem.DefaultView[0]["have_no"];

            HaveItem.DefaultView[0]["equip_spot"] = 0;

            SetEquipData();

            return Status.Equip.OK;
        }

        /// <summary>
        /// 装備条件判定
        /// </summary>
        /// <param name="EquipID">装備箇所ID</param>
        /// <param name="ItemNo">所持アイテムNo</param>
        /// <returns>処理ステータス</returns>
        public int EquipCondition(int EquipID, int ItemNo)
        {
            // 装備アイテム基本データ取得
            HaveItem.DefaultView.RowFilter = "box_type=" + Status.ItemBox.Normal + " and have_no=" + ItemNo;
            if (HaveItem.DefaultView.Count == 0) { return Status.Equip.NoHaveItemError; }

            CommonUnitDataEntity.have_item_listRow EquipRow = (CommonUnitDataEntity.have_item_listRow)HaveItem.DefaultView[0].Row;

            CommonItemEntity.item_listRow EquippingItemRow = LibItem.GetItemRow(EquipRow.it_num, EquipRow.created);

            // 01.装備済みか
            if (EquipRow.equip_spot == Status.EquipSpot.Main && EquipID != Status.EquipSpot.Main)
            {
                // メインに装備している場合（かつ、メイン以外に装備する場合）
                if (EquipID != Status.EquipSpot.Sub || EquipRow.it_box_count == 1)
                {
                    return Status.Equip.MainEquiped;
                }
                // 装備箇所がサブで所持数が１個以上の場合のみ、次の判定へ
            }
            if (EquipRow.equip_spot == Status.EquipSpot.Sub && EquipID != Status.EquipSpot.Sub)
            {
                // サブに装備している場合（かつ、サブ以外に装備する場合）
                if (EquipID != Status.EquipSpot.Main || EquipRow.it_box_count == 1)
                {
                    return Status.Equip.SubEquiped;
                }
                // 装備箇所がメインで所持数が１個以上の場合のみ、次の判定へ
            }
            if (EquipRow.equip_spot == Status.EquipSpot.Head && EquipID != Status.EquipSpot.Head)
            {
                // 頭部に装備している場合（かつ、頭部以外に装備する場合）
                return Status.Equip.HeadEquiped;
            }
            if (EquipRow.equip_spot == Status.EquipSpot.Body && EquipID != Status.EquipSpot.Body)
            {
                // 胴体に装備している場合（かつ、胴体以外に装備する場合）
                return Status.Equip.BodyEquiped;
            }
            if (EquipRow.equip_spot == Status.EquipSpot.Accesory && EquipID != Status.EquipSpot.Accesory)
            {
                // 装飾に装備している場合（かつ、装飾以外に装備する場合）
                return Status.Equip.AccessoryEquiped;
            }

            // 02.装備箇所判定
            bool DoubleHand = false;// 二刀流判定実行フラグ

            switch (EquipID)
            {
                case Status.EquipSpot.Main:
                    if (EquippingItemRow.it_equip_parts != EquipID) { return Status.Equip.SpotError; }

                    // 盾装備の場合
                    if (EquippingItemRow.it_equip_parts == EquipID && EquippingItemRow.it_both_hand)
                    {
                        HaveItem.DefaultView.RowFilter = "equip_spot=" + Status.EquipSpot.Sub;

                        if (HaveItem.DefaultView.Count > 0)
                        {
                            if (LibItem.GetType((int)HaveItem.DefaultView[0]["it_num"], (bool)HaveItem.DefaultView[0]["created"]) == 33)
                            {
                                // サブに盾装備してる
                                return Status.Equip.SubEquipGreatShield;
                            }
                        }
                    }
                    break;
                case Status.EquipSpot.Sub:
                    if (EquippingItemRow.it_equip_parts != EquipID)
                    {
                        // スキル「両手利き or 二刀流」所持判定（所持の場合は例外的にOK）
                        if (EffectList.FindByeffect_id(891) == null && EffectList.FindByeffect_id(892) == null)
                        {
                            // 所持していないのでエラー
                            return Status.Equip.SpotError;
                        }

                        DoubleHand = true;
                    }

                    // 盾装備の場合
                    if (EquippingItemRow.it_equip_parts == EquipID && EquippingItemRow.it_type == 33)
                    {
                        HaveItem.DefaultView.RowFilter = "equip_spot=" + Status.EquipSpot.Main;

                        if (HaveItem.DefaultView.Count > 0)
                        {
                            int MainWeaponNoBoth = (int)HaveItem.DefaultView[0]["it_num"];
                            bool MainWeaponCreatedBoth = (bool)HaveItem.DefaultView[0]["created"];
                            CommonItemEntity.item_listRow MainBothItemRow = LibItem.GetItemRow(MainWeaponNoBoth, MainWeaponCreatedBoth);

                            if (MainBothItemRow.it_both_hand)
                            {
                                // メインが両手武器です
                                return Status.Equip.MainWeaponIsTwoHands;
                            }
                        }
                    }
                    break;
                case Status.EquipSpot.Head:
                    if (EquippingItemRow.it_equip_parts != EquipID) { return Status.Equip.SpotError; }
                    break;
                case Status.EquipSpot.Body:
                    if (EquippingItemRow.it_equip_parts != EquipID) { return Status.Equip.SpotError; }
                    break;
                default:
                    if (EquippingItemRow.it_equip_parts != EquipID) { return Status.Equip.SpotError; }
                    break;
            }

            // 03.二刀流判定
            if (DoubleHand)
            {
                // メイン武器が片手武器か
                HaveItem.DefaultView.RowFilter = "equip_spot=" + Status.EquipSpot.Main;

                if (HaveItem.DefaultView.Count == 0)
                {
                    // メイン武器を装備していない
                    return Status.Equip.MainWeaponIsNoEquiped;
                }

                int MainWeaponNo = (int)HaveItem.DefaultView[0]["it_num"];
                bool MainWeaponCreated = (bool)HaveItem.DefaultView[0]["created"];
                CommonItemEntity.item_listRow MainItemRow = LibItem.GetItemRow(MainWeaponNo, MainWeaponCreated);
                if (MainItemRow.it_both_hand)
                {
                    // メインが両手武器です
                    return Status.Equip.MainWeaponIsTwoHands;
                }

                // サブウェポンが片手武器か
                if (EquippingItemRow.it_both_hand)
                {
                    // サブが両手武器です
                    return Status.Equip.SubWeaponIsTwoHands;
                }
            }

            // 04.種族装備制限判定
            if (EquippingItemRow.it_ok_sex > 0 && _sex > 0)
            {
                // 性別判定。不明はすべて装備できる
                if (EquippingItemRow.it_ok_sex != _sex)
                {
                    // 性別がマッチしない
                    return Status.Equip.SexMatch;
                }
            }
            if (EquippingItemRow.it_ok_race > 0 && EquippingItemRow.it_ok_race != _race)
            {
                // 種族がマッチしない
                return Status.Equip.RaceMatch;
            }

            // 05.レベル制限装備判定
            if (EquippingItemRow.it_equip_level > InstallClassLevel)
            {
                return Status.Equip.NotLevel;
            }

            // 06.インストール制限装備判定
            string[] Install = EquippingItemRow.it_equip_install.Split(',');
            bool IsInstallOk = false;
            for (int i = 0; i < Install.Length; i++)
            {
                if (int.Parse(Install[i]) == IntallClassID)
                {
                    IsInstallOk = true;
                }
            }
            if (!IsInstallOk)
            {
                return Status.Equip.InstallMatch;
            }

            return Status.Equip.OK;
        }

        /// <summary>
        /// メッセージ付きアイテム取得処理
        /// </summary>
        /// <param name="GetItemNo">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <param name="GetBox">個数</param>
        /// <param name="TextMsg">メッセージ</param>
        /// <returns>成功したか否か</returns>
        public bool AddItemWithMessage(int GetItemNo, bool IsCreated, int GetBox, ref string TextMsg)
        {
            return AddItemWithMessage(GetItemNo, IsCreated, GetBox, true, ref TextMsg);
        }

        /// <summary>
        /// メッセージ付きアイテム取得処理
        /// </summary>
        /// <param name="GetItemNo">アイテム番号</param>
        /// <param name="IsCreated">作成フラグ</param>
        /// <param name="GetBox">個数</param>
        /// <param name="FixMineName">自分の名前をメッセージに含めるか</param>
        /// <param name="TextMsg">メッセージ格納クラス</param>
        /// <returns>成功したか否か</returns>
        public bool AddItemWithMessage(int GetItemNo, bool IsCreated, int GetBox, bool FixMineName, ref string TextMsg)
        {
            int ItemNo = 0;

            string OverOneBox = "";
            if (GetBox > 1)
            {
                OverOneBox = "[" + GetBox + "個]";
            }

            string MineName = "";

            if (FixMineName)
            {
                MineName = NickName + "は";
            }

            bool ReturnFlag = AddItem(Status.ItemBox.Normal, GetItemNo, IsCreated, ref GetBox, ref ItemNo);

            if (ReturnFlag)
            {
                //表示
                TextMsg = "<div class=\"sys_mes\">" + MineName + LibResultText.CSSEscapeItem(LibItem.GetItemName(GetItemNo, IsCreated)) + OverOneBox + "を手に入れた。</div>";
            }
            else
            {
                //表示
                TextMsg = "<div class=\"sys_mes\">" + MineName + LibResultText.CSSEscapeItem(LibItem.GetItemName(GetItemNo, IsCreated)) + OverOneBox + "を持てなかった為、諦めた……。</div>";
            }

            return ReturnFlag;
        }

        /// <summary>
        /// 所持ボックス番号
        /// </summary>
        /// <returns>ボックスの番号リスト</returns>
        public int[] GetBoxItemNumbers()
        {
            HaveItem.DefaultView.RowFilter = "box_type=" + Status.ItemBox.Box;

            List<int> Items = new List<int>();

            foreach (DataRowView BazzerUnLockRow in HaveItem.DefaultView)
            {
                Items.Add((int)BazzerUnLockRow["have_no"]);
            }

            return Items.ToArray();
        }

        /// <summary>
        /// 所持数の確認
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <returns>所持数以下ならばfalse、いっぱいならばtrue</returns>
        public bool CheckHaveItemMax(int BoxType)
        {
            return CheckHaveItemMax(BoxType, 1);
        }

        /// <summary>
        /// 所持数の確認
        /// </summary>
        /// <param name="BoxType">所持種別</param>
        /// <param name="BoxValid">必要な空き数</param>
        /// <returns>所持数以下ならばfalse、いっぱいならばtrue</returns>
        public bool CheckHaveItemMax(int BoxType, int BoxValid)
        {
            HaveItem.DefaultView.RowFilter = "box_type=" + BoxType;

            switch (BoxType)
            {
                case Status.ItemBox.Normal:
                    if ((HaveItem.DefaultView.Count + BoxValid) > MaxHaveItem)
                    {
                        return true;
                    }
                    break;
                case Status.ItemBox.Box:
                    return false;
            }

            return false;
        }

        /// <summary>
        /// 貴重品を入手
        /// </summary>
        /// <param name="KeyItemID">貴重品番号</param>
        /// <returns>成功したか否か</returns>
        public bool AddKeyItem(int KeyItemID)
        {
            bool IsOK = false;

            CommonUnitDataEntity.key_item_listRow Row = KeyItemList.FindBykey_item_id(KeyItemID);

            if (Row != null)
            {
                // すでに持っている場合
                IsOK = false;
            }
            else
            {
                // 持っていない場合、新たに設定する
                CommonUnitDataEntity.key_item_listRow NewKeyItem = KeyItemList.Newkey_item_listRow();

                NewKeyItem.key_item_id = KeyItemID;
                NewKeyItem._new = true;

                KeyItemList.Addkey_item_listRow(NewKeyItem);

                IsOK = true;
            }

            return IsOK;

        }

        /// <summary>
        /// キーアイテムを除去(アイテム番号指定)
        /// </summary>
        /// <param name="KeyItemID">貴重品番号</param>
        /// <returns>成功したか否か</returns>
        public bool KeyItemNoRemoveKeyItem(int KeyItemID)
        {
            CommonUnitDataEntity.key_item_listRow Row = KeyItemList.FindBykey_item_id(KeyItemID);

            if (Row == null)
            {
                return false;
            }

            Row.Delete();

            return true;
        }
    }
}
