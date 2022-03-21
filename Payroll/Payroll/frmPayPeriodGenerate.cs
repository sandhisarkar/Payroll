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
    public partial class frmPayPeriodGenerate : Form
    {
        public static string dgvInd;

        public frmPayPeriodGenerate()
        {
            InitializeComponent();
        }

        public string pType()
        {
            string state = "";
            if (comboBox1.Text == "Monthly Payroll")
            {
                state = "MP";
            }
            
            return state;
        }

        private void frmPayPeriodGenerate_Load(object sender, EventArgs e)
        {
            pType();

            dateTimePicker1.CustomFormat = " ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((pType().ToString() == null) || (pType().ToString() == ""))
            {
                MessageBox.Show("Please select a proper Payroll Type...");
                comboBox1.Focus();
                return;
            }

            if ((dateTimePicker1.Text == null) || (dateTimePicker1.Text == " "))
            {
                MessageBox.Show("Please select a proper Payroll Period...");
                dateTimePicker1.Focus();
                return;
            }

            string sql = "select * from tbl_payroll where pay_period = '"+dateTimePicker1.Text+"'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Selected Payroll period is already exists...");
                button1.Focus();
                return;
            }
            else
            {
                
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {

            dateTimePicker1.CustomFormat = "MM-yyyy";

            button1.Focus();
        }
       
    }
}
