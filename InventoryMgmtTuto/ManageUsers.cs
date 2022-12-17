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
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }

        DbHelper DBHelper = new DbHelper();

        string query = "select * from UserTbl";
        string subject = "User";

        private void label3_Click(object sender, EventArgs e)
        {
            DBHelper.exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string addQuery = "insert into UserTbl values('" + unameTb.Text + "','" + FnameTb.Text + "','" + PasswordTb.Text + "','" + PhoneTb.Text + "')";
            try
            {
                DBHelper.add(addQuery, subject);
                DBHelper.populate(query, UsersGV);
            }
            catch
            {

            }
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            DBHelper.populate(query, UsersGV);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string editQuery = "update UserTbl set Uname ='" + unameTb.Text + "',Ufullname='" + FnameTb.Text + "',Upassword='" + PasswordTb.Text + "'where UPhone ='" + PhoneTb.Text + "'";
            try
            {
                DBHelper.edit(editQuery, subject);
                DBHelper.populate(query, UsersGV);
            }
            catch
            {

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string deleteQuery= "delete from UserTbl where  UPhone= '" + PhoneTb.Text + "';";

            if (PhoneTb.Text == "")
            {
                MessageBox.Show("Enter Phone Number");
            }
            else
            {
                DBHelper.delete(deleteQuery, subject);
                DBHelper.populate(query, UsersGV);
            }
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        private void UsersGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UsersGV.CurrentRow.Selected = true;
            unameTb.Text = UsersGV.Rows[e.RowIndex].Cells["Uname"].Value.ToString();
            FnameTb.Text = UsersGV.Rows[e.RowIndex].Cells["Ufullname"].Value.ToString();
            PasswordTb.Text = UsersGV.Rows[e.RowIndex].Cells["Upassword"].Value.ToString();
            PhoneTb.Text = UsersGV.Rows[e.RowIndex].Cells["Uphone"].Value.ToString();
        }
    }
}
