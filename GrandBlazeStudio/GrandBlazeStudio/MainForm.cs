using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CommonLibrary;
using System.Threading;
using CommonFormLibrary;
using DataEditForm;
using CommonLibrary.Character;
using CommonLibrary.DataFormat.Entity;

namespace GrandBlazeStudio
{
    public partial class MainForm : Form
    {
        static private Mutex _mutex;

        public MainForm()
        {
            InitializeComponent();
        }

        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.GRBClientSize = this.ClientSize;
            Properties.Settings.Default.GRBLocation = this.Location;
            Properties.Settings.Default.GRBWindowState = this.WindowState;

            // 設定ファイルの内容保存
            GrandBlazeStudio.Properties.Settings.Default.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _mutex = new Mutex(false, "GrandBlazeStudio");

            if (_mutex.WaitOne(0, false) == false)
            {
                Application.Exit();
            }

            this.WindowState = GrandBlazeStudio.Properties.Settings.Default.GRBWindowState;
            this.ClientSize = GrandBlazeStudio.Properties.Settings.Default.GRBClientSize;
            this.Location = GrandBlazeStudio.Properties.Settings.Default.GRBLocation;

            GrandBlazeStudio.MainFormItem.MainFromDoc mainFrom = new GrandBlazeStudio.MainFormItem.MainFromDoc();

            mainFrom.MdiParent = this;
            mainFrom.Show();
        }

        private void toolStripMenuItemDataVase_Click(object sender, EventArgs e)
        {
            DataBaseMain database = new DataBaseMain();
            database.ShowDialog(this);
        }

        private void キャラクターデータクリアToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("キャラクターデータをすべてクリアします。よろしいですか？", "キャラデータクリア", MessageBoxButtons.OKCancel) == DialogResult.Cancel) { return; }

            LibUnitBaseMini CharaMini = new LibUnitBaseMini();
            foreach (CharacterDataEntity.ts_character_listRow CharaRow in CharaMini.GetCharacters())
            {
                LibPlayer chara = new LibPlayer(Status.Belong.Friend, CharaRow.entry_no);
                chara.Delete(GrandBlazeStudio.Properties.Settings.Default.UpdateCnt);
            }
            MessageBox.Show("キャラクターデータクリアが完了しました。");
        }

    }
}