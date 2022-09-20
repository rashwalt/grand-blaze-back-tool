using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// アカウント確認
        /// </summary>
        private void CheckAccount()
        {
            foreach (LibPlayer Chara in CharaList)
            {
                int EntryNo = Chara.EntryNo;
                int AccountStatus = Chara.AccountStatus;

                if (Chara.NewPlayRegistUpdate != GrandBlazeStudio.Properties.Settings.Default.UpdateCnt)
                {
                    // 新規作成キャラクター以外ならば
                    // 新規作成フラグ更新
                    Chara.IsNewPlayer = false;
                    Chara.IsInstallClassChanging = false;
                }

                // キャラクター削除が選択されている場合
                ContinueDataEntity.ts_continue_profileRow ContinueProfileRow = con.Entity.ts_continue_profile.FindByentry_no(EntryNo);

                if (ContinueProfileRow != null && ContinueProfileRow.account_status == Status.AccountStatus.Delete)
                {
                    // 削除実行
                    Chara.Delete(GrandBlazeStudio.Properties.Settings.Default.UpdateCnt);
                    continue;
                }

                if (AccountStatus == Status.Account.RedCard || AccountStatus == Status.Account.Out)
                {
                    // 警告、または強制退会処分の場合、登録はなかったものとして扱う
                    continue;
                }
                if (AccountStatus == Status.Account.Ban)
                {
                    // 削除
                    Chara.Delete(GrandBlazeStudio.Properties.Settings.Default.UpdateCnt);
                    continue;
                }

                // 継続登録確認
                ContinueDataEntity.ts_continue_mainRow ContinueMainRow = con.Entity.ts_continue_main.FindByentry_no(EntryNo);

                if (ContinueMainRow == null && Chara.AccountStatus != Status.Account.Freeze)
                {
                    // 継続登録されていない場合、未継続回数だけカウントしてスキップ
                    // 凍結の場合は処理しない
                    Chara.ContinueNoCount++;
                    Chara.ContinueBonus = 0;

                    // NOTE:警告メッセージ、未継続ペナルティは別途行う

                    if (Chara.ContinueNoCount >= LibConst.NoContinueCountMax)
                    {
                        // 削除の実行
                        Chara.Delete(GrandBlazeStudio.Properties.Settings.Default.UpdateCnt);
                    }

                    continue;
                }
                else if (ContinueMainRow == null && Chara.AccountStatus == Status.Account.Freeze)
                {
                    // 凍結状態の時
                    // 各種登録を削除
                    if (ContinueProfileRow != null)
                    {
                        ContinueProfileRow.Delete();
                        con.Entity.ts_continue_profile.AcceptChanges();
                    }

                    ContinueDataEntity.ts_continue_battle_preparationRow ContinueBattleRow = con.Entity.ts_continue_battle_preparation.FindByentry_no(EntryNo);
                    if (ContinueBattleRow != null)
                    {
                        ContinueBattleRow.Delete();
                        con.Entity.ts_continue_battle_preparation.AcceptChanges();
                    }

                    ContinueDataEntity.ts_continue_shoppingRow[] ContinueShoppingRows = (ContinueDataEntity.ts_continue_shoppingRow[])con.Entity.ts_continue_shopping.Select("entry_no=" + EntryNo);
                    if (ContinueShoppingRows.Length > 0)
                    {
                        foreach (ContinueDataEntity.ts_continue_shoppingRow TargetRow in ContinueShoppingRows)
                        {
                            TargetRow.Delete();
                        }
                        con.Entity.ts_continue_shopping.AcceptChanges();
                    }

                    ContinueDataEntity.ts_continue_messageRow[] ContinueMessageRows = (ContinueDataEntity.ts_continue_messageRow[])con.Entity.ts_continue_message.Select("entry_no=" + EntryNo);
                    if (ContinueMessageRows.Length > 0)
                    {
                        foreach (ContinueDataEntity.ts_continue_messageRow TargetRow in ContinueMessageRows)
                        {
                            TargetRow.Delete();
                        }
                        con.Entity.ts_continue_message.AcceptChanges();
                    }

                    ContinueDataEntity.ts_continue_tradeRow[] ContinueTradeRows = (ContinueDataEntity.ts_continue_tradeRow[])con.Entity.ts_continue_trade.Select("entry_no=" + EntryNo);
                    if (ContinueTradeRows.Length > 0)
                    {
                        foreach (ContinueDataEntity.ts_continue_tradeRow TargetRow in ContinueTradeRows)
                        {
                            TargetRow.Delete();
                        }
                        con.Entity.ts_continue_trade.AcceptChanges();
                    }

                    ContinueDataEntity.ts_continue_battle_actionRow[] ContinueActionRows = (ContinueDataEntity.ts_continue_battle_actionRow[])con.Entity.ts_continue_battle_action.Select("entry_no=" + EntryNo);
                    if (ContinueActionRows.Length > 0)
                    {
                        foreach (ContinueDataEntity.ts_continue_battle_actionRow TargetRow in ContinueActionRows)
                        {
                            TargetRow.Delete();
                        }
                        con.Entity.ts_continue_battle_action.AcceptChanges();
                    }

                    ContinueDataEntity.ts_continue_serifRow[] ContinueSerifRows = (ContinueDataEntity.ts_continue_serifRow[])con.Entity.ts_continue_serif.Select("entry_no=" + EntryNo);
                    if (ContinueSerifRows.Length > 0)
                    {
                        foreach (ContinueDataEntity.ts_continue_serifRow TargetRow in ContinueSerifRows)
                        {
                            TargetRow.Delete();
                        }
                        con.Entity.ts_continue_serif.AcceptChanges();
                    }

                    ContinueDataEntity.ts_continue_iconRow[] ContinueIconRows = (ContinueDataEntity.ts_continue_iconRow[])con.Entity.ts_continue_icon.Select("entry_no=" + EntryNo);
                    if (ContinueIconRows.Length > 0)
                    {
                        foreach (ContinueDataEntity.ts_continue_iconRow TargetRow in ContinueIconRows)
                        {
                            TargetRow.Delete();
                        }
                        con.Entity.ts_continue_icon.AcceptChanges();
                    }

                    ContinueDataEntity.ts_continue_buy_bazzerRow[] ContinueBuyBazzerRows = (ContinueDataEntity.ts_continue_buy_bazzerRow[])con.Entity.ts_continue_buy_bazzer.Select("entry_no=" + EntryNo);
                    if (ContinueBuyBazzerRows.Length > 0)
                    {
                        foreach (ContinueDataEntity.ts_continue_buy_bazzerRow TargetRow in ContinueBuyBazzerRows)
                        {
                            TargetRow.Delete();
                        }
                        con.Entity.ts_continue_buy_bazzer.AcceptChanges();
                    }

                    ContinueDataEntity.ts_continue_sell_bazzerRow[] ContinueSellBazzerRows = (ContinueDataEntity.ts_continue_sell_bazzerRow[])con.Entity.ts_continue_sell_bazzer.Select("entry_no=" + EntryNo);
                    if (ContinueSellBazzerRows.Length > 0)
                    {
                        foreach (ContinueDataEntity.ts_continue_sell_bazzerRow TargetRow in ContinueSellBazzerRows)
                        {
                            TargetRow.Delete();
                        }
                        con.Entity.ts_continue_sell_bazzer.AcceptChanges();
                    }

                    continue;
                }

                // 継続登録されている場合
                Chara.ContinueNoCount = 0;
                Chara.ContinueBonus++;
                Chara.LastUpdate = ContinueMainRow.time;

                if (Chara.AccountStatus == Status.Account.Freeze)
                {
                    // 凍結だった場合、解凍
                    Chara.AccountStatus = Status.Account.Normal;
                }
            }

            // 削除キャラクターを要素から排除
            CharaList.RemoveAll(chs => !chs.IsValid);
        }
    }
}
