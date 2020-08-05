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
        public MonResult(MainMenu main, int id, string access_auth, string staff_name, int type, string slipNumber)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            this.access_auth = access_auth;
            this.staff_name = staff_name;
            this.type = type;
            this.slipNumber = slipNumber;
        }

        private void Return3_Click(object sender, EventArgs e)//戻るボタン
        {
            this.Close();
            mainMenu.Show();
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
                string sql_str = "select * from staff_m;";
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
            NpgsqlConnection conn2 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter2;
            conn2.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str2 = "select A.settlement_date, A.delivery_method, A.payment_method, B.staff_name, B.company_name, E.type_name,A.settlement_date, " +
                              "A.delivery_method, A.payment_method, B.staff_name, B.company_name, C.amount,A.settlement_date, A.delivery_method, A.payment_method, " +
                              "B.staff_name, B.company_name, C.amount,A.settlement_date, A.delivery_method, A.payment_method, B.staff_name, B.company_name, C.amount " +
                              "from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )inner join statement_calc_data C ON " +
                              "(A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                              " inner join TYPE E ON( A.type = E.type and B.type = E.type ) where B.invalid = 0;";
            conn2.Open();

            adapter2 = new NpgsqlDataAdapter(sql_str2, conn2);
            adapter2.Fill(dt);
            
            //DataRow row;
            //row = dt.Rows[0];
            int a = dt.Rows.Count;
            /*string date = row["settlement_date"].ToString();
            string type = row["type_name"].ToString();
            string method = row["payment_method"].ToString();
            string staffname = row["staff_name"].ToString();
            string company_name = row["company_name"].ToString();
            string delivery = row["delivery_method"].ToString();*/
            
            #region "label"
            int insertRow = 0;
            int insertColumn = 0;

            if ( tableLayoutPanel2.GetControlFromPosition(insertColumn, insertRow) == null)
            {
               for (insertRow = 0; insertRow <= a * 2; insertRow = insertRow + 2)
               {
                    #region "再度開く"
                    NpgsqlConnection conn3 = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter3;
                    conn3.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                    string sql_str3 = "select A.settlement_date, A.delivery_method, A.payment_method, B.staff_name, B.company_name, E.type_name,A.settlement_date, " +
                                      "A.delivery_method, A.payment_method, B.staff_name, B.company_name, C.amount,A.settlement_date, A.delivery_method, A.payment_method, " +
                                      "B.staff_name, B.company_name, C.amount,A.settlement_date, A.delivery_method, A.payment_method, B.staff_name, B.company_name, C.amount " +
                                      "from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )inner join statement_calc_data C ON " +
                                      "(A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                                      " inner join TYPE E ON( A.type = E.type and B.type = E.type ) where B.invalid = 0;";
                    conn3.Open();

                    adapter3 = new NpgsqlDataAdapter(sql_str3, conn3);
                    adapter3.Fill(dt);

                    DataRow row3;
                    row3 = dt.Rows[(insertRow + 1)/2];
                    string date = row3["settlement_date"].ToString();
                    string type = row3["type_name"].ToString();
                    string method = row3["payment_method"].ToString();
                    string staffname = row3["staff_name"].ToString();
                    string company_name = row3["company_name"].ToString();
                    string delivery = row3["delivery_method"].ToString();
                    #endregion
                    #region "label名"
                    Label label0 = new Label();
                        Label label1 = new Label();
                        Label label2 = new Label();
                        Label label3 = new Label();
                        Label label4 = new Label();
                        Label label5 = new Label();
                        Label label6 = new Label();
                        Label label7 = new Label();
                        Label label8 = new Label();
                        Label label9 = new Label();
                        Label label10 = new Label();
                        Label label11 = new Label();
                        Label label12 = new Label();
                        Label label13 = new Label();
                        Label label14 = new Label();
                        Label label15 = new Label();
                        Label label16 = new Label();
                        Label label17 = new Label();
                        Label label18 = new Label();
                        Label label19 = new Label();
                        Label label20 = new Label();
                        Label label21 = new Label();
                        Label label22 = new Label();
                        Label label23 = new Label();
                        #endregion
                    #region "各labelの中身"
                        label0.Text = date;
                        label1.Text = method;
                        label2.Text = staffname;
                        label3.Text = type;
                        label4.Text = "買";
                        label5.Text = "買";
                        label6.Text = "買";
                        label7.Text = "買";
                        label8.Text = "買";
                        label9.Text = "買";
                        label10.Text = "買";
                        label11.Text = "買";
                        label12.Text = "買";
                        label13.Text = "卸";
                        label14.Text = "卸";
                        label15.Text = "卸";
                        label16.Text = "卸";
                        label17.Text = "卸";
                        label18.Text = "卸";
                        label19.Text = "卸";
                        label20.Text = "卸";
                        label21.Text = "卸";
                        label22.Text = company_name;
                        label23.Text = delivery;
                        #endregion
                    #region "入力"
                        tableLayoutPanel2.Controls.Add(label0, insertColumn, insertRow);
                        tableLayoutPanel2.Controls.Add(label1, insertColumn + 1, insertRow + 1);
                        tableLayoutPanel2.Controls.Add(label2, insertColumn + 2, insertRow);
                        tableLayoutPanel2.Controls.Add(label3, insertColumn + 3, insertRow + 1);
                        tableLayoutPanel2.Controls.Add(label4, insertColumn + 4, insertRow);
                        tableLayoutPanel2.Controls.Add(label5, insertColumn + 5, insertRow);
                        tableLayoutPanel2.Controls.Add(label6, insertColumn + 6, insertRow);
                        tableLayoutPanel2.Controls.Add(label7, insertColumn + 7, insertRow);
                        tableLayoutPanel2.Controls.Add(label8, insertColumn + 8, insertRow);
                        tableLayoutPanel2.Controls.Add(label9, insertColumn + 9, insertRow);
                        tableLayoutPanel2.Controls.Add(label10, insertColumn + 10, insertRow);
                        tableLayoutPanel2.Controls.Add(label11, insertColumn + 11, insertRow);
                        tableLayoutPanel2.Controls.Add(label12, insertColumn + 12, insertRow);
                        tableLayoutPanel2.Controls.Add(label13, insertColumn + 4, insertRow + 1);
                        tableLayoutPanel2.Controls.Add(label14, insertColumn + 5, insertRow + 1);
                        tableLayoutPanel2.Controls.Add(label15, insertColumn + 6, insertRow + 1);
                        tableLayoutPanel2.Controls.Add(label16, insertColumn + 7, insertRow + 1);
                        tableLayoutPanel2.Controls.Add(label17, insertColumn + 8, insertRow + 1);
                        tableLayoutPanel2.Controls.Add(label18, insertColumn + 9, insertRow + 1);
                        tableLayoutPanel2.Controls.Add(label19, insertColumn + 10, insertRow + 1);
                        tableLayoutPanel2.Controls.Add(label20, insertColumn + 11, insertRow + 1);
                        tableLayoutPanel2.Controls.Add(label21, insertColumn + 12, insertRow + 1);
                        tableLayoutPanel2.Controls.Add(label22, insertColumn + 3, insertRow);
                        tableLayoutPanel2.Controls.Add(label23, insertColumn + 1, insertRow);
                        #endregion
               }
                
               
            }
            #endregion
            
        }

        private void Search1_Click(object sender, EventArgs e)
        {
            NpgsqlDataAdapter adapter;
            NpgsqlConnection conn4 = new NpgsqlConnection();
            conn4.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str2 = "select A.settlement_date, A.delivery_method, A.payment_method, B.staff_name, B.company_name, C.amount,A.settlement_date, " +
                              "A.delivery_method, A.payment_method, B.staff_name, B.company_name, C.amount,A.settlement_date, A.delivery_method, A.payment_method, " +
                              "B.staff_name, B.company_name, C.amount,A.settlement_date, A.delivery_method, A.payment_method, B.staff_name, B.company_name, C.amount " +
                              "from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )inner join statement_calc_data C ON " +
                              "(A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                              " where B.invalid = 0;";
            conn4.Open();

            adapter = new NpgsqlDataAdapter(sql_str2, conn4);
            adapter.Fill(dt);
        }
        #region "担当者選択コンボボックス"
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            if (a > 1)
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
                
            
        }
        #endregion
        #region "成績入力画面へ"
        private void Button2_Click(object sender, EventArgs e)
        {
            RecordList recordList = new RecordList(statement, staff_id, staff_name, type, slipNumber);
            this.Close();
            recordList.Show();
        }
        #endregion

        private void Button1_Click(object sender, EventArgs e)
        {
            NpgsqlDataAdapter adapter;
            NpgsqlConnection conn4 = new NpgsqlConnection();
            conn4.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str2 = "select A.settlement_date, A.delivery_method, A.payment_method, B.staff_name, B.company_name, C.amount,A.settlement_date, " +
                              "A.delivery_method, A.payment_method, B.staff_name, B.company_name, C.amount,A.settlement_date, A.delivery_method, A.payment_method, " +
                              "B.staff_name, B.company_name, C.amount,A.settlement_date, A.delivery_method, A.payment_method, B.staff_name, B.company_name, C.amount " +
                              "from statement_data A inner join client_m_corporate B ON (A.antique_number = B.antique_number )inner join statement_calc_data C ON " +
                              "(A.document_number = C.document_number ) inner join item_m D ON (C.main_category_code = D.main_category_code and C.item_code = D.item_code ) " +
                              " where B.invalid = 0;";
            conn4.Open();

            adapter = new NpgsqlDataAdapter(sql_str2, conn4);
            adapter.Fill(dt);
        }
    }
}
