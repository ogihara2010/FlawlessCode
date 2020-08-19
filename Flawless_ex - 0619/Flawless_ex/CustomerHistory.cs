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
        bool screan = true;
        string document;
        int control;
        public CustomerHistory(MainMenu main, int id, string data, string pass)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            this.data = data;
            this.Pass = pass;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            CustomerHistorySelect customerHistorySelect = new CustomerHistorySelect(mainMenu, staff_id, data, Pass);
            screan = false;
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
            string  controlNumber;
            string antiqueNumber;
            string mainCategory;
            string code;
            string item;
            string itemcode;
            string date1 = this.dateTimePicker1.Text;
            string date2 = this.settlementBox.Text;
            string method;
            int amount1;
            int amount2;
            string amount01;
            string amount02;
            int ant;
            int amt;
            int amt1;
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
                #region "ラジオボタン"
                if (radioButton3.Checked == true)
                {
                    search1 = "and";

                }
                else { }
                if (radioButton6.Checked == true)
                {
                    search2 = "and";

                }
                else { }
                if (radioButton31.Checked == true)
                {
                    search3 = "and";

                }
                else { }
                if (radioButton33.Checked == true)
                {
                    search4 = "and";

                }
                else { }
                if (radioButton35.Checked == true)
                {
                    search5 = "and";

                }
                else { }
                if (radioButton37.Checked == true)
                {
                    search6 = "and";

                }
                else { }
                if (radioButton39.Checked == true)
                {
                    search7 = "and";

                }
                else { }
                if (radioButton41.Checked == true)
                {
                    search8 = "and";

                }
                else { }
                if (radioButton43.Checked == true)
                {
                    search9 = "and";

                }
                else { }
                if (radioButton45.Checked == true)
                {
                    search10 = "and";

                }
                else { }
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
                #endregion
                #region "入力パラメーター"
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    shopname = "B.shopname = '" + this.textBox1.Text + "'" + search1;
                    
                }
                else
                {
                    shopname = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    shopnamekana = " B.shop_name_kana = '" + this.textBox2.Text + "'" + search2;
                }
                else
                {
                    shopnamekana = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    name = this.textBox3.Text;
                }
                else { }
                if (!string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    address = " B.address like '% " + this.textBox4.Text + "%'" + search3;
                }
                else
                {
                    address = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    addresskana = " B.address_kana = '" + this.textBox5.Text + "'" + search4;
                }
                else
                {
                    addresskana = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    phoneNumber = " B.phone_number = '" + this.textBox6.Text + "'" + search5;
                }
                else
                {
                    phoneNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    documentNumber = " A.document_number = '" + this.textBox7.Text + "' " + search7;
                }
                else
                {
                    documentNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    controlNumber = " A.control_number = " + int.Parse(this.textBox8.Text) + " ";
                }
                else
                {
                    controlNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    antiqueNumber = " B.antique_number = " +  int.Parse(this.textBox9.Text) + " " + search8;
                }
                else
                {
                    antiqueNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                {
                    mainCategory = " E.main_category_name = '" + this.comboBox1.Text + "'" + search9;
                }
                else
                {
                    mainCategory = "";
                }
                if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    item = " D.item_name = '"  + this.comboBox2.Text + "'" + search10;
                }
                else
                {
                    item = "";
                }
                if (!string.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    method = search11 + " A.payment_method = '" + this.comboBox3.Text + "'" + search12;
                }
                else
                {
                    method = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amount01 = " A.total >= " + int.Parse(this.textBox10.Text);
                }
                else
                {
                    amount01 = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amount02 = " and A.total <= "  + int.Parse(this.textBox11.Text) ;
                }
                else
                {
                    amount02 = "";
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
                    control = int.Parse(textBox9.Text);
                }
                #endregion
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
                    conn3.Open();
                    adapter3 = new NpgsqlDataAdapter(sql3, conn3);
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
                    string sql = "select A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount, E.main_category_name from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                            "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                            "where A.type = 0 and " + shopname +  shopnamekana +  address +  addresskana +  phoneNumber +  documentNumber +  antiqueNumber + mainCategory  + item  + "( A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "')" + 
                              method +  amount01 + amount02 + ";";
                    conn.Open();
                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);
                    #endregion

                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }
                    
                    string name1 = textBox1.Text;
                    string phoneNumber1 = textBox6.Text;
                    string address1 = this.textBox4.Text;
                    string addresskana1 = this.textBox5.Text;
                    string item1 = itemcode;
                    string code1 = code;
                    string method1 = this.comboBox3.Text;
                    int antique = ant;
                    document = documentNumber;
                    amount2 = amt;
                    amount1 = amt1;

                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1, amount1, amount2, search1, search2, search3, search4, search5, search6, search7,search8, search9, search10, search11, search12, data, Pass, document, control, antique);
                    this.Hide();
                    dataSearch.Show();
                }
                else if (data == "D")
                {
                    string sql = "select A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount from delivery_m A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                           "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                           "where "+ shopname  +  shopnamekana  +  address +  addresskana  +  phoneNumber + controlNumber + antiqueNumber +  mainCategory +  item + "( A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "')" + 
                              method  + amount01  + amount02 + ";";
                    conn.Open();
                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);

                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }
                    string name1 = textBox1.Text;
                    string phoneNumber1 = textBox6.Text;
                    string address1 = this.textBox4.Text;
                    string addresskana1 = this.textBox5.Text;
                    string item1 = itemcode;
                    string code1 = code;
                    //control = int.Parse(this.textBox8.Text);
                    string method1 = this.comboBox3.Text;
                    int antique = ant;
                    amount2 = amt;
                    amount1 = amt1;

                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1,  amount1, amount2, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, data, Pass, document, control, antique);
                    screan = false;
                    this.Close();
                    dataSearch.Show();
                }
            }
            #region "検索条件　個人"
            if (radioButton2.Checked == true)
            {
                type = 1;
                #region "ラジオボタン"
                if (radioButton31.Checked == true)
                {
                    search3 = "and";

                }
                else { }
                if (radioButton33.Checked == true)
                {
                    search4 = "and";

                }
                else { }
                if (radioButton35.Checked == true)
                {
                    search5 = "and";

                }
                else { }
                if (radioButton37.Checked == true)
                {
                    search6 = "and";

                }
                else { }
                if (radioButton39.Checked == true)
                {
                    search7 = "and";

                }
                else { }
                if (radioButton41.Checked == true)
                {
                    search8 = "and";

                }
                else { }
                if (radioButton43.Checked == true)
                {
                    search9 = "and";

                }
                else { }
                if (radioButton45.Checked == true)
                {
                    search10 = "and";

                }
                else { }
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
                #endregion
                #region "パラメーター"
                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    name = "B.name = '" + this.textBox3.Text + "'" + search3;
                }
                else
                {
                    name = "";
                }
                
                if (!string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    address = " B.address like '% " + this.textBox4.Text + "%'" + search4;
                }
                else
                {
                    address = "";
                }
                
                if (!string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    addresskana = " B.address_kana = '" + this.textBox5.Text + "'" + search5;
                }
                else
                {
                    addresskana = "";
                }
                
                if (!string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    phoneNumber = " B.phone_number = '" + this.textBox6.Text + "'" + search7;
                }
                else
                {
                    phoneNumber = "";
                }
                
                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    documentNumber = " A.document_number = '" + this.textBox7.Text + "'" + search8;
                }
                else
                {
                    documentNumber = "";
                }
                
                if (!string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    controlNumber = " A.control_number = " + int.Parse(this.textBox8.Text) + " " + search8;
                }
                else
                {
                    controlNumber = "";
                }                                
                if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                {
                    mainCategory = " E.main_category_name = '" + this.comboBox1.Text + "' " + search10;
                }
                else
                {
                    mainCategory = "";
                }
                
                if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    item = " D.item_name = '" + this.comboBox2.Text + "'" + search11;
                }
                else
                {
                    item = "";
                }                                
                if (!string.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    method = search11 + " A.payment_method = '" + this.comboBox3.Text + "'" + search12;
                }
                else
                {
                    method = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amount01 = " (A.total >= " + int.Parse(this.textBox10.Text);
                }
                else
                {
                    amount01 = " ";
                }
                if (!string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amount02 = " and A.total <= " + int.Parse(this.textBox11.Text);
                }
                else
                {
                    amount02 = "";
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
                    conn3.Open();
                    adapter3 = new NpgsqlDataAdapter(sql3, conn3);
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
                    string sql = "select A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                            "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                            "where A.type = 1 and " + name +  address +  addresskana  +  phoneNumber  +  documentNumber +  mainCategory  +  item  + " (A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "') "  + method  +  amount01 +  amount02 + ";";

                    conn.Open();
                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);
                    #endregion

                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }

                    string name1 = this.textBox3.Text;
                    string phoneNumber1 = this.textBox6.Text;
                    string address1 = this.textBox4.Text;
                    string addresskana1 = this.textBox5.Text;
                    string item1 = itemcode;
                    string code1 = code;
                    string method1 = this.comboBox3.Text;
                    int antique = ant;
                    amount2 = amt;
                    amount1 = amt1;

                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1, amount1, amount2, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, data, Pass, document, control, antique);
                    screan = false;
                    this.Close();
                    dataSearch.Show();
                }
                if (data == "D")
                {
                    string sql = "select A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from delivery_m A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                            "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code) " +
                            "where " + name  + address +  addresskana + phoneNumber + controlNumber +  mainCategory  + item + " (A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "') " 
                             +  method  + amount01 + amount02 + ";";

                    conn.Open();
                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt7);

                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }

                    string name1 = this.textBox3.Text;
                    string phoneNumber1 = this.textBox6.Text;
                    string address1 = this.textBox4.Text;
                    string addresskana1 = this.textBox5.Text;
                    string item1 = itemcode;
                    string code1 = code;
                    string method1 = this.comboBox3.Text;
                    int antique = ant;
                    amount2 = amt;
                    amount1 = amt1;

                    control = int.Parse(this.textBox8.Text);
                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1, amount1, amount2, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, data, Pass, document, control, antique);
                    screan = false;
                    this.Close();
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

        private void CustomerHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                CustomerHistorySelect customerHistorySelect = new CustomerHistorySelect(mainMenu, staff_id, data, Pass);
                customerHistorySelect.Show();
            }
        }
    }
}
