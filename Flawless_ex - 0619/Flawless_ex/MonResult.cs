using System;
using System.Windows.Forms;
using System.Data;
using Npgsql;

namespace Flawless_ex
{
    public partial class MonResult : Form
    {
        MainMenu mainMenu;
        int staff_id;
        DataTable dt = new DataTable();
        public MonResult(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Return3_Click(object sender, EventArgs e)//戻るボタン
        {
            this.Close();
            mainMenu.Show();
        }

        private void choice_Click(object sender, EventArgs e)//選択ボタン
        {

        }

        private void MonResult_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select A.settlement_date, A.delivery_date, B.staff_name, B.shop_name, C.amount from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                                 "inner join statement_calc C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                 " where B.invalid = 0;";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "査定日";
            dataGridView1.Columns[1].HeaderText = "売却日";
            dataGridView1.Columns[2].HeaderText = "担当";
            dataGridView1.Columns[3].HeaderText = "顧客名";
            dataGridView1.Columns[4].HeaderText = "金額合計";
            //dataGridView1.Columns[5].HeaderText = "卸値合計";
        }
    }
}
