using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace Flawless_ex
{
    public partial class CustomerHistory : Form
    {
        int staff_id;
        MainMenu mainMenu;
        DataTable dt = new DataTable();  // 大分類
        DataTable dt2 = new DataTable(); //品名と大分類関連付け
        DataTable dt3 = new DataTable(); // 品名情報すべて
        DataTable dt4 = new DataTable();
        int a = 0; // クリック数 
        int itemMainCategoryCode;
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        public CustomerHistory(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }

        private void CustomerHistory_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            //大分類検索用
            string sql_str = "select * from main_category_m where invalid = 0 order by main_category_code;";
            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            
            //品名検索用
            string sql_str2 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.invalid = 0;";
            NpgsqlDataAdapter adapter1;
            adapter1 = new NpgsqlDataAdapter(sql_str2, conn);
            adapter1.Fill(dt2);

            conn.Close();

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "main_category_name";
            comboBox1.ValueMember = "main_category_code";

            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "item_name";
            comboBox2.ValueMember = "item_code";
        }

        private void dataSelectButton_Click(object sender, EventArgs e)//検索ボタン
        {
            int type;
            string shopname;
            string shopnamekana;
            string name;
            string search1 = "or";
            string search2 = "or";
            string search3 = "or";
            string search4 = "or";
            string search5 = "or";
            string search6 = "or";
            string search7 = "or";
            string search8 = "or";
            string search9 = "or";
            string search10 = "or";
            string search11 = "or";
            string search12 = "or";
            string search13 = "or";
            string search14 = "or";

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            DataSearchResults dataSearch = new DataSearchResults(this);
            this.Hide();
            dataSearch.Show();
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "氏名";
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            radioButton5.Enabled = false;
            radioButton6.Enabled = false;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "担当者名";
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            radioButton5.Enabled = true;
            radioButton6.Enabled = true;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (a > 1) {
                int codeNum = (int)comboBox1.SelectedValue;
                dt2.Clear();
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt2);
                comboBox2.DataSource = dt2;
                comboBox2.DisplayMember = "item_name";
                comboBox2.ValueMember = "item_code";

                conn.Close();
            }
            a++;
        }
    }
}
