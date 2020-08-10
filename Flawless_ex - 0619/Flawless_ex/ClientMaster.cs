using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class ClientMaster : Form
    {
        MasterMaintenanceMenu masterMenu;
        MasterMaintenanceMenu_BC masterMenu_BC;
        MainMenu mainMenu;
        DataTable dt = new DataTable();
        int staff_code;
        string access_auth;
        int type;
        string pass;
        bool screan = true;
        public ClientMaster(MasterMaintenanceMenu mster, int staff_code, string access_auth)
        {
            InitializeComponent();
            masterMenu = mster;
            this.staff_code = staff_code;
            this.access_auth = access_auth;
        }
        public ClientMaster(MasterMaintenanceMenu_BC mster_BC)
        {
            InitializeComponent();
            masterMenu_BC = mster_BC;
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            MasterMaintenanceMenu masterMenu = new MasterMaintenanceMenu(mainMenu, staff_code, access_auth, pass);
            screan = false;
            this.Close();
            masterMenu.Show();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                type = 0;
            }
            if (tabControl1.SelectedIndex == 1)
            {
                type = 1;
            }
            ClientMaster_add clientMaster_Add = new ClientMaster_add(masterMenu, staff_code, access_auth, type);
            screan = false;
            this.Close();
            clientMaster_Add.Show();
        }

        private void Search1_Click(object sender, EventArgs e)
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

            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            //法人
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' "  + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "担当者名、住所が無記入で最後のラジオボタンがand"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            this.Close();
                                            clientMastersearch.Show();

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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "search2がor住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2  + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "担当者、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' "  + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1  + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "店舗名、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1  + " staff_name = '" + clientStaff +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "店舗名以外未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName  + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            this.Close();
                                            clientMastersearch.Show();

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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1  + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1  + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "店舗、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 +  " staff_name = '" + clientStaff +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            this.Close();
                                            clientMastersearch.Show();

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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "担当者のみ"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName  + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 +  " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
                                        }
                                        #endregion
                                        #region "担当者、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' "  + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' "  + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2  + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
                                        }
                                        #endregion
                                        #region "担当者、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' "  + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
                                        }
                                        #endregion
                                        #region "住所、担当者以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' "  + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
                                        }
                                        #endregion
                                        #region "店舗名、住所未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName +  "' " + search2 + " staff_name = '" + clientStaff + "' "  + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' "  + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
                                        }
                                        #endregion
                                        #region "住所、店舗名未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 +  " staff_name = '" + clientStaff +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 +  " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
                                        }
                                        #endregion
                                        #region "会社名以外名未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 +  " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
                                        }
                                        #endregion
                                        #region "会社名以外未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName  + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 +  " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
                                        }
                                        #endregion
                                        #region "店舗名、住所未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 +  " staff_name = '" + clientStaff +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "店舗名、住所未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName +  "' " + search2 + " staff_name = '" + clientStaff +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 +  " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
                                        }
                                        #endregion
                                        #region "会社名のみ記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName  + "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' "  + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "会社名のみ記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName +  "' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();

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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();

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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();

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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                screan = false;
                                this.Close();
                                clientMastersearch.Show();
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
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " +  search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " +  search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " +  search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " +  search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " +  search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " +  search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " +  search5 + " antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " +  search5 + " (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                conn.Close();
                                ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                screan = false;
                                this.Close();
                                clientMastersearch.Show();
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
                                ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                screan = false;
                                this.Close();
                                clientMastersearch.Show();
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
                                conn.Close();
                                ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                screan = false;
                                this.Close();
                                clientMastersearch.Show();
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
                                ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                                screan = false;
                                this.Close();
                                clientMastersearch.Show();
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
                            conn.Close();
                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                            screan = false;
                            this.Close();
                            clientMastersearch.Show();
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
                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                            screan = false;
                            this.Close();
                            clientMastersearch.Show();
                        }
                        #endregion
                    }
                    #endregion
                }


            }
        }


        private void ClientMaster_Load(object sender, EventArgs e)
        {

        }

        private void ClientMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                MasterMaintenanceMenu masterMenu = new MasterMaintenanceMenu(mainMenu, staff_code, access_auth, pass);
                masterMenu.Show();
            }
        }
    }
}