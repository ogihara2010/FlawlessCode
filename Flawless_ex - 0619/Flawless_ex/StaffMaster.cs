using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class StaffMaster : Form　//担当者マスタメンテナンスメニュー
    {
        MasterMaintenanceMenu masterMenu;
        MainMenu mainMenu;
        DataTable dt = new DataTable();
        public int staff_code;
        public string access_auth;
        bool screan = true;
        public string Pass;

        public StaffMaster(MasterMaintenanceMenu mster, int staff_code, string pass, string access_auth)
        {
            InitializeComponent();

            masterMenu = mster;
            this.staff_code = staff_code;
            this.Pass = pass;
            this.access_auth = access_auth;

        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void updateButtonClick(object sender, EventArgs e)
        {
            int code = (int)dataGridView1.CurrentRow.Cells[0].Value; //選択した担当者コードを取得

            StaffUpdateMenu staffUpdateMenu = new StaffUpdateMenu(masterMenu, code, staff_code, access_auth, Pass);
            screan = false;
            this.Close();
            staffUpdateMenu.Show();
        }

        private void StaffMaster_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select staff_code, staff_name, main_category_name from staff_m A inner join main_category_m B on A.main_category_code = B.main_category_code where A.invalid = 0 order by staff_code;";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "担当者コード";
            dataGridView1.Columns[1].HeaderText = "担当者名";
            dataGridView1.Columns[2].HeaderText = "大分類";

            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            conn.Close();
        }

        private void addButtonClick(object sender, EventArgs e)//登録画面に遷移
        {
            StaffAddStaff addStaff = new StaffAddStaff(dt, masterMenu, staff_code, Pass, access_auth);
            screan = false;
            this.Close();
            addStaff.Show();
        }

        private void StaffMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                MasterMaintenanceMenu masterMenu = new MasterMaintenanceMenu(mainMenu, staff_code, access_auth, Pass);
                masterMenu.Show();
            }
            else
            {
                screan = true;
            }
        }
            
    }
}

