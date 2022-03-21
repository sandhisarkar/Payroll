using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;
using NovaNet.Utils;
using NovaNet.wfe;

namespace Payroll
{
    public partial class frmBankDetailsAdd : Form
    {
        public frmBankDetailsAdd()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool saveFlag = _InsertBankList();

            if (saveFlag == true)
            {
                MessageBox.Show("New bank details entered");
                this.Close();
            }
            else
            {
                MessageBox.Show("There's some error to add new bank details");
            }
        }

        public bool _InsertBankList()
        {
            bool retVal = false;
            string sql = string.Empty;
            string co = "01";
            string isdel = "N";

            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            sql = "insert into tbl_bank_details(co_id,bank_id,details_key,details_value,is_deleted,created_on,created_by)" +
                "values('" + co + "', " +
                    "'" + frmBankList.dgvInd + "', " +
                    "'" + textBox1.Text + "', " +
                    "'" + textBox2.Text + "', " +
                    "'" + isdel + "', " +
                    "'" + date + "', " +
                    "'" + frmLogin.loggedUser + "')";

            System.Diagnostics.Debug.Print(sql);
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;

            }
            else
            {
                retVal = false;
            }


            return retVal;

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            button1.Focus();
        }
    }
}
