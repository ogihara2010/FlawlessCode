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
        public int staff_id;
        public int type;
        public string staff_name;
        public string address;
        decimal Total;
        public string access_auth;
        public string document;
        public int control;
        public string data;
        public string pass;
        public int grade;
        public string clientName;
        public string shopName;
        public string clientStaff;
        public string iname;
        public string iaddress;
        public int check;
        #region "買取販売履歴"
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
        public client_search(Statement statement, int id, int type, string staff_name, string address, decimal Total, int control, string document, string access_suth, string pass)
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
            this.access_auth = access_suth;
            this.pass = pass;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻る
        {
            this.Close();
        }

        private void search1_Click(object sender, EventArgs e)
        {
            int type;
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            if (tabControl1.SelectedIndex == 0)
            {
                type = 0;
                #region "会社名あり"
                if (!string.IsNullOrWhiteSpace(clientNameTextBox.Text))
                {
                    clientName = this.clientNameTextBox.Text;
                    #region "店舗名あり"
                    if (!string.IsNullOrWhiteSpace(shopNameTextBox.Text))
                    {
                        shopName = this.shopNameTextBox.Text;
                        #region "担当者名あり"
                        if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                        {
                            clientStaff = this.clientStaffNameTextBox.Text;
                            #region "全部記入"
                            if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                            {
                                address = this.addressTextBox.Text;

                                string sql2 = "select company_name, shop_name, name, address,  code from client_m where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' " + " and shop_name like '%" + shopName + "%' " + " and name like '%" + clientStaff + "%' " + " and address like '%" + address + "%' ) order by company_kana, shop_name_kana ASC"; //住所部分一致検索　　全部記入
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
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
                                string sql2 = "select company_name, shop_name, name, address,  code from client_m where invalid = 0 and (type = 0 and company_name = '%" + clientName + "%' " + " and shop_name = '%" + shopName + "%'" + " and name like '%" + clientStaff + "%') order by company_kana, shop_name_kana ASC"; //住所のみ無記入
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
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
                        #region "担当者名未記入"
                        else
                        {
                            #region "担当者名のみが未記入"
                            if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                            {
                                address = this.addressTextBox.Text;

                                string sql2 = "select company_name, shop_name, name, address,  code from client_m where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' " + " and shop_name like '%" + shopName + "%' " + " and address like '%" + address + "%' ) order by company_kana, shop_name_kana ASC"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
                                    search_Result.ShowDialog();
                                }
                                else
                                {
                                    search_Result.Show();
                                }

                            }
                            #endregion
                            #region "担当者名と住所が未記入"
                            else
                            {
                                string sql = "select company_name, shop_name, name, address,  code from client_m where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' " + " and shop_name like '%" + shopName + "%' ) order by company_kana, shop_name_kana ASC";
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
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
                    #region "店舗名未記入"
                    else
                    {
                        #region "担当者名あり"
                        if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                        {
                            clientStaff = this.clientStaffNameTextBox.Text;
                            #region "店舗名のみ未記入"
                            if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                            {
                                address = this.addressTextBox.Text;

                                string sql2 = "select company_name, shop_name, name, address,  code from client_m where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' " + " and name like '%" + clientStaff + "%' " + " address like '%" + address + "%' ) order by company_kana, shop_name_kana ASC"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
                                    search_Result.ShowDialog();
                                }
                                else
                                {
                                    search_Result.Show();
                                }

                            }
                            #endregion
                            #region "店舗名と住所が未記入"
                            else
                            {
                                string sql = "select company_name, shop_name, name, address,  code from client_m where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' " + " and name like '%" + clientStaff + "%' ) order by company_kana, shop_name_kana ASC";
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
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
                        #region "担当者名未記入"
                        else
                        {
                            #region "会社名、住所以外未記入"
                            if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                            {
                                address = this.addressTextBox.Text;
                                string sql = "select company_name, shop_name, name, address, code from client_m where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' and address like '%" + address + "%' ) order by company_kana, shop_name_kana ASC;";
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
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
                                string sql = "select company_name, shop_name, name, address, code from client_m where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' ) order by company_kana, shop_name_kana ASC";
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
                                    //this.Visible = false;
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
                #region "会社名なし"
                else
                {
                    if (!string.IsNullOrWhiteSpace(shopNameTextBox.Text))
                    {
                        shopName = this.shopNameTextBox.Text;
                        #region "担当者名あり"
                        if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                        {
                            clientStaff = this.clientStaffNameTextBox.Text;
                            #region "会社名のみ未記入"
                            if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                            {
                                address = this.addressTextBox.Text;

                                string sql2 = "select company_name, shop_name, name, address, code from client_m where invalid = 0 and (type = 0 and  shop_name like '%" + shopName + "%' " + " name like '%" + clientStaff + "%' " + " address like '%" + address + "%' ) order by company_kana, shop_name_kana ASC"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
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
                                string sql = "select company_name, shop_name, name, address, code from client_m where invalid = 0 and (type = 0 and  shop_name like '%" + shopName + "%' " + " name like '%" + clientStaff + "%' ) order by company_kana, shop_name_kana ASC";
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
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
                        #endregion
                        #region "担当者名未記入"
                        {
                            #region "会社名、担当者名未記入"
                            if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                            {
                                address = this.addressTextBox.Text;

                                string sql2 = "select company_name, shop_name, name, address, code from client_m where invalid = 0 and (type = 0 and shop_name like '%" + shopName + "%' and address like '%" + address + "%' ) order by company_kana, shop_name_kana ASC"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
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
                                string sql = "select company_name, shop_name, name, address, code from client_m where invalid = 0 and (type = 0 and shop_name like '%" + shopName + "%' ) order by company_kana, shop_name_kana ASC";
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                shopName = search_Result.name;
                                if (statement.Visible == true)
                                {
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
                        if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                        {
                            clientStaff = this.clientStaffNameTextBox.Text;
                            #region "担当者、住所のみ記入"
                            if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                            {
                                address = this.addressTextBox.Text;

                                string sql2 = "select company_name, shop_name, name, address, code from client_m where invalid = 0 and (type = 0 and name like '%" + clientStaff + "%' and address like '%" + address + "%' ) order by company_kana, shop_name_kana ASC"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
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
                                string sql = "select company_name, shop_name, name, address, code from client_m where invalid = 0 and (type = 0 and name like '%" + clientStaff + "%' ) order by company_kana, shop_name_kana ASC";
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
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
                            #region "住所のみ"
                            if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                            {
                                address = this.addressTextBox.Text;

                                string sql2 = "select company_name, shop_name, name, address, code from client_m where invalid = 0 and (type = 0 and  address like '%" + address + "%' ) order by company_kana, shop_name_kana ASC"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                                this.Close();
                                screan = false;
                                if (statement.Visible == true)
                                {
                                    search_Result.ShowDialog();
                                }
                                else
                                {
                                    search_Result.Show();
                                }
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
                }
                #endregion
                //法人終了
                //個人
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                type = 1;
                #region "名前あり"
                if (!string.IsNullOrWhiteSpace(inameTextBox.Text))
                {
                    iname = inameTextBox.Text;
                    #region "住所あり"
                    if (!string.IsNullOrWhiteSpace(iaddressTextBox.Text))
                    {
                        iaddress = iaddressTextBox.Text;
                        #region "古物商許可証あり"
                        if (radioButton1.Checked == true)//古物商許可証あり
                        {
                            check = 0;
                            string sql2 = "select name,address, antique_license, code from client_m where invalid = 0 and (type = 1 and name like '%" + iname + "%' and address like '%" + iaddress + "%' and antique_license is not null ) order by name_kana, address_kana"; //住所部分一致検索
                            conn.Open();

                            adapter = new NpgsqlDataAdapter(sql2, conn);
                            adapter.Fill(dt);

                            conn.Close();
                            conn.Close();
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                            this.Close();
                            screan = false;
                            if (statement.Visible == true)
                            {
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
                            string sql2 = "select name,address, antique_license, code from client_m where invalid = 0 and (type = 1 and name like '%" + iname + "%' and address like '%" + iaddress + "%' and (antique_license is null or antique_license = 'なし') order by name_kana, address_kana"; //住所部分一致検索
                            conn.Open();

                            adapter = new NpgsqlDataAdapter(sql2, conn);
                            adapter.Fill(dt);

                            conn.Close();
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                            this.Close();
                            screan = false;
                            if (statement.Visible == true)
                            {
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
                    #region "住所なし"
                    else
                    {
                        #region "古物商許可証あり"
                        if (radioButton1.Checked == true)//古物商許可証あり
                        {
                            check = 0;
                            string sql2 = "select name,address, antique_license, code from client_m where invalid = 0 and (type = 1 and name like '%" + iname + "%' and antique_license is not null ) order by name_kana, address_kana"; //住所部分一致検索
                            conn.Open();

                            adapter = new NpgsqlDataAdapter(sql2, conn);
                            adapter.Fill(dt);

                            conn.Close();
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                            this.Close();
                            screan = false;
                            if (statement.Visible == true)
                            {
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
                            string sql2 = "select name,address, antique_license, code from client_m where invalid = 0 and (type = 1 and name like '%" + iname + "%' and (antique_license is null or antique_license = 'なし') ) order by name_kana, address_kana"; //住所部分一致検索
                            conn.Open();

                            adapter = new NpgsqlDataAdapter(sql2, conn);
                            adapter.Fill(dt);

                            conn.Close();
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                            this.Close();
                            screan = false;
                            if (statement.Visible == true)
                            {
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
                #region "名前なし"
                else
                {
                    #region "住所あり"
                    if (!string.IsNullOrWhiteSpace(iaddressTextBox.Text))
                    {
                        iaddress = iaddressTextBox.Text;
                        #region "古物商許可証あり"
                        if (radioButton1.Checked == true)//古物商許可証あり
                        {
                            check = 0;
                            string sql2 = "select name,address, antique_license, code from client_m where invalid = 0 and (type = 1 and  address like '%" + iaddress + "%' and antique_license is not null ) order by name_kana, address_kana"; //住所部分一致検索
                            conn.Open();

                            adapter = new NpgsqlDataAdapter(sql2, conn);
                            adapter.Fill(dt);

                            conn.Close();
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                            this.Close();
                            screan = false;
                            if (statement.Visible == true)
                            {
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
                            string sql2 = "select name,address, antique_license, code from client_m where invalid = 0 and (type = 1 and address like '%" + iaddress + "%' and (antique_license is null or antique_license = 'なし') ) order by name_kana, address_kana"; //住所部分一致検索
                            conn.Open();

                            adapter = new NpgsqlDataAdapter(sql2, conn);
                            adapter.Fill(dt);

                            conn.Close();
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                            this.Close();
                            screan = false;
                            if (statement.Visible == true)
                            {
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
                    #region "住所なし"
                    else
                    {
                        #region "古物商許可証あり"
                        if (radioButton1.Checked == true)//古物商許可証あり
                        {
                            check = 0;
                            string sql2 = "select name,address, antique_license, code from client_m where invalid = 0 and (type = 1 and  antique_license is not null ) order by name_kana, address_kana"; //住所部分一致検索
                            conn.Open();

                            adapter = new NpgsqlDataAdapter(sql2, conn);
                            adapter.Fill(dt);

                            conn.Close();
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                            this.Close();
                            screan = false;
                            if (statement.Visible == true)
                            {
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
                            string sql2 = "select name,address, antique_license, code from client_m where invalid = 0 and (type = 1 and (antique_license is null or antique_license = 'なし') ) order by name_kana, address_kana"; //住所部分一致検索
                            conn.Open();

                            adapter = new NpgsqlDataAdapter(sql2, conn);
                            adapter.Fill(dt);

                            conn.Close();
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
                            this.Close();
                            screan = false;
                            if (statement.Visible == true)
                            {
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
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            client_add client_Add = new client_add(statement, staff_id, type, access_auth, pass);
            screan = false;
            this.Close();
            this.data = client_Add.data;
            client_Add.ShowDialog();
        }
    }
}