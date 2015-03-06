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

namespace ProductManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Product aProduct=new Product();
            aProduct.productId = productIdTextBox.Text;
            aProduct.productName = productNameTextBox.Text;
            aProduct.productPrice = productPriceTextBox.Text;
            aProduct.productCategory = productCategoryTextBox.Text;

            string connectionString = @"server=BITM-401-PC0\SQLEXPRESS; database=product; Integrated Security=true";
            SqlConnection connection=new SqlConnection(connectionString);

            //Insert Query
            string query = "INSERT INTO product VALUES('"+aProduct.productId+"','"+aProduct.productName+"','"+aProduct.productPrice+"','"+aProduct.productCategory+"')";
            
            SqlCommand command=new SqlCommand(query,connection);
            connection.Open();
            int rowAffected=command.ExecuteNonQuery();
            connection.Close();
            productIdTextBox.Text = string.Empty;
            productNameTextBox.Text = string.Empty;
            productPriceTextBox.Text = string.Empty;
            productCategoryTextBox.Text = string.Empty;
            MessageBox.Show("Product Added Successfully");

            //Display in list Box
            query = "";
            query = "SELECT * from product";
            SqlCommand ccommand=new SqlCommand(query,connection);
            connection.Open();
            SqlDataReader reader = ccommand.ExecuteReader();
            
            List<Product> listProduct=new List<Product>();
           
            while (reader.Read())
            {
                Product rProduct=new Product();
                rProduct.productId = reader["ID"].ToString();
                rProduct.productName = reader["productName"].ToString();
                rProduct.productPrice = reader["ProductPrice"].ToString();
                rProduct.productCategory = reader["Productcategory"].ToString();
                listProduct.Add(rProduct);

            }
            reader.Close();
            connection.Close();
           productListView.Items.Clear();
            foreach (Product fProduct in listProduct)
            {
           
                ListViewItem item=new ListViewItem(fProduct.productId.ToString());
                item.SubItems.Add(fProduct.productName);
                item.SubItems.Add(fProduct.productPrice);
                item.SubItems.Add(fProduct.productCategory);
                productListView.Items.Add(item);

            }

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string connectionString = @"server=BITM-401-PC0\SQLEXPRESS; database=product; Integrated Security=true";
            SqlConnection connection=new SqlConnection(connectionString);

            //Select query
            string query = "Select * from product where ID='"+productIdTextBox.Text+"'";

            SqlCommand command=new SqlCommand(query,connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                
                productIdTextBox.Text = reader["ID"].ToString();
                productNameTextBox.Text = reader["ProductName"].ToString();
                productPriceTextBox.Text = reader["ProductPrice"].ToString();
                productCategoryTextBox.Text = reader["ProductCategory"].ToString();
            }
        }
    }
}
