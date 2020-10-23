using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;

namespace Flawless_ex
{
    public partial class DataSearchResults : Form
    {
        CustomerHistory customerHistory;
        MainMenu mainMenu;
        int type;
        public string name1;
        public string phoneNumber1;
        public string address1;
        public string address;
        public string addresskana1;
        public string code1;
        public string item1;
        public string date1;
        public DateTime date2;
        public string method1;
        public string amountA;
        public string amountB;
        public string antiqueNumber;
        public string staff_name;
        public int staff_id;
        public string data;
        decimal Total;
        public int control;
        public string document;
        public string access_auth;
        public string Pass;
        DataTable dt = new DataTable();
        //bool screan = true;
        public string documentNumber;
        public int grade;
        int pageNumber;
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        NpgsqlConnection conn;
        NpgsqlDataReader reader;
        string sql;
        int ClientCode;             //顧客番号
        string deliveryDate;        //受渡日
        string settlementData;      //決済日
        string settlementMethod;    //支払方法
        string companyName;
        string shopName;
        string tel;
        string fax;
        string name;
        string Address;
        string birthday;
        string occupation;
        int Type;
        int PreviewRow;

        #region"計算書"
        List<DataTable> dataTables = new List<DataTable>();             //計算書の表の datatable
        List<DataTable> statementDataTable = new List<DataTable>();     //計算書の合計・総数の datatable
        List<DataTable> clientDataTable = new List<DataTable>();        //計算書の顧客情報の datatable
        List<string> DNumber = new List<string>();                      //計算書の伝票番号
        List<int> previewRow = new List<int>();                         //各計算書の行数

        DataTable table = new DataTable();                              //計算書の表用
        DataTable statementTotalTable = new DataTable();                //計算書の合計・決算方法・受取方法など用
        DataTable clientTable = new DataTable();                        //計算書の顧客情報用
        #endregion
        #region"納品書"
        List<DataTable> deliveryTable = new List<DataTable>();          //納品書の合計、決済方法など
        List<DataTable> deliveryCalcTable = new List<DataTable>();      //納品書の表の datatable
        List<DataTable> deliveryClientTable = new List<DataTable>();    //納品書の顧客情報の datatable
        List<string> CNumber = new List<string>();                      //納品書の管理番号

        DataTable DeliveryTable = new DataTable();                      //納品書の合計・決済方法など用
        DataTable DeliveryCalcTable = new DataTable();                  //納品書の表用
        DataTable DeliveryClientTable = new DataTable();                 //納品書の顧客情報用
        #endregion

        string antiqueLicense;
        string totalMoney;
        string totalCount;
        string totalWeight;
        int repeat;

        #region "納品書"
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
        #region "計算書"
        decimal amount10;
        decimal amount11;
        decimal amount12;
        decimal amount13;
        decimal amount14;
        decimal amount15;
        decimal amount16;
        decimal amount17;
        decimal amount18;
        decimal amount19;
        decimal amount110;
        decimal amount111;
        decimal amount112;
        #endregion
        public DataSearchResults(MainMenu main, int type, int id, DataTable table, string data, string pass, string document, int control, string antiqueNumber, string documentNumber, string access_auth)
        {
            InitializeComponent();
            mainMenu = main;
            this.type = type;

            dt = table;
            //this.name1 = name1;
            //this.phoneNumber1 = phoneNumber1;
            //this.address1 = address1;
            //this.addresskana1 = addresskana1;
            //this.code1 = code1;
            //this.item1 = item1;
            //this.date1 = date1;
            //this.date2 = date2;
            //this.method1 = method1;
            //this.amountA = amountA;
            //this.amountB = amountB;
            this.antiqueNumber = antiqueNumber;
            this.documentNumber = documentNumber;
            staff_id = id;
            this.data = data;
            this.Pass = pass;
            this.document = document;
            this.control = control;
            this.access_auth = access_auth;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻るボタン
        {
            this.Close();
        }

        private void DataSearchResults_Load(object sender, EventArgs e)
        {
            #region "計算書"
            if (data == "S")
            {                
                #region "法人"
                if (type == 0)
                {
                    dataGridView1.DataSource = dt;

                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                    dataGridView1.Columns.Insert(0, column);

                    #region"ヘッダー名"
                    dataGridView1.Columns[1].HeaderText = "伝票番号";
                    dataGridView1.Columns[2].HeaderText = "決済日";
                    dataGridView1.Columns[3].HeaderText = "受渡日";
                    dataGridView1.Columns[4].HeaderText = "店舗名";
                    dataGridView1.Columns[5].HeaderText = "担当者名・個人名";
                    dataGridView1.Columns[6].HeaderText = "電話番号";
                    dataGridView1.Columns[7].HeaderText = "住所";
                    dataGridView1.Columns[8].HeaderText = "品名";
                    dataGridView1.Columns[9].HeaderText = "金額";
                    #endregion

                    dataGridView1.Columns[10].Visible = false;

                    #region"読み取り専用"
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].ReadOnly = true;
                    dataGridView1.Columns[4].ReadOnly = true;
                    dataGridView1.Columns[5].ReadOnly = true;
                    dataGridView1.Columns[6].ReadOnly = true;
                    dataGridView1.Columns[7].ReadOnly = true;
                    dataGridView1.Columns[8].ReadOnly = true;
                    dataGridView1.Columns[9].ReadOnly = true;
                    #endregion

                    //conn.Close();
                    if (data == null)
                    {
                        document = (string)dataGridView1.CurrentRow.Cells[1].Value;
                        staff_name = (string)dataGridView1.CurrentRow.Cells[4].Value;
                        address = (string)dataGridView1.CurrentRow.Cells[6].Value;
                    }

                }
                #endregion
                #region "個人"
                if (type == 1)
                {
                    dataGridView1.DataSource = dt;

                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                    dataGridView1.Columns.Insert(0, column);

                    #region"ヘッダー名"
                    dataGridView1.Columns[1].HeaderText = "伝票番号";
                    dataGridView1.Columns[2].HeaderText = "決済日";
                    dataGridView1.Columns[3].HeaderText = "受渡日";
                    dataGridView1.Columns[4].HeaderText = "氏名";
                    dataGridView1.Columns[5].HeaderText = "電話番号";
                    dataGridView1.Columns[6].HeaderText = "住所";
                    dataGridView1.Columns[7].HeaderText = "品名";
                    dataGridView1.Columns[8].HeaderText = "金額";
                    #endregion

                    dataGridView1.Columns[9].Visible = false;

                    #region"読み取り専用"
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].ReadOnly = true;
                    dataGridView1.Columns[4].ReadOnly = true;
                    dataGridView1.Columns[5].ReadOnly = true;
                    dataGridView1.Columns[6].ReadOnly = true;
                    dataGridView1.Columns[7].ReadOnly = true;
                    dataGridView1.Columns[8].ReadOnly = true;
                    #endregion

                    //conn.Close();
                    document = (string)dataGridView1.CurrentRow.Cells[1].Value;
                    staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                    address = (string)dataGridView1.CurrentRow.Cells[5].Value;
                }
                #endregion
            }
            #endregion
            #region "納品書"
            if (data == "D")
            {                
                #region "法人"
                if (type == 0)
                {
                    dataGridView1.DataSource = dt;

                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                    dataGridView1.Columns.Insert(0, column);

                    #region"ヘッダー名"
                    dataGridView1.Columns[1].HeaderText = "管理番号";
                    dataGridView1.Columns[2].HeaderText = "決済日";
                    dataGridView1.Columns[3].HeaderText = "受渡日";
                    dataGridView1.Columns[4].HeaderText = "店舗名";
                    dataGridView1.Columns[5].HeaderText = "担当者名・個人名";
                    dataGridView1.Columns[6].HeaderText = "電話番号";
                    dataGridView1.Columns[7].HeaderText = "住所";
                    dataGridView1.Columns[8].HeaderText = "品名";
                    dataGridView1.Columns[9].HeaderText = "金額";
                    #endregion

                    #region"読み取り専用"
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].ReadOnly = true;
                    dataGridView1.Columns[4].ReadOnly = true;
                    dataGridView1.Columns[5].ReadOnly = true;
                    dataGridView1.Columns[6].ReadOnly = true;
                    dataGridView1.Columns[7].ReadOnly = true;
                    dataGridView1.Columns[8].ReadOnly = true;
                    dataGridView1.Columns[9].ReadOnly = true;
                    #endregion

                    //conn.Close();

                    control = (int)dataGridView1.CurrentRow.Cells[1].Value;
                    staff_name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                    address = (string)dataGridView1.CurrentRow.Cells[5].Value;
                }
                #endregion
                #region "個人"
                if (type == 1)
                {
                    dataGridView1.DataSource = dt;

                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                    dataGridView1.Columns.Insert(0, column);

                    #region"ヘッダー名"
                    dataGridView1.Columns[1].HeaderText = "管理番号";
                    dataGridView1.Columns[2].HeaderText = "決済日";
                    dataGridView1.Columns[3].HeaderText = "受渡日";
                    dataGridView1.Columns[4].HeaderText = "氏名";
                    dataGridView1.Columns[5].HeaderText = "電話番号";
                    dataGridView1.Columns[6].HeaderText = "住所";
                    dataGridView1.Columns[7].HeaderText = "品名";
                    dataGridView1.Columns[8].HeaderText = "金額";
                    #endregion
                    #region"読み取り専用"
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].ReadOnly = true;
                    dataGridView1.Columns[4].ReadOnly = true;
                    dataGridView1.Columns[5].ReadOnly = true;
                    dataGridView1.Columns[6].ReadOnly = true;
                    dataGridView1.Columns[7].ReadOnly = true;
                    dataGridView1.Columns[8].ReadOnly = true;
                    #endregion

                    //conn.Close();
                    control = (int)dataGridView1.CurrentRow.Cells[1].Value;
                    staff_name = (string)dataGridView1.CurrentRow.Cells[4].Value;
                    address = (string)dataGridView1.CurrentRow.Cells[6].Value;
                }
                #endregion
            }
            #endregion

        }
        
        #region "詳細表示"
        private void Button1_Click(object sender, EventArgs e)
        {
            pageNumber = 0;
            repeat = 0;

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            //pd.Print();
            printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Document = pd;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            conn.Open();

            if (data == "S")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value))
                    {
                        repeat++;
                        table = table.Clone();
                        statementTotalTable = statementTotalTable.Clone();
                        clientTable = clientTable.Clone();

                        #region"伝票番号"
                        DNumber.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                        #endregion

                        #region"顧客番号、受渡日、決済日、決済方法、合計金額、総数"
                        statementDataTable.Add(statementTotalTable);
                        sql = "select * from statement_data where document_number = '" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "';";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(statementDataTable[repeat - 1]);

                        cmd = new NpgsqlCommand(sql, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientCode = (int)reader["code"];
                            }
                        }
                        #endregion

                        #region"顧客情報"
                        clientDataTable.Add(clientTable);
                        sql = "select * from client_m where code = '" + ClientCode + "';";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(clientDataTable[repeat - 1]);
                        #endregion

                        string sql_PreviewCount = "select count(*) from statement_calc_data where document_number = '" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "';";
                        cmd = new NpgsqlCommand(sql_PreviewCount, conn);
                        PreviewRow = int.Parse(cmd.ExecuteScalar().ToString());
                        previewRow.Add(PreviewRow);

                        string sql_StatementPreview = "select * from statement_calc_data A inner join item_m B on A.item_code = B.item_code" +
                            " where document_number = '" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "' order by A.record_number;";
                        adapter = new NpgsqlDataAdapter(sql_StatementPreview, conn);
                        dataTables.Add(table);
                        adapter.Fill(dataTables[repeat - 1]);
                    }
                }
                DialogResult dr = printPreviewDialog1.ShowDialog();
            }
            else if (data == "D")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value))
                    {
                        repeat++;
                        DeliveryTable = DeliveryTable.Clone();
                        DeliveryCalcTable = DeliveryCalcTable.Clone();
                        DeliveryClientTable = DeliveryClientTable.Clone();

                        #region"管理番号"
                        CNumber.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                        #endregion

                        #region "納品書 下の部分"
                        deliveryTable.Add(DeliveryTable);
                        string sql_str = "select * from delivery_m where control_number =" + int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()) + ";";
                        adapter = new NpgsqlDataAdapter(sql_str, conn);
                        adapter.Fill(deliveryTable[repeat - 1]) ;

                        cmd = new NpgsqlCommand(sql_str, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientCode = (int)reader["code"];
                            }
                        }

                        #endregion

                        #region "納品書　表の部分"
                        deliveryCalcTable.Add(DeliveryCalcTable);
                        string sql_str2 = "select * from delivery_calc A inner join item_m B on A.item_code = B.item_code" +
                            " where control_number =" + int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()) + " order by record_number;";
                        adapter = new NpgsqlDataAdapter(sql_str2, conn);
                        adapter.Fill(deliveryCalcTable[repeat - 1]) ;

                        #region"コメントアウト"
                        //int a = dt2.Rows.Count;

                        //if (print >= 1)
                        //{
                        //    a = a / (print + 1);
                        //}
                        //else { }

                        //for (int b = 1; b <= a; b++)
                        //{
                        //    #region "再度開く"
                        //    NpgsqlConnection conn5 = new NpgsqlConnection();
                        //    NpgsqlDataAdapter adapter5;
                        //    conn5 = postgre.connection();

                        //    string sql_str5 = "select * from delivery_calc where control_number =" + control + " and record_number = " + b + ";";
                        //    adapter5 = new NpgsqlDataAdapter(sql_str5, conn5);
                        //    adapter5.Fill(dt5);

                        //    int c = b - 1;
                        //    DataRow row5;
                        //    row5 = dt5.Rows[c];
                        //    string weight = row5["weight"].ToString();
                        //    string count = row5["count"].ToString();
                        //    string remark = row5["remarks"].ToString();
                        //    string unit_price = row5["unit_price"].ToString();
                        //    string amount = row5["amount"].ToString();
                        //    string category = row5["main_category_code"].ToString();
                        //    string item = row5["item_code"].ToString();
                        //    #endregion

                        //    #region "大分類"
                        //    NpgsqlConnection conn3 = new NpgsqlConnection();
                        //    NpgsqlDataAdapter adapter3;
                        //    conn3 = postgre.connection();
                        //    //conn3.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                        //    string sql_str3 = "select * from main_category_m where main_category_code =" + category + ";";
                        //    adapter3 = new NpgsqlDataAdapter(sql_str3, conn3);
                        //    adapter3.Fill(dt3);

                        //    conn3.Close();
                        //    DataRow row3;
                        //    row3 = dt3.Rows[c];
                        //    string categoryName = row3["main_category_name"].ToString();
                        //    #endregion
                        //    #region "品名"
                        //    NpgsqlConnection conn4 = new NpgsqlConnection();
                        //    NpgsqlDataAdapter adapter4;
                        //    conn4 = postgre.connection();
                        //    //conn4.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                        //    string sql_str4 = "select * from item_m where item_code =" + item + ";";
                        //    adapter4 = new NpgsqlDataAdapter(sql_str4, conn4);
                        //    adapter4.Fill(dt4);
                        //    conn4.Close();

                        //    #endregion
                        //    #region "表の中身"
                        //    if (b == 1)
                        //    {
                        //        DataRow row4;
                        //        row4 = dt4.Rows[0];
                        //        string itemName1 = row4["item_name"].ToString();
                        //        #region "納品書1行目"
                        //        e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 430));
                        //        e.Graphics.DrawString(itemName1, font, brush, new PointF(200, 430));
                        //        e.Graphics.DrawString(weight, font, brush, new PointF(330, 430));
                        //        e.Graphics.DrawString(count, font, brush, new PointF(490, 430));
                        //        e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 430));
                        //        e.Graphics.DrawString(amount, font, brush, new PointF(550, 430));
                        //        e.Graphics.DrawString(remark, font, brush, new PointF(690, 430));
                        //        #endregion
                        //    }
                        //    else if (b == 2)
                        //    {
                        //        DataRow row4;
                        //        row4 = dt4.Rows[1];
                        //        string itemName2 = row4["item_name"].ToString();
                        //        #region "納品書2行目"
                        //        e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 480));
                        //        e.Graphics.DrawString(itemName2, font, brush, new PointF(200, 480));
                        //        e.Graphics.DrawString(weight, font, brush, new PointF(330, 480));
                        //        e.Graphics.DrawString(count, font, brush, new PointF(490, 480));
                        //        e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 480));
                        //        e.Graphics.DrawString(amount, font, brush, new PointF(550, 480));
                        //        e.Graphics.DrawString(remark, font, brush, new PointF(690, 480));
                        //        #endregion
                        //    }
                        //    else if (b == 3)
                        //    {
                        //        DataRow row4;
                        //        row4 = dt4.Rows[2];
                        //        string itemName3 = row4["item_name"].ToString();
                        //        #region "納品書3行目"
                        //        e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 530));
                        //        e.Graphics.DrawString(itemName3, font, brush, new PointF(200, 530));
                        //        e.Graphics.DrawString(weight, font, brush, new PointF(330, 530));
                        //        e.Graphics.DrawString(count, font, brush, new PointF(490, 530));
                        //        e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 530));
                        //        e.Graphics.DrawString(amount, font, brush, new PointF(550, 530));
                        //        e.Graphics.DrawString(remark, font, brush, new PointF(690, 530));
                        //        #endregion
                        //    }
                        //    if (b == 4)
                        //    {
                        //        DataRow row4;
                        //        row4 = dt4.Rows[0];
                        //        string itemName4 = row4["item_name"].ToString();
                        //        #region "納品書4行目"
                        //        e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 580));
                        //        e.Graphics.DrawString(itemName4, font, brush, new PointF(200, 580));
                        //        e.Graphics.DrawString(weight, font, brush, new PointF(330, 580));
                        //        e.Graphics.DrawString(count, font, brush, new PointF(490, 580));
                        //        e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 580));
                        //        e.Graphics.DrawString(amount, font, brush, new PointF(550, 580));
                        //        e.Graphics.DrawString(remark, font, brush, new PointF(690, 580));
                        //        #endregion
                        //    }
                        //    else if (b == 5)
                        //    {
                        //        DataRow row4;
                        //        row4 = dt4.Rows[0];
                        //        string itemName5 = row4["item_name"].ToString();
                        //        #region "納品書5行目"
                        //        e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 630));
                        //        e.Graphics.DrawString(itemName5, font, brush, new PointF(200, 630));
                        //        e.Graphics.DrawString(weight, font, brush, new PointF(330, 630));
                        //        e.Graphics.DrawString(count, font, brush, new PointF(490, 630));
                        //        e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 630));
                        //        e.Graphics.DrawString(amount, font, brush, new PointF(550, 630));
                        //        e.Graphics.DrawString(remark, font, brush, new PointF(690, 630));
                        //        #endregion
                        //    }
                        //    else if (b == 6)
                        //    {
                        //        DataRow row4;
                        //        row4 = dt4.Rows[0];
                        //        string itemName6 = row4["item_name"].ToString();
                        //        #region "納品書6行目"
                        //        e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 680));
                        //        e.Graphics.DrawString(itemName6, font, brush, new PointF(200, 680));
                        //        e.Graphics.DrawString(weight, font, brush, new PointF(330, 680));
                        //        e.Graphics.DrawString(count, font, brush, new PointF(490, 680));
                        //        e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 680));
                        //        e.Graphics.DrawString(amount, font, brush, new PointF(550, 680));
                        //        e.Graphics.DrawString(remark, font, brush, new PointF(690, 680));
                        //        #endregion
                        //    }
                        //    if (b == 7)
                        //    {
                        //        DataRow row4;
                        //        row4 = dt4.Rows[0];
                        //        string itemName7 = row4["item_name"].ToString();
                        //        #region "納品書7行目"
                        //        e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 730));
                        //        e.Graphics.DrawString(itemName7, font, brush, new PointF(200, 730));
                        //        e.Graphics.DrawString(weight, font, brush, new PointF(330, 730));
                        //        e.Graphics.DrawString(count, font, brush, new PointF(490, 730));
                        //        e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 730));
                        //        e.Graphics.DrawString(amount, font, brush, new PointF(550, 730));
                        //        e.Graphics.DrawString(remark, font, brush, new PointF(690, 730));
                        //        #endregion
                        //    }
                        //    else if (b == 8)
                        //    {
                        //        DataRow row4;
                        //        row4 = dt4.Rows[0];
                        //        string itemName8 = row4["item_name"].ToString();
                        //        #region "納品書8行目"
                        //        e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 780));
                        //        e.Graphics.DrawString(itemName8, font, brush, new PointF(200, 780));
                        //        e.Graphics.DrawString(weight, font, brush, new PointF(330, 780));
                        //        e.Graphics.DrawString(count, font, brush, new PointF(490, 780));
                        //        e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 780));
                        //        e.Graphics.DrawString(amount, font, brush, new PointF(550, 780));
                        //        e.Graphics.DrawString(remark, font, brush, new PointF(690, 780));
                        //        #endregion
                        //    }
                        //    else if (b == 9)
                        //    {
                        //        DataRow row4;
                        //        row4 = dt4.Rows[0];
                        //        string itemName9 = row4["item_name"].ToString();
                        //        #region "納品書9行目"
                        //        e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 830));
                        //        e.Graphics.DrawString(itemName9, font, brush, new PointF(200, 830));
                        //        e.Graphics.DrawString(weight, font, brush, new PointF(330, 830));
                        //        e.Graphics.DrawString(count, font, brush, new PointF(490, 830));
                        //        e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 830));
                        //        e.Graphics.DrawString(amount, font, brush, new PointF(550, 830));
                        //        e.Graphics.DrawString(remark, font, brush, new PointF(690, 830));
                        //        #endregion
                        //    }
                        //    if (b == 10)
                        //    {
                        //        DataRow row4;
                        //        row4 = dt4.Rows[0];
                        //        string itemName10 = row4["item_name"].ToString();
                        //        #region "納品書10行目"
                        //        e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 880));
                        //        e.Graphics.DrawString(itemName10, font, brush, new PointF(200, 880));
                        //        e.Graphics.DrawString(weight, font, brush, new PointF(330, 880));
                        //        e.Graphics.DrawString(count, font, brush, new PointF(490, 880));
                        //        e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 880));
                        //        e.Graphics.DrawString(amount, font, brush, new PointF(550, 880));
                        //        e.Graphics.DrawString(remark, font, brush, new PointF(690, 880));
                        //        #endregion
                        //    }
                        //    else
                        //    {

                        //    }
                        //    #endregion

                        //}
                        #endregion
                        #region "顧客情報"
                        DeliveryClientTable = DeliveryClientTable.Clone();
                        deliveryClientTable.Add(DeliveryClientTable);
                            string sql_str6 = "select * from client_m where code = " + ClientCode + ";";
                            adapter = new NpgsqlDataAdapter(sql_str6, conn);
                        adapter.Fill(deliveryClientTable[repeat - 1]) ;
                        #endregion
                        #endregion
                    }
                }
                DialogResult dr = printPreviewDialog1.ShowDialog();
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (pageNumber < repeat)
            {
                if (data == "S")
                {
                    //標準テキスト
                    Font font = new Font("MS Pゴシック", 10.5f, 0, GraphicsUnit.Pixel);
                    Font font1 = new Font("MS Pゴシック", 20f, FontStyle.Bold);
                    Font font3 = new Font("MS Pゴシック", 14f, 0, GraphicsUnit.Pixel);

                    //下線付きテキスト
                    Font font2 = new Font("MS Pゴシック", 14f, FontStyle.Underline, GraphicsUnit.Pixel);    //真ん中で使用

                    Brush brush = new SolidBrush(Color.Black);

                    Pen p = new Pen(Color.Black);

                    #region"Format 変換"
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;    //中央表示

                    StringFormat stringFormat1 = new StringFormat();
                    stringFormat1.Alignment = StringAlignment.Far;      //右寄せ

                    StringFormat stringFormat2 = new StringFormat();
                    stringFormat2.Alignment = StringAlignment.Near;
                    #endregion
                    #region"表の変数"
                    int width = 100;
                    int widthLong = 130;                    //品物詳細と備考の幅
                    int widthWeight = 80;                   //重量の幅
                    int countWidth = 60;                    //数量の幅
                    int widthLong1 = 120;                   //税額金額の幅
                    int widthLong2 = 140;                   //小計と合計金額の幅
                    int d = 50;
                    int d3 = 60;
                    int height = 20;
                    int h = 4;

                    int d2 = 20;    //  調整

                    int x1 = 50;           //表の x 座標
                    int x2 = x1 + width;
                    int x3 = x2 + widthLong;
                    int x4 = x3 + widthWeight;
                    int x5 = x4 + width;
                    int x6 = x5 + countWidth;
                    int x7 = x6 + width;

                    int x01 = 50;
                    int x02 = x01 + width;
                    int x03 = x02 + widthLong2;
                    int x04 = x03 + width;
                    int x05 = x04 + widthLong1;
                    int x06 = x05 + width;


                    int y1 = 200;               //上表の y 座標（１行目）
                    int y12 = y1 + height;      //上表のy 座標（２行目）
                    int y13 = y12 + height;
                    int y14 = y13 + height;
                    int y15 = y14 + height;
                    int y16 = y15 + height;
                    int y17 = y16 + height;
                    int y18 = y17 + height;
                    int y19 = y18 + height;
                    int y110 = y19 + height;
                    int y111 = y110 + height;   //上表のy 座標（１０行目）
                    int y112 = y111 + height;
                    int y113 = y112 + height;
                    int y114 = y113 + height;
                    int y115 = y114 + height;
                    int y116 = y115 + height;

                    int y2 = 650;               //下表の y 座標（１行目）
                    int y22 = y2 + height;      //下表のy 座標（２行目）
                    int y23 = y22 + height;
                    int y24 = y23 + height;
                    int y25 = y24 + height;
                    int y26 = y25 + height;
                    int y27 = y26 + height;
                    int y28 = y27 + height;
                    int y29 = y28 + height;
                    int y210 = y29 + height;
                    int y211 = y210 + height;   //下表のy 座標（１０行目）
                    int y212 = y211 + height;
                    int y213 = y212 + height;
                    int y214 = y213 + height;
                    int y215 = y214 + height;

                    #endregion

                    #region"値の挿入"
                    DataRow row = statementDataTable[pageNumber].Rows[0];
                    deliveryDate = DateTime.Parse(row["delivery_date"].ToString()).ToShortDateString();
                    settlementData = ((DateTime)row["settlement_date"]).ToString("yyyy/MM/dd");
                    settlementMethod = row["payment_method"].ToString();
                    totalMoney = ((decimal)row["total"]).ToString("c0");
                    totalWeight = ((decimal)row["total_weight"]).ToString("n1");
                    totalCount = ((int)row["total_amount"]).ToString("n0");

                    if (totalWeight == "0.0") 
                    {
                        totalWeight = "";
                    }

                    if (totalCount == "0") 
                    {
                        totalCount = "";
                    }

                    DataRow row1 = clientDataTable[pageNumber].Rows[0];
                    Type = (int)row1["type"];
                    companyName = row1["company_name"].ToString();
                    shopName = row1["shop_name"].ToString();
                    name = row1["name"].ToString();
                    tel = row1["phone_number"].ToString();
                    fax = row1["fax_number"].ToString();
                    Address = row1["address"].ToString();
                    birthday = row1["birthday"].ToString();
                    occupation = row1["occupation"].ToString();
                    antiqueLicense = row1["antique_license"].ToString();
                    #endregion

                    #region"法人、個人事業主、一般で共通"
                    //一番上
                    e.Graphics.DrawString("計算書（お客様控え）", font1, brush, new PointF(280, 30));

                    //伝票番号
                    e.Graphics.DrawString(DNumber[pageNumber], font, brush, new PointF(680, 40));
                    e.Graphics.DrawString(DNumber[pageNumber], font, brush, new PointF(680, 600));

                    //右上
                    e.Graphics.DrawString("株式会社 Flawless", font, brush, new PointF(550, 80));
                    e.Graphics.DrawString("〒110-0005", font, brush, new PointF(550, 100));
                    e.Graphics.DrawString("東京都台東区上野5-8-5 フロンティア秋葉原３階", font, brush, new PointF(550, 120));
                    e.Graphics.DrawString("受け渡し日", font, brush, new PointF(600, 140));
                    e.Graphics.DrawString("決済日", font, brush, new PointF(600, 160));
                    e.Graphics.DrawString("決済方法", font, brush, new PointF(600, 180));

                    e.Graphics.DrawString("：" + deliveryDate, font, brush, new PointF(600 + d3, 140));    //受け渡し日
                    e.Graphics.DrawString("：" + settlementData, font, brush, new PointF(600 + d3, 160));    //決済日
                    e.Graphics.DrawString("：" + settlementMethod, font, brush, new PointF(600 + d3, 180));    //決済方法

                    //真ん中

                    e.Graphics.DrawString("計算書（兼領収書）", font1, brush, new PointF(280, 550));
                    e.Graphics.DrawString("株式会社 Flawless 御中", font2, brush, new PointF(x1, 600));

                    //右下

                    e.Graphics.DrawString("受け渡し日", font, brush, new PointF(570, 970));
                    e.Graphics.DrawString("決済日", font, brush, new PointF(570, 990));
                    e.Graphics.DrawString("決済方法", font, brush, new PointF(570, 1010));
                    e.Graphics.DrawString("上記の通り領収致しました。", font, brush, new PointF(520, 1030));
                    e.Graphics.DrawString("署名", font3, brush, new PointF(470, 1070));
                    e.Graphics.DrawString("印", font, brush, new PointF(730, 1020));
                    e.Graphics.DrawString("署名のないものは無効", font, brush, new PointF(570, 1100));

                    e.Graphics.DrawString("：" + deliveryDate, font, brush, new PointF(570 + d3, 970));    //受け渡し日
                    e.Graphics.DrawString("：" + settlementData, font, brush, new PointF(570 + d3, 990));    //決済日
                    e.Graphics.DrawString("：" + settlementMethod, font, brush, new PointF(570 + d3, 1010));    //決済方法
                    e.Graphics.DrawString("：", font3, brush, new PointF(450 + d, 1070));   //署名

                    e.Graphics.DrawRectangle(p, 470, 1090, 250, 0.1f);            //署名下の下線
                    e.Graphics.DrawRectangle(p, 730 - d2, 1020 - d2, d2 * 2 + 10, d2 * 2 + 10);         //印の枠
                    #endregion

                    #region"ページ上のお客様情報"
                    //法人の場合

                    if (Type == 0)
                    {
                        e.Graphics.DrawString("会社名", font, brush, new PointF(x1, 80));
                        e.Graphics.DrawString("店舗名", font, brush, new PointF(x1, 100));
                        e.Graphics.DrawString("担当者名", font, brush, new PointF(x1, 120));
                        e.Graphics.DrawString("TEL", font, brush, new PointF(x1, 140));
                        e.Graphics.DrawString("FAX", font, brush, new PointF(x1, 160));

                        e.Graphics.DrawString("：" + companyName, font, brush, new PointF(x1 + d, 80));
                        e.Graphics.DrawString("：" + shopName, font, brush, new PointF(x1 + d, 100));
                        e.Graphics.DrawString("：" + name + "様", font, brush, new PointF(x1 + d, 120));
                        e.Graphics.DrawString("：" + tel, font, brush, new PointF(x1 + d, 140));
                        e.Graphics.DrawString("：" + fax, font, brush, new PointF(x1 + d, 160));
                    }
                    #region"個人の場合"
                    else if (Type == 1)
                    {
                        if (!string.IsNullOrEmpty(antiqueLicense))     //個人事業主の場合
                        {
                            e.Graphics.DrawString("氏名", font, brush, new PointF(x1, 80));
                            e.Graphics.DrawString("TEL", font, brush, new PointF(x1, 100));
                            e.Graphics.DrawString("FAX", font, brush, new PointF(x1, 120));

                            e.Graphics.DrawString("：" + name + "様", font, brush, new PointF(x1 + d, 80));
                            e.Graphics.DrawString("：" + tel, font, brush, new PointF(x1 + d, 100));
                            e.Graphics.DrawString("：" + fax, font, brush, new PointF(x1 + d, 120));
                        }

                        else
                        {                   //個人
                            e.Graphics.DrawString("氏名", font, brush, new PointF(x1, 80));
                            e.Graphics.DrawString("住所", font, brush, new PointF(x1, 100));
                            e.Graphics.DrawString("TEL", font, brush, new PointF(x1, 120));
                            e.Graphics.DrawString("FAX", font, brush, new PointF(x1, 140));
                            e.Graphics.DrawString("生年月日", font, brush, new PointF(x1, 160)); ;
                            e.Graphics.DrawString("職業", font, brush, new PointF(x1, 180));

                            e.Graphics.DrawString("：" + name + "様", font, brush, new PointF(x1 + d, 80));
                            e.Graphics.DrawString("：" + Address, font, brush, new PointF(x1 + d, 100));
                            e.Graphics.DrawString("：" + tel, font, brush, new PointF(x1 + d, 120));
                            e.Graphics.DrawString("：" + fax, font, brush, new PointF(x1 + d, 140));
                            e.Graphics.DrawString("：" + birthday, font, brush, new PointF(x1 + d, 160)); ;
                            e.Graphics.DrawString("：" + occupation, font, brush, new PointF(x1 + d, 180));
                        }
                    }
                    #endregion
                    #endregion

                    #region"ページ下のお客様情報"
                    //法人の場合

                    if (Type == 0)
                    {
                        e.Graphics.DrawString("会社名", font, brush, new PointF(x1, 950));
                        e.Graphics.DrawString("店舗名", font, brush, new PointF(x1, 970));
                        e.Graphics.DrawString("担当者名", font, brush, new PointF(x1, 990));
                        e.Graphics.DrawString("TEL", font, brush, new PointF(x1, 1010));
                        e.Graphics.DrawString("FAX", font, brush, new PointF(x1, 1030));

                        e.Graphics.DrawString("：" + companyName, font, brush, new PointF(x1 + d, 970));
                        e.Graphics.DrawString("：" + shopName, font, brush, new PointF(x1 + d, 990));
                        e.Graphics.DrawString("：" + name, font, brush, new PointF(x1 + d, 1010));
                        e.Graphics.DrawString("：" + tel, font, brush, new PointF(x1 + d, 1030));
                        e.Graphics.DrawString("：" + fax, font, brush, new PointF(x1 + d, 1050));
                    }

                    #region"個人の場合"
                    else if (Type == 1)
                    {

                        if (!string.IsNullOrEmpty(antiqueLicense))     //個人事業主の場合
                        {
                            e.Graphics.DrawString("氏名", font, brush, new PointF(x1, 950));
                            e.Graphics.DrawString("TEL", font, brush, new PointF(x1, 970));
                            e.Graphics.DrawString("FAX", font, brush, new PointF(x1, 990));

                            e.Graphics.DrawString("：" + name, font, brush, new PointF(x1 + d, 950));
                            e.Graphics.DrawString("：" + tel, font, brush, new PointF(x1 + d, 970));
                            e.Graphics.DrawString("：" + fax, font, brush, new PointF(x1 + d, 990));
                        }
                        else
                        {
                            //個人の場合
                            e.Graphics.DrawString("氏名", font, brush, new PointF(x1, 950));
                            e.Graphics.DrawString("住所", font, brush, new PointF(x1, 970));
                            e.Graphics.DrawString("TEL", font, brush, new PointF(x1, 990));
                            e.Graphics.DrawString("FAX", font, brush, new PointF(x1, 1010));
                            e.Graphics.DrawString("生年月日", font, brush, new PointF(x1, 1030));
                            e.Graphics.DrawString("職業", font, brush, new PointF(x1, 1050));

                            e.Graphics.DrawString("：" + name, font, brush, new PointF(x1 + d, 950));
                            e.Graphics.DrawString("：" + Address, font, brush, new PointF(x1 + d, 970));
                            e.Graphics.DrawString("：" + tel, font, brush, new PointF(x1 + d, 990));
                            e.Graphics.DrawString("：" + fax, font, brush, new PointF(x1 + d, 1010));
                            e.Graphics.DrawString("：" + birthday, font, brush, new PointF(x1 + d, 1030)); ;
                            e.Graphics.DrawString("：" + occupation, font, brush, new PointF(x1 + d, 1050));
                        }
                    }
                    #endregion
                    #endregion

                    #region"印刷プレビュー：表の中身（上と下両方）"
                    //Rectangle((左上座標),幅,高さ,フォーマット)：フォーマットで位置・形式を指定

                    DataRow rowStatementPreview;
                    rowStatementPreview = dataTables[pageNumber].Rows[0];

                    //１行目の表の中身
                    string ItemNamePreview = rowStatementPreview["item_name"].ToString();
                    string ItemDetailPreview = rowStatementPreview["detail"].ToString();
                    string WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                    string UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                    string CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                    string MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                    string RemarkPreview = rowStatementPreview["remarks"].ToString();

                    for (int i = 1; i <= previewRow[pageNumber]; i++)
                    {
                        #region"１行目（ヘッダー）"
                        e.Graphics.DrawString("品目", font3, brush, new RectangleF(x1, y1, width, height), stringFormat);
                        e.Graphics.DrawString("品物詳細", font3, brush, new RectangleF(x2, y1, widthLong, height), stringFormat);
                        e.Graphics.DrawString("重量", font3, brush, new RectangleF(x3, y1, widthWeight, height), stringFormat);
                        e.Graphics.DrawString("単価", font3, brush, new RectangleF(x4, y1, width, height), stringFormat);
                        e.Graphics.DrawString("数量", font3, brush, new RectangleF(x5, y1, countWidth, height), stringFormat);
                        e.Graphics.DrawString("金額", font3, brush, new RectangleF(x6, y1, width, height), stringFormat);
                        e.Graphics.DrawString("備考", font3, brush, new RectangleF(x7, y1, widthLong, height), stringFormat);

                        e.Graphics.DrawString("品目", font3, brush, new RectangleF(x1, y2, width, height), stringFormat);
                        e.Graphics.DrawString("品物詳細", font3, brush, new RectangleF(x2, y2, widthLong, height), stringFormat);
                        e.Graphics.DrawString("重量", font3, brush, new RectangleF(x3, y2, widthWeight, height), stringFormat);
                        e.Graphics.DrawString("単価", font3, brush, new RectangleF(x4, y2, width, height), stringFormat);
                        e.Graphics.DrawString("数量", font3, brush, new RectangleF(x5, y2, countWidth, height), stringFormat);
                        e.Graphics.DrawString("金額", font3, brush, new RectangleF(x6, y2, width, height), stringFormat);
                        e.Graphics.DrawString("備考", font3, brush, new RectangleF(x7, y2, widthLong, height), stringFormat);
                        #endregion

                        #region "ヘッダー以下"
                        switch (i)
                        {
                            #region"２行目"
                            case 1:
                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y12 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y12 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y12 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y12 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y12 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y12 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y12 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y22 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y22 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y22 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y22 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y22 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y22 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y22 + h, widthLong, height), stringFormat2);
                                break;
                            #endregion
                            #region"３行目"
                            case 2:
                                rowStatementPreview = dataTables[pageNumber].Rows[1];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y13 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y13 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y13 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y13 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y13 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y13 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y13 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y23 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y23 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y23 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y23 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y23 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y23 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y23 + h, widthLong, height), stringFormat2);

                                break;
                            #endregion
                            #region"４行目"
                            case 3:
                                rowStatementPreview = dataTables[pageNumber].Rows[2];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y14 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y14 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y14 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y14 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y14 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y14 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y14 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y24 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y24 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y24 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y24 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y24 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y24 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y24 + h, widthLong, height), stringFormat2);

                                break;
                            #endregion
                            #region"５行目"
                            case 4:
                                rowStatementPreview = dataTables[pageNumber].Rows[3];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y15 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y15 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y15 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y15 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y15 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y15 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y15 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y25 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y25 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y25 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y25 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y25 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y25 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y25 + h, widthLong, height), stringFormat2);

                                break;

                            #endregion
                            #region"６行目"
                            case 5:
                                rowStatementPreview = dataTables[pageNumber].Rows[4];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y16 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y16 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y16 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y16 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y16 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y16 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y16 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y26 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y26 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y26 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y26 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y26 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y26 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y26 + h, widthLong, height), stringFormat2);

                                break;
                            #endregion
                            #region"７行目"
                            case 6:
                                rowStatementPreview = dataTables[pageNumber].Rows[5];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y17 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y17 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y17 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y17 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y17 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y17 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y17 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y27 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y27 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y27 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y27 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y27 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y27 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y27 + h, widthLong, height), stringFormat2);

                                break;
                            #endregion
                            #region"８行目"
                            case 7:
                                rowStatementPreview = dataTables[pageNumber].Rows[6];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y18 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y18 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y18 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y18 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y18 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y18 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y18 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y28 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y28 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y28 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y28 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y28 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y28 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y28 + h, widthLong, height), stringFormat2);

                                break;
                            #endregion
                            #region"９行目"
                            case 8:
                                rowStatementPreview = dataTables[pageNumber].Rows[7];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y19 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y19 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y19 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y19 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y19 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y19 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y19 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y29 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y29 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y29 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y29 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y29 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y29 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y29 + h, widthLong, height), stringFormat2);

                                break;
                            #endregion
                            #region"１０行目"
                            case 9:
                                rowStatementPreview = dataTables[pageNumber].Rows[8];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y110 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y110 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y110 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y110 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y110 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y110 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y110 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y210 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y210 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y210 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y210 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y210 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y210 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y210 + h, widthLong, height), stringFormat2);

                                break;
                            #endregion
                            #region"１１行目"
                            case 10:
                                rowStatementPreview = dataTables[pageNumber].Rows[9];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y111 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y111 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y111 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y111 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y111 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y111 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y111 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y211 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y211 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y211 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y211 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y211 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y211 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y211 + h, widthLong, height), stringFormat2);

                                break;
                            #endregion
                            #region"１２行目"
                            case 11:
                                rowStatementPreview = dataTables[pageNumber].Rows[10];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y112 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y112 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y112 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y112 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y112 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y112 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y112 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y212 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y212 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y212 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y212 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y212 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y212 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y212 + h, widthLong, height), stringFormat2);

                                break;
                            #endregion
                            #region"１３行目"
                            case 12:
                                rowStatementPreview = dataTables[pageNumber].Rows[11];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y113 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y113 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y113 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y113 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y113 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y113 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y113 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y213 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y213 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y213 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y213 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y213 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y213 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y213 + h, widthLong, height), stringFormat2);

                                break;
                            #endregion
                            #region"１４行目"
                            case 13:
                                rowStatementPreview = dataTables[pageNumber].Rows[12];

                                ItemNamePreview = rowStatementPreview["item_name"].ToString();
                                ItemDetailPreview = rowStatementPreview["detail"].ToString();
                                WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
                                UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
                                CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
                                MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
                                RemarkPreview = rowStatementPreview["remarks"].ToString();

                                if (WeightPreview == "0.0")
                                {
                                    WeightPreview = "";
                                }
                                if (CountPreview == "0")
                                {
                                    CountPreview = "";
                                }

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y114 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y114 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y114 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y114 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y114 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y114 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y114 + h, widthLong, height), stringFormat2);

                                e.Graphics.DrawString(ItemNamePreview, font, brush, new RectangleF(x1, y214 + h, width, height), stringFormat);
                                e.Graphics.DrawString(ItemDetailPreview, font, brush, new RectangleF(x2, y214 + h, widthLong, height), stringFormat2);
                                e.Graphics.DrawString(WeightPreview, font, brush, new RectangleF(x3, y214 + h, widthWeight, height), stringFormat1);
                                e.Graphics.DrawString(UnitPricePreview, font, brush, new RectangleF(x4, y214 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(CountPreview, font, brush, new RectangleF(x5, y214 + h, countWidth, height), stringFormat1);
                                e.Graphics.DrawString(MoneyPreview, font, brush, new RectangleF(x6, y214 + h, width, height), stringFormat1);
                                e.Graphics.DrawString(RemarkPreview, font, brush, new RectangleF(x7, y214 + h, widthLong, height), stringFormat2);

                                break;
                                #endregion
                        }
                        #endregion
                    }
                    #region"１５行目（上の表は総重量と総数、下の表は小計と合計）"
                    e.Graphics.DrawString("", font, brush, new RectangleF(x1, y115 + h, width, height), stringFormat);
                    e.Graphics.DrawString("総重量", font, brush, new RectangleF(x2, y115 + h, widthLong, height), stringFormat);
                    e.Graphics.DrawString(totalWeight, font, brush, new RectangleF(x3, y115 + h, widthWeight, height), stringFormat1);
                    e.Graphics.DrawString("総数", font, brush, new RectangleF(x4, y115 + h, width, height), stringFormat);
                    e.Graphics.DrawString(totalCount, font, brush, new RectangleF(x5, y115 + h, countWidth, height), stringFormat1);

                    e.Graphics.DrawString("小計", font, brush, new RectangleF(x01, y215 + h, width, height), stringFormat);
                    e.Graphics.DrawString(totalMoney, font, brush, new RectangleF(x02, y215 + h, widthLong2, height), stringFormat1);
                    e.Graphics.DrawString("税額", font, brush, new RectangleF(x03, y215 + h, width, height), stringFormat);
                    e.Graphics.DrawString("", font, brush, new RectangleF(x04, y215 + h, widthLong1, height), stringFormat1);
                    e.Graphics.DrawString("合計", font, brush, new RectangleF(x05, y215 + h, width, height), stringFormat);
                    e.Graphics.DrawString(totalMoney, font, brush, new RectangleF(x06, y215 + h, widthLong2, height), stringFormat1);
                    #endregion
                    #region"１６行目（上の表は小計と合計のみ）"
                    e.Graphics.DrawString("小計", font, brush, new RectangleF(x01, y116 + h, width, height), stringFormat);
                    e.Graphics.DrawString(totalMoney, font, brush, new RectangleF(x02, y116 + h, widthLong2, height), stringFormat1);
                    e.Graphics.DrawString("税額", font, brush, new RectangleF(x03, y116 + h, width, height), stringFormat);
                    e.Graphics.DrawString("", font, brush, new RectangleF(x04, y116 + h, widthLong1, height), stringFormat1);
                    e.Graphics.DrawString("合計", font, brush, new RectangleF(x05, y116 + h, width, height), stringFormat);
                    e.Graphics.DrawString(totalMoney, font, brush, new RectangleF(x06, y116 + h, widthLong2, height), stringFormat1);
                    #endregion
                    #endregion

                    #region"上の表　枠線"
                    //Rectangle(左上隅の x 座標, 左上隅の y 座標, 四角形の幅, 四角形の高さ)
                    #region"１行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y1, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y1, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y1, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y1, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y1, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y1, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y1, widthLong, height));
                    #endregion
                    #region"２行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y12, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y12, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y12, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y12, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y12, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y12, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y12, widthLong, height));
                    #endregion
                    #region"３行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y13, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y13, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y13, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y13, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y13, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y13, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y13, widthLong, height));
                    #endregion
                    #region"４行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y14, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y14, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y14, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y14, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y14, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y14, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y14, widthLong, height));
                    #endregion
                    #region"５行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y15, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y15, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y15, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y15, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y15, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y15, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y15, widthLong, height));
                    #endregion
                    #region"６行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y16, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y16, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y16, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y16, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y16, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y16, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y16, widthLong, height));
                    #endregion
                    #region"７行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y17, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y17, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y17, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y17, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y17, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y17, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y17, widthLong, height));
                    #endregion
                    #region"８行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y18, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y18, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y18, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y18, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y18, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y18, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y18, widthLong, height));
                    #endregion
                    #region"９行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y19, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y19, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y19, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y19, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y19, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y19, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y19, widthLong, height));
                    #endregion
                    #region"１０行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y110, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y110, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y110, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y110, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y110, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y110, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y110, widthLong, height));
                    #endregion
                    #region"１１行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y111, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y111, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y111, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y111, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y111, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y111, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y111, widthLong, height));
                    #endregion
                    #region"１２行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y112, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y112, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y112, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y112, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y112, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y112, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y112, widthLong, height));
                    #endregion
                    #region"１３行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y113, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y113, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y113, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y113, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y113, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y113, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y113, widthLong, height));
                    #endregion
                    #region"１４行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y114, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y114, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y114, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y114, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y114, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y114, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y114, widthLong, height));
                    #endregion
                    #region"１５行目（総重量と総数）"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y115, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y115, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y115, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y115, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y115, countWidth, height));
                    #endregion
                    #region"１６行目（小計・税額・合計）"
                    e.Graphics.DrawRectangle(p, new Rectangle(x01, y116, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x02, y116, widthLong2, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x03, y116, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x04, y116, widthLong1, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x05, y116, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x06, y116, widthLong2, height));
                    #endregion

                    #endregion

                    #region"下の表　枠線"

                    #region"１行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y2, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y2, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y2, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y2, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y2, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y2, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y2, widthLong, height));
                    #endregion
                    #region"２行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y22, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y22, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y22, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y22, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y22, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y22, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y22, widthLong, height));
                    #endregion
                    #region"３行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y23, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y23, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y23, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y23, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y23, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y23, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y23, widthLong, height));
                    #endregion
                    #region"４行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y24, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y24, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y24, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y24, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y24, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y24, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y24, widthLong, height));
                    #endregion
                    #region"５行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y25, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y25, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y25, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y25, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y25, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y25, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y25, widthLong, height));
                    #endregion
                    #region"６行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y26, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y26, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y26, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y26, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y26, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y26, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y26, widthLong, height));
                    #endregion
                    #region"７行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y27, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y27, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y27, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y27, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y27, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y27, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y27, widthLong, height));
                    #endregion
                    #region"８行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y28, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y28, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y28, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y28, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y28, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y28, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y28, widthLong, height));
                    #endregion
                    #region"９行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y29, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y29, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y29, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y29, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y29, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y29, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y29, widthLong, height));
                    #endregion
                    #region"１０行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y210, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y210, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y210, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y210, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y210, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y210, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y210, widthLong, height));
                    #endregion
                    #region"１１行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y211, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y211, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y211, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y211, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y211, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y211, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y211, widthLong, height));
                    #endregion
                    #region"１２行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y212, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y212, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y212, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y212, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y212, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y212, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y212, widthLong, height));
                    #endregion
                    #region"１３行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y213, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y213, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y213, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y213, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y213, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y213, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y213, widthLong, height));
                    #endregion
                    #region"１４行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(x1, y214, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x2, y214, widthLong, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x3, y214, widthWeight, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x4, y214, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x5, y214, countWidth, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x6, y214, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x7, y214, widthLong, height));
                    #endregion
                    #region"１５行目（小計・総額・合計）"
                    e.Graphics.DrawRectangle(p, new Rectangle(x01, y215, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x02, y215, widthLong2, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x03, y215, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x04, y215, widthLong1, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x05, y215, width, height));
                    e.Graphics.DrawRectangle(p, new Rectangle(x06, y215, widthLong2, height));
                    #endregion

                    #endregion

                    pageNumber++;
                    if (pageNumber < repeat)
                    {
                        e.HasMorePages = true;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }

                }

                else if (data == "D")
                {
                    Font font = new Font("MS Pゴシック", 10.5f);
                    Font font1 = new Font("メイリオ", 36f);
                    Brush brush = new SolidBrush(Color.Black);

                    #region "納品書 下の部分"
                    DataRow row;
                    row = deliveryTable[pageNumber].Rows[0];
                    string method = row["payment_method"].ToString();
                    string orderDate = row["order_date"].ToString();
                    string deliveryDate = row["delivery_date"].ToString();
                    string settlementDate = ((DateTime)row["settlement_date"]).ToString("yyyy/MM/dd");
                    string bank = row["account_payble"].ToString();
                    string vat = row["vat"].ToString();
                    string vat_rate = row["vat_rate"].ToString();
                    string vat_amount = row["vat_amount"].ToString();
                    string sub_total = row["sub_total"].ToString();
                    string total = row["total"].ToString();
                    string customer = row["honorific_title"].ToString();
                    string name = row["name"].ToString();
                    string sealPrint = row["seaal_print"].ToString();
                    string TotalWeight = row["total_weight"].ToString();
                    string TotalCount = row["total_count"].ToString();
                    string kind = row["type"].ToString();
                    #endregion

                    #region "納品書　表の部分"
                    int a = deliveryCalcTable[pageNumber].Rows.Count;
                    DataRow row5;
                    row5 = deliveryCalcTable[pageNumber].Rows[0];
                    string weight = ((decimal)row5["weight"]).ToString("n1");
                    string count = ((int)row5["count"]).ToString("n0");
                    string remark = row5["remarks"].ToString();
                    string unit_price = ((decimal)row5["unit_price"]).ToString("n0");
                    string amount = ((decimal)row5["amount"]).ToString("c0");
                    string itemName = row5["item_name"].ToString();
                    string detail = row5["detail"].ToString();

                    for (int i = 0; i < a; i++)
                    {
                        #region "表の中身"
                        switch (i)
                        {
                            #region "納品書1行目"
                            case 0:
                                if (weight == "0.0")
                                {
                                    weight = "";
                                }
                                if (count == "0")
                                {
                                    count = "";
                                }

                                e.Graphics.DrawString(itemName, font, brush, new PointF(30, 430));
                                e.Graphics.DrawString(detail, font, brush, new PointF(200, 430));
                                e.Graphics.DrawString(weight, font, brush, new PointF(330, 430));
                                e.Graphics.DrawString(count, font, brush, new PointF(490, 430));
                                e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 430));
                                e.Graphics.DrawString(amount, font, brush, new PointF(550, 430));
                                e.Graphics.DrawString(remark, font, brush, new PointF(690, 430));
                                break;
                            #endregion
                            #region "納品書2行目"
                            case 1:
                                row5 = deliveryCalcTable[pageNumber].Rows[i];
                                weight = ((decimal)row5["weight"]).ToString("n1");
                                count = ((int)row5["count"]).ToString("n0");
                                remark = row5["remarks"].ToString();
                                unit_price = ((decimal)row5["unit_price"]).ToString("n0");
                                amount = ((decimal)row5["amount"]).ToString("c0");
                                itemName = row5["item_name"].ToString();
                                detail = row5["detail"].ToString();

                                if (weight == "0.0")
                                {
                                    weight = "";
                                }
                                if (count == "0")
                                {
                                    count = "";
                                }

                                e.Graphics.DrawString(itemName, font, brush, new PointF(30, 480));
                                e.Graphics.DrawString(detail, font, brush, new PointF(200, 480));
                                e.Graphics.DrawString(weight, font, brush, new PointF(330, 480));
                                e.Graphics.DrawString(count, font, brush, new PointF(490, 480));
                                e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 480));
                                e.Graphics.DrawString(amount, font, brush, new PointF(550, 480));
                                e.Graphics.DrawString(remark, font, brush, new PointF(690, 480));
                                break;
                            #endregion
                            #region "納品書3行目"
                            case 2:
                                row5 = deliveryCalcTable[pageNumber].Rows[i];
                                weight = ((decimal)row5["weight"]).ToString("n1");
                                count = ((int)row5["count"]).ToString("n0");
                                remark = row5["remarks"].ToString();
                                unit_price = ((decimal)row5["unit_price"]).ToString("n0");
                                amount = ((decimal)row5["amount"]).ToString("c0");
                                itemName = row5["item_name"].ToString();
                                detail = row5["detail"].ToString();

                                if (weight == "0.0")
                                {
                                    weight = "";
                                }
                                if (count == "0")
                                {
                                    count = "";
                                }

                                e.Graphics.DrawString(itemName, font, brush, new PointF(30, 530));
                                e.Graphics.DrawString(detail, font, brush, new PointF(200, 530));
                                e.Graphics.DrawString(weight, font, brush, new PointF(330, 530));
                                e.Graphics.DrawString(count, font, brush, new PointF(490, 530));
                                e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 530));
                                e.Graphics.DrawString(amount, font, brush, new PointF(550, 530));
                                e.Graphics.DrawString(remark, font, brush, new PointF(690, 530));
                                break;
                            #endregion
                            #region "納品書4行目"
                            case 3:
                                row5 = deliveryCalcTable[pageNumber].Rows[i];
                                weight = ((decimal)row5["weight"]).ToString("n1");
                                count = ((int)row5["count"]).ToString("n0");
                                remark = row5["remarks"].ToString();
                                unit_price = ((decimal)row5["unit_price"]).ToString("n0");
                                amount = ((decimal)row5["amount"]).ToString("c0");
                                itemName = row5["item_name"].ToString();
                                detail = row5["detail"].ToString();

                                if (weight == "0.0")
                                {
                                    weight = "";
                                }
                                if (count == "0")
                                {
                                    count = "";
                                }

                                e.Graphics.DrawString(itemName, font, brush, new PointF(30, 580));
                                e.Graphics.DrawString(detail, font, brush, new PointF(200, 580));
                                e.Graphics.DrawString(weight, font, brush, new PointF(330, 580));
                                e.Graphics.DrawString(count, font, brush, new PointF(490, 580));
                                e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 580));
                                e.Graphics.DrawString(amount, font, brush, new PointF(550, 580));
                                e.Graphics.DrawString(remark, font, brush, new PointF(690, 580));
                                break;
                            #endregion
                            #region "納品書5行目"
                            case 4:
                                row5 = deliveryCalcTable[pageNumber].Rows[i];
                                weight = ((decimal)row5["weight"]).ToString("n1");
                                count = ((int)row5["count"]).ToString("n0");
                                remark = row5["remarks"].ToString();
                                unit_price = ((decimal)row5["unit_price"]).ToString("n0");
                                amount = ((decimal)row5["amount"]).ToString("c0");
                                itemName = row5["item_name"].ToString();
                                detail = row5["detail"].ToString();

                                if (weight == "0.0")
                                {
                                    weight = "";
                                }
                                if (count == "0")
                                {
                                    count = "";
                                }

                                e.Graphics.DrawString(itemName, font, brush, new PointF(30, 630));
                                e.Graphics.DrawString(detail, font, brush, new PointF(200, 630));
                                e.Graphics.DrawString(weight, font, brush, new PointF(330, 630));
                                e.Graphics.DrawString(count, font, brush, new PointF(490, 630));
                                e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 630));
                                e.Graphics.DrawString(amount, font, brush, new PointF(550, 630));
                                e.Graphics.DrawString(remark, font, brush, new PointF(690, 630));
                                break;

                            #endregion
                            #region "納品書6行目"
                            case 5:
                                row5 = deliveryCalcTable[pageNumber].Rows[i];
                                weight = ((decimal)row5["weight"]).ToString("n1");
                                count = ((int)row5["count"]).ToString("n0");
                                remark = row5["remarks"].ToString();
                                unit_price = ((decimal)row5["unit_price"]).ToString("n0");
                                amount = ((decimal)row5["amount"]).ToString("c0");
                                itemName = row5["item_name"].ToString();
                                detail = row5["detail"].ToString();

                                if (weight == "0.0")
                                {
                                    weight = "";
                                }
                                if (count == "0")
                                {
                                    count = "";
                                }

                                e.Graphics.DrawString(itemName, font, brush, new PointF(30, 680));
                                e.Graphics.DrawString(detail, font, brush, new PointF(200, 680));
                                e.Graphics.DrawString(weight, font, brush, new PointF(330, 680));
                                e.Graphics.DrawString(count, font, brush, new PointF(490, 680));
                                e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 680));
                                e.Graphics.DrawString(amount, font, brush, new PointF(550, 680));
                                e.Graphics.DrawString(remark, font, brush, new PointF(690, 680));
                                break;
                            #endregion
                            #region "納品書7行目"
                            case 6:
                                row5 = deliveryCalcTable[pageNumber].Rows[i];
                                weight = ((decimal)row5["weight"]).ToString("n1");
                                count = ((int)row5["count"]).ToString("n0");
                                remark = row5["remarks"].ToString();
                                unit_price = ((decimal)row5["unit_price"]).ToString("n0");
                                amount = ((decimal)row5["amount"]).ToString("c0");
                                itemName = row5["item_name"].ToString();
                                detail = row5["detail"].ToString();

                                if (weight == "0.0")
                                {
                                    weight = "";
                                }
                                if (count == "0")
                                {
                                    count = "";
                                }

                                e.Graphics.DrawString(itemName, font, brush, new PointF(30, 730));
                                e.Graphics.DrawString(detail, font, brush, new PointF(200, 730));
                                e.Graphics.DrawString(weight, font, brush, new PointF(330, 730));
                                e.Graphics.DrawString(count, font, brush, new PointF(490, 730));
                                e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 730));
                                e.Graphics.DrawString(amount, font, brush, new PointF(550, 730));
                                e.Graphics.DrawString(remark, font, brush, new PointF(690, 730));
                                break;
                            #endregion
                            #region "納品書8行目"
                            case 7:
                                row5 = deliveryCalcTable[pageNumber].Rows[i];
                                weight = ((decimal)row5["weight"]).ToString("n1");
                                count = ((int)row5["count"]).ToString("n0");
                                remark = row5["remarks"].ToString();
                                unit_price = ((decimal)row5["unit_price"]).ToString("n0");
                                amount = ((decimal)row5["amount"]).ToString("c0");
                                itemName = row5["item_name"].ToString();
                                detail = row5["detail"].ToString();

                                if (weight == "0.0")
                                {
                                    weight = "";
                                }
                                if (count == "0")
                                {
                                    count = "";
                                }

                                e.Graphics.DrawString(itemName, font, brush, new PointF(30, 780));
                                e.Graphics.DrawString(detail, font, brush, new PointF(200, 780));
                                e.Graphics.DrawString(weight, font, brush, new PointF(330, 780));
                                e.Graphics.DrawString(count, font, brush, new PointF(490, 780));
                                e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 780));
                                e.Graphics.DrawString(amount, font, brush, new PointF(550, 780));
                                e.Graphics.DrawString(remark, font, brush, new PointF(690, 780));
                                break;
                            #endregion
                            #region "納品書9行目"
                            case 8:
                                row5 = deliveryCalcTable[pageNumber].Rows[i];
                                weight = ((decimal)row5["weight"]).ToString("n1");
                                count = ((int)row5["count"]).ToString("n0");
                                remark = row5["remarks"].ToString();
                                unit_price = ((decimal)row5["unit_price"]).ToString("n0");
                                amount = ((decimal)row5["amount"]).ToString("c0");
                                itemName = row5["item_name"].ToString();
                                detail = row5["detail"].ToString();

                                if (weight == "0.0")
                                {
                                    weight = "";
                                }
                                if (count == "0")
                                {
                                    count = "";
                                }

                                e.Graphics.DrawString(itemName, font, brush, new PointF(30, 830));
                                e.Graphics.DrawString(detail, font, brush, new PointF(200, 830));
                                e.Graphics.DrawString(weight, font, brush, new PointF(330, 830));
                                e.Graphics.DrawString(count, font, brush, new PointF(490, 830));
                                e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 830));
                                e.Graphics.DrawString(amount, font, brush, new PointF(550, 830));
                                e.Graphics.DrawString(remark, font, brush, new PointF(690, 830));
                                break;
                            #endregion
                            #region "納品書10行目"
                            case 9:
                                row5 = deliveryCalcTable[pageNumber].Rows[i];
                                weight = ((decimal)row5["weight"]).ToString("n1");
                                count = ((int)row5["count"]).ToString("n0");
                                remark = row5["remarks"].ToString();
                                unit_price = ((decimal)row5["unit_price"]).ToString("n0");
                                amount = ((decimal)row5["amount"]).ToString("c0");
                                itemName = row5["item_name"].ToString();
                                detail = row5["detail"].ToString();

                                if (weight == "0.0")
                                {
                                    weight = "";
                                }
                                if (count == "0")
                                {
                                    count = "";
                                }

                                e.Graphics.DrawString(itemName, font, brush, new PointF(30, 880));
                                e.Graphics.DrawString(detail, font, brush, new PointF(200, 880));
                                e.Graphics.DrawString(weight, font, brush, new PointF(330, 880));
                                e.Graphics.DrawString(count, font, brush, new PointF(490, 880));
                                e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 880));
                                e.Graphics.DrawString(amount, font, brush, new PointF(550, 880));
                                e.Graphics.DrawString(remark, font, brush, new PointF(690, 880));
                                break;
                                #endregion
                        }
                        #endregion
                    }

                    #region "顧客情報"
                    DataRow row6 = deliveryClientTable[pageNumber].Rows[0];
                    string address = row6["address"].ToString();
                    string phoneNumber = row6["phone_number"].ToString();
                    string faxNumber = row6["fax_number"].ToString();
                    string PostalCode1 = row6["postal_code1"].ToString();
                    string PostalCode2 = row6["postal_code2"].ToString();
                    #region"個人情報記入"
                    e.Graphics.DrawString("〒" + PostalCode1 + PostalCode2, font, brush, new PointF(360, 80));
                    e.Graphics.DrawString(address, font, brush, new PointF(360, 100));
                    e.Graphics.DrawString("(TEL)" + phoneNumber, font, brush, new PointF(360, 120));
                    e.Graphics.DrawString("(FAX)" + faxNumber, font, brush, new PointF(520, 120));
                    #endregion
                    #endregion
                    #endregion


                    #region "タイトル"
                    e.Graphics.DrawString("納品書", font, brush, new PointF(40, 120));
                    e.Graphics.DrawString("管理番号", font, brush, new PointF(40, 220));
                    e.Graphics.DrawString("注文日", font, brush, new PointF(540, 220));
                    e.Graphics.DrawString(kind, font, brush, new PointF(540, 270));
                    e.Graphics.DrawString("決済日", font, brush, new PointF(540, 320));
                    e.Graphics.DrawString("印鑑を印刷する", font, brush, new PointF(350, 200));
                    e.Graphics.DrawString("品名", font, brush, new PointF(60, 380));
                    e.Graphics.DrawString("品物詳細", font, brush, new PointF(180, 380));
                    e.Graphics.DrawString("重量", font, brush, new PointF(310, 380));
                    e.Graphics.DrawString("単価", font, brush, new PointF(390, 380));
                    e.Graphics.DrawString("数量", font, brush, new PointF(470, 380));
                    e.Graphics.DrawString("金額", font, brush, new PointF(550, 380));
                    e.Graphics.DrawString("備考", font, brush, new PointF(690, 380));
                    e.Graphics.DrawString("総重量", font, brush, new PointF(200, 910));
                    e.Graphics.DrawString("総数", font, brush, new PointF(390, 910));
                    e.Graphics.DrawString("お支払方法", font, brush, new PointF(35, 950));
                    e.Graphics.DrawString("振込先", font, brush, new PointF(50, 1050));
                    e.Graphics.DrawString("小計", font, brush, new PointF(310, 950));
                    e.Graphics.DrawString("消費税区分", font, brush, new PointF(310, 990));
                    e.Graphics.DrawString("消費税率", font, brush, new PointF(310, 1030));
                    e.Graphics.DrawString("消費税額", font, brush, new PointF(310, 1070));
                    e.Graphics.DrawString("合計金額", font, brush, new PointF(310, 1110));
                    #endregion
                    #region "記入事項"
                    e.Graphics.DrawString(name, font, brush, new PointF(70, 50));
                    e.Graphics.DrawString(customer, font, brush, new PointF(260, 50));
                    e.Graphics.DrawString(CNumber[pageNumber], font, brush, new PointF(200, 220));
                    e.Graphics.DrawString(orderDate, font, brush, new PointF(620, 220));
                    e.Graphics.DrawString(deliveryDate, font, brush, new PointF(620, 270));
                    e.Graphics.DrawString(settlementDate, font, brush, new PointF(620, 320));
                    e.Graphics.DrawString(method, font, brush, new PointF(180, 950));
                    e.Graphics.DrawString(sub_total, font, brush, new PointF(690, 950));
                    e.Graphics.DrawString(bank, font, brush, new PointF(180, 1050));
                    e.Graphics.DrawString(vat, font, brush, new PointF(690, 990));
                    e.Graphics.DrawString(vat_rate + "％", font, brush, new PointF(690, 1030));
                    e.Graphics.DrawString(vat_amount, font, brush, new PointF(690, 1070));
                    e.Graphics.DrawString(total, font, brush, new PointF(690, 1110));
                    e.Graphics.DrawString(TotalWeight, font, brush, new PointF(330, 910));
                    e.Graphics.DrawString(TotalCount, font, brush, new PointF(480, 910));
                    if (sealPrint == "する")
                    {
                        e.Graphics.DrawString("✓", font1, brush, new PointF(360, 200));
                    }
                    //e.Graphics.DrawString(remark, font, brush, new PointF(690, 430));

                    #endregion
                    Pen p = new Pen(Color.Black);
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 30, 200, 50)); // 座標(20,100)に幅200，高さ50の四角形
                    e.Graphics.DrawRectangle(p, new Rectangle(220, 30, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 100, 300, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 200, 100, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(120, 200, 150, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(380, 220, 30, 30));
                    #region "注文日"
                    e.Graphics.DrawRectangle(p, new Rectangle(600, 200, 170, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(600, 250, 170, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(600, 300, 170, 50));
                    #endregion
                    #region"枠"
                    #region "見出し"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 370, 120, 30));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 370, 150, 30));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 370, 80, 30));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 370, 80, 30));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 370, 80, 30));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 370, 80, 30));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 370, 150, 30));
                    #endregion
                    #region "1行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 400, 120, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 400, 150, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 400, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 400, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 400, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 400, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 400, 150, 50));
                    #endregion
                    #region "2行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 450, 120, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 450, 150, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 450, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 450, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 450, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 450, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 450, 150, 50));
                    #endregion
                    #region "3行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 500, 120, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 500, 150, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 500, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 500, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 500, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 500, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 500, 150, 50));
                    #endregion
                    #region "4行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 550, 120, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 550, 150, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 550, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 550, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 550, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 550, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 550, 150, 50));
                    #endregion
                    #region "5行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 600, 120, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 600, 150, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 600, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 600, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 600, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 600, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 600, 150, 50));
                    #endregion
                    #region "6行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 650, 120, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 650, 150, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 650, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 650, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 650, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 650, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 650, 150, 50));
                    #endregion
                    #region "7行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 700, 120, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 700, 150, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 700, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 700, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 700, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 700, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 700, 150, 50));
                    #endregion
                    #region "8行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 750, 120, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 750, 150, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 750, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 750, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 750, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 750, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 750, 150, 50));
                    #endregion
                    #region "9行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 800, 120, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 800, 150, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 800, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 800, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 800, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 800, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 800, 150, 50));
                    #endregion
                    #region "10行目"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 850, 120, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 850, 150, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 850, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 850, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 850, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 850, 80, 50));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 850, 150, 50));
                    #endregion
                    #region "総数"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 900, 270, 30));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 900, 80, 30));
                    e.Graphics.DrawRectangle(p, new Rectangle(370, 900, 80, 30));
                    e.Graphics.DrawRectangle(p, new Rectangle(450, 900, 80, 30));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 900, 80, 30));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 900, 150, 30));
                    #endregion
                    #region　"お支払方法"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 930, 120, 40));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 930, 150, 40));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 930, 240, 40));
                    e.Graphics.DrawRectangle(p, new Rectangle(530, 930, 80, 40));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 930, 150, 40));
                    #endregion
                    #region "振込先"
                    e.Graphics.DrawRectangle(p, new Rectangle(20, 970, 120, 160));
                    e.Graphics.DrawRectangle(p, new Rectangle(140, 970, 150, 160));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 970, 320, 40));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 970, 150, 40));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 1010, 320, 40));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 1010, 150, 40));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 1050, 320, 40));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 1050, 150, 40));
                    e.Graphics.DrawRectangle(p, new Rectangle(290, 1090, 320, 40));
                    e.Graphics.DrawRectangle(p, new Rectangle(610, 1090, 150, 40));
                    #endregion
                    #endregion
                    #region "ロゴ"
                    Image newImage = Image.FromFile(@"C:\Users\Flawlessロゴ.png");
                    Rectangle destRect = new Rectangle(350, 20, 400, 50);
                    e.Graphics.DrawImage(newImage, destRect);
                    #endregion
                    #region "印鑑"
                    Image newImage1 = Image.FromFile(@"C:\Users\印鑑.png");
                    Rectangle destRect2 = new Rectangle(580, -50, 300, 300);
                    e.Graphics.DrawImage(newImage1, destRect2);
                    #endregion


                    pageNumber++;
                    if (pageNumber < repeat)
                    {
                        e.HasMorePages = true;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }
                }
            }
        }


        #endregion

        private void DataSearchResults_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
