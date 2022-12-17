using System;
using System.Collections;
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
using InventoryMgmtTuto.DBHelper;
using InventoryMgmtTuto.Interfaces;

namespace InventoryMgmtTuto
{
    public partial class ManageProducts : Form
    {

        IRepository productRepository;

        public ManageProducts()
        {
            InitializeComponent();
        }


        SqlConnection Con = new SqlConnection(@"Data Source=Aghayev-Desktop;Initial Catalog=Inventorydb;Integrated Security=True;Pooling=False");
        DbHelper DBHelper = new DbHelper();
        string query = "select * from ProductTbl";
        string subject = "Product";
        private void label3_Click(object sender, EventArgs e)
        {
            DBHelper.exit();
        }


        void filterbycategory()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTbl where ProdCat = '"+SearchCombo.SelectedValue.ToString()+"'";
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
            SqlCommand cmd = new SqlCommand(query,Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName",typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CatName";
                CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }
        }

        void fillSearchCombo()
        {
            string query = "select * from CategoryTbl where CatName='" +SearchCombo.SelectedValue.ToString()+"'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CatName";
                CatCombo.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {
            fillcategory();
            DBHelper.populate(query, ProductsGV);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string addQuery = "insert into ProductTbl values('" + ProdIdTb.Text + "','" + ProdNameTb.Text + "','" + QtyTb.Text + "','" + PriceTb.Text + "','" + DescriptionTb.Text + "','" + CatCombo.SelectedValue.ToString() + "')";
            try
            {
                DBHelper.add(addQuery, subject);
                DBHelper.populate(query, ProductsGV);
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string editQuery = "update ProductTbl set ProdName ='" + ProdNameTb.Text + "',ProdQty='" + QtyTb.Text + "',ProdPrice='" + PriceTb.Text + "',ProdDesc='" + DescriptionTb.Text + "'where ProdId ='" + ProdIdTb.Text + "'";
            try
            {
                DBHelper.edit(editQuery, subject);
                DBHelper.populate(query, ProductsGV);
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string deleteQuery = "delete from ProductTbl where  ProdId= '" + ProdIdTb.Text + "';";
            if (ProdIdTb.Text == "")
            {
                MessageBox.Show("Enter Phone Number");
            }
            else
            {
                DBHelper.delete(deleteQuery, subject);
                DBHelper.populate(query, ProductsGV);
            }
        }
        private void CategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        

        }

        private void button5_Click(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DBHelper.populate(query, ProductsGV);
        }

        private void ProdIdTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void SearchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        private void ProductsGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductsGV.CurrentRow.Selected = true;
            ProdIdTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdId"].Value.ToString();
            ProdNameTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdName"].Value.ToString();
            QtyTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdQty"].Value.ToString();
            PriceTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdPrice"].Value.ToString();
            DescriptionTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdDesc"].Value.ToString();
            CatCombo.SelectedValue = ProductsGV.Rows[e.RowIndex].Cells["ProdCat"].Value.ToString();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
