using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class MainCategoryMaster : Form//大分類マスタメンテナンスメニュー
    {
        MasterMaintenanceMenu master;
        int staff_code;
        DataTable dt = new DataTable();
        public MainCategoryMaster(MasterMaintenanceMenu master, int staff_code)
        {
            InitializeComponent();
            this.master = master;
            this.staff_code = staff_code;
        }

        private void MainCategoryMaster_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            dt = new DataTable();
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select main_category_name, main_category_code from main_category_m where invalid = 0";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "大分類名";
            dataGridView1.Columns[1].HeaderText = "大分類コード";
        }



        private void addButton(object sender, EventArgs e)
        {
            AddMainCategoryMenu addMainCategory = new AddMainCategoryMenu(dt, master,staff_code);
            this.Close();
            addMainCategory.Show();
        }

        private void returnButtonClick(object sender, EventArgs e)
        {
            ItemMaster itemMaster = new ItemMaster(master, staff_code);
            this.Close();
            itemMaster.Show();
        }

        private void updateButtonClick(object sender, EventArgs e)
        {
            int code = (int)dataGridView1.CurrentRow.Cells[1].Value;//選択した大分類コード取得

            UpdateMainCategoryMenu updateMain = new UpdateMainCategoryMenu(master, code,staff_code);
            this.Close();
            updateMain.Show();
        }
    }
}
