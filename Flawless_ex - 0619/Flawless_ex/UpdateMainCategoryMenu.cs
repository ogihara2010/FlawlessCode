using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
namespace Flawless_ex
{
    public partial class UpdateMainCategoryMenu : Form//大分類更新メニュー
    {
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable(); //選択した大分類コード全ての情報

        int mainCode; //大分類コード
        string mainName; //大分類名

        public UpdateMainCategoryMenu(MasterMaintenanceMenu master, int code)
        {
            InitializeComponent();

            this.master = master;
            this.mainCode = code;
        }

        private void UpdateMainCategoryMenu_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            DataTable dt = new DataTable();
            DataRow row;
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select* from main_category_m where main_category_code = " + mainCode + "";

            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            row = dt.Rows[0];
            mainName = row["main_category_name"].ToString();

            this.mainCategoryCodeTextBox.Text = mainCode.ToString();
            this.mainCategoryNameTextBox.Text = mainName;
            

            conn.Close();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            MainCategoryMaster master = new MainCategoryMaster(this.master);

            this.Close();
            master.Show();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                NpgsqlCommandBuilder builder;


                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "delete from main_category_m where main_category_code = " + mainCode + "";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);
                adapter.Fill(dt);
                adapter.Update(dt);
                MessageBox.Show("削除完了");

                conn.Close();


                MainCategoryMaster mainCategory = new MainCategoryMaster(master);
                this.Close();
                master.Show();
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

                string sql_str = "update main_category_m set main_category_name = '" + mainName + "' where main_category_code = "+ mainCode + ";";

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);

                adapter.Fill(dt);
                adapter.Update(dt);
                MessageBox.Show("更新完了");

                conn.Close();

                MainCategoryMaster mainCategory = new MainCategoryMaster(master);
                this.Close();
                mainCategory.Show();
            }
            else { }

        }
    }
}
