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

        public ClientMaster_search(MasterMaintenanceMenu master,DataTable dt)
        {
            InitializeComponent();
            this.master = master;
            this.dt = dt;
        }

        private void ClientMaster_search_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "会社名";
            dataGridView1.Columns[1].HeaderText = "店舗名";
            dataGridView1.Columns[2].HeaderText = "担当者名";
            dataGridView1.Columns[3].HeaderText = "住所";

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

        private void ClientMaster_search_Load_1(object sender, EventArgs e)
        {

        }
    }
}
