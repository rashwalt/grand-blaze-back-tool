using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace DataEditForm.Area
{
    public partial class AreaMarkPanel : CommonFormLibrary.TreeBasePanel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AreaMarkPanel()
        {
            InitializeComponent();

            // データバインド
            this.comboBoxAreaNation.DataSource = LibArea.Entity.mt_nation_list;
            this.comboBoxAreaNation.ValueMember = LibArea.Entity.mt_nation_list.nation_idColumn.ColumnName;
            this.comboBoxAreaNation.DisplayMember = LibArea.Entity.mt_nation_list.nation_nameColumn.ColumnName;

            // コピー、削除は禁止
            this.toolStripMenuItemCopy.Visible = false;
            this.toolStripMenuItemDelete.Visible = false;
        }

        private int SelectedNo
        {
            get { return int.Parse(this.textBoxNo.Text); }
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Panel_Load(object sender, EventArgs e)
        {
            LoadData();
            if (this.treeViewList.SelectedNode != null && this.treeViewList.SelectedNode.Level > 0)
            {
                PrivateView(((AreaMarkEntity.mt_area_listRow)this.treeViewList.SelectedNode.Tag).area_id);
            }
            else
            {
                PrivateView(0);
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        public override void Cancel()
        {
            AreaMarkEntity entity = LibArea.Entity;
            entity.RejectChanges();
        }

        /// <summary>
        /// データ表示
        /// </summary>
        private void LoadData()
        {
            AreaMarkEntity entity = LibArea.Entity;

            this.treeViewList.Nodes.Clear();

            // 親を全部追加
            List<TreeNode> treeNodeParent = new List<TreeNode>();
            foreach (AreaMarkEntity.nation_listRow NationRow in entity.mt_nation_list)
            {
                List<TreeNode> treeNodeChild = new List<TreeNode>();

                foreach (AreaMarkEntity.mt_area_listRow AreaRow in NationRow.Getmt_area_listRows())
                {
                    List<TreeNode> TreeNodeGrandChild = new List<TreeNode>();

                    TreeNode ParentNode1 = new TreeNode(AreaRow.area_name);
                    ParentNode1.Tag = AreaRow;
                    treeNodeChild.Add(ParentNode1);
                }
                TreeNode ParentNode2 = new TreeNode(NationRow.nation_name, treeNodeChild.ToArray());
                ParentNode2.Tag = NationRow;

                treeNodeParent.Add(ParentNode2);
            }

            TreeNode[] treeNodeRoot = treeNodeParent.ToArray();

            this.treeViewList.Nodes.AddRange(treeNodeRoot);
            this.treeViewList.ExpandAll();

            this.treeViewList.SelectedNode = null;

            if (this.textBoxNo.Text.Length > 0 && this.textBoxName.Text.Length > 0)
            {
                SelectNode(SelectedNo, this.treeViewList.Nodes);
            }

            if (this.treeViewList.Nodes.Count > 0 && this.treeViewList.SelectedNode == null)
            {
                this.treeViewList.TopNode = this.treeViewList.Nodes[0];
            }
        }

        /// <summary>
        /// 指定されたコードを持ったノードを選択
        /// </summary>
        /// <param name="code">コード</param>
        private void SelectNode(int no, TreeNodeCollection nodeCollection)
        {
            foreach (TreeNode parentNode in nodeCollection)
            {
                foreach (TreeNode node in parentNode.Nodes)
                {
                    AreaMarkEntity.mt_area_listRow row = (AreaMarkEntity.mt_area_listRow)node.Tag;
                    if (no == row.area_id)
                    {
                        this.treeViewList.SelectedNode = node;
                    }
                }
            }
        }

        /// <summary>
        /// 詳細表示
        /// </summary>
        /// <param name="AreaID">種族ID</param>
        private void PrivateView(int AreaID)
        {
            if (AreaID < 0) { return; }

            AreaMarkEntity entity = LibArea.Entity;

            if (AreaID == 0)
            {
                AreaID = LibInteger.GetNewUnderNum(entity.mt_area_list, entity.mt_area_list.area_idColumn.ColumnName);
            }

            // 基本情報の表示
            AreaMarkEntity.mt_area_listRow baseRow = entity.mt_area_list.FindByarea_id(AreaID);

            this.textBoxNo.Text = AreaID.ToString();

            if (baseRow == null)
            {
                // 行追加はエンティティへの反映時に

                return;
            }

            this.textBoxName.Text = baseRow.area_name;

            this.comboBoxAreaNation.SelectedValue = baseRow.nation_id;
        }

        /// <summary>
        /// 選択イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void treeViewList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if ((e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse) && this.treeViewList.SelectedNode != null && this.treeViewList.SelectedNode.Level > 0)
            {
                UpdateEntity();
                PrivateView(((AreaMarkEntity.mt_area_listRow)this.treeViewList.SelectedNode.Tag).area_id);
                LoadData();
            }
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            PrivateView(0);
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        public override bool CheckModify()
        {
            UpdateEntity();
            AreaMarkEntity entity = LibArea.Entity;

            return entity.GetChanges() != null;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            UpdateEntity();

            LibDBLocal dba = new LibDBLocal(Status.DataBaseAccessTarget.Master);

            try
            {
                dba.BeginTransaction();
                dba.Update(LibArea.Entity.mt_area_list);

                dba.Commit();
            }
            catch
            {
                dba.Rollback();
            }
            finally
            {
                dba.Dispose();
            }

            LibArea.LoadArea();
        }

        /// <summary>
        /// 変更内容をエンティティに反映
        /// </summary>
        private void UpdateEntity()
        {
            AreaMarkEntity entity = LibArea.Entity;

            if (this.textBoxName.Text.Length == 0) { return; }

            AreaMarkEntity.mt_area_listRow row = entity.mt_area_list.FindByarea_id(int.Parse(this.textBoxNo.Text));
            bool isAdd = false;

            if (row == null)
            {
                // 新規追加
                row = entity.mt_area_list.Newmt_area_listRow();
                isAdd = true;
                row.area_id = int.Parse(this.textBoxNo.Text);
            }

            if (isAdd || row.area_name != this.textBoxName.Text) { row.area_name = this.textBoxName.Text; }

            if (isAdd || row.nation_id != (int)this.comboBoxAreaNation.SelectedValue) { row.nation_id = (int)this.comboBoxAreaNation.SelectedValue; }

            if (isAdd)
            {
                entity.mt_area_list.Addmt_area_listRow(row);
            }
        }
    }
}
