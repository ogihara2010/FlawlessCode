using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace Flawless_ex
{
    public partial class Operatelog : Form
    {

        public int staff_id;
        public string Access_auth;
        public string Pass;
        TopMenu topMenu;
        MainMenu mainMenu;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt7 = new DataTable();
        public Operatelog(MainMenu main, int id, string access_auth, string pass)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            Access_auth = access_auth;
            Pass = pass;
        }

        private void Return1_Click(object sender, EventArgs e)
        {
            MainMenu main = new MainMenu(topMenu, staff_id, Pass, Access_auth);
            this.Close();
            main.Show();
        }

        private void Operatelog_Load(object sender, EventArgs e)
        {
            
           
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {                                    
            #region "計算書"
            if (comboBox1.Text == "計算書")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;

                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select B.registration_date, C.staff_name, E.item_name, D.item_name, A.reason from list_result2  A inner join list_result  B " +
                                 "ON (A.grade_number = B.result) inner join staff_m C ON (B.staff_code = C.staff_code) inner join item_m D " +
                                 "ON (A.item_name_change = D.item_code) inner join item_m E ON (A.item_code = E.item_code);";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt7);
                dataGridView1.DataSource = dt7;
                dataGridView1.Columns[0].HeaderText = "更新日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更前";
                dataGridView1.Columns[3].HeaderText = "変更後";
                dataGridView1.Columns[4].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
        }

        private void ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            #region "担当者マスタ"
            if (comboBox1.Text == "担当者マスタ")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from revisions where data = 1;";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "変更データ";
                dataGridView1.Columns[1].HeaderText = "変更日時";
                dataGridView1.Columns[2].HeaderText = "変更者";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "品名マスタ"
            else if (comboBox1.Text == "品名マスタ")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter2;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from revisions where data = 2;";
                conn.Open();

                adapter2 = new NpgsqlDataAdapter(sql_str, conn);
                adapter2.Fill(dt4);
                dataGridView1.DataSource = dt4;
                dataGridView1.Columns[0].HeaderText = "変更データ";
                dataGridView1.Columns[1].HeaderText = "変更日時";
                dataGridView1.Columns[2].HeaderText = "変更者";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "大分類マスタ"
            else if (comboBox1.Text == "大分類マスタ")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter3;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from revisions where data = 3; ";
                conn.Open();

                adapter3 = new NpgsqlDataAdapter(sql_str, conn);
                adapter3.Fill(dt5);
                dataGridView1.DataSource = dt5;
                dataGridView1.Columns[0].HeaderText = "変更データ";
                dataGridView1.Columns[1].HeaderText = "変更日時";
                dataGridView1.Columns[2].HeaderText = "変更者";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "消費税マスタ"
            else if (comboBox1.Text == "消費税マスタ")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter9;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select A.upd_date, B.staff_name, A.vat_rate from vat_m  A inner join staff_m  B ON (A.upd_name = B.staff_code);";
                conn.Open();

                adapter9 = new NpgsqlDataAdapter(sql_str, conn);
                adapter9.Fill(dt6);
                dataGridView1.DataSource = dt6;
                dataGridView1.Columns[0].HeaderText = "更新日時";
                dataGridView1.Columns[1].HeaderText = "更新者";
                dataGridView1.Columns[2].HeaderText = "更新後の税率";
                conn.Close();
            }
            #endregion
            #region "顧客マスタ　法人"
            else if (comboBox1.Text == "顧客マスタ　法人")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter4;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from revisions where data = 4;";
                conn.Open();

                adapter4 = new NpgsqlDataAdapter(sql_str, conn);
                adapter4.Fill(dt2);
                dataGridView1.DataSource = dt2;
                dataGridView1.Columns[0].HeaderText = "変更データ";
                dataGridView1.Columns[1].HeaderText = "変更日時";
                dataGridView1.Columns[2].HeaderText = "変更者";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "顧客マスタ　個人"
            if (comboBox1.Text == "顧客マスタ　個人")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from revisions where data = 5;";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt3);
                dataGridView1.DataSource = dt3;
                dataGridView1.Columns[0].HeaderText = "データ";
                dataGridView1.Columns[1].HeaderText = "変更日時";
                dataGridView1.Columns[2].HeaderText = "変更者";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
        }
    }
}
