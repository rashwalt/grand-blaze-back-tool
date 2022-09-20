using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataEditForm;
using CommonFormLibrary;
using DataEditForm.SkillGet;
using DataEditForm.Skill;
using DataEditForm.Item;
using DataEditForm.Monster.Data;
using DataEditForm.SetBonus;
using DataEditForm.InstallClass;
using DataEditForm.Area;
using DataEditForm.KeyItem;
using DataEditForm.Quest;
using DataEditForm.StatusData;
using DataEditForm.Guest;
using DataEditForm.Field;
using DataEditForm.Weather;
using DataEditForm.Trap;

namespace DataEditForm
{
    public partial class DataBaseMain : Form
    {
        public DataBaseMain()
        {
            InitializeComponent();
        }

        private List<BasePanel> DataBasePanels = new List<BasePanel>();

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataBaseMain_Load(object sender, EventArgs e)
        {
            this.SuspendLayout();

            try
            {
                DataBasePanels.Add(new SkillMainPanel());
                DataBasePanels.Add(new SkillGetListPanel());
                DataBasePanels.Add(new ItemMainPanel());
                DataBasePanels.Add(new GuestMainPanel());
                DataBasePanels.Add(new MonsterDataPanel());
                DataBasePanels.Add(new InstallClassPanel());
                DataBasePanels.Add(new AreaMarkPanel());
                DataBasePanels.Add(new KeyItemPanel());
                DataBasePanels.Add(new QuestEditPanel());
                DataBasePanels.Add(new StatusEditPanel());
                DataBasePanels.Add(new TrapDataPanel());
                DataBasePanels.Add(new FieldDataPanel());
                DataBasePanels.Add(new WeatherDataPanel());
                DataBasePanels.Add(new SetBonusPanel());

                foreach (BasePanel panel in DataBasePanels)
                {
                    TabPage pageTemp = new TabPage();

                    pageTemp.Text = panel.TitleName;
                    pageTemp.Location = new Point(4, 21);
                    pageTemp.Padding = new Padding(3);
                    pageTemp.UseVisualStyleBackColor = true;
                    pageTemp.BackColor = SystemColors.Control;
                    pageTemp.ForeColor = SystemColors.ControlText;

                    panel.Dock = DockStyle.Fill;
                    pageTemp.Controls.Add(panel);

                    this.tabControl.TabPages.Add(pageTemp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }

            this.ResumeLayout();
        }

        /// <summary>
        /// 閉じられるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataBaseMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (BasePanel panel in DataBasePanels)
            {
                if (panel.CheckModify())
                {
                    switch (MessageBox.Show(panel.TitleName + "が変更されています。終了してもよろしいですか？", "終了確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.OK:
                            panel.Cancel();
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            foreach (BasePanel panel in DataBasePanels)
            {
                panel.Save();
            }
            this.Close();
        }
    }
}
