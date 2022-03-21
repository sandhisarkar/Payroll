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
    public partial class frmOfficeEdit : Form
    {
        public frmOfficeEdit()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmOfficeList fm = new frmOfficeList();
            this.Hide();
            fm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOfficeEdit_Load(object sender, EventArgs e)
        {
               string index = frmOfficeList.dgvInd;
               comboBox1.Text = getData(index).Rows[0][0].ToString();

               textBox1.Text = getData(index).Rows[0][1].ToString();
               textBox2.Text = getData(index).Rows[0][2].ToString();
               comboBox2.SelectedText = getData(index).Rows[0][3].ToString();
               textBox3.Text = getData(index).Rows[0][4].ToString();
               comboBox3.Text = getData(index).Rows[0][5].ToString();
               comboBox4.Text = getData(index).Rows[0][6].ToString();

               comboBox5.Text = getData(index).Rows[0][1].ToString();

               dateTimePicker1.Text = getData(index).Rows[0][7].ToString();

        }
        public DataTable getData(string ind)
        {
            string sql = "Select e.office_type_name,a.office_name,a.reporting_office_id,b.premises_name,b.premises_address,c.state_name,d.dist_name,a.start_date from tbl_office a, tbl_premises b, tbl_state c, tbl_district d,tbl_office_type e where a.office_name = '" + ind + "' and a.premises_id = b.premises_id and b.state_id = c.state_id and b.district_id = d.dist_id and c.state_id = d.state_id and a.office_type_id = e.office_type_id";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            return dt;
        }
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            bool saveFlag = _UpdateOffice();

            if (saveFlag == true)
            {
                MessageBox.Show("Office Updated");
                this.Close();
            }
            else
            {
                MessageBox.Show("There's some error to save the office");
            }
        }
        public bool _UpdateOffice()
        {
            bool retVal = false;
            //string sql = string.Empty;
            string co = "01";
            string id = "HO";
            string isdel = "N";
            //string ptype = prtype();
            string sdate = dateTimePicker1.Text;
            string fsdate = sdate.Substring(6, 4).ToString() + "-" + sdate.Substring(3, 2).ToString() + "-" + sdate.Substring(0, 2).ToString();
            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            string sql = "UPDATE tbl_office SET   " +
                    "`office_type_id` = '"+id+"', " +
                    "`office_code` = '" + textBox2.Text + "', " +
                    "`office_name` = '" + textBox1.Text + "', " +
                    "`premises_id` = '" + comboBox2.SelectedValue.ToString() + "'," +
                    "`reporting_office_id` = '" + comboBox5.SelectedValue.ToString() + "'," +
                    "`start_date` = '" + fsdate + "' WHERE `office_name` = '" + frmOfficeList.dgvInd + "'";
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            premisesstate();
            comboBox2.Focus();
        }
        public void premisesstate()
        {
            string sql = "Select premises_id,premises_name from tbl_premises";
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

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            pstate();
            comboBox4.Focus();
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
        private void comboBox4_Leave(object sender, EventArgs e)
        {
            pdis();
            comboBox5.Focus();
        }
        public void reOf()
        {
            string sql = "Select office_name,office_code from tbl_office";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox5.DataSource = dt;
            comboBox5.ValueMember = "office_code";
            comboBox5.DisplayMember = "office_name";
        }

        private void comboBox5_Leave(object sender, EventArgs e)
        {
            reOf();
            dateTimePicker1.Focus();
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            button1.Focus();
        }

    }
}
