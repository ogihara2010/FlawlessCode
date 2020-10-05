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
        public string name1;
        public string phoneNumber1;
        public string address1;
        public string address;
        public string addresskana1;
        public string code1;
        public string item1;
        public string date1;
        public string date2;
        public string method1;
        public string amountA;
        public string amountB;
        public string antiqueNumber;
        public string staff_name;
        public int staff_id;
        public string data;
        decimal Total;
        public int control;
        public string document;
        public string access_auth;
        public string Pass;
        DataTable dt = new DataTable();
        //bool screan = true;
        public string documentNumber;
        public int grade;
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
        public DataSearchResults(MainMenu main, int type, int id, string name1, string phoneNumber1, string address1, string addresskana1, string code1, string item1, string date1, string date2, string method1, string amountA, string amountB, string data, string pass, string document, int control, string antiqueNumber, string documentNumber, string access_auth)
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
            this.Close();
        }

        private void DataSearchResults_Load(object sender, EventArgs e)
        {
            #region "計算書"
            if (data == "S")
            {                
                #region "法人"
                if (type == 0)
                {
                    NpgsqlConnection conn = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter;
                    PostgreSQL postgre = new PostgreSQL();
                    conn = postgre.connection();
                    //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.document_number, A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                            "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                            "where B.type = 0 " + name1 + address1 + addresskana1 + phoneNumber1 + documentNumber + antiqueNumber + code1 + item1 + " and ( A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "')" +
                              method1 + amountA + amountB + ";";
                    /*"select A.document_number, A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                     "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                     "where B.shop_name = '" + name1  + "'" + search1 + " B.address like '%"  + address1 + "%'" + search2 +  " B.address_kana = '" + addresskana1 + search3 + " B.phone_number = '" + phoneNumber1 + search4 + " A.document_number = '" + document + "' " + search5 + " B.antique_number = " + antique + " " + search6
                     + " D.main_category_code = " + code1  + search7 + " D.item_code = " + item1 +  search8 + "( A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "')" + search9 + " A.payment_method = '"+  method1 + "'" + search10 +  " (A.total >= " + amount1 + " and A.total <= " + amount2 + ");";*/

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
                    if (data == null)
                    {
                        document = (string)dataGridView1.CurrentRow.Cells[0].Value;
                        staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                        address = (string)dataGridView1.CurrentRow.Cells[5].Value;
                    }

                }
                #endregion
                #region "個人"
                if (type == 1)
                {
                    NpgsqlConnection conn = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter;
                    PostgreSQL postgre = new PostgreSQL();
                    conn = postgre.connection();
                    //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.document_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                            "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                            "where B.type = 1 " + name1 + address1 + addresskana1 + phoneNumber1 + documentNumber + code1 + item1 + " and (A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "') " + method1 + amountA + amountB + ";";

                    /*"select A.document_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                             "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                 "where B.name = '" + name1 + "'" + search3 + " B.address like '% " + address + "%'" + search4 + " B.address_kana = '" + addresskana1 + "'" + search5
                         + " B.phone_number = '" + phoneNumber1 + "'" + " " + search7 + " A.document_number = '" + document + "' " + search8 + " "
                         + " D.main_category_code = " + code1 + " " + search10 + " D.item_code = " + item1 + " " + search11 + " (A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "') " + search11
                         + " A.payment_method = '" + method1 + "'" + search12 + " (A.total >= " + amountA + " and A.total <= " + amountB + ");";*/
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
                #region "法人"
                if (type == 0)
                {
                    NpgsqlConnection conn = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter;
                    PostgreSQL postgre = new PostgreSQL();
                    conn = postgre.connection();
                    //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.control_number, A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount, A.antique_number from delivery_m A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                           "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                           "where B.type = 0 " + name1 + address1 + addresskana1 + phoneNumber1 + documentNumber + antiqueNumber + code1 + item1 + " and ( A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "')" +
                              method1 + amountA + amountB + ";";

                    /*"select A.control_number, A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount,  A.antique_number from delivery_m A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                                 "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                 "where B.shop_name = '" + name1 + "'" + search2 +  " B.address like '% " + address + "%'" + search3 + " B.address_kana = '" + addresskana1 + "'" + search4
                                 + " B.phone_number = '" + phoneNumber1 + "'" + search5 + " A.control_number = " + control + " " + search7 + " B.antique_number = " + antiqueNumber + " " + search8
                                 + " D.main_category_code = " + code1 + " " + search9 + " D.item_code = " + item1 + " " + search10 + "( A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "')" + search11
                                 + " A.payment_method = '" + method1 + "'" + search12 + "( A.total >= " + amountA + " and A.total <= " + amountB + ");";*/

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
                    dataGridView1.Columns[9].HeaderText = "古物番号";
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
                    PostgreSQL postgre = new PostgreSQL();
                    conn = postgre.connection();
                    //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.control_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from delivery_m A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                            "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code) " +
                            "where B.type = 1 " + name1 + address1 + addresskana1 + phoneNumber1 + documentNumber + code1 + item1 + " and (A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "') "
                             + method1 + amountA + amountB + ";";
                    /* "select A.control_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount, A.id_number from delivery_m A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                              "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                  "where B.name = '" + name1 + "'" + search1  + " B.address like '% " + address + "%'" + search3 + " B.address_kana = '" + addresskana1 + "'" + search4
                         + " B.phone_number = '" + phoneNumber1 + "'" + search5 + " A.control_number = " + control + " "  + search8
                         + " D.main_category_code = " + code1 + " " + search9 + " D.item_code = " + item1 + " " + search10 + "( A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "')" + search11
                         + " A.payment_method = '" + method1 + "'" + search12 + "( A.total >= " + amountA + " and A.total <= " + amountB + ");";*/
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
            if (type == 0)
            {
                type = 0;
                document = (string)dataGridView1.CurrentRow.Cells[0].Value;
                staff_name = (string)dataGridView1.CurrentRow.Cells[4].Value;
                address = (string)dataGridView1.CurrentRow.Cells[6].Value;
            }
            if (type == 1)
            {
                type = 1;
                document = (string)dataGridView1.CurrentRow.Cells[0].Value;
                staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                address = (string)dataGridView1.CurrentRow.Cells[5].Value;
            }
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, Pass, document, control, data, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
            //screan = false;
            //this.Close();
            this.data = statement.data;
            statement.ShowDialog();
        }
        #endregion
        #region "詳細表示"
        private void Button1_Click(object sender, EventArgs e)
        {
            if (data == "S")
            {
                if (type == 0)
                {
                    type = 0;
                    document = (string)dataGridView1.CurrentRow.Cells[0].Value;
                    staff_name = (string)dataGridView1.CurrentRow.Cells[4].Value;
                    address = (string)dataGridView1.CurrentRow.Cells[6].Value;
                }
                if (type == 1)
                {
                    type = 1;
                    document = (string)dataGridView1.CurrentRow.Cells[0].Value;
                    staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                    address = (string)dataGridView1.CurrentRow.Cells[5].Value;
                }
                Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, Pass, document, control, data, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
                //screan = false;
                //this.Close();
                this.data = statement.data;
                this.document = statement.document;
                statement.ShowDialog();
            }
            else if (data == "D")
            {
                control = (int)dataGridView1.CurrentRow.Cells[0].Value;
                staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                address = (string)dataGridView1.CurrentRow.Cells[5].Value;
                Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, Pass, document, control, data, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
                //screan = false;
                //this.Close();
                this.data = statement.data;
                statement.ShowDialog();
            }            
        }
        #endregion

        private void DataSearchResults_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
