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
    public partial class frmBankAdviceList : Form
    {
        public static string dgvInd;

        public frmBankAdviceList()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBankAdviceList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bankAdviceList();
        }
        public DataTable bankAdviceList()
        {
            string sql = "Select bank_advice_id as 'Bank Advice ID',bank_advice_name as 'Bank Advice Name',bank_advice_description as 'Bank Advice Description' from tbl_bank_advice ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please selec a row");
            }
            else
            {

                bool saveFlag = _DeleteBankAdvice();

                if (saveFlag == true)
                {
                    MessageBox.Show("Selected bank advice deleted");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error to delete the seleced bank advice");
                }
            }
        }
        public bool _DeleteBankAdvice()
        {
            bool retVal = false;
            string sql = string.Empty;
            string index1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            sql = "delete from tbl_bank_advice " +
                  "WHERE `bank_advice_id` = '" + index1 + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
            }
            return retVal;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmBankAdviceAdd fm = new frmBankAdviceAdd();
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Close();
            this.Hide();
            fm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmBankAdviceEdit fm = new frmBankAdviceEdit();
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Close();
            this.Hide();
            fm.ShowDialog();
        }

    }
}
