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
    public partial class frmEmployeeEdit : Form
    {
        public frmEmployeeEdit()
        {
            InitializeComponent();
        }

        public DataTable getDataPerDetails(string xyz)
        {
            string sql = "Select emp_code,emp_name,father_husband_flag,father_husband_name,emp_dob,emp_sex,emp_marital,religion_id,caste_id,emp_id_mark,emp_bloodgrp,edu_id,edu_other,previous_experience_inMonths,emp_permanent_address,permanent_state_id,permanent_district_id,emp_permanent_PIN,emp_contact_no,emp_email_id,is_present_same,emp_present_address,present_state_id,present_district_id,emp_present_PIN from tbl_employee where emp_code = '" + xyz + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }
        public void allEmpType()
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
        
        public DataTable getDataEmployeeEmployment(string xyz)
        {
            string sql = "Select b.employment_type_name,a.isRenewable,a.empmnt_period_InMonth,date_format(a.active_from, '%Y-%m-%d'),date_format(a.active_upto, '%Y-%m-%d') from tbl_employee_employment a,tbl_employment_type b where a.emp_id = '" + xyz + "' and a.employment_type_id = b.employment_type_id";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
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


        public DataTable getRel(string id)
        {
            string sql = "select religion_name from tbl_religion where religion_id = '"+id+"'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getCast(string id)
        {
            string sql = "select cast_name from tbl_cast where cast_id = '" + id + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable geteduQuali(string id)
        {
            string sql = "select edu_name from tbl_edu_qualification where edu_id = '" + id + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getpermState(string id)
        {
            string sql = "select state_name from tbl_state where state_id = '" + id + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getpresState(string id)
        {
            string sql = "select state_name from tbl_state where state_id = '" + id + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getpermDis(string id)
        {
            string sql = "select a.dist_name from tbl_district a,tbl_state b where a.dist_id = '" + id + "' and a.state_id = b.state_id";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        

        public DataTable getpresDis(string id)
        {
            string sql = "select a.dist_name from tbl_district a,tbl_state b where a.dist_id = '" + id + "' and a.state_id = b.state_id";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }
        public void checkList()
        {
            if (checkBox1.Checked == true)
            {
                textBox11.Enabled = false;
                comboBox9.Enabled = false;
                comboBox10.Enabled = false;
                textBox12.Enabled = false;
            }
            if (checkBox1.Checked == false)
            {
                
                textBox11.Enabled = true;
                comboBox9.Enabled = true;
                comboBox10.Enabled = true;
                textBox12.Enabled = true;
            }
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
            string sql = "select office_id, office_name from tbl_office";
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
            string sql = "select dept_id, dept_name from tbl_department";
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
            string sql = "select desgn_id, desgn_name from tbl_designation";
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

        public DataTable getDataJoinFinal1(string xyz)
        {
            string sql = "select b.office_type_name,a.office_name, c.actiive_from from tbl_office a,tbl_office_type b,tbl_employee_posting c where a.office_type_id = b.office_type_id   and c.emp_id = '"+xyz+"'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getDataJoinFinal2(string xyz)
        {
            string sql = "select a.dept_name,b.desgn_name from tbl_department a, tbl_designation b,tbl_employee_posting c where a.dept_id = c.dept_id and a.dept_id = b.dept_id and b.desgn_id =c.desgn_id and c.emp_id = '" + xyz + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getDataJoinFinal3(string xyz)
        {
            string sql = "select premises_id from tbl_employee_posting where emp_id = '" + xyz + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getDataJoinFinal5(string xyz)
        {
            string sql = "select a.state_name from tbl_state a,tbl_employee b where a.state_id = b.permanent_state_id and b.emp_code = '" + xyz + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getDataJoinFinal4(string xyz)
        {
            string sql = "select a.state_name from tbl_state a,tbl_premises b where a.state_id = b.state_id and b.premises_id = '"+xyz+"'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }
        public DataTable getDataJoin(string xyz)
        {
            string sql = "select b.office_type_name,c.office_name,d.dept_name,e.desgn_name,a.actiive_from,f.state_name from tbl_employee_posting a, tbl_office_type b,tbl_office c,tbl_department d,tbl_designation e,tbl_state f,tbl_premises g where a.emp_id = '" + xyz + "' and b.office_type_id = c.office_type_id and a.office_type_id = b.office_type_id and d.office_type_id = c.office_type_id and d.office_type_id = b.office_type_id and d.dept_id = e.dept_id and a.dept_id = d.dept_id and a.desgn_id = e.desgn_id and a.premises_id = g.premises_id and g.state_id = f.state_id";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getDataLeave(string xyz)
        {
            string sql = "select period_end_date,cl_used,ml_used,el_used,mtl_used from tbl_employee_leave where emp_id = '"+xyz+"'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getDataPostingState(string xyz)
        {
            string sql = "select b.state_name from tbl_employee_posting a,tbl_state b where a.posting_id = b.state_id and a.emp_id = '" + xyz + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getDataDocuments(string xyz)
        {
            string sql = "select has_biodata,has_admitCard,has_voterID,has_rationCard,has_residencialCertificate,has_eduCertificate,has_photo,has_anySpecialDoc,special_docDetails from tbl_employee where emp_code = '" + xyz + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable getDataJoinSecurity(string xyz)
        {
            string sql = "select need_joining_security,secuity_amount_atJoining from tbl_employee where emp_code = '" + xyz + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public void gettbl_bank()
        {
            string sql = "select bank_id,bank_name from tbl_bank";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox18.DataSource = dt;
            comboBox18.ValueMember = "bank_id";
            comboBox18.DisplayMember = "bank_name";
        }

        public DataTable getDatatbl_bank(string xyz)
        {
            string sql = "select bank_name from tbl_bank where bank_id = '"+xyz+"'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            return dt;
        }
        public void gettbl_bank_advice()
        {
            string sql = "select bank_advice_id,bank_advice_name from tbl_bank_advice";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            comboBox17.DataSource = dt;
            comboBox17.ValueMember = "bank_advice_id";
            comboBox17.DisplayMember = "bank_advice_name";
        }

        public DataTable getDatatbl_bank_advice(string xyz)
        {
            string sql = "select bank_advice_name from tbl_bank_advice where bank_advice_id = '" + xyz + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            return dt;
        }

        public DataTable getSalary(string xyz)
        {
            string sql = "select isOnlyGrossPay,basic,hra,da,specialAllowance,conv_travlg_allow,lta,petrol_car_reim,medicalAllowance,mobileAllowance,performance_project_allow,advance_taken,gross_pay,allowance_Lieu_pf,fieldAllowance,telephone_reim,acting_allow,site_allow,incentive,addition_arrear_pay,net_salary,staff_welfare,satff_security,house_rent,is_ptax_applicable,ptax,is_pf_applicable,pf_amt,pf_ac_no,is_esi_applicable,esi_amt,is_it_applicable,it_amt,advance_deducted,total_deduct,emp_account_no,bank_advice_id,bank_id,bank_adv_cheque from tbl_employee_salary where emp_id = '" + xyz + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void frmEmployeeEdit_Load(object sender, EventArgs e)
        {
            popRel();
            popCast();
            eduQualification();
            permState();
            permDis();
            checkList();
            presState();
            presDis();
            allEmpType();
            joinOffTypeName();
            joinOffName();
            joinDep();
            joinDes();
            joinSite();
            gettbl_bank();
            gettbl_bank_advice();
            

            string Ind_no = frmEmployeeDetails.dgvInd;

            textBox1.Text = getDataPerDetails(Ind_no).Rows[0][0].ToString();
            textBox2.Text = getDataPerDetails(Ind_no).Rows[0][1].ToString();
            string fh_flag = getDataPerDetails(Ind_no).Rows[0][2].ToString();
            if (fh_flag == "F")
            {
                radioButton1.Checked = true;
            }
            if (fh_flag == "H")
            {
                radioButton2.Checked = true;
            }
            if (fh_flag == "O")
            {
                radioButton3.Checked = true;
            }

            textBox3.Text = getDataPerDetails(Ind_no).Rows[0][3].ToString();
            string dob = getDataPerDetails(Ind_no).Rows[0][4].ToString();
            if ((dob == null) || (dob == " "))
            {
                dateTimePicker1.CustomFormat = " ";

                dateTimePicker1.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dateTimePicker1.Text = dob;
            }

            string sexFlag = getDataPerDetails(Ind_no).Rows[0][5].ToString();
            if (sexFlag == "M")
            {
                comboBox1.Text = "Male";
            }
            if (sexFlag == "F")
            {
                comboBox1.Text = "Female";
            }
            string marstatFlag = getDataPerDetails(Ind_no).Rows[0][6].ToString();
            if (marstatFlag == "S")
            {
                comboBox2.Text = "Single";
            }
            if (marstatFlag == "M")
            {
                comboBox2.Text = "Married";
            }

            string relId = getDataPerDetails(Ind_no).Rows[0][7].ToString();
            comboBox3.Text = getRel(relId).Rows[0][0].ToString();

            string castId = getDataPerDetails(Ind_no).Rows[0][8].ToString();
            comboBox4.Text = getCast(castId).Rows[0][0].ToString();

            textBox4.Text = getDataPerDetails(Ind_no).Rows[0][9].ToString();

            comboBox5.Text = getDataPerDetails(Ind_no).Rows[0][10].ToString();

            string eduq = getDataPerDetails(Ind_no).Rows[0][11].ToString();
            comboBox6.Text = geteduQuali(eduq).Rows[0][0].ToString();

            textBox5.Text = getDataPerDetails(Ind_no).Rows[0][12].ToString();
            textBox6.Text = getDataPerDetails(Ind_no).Rows[0][13].ToString();

            textBox7.Text = getDataPerDetails(Ind_no).Rows[0][14].ToString();

            string permanentState = getDataPerDetails(Ind_no).Rows[0][15].ToString();
            comboBox7.Text = getpermState(permanentState).Rows[0][0].ToString();
            string permanentDis = getDataPerDetails(Ind_no).Rows[0][16].ToString();
            comboBox8.Text = getpermDis(permanentDis).Rows[0][0].ToString();

            textBox8.Text = getDataPerDetails(Ind_no).Rows[0][17].ToString();
            textBox9.Text = getDataPerDetails(Ind_no).Rows[0][18].ToString();
            textBox10.Text = getDataPerDetails(Ind_no).Rows[0][19].ToString();

            string isPres = getDataPerDetails(Ind_no).Rows[0][20].ToString();
            if (isPres == "Y")
            {
                checkBox1.Checked = true;
                textBox11.Enabled = false;
                comboBox9.Enabled = false;
                comboBox10.Enabled = false;
                textBox12.Enabled = false;
            }
            if (isPres == "N")
            {
                checkBox1.Checked = false;
                textBox11.Enabled = true;
                comboBox9.Enabled = true;
                comboBox10.Enabled = true;
                textBox12.Enabled = true;
            }

            textBox11.Text = getDataPerDetails(Ind_no).Rows[0][21].ToString();

            string presSta = getDataPerDetails(Ind_no).Rows[0][22].ToString();
            if (presSta == "0")
            {
                comboBox9.Text = "Select";
            }
            else
            {
                comboBox9.Text = getpresState(presSta).Rows[0][0].ToString();
            }
            
            string presD = getDataPerDetails(Ind_no).Rows[0][23].ToString();
            if (presD == "0")
            {
                comboBox10.Text = "Select";
            }
            else
            {
                comboBox10.Text = getpresDis(presD).Rows[0][0].ToString();
            }
            

            textBox12.Text = getDataPerDetails(Ind_no).Rows[0][24].ToString();

            comboBox11.Text = getDataEmployeeEmployment(Ind_no).Rows[0][0].ToString();
            string isrenew = getDataEmployeeEmployment(Ind_no).Rows[0][1].ToString();
            if (isrenew == "Y")
            {
                checkBox2.Checked = true;
            }
            if (isrenew == "N")
            {
                checkBox2.Checked = false;
            }
            textBox13.Text = getDataEmployeeEmployment(Ind_no).Rows[0][2].ToString();
            string sdate = getDataEmployeeEmployment(Ind_no).Rows[0][3].ToString();
            if ((sdate == null) || (sdate == " "))
            {
                
                dateTimePicker2.CustomFormat = " ";

                dateTimePicker2.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dateTimePicker2.Text = sdate;
            }
            string edate = getDataEmployeeEmployment(Ind_no).Rows[0][4].ToString();
            if ((edate == null) || (edate == ""))
            {
                
                dateTimePicker3.CustomFormat = " ";

                dateTimePicker3.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dateTimePicker3.Text = edate;
            }

            comboBox12.Text = getDataJoinFinal1(Ind_no).Rows[0][0].ToString();
            comboBox13.Text = getDataJoinFinal1(Ind_no).Rows[0][1].ToString();
            comboBox14.Text = getDataJoinFinal2(Ind_no).Rows[0][0].ToString();
            comboBox15.Text = getDataJoinFinal2(Ind_no).Rows[0][1].ToString();
            string ab = getDataJoinFinal1(Ind_no).Rows[0][2].ToString();
            if ((ab == null) || (ab == " "))
            {

                dateTimePicker4.CustomFormat = " ";

                dateTimePicker4.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dateTimePicker4.Text = sdate;
            }

            comboBox16.Text = getDataPostingState(Ind_no).Rows[0][0].ToString();

            string abc = getDataLeave(Ind_no).Rows[0][0].ToString();
            if ((abc == null) || (abc == " "))
            {

                dateTimePicker5.CustomFormat = " ";

                dateTimePicker5.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dateTimePicker5.Text = sdate;
            }

            textBox14.Text = getDataLeave(Ind_no).Rows[0][1].ToString();
            textBox15.Text = getDataLeave(Ind_no).Rows[0][2].ToString();
            textBox16.Text = getDataLeave(Ind_no).Rows[0][3].ToString();
            textBox17.Text = getDataLeave(Ind_no).Rows[0][4].ToString();

            string a = getDataDocuments(Ind_no).Rows[0][0].ToString();
            if (a == "Y")
            {
                checkBox4.Checked = true;
            }
            if (a == "N")
            {
                checkBox4.Checked = false;
            }
            string b = getDataDocuments(Ind_no).Rows[0][1].ToString();
            if (b == "Y")
            {
                checkBox5.Checked = true;
            }
            if (b == "N")
            {
                checkBox5.Checked = false;
            }
            string c = getDataDocuments(Ind_no).Rows[0][2].ToString();
            if (c == "Y")
            {
                checkBox6.Checked = true;
            }
            if (c == "N")
            {
                checkBox6.Checked = false;
            }
            string d = getDataDocuments(Ind_no).Rows[0][3].ToString();
            if (d == "Y")
            {
                checkBox7.Checked = true;
            }
            if (d == "N")
            {
                checkBox7.Checked = false;
            }
            string ea = getDataDocuments(Ind_no).Rows[0][4].ToString();
            if (ea == "Y")
            {
                checkBox8.Checked = true;
            }
            if (ea == "N")
            {
                checkBox8.Checked = false;
            }
            string f = getDataDocuments(Ind_no).Rows[0][5].ToString();
            if (f == "Y")
            {
                checkBox9.Checked = true;
            }
            if (f == "N")
            {
                checkBox9.Checked = false;
            }
            string g = getDataDocuments(Ind_no).Rows[0][6].ToString();
            if (g == "Y")
            {
                checkBox10.Checked = true;
            }
            if (g == "N")
            {
                checkBox10.Checked = false;
            }
            string h = getDataDocuments(Ind_no).Rows[0][7].ToString();
            if (h == "Y")
            {
                checkBox11.Checked = true;
            }
            if (h == "N")
            {
                checkBox11.Checked = false;
            }
            textBox18.Text = getDataDocuments(Ind_no).Rows[0][8].ToString();

            string securityJoin = getDataJoinSecurity(Ind_no).Rows[0][0].ToString();
            if (securityJoin == "Y")
            {
                checkBox3.Checked = true;
                textBox19.Text = "2000";
            }
            if (securityJoin == "N")
            {
                checkBox3.Checked = false;
                textBox19.Text = "0.00";
            }
            textBox20.Text = getDataJoinSecurity(Ind_no).Rows[0][1].ToString();

            string isgross = getSalary(Ind_no).Rows[0][0].ToString();
            if (isgross == "Y")
            {
                checkBox12.Checked = true;
            }
            if (isgross == "N")
            {
                checkBox12.Checked = false;
            }
            textBox21.Text = getSalary(Ind_no).Rows[0][1].ToString();
            textBox22.Text = getSalary(Ind_no).Rows[0][2].ToString();
            textBox23.Text = getSalary(Ind_no).Rows[0][3].ToString();
            textBox24.Text = getSalary(Ind_no).Rows[0][4].ToString();
            textBox25.Text = getSalary(Ind_no).Rows[0][5].ToString();
            textBox26.Text = getSalary(Ind_no).Rows[0][6].ToString();
            textBox27.Text = getSalary(Ind_no).Rows[0][7].ToString();
            textBox28.Text = getSalary(Ind_no).Rows[0][8].ToString();
            textBox29.Text = getSalary(Ind_no).Rows[0][9].ToString();
            textBox30.Text = getSalary(Ind_no).Rows[0][10].ToString();
            textBox31.Text = getSalary(Ind_no).Rows[0][11].ToString();
            textBox32.Text = getSalary(Ind_no).Rows[0][12].ToString();
            textBox33.Text = getSalary(Ind_no).Rows[0][13].ToString();
            textBox34.Text = getSalary(Ind_no).Rows[0][14].ToString();
            textBox35.Text = getSalary(Ind_no).Rows[0][15].ToString();
            textBox36.Text = getSalary(Ind_no).Rows[0][16].ToString();
            textBox37.Text = getSalary(Ind_no).Rows[0][17].ToString();
            textBox38.Text = getSalary(Ind_no).Rows[0][18].ToString();
            textBox39.Text = getSalary(Ind_no).Rows[0][19].ToString();
            textBox40.Text = getSalary(Ind_no).Rows[0][20].ToString();
            textBox42.Text = getSalary(Ind_no).Rows[0][21].ToString();
            textBox43.Text = getSalary(Ind_no).Rows[0][22].ToString();
            textBox44.Text = getSalary(Ind_no).Rows[0][23].ToString();
            string ptaxcheck = getSalary(Ind_no).Rows[0][24].ToString();
            if (ptaxcheck == "Y")
            {
                checkBox13.Checked = true;
            }
            if (ptaxcheck == "N")
            {
                checkBox13.Checked = false;
                textBox45.Enabled = false;
            }
            textBox45.Text = getSalary(Ind_no).Rows[0][25].ToString();
            string pfcheck = getSalary(Ind_no).Rows[0][26].ToString();
            if (pfcheck == "Y")
            {
                checkBox14.Checked = true;
            }
            if (pfcheck == "N")
            {
                checkBox14.Checked = false;
                textBox46.Enabled = false;
            }
            textBox46.Text = getSalary(Ind_no).Rows[0][27].ToString();
            textBox47.Text = getSalary(Ind_no).Rows[0][28].ToString();
            string esicheck = getSalary(Ind_no).Rows[0][29].ToString();
            if (esicheck == "Y")
            {
                checkBox15.Checked = true;
                textBox48.Text = getSalary(Ind_no).Rows[0][30].ToString();
                textBox48.Enabled = true;
            }
            if (esicheck == "N")
            {
                checkBox15.Checked = false;
                textBox48.Enabled = false;
                
            }
            //textBox48.Text = getSalary(Ind_no).Rows[0][30].ToString();
            string incomecheck = getSalary(Ind_no).Rows[0][31].ToString();
            if (incomecheck == "Y")
            {
                checkBox13.Checked = true;
                textBox49.Text = getSalary(Ind_no).Rows[0][32].ToString();
                textBox50.Text = getSalary(Ind_no).Rows[0][33].ToString();
                textBox51.Enabled = false;
                
            }
            if (incomecheck == "N")
            {
                checkBox16.Checked = false;
                textBox49.Enabled = false;
                textBox50.Enabled = false;
                textBox51.Enabled = false;
                
            }

            textBox51.Text = getSalary(Ind_no).Rows[0][34].ToString();
            string bnk_ad_id = getSalary(Ind_no).Rows[0][36].ToString();
            if (bnk_ad_id != "")
            {
                textBox41.Text = getSalary(Ind_no).Rows[0][35].ToString();
                comboBox17.Text = getDatatbl_bank_advice(bnk_ad_id).Rows[0][0].ToString();
                radioButton4.Checked = false;
                radioButton5.Checked = true;
                
            }
            else
            {
                comboBox17.Text = null;
                radioButton4.Checked = true;
                radioButton5.Checked = false;
                textBox41.Text = "";
            }
            string bnk_id = getSalary(Ind_no).Rows[0][37].ToString();
            if (bnk_id != "")
            {
                textBox41.Text = getSalary(Ind_no).Rows[0][35].ToString();
                comboBox18.Text = getDatatbl_bank(bnk_id).Rows[0][0].ToString();
                radioButton4.Checked = false;
                radioButton5.Checked = true;
            }
            else
            {
                comboBox18.Text = null;
                radioButton4.Checked = true;
                radioButton5.Checked = false;
                textBox41.Text = "";
            }
            string bnk_adv_check = getSalary(Ind_no).Rows[0][38].ToString();
            if (bnk_adv_check == "Bank")
            {
                textBox41.Text = getSalary(Ind_no).Rows[0][35].ToString();
                comboBox17.Text = getDatatbl_bank_advice(bnk_ad_id).Rows[0][0].ToString();
                comboBox18.Text = getDatatbl_bank(bnk_id).Rows[0][0].ToString();
                radioButton4.Checked = false;
                radioButton5.Checked = true;
            }
            if (bnk_adv_check == "Cheque")
            {
                comboBox18.Text = null;
                comboBox17.Text = null;
                comboBox17.Enabled = false;
                comboBox18.Enabled = false;
                textBox41.Enabled = false;
                radioButton4.Checked = true;
                radioButton5.Checked = false;
                textBox41.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            checkList();
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
           
        }

        private void checkBox3_Click_1(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                
                textBox19.Text = "2000";
            }
            if (checkBox3.Checked == false)
            {
                
                textBox19.Text = "0.00";
            }
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                textBox41.Enabled = false;
                comboBox17.Enabled = false;
                comboBox18.Enabled = false;
                comboBox17.Text = "";
                comboBox18.Text = "";
                textBox41.Text = "";
            }
            
        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            textBox41.Enabled = true;
            comboBox17.Enabled = true;
            comboBox18.Enabled = true;
        }
    }
}
