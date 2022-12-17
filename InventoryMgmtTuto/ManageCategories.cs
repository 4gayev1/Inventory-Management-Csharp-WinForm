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
using System.Collections;
using InventoryMgmtTuto.DBHelper;
using System.Xml.Linq;


namespace InventoryMgmtTuto
{
    public partial class ManageCategories : Form
    {
       
        public ManageCategories()
        {
            InitializeComponent();
        }

        DbHelper DBHelper = new DbHelper();
        string query = "select * from CategoryTbl";
        string subject = "Category";

        private void label3_Click(object sender, EventArgs e)
        {
            DBHelper.exit();
        }
 
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string addtQuery = "insert into CategoryTbl values('" + CatIdTb.Text + "','" + CatNameTb.Text + "')";
                DBHelper.add(addtQuery, subject);
                DBHelper.populate(query, CategoriesGV);
            }
            catch
            {
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string editQuery = "update CategoryTbl set CatName ='" + CatNameTb.Text + "'where CatId ='" + CatIdTb.Text + "'";
                DBHelper.edit(editQuery, subject);
                DBHelper.populate(query, CategoriesGV);
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CatIdTb.Text == "")
            {
                MessageBox.Show("Enter Phone Number");
            }
            else
            {
                string deleteQuery = "delete from CategoryTbl where  CatId= '" + CatIdTb.Text + "';";
                DBHelper.delete(deleteQuery, subject);
                DBHelper.populate(query, CategoriesGV);
            }
        }
        private void ManageCategories_Load(object sender, EventArgs e)
        {
            
            DBHelper.populate(query, CategoriesGV);
        }

        private void CategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        private void CategoriesGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CategoriesGV.CurrentRow.Selected = true;
            CatIdTb.Text = CategoriesGV.Rows[e.RowIndex].Cells["CatId"].Value.ToString();
            CatNameTb.Text = CategoriesGV.Rows[e.RowIndex].Cells["CatName"].Value.ToString();
        }
    }
}
