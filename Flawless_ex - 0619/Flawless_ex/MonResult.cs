using System;
using System.Windows.Forms;
using System.Data;
using Npgsql;

namespace Flawless_ex
{
    public partial class MonResult : Form
    {
        MainMenu mainMenu;
        Statement statement;
        int staff_id;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        string staff_name;
        int type;
        string slipNumber;
        string access_auth;
        int a = 0; // クリック数 
        int Grade;
        int Antique;
        int Id;
        string Pass;
        bool NameChange;
        TopMenu top;
        bool screan = true;


        public MonResult(MainMenu main, int id, string access_auth, string staff_name, int type, string slipNumber, string pass, int grade)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            this.access_auth = access_auth;
            this.staff_name = staff_name;
            this.type = type;
            this.slipNumber = slipNumber;
            this.Pass = pass;
            this.Grade = grade;
        }

        private void Return3_Click(object sender, EventArgs e)//戻るボタン
        {
            this.Close();
        }

        private void MonResult_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                                                                                                                                     //検索用
            #region "担当者権限"
            if (access_auth == "C")
            {
                this.button2.Visible = false;
                string sql_str = "select * from staff_m where staff_code = " + staff_id + ";";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt3);
                comboBox1.DataSource = dt3;
                comboBox1.DisplayMember = "staff_name";
                comboBox1.ValueMember = "staff_code";
            }
            else
            {
                string sql_str = "select * from staff_m order by staff_code;";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt3);
                comboBox1.DataSource = dt3;
                comboBox1.DisplayMember = "staff_name";
                comboBox1.ValueMember = "staff_code";
            }
            #endregion
            if (slipNumber == null )
            {
                this.button2.Enabled = false;
            }
        }

        private void Search1_Click(object sender, EventArgs e)
        {
            dt.Clear();
            string search1 = "or";
            string search2 = "or";
            string staff;
            string date1;
            string date2;
            staff = this.comboBox1.Text;
            date1 = this.deliveryDateBox.Text;
            date2 = this.dateTimePicker1.Text;
            if (radioButton1.Checked == true)
            {
                type = 0;
            }
            if (radioButton2.Checked == true)
            {
                type = 1;
            }
            if (radioButton3.Checked == true)
            {
                search1 = "and";
            }
            else { }
            if (radioButton5.Checked == true)
            {
                search2 = "and";
            }
            else { }

            NpgsqlDataAdapter adapter;
            NpgsqlConnection conn4 = new NpgsqlConnection();
            conn4.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            if (access_auth == "C")
            {

            }
            else
            {
                string sql_str2 = "select C.assessment_date, C.delivery_method, C.payment_method, D.staff_name, A.staff_name, E.type_name, A.metal_purchase, A.metal_wholesale, " +
                              "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, " +
                              "A.sum_money, A.sum_wholesale_price from list_result A inner join statement_data C ON " +
                              "(A.document_number = C.document_number ) inner join staff_m D ON (C.staff_code = D.staff_code )  inner join type E ON (A.type = E.type) where D.invalid = 0 " +
                              " and D.staff_name = '" + staff + "'" + search1 + " (C.assessment_date >= '" + date1 + "' and C.assessment_date <= '" + date2 + "')" + search2 + " E.type = " + type + ";";
                conn4.Open();

                adapter = new NpgsqlDataAdapter(sql_str2, conn4);
                adapter.Fill(dt);
            }
            
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "査定日";
            dataGridView1.Columns[1].HeaderText = "受渡方法";
            dataGridView1.Columns[2].HeaderText = "お支払方法";
            dataGridView1.Columns[3].HeaderText = "担当";
            dataGridView1.Columns[4].HeaderText = "顧客名";
            dataGridView1.Columns[5].HeaderText = "区分";
            dataGridView1.Columns[6].HeaderText = "地金買取額";
            dataGridView1.Columns[7].HeaderText = "地金卸値";
            dataGridView1.Columns[8].HeaderText = "ダイヤ買取額";
            dataGridView1.Columns[9].HeaderText = "ダイヤ卸値";
            dataGridView1.Columns[10].HeaderText = "ブランド買取額";
            dataGridView1.Columns[11].HeaderText = "ブランド卸値";
            dataGridView1.Columns[12].HeaderText = "製品・ジュエリー買取額";
            dataGridView1.Columns[13].HeaderText = "製品・ジュエリー卸値";
            dataGridView1.Columns[14].HeaderText = "その他買取額";
            dataGridView1.Columns[15].HeaderText = "その他卸値";
            dataGridView1.Columns[16].HeaderText = "合計金額";
            dataGridView1.Columns[17].HeaderText = "卸値合計";
        }
        #region "担当者選択コンボボックス"
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            if (a > 0)
            {
                if (access_auth == "C")
                {
                    conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    conn.Open();
                    //品名検索用
                    string sql_str = "select * from staff_m where staff_code = " + staff_id + ";";
                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt3);
                    comboBox1.DataSource = dt3;
                    comboBox1.DisplayMember = "staff_name";
                    comboBox1.ValueMember = "staff_code";

                    conn.Close();
                }
                else
                {
                    conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    conn.Open();
                    //品名検索用
                    string sql_str = "select * from staff_m ;";
                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt3);
                    comboBox1.DataSource = dt3;
                    comboBox1.DisplayMember = "staff_name";
                    comboBox1.ValueMember = "staff_code";

                    conn.Close();
                }
            }
            a++;
        }
        #endregion
        #region "成績入力画面へ"
        private void Button2_Click(object sender, EventArgs e)
        {
            RecordList recordList = new RecordList(statement, staff_id, staff_name, type, slipNumber, Grade, Antique, Id, access_auth, Pass, NameChange);
            screan = false;
            this.Close();
            recordList.Show();
        }
        #endregion

        private void Button1_Click(object sender, EventArgs e)
        {
            NpgsqlDataAdapter adapter;
            NpgsqlConnection conn4 = new NpgsqlConnection();
            conn4.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            if (access_auth == "C")
            {
                string sql_str2 = "select B.assessment_date, C.delivery_method, C.payment_method, D.staff_name, A.staff_name, E.type_name, A.metal_purchase, A.metal_wholesale, " +
                              "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, " +
                              "A.sum_money, A.sum_wholesale_price from list_result A inner join list_result2 B ON (A.document_number = B.document_number )inner join statement_data C ON " +
                              "(A.document_number = C.document_number ) inner join staff_m D ON (C.staff_code = D.staff_code )  inner join type E ON (A.type = E.type) where D.invalid = 0 " +
                              "and A.staff_code = " + staff_id + ";";

                conn4.Open();

                adapter = new NpgsqlDataAdapter(sql_str2, conn4);
                adapter.Fill(dt);
            }
            else
            {
                string sql_str2 = "select B.assessment_date, C.delivery_method, C.payment_method, D.staff_name, A.staff_name, E.type_name, A.metal_purchase, A.metal_wholesale, " +
                              "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, " +
                              "A.sum_money, A.sum_wholesale_price from list_result A inner join list_result2 B ON (A.document_number = B.document_number )inner join statement_data C ON " +
                              "(A.document_number = C.document_number ) inner join staff_m D ON (C.staff_code = D.staff_code )  inner join type E ON (A.type = E.type) where D.invalid = 0;";

                conn4.Open();

                adapter = new NpgsqlDataAdapter(sql_str2, conn4);
                adapter.Fill(dt);
            }
            conn4.Close();
            
        }

        private void MonResult_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                mainMenu = new MainMenu(top, staff_id, Pass, access_auth);
                mainMenu.Show();
            }
            else
            {
                screan = true;
            }
        }
    }
}
