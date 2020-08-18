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
        string address;
        string staff_name;
        decimal Total;
        string access_auth;
        string document;
        int control;
        string data;
        #region "買取販売履歴"
        string search1;
        string search2;
        string search3;
        string search4;
        string search5;
        string search6;
        string search7;
        string search8;
        string search9;
        string search10;
        string search11;
        string search12;
        #endregion
        string pass;
        #region "納品書の引数"
        decimal amount00;
        decimal amount01;
        decimal amount02;
        decimal amount03;
        decimal amount04;
        decimal amount05;
        decimal amount06;
        decimal amount07;
        decimal amount08;
        decimal amount09;
        decimal amount010;
        decimal amount011;
        decimal amount012;
        #endregion
        public client_search_result(DataTable dt, int type, int check, MainMenu mainMenu, int id, decimal Total, int control, decimal amount00, decimal amount01, decimal amount02, decimal amount03, decimal amount04, decimal amount05, decimal amount06, decimal amount07, decimal amount08, decimal amount09, decimal amount010, decimal amount011, decimal amount012)
        {
            InitializeComponent();

            this.dt = dt;
            this.type = type;
            this.check = check;
            this.mainMenu = mainMenu;
            //this.statement = statement;
            staff_id = id;
            this.Total = Total;
            this.control = control;
            #region "納品書の引数"
            this.amount00 = amount00;
            this.amount01 = amount01;
            this.amount02 = amount02;
            this.amount03 = amount03;
            this.amount04 = amount04;
            this.amount05 = amount05;
            this.amount06 = amount06;
            this.amount07 = amount07;
            this.amount08 = amount08;
            this.amount09 = amount09;
            this.amount010 = amount010;
            this.amount011 = amount011;
            this.amount012 = amount012;
            #endregion
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
            client_search client_Search = new client_search(mainMenu, staff_id, type, staff_name, address, Total, control, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012);

            this.Close();
            client_Search.Show();
        }

        private void selectionButton_Click(object sender, EventArgs e)//選択
        {
            DataTable dt2 = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            if (type == 0)//法人
            {
                int anumber = (int)dataGridView1.CurrentRow.Cells[4].Value;//選択したPKを取得

                string staff_name = (string)this.dataGridView1.CurrentRow.Cells[2].Value;
                string address = (string)this.dataGridView1.CurrentRow.Cells[3].Value;

                string sql_str = "select type,company_name,shop_name, staff_name,address, register_date, remarks  from client_m_corporate where antique_number = '" + anumber + "';";
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt2);

                conn.Close();

                
                Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012);
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

                Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012);
                this.Close();
                statement.clientDt = dt2;
                statement.count = 1;
                statement.Show();
            }
        }
    }
}
