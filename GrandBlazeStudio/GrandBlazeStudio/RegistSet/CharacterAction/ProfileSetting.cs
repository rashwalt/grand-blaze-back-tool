using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.Character;

namespace GrandBlazeStudio.RegistSet.CharacterAction
{
    partial class CharacterAction
    {
        /// <summary>
        /// プロフィール設定
        /// </summary>
        private void ProfileSetting()
        {
            int ImageSizeWidth = 0;
            int ImageSizeHeight = 0;

            foreach (LibPlayer Mine in CharaList)
            {
                int EntryNo = Mine.EntryNo;

                ContinueDataEntity.ts_continue_profileRow ContinueProfileRow = con.Entity.ts_continue_profile.FindByentry_no(EntryNo);

                if (ContinueProfileRow == null)
                {
                    // ない場合はスキップ
                    continue;
                }

                // 愛称変更
                if (ContinueProfileRow.nick_name.Length > 0 && ContinueProfileRow.nick_name != Mine.NickName)
                {
                    Mine.NickName = ContinueProfileRow.nick_name;

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "愛称を" + Mine.NickName + "に変更した。", Status.MessageLevel.Normal);
                }

                // 年齢変更
                if (ContinueProfileRow.age > 0 && ContinueProfileRow.age != Mine.Age)
                {
                    Mine.Age = ContinueProfileRow.age;

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "年齢を変更した。", Status.MessageLevel.Normal);
                }

                // 身長変更
                if (ContinueProfileRow.height > 0 && ContinueProfileRow.height != Mine.Height)
                {
                    Mine.Height = ContinueProfileRow.height;

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "身長を変更した。", Status.MessageLevel.Normal);
                }

                // 体重変更
                if (ContinueProfileRow.weight > 0 && ContinueProfileRow.weight != Mine.Weight)
                {
                    Mine.Weight = ContinueProfileRow.weight;

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "体重を変更した。", Status.MessageLevel.Normal);
                }

                // プロフィール変更
                if (ContinueProfileRow.profile.Length > 0 && ContinueProfileRow.profile != Mine.Profile)
                {
                    Mine.Profile = ContinueProfileRow.profile;

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "プロフィールを変更した。", Status.MessageLevel.Normal);
                }

                // 画像変更
                if (ContinueProfileRow.image_url.Replace("http://", "").Length > 0 && ContinueProfileRow.image_url != "http://www.grand-blaze.com/default.gif" && ContinueProfileRow.image_url != "http://www.grand-blaze.com/defimage.gif" && ContinueProfileRow.image_copyright.Length > 0 &&
                    (ContinueProfileRow.image_url != Mine.ImageURL || ContinueProfileRow.image_width != Mine.ImageWidthSize || ContinueProfileRow.image_height != Mine.ImageHeightSize || ContinueProfileRow.image_copyright != Mine.ImageCopyright))
                {
                    Mine.ImageURL = ContinueProfileRow.image_url;
                    ImageSizeWidth = ContinueProfileRow.image_width;
                    ImageSizeHeight = ContinueProfileRow.image_height;
                    if (ImageSizeWidth <= 0 || ImageSizeWidth > 250)
                    {
                        ImageSizeWidth = 250;
                    }
                    if (ImageSizeHeight <= 0 || ImageSizeHeight > 300)
                    {
                        ImageSizeHeight = 300;
                    }
                    Mine.ImageWidthSize = ImageSizeWidth;
                    Mine.ImageHeightSize = ImageSizeHeight;
                    Mine.ImageCopyright = ContinueProfileRow.image_copyright;

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "キャラクターグラフィックを変更した。", Status.MessageLevel.Normal);
                }
                else if (Mine.ImageURL.Replace("http://", "").Length > 0 && ContinueProfileRow.image_url == Mine.ImageURL)
                {
                    ImageSizeWidth = ContinueProfileRow.image_width;
                    ImageSizeHeight = ContinueProfileRow.image_height;
                    if (ImageSizeWidth <= 0 || ImageSizeWidth > 250)
                    {
                        ImageSizeWidth = 250;
                    }
                    if (ImageSizeHeight <= 0 || ImageSizeHeight > 300)
                    {
                        ImageSizeHeight = 300;
                    }
                    Mine.ImageWidthSize = ImageSizeWidth;
                    Mine.ImageHeightSize = ImageSizeHeight;
                    if (ContinueProfileRow.image_copyright.Length > 0)
                    {
                        Mine.ImageCopyright = ContinueProfileRow.image_copyright;
                    }

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "キャラクターグラフィックを変更した。", Status.MessageLevel.Normal);
                }
                else if (ContinueProfileRow.image_url == "http://www.grand-blaze.com/defimage.gif")
                {
                    Mine.ImageURL = "";
                    ImageSizeWidth = 0;
                    ImageSizeHeight = 0;
                    Mine.ImageCopyright = "";

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "キャラクターグラフィックをデフォルトに戻した。", Status.MessageLevel.Normal);
                }
                else if (ContinueProfileRow.image_url == "http://www.grand-blaze.com/default.gif")
                {
                    Mine.ImageURL = "http://www.grand-blaze.com/static/images/result/no_img.gif";
                    ImageSizeWidth = 250;
                    ImageSizeHeight = 300;
                    Mine.ImageCopyright = "Grand Blaze Products.";

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "キャラクターグラフィックを非表示にした。", Status.MessageLevel.Normal);
                }

                if (ContinueProfileRow.image_link_url.Replace("http://", "").Length > 0 && ContinueProfileRow.image_link_url != "http://www.grand-blaze.com/default.gif" && ContinueProfileRow.image_link_url != Mine.ImageLinkURL)
                {
                    Mine.ImageLinkURL = ContinueProfileRow.image_link_url;

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "キャラクター画像からのリンク先を変更した。", Status.MessageLevel.Normal);
                }
                else if (ContinueProfileRow.image_link_url == "http://www.grand-blaze.com/default.gif")
                {
                    Mine.ImageLinkURL = "";

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "キャラクター画像からのリンク先を削除した。", Status.MessageLevel.Normal);
                }

                // ユニーク名変更
                if (ContinueProfileRow.unique_name.Length > 0 && ContinueProfileRow.unique_name != Mine.UniqueName)
                {
                    Mine.UniqueName = ContinueProfileRow.unique_name;

                    LibPlayerMemo.AddSystemMessage(EntryNo, Status.PlayerSysMemoType.ProfileSetting, "大切なものを変更した。", Status.MessageLevel.Normal);
                }
            }
        }
    }
}
