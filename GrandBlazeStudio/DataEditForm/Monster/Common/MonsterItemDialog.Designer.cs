namespace DataEditForm.Monster.Common
{
    partial class MonsterItemDialog
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.itemSelectionPanelDrop = new CommonFormLibrary.CommonPanel.ItemSelectionPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.itemSelectionPanelStiel = new CommonFormLibrary.CommonPanel.ItemSelectionPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.itemSelectionPanelPoacher = new CommonFormLibrary.CommonPanel.ItemSelectionPanel();
            this.buttonSync = new System.Windows.Forms.Button();
            this.buttonSync2 = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(338, 6);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(430, 6);
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonSync2);
            this.panelMain.Controls.Add(this.buttonSync);
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Size = new System.Drawing.Size(528, 348);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(504, 305);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.itemSelectionPanelDrop);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(496, 279);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ドロップ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // itemSelectionPanelDrop
            // 
            this.itemSelectionPanelDrop.DataPropertyNameCount = "";
            this.itemSelectionPanelDrop.DataPropertyNameItemName = "";
            this.itemSelectionPanelDrop.DataPropertyNameProb = "";
            this.itemSelectionPanelDrop.DataSource = null;
            this.itemSelectionPanelDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemSelectionPanelDrop.GimlInclude = true;
            this.itemSelectionPanelDrop.GimlVisible = true;
            this.itemSelectionPanelDrop.Location = new System.Drawing.Point(3, 3);
            this.itemSelectionPanelDrop.Name = "itemSelectionPanelDrop";
            this.itemSelectionPanelDrop.ProbIsText = true;
            this.itemSelectionPanelDrop.ProbVisible = true;
            this.itemSelectionPanelDrop.Size = new System.Drawing.Size(490, 273);
            this.itemSelectionPanelDrop.TabIndex = 0;
            this.itemSelectionPanelDrop.EditClick += new CommonFormLibrary.CommonPanel.ItemSelectionPanel.SelectionItemEventHandler(this.itemSelectionPanelDrop_EditClick);
            this.itemSelectionPanelDrop.DeleteClick += new CommonFormLibrary.CommonPanel.ItemSelectionPanel.SelectionItemEventHandler(this.itemSelectionPanelDrop_DeleteClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.itemSelectionPanelStiel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(496, 279);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "スティール";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // itemSelectionPanelStiel
            // 
            this.itemSelectionPanelStiel.DataPropertyNameCount = "";
            this.itemSelectionPanelStiel.DataPropertyNameItemName = "";
            this.itemSelectionPanelStiel.DataPropertyNameProb = "";
            this.itemSelectionPanelStiel.DataSource = null;
            this.itemSelectionPanelStiel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemSelectionPanelStiel.GimlInclude = true;
            this.itemSelectionPanelStiel.GimlVisible = true;
            this.itemSelectionPanelStiel.Location = new System.Drawing.Point(3, 3);
            this.itemSelectionPanelStiel.Name = "itemSelectionPanelStiel";
            this.itemSelectionPanelStiel.ProbIsText = true;
            this.itemSelectionPanelStiel.ProbVisible = true;
            this.itemSelectionPanelStiel.Size = new System.Drawing.Size(490, 273);
            this.itemSelectionPanelStiel.TabIndex = 1;
            this.itemSelectionPanelStiel.EditClick += new CommonFormLibrary.CommonPanel.ItemSelectionPanel.SelectionItemEventHandler(this.itemSelectionPanelStiel_EditClick);
            this.itemSelectionPanelStiel.DeleteClick += new CommonFormLibrary.CommonPanel.ItemSelectionPanel.SelectionItemEventHandler(this.itemSelectionPanelStiel_DeleteClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.itemSelectionPanelPoacher);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(496, 279);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "密漁";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // itemSelectionPanelPoacher
            // 
            this.itemSelectionPanelPoacher.DataPropertyNameCount = "";
            this.itemSelectionPanelPoacher.DataPropertyNameItemName = "";
            this.itemSelectionPanelPoacher.DataPropertyNameProb = "";
            this.itemSelectionPanelPoacher.DataSource = null;
            this.itemSelectionPanelPoacher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemSelectionPanelPoacher.GimlInclude = true;
            this.itemSelectionPanelPoacher.GimlVisible = true;
            this.itemSelectionPanelPoacher.Location = new System.Drawing.Point(3, 3);
            this.itemSelectionPanelPoacher.Name = "itemSelectionPanelPoacher";
            this.itemSelectionPanelPoacher.ProbIsText = true;
            this.itemSelectionPanelPoacher.ProbVisible = true;
            this.itemSelectionPanelPoacher.Size = new System.Drawing.Size(490, 273);
            this.itemSelectionPanelPoacher.TabIndex = 2;
            this.itemSelectionPanelPoacher.EditClick += new CommonFormLibrary.CommonPanel.ItemSelectionPanel.SelectionItemEventHandler(this.itemSelectionPanelPoacher_EditClick);
            this.itemSelectionPanelPoacher.DeleteClick += new CommonFormLibrary.CommonPanel.ItemSelectionPanel.SelectionItemEventHandler(this.itemSelectionPanelPoacher_DeleteClick);
            // 
            // buttonSync
            // 
            this.buttonSync.Location = new System.Drawing.Point(292, 319);
            this.buttonSync.Name = "buttonSync";
            this.buttonSync.Size = new System.Drawing.Size(108, 23);
            this.buttonSync.TabIndex = 1;
            this.buttonSync.Text = "スティールに同期";
            this.buttonSync.UseVisualStyleBackColor = true;
            this.buttonSync.Click += new System.EventHandler(this.buttonSync_Click);
            // 
            // buttonSync2
            // 
            this.buttonSync2.Location = new System.Drawing.Point(406, 319);
            this.buttonSync2.Name = "buttonSync2";
            this.buttonSync2.Size = new System.Drawing.Size(108, 23);
            this.buttonSync2.TabIndex = 2;
            this.buttonSync2.Text = "密漁に同期";
            this.buttonSync2.UseVisualStyleBackColor = true;
            this.buttonSync2.Click += new System.EventHandler(this.buttonSync2_Click);
            // 
            // MonsterItemDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(528, 381);
            this.Name = "MonsterItemDialog";
            this.Text = "モンスター所持品";
            this.Load += new System.EventHandler(this.MonsterItemDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private CommonFormLibrary.CommonPanel.ItemSelectionPanel itemSelectionPanelDrop;
        private CommonFormLibrary.CommonPanel.ItemSelectionPanel itemSelectionPanelStiel;
        private System.Windows.Forms.Button buttonSync;
        private System.Windows.Forms.TabPage tabPage3;
        private CommonFormLibrary.CommonPanel.ItemSelectionPanel itemSelectionPanelPoacher;
        private System.Windows.Forms.Button buttonSync2;
    }
}
