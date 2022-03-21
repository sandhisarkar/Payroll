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
    public partial class frmDesList : Form
    {
        public static string dgvInd;

        public frmDesList()
        {
            InitializeComponent();
        }

        private void frmDesList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = desList();
        }

        public DataTable desList()
        {
            string sql = "Select a.desgn_id as 'Designation ID',b.office_type_name as 'Office',c.dept_name as 'Department',a.desgn_name as 'Designation'  from tbl_designation a,tbl_department c, tbl_office_type b where b.office_type_id = c.office_type_id and a.dept_id = c.dept_id";
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
            frmDesAdd fm = new frmDesAdd();
            this.Hide();
            fm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDesEdit fm = new frmDesEdit();
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Close();
            this.Hide();
            fm.ShowDialog();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            frmDesEdit fm = new frmDesEdit();
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

                bool saveFlag = _DeleteDes();

                if (saveFlag == true)
                {
                    MessageBox.Show("Selected designation deleted");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error to delete the seleced designation");
                }
            }
        }
        public bool _DeleteDes()
        {
            bool retVal = false;
            string sql = string.Empty;
            string index1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            sql = "delete from tbl_designation " +
                  "WHERE `desgn_id` = '" + index1 + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
            }
            return retVal;

        }
    }
}
