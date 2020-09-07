using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class client_search : Form
    {
        Statement statement;
        MainMenu mainMenu;
        DataTable dt = new DataTable();
        public int count = 0;//計算書/納品書の顧客選択用
        int staff_id;
        int type;
        string staff_name;
        string address;
        decimal Total;
        string access_auth;
        string document;
        int control;
        string data;
        string pass;
        int grade;
        #region "買取販売履歴"
        string search01;
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
        string name1;
        string phoneNumber1;
        string address1;
        string addresskana1;
        string code1;
        string item1;
        string date1;
        string date2;
        string method1;
        string amountA;
        string amountB;
        string antiqueNumber;
        string documentNumber;
        #endregion
        bool screan = true;
        #region "納品書の引数"
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
        #region "計算書の引数"
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
        public client_search(Statement statement, int id, int type, string staff_name, string address, decimal Total, int control, decimal amount00, decimal amount01, decimal amount02, decimal amount03, decimal amount04, decimal amount05, decimal amount06, decimal amount07, decimal amount08, decimal amount09, decimal amount010, decimal amount011, decimal amount012, decimal amount10, decimal amount11, decimal amount12, decimal amount13, decimal amount14, decimal amount15, decimal amount16, decimal amount17, decimal amount18, decimal amount19, decimal amount110, decimal amount111, decimal amount112, string document, string access_suth, string pass)
        {
            InitializeComponent();

            this.statement = statement;
            //this.mainMenu = mainMenu;
            staff_id = id;
            this.type = type;
            this.address = address;
            this.staff_name = staff_name;
            this.Total = Total;
            this.control = control;
            this.document = document;
            #region "納品書の引数"
            this.amount00 = amount00;
            this.amount01 = amount01;
            this.amount02 = amount02;
            this.amount03 = amount03;
            this.amount04 = amount04;
            this.amount05 = amount05;
            this.amount06 = amount06;
            this.amount07 = amount07;
            this.amount08 = amount08;
            this.amount09 = amount09;
            this.amount010 = amount010;
            this.amount011 = amount011;
            this.amount012 = amount012;
            #endregion
            #region "計算書の引数"
            this.amount10 = amount10;
            this.amount11 = amount11;
            this.amount12 = amount12;
            this.amount13 = amount13;
            this.amount14 = amount14;
            this.amount15 = amount15;
            this.amount16 = amount16;
            this.amount17 = amount17;
            this.amount18 = amount18;
            this.amount19 = amount19;
            this.amount110 = amount110;
            this.amount111 = amount111;
            this.amount112 = amount112;
            #endregion
        }

        private void returnButton_Click(object sender, EventArgs e)//戻る
        {
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, search01, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
            this.Close();
            statement.Show();
        }

        private void client_search2_Load(object sender, EventArgs e)
        {


        }

        private void search1_Click(object sender, EventArgs e)
        {
            string clientName;
            string shopName;
            string clientStaff;
            string address;
            string search1 = "or";
            string search2 = "or";
            string search3 = "or";
            string search4 = "or";
            string search5 = "or";
            int check = 0;
            int type;
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定


            if (tabControl1.SelectedIndex == 0)
            {
                type = 0;
                if (!string.IsNullOrWhiteSpace(clientNameTextBox.Text))
                {
                    clientName = this.clientNameTextBox.Text;
                    #region "search1がand"
                    if (andRadioButton1.Checked == true)
                    {
                        search1 = "and";
                        #region "店舗名あり"
                        if (!string.IsNullOrWhiteSpace(shopNameTextBox.Text))
                        {
                            shopName = this.shopNameTextBox.Text;
                            #region "search2がand"
                            if (andRadioButton2.Checked == true)
                            {
                                search2 = "and";
                                #region "担当者名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索　　全部記入
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "住所以外記入　最後and"
                                        else
                                        {
                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "')"; //住所のみ無記入
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "最後のラジオボタンがorで全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索　　全部記入 最後はor
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "最後のラジオボタンがorで住所以外記入"
                                        else
                                        {
                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "')"; //住所のみ無記入
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "担当者名未記入"
                                else
                                {
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {

                                        search3 = "and";
                                        #region "担当者名が無記入　最後のラジオボタンがand"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "担当者名、住所が無記入で最後のラジオボタンがand"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion

                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "担当者名が無記入　最後のラジオボタンがor"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "担当者名、住所が無記入で最後のラジオボタンがor"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                            #region "search2がor"
                            else
                            {
                                #region "担当者名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "search2がor全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "search2がor住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "search2,3がor全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "search2,3がor住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "担当者名未記入"
                                else
                                {
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "担当者名以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "担当者、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "担当者名以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "担当者、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                        #region "店舗名未記入"
                        else
                        {
                            #region "search2がand"
                            if (andRadioButton2.Checked == true)
                            {
                                search2 = "and";
                                #region "会社名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "店舗名以外"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "店舗名、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "店舗名以外"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "店舗名、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "会社名未記入"
                                else
                                {
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region"店舗名、住所以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "店舗名以外未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3かor"
                                    else
                                    {
                                        #region"店舗名、住所以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "店舗名以外未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                            #region "search2がor"
                            else
                            {
                                #region "会社名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "店舗名以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "店舗、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "店舗以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "店舗、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "会社名未記入"
                                else
                                {
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "担当者、住所のみ記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "担当者のみ記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "担当者、住所のみ"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "担当者のみ"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                    #region "search1がor"
                    else
                    {
                        #region "店舗名記入"
                        if (!string.IsNullOrWhiteSpace(shopNameTextBox.Text))
                        {
                            shopName = this.shopNameTextBox.Text;
                            #region "search2がand"
                            if (andRadioButton2.Checked == true)
                            {
                                search2 = "and";
                                #region "担当者名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "担当者名未記入"
                                else
                                {
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "担当者以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                        #region "担当者、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "担当者以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                        #region "担当者、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                            #region "search2がor"
                            else
                            {
                                #region "担当者名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "担当者名未記入"
                                else
                                {
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "担当者名以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                        #region "担当者、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "担当者以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {

                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                        #region "住所、担当者以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                        #region "店舗名未記入"
                        else
                        {
                            #region "search2がand"
                            if (andRadioButton2.Checked == true)
                            {
                                search2 = "and";
                                #region "担当者名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "店舗名未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                        #region "店舗名、住所未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search2 + " staff_name = '" + clientStaff + "' " + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "店舗名未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                        #region "住所、店舗名未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "担当者名未記入"
                                else
                                {
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "担当者名、店舗名未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                        #region "会社名以外名未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "担当者名、店舗名未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                        #region "会社名以外未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                            #region "search2がor"
                            else
                            {
                                #region "担当者名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "店舗名未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                        #region "店舗名、住所未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "店舗名未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "店舗名、住所未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search2 + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "担当者名未記入"
                                else
                                {
                                    #region "search3がand"
                                    if (andRadioButton3.Checked == true)
                                    {
                                        search3 = "and";
                                        #region "住所、会社名のみ記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                        #region "会社名のみ記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region "search3がor"
                                    else
                                    {
                                        #region "住所、会社名のみ記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }

                                        }
                                        #endregion
                                        #region "会社名のみ記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                            this.Close();
                                            screan = false;
                                            if (statement.Visible == true)
                                            {
                                                this.Visible = false;
                                                search_Result.ShowDialog();
                                            }
                                            else
                                            {
                                                search_Result.Show();
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(shopNameTextBox.Text))
                    {
                        shopName = this.shopNameTextBox.Text;
                        #region "search2がand"
                        if (andRadioButton2.Checked == true)
                        {
                            search2 = "and";
                            #region "担当者名あり"
                            if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                            {
                                clientStaff = this.clientStaffNameTextBox.Text;
                                #region "search3がand"
                                if (andRadioButton3.Checked == true)
                                {
                                    search3 = "and";
                                    #region "会社名のみ未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and  shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }

                                    }
                                    #endregion
                                    #region "会社名、住所未記入"
                                    else
                                    {
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and  shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "search3がor"
                                else
                                {
                                    #region "会社名のみ未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and  shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }

                                    }
                                    #endregion
                                    #region "会社名、住所未記入"
                                    else
                                    {
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            #endregion
                            #region "担当者名未記入"
                            {
                                #region "search3がand"
                                if (andRadioButton3.Checked == true)
                                {
                                    search3 = "and";
                                    #region "会社名、担当者名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                    #region "店舗名以外未記入"
                                    else
                                    {
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "search3がor"
                                else
                                {
                                    #region "会社名、担当者名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }

                                    }
                                    #endregion
                                    #region "店舗名以外記入"
                                    else
                                    {
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                        #region "search2がor"
                        else
                        {
                            #region "担当者名あり"
                            if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                            {
                                clientStaff = this.clientStaffNameTextBox.Text;
                                #region "search3がand"
                                if (andRadioButton3.Checked == true)
                                {
                                    search3 = "and";
                                    #region "会社名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                    #region "会社名、住所未記入"
                                    else
                                    {
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "search3がor"
                                else
                                {
                                    #region "会社名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                    #region "会社名、住所未記入"
                                    else
                                    {
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                            #region "担当者名未記入"
                            else
                            {
                                #region "search3がand"
                                if (andRadioButton3.Checked == true)
                                {
                                    search3 = "and";
                                    #region "会社名、担当者名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                    #region "店舗名以外未記入"
                                    else
                                    {
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                                #region "search3がor"
                                else
                                {
                                    #region "会社名、担当者名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                    #region "店舗名以外未記入"
                                    else
                                    {
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                        this.Close();
                                        screan = false;
                                        if (statement.Visible == true)
                                        {
                                            this.Visible = false;
                                            search_Result.ShowDialog();
                                        }
                                        else
                                        {
                                            search_Result.Show();
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                        {
                            clientStaff = this.clientStaffNameTextBox.Text;
                            #region "search3がand"
                            if (andRadioButton3.Checked == true)
                            {
                                search3 = "and";
                                #region "担当者、住所のみ記入"
                                if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                {
                                    address = this.addressTextBox.Text;

                                    string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                                #region "担当者以外未記入"
                                else
                                {
                                    string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and staff_name = '" + clientStaff + "' )";
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            #region "search3がor"
                            else
                            {
                                #region "担当者、住所のみ記入"
                                if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                {
                                    address = this.addressTextBox.Text;

                                    string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                                #region "担当者以外未記入"
                                else
                                {
                                    string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and staff_name = '" + clientStaff + "' )";
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region "住所のみ"
                            if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                            {
                                address = this.addressTextBox.Text;

                                string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and  address like '%" + address + "%' )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                this.Close();
                                search_Result.ShowDialog();
                            }
                            #endregion
                            #region "未記入"
                            else
                            {
                                MessageBox.Show("検索条件を入力して下さい。");
                                return;
                            }
                            #endregion
                        }
                    }
                }//法人終了
                //個人
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                string iname;
                string iaddress;
                type = 1;

                if (!string.IsNullOrWhiteSpace(inameTextBox.Text))
                {
                    iname = inameTextBox.Text;
                    #region "search4がand"
                    if (andRadioButton4.Checked == true)
                    {
                        search4 = "and";
                        #region "住所あり"
                        if (!string.IsNullOrWhiteSpace(iaddressTextBox.Text))
                        {
                            iaddress = iaddressTextBox.Text;
                            #region "search5がand"
                            if (andRadioButton5.Checked == true)
                            {
                                search5 = "and";
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass); ;
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " (antique_license is null or antique_license = 'なし')"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            #region "search5がor"
                            else
                            {
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                        #region "住所なし"
                        else
                        {
                            #region "search5がand"
                            if (andRadioButton5.Checked == true)
                            {
                                search5 = "and";
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            #region "search5がor"
                            else
                            {
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                    #region "search4がor"
                    else
                    {
                        #region "住所あり"
                        if (!string.IsNullOrWhiteSpace(iaddressTextBox.Text))
                        {
                            iaddress = iaddressTextBox.Text;
                            #region "search5がand"
                            if (andRadioButton5.Checked == true)
                            {
                                search5 = "and";
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            #region "search5がor"
                            else
                            {
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                        #region "住所なし"
                        else
                        {
                            #region "search5がand"
                            if (andRadioButton5.Checked == true)
                            {
                                search5 = "and";
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            #region "search5がor"
                            else
                            {
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                    this.Close();
                                    screan = false;
                                    if (statement.Visible == true)
                                    {
                                        this.Visible = false;
                                        search_Result.ShowDialog();
                                    }
                                    else
                                    {
                                        search_Result.Show();
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region "住所あり"
                    if (!string.IsNullOrWhiteSpace(iaddressTextBox.Text))
                    {
                        iaddress = iaddressTextBox.Text;
                        if (andRadioButton5.Checked == true)
                        {
                            search5 = "and";
                            #region "古物商許可証あり"
                            if (radioButton1.Checked == true)//古物商許可証あり
                            {
                                check = 0;
                                string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and  address like '%" + iaddress + "%' " + search5 + " antique_license is not null )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
                                    this.Visible = false;
                                    search_Result.ShowDialog();
                                }
                                else
                                {
                                    search_Result.Show();
                                }
                            }
                            #endregion
                            #region "古物商許可証なし"
                            else if (radioButton2.Checked == true)//古物商許可証なし
                            {
                                check = 1;
                                string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and address like '%" + iaddress + "%' " + search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
                                    this.Visible = false;
                                    search_Result.ShowDialog();
                                }
                                else
                                {
                                    search_Result.Show();
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region "古物商許可証あり"
                            if (radioButton1.Checked == true)//古物商許可証あり
                            {
                                check = 0;
                                string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and address like '%" + iaddress + "%' " + search5 + " antique_license is not null )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
                                    this.Visible = false;
                                    search_Result.ShowDialog();
                                }
                                else
                                {
                                    search_Result.Show();
                                }
                            }
                            #endregion
                            #region "古物商許可証なし"
                            else if (radioButton2.Checked == true)//古物商許可証なし
                            {
                                check = 1;
                                string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and address like '%" + iaddress + "%' " + search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
                                    this.Visible = false;
                                    search_Result.ShowDialog();
                                }
                                else
                                {
                                    search_Result.Show();
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                    #region "住所なし"
                    else
                    {
                        #region "古物商許可証あり"
                        if (radioButton1.Checked == true)//古物商許可証あり
                        {
                            check = 0;
                            string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and  antique_license is not null )"; //住所部分一致検索
                            conn.Open();

                            adapter = new NpgsqlDataAdapter(sql2, conn);
                            adapter.Fill(dt);

                            conn.Close();
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                            this.Close();
                            screan = false;
                            if (statement.Visible == true)
                            {
                                this.Visible = false;
                                search_Result.ShowDialog();
                            }
                            else
                            {
                                search_Result.Show();
                            }
                        }
                        #endregion
                        #region "古物商許可証なし"
                        else if (radioButton2.Checked == true)//古物商許可証なし
                        {
                            check = 1;
                            string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                            conn.Open();

                            adapter = new NpgsqlDataAdapter(sql2, conn);
                            adapter.Fill(dt);

                            conn.Close();
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
                            this.Close();
                            screan = false;
                            if (statement.Visible == true)
                            {
                                this.Visible = false;
                                search_Result.ShowDialog();
                            }
                            else
                            {
                                search_Result.Show();
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            client_add client_Add = new client_add(mainMenu, staff_id, type, access_auth, pass);
            this.Close();
            client_Add.Show();

        }
    }
}