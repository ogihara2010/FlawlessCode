using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace Flawless_ex
{
    public partial class Operatelog : Form
    {

        int staff_id;
        string Access_auth;
        string Pass;
        string document;
        int control;
        int grade;
        MainMenu mainMenu;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        DataTable dt9 = new DataTable();
        DataTable dt10 = new DataTable();
        DataTable dt11 = new DataTable();
        DataTable dt12 = new DataTable();
        DataTable dt13 = new DataTable();
        DataTable dt14 = new DataTable();
        DataTable dt15 = new DataTable();
        DataTable dt16 = new DataTable();
        DataTable dt17 = new DataTable();
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
            this.Close();
            mainMenu.Show();
        }

        private void Operatelog_Load(object sender, EventArgs e)
        {
            
           
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region "担当者マスタ　名前変更"
            if (comboBox1.Text == "担当者マスタ  名前変更")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select B.insert_date, C.staff_name, B.staff_name_before, B.staff_name_after, A.reason  from staff_m A inner join " +
                                 " staff_name_revisions B ON (A.staff_code = B.staff_code) inner join staff_m C ON (B.insert_name = C.staff_code);";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更前";
                dataGridView1.Columns[3].HeaderText = "変更後";
                dataGridView1.Columns[4].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "担当者　パスワード変更"
            else if (comboBox1.Text == "担当者マスタ　パスワード変更")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select B.insert_date, C.staff_name, B.staff_password_before, B.staff_password_after, A.reason  from staff_m A inner join " +
                                 " staff_password_revisions B ON (A.staff_code = B.staff_code) inner join staff_m C ON (B.insert_name = C.staff_code);";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更前";
                dataGridView1.Columns[3].HeaderText = "変更後";
                dataGridView1.Columns[4].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "担当者　アクセス権限"
            else if (comboBox1.Text == "担当者マスタ　アクセス権限変更")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select B.insert_date, C.staff_name, A.staff_name, B.access_auth_before, B.access_auth_after, A.reason  from staff_m A " +
                                 "inner join staff_access_auth_revisions B ON (A.staff_code = B.staff_code) inner join staff_m C ON (B.insert_name = C.staff_code);";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "権限を変更された担当者";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "担当者　無効"
            else if (comboBox1.Text == "担当者マスタ　無効")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select B.invalid_date, C.staff_name, A.staff_code, A.staff_name, A.access_auth, A.password, A.reason  from staff_m A " +
                                 "inner join staff_m_revisions_invalid B ON (A.staff_code = B.staff_code) inner join staff_m C ON (B.insert_name = C.staff_code) where A.invalid = 1;";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "無効になった担当者コード";
                dataGridView1.Columns[3].HeaderText = "無効になった担当者";
                dataGridView1.Columns[4].HeaderText = "その担当者のアクセス権限";
                dataGridView1.Columns[5].HeaderText = "その担当者のパスワード";
                dataGridView1.Columns[6].HeaderText = "無効理由";
                conn.Close();
            }
            #endregion
            else if (comboBox1.Text == "顧客マスタ　法人")
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
            else if (comboBox1.Text == "顧客マスタ　個人")
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
            #region "品名マスタ　無効"
            else if (comboBox1.Text == "品名マスタ　無効")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select B.invalid_date, C.staff_name, A.item_code, A.main_category_code,A.item_name, A.reason from item_m A " +
                                 " inner join item_m_invalid_revisions B ON (A.item_code = B.item_code) inner join staff_m C ON (B.insert_name = C.staff_code) where A.invalid = 1;";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt4);
                dataGridView1.DataSource = dt4;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "無効にした品名コード";
                dataGridView1.Columns[3].HeaderText = "無効にした大分類コード";
                dataGridView1.Columns[4].HeaderText = "無効にした品名";
                dataGridView1.Columns[5].HeaderText = "無効理由";
                conn.Close();
            }
            #endregion
            #region "品名マスタ　大分類変更"
            else if (comboBox1.Text == "品名マスタ　大分類変更")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select B.change_date, C.staff_name, B.main_category_code_before, B.main_category_code_after, A.reason from item_m A " +
                                 " inner join item_m_main_category_code_revisions B ON (A.item_code = B.item_code) " +
                                 " inner join staff_m C ON (B.insert_name = C.staff_code);";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt4);
                dataGridView1.DataSource = dt4;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更前のコード";
                dataGridView1.Columns[3].HeaderText = "変更後のコード";
                dataGridView1.Columns[4].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "品名マスタ　品名変更"
            else if (comboBox1.Text == "品名マスタ　品名変更")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select B.change, C.staff_name, B.item_name_before, B.item_name_after, A.reason from item_m A " +
                                 "inner join item_m_item_name_revisions B ON (A.item_code = B.item_code) inner join staff_m C" +
                                 " ON (B.insert_name = C.staff_code);";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt4);
                dataGridView1.DataSource = dt4;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更前";
                dataGridView1.Columns[3].HeaderText = "変更後";
                dataGridView1.Columns[4].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "大分類　無効"
            else if (comboBox1.Text == "大分類マスタ　無効")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select B.invalid_date, C.staff_name, B.main_category_code, A.main_category_name, A.reason from main_category_m A " +
                    "inner join main_category_m_invalid_revisions B ON (A.main_category_code = B.main_category_code ) inner join staff_m C " +
                    "ON ( A.insert_name = C.staff_code) inner join invalid D ON (A.invalid = D.invalid) where A.invalid = 1;";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt5);
                dataGridView1.DataSource = dt5;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "無効にした大分類コード";
                dataGridView1.Columns[3].HeaderText = "無効にした大分類名";
                dataGridView1.Columns[4].HeaderText = "無効理由";
                conn.Close();
            }
            #endregion
            #region "大分類名変更した場合"
            else if (comboBox1.Text == "大分類マスタ　大分類名変更")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select B.change_date, C.staff_name, B.main_category_name_before, B.main_category_name_after, A.reason from main_category_m A " +
                                 "inner join main_category_m_name_revisions B ON (A.main_category_code = B.main_category_code) inner join staff_m C ON ( A.insert_name = C.staff_code);";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt5);
                dataGridView1.DataSource = dt5;
                dataGridView1.Columns[0].HeaderText = "変更日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更前";
                dataGridView1.Columns[3].HeaderText = "変更後";
                dataGridView1.Columns[4].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "消費税マスタ"
            else if (comboBox1.Text == "消費税マスタ")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select A.upd_date, B.staff_name, A.vat_rate from vat_m  A inner join staff_m  B ON (A.upd_name = B.staff_code);";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt6);
                dataGridView1.DataSource = dt6;
                dataGridView1.Columns[0].HeaderText = "更新日時";
                dataGridView1.Columns[1].HeaderText = "更新者";
                dataGridView1.Columns[2].HeaderText = "更新後の税率";
                conn.Close();
            }
            #endregion
            #region "計算書"
            else if (comboBox1.Text == "計算書")
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
            #region "納品書"
            else if (comboBox1.Text == "納品書")
            {
                this.button1.Visible = true;
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter13;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select A.registration_date, C.staff_name, A.control_number, A.reason from delivery_m  A " +
                                 " inner join staff_m C ON (A.staff_code = C.staff_code) " +
                                 " where A.reason is not Null;";
                conn.Open();

                adapter13 = new NpgsqlDataAdapter(sql_str, conn);
                adapter13.Fill(dt14);
                dataGridView1.DataSource = dt14;
                dataGridView1.Columns[0].HeaderText = "更新日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更があった管理番号";
                dataGridView1.Columns[3].HeaderText = "変更理由";
                conn.Close();

                control = (int)this.dataGridView1.CurrentRow.Cells[2].Value;
            }
            #endregion
            #region "成績入力"
            else if (comboBox1.Text == "成績一覧")
            {
                this.button1.Visible = false;
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter14;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select A.registration_date, B.staff_name, A.grade_number,C.item_name, D.item_name, A.reason from item_name_change_revisions  A " +
                                 " inner join staff_m B ON (A.staff_id = B.staff_code) inner join item_m C ON (A.before_item_code = C.item_code) inner join item_m D " +
                                 " ON (A.after_item_code = D.item_code);";
                conn.Open();

                adapter14 = new NpgsqlDataAdapter(sql_str, conn);
                adapter14.Fill(dt15);
                dataGridView1.DataSource = dt15;
                dataGridView1.Columns[0].HeaderText = "更新日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更があった成績番号";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();

                grade = (int)this.dataGridView1.CurrentRow.Cells[2].Value;
            }
            #endregion
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "計算書")
            {
                document = (string)this.dataGridView1.CurrentRow.Cells[2].Value;
            }
            else if (comboBox1.Text == "納品書")
            {
                control = (int)this.dataGridView1.CurrentRow.Cells[2].Value;
            }


            Invoice invoice = new Invoice(mainMenu, staff_id, document, control, grade);
            this.Close();
            invoice.Show();
        }
    }
}
