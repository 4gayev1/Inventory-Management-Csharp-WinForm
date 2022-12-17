using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmtTuto.DBHelper
{
    public class DbHelper
    {
        SqlConnection Con = new SqlConnection(@"Data Source=Aghayev-Desktop;Initial Catalog=Inventorydb;Integrated Security=True;Pooling=False");


       
        public void populate( string query,DataGridView GV)
        {
            try
            {
                Con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                GV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }


        public void add( string query,string message)
        {
                Con.Open();
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show(message + " added");
                Con.Close();
        }

        public void edit(string query, string message)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show(message + " updated");
            Con.Close();
        }

        public void delete(string query, string message)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show(message + " deleted");
            Con.Close();
        }


        public void exit()
        {
            Application.Exit();
        }
    }
}
