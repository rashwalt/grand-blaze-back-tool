using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.DataFormat.Entity;
using CommonLibrary.DataAccess;

namespace GrandBlazeStudio.MainFormItem
{
    public partial class EventJoinForm : CommonFormLibrary.BaseDialog
    {
        public EventJoinForm()
        {
            InitializeComponent();
        }

        private LibContinue con = new LibContinue();

        private void EventJoinForm_Load(object sender, EventArgs e)
        {
            this.dataGridView.AutoGenerateColumns = false;

            this.dataGridView.DataSource = con.Entity.ts_continue_official_event;
            this.columnEntryNo.DataPropertyName = "entry_no";
            this.columnBride.DataPropertyName = "bride";
            this.columnGroom.DataPropertyName = "groom";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            foreach (ContinueDataEntity.ts_continue_official_eventRow row in con.Entity.ts_continue_official_event)
            {
                if (row.IsgroomNull())
                {
                    row.groom = false;
                }
                if (row.IsbrideNull())
                {
                    row.bride = false;
                }
            }

            LibDBLocal dba = new LibDBLocal();

            try
            {
                dba.BeginTransaction();
                dba.Update(con.Entity.ts_continue_official_event);
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

            this.Close();
        }
    }
}
