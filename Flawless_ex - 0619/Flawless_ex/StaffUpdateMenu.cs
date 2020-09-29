using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
using System.Text;

namespace Flawless_ex
{
    public partial class StaffUpdateMenu : Form　//担当者更新メニュー
    {

        MasterMaintenanceMenu master;
        DataTable dt = new DataTable(); //選択した担当者コード全ての情報

        public int staffCode;//担当者コード
        public string staffName;//担当者名
        public string staffNameKana;//担当者名カナ
        public int main_category;//大分類初期値
        public string password;//パスワード
        public string rePassword;//パスワード再入力
        public string access_auth;//アクセス権限
        public string access_auth1; //更新用
        public int code;//ログイン担当者コード
        bool screan = true;
        public string Pass;
        public string kana;

        public StaffUpdateMenu(MasterMaintenanceMenu master, int staff_code, int code, string access_auth, string pass)
        {
            InitializeComponent();

            this.master = master;
            this.staffCode = staff_code;//選択した担当者コード
            this.code = code;
            this.Pass = pass;
            this.access_auth = access_auth;
        }


        private void ReturnButton(object sender, EventArgs e)
        {
            this.Close();
        }

        private void removeButton_Click(object sender, EventArgs e)//無効ボタン
        {
            DialogResult result = MessageBox.Show("無効をしますか？", "確認", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                NpgsqlCommandBuilder builder;
                string reason = this.reason.Text;
                #region "起こりうるミス"
                if (string.IsNullOrEmpty(password))//パスワード未入力
                {
                    if (string.IsNullOrEmpty(rePassword))
                    {
                        MessageBox.Show("パスワードを入力してください");
                        conn.Close();
                        return;
                    }
                }
                else if (this.passwordText.Text != this.passwordReText.Text)
                {
                    MessageBox.Show("パスワードを確認してください。");
                    return;
                }
                else if (string.IsNullOrEmpty(parsonNameText.Text))
                {
                    MessageBox.Show("担当者名を入力して下さい。");
                    return;
                }
                else if (string.IsNullOrEmpty(parsonNamt2Text.Text))
                {
                    MessageBox.Show("担当者のカナを入力して下さい。");
                    return;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(staffNameKana, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                {
                    MessageBox.Show("カタカナを入力して下さい。");
                    return;
                }
                else if (string.IsNullOrEmpty(passwordText.Text))
                {
                    MessageBox.Show("パスワードを入力して下さい。");
                    return;
                }
                else if (string.IsNullOrEmpty(passwordReText.Text))
                {
                    MessageBox.Show("パスワードを再入力して下さい。");
                    return;
                }
                else if (this.accessButton.SelectedIndex == -1)
                {
                    MessageBox.Show("アクセス権限を選択して下さい。");
                    return;
                }
                else if (this.mainCategoryComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("大分類名を選択して下さい。");
                    return;
                }
                else if (string.IsNullOrEmpty(this.reason.Text))
                {
                    MessageBox.Show("理由を入力して下さい。");
                    return;
                }
                else { }
                #endregion


                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                string remove_sql = "update staff_m set invalid = 1, reason = '" + reason + "' where staff_code = " + staffCode + "";

                adapter = new NpgsqlDataAdapter(remove_sql, conn);
                builder = new NpgsqlCommandBuilder(adapter);
                adapter.Fill(dt);
                adapter.Update(dt);

                //履歴
                DateTime dateTime = DateTime.Now;
                string remove_rivisions_sql = "insert into revisions values(" + 1 + ",'" + dateTime + "'," + code + ",'" +　"有効" +　"','" + "無効" + "','" + reason + "');";
                NpgsqlCommand cmd = new NpgsqlCommand(remove_rivisions_sql, conn);
                NpgsqlDataReader dtr = cmd.ExecuteReader();

                conn.Close();
                MessageBox.Show("選択した担当者を無効にしました", "無効処理完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); ;
            }
            else
            {
                return;
            }
            this.Close();
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
            string reason; // 変更理由

            name = staffName;
            staffName = this.parsonNameText.Text;//担当者名
            name_kana = staffNameKana;
            staffNameKana = this.parsonNamt2Text.Text;//担当者名カナ
            main_code = main_category;
            main_category = (int)this.mainCategoryComboBox.SelectedValue;//大分類初期値
            pass = password;
            password = this.passwordText.Text;//パスワード
            rePassword = this.passwordReText.Text;//パスワード再入力
            access = access_auth1;
            access_auth1 = this.accessButton.Text;//アクセス権限
            reason = this.reason.Text;

            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();
            string sql_str = " update staff_m set  staff_name = '" + staffName + "', staff_name_kana = '" + staffNameKana + "', main_category_code =" + main_category + ",password = '" + password + "', access_auth = '" + access_auth1 + "' , reason = '" + reason +"' where staff_code =" + staffCode + " ;";


            #region "起こりうるミス"
            if (string.IsNullOrEmpty(password))//パスワード未入力
            {
                if (string.IsNullOrEmpty(rePassword))
                {
                    MessageBox.Show("パスワードを入力してください");
                    conn.Close();
                    return;
                }
            }
            else if (password != rePassword)
            {
                MessageBox.Show("パスワードを確認して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(parsonNameText.Text))
            {
                MessageBox.Show("担当者名を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(parsonNamt2Text.Text))
            {
                MessageBox.Show("担当者のカナを入力して下さい。");
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(staffNameKana, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            {
                MessageBox.Show("カタカナを入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(passwordText.Text))
            {
                MessageBox.Show("パスワードを入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(passwordReText.Text))
            {
                MessageBox.Show("パスワードを再入力して下さい。");
                return;
            }
            else if (this.accessButton.SelectedIndex == -1)
            {
                MessageBox.Show("アクセス権限を選択して下さい。");
                return;
            }
            else if (this.mainCategoryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("大分類名を選択して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(this.reason.Text))
            {
                MessageBox.Show("理由を入力して下さい。");
                return;
            }
            else { }
            #endregion

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            builder = new NpgsqlCommandBuilder(adapter);

            adapter.Fill(dt);
            adapter.Update(dt);
            MessageBox.Show("担当者の設定を変更しました", "変更完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) ;

            //履歴
            DateTime dateTime = DateTime.Now;
            #region "担当者名変更"
            if (this.parsonNameText.Text != name)
            {
                string sql_staffNameRevisions = "insert into revisions values(" + 1 + ",'" + dateTime + "'," + code + ",'" + name + "','" + staffName + "','" + reason  + "');";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_staffNameRevisions, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            #endregion
            #region "パスワード"
            else if (this.passwordText.Text != pass)
            {
                string sql_passwordRevisions = "insert into revisions values(" + 1 + ",'" + dateTime + "', " + code + ",'" + pass + "','" + password + "','" + reason + "')";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_passwordRevisions, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            #endregion
            #region "大分類コード"
            else if (main_category != main_code)
            {
                string sql_main_category_code_revisions = "insert into revisions values(" + 1 + ",'" + dateTime + "'," + code + "," + main_category + ", " + main_code + ",'" + reason + "')";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_main_category_code_revisions, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            #endregion
            #region "アクセス権限"
            else if (access_auth1 != access)
            {
                string sql_access_revisions = "insert into revisions values(" + 1 + ",'" + dateTime + "', " + code + ", '" + access + "', '" + access_auth1 + "','" + reason + "')";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_access_revisions, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            #endregion
            this.Close();
        }

        private void UpdateMenu_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            DataTable dt = new DataTable();
            DataRow row;
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select * from staff_m where staff_code = " + staffCode + ";";

            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            row = dt.Rows[0];
            staffName = row["staff_name"].ToString();
            staffNameKana = row["staff_name_kana"].ToString();
            main_category = (int)row["main_category_code"];
            password = row["password"].ToString();
            access_auth1 = row["access_auth"].ToString();

            //担当者ごとの大分類の初期値を先頭に
            DataTable dt1 = new DataTable();
            string sql_str2 = "select * from main_category_m order by main_category_code = " + main_category + "desc;";
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
            this.accessButton.Text = access_auth1;

            conn.Close();
        }

        private void StaffUpdateMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            StaffMaster staffMaster = new StaffMaster(master, code, Pass, access_auth);
            staffMaster.Show();
        }

        private void parsonNamt2Text_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(parsonNamt2Text.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            parsonNamt2Text.Text = stringBuilder.ToString();
            parsonNamt2Text.Select(parsonNamt2Text.Text.Length, 0);
        }

    }
}
