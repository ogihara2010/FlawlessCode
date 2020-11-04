using System;
using System.Windows.Forms;
using System.Data;
using Npgsql;
using System.Drawing;

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
        bool CarryOver;
        bool MonthCatalog;
        bool notLoad = false;

        #region"表の合計、合計の変数"
        decimal TotalPurchase;
        decimal TotalWholesale;
        decimal TotalProfit;
        decimal MetalPurchase;
        decimal MetalWholesale;
        decimal MetalProfit;
        decimal DiamondPurchase;
        decimal DiamondWholesale;
        decimal DiamondProfit;
        decimal BrandPurchase;
        decimal BrandWholesale;
        decimal BrandProfit;
        decimal ProductPurchase;
        decimal ProductWholesale;
        decimal ProductProfit;
        decimal OtherPurchase;
        decimal OtherWholesale;
        decimal OtherProfit;

        decimal TotalPurchase2;
        decimal TotalWholesale2;
        decimal TotalProfit2;
        decimal MetalPurchase2;
        decimal MetalWholesale2;
        decimal MetalProfit2;
        decimal DiamondPurchase2;
        decimal DiamondWholesale2;
        decimal DiamondProfit2;
        decimal BrandPurchase2;
        decimal BrandWholesale2;
        decimal BrandProfit2;
        decimal ProductPurchase2;
        decimal ProductWholesale2;
        decimal ProductProfit2;
        decimal OtherPurchase2;
        decimal OtherWholesale2;
        decimal OtherProfit2;
        #endregion

        NpgsqlDataReader reader;
        NpgsqlCommand cmd;

        public string scr;

        public MonResult(MainMenu main, int id, string access_auth, string staff_name, int type, string slipNumber, string pass, int grade, bool carryOver, bool monthCatalog)
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
            this.CarryOver = carryOver;
            this.MonthCatalog = monthCatalog;
        }

        private void Return3_Click(object sender, EventArgs e)//戻るボタン
        {
            scr = "MainMenu";
            this.Close();
        }

        private void MonResult_Load(object sender, EventArgs e)
        {
            companyCheckBox.Checked = true;
            individualCheckBox.Checked = true;

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            conn.Open();

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

                cmd = new NpgsqlCommand(sql_str, conn);
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Text = reader["staff_name"].ToString();
                        //comboBox1.SelectedIndex = (int)reader["staff_code"];
                    }
                }

            }
            else
            {
                string sql_str = "select * from staff_m order by staff_code;";
                //string sql_str = "select * from staff_m where invalid = 0 order by staff_code;";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt3);
                comboBox1.DataSource = dt3;
                comboBox1.DisplayMember = "staff_name";
                comboBox1.ValueMember = "staff_code";

                sql_str = "select * from staff_m where staff_code = " + staff_id + ";";

                cmd = new NpgsqlCommand(sql_str, conn);
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Text = reader["staff_name"].ToString();
                        //comboBox1.SelectedIndex = (int)reader["staff_code"];
                    }
                }
            }
            #endregion
            if (slipNumber == null )
            {
                //this.button2.Enabled = false;
                button2.Visible = false;
            }
            conn.Close();

            #region"年のコンボボックス"
            
            DateTime dateTime = DateTime.Now;
            int year = dateTime.Year;

            for (int i = 2010; i <= year; i++)
            {
                yearComboBox.Items.Add(i);
            }

            #endregion
            #region"色"
            MetalPurChaseTextBox.BackColor = SystemColors.Control;
            MetalWholesaleTextBox.BackColor = SystemColors.Control;
            MetalProfitTextBox.BackColor = SystemColors.Control;
            DiamondPurChaseTextBox.BackColor = SystemColors.Control;
            DiamondWholesaleTextBox.BackColor = SystemColors.Control;
            DiamondProfitTextBox.BackColor = SystemColors.Control;
            BrandPurChaseTextBox.BackColor = SystemColors.Control;
            BrandWholesaleTextBox.BackColor = SystemColors.Control;
            BrandProfitTextBox.BackColor = SystemColors.Control;
            ProductPurChaseTextBox.BackColor = SystemColors.Control;
            ProductWholesaleTextBox.BackColor = SystemColors.Control;
            ProductProfitTextBox.BackColor = SystemColors.Control;
            OtherPurChaseTextBox.BackColor = SystemColors.Control;
            OtherWholesaleTextBox.BackColor = SystemColors.Control;
            OtherProfitTextBox.BackColor = SystemColors.Control;
            PurChaseTotalTextBox.BackColor = SystemColors.Control;
            WholesaleTotalTextBox.BackColor = SystemColors.Control;
            ProfitTotalTextBox.BackColor = SystemColors.Control;

            MerePurChaseTextBox.BackColor = SystemColors.Control;
            MereWholesaleTextBox.BackColor = SystemColors.Control;
            MereProfitTextBox.BackColor = SystemColors.Control;
            #endregion

            notLoad = true;
        }

        private void Search1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.SelectedIndex.ToString());
            #region"選択事項"
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("検索する担当者を選択してください", "選択不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (yearComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("検索する年を選択してください", "選択不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (monthComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("検索する月を選択してください", "選択不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            dt.Clear();
            #region"合計値"
            TotalPurchase2 = 0;
            TotalWholesale2 = 0;
            TotalProfit2 = 0;

            MetalPurchase2 = 0;
            MetalWholesale2 = 0;
            MetalProfit2 = 0;
            DiamondPurchase2 = 0;
            DiamondWholesale2 = 0;
            DiamondProfit2 = 0;
            BrandPurchase2 = 0;
            BrandWholesale2 = 0;
            BrandProfit2 = 0;
            ProductPurchase2 = 0;
            ProductWholesale2 = 0;
            ProductProfit2 = 0;
            BrandPurchase2 = 0;
            BrandWholesale2 = 0;
            BrandProfit2 = 0;
            OtherPurchase2 = 0;
            OtherWholesale2 = 0;
            OtherProfit2 = 0;

            decimal MerePurchase = 0;
            decimal MereWholesale = 0;
            decimal MereProfit = 0;
            decimal MerePurchase2 = 0;
            decimal MereWholesale2 = 0;
            decimal MereProfit2 = 0;
            #endregion
            int staff = (int)comboBox1.SelectedValue;
            string date = yearComboBox.Text + "-" + monthComboBox.Text;
            string date1 = yearComboBox.Text + "年" + monthComboBox.Text + "月";
            //DateTime Date = DateTime.Parse(date);

            NpgsqlDataAdapter adapter;
            NpgsqlConnection conn4 = new NpgsqlConnection();
            PostgreSQL postgre = new PostgreSQL();
            conn4 = postgre.connection();

            conn4.Open();

            string sql_str = "";                    //上の表データ取得用
            string sql_str2 = "";                   //メレを除いた合計金額取得用
            string sql_mere = "";                   //メレの合計金額取得用

            //法人のラジオボタンにチェック
            if (companyCheckBox.Checked == true && individualCheckBox.Checked == false)
            {
                type = 0;

                sql_str = "select B.assessment_date, A.sum_money, A.sum_wholesale_price, A.profit, B.delivery_method, B.payment_method, C.staff_name, D.name, E.type_name, A.metal_purchase, A.metal_wholesale, " +
                              "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, A.result " +
                              "from list_result A inner join statement_data B on (A.document_number = B.document_number) inner join staff_m C on (A.staff_code = C.staff_code) inner join client_m D on (D.code = A.code)" +
                              "inner join type E ON (A.type = E.type) where C.staff_code = '" + staff + "' and cast(B.assessment_date as text) like '%" + date + "%' and A.type = '" + type + "' order by A.result;";
            }
            //個人のラジオボタンにチェック
            else if (individualCheckBox.Checked == true && companyCheckBox.Checked == false) 
            {
                type = 1;

                sql_str = "select B.assessment_date, A.sum_money, A.sum_wholesale_price, A.profit, B.delivery_method, B.payment_method, C.staff_name, D.name, E.type_name, A.metal_purchase, A.metal_wholesale, " +
                              "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, A.result " +
                              "from list_result A inner join statement_data B on (A.document_number = B.document_number) inner join staff_m C on (A.staff_code = C.staff_code) inner join client_m D on (D.code = A.code)" +
                              "inner join type E ON (A.type = E.type) where C.staff_code = '" + staff + "' and cast(B.assessment_date as text) like '%" + date + "%' and A.type = '" + type + "' order by A.result;";
            }
            //法人と個人どちらも選択していない時 or 両方チェック
            else
            {
                sql_str = "select B.assessment_date, A.sum_money, A.sum_wholesale_price, A.profit, B.delivery_method, B.payment_method, C.staff_name, D.name, E.type_name, A.metal_purchase, A.metal_wholesale, " +
                              "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, A.result " +
                              "from list_result A inner join statement_data B on (A.document_number = B.document_number) inner join staff_m C on (A.staff_code = C.staff_code) inner join client_m D on (D.code = A.code)" +
                              "inner join type E ON (A.type = E.type) where C.staff_code = '" + staff + "' and cast(B.assessment_date as text) like '%" + date + "%' order by A.result;";
            }

            adapter = new NpgsqlDataAdapter(sql_str, conn4);
            adapter.Fill(dt);

            #region"datagridview のヘッダーとフォーマット"
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "査定日";
            dataGridView1.Columns[1].HeaderText = "合計買取額";
            dataGridView1.Columns[2].HeaderText = "卸値合計";
            dataGridView1.Columns[3].HeaderText = "利益合計";
            dataGridView1.Columns[4].HeaderText = "受け渡し方法";
            dataGridView1.Columns[5].HeaderText = "お支払方法";
            dataGridView1.Columns[6].HeaderText = "担当";
            dataGridView1.Columns[7].HeaderText = "顧客名";
            dataGridView1.Columns[8].HeaderText = "区分";
            dataGridView1.Columns[9].HeaderText = "地金買取額";
            dataGridView1.Columns[10].HeaderText = "地金卸値";
            dataGridView1.Columns[11].HeaderText = "ダイヤ買取額";
            dataGridView1.Columns[12].HeaderText = "ダイヤ卸値";
            dataGridView1.Columns[13].HeaderText = "ブランド買取額";
            dataGridView1.Columns[14].HeaderText = "ブランド卸値";
            dataGridView1.Columns[15].HeaderText = "製品・ジュエリー買取額";
            dataGridView1.Columns[16].HeaderText = "製品・ジュエリー卸値";
            dataGridView1.Columns[17].HeaderText = "その他買取額";
            dataGridView1.Columns[18].HeaderText = "その他卸値";

            dataGridView1.Columns[1].DefaultCellStyle.Format = "c";         //買取合計
            dataGridView1.Columns[2].DefaultCellStyle.Format = "c";         //卸値合計
            dataGridView1.Columns[3].DefaultCellStyle.Format = "c";         //利益合計
            dataGridView1.Columns[9].DefaultCellStyle.Format = "c";         //地金買取額
            dataGridView1.Columns[10].DefaultCellStyle.Format = "c";         //地金卸値
            dataGridView1.Columns[11].DefaultCellStyle.Format = "c";         //ダイヤ買取額
            dataGridView1.Columns[12].DefaultCellStyle.Format = "c";         //ダイヤ卸値
            dataGridView1.Columns[13].DefaultCellStyle.Format = "c";         //ブランド買取額
            dataGridView1.Columns[14].DefaultCellStyle.Format = "c";        //ブランド卸値
            dataGridView1.Columns[15].DefaultCellStyle.Format = "c";        //製品・ジュエリー買取額
            dataGridView1.Columns[16].DefaultCellStyle.Format = "c";        //製品・ジュエリー卸値
            dataGridView1.Columns[17].DefaultCellStyle.Format = "c";        //その他買取額
            dataGridView1.Columns[18].DefaultCellStyle.Format = "c";        //その他卸値

            dataGridView1.Columns[19].Visible = false;
            #endregion

            //合計取得用（法人のみ選択）
            if (companyCheckBox.Checked == true && individualCheckBox.Checked == false)
            {
                type = 0;

                sql_str2 = "select A.sum_money, A.sum_wholesale_price, A.profit, A.metal_purchase, A.metal_wholesale, A.metal_profit, A.diamond_purchase, A.diamond_wholesale, A.diamond_profit," +
                " A.brand_purchase, A.brand_wholesale, A.brand_profit, A.product_purchase ,A.product_wholesale, A.product_profit, A.other_purchase, A.other_wholesale, A.other_profit " +
                "from list_result A inner join statement_data B ON (A.document_number = B.document_number ) inner join staff_m C ON (B.staff_code = C.staff_code )" +
                " where C.staff_code = '" + staff + "' and cast(B.assessment_date as text) like '%" + date + "%' and A.type = '" + type + "' ;";

                //メレの合計取得用（品名マスタ、担当者マスタ、成績一覧情報、成績一覧入力データ結合）
                sql_mere = "select A.profit, A.money, A.wholesale_price from list_result2 A inner join item_m B on A.item_code = B.item_code inner join list_result C on A.grade_number = C.result " +
                    "inner join staff_m D on C.staff_code = D.staff_code" +
                    " where D.staff_code = '" + staff + "' and cast(A.assessment_date as text) like'%" + date1 + "%' and A.item_code = '" + 35 + "' and C.type = '" + type + "' ;";

            }
            //合計取得用（個人のみ選択）
            else if (individualCheckBox.Checked == true && companyCheckBox.Checked == false)
            {
                type = 1;

                sql_str2 = "select A.sum_money, A.sum_wholesale_price, A.profit, A.metal_purchase, A.metal_wholesale, A.metal_profit, A.diamond_purchase, A.diamond_wholesale, A.diamond_profit," +
                " A.brand_purchase, A.brand_wholesale, A.brand_profit, A.product_purchase ,A.product_wholesale, A.product_profit, A.other_purchase, A.other_wholesale, A.other_profit " +
                "from list_result A inner join statement_data B ON (A.document_number = B.document_number ) inner join staff_m C ON (B.staff_code = C.staff_code )" +
                " where C.staff_code = '" + staff + "' and cast(B.assessment_date as text) like '%" + date + "%' and A.type = '" + type + "' order by A.result;";

                //メレの合計取得用（品名マスタ、担当者マスタ、成績一覧情報、成績一覧入力データ結合）
                sql_mere = "select A.profit, A.money, A.wholesale_price from list_result2 A inner join item_m B on A.item_code = B.item_code inner join list_result C on A.grade_number = C.result " +
                    "inner join staff_m D on C.staff_code = D.staff_code" +
                    " where D.staff_code = '" + staff + "' and cast(A.assessment_date as text) like'%" + date1 + "%' and A.item_code = '" + 35 + "' and C.type = '" + type + "' order by A.grade_number, A.record_number;";
            }
            //合計取得用（法人と個人未選択）
            else
            {
                sql_str2 = "select A.sum_money, A.sum_wholesale_price, A.profit, A.metal_purchase, A.metal_wholesale, A.metal_profit, A.diamond_purchase, A.diamond_wholesale, A.diamond_profit," +
                " A.brand_purchase, A.brand_wholesale, A.brand_profit, A.product_purchase ,A.product_wholesale, A.product_profit, A.other_purchase, A.other_wholesale, A.other_profit " +
                "from list_result A inner join statement_data B ON (A.document_number = B.document_number ) inner join staff_m C ON (B.staff_code = C.staff_code )" +
                " where C.staff_code = '" + staff + "' and cast(B.assessment_date as text) like '%" + date + "%' order by A.result;";

                //メレの合計取得用（品名マスタ、担当者マスタ、成績一覧情報、成績一覧入力データ結合）
                sql_mere = "select A.profit, A.money, A.wholesale_price from list_result2 A inner join item_m B on A.item_code = B.item_code inner join list_result C on A.grade_number = C.result " +
                    "inner join staff_m D on C.staff_code = D.staff_code" +
                    " where D.staff_code = '" + staff + "' and cast(A.assessment_date as text) like'%" + date1 + "%' and A.item_code = '" + 35 + "' order by A.grade_number, A.record_number;";
            }

            cmd = new NpgsqlCommand(sql_str2, conn4);
            #region"大分類の合計を取得して表示"
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //合計
                    TotalPurchase = (decimal)reader["sum_money"];
                    TotalPurchase2 += TotalPurchase;
                    TotalWholesale = (decimal)reader["sum_wholesale_price"];
                    TotalWholesale2 += TotalWholesale;
                    TotalProfit = (decimal)reader["profit"];
                    TotalProfit2 += TotalProfit;
                    //地金
                    MetalPurchase = (decimal)reader["metal_purchase"];
                    MetalPurchase2 += MetalPurchase;
                    MetalWholesale = (decimal)reader["metal_wholesale"];
                    MetalWholesale2 += MetalWholesale;
                    MetalProfit = (decimal)reader["metal_profit"];
                    MetalProfit2 += MetalProfit;
                    //ダイヤ
                    DiamondPurchase = (decimal)reader["diamond_purchase"];
                    DiamondPurchase2 += DiamondPurchase;
                    DiamondWholesale = (decimal)reader["diamond_wholesale"];
                    DiamondWholesale2 += DiamondWholesale;
                    DiamondProfit = (decimal)reader["diamond_profit"];
                    DiamondProfit2 += DiamondProfit;
                    //ブランド
                    BrandPurchase = (decimal)reader["brand_purchase"];
                    BrandWholesale = (decimal)reader["brand_wholesale"];
                    BrandProfit = (decimal)reader["brand_profit"];
                    BrandPurchase2 += BrandPurchase;
                    BrandWholesale2 += BrandWholesale;
                    BrandProfit2 += BrandProfit;
                    //製品・ジュエリー
                    ProductPurchase = (decimal)reader["product_purchase"];
                    ProductWholesale = (decimal)reader["product_wholesale"];
                    ProductProfit = (decimal)reader["product_profit"];
                    ProductPurchase2 += ProductPurchase;
                    ProductWholesale2 += ProductWholesale;
                    ProductProfit2 += ProductProfit;
                    //その他
                    OtherPurchase = (decimal)reader["other_purchase"];
                    OtherWholesale = (decimal)reader["other_wholesale"];
                    OtherProfit = (decimal)reader["other_profit"];
                    OtherPurchase2 += OtherPurchase;
                    OtherWholesale2 += OtherWholesale;
                    OtherProfit2 += OtherProfit;
                }
            }
            #region"合計金額をフォーマット処理してテキストに代入"
            //地金
            MetalPurChaseTextBox.Text = MetalPurchase2.ToString("C");
            MetalWholesaleTextBox.Text = MetalWholesale2.ToString("C");
            MetalProfitTextBox.Text = MetalProfit2.ToString("C");
            //ダイヤ
            DiamondPurChaseTextBox.Text = DiamondPurchase2.ToString("C");
            DiamondWholesaleTextBox.Text = DiamondWholesale2.ToString("C");
            DiamondProfitTextBox.Text = DiamondProfit2.ToString("C");
            //ブランド
            BrandPurChaseTextBox.Text = BrandPurchase2.ToString("C");
            BrandWholesaleTextBox.Text = BrandWholesale2.ToString("C");
            BrandProfitTextBox.Text = BrandProfit2.ToString("C");
            //製品・ジュエリー
            ProductPurChaseTextBox.Text = ProductPurchase2.ToString("C");
            ProductWholesaleTextBox.Text = ProductWholesale2.ToString("C");
            ProductProfitTextBox.Text = ProductProfit2.ToString("C");
            //その他
            OtherPurChaseTextBox.Text = OtherPurchase2.ToString("C");
            OtherWholesaleTextBox.Text = OtherWholesale2.ToString("C");
            OtherProfitTextBox.Text = OtherProfit2.ToString("C");
            //合計
            PurChaseTotalTextBox.Text = TotalPurchase2.ToString("C");
            WholesaleTotalTextBox.Text = TotalWholesale2.ToString("C");
            ProfitTotalTextBox.Text = TotalProfit2.ToString("C");
            #endregion
            #endregion

            cmd = new NpgsqlCommand(sql_mere, conn4);
            #region"検索範囲内のメレを取得して表示"
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    MerePurchase = (decimal)reader["money"];
                    MereWholesale = (decimal)reader["wholesale_price"];
                    MereProfit = (decimal)reader["profit"];
                    MerePurchase2 += MerePurchase;
                    MereWholesale2 += MereWholesale;
                    MereProfit2 += MereProfit;
                }
            }

            #region"メレの合計をフォーマット処理して表示"
            MerePurChaseTextBox.Text = MerePurchase2.ToString("c0");
            MereWholesaleTextBox.Text = MereWholesale2.ToString("c0");
            MereProfitTextBox.Text = MereProfit2.ToString("c0");
            #endregion

            #endregion
            conn4.Close();
        }


        #region "担当者選択コンボボックス"
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //NpgsqlConnection conn = new NpgsqlConnection();
            //NpgsqlDataAdapter adapter;

            //if (a > 0)
            //{
            //    if (access_auth == "C")
            //    {
            //        PostgreSQL postgre = new PostgreSQL();
            //        conn = postgre.connection();

            //        conn.Open();
            //        //品名検索用
            //        string sql_str = "select * from staff_m where staff_code = " + staff_id + ";";
            //        adapter = new NpgsqlDataAdapter(sql_str, conn);
            //        adapter.Fill(dt3);
            //        comboBox1.DataSource = dt3;
            //        comboBox1.DisplayMember = "staff_name";
            //        comboBox1.ValueMember = "staff_code";

            //        conn.Close();
            //    }
            //    else
            //    {
            //        PostgreSQL postgre = new PostgreSQL();
            //        conn = postgre.connection();

            //        conn.Open();
            //        //品名検索用
            //        string sql_str = "select * from staff_m ;";
            //        adapter = new NpgsqlDataAdapter(sql_str, conn);
            //        adapter.Fill(dt3);
            //        comboBox1.DataSource = dt3;
            //        comboBox1.DisplayMember = "staff_name";
            //        comboBox1.ValueMember = "staff_code";

            //        conn.Close();
            //    }
            //}
            //a++;
        }
        #endregion
        #region "成績入力画面へ"
        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        //全レコード検索
        private void Button1_Click(object sender, EventArgs e)
        {
            //NpgsqlDataAdapter adapter;
            //NpgsqlConnection conn4 = new NpgsqlConnection();
            //PostgreSQL postgre = new PostgreSQL();
            //conn4 = postgre.connection();

            //dt.Clear();

            //string sql_str2 = "";

            ////権限 C
            //if (access_auth == "C")
            //{
            //    //法人選択時
            //    if (radioButton1.Checked == true)
            //    {
            //        type = 0;

            //        sql_str2 = "select B.assessment_date, B.delivery_method, B.payment_method, C.staff_name, A.staff_name, A.type_name, A.metal_purchase, A.metal_wholesale, " +
            //                      "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, " +
            //                      "A.sum_money, A.sum_wholesale_price from list_result A inner join statement_data B ON " +
            //                      "(A.document_number = B.document_number ) inner join staff_m C ON (B.staff_code = C.staff_code ) where A.type = '" + type + "' and A.staff_code = " + staff_id + ";";

            //        //sql_str2 = "select A.assessment_date, C.delivery_method, C.payment_method, D.staff_name, A.staff_name, E.type_name, A.metal_purchase, A.metal_wholesale, " +
            //        //              "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, " +
            //        //              "A.sum_money, A.sum_wholesale_price from list_result A inner join list_result2 B ON (A.document_number = B.document_number )inner join statement_data C ON " +
            //        //              "(A.document_number = C.document_number ) inner join staff_m D ON (C.staff_code = D.staff_code )  inner join type E ON (A.type = E.type) where D.invalid = 0 " +
            //        //              "and A.staff_code = " + staff_id + ";";
            //    }
            //    //個人選択時
            //    else if (radioButton2.Checked == true)
            //    {
            //        type = 1;

            //        sql_str2 = "select B.assessment_date, B.delivery_method, B.payment_method, C.staff_name, A.staff_name, A.type_name, A.metal_purchase, A.metal_wholesale, " +
            //                      "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, " +
            //                      "A.sum_money, A.sum_wholesale_price from list_result A inner join statement_data B ON " +
            //                      "(A.document_number = B.document_number ) inner join staff_m C ON (B.staff_code = C.staff_code ) where A.type = '" + type + "' and A.staff_code = " + staff_id + ";";
            //    }
            //    //法人と個人ともに未選択
            //    else
            //    {
            //        sql_str2 = "select B.assessment_date, B.delivery_method, B.payment_method, C.staff_name, A.staff_name, A.type_name, A.metal_purchase, A.metal_wholesale, " +
            //                      "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, " +
            //                      "A.sum_money, A.sum_wholesale_price from list_result A inner join statement_data B ON " +
            //                      "(A.document_number = B.document_number ) inner join staff_m C ON (B.staff_code = C.staff_code ) and A.staff_code = " + staff_id + ";";
            //    }
            //    conn4.Open();

            //    adapter = new NpgsqlDataAdapter(sql_str2, conn4);
            //    adapter.Fill(dt);
            //}
            ////権限 A, B
            //else
            //{
            //    //法人選択時
            //    if (radioButton1.Checked == true)
            //    {
            //        type = 0;

            //        sql_str2 = "select B.assessment_date, B.delivery_method, B.payment_method, C.staff_name, A.staff_name, A.type_name, A.metal_purchase, A.metal_wholesale, " +
            //                      "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, " +
            //                      "A.sum_money, A.sum_wholesale_price from list_result A inner join statement_data B ON " +
            //                      "(A.document_number = B.document_number ) inner join staff_m C ON (A.staff_code = C.staff_code ) where A.type = '" + type + "';";
            //    }
            //    //個人選択時
            //    else if (radioButton2.Checked == true)
            //    {
            //        type = 1;

            //        sql_str2 = "select B.assessment_date, B.delivery_method, B.payment_method, C.staff_name, A.staff_name, A.type_name, A.metal_purchase, A.metal_wholesale, " +
            //                      "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, " +
            //                      "A.sum_money, A.sum_wholesale_price from list_result A inner join statement_data B ON " +
            //                      "(A.document_number = B.document_number ) inner join staff_m C ON (A.staff_code = C.staff_code ) where A.type = '" + type + "';";
            //    }
            //    //法人・個人未選択時
            //    else
            //    {
            //        sql_str2 = "select B.assessment_date, B.delivery_method, B.payment_method, C.staff_name, A.staff_name, A.type_name, A.metal_purchase, A.metal_wholesale, " +
            //                      "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, " +
            //                      "A.sum_money, A.sum_wholesale_price from list_result A inner join statement_data B ON " +
            //                      "(A.document_number = B.document_number ) inner join staff_m C ON (A.staff_code = C.staff_code );";
            //    }

            //    //sql_str2 = "select B.assessment_date, C.delivery_method, C.payment_method, D.staff_name, A.staff_name, E.type_name, A.metal_purchase, A.metal_wholesale, " +
            //    //                  "A.diamond_purchase, A.diamond_wholesale, A.brand_purchase, A.brand_wholesale, A.product_purchase ,A.product_wholesale, A.other_purchase, A.other_wholesale, " +
            //    //                  "A.sum_money, A.sum_wholesale_price from list_result A inner join list_result2 B ON (A.document_number = B.document_number )inner join statement_data C ON " +
            //    //                  "(A.document_number = C.document_number ) inner join staff_m D ON (C.staff_code = D.staff_code )  inner join type E ON (A.type = E.type) where D.invalid = 0;";

            //    conn4.Open();

            //    adapter = new NpgsqlDataAdapter(sql_str2, conn4);
            //    adapter.Fill(dt);
            //}
            //conn4.Close();
            
        }

        private void MonResult_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MonthCatalog && screan) 
            {

                if (scr == "MainMenu")
                {
                    MainMenu mainmenu = new MainMenu(top, staff_id, Pass, access_auth);
                    mainmenu.Show();
                }
                else
                {
                    RecordList recordList = new RecordList(statement, staff_id, staff_name, type, slipNumber, Grade, Antique, Id, access_auth, Pass, NameChange, CarryOver, MonthCatalog);
                    recordList.Show();
                }
            }
            else if (screan)
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
