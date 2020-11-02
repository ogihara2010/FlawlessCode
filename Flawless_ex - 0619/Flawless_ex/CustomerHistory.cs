using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;
using System.Text;
using System.Collections.Generic;

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
        DataTable dt8 = new DataTable();
        int type;
        int number;
        int a = 0; // クリック数 
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        NpgsqlDataAdapter adapter2;
        NpgsqlDataAdapter adapter3;

        List<string> fNumber = new List<string>();
        List<int> CNumber = new List<int>();

        public string data;
        string Pass;
        bool screan = true;
        string document;
        int control;
        string antiqueNumber;
        string access_auth;
        string kana;
        bool noLoad = false;

        DateTime? date1;
        DateTime? date2;


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

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "main_category_name";
            comboBox1.ValueMember = "main_category_code";
            comboBox1.SelectedIndex = -1;

            //品名検索用
            string sql_str2 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.invalid = 0;";
            NpgsqlDataAdapter adapter1;
            adapter1 = new NpgsqlDataAdapter(sql_str2, conn);
            adapter1.Fill(dt2);

            conn.Close();

            //comboBox1.DataSource = dt;
            //comboBox1.DisplayMember = "main_category_name";
            //comboBox1.ValueMember = "main_category_code";
            //comboBox1.SelectedIndex = -1;

            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "item_name";
            comboBox2.ValueMember = "item_code";
            comboBox2.SelectedIndex = -1;
            noLoad = true;
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

            string item = "";
            string itemcode;

            string Dlist;
            int rowCount = 0;
            
            //DateTime? Date1 ;                                                     //検索用
            //DateTime? Date2 ;                                                     //検索用

            //if (dateTimePicker1.Value == null)
            //{
            //    Date1=
            //}
            DateTime Date1 = DateTime.Parse(dateTimePicker1.Value.ToShortDateString());                                                     //検索用
            DateTime Date2 = DateTime.Parse(settlementBox.Value.ToShortDateString()).AddHours(23).AddMinutes(59).AddSeconds(59);             //検索用

            string method;
            decimal amount1;
            decimal amount2;
            string amountA;
            string amountB;
            decimal ant;
            decimal amt;
            decimal amt1;
            #endregion
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            NpgsqlConnection conn2 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter2;
            NpgsqlConnection conn3 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter3;
            NpgsqlCommand cmd;
            NpgsqlDataReader reader;

            dt5.Clear();
            dt7.Clear();
            dt8.Clear();
            fNumber.Clear();
            CNumber.Clear();

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            conn.Open();

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
                    name = "and B.name like '%" + this.textBox3.Text + "%'";
                }
                else
                {
                    name = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    address = " and B.address like '%" + this.textBox4.Text + "%'";
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
                    phoneNumber = " and B.phone_number like '%" + this.textBox6.Text + "%'";
                }
                else
                {
                    phoneNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    documentNumber = " and A.document_number like '%" + this.textBox7.Text + "%' ";
                }
                else
                {
                    documentNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    controlNumber = " and cast(A.control_number as text) like '%" + int.Parse(this.textBox8.Text) + "%' ";
                }
                else
                {
                    controlNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    antiqueNumber = " and cast(B.antique_number as text) like '%" + decimal.Parse(this.textBox9.Text) + "%' ";
                }
                else
                {
                    antiqueNumber = "";
                }
                //if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                //{
                //    mainCategory = " and E.main_category_name like '%" + this.comboBox1.SelectedIndex + "%'";
                //}
                //else
                //{
                //    mainCategory = "";
                //}
                //if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                //{
                //    item = " and D.item_code like '%" + this.comboBox2.SelectedIndex + "%'";
                //}
                //else
                //{
                //    item = "";
                //}
                if (!string.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    method = " and A.payment_method like '%" + this.comboBox3.Text + "%'";
                }
                else
                {
                    method = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amountA = " and A.total >= " + decimal.Parse(this.textBox10.Text);
                }
                else
                {
                    amountA = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amountB = " and A.total <= " + decimal.Parse(this.textBox11.Text);
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
                    ant = decimal.Parse(textBox9.Text);
                }
                if (string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amt = 0;
                }
                else
                {
                    amt = decimal.Parse(textBox11.Text);
                }
                if (string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amt1 = 0;
                }
                else
                {
                    amt1 = decimal.Parse(textBox10.Text);
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
                //if (comboBox1.SelectedIndex != -1)
                //{
                //    string sql2 = "select * from main_category_m where main_category_code = '" + this.comboBox1.SelectedIndex + "';";
                //    cmd = new NpgsqlCommand(sql2, conn);

                //    using (reader = cmd.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {

                //        }
                //    }
                //    //adapter2 = new NpgsqlDataAdapter(sql2, conn);
                //    //adapter2.Fill(dt5);
                //    //DataRow row;
                //    //row = dt5.Rows[0];
                //    //code = row["main_category_code"].ToString();
                //}

                #endregion
                #region "品名をコードに変換"
                if (comboBox2.SelectedIndex != -1)
                {
                    if (data == "S")
                    {
                        string sql3 = "select * from statement_calc_data A inner join statement_data B on A.document_number = B.document_number " +
                            "where item_code = '" + this.comboBox2.SelectedValue + "' order by notfnumber;";
                        adapter = new NpgsqlDataAdapter(sql3, conn);
                        adapter.Fill(dt5);

                        DataRow row;
                        rowCount = dt5.Rows.Count;
                        for (int i = 0; i < rowCount; i++)
                        {
                            row = dt5.Rows[i];
                            if (i != 0)
                            {
                                int listCount = fNumber.Count;
                                if (fNumber[listCount - 1] != row["document_number"].ToString())
                                {
                                    fNumber.Add(row["document_number"].ToString());
                                }
                            }

                            if (i == 0)
                            {
                                fNumber.Add(row["document_number"].ToString());
                            }
                        }
                    }
                    else if (data == "D")
                    {
                        string sql3 = "select * from delivery_calc where item_code = '" + this.comboBox2.SelectedValue + "' order by control_number;";
                        adapter = new NpgsqlDataAdapter(sql3, conn);
                        adapter.Fill(dt5);

                        DataRow row;
                        rowCount = dt5.Rows.Count;
                        for (int i = 0; i < rowCount; i++)
                        {
                            row = dt5.Rows[i];
                            if (i != 0)
                            {
                                int listCount = CNumber.Count;
                                if (CNumber[listCount - 1] != (int)row["control_number"])
                                {
                                    CNumber.Add((int)row["control_number"]);
                                }
                            }

                            if (i == 0)
                            {
                                CNumber.Add((int)row["control_number"]);
                            }
                        }
                    }
                }
                #endregion

                if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                //    if (data == "S") {
                //        for (int i = 0; i < fNumber.Count; i++)
                //        {
                //            //品名コードから計算書を取得
                //            item += " A.document_number = '" + fNumber[i] + "'";
                //        }
                //    } else if (data == "D")
                //    {

                //    }
                //}
                //else
                //{
                //    item = "";
                }


                if (data == "S")
                {
                    if (fNumber.Count != 0)
                    {
                        for (int i = 0; i < fNumber.Count; i++)
                        {
                            string sql = "select A.document_number, A.settlement_date, A.delivery_date, B.shop_name, B.name, B.phone_number, B.address, B.antique_number, A.total, A.notfnumber from statement_data A inner join client_m B ON (A.code = B.code )" +
                                    "where B.type = 0 " + shopname + shopnamekana + name + address + addresskana + phoneNumber + documentNumber + antiqueNumber + " and A.document_number = '" + fNumber[i] + "' and ( A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "')" +
                                      method + amountA + amountB + ";";

                            //string sql = "select A.document_number, A.settlement_date, A.delivery_date, B.shop_name, B.name, B.phone_number, B.address, B.antique_number, A.amount, A.notfnumber from statement_data A inner join client_m B ON (A.code = B.code )" +
                            //        "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                            //        "where B.type = 0 " + shopname + shopnamekana + name + address + addresskana + phoneNumber + documentNumber + antiqueNumber + mainCategory + item + " and ( A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "')" +
                            //          method + amountA + amountB + " order by A.notfnumber ;";

                            adapter = new NpgsqlDataAdapter(sql, conn);
                            adapter.Fill(dt7);
                        }
                    }
                    else
                    {
                        string sql = "select A.document_number, A.settlement_date, A.delivery_date, B.shop_name, B.name, B.phone_number, B.address, B.antique_number, A.total, A.notfnumber from statement_data A inner join client_m B ON (A.code = B.code )" +
                                "where B.type = 0 " + shopname + shopnamekana + name + address + addresskana + phoneNumber + documentNumber + antiqueNumber + " and ( A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "')" +
                                  method + amountA + amountB + " order by A.notfnumber ;";

                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dt7);
                    }
                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }

                    document = this.textBox7.Text;

                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, dt7, data, Pass, document, control, antiqueNumber, documentNumber, access_auth);
                    //DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, Date2, method1, amountA, amountB, data, Pass, document, control, antiqueNumber, documentNumber, access_auth);
                    this.data = dataSearch.data;
                    dataSearch.ShowDialog();
                }
                else if (data == "D")
                {
                    if (CNumber.Count != 0)
                    {
                        for (int i = 0; i < CNumber.Count; i++)
                        {
                            string sql = "select A.control_number, A.settlement_date, A.delivery_date, B.shop_name, B.name, B.phone_number, B.address, B.antique_number, A.total from delivery_m A inner join client_m B ON (A.code = B.code )" +
                                   "where A.types1 = 0 " + shopname + shopnamekana + name + address + addresskana + phoneNumber + controlNumber + antiqueNumber + " and A.control_number = '" + CNumber[i] + "' and ( A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "') " +
                                      method + amountA + amountB + " order by control_number;";

                            //string sql = "select A.control_number, A.settlement_date, A.delivery_date, B.shop_name, B.name, B.phone_number, B.address, B.antique_number, A.total from delivery_m A inner join client_m B ON (A.code = B.code )" +
                            //       "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                            //       "where A.types1 = 0 " + shopname + shopnamekana + name + address + addresskana + phoneNumber + controlNumber + antiqueNumber + mainCategory + item + " and ( A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "') " +
                            //          method + amountA + amountB + " order by control_number;";

                            adapter = new NpgsqlDataAdapter(sql, conn);
                            adapter.Fill(dt7);
                        }
                    }
                    else
                    {
                          string sql = "select A.control_number, A.settlement_date, A.delivery_date, B.shop_name, B.name, B.phone_number, B.address, B.antique_number, A.total from delivery_m A inner join client_m B ON (A.code = B.code )" +
                                    "where A.types1 = 0 " + shopname + shopnamekana + name + address + addresskana + phoneNumber + controlNumber + antiqueNumber + " and ( A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "') " +
                                       method + amountA + amountB + " order by control_number;";

                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dt7);
                    }
                    int a = dt7.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }
                    documentNumber = controlNumber;

                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, dt7, data, Pass, document, control, antiqueNumber, documentNumber, access_auth);
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
                    address = " and B.address like '%" + this.textBox4.Text + "%'";
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
                    phoneNumber = " and B.phone_number like '%" + this.textBox6.Text + "%'";
                }
                else
                {
                    phoneNumber = "";
                }

                if (!string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    documentNumber = " and A.document_number like '%" + this.textBox7.Text + "%'";
                }
                else
                {
                    documentNumber = "";
                }

                if (!string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    controlNumber = " and cast(A.control_number as text) like '%" + int.Parse(this.textBox8.Text) + "%' ";
                }
                else
                {
                    controlNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox9.Text))
                {
                    antiqueNumber = " and cast(B.antique_number as text) like '%" + decimal.Parse(this.textBox9.Text) + "%' ";
                }
                else
                {
                    antiqueNumber = "";
                }
                if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                {
                    mainCategory = " and E.main_category_name like '%" + this.comboBox1.Text + "%' ";
                }
                else
                {
                    mainCategory = "";
                }

                //if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                //{
                //    item = " and D.item_name like '%" + this.comboBox2.Text + "%'";
                //}
                //else
                //{
                //    item = "";
                //}
                if (!string.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    method = " and A.payment_method like '%" + this.comboBox3.Text + "%'";
                }
                else
                {
                    method = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amountA = " and A.total >= " + decimal.Parse(this.textBox10.Text);
                }
                else
                {
                    amountA = "";
                }
                if (!string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amountB = " and A.total <= " + decimal.Parse(this.textBox11.Text) ;
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
                    ant = decimal.Parse(textBox9.Text);
                }
                if (string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    amt = 0;
                }
                else
                {
                    amt = decimal.Parse(textBox11.Text);
                }
                if (string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    amt1 = 0;
                }
                else
                {
                    amt1 = decimal.Parse(textBox10.Text);
                }
                #endregion
                #region "繋げるSQL"
                #region "大分類名をコードに変換"
                #endregion
                #region "品名をコードに変換"
                if (comboBox2.SelectedIndex != -1)
                {
                    if (data == "S")
                    {
                        string sql3 = "select * from statement_calc_data A inner join statement_data B on A.document_number = B.document_number " +
                            "where item_code = '" + this.comboBox2.SelectedValue + "' order by notfnumber;";
                        adapter = new NpgsqlDataAdapter(sql3, conn);
                        adapter.Fill(dt5);

                        DataRow row;
                        rowCount = dt5.Rows.Count;
                        for (int i = 0; i < rowCount; i++)
                        {
                            row = dt5.Rows[i];
                            if (i != 0)
                            {
                                int listCount = fNumber.Count;
                                if (fNumber[listCount - 1] != row["document_number"].ToString())
                                {
                                    fNumber.Add(row["document_number"].ToString());
                                }
                            }

                            if (i == 0)
                            {
                                fNumber.Add(row["document_number"].ToString());
                            }
                        }
                    }
                    else if (data == "D")
                    {
                        string sql3 = "select * from delivery_calc where item_code = '" + this.comboBox2.SelectedValue + "' order by control_number;";
                        adapter = new NpgsqlDataAdapter(sql3, conn);
                        adapter.Fill(dt5);

                        DataRow row;
                        rowCount = dt5.Rows.Count;
                        for (int i = 0; i < rowCount; i++)
                        {
                            row = dt5.Rows[i];
                            if (i != 0)
                            {
                                int listCount = CNumber.Count;
                                if (CNumber[listCount - 1] != (int)row["control_number"])
                                {
                                    CNumber.Add((int)row["control_number"]);
                                }
                            }

                            if (i == 0)
                            {
                                CNumber.Add((int)row["control_number"]);
                            }
                        }
                    }
                }
                #endregion

                if (data == "S")
                {
                    if (fNumber.Count != 0)
                    {
                        for (int i = 0; i < fNumber.Count; i++)
                        {
                            string sql = "select A.document_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, B.antique_number, A.total, A.notfnumber from statement_data A inner join client_m B ON ( A.code = B.code )" +
                                    "where A.type = 1 " + name + address + addresskana + phoneNumber + documentNumber + antiqueNumber + "and A.document_number = '" + fNumber[i] + "' and  (A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "') " + method + amountA + amountB + " order by notfnumber;";

                            //string sql = "select A.document_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount, A.notfnumber from statement_data A inner join client_m B ON ( A.code = B.code )" +
                            //        "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) inner join main_category_m E ON (D.main_category_code = E.main_category_code)" +
                            //        "where A.type = 1 " + name + address + addresskana + phoneNumber + documentNumber + mainCategory + item + " and (A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "') " + method + amountA + amountB + " order by notfnumber;";

                            adapter = new NpgsqlDataAdapter(sql, conn);
                            adapter.Fill(dt8);
                        }
                    }
                    else
                    {
                        string sql = "select A.document_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, B.antique_number, A.total, A.notfnumber from statement_data A inner join client_m B ON ( A.code = B.code )" +
                                "where A.type = 1 " + name + address + addresskana + phoneNumber + documentNumber + antiqueNumber + " and (A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "') " + method + amountA + amountB + " order by notfnumber;";

                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dt8);
                    }
                    int a = dt8.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }

                    antiqueNumber = null;
                    

                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, dt8, data, Pass, document, control, antiqueNumber, documentNumber, access_auth);
                    this.data = dataSearch.data;
                    dataSearch.ShowDialog();
                }
                if (data == "D")
                {
                    if (CNumber.Count != 0)
                    {
                        for (int i = 0; i < CNumber.Count; i++)
                        {
                            string sql = "select A.control_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, B.antique_number, A.total from delivery_m A inner join client_m B ON ( A.code = B.code )" +
                                    "where A.types1 = 1 " + name + address + addresskana + phoneNumber + controlNumber + antiqueNumber + "and A.control_number = '" + CNumber[i] + "'  and  (A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "')  "
                                     + method + amountA + amountB + ";";

                            adapter = new NpgsqlDataAdapter(sql, conn);
                            adapter.Fill(dt8);
                        }
                    }
                    else
                    {
                        string sql = "select A.control_number, A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, B.antique_number, A.total from delivery_m A inner join client_m B ON ( A.code = B.code )" +
                                    "where A.types1 = 1 " + name + address + addresskana + phoneNumber + controlNumber + antiqueNumber + " and  (A.settlement_date >= '" + Date1 + "' and A.settlement_date <= '" + Date2 + "')  "
                                     + method + amountA + amountB + "order by control_number;";

                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dt8);
                    }
                    int a = dt8.Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("検索データがありません。");
                        return;
                    }

                    antiqueNumber = null;
                    documentNumber = controlNumber;
                    

                    //control = int.Parse(this.textBox8.Text);
                    DataSearchResults dataSearch = new DataSearchResults(mainMenu, type, staff_id, dt8, data, Pass, document, control, antiqueNumber, documentNumber, access_auth);
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
            if (noLoad)
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                NpgsqlDataReader reader;
                dt2.Clear();

                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                conn.Open();
                int itemCode = int.Parse(comboBox1.SelectedValue.ToString());

                string sql = "select * from item_m where main_category_code = '" + itemCode + "';";
                adapter = new NpgsqlDataAdapter(sql, conn);
                adapter.Fill(dt2);

                comboBox2.DataSource = dt2;
                comboBox2.DisplayMember = "item_name";
                comboBox2.ValueMember = "item_code";

                conn.Close();
            }
        }

        #region"カタカナを半角に変換"
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(textBox2.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            textBox2.Text = stringBuilder.ToString();
            textBox2.Select(textBox2.Text.Length, 0);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(textBox5.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            textBox5.Text = stringBuilder.ToString();
            textBox5.Select(textBox5.Text.Length, 0);
        }
        #endregion

        #region"datetimepicker"
        //private void setDateTimePicker1(DateTime? dateTime)
        //{
        //    if (dateTime == null)
        //    {
        //        dateTimePicker1.Format = DateTimePickerFormat.Custom;
        //        dateTimePicker1.CustomFormat = " ";
        //    }
        //    else
        //    {
        //        dateTimePicker1.Format = DateTimePickerFormat.Long;
        //        dateTimePicker1.Value = (DateTime)dateTime;
        //    }
        //}
        //private void setDateTimePicker2(DateTime? dateTime)
        //{
        //    if (dateTime == null)
        //    {
        //        settlementBox.Format = DateTimePickerFormat.Custom;
        //        settlementBox.CustomFormat = " ";
        //    }
        //    else
        //    {
        //        settlementBox.Format = DateTimePickerFormat.Long;
        //        settlementBox.Value = (DateTime)dateTime;
        //    }
        //}

        //private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        //{
        //    date1 = dateTimePicker1.Value;
        //    setDateTimePicker1(date1);
        //}

        //private void dateTimePicker1_MouseDown(object sender, MouseEventArgs e)
        //{

        //}

        //private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        //{

        //}

        //private void settlementBox_KeyDown(object sender, KeyEventArgs e)
        //{

        //}

        //private void settlementBox_MouseDown(object sender, MouseEventArgs e)
        //{

        //}

        //private void settlementBox_ValueChanged(object sender, EventArgs e)
        //{
        //    date2 = settlementBox.Value;
        //    setDateTimePicker2(date2);
        //}
        #endregion

        //private void BuyDateTimePicker1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Y < 0 || e.Y > BuyDateTimePicker1.Height)
        //    {
        //        SendKeys.SendWait("%{DOWN}");
        //    }
        //}

        //private void BuyDateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
        //    {
        //        date1 = null;
        //        setDateTimePicker1(date1);
        //    }
        //}

        //private void BuyDateTimePicker1_ValueChanged(object sender, EventArgs e)
        //{
        //    date1 = BuyDateTimePicker1.Value;
        //    setDateTimePicker1(date1);
        //}

    }
}
