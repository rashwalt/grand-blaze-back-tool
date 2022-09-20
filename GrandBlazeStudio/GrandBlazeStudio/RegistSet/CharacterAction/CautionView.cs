using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonLibrary;
using System.IO;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// 警告表示
        /// </summary>
        private void CautionView()
        {
            foreach (LibPlayer Chara in CharaList)
            {
                int EntryNo = Chara.EntryNo;

                switch (Chara.AccountStatus)
                {
                    case Status.Account.MailFailed:
                        // メールアドレス不正
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.CautionView, "登録されているメールアドレスが正確でないか、メール提供者・事業者により停止、または無効にされています。<br />有効なメールアドレスを登録してください。<br />※ご利用になれないメールアドレスを使用したままですと、アカウントが停止されることがあります。", Status.MessageLevel.Error);
                        break;
                    case Status.Account.YellowCard:
                        // 注意
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.CautionView, "Grand Blazeご利用規約に抵触している可能性があります。<br />心当たりのある場合は、抵触行為をやめてください。<br />心当たりがない、または手違いでこのメッセージが表示されている場合、お早めに<a href=\"http://www.grand-blaze.com/support/mail.html\" target=\"_blank\">ゲームマスター</a>までご連絡ください。", Status.MessageLevel.Caution);
                        Chara.AccountStatus = Status.Account.YellowCard;
                        break;
                    case Status.Account.RedCard:
                        // 警告
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.CautionView, "Grand Blazeご利用規約に違反している可能性があります。<br />心当たりのある場合は、違反行為を直ちにやめてください。<br />心当たりがない、または手違いでこのメッセージが表示されている場合、至急、<a href=\"http://www.grand-blaze.com/support/mail.html\" target=\"_blank\">ゲームマスター</a>までご連絡ください。", Status.MessageLevel.Error);
                        Chara.AccountStatus = Status.Account.Out;
                        break;
                    case Status.Account.Out:
                        // アカウントバン
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.CautionView, "こちらの判断でご利用のアカウントを停止させていただきました。", Status.MessageLevel.Error);
                        Chara.AccountStatus = Status.Account.Ban;
                        break;
                }

                if (Chara.AccountStatus >= 2 && Chara.AccountStatus <= 6)
                {
                    continue;
                }

                switch (Chara.ContinueNoCount)
                {
                    case LibConst.NoContinueCountMax - 1:
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.CautionView, "継続登録書を冒険者ギルドに提出してください。<br />次回も提出が認められなかった場合、「ＭＩＡ」として認定されます。ご注意下さい。", Status.MessageLevel.Caution);
                        break;
                    case LibConst.NoContinueCountMax:
                        LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.CautionView, "規定回数、継続登録書を冒険者ギルドに提出しなかったため、「ＭＩＡ」として認定されました。<br />現在所属しているパーティから離脱し、次回の更新時に削除されます。", Status.MessageLevel.Error);
                        break;
                }
            }
        }
    }
}
