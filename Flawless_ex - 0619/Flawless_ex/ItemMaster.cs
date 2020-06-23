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
    public partial class ItemMaster : Form　//品名マスタメンテナンスメニュー
    {
        MasterMaintenanceMenu master;
        DataTable dt;
        public ItemMaster(MasterMaintenanceMenu master)
        {
            InitializeComponent();
            this.master = master;
        }

        private void ProductNameMenu_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();//変更予定　大分類名、小分類名
            NpgsqlDataAdapter adapter;
            dt = new DataTable();
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select * from item_m";
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
            ProductAddMenu productAdd = new ProductAddMenu(dt,master);

            this.Close(); 
            productAdd.Show();
        }

        private void changeDeleteButton_Click(object sender, EventArgs e)
        {
            int code = (int)dataGridView1.CurrentRow.Cells[0].Value; //選択した品名コードを取得
            ProductChangeDeleteMenu changeDeleteMenu = new ProductChangeDeleteMenu(this, master, code);
            this.Close();
            changeDeleteMenu.Show();

        }
    }
}
