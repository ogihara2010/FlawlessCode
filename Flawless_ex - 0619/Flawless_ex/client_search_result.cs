﻿using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class client_search_result : Form
    {
        DataTable dt = new DataTable();
        public int type;
        public int check;
        Statement statement;
        MainMenu mainMenu;
        public int staff_id;
        public string address;
        public string staff_name;
        public string shop_name;
        public string name;
        public string company_name;
        public decimal Total;
        public string access_auth;
        public string document;
        public int control;
        public string data;
        public int grade;
        public int ClientCode;
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
        public string pass;

        public client_search_result(DataTable dt, int type, int check, Statement statement, int id, decimal Total, int control, string document, string Access_auth, string Pass) 
        {
            InitializeComponent();

            this.dt = dt;
            this.type = type;
            this.check = check;
            //this.mainMenu = mainMenu;
            this.statement = statement;
            staff_id = id;
            this.Total = Total;
            this.control = control;
            this.document = document;
            this.access_auth = Access_auth;
            this.pass = Pass;
        }

        private void client_search_result_Load(object sender, EventArgs e)
        {
            if (type == 0)
            {
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "会社名";
                dataGridView1.Columns[1].HeaderText = "店舗名";
                dataGridView1.Columns[2].HeaderText = "担当者名";
                dataGridView1.Columns[3].HeaderText = "住所";
                dataGridView1.Columns["code"].Visible = false;
                dataGridView1.Columns["company_kana"].Visible = false;
                dataGridView1.Columns["shop_name_kana"].Visible = false;
            }
            else if (type == 1)
            {

                int count = dt.Rows.Count;

                if (check == 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        dt.Rows[i]["antique_license"] = "登録済み";
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "氏名";
                    dataGridView1.Columns[1].HeaderText = "住所";
                    dataGridView1.Columns[2].HeaderText = "古物商許可証";
                    dataGridView1.Columns["code"].Visible = false;
                    dataGridView1.Columns["name_kana"].Visible = false;
                    dataGridView1.Columns["address_kana"].Visible = false;
                }
                else if (check == 1)
                {
                    for (int i = 0; i < count; i++)
                    {
                        dt.Rows[i]["antique_license"] = "未登録";
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "氏名";
                    dataGridView1.Columns[1].HeaderText = "住所";
                    dataGridView1.Columns[2].HeaderText = "古物商許可証";
                    dataGridView1.Columns["code"].Visible = false;
                    dataGridView1.Columns["name_kana"].Visible = false;
                    dataGridView1.Columns["address_kana"].Visible = false;
                }

            }
        }


        private void returnButton_Click(object sender, EventArgs e)//戻る
        {
            client_search client_Search = new client_search(statement, staff_id, type, staff_name, address, Total, control, document, access_auth, pass);
            //if (type == 0)
            //{
            //    this.company_name = client_Search.clientName;
            //    this.shop_name = client_Search.shopName;
            //    this.staff_name = client_Search.clientStaff;
            //    this.address = client_Search.address;
            //}
            //else if (type == 1)
            //{
            //    this.name = client_Search.iname;
            //    this.address = client_Search.iaddress;
            //    this.check = client_Search.check;
            //}
            //else { }
            this.Close();
            statement.AddOwnedForm(client_Search);
            client_Search.ShowDialog();
        }

        private void selectionButton_Click(object sender, EventArgs e)//選択
        {
            DataTable dt2 = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            if (type == 0)//法人
            {
                ClientCode = (int)dataGridView1.CurrentRow.Cells[4].Value;//選択したPKを取得

                string staff_name = (string)this.dataGridView1.CurrentRow.Cells[2].Value;
                string address = (string)this.dataGridView1.CurrentRow.Cells[3].Value;

                
                if (statement.Visible == true)
                {
                    statement.staff_id = staff_id;
                    statement.ClientCode = ClientCode;
                    statement.client_staff_name = staff_name;
                    statement.address = address;
                    statement.type = 0;
                    //statement.clientDt = dt2;
                    statement.count = 1;
                    this.Close();
                    statement.Show();
                }
                else
                {
                    string sql_str = "select type,company_name,shop_name, staff_name,address, register_date, remarks, code  from client_m where code = '" + ClientCode + "';";
                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt2);

                    conn.Close();

                    statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
                    this.Close();
                    statement.ClientCode = ClientCode;
                    statement.clientDt = dt2;
                    statement.count = 1;
                    statement.Show();
                }

            }
            else if (type == 1)
            {
                ClientCode = (int)dataGridView1.CurrentRow.Cells[3].Value;//選択したPKを取得
                string staff_name = (string)this.dataGridView1.CurrentRow.Cells[0].Value;
                string address = (string)this.dataGridView1.CurrentRow.Cells[1].Value;

                if (statement.Visible == true)
                {
                    statement.staff_id = staff_id;
                    statement.ClientCode = ClientCode;
                    statement.client_staff_name = staff_name;
                    statement.address = address;
                    statement.type = 1;
                    statement.count = 1;
                    this.Close();
                    statement.Show();
                }
                else
                {
                    string sql_str2 = "select type,name, address,register_date, remarks, code from client_m where code = " + ClientCode + "  ";
                    conn.Open();
                    adapter = new NpgsqlDataAdapter(sql_str2, conn);
                    adapter.Fill(dt2);

                    conn.Close();

                    statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
                    this.Close();
                    statement.ClientCode = ClientCode;
                    statement.clientDt = dt2;
                    statement.count = 1;
                    statement.type = 1;
                    statement.Show();
                }
            }
        }
    }
}