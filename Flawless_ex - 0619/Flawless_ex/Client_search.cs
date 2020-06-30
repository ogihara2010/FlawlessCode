﻿using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class client_search : Form
    {
        Statement statement;
        DataTable dt = new DataTable();
        public int count = 0;//計算書/納品書の顧客選択用

        public client_search(Statement statement)
        {
            InitializeComponent();

            this.statement = statement;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻る
        {
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

                    string sql2 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' "; //住所部分一致検索
                    string sql21 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where shop_name = '" + shopName + "' " + search2 + " address like '%" + address + "%' "; //住所部分一致検索and店舗名・住所
                    string sql22 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "' " + search3 + " address like '%" + address + "%' "; //住所部分一致検索and会社名・担当者名・住所
                    string sql23 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where company_name = '" + clientName + "' " + search1 + "  address like '%" + address + "%' "; //住所部分一致検索and会社名・住所

                    conn.Open();

                    if (!string.IsNullOrWhiteSpace(address))
                    {
                        if (!string.IsNullOrWhiteSpace(shopName))
                        {
                            adapter = new NpgsqlDataAdapter(sql21, conn);
                            adapter.Fill(dt);
                        }
                        else
                        {
                        }

                        if (!string.IsNullOrWhiteSpace(clientName))
                        {
                            adapter = new NpgsqlDataAdapter(sql23, conn);
                            adapter.Fill(dt);
                        }

                        if (!string.IsNullOrWhiteSpace(clientName))
                        {
                            if (!string.IsNullOrWhiteSpace(shopName))
                            {
                                if (!string.IsNullOrWhiteSpace(clientStaff))
                                {
                                    adapter = new NpgsqlDataAdapter(sql2, conn);
                                    adapter.Fill(dt);
                                }
                            }
                            else if (!string.IsNullOrWhiteSpace(clientStaff))
                            {
                                adapter = new NpgsqlDataAdapter(sql22, conn);
                                adapter.Fill(dt);
                            }
                        }



                        if (!string.IsNullOrWhiteSpace(clientStaff))
                        {
                            adapter = new NpgsqlDataAdapter(sql2, conn);
                            adapter.Fill(dt);
                        }


                    }
                    else { }

                    conn.Close();

                    using (client_search_result search_Result = new client_search_result(dt, type, check, statement))
                    {
                        this.Close();
                        search_Result.ShowDialog();
                    }



                }
                else
                {
                    address = "";

                    string sql = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address = '" + address + "' ";
                    string sql1 = "select company_name, shop_name, staff_name, address, antique_number from client_m_corporate where type = 0 and company_name = '" + clientName + "' " + search1 + " staff_name = '" + clientStaff + "'";//会社名・担当者名
              

                    conn.Open();

                    if (string.IsNullOrWhiteSpace(address))
                    {
                        if (!string.IsNullOrWhiteSpace(clientName))
                        {
                            if (!string.IsNullOrWhiteSpace(clientStaff))
                            {
                                adapter = new NpgsqlDataAdapter(sql1, conn);
                                adapter.Fill(dt);
                            }

                            if (!string.IsNullOrWhiteSpace(shopName))
                            {
                                if (!string.IsNullOrWhiteSpace(clientStaff))
                                {
                                    adapter = new NpgsqlDataAdapter(sql, conn);
                                    adapter.Fill(dt);
                                }
                            }
                        }
                        else 
                        {
                            adapter = new NpgsqlDataAdapter(sql, conn);
                            adapter.Fill(dt);
                        }

                        if (!string.IsNullOrWhiteSpace(shopName))
                        {
                            if (!string.IsNullOrWhiteSpace(clientStaff))
                            {
                                adapter = new NpgsqlDataAdapter(sql, conn);
                                adapter.Fill(dt);
                            }
                        }

                        

                    }
                    else { }

                    conn.Close();

                    using (client_search_result search_Result = new client_search_result(dt, type, check, statement))
                    {
                        this.Close();
                        search_Result.ShowDialog();
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
                        string sql2 = "select name,address, antique_license, id_number, remarks from client_m_individual where name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " antique_license is not null "; //住所部分一致検索

                        conn.Open();

                        adapter = new NpgsqlDataAdapter(sql2, conn);
                        adapter.Fill(dt);

                        conn.Close();

                        using (client_search_result search_Result = new client_search_result(dt, type, check, statement))
                        {
                            this.Close();
                            search_Result.ShowDialog();
                        }


                    }
                    else if (radioButton2.Checked == true)//古物商許可証なし
                    {
                        check = 1;
                        string sql2 = "select name,address, antique_license, id_number,remarks from client_m_individual where name = '" + iname + "' " + search4 + " address like '%" + iaddress + "%' " + search5 + " antique_license is null "; //住所部分一致検索

                        conn.Open();

                        adapter = new NpgsqlDataAdapter(sql2, conn);
                        adapter.Fill(dt);


                        conn.Close();

                        using (client_search_result search_Result = new client_search_result(dt, type, check, statement))
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
                        string sql2 = "select name,address, antique_license, id_number from client_m_individual where name = '" + iname + "' " + search4 + "  antique_license is not null ";
                        conn.Open();

                        adapter = new NpgsqlDataAdapter(sql2, conn);
                        adapter.Fill(dt);

                        conn.Close();

                        using (client_search_result search_Result = new client_search_result(dt, type, check, statement))
                        {
                            this.Close();
                            search_Result.ShowDialog();
                        }


                    }
                    else if (radioButton2.Checked == true)//古物商許可証なし
                    {
                        check = 1;
                        string sql2 = "select name,address, antique_license, id_number from client_m_individual where name = '" + iname + "' " + search4 + " antique_license is null ";
                        conn.Open();

                        adapter = new NpgsqlDataAdapter(sql2, conn);
                        adapter.Fill(dt);

                        conn.Close();

                        using (client_search_result search_Result = new client_search_result(dt, type, check, statement))
                        {
                            this.Close();
                            search_Result.ShowDialog();
                        }


                    }
                }


            }
        }
    }
}
