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
        public int staff_code;
        public string access_auth;
        public int type;
        bool screan = true;
        public string Pass;

        public ClientMaster(MasterMaintenanceMenu mster, int staff_code, string access_auth, string pass)
        {
            InitializeComponent();
            masterMenu = mster;
            this.staff_code = staff_code;
            this.access_auth = access_auth;
            this.Pass = pass;
        }
        public ClientMaster(MasterMaintenanceMenu_BC mster_BC)
        {
            InitializeComponent();
            masterMenu_BC = mster_BC;
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            MasterMaintenanceMenu masterMenu = new MasterMaintenanceMenu(mainMenu, staff_code, access_auth, Pass);
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
            ClientMaster_add clientMaster_Add = new ClientMaster_add(masterMenu, staff_code, access_auth, type, Pass);
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
            int check = 0;
            int type;
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            //法人
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

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' and shop_name like '%" + shopName + "%' and staff_name like '%" + clientStaff + "%' and address like '%" + address + "%' )"; //住所部分一致検索　　全部記入
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "住所以外記入　最後and"
                                        else
                                        {
                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' and shop_name like '%" + shopName + "%' and staff_name like '%" + clientStaff + "%')"; //住所のみ無記入
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
                                        }
                                        #endregion
                                }
                                #endregion
                                #region "担当者名未記入"
                                else
                                {
                                        #region "担当者名が未記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' and shop_name like '%" + shopName + "%' and address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "担当者名、住所が未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' and shop_name like '%" + shopName +  "'%)";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                            this.Close();
                                            clientMastersearch.Show();
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
                                        #region "店舗名以外"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' and staff_name like '%" + clientStaff + "%' and address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "店舗名、住所以外記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' and staff_name like '%" + clientStaff +  "%' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();
                                        }
                                        #endregion
                                }
                                #endregion
                                #region "担当者名未記入"
                                else
                                {
                                        #region"店舗名、住所以外記入"
                                        if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                                        {
                                            address = this.addressTextBox.Text;

                                            string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name like '%" + clientName + "%' and address like '%" + address + "%' )"; //住所部分一致検索
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql2, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                            screan = false;
                                            this.Close();
                                            clientMastersearch.Show();

                                        }
                                        #endregion
                                        #region "店舗名以外未記入"
                                        else
                                        {
                                            string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name like '%" + clientName  + "%' )";
                                            conn.Open();

                                            adapter = new NpgsqlDataAdapter(sql, conn);
                                            adapter.Fill(dt);

                                            conn.Close();
                                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
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

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and  shop_name like '%" + shopName + "%' and staff_name like '%" + clientStaff + "%'  address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();

                                    }
                                    #endregion
                                    #region "会社名、住所未記入"
                                    else
                                    {
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and  shop_name like '%" + shopName + "%' and staff_name like '%" + clientStaff + "%' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
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

                                        string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name like '%" + shopName + "%' and address like '%" + address + "%' )"; //住所部分一致検索
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql2, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                        screan = false;
                                        this.Close();
                                        clientMastersearch.Show();
                                    }
                                    #endregion
                                    #region "店舗名以外未記入"
                                    else
                                    {
                                        string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and shop_name like '%" + shopName + "%' )";
                                        conn.Open();

                                        adapter = new NpgsqlDataAdapter(sql, conn);
                                        adapter.Fill(dt);

                                        conn.Close();
                                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                        this.Close();
                                        clientMastersearch.Show();
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

                                    string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and staff_name like '%" + clientStaff + "%' and address like '%" + address + "%' )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
                                }
                                #endregion
                                #region "担当者以外未記入"
                                else
                                {
                                    string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and staff_name like '%" + clientStaff + "%' )";
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
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
                #endregion

                //個人
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                string iname;
                string iaddress;
                type = 1;
                #region "氏名あり"
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name like '%" + iname + "%' and address like '%" + iaddress + "%' and antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name like '%" + iname + "%' and address like '%" + iaddress + "%' and (antique_license is null or antique_license = 'なし'))"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
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
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name like '%" + iname + "%' and antique_license is not null )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
                                }
                                #endregion
                                #region "古物商許可証なし"
                                else if (radioButton2.Checked == true)//古物商許可証なし
                                {
                                    check = 1;
                                    string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name like '%" + iname + "%' and (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                    conn.Open();

                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);

                                    conn.Close();
                                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                    screan = false;
                                    this.Close();
                                    clientMastersearch.Show();
                                }
                                #endregion
                        }
                        #endregion
                }
                #endregion
                #region "氏名なし"
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
                                string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and  address like '%" + iaddress + "%' and antique_license is not null )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                conn.Close();
                                ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                screan = false;
                                this.Close();
                                clientMastersearch.Show();
                            }
                            #endregion
                            #region "古物商許可証なし"
                            else if (radioButton2.Checked == true)//古物商許可証なし
                            {
                                check = 1;
                                string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and address like '%" + iaddress + "%' and (antique_license is null or antique_license = 'なし') )"; //住所部分一致検索
                                conn.Open();

                                adapter = new NpgsqlDataAdapter(sql2, conn);
                                adapter.Fill(dt);

                                conn.Close();
                                ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
                                screan = false;
                                this.Close();
                                clientMastersearch.Show();
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
                            conn.Close();
                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
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
                            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth, Pass);
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
        }


        private void ClientMaster_Load(object sender, EventArgs e)
        {

        }

        private void ClientMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                MasterMaintenanceMenu masterMenu = new MasterMaintenanceMenu(mainMenu, staff_code, access_auth, Pass);
                masterMenu.Show();
            }
        }
    }
}