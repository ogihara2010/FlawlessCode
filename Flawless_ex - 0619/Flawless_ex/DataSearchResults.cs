using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace Flawless_ex
{
    public partial class DataSearchResults : Form
    {
        CustomerHistory customerHistory;
        MainMenu mainMenu;
        int type;
        string name1;
        string phoneNumber1;
        string address1;
        string address;
        string item1;
        string search1;
        string search2;
        string search3;

        string staff_name;
        int staff_id;
        string data;
        decimal Total;
        int control;
        string document;
        string access_auth;
        string Pass;
        int grade;

        DataTable dt = new DataTable();
        public DataSearchResults(MainMenu main, int type, int id, string name1, string phoneNumber1, string address1, string item1, string search1, string search2, string search3, string data, string pass, string Access_auth)
        {
            InitializeComponent();
            mainMenu = main;
            this.type = type;
            this.name1 = name1;
            this.phoneNumber1 = phoneNumber1;
            this.address1 = address1;
            this.item1 = item1;
            this.search1 = search1;
            this.search2 = search2;
            this.search3 = search3;
            staff_id = id;
            this.data = data;
            this.Pass = pass;
            this.access_auth = Access_auth;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻るボタン
        {
            this.Close();
        }

        private void DataSearchResults_Load(object sender, EventArgs e)
        {
            #region "計算書"
            if (data == "S")
            {
                this.button1.Enabled = false;
                #region "法人"
                if (type == 0)
                {
                    NpgsqlConnection conn = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter;
                    conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.document_number, A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                                      "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                      " where B.invalid = 0 and B.shop_name = '" + name1 + "' " + search1 + " B.phone_number = '" + phoneNumber1 + "'" + " " + search2 + " B.address like '% " + address1 + " %' " + search3 + " D.item_name = '" + item1 + "';";
                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "伝票番号";
                    dataGridView1.Columns[1].HeaderText = "決済日";
                    dataGridView1.Columns[2].HeaderText = "受渡日";
                    dataGridView1.Columns[3].HeaderText = "店舗名";
                    dataGridView1.Columns[4].HeaderText = "担当者名・個人名";
                    dataGridView1.Columns[5].HeaderText = "電話番号";
                    dataGridView1.Columns[6].HeaderText = "住所";
                    dataGridView1.Columns[7].HeaderText = "品名";
                    dataGridView1.Columns[8].HeaderText = "金額";

                    conn.Close();
                    document = (string)dataGridView1.CurrentRow.Cells[0].Value;
                    staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                    address = (string)dataGridView1.CurrentRow.Cells[5].Value;
                }
                #endregion
                #region "個人"
                if (type == 1)
                {
                    NpgsqlConnection conn = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter;
                    conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.document_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                                 "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                     "where B.invalid = 0 and B.name = '" + name1 + "' " + search1 + " B.phone_number = '" + phoneNumber1 + "'" + " " + search2 + " B.address like '% " + address1 + " %' " + search3 + " D.item_name = '" + item1 + "';";
                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "伝票番号";
                    dataGridView1.Columns[1].HeaderText = "決済日";
                    dataGridView1.Columns[2].HeaderText = "受渡日";
                    dataGridView1.Columns[3].HeaderText = "氏名";
                    dataGridView1.Columns[4].HeaderText = "電話番号";
                    dataGridView1.Columns[5].HeaderText = "住所";
                    dataGridView1.Columns[6].HeaderText = "品名";
                    dataGridView1.Columns[7].HeaderText = "金額";

                    conn.Close();
                    document = (string)dataGridView1.CurrentRow.Cells[0].Value;
                    staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                    address = (string)dataGridView1.CurrentRow.Cells[5].Value;
                }
                #endregion
            }
            #endregion
            #region "納品書"
            if (data == "D")
            {
                this.button2.Enabled = false;
                #region "法人"
                if (type == 0)
                {
                    NpgsqlConnection conn = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter;
                    conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.control_number, A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount,  A.antique_number from delivery_m A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                                     "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                     " where B.invalid = 0 and B.shop_name = '" + name1 + "' " + search1 + " B.phone_number = '" + phoneNumber1 + "'" + " " + search2 + " B.address like '% " + address1 + " %' " + search3 + " D.item_name = '" + item1 + "';";
                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "管理番号";
                    dataGridView1.Columns[1].HeaderText = "決済日";
                    dataGridView1.Columns[2].HeaderText = "受渡日";
                    dataGridView1.Columns[3].HeaderText = "店舗名";
                    dataGridView1.Columns[4].HeaderText = "担当者名・個人名";
                    dataGridView1.Columns[5].HeaderText = "電話番号";
                    dataGridView1.Columns[6].HeaderText = "住所";
                    dataGridView1.Columns[7].HeaderText = "品名";
                    dataGridView1.Columns[8].HeaderText = "金額";
                    conn.Close();
                    
                    control = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                    address = (string)dataGridView1.CurrentRow.Cells[5].Value;
                }
                #endregion
                #region "個人"
                if (type == 1)
                {
                    NpgsqlConnection conn = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter;
                    conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.control_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount, A.id_number from delivery_m A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                                 "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                     "where B.invalid = 0 and B.name = '" + name1 + "' " + search1 + " B.phone_number = '" + phoneNumber1 + "'" + " " + search2 + " B.address like '% " + address1 + " %' " + search3 + " D.item_name = '" + item1 + "';";
                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "管理番号";
                    dataGridView1.Columns[1].HeaderText = "決済日";
                    dataGridView1.Columns[2].HeaderText = "受渡日";
                    dataGridView1.Columns[3].HeaderText = "氏名";
                    dataGridView1.Columns[4].HeaderText = "電話番号";
                    dataGridView1.Columns[5].HeaderText = "住所";
                    dataGridView1.Columns[6].HeaderText = "品名";
                    dataGridView1.Columns[7].HeaderText = "金額";
                    conn.Close();
                    control = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                    address = (string)dataGridView1.CurrentRow.Cells[5].Value;
                }
                #endregion
            }
            #endregion

        }
        #region "計算書"
        private void Button2_Click(object sender, EventArgs e)
        {
            document = (string)dataGridView1.CurrentRow.Cells[0].Value;
            staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
            address = (string)dataGridView1.CurrentRow.Cells[5].Value;
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, Pass, document, control, data, search1, search2, search3, grade);
            this.Close();
            statement.Show();
        }
        #endregion
        #region "納品書"
        private void Button1_Click(object sender, EventArgs e)
        {
            control = (int)dataGridView1.CurrentRow.Cells[0].Value;
            staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
            address = (string)dataGridView1.CurrentRow.Cells[5].Value;
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, Pass, document, control, data, search1, search2, search3, grade);
            this.Close();
            statement.Show();
        }
        #endregion

        private void DataSearchResults_FormClosed(object sender, FormClosedEventArgs e)
        {
            CustomerHistory customerHistory = new CustomerHistory(mainMenu, staff_id, data, Pass, access_auth);
            customerHistory.Show();
        }
    }
}
