﻿using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace Flawless_ex
{
    public partial class NextMonth : Form
    {
        int staff_id;
        MainMenu mainMenu;
        Statement statement;
        string staff_name;
        int type;
        string slipNumber;
        int Grade;
        DataTable dt = new DataTable();
        string Pass;
        TopMenu top;
        string Access_auth;
        int antique;
        int id;
        bool NameChange;

        public NextMonth(MainMenu main, int id, string pass, string access_auth)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            Pass = pass;
            Access_auth = access_auth;
        }

        private void Return4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //選択ボタンクリック時
        private void Choice2_Click(object sender, EventArgs e)
        {
            string slipNumber = (string)dataGridView1.CurrentRow.Cells[0].Value;
            Grade = (int)dataGridView1.CurrentRow.Cells[1].Value;
            string staff_name = (string)dataGridView1.CurrentRow.Cells[5].Value;
            
            RecordList recordList = new RecordList(statement, staff_id, staff_name, type, slipNumber, Grade, antique, id, Access_auth, Pass, NameChange);
            this.Hide();
            recordList.Show();
        }

        private void NextMonth_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select A.document_number, A.grade_number, A.assessment_date, B.delivery_method, B.payment_method, C.staff_name, A.buyer from list_result2 A " +
                             "inner join statement_data B ON (A.document_number = B.document_number ) " +
                             "inner join staff_m C ON (B.staff_code = C.staff_code ) where carry_over_month = '" + 1 + "';";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "伝票番号";
            dataGridView1.Columns[1].HeaderText = "成績番号";
            dataGridView1.Columns[2].HeaderText = "査定日";
            dataGridView1.Columns[3].HeaderText = "受渡方法";
            dataGridView1.Columns[4].HeaderText = "決済方法";
            dataGridView1.Columns[5].HeaderText = "担当者";
            dataGridView1.Columns[6].HeaderText = "買取先";

            conn.Close();
        }

        private void NextMonth_FormClosed(object sender, EventArgs e)
        {
            mainMenu = new MainMenu(top, staff_id, Pass, Access_auth);
            mainMenu.Show();
        }
    }
}