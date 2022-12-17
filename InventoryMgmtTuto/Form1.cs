using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using InventoryMgmtTuto.DBHelper;


namespace InventoryMgmtTuto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DbHelper DBHelper = new DbHelper();
        SqlConnection Con = new SqlConnection(@"Data Source=Aghayev-Desktop;Initial Catalog=Inventorydb;Integrated Security=True;Pooling=False");

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) PasswordTb.UseSystemPasswordChar = false;
            else PasswordTb.UseSystemPasswordChar = true;            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            UnameTb.Text = "";
            PasswordTb.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from userTbl where Uname='" + UnameTb.Text + "' and Upassword ='" + PasswordTb.Text + "'",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                HomeForm Home = new HomeForm();
                Home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
            Con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            DBHelper.exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}