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
    public partial class frmBankEdit : Form
    {
        public frmBankEdit()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBankEdit_Load(object sender, EventArgs e)
        {
            string Ind_no = frmBankList.dgvInd;

            textBox1.Text = getData(Ind_no).Rows[0][0].ToString();
            textBox2.Text = getData(Ind_no).Rows[0][1].ToString();
        }

        public DataTable getData(string xyz)
        {
            string sql = "Select bank_name,bank_address from tbl_bank where bank_id = '" + xyz + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool saveFlag = _UpdateBank();

            if (saveFlag == true)
            {
                MessageBox.Show("Bank Updated");
                this.Close();
            }
            else
            {
                MessageBox.Show("There's some error to save the bank");
            }
        }

        public bool _UpdateBank()
        {
            bool retVal = false;
            //string sql = string.Empty;

            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            string sql = "UPDATE tbl_bank SET   " +
                    "`bank_name` = '" + textBox1.Text + "', `bank_address` = '" + textBox2.Text + "',`modified_on` = '" + date + "',`modified_by` = '" + frmLogin.loggedUser + "'" + " WHERE `bank_id` = '" + frmBankList.dgvInd + "' ";

            // sql = sql + ""; 

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
            return true;
        }
    }
}
