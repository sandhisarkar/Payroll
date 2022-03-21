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
    public partial class frmDepAdd : Form
    {
        public frmDepAdd()
        {
            InitializeComponent();
        }

        public void ofType()
        {
            string sql = "Select office_type_id, office_type_name from tbl_office_type ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "office_type_id";
            comboBox1.DisplayMember = "office_type_name";
        }

        private void frmDepAdd_Load(object sender, EventArgs e)
        {
            ofType();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            button1.Focus();
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

        private void button1_Click(object sender, EventArgs e)
        {
            bool saveFlag = _InsertDepartment();

            if (saveFlag == true)
            {
                MessageBox.Show("New department entered");
                this.Close();
            }
            else
            {
                MessageBox.Show("There's some error to add new department");
            }
        }

        public bool _InsertDepartment()
        {
            bool retVal = false;
            string sql = string.Empty;
            string co = "01";
            string isdel = "N";
            
            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            sql = "insert into tbl_department(co_id,dept_name,office_type_id,is_deleted,created_on,created_by)" +
                "values('" + co + "', " +
                    "'" + textBox1.Text + "', " +
                    "'" + comboBox1.SelectedValue.ToString() + "', " +
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
    }
}
