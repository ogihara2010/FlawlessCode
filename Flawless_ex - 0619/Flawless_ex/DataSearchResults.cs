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
        string addresskana1;
        string code1;
        string item1;
        string date1;
        string date2;
        string method1;
        string amountA;
        string amountB;
        string antiqueNumber;
        #region "買取販売履歴"
        string search1;
        string search2;
        string search3;
        string search4;
        string search5;
        string search6;
        string search7;
        string search8;
        string search9;
        string search10;
        string search11;
        string search12;
        #endregion
        string staff_name;
        int staff_id;
        string data;
        decimal Total;
        int control;
        string document;
        string access_auth;
        string Pass;
        DataTable dt = new DataTable();
        bool screan = true;
        string documentNumber;
        #region "納品書"
        decimal amount00;
        decimal amount01;
        decimal amount02;
        decimal amount03;
        decimal amount04;
        decimal amount05;
        decimal amount06;
        decimal amount07;
        decimal amount08;
        decimal amount09;
        decimal amount010;
        decimal amount011;
        decimal amount012;
        #endregion
        #region "計算書"
        decimal amount10;
        decimal amount11;
        decimal amount12;
        decimal amount13;
        decimal amount14;
        decimal amount15;
        decimal amount16;
        decimal amount17;
        decimal amount18;
        decimal amount19;
        decimal amount110;
        decimal amount111;
        decimal amount112;
        #endregion
        public DataSearchResults(MainMenu main, int type, int id, string name1, string phoneNumber1, string address1, string addresskana1, string code1, string item1, string date1, string date2, string method1, string amountA, string amountB, string search1, string search2, string search3, string search4, string search5, string search6, string search7, string search8, string search9, string search10, string search11, string search12, string data, string pass, string document, int control, string antiqueNumber, string documentNumber, string access_auth)
        {
            InitializeComponent();
            mainMenu = main;
            this.type = type;
            this.name1 = name1;
            this.phoneNumber1 = phoneNumber1;
            this.address1 = address1;
            this.addresskana1 = addresskana1;
            this.code1 = code1;
            this.item1 = item1;
            this.date1 = date1;
            this.date2 = date2;
            this.method1 = method1;
            this.amountA = amountA;
            this.amountB = amountB;
            this.antiqueNumber = antiqueNumber;
            #region "買取販売履歴"
            this.search1 = search1;
            this.search2 = search2;
            this.search3 = search3;
            this.search4 = search4;
            this.search5 = search5;
            this.search6 = search6;
            this.search7 = search7;
            this.search8 = search8;
            this.search9 = search9;
            this.search10 = search10;
            this.search11 = search11;
            this.search12 = search12;
            #endregion
            this.documentNumber = documentNumber;
            staff_id = id;
            this.data = data;
            this.Pass = pass;
            this.document = document;
            this.control = control;
            this.access_auth = access_auth;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻るボタン
        {
            CustomerHistory customerHistory = new CustomerHistory(mainMenu, staff_id, data, Pass);
            screan = false;
            this.Close();
            customerHistory.Show();
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
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, Pass, document, control, data, search1, search2, search3);
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
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, Pass, document, control, data, search1, search2, search3);
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
