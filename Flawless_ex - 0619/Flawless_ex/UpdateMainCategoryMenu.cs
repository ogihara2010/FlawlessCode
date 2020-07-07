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

        int mainCode; //大分類コード
        string mainName; //大分類名
        int staff_code;//ログイン者の担当者コード
        string mName;

        public UpdateMainCategoryMenu(MasterMaintenanceMenu master, int code,int staff_code)
        {
            InitializeComponent();

            this.master = master;
            this.mainCode = code;
            this.staff_code = staff_code;
        }

        private void UpdateMainCategoryMenu_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            DataTable dt = new DataTable();
            DataRow row;
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
            MainCategoryMaster master = new MainCategoryMaster(this.master,staff_code);

            this.Close();
            master.Show();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("無効にしますか？", "確認", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                NpgsqlCommandBuilder builder;


                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "update main_category_m set invalid = 1 where main_category_code = " + mainCode + "";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);
                adapter.Fill(dt);
                adapter.Update(dt);
                MessageBox.Show("無効完了");

                //履歴
                DateTime dat = DateTime.Now;

                //無効履歴
                string sql_mCategoryRevisions = "insert into main_category_m_invalid_revisions values(" + mainCode + ",'" + dat + "'," + staff_code + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_mCategoryRevisions, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                conn.Close();


                MainCategoryMaster mainCategory = new MainCategoryMaster(master,staff_code);
                this.Close();
                mainCategory.Show();
            }
            else { }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("更新をしますか？", "確認", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                NpgsqlCommandBuilder builder;
                DataTable dt = new DataTable();
                
                
                string mainName = mainCategoryNameTextBox.Text;

                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                string sql_str = "update main_category_m set main_category_name = '" + mainName + "' where main_category_code = " + mainCode + ";";

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);

                adapter.Fill(dt);
                adapter.Update(dt);
                MessageBox.Show("更新完了");

                //履歴
                DateTime dat = DateTime.Now;
                //大分類履歴
                string sql_mCategroy_name_revisions = "insert into main_category_m_name_revisions values("+ mainCode +", '" + mName +"','" + mainName +"','" + dat +"'," + staff_code + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_mCategroy_name_revisions, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                conn.Close();

                MainCategoryMaster mainCategory = new MainCategoryMaster(master,staff_code);
                this.Close();
                mainCategory.Show();
            }
            else { }

        }
    }
}
