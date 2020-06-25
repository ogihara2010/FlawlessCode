using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class ItemMaster : Form　//品名マスタメンテナンスメニュー
    {
        MasterMaintenanceMenu master;
        DataTable dt;
        int staff_code;
        public ItemMaster(MasterMaintenanceMenu master, int staff_code)
        {
            InitializeComponent();
            this.master = master;
            this.staff_code = staff_code;
        }

        private void ProductNameMenu_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            dt = new DataTable();
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select main_category_name, item_name, item_code from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.invalid = 0 and main_category_m.invalid = 0";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "大分類名";
            dataGridView1.Columns[1].HeaderText = "品名";
            dataGridView1.Columns[2].HeaderText = "品名コード";

            conn.Close();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
            master.Show();
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            ProductAddMenu productAdd = new ProductAddMenu(dt, master,staff_code);

            this.Close();
            productAdd.Show();
        }

        private void changeDeleteButton_Click(object sender, EventArgs e)
        {
            int code = (int)dataGridView1.CurrentRow.Cells[2].Value; //選択した品名コードを取得
            ProductChangeDeleteMenu changeDeleteMenu = new ProductChangeDeleteMenu(this, master, code,staff_code);
            this.Close();
            changeDeleteMenu.Show();

        }

        private void mainCategoryMenu_Click(object sender, EventArgs e)//大分類マスタ
        {
            MainCategoryMaster mainCategory = new MainCategoryMaster(master,staff_code);

            this.Close();
            mainCategory.Show();
        }
    }
}
