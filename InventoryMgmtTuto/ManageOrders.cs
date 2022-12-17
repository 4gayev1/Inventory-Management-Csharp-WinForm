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
using System.Xml.Linq;
using InventoryMgmtTuto.DBHelper;
using System.Security.Cryptography.X509Certificates;

namespace InventoryMgmtTuto
{
    public partial class ManageOrders : Form
    {
        public ManageOrders()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=Aghayev-Desktop;Initial Catalog=Inventorydb;Integrated Security=True;Pooling=False");
        DataTable table = new DataTable();
        DbHelper DBHelper = new DbHelper();
        string populateQuery = "select * from CustomerTbl";
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        void populateproducts()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];

                Con.Close();
            }
            catch
            {

            }
        }

        void fillcategory()
        {
            string query = "select * from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }
        }


        void updateproduct()
        {
            int id = Convert.ToInt32(ProductsGV.Rows[ProductsGV.CurrentCell.RowIndex].Cells["ProdId"].Value.ToString()); 
            int newqty = stock - Convert.ToInt32(QtyTb.Text);
            if (newqty < 0) MessageBox.Show("operation failed");
            else
            {
                Con.Open();
                string query = "update producttbl set prodqty =" + newqty + "where ProdId=" + id + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                populateproducts();
            }
        }





        int num =0;
        int uprice, totprice, qty;
        string product;
        private void ManageOrders_Load(object sender, EventArgs e)
        {
            DBHelper.populate(populateQuery, CustomersGV);
            populateproducts();
            fillcategory();
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("Product", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("UPrice", typeof(int));
            table.Columns.Add("TotPrice", typeof(int));
        }

        private void button1_Click(object sender, EventArgs e)
        {
          if(OrderIdTb.Text == "" || CustId.Text == "" || CustName.Text=="" || TotAmount.Text == "")
            {
                MessageBox.Show("Enter Correctly");
            }
            else
            {
                string orderQuery = "insert into OrderTbl values('" + OrderIdTb.Text + "','" + CustId.Text + "','" + CustName.Text + "','" + orderdate.Value.ToString() + "','" + TotAmount.Text + "')";
                string subject = "Order";
                DBHelper.add(orderQuery, subject);
                DBHelper.populate(populateQuery, CustomersGV);


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
          ViewOrders view = new ViewOrders();
            view.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        int flag = 0;
        int stock;
        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

            int sum = 0;

        private void button2_Click_1(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        private void CustomersGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomersGV.CurrentRow.Selected = true;
            CustId.Text = CustomersGV.Rows[e.RowIndex].Cells["CustId"].Value.ToString();
            CustName.Text = CustomersGV.Rows[e.RowIndex].Cells["CustName"].Value.ToString();
        }

        private void ProductsGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductsGV.CurrentRow.Selected = true;
            product = ProductsGV.Rows[e.RowIndex].Cells["ProdName"].Value.ToString();
            stock = Convert.ToInt32(ProductsGV.Rows[e.RowIndex].Cells["ProdQty"].Value.ToString());
            uprice = Convert.ToInt32(ProductsGV.Rows[e.RowIndex].Cells["ProdPrice"].Value.ToString());
            flag = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void TotAmount_Click(object sender, EventArgs e)
        {

        }

        private void OrderGv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (QtyTb.Text == "")
            {
                MessageBox.Show("Enter Order Amount");
            }
            else if(flag == 0)
            {
                MessageBox.Show("Select Product");
            }
            else if (Convert.ToInt32(QtyTb.Text) > stock)
            {
                MessageBox.Show("There is no enough stock");
            }
            else
            {
                num = num + 1;
                qty = Convert.ToInt32(QtyTb.Text);
                totprice = qty * uprice;
                table.Rows.Add(num, product, qty, uprice, totprice);
                OrderGv.DataSource = table;
                flag = 0;
            }
            sum = sum + totprice;
            TotAmount.Text =  sum.ToString();
            updateproduct();
        }

  

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTbl where ProdCat = '" + SearchCombo.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
    }
}
