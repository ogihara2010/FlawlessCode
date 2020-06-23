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
    public partial class AddMainCategoryMenu : Form//大分類　新規登録メニュー
    {
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        MasterMaintenanceMenu master;
        DataTable dt;
        public AddMainCategoryMenu(DataTable dt, MasterMaintenanceMenu master)
        {
            InitializeComponent();

            this.master = master;
            this.dt = dt;
        }

        private void AddMainCategoryMenu_Load(object sender, EventArgs e)
        {
            
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            MainCategoryMaster mainCategory = new MainCategoryMaster(master);
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

                string sql_str = "insert into main_category_m values(" + mainCode + ",'" + mainName + "');";

                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);

                adapter.Fill(dt);
                adapter.Update(dt);


                conn.Close();

                MessageBox.Show("登録完了");

                MainCategoryMaster mainCategory = new MainCategoryMaster(master);
                this.Close();
                mainCategory.Show();
            }
            else { }

            
        }
    }
}
