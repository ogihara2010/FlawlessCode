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
        public string data;
        string pass;
        int grade;
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
        }

        private void returnButton_Click(object sender, EventArgs e)//戻る
        {
            this.Close();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' "  + " staff_name = '" + clientStaff + "' "  + " address like '%" + address + "%' )"; //住所部分一致検索　　全部記入
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' "  + " staff_name = '" + clientStaff + "')"; //住所のみ無記入
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "最後のラジオボタンがorで全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' "  + " staff_name = '" + clientStaff + "' "  + " address like '%" + address + "%' )"; //住所部分一致検索　　全部記入 最後はor
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "')"; //住所のみ無記入
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "担当者名未記入"
                                else
                                {
                                        #region "担当者名が無記入　最後のラジオボタンがand"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "担当者名が無記入　最後のラジオボタンがor"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "担当者名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;                                    
                                        #region "search2がor全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "search2,3がor全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " +  " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "担当者名未記入"
                                else
                                {                                    
                                        #region "担当者名以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "担当者名以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                        #region "店舗名未記入"
                        else
                        {
                                #region "会社名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;                                    
                                        #region "店舗名以外"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " +  " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "店舗名以外"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "会社名未記入"
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region"店舗名、住所以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "会社名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                        #region "店舗名以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "店舗以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "会社名未記入"
                                else
                                {
                                        #region "担当者、住所のみ記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "担当者、住所のみ"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                        #region "店舗名記入"
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' "  + " staff_name = '" + clientStaff + "' "  + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " +  " staff_name = '" + clientStaff + "' " + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' " + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "担当者名未記入"
                                else
                                {
                                        #region "担当者以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "担当者以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "担当者名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                        #region "全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "全部記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' " + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "担当者名未記入"
                                else
                                {
                                        #region "担当者名以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "担当者以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {

                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " +  " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                        #region "店舗名未記入"
                        else
                        {
                                #region "担当者名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                        #region "店舗名未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + " staff_name = '" + clientStaff + "' " + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "店舗名未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "担当者名未記入"
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "担当者名、店舗名未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "担当者名あり"
                                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                                {
                                    clientStaff = this.clientStaffNameTextBox.Text;
                                        #region "店舗名未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "店舗名未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " +  " staff_name = '" + clientStaff + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "担当者名未記入"
                                else
                                {
                                        #region "住所、会社名のみ記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        #region "住所、会社名のみ記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and  shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and  shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    #region "会社名のみ未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and  shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                            #endregion
                            #region "担当者名未記入"
                            {
                                    #region "会社名、担当者名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    #region "会社名、担当者名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                            #region "担当者名あり"
                            if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                            {
                                clientStaff = this.clientStaffNameTextBox.Text;
                                    #region "会社名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    #region "会社名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + " staff_name = '" + clientStaff + "' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                            #region "担当者名未記入"
                            else
                            {
                                    #region "会社名、担当者名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    #region "会社名、担当者名未記入"
                                    if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                    {
                                        address = this.addressTextBox.Text;

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name = '" + shopName + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                        client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                        if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                        {
                            clientStaff = this.clientStaffNameTextBox.Text;
                                #region "担当者、住所のみ記入"
                                if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                {
                                    address = this.addressTextBox.Text;

                                    string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and staff_name = '" + clientStaff + "' "  + " address like '%" + address + "%' )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "担当者、住所のみ記入"
                                if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                {
                                    address = this.addressTextBox.Text;

                                    string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and staff_name = '" + clientStaff + "' " + " address like '%" + address + "%' )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                            #region "住所のみ"
                            if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                            {
                                address = this.addressTextBox.Text;

                                string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and  address like '%" + address + "%' )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                        #region "住所あり"
                        if (!string.IsNullOrWhiteSpace(iaddressTextBox.Text))
                        {
                            iaddress = iaddressTextBox.Text;
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " address like '%" + iaddress + "%' " + " antique_license is not null )"; //住所部分一致検索
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " address like '%" + iaddress + "%' " + " (antique_license is null or antique_license = 'なし')"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " address like '%" + iaddress + "%' " + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " address like '%" + iaddress + "%' " + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                        #region "住所なし"
                        else
                        {
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " antique_license is not null )"; //住所部分一致検索
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                        #region "住所あり"
                        if (!string.IsNullOrWhiteSpace(iaddressTextBox.Text))
                        {
                            iaddress = iaddressTextBox.Text;
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " address like '%" + iaddress + "%' " + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " address like '%" + iaddress + "%' " + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " address like '%" + iaddress + "%' " + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " address like '%" + iaddress + "%' " + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                        #region "住所なし"
                        else
                        {
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                #region "古物商許可証あり"
                                if (radioButton1.Checked == true)//古物商許可証あり
                                {
                                    check = 0;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                    #region "住所あり"
                    if (!string.IsNullOrWhiteSpace(iaddressTextBox.Text))
                    {
                        iaddress = iaddressTextBox.Text;
                            #region "古物商許可証あり"
                            if (radioButton1.Checked == true)//古物商許可証あり
                            {
                                check = 0;
                                string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and  address like '%" + iaddress + "%' " + " antique_license is not null )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and address like '%" + iaddress + "%' "  + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                            #region "古物商許可証あり"
                            if (radioButton1.Checked == true)//古物商許可証あり
                            {
                                check = 0;
                                string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and address like '%" + iaddress + "%' " + " antique_license is not null )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                                string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and address like '%" + iaddress + "%' " +  " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
                            client_search_result search_Result = new client_search_result(dt, type, check, statement, staff_id, Total, control, document, access_auth, pass);
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
            screan = false;
            this.Close();
            this.data = client_Add.data;
            client_Add.ShowDialog();
        }
    }
}