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
    public partial class ClientMaster : Form
    {
        MasterMaintenanceMenu masterMenu;
        MasterMaintenanceMenu_BC masterMenu_BC;
        DataTable dt = new DataTable();
        public ClientMaster(MasterMaintenanceMenu mster)
        {
            InitializeComponent();
            masterMenu = mster;
        }
        public ClientMaster(MasterMaintenanceMenu_BC mster_BC)
        {
            InitializeComponent();
            masterMenu_BC = mster_BC;
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            masterMenu.Show();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            ClientMaster_add clientMaster_Add = new ClientMaster_add(masterMenu);
            this.Close();
            clientMaster_Add.Show();
        }

        private void Search1_Click(object sender, EventArgs e)
        {
            string CompanyName = this.textBox1.Text;
            string ShopName = this.textBox2.Text;
            string ClientStaff = this.textBox3.Text;
            string Address = this.textBox4.Text;
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "Select * From client_m_corporate where company_name = " + " '" + CompanyName + "'" + " or shop_name = " + " '" + ShopName + "' " + " or staff_name =" + " '" + ClientStaff + "' " + " or address =" + " '" + Address + "' " + ";";
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            //conn.Close();
            ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu);
            this.Close();
            clientMastersearch.Show();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ClientMaster_UPD clientMaster_UPD = new ClientMaster_UPD(masterMenu);
            this.Close();
            clientMaster_UPD.Show();
        }
    }
}