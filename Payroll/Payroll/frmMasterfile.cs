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

using Microsoft;
using Microsoft.Office.Interop.Excel;

namespace Payroll
{
    public partial class frmMasterfile : Form
    {
        

        public frmMasterfile()
        {
            InitializeComponent();
        }

        public string xxx(string id)
        {
            string sql = "SELECT emp_id from tbl_emp_payroll where payroll_id ='" + frmPayrollList.dgvInd + "'";
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            string abc = dt.Rows[0][0].ToString();
            return abc;
        }

        private void frmMasterfile_Load(object sender, EventArgs e)
        {

            
            dataGridView1.Columns.Add("Sl No", "Sl No");

            string sql = "SELECT b.emp_id as 'Emp Code',c.emp_name as 'Employee Name',d.desgn_name as 'Designation',e.dept_name as 'Department',c.emp_joining_date as 'DOJ',b.no_days_calculated as 'No of Days',b.no_days_absent_deduct as 'No of Days Present',b.basic as 'Basic',b.da as 'DA',b.hra as 'HRA',b.specialAllowance as 'Spl. Pay',b.conv_travlg_allow as 'Conveyance Allowance',b.medicalAllowance as 'Medical Allowance',b.lta as 'LTA',b.petrol_car_reim as 'Petrol/Car Allowance',b.mobileAllowance as 'Mobile-phone Allowance',b.addition_arrear_pay as 'Arrear',b.advance_taken as 'Advance Taken',b.gross_pay as 'Gross Salary',b.ptax as 'Prof. Tax',b.pf as 'Provident  Fund ( Employee 12%)',b.esi as 'ESI (Employee 0.75%)',b.income_tax as 'Income Tax',b.advance_deducted as 'Advance Deducted',b.others_deduction as 'Other Deduction',b.total_deduct as 'Total Deduct',b.telephone_reim as 'Mobile Reimbursement',b.site_allow as 'On Site Reimbursement',b.specialAllowance as 'Special Reimbursement',b.incentive as 'Incentive Reimbursement',b.gratuity as 'Gratuity',b.leave_encash as 'Leave Encashment',b.net_payable as 'Net Salary',b.bank_adv_cheque as 'Mode of Pay',f.emp_account_no as 'Bank A/C No.' FROM `tbl_employee_posting` a,`tbl_emp_payroll` b,`tbl_employee` c,`tbl_designation` d,`tbl_department` e,`tbl_employee_salary` f WHERE f.emp_id = a.emp_id AND d.dept_id = e.dept_id AND a.desgn_id =d.desgn_id AND a.emp_id = b.emp_id AND b.emp_id = c.emp_code AND b.payroll_id = '" + frmPayrollList.dgvInd + "'";
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            dataGridView1.DataSource = dt;

            dataGridView1.Columns.Add("Bank Adv.", "Bank Adv.");
            dataGridView1.Columns.Add("Remarks", "Remarks");

            dataGridView1.AutoSize = true;
            dataGridView1.ScrollBars =  System.Windows.Forms.ScrollBars.Both;
            dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;

            

            int cou = dt.Rows.Count;
            for (int i = 0; i < cou; i++)
            {
                
               dataGridView1.Rows[i].Cells[0].Value = i+1;
               dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][0].ToString();
               dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][1].ToString();
               dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][2].ToString();
               dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][3].ToString();
               dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i][4].ToString();
               dataGridView1.Rows[i].Cells[6].Value = dt.Rows[i][5].ToString();
               dataGridView1.Rows[i].Cells[7].Value = Convert.ToInt32(dt.Rows[i][5].ToString()) - Convert.ToInt32(dt.Rows[i][6].ToString());
               dataGridView1.Rows[i].Cells[8].Value = dt.Rows[i][7].ToString();
               dataGridView1.Rows[i].Cells[9].Value = dt.Rows[i][8].ToString();
               dataGridView1.Rows[i].Cells[10].Value = dt.Rows[i][9].ToString();
               dataGridView1.Rows[i].Cells[11].Value = dt.Rows[i][10].ToString();
               dataGridView1.Rows[i].Cells[12].Value = dt.Rows[i][11].ToString();
               dataGridView1.Rows[i].Cells[13].Value = dt.Rows[i][12].ToString();
               dataGridView1.Rows[i].Cells[14].Value = dt.Rows[i][13].ToString();
               dataGridView1.Rows[i].Cells[15].Value = dt.Rows[i][14].ToString();
               dataGridView1.Rows[i].Cells[16].Value = dt.Rows[i][15].ToString();
               dataGridView1.Rows[i].Cells[17].Value = dt.Rows[i][16].ToString();
               dataGridView1.Rows[i].Cells[18].Value = dt.Rows[i][17].ToString();
               dataGridView1.Rows[i].Cells[19].Value = dt.Rows[i][18].ToString();
               dataGridView1.Rows[i].Cells[20].Value = dt.Rows[i][19].ToString();
               dataGridView1.Rows[i].Cells[21].Value = dt.Rows[i][20].ToString();
               dataGridView1.Rows[i].Cells[22].Value = dt.Rows[i][21].ToString();
               dataGridView1.Rows[i].Cells[23].Value = dt.Rows[i][22].ToString();
               dataGridView1.Rows[i].Cells[24].Value = dt.Rows[i][23].ToString();
               dataGridView1.Rows[i].Cells[25].Value = dt.Rows[i][24].ToString();
               dataGridView1.Rows[i].Cells[26].Value = dt.Rows[i][25].ToString();
               dataGridView1.Rows[i].Cells[27].Value = dt.Rows[i][26].ToString();
               dataGridView1.Rows[i].Cells[28].Value = dt.Rows[i][27].ToString();
               dataGridView1.Rows[i].Cells[29].Value = dt.Rows[i][28].ToString();
               dataGridView1.Rows[i].Cells[30].Value = dt.Rows[i][29].ToString();
               dataGridView1.Rows[i].Cells[31].Value = dt.Rows[i][30].ToString();
               dataGridView1.Rows[i].Cells[32].Value = dt.Rows[i][31].ToString();
               dataGridView1.Rows[i].Cells[33].Value = dt.Rows[i][32].ToString();
               dataGridView1.Rows[i].Cells[34].Value = dt.Rows[i][33].ToString();
               dataGridView1.Rows[i].Cells[35].Value = dt.Rows[i][34].ToString();
               dataGridView1.Rows[i].Cells[36].Value="Bank Adv";
               dataGridView1.Rows[i].Cells[37].Value = "Remarks";
            }

            
            
        }


        public System.Data.DataTable getSalDetails(string xyz)
        {
            string sql = "SELECT basic,hra,da,specialAllowance,conv_travlg_allow,lta,petrol_car_reim,medicalAllowance,mobileAllowance,performance_project_allow,advance_taken,gross_pay,net_salary,staff_welfare,satff_security,house_rent,ptax,pf_amt,esi_amt,it_amt,advance_deducted,total_deduct,is_pf_applicable,is_esi_applicable from tbl_employee_salary where emp_id = '" + xyz + "' ";
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            app.Visible = true;

            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;

            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                Range range2 = worksheet.Cells[6, i];
                range2.Borders.Color = ColorTranslator.ToOle(Color.Black);
                range2.EntireRow.AutoFit();
                range2.EntireColumn.AutoFit();
                worksheet.Cells[6, i] = dataGridView1.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Range range3 = worksheet.Cells[i + 7, j + 1];
                    range3.Borders.Color = ColorTranslator.ToOle(Color.Black);
                    range3.EntireRow.AutoFit();
                    range3.EntireColumn.AutoFit();
                    worksheet.Cells[i + 7, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();

                }

            }

            string namexls = "" + ".xls";

            string path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            sfdUAT.Filter = "Xls files (*.xls)|*.xls";
            sfdUAT.FilterIndex = 2;
            sfdUAT.RestoreDirectory = true;
            sfdUAT.FileName = namexls;
            sfdUAT.ShowDialog();

            workbook.SaveAs(sfdUAT.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            app.Quit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
