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
    public partial class frmPremisesList : Form
    {
        public static string dgvInd;

        public frmPremisesList()
        {
            InitializeComponent();
        }

        private void frmPremises_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = premisesList();
            
            
        }

        public DataTable premisesList()
        {
            string sql = "Select premises_id as 'ID', premises_name as 'Name', premises_type as 'Type' from tbl_premises ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPremisesAdd fm = new frmPremisesAdd();
            this.Hide();
            fm.ShowDialog();
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
                
                bool saveFlag = _DeletePremises();

                if (saveFlag == true)
                {
                    MessageBox.Show("Selected premises deleted");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There's some error to delete the seleced premises");
                }
            }
        }

        public bool _DeletePremises()
        {
            bool retVal = false;
            string sql = string.Empty;
            string index1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            sql = "delete from tbl_premises " +
                  "WHERE `premises_id` = '" + index1 + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, frmLogin.dbcon);
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
            }
            return retVal;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmpremisesEdit fm =new frmpremisesEdit();
            dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            this.Close();
            this.Hide();
            fm.ShowDialog();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
             dgvInd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
             frmpremisesEdit fm = new frmpremisesEdit();
             this.Close();
             this.Hide();
             fm.ShowDialog();
        }
    }
}
