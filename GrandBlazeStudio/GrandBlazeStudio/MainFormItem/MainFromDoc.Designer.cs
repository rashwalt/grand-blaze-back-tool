namespace GrandBlazeStudio.MainFormItem
{
    partial class MainFromDoc
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioWedding = new System.Windows.Forms.RadioButton();
            this.radioNoEvent = new System.Windows.Forms.RadioButton();
            this.radioSaintCristmas = new System.Windows.Forms.RadioButton();
            this.radioTrickOrTreat = new System.Windows.Forms.RadioButton();
            this.radioSummerEvent = new System.Windows.Forms.RadioButton();
            this.radioBujinsai = new System.Windows.Forms.RadioButton();
            this.radioHinamatsuri = new System.Windows.Forms.RadioButton();
            this.radioNewYear = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxNewPlayerReset = new System.Windows.Forms.CheckBox();
            this.textBoxPrivateEditOn = new System.Windows.Forms.TextBox();
            this.textBoxPartyEditOn = new System.Windows.Forms.TextBox();
            this.numericUpDownUpdateCnt = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonExeStart = new System.Windows.Forms.Button();
            this.checkBoxUpdate = new System.Windows.Forms.CheckBox();
            this.checkBoxListStatus = new System.Windows.Forms.CheckBox();
            this.checkBoxPartyAction = new System.Windows.Forms.CheckBox();
            this.checkBoxCharacterAction = new System.Windows.Forms.CheckBox();
            this.checkBoxDataImport = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonDetouch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStatusView = new System.Windows.Forms.Button();
            this.textBoxTabView = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxInstance = new System.Windows.Forms.TextBox();
            this.radioButtonDev = new System.Windows.Forms.RadioButton();
            this.radioButtonPro = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpdateCnt)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioWedding);
            this.groupBox2.Controls.Add(this.radioNoEvent);
            this.groupBox2.Controls.Add(this.radioSaintCristmas);
            this.groupBox2.Controls.Add(this.radioTrickOrTreat);
            this.groupBox2.Controls.Add(this.radioSummerEvent);
            this.groupBox2.Controls.Add(this.radioBujinsai);
            this.groupBox2.Controls.Add(this.radioHinamatsuri);
            this.groupBox2.Controls.Add(this.radioNewYear);
            this.groupBox2.Location = new System.Drawing.Point(285, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 289);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "特殊イベント";
            // 
            // radioWedding
            // 
            this.radioWedding.AutoSize = true;
            this.radioWedding.Location = new System.Drawing.Point(6, 172);
            this.radioWedding.Name = "radioWedding";
            this.radioWedding.Size = new System.Drawing.Size(135, 16);
            this.radioWedding.TabIndex = 10;
            this.radioWedding.TabStop = true;
            this.radioWedding.Text = "結婚式 - 公式サポート";
            this.radioWedding.UseVisualStyleBackColor = true;
            this.radioWedding.CheckedChanged += new System.EventHandler(this.radioWedding_CheckedChanged);
            // 
            // radioNoEvent
            // 
            this.radioNoEvent.AutoSize = true;
            this.radioNoEvent.Checked = true;
            this.radioNoEvent.Location = new System.Drawing.Point(6, 18);
            this.radioNoEvent.Name = "radioNoEvent";
            this.radioNoEvent.Size = new System.Drawing.Size(80, 16);
            this.radioNoEvent.TabIndex = 9;
            this.radioNoEvent.TabStop = true;
            this.radioNoEvent.Text = "イベント無し";
            this.radioNoEvent.UseVisualStyleBackColor = true;
            this.radioNoEvent.CheckedChanged += new System.EventHandler(this.radioNoEvent_CheckedChanged);
            // 
            // radioSaintCristmas
            // 
            this.radioSaintCristmas.AutoSize = true;
            this.radioSaintCristmas.Location = new System.Drawing.Point(6, 150);
            this.radioSaintCristmas.Name = "radioSaintCristmas";
            this.radioSaintCristmas.Size = new System.Drawing.Size(188, 16);
            this.radioSaintCristmas.TabIndex = 8;
            this.radioSaintCristmas.Text = "セント・クリスマス　アウラ祭 - 12/24";
            this.radioSaintCristmas.UseVisualStyleBackColor = true;
            this.radioSaintCristmas.CheckedChanged += new System.EventHandler(this.radioSaintCristmas_CheckedChanged);
            // 
            // radioTrickOrTreat
            // 
            this.radioTrickOrTreat.AutoSize = true;
            this.radioTrickOrTreat.Location = new System.Drawing.Point(6, 128);
            this.radioTrickOrTreat.Name = "radioTrickOrTreat";
            this.radioTrickOrTreat.Size = new System.Drawing.Size(138, 16);
            this.radioTrickOrTreat.TabIndex = 7;
            this.radioTrickOrTreat.Text = "Trick or Treat - 10/31";
            this.radioTrickOrTreat.UseVisualStyleBackColor = true;
            this.radioTrickOrTreat.CheckedChanged += new System.EventHandler(this.radioTrickOrTreat_CheckedChanged);
            // 
            // radioSummerEvent
            // 
            this.radioSummerEvent.AutoSize = true;
            this.radioSummerEvent.Location = new System.Drawing.Point(6, 106);
            this.radioSummerEvent.Name = "radioSummerEvent";
            this.radioSummerEvent.Size = new System.Drawing.Size(189, 16);
            this.radioSummerEvent.TabIndex = 6;
            this.radioSummerEvent.Text = "星河への願い ～ 天龍祭 - 07/24";
            this.radioSummerEvent.UseVisualStyleBackColor = true;
            this.radioSummerEvent.CheckedChanged += new System.EventHandler(this.radioSummerEvent_CheckedChanged);
            // 
            // radioBujinsai
            // 
            this.radioBujinsai.AutoSize = true;
            this.radioBujinsai.Location = new System.Drawing.Point(6, 84);
            this.radioBujinsai.Name = "radioBujinsai";
            this.radioBujinsai.Size = new System.Drawing.Size(103, 16);
            this.radioBujinsai.TabIndex = 3;
            this.radioBujinsai.Text = "武皇祭 - 05/05";
            this.radioBujinsai.UseVisualStyleBackColor = true;
            this.radioBujinsai.CheckedChanged += new System.EventHandler(this.radioBujinsai_CheckedChanged);
            // 
            // radioHinamatsuri
            // 
            this.radioHinamatsuri.AutoSize = true;
            this.radioHinamatsuri.Location = new System.Drawing.Point(6, 62);
            this.radioHinamatsuri.Name = "radioHinamatsuri";
            this.radioHinamatsuri.Size = new System.Drawing.Size(113, 16);
            this.radioHinamatsuri.TabIndex = 2;
            this.radioHinamatsuri.Text = "ひなまつり - 03/03";
            this.radioHinamatsuri.UseVisualStyleBackColor = true;
            this.radioHinamatsuri.CheckedChanged += new System.EventHandler(this.radioHinamatsuri_CheckedChanged);
            // 
            // radioNewYear
            // 
            this.radioNewYear.AutoSize = true;
            this.radioNewYear.Location = new System.Drawing.Point(6, 40);
            this.radioNewYear.Name = "radioNewYear";
            this.radioNewYear.Size = new System.Drawing.Size(171, 16);
            this.radioNewYear.TabIndex = 0;
            this.radioNewYear.Text = "あけましておめでとう！ - 01/01";
            this.radioNewYear.UseVisualStyleBackColor = true;
            this.radioNewYear.CheckedChanged += new System.EventHandler(this.radioNewYear_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxNewPlayerReset);
            this.groupBox1.Controls.Add(this.textBoxPrivateEditOn);
            this.groupBox1.Controls.Add(this.textBoxPartyEditOn);
            this.groupBox1.Controls.Add(this.numericUpDownUpdateCnt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonExeStart);
            this.groupBox1.Controls.Add(this.checkBoxUpdate);
            this.groupBox1.Controls.Add(this.checkBoxListStatus);
            this.groupBox1.Controls.Add(this.checkBoxPartyAction);
            this.groupBox1.Controls.Add(this.checkBoxCharacterAction);
            this.groupBox1.Controls.Add(this.checkBoxDataImport);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 289);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登録処理";
            // 
            // checkBoxNewPlayerReset
            // 
            this.checkBoxNewPlayerReset.AutoSize = true;
            this.checkBoxNewPlayerReset.Checked = true;
            this.checkBoxNewPlayerReset.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNewPlayerReset.Enabled = false;
            this.checkBoxNewPlayerReset.Location = new System.Drawing.Point(181, 107);
            this.checkBoxNewPlayerReset.Name = "checkBoxNewPlayerReset";
            this.checkBoxNewPlayerReset.Size = new System.Drawing.Size(80, 16);
            this.checkBoxNewPlayerReset.TabIndex = 13;
            this.checkBoxNewPlayerReset.Text = "新規リセット";
            this.checkBoxNewPlayerReset.UseVisualStyleBackColor = true;
            // 
            // textBoxPrivateEditOn
            // 
            this.textBoxPrivateEditOn.Enabled = false;
            this.textBoxPrivateEditOn.Location = new System.Drawing.Point(188, 39);
            this.textBoxPrivateEditOn.Name = "textBoxPrivateEditOn";
            this.textBoxPrivateEditOn.Size = new System.Drawing.Size(74, 19);
            this.textBoxPrivateEditOn.TabIndex = 12;
            // 
            // textBoxPartyEditOn
            // 
            this.textBoxPartyEditOn.Enabled = false;
            this.textBoxPartyEditOn.Location = new System.Drawing.Point(188, 61);
            this.textBoxPartyEditOn.Name = "textBoxPartyEditOn";
            this.textBoxPartyEditOn.Size = new System.Drawing.Size(74, 19);
            this.textBoxPartyEditOn.TabIndex = 11;
            // 
            // numericUpDownUpdateCnt
            // 
            this.numericUpDownUpdateCnt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.numericUpDownUpdateCnt.Location = new System.Drawing.Point(83, 235);
            this.numericUpDownUpdateCnt.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownUpdateCnt.Name = "numericUpDownUpdateCnt";
            this.numericUpDownUpdateCnt.Size = new System.Drawing.Size(178, 19);
            this.numericUpDownUpdateCnt.TabIndex = 10;
            this.numericUpDownUpdateCnt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownUpdateCnt.ValueChanged += new System.EventHandler(this.numericUpDownUpdateCnt_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "更新回数:";
            // 
            // buttonExeStart
            // 
            this.buttonExeStart.Location = new System.Drawing.Point(25, 260);
            this.buttonExeStart.Name = "buttonExeStart";
            this.buttonExeStart.Size = new System.Drawing.Size(200, 23);
            this.buttonExeStart.TabIndex = 6;
            this.buttonExeStart.Text = "登録処理実行(&A)";
            this.buttonExeStart.UseVisualStyleBackColor = true;
            this.buttonExeStart.Click += new System.EventHandler(this.buttonExeStart_Click);
            // 
            // checkBoxUpdate
            // 
            this.checkBoxUpdate.AutoSize = true;
            this.checkBoxUpdate.Location = new System.Drawing.Point(6, 107);
            this.checkBoxUpdate.Name = "checkBoxUpdate";
            this.checkBoxUpdate.Size = new System.Drawing.Size(84, 16);
            this.checkBoxUpdate.TabIndex = 5;
            this.checkBoxUpdate.Text = "5. 最終処理";
            this.checkBoxUpdate.UseVisualStyleBackColor = true;
            this.checkBoxUpdate.CheckedChanged += new System.EventHandler(this.checkBoxUpdate_CheckedChanged);
            // 
            // checkBoxListStatus
            // 
            this.checkBoxListStatus.AutoSize = true;
            this.checkBoxListStatus.Location = new System.Drawing.Point(6, 85);
            this.checkBoxListStatus.Name = "checkBoxListStatus";
            this.checkBoxListStatus.Size = new System.Drawing.Size(159, 16);
            this.checkBoxListStatus.TabIndex = 4;
            this.checkBoxListStatus.Text = "4. ステータス表示/リスト作成";
            this.checkBoxListStatus.UseVisualStyleBackColor = true;
            // 
            // checkBoxPartyAction
            // 
            this.checkBoxPartyAction.AutoSize = true;
            this.checkBoxPartyAction.Location = new System.Drawing.Point(6, 63);
            this.checkBoxPartyAction.Name = "checkBoxPartyAction";
            this.checkBoxPartyAction.Size = new System.Drawing.Size(96, 16);
            this.checkBoxPartyAction.TabIndex = 3;
            this.checkBoxPartyAction.Text = "3. パーティ行動";
            this.checkBoxPartyAction.UseVisualStyleBackColor = true;
            this.checkBoxPartyAction.CheckedChanged += new System.EventHandler(this.checkBoxPartyAction_CheckedChanged);
            // 
            // checkBoxCharacterAction
            // 
            this.checkBoxCharacterAction.AutoSize = true;
            this.checkBoxCharacterAction.Location = new System.Drawing.Point(6, 41);
            this.checkBoxCharacterAction.Name = "checkBoxCharacterAction";
            this.checkBoxCharacterAction.Size = new System.Drawing.Size(148, 16);
            this.checkBoxCharacterAction.TabIndex = 2;
            this.checkBoxCharacterAction.Text = "2: 新規＆キャラクター行動";
            this.checkBoxCharacterAction.UseVisualStyleBackColor = true;
            this.checkBoxCharacterAction.CheckedChanged += new System.EventHandler(this.checkBoxCharacterAction_CheckedChanged);
            // 
            // checkBoxDataImport
            // 
            this.checkBoxDataImport.AutoSize = true;
            this.checkBoxDataImport.Location = new System.Drawing.Point(6, 19);
            this.checkBoxDataImport.Name = "checkBoxDataImport";
            this.checkBoxDataImport.Size = new System.Drawing.Size(110, 16);
            this.checkBoxDataImport.TabIndex = 0;
            this.checkBoxDataImport.Text = "1: データインポート";
            this.checkBoxDataImport.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxInstance);
            this.groupBox3.Controls.Add(this.buttonDetouch);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 307);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(494, 47);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "オプション";
            // 
            // buttonDetouch
            // 
            this.buttonDetouch.Location = new System.Drawing.Point(380, 15);
            this.buttonDetouch.Name = "buttonDetouch";
            this.buttonDetouch.Size = new System.Drawing.Size(108, 23);
            this.buttonDetouch.TabIndex = 15;
            this.buttonDetouch.Text = "DBデタッチ";
            this.buttonDetouch.UseVisualStyleBackColor = true;
            this.buttonDetouch.Click += new System.EventHandler(this.buttonDetouch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "SQLサーバーインスタンス:";
            // 
            // buttonStatusView
            // 
            this.buttonStatusView.Location = new System.Drawing.Point(512, 12);
            this.buttonStatusView.Name = "buttonStatusView";
            this.buttonStatusView.Size = new System.Drawing.Size(75, 23);
            this.buttonStatusView.TabIndex = 15;
            this.buttonStatusView.Text = "能力値表示";
            this.buttonStatusView.UseVisualStyleBackColor = true;
            this.buttonStatusView.Click += new System.EventHandler(this.buttonStatusView_Click);
            // 
            // textBoxTabView
            // 
            this.textBoxTabView.Location = new System.Drawing.Point(512, 41);
            this.textBoxTabView.Multiline = true;
            this.textBoxTabView.Name = "textBoxTabView";
            this.textBoxTabView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTabView.Size = new System.Drawing.Size(247, 260);
            this.textBoxTabView.TabIndex = 16;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButtonPro);
            this.groupBox4.Controls.Add(this.radioButtonDev);
            this.groupBox4.Location = new System.Drawing.Point(512, 309);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(247, 45);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "トランザクション";
            // 
            // textBoxInstance
            // 
            this.textBoxInstance.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::GrandBlazeStudio.Properties.Settings.Default, "SQLServerInstance", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxInstance.Location = new System.Drawing.Point(133, 17);
            this.textBoxInstance.Name = "textBoxInstance";
            this.textBoxInstance.Size = new System.Drawing.Size(226, 19);
            this.textBoxInstance.TabIndex = 1;
            this.textBoxInstance.Text = global::GrandBlazeStudio.Properties.Settings.Default.SQLServerInstance;
            this.textBoxInstance.TextChanged += new System.EventHandler(this.textBoxInstance_TextChanged);
            // 
            // radioButtonDev
            // 
            this.radioButtonDev.AutoSize = true;
            this.radioButtonDev.Location = new System.Drawing.Point(6, 18);
            this.radioButtonDev.Name = "radioButtonDev";
            this.radioButtonDev.Size = new System.Drawing.Size(47, 16);
            this.radioButtonDev.TabIndex = 0;
            this.radioButtonDev.TabStop = true;
            this.radioButtonDev.Text = "開発";
            this.radioButtonDev.UseVisualStyleBackColor = true;
            this.radioButtonDev.CheckedChanged += new System.EventHandler(this.radioButtonDev_CheckedChanged);
            // 
            // radioButtonPro
            // 
            this.radioButtonPro.AutoSize = true;
            this.radioButtonPro.Location = new System.Drawing.Point(59, 18);
            this.radioButtonPro.Name = "radioButtonPro";
            this.radioButtonPro.Size = new System.Drawing.Size(47, 16);
            this.radioButtonPro.TabIndex = 1;
            this.radioButtonPro.TabStop = true;
            this.radioButtonPro.Text = "本番";
            this.radioButtonPro.UseVisualStyleBackColor = true;
            this.radioButtonPro.CheckedChanged += new System.EventHandler(this.radioButtonPro_CheckedChanged);
            // 
            // MainFromDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 366);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.textBoxTabView);
            this.Controls.Add(this.buttonStatusView);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFromDoc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "メイン";
            this.Load += new System.EventHandler(this.MainFromDoc_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpdateCnt)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioNoEvent;
        private System.Windows.Forms.RadioButton radioSaintCristmas;
        private System.Windows.Forms.RadioButton radioTrickOrTreat;
        private System.Windows.Forms.RadioButton radioSummerEvent;
        private System.Windows.Forms.RadioButton radioBujinsai;
        private System.Windows.Forms.RadioButton radioHinamatsuri;
        private System.Windows.Forms.RadioButton radioNewYear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownUpdateCnt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonExeStart;
        private System.Windows.Forms.CheckBox checkBoxUpdate;
        private System.Windows.Forms.CheckBox checkBoxListStatus;
        private System.Windows.Forms.CheckBox checkBoxPartyAction;
        private System.Windows.Forms.CheckBox checkBoxCharacterAction;
        private System.Windows.Forms.CheckBox checkBoxDataImport;
        private System.Windows.Forms.TextBox textBoxPartyEditOn;
        private System.Windows.Forms.TextBox textBoxPrivateEditOn;
        private System.Windows.Forms.CheckBox checkBoxNewPlayerReset;
        private System.Windows.Forms.RadioButton radioWedding;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxInstance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDetouch;
        private System.Windows.Forms.Button buttonStatusView;
        private System.Windows.Forms.TextBox textBoxTabView;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButtonPro;
        private System.Windows.Forms.RadioButton radioButtonDev;
    }
}