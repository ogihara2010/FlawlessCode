using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Flawless_ex
{
    public partial class TopMenu : Form　//ログインメニュー
    {
        public TopMenu()
        {
            InitializeComponent();
        }


        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void roginButton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlCommand cmd = new NpgsqlCommand();
            NpgsqlDataAdapter adapter;
            DataTable dt = new DataTable();
            //TopMenu topMenu = new TopMenu();

            int id = int.Parse(this.roginIdTextBox.Text);
            string password = this.passwordTextBox.Text;


            string sql_str = "select* from staff_m where staff_code = " + id + " and password = '" + password + "' ";
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();
            

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            int count = dt.Rows.Count;



            if (count == 1)
            {
                DataRow row;
                row = dt.Rows[0];
                string access_auth = row["access_auth"].ToString();

                MainMenu mainMenu = new MainMenu(this, id, password, access_auth);
                this.Hide();
                mainMenu.Show();

                roginIdTextBox.Text = "";
                passwordTextBox.Text = "";

            }
            else if (passwordTextBox.Text == string.Empty)
            {
                MessageBox.Show("パスワードを入力してください。");
            }
            else
            {
                MessageBox.Show("担当者コード,パスワードが違います");
            }

            conn.Close();

        }
    }
}
