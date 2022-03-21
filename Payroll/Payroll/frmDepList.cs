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
    public partial class frmDepList : Form
    {
        public static string dgvInd;

        public frmDepList()
        {
            InitializeComponent();
        }

        private void frmDepList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = depList();
        }

        public DataTable depList()
        {
            string sql = "Select a.dept_id as 'Depertment ID', a.dept_name as 'Name', b.office_type_name as 'Office' from tbl_department a, tbl_office_type b where a.office_type_id = b.office_type_id ";
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

        private void button1_Click(object sender, EventArgs e)
        {
            frmDepAdd fm = new frmDepAdd();
            this.Hide();
            fm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDepEdit fm = new frmDepEdit();
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Close();
            this.Hide();
            fm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please selec a row");
            }
            else
            {

                bool saveFlag = _DeleteDept();

                if (saveFlag == true)
                {
                    MessageBox.Show("Selected department deleted");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error to delete the seleced department");
                }
            }
        }
        public bool _DeleteDept()
        {
            bool retVal = false;
            string sql = string.Empty;
            string index1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            sql = "delete from tbl_department " +
                  "WHERE `dept_id` = '" + index1 + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
            }
            return retVal;

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            frmDepEdit fm = new frmDepEdit();
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Close();
            this.Hide();
            fm.ShowDialog();
        }
    }
}
