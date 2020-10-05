using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class UpdateMainCategoryMenu : Form//大分類更新メニュー
    {
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable(); //選択した大分類コード全ての情報

        public int mainCode; //大分類コード
        public string mainName; //大分類名
        public int staff_code;//ログイン者の担当者コード
        public string mName;
        public string Access_auth;
        public string Pass;
        public string reason;

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlDataAdapter adapter;
        DataRow row;
        NpgsqlCommandBuilder builder;
        NpgsqlTransaction transaction;
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        NpgsqlDataAdapter adapter2;

        public UpdateMainCategoryMenu(MasterMaintenanceMenu master, int code, int staff_code, string access_auth, string pass)
        {
            InitializeComponent();

            this.master = master;
            this.mainCode = code;
            this.staff_code = staff_code;
            this.Access_auth = access_auth;
            this.Pass = pass;
        }

        private void UpdateMainCategoryMenu_Load(object sender, EventArgs e)
        {
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            string sql_str = "select* from main_category_m where main_category_code = " + mainCode + "";

            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            row = dt.Rows[0];
            mainName = row["main_category_name"].ToString();

            this.mainCategoryCodeTextBox.Text = mainCode.ToString();
            this.mainCategoryNameTextBox.Text = mainName;
            mName = mainName;

            conn.Close();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mainCategoryNameTextBox.Text))
            {
                MessageBox.Show("大分類名が入力されておりません。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(reasonText.Text))
            {
                MessageBox.Show("選択した大分類名を無効にする理由を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("無効にしますか？", "確認", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                MessageBox.Show("大分類マスタ一覧に戻ります", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            //大分類名・理由入力済みかつYes
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            conn.Open();

            using (transaction = conn.BeginTransaction())
            {
                string sql_str = "update main_category_m set invalid = 1 where main_category_code = " + mainCode + ";";
                cmd = new NpgsqlCommand(sql_str, conn);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            
            //無効履歴
            DateTime dat = DateTime.Now;
            reason = reasonText.Text;            
            string sql_mCategoryRevisions = "insert into revisions values(" + 3 + ",'" + dat + "'," + staff_code + ",'" + "有効" + "', '" + "無効" + "','" + reason + "');";
            cmd = new NpgsqlCommand(sql_mCategoryRevisions, conn);
            reader = cmd.ExecuteReader();
            
            MessageBox.Show("選択された大分類名を無効にしました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) ;
            
            conn.Close();
            this.Close();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mainCategoryNameTextBox.Text))
            {
                MessageBox.Show("大分類名が入力されておりません。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (mainName == mainCategoryNameTextBox.Text)
            {
                MessageBox.Show("大分類名が変更されておりません。", "変更エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(reasonText.Text))
            {
                MessageBox.Show("選択した大分類名を変更する理由を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //大分類名・理由入力済み
            DialogResult result = MessageBox.Show("更新をしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes ) 
            {
                mainName = mainCategoryNameTextBox.Text;
                reason = reasonText.Text;

                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();

                string sql_str = "update main_category_m set main_category_name = '" + mainName + "' where main_category_code = " + mainCode + ";";
                string sql_str2 = "update main_category_m set reason = '" + reason + "' where main_category_code = " + mainCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter2 = new NpgsqlDataAdapter(sql_str2, conn);
                builder = new NpgsqlCommandBuilder(adapter);

                adapter.Fill(dt);
                adapter.Update(dt);
                adapter2.Fill(dt);
                adapter2.Update(dt);
                MessageBox.Show("大分類名を変更しました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) ;

                //履歴
                DateTime dat = DateTime.Now;
                //大分類履歴
                string sql_mCategroy_name_revisions = "insert into revisions values(" + 3 + ",'" + dat + "'," + staff_code + ", '" + mName + "','" + mainName + "', '" + reason + "');";
                cmd = new NpgsqlCommand(sql_mCategroy_name_revisions, conn);
                reader = cmd.ExecuteReader();
                reader.Close();

                conn.Close();
                this.Close();
            }
        }

        private void UpdateMainCategoryMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainCategoryMaster mainCategory = new MainCategoryMaster(master, staff_code, Access_auth, Pass);
            mainCategory.Show();
        }
    }
}
