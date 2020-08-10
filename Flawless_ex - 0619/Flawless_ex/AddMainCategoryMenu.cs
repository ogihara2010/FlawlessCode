using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class AddMainCategoryMenu : Form//大分類　新規登録メニュー
    {
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        NpgsqlTransaction transaction;
        NpgsqlDataReader reader;
        NpgsqlCommand cmd;

        MasterMaintenanceMenu master;
        DataTable dt;
        int staff_code;
        string Access_auth;
        string Pass;
        string MainName;

        public AddMainCategoryMenu(DataTable dt, MasterMaintenanceMenu master, int staff_code, string access_auth, string pass)
        {
            InitializeComponent();

            this.master = master;
            this.dt = dt;
            this.staff_code = staff_code;
            this.Access_auth = access_auth;
            this.Pass = pass;
        }

        private void AddMainCategoryMenu_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            //大分類コード取得
            string staffCode_sql = "select main_category_code from main_category_m order by main_category_code desc";
            DataTable mcategoryCode = new DataTable();
            adapter = new NpgsqlDataAdapter(staffCode_sql, conn);
            adapter.Fill(mcategoryCode);

            conn.Close();

            //大分類コード最大値取得＆新規番号作成
            DataRow row;
            row = mcategoryCode.Rows[0];
            int code = (int)row["main_category_code"];
            code++;
            mainCategoryCodeTextBox.Text = code.ToString();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mainCategoryNameTextBox.Text))
            {
                MessageBox.Show("大分類名が未入力です。", "登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("登録をしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes && !(string.IsNullOrEmpty(mainCategoryNameTextBox.Text)))
            {
                NpgsqlCommandBuilder builder;

                int mainCode = int.Parse(mainCategoryCodeTextBox.Text);//大分類コード
                string mainName = mainCategoryNameTextBox.Text;//大分類名
                DateTime dat = DateTime.Now;
                dt = new DataTable();

                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                using (transaction = conn.BeginTransaction())
                {
                    string SQL = "select * from main_category_m;";
                    cmd = new NpgsqlCommand(SQL, conn);
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MainName = reader["main_category_name"].ToString();
                            if (MainName == mainName)
                            {
                                MessageBox.Show("既に登録されている大分類名です", "登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                conn.Close();
                                mainCategoryNameTextBox.Text = "";
                                return;
                            }
                        }
                    }
                }


                string sql_str = "insert into main_category_m values(" + mainCode + ",'" + mainName + "', '" + dat + "',0," + staff_code + ")";

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);

                adapter.Fill(dt);
                adapter.Update(dt);

                conn.Close();

                MessageBox.Show("大分類名を新規登録しました", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); 
                this.Close();
            }
        }

        private void AddMainCategoryMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainCategoryMaster mainCategory = new MainCategoryMaster(master, staff_code, Access_auth, Pass);
            mainCategory.Show();
        }
    }
}
