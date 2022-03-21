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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text =  "NTPL Accounts" +"                     "+ "Logged in User_id is :" +  " " +frmLogin.loggedUser;

            int n = accessUser().Rows.Count;
            if (n > 0)
            {
                toolsToolStripMenuItem.Visible = true;
                mastersToolStripMenuItem.Visible = false;
                employeeToolStripMenuItem.Visible = false;
                payrollToolStripMenuItem.Visible = false;
                eSIUpdateToolStripMenuItem.Visible = false;
                toolsToolStripMenuItem.Visible = false;
                monthlyPayrollGenerationToolStripMenuItem.Visible = false;
            }
            else
            {
                toolsToolStripMenuItem.Visible = true;
                mastersToolStripMenuItem.Visible = true;
                employeeToolStripMenuItem.Visible = true;
                payrollToolStripMenuItem.Visible = true;
                eSIUpdateToolStripMenuItem.Visible = false;
                toolsToolStripMenuItem.Visible = false;
                monthlyPayrollGenerationToolStripMenuItem.Visible = false;
            }
        }


        public DataTable accessUser()
        {
            string sql = "Select * from tbl_user where is_Admin = 'Y' and user_id = '"+frmLogin.loggedUser+"'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void officeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPremisesList frmp = new frmPremisesList();
            frmp.ShowDialog();
        }

        private void eSIUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmESIChange fm = new frmESIChange();
            fm.ShowDialog();
        }

        private void officeProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOfficeList fm = new frmOfficeList();
            fm.ShowDialog();
        }

        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDepList fm = new frmDepList();
            fm.ShowDialog();
        }

        private void designationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDesList fm = new frmDesList();
            fm.ShowDialog();
        }

        private void bankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBankList fm = new frmBankList();
            fm.ShowDialog();
        }

        private void bankAdviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBankAdviceList fm = new frmBankAdviceList();
            fm.ShowDialog();
        }

        private void employeeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeeDetails fm = new frmEmployeeDetails();
            fm.ShowDialog();
        }

        private void newJoiningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeeAdd fm = new frmEmployeeAdd();
            fm.ShowDialog();
        }

        private void payrollDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPayrollList fm = new frmPayrollList();
            fm.ShowDialog();
        }

        private void monthlyPayrollGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPayPeriodGenerate fm = new frmPayPeriodGenerate();
            fm.ShowDialog();
        }
    }
}
