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
    public partial class frmpremisesEdit : Form
    {
        public frmpremisesEdit()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmPremisesList fm = new frmPremisesList();
            this.Hide();
            fm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            prtype();
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

        public void pdis()
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void frmpremisesEdit_Load(object sender, EventArgs e)
        {
            prtype();

            

            string Ind_no = frmPremisesList.dgvInd;

            string prem_type = getData(Ind_no).Rows[0][0].ToString();
            if (prem_type == "O")
            {
                comboBox1.SelectedText = "Owned";
            }
            if (prem_type == "R")
            {
                comboBox1.SelectedText = "Rented";
            }

            textBox1.Text = getData(Ind_no).Rows[0][1].ToString();

            textBox2.Text = getData(Ind_no).Rows[0][2].ToString();

            textBox3.Text = getData(Ind_no).Rows[0][3].ToString();

            string tempDis = getData(Ind_no).Rows[0][5].ToString();

            string tempState = getData(Ind_no).Rows[0][4].ToString();

            comboBox2.Text = getstate(tempState).ToString();

            comboBox3.Text = getDis(tempDis).ToString();

            dateTimePicker1.Text = getData(Ind_no).Rows[0][6].ToString();

        }
        public string getstate(string ind)
        {
            string sql = "Select state_name from tbl_state where state_id = '" + ind + "' ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            return dt.Rows[0][0].ToString();
        }
        public string getDis(string ind)
        {
            string sql = "Select dist_name from tbl_district where dist_id = '" + ind + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

           return dt.Rows[0][0].ToString();
        }

        public DataTable getData(string ind)
        {
            string sql = "Select premises_type,premises_name,premises_address,premises_location,state_id,district_id,start_dt from tbl_premises where premises_id = '"+ind+"' ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool saveFlag = _UpdatePremises();

            if (saveFlag == true)
            {
                MessageBox.Show("Premises Updated");
                this.Close();
            }
            else
            {
                MessageBox.Show("There's some error to save the premises");
            }
        }

        public bool _UpdatePremises()
        {
            bool retVal = false;
            //string sql = string.Empty;
            string co = "01";
            string isdel = "N";
            string ptype = prtype();
            string sdate = dateTimePicker1.Text;
            string fsdate = sdate.Substring(6, 4).ToString() + "-" + sdate.Substring(3, 2).ToString() + "-" + sdate.Substring(0, 2).ToString();
            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            string sql = "UPDATE tbl_premises SET   " +
                    "`premises_type` = '" + prtype() + "', " +
                    "`premises_name` = '" + textBox1.Text + "', " +
                    "`premises_address` = '" + textBox2.Text + "', " +
                    "`premises_location` = '" + textBox3.Text + "',"+
                    "`state_id` = '" + comboBox2.SelectedValue.ToString() + "'," +
                    "`district_id` = '" + comboBox3.SelectedValue.ToString() + "'," +
                    "`start_dt` = '" + fsdate + "' WHERE `premises_id` = '" + frmPremisesList.dgvInd + "' ";
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

        private void textBox3_Leave(object sender, EventArgs e)
        {
            pstate();
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            pdis();
        }
    }
}
