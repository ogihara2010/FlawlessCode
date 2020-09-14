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
        Statement statement;
        MainMenu mainMenu;
        int staff_id;
        string address;
        string staff_name;
        decimal Total;
        string access_auth;
        string document;
        int control;
        string data;
        int grade;
        #region "買取販売履歴"
        string name1;
        string phoneNumber1;
        string address1;
        string addresskana1;
        string code1;
        string item1;
        string date1;
        string date2;
        string method1;
        string amountA;
        string amountB;
        string antiqueNumber;
        string documentNumber;
        #endregion
        string pass;

        public client_search_result(DataTable dt, int type, int check, Statement statement, int id, decimal Total, int control, string document, string Access_auth, string Pass)
        {
            InitializeComponent();

            this.dt = dt;
            this.type = type;
            this.check = check;
            //this.mainMenu = mainMenu;
            this.statement = statement;
            staff_id = id;
            this.Total = Total;
            this.control = control;
            this.document = document;
            this.access_auth = Access_auth;
            this.pass = Pass;
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
            client_search client_Search = new client_search(statement, staff_id, type, staff_name, address, Total, control, document, access_auth, pass);

            this.Close();
            client_Search.Show();
        }

        private void selectionButton_Click(object sender, EventArgs e)//選択
        {
            DataTable dt2 = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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

                if (statement.Visible == true)
                {
                    statement.staff_id = staff_id;
                    statement.client_staff_name = staff_name;
                    statement.address = address;
                    statement.type = 0;
                    statement.count = 1;
                    this.Close();
                    statement.Show();
                }
                else
                {
                    statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
                    this.Close();
                    statement.clientDt = dt2;
                    statement.count = 1;
                    statement.Show();
                }

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

                if (statement.Visible == true)
                {
                    statement.staff_id = staff_id;
                    statement.client_staff_name = staff_name;
                    statement.address = address;
                    statement.type = 1;
                    statement.count = 1;
                    this.Close();
                    statement.Show();
                }
                else
                {
                    statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
                    this.Close();
                    statement.clientDt = dt2;
                    statement.count = 1;
                    statement.type = 1;
                    statement.Show();
                }
            }
        }
    }
}