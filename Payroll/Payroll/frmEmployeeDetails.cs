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
    public partial class frmEmployeeDetails : Form
    {
        public static string dgvInd;

        public frmEmployeeDetails()
        {
            InitializeComponent();
        }

        private void frmEmployeeDetails_Load(object sender, EventArgs e)
        {
            esate();
        }
        public string esate()
        {
            string state = "";
            if (comboBox1.Text == "Approved")
            {
                state = "A";
            }
            if (comboBox1.Text == "Pending")
            {
                state = "P";
            }
            if (comboBox1.Text == "Resigned")
            {
                state = "R";
            }
            if (comboBox1.Text == "Terminated")
            {
                state = "T";
            }
            if (comboBox1.Text == "Deleted")
            {
                state = "D";
            }
            return state;
        }
        public DataTable EmpList(string xyz)
        {
            string sql = "SELECT DISTINCT a.emp_code as 'Code',a.emp_name as 'Emp Name',d.office_type_id as 'Office Type',e.office_name as 'Office Name',f.dept_name as 'Department',g.desgn_name as 'Designation',date_format(a.emp_joining_date, '%d-%m-%Y') as 'Joining Date',b.employment_type_name as 'Employment' FROM `tbl_employment_type` b,`tbl_employee` a,`tbl_employee_employment` c,`tbl_employee_posting`d,`tbl_office` e,`tbl_department` f,`tbl_designation` g WHERE c.emp_id = a.emp_code AND b.employment_type_id = c.employment_type_id  AND d.emp_id = a.emp_code AND d.office_id = e.office_id AND d.dept_id = f.dept_id AND d.desgn_id = g.desgn_id and a.emp_status = '" + xyz + "' ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = EmpList(esate());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please selec a row");
            }
            else
            {

                bool saveFlag = _UpdateEmployee();

                if (saveFlag == true)
                {
                    MessageBox.Show("Selected employee status updated");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error to update the seleced employee status");
                }
            }
        }
        public bool _UpdateEmployee()
        {
            bool retVal = false;
            //string sql = string.Empty;
            string index1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            string sql = "UPDATE tbl_employee SET   " +
                    "`emp_status` = 'R',`modified_on` = '" + date + "',`modified_by` = '" + frmLogin.loggedUser + "' " + " WHERE `emp_code` = '" + index1 + "' ";

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

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please selec a row");
            }
            else
            {
                frmEmployeeEdit fm = new frmEmployeeEdit();
                dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                fm.ShowDialog();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please selec a row");
            }
            else
            {

                dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                bool saveFlag = _UpdateESI();

                if (saveFlag == true)
                {
                    MessageBox.Show("Updated");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error");
                }

            }
        }

        public DataTable getSalDetails(string xyz)
        {
            string sql = "SELECT basic,hra,da,specialAllowance,conv_travlg_allow,lta,petrol_car_reim,medicalAllowance,mobileAllowance,performance_project_allow,advance_taken,gross_pay,net_salary,staff_welfare,satff_security,house_rent,ptax,pf_amt,esi_amt,it_amt,advance_deducted,total_deduct from tbl_employee_salary where emp_id = '" + xyz + "' ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public bool _UpdateESI()
        {
            bool retVal = false;
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            //add   
            string basic = getSalDetails(dgvInd).Rows[0][0].ToString();
            string hra = getSalDetails(dgvInd).Rows[0][1].ToString();
            string da = getSalDetails(dgvInd).Rows[0][2].ToString();
            string sa = getSalDetails(dgvInd).Rows[0][3].ToString();
            string cta = getSalDetails(dgvInd).Rows[0][4].ToString();
            string lta = getSalDetails(dgvInd).Rows[0][5].ToString();
            string pcr = getSalDetails(dgvInd).Rows[0][6].ToString();
            string meA = getSalDetails(dgvInd).Rows[0][7].ToString();
            string moA = getSalDetails(dgvInd).Rows[0][8].ToString();
            string ppA = getSalDetails(dgvInd).Rows[0][9].ToString();
            string at = getSalDetails(dgvInd).Rows[0][10].ToString();
            string gross = getSalDetails(dgvInd).Rows[0][11].ToString();

            decimal b = Convert.ToDecimal(basic);
            decimal h = Convert.ToDecimal(hra);
            decimal d = Convert.ToDecimal(da);
            decimal sa_I = Convert.ToDecimal(sa);
            decimal cta_I = Convert.ToDecimal(cta);
            decimal lta_I = Convert.ToDecimal(lta);
            decimal pcr_I = Convert.ToDecimal(pcr);
            decimal meaI = Convert.ToDecimal(meA);
            decimal moaI = Convert.ToDecimal(moA);
            decimal ppaI = Convert.ToDecimal(ppA);
            decimal atI = Convert.ToDecimal(at);

            decimal gross_I = Convert.ToDecimal(gross);

            string net = getSalDetails(dgvInd).Rows[0][12].ToString();
            decimal netI = Convert.ToDecimal(net);

            //deduct
            string staW = getSalDetails(dgvInd).Rows[0][13].ToString();
            string stase = getSalDetails(dgvInd).Rows[0][14].ToString();
            string house = getSalDetails(dgvInd).Rows[0][15].ToString();
            string ptax = getSalDetails(dgvInd).Rows[0][16].ToString();
            string pf = getSalDetails(dgvInd).Rows[0][17].ToString();
            string esi = getSalDetails(dgvInd).Rows[0][18].ToString();
            string iT = getSalDetails(dgvInd).Rows[0][19].ToString();
            string advance_d = getSalDetails(dgvInd).Rows[0][20].ToString();
            string totalD = getSalDetails(dgvInd).Rows[0][21].ToString();


            decimal staWI = Convert.ToDecimal(staW);
            decimal staseI = Convert.ToDecimal(stase);
            decimal houseI = Convert.ToDecimal(house);

            decimal ptaxI = Convert.ToDecimal(ptax);
            decimal pfI = Convert.ToDecimal(pf);

            decimal esiI = Convert.ToDecimal(esi);

            decimal iTI = Convert.ToDecimal(iT);
            decimal advanceDI = Convert.ToDecimal(advance_d);

            decimal totat_de_I = Convert.ToDecimal(totalD);

            decimal FINAL_W = (staWI + staseI + houseI);
            decimal FINAL_P = (ptaxI + pfI);
            decimal temp_esi = ((gross_I * 75) / 10000);
            decimal again_temp_esi = temp_esi - Math.Round(temp_esi);
            double x1 = Convert.ToDouble(again_temp_esi);
            decimal FINAL_ESI;
            decimal roundESI = Math.Round(temp_esi);
            if (x1 > 0.01)
            {
                FINAL_ESI = roundESI + 1;

            }
            else
            {
                FINAL_ESI = Math.Round(temp_esi);
            }
            if (gross_I > 21000)
            {
                FINAL_ESI = 0;
            }

            decimal FINAL_itI = (iTI + advanceDI);

            decimal FINAL_DEDUCT = FINAL_W + FINAL_P + FINAL_ESI + FINAL_itI;

            //net amount
            decimal FINAL_NET_AMOUNT = gross_I - FINAL_DEDUCT;

            //string fsdate = sdate.Substring(6, 4).ToString() + "-" + sdate.Substring(3, 2).ToString() + "-" + sdate.Substring(0, 2).ToString();
            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            string sql = "UPDATE tbl_employee_salary SET   " +
                    "`net_salary` = '" + FINAL_NET_AMOUNT + "', " +
                    "`esi_amt` = '" + FINAL_ESI + "'," +
                    "`total_deduct` = '" + FINAL_DEDUCT + "'" +
                    " WHERE `emp_id` = '" + dgvInd + "'";
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

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}

