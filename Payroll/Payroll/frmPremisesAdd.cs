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
    public partial class frmPremisesAdd : Form
    {
        public frmPremisesAdd()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            prtype();
        }

        public string prtype()
        {
            string ptype = "";
            if (comboBox1.SelectedItem == "Owned")
            {
                ptype = "O";

            }
            if (comboBox1.SelectedItem == "Rented")
            {
                ptype = "R";

            }
            return ptype;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmPremisesList fm = new frmPremisesList();
            this.Hide();
            fm.ShowDialog();
        }

        private void frmPremisesAdd_Load(object sender, EventArgs e)
        {
            prtype();

            pstate();
            pdis();
            dateTimePicker1.Text = "";
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            prtype();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                textBox2.Focus();
            }
            else
            {
                MessageBox.Show("you cannot leave this blank");
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                textBox3.Focus();
            }
            else
            {
                MessageBox.Show("you cannot leave this blank");
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                comboBox2.Focus();
            }
            else
            {
                MessageBox.Show("you cannot leave this blank");
            }
        }

        public void pstate()
        {
            string sql = "Select state_id,state_name from tbl_state ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.ValueMember = "state_id";
            comboBox2.DisplayMember = "state_name";

        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "Select state_id,state_name from tbl_state ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.ValueMember = "state_id";
            comboBox2.DisplayMember = "state_name";
        }
        public void pdis()
        {
            string sql = "Select dist_id,dist_name from tbl_district where state_id =  '"+comboBox2.SelectedValue.ToString()+"'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox3.DataSource = dt;
            comboBox3.ValueMember = "dist_id";
            comboBox3.DisplayMember = "dist_name";
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "Select dist_id,dist_name from tbl_district where state_id =  '" + comboBox2.SelectedValue.ToString() + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            comboBox3.DataSource = dt;
            comboBox3.ValueMember = "dist_id";
            comboBox3.DisplayMember = "dist_name";
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            button1.Focus();
            string abc = DateTime.Now.Month.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool saveFlag = _InsertPremises();

            if (saveFlag == true)
            {
                MessageBox.Show("New premises entered");
                this.Close();
            }
            else
            {
                MessageBox.Show("There's some error to add new premises");
            }

        }
       /* public bool savePremesis()
        {
            bool retVal = false;

            _InsertPremises();

            return retVal;

        }*/

        public bool _InsertPremises()
        {
            bool retVal = false;
            string sql = string.Empty;
            string co = "01";
            string isdel = "N";
            string ptype = prtype();
            string sdate = dateTimePicker1.Text;
            string fsdate = sdate.Substring(6, 4).ToString() + "-" + sdate.Substring(3, 2).ToString() + "-" + sdate.Substring(0, 2).ToString();
            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            sql = "insert into tbl_premises(co_id,premises_name,premises_address,district_id,state_id,premises_location,premises_type,start_dt,is_deleted,created_on,created_by)" +
                "values('" + co + "', " +
                    "'" + textBox1.Text + "', " +
                    "'" + textBox2.Text + "', " +
                    "'" + comboBox3.SelectedValue.ToString() + "', " +
                    "'" + comboBox2.SelectedValue.ToString() + "', " +
                    "'" + textBox3.Text + "','" + ptype + "','" + fsdate + "','" + isdel + "','" + date + "','" + frmLogin.loggedUser + "')";

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
