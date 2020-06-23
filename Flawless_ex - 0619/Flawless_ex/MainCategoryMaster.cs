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
    public partial class MainCategoryMaster : Form//大分類マスタメンテナンスメニュー
    {
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable();
        public MainCategoryMaster(MasterMaintenanceMenu master)
        {
            InitializeComponent();
            this.master = master;
        }

        private void MainCategoryMaster_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            dt = new DataTable();
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select main_category_name, main_category_code from main_category_m";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "大分類名";
            dataGridView1.Columns[1].HeaderText = "大分類コード";
        }



        private void addButton(object sender, EventArgs e)
        {
            AddMainCategoryMenu addMainCategory = new AddMainCategoryMenu(dt,master);
            this.Close();
            addMainCategory.Show();
        }

        private void returnButtonClick(object sender, EventArgs e)
        {
            this.Close();
            master.Show();
        }

        private void updateButtonClick(object sender, EventArgs e)
        {
            int code = (int)dataGridView1.CurrentRow.Cells[1].Value;//選択した大分類コード取得

            UpdateMainCategoryMenu updateMain = new UpdateMainCategoryMenu(master, code);
            this.Close();
            updateMain.Show();
        }
    }
}
