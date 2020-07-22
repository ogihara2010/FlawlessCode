﻿using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace Flawless_ex
{
    public partial class DataSearchResults : Form
    {
        CustomerHistory customerHistory;
        MainMenu mainMenu;
        int type;
        string name1;
        string phoneNumber1;
        string address1;
        string address;
        string item1;
        string search1;
        string search2;
        string search3;
        string staff_name;
        int staff_id;
        DataTable dt = new DataTable();
        public DataSearchResults(CustomerHistory customer, int type, int id, string name1, string phoneNumber1, string address1, string item1, string search1, string search2, string search3)
        {
            InitializeComponent();
            customerHistory = customer;
            this.type = type;
            this.name1 = name1;
            this.phoneNumber1 = phoneNumber1;
            this.address1 = address1;
            this.item1 = item1;
            this.search1 = search1;
            this.search2 = search2;
            this.search3 = search3;
            staff_id = id;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻るボタン
        {

            this.Close();
            customerHistory.Show();

        }

        private void DataSearchResults_Load(object sender, EventArgs e)
        {
            if (type == 0)
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                                 "inner join statement_calc C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                 " where B.invalid = 0 and B.shop_name = '" + name1 + "' " + search1 + " B.phone_number = '" + phoneNumber1 + "'" + " " + search2 + " B.address like '% " + address1 + " %' "  + search3 + " D.item_name = '" + item1 + "';";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "決済日";
                dataGridView1.Columns[1].HeaderText = "受渡日";
                dataGridView1.Columns[2].HeaderText = "店舗名";
                dataGridView1.Columns[3].HeaderText = "担当者名・個人名";
                dataGridView1.Columns[4].HeaderText = "電話番号";
                dataGridView1.Columns[5].HeaderText = "住所";
                dataGridView1.Columns[6].HeaderText = "品名";
                dataGridView1.Columns[7].HeaderText = "金額";

                conn.Close();
            }
            if (type == 1)
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                             "inner join statement_calc C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                 "where B.invalid = 0 and B.name = '" + name1 + "' " + search1 + " B.phone_number = '" + phoneNumber1 + "'" + " " + search2 + " B.address like '% " + address1 + " %' " + search3 + " D.item_name = '" + item1 + "';";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "決済日";
                dataGridView1.Columns[1].HeaderText = "受渡日";
                dataGridView1.Columns[2].HeaderText = "氏名";
                dataGridView1.Columns[3].HeaderText = "電話番号";
                dataGridView1.Columns[4].HeaderText = "住所";
                dataGridView1.Columns[5].HeaderText = "品名";
                dataGridView1.Columns[6].HeaderText = "金額";

                conn.Close();
            }

            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address);
            this.Close();
            statement.Show();
        }
    }
}
