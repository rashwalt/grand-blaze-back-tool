using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.DataAccess;

namespace GrandBlazeStudio.MainFormItem
{
    public partial class UpdateProcess : Form
    {
        public UpdateProcess()
        {
            InitializeComponent();
        }

        private bool IsDataImport = true;
        private bool IsCharacterAction = true;
        private bool IsPartyAction = true;
        private bool IsStatusAndList = true;
        private bool IsUpdate = true;
        private int MaxProcessCnt = 0;
        private int ProcessCnt = 0;
        private string PartyNoList = "";
        private string PrivateNoList = "";
        private bool IsNewPlayerReset = true;
        private void DoWork()
        {
            // 処理の実行
            progressBar.Maximum = MaxProcessCnt;

            // 更新処理の実行
            int i;
            string ProcessStr = "";
            Application.DoEvents();

            for (i = 1; i < 6; i++)
            {
                Application.DoEvents();

                if (i == 1 && !IsDataImport) { continue; }
                else if (i == 2 && !IsCharacterAction) { continue; }
                else if (i == 3 && !IsPartyAction) { continue; }
                else if (i == 4 && !IsStatusAndList) { continue; }
                else if (i == 5 && !IsUpdate) { continue; }

                switch (i)
                {
                    case 1:
                        ProcessStr = "データインポート";
                        break;
                    case 2:
                        ProcessStr = "キャラクター行動";
                        break;
                    case 3:
                        ProcessStr = "パーティ行動";
                        break;
                    case 4:
                        ProcessStr = "ステータス表示/リスト作成";
                        break;
                    case 5:
                        ProcessStr = "アップデート処理";
                        break;
                }

                int percent = (int)((double)ProcessCnt / (double)MaxProcessCnt * 100.0);

                progressBar.PerformStep();
                this.labelUpdateText.Text = "更新状況: " + ProcessStr + "を処理中です... " + percent + "％完了";

                Application.DoEvents();

                // iの値によって行う処理切り替え
                switch (i)
                {
                    case 1:
                        // データのインポート
                        RegistSet.DataImport.DataImportMain impData = new GrandBlazeStudio.RegistSet.DataImport.DataImportMain();
                        impData.Imports();

                        LibDBBackup.Done(1, GrandBlazeStudio.Properties.Settings.Default.UpdateCnt);
                        break;
                    case 2:
                        // 新規登録処理
                        RegistSet.NewPlay.NewGameMain newGame = new GrandBlazeStudio.RegistSet.NewPlay.NewGameMain();
                        newGame.NewGameSet();
                        // キャラクター行動
                        RegistSet.CharacterAction.CharacterAction chAction = new GrandBlazeStudio.RegistSet.CharacterAction.CharacterAction();
                        chAction.Action(PrivateNoList);

                        if (PrivateNoList.Length == 0)
                        {
                            LibDBBackup.Done(2, GrandBlazeStudio.Properties.Settings.Default.UpdateCnt);
                        }
                        break;
                    case 3:
                        // パーティ行動
                        RegistSet.PartyAction.PartyActionMain ptAction = new GrandBlazeStudio.RegistSet.PartyAction.PartyActionMain();
                        ptAction.Action(PartyNoList);

                        if (PartyNoList.Length == 0)
                        {
                            LibDBBackup.Done(3, GrandBlazeStudio.Properties.Settings.Default.UpdateCnt);
                        }
                        break;
                    case 4:
                        // ステータスリスト
                        RegistSet.StatusList.StatusListMain stList = new GrandBlazeStudio.RegistSet.StatusList.StatusListMain();
                        stList.Draw();
                        break;
                    case 5:
                        // 最終処理
                        RegistSet.Update.UpdateMain upDate = new GrandBlazeStudio.RegistSet.Update.UpdateMain();
                        upDate.Do(IsNewPlayerReset);
                        if (IsNewPlayerReset)
                        {
                            RegistSet.Update.ArchiverSetup arSet = new GrandBlazeStudio.RegistSet.Update.ArchiverSetup();
                            arSet.Done();
                        }
                        break;
                }

                ProcessCnt++;

                int percent2 = (int)((double)ProcessCnt / (double)MaxProcessCnt * 100.0);
                this.labelUpdateText.Text = "更新状況: " + ProcessStr + "を処理中です... " + percent2 + "％完了";

                Application.DoEvents();
            }

            this.labelUpdateText.Text = "全更新処理完了";

            MessageBox.Show("すべての処理が完了しました。", "処理完了", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        public void SetProcess(bool Imports, bool Chara, bool Party, bool List, bool Update)
        {
            IsDataImport = Imports;
            IsCharacterAction = Chara;
            IsPartyAction = Party;
            IsStatusAndList = List;
            IsUpdate = Update;

            MaxProcessCnt = 0;

            if (IsDataImport) { MaxProcessCnt++; }
            if (IsCharacterAction) { MaxProcessCnt++; }
            if (IsPartyAction) { MaxProcessCnt++; }
            if (IsStatusAndList) { MaxProcessCnt++; }
            if (IsUpdate) { MaxProcessCnt++; }
        }

        public void SetEntryOnlyOn(string BattlePartyNos, string PrivateNos, bool NewPlayerReset)
        {
            PartyNoList = BattlePartyNos;
            PrivateNoList = PrivateNos;
            IsNewPlayerReset = NewPlayerReset;
        }

        private void UpdateProcess_Shown(object sender, EventArgs e)
        {
            // 更新の開始
            ProcessCnt = 0;

            Application.DoEvents();
            DoWork();
        }
    }
}