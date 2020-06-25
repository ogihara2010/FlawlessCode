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
    public partial class ClientMaster_search : Form
    {
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable();

        public ClientMaster_search(MasterMaintenanceMenu master)
        {
            InitializeComponent();
            this.master = master;
        }

        private void ClientMaster_search_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select company_name, shop_name, client_staff_name, address from client_m_corporate where invalid = 0;";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "会社名";
            dataGridView1.Columns[1].HeaderText = "店舗名";
            dataGridView1.Columns[2].HeaderText = "担当者名";
            dataGridView1.Columns[3].HeaderText = "住所";

            conn.Close();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            ClientMaster_add clientMasteradd = new ClientMaster_add(master);

            this.Close();
            clientMasteradd.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ClientMaster clientmaster = new ClientMaster(master);

            this.Close();
            clientmaster.Show();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
