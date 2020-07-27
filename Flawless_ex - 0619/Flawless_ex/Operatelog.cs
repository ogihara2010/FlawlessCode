using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace Flawless_ex
{
    public partial class Operatelog : Form
    {

        int staff_id;
        MainMenu mainMenu;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        public Operatelog(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;

        }

        private void Return1_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }

        private void Operatelog_Load(object sender, EventArgs e)
        {
            
           
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "担当者マスタ")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select registration_date, staff_name, staff_name, staff_name_kana, main_category_code, access_auth  from staff_m ;";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更データ";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            if (comboBox1.Text == "顧客マスタ　法人")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select registration_date, register_date, staff_name, shop_name, company_name, address from client_m_corporate ;"; 
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt2);
                dataGridView1.DataSource = dt2;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更データ";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            if (comboBox1.Text == "顧客マスタ　個人")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select registration_date, register_date, name, name_kana, phone_number, address from client_m_individual ;";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt3);
                dataGridView1.DataSource = dt3;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更データ";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            if (comboBox1.Text == "品名マスタ")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select main_category_code, item_name, item_code, registration_date, invalid, item_name from item_m ;";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt4);
                dataGridView1.DataSource = dt4;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更データ";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            if (comboBox1.Text == "大分類マスタ")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select main_category_code, main_category_name, registration_date, insert_name, invalid, insert_name from main_category_m ;";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt5);
                dataGridView1.DataSource = dt5;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更データ";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            if (comboBox1.Text == "消費税マスタ")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select vat_rate, vat_rate, vat_rate, vat_rate, vat_rate, vat_rate from vat_m ;";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt6);
                dataGridView1.DataSource = dt6;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更データ";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
        }
    }
}
