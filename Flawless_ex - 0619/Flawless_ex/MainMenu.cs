﻿using Npgsql;
using System;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class MainMenu : Form　//メインメニュー
    {
        int type;
        string staff_name;
        string address;
        TopMenu top = new TopMenu();
        string access_auth;
        int staff_id;
        string data;
        string slipNumber;
        decimal Total;
        string Pass;
        public MainMenu(TopMenu topMenu, int id, string pass, string access_auth)
        {
            InitializeComponent();

            this.access_auth = access_auth;
            staff_id = id;
            Pass = pass;
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlCommand cmd;
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str2 = "select* from staff_m where staff_code = " + id + " and password = '" + pass + "'";
            cmd = new NpgsqlCommand(sql_str2, conn);
            conn.Open();

            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                label1.Text = (string.Format("{0}:{1}", reader["staff_code"].ToString(), reader["staff_name"]));
                break;
            }
            conn.Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MasterMainte_Click(object sender, EventArgs e)//権限によって
        {
            MasterMaintenanceMenu masterMenu = new MasterMaintenanceMenu(this, staff_id, access_auth);

            this.Hide();
            masterMenu.Show();

        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            
        }
        #region "計算書・納品書"
        private void Statement_DeliveryButton_Click(object sender, EventArgs e)
        {
            Statement statement = new Statement(this, staff_id, type, staff_name, address, access_auth, Total, Pass);

            this.Hide();
            statement.Show();
        }
        #endregion
        #region "インボイス"
        private void Invoice_Click(object sender, EventArgs e)
        {
            Invoice invoice = new Invoice(this, staff_id);

            this.Hide();
            invoice.Show();
        }
        #endregion
        #region"操作履歴ボタン"
        private void Operatelog_Click(object sender, EventArgs e)
        {
            Operatelog operatelog = new Operatelog(this, staff_id);

            this.Hide();
            operatelog.Show();
        }
        #endregion
        #region "買取販売データ検索ボタン"
        private void CustomerHistoriButton_Click(object sender, EventArgs e)
        {
            CustomerHistorySelect customerHistorySelect = new CustomerHistorySelect(this, staff_id, data, Pass);

            this.Hide();
            customerHistorySelect.Show();
        }
        #endregion
        #region "月間成績表一覧"
        private void MonResults_Click(object sender, EventArgs e)
        {
            MonResult monresult = new MonResult(this, staff_id, access_auth, staff_name, type, slipNumber, Pass);

            this.Hide();
            monresult.Show();
        }
        #endregion
        #region"未入力・次月持ち越し"
        private void Button3_Click(object sender, EventArgs e)
        {
            NextMonth nextmonth = new NextMonth(this, staff_id, Pass, access_auth);

            this.Hide();
            nextmonth.Show();
        }
        #endregion
    }
}
