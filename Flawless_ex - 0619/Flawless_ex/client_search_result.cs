using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class client_search_result : Form
    {
        DataTable dt = new DataTable();
        int type;
        int check;
        //Statement statement;
        MainMenu mainMenu;
        int staff_id;
        string staff_name;
        string address;
        string SlipNumber;
        string access_auth;

        public client_search_result(DataTable dt, int type, int check, MainMenu mainMenu, int id)
        {
            InitializeComponent();

            this.dt = dt;
            this.type = type;
            this.check = check;
            this.mainMenu = mainMenu;
            //this.statement = statement;
            staff_id = id;
        }

        private void client_search_result_Load(object sender, EventArgs e)
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


        private void returnButton_Click(object sender, EventArgs e)//戻る
        {
            client_search client_Search = new client_search(mainMenu, staff_id, type, SlipNumber, access_auth);

            this.Close();
            client_Search.Show();
        }

        private void selectionButton_Click(object sender, EventArgs e)//選択
        {
            DataTable dt2 = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            if (type == 0)//法人
            {
                int anumber = (int)dataGridView1.CurrentRow.Cells[4].Value;//選択したPKを取得

                string staff_name = (string)this.dataGridView1.CurrentRow.Cells[2].Value;
                string address = (string)this.dataGridView1.CurrentRow.Cells[3].Value;

                string sql_str = "select type,company_name,shop_name, staff_name,address, register_date, remarks  from client_m_corporate where antique_number = " + anumber + " ";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt2);

                conn.Close();


                Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, SlipNumber, access_auth);
                this.Close();
                statement.clientDt = dt2;
                statement.count = 1;
                statement.Show();
            }
            else if (type == 1)
            {
                int id_number = (int)dataGridView1.CurrentRow.Cells[3].Value;//選択したPKを取得
                string staff_name = (string)this.dataGridView1.CurrentRow.Cells[0].Value;
                string address = (string)this.dataGridView1.CurrentRow.Cells[1].Value;

                string sql_str2 = "select type,name, address,register_date, remarks from client_m_individual where id_number = " + id_number + "  ";
                conn.Open();
                adapter = new NpgsqlDataAdapter(sql_str2, conn);
                adapter.Fill(dt2);

                conn.Close();

                Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, SlipNumber, access_auth);
                this.Close();
                statement.clientDt = dt2;
                statement.count = 1;
                statement.Show();
            }
        }
    }
}
