using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace Flawless_ex
{
    public partial class NextMonth : Form
    {
        int staff_id;
        MainMenu mainMenu;
        Statement statement;
        string staff_name;
        int type;
        string slipNumber;
        DataTable dt = new DataTable();
        public NextMonth(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Return4_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }

        private void Choice2_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select A.assessment_date,B.delivery_method, B.payment_method, D.staff_name, A.buyer, A.document_number from list_result2 A inner join statement_data B ON (A.document_number = B.document_number ) inner join statement_calc_data C" +
                " ON (A.document_number = C.document_number) inner join staff_m D ON (B.staff_code = D.staff_code )where carry_over_month = 1;";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            conn.Close();
            DataRow row;
            row = dt.Rows[0];
            string slipNumber = row["document_number"].ToString();

            NpgsqlConnection conn2 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter2;
            conn2.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str2 = "select A.assessment_date,B.delivery_method, B.payment_method, D.staff_name, A.buyer, A.document_number from list_result2 A inner join statement_data B ON (A.document_number = B.document_number ) inner join statement_calc_data C" +
                " ON (A.document_number = C.document_number) inner join staff_m D ON (B.staff_code = D.staff_code )where carry_over_month = 1 and A.document_number = '" + slipNumber + "';";
            conn.Open();

            adapter2 = new NpgsqlDataAdapter(sql_str2, conn2);
            adapter2.Fill(dt);
            conn2.Close();

            RecordList recordList = new RecordList(statement, staff_id, staff_name, type, slipNumber);
            this.Close();
            recordList.Show();
        }

        private void NextMonth_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select A.assessment_date,B.delivery_method, B.payment_method, D.staff_name, A.buyer, A.document_number from list_result2 A inner join statement_data B ON (A.document_number = B.document_number ) inner join statement_calc_data C" +
                " ON (A.document_number = C.document_number) inner join staff_m D ON (B.staff_code = D.staff_code )where carry_over_month = 1;";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "査定日";
            dataGridView1.Columns[1].HeaderText = "受渡方法";
            dataGridView1.Columns[2].HeaderText = "決済方法";
            dataGridView1.Columns[3].HeaderText = "担当者";
            dataGridView1.Columns[4].HeaderText = "買取先";
            dataGridView1.Columns[5].HeaderText = "伝票番号";

            conn.Close();
        }
    }
}
