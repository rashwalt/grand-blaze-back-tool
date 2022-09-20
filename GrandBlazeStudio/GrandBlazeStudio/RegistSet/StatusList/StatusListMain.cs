using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary;

namespace GrandBlazeStudio.RegistSet.StatusList
{
    partial class StatusListMain
    {
        public StatusListMain()
        {
            // コンストラクタ
        }

        private LibUnitBaseMini CharaMini = new LibUnitBaseMini();

        public void Draw()
        {
            // ステータス表示
            ViewStatusDraw();

            // トップリスト
            TopListDraw();

            // リスト設定
            ListDraw();

            // 新規キャラクターリスト
            NewPlayerDraw();
        }
    }
}
