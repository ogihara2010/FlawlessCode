using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
namespace Flawless_ex
{
    public partial class ProductAddMenu : Form　//品名新規登録メニュー
    {
        MasterMaintenanceMenu master;
        DataTable dt;


        public ProductAddMenu(DataTable dt, MasterMaintenanceMenu master)
        {
            InitializeComponent();

            this.master = master;
            this.dt = dt;
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            ItemMaster product = new ItemMaster(master);

            this.Close();
            product.Show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            NpgsqlCommandBuilder builder;

            int productCode = int.Parse(productCodeTextBox.Text);
            string productName = productNameTextBox.Text;

            string sql_str = "insert into productlist values(" + productCode + ", '" + productName + "')";

            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = test_login;"; //変更予定
            conn.Open();
            adapter = new NpgsqlDataAdapter(sql_str, conn);
            builder = new NpgsqlCommandBuilder(adapter);

            adapter.Fill(dt);
            adapter.Update(dt);

            MessageBox.Show("登録完了");

            conn.Close();
        }

     
    }
}
