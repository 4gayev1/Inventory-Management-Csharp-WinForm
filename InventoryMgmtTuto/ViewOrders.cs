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
using InventoryMgmtTuto.DBHelper;


namespace InventoryMgmtTuto
{
    public partial class ViewOrders : Form
    {
        public ViewOrders()
        {
            InitializeComponent();
        }

        DbHelper DBHelper = new DbHelper();
        string query = "select * from OrderTbl";
  
        private void ViewOrders_Load(object sender, EventArgs e)
        {
            DBHelper.populate(query, OrdersGV);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void OrdersGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
                printDocument1.Print();
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
            e.Graphics.DrawString("Order Summary",new Font("Centruy",25, FontStyle.Bold),Brushes.Red,new Point(230));
            e.Graphics.DrawString("Order Id:" + OrdersGV.Rows[OrdersGV.CurrentCell.RowIndex].Cells["OrderId"].Value.ToString(), new Font("Centruy", 20, FontStyle.Regular), Brushes.Black, new Point(80,100));
            e.Graphics.DrawString("Customer Id:" + OrdersGV.Rows[OrdersGV.CurrentCell.RowIndex].Cells["CustId"].Value.ToString()
, new Font("Centruy", 20, FontStyle.Regular), Brushes.Black, new Point(80, 150));
            e.Graphics.DrawString("Customer Name:" + OrdersGV.Rows[OrdersGV.CurrentCell.RowIndex].Cells["CustName"].Value.ToString()
, new Font("Centruy", 20, FontStyle.Regular), Brushes.Black, new Point(80, 200));
            e.Graphics.DrawString("Order Date:" + OrdersGV.Rows[OrdersGV.CurrentCell.RowIndex].Cells["OrderDate"].Value.ToString()
, new Font("Centruy", 20, FontStyle.Regular), Brushes.Black, new Point(80, 250));
            e.Graphics.DrawString("Total Amount:" + OrdersGV.Rows[OrdersGV.CurrentCell.RowIndex].Cells["TotalAmt"].Value.ToString()
, new Font("Centruy", 20, FontStyle.Regular), Brushes.Black, new Point(80, 3000));

        }
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
        }

    }
}
