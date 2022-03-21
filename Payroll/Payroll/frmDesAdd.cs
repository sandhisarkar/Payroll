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
    public partial class frmDesAdd : Form
    {
        public frmDesAdd()
        {
            InitializeComponent();
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            comboBox2.Focus();
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

        public void DepList()
        {
            string sql = "Select b.dept_id, b.dept_name from tbl_office_type a,tbl_department b where a.office_type_id = b.office_type_id";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.ValueMember = "dept_id";
            comboBox2.DisplayMember = "dept_name";
        }

        private void frmDesAdd_Load(object sender, EventArgs e)
        {
            ofType();
            DepList();
            statCheckFD();
            statCheckTD();
        }

        public string statCheckFD()
        {
            string Fstat = "";
            

            if (checkBox1.Checked == true)
            {
                Fstat = "Y";
            }
            else
            {
                Fstat = "N";
            }
            return Fstat;

        }

        public string statCheckTD()
        {
            
            string Tstat = "";

            if (checkBox2.Checked == true)
            {
                Tstat = "Y";
            }
            else
            {
                Tstat = "N";
            }
            return Tstat;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            checkBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDesList fm = new frmDesList();
            this.Hide();
            fm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool saveFlag = _InsertDesignation();

            if (saveFlag == true)
            {
                MessageBox.Show("New designation entered");
                this.Close();
            }
            else
            {
                MessageBox.Show("There's some error to add new designation");
            }
        }

        public bool _InsertDesignation()
        {
            bool retVal = false;
            string sql = string.Empty;
            string co = "01";
            string isdel = "N";

            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            sql = "insert into tbl_designation(co_id,desgn_name,isTopLevel,isFieldLevel,dept_id,is_deleted,created_on,created_by)" +
                "values('" + co + "', " +
                    "'" + textBox1.Text + "', " +
                    "'" + statCheckTD() + "', " +
                    "'" + statCheckFD() + "', " +
                    "'" + comboBox2.SelectedValue.ToString() + "', " +
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

        private void checkBox1_Leave(object sender, EventArgs e)
        {
            checkBox2.Focus();
        }

        private void checkBox2_Leave(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
