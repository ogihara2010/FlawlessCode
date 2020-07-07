using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace Flawless_ex
{
    public partial class DataSearchResults : Form
    {
        CustomerHistory customerHistory;
        DataTable dt = new DataTable();
        public DataSearchResults(CustomerHistory customer)
        {
            InitializeComponent();

            customerHistory = customer;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻るボタン
        {

            this.Close();
            customerHistory.Show();

        }

        private void DataSearchResults_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select staff_code, staff_name, main_category_name from staff_m inner join main_category_m on staff_m.main_category_code = main_category_m.main_category_code where staff_m.invalid = 0 ";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "決済日";
            dataGridView1.Columns[1].HeaderText = "受渡日";
            dataGridView1.Columns[2].HeaderText = "店舗名";
            dataGridView1.Columns[3].HeaderText = "担当者名・個人名";
            dataGridView1.Columns[4].HeaderText = "電話番号";
            dataGridView1.Columns[5].HeaderText = "住所";
            dataGridView1.Columns[6].HeaderText = "品名";
            dataGridView1.Columns[7].HeaderText = "金額";

            conn.Close();
        }
    }
}
