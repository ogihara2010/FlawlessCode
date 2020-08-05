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
        int Grade;
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
            string slipNumber = (string)dataGridView1.CurrentRow.Cells[0].Value; //選択した担当者コードを取得
            string date1 = (string)dataGridView1.CurrentRow.Cells[1].Value;
            string delivery_method = (string)dataGridView1.CurrentRow.Cells[2].Value;
            string pay_method = (string)dataGridView1.CurrentRow.Cells[3].Value;
            string staff_name = (string)dataGridView1.CurrentRow.Cells[4].Value;
            string buyer = (string)dataGridView1.CurrentRow.Cells[5].Value;

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select A.document_number, A.assessment_date,B.delivery_method, B.payment_method, C.staff_name, A.buyer, A.grade_number, B.type from list_result2 A " +
                             "inner join statement_data B ON (A.document_number = B.document_number ) " +
                             "inner join staff_m C ON (B.staff_code = C.staff_code ) where carry_over_month = 1 and document_number = '" + slipNumber +"' " +
                             "and assessment_date = '"+ date1 + "' delivery_method = '" + delivery_method + "' and payment_method = '" + pay_method +"' and " +
                             " and staff_name = '" + staff_name +"'and buyer = '" + buyer + "';";
            conn.Open();
            DataRow row;
            row = dt.Rows[0];
            Grade = (int)row["grade_number"];
            type = (int)row["type"];

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            conn.Close();

            RecordList recordList = new RecordList(statement, staff_id, staff_name, type, slipNumber, Grade);
            this.Close();
            recordList.Show();
        }

        private void NextMonth_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select A.document_number, A.assessment_date,B.delivery_method, B.payment_method, C.staff_name, A.buyer from list_result2 A " +
                             "inner join statement_data B ON (A.document_number = B.document_number ) " +
                             "inner join staff_m C ON (B.staff_code = C.staff_code ) where carry_over_month = 1;";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "伝票番号";
            dataGridView1.Columns[1].HeaderText = "査定日";
            dataGridView1.Columns[2].HeaderText = "受渡方法";
            dataGridView1.Columns[3].HeaderText = "決済方法";
            dataGridView1.Columns[4].HeaderText = "担当者";
            dataGridView1.Columns[5].HeaderText = "買取先";
            

            conn.Close();
        }
    }
}
