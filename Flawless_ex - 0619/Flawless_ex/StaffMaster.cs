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
    public partial class StaffMaster : Form　//担当者マスタメンテナンスメニュー
    {
        MasterMaintenanceMenu masterMenu;
        DataTable dt = new DataTable();

        public StaffMaster(MasterMaintenanceMenu mster)
        {
            InitializeComponent();

            masterMenu = mster;
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

            StaffUpdateMenu staffUpdateMenu = new StaffUpdateMenu(masterMenu, code);
            this.Close();
            staffUpdateMenu.Show();
        }

        private void StaffMaster_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select staff_code, staff_name from staff_m";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "担当者コード";
            dataGridView1.Columns[1].HeaderText = "担当者名";

            conn.Close();
        }

        private void addButtonClick(object sender, EventArgs e)//登録画面に遷移
        {
            StaffAddStaff addStaff = new StaffAddStaff(dt, masterMenu);
            this.Close();
            addStaff.Show();
        }
    }
}

