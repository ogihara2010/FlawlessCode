using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
using System.Text;

namespace Flawless_ex
{
    public partial class StaffAddStaff : Form　//担当者新規登録メニュー
    {
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        MasterMaintenanceMenu master;
        DataTable dt;
        public int staff_code;
        bool screan = true;
        public string Pass;
        public string access_auth;
        public string kana;

        public StaffAddStaff(DataTable dt, MasterMaintenanceMenu master, int staff_code, string pass, string access_auth)
        {
            InitializeComponent();

            this.dt = dt;
            this.master = master;
            this.staff_code = staff_code;
            this.Pass = pass;
            this.access_auth = access_auth;
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            dt.Clear();

            int staffCode1 = int.Parse(parsonCodeText.Text);//担当者コード
            string staffName = this.parsonNameText.Text;//担当者名
            string staffNameKana = this.parsonNamt2Text.Text;//担当者名カナ
            int mainCategoryCode = (int)this.mainCategoryComboBox.SelectedValue; //大分類初期値
            string password = this.passwordText.Text;//パスワード
            string rePassword = this.passwordReText.Text;//パスワード再入力
            string access_auth = this.accessButton.Text;//アクセス権限
            DateTime dat = DateTime.Now;
            string d = dat.ToString();//登録日時

            #region "起こりうるミス"
            if (password != rePassword)
            {
                MessageBox.Show("パスワードを確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(parsonNameText.Text))
            {
                MessageBox.Show("担当者名を入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(parsonNamt2Text.Text))
            {
                MessageBox.Show("担当者のカナを入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(staffNameKana, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            {
                MessageBox.Show("カタカナを入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(passwordText.Text))
            {
                MessageBox.Show("パスワードを入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(passwordReText.Text))
            {
                MessageBox.Show("パスワードを再入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (this.accessButton.SelectedIndex == -1)
            {
                MessageBox.Show("アクセス権限を選択して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (this.mainCategoryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("大分類名を選択して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            DialogResult result = MessageBox.Show("登録をしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                NpgsqlCommandBuilder builder;


                string sql_str = "insert into staff_m values(" + staffCode1 + " , '" + staffName + "', '" + staffNameKana + "'," + mainCategoryCode + ",'" + password + "', '" + access_auth + "','" + d + "'," + 0 + ")";

                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);

                adapter.Fill(dt);
                adapter.Update(dt);

                conn.Close();

                MessageBox.Show("担当者を登録しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) ;

                this.Close();
            }
            else { }

        }

        private void StaffAddStaff_Load(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            conn.Open();

            string sql_str2 = "select * from main_category_m where invalid = 0 order by main_category_code;";
            adapter = new NpgsqlDataAdapter(sql_str2, conn);
            adapter.Fill(dt2);

            //担当者コード取得
            string staffCode_sql = "select staff_code from staff_m order by staff_code desc;";
            DataTable staffCode = new DataTable();
            adapter = new NpgsqlDataAdapter(staffCode_sql, conn);
            adapter.Fill(staffCode);

            conn.Close();

            mainCategoryComboBox.DataSource = dt2;
            mainCategoryComboBox.DisplayMember = "main_category_name";
            mainCategoryComboBox.ValueMember = "main_category_code";

            //担当者コード最大値取得＆新規番号作成
            DataRow row;
            row = staffCode.Rows[0];
            int code = (int)row["staff_code"];
            code++;
            parsonCodeText.Text = code.ToString();
        }

        private void StaffAddStaff_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                StaffMaster staffMaster = new StaffMaster(master, staff_code, Pass, access_auth);
                staffMaster.Show();
            }
            else
            {
                screan = true;
            }
        }

        private void parsonNamt2Text_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(parsonNamt2Text.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            parsonNamt2Text.Text = stringBuilder.ToString();
            parsonNamt2Text.Select(parsonNamt2Text.Text.Length, 0);
        }

        private void passwordText_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordText.Text))
            {
                label6.Visible = true;
                passwordReText.Visible = true;
            }
            else
            {
                label6.Visible = false;
                passwordReText.Visible = false;
            }
        }
    }
}
