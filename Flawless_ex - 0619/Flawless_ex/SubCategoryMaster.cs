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
    public partial class SubCategoryMaster : Form//小分類マスタメンテナンス
    {
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable();
        public SubCategoryMaster(MasterMaintenanceMenu master)
        {
            InitializeComponent();

            this.master = master;
        }

        private void SubCategoryMaster_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            dt = new DataTable();
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select main_category_name, sub_category_name, sub_category_code from sub_category_m inner join main_category_m on sub_category_m.main_category_code = main_category_m.main_category_code;";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "大分類名";
            dataGridView1.Columns[1].HeaderText = "小分類名";
            dataGridView1.Columns[2].HeaderText = "小分類コード";

            conn.Close();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            this.Close();
            master.Show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddSubCategoryMenu addSubCategory = new AddSubCategoryMenu(dt,master);

            this.Close();
            addSubCategory.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int code = (int)dataGridView1.CurrentRow.Cells[2].Value;//選択した小分類コード取得

            updateSubCategoryMenu updateSubCategory = new updateSubCategoryMenu(master, code);
            this.Close();
            updateSubCategory.Show();
        }
    }
}
