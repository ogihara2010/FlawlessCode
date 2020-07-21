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
        public CustomerHistory(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }

        private void CustomerHistory_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
            int documentNumber;
            int controlNumber;
            int antiqueNumber;
            string mainCategory;
            string item;
            string date1;
            string date2;
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

            conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn2.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn3.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
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
                    documentNumber = int.Parse(this.textBox7.Text);
                }
                else
                {
                    documentNumber = 0;
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
                if (!string.IsNullOrWhiteSpace(textBox12.Text))
                {
                    date1 = this.textBox12.Text;
                }
                else
                {
                    date1 = "1900/4/1";
                }
                if (!string.IsNullOrWhiteSpace(textBox13.Text))
                {
                    date2 = this.textBox13.Text;
                }
                else
                {
                    date2 = "2500/12/31";
                }
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

                string sql = "select A.order_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, A.total, D.item_name from delivery_m A inner join client_m_corporate B ON (A.antique_number = B.antique_number and A.id_number = B.insert_name )" +
                             "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                             "where B.shop_name = '" + shopname + "'" + search1 + " B.shop_name_kana = '" + shopnamekana + "'" + search2 + " B.address like '% " + address + "%'" + search3 + " B.address_kana = '" + addresskana + "'" + search4
                              + " B.phone_number = '" + phoneNumber + "'" + search5 + " A.control_number = " + controlNumber + " " + search7 + " B.antique_number = " + antiqueNumber + " " + search8
                              + " D.main_category_code = " + code  + " "+ search9 + " D.item_code = " + itemcode + " " + search10 + "( A.settlement_date >= '"  + date1 + "' and A.settlement_date <= '" + date2 + "')" + search11 
                              + " A.payment_method = '" + method + "'" + search12 + " A.total >= " + amount1 + " and A.total <= " + amount2 + ";"; 


                conn.Open();
                adapter = new NpgsqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                #endregion
                DataRow row3;
                row3 = dt.Rows[0];
                name = row3["shop_name"].ToString();
                phoneNumber = row3["phone_number"].ToString();
                address = row3["address"].ToString();
                item = row3["item_name"].ToString();

                DataSearchResults dataSearch = new DataSearchResults(this, type, name, phoneNumber, address, item, search1, search3, search5);
                this.Hide();
                dataSearch.Show();
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
                    documentNumber = int.Parse(this.textBox7.Text);
                }
                else
                {
                    documentNumber = 0;
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
                if (!string.IsNullOrWhiteSpace(textBox12.Text))
                {
                    date1 = this.textBox12.Text;
                }
                else
                {
                    date1 = "1900/4/1";
                }
                if (!string.IsNullOrWhiteSpace(textBox13.Text))
                {
                    date2 = this.textBox13.Text;
                }
                else
                {
                    date2 = "2500/12/31";
                }
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

                string sql = "select A.order_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, A.total from delivery_m A inner join client_m_individual B ON ( A.name = B.name )" +
                             "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code )inner join statement_m E ON (B.id_number = E.id_number) inner join statement_calc F ON (E.document_number = F.document_number) " +
                             "where B.name = '" + name + "'" + search3 + " B.address like '% " + address + "%'" + search4 + " B.address_kana = '" + addresskana + "'" + search5
                              + " B.phone_number = '" + phoneNumber + "'" + search6 + " E.document_number = " + documentNumber + " " + search7 + " A.control_number = " + controlNumber + " " + search8 +  " " 
                              + " D.main_category_code = " + code + " " + search10 + " D.item_code = " + itemcode + " " + search11 + " A.settlement_date >= '" + date1 + "' and A.settlement_date <= '" + date2 + "'" + search11
                              + " A.payment_method = '" + method + "'" + search12 + " A.total >= " + amount1 + " and A.total <= " + amount2 + ";"; 


                conn.Open();
                adapter = new NpgsqlDataAdapter(sql, conn);
                adapter.Fill(dt);
                #endregion

                DataRow row3;
                row3 = dt.Rows[0];
                name = row3["name"].ToString();
                phoneNumber = row3["phone_number"].ToString();
                address = row3["address"].ToString();
                item = row3["item_name"].ToString();

                DataSearchResults dataSearch = new DataSearchResults(this, type, name, phoneNumber, address, item, search3, search4, search5);
                this.Hide();
                dataSearch.Show();
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
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
    }
}
