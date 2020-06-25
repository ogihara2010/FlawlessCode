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

            string sql_str = "select staff_code, staff_name from client_m_corporate";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            listBox1.DataSource = dt;
            listBox1.Items[0] = "担当者コード";
            listBox1.Items[1] = "担当者名";

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
    }
}
