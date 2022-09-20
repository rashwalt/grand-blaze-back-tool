namespace GrandBlazeStudio
{
    partial class MainForm
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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.コマンドCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.キャラクターデータクリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ウィンドウWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.スクリプトSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.エクスポートEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.toolStripMenuItemTool,
            this.コマンドCToolStripMenuItem,
            this.ウィンドウWToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.MdiWindowListItem = this.ウィンドウWToolStripMenuItem;
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(702, 26);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了XToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 終了XToolStripMenuItem
            // 
            this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            this.終了XToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.終了XToolStripMenuItem.Text = "終了(&X)";
            this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
            // 
            // toolStripMenuItemTool
            // 
            this.toolStripMenuItemTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDataBase});
            this.toolStripMenuItemTool.Name = "toolStripMenuItemTool";
            this.toolStripMenuItemTool.Size = new System.Drawing.Size(74, 22);
            this.toolStripMenuItemTool.Text = "ツール(&T)";
            // 
            // toolStripMenuItemDataBase
            // 
            this.toolStripMenuItemDataBase.Name = "toolStripMenuItemDataBase";
            this.toolStripMenuItemDataBase.Size = new System.Drawing.Size(167, 22);
            this.toolStripMenuItemDataBase.Text = "データベース(&D)";
            this.toolStripMenuItemDataBase.Click += new System.EventHandler(this.toolStripMenuItemDataVase_Click);
            // 
            // コマンドCToolStripMenuItem
            // 
            this.コマンドCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.キャラクターデータクリアToolStripMenuItem,
            this.スクリプトSToolStripMenuItem});
            this.コマンドCToolStripMenuItem.Name = "コマンドCToolStripMenuItem";
            this.コマンドCToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.コマンドCToolStripMenuItem.Text = "コマンド(&C)";
            // 
            // キャラクターデータクリアToolStripMenuItem
            // 
            this.キャラクターデータクリアToolStripMenuItem.Name = "キャラクターデータクリアToolStripMenuItem";
            this.キャラクターデータクリアToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.キャラクターデータクリアToolStripMenuItem.Text = "キャラクターデータクリア";
            this.キャラクターデータクリアToolStripMenuItem.Click += new System.EventHandler(this.キャラクターデータクリアToolStripMenuItem_Click);
            // 
            // ウィンドウWToolStripMenuItem
            // 
            this.ウィンドウWToolStripMenuItem.Name = "ウィンドウWToolStripMenuItem";
            this.ウィンドウWToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.ウィンドウWToolStripMenuItem.Text = "ウィンドウ(&W)";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "mdb";
            this.openFileDialog.Filter = "Access DB ファイル(*.mdb)|*.mdb|すべてのファイル(*.*)|*.*";
            this.openFileDialog.Title = "データベースファイルの設定";
            // 
            // スクリプトSToolStripMenuItem
            // 
            this.スクリプトSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.エクスポートEToolStripMenuItem});
            this.スクリプトSToolStripMenuItem.Name = "スクリプトSToolStripMenuItem";
            this.スクリプトSToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.スクリプトSToolStripMenuItem.Text = "スクリプト(&S)";
            // 
            // エクスポートEToolStripMenuItem
            // 
            this.エクスポートEToolStripMenuItem.Name = "エクスポートEToolStripMenuItem";
            this.エクスポートEToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.エクスポートEToolStripMenuItem.Text = "エクスポート(&E)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 537);
            this.Controls.Add(this.menuMain);
            this.IsMdiContainer = true;
            this.Location = new System.Drawing.Point(120, 120);
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainForm";
            this.Text = "Grand Blaze Studio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem ウィンドウWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTool;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDataBase;
        private System.Windows.Forms.ToolStripMenuItem コマンドCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem キャラクターデータクリアToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem スクリプトSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem エクスポートEToolStripMenuItem;

    }
}

