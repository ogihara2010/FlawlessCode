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
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt7 = new DataTable();
        int type;
        int number;
        int a = 0; // クリック数 
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        NpgsqlDataAdapter adapter2;
        NpgsqlDataAdapter adapter3;
        public string data;
        string Pass;
        bool screan = true;
        string document;
        int control;
        string antiqueNumber;
        string access_auth;
        public CustomerHistory(MainMenu main, int id, string data, string pass, string access_auth)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            this.data = data;
            this.Pass = pass;
            this.access_auth = access_auth;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            CustomerHistorySelect customerHistorySelect = new CustomerHistorySelect(mainMenu, staff_id, data, Pass, access_auth);
            screan = false;
            this.Close();
            customerHistorySelect.Show();
        }

        private void CustomerHistory_Load(object sender, EventArgs e)
        {
            if (data == "S")
            {
                this.textBox8.Visible = false;
                this.label9.Visible = false;
            }
            else if (data == "D")
            {
                this.textBox7.Visible = false;
                this.label8.Visible = false;
            }

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            conn.Open();

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
            comboBox1.SelectedIndex = -1;

            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "item_name";
            comboBox2.ValueMember = "item_code";
            comboBox2.SelectedIndex = -1;
        }

        private void dataSelectButton_Click(object sender, EventArgs e)//検索ボタン
        {
            #region "パラメータ"
            string shopname;
            string shopnamekana;
            string name;
            string address;
            string addresskana;
            string phoneNumber;
            string documentNumber;
            string controlNumber;
            string antiqueNumber;
            string mainCategory;
            string code;
            string item;
            string itemcode;
            
            string date1 = dateTimePicker1.Text;            //引数用
            string date2 = settlementBox.Text;              //引数用

            DateTime Date1 = DateTime.Parse(dateTimePicker1.Value.ToShortDateString());                                                     //検索用
            DateTime Date2 = DateTime.Parse(settlementBox.Value.ToShortDateString()).AddHours(23).AddMinutes(59).AddSeconds(59);             //検索用

            string method;
            int amount1;
            int amount2;
            string amountA;
            string amountB;
            int ant;
            int amt;
            int amt1;
            #endregion
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            NpgsqlConnection conn2 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter2;
            NpgsqlConnection conn3 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter3;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            conn.Open();

            //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            #region "検索条件 法人"
            if (radioButton1.Checked == true)
            {
                type = 0;
                #region "入力パラメーター"
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    shopname = " and B.shop_name like '%" + this.textBox1.Text + "%'";
                }
                else
                {
                    shopname = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    shopnamekana = " and B.shop_name_kana like '%" + this.textBox2.Text + "%'";
                }
                else
                {
                    shopnamekana = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    name = "and B.staff_name like '%" + this.textBox3.Text + "%'";
                }
                else
                {
                    name = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    address = " and B.address like '% " + this.textBox4.Text + "%'";
                }
                else
                {
                    address = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    addresskana = " and B.address_kana like '%" + this.textBox5.Text + "%'";
                }
                else
                {
                    addresskana = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    phoneNumber = " and B.phone_number = '" + this.textBox6.Text + "'";
                }
                else
                {
                    phoneNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    documentNumber = " and A.document_number = '" + this.textBox7.Text + "' ";
                }
                else
                {
                    documentNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    controlNumber = " and A.control_number = " + int.Parse(this.textBox8.Text) + " ";
                }
                else
                {
                    controlNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    antiqueNumber = " and B.antique_number = " + int.Parse(this.textBox9.Text) + " ";
                }
                else
                {
                    antiqueNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                {
                    mainCategory = " and E.main_category_name = '" + this.comboBox1.Text + "'";
                }
                else
                {
                    mainCategory = "";
                }
                if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    item = " and D.item_name = '" + this.comboBox2.Text + "'";
                }
                else
                {
                    item = "";
                }
                if (!string.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    method = " and A.payment_method = '" + this.comboBox3.Text + "'";
                }
                else
                {
                    method = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amountA = " and A.total >= " + int.Parse(this.textBox10.Text);
                }
                else
                {
                    amountA = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amountB = " and A.total <= " + int.Parse(this.textBox11.Text);
                }
                else
                {
                    amountB = "";
                }
                if (string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    ant = 0;
                }
                else
                {
                    ant = int.Parse(textBox9.Text);
                }
                if (string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amt = 0;
                }
                else
                {
                    amt = int.Parse(textBox11.Text);
                }
                if (string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amt1 = 0;
                }
                else
                {
                    amt1 = int.Parse(textBox11.Text);
                }
                if (string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    control = 0;
                }
                else
                {
                    control = int.Parse(textBox8.Text);
                }
                #endregion
                #region "繋げるSQL"
                #region "大分類名をコードに変換"
                if (comboBox1.SelectedIndex != -1)
                {
                    string sql2 = "select * from main_category_m where main_category_name = '" + this.comboBox1.Text + "';";
                    conn2.Open();
                    adapter2 = new NpgsqlDataAdapter(sql2, conn2);
                    adapter2.Fill(dt5);
                    DataRow row;
                    row = dt5.Rows[0];
                    code = row["main_category_code"].ToString();
                }
                else
                {
                    code = "1";
                }

                #endregion
                #region "品名をコードに変換"
                if (comboBox2.SelectedIndex != -1)
                {
                    string sql3 = "select * from item_m where item_name = '" + this.comboBox2.Text + "';";
                    conn.Open();
                    adapter3 = new NpgsqlDataAdapter(sql3, conn);
                    adapter3.Fill(dt6);
                    DataRow row2;
                    row2 = dt6.Rows[0];
                    itemcode = row2["item_code"].ToString();
                }
                else
                {
                    itemcode = "1";
                }

                #endregion

                if (data == "S")
                {
                    string sql = "select A.document_number, A.settlement_date, A.delivery_date, B.shop_name, B.name, B.phone_number, B.address, D.item_name, C.amount, E.main_category_name from statement_data A inner join client_m B ON (A.code = B.code )" +
                            "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                            "where B.type = 0 " + shopname + shopnamekana + name + address + addresskana + phoneNumber + documentNumber + antiqueNumber + mainCategory + item + " and ( A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "')" +
                              method + amountA + amountB + " ;";

                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);

                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }

                    string name1 = shopname;
                    string phoneNumber1 = phoneNumber;
                    string address1 = address;
                    string addresskana1 = addresskana;
                    string item1 = item;
                    string code1 = mainCategory;
                    string method1 = method;
                    int antique = ant;
                    document = this.textBox7.Text;
                    amount2 = amt;
                    amount1 = amt1;

                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, data, Pass, document, control, antiqueNumber, documentNumber, access_auth);
                    this.data = dataSearch.data;
                    dataSearch.ShowDialog();
                }
                else if (data == "D")
                {
                    string sql = "select A.control_number, A.settlement_date, A.delivery_date, B.shop_name, B.name, B.phone_number, B.address, D.item_name, C.amount from delivery_m A inner join client_m B ON (A.code = B.code )" +
                           "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                           "where A.types1 = 0 " + shopname + shopnamekana + name + address + addresskana + phoneNumber + controlNumber + antiqueNumber + mainCategory + item + " and ( A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "') " +
                              method + amountA + amountB + " order by control_number;";
                    
                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);

                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }
                    string name1 = shopname;
                    string phoneNumber1 = phoneNumber;
                    string address1 = address;
                    string addresskana1 = addresskana;
                    string item1 = item;
                    string code1 = mainCategory;
                    //control = int.Parse(this.textBox8.Text);
                    string method1 = method;
                    documentNumber = controlNumber;
                    int antique = ant;
                    amount2 = amt;
                    amount1 = amt1;

                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, data, Pass, document, control, antiqueNumber, documentNumber, access_auth);
                    this.data = dataSearch.data;
                    dataSearch.ShowDialog();
                }
                #endregion
            }
            #endregion
            #region "検索条件　個人"
            if (radioButton2.Checked == true)
            {
                type = 1;
                #region "パラメーター"
                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    name = " and B.name like '%" + this.textBox3.Text + "%'";
                }
                else
                {
                    name = "";
                }

                if (!string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    address = " and B.address like '% " + this.textBox4.Text + "%'";
                }
                else
                {
                    address = "";
                }

                if (!string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    addresskana = " and B.address_kana like '%" + this.textBox5.Text + "%'";
                }
                else
                {
                    addresskana = "";
                }

                if (!string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    phoneNumber = " and B.phone_number = '" + this.textBox6.Text + "'";
                }
                else
                {
                    phoneNumber = "";
                }

                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    documentNumber = " and A.document_number = '" + this.textBox7.Text + "'";
                }
                else
                {
                    documentNumber = "";
                }

                if (!string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    controlNumber = " and A.control_number = " + int.Parse(this.textBox8.Text) + " ";
                }
                else
                {
                    controlNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                {
                    mainCategory = " and E.main_category_name = '" + this.comboBox1.Text + "' ";
                }
                else
                {
                    mainCategory = "";
                }

                if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    item = " and D.item_name = '" + this.comboBox2.Text + "'";
                }
                else
                {
                    item = "";
                }
                if (!string.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    method = " and A.payment_method = '" + this.comboBox3.Text + "'";
                }
                else
                {
                    method = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amountA = " and (A.total >= " + int.Parse(this.textBox10.Text);
                }
                else
                {
                    amountA = " ";
                }
                if (!string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amountB = " and A.total <= " + int.Parse(this.textBox11.Text);
                }
                else
                {
                    amountB = "";
                }
                if (string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    ant = 0;
                }
                else
                {
                    ant = int.Parse(textBox9.Text);
                }
                if (string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amt = 0;
                }
                else
                {
                    amt = int.Parse(textBox11.Text);
                }
                if (string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amt1 = 0;
                }
                else
                {
                    amt1 = int.Parse(textBox10.Text);
                }
                #endregion
                #region "繋げるSQL"
                #region "大分類名をコードに変換"
                if (comboBox1.SelectedIndex != -1)
                {
                    string sql2 = "select * from main_category_m where main_category_name = '" + this.comboBox1.Text + "';";
                    conn2.Open();
                    adapter2 = new NpgsqlDataAdapter(sql2, conn2);
                    adapter2.Fill(dt5);
                    DataRow row;
                    row = dt5.Rows[0];
                    code = row["main_category_code"].ToString();
                }
                else
                {
                    code = "100";
                }
                #endregion
                #region "品名をコードに変換"
                if (comboBox2.SelectedIndex != -1)
                {
                    string sql3 = "select * from item_m where item_name = '" + this.comboBox2.Text + "';";
                    conn.Open();
                    adapter3 = new NpgsqlDataAdapter(sql3, conn);
                    adapter3.Fill(dt6);
                    DataRow row2;
                    row2 = dt6.Rows[0];
                    itemcode = row2["item_code"].ToString();
                }
                else
                {
                    itemcode = "1000";
                }
                #endregion

                if (data == "S")
                {
                    string sql = "select A.document_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m B ON ( A.code = B.code )" +
                            "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                            "where A.type = 1 " + name + address + addresskana + phoneNumber + documentNumber + mainCategory + item + " and (A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "') " + method + amountA + amountB + " order by notfnumber;";

                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);

                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }

                    string name1 = name;
                    string phoneNumber1 = phoneNumber;
                    string address1 = address;
                    string addresskana1 = addresskana;
                    string item1 = item;
                    string code1 = mainCategory;
                    string method1 = method;
                    antiqueNumber = null;
                    amount2 = amt;
                    amount1 = amt1;

                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, data, Pass, document, control, antiqueNumber, documentNumber, access_auth);
                    this.data = dataSearch.data;
                    dataSearch.ShowDialog();
                }
                if (data == "D")
                {
                    string sql = "select A.control_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from delivery_m A inner join client_m B ON ( A.code = B.code )" +
                            "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code) " +
                            "where A.types1 = 0 " + name + address + addresskana + phoneNumber + controlNumber + mainCategory + item + " and (A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "')  "
                             + method + amountA + amountB + "order by control_number;";

                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);

                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }

                    string name1 = name;
                    string phoneNumber1 = phoneNumber;
                    string address1 = address;
                    string addresskana1 = addresskana;
                    string item1 = item;
                    string code1 = mainCategory;
                    string method1 = method;
                    antiqueNumber = null;
                    documentNumber = controlNumber;
                    amount2 = amt;
                    amount1 = amt1;

                    //control = int.Parse(this.textBox8.Text);
                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, data, Pass, document, control, antiqueNumber, documentNumber, access_auth);
                    this.data = dataSearch.data;
                    dataSearch.ShowDialog();
                }
                #endregion
            }
            #endregion
            conn.Close();
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "氏名";
            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "担当者名";
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }

        private void CustomerHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                CustomerHistorySelect customerHistorySelect = new CustomerHistorySelect(mainMenu, staff_id, data, Pass, access_auth);
                customerHistorySelect.Show();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            conn.Open();
        }
    }
}
