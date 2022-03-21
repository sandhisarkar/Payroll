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
    public partial class frmEmployeeAdd : Form
    {
        public DataTable emp_id()
        {
            string sql = "select max(emp_code) from tbl_employee";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public string father_hus_flag()
        {
            string flag = "";
            if (radioButton1.Checked == true)
            {
                flag = "F";
            }
            if (radioButton2.Checked == true)
            {
                flag = "H";
            }
            if (radioButton3.Checked == true)
            {
                flag = "O";
            }
            return flag;
        }

        public string SexFlag()
        {
            string sexFlag = "";
            if (comboBox1.Text == "Male")
            {
                sexFlag = "M";
            }
            if (comboBox1.Text == "Female")
            {
                sexFlag = "F";
            }
            return sexFlag;
        }

        public string MaritialStat()
        {
            string marstatFlag = "";
            if (comboBox2.Text == "Single")
            {
                marstatFlag = "S";
            }
            if (comboBox2.Text == "Married")
            {
                marstatFlag = "M";
            }
            return marstatFlag;
        }

        public void popRel()
        {
            string sql = "select religion_id,religion_name from tbl_religion";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox3.DataSource = dt;
            comboBox3.ValueMember = "religion_id";
            comboBox3.DisplayMember = "religion_name";
        }

        public void popCast()
        {
            string sql = "select cast_id, cast_name from tbl_cast";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox4.DataSource = dt;
            comboBox4.ValueMember = "cast_id";
            comboBox4.DisplayMember = "cast_name";
        }

        public void eduQualification()
        {
            string sql = "select edu_id, edu_name from tbl_edu_qualification";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox6.DataSource = dt;
            comboBox6.ValueMember = "edu_id";
            comboBox6.DisplayMember = "edu_name";
        }

        public void permState()
        {
            string sql = "select state_id, state_name from tbl_state";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox7.DataSource = dt;
            comboBox7.ValueMember = "state_id";
            comboBox7.DisplayMember = "state_name";
        }

        public void presState()
        {
            string sql = "select state_id, state_name from tbl_state";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox9.DataSource = dt;
            comboBox9.ValueMember = "state_id";
            comboBox9.DisplayMember = "state_name";
        }

        public void permDis()
        {
            string sql = "select a.dist_id, a.dist_name from tbl_district a, tbl_state b where a.state_id = b.state_id";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox8.DataSource = dt;
            comboBox8.ValueMember = "dist_id";
            comboBox8.DisplayMember = "dist_name";
        }

        public void presDis()
        {
            string sql = "select a.dist_id, a.dist_name from tbl_district a, tbl_state b where a.state_id = b.state_id";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox10.DataSource = dt;
            comboBox10.ValueMember = "dist_id";
            comboBox10.DisplayMember = "dist_name";
        }
        public string ispresentpermanent()
        {
            string isprespermFlag = "";
            if (checkBox1.Checked == true)
            {
                isprespermFlag = "Y";
                textBox11.Enabled = false;
                comboBox9.Enabled = false;
                comboBox10.Enabled = false;
                textBox12.Enabled = false;
                textBox11.Text = "";
                textBox12.Text = "";
                comboBox9.Text = "0";
                comboBox10.Text = "0";
            }
            else
            {
                isprespermFlag = "N";
                textBox11.Enabled = true;
                comboBox9.Enabled = true;
                comboBox10.Enabled = true;
                textBox12.Enabled = true;
            }
            return isprespermFlag;
        }

        public void emp_type()
        {
            string sql = "select employment_type_id,employment_type_name from tbl_employment_type";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox11.DataSource = dt;
            comboBox11.ValueMember = "employment_type_id";
            comboBox11.DisplayMember = "employment_type_name";
        }

        public void joinOffTypeName()
        {
            string sql = "select office_type_id, office_type_name from tbl_office_type";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox12.DataSource = dt;
            comboBox12.ValueMember = "office_type_id";
            comboBox12.DisplayMember = "office_type_name";
        }

        public void joinOffName()
        {
            string sql = "select office_id, office_name from tbl_office  where office_type_id = '" + comboBox12.SelectedValue + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox13.DataSource = dt;
            comboBox13.ValueMember = "office_id";
            comboBox13.DisplayMember = "office_name";
        }
        public void joinDep()
        {
            string sql = "select dept_id, dept_name from tbl_department where office_type_id = '" + comboBox12.SelectedValue + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox14.DataSource = dt;
            comboBox14.ValueMember = "dept_id";
            comboBox14.DisplayMember = "dept_name";
        }

        public void joinDes()
        {
            string sql = "select desgn_id, desgn_name from tbl_designation where dept_id ='" + comboBox14.SelectedValue + "' ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox15.DataSource = dt;
            comboBox15.ValueMember = "desgn_id";
            comboBox15.DisplayMember = "desgn_name";
        }

        public void joinSite()
        {
            string sql = "select state_id, state_name from tbl_state";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox16.DataSource = dt;
            comboBox16.ValueMember = "state_id";
            comboBox16.DisplayMember = "state_name";
        }

        public frmEmployeeAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmployeeAdd_Load(object sender, EventArgs e)
        {
            int prevEmpID = Convert.ToInt32(emp_id().Rows[0][0].ToString());
            int currentEmpID = (prevEmpID + 1);
            textBox1.Text = Convert.ToString(currentEmpID);
            father_hus_flag();
            SexFlag();
            MaritialStat();
            popRel();
            popCast();

            eduQualification();

            permState();
            permDis();
            ispresentpermanent();

            presState();
            presDis();

            emp_type();

            joinOffTypeName();
            dateTimePicker4.CustomFormat = " ";
            dateTimePicker5.Text = DateTime.Now.Year.ToString() + "-12" + "-31";


        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("You cannot leave employee name field blank.");
                textBox2.Focus();
                //textBox2.Select();

            }
            if (father_hus_flag() == "")
            {
                MessageBox.Show("You have to select one of these radiobutton (Father/Husband/Gurdian)");
                // radioButton1.Focus();

            }

            if (dateTimePicker1.Text == DateTime.Now.ToString())
            {
                MessageBox.Show("Please select proper DOB");
                dateTimePicker1.Focus();
                dateTimePicker1.Select();

            }

            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select employee's sex");
                comboBox1.Focus();
                comboBox1.Select();
            }

            if (comboBox2.Text == "")
            {
                MessageBox.Show("Please select employee's Marital Status");
                comboBox2.Focus();
                comboBox2.Select();
            }

            if (comboBox3.Text == "")
            {
                MessageBox.Show("Please select employee's Religion");
                comboBox3.Focus();
                comboBox3.Select();
            }

            if (comboBox4.Text == "")
            {
                MessageBox.Show("Please select employee's Cast");
                comboBox4.Focus();
                comboBox4.Select();
            }

            string idenMark;
            if (textBox4.Text == "")
            {
                idenMark = "N/A";
            }
            else
            {
                idenMark = textBox4.Text;
            }

            string bloodGroup;
            if (comboBox5.Text == "")
            {
                MessageBox.Show("Please select employee's blood group");
                comboBox5.Focus();
            }
            else
            {
                bloodGroup = comboBox5.Text;
            }

            string eduQualif;
            if (comboBox6.Text == "")
            {
                MessageBox.Show("Please select employee's educational qualification");
                comboBox6.Focus();
            }
            else
            {
                eduQualif = comboBox6.Text;
            }

            string otherQualif;
            if (textBox5.Text == "")
            {
                otherQualif = "N/A";
            }
            else
            {
                otherQualif = textBox5.Text;
            }

            string permanentAdd;
            if (textBox7.Text == "")
            {
                MessageBox.Show("Please mention employee's permanent address");
                textBox7.Focus();
            }
            else
            {
                permanentAdd = textBox7.Text;
            }

            string permanentState;
            if (comboBox7.Text == "")
            {
                MessageBox.Show("Please select employee's Permanent State");
                comboBox7.Focus();
            }
            else
            {
                permanentState = comboBox7.Text;
            }

            string permanentDis;
            if (comboBox8.Text == "")
            {
                MessageBox.Show("Please select employee's Permanent District");
                comboBox8.Focus();
            }
            else
            {
                permanentDis = comboBox8.Text;
            }

            if (textBox8.Text == "")
            {
                MessageBox.Show("Please mention employee's Permanent Pincode");
                textBox8.Focus();
            }

            if (textBox9.Text == "")
            {
                MessageBox.Show("Please mention employee's Permanent Contact Number");
                textBox9.Focus();
            }

            if (textBox10.Text == "")
            {
                MessageBox.Show("Please mention employee's Email id");
                textBox10.Focus();
            }

            string checkFlag = "";
            if (checkBox1.Checked == true)
            {
                checkFlag = "Y";
            }
            else
            {
                checkFlag = "N";
            }

            string presADD;
            if (textBox11.Text == "")
            {
                presADD = "";
            }
            else
            {
                presADD = textBox11.Text;
            }

            string presState;
            if (comboBox9.Text == "Select")
            {
                comboBox9.SelectedValue = "0";
            }
            else
            {
                presState = comboBox9.Text;
            }

            string presDis;
            if (comboBox10.Text == "Select")
            {
                comboBox10.SelectedValue = "0";
            }
            else
            {
                presDis = comboBox10.Text;
            }

            string presPIN;
            if (textBox12.Text == "")
            {
                presPIN = textBox12.Text;
            }
            else
            {
                presPIN = textBox12.Text;
            }


        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox11.Enabled = false;
                comboBox9.Enabled = false;
                comboBox10.Enabled = false;
                textBox12.Enabled = false;
                textBox11.Text = "";
                comboBox9.Text = "Select";
                comboBox10.Text = "Select";
                textBox12.Text = "";
            }
            else
            {
                textBox11.Enabled = true;
                comboBox9.Enabled = true;
                comboBox10.Enabled = true;
                textBox12.Enabled = true;
            }
        }

        private void comboBox11_Leave(object sender, EventArgs e)
        {
            if (comboBox11.SelectedText == "Contrctual-Payroll")
            {
                checkBox2.Enabled = true;
                textBox13.Enabled = false;
                dateTimePicker2.Enabled = true;
                dateTimePicker2.CustomFormat = " ";
                dateTimePicker3.Enabled = false;
                dateTimePicker3.CustomFormat = " ";
            }
            else
            {
                checkBox2.Enabled = false;
                textBox13.Enabled = false;
                dateTimePicker2.Enabled = true;
                dateTimePicker2.CustomFormat = " ";
                dateTimePicker3.Enabled = false;
                dateTimePicker3.CustomFormat = " ";
            }

        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox11.SelectedText == "Contrctual-Payroll")
            {
                checkBox2.Enabled = true;
                textBox13.Enabled = false;
                dateTimePicker2.Enabled = true;
                dateTimePicker2.CustomFormat = " ";
                dateTimePicker3.Enabled = false;
                dateTimePicker3.CustomFormat = " ";
            }
            else
            {
                checkBox2.Enabled = false;
                textBox13.Enabled = false;
                dateTimePicker2.Enabled = true;
                dateTimePicker2.CustomFormat = " ";
                dateTimePicker3.Enabled = false;
                dateTimePicker3.CustomFormat = " ";
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.CustomFormat = "dd-MM-yyyy";
        }

        private void comboBox15_Leave(object sender, EventArgs e)
        {
            dateTimePicker4.CustomFormat = " ";
        }

        private void dateTimePicker4_Leave(object sender, EventArgs e)
        {
            dateTimePicker4.CustomFormat = "dd-MM-yyyy";
            joinSite();
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            joinOffTypeName();


        }

        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
           // joinOffName();

        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
           // joinDep();

        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            //joinDes();
            //dateTimePicker4.CustomFormat = " ";
        }

        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {
            joinSite();
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker4.CustomFormat = "dd-MM-yyyy";
        }

        private void comboBox12_Leave(object sender, EventArgs e)
        {
            joinOffName();
        }

        private void comboBox13_Leave(object sender, EventArgs e)
        {
            joinDep();
        }

        private void comboBox14_Leave(object sender, EventArgs e)
        {
            joinDes();
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker5.CustomFormat = "dd-MM-yyyy";
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox19.Text = "2000";
            }
            else
            {
                textBox19.Text = "0.00";
            }
        }
    }
}
