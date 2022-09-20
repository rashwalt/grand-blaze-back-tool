using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.DataAccess;

namespace GrandBlazeStudio.MainFormItem
{
    public partial class MainFromDoc : Form
    {
        public MainFromDoc()
        {
            InitializeComponent();
        }

        private void buttonExeStart_Click(object sender, EventArgs e)
        {
            if (this.radioWedding.Checked)
            {
                MainFormItem.EventJoinForm evtForm = new EventJoinForm();
                evtForm.ShowDialog(this);
            }

            MainFormItem.UpdateProcess updForm = new GrandBlazeStudio.MainFormItem.UpdateProcess();

            updForm.SetProcess(this.checkBoxDataImport.Checked, this.checkBoxCharacterAction.Checked, this.checkBoxPartyAction.Checked, this.checkBoxListStatus.Checked, this.checkBoxUpdate.Checked);
            updForm.SetEntryOnlyOn(this.textBoxPartyEditOn.Text, this.textBoxPrivateEditOn.Text, this.checkBoxNewPlayerReset.Checked);
            updForm.ShowDialog(this);
            updForm.Dispose();
        }

        private void MainFromDoc_Load(object sender, EventArgs e)
        {
            // 基本データ表示
            this.numericUpDownUpdateCnt.Value = GrandBlazeStudio.Properties.Settings.Default.UpdateCnt;

            if (GrandBlazeStudio.Properties.Settings.Default.UseProduction)
            {
                this.radioButtonDev.Checked = false;
                this.radioButtonPro.Checked = true;
            }
            else
            {
                this.radioButtonDev.Checked = true;
                this.radioButtonPro.Checked = false;
            }

            ConfigLoad();
        }

        private void ConfigLoad()
        {
            // データのロード
            LibCommonLibrarySettings.DataBaseInstance = this.textBoxInstance.Text;
            LibCommonLibrarySettings.UpdateCnt = GrandBlazeStudio.Properties.Settings.Default.UpdateCnt;
            LibCommonLibrarySettings.Seed = GrandBlazeStudio.Properties.Settings.Default.Seed;
            LibCommonLibrarySettings.ResultBasePath = GrandBlazeStudio.Properties.Settings.Default.ResultBasePath;
            LibCommonLibrarySettings.ResultBackupPath = GrandBlazeStudio.Properties.Settings.Default.ResultBackupPath;
            LibCommonLibrarySettings.Characters = GrandBlazeStudio.Properties.Settings.Default.Characters;
            LibCommonLibrarySettings.Partys = GrandBlazeStudio.Properties.Settings.Default.Partys;
            LibCommonLibrarySettings.Privates = GrandBlazeStudio.Properties.Settings.Default.Privates;
            LibCommonLibrarySettings.DataBaseBackupPath = GrandBlazeStudio.Properties.Settings.Default.DataBaseBackupPath;
            LibCommonLibrarySettings.DataBasePathMaster = GrandBlazeStudio.Properties.Settings.Default.DataBasePathMaster;
            LibCommonLibrarySettings.DataBasePathMDF = GrandBlazeStudio.Properties.Settings.Default.DataBasePathMDF;
            LibCommonLibrarySettings.EventScriptDir = GrandBlazeStudio.Properties.Settings.Default.EventScriptDir;
            LibCommonLibrarySettings.EditorPath = GrandBlazeStudio.Properties.Settings.Default.EditorPath;
            LibCommonLibrarySettings.IsProduction = GrandBlazeStudio.Properties.Settings.Default.UseProduction;
            LibCommonLibrarySettings.TestDataBasePathMDF = GrandBlazeStudio.Properties.Settings.Default.DataBasePathMDFTest;
        }

        private void numericUpDownUpdateCnt_ValueChanged(object sender, EventArgs e)
        {
            GrandBlazeStudio.Properties.Settings.Default.UpdateCnt = (int)this.numericUpDownUpdateCnt.Value;
            LibCommonLibrarySettings.UpdateCnt = GrandBlazeStudio.Properties.Settings.Default.UpdateCnt;
        }

        private void checkBoxCharacterAction_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxPrivateEditOn.Enabled = this.checkBoxCharacterAction.Checked;
        }

        private void checkBoxPartyAction_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxPartyEditOn.Enabled = this.checkBoxPartyAction.Checked;
        }

        private void checkBoxUpdate_CheckedChanged(object sender, EventArgs e)
        {
            this.checkBoxNewPlayerReset.Enabled = this.checkBoxUpdate.Checked;
        }

        private void radioNewYear_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioNewYear.Checked)
            {
                LibQuest.OfficialEventID = 1;
            }
        }

        private void radioNoEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioNoEvent.Checked)
            {
                LibQuest.OfficialEventID = 0;
            }
        }

        private void radioHinamatsuri_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioHinamatsuri.Checked)
            {
                LibQuest.OfficialEventID = 2;
            }
        }

        private void radioBujinsai_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioBujinsai.Checked)
            {
                LibQuest.OfficialEventID = 3;
            }
        }

        private void radioSummerEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioSummerEvent.Checked)
            {
                LibQuest.OfficialEventID = 4;
            }
        }

        private void radioTrickOrTreat_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioTrickOrTreat.Checked)
            {
                LibQuest.OfficialEventID = 5;
            }
        }

        private void radioSaintCristmas_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioSaintCristmas.Checked)
            {
                LibQuest.OfficialEventID = 6;
            }
        }

        private void radioWedding_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioWedding.Checked)
            {
                LibQuest.OfficialEventID = 7;
            }
        }

        private void buttonDetouch_Click(object sender, EventArgs e)
        {
            LibDBBackup.Detouch();
            MessageBox.Show("デタッチが完了しました。");
        }

        private void textBoxInstance_TextChanged(object sender, EventArgs e)
        {
            LibCommonLibrarySettings.DataBaseInstance = this.textBoxInstance.Text;
        }

        private void buttonStatusView_Click(object sender, EventArgs e)
        {
            StringBuilder str_hp = new StringBuilder();
            StringBuilder str_mp = new StringBuilder();
            StringBuilder str_str = new StringBuilder();
            StringBuilder str_agi = new StringBuilder();
            StringBuilder str_mag = new StringBuilder();
            StringBuilder str_unq = new StringBuilder();
            foreach (CommonLibrary.DataFormat.Entity.InstallDataEntity.mt_install_class_listRow InstallRow in LibInstall.Entity.mt_install_class_list)
            {
                str_hp.Append(InstallRow.classname);
                str_mp.Append(InstallRow.classname);
                str_str.Append(InstallRow.classname);
                str_agi.Append(InstallRow.classname);
                str_mag.Append(InstallRow.classname);
                str_unq.Append(InstallRow.classname);
                for (int level = 1; level < 100; level++)
                {
                    int _max_hp = (int)LibRankData.GetRankToHP(InstallRow.up_hp, level);
                    int _max_mp = (int)LibRankData.GetRankToMP(InstallRow.up_mp, level);
                    int _str = (int)LibRankData.GetRankToSTR(InstallRow.up_str, level);
                    int _agi = (int)LibRankData.GetRankToSPD(InstallRow.up_agi, level);
                    int _mag = (int)LibRankData.GetRankToMAG(InstallRow.up_mag, level);
                    int _unq = (int)LibRankData.GetRankToVIT(InstallRow.up_unq, level);

                    str_hp.Append("\t" + _max_hp);
                    str_mp.Append("\t" + _max_mp);
                    str_str.Append("\t" + _str);
                    str_agi.Append("\t" + _agi);
                    str_mag.Append("\t" + _mag);
                    str_unq.Append("\t" + _unq);
                }

                str_hp.AppendLine("");
                str_mp.AppendLine("");
                str_str.AppendLine("");
                str_agi.AppendLine("");
                str_mag.AppendLine("");
                str_unq.AppendLine("");
            }

            //for (int i = 0; i <= 6; i++)
            //{
            //    int _max_hp2 = (int)LibRankData.GetRankToHP(i, 1);
            //    int _max_mp2 = (int)LibRankData.GetRankToMP(i, 1);
            //    int _str2 = (int)LibRankData.GetRankToSTR(i, 1);
            //    int _agi2 = (int)LibRankData.GetRankToSPD(i, 1);
            //    int _mag2 = (int)LibRankData.GetRankToMAG(i, 1);
            //    int _unq2 = (int)LibRankData.GetRankToVIT(i, 1);

            //    str_hp.Append(_max_hp2);
            //    str_mp.Append(_max_mp2);
            //    str_str.Append(_str2);
            //    str_agi.Append(_agi2);
            //    str_mag.Append(_mag2);
            //    str_unq.Append(_unq2);
            //    for (int level = 10; level < 100; level+=10)
            //    {
            //        int _max_hp = (int)LibRankData.GetRankToHP(i, level);
            //        int _max_mp = (int)LibRankData.GetRankToMP(i, level);
            //        int _str = (int)LibRankData.GetRankToSTR(i, level);
            //        int _agi = (int)LibRankData.GetRankToSPD(i, level);
            //        int _mag = (int)LibRankData.GetRankToMAG(i, level);
            //        int _unq = (int)LibRankData.GetRankToVIT(i, level);

            //        str_hp.Append("\t" + _max_hp);
            //        str_mp.Append("\t" + _max_mp);
            //        str_str.Append("\t" + _str);
            //        str_agi.Append("\t" + _agi);
            //        str_mag.Append("\t" + _mag);
            //        str_unq.Append("\t" + _unq);
            //    }
            //    int _max_hp3 = (int)LibRankData.GetRankToHP(i, 99);
            //    int _max_mp3 = (int)LibRankData.GetRankToMP(i, 99);
            //    int _str3 = (int)LibRankData.GetRankToSTR(i, 99);
            //    int _agi3 = (int)LibRankData.GetRankToSPD(i, 99);
            //    int _mag3 = (int)LibRankData.GetRankToMAG(i, 99);
            //    int _unq3 = (int)LibRankData.GetRankToVIT(i, 99);

            //    str_hp.Append("\t" + _max_hp3);
            //    str_mp.Append("\t" + _max_mp3);
            //    str_str.Append("\t" + _str3);
            //    str_agi.Append("\t" + _agi3);
            //    str_mag.Append("\t" + _mag3);
            //    str_unq.Append("\t" + _unq3);
            //    str_hp.AppendLine("");
            //    str_mp.AppendLine("");
            //    str_str.AppendLine("");
            //    str_agi.AppendLine("");
            //    str_mag.AppendLine("");
            //    str_unq.AppendLine("");
            //}
            

            StringBuilder str = new StringBuilder();
            str.AppendLine(str_hp.ToString());
            str.AppendLine("");
            str.AppendLine(str_mp.ToString());
            str.AppendLine("");
            str.AppendLine(str_str.ToString());
            str.AppendLine("");
            str.AppendLine(str_agi.ToString());
            str.AppendLine("");
            str.AppendLine(str_mag.ToString());
            str.AppendLine("");
            str.AppendLine(str_unq.ToString());
            str.AppendLine("");

            this.textBoxTabView.Text = str.ToString();
        }

        private void radioButtonDev_CheckedChanged(object sender, EventArgs e)
        {
            changedTransactions();
        }

        private void radioButtonPro_CheckedChanged(object sender, EventArgs e)
        {
            changedTransactions();
        }

        private void changedTransactions()
        {
            if (this.radioButtonPro.Checked)
            {
                GrandBlazeStudio.Properties.Settings.Default.UseProduction = true;
            }
            else
            {
                GrandBlazeStudio.Properties.Settings.Default.UseProduction = false;
            }
            LibCommonLibrarySettings.IsProduction = GrandBlazeStudio.Properties.Settings.Default.UseProduction;
        }
    }
}