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
    public partial class frmOfficeList : Form
    {
        public static string dgvInd;
        public static string dgvInd1;

        public frmOfficeList()
        {
            InitializeComponent();
        }

        public DataTable officeList()
        {
            string sql = "SELECT a.office_code as 'Code', a.office_name as 'Name', b.office_type_name 'Type', c.premises_name as 'Premises' FROM tbl_office a,`tbl_office_type` b,`tbl_premises` c WHERE a.office_type_id = b.office_type_id AND a.premises_id = c.premises_id";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmOfficeAdd fm = new frmOfficeAdd();
            this.Hide();
            fm.ShowDialog();
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
                
                bool saveFlag = _DeleteOffice();

                if (saveFlag == true)
                {
                    MessageBox.Show("Selected offie deleted");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error to delete the seleced office");
                }
            }
        }
        public DataTable pName(string abc)
        {
            string sql = "Select premises_id from tbl_premises where premises_name = '"+abc+"'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public bool _DeleteOffice()
        {
            bool retVal = false;
            string sql = string.Empty;
            string index1 = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string index2 = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            sql = "delete from tbl_office " +
                  "WHERE `office_name` = '" + index1 + "' and premises_id = '"+pName(index2).Rows[0][0].ToString()+"'";
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
            }
            return retVal;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmOfficeEdit fm = new frmOfficeEdit();
            dgvInd = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dgvInd1 = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            this.Close();
            this.Hide();
            fm.ShowDialog();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
             dgvInd = dataGridView1.CurrentRow.Cells[1].Value.ToString();
             dgvInd1 = dataGridView1.CurrentRow.Cells[3].Value.ToString();
             frmOfficeEdit fm = new frmOfficeEdit();
             this.Close();
             this.Hide();
             fm.ShowDialog();
        }

        private void frmOfficeList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = officeList();
        }
    }
}
