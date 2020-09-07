using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace Flawless_ex
{
    public partial class Invoice : Form
    {
        int staff_id;
        MainMenu mainMenu;
        string document;
        int control;
        int grade;
        string access_auth;
        string pass;
        NpgsqlConnection conn = new NpgsqlConnection();
        
        
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt9 = new DataTable();
        DataTable dt10 = new DataTable();
        DataTable dt11 = new DataTable();
        DataTable dt14 = new DataTable();
        DataTable dt17 = new DataTable();
        DataTable dt20 = new DataTable();
        DataTable dt23 = new DataTable();
        DataTable dt26 = new DataTable();
        DataTable dt29 = new DataTable();
        DataTable dt32 = new DataTable();
        DataTable dt35 = new DataTable();
        public Invoice(MainMenu main, int id, string document, int control, int grade)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            this.document = document;
            this.control = control;
            this.grade = grade;
        }

        private void Return5_Click(object sender, EventArgs e)
        {
            Operatelog operatelog = new Operatelog(mainMenu, staff_id, access_auth, pass);
            this.Close();
            operatelog.Show();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region "計算書　決済方法"
            if (comboBox1.Text == "計算書　決済方法")
            {
                if (document == null)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                NpgsqlDataAdapter adapter;
                NpgsqlDataAdapter adapter2;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from statement_data where document_number = '" + document + "';";
                string sql_str2 = "select * from statement_data_revisions where document_number = '" + document + "';";
                
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                DataRow row;
                row = dt.Rows[0];
                
                string pay = row["payment_method"].ToString();
                adapter2 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter2.Fill(dt2);
                DataRow row2;
                row2 = dt2.Rows[0];                
                string pay2 = row2["payment_method"].ToString();

                if (pay != pay2)
                {
                    string sql_str3 = "select A.assessment_date, C.staff_name, A.document_number, B.payment_method, A.payment_method, A.reason from statement_data A inner join " +
                                      " statement_data_revisions B ON (A.document_number = B.document_number) inner join staff_m C ON (A.staff_code = C. staff_code) where A.document_number = '" + document + "' and A.reason is not null";
                    adapter = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter.Fill(dt6);
                }
                else
                {
                    MessageBox.Show("変更はありません。");
                    return;
                }

                dataGridView2.DataSource = dt6;
                dataGridView2.Columns[0].HeaderText = "更新日時";
                dataGridView2.Columns[1].HeaderText = "変更者";
                dataGridView2.Columns[2].HeaderText = "変更があった伝票番号";
                dataGridView2.Columns[3].HeaderText = "変更前";
                dataGridView2.Columns[4].HeaderText = "変更後";
                dataGridView2.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "計算書　受渡方法"
            if (comboBox1.Text == "計算書　受渡方法")
            {
                if (document == null)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                NpgsqlDataAdapter adapter3;
                NpgsqlDataAdapter adapter4;
                NpgsqlDataAdapter adapter6;

                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from statement_data where document_number = '" + document + "';";
                string sql_str2 = "select * from statement_data_revisions where document_number = '" + document + "';";
                conn.Open();

                adapter3 = new NpgsqlDataAdapter(sql_str, conn);
                adapter3.Fill(dt3);
                DataRow row;
                row = dt3.Rows[0];

                string delivery = row["delivery_method"].ToString();
                adapter4 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter4.Fill(dt4);
                DataRow row2;
                row2 = dt4.Rows[0];
                string delivery2 = row2["delivery_method"].ToString();

                if (delivery != delivery2)
                {
                    string sql_str3 = "select A.assessment_date, C.staff_name, A.document_number, B.delivery_method, A.delivery_method, A.reason from statement_data A inner join " +
                                      " statement_data_revisions B ON (A.document_number = B.document_number) inner join staff_m C ON (A.staff_code = C.staff_code) where A.document_number = '" + document + "' and A.reason is not null";
                    adapter6 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter6.Fill(dt5);
                }
                else
                {
                    MessageBox.Show("変更はありません。");
                    return;
                }

                dataGridView2.DataSource = dt5;
                dataGridView2.Columns[0].HeaderText = "更新日時";
                dataGridView2.Columns[1].HeaderText = "変更者";
                dataGridView2.Columns[2].HeaderText = "変更があった伝票番号";
                dataGridView2.Columns[3].HeaderText = "変更前";
                dataGridView2.Columns[4].HeaderText = "変更後";
                dataGridView2.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "納品書　決済日"
            if (comboBox1.Text == "納品書　決済日")
            {
                if (control == 0)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                DataTable dt12 = new DataTable();
                DataTable dt13 = new DataTable();
                NpgsqlDataAdapter adapter14;
                NpgsqlDataAdapter adapter15;
                NpgsqlDataAdapter adapter16;

                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m where control_number = " + control + ";";
                string sql_str2 = "select * from delivery_m_revisions where control_number = " + control + ";";

                adapter14 = new NpgsqlDataAdapter(sql_str, conn);
                adapter14.Fill(dt12);
                DataRow row;
                row = dt12.Rows[0];
                string settlement = row["settlement_date"].ToString();

                adapter15 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter15.Fill(dt13);
                DataRow row2;
                row2 = dt13.Rows[0];
                string settlement2 = row2["settlement_date"].ToString();

                if (settlement != settlement2)
                {
                    string sql_str3 = "select A.registration_date, C.staff_name, A.control_number, B.settlement_date,A.settlement_date, A.reason from delivery_m A " +
                                      " inner join delivery_m_revisions B ON (A.control_number = B.control_number ) inner join staff_m C ON " +
                                      "(A.staff_code = C.staff_code) where A.control_number = '" + control + ";";

                    adapter16 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter16.Fill(dt14);
                }
                else
                {
                    MessageBox.Show("データがありません。");
                    return;
                }

                dataGridView2.DataSource = dt14;
                dataGridView2.Columns[0].HeaderText = "更新日時";
                dataGridView2.Columns[1].HeaderText = "変更者";
                dataGridView2.Columns[2].HeaderText = "変更があった管理番号";
                dataGridView2.Columns[3].HeaderText = "変更前";
                dataGridView2.Columns[4].HeaderText = "変更後";
                dataGridView2.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "納品書　注文日"
            if (comboBox1.Text == "納品書　注文日")
            {
                if (control == 0)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                DataTable dt15 = new DataTable();
                DataTable dt16 = new DataTable();
                NpgsqlDataAdapter adapter17;
                NpgsqlDataAdapter adapter18;
                NpgsqlDataAdapter adapter19;

                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m where control_number = " + control + ";";
                string sql_str2 = "select * from delivery_m_revisions where control_number = " + control + ";";

                adapter17 = new NpgsqlDataAdapter(sql_str, conn);
                adapter17.Fill(dt15);
                DataRow row;
                row = dt15.Rows[0];
                string order = row["order_date"].ToString();

                adapter18 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter18.Fill(dt16);
                DataRow row2;
                row2 = dt16.Rows[0];
                string order2 = row2["order_date"].ToString();

                if (order != order2)
                {
                    string sql_str3 = "select A.registration_date, C.staff_name, A.control_number, B.order_date,A.order_date, A.reason from delivery_m A " +
                                      " inner join delivery_m_revisions B ON (A.control_number = B.control_number ) inner join staff_m C ON " +
                                      "(A.staff_code = C.staff_code) where A.control_number = '" + control + ";";

                    adapter19 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter19.Fill(dt17);
                }
                else
                {
                    MessageBox.Show("データがありません。");
                    return;
                }

                dataGridView2.DataSource = dt17;
                dataGridView2.Columns[0].HeaderText = "更新日時";
                dataGridView2.Columns[1].HeaderText = "変更者";
                dataGridView2.Columns[2].HeaderText = "変更があった管理番号";
                dataGridView2.Columns[3].HeaderText = "変更前";
                dataGridView2.Columns[4].HeaderText = "変更後";
                dataGridView2.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "納品書　納品日"
            if (comboBox1.Text == "納品書　納品日")
            {
                if (control == 0)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                DataTable dt18 = new DataTable();
                DataTable dt19 = new DataTable();
                NpgsqlDataAdapter adapter20;
                NpgsqlDataAdapter adapter21;
                NpgsqlDataAdapter adapter22;

                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m where control_number = " + control + ";";
                string sql_str2 = "select * from delivery_m_revisions where control_number = " + control + ";";

                adapter20 = new NpgsqlDataAdapter(sql_str, conn);
                adapter20.Fill(dt18);
                DataRow row;
                row = dt18.Rows[0];
                string delivery = row["delivery_date"].ToString();

                adapter21 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter21.Fill(dt19);
                DataRow row2;
                row2 = dt19.Rows[0];
                string delivery2 = row["delivery_date"].ToString();

                if (delivery != delivery2)
                {
                    string sql_str3 = "select A.registration_date, C.staff_name, A.control_number, B.delivery_date,A.delivery_date, A.reason from delivery_m A " +
                                      " inner join delivery_m_revisions B ON (A.control_number = B.control_number ) inner join staff_m C ON " +
                                      "(A.staff_code = C.staff_code) where A.control_number = '" + control + ";";

                    adapter22 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter22.Fill(dt20);
                }
                else
                {
                    MessageBox.Show("データがありません。");
                    return;
                }

                dataGridView1.DataSource = dt20;
                dataGridView1.Columns[0].HeaderText = "更新日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更があった管理番号";
                dataGridView1.Columns[3].HeaderText = "変更前";
                dataGridView1.Columns[4].HeaderText = "変更後";
                dataGridView1.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "納品書　宛名"
            if (comboBox1.Text == "納品書　宛名")
            {
                if (control == 0)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                DataTable dt21 = new DataTable();
                DataTable dt22 = new DataTable();
                NpgsqlDataAdapter adapter23;
                NpgsqlDataAdapter adapter24;
                NpgsqlDataAdapter adapter25;

                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m where control_number = " + control + ";";
                string sql_str2 = "select * from delivery_m_revisions where control_number = " + control + ";";

                adapter23 = new NpgsqlDataAdapter(sql_str, conn);
                adapter23.Fill(dt21);
                DataRow row;
                row = dt21.Rows[0];
                string name = row["name"].ToString();

                adapter24 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter24.Fill(dt22);
                DataRow row2;
                row2 = dt22.Rows[0];
                string name2 = row2["delivery_date"].ToString();

                if (name != name2)
                {
                    string sql_str3 = "select A.registration_date, C.staff_name, A.control_number, B.name, A.name, A.reason from delivery_m A " +
                                      " inner join delivery_m_revisions B ON (A.control_number = B.control_number ) inner join staff_m C ON " +
                                      "(A.staff_code = C.staff_code) where A.control_number = " + control + ";";

                    adapter25 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter25.Fill(dt23);
                }
                else
                {
                    MessageBox.Show("データがありません。");
                    return;
                }

                dataGridView2.DataSource = dt23;
                dataGridView2.Columns[0].HeaderText = "更新日時";
                dataGridView2.Columns[1].HeaderText = "変更者";
                dataGridView2.Columns[2].HeaderText = "変更があった管理番号";
                dataGridView2.Columns[3].HeaderText = "変更前";
                dataGridView2.Columns[4].HeaderText = "変更後";
                dataGridView2.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "納品書　印鑑印刷"
            if (comboBox1.Text == "納品書　印鑑印刷")
            {
                if (control == 0)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                DataTable dt24 = new DataTable();
                DataTable dt25 = new DataTable();
                NpgsqlDataAdapter adapter26;
                NpgsqlDataAdapter adapter27;
                NpgsqlDataAdapter adapter28;

                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m where control_number = " + control + ";";
                string sql_str2 = "select * from delivery_m_revisions where control_number = " + control + ";";

                adapter26 = new NpgsqlDataAdapter(sql_str, conn);
                adapter26.Fill(dt24);
                DataRow row;
                row = dt24.Rows[0];
                string seal = row["seaal_print"].ToString();

                adapter27 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter27.Fill(dt25);
                DataRow row2;
                row2 = dt25.Rows[0];
                string seal2 = row2["seal_print"].ToString();

                if (seal != seal2)
                {
                    string sql_str3 = "select A.registration_date, C.staff_name, A.control_number, B.seal_print, A.seaal_print, A.reason from delivery_m A " +
                                      " inner join delivery_m_revisions B ON (A.control_number = B.control_number ) inner join staff_m C ON " +
                                      "(A.staff_code = C.staff_code) where A.control_number = " + control + ";";

                    adapter28 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter28.Fill(dt26);
                }
                else
                {
                    MessageBox.Show("データがありません。");
                    return;
                }

                dataGridView2.DataSource = dt26;
                dataGridView2.Columns[0].HeaderText = "更新日時";
                dataGridView2.Columns[1].HeaderText = "変更者";
                dataGridView2.Columns[2].HeaderText = "変更があった管理番号";
                dataGridView2.Columns[3].HeaderText = "変更前";
                dataGridView2.Columns[4].HeaderText = "変更後";
                dataGridView2.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "納品書　通貨"
            if (comboBox1.Text == "納品書　通貨")
            {
                if (control == 0)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                DataTable dt27 = new DataTable();
                DataTable dt28 = new DataTable();
                NpgsqlDataAdapter adapter29;
                NpgsqlDataAdapter adapter30;
                NpgsqlDataAdapter adapter31;

                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m where control_number = " + control + ";";
                string sql_str2 = "select * from delivery_m_revisions where control_number = " + control + ";";

                adapter29 = new NpgsqlDataAdapter(sql_str, conn);
                adapter29.Fill(dt27);
                DataRow row;
                row = dt27.Rows[0];
                string currency = row["currency"].ToString();

                adapter30 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter30.Fill(dt28);
                DataRow row2;
                row2 = dt28.Rows[0];
                string currency2 = row2["currency"].ToString();

                if (currency != currency2)
                {
                    string sql_str3 = "select A.registration_date, C.staff_name, A.control_number, B.currency, A.currency, A.reason from delivery_m A " +
                                      " inner join delivery_m_revisions B ON (A.control_number = B.control_number ) inner join staff_m C ON " +
                                      "(A.staff_code = C.staff_code) where A.control_number = '" + control + ";";

                    adapter31 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter31.Fill(dt29);
                }
                else
                {
                    MessageBox.Show("データがありません。");
                    return;
                }

                dataGridView2.DataSource = dt29;
                dataGridView2.Columns[0].HeaderText = "更新日時";
                dataGridView2.Columns[1].HeaderText = "変更者";
                dataGridView2.Columns[2].HeaderText = "変更があった管理番号";
                dataGridView2.Columns[3].HeaderText = "変更前";
                dataGridView2.Columns[4].HeaderText = "変更後";
                dataGridView2.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "納品書　種別"
            if (comboBox1.Text == "納品書　種別")
            {
                if (control == 0)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                DataTable dt30 = new DataTable();
                DataTable dt31 = new DataTable();
                NpgsqlDataAdapter adapter32;
                NpgsqlDataAdapter adapter33;
                NpgsqlDataAdapter adapter34;

                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m where control_number = " + control + ";";
                string sql_str2 = "select * from delivery_m_revisions where control_number = " + control + ";";

                adapter32 = new NpgsqlDataAdapter(sql_str, conn);
                adapter32.Fill(dt30);
                DataRow row;
                row = dt30.Rows[0];
                string type = row["type"].ToString();

                adapter33 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter33.Fill(dt31);
                DataRow row2;
                row2 = dt31.Rows[0];
                string type2 = row2["type"].ToString();

                if (type != type2)
                {
                    string sql_str3 = "select A.registration_date, C.staff_name, A.control_number, B.type, A.type, A.reason from delivery_m A " +
                                      " inner join delivery_m_revisions B ON (A.control_number = B.control_number ) inner join staff_m C ON " +
                                      "(A.staff_code = C.staff_code) where A.control_number = '" + control + ";";

                    adapter34 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter34.Fill(dt32);
                }
                else
                {
                    MessageBox.Show("データがありません。");
                    return;
                }

                dataGridView2.DataSource = dt32;
                dataGridView2.Columns[0].HeaderText = "更新日時";
                dataGridView2.Columns[1].HeaderText = "変更者";
                dataGridView2.Columns[2].HeaderText = "変更があった管理番号";
                dataGridView2.Columns[3].HeaderText = "変更前";
                dataGridView2.Columns[4].HeaderText = "変更後";
                dataGridView2.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "納品書　支払方法"
            if (comboBox1.Text == "納品書　支払方法")
            {
                if (control == 0)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                DataTable dt33 = new DataTable();
                DataTable dt34 = new DataTable();
                NpgsqlDataAdapter adapter35;
                NpgsqlDataAdapter adapter36;
                NpgsqlDataAdapter adapter37;

                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m where control_number = " + control + ";";
                string sql_str2 = "select * from delivery_m_revisions where control_number = " + control + ";";

                adapter35 = new NpgsqlDataAdapter(sql_str, conn);
                adapter35.Fill(dt33);
                DataRow row;
                row = dt33.Rows[0];
                string pay = row["payment_method"].ToString();

                adapter36 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter36.Fill(dt34);
                DataRow row2;
                row2 = dt34.Rows[0];
                string pay2 = row2["payment_method"].ToString();

                if (pay != pay2)
                {
                    string sql_str3 = "select A.registration_date, C.staff_name, A.control_number, B.payment_method, A.payment_method, A.reason from delivery_m A " +
                                      " inner join delivery_m_revisions B ON (A.control_number = B.control_number ) inner join staff_m C ON " +
                                      "(A.staff_code = C.staff_code) where A.control_number = '" + control + ";";

                    adapter37 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter37.Fill(dt35);
                }
                else
                {
                    MessageBox.Show("データがありません。");
                    return;
                }

                dataGridView2.DataSource = dt35;
                dataGridView2.Columns[0].HeaderText = "更新日時";
                dataGridView2.Columns[1].HeaderText = "変更者";
                dataGridView2.Columns[2].HeaderText = "変更があった管理番号";
                dataGridView2.Columns[3].HeaderText = "変更前";
                dataGridView2.Columns[4].HeaderText = "変更後";
                dataGridView2.Columns[5].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region "計算書　商品変更"
            if (comboBox2.Text == "計算書　商品変更")
            {
                if (document == null)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                DataTable dt7 = new DataTable();
                DataTable dt8 = new DataTable();
                NpgsqlDataAdapter adapter7;
                NpgsqlDataAdapter adapter8;
                NpgsqlDataAdapter adapter9;
                NpgsqlDataAdapter adapter10;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from statement_calc_data where document_number = '" + document + "';";
                string sql_str2 = "select * from statement_calc_data_revisions where document_number = '" + document + "';";

                adapter7 = new NpgsqlDataAdapter(sql_str, conn);
                adapter7.Fill(dt7);
                int a = dt7.Rows.Count;

                adapter8 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter8.Fill(dt8);
                int b = dt8.Rows.Count;

                if (a > b)
                {
                    string sql_str3 = "select C.assessment_date, C.staff_code, A.document_number, A.record_number,E.main_category_name, F.item_name, C.reason from statement_calc_data A " +
                                      " inner join statement_calc_data_revisions B ON (A.document_number = B.document_number ) inner join statement_data C ON " +
                                      "(A.document_number = C.document_number) inner join staff_m D ON (C.staff_code = D.staff_code) inner join main_category_m E " +
                                      " ON (A.main_category_code = E.main_category_code ) inner join item_m F ON (A.item_code  = F.item_code) " +
                                      " where A.document_number = '" + document + "' and A.record_number > " + b + ";";

                    adapter9 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter9.Fill(dt9);
                }
                else if(a < b) 
                {
                    string sql_str3 = "select C.assessment_date, D.staff_name, A.document_number, A.record_number,E.main_category_name, F.item_name, C.reason from statement_calc_data_revisions A " +
                                      " inner join statement_calc_data B ON (A.document_number = B.document_number ) inner join statement_data C ON " +
                                      "(A.document_number = C.document_number) inner join staff_m D ON (C.staff_code = D.staff_code) inner join main_category_m E " +
                                      " ON (A.main_category_code = E.main_category_code ) inner join item_m F ON (A.item_code  = F.item_code) " +
                                      "where A.document_number = '" + document + "' and A.record_number > " + a + ";";

                    adapter10 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter10.Fill(dt10);
                }

                dataGridView1.DataSource = dt9;
                dataGridView1.Columns[0].HeaderText = "更新日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更があった伝票番号";
                dataGridView1.Columns[3].HeaderText = "変更があった行番号";
                dataGridView1.Columns[4].HeaderText = "変更前";
                dataGridView1.Columns[5].HeaderText = "変更後"; 
                dataGridView1.Columns[6].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
            #region "納品書　商品変更"
            if (comboBox2.Text == "納品書　商品変更")
            {
                if (control == 0)
                {
                    MessageBox.Show("選択できません。");
                    return;
                }
                else { }
                DataTable dt9 = new DataTable();
                DataTable dt10 = new DataTable();
                NpgsqlDataAdapter adapter11;
                NpgsqlDataAdapter adapter12;
                NpgsqlDataAdapter adapter13;
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_calc where control_number = " + control + ";";
                string sql_str2 = "select * from delivery_calc_revisions where control_number = " + control + ";";

                adapter11 = new NpgsqlDataAdapter(sql_str, conn);
                adapter11.Fill(dt9);
                int a = dt9.Rows.Count;

                adapter12 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter12.Fill(dt10);
                int b = dt10.Rows.Count;

                if (a > b)
                {
                    string sql_str3 = "select C.registration_date, D.staff_name, A.control_number, A.record_number,E.main_category_name, F.item_name, C.reason from delivery_calc A " +
                                      " inner join delivery_calc_revisions B ON (A.control_number = B.control_number ) inner join delivery_m C ON " +
                                      "(A.control_number = C.control_number) inner join staff_m D ON (C.staff_code = D.staff_code ) inner join main_category_m E ON" +
                                      "(A.main_category_code = E.main_category_code) inner join item_m F ON (A.item_code = F.item_code) " +
                                      " where A.control_number = '" + control + "' and A.record_number > " + b + ";";

                    adapter13 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter13.Fill(dt11);
                }
                else if (a < b)
                {
                    string sql_str3 = "select C.registration_date, C.staff_code, A.control_number, A.record_number,A.main_category_code, A.item_code, C.reason from delivery_calc_revisions A " +
                                      " inner join delivery_calc B ON (A.control_number = B.control_number ) inner join delivery_m C ON " +
                                      "(A.control_number = C.control_number) inner join staff_m D ON (C.staff_code = D.staff_code ) inner join main_category_m E ON " +
                                      " (A.main_category_code = E.main_category_code) inner join item_m F ON (A.item_code = F.item_code)" +
                                      " where A.control_number = '" + control + "' and A.record_number > " + a + ";";

                    adapter13 = new NpgsqlDataAdapter(sql_str3, conn);
                    adapter13.Fill(dt11);
                }

                dataGridView1.DataSource = dt11;
                dataGridView1.Columns[0].HeaderText = "更新日時";
                dataGridView1.Columns[1].HeaderText = "変更者";
                dataGridView1.Columns[2].HeaderText = "変更があった伝票番号";
                dataGridView1.Columns[3].HeaderText = "変更があった行番後";
                dataGridView1.Columns[4].HeaderText = "変更前";
                dataGridView1.Columns[5].HeaderText = "変更後";
                dataGridView1.Columns[6].HeaderText = "変更理由";
                conn.Close();
            }
            #endregion
        }
    }
    }
