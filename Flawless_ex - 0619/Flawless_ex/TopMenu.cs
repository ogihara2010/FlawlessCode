using Npgsql;
using System;
using System.Data;
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
            int id = 0;
            int count;
            int invalid;
            string pass;



            if (!string.IsNullOrWhiteSpace(roginIdTextBox.Text) && !string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                var result = 0;
                var ret = int.TryParse(roginIdTextBox.Text, out result);
                if (ret == true)
                {
                    id = int.Parse(roginIdTextBox.Text);
                }
                else
                {
                    MessageBox.Show("担当者コード、パスワードが違います");
                    return;
                }

                string password = this.passwordTextBox.Text;

                if (id == 0)
                {

                }
                else
                {
                    string sql_str = "select* from staff_m where staff_code = " + id + " ";
                    conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                    conn.Open();


                    //検索結果の数
                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt);
                    count = dt.Rows.Count;

                    if (count == 0)
                    {
                        MessageBox.Show("担当者コード、パスワードが違います");
                        roginIdTextBox.Text = "";
                        passwordTextBox.Text = "";
                        return;
                    }
                    else { }

                    DataRow row2;
                    row2 = dt.Rows[0];
                    invalid = (int)row2["invalid"];
                    pass = row2["password"].ToString();

                    if (count == 1 && password == pass && invalid == 0)
                    {
                        DataRow row;
                        row = dt.Rows[0];
                        string access_auth = row["access_auth"].ToString();

                        MainMenu mainMenu = new MainMenu(this, id, password, access_auth);
                        this.Hide();
                        mainMenu.Show();

                    }
                    else
                    {
                        MessageBox.Show("担当者コード、パスワードが違います");

                    }
                    conn.Close();

                    roginIdTextBox.Text = "";
                    passwordTextBox.Text = "";
                }





            }
            else if (!string.IsNullOrWhiteSpace(roginIdTextBox.Text))
            {
                MessageBox.Show("パスワードを入力してください");
            }
            else if (!string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("担当者コードを入力してください");
            }
            else
            {
                MessageBox.Show("担当者コード、パスワードを入力してください");
            }

        }
    }
}
