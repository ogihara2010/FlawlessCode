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
        int itemMainCategoryCode;
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        NpgsqlConnection conn2 = new NpgsqlConnection();
        NpgsqlDataAdapter adapter2;
        NpgsqlConnection conn3 = new NpgsqlConnection();
        NpgsqlDataAdapter adapter3;
        string data;
        string Pass;
        string Access_auth;

        public CustomerHistory(MainMenu main, int id, string data, string pass, string access_auth)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            this.data = data;
            this.Pass = pass;
            this.Access_auth = access_auth;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            CustomerHistorySelect customerHistorySelect = new CustomerHistorySelect(mainMenu, staff_id, data, Pass, Access_auth);
            this.Close();
            customerHistorySelect.Show();
        }

        private void CustomerHistory_Load(object sender, EventArgs e)
        {
            if (data == "S")
            {
                this.textBox8.Enabled = false;
                this.groupBox9.Enabled = false;
            }
            else if (data == "D")
            {
                this.textBox7.Enabled = false;
                this.groupBox8.Enabled = false;
            }
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
            #region "パラメータ"
            string shopname;
            string shopnamekana;
            string name;
            string address;
            string addresskana;
            string phoneNumber;
            string documentNumber;
            int controlNumber;
            int antiqueNumber;
            string mainCategory;
            string item;
            string date1 = this.dateTimePicker1.Text;
            string date2 = this.settlementBox.Text;
            string method;
            int amount1;
            int amount2;
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
            #endregion

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            NpgsqlConnection conn2 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter2;
            NpgsqlConnection conn3 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter3;

            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn2.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn3.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            #region "検索条件 法人"
            if (radioButton1.Checked == true)
            {
                type = 0;
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    shopname = this.textBox1.Text;
                }
                else
                {
                    shopname = "";
                }
                if (radioButton3.Checked == true)
                {
                    search1 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    shopnamekana = this.textBox2.Text;
                }
                else
                {
                    shopnamekana = "";
                }
                if (radioButton6.Checked == true)
                {
                    search2 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    name = this.textBox3.Text;
                }
                else
                {
                    name = "";
                }
                if (radioButton31.Checked == true)
                {
                    search3 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    address = this.textBox4.Text;
                }
                else
                {
                    address = "";
                }
                if (radioButton33.Checked == true)
                {
                    search4 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    addresskana = this.textBox4.Text;
                }
                else
                {
                    addresskana = "";
                }
                if (radioButton35.Checked == true)
                {
                    search5 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    phoneNumber = this.textBox6.Text;
                }
                else
                {
                    phoneNumber = "";
                }
                if (radioButton37.Checked == true)
                {
                    search6 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    documentNumber = this.textBox7.Text;
                }
                else
                {
                    documentNumber = "''";
                }
                if (radioButton39.Checked == true)
                {
                    search7 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    controlNumber = int.Parse(this.textBox8.Text);
                }
                else
                {
                    controlNumber = 0;
                }
                if (radioButton41.Checked == true)
                {
                    search8 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    antiqueNumber = int.Parse(this.textBox9.Text);
                }
                else
                {
                    antiqueNumber = 0;
                }
                if (radioButton43.Checked == true)
                {
                    search9 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                {
                    mainCategory = this.comboBox1.Text;
                }
                else
                {
                    mainCategory = "";
                }
                if (radioButton45.Checked == true)
                {
                    search10 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    item = this.comboBox2.Text;
                }
                else
                {
                    item = "";
                }
                if (radioButton47.Checked == true)
                {
                    search11 = "and";

                }
                else { }

                if (radioButton49.Checked == true)
                {
                    search12 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    method = this.comboBox3.Text;
                }
                else
                {
                    method = "";
                }
                if (radioButton51.Checked == true)
                {
                    search13 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amount1 = int.Parse(this.textBox10.Text);
                }
                else
                {
                    amount1 = 0;
                }
                if (!string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amount2 = int.Parse(this.textBox11.Text);
                }
                else
                {
                    amount2 = 999999999;
                }
                if (radioButton53.Checked == true)
                {
                    search14 = "and";

                }
                else { }
                #endregion
                #region "繋げるSQL"
                #region "大分類名をコードに変換"
                string sql2 = "select * from main_category_m where main_category_name = '" + mainCategory + "';";
                conn2.Open();
                adapter2 = new NpgsqlDataAdapter(sql2, conn2);
                adapter2.Fill(dt5);
                DataRow row;
                row = dt5.Rows[0];
                string code = row["main_category_code"].ToString();
                #endregion
                #region "品名をコードに変換"
                string sql3 = "select * from item_m where item_name = '" + item + "';";
                conn3.Open();
                adapter3 = new NpgsqlDataAdapter(sql3, conn3);
                adapter3.Fill(dt6);
                DataRow row2;
                row2 = dt6.Rows[0];
                string itemcode = row2["item_code"].ToString();
                #endregion

                if (data == "S")
                {
                    string sql = "select A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                            "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                            "where B.shop_name = '" + shopname + "'" + search1 + " B.shop_name_kana = '" + shopnamekana + "'" + search2 + " B.address like '% " + address + "%'" + search3 + " B.address_kana = '" + addresskana + "'" + search4
                             + " B.phone_number = '" + phoneNumber + "'" + search5 + " A.document_number = '" + documentNumber + "' " + search7 + " B.antique_number = " + antiqueNumber + " " + search8
                             + " D.main_category_code = " + code + " " + search9 + " D.item_code = " + itemcode + " " + search10 + "( A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "')" + search11
                             + " A.payment_method = '" + method + "'" + search12 + "( A.total >= " + amount1 + " and A.total <= " + amount2 + ");";
                    conn.Open();
                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);
                    #endregion
                    DataRow row3;
                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }
                    row3 = dt7.Rows[0];
                    string name1 = row3["shop_name"].ToString();
                    string phoneNumber1 = row3["phone_number"].ToString();
                    string address1 = row3["address"].ToString();
                    string item1 = row3["item_name"].ToString();


                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, item1, search1, search3, search5, data, Pass, Access_auth);
                    this.Hide();
                    dataSearch.Show();
                }
                else if (data == "D")
                {
                    string sql = "select A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount from delivery_m A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                           "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                           "where B.shop_name = '" + shopname + "'" + search1 + " B.shop_name_kana = '" + shopnamekana + "'" + search2 + " B.address like '% " + address + "%'" + search3 + " B.address_kana = '" + addresskana + "'" + search4
                            + " B.phone_number = '" + phoneNumber + "'" + search5 + " A.control_number = " + controlNumber + " " + search7 + " B.antique_number = " + antiqueNumber + " " + search8
                            + " D.main_category_code = " + code + " " + search9 + " D.item_code = " + itemcode + " " + search10 + "( A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "')" + search11
                            + " A.payment_method = '" + method + "'" + search12 + "( A.total >= " + amount1 + " and A.total <= " + amount2 + ");";
                    conn.Open();
                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);

                    DataRow row3;
                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }
                    row3 = dt7.Rows[0];
                    string name1 = row3["shop_name"].ToString();
                    string phoneNumber1 = row3["phone_number"].ToString();
                    string address1 = row3["address"].ToString();
                    string item1 = row3["item_name"].ToString();


                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, item1, search1, search3, search5, data, Pass, Access_auth);
                    this.Hide();
                    dataSearch.Show();
                }
            }
            #region "検索条件　個人"
            if (radioButton2.Checked == true)
            {
                type = 1;
                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    name = this.textBox3.Text;
                }
                else
                {
                    name = "";
                }
                if (radioButton31.Checked == true)
                {
                    search3 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    address = this.textBox4.Text;
                }
                else
                {
                    address = "";
                }
                if (radioButton33.Checked == true)
                {
                    search4 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    addresskana = this.textBox4.Text;
                }
                else
                {
                    addresskana = "";
                }
                if (radioButton35.Checked == true)
                {
                    search5 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    phoneNumber = this.textBox6.Text;
                }
                else
                {
                    phoneNumber = "";
                }
                if (radioButton37.Checked == true)
                {
                    search6 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    documentNumber = this.textBox7.Text;
                }
                else
                {
                    documentNumber = "''";
                }
                if (radioButton39.Checked == true)
                {
                    search7 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    controlNumber = int.Parse(this.textBox8.Text);
                }
                else
                {
                    controlNumber = 0;
                }
                if (radioButton41.Checked == true)
                {
                    search8 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    antiqueNumber = int.Parse(this.textBox9.Text);
                }
                else
                {
                    antiqueNumber = 0;
                }
                if (radioButton43.Checked == true)
                {
                    search9 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                {
                    mainCategory = this.comboBox1.Text;
                }
                else
                {
                    mainCategory = "";
                }
                if (radioButton45.Checked == true)
                {
                    search10 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    item = this.comboBox2.Text;
                }
                else
                {
                    item = "";
                }
                if (radioButton47.Checked == true)
                {
                    search11 = "and";

                }
                else { }

                if (radioButton49.Checked == true)
                {
                    search12 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    method = this.comboBox3.Text;
                }
                else
                {
                    method = "";
                }
                if (radioButton51.Checked == true)
                {
                    search13 = "and";

                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amount1 = int.Parse(this.textBox10.Text);
                }
                else
                {
                    amount1 = 0;
                }
                if (!string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amount2 = int.Parse(this.textBox11.Text);
                }
                else
                {
                    amount2 = 999999999;
                }
                if (radioButton53.Checked == true)
                {
                    search14 = "and";

                }
                else { }
                #endregion
                #region "繋げるSQL"
                #region "大分類名をコードに変換"
                string sql2 = "select * from main_category_m where main_category_name = '" + mainCategory + "';";
                conn2.Open();
                adapter2 = new NpgsqlDataAdapter(sql2, conn2);
                adapter2.Fill(dt5);
                DataRow row;
                row = dt5.Rows[0];
                string code = row["main_category_code"].ToString();
                #endregion
                #region "品名をコードに変換"
                string sql3 = "select * from item_m where item_name = '" + item + "';";
                conn3.Open();
                adapter3 = new NpgsqlDataAdapter(sql3, conn3);
                adapter3.Fill(dt6);
                DataRow row2;
                row2 = dt6.Rows[0];
                string itemcode = row2["item_code"].ToString();
                #endregion

                if (data == "S")
                {
                    string sql = "select A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                            "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                            "where B.name = '" + name + "'" + search3 + " B.address like '% " + address + "%'" + search4 + " B.address_kana = '" + addresskana + "'" + search5
                             + " B.phone_number = '" + phoneNumber + "'" + " " + search7 + " A.document_number = " + documentNumber + " " + search8 + " "
                             + " D.main_category_code = " + code + " " + search10 + " D.item_code = " + itemcode + " " + search11 + " (A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "') " + search11
                             + " A.payment_method = '" + method + "'" + search12 + " (A.total >= " + amount1 + " and A.total <= " + amount2 + ");";

                    conn.Open();
                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);
                    #endregion

                    DataRow row3;
                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }
                    row3 = dt7.Rows[0];
                    name = row3["name"].ToString();
                    phoneNumber = row3["phone_number"].ToString();
                    address = row3["address"].ToString();
                    item = row3["item_name"].ToString();

                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name, phoneNumber, address, item, search3, search4, search5, data, Pass, Access_auth);
                    this.Hide();
                    dataSearch.Show();
                }
                if (data == "D")
                {
                    string sql = "select A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from delivery_m A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                            "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                            "where B.name = '" + name + "'" + search3 + " B.address like '% " + address + "%'" + search4 + " B.address_kana = '" + addresskana + "'" + search5
                             + " B.phone_number = '" + phoneNumber + "'" + " " + search7 + " A.control_number = " + controlNumber + " " + search8 + " "
                             + " D.main_category_code = " + code + " " + search10 + " D.item_code = " + itemcode + " " + search11 + " (A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "') " + search11
                             + " A.payment_method = '" + method + "'" + search12 + " (A.total >= " + amount1 + " and A.total <= " + amount2 + ");";

                    conn.Open();
                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);
                    DataRow row3;
                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }
                    row3 = dt7.Rows[0];
                    name = row3["name"].ToString();
                    phoneNumber = row3["phone_number"].ToString();
                    address = row3["address"].ToString();
                    item = row3["item_name"].ToString();

                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name, phoneNumber, address, item, search3, search4, search5, data, Pass, Access_auth);
                    this.Hide();
                    dataSearch.Show();
                }
            }
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
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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

        private void CustomerHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            CustomerHistorySelect customerHistorySelect = new CustomerHistorySelect(mainMenu, staff_id, data, Pass, Access_auth);
            customerHistorySelect.Show();
        }
    }
}
