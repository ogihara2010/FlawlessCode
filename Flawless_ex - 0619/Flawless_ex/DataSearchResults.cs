using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace Flawless_ex
{
    public partial class DataSearchResults : Form
    {
        CustomerHistory customerHistory;
        MainMenu mainMenu;
        int type;
        string name1;
        string phoneNumber1;
        string address1;
        string address;
        string item1;
        string search1;
        string search2;
        string search3;
        string staff_name;
        int staff_id;
        string data;
        int antique;
        int control;
        int ID;
        DataTable dt = new DataTable();
        public DataSearchResults(MainMenu main, int type, int id, string name1, string phoneNumber1, string address1, string item1, string search1, string search2, string search3, string data)
        {
            InitializeComponent();
            mainMenu = main;
            this.type = type;
            this.name1 = name1;
            this.phoneNumber1 = phoneNumber1;
            this.address1 = address1;
            this.item1 = item1;
            this.search1 = search1;
            this.search2 = search2;
            this.search3 = search3;
            staff_id = id;
            this.data = data;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻るボタン
        {
            CustomerHistory customerHistory = new CustomerHistory(mainMenu,staff_id, data);
            this.Close();
            customerHistory.Show();
        }

        private void DataSearchResults_Load(object sender, EventArgs e)
        {
            #region "計算書"
            if (data == "S")
            {
                this.button1.Enabled = false;
                #region "法人"
                if (type == 0)
                {
                    NpgsqlConnection conn = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter;
                    conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                                     "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                     " where B.invalid = 0 and B.shop_name = '" + name1 + "' " + search1 + " B.phone_number = '" + phoneNumber1 + "'" + " " + search2 + " B.address like '% " + address1 + " %' " + search3 + " D.item_name = '" + item1 + "';";
                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "決済日";
                    dataGridView1.Columns[1].HeaderText = "受渡日";
                    dataGridView1.Columns[2].HeaderText = "店舗名";
                    dataGridView1.Columns[3].HeaderText = "担当者名・個人名";
                    dataGridView1.Columns[4].HeaderText = "電話番号";
                    dataGridView1.Columns[5].HeaderText = "住所";
                    dataGridView1.Columns[6].HeaderText = "品名";
                    dataGridView1.Columns[7].HeaderText = "金額";

                    conn.Close();
                }
                #endregion
                #region "個人"
                if (type == 1)
                {
                    NpgsqlConnection conn = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter;
                    conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount from statement_data A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                                 "inner join statement_calc_data C ON (A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                     "where B.invalid = 0 and B.name = '" + name1 + "' " + search1 + " B.phone_number = '" + phoneNumber1 + "'" + " " + search2 + " B.address like '% " + address1 + " %' " + search3 + " D.item_name = '" + item1 + "';";
                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "決済日";
                    dataGridView1.Columns[1].HeaderText = "受渡日";
                    dataGridView1.Columns[2].HeaderText = "氏名";
                    dataGridView1.Columns[3].HeaderText = "電話番号";
                    dataGridView1.Columns[4].HeaderText = "住所";
                    dataGridView1.Columns[5].HeaderText = "品名";
                    dataGridView1.Columns[6].HeaderText = "金額";

                    conn.Close();
                }
                #endregion
            }
            #endregion
            #region "納品書"
            if (data == "D")
            {
                this.button2.Enabled = false;
                #region "法人"
                if (type == 0)
                {
                    NpgsqlConnection conn = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter;
                    conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.settlement_date, A.delivery_date, B.shop_name, B.staff_name, B.phone_number, B.address, D.item_name, C.amount, A.control_number, A.antique_number from delivery_m A inner join client_m_corporate B ON (A.antique_number = B.antique_number )" +
                                     "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                     " where B.invalid = 0 and B.shop_name = '" + name1 + "' " + search1 + " B.phone_number = '" + phoneNumber1 + "'" + " " + search2 + " B.address like '% " + address1 + " %' " + search3 + " D.item_name = '" + item1 + "';";
                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "決済日";
                    dataGridView1.Columns[1].HeaderText = "受渡日";
                    dataGridView1.Columns[2].HeaderText = "店舗名";
                    dataGridView1.Columns[3].HeaderText = "担当者名・個人名";
                    dataGridView1.Columns[4].HeaderText = "電話番号";
                    dataGridView1.Columns[5].HeaderText = "住所";
                    dataGridView1.Columns[6].HeaderText = "品名";
                    dataGridView1.Columns[7].HeaderText = "金額";
                    conn.Close();
                    DataRow row3;
                    row3 = dt.Rows[0];
                    antique = (int)row3["antique_number"];
                    control = (int)row3["control_number"];
                }
                #endregion
                #region "個人"
                if (type == 1)
                {
                    NpgsqlConnection conn = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter;
                    conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str = "select A.settlement_date, A.delivery_date, B.name, B.phone_number, B.address, D.item_name, C.amount, A.control_number, A.id_number from statement_data A inner join client_m_individual B ON ( A.id_number = B.id_number )" +
                                 "inner join delivery_calc C ON (A.control_number = C.control_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                     "where B.invalid = 0 and B.name = '" + name1 + "' " + search1 + " B.phone_number = '" + phoneNumber1 + "'" + " " + search2 + " B.address like '% " + address1 + " %' " + search3 + " D.item_name = '" + item1 + "';";
                    conn.Open();

                    adapter = new NpgsqlDataAdapter(sql_str, conn);
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "決済日";
                    dataGridView1.Columns[1].HeaderText = "受渡日";
                    dataGridView1.Columns[2].HeaderText = "氏名";
                    dataGridView1.Columns[3].HeaderText = "電話番号";
                    dataGridView1.Columns[4].HeaderText = "住所";
                    dataGridView1.Columns[5].HeaderText = "品名";
                    dataGridView1.Columns[6].HeaderText = "金額";
                    conn.Close();
                }
                #endregion
            }
            #endregion

        }
        #region "計算書表示"
        private void Button2_Click(object sender, EventArgs e)
        {
            Statement statement = new Statement(mainMenu, staff_id, type,  name1, address);
            this.Close();
            statement.Show();
        }
        #endregion
        #region "納品書表示"
        private void Button1_Click(object sender, EventArgs e)
        {
            Statement statement = new Statement(mainMenu, staff_id, type, name1,address);
            this.Close();
            statement.Show();
        }
        #endregion
        /*
        #region "納品書プレビュー"
        private void pd_PrintPage2(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("MS Pゴシック", 10.5f);
            Font font1 = new Font("メイリオ", 36f);
            Brush brush = new SolidBrush(Color.Black);
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            #region "法人"
            if (type == 0)
            {
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m  where control_number = " + control + " and antique_number =" + antique + ";";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);

                conn.Close();
                #region "納品書（表の外のデータ）"
                DataRow row;
                row = dt.Rows[0];
                string sub_total = row["sub_total"].ToString();
                string vat = row["vat"].ToString();
                string vat_rate = row["vat_rate"].ToString();
                string vat_amount = row["vat_amount"].ToString();
                string total = row["total"].ToString();
                string bank = row["account_payble"].ToString();
                string name = row["name"].ToString();
                string customer = row["honorific_title"].ToString();
                string kind = row["type"].ToString();
                string orderDate = row["order_date"].ToString();
                string deliveryDate = row["delivery_date"].ToString();
                string settlementDate = row["settlement_date"].ToString();
                string sealPrint = row["seaal_print"].ToString();
                string method = row["payment_method"].ToString();
                string currency = row["currency"].ToString();
                string remarks2 = row["remarks2"].ToString();
                string TotalWeight = row["total_weight"].ToString();
                string TotalCount = row["total_count"].ToString();
                #endregion
                Pen p = new Pen(Color.Black);
                #region "表の枠"
                #region "見出し"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 110, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 110, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 110, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 110, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 110, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 110, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 110, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 110, 150, 30));
                #endregion
                #region "1行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 140, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 140, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 140, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 140, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 140, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 140, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 140, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 140, 150, 30));
                #endregion
                #region "2行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 170, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 170, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 170, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 170, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 170, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 170, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 170, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 170, 150, 30));
                #endregion
                #region "3行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 200, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 200, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 200, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 200, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 200, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 200, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 200, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 200, 150, 30));
                #endregion
                #region "4行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 230, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 230, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 230, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 230, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 230, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 230, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 230, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 230, 150, 30));
                #endregion
                #region "5行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 260, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 260, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 260, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 260, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 260, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 260, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 260, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 260, 150, 30));
                #endregion
                #region "6行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 290, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 290, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 290, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 290, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 290, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 290, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 290, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 290, 150, 30));
                #endregion
                #region "7行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 320, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 320, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 320, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 320, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 320, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 320, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 320, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 320, 150, 30));
                #endregion
                #region "8行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 350, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 350, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 350, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 350, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 350, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 350, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 350, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 350, 150, 30));
                #endregion
                #region "9行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 380, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 380, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 380, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 380, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 380, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 380, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 380, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 380, 150, 30));
                #endregion
                #region "10行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 410, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 410, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 410, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 410, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 410, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 410, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 410, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 410, 150, 30));
                #endregion
                #region "11行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 440, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 440, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 440, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 440, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 440, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 440, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 440, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 440, 150, 30));
                #endregion
                #region "12行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 470, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 470, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 470, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 470, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 470, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 470, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 470, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 470, 150, 30));
                #endregion
                #region "13行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 500, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 500, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 500, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 500, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 500, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 500, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 500, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 500, 150, 30));
                #endregion
                #region "14行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 530, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 530, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 530, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 530, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 530, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 530, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 530, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 530, 150, 30));
                #endregion
                #endregion
                #region "顧客選択の枠"

                #endregion
                #region "納品書1行目"
                e.Graphics.DrawString(sub_total, font, brush, new PointF(50, 430));
                e.Graphics.DrawString(vat, font, brush, new PointF(200, 430));
                e.Graphics.DrawString(vat_rate, font, brush, new PointF(330, 430));
                e.Graphics.DrawString(vat_amount, font, brush, new PointF(490, 430));
                e.Graphics.DrawString(total, font, brush, new PointF(390, 430));
                e.Graphics.DrawString(bank, font, brush, new PointF(550, 430));
                e.Graphics.DrawString(name, font, brush, new PointF(690, 430));
                #endregion
            }
            #endregion
            if (type == 1)
            {
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m  where control_number = " + control + " id_number =" + ID + ")";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);

                conn.Close();
                Pen p = new Pen(Color.Black);
                #region "表の枠"
                #region "見出し"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 110, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 110, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 110, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 110, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 110, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 110, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 110, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 110, 150, 30));
                #endregion
                #region "1行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 140, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 140, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 140, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 140, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 140, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 140, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 140, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 140, 150, 30));
                #endregion
                #region "2行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 170, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 170, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 170, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 170, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 170, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 170, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 170, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 170, 150, 30));
                #endregion
                #region "3行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 200, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 200, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 200, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 200, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 200, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 200, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 200, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 200, 150, 30));
                #endregion
                #region "4行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 230, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 230, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 230, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 230, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 230, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 230, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 230, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 230, 150, 30));
                #endregion
                #region "5行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 260, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 260, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 260, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 260, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 260, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 260, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 260, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 260, 150, 30));
                #endregion
                #region "6行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 290, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 290, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 290, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 290, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 290, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 290, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 290, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 290, 150, 30));
                #endregion
                #region "7行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 320, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 320, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 320, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 320, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 320, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 320, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 320, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 320, 150, 30));
                #endregion
                #region "8行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 350, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 350, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 350, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 350, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 350, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 350, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 350, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 350, 150, 30));
                #endregion
                #region "9行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 380, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 380, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 380, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 380, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 380, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 380, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 380, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 380, 150, 30));
                #endregion
                #region "10行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 410, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 410, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 410, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 410, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 410, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 410, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 410, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 410, 150, 30));
                #endregion
                #region "11行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 440, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 440, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 440, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 440, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 440, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 440, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 440, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 440, 150, 30));
                #endregion
                #region "12行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 470, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 470, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 470, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 470, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 470, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 470, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 470, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 470, 150, 30));
                #endregion
                #region "13行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 500, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 500, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 500, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 500, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 500, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 500, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 500, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 500, 150, 30));
                #endregion
                #region "14行目"
                e.Graphics.DrawRectangle(p, new Rectangle(20, 530, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(120, 530, 100, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(220, 530, 120, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(340, 530, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(420, 530, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(500, 530, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(580, 530, 80, 30));
                e.Graphics.DrawRectangle(p, new Rectangle(660, 530, 150, 30));
                #endregion
                #endregion
            }

        }
        #endregion
        #region "計算書プレビュー"
        private void pd_PrintPage1(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("MS Pゴシック", 10.5f);
            Font font1 = new Font("メイリオ", 36f);
            Brush brush = new SolidBrush(Color.Black);
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select * from delivery_m where staff_code =" + staff_id + ";";
            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            conn.Close();
        }
        #endregion*/
    }
}
