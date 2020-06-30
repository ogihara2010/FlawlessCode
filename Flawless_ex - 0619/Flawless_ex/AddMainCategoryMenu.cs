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
        MasterMaintenanceMenu master;
        DataTable dt;
        int staff_code;
        public AddMainCategoryMenu(DataTable dt, MasterMaintenanceMenu master, int staff_code)
        {
            InitializeComponent();

            this.master = master;
            this.dt = dt;
            this.staff_code = staff_code;
        }

        private void AddMainCategoryMenu_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
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
            MainCategoryMaster mainCategory = new MainCategoryMaster(master, staff_code);
            this.Close();
            mainCategory.Show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("登録をしますか？", "確認", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                NpgsqlCommandBuilder builder;

                int mainCode = int.Parse(mainCategoryCodeTextBox.Text);//大分類コード
                string mainName = mainCategoryNameTextBox.Text;//大分類名
                DateTime dat = DateTime.Now;

                string sql_str = "insert into main_category_m values(" + mainCode + ",'" + mainName + "', '" + dat + "',0);";

                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);

                adapter.Fill(dt);
                adapter.Update(dt);

                //新規登録履歴
                string sql_mCategoryRevisions = "insert into main_category_m_revisions values(" + mainCode + ",'" + dat + "'," + staff_code + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_mCategoryRevisions, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                conn.Close();

                MessageBox.Show("登録完了");



                MainCategoryMaster mainCategory = new MainCategoryMaster(master, staff_code);
                this.Close();
                mainCategory.Show();
            }
            else { }


        }
    }
}
