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
    public partial class frmDepEdit : Form
    {
        public frmDepEdit()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDepList fm = new frmDepList();
            this.Hide();
            fm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDepEdit_Load(object sender, EventArgs e)
        {
            oType();
            string Ind_no = frmDepList.dgvInd;

            comboBox1.Text = getData(Ind_no).Rows[0][1].ToString();
            textBox1.Text = getData(Ind_no).Rows[0][2].ToString();
        }

        public void oType()
        {
            string sql = "Select office_type_id,office_type_name from tbl_office_type ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "office_type_id";
            comboBox1.DisplayMember = "office_type_name";
        }

        public DataTable getData(string xyz)
        {
            string sql = "Select a.office_type_id,b.office_type_name,a.dept_name from tbl_department a, tbl_office_type b where a.dept_id = '"+xyz+"' and a.office_type_id = b.office_type_id";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool saveFlag = _UpdateDept();

            if (saveFlag == true)
            {
                MessageBox.Show("Department Updated");
                this.Close();
            }
            else
            {
                MessageBox.Show("There's some error to save the department");
            }
        }

        public bool _UpdateDept()
        {
            bool retVal = false;
            //string sql = string.Empty;
            
            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            string sql = "UPDATE tbl_department SET   " +
                    "`dept_name` = '" + textBox1.Text + "', `office_type_id` = '" + comboBox1.SelectedValue.ToString() + "' " + " WHERE `dept_id` = '" + frmDepList.dgvInd + "' ";
                    
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
