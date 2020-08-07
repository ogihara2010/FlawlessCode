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
        DataTable dt = new DataTable();
        int staff_code;
        string access_auth;
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
            this.Close();
            masterMenu.Show();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            ClientMaster_add clientMaster_Add = new ClientMaster_add(masterMenu, staff_code, access_auth);
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
                }
                else
                {
                    clientName = "";
                }

                if (andRadioButton1.Checked == true)
                {
                    search1 = "and";
                }
                else { }

                if (!string.IsNullOrWhiteSpace(shopNameTextBox.Text))
                {
                    shopName = this.shopNameTextBox.Text;
                }
                else
                {
                    shopName = "";
                }

                if (andRadioButton2.Checked == true)
                {
                    search2 = "and";
                }
                else { }

                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                {
                    clientStaff = this.clientStaffNameTextBox.Text;
                }
                else
                {
                    clientStaff = "";
                }

                if (andRadioButton3.Checked == true)
                {
                    search3 = "and";
                }
                else { }

                if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                {
                    address = this.addressTextBox.Text;

                    string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' )"; //住所部分一致検索
                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql2, conn);
                    adapter.Fill(dt);

                    conn.Close();
                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                    this.Close();
                    clientMastersearch.Show();

                }
                else
                {
                    address = "";

                    string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address = '" + address + "' )";
                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt);

                    conn.Close();
                    ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                    this.Close();
                    clientMastersearch.Show();
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
                }
                else
                {
                    iname = "";
                }

                if (andRadioButton4.Checked == true)
                {
                    search4 = "and";
                }



                if (!string.IsNullOrWhiteSpace(iaddressTextBox.Text))
                {
                    iaddress = iaddressTextBox.Text;
                    if (andRadioButton5.Checked == true)
                    {
                        search5 = "and";
                    }

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
                    else if (radioButton2.Checked == true)//古物商許可証なし
                    {
                        check = 1;
                        string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " antique_license is null )"; //住所部分一致検索
                        conn.Open();

                        adapter = new NpgsqlDataAdapter(sql2, conn);
                        adapter.Fill(dt);

                        conn.Close();
                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                        this.Close();
                        clientMastersearch.Show();
                    }
                }
                else
                {
                    iaddress = "";
                    if (andRadioButton5.Checked == true)
                    {
                        search5 = "and";
                    }

                    if (radioButton1.Checked == true)//古物商許可証あり
                    {
                        check = 0;
                        string sql2 = "select name,address, antique_license id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '" + iaddress + "' " + search5 + " antique_license is not null )";
                        conn.Open();

                        adapter = new NpgsqlDataAdapter(sql2, conn);
                        adapter.Fill(dt);

                        conn.Close();
                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                        this.Close();
                        clientMastersearch.Show();
                    }
                    else if (radioButton2.Checked == true)//古物商許可証なし
                    {
                        check = 1;
                        string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '" + iaddress + "' " + search5 + " antique_license is null )";
                        conn.Open();

                        adapter = new NpgsqlDataAdapter(sql2, conn);
                        adapter.Fill(dt);

                        conn.Close();
                        ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu, dt, type, check, staff_code, access_auth);
                        this.Close();
                        clientMastersearch.Show();
                    }
                }


            }





        }


        private void ClientMaster_Load(object sender, EventArgs e)
        {

        }

        private void ClientMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            masterMenu.Show();
        }
    }
}