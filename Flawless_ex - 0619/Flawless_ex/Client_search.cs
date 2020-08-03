using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class client_search : Form
    {
        //Statement statement;
        MainMenu mainMenu;
        DataTable dt = new DataTable();
        public int count = 0;//計算書/納品書の顧客選択用
        int staff_id;
        int type;
        string staff_name;
        string address;
        string Total;
        public client_search(MainMenu mainMenu, int id, int type, string staff_name, string address, string Total)
        {
            InitializeComponent();

            //this.statement = statement;
            this.mainMenu = mainMenu;
            staff_id = id;
            this.type = type;
            this.address = address;
            this.staff_name = staff_name;
            this.Total = Total;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻る
        {
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, Total);
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

            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定


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
                    client_search_result search_Result = new client_search_result(dt, type, check, mainMenu, staff_id, Total);
                    this.Close();
                    search_Result.Show();

                }
                else
                {
                    address = "";

                    string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where invalid = 0 and (type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address = '" + address + "' )";


                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(dt);

                    conn.Close();

                    using (client_search_result search_Result = new client_search_result(dt, type, check, mainMenu, staff_id, Total))
                    {
                        this.Close();
                        search_Result.Show();
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
                        string sql2 = "select name,address, antique_license, id_number, remarks from client_m_individual where invalid = 0 and (type = 1 and  name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " antique_license is not null )"; //住所部分一致検索

                        conn.Open();

                        adapter = new NpgsqlDataAdapter(sql2, conn);
                        adapter.Fill(dt);

                        conn.Close();

                        using (client_search_result search_Result = new client_search_result(dt, type, check, mainMenu, staff_id, Total))
                        {
                            this.Close();
                            search_Result.ShowDialog();
                        }


                    }
                    else if (radioButton2.Checked == true)//古物商許可証なし
                    {
                        check = 1;
                        string sql2 = "select name,address, antique_license, id_number,remarks from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " antique_license is null )"; //住所部分一致検索

                        conn.Open();

                        adapter = new NpgsqlDataAdapter(sql2, conn);
                        adapter.Fill(dt);


                        conn.Close();

                        using (client_search_result search_Result = new client_search_result(dt, type, check, mainMenu, staff_id, Total))
                        {
                            this.Close();
                            search_Result.ShowDialog();
                        }


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
                        string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + "  antique_license is not null )";
                        conn.Open();

                        adapter = new NpgsqlDataAdapter(sql2, conn);
                        adapter.Fill(dt);

                        conn.Close();

                        using (client_search_result search_Result = new client_search_result(dt, type, check, mainMenu, staff_id, Total))
                        {
                            this.Close();
                            search_Result.ShowDialog();
                        }


                    }
                    else if (radioButton2.Checked == true)//古物商許可証なし
                    {
                        check = 1;
                        string sql2 = "select name,address, antique_license, id_number from client_m_individual where invalid = 0 and (type = 1 and name = '" + iname + "' " + search4 + " antique_license is null )";
                        conn.Open();

                        adapter = new NpgsqlDataAdapter(sql2, conn);
                        adapter.Fill(dt);

                        conn.Close();

                        using (client_search_result search_Result = new client_search_result(dt, type, check, mainMenu, staff_id, Total))
                        {
                            this.Close();
                            search_Result.ShowDialog();
                        }


                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            client_add client_Add = new client_add(mainMenu, staff_id, type);
                this.Close();
                client_Add.Show();
            
        }
    }
}
