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
    public partial class frmPayrollList : Form
    {
        public static string dgvInd;

        public frmPayrollList()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public DataTable getPayroll()
        {
            string sql = "SELECT payroll_id as 'Payroll ID', payroll_type as 'Payroll Type',pay_period 'Period',calculation_start_from as 'Start Date',calculation_end_on as 'End Date',isSanctioned as 'Status' from tbl_payroll";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            return dt;
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getPayroll();

            int cou = getPayroll().Rows.Count;
            for (int i = 0; i < cou ; i++)
            {
                if(getPayroll().Rows[i][1].ToString() == "MP")
                {
                    dataGridView1.Rows[i].Cells[1].Value = "Monthly Payroll";
                }
                if (getPayroll().Rows[i][5].ToString() == "Y")
                {
                    dataGridView1.Rows[i].Cells[5].Value = "Sanctioned";
                }
                if (getPayroll().Rows[i][5].ToString() == "N")
                {
                    dataGridView1.Rows[i].Cells[5].Value = "Generated";
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please selec a row");
            }
            else
            {

                bool saveFlag = _UpdatEmpPayroll();

                if (saveFlag == true)
                {
                    //MessageBox.Show("Selected Payroll status updated");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error to update the seleced payroll status");
                }

                frmMasterfile fm = new frmMasterfile();
                dgvInd = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                fm.ShowDialog();
            }
        }

        public DataTable getSalDetails(string xyz, string abc)
        {
            string sql = "SELECT gross_pay,net_payable,staff_welfare,satff_security,house_rent,ptax,pf,esi,income_tax,advance_deducted,total_deduct,basic from tbl_emp_payroll where payroll_id = '" + xyz + "' and emp_id = '" + abc + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable emp_id(string xyz)
        {
            string sql = "select emp_id from tbl_emp_payroll where payroll_id  = '"+xyz+"' ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable getSalDetails(string xyz)
        {
            string sql = "SELECT is_pf_applicable,is_esi_applicable from tbl_employee_salary where emp_id = '" + xyz + "' ";
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public bool _UpdatEmpPayroll()
        {
            bool retVal = false;
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            int cou = emp_id(dgvInd).Rows.Count;
            //add  
            for (int i = 0; i < cou; i++)
            {
                string employee_id = emp_id(dgvInd).Rows[i][0].ToString();
                string gross = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][0].ToString();

              

                decimal gross_I = Convert.ToDecimal(gross);

                string net = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][1].ToString();
                decimal netI = Convert.ToDecimal(net);

                //deduct
                string staW = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][2].ToString();
                string stase = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][3].ToString();
                string house = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][4].ToString();
                string ptax = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][5].ToString();
                string pf = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][6].ToString();
                string esi = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][7].ToString();
                string iT = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][8].ToString();
                string advance_d = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][9].ToString();
                string totalD = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][10].ToString();
                string basic = getSalDetails(dgvInd, emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][11].ToString();

                string pf_status = getSalDetails(emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][0].ToString();
                string esi_status = getSalDetails(emp_id(dgvInd).Rows[i][0].ToString()).Rows[0][1].ToString();

                decimal staWI ;
                if(staW == "")
                {
                    staWI = 0;
                }
                else 
                {
                    staWI = Convert.ToDecimal(staW);
                }

                decimal basicI;
                if (basic == "")
                {
                    basicI = 0;
                }
                else
                {
                    basicI = Convert.ToDecimal(basic);
                }

                if (basicI > 15000)
                {
                    basicI = 15000;
                }
                
                decimal staseI;
                if(stase == "")
                {
                    staseI = 0;
                }
                else 
                {
                    staseI = Convert.ToDecimal(stase);
                } 
               
                decimal houseI;
                if(stase == "")
                {
                    houseI = 0;
                }
                else 
                {
                    houseI = Convert.ToDecimal(house);
                }

                decimal ptaxI = Convert.ToDecimal(ptax);
                //decimal pfI = Convert.ToDecimal(pf);
                decimal pfI;
                decimal temp_pf = ((basicI * 12) / 100);
                decimal again_temp_pf = temp_pf - Math.Round(temp_pf);
                decimal roundPF = Math.Round(temp_pf);
                double y1 = Convert.ToDouble(again_temp_pf);
                decimal FINAL_P;

                if (y1 >= 0.5)
                {
                    //FINAL_ESI = roundESI + 1;
                    pfI = roundPF + 1;
                    
                }
                else
                {
                    pfI = Math.Round(temp_pf);
                    
                }

                if(pf_status == "N")
                {
                    pfI = 0;
                }
                FINAL_P = pfI + ptaxI;
                

                //12 % calculate
                //FINAL_P = (ptaxI + pfI);
                decimal esiI = Convert.ToDecimal(esi);

                decimal iTI = Convert.ToDecimal(iT);
                decimal advanceDI = Convert.ToDecimal(advance_d);

                decimal totat_de_I = Convert.ToDecimal(totalD);

                decimal FINAL_W = (staWI + staseI + houseI);
                
                decimal temp_esi = ((gross_I * 75) / 10000);
                decimal again_temp_esi = temp_esi - Math.Round(temp_esi);
                double x1 = Convert.ToDouble(again_temp_esi);
                decimal FINAL_ESI;
                decimal roundESI = Math.Round(temp_esi);
                if (x1 >= 0.01)
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
                if (esi_status == "N")
                {
                    FINAL_ESI = 0;
                }
                decimal FINAL_itI = (iTI + advanceDI);

                decimal FINAL_DEDUCT = FINAL_W + FINAL_P + FINAL_ESI + FINAL_itI;

                //net amount
                decimal FINAL_NET_AMOUNT = gross_I - FINAL_DEDUCT;

                //string fsdate = sdate.Substring(6, 4).ToString() + "-" + sdate.Substring(3, 2).ToString() + "-" + sdate.Substring(0, 2).ToString();
                string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

                string sql = "UPDATE tbl_emp_payroll SET   " +
                        "`net_payable` = '" + FINAL_NET_AMOUNT + "', " +
                        "`esi` = '" + FINAL_ESI + "', `pf` = '" + pfI + "' ," +
                        "`total_deduct` = '" + FINAL_DEDUCT + "'" +
                        " WHERE `emp_id` = '" + emp_id(dgvInd).Rows[i][0].ToString() + "' and `payroll_id` = '" + dgvInd + "'";
                // sql = sql + ""; 

                //System.Diagnostics.Debug.Print(sql);
                System.Diagnostics.Debug.Print("Net Payable : " +FINAL_NET_AMOUNT);
                OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    retVal = true;

                }
                else
                {
                    retVal = false;
                }

                }
                return retVal;
                //return true;
            }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please selec a row");
            }
            else
            {

                bool saveFlag = _UpdatSanctionStatus();

                if (saveFlag == true)
                {
                    MessageBox.Show("Selected Payroll status updated");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error to update the seleced payroll status");
                }
            }
        }

        public bool _UpdatSanctionStatus()
        {
            bool retVal = false;
            //string sql = string.Empty;
            string index1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

            string sql = "UPDATE tbl_payroll SET   " +
                    "`isSanctioned` = 'Y',`modified_on` = '" + date + "',`modified_by` = '" + frmLogin.loggedUser + "' " + " WHERE `payroll_id` = '" + index1 + "' ";

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
            //return true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please selec a row");
            }
            else
            {

                bool saveFlag = deleteFn();

                if (saveFlag == true)
                {
                    MessageBox.Show("Selected Payroll deleted");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error to delete the seleced Payroll");
                }
            }
        }

        public bool deleteFn()
        {
            bool retval = false;
            if (retval == false)
            {
                _DeletePayroll();
                _DeleteEmpPayroll();

                retval = true;
            }
            return retval;
        }

        public bool _DeleteEmpPayroll()
        {
            bool retVal = false;
            string sql = string.Empty;
            string index1 = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            sql = "delete from tbl_emp_payroll " +
                  "WHERE `pay_period` = '" + index1 + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
            }
            return retVal;

        }

        public bool _DeletePayroll()
        {
            bool retVal = false;
            string sql = string.Empty;
            string index1 = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            sql = "delete from tbl_payroll " +
                  "WHERE `pay_period` = '" + index1 + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
            }
            return retVal;

        }
    }
}
