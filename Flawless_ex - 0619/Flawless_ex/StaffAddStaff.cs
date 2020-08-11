using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class StaffAddStaff : Form　//担当者新規登録メニュー
    {
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        MasterMaintenanceMenu master;
        DataTable dt;
        int staff_code;
        bool screan = true;
        string Pass;
        string access_auth;

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
            StaffMaster staffMaster = new StaffMaster(master, staff_code, access_auth, Pass);
            screan = false;
            this.Close();
            staffMaster.Show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("登録をしますか？", "確認", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                NpgsqlCommandBuilder builder;

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
                #endregion

                string sql_str = "insert into staff_m values(" + staffCode1 + " , '" + staffName + "', '" + staffNameKana + "'," + mainCategoryCode + ",'" + password + "', '" + access_auth + "','" + d + "'," + 0 + ")";


                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);


                adapter.Fill(dt);
                adapter.Update(dt);

                //履歴
                //string staff_revisions = "insert into staff_m_revisions values(" + staffCode1 + ",'" + d + "'," + staff_code + ")";
                //NpgsqlCommand cmd = new NpgsqlCommand(staff_revisions, conn);
                //NpgsqlDataReader sdr = cmd.ExecuteReader();


                conn.Close();

                MessageBox.Show("登録完了");


                StaffMaster staffMaster = new StaffMaster(master, staff_code, access_auth, Pass);
                screan = false;
                this.Close();
                staffMaster.Show();
            }
            else { }

        }

        private void StaffAddStaff_Load(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();

            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            string sql_str2 = "select* from main_category_m where invalid = 0";
            adapter = new NpgsqlDataAdapter(sql_str2, conn);
            adapter.Fill(dt2);

            //担当者コード取得
            string staffCode_sql = "select staff_code from staff_m order by staff_code desc";
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

        /*private void StaffAddStaff_FormClosing(object sender, FormClosingEventArgs e)
        {
            StaffMaster staffMaster = new StaffMaster(master, staff_code);
            staffMaster.Show();
        }*/

        private void StaffAddStaff_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                StaffMaster staffMaster = new StaffMaster(master, staff_code, access_auth, Pass);
                staffMaster.Show();
            }
        }
    }
}
