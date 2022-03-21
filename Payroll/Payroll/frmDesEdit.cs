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
    public partial class frmDesEdit : Form
    {
        public frmDesEdit()
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

        private void frmDesEdit_Load(object sender, EventArgs e)
        {
            ofType();
            DepList();
            statCheckFD();
            statCheckTD();

            string Ind_no = frmDesList.dgvInd;

            comboBox1.Text = getData(Ind_no).Rows[0][0].ToString();
            comboBox2.Text = getData(Ind_no).Rows[0][1].ToString();
            textBox1.Text = getData(Ind_no).Rows[0][2].ToString();

            if (getData(Ind_no).Rows[0][3].ToString() == "Y")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
            if (getData(Ind_no).Rows[0][4].ToString() == "Y")
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }
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

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            checkBox1.Focus();
        }

        private void checkBox1_Leave(object sender, EventArgs e)
        {
            checkBox2.Focus();
        }

        private void checkBox2_Leave(object sender, EventArgs e)
        {
            button1.Focus();
        }

        public DataTable getData(string xyz)
        {
            string sql = "Select a.office_type_name,b.dept_name,c.desgn_name,c.isFieldLevel,c.isTopLevel from tbl_department b, tbl_office_type a,tbl_designation c where c.desgn_id = '" + xyz + "' and a.office_type_id = b.office_type_id and b.dept_id = c.dept_id";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool saveFlag = _UpdateDes();

            if (saveFlag == true)
            {
                MessageBox.Show("Designation Updated");
                this.Close();
            }
            else
            {
                MessageBox.Show("There's some error to save the designation");
            }
        }

        public bool _UpdateDes()
        {
            bool retVal = false;
            //string sql = string.Empty;
            string Tstat = "";

            if (checkBox2.Checked == true)
            {
                Tstat = "Y";
            }
            else
            {
                Tstat = "N";
            }
            string Fstat = "";

            if (checkBox1.Checked == true)
            {
                Fstat = "Y";
            }
            else
            {
                Fstat = "N";
            }

            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            string sql = "UPDATE tbl_designation SET   " +
                    "`desgn_name` = '" + textBox1.Text + "', `isTopLevel` = '" + Tstat + "',`isFieldLevel` = '" + Fstat + "',`dept_id` = '" + comboBox2.SelectedValue.ToString() + "',`modified_on` = '" + date + "',`modified_by` = '" +frmLogin.loggedUser + "' " + " WHERE `desgn_id` = '" + frmDesList.dgvInd + "' ";

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
