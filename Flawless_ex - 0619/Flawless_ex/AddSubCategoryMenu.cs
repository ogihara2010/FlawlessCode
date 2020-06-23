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
    public partial class AddSubCategoryMenu : Form//小分類マスタメンテナンス　新規登録
    {
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        MasterMaintenanceMenu master;
        DataTable dt;

        public AddSubCategoryMenu(DataTable dt, MasterMaintenanceMenu master)
        {
            InitializeComponent();

            this.dt = dt;
            this.master = master;
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            SubCategoryMaster subCategory = new SubCategoryMaster(master);

            this.Close();
            subCategory.Show();
        }

        private void AddSubCategoryMenu_Load(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            string sql_str = "select* from main_category_m;";
            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt2);

            conn.Close();

            //コンボボックス表示
            this.meinCategoryNameComboBox.DataSource = dt2;
            this.meinCategoryNameComboBox.DisplayMember = "main_category_name";
            this.meinCategoryNameComboBox.ValueMember = "main_category_code";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("登録をしますか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                NpgsqlCommandBuilder builder;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                int mainCategoryCode = (int)this.meinCategoryNameComboBox.SelectedValue;
                string subName = this.subCategoryNameTextBox.Text;
                int subCode = int.Parse(this.subCategoryCodeTextBox.Text);

                string sql_str = "insert into sub_category_m values(" + mainCategoryCode + ", " + subCode + ",'" + subName + "');";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);

                adapter.Fill(dt);
                adapter.Update(dt);

                conn.Close();

                MessageBox.Show("登録完了");

                SubCategoryMaster subCategory = new SubCategoryMaster(master);
                this.Close();
                subCategory.Show();

                
            }
            else { }
        }
    }
}
