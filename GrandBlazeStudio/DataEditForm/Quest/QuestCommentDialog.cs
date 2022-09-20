using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonFormLibrary;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;

namespace DataEditForm.Quest
{
    public partial class QuestCommentDialog : BaseDialog
    {
        public QuestCommentDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="InQuestStep">進捗度</param>
        public QuestCommentDialog(int InQuestStep)
        {
            InitializeComponent();

            _QuestStep = InQuestStep;
        }

        private int _QuestStep = 0;

        /// <summary>
        /// クエストID
        /// </summary>
        public int QuestID = 0;

        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestCommentDialog_Load(object sender, EventArgs e)
        {
            if (_QuestStep >= 0)
            {
                QuestDataEntity entity = LibQuest.Entity;

                QuestDataEntity.mt_quest_stepRow row = entity.mt_quest_step.FindByquest_idquest_step(QuestID, _QuestStep);

                if (row == null) { return; }

                this.numericUpDownStep.Value = row.quest_step;

                this.textBoxComment.Text = row.comment.Replace("<br />", "\r\n");
            }
            else
            {
                this.numericUpDownStep.Value = 0;
                this.textBoxComment.Text = "";

                DataView QuestStepView = new DataView(LibQuest.Entity.mt_quest_step);
                QuestStepView.RowFilter = LibQuest.Entity.mt_quest_step.quest_idColumn.ColumnName + "=" + QuestID;
                QuestStepView.Sort = LibQuest.Entity.mt_quest_step.quest_stepColumn.ColumnName + " desc";

                if (QuestStepView.Count > 0)
                {
                    this.numericUpDownStep.Value = (int)QuestStepView[0][LibQuest.Entity.mt_quest_step.quest_stepColumn.ColumnName] + 1;
                }
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            // データ保存
            QuestDataEntity entity = LibQuest.Entity;

            bool isNew = false;
            QuestDataEntity.mt_quest_stepRow row = entity.mt_quest_step.FindByquest_idquest_step(QuestID, (int)this.numericUpDownStep.Value);

            if (row == null)
            {
                row = entity.mt_quest_step.Newmt_quest_stepRow();
                isNew = true;
            }

            if (isNew) { row.quest_id = QuestID; }

            if (isNew || row.quest_step != (int)this.numericUpDownStep.Value) { row.quest_step = (int)this.numericUpDownStep.Value; }

            string InComment = this.textBoxComment.Text.Replace("\r\n", "<br />");
            if (isNew || row.comment != InComment) { row.comment = InComment; }

            if (isNew) { entity.mt_quest_step.Addmt_quest_stepRow(row); }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
