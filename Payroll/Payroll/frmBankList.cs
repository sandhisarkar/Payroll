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
    public partial class frmBankList : Form
    {
        public static string dgvInd;

        public frmBankList()
        {
            InitializeComponent();
        }

        private void frmBankList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bankList();
        }

        public DataTable bankList()
        {
            string sql = "Select bank_id as 'Bank ID', bank_name as 'Bank Name',bank_address as 'Bank Address' from tbl_bank ";
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

                bool saveFlag = _DeleteBank();

                if (saveFlag == true)
                {
                    MessageBox.Show("Selected bank deleted");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error to delete the seleced bank");
                }
            }
        }

        public bool _DeleteBank()
        {
            bool retVal = false;
            string sql = string.Empty;
            string index1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            sql = "delete from tbl_bank " +
                  "WHERE `bank_id` = '" + index1 + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
            }
            return retVal;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmBankEdit fm = new frmBankEdit();
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Close();
            this.Hide();
            fm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmBankAdd fm = new frmBankAdd();
            this.Hide();
            fm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmBankDetailsList fm = new frmBankDetailsList();
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Close();
            this.Hide();
            fm.ShowDialog();
        }
    }
}
