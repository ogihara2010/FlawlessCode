using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
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
            //法人
            string clientName;
            string shopName;
            string clientStaff;
            string address;
            string search1 = "or";
            string search2 = "or";
            string search3 = "or";

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;


            if (tabControl1.SelectedIndex == 0)
            {
                if (!string.IsNullOrWhiteSpace(clientNameTextBox.Text))
                {
                    clientName = this.clientNameTextBox.Text;
                }
                else
                {
                    clientName = "";
                }

                if(andRadioButton1.Checked == true)
                {
                    search1 = "and";
                }
                else { }

                if (!string.IsNullOrWhiteSpace(shopNameTextBox.Text))
                {
                    shopName = this.shopNameTextBox.Text;
                }
                else
                {
                    shopName = "";
                }

                if(andRadioButton2.Checked == true)
                {
                    search2 = "and";
                }
                else { }

                if (!string.IsNullOrWhiteSpace(clientStaffNameTextBox.Text))
                {
                    clientStaff = this.clientStaffNameTextBox.Text;
                }
                else
                {
                    clientStaff = "";
                }

                if(andRadioButton3.Checked == true)
                {
                    search3 = "and";
                }
                else { }

                if (!string.IsNullOrWhiteSpace(addressTextBox.Text))
                {
                    address = this.addressTextBox.Text;
                }
                else
                {
                    address = "";
                }



                string sql = "select* from client_m_corporate where type = 0 and company_name = '" + clientName + "' " + search1 + " shop_name = '" + shopName + "' " + search2 + " staff_name = '" + clientStaff + "' " + search3 + " address = '" + address + "' ";
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                conn.Close();
                ClientMaster_search clientMastersearch = new ClientMaster_search(masterMenu,dt);
                this.Close();
                clientMastersearch.Show();

            }

            
            
            

        }


        private void ClientMaster_Load(object sender, EventArgs e)
        {

        }
    }
}