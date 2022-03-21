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
    public partial class frmOfficeAdd : Form
    {
        public frmOfficeAdd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmOfficeList fm = new frmOfficeList();
            this.Hide();
            fm.ShowDialog();
        }

        private void frmOfficeAdd_Load(object sender, EventArgs e)
        {
            oType();
            pName();
            pstate();
            pdis();
            reOffice();

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
        public void pName()
        {
            string sql = "Select premises_id,premises_name from tbl_premises ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.ValueMember = "premises_id";
            comboBox2.DisplayMember = "premises_name";
        }
        public void pstate()
        {
            string sql = "Select state_id,state_name from tbl_state ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox3.DataSource = dt;
            comboBox3.ValueMember = "state_id";
            comboBox3.DisplayMember = "state_name";

        }
        public void pdis()
        {
            string sql = "Select dist_id,dist_name from tbl_district where state_id =  '" + comboBox3.SelectedValue.ToString() + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox4.DataSource = dt;
            comboBox4.ValueMember = "dist_id";
            comboBox4.DisplayMember = "dist_name";
        }
        public void reOffice()
        {
            string sql = "Select reporting_office_id,office_name from tbl_office ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox5.DataSource = dt;
            comboBox5.ValueMember = "reporting_office_id";
            comboBox5.DisplayMember = "office_name";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_Leave(object sender, EventArgs e)
        {
            string sql = "Select dist_id,dist_name from tbl_district where state_id =  '" + comboBox3.SelectedValue.ToString() + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox4.DataSource = dt;
            comboBox4.ValueMember = "dist_id";
            comboBox4.DisplayMember = "dist_name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool saveFlag = _InsertOffice();

            if (saveFlag == true)
            {
                MessageBox.Show("New office entered");
                this.Close();
            }
            else
            {
                MessageBox.Show("There's some error to add new office");
            }
        }
        public bool _InsertOffice()
        {
            bool retVal = false;
            string sql = string.Empty;
            string co = "01";
            string isdel = "N";
            
            string sdate = dateTimePicker1.Text;
            string fsdate = sdate.Substring(6, 4).ToString() + "-" + sdate.Substring(3, 2).ToString() + "-" + sdate.Substring(0, 2).ToString();
            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            sql = "insert into tbl_office(co_id,office_type_id,office_code,office_name,premises_id,reporting_office_id,start_date,is_deleted,created_on,created_by)" +
                "values('" + co + "', " +
                    "'" + comboBox1.SelectedValue.ToString() + "', " +
                    "'" + textBox2.Text + "', " +
                    "'" + textBox1.Text + "', " +
                    "'" + comboBox2.SelectedValue.ToString() + "', " +
                    "'" + comboBox5.SelectedValue.ToString() + "','" + fsdate + "','" + isdel + "','" + date + "','" + frmLogin.loggedUser + "')";

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

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            button1.Focus();
        }
    }
}
