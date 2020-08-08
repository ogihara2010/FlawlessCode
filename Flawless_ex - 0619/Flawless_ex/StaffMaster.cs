using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class StaffMaster : Form　//担当者マスタメンテナンスメニュー
    {
        MasterMaintenanceMenu masterMenu;
        DataTable dt = new DataTable();
        int staff_code;
        public StaffMaster(MasterMaintenanceMenu mster, int staff_code)
        {
            InitializeComponent();

            masterMenu = mster;
            this.staff_code = staff_code;
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
            masterMenu.Show();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void updateButtonClick(object sender, EventArgs e)
        {
            int code = (int)dataGridView1.CurrentRow.Cells[0].Value; //選択した担当者コードを取得

            StaffUpdateMenu staffUpdateMenu = new StaffUpdateMenu(masterMenu, code, staff_code);
            this.Close();
            staffUpdateMenu.Show();
        }

        private void StaffMaster_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select staff_code, staff_name, main_category_name from staff_m inner join main_category_m on staff_m.main_category_code = main_category_m.main_category_code where staff_m.invalid = 0 ";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "担当者コード";
            dataGridView1.Columns[1].HeaderText = "担当者名";
            dataGridView1.Columns[2].HeaderText = "大分類";

            conn.Close();
        }

        private void addButtonClick(object sender, EventArgs e)//登録画面に遷移
        {
            StaffAddStaff addStaff = new StaffAddStaff(dt, masterMenu, staff_code);
            this.Close();
            addStaff.Show();
        }

        private void StaffMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            masterMenu.Show();
        }
    }
}

