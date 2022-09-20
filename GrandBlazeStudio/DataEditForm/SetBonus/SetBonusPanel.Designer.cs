namespace DataEditForm.SetBonus
{
    partial class SetBonusPanel
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

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkedListBoxEquip = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.effectSettingPanel = new CommonFormLibrary.CommonPanel.EffectSettingPanel();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.effectSettingPanel);
            this.panelMain.Controls.Add(this.textBoxName);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.textBoxNo);
            this.panelMain.Controls.Add(this.label1);
            // 
            // labelTitle
            // 
            this.labelTitle.Text = "セット効果";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkedListBoxEquip);
            this.groupBox1.Location = new System.Drawing.Point(424, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(99, 97);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "対応部位";
            // 
            // checkedListBoxEquip
            // 
            this.checkedListBoxEquip.CheckOnClick = true;
            this.checkedListBoxEquip.FormattingEnabled = true;
            this.checkedListBoxEquip.Items.AddRange(new object[] {
            "メイン",
            "サブ",
            "頭部",
            "胴体",
            "アクセサリ"});
            this.checkedListBoxEquip.Location = new System.Drawing.Point(6, 18);
            this.checkedListBoxEquip.Name = "checkedListBoxEquip";
            this.checkedListBoxEquip.Size = new System.Drawing.Size(85, 74);
            this.checkedListBoxEquip.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 43;
            this.label3.Text = "エフェクト:";
            // 
            // effectSettingPanel
            // 
            this.effectSettingPanel.Location = new System.Drawing.Point(6, 61);
            this.effectSettingPanel.Name = "effectSettingPanel";
            this.effectSettingPanel.Size = new System.Drawing.Size(412, 273);
            this.effectSettingPanel.TabIndex = 42;
            // 
            // textBoxName
            // 
            this.textBoxName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBoxName.Location = new System.Drawing.Point(92, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(326, 19);
            this.textBoxName.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "名称:";
            // 
            // textBoxNo
            // 
            this.textBoxNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxNo.Location = new System.Drawing.Point(6, 24);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.ReadOnly = true;
            this.textBoxNo.Size = new System.Drawing.Size(80, 19);
            this.textBoxNo.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "No.:";
            // 
            // SetBonusPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SetBonusPanel";
            this.Controls.SetChildIndex(this.panelList, 0);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox checkedListBoxEquip;
        private System.Windows.Forms.Label label3;
        private CommonFormLibrary.CommonPanel.EffectSettingPanel effectSettingPanel;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNo;
        private System.Windows.Forms.Label label1;
    }
}
