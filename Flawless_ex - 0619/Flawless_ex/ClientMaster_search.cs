using System;
using System.Data;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class ClientMaster_search : Form
    {
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable();
        int type;
        int check;

        public ClientMaster_search(MasterMaintenanceMenu master, DataTable dt, int type, int check)
        {
            InitializeComponent();
            this.master = master;
            this.dt = dt;
            this.type = type;//法人・個人
            this.check = check;//古物商許可証あり・なし
        }



        private void Button1_Click(object sender, EventArgs e)
        {
            ClientMaster clientmaster = new ClientMaster(master);

            this.Close();
            clientmaster.Show();
        }


        private void ClientMaster_search_Load_1(object sender, EventArgs e)
        {
            if (type == 0)
            {
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "会社名";
                dataGridView1.Columns[1].HeaderText = "店舗名";
                dataGridView1.Columns[2].HeaderText = "担当者名";
                dataGridView1.Columns[3].HeaderText = "住所";
                dataGridView1.Columns["antique_number"].Visible = false;


            }
            else if (type == 1)
            {

                int count = dt.Rows.Count;

                if (check == 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        dt.Rows[i]["antique_license"] = "あり";
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "氏名";
                    dataGridView1.Columns[1].HeaderText = "住所";
                    dataGridView1.Columns[2].HeaderText = "古物商許可証";
                    dataGridView1.Columns["id_number"].Visible = false;
                }
                else if (check == 1)
                {
                    for (int i = 0; i < count; i++)
                    {
                        dt.Rows[i]["antique_license"] = "なし";
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "氏名";
                    dataGridView1.Columns[1].HeaderText = "住所";
                    dataGridView1.Columns[2].HeaderText = "古物商許可証";
                    dataGridView1.Columns["id_number"].Visible = false;
                }

            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            ClientMaster_UPD clientMaster_UPD = new ClientMaster_UPD(master);

            this.Close();
            clientMaster_UPD.Show();
        }
    }
}
