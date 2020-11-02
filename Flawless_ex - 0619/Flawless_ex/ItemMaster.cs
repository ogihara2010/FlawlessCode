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
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlDataAdapter adapter;
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        MainMenu main;
        public string Access_auth;
        public string Pass;
        int MainCode;
        bool screan = true;

        int staff_code;
        public ItemMaster(MasterMaintenanceMenu master, int staff_code, string access_auth, string pass)
        {
            InitializeComponent();
            this.master = master;
            this.staff_code = staff_code;
            this.Access_auth = access_auth;
            this.Pass = pass;
        }

        private void ProductNameMenu_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            string sql_str = "select main_category_name, item_name, item_code, B.main_category_code from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code " +
                "where A.invalid = 0 and B.invalid = 0 order by main_category_code, item_code ;";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "大分類名";
            dataGridView1.Columns[1].HeaderText = "品名";
            dataGridView1.Columns[2].HeaderText = "品名コード";

            dataGridView1.Columns[2].DefaultCellStyle.Format = "#,0";
            dataGridView1.Columns[3].Visible = false;

            conn.Close();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            ProductAddMenu productAdd = new ProductAddMenu(dt, master, staff_code, Access_auth, Pass);
            screan = false;
            this.Close();
            productAdd.Show();
        }

        private void changeDeleteButton_Click(object sender, EventArgs e)
        {
            int code = (int)dataGridView1.CurrentRow.Cells[2].Value;        //選択した品名コードを取得
            MainCode = (int)dataGridView1.CurrentRow.Cells[3].Value;        //選択した大分類コードを取得

            ProductChangeDeleteMenu changeDeleteMenu = new ProductChangeDeleteMenu(this, master, code, staff_code, Access_auth, Pass, MainCode);
            screan = false;
            this.Close();
            changeDeleteMenu.Show();

        }

        private void mainCategoryMenu_Click(object sender, EventArgs e)//大分類マスタ
        {
            MainCategoryMaster mainCategory = new MainCategoryMaster(master, staff_code, Access_auth, Pass);
            screan = false;
            this.Close();
            mainCategory.Show();
        }

        private void ItemMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                master = new MasterMaintenanceMenu(main, staff_code, Access_auth, Pass);
                master.Show();
            }
        }
    }
}
