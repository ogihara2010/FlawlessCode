using Npgsql;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Windows.Forms;

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

            int id = int.Parse(this.roginIdTextBox.Text);
            string password = this.passwordTextBox.Text;


            string sql_str = "select* from staff_m where staff_code = " + id + " ";
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            
            //検索結果の数
            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            int count = dt.Rows.Count;

            DataRow row2;
            row2 = dt.Rows[0];
            int invalid = (int)row2["invalid"];
            string pass = row2["password"].ToString();

            if (count == 1 && password == pass)
            {
                

                if (invalid != 0)
                {
                    
                }
                else
                {
                    DataRow row;
                    row = dt.Rows[0];
                    string access_auth = row["access_auth"].ToString();

                    MainMenu mainMenu = new MainMenu(this, id, password, access_auth);
                    this.Hide();
                    mainMenu.Show();
                }

            }
            else { }
            if (passwordTextBox.Text == string.Empty)
            {
                MessageBox.Show("パスワードを入力してください。");
            }
            else　if(pass != password)
            {
                MessageBox.Show("担当者コード,パスワードが違います");
            }

            conn.Close();

            roginIdTextBox.Text = "";
            passwordTextBox.Text = "";

        }

        private void TopMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
