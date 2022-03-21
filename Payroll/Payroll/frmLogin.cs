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
    public partial class frmLogin : Form
    {
        public static OdbcConnection dbcon;
		public string err=null;
		private INIReader rd=null;
		private KeyValueStruct udtKeyValue;
		private FileorFolder fileExs=null;
        private IWin32Window parentWindow;
        private bool cancelNot = false;

        public static string loggedUser;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }
        public  OdbcConnection Connect()
        {
            string conString = null;
            string iniPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "\\" + Constants.INI_FILE_NAME;
            string err = null;
            fileExs = new FileorFolder();
            DialogResult result;
            INIFile ini = new INIFile();
            dbcon = new OdbcConnection(conString);

            try
            {
                if (File.Exists(iniPath) == true)
                {
                    conString = ini.ReadINI(Constants.INI_SECTION, Constants.INI_KEY, string.Empty, iniPath);
                    dbcon.ConnectionString = conString;
                    dbcon.Open();
                }
                else
                {
                    rd = new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
                    udtKeyValue.Key = Constants.INI_FILE_EROR.ToString();
                    udtKeyValue.Section = Constants.COMMON_EXCEPTION_SECTION;
                    string ErrMsg = rd.Read(udtKeyValue);
                    //result = MessageBox.Show("Error while connect to the database...You want to create it?", "Connection error", MessageBoxButtons.YesNo);
                    //if (result == DialogResult.Yes)
                    //{
                    //    frmConnection frmConn = new frmConnection(iniPath);
                    //}
                    //else
                    //{
                    //    Application.Exit();
                    //}
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                //result = MessageBox.Show("Error while connect to the database...You want to create it?","Connection error",MessageBoxButtons.YesNo);
                //if (result == DialogResult.Yes)
                //{
                //    frmConnection frmConn = new frmConnection(iniPath);
                //}
                //else
                //{
                //    Application.Exit();
                //}
            }
            if (dbcon.State == ConnectionState.Closed)
            {
                rd = new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
                udtKeyValue.Key = Constants.DB_CONNECTION_ERROR.ToString();
                udtKeyValue.Section = Constants.DB_CONNECTION_EXCEPTION_SECTION;
                string ErrMsg = rd.Read(udtKeyValue);
                result = MessageBox.Show("Error while connect to the database...You want to create it?", "Connection error", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    frmConnection frmConn = new frmConnection(iniPath);
                    if (parentWindow != null)
                    {
                        frmConn.ShowDialog(parentWindow);
                    }
                    else
                    {
                        frmConn.ShowDialog();
                    }
                    if ((File.Exists(iniPath) == true))
                    {
                        conString = ini.ReadINI(Constants.INI_SECTION, Constants.INI_KEY, string.Empty, iniPath);
                        dbcon.ConnectionString = conString;
                        dbcon.Open();
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
            return dbcon;
        }

        public DataTable selectUser(string a,string b)
        {

            string sql = "Select distinct user_id,user_pwd from tbl_user where user_id = '"+a+"' and user_pwd = '"+b+"'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcCommand cmd = new OdbcCommand(sql, Connect());
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (selectUser(textBoxUid.Text, textBoxPsw.Text).Rows.Count > 0)
            {
                string user = selectUser(textBoxUid.Text, textBoxPsw.Text).Rows[0][0].ToString();
                string password = selectUser(textBoxUid.Text, textBoxPsw.Text).Rows[0][1].ToString();

                
                   
                    frmMain frm = new frmMain();
                    loggedUser = user;
                    this.Hide();
                    frm.ShowDialog();
               

            }
            else
            {
                MessageBox.Show("Wrong user id or password !");
                textBoxUid.Focus();
            }
            
        }

        private void textBoxUid_Leave(object sender, EventArgs e)
        {
            if ((textBoxUid.Text == null) || (textBoxUid.Text == ""))
            {
                MessageBox.Show("You cannot leave User ID field blank...");
                textBoxUid.Focus();
                textBoxUid.Select();
            }
            else
            {
                
                textBoxPsw.Focus();
                textBoxPsw.Select();
            }
        }

        private void textBoxPsw_Leave(object sender, EventArgs e)
        {
            if ((textBoxPsw.Text == null) || (textBoxPsw.Text == ""))
            {                
                MessageBox.Show("You cannot leave Password field blank...");
                textBoxPsw.Focus();
                textBoxPsw.Select();
            }
            else
            {
                buttonLogin.Focus();
            }
        }

        
    }
}
