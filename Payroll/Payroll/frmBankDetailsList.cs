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
    public partial class frmBankDetailsList : Form
    {
        public static string dgvInd;

        public frmBankDetailsList()
        {
            InitializeComponent();
        }

        private void frmBankDetailsList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bankDetailsList();
        }

        public DataTable bankDetailsList()
        {
            string sql = "Select a.bank_details_id as 'Bank Details ID', b.bank_name as 'Bank Name',a.details_key as 'Bank Details Key',a.details_value as 'Bank Deatils Value' from tbl_bank_details a,tbl_bank b where a.bank_id = b.bank_id and a.bank_id = '"+frmBankList.dgvInd+"'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please selec a row");
            }
            else
            {

                bool saveFlag = _DeleteBankDetails();

                if (saveFlag == true)
                {
                    MessageBox.Show("Selected bank details deleted");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error to delete the seleced bank details");
                }
            }
        }

        public bool _DeleteBankDetails()
        {
            bool retVal = false;
            string sql = string.Empty;
            string index1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            sql = "delete from tbl_bank_details " +
                  "WHERE `bank_details_id` = '" + index1 + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
            }
            return retVal;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmBankDetailsAdd fm = new frmBankDetailsAdd();
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Close();
            this.Hide();
            fm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmBankDetailsEdit fm = new frmBankDetailsEdit();
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Close();
            this.Hide();
            fm.ShowDialog();
        }
    }
}
