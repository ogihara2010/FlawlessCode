using System;
using System.Data;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class ClientMaster_search : Form
    {
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable();
        public int type;
        public int check;
        public int staff_code;
        public string access_auth;
        bool screan = true;
        public string Pass;

        public ClientMaster_search(MasterMaintenanceMenu master, DataTable dt, int type, int check, int staff_code, string access_auth, string pass)
        {
            InitializeComponent();
            this.master = master;
            this.dt = dt;
            this.type = type;//法人・個人
            this.check = check;//古物商許可証あり・なし
            this.staff_code = staff_code;
            this.access_auth = access_auth;
            this.Pass = pass;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ClientMaster_search_Load_1(object sender, EventArgs e)
        {
            if (type == 0)
            {
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].HeaderText = "会社名";                //0
                dataGridView1.Columns[1].HeaderText = "店舗名";                //1
                dataGridView1.Columns[2].HeaderText = "担当者名";               //2
                dataGridView1.Columns[3].HeaderText = "住所";                 //3
                dataGridView1.Columns["code"].Visible = false;                  //4
                dataGridView1.Columns["company_kana"].Visible = false;
                dataGridView1.Columns["shop_name_kana"].Visible = false;
            }
            else if (type == 1)
            {

                int count = dt.Rows.Count;

                if (check == 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        dt.Rows[i]["antique_license"] = "登録済み";
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "氏名";                 //0
                    dataGridView1.Columns[1].HeaderText = "住所";                 //1
                    dataGridView1.Columns[2].HeaderText = "古物商許可証";         //2
                    dataGridView1.Columns["code"].Visible = false;                  //3
                    dataGridView1.Columns["name_kana"].Visible = false;
                    dataGridView1.Columns["address_kana"].Visible = false;
                }
                else if (check == 1)
                {
                    for (int i = 0; i < count; i++)
                    {
                        dt.Rows[i]["antique_license"] = "未登録";
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "氏名";
                    dataGridView1.Columns[1].HeaderText = "住所";
                    dataGridView1.Columns[2].HeaderText = "古物商許可証";
                    dataGridView1.Columns["code"].Visible = false;
                    dataGridView1.Columns["name_kana"].Visible = false;
                    dataGridView1.Columns["address_kana"].Visible = false;
                }

            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (type == 0)
            {
                string companyname = (string)this.dataGridView1.CurrentRow.Cells[0].Value; // 会社名
                string shopname = (string)this.dataGridView1.CurrentRow.Cells[1].Value; // 店舗名
                string name = (string)this.dataGridView1.CurrentRow.Cells[2].Value; //担当者名
                string address = (string)this.dataGridView1.CurrentRow.Cells[3].Value; //住所

                int ClientCode = (int)dataGridView1.CurrentRow.Cells[4].Value;          //顧客番号

                ClientMaster_UPD clientMaster_UPD = new ClientMaster_UPD(master, type, ClientCode, staff_code, access_auth, Pass);
                screan = false;
                this.Close();
                clientMaster_UPD.Show();
            }
            else if (type == 1)
            {
                string name = (string)this.dataGridView1.CurrentRow.Cells[0].Value; // 氏名
                string address = (string)this.dataGridView1.CurrentRow.Cells[1].Value; //住所

                int ClientCode = (int)dataGridView1.CurrentRow.Cells[3].Value;          //顧客番号

                ClientMaster_UPD clientMaster_UPD = new ClientMaster_UPD(master, type, ClientCode, staff_code, access_auth, Pass);
                screan = false;
                this.Close();
                clientMaster_UPD.Show();
            }
        }

        private void ClientMaster_search_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth, Pass);
                clientmaster.Show();
            }
            else
            {
                screan = true;
            }
        }
    }
}
