using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class StaffUpdateMenu : Form　//担当者更新メニュー
    {

        MasterMaintenanceMenu master;
        DataTable dt = new DataTable(); //選択した担当者コード全ての情報

        int staffCode;//担当者コード
        string staffName;//担当者名
        string staffNameKana;//担当者名カナ
        int main_category;//大分類初期値
        string password;//パスワード
        string rePassword;//パスワード再入力
        string access_auth;//アクセス権限
        int code;//ログイン担当者コード
        public StaffUpdateMenu(MasterMaintenanceMenu master, int staff_code, int code)
        {
            InitializeComponent();


            this.master = master;
            this.staffCode = staff_code;//選択した担当者コード
            this.code = code;
        }


        private void ReturnButton(object sender, EventArgs e)
        {
            StaffMaster personMaster = new StaffMaster(master, staffCode);//注意

            this.Close();
            personMaster.Show();
        }

        private void removeButton_Click(object sender, EventArgs e)//無効ボタン
        {
            DialogResult result = MessageBox.Show("無効をしますか？", "確認", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                NpgsqlCommandBuilder builder;


                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                string remove_sql = "update staff_m set invalid = 1 where staff_code = " + staffCode + "";

                adapter = new NpgsqlDataAdapter(remove_sql, conn);
                builder = new NpgsqlCommandBuilder(adapter);
                adapter.Fill(dt);
                adapter.Update(dt);

                //履歴
                DateTime dateTime = DateTime.Now;
                string remove_rivisions_sql = "insert into staff_m_revisions_invalid values(" + staffCode + ",'" + dateTime + "'," + code + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(remove_rivisions_sql, conn);
                NpgsqlDataReader dtr = cmd.ExecuteReader();

                conn.Close();
                MessageBox.Show("無効完了");
            }
            else
            {

            }

            StaffMaster staffMaster = new StaffMaster(master, staffCode);
            this.Close();
            staffMaster.Show();
        }

        private void updateButton_Click(object sender, EventArgs e)//更新ボタン
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            NpgsqlCommandBuilder builder;
            DataTable dt = new DataTable();

            string name; //担当者名　履歴
            string name_kana;//担当者名カナ履歴
            string pass;//パスワード　履歴
            int main_code;//大分類　履歴
            string access;//アクセス権限履歴

            name = staffName;
            staffName = this.parsonNameText.Text;//担当者名
            name_kana = staffNameKana;
            staffNameKana = this.parsonNamt2Text.Text;//担当者名カナ
            main_code = main_category;
            main_category = (int)this.mainCategoryComboBox.SelectedValue;//大分類初期値
            pass = password;
            password = this.passwordText.Text;//パスワード
            rePassword = this.passwordReText.Text;//パスワード再入力
            access = access_auth;
            access_auth = this.accessButton.Text;//アクセス権限

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();
            string sql_str = " update staff_m set  staff_name = '" + staffName + "', staff_name_kana = '" + staffNameKana + "', main_category_code =" + main_category + ",password = '" + password + "', access_auth = '" + access_auth + "' where staff_code =" + staffCode + " ";



            if (string.IsNullOrEmpty(password))//パスワード未入力
            {
                if (string.IsNullOrEmpty(rePassword))
                {
                    MessageBox.Show("パスワードを入力してください");
                    conn.Close();
                    return;
                }
            }
            else { }

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            builder = new NpgsqlCommandBuilder(adapter);

            adapter.Fill(dt);
            adapter.Update(dt);
            MessageBox.Show("更新完了");

            //履歴
            DateTime dateTime = DateTime.Now;
            //担当者名
            if (this.parsonNameText.Text != name)
            {
                string sql_staffNameRevisions = "insert into staff_name_revisions values('" + dateTime + "'," + staffCode + ",'" + name + "','" + name_kana + "','" + staffName + "', '" + staffNameKana + "'," + code + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_staffNameRevisions, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            //パスワード
            else if (this.passwordText.Text != pass)
            {
                string sql_passwordRevisions = "insert into staff_password_revisions values('" + dateTime + "', " + staffCode + ",'" + pass + "','" + password + "', " + code + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_passwordRevisions, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            //大分類コード
            else if (main_category != main_code)
            {
                string sql_main_category_code_revisions = "insert into staff_main_category_code_revisions values('" + dateTime + "'," + staffCode + "," + main_code + ", " + main_category + ", " + code + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_main_category_code_revisions, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            //アクセス権限
            else if (access_auth != access)
            {
                string sql_access_revisions = "insert into staff_access_auth_revisions values('" + dateTime + "', " + staffCode + ", '" + access + "', '" + access_auth + "', " + code + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_access_revisions, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }

            StaffMaster staffMaster = new StaffMaster(master, staffCode);

            this.Close();
            staffMaster.Show();

        }

        private void UpdateMenu_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            DataTable dt = new DataTable();
            DataRow row;
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select* from staff_m where staff_code = " + staffCode + "";

            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            row = dt.Rows[0];
            staffName = row["staff_name"].ToString();
            staffNameKana = row["staff_name_kana"].ToString();
            main_category = (int)row["main_category_code"];
            password = row["password"].ToString();
            access_auth = row["access_auth"].ToString();

            //担当者ごとの大分類の初期値を先頭に
            DataTable dt1 = new DataTable();
            string sql_str2 = "select* from main_category_m order by main_category_code = " + main_category + "desc;";
            adapter = new NpgsqlDataAdapter(sql_str2, conn);
            adapter.Fill(dt1);

            mainCategoryComboBox.DataSource = dt1;
            mainCategoryComboBox.DisplayMember = "main_category_name";
            mainCategoryComboBox.ValueMember = "main_category_code";
            mainCategoryComboBox.SelectedIndex = 0;

            this.parsonCodeText.Text = staffCode.ToString();
            this.parsonNameText.Text = staffName;
            this.parsonNamt2Text.Text = staffNameKana;
            this.passwordText.Text = password;
            this.accessButton.Text = access_auth;

            conn.Close();
        }
    }
}
