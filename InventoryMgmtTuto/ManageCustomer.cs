using InventoryMgmtTuto.DBHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InventoryMgmtTuto
{
    public partial class ManageCustomer : Form
    {
        public ManageCustomer()
        {
            InitializeComponent();
        }


        SqlConnection Con = new SqlConnection(@"Data Source=Aghayev-Desktop;Initial Catalog=Inventorydb;Integrated Security=True;Pooling=False");

        DbHelper DBHelper = new DbHelper();

        string query = "select * from CustomerTbl";
        string subject = "Customer";
        private void label3_Click(object sender, EventArgs e)
        {
            DBHelper.exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string addQuery = "insert into CustomerTbl values('" + Customerid.Text + "','" + CustomernameTb.Text + "','" + CustomerPhoneTb.Text + "')";
            try
            {
                DBHelper.add(addQuery, subject);
                DBHelper.populate(query, CustomersGV);
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string editQuery = "update CustomerTbl set CustId ='" + Customerid.Text + "',CustName='" + CustomernameTb.Text + "'where CustPhone ='" + CustomerPhoneTb.Text + "'";
            try
            {
                DBHelper.edit(editQuery, subject);
                DBHelper.populate(query, CustomersGV);
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string deleteQuery = "delete from CustomerTbl where  CustPhone= '" + CustomerPhoneTb.Text + "';";
            if (CustomerPhoneTb.Text == "")
            {
                MessageBox.Show("Enter Phone Number");
            }
            else
            {
                DBHelper.delete(deleteQuery, subject);
                DBHelper.populate(query, CustomersGV);
            }
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void ManageCustomer_Load(object sender, EventArgs e)
        {
            DBHelper.populate(query, CustomersGV);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        private void CustomersGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomersGV.CurrentRow.Selected = true;
            Customerid.Text = CustomersGV.Rows[e.RowIndex].Cells["CustID"].Value.ToString();
            CustomernameTb.Text = CustomersGV.Rows[e.RowIndex].Cells["CustName"].Value.ToString();
            CustomerPhoneTb.Text = CustomersGV.Rows[e.RowIndex].Cells["CustPhone"].Value.ToString();

            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from OrderTbl where CustId = " + Customerid.Text + "", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            OrderLabel.Text = dt.Rows[0][0].ToString();

            SqlDataAdapter sda1 = new SqlDataAdapter("Select Sum(TotalAmt) from OrderTbl where CustId = " + Customerid.Text + "", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            AmountLabel.Text = dt1.Rows[0][0].ToString();

            SqlDataAdapter sda2 = new SqlDataAdapter("Select Max(OrderDate) from OrderTbl where CustId = " + Customerid.Text + "", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            DateLabel.Text = dt2.Rows[0][0].ToString();
            Con.Close();
        }
    }
}
