using Npgsql;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Net;
using System.Drawing.Printing;

namespace Flawless_ex
{
    public partial class Statement : Form //計算書/納品書作成メニュー
    {
        int a = 0;//大分類クリックカウント数（計算書）
        int b = 0;//大分類クリックカウント数（納品書）
        int staff_id;
        int itemMainCategoryCode;
        int type = 0;
        string path;
        decimal total;
        int Grade; 
        int AntiqueNumber;
        int ID_Number;
        bool screan = true;
        #region "再登録"
        int Re;
        string sql_str;
        string sql_str2;
        string sql_str3;
        string sql_str4;
        #endregion
        #region "買取販売履歴の引数"
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

        #region"計算書　各大分類コード"
        int mainCategoryCode0;      //大分類コード（1行目）
        int mainCategoryCode1;      //大分類コード（2行目）
        int mainCategoryCode2;      //大分類コード（3行目）
        int mainCategoryCode3;      //大分類コード（4行目）
        int mainCategoryCode4;      //大分類コード（5行目）
        int mainCategoryCode5;      //大分類コード（6行目）
        int mainCategoryCode6;      //大分類コード（7行目）
        int mainCategoryCode7;      //大分類コード（8行目）
        int mainCategoryCode8;      //大分類コード（9行目）
        int mainCategoryCode9;      //大分類コード（10行目）
        int mainCategoryCode10;      //大分類コード（11行目）
        int mainCategoryCode11;      //大分類コード（12行目）
        int mainCategoryCode12;      //大分類コード（13行目）
        #endregion

        #region"納品書　各大分類コード"
        int mainCategoryCode00;      //大分類コード（1行目）
        int mainCategoryCode01;      //大分類コード（2行目）
        int mainCategoryCode02;      //大分類コード（3行目）
        int mainCategoryCode03;      //大分類コード（4行目）
        int mainCategoryCode04;      //大分類コード（5行目）
        int mainCategoryCode05;      //大分類コード（6行目）
        int mainCategoryCode06;      //大分類コード（7行目）
        int mainCategoryCode07;      //大分類コード（8行目）
        int mainCategoryCode08;      //大分類コード（9行目）
        int mainCategoryCode09;      //大分類コード（10行目）
        int mainCategoryCode010;      //大分類コード（11行目）
        int mainCategoryCode011;      //大分類コード（12行目）
        int mainCategoryCode012;      //大分類コード（13行目）
        #endregion

        #region"計算書　各品目コード"
        int itemCode0;      //品目コード（1行目）
        int itemCode1;      //品目コード（2行目）
        int itemCode2;      //品目コード（3行目）
        int itemCode3;      //品目コード（4行目）
        int itemCode4;      //品目コード（5行目）
        int itemCode5;      //品目コード（6行目）
        int itemCode6;      //品目コード（7行目）
        int itemCode7;      //品目コード（8行目）
        int itemCode8;      //品目コード（9行目）
        int itemCode9;      //品目コード（10行目）
        int itemCode10;      //品目コード（11行目）
        int itemCode11;      //品目コード（12行目）
        int itemCode12;      //品目コード（13行目）
        #endregion

        #region"納品書　各品目コード"
        int itemCode00;      //品目コード（1行目）
        int itemCode01;      //品目コード（2行目）
        int itemCode02;      //品目コード（3行目）
        int itemCode03;      //品目コード（4行目）
        int itemCode04;      //品目コード（5行目）
        int itemCode05;      //品目コード（6行目）
        int itemCode06;      //品目コード（7行目）
        int itemCode07;      //品目コード（8行目）
        int itemCode08;      //品目コード（9行目）
        int itemCode09;      //品目コード（10行目）
        int itemCode010;      //品目コード（11行目）
        int itemCode011;      //品目コード（12行目）
        int itemCode012;      //品目コード（13行目）
        #endregion

        #region"変数"
        decimal Tax;        //計算用の税（表示用を別に用意）        
        int countsum;        //総数計算用
        decimal weisum;     //総重量計算用
        decimal subSum;     //税抜き時の合計金額用
        decimal sub;          //重量×単価 or 数量×単価、計算書では税込み、納品書では税抜き
        decimal sub1;           //重量×単価 or 数量×単価、納品書用の税込み
        decimal sum;          //税込み時の合計金額用
        decimal TaxAmount;  //税額
        public DataTable clientDt = new DataTable();//顧客情報
        public int count = 0;
        public int count1 = 0;
        string docuNum;     //計算書の伝票番号
        string Num;         //計算書の伝票番号の数字部分（F を切り取った直後）
        int conNum;           //納品書の管理番号
        bool second = false;     //ロード時無視用
        #endregion
        #region "計算書"
        #region "数量"
        int countsum0;
        int countsum1;
        int countsum2;
        int countsum3;
        int countsum4;
        int countsum5;
        int countsum6;
        int countsum7;
        int countsum8;
        int countsum9;
        int countsum10;
        int countsum11;
        int countsum12;
        #endregion
        #region "重量"
        decimal weisum0;
        decimal weisum1;
        decimal weisum2;
        decimal weisum3;
        decimal weisum4;
        decimal weisum5;
        decimal weisum6;
        decimal weisum7;
        decimal weisum8;
        decimal weisum9;
        decimal weisum10;
        decimal weisum11;
        decimal weisum12;
        #endregion
        #region "金額"
        decimal subSum0;
        decimal subSum1;
        decimal subSum2;
        decimal subSum3;
        decimal subSum4;
        decimal subSum5;
        decimal subSum6;
        decimal subSum7;
        decimal subSum8;
        decimal subSum9;
        decimal subSum10;
        decimal subSum11;
        decimal subSum12;
        #endregion
        #region "データ保持"
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
        #endregion
        #region "納品書"
        #region "数量"
        int countsum00;
        int countsum01;
        int countsum02;
        int countsum03;
        int countsum04;
        int countsum05;
        int countsum06;
        int countsum07;
        int countsum08;
        int countsum09;
        int countsum010;
        int countsum011;
        int countsum012;
        #endregion
        #region "重量"
        decimal weisum00;
        decimal weisum01;
        decimal weisum02;
        decimal weisum03;
        decimal weisum04;
        decimal weisum05;
        decimal weisum06;
        decimal weisum07;
        decimal weisum08;
        decimal weisum09;
        decimal weisum010;
        decimal weisum011;
        decimal weisum012;
        #endregion
        #region "金額"
        decimal subSum00;
        decimal subSum01;
        decimal subSum02;
        decimal subSum03;
        decimal subSum04;
        decimal subSum05;
        decimal subSum06;
        decimal subSum07;
        decimal subSum08;
        decimal subSum09;
        decimal subSum010;
        decimal subSum011;
        decimal subSum012;
        #endregion
        #region "データ保持"
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
        #region "税抜金額"
        decimal sub00;
        decimal sub01;
        decimal sub02;
        decimal sub03;
        decimal sub04;
        decimal sub05;
        decimal sub06;
        decimal sub07;
        decimal sub08;
        decimal sub09;
        decimal sub010;
        decimal sub011;
        decimal sub012;
        #endregion
        #region "税込み金額"
        decimal sub10;
        decimal sub11;
        decimal sub12;
        decimal sub13;
        decimal sub14;
        decimal sub15;
        decimal sub16;
        decimal sub17;
        decimal sub18;
        decimal sub19;
        decimal sub110;
        decimal sub111;
        decimal sub112;
        #endregion
        #endregion

        #region"画像のPATH"
        string AolFinancialShareholder = "";
        string TaxCertification = "";
        string SealCertification = "";
        string ResidenceCard = "";
        string ResidencePeriod = "";
        string AntiqueLicence = "";
        #endregion

        string client_staff_name;
        string address;
        string register_date;
        string remarks;
        string access_auth;
        string document;
        int control;
        string data;
        string item;
        string phone;
        string pass;
        int regist;
        int regist1;
        bool NameChange = false;                    //品名を変更したら true
        bool CarryOver;                      //次月持ち越しから画面遷移したとき
        bool MonthCatalog;
        #region"計算書・納品書での各金額（計算書と納品書で扱いが少し違う）"
        decimal money0;
        decimal money1;
        decimal money2;
        decimal money3;
        decimal money4;
        decimal money5;
        decimal money6;
        decimal money7;
        decimal money8;
        decimal money9;
        decimal money10;
        decimal money11;
        decimal money12;
        #endregion

        int number;     //伝票番号の数字五桁

        MainMenu mainMenu;

        #region"DataTable"
        DataTable dt = new DataTable();//大分類

        DataTable dt2 = new DataTable();//品名と大分類関連付け

        DataTable dt4 = new DataTable();//2行目
        DataTable dt5 = new DataTable();//3行目
        DataTable dt6 = new DataTable();//4行目
        DataTable dt7 = new DataTable();//5行目
        DataTable dt8 = new DataTable();//6行目
        DataTable dt9 = new DataTable();//7行目
        DataTable dt10 = new DataTable();//8行目
        DataTable dt11 = new DataTable();//9行目
        DataTable dt12 = new DataTable();//10行目
        DataTable dt13 = new DataTable();//11行目
        DataTable dt14 = new DataTable();//12行目
        DataTable dt15 = new DataTable();//13行目

        DataTable dt16 = new DataTable();
        DataTable dt17 = new DataTable();
        DataTable dt18 = new DataTable();
        DataTable dt19 = new DataTable();
        //計算書用
        DataTable dt200 = new DataTable();
        DataTable dt201 = new DataTable();
        DataTable dt202 = new DataTable();
        DataTable dt203 = new DataTable();
        DataTable dt204 = new DataTable();
        DataTable dt205 = new DataTable();
        DataTable dt206 = new DataTable();
        DataTable dt207 = new DataTable();
        DataTable dt208 = new DataTable();
        DataTable dt209 = new DataTable();
        DataTable dt210 = new DataTable();
        DataTable dt211 = new DataTable();
        //納品書用
        DataTable deliverydt200 = new DataTable();
        DataTable deliverydt201 = new DataTable();
        DataTable deliverydt202 = new DataTable();
        DataTable deliverydt203 = new DataTable();
        DataTable deliverydt204 = new DataTable();
        DataTable deliverydt205 = new DataTable();
        DataTable deliverydt206 = new DataTable();
        DataTable deliverydt207 = new DataTable();
        DataTable deliverydt208 = new DataTable();
        DataTable deliverydt209 = new DataTable();
        DataTable deliverydt210 = new DataTable();
        DataTable deliverydt211 = new DataTable();
        DataTable deliverydt212 = new DataTable();

        DataTable dt3 = new DataTable();// 品名情報全て
        #region "計算書履歴"
        DataTable redt4 = new DataTable();
        DataTable redt5 = new DataTable();
        DataTable redt6 = new DataTable();
        DataTable redt7 = new DataTable();
        DataTable redt8 = new DataTable();
        DataTable redt9 = new DataTable();
        DataTable redt10 = new DataTable();
        DataTable redt11 = new DataTable();
        DataTable redt12 = new DataTable();
        DataTable redt13 = new DataTable();
        DataTable redt14 = new DataTable();
        DataTable redt15 = new DataTable();
        #endregion
        #endregion

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        NpgsqlDataReader reader;
        NpgsqlTransaction transaction;

        public Statement(MainMenu main, int id, int type, string client_staff_name, string address, string access_auth, decimal Total, string Pass, string document, int control, string data, string search1, string search2, string search3,string search4, string search5, string search6, string search7, string search8, string search9, string search10, string search11, string search12, decimal amount00, decimal amount01, decimal amount02, decimal amount03, decimal amount04, decimal amount05, decimal amount06, decimal amount07, decimal amount08, decimal amount09, decimal amount010, decimal amount011, decimal amount012, decimal amount10, decimal amount11, decimal amount12, decimal amount13, decimal amount14, decimal amount15, decimal amount16, decimal amount17, decimal amount18, decimal amount19, decimal amount110, decimal amount111, decimal amount112, string name1, string phoneNumber1, string addresskana1, string code1, string item1, string date1, string date2, string method1, string amountA, string amountB, string antiqueNumber, string documentNumber, string address1)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            this.type = type;
            this.client_staff_name = client_staff_name;
            this.address = address;
            this.access_auth = access_auth;
            total = Total;
            this.document = document;
            this.control = control;
            this.data = data;
            #region "買取販売履歴"
            this.search1 = search1;
            this.search2 = search2;
            this.search3 = search3;
            this.search4 = search4;
            this.search5 = search5;
            this.search6 = search6;
            this.search7 = search7;
            this.search8 = search8;
            this.search9 = search9;
            this.search10 = search10;
            this.search11 = search11;
            this.search12 = search12;
            this.name1 = name1;
            this.phoneNumber1 = phoneNumber1;
            this.addresskana1 = addresskana1;
            this.code1 = code1;
            this.item1 = item1;
            this.date1 = date1;
            this.date2 = date2;
            this.method1 = method1;
            this.antiqueNumber = antiqueNumber;
            this.amountA = amountA;
            this.amountB = amountB;
            this.documentNumber = documentNumber;
            this.address1 = address1;
            #endregion
            this.pass = Pass;
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
            #region "計算書の引数"
            this.amount10 = amount10;
            this.amount11 = amount11;
            this.amount12 = amount12;
            this.amount13 = amount13;
            this.amount14 = amount14;
            this.amount15 = amount15;
            this.amount16 = amount16;
            this.amount17 = amount17;
            this.amount18 = amount18;
            this.amount19 = amount19;
            this.amount110 = amount110;
            this.amount111 = amount111;
            this.amount112 = amount112;
            #endregion
        }

        private void Statement_Load(object sender, EventArgs e)
        {
            #region "ボタン"
            if (data == "S")
            {
                #region "計算書のタブのみ表示"
                tabControl1.SelectedTab = SettlementDayBox;
                tabControl1.TabPages.Remove(tabPage2);
                #endregion
                
            }
            else if (data == "D")
            {
                #region "納品書のタブのみ表示"
                tabControl1.SelectedTab = tabPage2;
                tabControl1.TabPages.Remove(SettlementDayBox);
                #endregion
            }
            else if (Grade != 0)
            {
                this.label10.Visible = false;
                this.textBox2.Visible = false;
                this.previewButton.Enabled = true;
                this.RecordListButton.Enabled = true;
                this.label9.Visible = true;
                this.textBox1.Visible = true;
                this.DeliveryPreviewButton.Enabled = false;
                this.button1.Enabled = false;
                this.button2.Enabled = false;
            }
            else
            {
                this.label10.Visible = false;
                this.textBox2.Visible = false;
                this.label9.Visible = false;
                this.textBox1.Visible = false;
                this.previewButton.Enabled = false;
                this.RecordListButton.Enabled = false;
                this.DeliveryPreviewButton.Enabled = false;
                this.button1.Enabled = false;
                this.button2.Enabled = false;
            }
            #endregion
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select * from staff_m where staff_code = " + staff_id + ";";　//担当者名取得用
            string sql;                                                 //伝票番号・管理番号取得
            sql = "select document_number from statement_data;";     //伝票番号の取得用
            cmd = new NpgsqlCommand(sql_str, conn);

            conn.Open();

            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    label2.Text = string.Format("{0}", reader["staff_name"]);
                    itemMainCategoryCode = (int)reader["main_category_code"];
                }
            }

            cmd = new NpgsqlCommand(sql, conn);

            #region"計算書の伝票番号"
            #region "買取販売から遷移してきた場合"
            if (data == "S")
            {
                documentNumberTextBox.Text = document;
            }
            #endregion
            #region "成績入力"
            else if (Grade != 0)
            {
                documentNumberTextBox.Text = document;
            }
            #endregion
            else
            {
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        docuNum = reader["document_number"].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(docuNum))
                {
                    Num = docuNum.Trim('F');        //伝票番号の数字部分
                }
                else
                {
                    docuNum = "F0";
                    Num = docuNum.Trim('F');       //伝票番号の数字部分
                }

                number = int.Parse(Num) + 1;
                documentNumberTextBox.Text = "F" + number;       //Fを追加
            }

            #endregion

            #region"納品書の管理番号"
            #region "買取販売から遷移してきた場合"
            if (data == "D")
            {
                documentNumberTextBox2.Text = control.ToString();
            }
            #endregion
            else
            {
                sql = "select control_number from delivery_m;";         //管理番号取得
                cmd = new NpgsqlCommand(sql, conn);

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        conNum = (int)reader["control_number"];
                    }
                }

                if (string.IsNullOrEmpty(conNum.ToString()))
                {
                    conNum = 0;
                }

                number = conNum + 1;
                documentNumberTextBox2.Text = number.ToString();
            }
            
            #endregion
            
           //担当者ごとの大分類の初期値を先頭に
           string sql_str2 = "select * from main_category_m where invalid = 0 order by main_category_code asc;";
           adapter = new NpgsqlDataAdapter(sql_str2, conn);
           adapter.Fill(dt);
            

            //品名検索用
            string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + itemMainCategoryCode + ";";
            adapter = new NpgsqlDataAdapter(sql_str3, conn);
            adapter.Fill(dt2);

            string sql_str4 = "select main_category_name from main_category_m where main_category_code = " + itemMainCategoryCode + ";";
            adapter = new NpgsqlDataAdapter(sql_str4, conn);
            adapter.Fill(dt3);
            
            DataRow row;
            row = dt3.Rows[0];
            string MainName = row["main_category_name"].ToString();

            //税率取得
            string sql_str5 = "select vat_rate from vat_m;";
            cmd = new NpgsqlCommand(sql_str5, conn);
            string vat_rate = "";

            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    vat_rate = reader["vat_rate"].ToString();
                }
            }

            tax.Text = vat_rate;
            Tax = decimal.Parse(tax.Text.ToString());
            tax.Text = string.Format("{0:P}", Tax / 100);

            conn.Close();

            if (data != "S")
            {
                #region "納品書"
                #region "納品書　大分類"
                #region "納品書1行目"
                DataTable deliverydt = new DataTable();
                deliverydt = dt.Copy();
                mainCategoryComboBox00.DataSource = deliverydt;
                mainCategoryComboBox00.DisplayMember = "main_category_name";
                mainCategoryComboBox00.ValueMember = "main_category_code";
                mainCategoryComboBox00.SelectedIndex = 0;//担当者ごとの初期値設定
                #endregion
                #region "納品書2行目"
                DataTable deliverydt100 = new DataTable();
                deliverydt100 = dt.Copy();
                mainCategoryComboBox01.DataSource = deliverydt100;
                mainCategoryComboBox01.DisplayMember = "main_category_name";
                mainCategoryComboBox01.ValueMember = "main_category_code";
                mainCategoryComboBox01.SelectedIndex = 0;
                #endregion
                #region "納品書3行目"
                DataTable deliverydt101 = new DataTable();
                deliverydt101 = dt.Copy();
                mainCategoryComboBox02.DataSource = deliverydt101;
                mainCategoryComboBox02.DisplayMember = "main_category_name";
                mainCategoryComboBox02.ValueMember = "main_category_code";
                mainCategoryComboBox02.SelectedIndex = 0;
                #endregion
                #region "納品書4行目"
                DataTable deliverydt102 = new DataTable();
                deliverydt102 = dt.Copy();
                mainCategoryComboBox03.DataSource = deliverydt102;
                mainCategoryComboBox03.DisplayMember = "main_category_name";
                mainCategoryComboBox03.ValueMember = "main_category_code";
                mainCategoryComboBox03.SelectedIndex = 0;
                #endregion
                #region "納品書5行目"
                DataTable deliverydt103 = new DataTable();
                deliverydt103 = dt.Copy();
                mainCategoryComboBox04.DataSource = deliverydt103;
                mainCategoryComboBox04.DisplayMember = "main_category_name";
                mainCategoryComboBox04.ValueMember = "main_category_code";
                mainCategoryComboBox04.SelectedIndex = 0;
                #endregion
                #region "納品書6行目"
                DataTable deliverydt104 = new DataTable();
                deliverydt104 = dt.Copy();
                mainCategoryComboBox05.DataSource = deliverydt104;
                mainCategoryComboBox05.DisplayMember = "main_category_name";
                mainCategoryComboBox05.ValueMember = "main_category_code";
                mainCategoryComboBox05.SelectedIndex = 0;
                #endregion
                #region "納品書7行目"
                DataTable deliverydt105 = new DataTable();
                deliverydt105 = dt.Copy();
                mainCategoryComboBox06.DataSource = deliverydt105;
                mainCategoryComboBox06.DisplayMember = "main_category_name";
                mainCategoryComboBox06.ValueMember = "main_category_code";
                mainCategoryComboBox06.SelectedIndex = 0;
                #endregion
                #region "納品書8行目"
                DataTable deliverydt106 = new DataTable();
                deliverydt106 = dt.Copy();
                mainCategoryComboBox07.DataSource = deliverydt106;
                mainCategoryComboBox07.DisplayMember = "main_category_name";
                mainCategoryComboBox07.ValueMember = "main_category_code";
                mainCategoryComboBox07.SelectedIndex = 0;
                #endregion
                #region "納品書9行目"
                DataTable deliverydt107 = new DataTable();
                deliverydt107 = dt.Copy();
                mainCategoryComboBox08.DataSource = deliverydt107;
                mainCategoryComboBox08.DisplayMember = "main_category_name";
                mainCategoryComboBox08.ValueMember = "main_category_code";
                mainCategoryComboBox08.SelectedIndex = 0;
                #endregion
                #region "納品書10行目"
                DataTable deliverydt108 = new DataTable();
                deliverydt108 = dt.Copy();
                mainCategoryComboBox09.DataSource = deliverydt108;
                mainCategoryComboBox09.DisplayMember = "main_category_name";
                mainCategoryComboBox09.ValueMember = "main_category_code";
                mainCategoryComboBox09.SelectedIndex = 0;
                #endregion
                #region "納品書11行目"
                DataTable deliverydt109 = new DataTable();
                deliverydt109 = dt.Copy();
                mainCategoryComboBox010.DataSource = deliverydt109;
                mainCategoryComboBox010.DisplayMember = "main_category_name";
                mainCategoryComboBox010.ValueMember = "main_category_code";
                mainCategoryComboBox010.SelectedIndex = 0;
                #endregion
                #region "納品書12行目"
                DataTable deliverydt110 = new DataTable();
                deliverydt110 = dt.Copy();
                mainCategoryComboBox011.DataSource = deliverydt110;
                mainCategoryComboBox011.DisplayMember = "main_category_name";
                mainCategoryComboBox011.ValueMember = "main_category_code";
                mainCategoryComboBox011.SelectedIndex = 0;
                #endregion
                #region "納品書13行目"
                DataTable deliverydt112 = new DataTable();
                deliverydt112 = dt.Copy();
                mainCategoryComboBox012.DataSource = deliverydt112;
                mainCategoryComboBox012.DisplayMember = "main_category_name";
                mainCategoryComboBox012.ValueMember = "main_category_code";
                mainCategoryComboBox012.SelectedIndex = 0;
                #endregion
                #endregion
                #region "納品書　品名"
                #region "納品書1行目"
                deliverydt200 = dt2.Copy();
                itemComboBox00.DataSource = deliverydt200;
                itemComboBox00.DisplayMember = "item_name";
                itemComboBox00.ValueMember = "item_code";
                #endregion
                #region "納品書2行目"
                deliverydt201 = dt2.Copy();
                itemComboBox01.DataSource = deliverydt201;
                itemComboBox01.DisplayMember = "item_name";
                itemComboBox01.ValueMember = "item_code";
                #endregion
                #region "納品書3行目"
                deliverydt202 = dt2.Copy();
                itemComboBox02.DataSource = deliverydt202;
                itemComboBox02.DisplayMember = "item_name";
                itemComboBox02.ValueMember = "item_code";
                #endregion
                #region "納品書4行目"
                deliverydt203 = dt2.Copy();
                itemComboBox03.DataSource = deliverydt203;
                itemComboBox03.DisplayMember = "item_name";
                itemComboBox03.ValueMember = "item_code";
                #endregion
                #region "納品書5行目"
                deliverydt204 = dt2.Copy();
                itemComboBox04.DataSource = deliverydt204;
                itemComboBox04.DisplayMember = "item_name";
                itemComboBox04.ValueMember = "item_code";
                #endregion
                #region "納品書6行目"
                deliverydt205 = dt2.Copy();
                itemComboBox05.DataSource = deliverydt205;
                itemComboBox05.DisplayMember = "item_name";
                itemComboBox05.ValueMember = "item_code";
                #endregion
                #region "納品書7行目"
                deliverydt206 = dt2.Copy();
                itemComboBox06.DataSource = deliverydt206;
                itemComboBox06.DisplayMember = "item_name";
                itemComboBox06.ValueMember = "item_code";
                #endregion
                #region "納品書8行目"
                deliverydt207 = dt2.Copy();
                itemComboBox07.DataSource = deliverydt207;
                itemComboBox07.DisplayMember = "item_name";
                itemComboBox07.ValueMember = "item_code";
                #endregion
                #region "納品書9行目"
                deliverydt208 = dt2.Copy();
                itemComboBox08.DataSource = deliverydt208;
                itemComboBox08.DisplayMember = "item_name";
                itemComboBox08.ValueMember = "item_code";
                #endregion
                #region "納品書10行目"
                deliverydt209 = dt2.Copy();
                itemComboBox09.DataSource = deliverydt209;
                itemComboBox09.DisplayMember = "item_name";
                itemComboBox09.ValueMember = "item_code";
                #endregion
                #region "納品書11行目"
                deliverydt210 = dt2.Copy();
                itemComboBox010.DataSource = deliverydt210;
                itemComboBox010.DisplayMember = "item_name";
                itemComboBox010.ValueMember = "item_code";
                #endregion
                #region "納品書12行目"
                deliverydt211 = dt2.Copy();
                itemComboBox011.DataSource = deliverydt211;
                itemComboBox011.DisplayMember = "item_name";
                itemComboBox011.ValueMember = "item_code";
                #endregion
                #region "納品書13行目"
                //納品書
                deliverydt212 = dt2.Copy();
                itemComboBox012.DataSource = deliverydt212;
                itemComboBox012.DisplayMember = "item_name";
                itemComboBox012.ValueMember = "item_code";
                #endregion
                #endregion
                #endregion
            }
            if (data != "D")
            {
                #region "計算書"
                #region "計算書　大分類"
                #region "計算書1行目"
                mainCategoryComboBox0.DataSource = dt;
                mainCategoryComboBox0.DisplayMember = "main_category_name";
                mainCategoryComboBox0.ValueMember = "main_category_code";
                mainCategoryComboBox0.SelectedIndex = 0;//担当者ごとの初期値設定
                #endregion
                #region "計算書2行目"
                DataTable dt100 = new DataTable();
                dt100 = dt.Copy();
                mainCategoryComboBox1.DataSource = dt100;
                mainCategoryComboBox1.DisplayMember = "main_category_name";
                mainCategoryComboBox1.ValueMember = "main_category_code";
                mainCategoryComboBox1.SelectedIndex = 0;
                #endregion
                #region "計算書3行目"
                DataTable dt101 = new DataTable();
                dt101 = dt.Copy();
                mainCategoryComboBox2.DataSource = dt101;
                mainCategoryComboBox2.DisplayMember = "main_category_name";
                mainCategoryComboBox2.ValueMember = "main_category_code";
                mainCategoryComboBox2.SelectedIndex = 0;
                #endregion
                #region "計算書4行目"
                DataTable dt102 = new DataTable();
                dt102 = dt.Copy();
                mainCategoryComboBox3.DataSource = dt102;
                mainCategoryComboBox3.DisplayMember = "main_category_name";
                mainCategoryComboBox3.ValueMember = "main_category_code";
                mainCategoryComboBox3.SelectedIndex = 0;
                #endregion
                #region "計算書5行目"
                DataTable dt103 = new DataTable();
                dt103 = dt.Copy();
                mainCategoryComboBox4.DataSource = dt103;
                mainCategoryComboBox4.DisplayMember = "main_category_name";
                mainCategoryComboBox4.ValueMember = "main_category_code";
                mainCategoryComboBox4.SelectedIndex = 0;
                #endregion
                #region "計算書6行目"
                DataTable dt104 = new DataTable();
                dt104 = dt.Copy();
                mainCategoryComboBox5.DataSource = dt104;
                mainCategoryComboBox5.DisplayMember = "main_category_name";
                mainCategoryComboBox5.ValueMember = "main_category_code";
                mainCategoryComboBox5.SelectedIndex = 0;
                #endregion
                #region "計算書7行目"
                DataTable dt105 = new DataTable();
                dt105 = dt.Copy();
                mainCategoryComboBox6.DataSource = dt105;
                mainCategoryComboBox6.DisplayMember = "main_category_name";
                mainCategoryComboBox6.ValueMember = "main_category_code";
                mainCategoryComboBox6.SelectedIndex = 0;
                #endregion
                #region "計算書8行目"
                DataTable dt106 = new DataTable();
                dt106 = dt.Copy();
                mainCategoryComboBox7.DataSource = dt106;
                mainCategoryComboBox7.DisplayMember = "main_category_name";
                mainCategoryComboBox7.ValueMember = "main_category_code";
                mainCategoryComboBox7.SelectedIndex = 0;
                #endregion
                #region "計算書9行目"
                DataTable dt107 = new DataTable();
                dt107 = dt.Copy();
                mainCategoryComboBox8.DataSource = dt107;
                mainCategoryComboBox8.DisplayMember = "main_category_name";
                mainCategoryComboBox8.ValueMember = "main_category_code";
                mainCategoryComboBox8.SelectedIndex = 0;
                #endregion
                #region "計算書10行目"
                DataTable dt108 = new DataTable();
                dt108 = dt.Copy();
                mainCategoryComboBox9.DataSource = dt108;
                mainCategoryComboBox9.DisplayMember = "main_category_name";
                mainCategoryComboBox9.ValueMember = "main_category_code";
                mainCategoryComboBox9.SelectedIndex = 0;
                #endregion
                #region "計算書11行目"
                DataTable dt109 = new DataTable();
                dt109 = dt.Copy();
                mainCategoryComboBox10.DataSource = dt109;
                mainCategoryComboBox10.DisplayMember = "main_category_name";
                mainCategoryComboBox10.ValueMember = "main_category_code";
                mainCategoryComboBox10.SelectedIndex = 0;
                #endregion
                #region "計算書12行目"
                DataTable dt110 = new DataTable();
                dt110 = dt.Copy();
                mainCategoryComboBox11.DataSource = dt110;
                mainCategoryComboBox11.DisplayMember = "main_category_name";
                mainCategoryComboBox11.ValueMember = "main_category_code";
                mainCategoryComboBox11.SelectedIndex = 0;
                #endregion
                #region "計算書13行目"
                DataTable dt111 = new DataTable();
                dt111 = dt.Copy();
                mainCategoryComboBox12.DataSource = dt111;
                mainCategoryComboBox12.DisplayMember = "main_category_name";
                mainCategoryComboBox12.ValueMember = "main_category_code";
                mainCategoryComboBox12.SelectedIndex = 0;
                #endregion
                #endregion
                #region "計算書　品名"
                #region "計算書1行目"
                itemComboBox0.DataSource = dt2;
                itemComboBox0.DisplayMember = "item_name";
                itemComboBox0.ValueMember = "item_code";
                #endregion
                #region "計算書2行目"
                dt200 = dt2.Copy();
                itemComboBox1.DataSource = dt200;
                itemComboBox1.DisplayMember = "item_name";
                itemComboBox1.ValueMember = "item_code";
                #endregion
                #region "計算書3行目"
                dt201 = dt2.Copy();
                itemComboBox2.DataSource = dt201;
                itemComboBox2.DisplayMember = "item_name";
                itemComboBox2.ValueMember = "item_code";
                #endregion
                #region "計算書4行目"
                dt202 = dt2.Copy();
                itemComboBox3.DataSource = dt202;
                itemComboBox3.DisplayMember = "item_name";
                itemComboBox3.ValueMember = "item_code";
                #endregion
                #region "計算書5行目"
                dt203 = dt2.Copy();
                itemComboBox4.DataSource = dt203;
                itemComboBox4.DisplayMember = "item_name";
                itemComboBox4.ValueMember = "item_code";
                #endregion
                #region "計算書6行目"
                dt204 = dt2.Copy();
                itemComboBox5.DataSource = dt204;
                itemComboBox5.DisplayMember = "item_name";
                itemComboBox5.ValueMember = "item_code";
                #endregion
                #region "計算書7行目"
                dt205 = dt2.Copy();
                itemComboBox6.DataSource = dt205;
                itemComboBox6.DisplayMember = "item_name";
                itemComboBox6.ValueMember = "item_code";
                #endregion
                #region "計算書8行目"
                dt206 = dt2.Copy();
                itemComboBox7.DataSource = dt206;
                itemComboBox7.DisplayMember = "item_name";
                itemComboBox7.ValueMember = "item_code";
                #endregion
                #region "計算書9行目"
                dt207 = dt2.Copy();
                itemComboBox8.DataSource = dt207;
                itemComboBox8.DisplayMember = "item_name";
                itemComboBox8.ValueMember = "item_code";
                #endregion
                #region "計算書10行目"
                dt208 = dt2.Copy();
                itemComboBox9.DataSource = dt208;
                itemComboBox9.DisplayMember = "item_name";
                itemComboBox9.ValueMember = "item_code";
                #endregion
                #region "計算書11行目"
                dt209 = dt2.Copy();
                itemComboBox10.DataSource = dt209;
                itemComboBox10.DisplayMember = "item_name";
                itemComboBox10.ValueMember = "item_code";
                #endregion
                #region "計算書12行目"
                dt210 = dt2.Copy();
                itemComboBox11.DataSource = dt210;
                itemComboBox11.DisplayMember = "item_name";
                itemComboBox11.ValueMember = "item_code";
                #endregion
                #region "計算書13行目"
                dt211 = dt2.Copy();
                itemComboBox12.DataSource = dt211;
                itemComboBox12.DisplayMember = "item_name";
                itemComboBox12.ValueMember = "item_code";
                #endregion
                #endregion
                #endregion
            }

            if (data == "S")
            {
                #region "計算書の表の入力を呼び出し"
                DataTable dt19 = new DataTable();
                string str_document = "select * from statement_calc_data where document_number = '" + document + "';";
                adapter = new NpgsqlDataAdapter(str_document, conn);
                adapter.Fill(dt19);
                int st = dt19.Rows.Count;
                #endregion
                #region "計算書の表の外部の入力データを呼び出し"
                DataTable dt21 = new DataTable();
                string str_document1 = "select * from statement_data where document_number = '" + document + "';";
                adapter = new NpgsqlDataAdapter(str_document1, conn);
                adapter.Fill(dt21);
                DataRow row1;
                row1 = dt21.Rows[0];
                int types = (int)row1["type"];  //法人か個人か
                #region "法人"
                if (types == 0)
                {
                    typeTextBox.Text = "法人";
                    #region "入力する値"
                    #region "顧客"
                    this.companyTextBox.Text = row1["company_name"].ToString();
                    this.shopNameTextBox.Text = row1["shop_name"].ToString();
                    this.clientNameTextBox.Text = row1["staff_name"].ToString();
                    DataTable dt25 = new DataTable();
                    string str_client = "select * from client_m_corporate where type = 0 and company_name = '" + this.companyTextBox.Text + "' and shop_name = '" + this.shopNameTextBox.Text + "' and staff_name = '" + this.clientNameTextBox.Text + "';";
                    adapter = new NpgsqlDataAdapter(str_client, conn);
                    adapter.Fill(dt25);
                    DataRow row2;
                    row2 = dt25.Rows[0];
                    this.antiqueLicenceTextBox.Text = row2["antique_license"].ToString();
                    this.registerDateTextBox.Text = row2["registration_date"].ToString();
                    this.clientRemarksTextBox.Text = row2["remarks"].ToString();
                    #endregion
                    #region "枠外"
                    this.subTotal.Text = row1["sub_total"].ToString();
                    subTotal.Text = string.Format("{0:C}", decimal.Parse(subTotal.Text, System.Globalization.NumberStyles.Number));
                    int sum = int.Parse(row1["total"].ToString());
                    this.sumTextBox.Text = row1["total"].ToString();
                    sumTextBox.Text = string.Format("{0:C}", decimal.Parse(sumTextBox.Text, System.Globalization.NumberStyles.Number));
                    this.taxAmount.Text = row1["tax_amount"].ToString();
                    taxAmount.Text = string.Format("{0:C}", decimal.Parse(taxAmount.Text, System.Globalization.NumberStyles.Number));
                    this.paymentMethodsComboBox.SelectedItem = row1["payment_method"].ToString();
                    this.deliveryComboBox.SelectedItem = row1["delivery_method"].ToString();
                    this.totalCount.Text = row1["total_amount"].ToString();
                    int cou = int.Parse(row1["total_amount"].ToString());
                    totalCount.Text = string.Format("{0:#,0}", cou);
                    decimal wei = decimal.Parse(row1["total_weight"].ToString());
                    this.totalWeight.Text = row1["total_weight"].ToString();
                    totalWeight.Text = string.Format("{0:#,0}", Math.Round(wei, 1, MidpointRounding.AwayFromZero));
                    if (sum >= 2000000)
                    {
                        groupBox1.Show();
                        groupBox1.BackColor = Color.OrangeRed;
                    }
                    #endregion
                    #endregion
                }
                #endregion
                #region "個人"
                if (types == 1)
                {
                    #region "入力する値"
                    #region "顧客"
                    label16.Text = "氏名";
                    label17.Text = "生年月日";
                    label18.Text = "職業";
                    label38.Visible = false;
                    registerDateTextBox.Visible = false;
                    typeTextBox.Text = "個人";
                    this.companyTextBox.Text = row1["name"].ToString();
                    this.shopNameTextBox.Text = row1["birthday"].ToString();
                    this.clientNameTextBox.Text = row1["occupation"].ToString();
                    DataTable dt25 = new DataTable();
                    string str_client = "select * from client_m_individual where type = 1 and name = '" + this.companyTextBox.Text + "' and birthday = '" + this.shopNameTextBox.Text + "' and occupation = '" + this.clientNameTextBox.Text + "';";
                    adapter = new NpgsqlDataAdapter(str_client, conn);
                    adapter.Fill(dt25);
                    DataRow row2;
                    row2 = dt25.Rows[0];
                    this.antiqueLicenceTextBox.Text = row2["antique_license"].ToString();
                    this.clientRemarksTextBox.Text = row2["remarks"].ToString();
                    #endregion
                    #region "枠外"
                    this.subTotal.Text = row1["sub_total"].ToString();
                    subTotal.Text = string.Format("{0:C}", decimal.Parse(subTotal.Text, System.Globalization.NumberStyles.Number));
                    this.sumTextBox.Text = row1["total"].ToString();
                    int sum = int.Parse(row1["total"].ToString());
                    sumTextBox.Text = string.Format("{0:C}", decimal.Parse(sumTextBox.Text, System.Globalization.NumberStyles.Number));
                    this.taxAmount.Text = row1["tax_amount"].ToString();
                    taxAmount.Text = string.Format("{0:C}", decimal.Parse(taxAmount.Text, System.Globalization.NumberStyles.Number));
                    this.paymentMethodsComboBox.SelectedItem = row1["payment_method"].ToString();
                    this.deliveryComboBox.SelectedItem = row1["delivery_method"].ToString();
                    this.totalCount.Text = row1["total_amount"].ToString();
                    int cou = int.Parse(row1["total_amount"].ToString());
                    totalCount.Text = string.Format("{0:#,0}", cou);
                    decimal wei = decimal.Parse(row1["total_weight"].ToString());
                    this.totalWeight.Text = row1["total_weight"].ToString();
                    totalWeight.Text = string.Format("{0:#,0}", Math.Round(wei, 1, MidpointRounding.AwayFromZero));
                    if (sum >= 2000000)
                    {
                        groupBox1.Show();
                        groupBox1.BackColor = Color.OrangeRed;
                    }
                    #endregion
                    #endregion
                }
                #endregion
                #endregion
                for (int St = 0; St <= (st - 1); St++)
                {
                    #region "1行目"
                    if (St == 0)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode0 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode0 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt23 = new DataTable();
                        string sql_document = "select * from main_category_m where invalid = 0;";
                        adapter = new NpgsqlDataAdapter(sql_document, conn);
                        adapter.Fill(dt23);
                        mainCategoryComboBox0.DataSource = dt23;
                        mainCategoryComboBox0.DisplayMember = "main_category_name";
                        mainCategoryComboBox0.ValueMember = "main_category_code";
                        mainCategoryComboBox0.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox0.SelectedValue = itemMainCategoryCode0;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt24 = new DataTable();
                        string sql_item1 = "select * from item_m  where invalid = 0;";
                        adapter = new NpgsqlDataAdapter(sql_item1, conn);
                        adapter.Fill(dt24);
                        itemComboBox0.DataSource = dt24;
                        itemComboBox0.DisplayMember = "item_name";
                        itemComboBox0.ValueMember = "item_code";
                        itemComboBox0.SelectedValue = itemCode0;
                        #endregion
                        #endregion
                        #region "入力された項目 1行目"
                        this.weightTextBox0.Text = dataRow1["weight"].ToString();
                        this.countTextBox0.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox0.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox0.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox0.Text = dataRow1["amount"].ToString();
                        moneyTextBox0.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox0.Text, System.Globalization.NumberStyles.Number));
                        this.remarks0.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "2行目"
                    if (St == 1)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode1 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode1 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt231 = new DataTable();
                        string sql_document1 = "select * from main_category_m where invalid = 0;";
                        adapter = new NpgsqlDataAdapter(sql_document1, conn);
                        adapter.Fill(dt231);
                        mainCategoryComboBox1.DataSource = dt231;
                        mainCategoryComboBox1.DisplayMember = "main_category_name";
                        mainCategoryComboBox1.ValueMember = "main_category_code";
                        mainCategoryComboBox1.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox1.SelectedValue = itemMainCategoryCode1;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt241 = new DataTable();
                        string sql_item2 = "select * from item_m  where invalid = 0;";
                        adapter = new NpgsqlDataAdapter(sql_item2, conn);
                        adapter.Fill(dt241);
                        itemComboBox1.DataSource = dt241;
                        itemComboBox1.DisplayMember = "item_name";
                        itemComboBox1.ValueMember = "item_code";
                        itemComboBox1.SelectedValue = itemCode1;
                        #endregion
                        #endregion
                        #region "入力された項目 2行目"
                        this.weightTextBox1.Text = dataRow1["weight"].ToString();
                        this.countTextBox1.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox1.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox1.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox1.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox1.Text = dataRow1["amount"].ToString();
                        moneyTextBox1.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox1.Text, System.Globalization.NumberStyles.Number));
                        this.remarks1.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "3行目"
                    if (St == 2)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode2 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode2 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt232 = new DataTable();
                        string sql_document2 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_document2, conn);
                        adapter.Fill(dt232);
                        mainCategoryComboBox2.DataSource = dt232;
                        mainCategoryComboBox2.DisplayMember = "main_category_name";
                        mainCategoryComboBox2.ValueMember = "main_category_code";
                        mainCategoryComboBox2.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox2.SelectedValue = itemMainCategoryCode2;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt242 = new DataTable();
                        string sql_item3 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item3, conn);
                        adapter.Fill(dt242);
                        itemComboBox2.DataSource = dt242;
                        itemComboBox2.DisplayMember = "item_name";
                        itemComboBox2.ValueMember = "item_code";
                        itemComboBox2.SelectedValue = itemCode2;
                        #endregion
                        #endregion
                        #region "入力された項目 3行目"
                        this.weightTextBox2.Text = dataRow1["weight"].ToString();
                        this.countTextBox2.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox2.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox2.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox2.Text = dataRow1["amount"].ToString();
                        moneyTextBox2.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox2.Text, System.Globalization.NumberStyles.Number));
                        this.remarks2.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "4行目"
                    if (St == 3)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode3 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode3 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt233 = new DataTable();
                        string sql_document3 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_document3, conn);
                        adapter.Fill(dt233);
                        mainCategoryComboBox3.DataSource = dt233;
                        mainCategoryComboBox3.DisplayMember = "main_category_name";
                        mainCategoryComboBox3.ValueMember = "main_category_code";
                        mainCategoryComboBox3.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox3.SelectedValue = itemMainCategoryCode3;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt243 = new DataTable();
                        string sql_item4 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item4, conn);
                        adapter.Fill(dt243);
                        itemComboBox3.DataSource = dt243;
                        itemComboBox3.DisplayMember = "item_name";
                        itemComboBox3.ValueMember = "item_code";
                        itemComboBox3.SelectedValue = itemCode3;
                        #endregion
                        #endregion
                        #region "入力された項目 4行目"
                        this.weightTextBox3.Text = dataRow1["weight"].ToString();
                        this.countTextBox3.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox3.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox3.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox3.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox3.Text = dataRow1["amount"].ToString();
                        moneyTextBox3.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox3.Text, System.Globalization.NumberStyles.Number));
                        this.remarks3.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "5行目"
                    if (St == 4)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document4 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document4, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode4 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode4 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt234 = new DataTable();
                        string sql_document4 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_document4, conn);
                        adapter.Fill(dt234);
                        mainCategoryComboBox4.DataSource = dt234;
                        mainCategoryComboBox4.DisplayMember = "main_category_name";
                        mainCategoryComboBox4.ValueMember = "main_category_code";
                        mainCategoryComboBox4.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox4.SelectedValue = itemMainCategoryCode4;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt244 = new DataTable();
                        string sql_item5 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item5, conn);
                        adapter.Fill(dt244);
                        itemComboBox4.DataSource = dt244;
                        itemComboBox4.DisplayMember = "item_name";
                        itemComboBox4.ValueMember = "item_code";
                        itemComboBox4.SelectedValue = itemCode4;
                        #endregion
                        #endregion
                        #region "入力された項目 5行目"
                        this.weightTextBox4.Text = dataRow1["weight"].ToString();
                        this.countTextBox4.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox4.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox4.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox4.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox4.Text = dataRow1["amount"].ToString();
                        moneyTextBox4.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox4.Text, System.Globalization.NumberStyles.Number));
                        this.remarks4.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "6行目"
                    if (St == 5)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode5 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode5 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt235 = new DataTable();
                        string sql_document5 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_document5, conn);
                        adapter.Fill(dt235);
                        mainCategoryComboBox5.DataSource = dt235;
                        mainCategoryComboBox5.DisplayMember = "main_category_name";
                        mainCategoryComboBox5.ValueMember = "main_category_code";
                        mainCategoryComboBox5.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox5.SelectedValue = itemMainCategoryCode5;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt245 = new DataTable();
                        string sql_item6 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item6, conn);
                        adapter.Fill(dt245);
                        itemComboBox5.DataSource = dt245;
                        itemComboBox5.DisplayMember = "item_name";
                        itemComboBox5.ValueMember = "item_code";
                        itemComboBox5.SelectedValue = itemCode5;
                        #endregion
                        #endregion
                        #region "入力された項目 6行目"
                        this.weightTextBox5.Text = dataRow1["weight"].ToString();
                        this.countTextBox5.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox5.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox5.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox5.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox5.Text = dataRow1["amount"].ToString();
                        moneyTextBox5.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox5.Text, System.Globalization.NumberStyles.Number));
                        this.remarks5.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "7行目"
                    if (St == 6)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode6 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode6 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt236 = new DataTable();
                        string sql_document6 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_document6, conn);
                        adapter.Fill(dt236);
                        mainCategoryComboBox6.DataSource = dt236;
                        mainCategoryComboBox6.DisplayMember = "main_category_name";
                        mainCategoryComboBox6.ValueMember = "main_category_code";
                        mainCategoryComboBox6.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox6.SelectedValue = itemMainCategoryCode6;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt246 = new DataTable();
                        string sql_item7 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item7, conn);
                        adapter.Fill(dt246);
                        itemComboBox6.DataSource = dt246;
                        itemComboBox6.DisplayMember = "item_name";
                        itemComboBox6.ValueMember = "item_code";
                        itemComboBox6.SelectedValue = itemCode6;
                        #endregion
                        #endregion
                        #region "入力された項目 7行目"
                        this.weightTextBox6.Text = dataRow1["weight"].ToString();
                        this.countTextBox6.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox6.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox6.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox6.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox6.Text = dataRow1["amount"].ToString();
                        moneyTextBox6.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox6.Text, System.Globalization.NumberStyles.Number));
                        this.remarks6.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "8行目"
                    if (St == 7)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode7 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode7 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt237 = new DataTable();
                        string sql_document7 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_document7, conn);
                        adapter.Fill(dt237);
                        mainCategoryComboBox7.DataSource = dt237;
                        mainCategoryComboBox7.DisplayMember = "main_category_name";
                        mainCategoryComboBox7.ValueMember = "main_category_code";
                        mainCategoryComboBox7.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox7.SelectedValue = itemMainCategoryCode7;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt247 = new DataTable();
                        string sql_item8 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item8, conn);
                        adapter.Fill(dt247);
                        itemComboBox7.DataSource = dt247;
                        itemComboBox7.DisplayMember = "item_name";
                        itemComboBox7.ValueMember = "item_code";
                        itemComboBox7.SelectedValue = itemCode7;
                        #endregion
                        #endregion
                        #region "入力された項目 8行目"
                        this.weightTextBox7.Text = dataRow1["weight"].ToString();
                        this.countTextBox7.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox7.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox7.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox7.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox7.Text = dataRow1["amount"].ToString();
                        moneyTextBox7.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox7.Text, System.Globalization.NumberStyles.Number));
                        this.remarks7.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "9行目"
                    if (St == 8)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode8 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode8 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt238 = new DataTable();
                        string sql_document8 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_document8, conn);
                        adapter.Fill(dt238);
                        mainCategoryComboBox8.DataSource = dt238;
                        mainCategoryComboBox8.DisplayMember = "main_category_name";
                        mainCategoryComboBox8.ValueMember = "main_category_code";
                        mainCategoryComboBox8.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox8.SelectedValue = itemMainCategoryCode8;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt248 = new DataTable();
                        string sql_item9 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item9, conn);
                        adapter.Fill(dt248);
                        itemComboBox8.DataSource = dt248;
                        itemComboBox8.DisplayMember = "item_name";
                        itemComboBox8.ValueMember = "item_code";
                        itemComboBox8.SelectedValue = itemCode8;
                        #endregion
                        #endregion
                        #region "入力された項目 9行目"
                        this.weightTextBox8.Text = dataRow1["weight"].ToString();
                        this.countTextBox8.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox8.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox8.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox8.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox8.Text = dataRow1["amount"].ToString();
                        moneyTextBox8.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox8.Text, System.Globalization.NumberStyles.Number));
                        this.remarks8.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "10行目"
                    if (St == 9)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode9 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode9 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt239 = new DataTable();
                        string sql_document9 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_document9, conn);
                        adapter.Fill(dt239);
                        mainCategoryComboBox9.DataSource = dt239;
                        mainCategoryComboBox9.DisplayMember = "main_category_name";
                        mainCategoryComboBox9.ValueMember = "main_category_code";
                        mainCategoryComboBox9.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox9.SelectedValue = itemMainCategoryCode9;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt249 = new DataTable();
                        string sql_item10 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item10, conn);
                        adapter.Fill(dt249);
                        itemComboBox9.DataSource = dt249;
                        itemComboBox9.DisplayMember = "item_name";
                        itemComboBox9.ValueMember = "item_code";
                        itemComboBox9.SelectedValue = itemCode9;
                        #endregion
                        #endregion
                        #region "入力された項目 10行目"
                        this.weightTextBox9.Text = dataRow1["weight"].ToString();
                        this.countTextBox9.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox9.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox9.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox9.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox9.Text = dataRow1["amount"].ToString();
                        moneyTextBox9.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox9.Text, System.Globalization.NumberStyles.Number));
                        this.remarks9.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "11行目"
                    if (St == 10)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode10 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode10 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt2310 = new DataTable();
                        string sql_document10 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_document10, conn);
                        adapter.Fill(dt2310);
                        mainCategoryComboBox10.DataSource = dt2310;
                        mainCategoryComboBox10.DisplayMember = "main_category_name";
                        mainCategoryComboBox10.ValueMember = "main_category_code";
                        mainCategoryComboBox10.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox10.SelectedValue = itemMainCategoryCode10;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2410 = new DataTable();
                        string sql_item11 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item11, conn);
                        adapter.Fill(dt2410);
                        itemComboBox10.DataSource = dt2410;
                        itemComboBox10.DisplayMember = "item_name";
                        itemComboBox10.ValueMember = "item_code";
                        itemComboBox10.SelectedValue = itemCode10;
                        #endregion
                        #endregion
                        #region "入力された項目 11行目"
                        this.weightTextBox10.Text = dataRow1["weight"].ToString();
                        this.countTextBox10.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox10.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox10.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox10.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox10.Text = dataRow1["amount"].ToString();
                        moneyTextBox10.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox10.Text, System.Globalization.NumberStyles.Number));
                        this.remarks10.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "12行目"
                    if (St == 11)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode11 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode11 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt2311 = new DataTable();
                        string sql_document11 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_document11, conn);
                        adapter.Fill(dt2311);
                        mainCategoryComboBox11.DataSource = dt2311;
                        mainCategoryComboBox11.DisplayMember = "main_category_name";
                        mainCategoryComboBox11.ValueMember = "main_category_code";
                        mainCategoryComboBox11.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox11.SelectedValue = itemMainCategoryCode11;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2411 = new DataTable();
                        string sql_item12 = "select * from item_m  where invalid = 0;";
                        adapter = new NpgsqlDataAdapter(sql_item12, conn);
                        adapter.Fill(dt2411);
                        itemComboBox11.DataSource = dt2411;
                        itemComboBox11.DisplayMember = "item_name";
                        itemComboBox11.ValueMember = "item_code";
                        itemComboBox11.SelectedValue = itemCode11;
                        #endregion
                        #endregion
                        #region "入力された項目 12行目"
                        this.weightTextBox11.Text = dataRow1["weight"].ToString();
                        this.countTextBox11.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox11.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox11.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox11.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox1.Text = dataRow1["amount"].ToString();
                        moneyTextBox11.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox11.Text, System.Globalization.NumberStyles.Number));
                        this.remarks11.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "13行目"
                    if (St == 12)
                    {
                        DataTable dt22 = new DataTable();
                        string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_document2, conn);
                        adapter.Fill(dt22);
                        DataRow dataRow1;
                        dataRow1 = dt22.Rows[0];
                        int itemMainCategoryCode12 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode12 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt2312 = new DataTable();
                        string sql_document12 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_document12, conn);
                        adapter.Fill(dt2312);
                        mainCategoryComboBox12.DataSource = dt2312;
                        mainCategoryComboBox12.DisplayMember = "main_category_name";
                        mainCategoryComboBox12.ValueMember = "main_category_code";
                        mainCategoryComboBox12.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox12.SelectedValue = itemMainCategoryCode12;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2412 = new DataTable();
                        string sql_item13 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item13, conn);
                        adapter.Fill(dt2412);
                        itemComboBox12.DataSource = dt2412;
                        itemComboBox12.DisplayMember = "item_name";
                        itemComboBox12.ValueMember = "item_code";
                        itemComboBox12.SelectedValue = itemCode12;
                        #endregion
                        #endregion
                        #region "入力された項目 13行目"
                        this.weightTextBox12.Text = dataRow1["weight"].ToString();
                        this.countTextBox12.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox12.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox12.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox12.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox12.Text = dataRow1["amount"].ToString();
                        moneyTextBox12.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox12.Text, System.Globalization.NumberStyles.Number));
                        this.remarks12.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                }
            }
            if (data == "D")
            {
                #region "納品書の表の入力を呼び出し"
                DataTable dt20 = new DataTable();
                string str_control = "select * from delivery_calc where control_number = " + control + ";";
                adapter = new NpgsqlDataAdapter(str_control, conn);
                adapter.Fill(dt20);
                int de = dt20.Rows.Count;
                #endregion
                #region "納品書の表の外のデータを呼び出し"
                DataTable dt21 = new DataTable();
                string sql_control = "select * from delivery_m where control_number = " + control + ";";
                adapter = new NpgsqlDataAdapter(sql_control, conn);
                adapter.Fill(dt21);
                DataRow row1;
                row1 = dt21.Rows[0];
                int type1 = (int)row1["types1"];
                string yes = row1["seaal_print"].ToString();
                if (type1 == 0)
                {
                    #region "枠外"
                    this.name.Text = row1["name"].ToString();
                    this.titleComboBox.SelectedItem = row1["honorific_title"].ToString();
                    this.RemarkRegister.Text = row1["remarks2"].ToString();
                    this.typeComboBox.SelectedItem = row1["type"].ToString();
                    this.paymentMethodComboBox.SelectedItem = row1["payment_method"].ToString();
                    this.PayeeTextBox1.Text = row1["account_payble"].ToString();
                    this.CoinComboBox.SelectedItem = row1["currency"].ToString();
                    this.comboBox11.SelectedItem = row1["vat"].ToString();
                    if (yes == "する")
                    {
                        sealY.Checked = true;
                    }
                    if (yes == "しない")
                    {
                        sealN.Checked = true;
                    }
                    this.totalCount2.Text = row1["total_count"].ToString();
                    totalCount2.Text = string.Format("{0:#,0}", this.totalCount2.Text);
                    this.totalWeight2.Text = row1["total_weight"].ToString();
                    totalWeight2.Text = string.Format("{0:#,0}", Math.Round(decimal.Parse(this.totalWeight2.Text), 1, MidpointRounding.AwayFromZero));
                    this.sumTextBox2.Text = row1["total"].ToString();
                    sumTextBox2.Text = string.Format("{0:C}", decimal.Parse(sumTextBox2.Text, System.Globalization.NumberStyles.Number));
                    this.subTotal2.Text = row1["sub_total"].ToString();
                    subTotal2.Text = string.Format("{0:C}", decimal.Parse(subTotal2.Text, System.Globalization.NumberStyles.Number));
                    this.taxAmount2.Text = row1["vat_amount"].ToString();
                    taxAmount2.Text = string.Format("{0:C}", decimal.Parse(taxAmount2.Text, System.Globalization.NumberStyles.Number));
                    this.tax.Text = row1["vat_rate"].ToString() + ".00%";
                    #endregion
                    #region "顧客"
                    int antique = (int)row1["antique_number"];
                    typeTextBox2.Text = "法人";
                    DataTable dt25 = new DataTable();
                    string str_client = "select * from client_m_corporate where type = 0 and antique_number = " + antique + " ;";
                    adapter = new NpgsqlDataAdapter(str_client, conn);
                    adapter.Fill(dt25);
                    DataRow row2;
                    row2 = dt25.Rows[0];
                    this.companyTextBox2.Text = row2["company_name"].ToString();
                    this.shopNameTextBox2.Text = row2["shop_name"].ToString();
                    this.clientNameTextBox2.Text = row2["staff_name"].ToString();
                    this.antiqueLicenceTextBox2.Text = row2["antique_license"].ToString();
                    this.registerDateTextBox2.Text = row2["registration_date"].ToString();
                    this.clientRemarksTextBox2.Text = row2["remarks"].ToString();
                    #endregion
                }
                if (type1 == 1)
                {
                    typeTextBox2.Text = "個人";
                    #region "枠外"
                    this.name.Text = row1["name"].ToString();
                    this.titleComboBox.SelectedItem = row1["honorific_title"].ToString();
                    this.RemarkRegister.Text = row1["remarks2"].ToString();
                    this.typeComboBox.SelectedItem = row1["type"].ToString();
                    this.paymentMethodComboBox.SelectedItem = row1["payment_method"].ToString();
                    this.PayeeTextBox1.Text = row1["account_payble"].ToString();
                    this.CoinComboBox.SelectedItem = row1["currency"].ToString();
                    this.comboBox11.SelectedItem = row1["vat"].ToString();
                    if (yes == "する")
                    {
                        sealY.Checked = true;
                    }
                    if (yes == "しない")
                    {
                        sealN.Checked = true;
                    }
                    this.totalCount2.Text = row1["total_count"].ToString();
                    totalCount2.Text = string.Format("{0:#,0}", this.totalCount2.Text);
                    this.totalWeight2.Text = row1["total_weight"].ToString();
                    totalWeight2.Text = string.Format("{0:#,0}", Math.Round(decimal.Parse(this.totalWeight2.Text), 1, MidpointRounding.AwayFromZero));
                    this.sumTextBox2.Text = row1["total"].ToString();
                    sumTextBox2.Text = string.Format("{0:C}", decimal.Parse(sumTextBox2.Text, System.Globalization.NumberStyles.Number));
                    this.subTotal2.Text = row1["sub_total"].ToString();
                    subTotal2.Text = string.Format("{0:C}", decimal.Parse(subTotal2.Text, System.Globalization.NumberStyles.Number));
                    this.taxAmount2.Text = row1["vat_amount"].ToString();
                    taxAmount2.Text = string.Format("{0:C}", decimal.Parse(taxAmount2.Text, System.Globalization.NumberStyles.Number));
                    this.tax.Text = row1["vat_rate"].ToString() + ".00%";
                    #endregion
                    #region "顧客"
                    typeTextBox2.Text = "個人";
                    label75.Text = "氏名";
                    label76.Text = "職業";
                    label77.Text = "生年月日";
                    label36.Visible = false;
                    registerDateTextBox2.Visible = false;
                    int idNumber = (int)row1["id_number"];
                    typeTextBox2.Text = "法人";
                    DataTable dt25 = new DataTable();
                    string str_client = "select * from client_m_individual where type = 1 and id_number = " + idNumber + " ;";
                    adapter = new NpgsqlDataAdapter(str_client, conn);
                    adapter.Fill(dt25);
                    DataRow row2;
                    row2 = dt25.Rows[0];
                    this.companyTextBox2.Text = row2["name"].ToString();
                    this.shopNameTextBox2.Text = row2["birthday"].ToString();
                    this.clientNameTextBox2.Text = row2["occupation"].ToString();
                    this.antiqueLicenceTextBox2.Text = row2["antique_license"].ToString();
                    this.clientRemarksTextBox2.Text = row2["remarks"].ToString();
                    #endregion
                }
                #endregion

                for (int De = 0; De <= (de - 1); De++)
                {
                    #region "1行目"
                    if (De ==0)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode00 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode00 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt26 = new DataTable();
                        string sql_control1 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control1, conn);
                        adapter.Fill(dt26);
                        mainCategoryComboBox00.DataSource = dt26;
                        mainCategoryComboBox00.DisplayMember = "main_category_name";
                        mainCategoryComboBox00.ValueMember = "main_category_code";
                        mainCategoryComboBox00.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox00.SelectedValue = itemMainCategoryCode00;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt27 = new DataTable();
                        string sql_item1 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item1, conn);
                        adapter.Fill(dt27);
                        itemComboBox00.DataSource = dt27;
                        itemComboBox00.DisplayMember = "item_name";
                        itemComboBox00.ValueMember = "item_code";
                        itemComboBox00.SelectedValue = itemCode00;
                        #endregion
                        #endregion
                        #region "入力された項目 1行目"
                        this.weightTextBox00.Text = dataRow1["weight"].ToString();
                        this.countTextBox00.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox00.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox00.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox00.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox00.Text = dataRow1["amount"].ToString();
                        moneyTextBox00.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox00.Text, System.Globalization.NumberStyles.Number));
                        this.remarks00.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "2行目"
                    if (De == 1)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode01 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode01 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt261 = new DataTable();
                        string sql_control2 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control2, conn);
                        adapter.Fill(dt261);
                        mainCategoryComboBox01.DataSource = dt261;
                        mainCategoryComboBox01.DisplayMember = "main_category_name";
                        mainCategoryComboBox01.ValueMember = "main_category_code";
                        mainCategoryComboBox01.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox01.SelectedValue = itemMainCategoryCode01;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt271 = new DataTable();
                        string sql_item2 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item2, conn);
                        adapter.Fill(dt271);
                        itemComboBox01.DataSource = dt271;
                        itemComboBox01.DisplayMember = "item_name";
                        itemComboBox01.ValueMember = "item_code";
                        itemComboBox01.SelectedValue = itemCode01;
                        #endregion
                        #endregion
                        #region "入力された項目 2行目"
                        this.weightTextBox01.Text = dataRow1["weight"].ToString();
                        this.countTextBox01.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox01.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox01.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox01.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox01.Text = dataRow1["amount"].ToString();
                        moneyTextBox01.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox01.Text, System.Globalization.NumberStyles.Number));
                        this.remarks01.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "3行目"
                    if (De == 2)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode02 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode02 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt262 = new DataTable();
                        string sql_control3 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control3, conn);
                        adapter.Fill(dt262);
                        mainCategoryComboBox02.DataSource = dt262;
                        mainCategoryComboBox02.DisplayMember = "main_category_name";
                        mainCategoryComboBox02.ValueMember = "main_category_code";
                        mainCategoryComboBox02.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox02.SelectedValue = itemMainCategoryCode02;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt272 = new DataTable();
                        string sql_item3 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item3, conn);
                        adapter.Fill(dt272);
                        itemComboBox02.DataSource = dt272;
                        itemComboBox02.DisplayMember = "item_name";
                        itemComboBox02.ValueMember = "item_code";
                        itemComboBox02.SelectedValue = itemCode02;
                        #endregion
                        #endregion
                        #region "入力された項目 3行目"
                        this.weightTextBox02.Text = dataRow1["weight"].ToString();
                        this.countTextBox02.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox02.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox02.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox02.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox02.Text = dataRow1["amount"].ToString();
                        moneyTextBox02.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox02.Text, System.Globalization.NumberStyles.Number));
                        this.remarks02.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "4行目"
                    if (De == 3)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode03 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode03 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt263 = new DataTable();
                        string sql_control4 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control4, conn);
                        adapter.Fill(dt263);
                        mainCategoryComboBox03.DataSource = dt263;
                        mainCategoryComboBox03.DisplayMember = "main_category_name";
                        mainCategoryComboBox03.ValueMember = "main_category_code";
                        mainCategoryComboBox03.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox03.SelectedValue = itemMainCategoryCode03;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt273 = new DataTable();
                        string sql_item4 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item4, conn);
                        adapter.Fill(dt273);
                        itemComboBox03.DataSource = dt273;
                        itemComboBox03.DisplayMember = "item_name";
                        itemComboBox03.ValueMember = "item_code";
                        itemComboBox03.SelectedValue = itemCode03;
                        #endregion
                        #endregion
                        #region "入力された項目 4行目"
                        this.weightTextBox03.Text = dataRow1["weight"].ToString();
                        this.countTextBox03.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox03.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox03.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox03.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox03.Text = dataRow1["amount"].ToString();
                        moneyTextBox03.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox03.Text, System.Globalization.NumberStyles.Number));
                        this.remarks03.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "5行目"
                    if (De == 4)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode04 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode04 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt264 = new DataTable();
                        string sql_control5 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control5, conn);
                        adapter.Fill(dt264);
                        mainCategoryComboBox04.DataSource = dt264;
                        mainCategoryComboBox04.DisplayMember = "main_category_name";
                        mainCategoryComboBox04.ValueMember = "main_category_code";
                        mainCategoryComboBox04.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox04.SelectedValue = itemMainCategoryCode04;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt274 = new DataTable();
                        string sql_item4 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item4, conn);
                        adapter.Fill(dt274);
                        itemComboBox04.DataSource = dt274;
                        itemComboBox04.DisplayMember = "item_name";
                        itemComboBox04.ValueMember = "item_code";
                        itemComboBox04.SelectedValue = itemCode04;
                        #endregion
                        #endregion
                        #region "入力された項目 5行目"
                        this.weightTextBox04.Text = dataRow1["weight"].ToString();
                        this.countTextBox04.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox04.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox04.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox04.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox04.Text = dataRow1["amount"].ToString();
                        moneyTextBox04.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox04.Text, System.Globalization.NumberStyles.Number));
                        this.remarks04.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "6行目"
                    if (De == 5)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode05 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode05 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt265 = new DataTable();
                        string sql_control6 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control6, conn);
                        adapter.Fill(dt265);
                        mainCategoryComboBox05.DataSource = dt265;
                        mainCategoryComboBox05.DisplayMember = "main_category_name";
                        mainCategoryComboBox05.ValueMember = "main_category_code";
                        mainCategoryComboBox05.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox05.SelectedValue = itemMainCategoryCode05;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt275 = new DataTable();
                        string sql_item6 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item6, conn);
                        adapter.Fill(dt275);
                        itemComboBox05.DataSource = dt275;
                        itemComboBox05.DisplayMember = "item_name";
                        itemComboBox05.ValueMember = "item_code";
                        itemComboBox05.SelectedValue = itemCode05;
                        #endregion
                        #endregion
                        #region "入力された項目 6行目"
                        this.weightTextBox05.Text = dataRow1["weight"].ToString();
                        this.countTextBox05.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox05.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox05.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox05.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox05.Text = dataRow1["amount"].ToString();
                        moneyTextBox05.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox05.Text, System.Globalization.NumberStyles.Number));
                        this.remarks05.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "7行目"
                    if (De == 6)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode06 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode06 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt266 = new DataTable();
                        string sql_control7 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control7, conn);
                        adapter.Fill(dt266);
                        mainCategoryComboBox06.DataSource = dt266;
                        mainCategoryComboBox06.DisplayMember = "main_category_name";
                        mainCategoryComboBox06.ValueMember = "main_category_code";
                        mainCategoryComboBox06.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox06.SelectedValue = itemMainCategoryCode06;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt276 = new DataTable();
                        string sql_item7 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item7, conn);
                        adapter.Fill(dt276);
                        itemComboBox06.DataSource = dt276;
                        itemComboBox06.DisplayMember = "item_name";
                        itemComboBox06.ValueMember = "item_code";
                        itemComboBox06.SelectedValue = itemCode06;
                        #endregion
                        #endregion
                        #region "入力された項目 7行目"
                        this.weightTextBox06.Text = dataRow1["weight"].ToString();
                        this.countTextBox06.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox06.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox06.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox06.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox06.Text = dataRow1["amount"].ToString();
                        moneyTextBox06.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox06.Text, System.Globalization.NumberStyles.Number));
                        this.remarks06.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "8行目"
                    if (De == 7)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode07 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode07 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt267 = new DataTable();
                        string sql_control8 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control8, conn);
                        adapter.Fill(dt267);
                        mainCategoryComboBox07.DataSource = dt267;
                        mainCategoryComboBox07.DisplayMember = "main_category_name";
                        mainCategoryComboBox07.ValueMember = "main_category_code";
                        mainCategoryComboBox07.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox07.SelectedValue = itemMainCategoryCode07;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt277 = new DataTable();
                        string sql_item8 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item8, conn);
                        adapter.Fill(dt277);
                        itemComboBox07.DataSource = dt277;
                        itemComboBox07.DisplayMember = "item_name";
                        itemComboBox07.ValueMember = "item_code";
                        itemComboBox07.SelectedValue = itemCode07;
                        #endregion
                        #endregion
                        #region "入力された項目 8行目"
                        this.weightTextBox07.Text = dataRow1["weight"].ToString();
                        this.countTextBox07.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox07.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox07.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox07.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox07.Text = dataRow1["amount"].ToString();
                        moneyTextBox07.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox07.Text, System.Globalization.NumberStyles.Number));
                        this.remarks07.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "9行目"
                    if (De == 8)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode08 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode08 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt268 = new DataTable();
                        string sql_control9 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control9, conn);
                        adapter.Fill(dt268);
                        mainCategoryComboBox08.DataSource = dt268;
                        mainCategoryComboBox08.DisplayMember = "main_category_name";
                        mainCategoryComboBox08.ValueMember = "main_category_code";
                        mainCategoryComboBox08.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox08.SelectedValue = itemMainCategoryCode08;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt278 = new DataTable();
                        string sql_item9 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item9, conn);
                        adapter.Fill(dt278);
                        itemComboBox08.DataSource = dt278;
                        itemComboBox08.DisplayMember = "item_name";
                        itemComboBox08.ValueMember = "item_code";
                        itemComboBox08.SelectedValue = itemCode08;
                        #endregion
                        #endregion
                        #region "入力された項目 9行目"
                        this.weightTextBox08.Text = dataRow1["weight"].ToString();
                        this.countTextBox08.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox08.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox08.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox08.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox08.Text = dataRow1["amount"].ToString();
                        moneyTextBox08.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox08.Text, System.Globalization.NumberStyles.Number));
                        this.remarks08.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "10行目"
                    if (De == 9)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode09 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode09 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt269 = new DataTable();
                        string sql_control10 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control10, conn);
                        adapter.Fill(dt269);
                        mainCategoryComboBox09.DataSource = dt269;
                        mainCategoryComboBox09.DisplayMember = "main_category_name";
                        mainCategoryComboBox09.ValueMember = "main_category_code";
                        mainCategoryComboBox09.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox09.SelectedValue = itemMainCategoryCode09;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt279 = new DataTable();
                        string sql_item10 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item10, conn);
                        adapter.Fill(dt279);
                        itemComboBox09.DataSource = dt279;
                        itemComboBox09.DisplayMember = "item_name";
                        itemComboBox09.ValueMember = "item_code";
                        itemComboBox09.SelectedValue = itemCode09;
                        #endregion
                        #endregion
                        #region "入力された項目 10行目"
                        this.weightTextBox09.Text = dataRow1["weight"].ToString();
                        this.countTextBox09.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox09.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox09.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox09.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox09.Text = dataRow1["amount"].ToString();
                        moneyTextBox09.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox09.Text, System.Globalization.NumberStyles.Number));
                        this.remarks09.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "11行目"
                    if (De == 10)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode010 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode010 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt2610 = new DataTable();
                        string sql_control11 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control11, conn);
                        adapter.Fill(dt2610);
                        mainCategoryComboBox010.DataSource = dt2610;
                        mainCategoryComboBox010.DisplayMember = "main_category_name";
                        mainCategoryComboBox010.ValueMember = "main_category_code";
                        mainCategoryComboBox010.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox010.SelectedValue = itemMainCategoryCode010;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2710 = new DataTable();
                        string sql_item11 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item11, conn);
                        adapter.Fill(dt2710);
                        itemComboBox010.DataSource = dt2710;
                        itemComboBox010.DisplayMember = "item_name";
                        itemComboBox010.ValueMember = "item_code";
                        itemComboBox010.SelectedValue = itemCode010;
                        #endregion
                        #endregion
                        #region "入力された項目 11行目"
                        this.weightTextBox010.Text = dataRow1["weight"].ToString();
                        this.countTextBox010.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox010.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox010.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox010.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox010.Text = dataRow1["amount"].ToString();
                        moneyTextBox010.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox010.Text, System.Globalization.NumberStyles.Number));
                        this.remarks010.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "12行目"
                    if (De == 11)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode011 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode011 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt2611 = new DataTable();
                        string sql_control12 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control12, conn);
                        adapter.Fill(dt2611);
                        mainCategoryComboBox011.DataSource = dt2611;
                        mainCategoryComboBox011.DisplayMember = "main_category_name";
                        mainCategoryComboBox011.ValueMember = "main_category_code";
                        mainCategoryComboBox011.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox011.SelectedValue = itemMainCategoryCode011;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2711 = new DataTable();
                        string sql_item12 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item12, conn);
                        adapter.Fill(dt2711);
                        itemComboBox011.DataSource = dt2711;
                        itemComboBox011.DisplayMember = "item_name";
                        itemComboBox011.ValueMember = "item_code";
                        itemComboBox011.SelectedValue = itemCode011;
                        #endregion
                        #endregion
                        #region "入力された項目 12行目"
                        this.weightTextBox011.Text = dataRow1["weight"].ToString();
                        this.countTextBox011.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox011.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox011.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox011.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox011.Text = dataRow1["amount"].ToString();
                        moneyTextBox011.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox011.Text, System.Globalization.NumberStyles.Number));
                        this.remarks011.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                    #region "13行目"
                    if (De == 12)
                    {
                        DataTable dt25 = new DataTable();
                        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
                        adapter = new NpgsqlDataAdapter(str_control0, conn);
                        adapter.Fill(dt25);
                        DataRow dataRow1;
                        dataRow1 = dt25.Rows[0];
                        int itemMainCategoryCode012 = (int)dataRow1["main_category_code"]; //大分類
                        int itemCode012 = (int)dataRow1["item_code"];　　//品名
                        #region "コンボボックス"
                        #region "大分類"
                        DataTable dt2612 = new DataTable();
                        string sql_control13 = "select * from main_category_m where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_control13, conn);
                        adapter.Fill(dt2612);
                        mainCategoryComboBox012.DataSource = dt2612;
                        mainCategoryComboBox012.DisplayMember = "main_category_name";
                        mainCategoryComboBox012.ValueMember = "main_category_code";
                        mainCategoryComboBox012.SelectedIndex = 0;//担当者ごとの初期値設定
                        mainCategoryComboBox012.SelectedValue = itemMainCategoryCode012;
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2712 = new DataTable();
                        string sql_item13 = "select * from item_m  where invalid = 0 ;";
                        adapter = new NpgsqlDataAdapter(sql_item13, conn);
                        adapter.Fill(dt2712);
                        itemComboBox012.DataSource = dt2712;
                        itemComboBox012.DisplayMember = "item_name";
                        itemComboBox012.ValueMember = "item_code";
                        itemComboBox012.SelectedValue = itemCode012;
                        #endregion
                        #endregion
                        #region "入力された項目 13行目"
                        this.weightTextBox012.Text = dataRow1["weight"].ToString();
                        this.countTextBox012.Text = dataRow1["count"].ToString();
                        this.unitPriceTextBox012.Text = dataRow1["unit_price"].ToString();
                        unitPriceTextBox012.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox012.Text, System.Globalization.NumberStyles.Number));
                        this.moneyTextBox012.Text = dataRow1["amount"].ToString();
                        moneyTextBox012.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox012.Text, System.Globalization.NumberStyles.Number));
                        this.remarks012.Text = dataRow1["remarks"].ToString();
                        #endregion
                    }
                    #endregion
                }
            }

            #region "顧客選択から計算書戻ってきた時に"
            if (amount10 != 0)
            {
                Properties.Settings.Default.Reload();
                DataTable upddt = new DataTable();
                string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                adapter.Fill(upddt);
                DataRow dataRow2;
                dataRow2 = upddt.Rows[0];
                string date = dataRow2["registration_date"].ToString();
                DataTable dt8 = new DataTable();
                string str_sql_real = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "';";
                adapter = new NpgsqlDataAdapter(str_sql_real, conn);
                adapter.Fill(dt8);
                int c = dt8.Rows.Count;
                if (c >= 1)
                {
                    DataRow dataRow;
                    dataRow = dt8.Rows[0];
                    string totalweight = dataRow["totalweight"].ToString();
                    string totalCount = dataRow["totalcount"].ToString();
                    this.totalWeight.Text = totalweight;
                    this.totalCount.Text = totalCount;
                    #region "再度開く"
                    DataTable dt9 = new DataTable();
                    string str_sql_if2 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if2, conn);
                    adapter.Fill(dt9);
                    int d = dt9.Rows.Count;
                    #endregion
                    for (int i = 0; i <= (d - 1); i++)
                    {
                        #region "計算書1行目"
                        if (i == 0)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode0 = (int)dataRow1["main_category_code"];
                            int itemCode0 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str6 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str6, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox0.DataSource = dt16;
                            mainCategoryComboBox0.DisplayMember = "main_category_name";
                            mainCategoryComboBox0.ValueMember = "main_category_code";
                            mainCategoryComboBox0.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox0.SelectedValue = itemMainCategoryCode0;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str7 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str7, conn);
                            adapter.Fill(dt17);
                            itemComboBox0.DataSource = dt17;
                            itemComboBox0.DisplayMember = "item_name";
                            itemComboBox0.ValueMember = "item_code";
                            itemComboBox0.SelectedValue = itemCode0;
                            #endregion
                            #endregion
                            #region "入力された項目 1行目"
                            this.subTotal.Text = dataRow1["subtotal"].ToString();
                            subTotal.Text = string.Format("{0:C}", decimal.Parse(subTotal.Text, System.Globalization.NumberStyles.Number));
                            this.sumTextBox.Text = dataRow1["total"].ToString();
                            sumTextBox.Text = string.Format("{0:C}", decimal.Parse(sumTextBox.Text, System.Globalization.NumberStyles.Number));
                            this.taxAmount.Text = dataRow1["tax_amount"].ToString();
                            //taxAmount.Text = string.Format("{0:C}", decimal.Parse(taxAmount.Text, System.Globalization.NumberStyles.Number));
                            this.weightTextBox0.Text = dataRow1["weight"].ToString();
                            this.countTextBox0.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox0.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox0.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox0.Text = dataRow1["amount"].ToString();
                            moneyTextBox0.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox0.Text, System.Globalization.NumberStyles.Number));
                            this.remarks0.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書2行目"
                        else if (i == 1)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode1 = (int)dataRow1["main_category_code"];
                            int itemCode1 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str8 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            DataTable dt161 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str8, conn);
                            adapter.Fill(dt161);
                            mainCategoryComboBox1.DataSource = dt161;
                            mainCategoryComboBox1.DisplayMember = "main_category_name";
                            mainCategoryComboBox1.ValueMember = "main_category_code";
                            mainCategoryComboBox1.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox1.SelectedValue = itemMainCategoryCode1;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str9 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            DataTable dt171 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str9, conn);
                            adapter.Fill(dt171);
                            itemComboBox1.DataSource = dt171;
                            itemComboBox1.DisplayMember = "item_name";
                            itemComboBox1.ValueMember = "item_code";
                            itemComboBox1.SelectedValue = itemCode1;
                            #endregion
                            #endregion
                            #region "入力された項目 2行目"
                            this.weightTextBox1.Text = dataRow1["weight"].ToString();
                            this.countTextBox1.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox1.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox1.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox1.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox1.Text = dataRow1["amount"].ToString();
                            moneyTextBox1.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox1.Text, System.Globalization.NumberStyles.Number));
                            this.remarks1.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書3行目"
                        else if (i == 2)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode2 = (int)dataRow1["main_category_code"];
                            int itemCode2 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str10 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            DataTable dt162 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str10, conn);
                            adapter.Fill(dt162);
                            mainCategoryComboBox2.DataSource = dt162;
                            mainCategoryComboBox2.DisplayMember = "main_category_name";
                            mainCategoryComboBox2.ValueMember = "main_category_code";
                            mainCategoryComboBox2.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox2.SelectedValue = itemMainCategoryCode2;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str11 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str11, conn);
                            DataTable dt172 = new DataTable();
                            adapter.Fill(dt172);
                            itemComboBox2.DataSource = dt172;
                            itemComboBox2.DisplayMember = "item_name";
                            itemComboBox2.ValueMember = "item_code";
                            itemComboBox2.SelectedValue = itemCode2;
                            #endregion
                            #endregion
                            #region "入力された項目 3行目"
                            this.weightTextBox2.Text = dataRow1["weight"].ToString();
                            this.countTextBox2.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox2.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox2.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox2.Text = dataRow1["amount"].ToString();
                            moneyTextBox2.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox2.Text, System.Globalization.NumberStyles.Number));
                            this.remarks2.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書4行目"
                        else if (i == 3)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode3 = (int)dataRow1["main_category_code"];
                            int itemCode3 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str12 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str12, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox3.DataSource = dt16;
                            mainCategoryComboBox3.DisplayMember = "main_category_name";
                            mainCategoryComboBox3.ValueMember = "main_category_code";
                            mainCategoryComboBox3.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox3.SelectedValue = itemMainCategoryCode3;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str13 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str13, conn);
                            adapter.Fill(dt17);
                            itemComboBox3.DataSource = dt17;
                            itemComboBox3.DisplayMember = "item_name";
                            itemComboBox3.ValueMember = "item_code";
                            itemComboBox3.SelectedValue = itemCode3;
                            #endregion
                            #endregion
                            #region "入力された項目 4行目"
                            this.weightTextBox3.Text = dataRow1["weight"].ToString();
                            this.countTextBox3.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox3.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox3.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox3.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox3.Text = dataRow1["amount"].ToString();
                            moneyTextBox3.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox3.Text, System.Globalization.NumberStyles.Number));
                            this.remarks3.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書5行目"
                        else if (i == 4)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode4 = (int)dataRow1["main_category_code"];
                            int itemCode4 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str14 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str14, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox4.DataSource = dt16;
                            mainCategoryComboBox4.DisplayMember = "main_category_name";
                            mainCategoryComboBox4.ValueMember = "main_category_code";
                            mainCategoryComboBox4.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox4.SelectedValue = itemMainCategoryCode4;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str15 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str15, conn);
                            adapter.Fill(dt17);
                            itemComboBox4.DataSource = dt17;
                            itemComboBox4.DisplayMember = "item_name";
                            itemComboBox4.ValueMember = "item_code";
                            itemComboBox4.SelectedValue = itemCode4;
                            #endregion
                            #endregion
                            #region "入力された項目 5行目"
                            this.weightTextBox4.Text = dataRow1["weight"].ToString();
                            this.countTextBox4.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox4.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox4.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox4.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox4.Text = dataRow1["amount"].ToString();
                            moneyTextBox4.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox4.Text, System.Globalization.NumberStyles.Number));
                            this.remarks4.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書6行目"
                        else if (i == 5)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode5 = (int)dataRow1["main_category_code"];
                            int itemCode5 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str16 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str16, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox5.DataSource = dt16;
                            mainCategoryComboBox5.DisplayMember = "main_category_name";
                            mainCategoryComboBox5.ValueMember = "main_category_code";
                            mainCategoryComboBox5.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox5.SelectedValue = itemMainCategoryCode5;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str17 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str17, conn);
                            adapter.Fill(dt17);
                            itemComboBox5.DataSource = dt17;
                            itemComboBox5.DisplayMember = "item_name";
                            itemComboBox5.ValueMember = "item_code";
                            itemComboBox5.SelectedValue = itemCode5;
                            #endregion
                            #endregion
                            #region "入力された項目 6行目"
                            this.weightTextBox5.Text = dataRow1["weight"].ToString();
                            this.countTextBox5.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox5.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox5.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox5.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox5.Text = dataRow1["amount"].ToString();
                            moneyTextBox5.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox5.Text, System.Globalization.NumberStyles.Number));
                            this.remarks5.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書7行目"
                        else if (i == 6)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode6 = (int)dataRow1["main_category_code"];
                            int itemCode6 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str18 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str18, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox6.DataSource = dt16;
                            mainCategoryComboBox6.DisplayMember = "main_category_name";
                            mainCategoryComboBox6.ValueMember = "main_category_code";
                            mainCategoryComboBox6.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox6.SelectedValue = itemMainCategoryCode6;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str19 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str19, conn);
                            adapter.Fill(dt17);
                            itemComboBox6.DataSource = dt17;
                            itemComboBox6.DisplayMember = "item_name";
                            itemComboBox6.ValueMember = "item_code";
                            itemComboBox6.SelectedValue = itemCode6;
                            #endregion
                            #endregion
                            #region "入力された項目 7行目"
                            this.weightTextBox6.Text = dataRow1["weight"].ToString();
                            this.countTextBox6.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox6.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox6.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox6.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox6.Text = dataRow1["amount"].ToString();
                            moneyTextBox6.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox6.Text, System.Globalization.NumberStyles.Number));
                            this.remarks6.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書8行目"
                        else if (i == 7)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode7 = (int)dataRow1["main_category_code"];
                            int itemCode7 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str20 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str20, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox7.DataSource = dt16;
                            mainCategoryComboBox7.DisplayMember = "main_category_name";
                            mainCategoryComboBox7.ValueMember = "main_category_code";
                            mainCategoryComboBox7.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox7.SelectedValue = itemMainCategoryCode7;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str21 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str21, conn);
                            adapter.Fill(dt17);
                            itemComboBox7.DataSource = dt17;
                            itemComboBox7.DisplayMember = "item_name";
                            itemComboBox7.ValueMember = "item_code";
                            itemComboBox7.SelectedValue = itemCode7;
                            #endregion
                            #endregion
                            #region "入力された項目 8行目"
                            this.weightTextBox7.Text = dataRow1["weight"].ToString();
                            this.countTextBox7.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox7.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox7.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox7.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox7.Text = dataRow1["amount"].ToString();
                            moneyTextBox7.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox7.Text, System.Globalization.NumberStyles.Number));
                            this.remarks7.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書9行目"
                        else if (i == 8)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode8 = (int)dataRow1["main_category_code"];
                            int itemCode8 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str22 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str22, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox8.DataSource = dt16;
                            mainCategoryComboBox8.DisplayMember = "main_category_name";
                            mainCategoryComboBox8.ValueMember = "main_category_code";
                            mainCategoryComboBox8.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox8.SelectedValue = itemMainCategoryCode8;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str23 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str23, conn);
                            adapter.Fill(dt17);
                            itemComboBox8.DataSource = dt17;
                            itemComboBox8.DisplayMember = "item_name";
                            itemComboBox8.ValueMember = "item_code";
                            itemComboBox8.SelectedValue = itemCode8;
                            #endregion
                            #endregion
                            #region "入力された項目 9行目"
                            this.weightTextBox8.Text = dataRow1["weight"].ToString();
                            this.countTextBox8.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox8.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox8.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox8.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox8.Text = dataRow1["amount"].ToString();
                            moneyTextBox8.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox8.Text, System.Globalization.NumberStyles.Number));
                            this.remarks8.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書10行目"
                        else if (i == 9)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode9 = (int)dataRow1["main_category_code"];
                            int itemCode9 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str24 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str24, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox9.DataSource = dt16;
                            mainCategoryComboBox9.DisplayMember = "main_category_name";
                            mainCategoryComboBox9.ValueMember = "main_category_code";
                            mainCategoryComboBox9.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox9.SelectedValue = itemMainCategoryCode9;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str25 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str25, conn);
                            adapter.Fill(dt17);
                            itemComboBox9.DataSource = dt17;
                            itemComboBox9.DisplayMember = "item_name";
                            itemComboBox9.ValueMember = "item_code";
                            itemComboBox9.SelectedValue = itemCode9;
                            #endregion
                            #endregion
                            #region "入力された項目 10行目"
                            this.weightTextBox9.Text = dataRow1["weight"].ToString();
                            this.countTextBox9.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox9.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox9.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox9.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox9.Text = dataRow1["amount"].ToString();
                            moneyTextBox9.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox9.Text, System.Globalization.NumberStyles.Number));
                            this.remarks9.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書11行目"
                        else if (i == 10)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode10 = (int)dataRow1["main_category_code"];
                            int itemCode10 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str26 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str26, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox10.DataSource = dt16;
                            mainCategoryComboBox10.DisplayMember = "main_category_name";
                            mainCategoryComboBox10.ValueMember = "main_category_code";
                            mainCategoryComboBox10.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox10.SelectedValue = itemMainCategoryCode10;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str27 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str27, conn);
                            adapter.Fill(dt17);
                            itemComboBox10.DataSource = dt17;
                            itemComboBox10.DisplayMember = "item_name";
                            itemComboBox10.ValueMember = "item_code";
                            itemComboBox10.SelectedValue = itemCode10;
                            #endregion
                            #endregion
                            #region "入力された項目 11行目"
                            this.weightTextBox10.Text = dataRow1["weight"].ToString();
                            this.countTextBox10.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox10.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox10.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox10.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox10.Text = dataRow1["amount"].ToString();
                            moneyTextBox10.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox10.Text, System.Globalization.NumberStyles.Number));
                            this.remarks10.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書12行目"
                        else if (i == 11)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode11 = (int)dataRow1["main_category_code"];
                            int itemCode11 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str28 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str28, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox11.DataSource = dt16;
                            mainCategoryComboBox11.DisplayMember = "main_category_name";
                            mainCategoryComboBox11.ValueMember = "main_category_code";
                            mainCategoryComboBox11.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox11.SelectedValue = itemMainCategoryCode11;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str29 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str29, conn);
                            adapter.Fill(dt17);
                            itemComboBox11.DataSource = dt17;
                            itemComboBox11.DisplayMember = "item_name";
                            itemComboBox11.ValueMember = "item_code";
                            itemComboBox11.SelectedValue = itemCode11;
                            #endregion
                            #endregion
                            #region "入力された項目 12行目"
                            this.weightTextBox11.Text = dataRow1["weight"].ToString();
                            this.countTextBox11.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox11.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox11.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox11.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox11.Text = dataRow1["amount"].ToString();
                            moneyTextBox11.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox11.Text, System.Globalization.NumberStyles.Number));
                            this.remarks11.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "計算書13行目"
                        else if (i == 12)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and registration_date = '" + date + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode12 = (int)dataRow1["main_category_code"];
                            int itemCode12 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str30 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str30, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox12.DataSource = dt16;
                            mainCategoryComboBox12.DisplayMember = "main_category_name";
                            mainCategoryComboBox12.ValueMember = "main_category_code";
                            mainCategoryComboBox12.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox12.SelectedValue = itemMainCategoryCode12;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str31 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str31, conn);
                            adapter.Fill(dt17);
                            itemComboBox12.DataSource = dt17;
                            itemComboBox12.DisplayMember = "item_name";
                            itemComboBox12.ValueMember = "item_code";
                            itemComboBox12.SelectedValue = itemCode12;
                            #endregion
                            #endregion
                            #region "入力された項目 13行目"
                            this.weightTextBox12.Text = dataRow1["weight"].ToString();
                            this.countTextBox12.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox12.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox12.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox12.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox12.Text = dataRow1["amount"].ToString();
                            moneyTextBox12.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox12.Text, System.Globalization.NumberStyles.Number));
                            this.remarks12.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                    }

                }
            }
            #endregion
            #region "顧客選択から納品書に戻ってきた時"
            if (control != 0 && amount00 != 0)
            {
                DataTable upddt = new DataTable();
                string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                adapter.Fill(upddt);
                DataRow dataRow2;
                dataRow2 = upddt.Rows[0];
                string date = dataRow2["upd_date"].ToString();
                DataTable dt8 = new DataTable();
                string str_sql_if2= "select * from delivery_calc_if where control_number = " + control + "and upd_date = '" + date + "';";
                adapter = new NpgsqlDataAdapter(str_sql_if2, conn);
                adapter.Fill(dt8);
                int c = dt8.Rows.Count;
                if (c >= 1)
                {
                    for (int i = 0; i <= (c - 1); i++)
                    {
                        #region "納品書1行目"
                        if (i == 0)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control  + " and record_number = " + (i + 1) + "and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode0 = (int)dataRow1["main_category_m"];
                            int itemCode0 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str6 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str6, conn);
                            adapter.Fill(dt18);
                            mainCategoryComboBox00.DataSource = dt18;
                            mainCategoryComboBox00.DisplayMember = "main_category_name";
                            mainCategoryComboBox00.ValueMember = "main_category_code";
                            mainCategoryComboBox00.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox00.SelectedValue = itemMainCategoryCode0;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str7 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str7, conn);
                            adapter.Fill(dt19);
                            itemComboBox00.DataSource = dt19;
                            itemComboBox00.DisplayMember = "item_name";
                            itemComboBox00.ValueMember = "item_code";
                            itemComboBox00.SelectedValue = itemCode0;
                            #endregion
                            #endregion
                            #region "入力された項目 1行目"
                            this.weightTextBox00.Text = dataRow1["weight"].ToString();
                            this.countTextBox00.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox00.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox00.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox00.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox00.Text = dataRow1["amount"].ToString();
                            moneyTextBox00.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox00.Text, System.Globalization.NumberStyles.Number));
                            this.remarks00.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書2行目"
                        else if (i == 1)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode1 = (int)dataRow1["main_category_m"];
                            int itemCode1 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str8 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            DataTable dt181 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str8, conn);
                            adapter.Fill(dt181);
                            mainCategoryComboBox01.DataSource = dt181;
                            mainCategoryComboBox01.DisplayMember = "main_category_name";
                            mainCategoryComboBox01.ValueMember = "main_category_code";
                            mainCategoryComboBox01.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox01.SelectedValue = itemMainCategoryCode1;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str9 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            DataTable dt191 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str9, conn);
                            adapter.Fill(dt191);
                            itemComboBox01.DataSource = dt191;
                            itemComboBox01.DisplayMember = "item_name";
                            itemComboBox01.ValueMember = "item_code";
                            itemComboBox01.SelectedValue = itemCode1;
                            #endregion
                            #endregion
                            #region "入力された項目 2行目"
                            this.weightTextBox01.Text = dataRow1["weight"].ToString();
                            this.countTextBox01.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox01.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox01.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox01.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox01.Text = dataRow1["amount"].ToString();
                            moneyTextBox01.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox01.Text, System.Globalization.NumberStyles.Number));
                            this.remarks01.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書3行目"
                        else if (i == 2)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode2 = (int)dataRow1["main_category_m"];
                            int itemCode2 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str10 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            DataTable dt182 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str10, conn);
                            adapter.Fill(dt182);
                            mainCategoryComboBox02.DataSource = dt182;
                            mainCategoryComboBox02.DisplayMember = "main_category_name";
                            mainCategoryComboBox02.ValueMember = "main_category_code";
                            mainCategoryComboBox02.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox02.SelectedValue = itemMainCategoryCode2;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str11 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str11, conn);
                            DataTable dt192 = new DataTable();
                            adapter.Fill(dt192);
                            itemComboBox02.DataSource = dt192;
                            itemComboBox02.DisplayMember = "item_name";
                            itemComboBox02.ValueMember = "item_code";
                            itemComboBox02.SelectedValue = itemCode2;
                            #endregion
                            #endregion
                            #region "入力された項目 3行目"
                            this.weightTextBox02.Text = dataRow1["weight"].ToString();
                            this.countTextBox02.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox02.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox02.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox02.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox02.Text = dataRow1["amount"].ToString();
                            moneyTextBox02.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox02.Text, System.Globalization.NumberStyles.Number));
                            this.remarks02.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書4行目"
                        else if (i == 3)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode3 = (int)dataRow1["main_category_m"];
                            int itemCode3 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str12 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str12, conn);
                            DataTable dt183 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str12, conn);
                            adapter.Fill(dt183);
                            mainCategoryComboBox03.DataSource = dt183;
                            mainCategoryComboBox03.DisplayMember = "main_category_name";
                            mainCategoryComboBox03.ValueMember = "main_category_code";
                            mainCategoryComboBox03.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox03.SelectedValue = itemMainCategoryCode3;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str13 = "select * from item_m where invalid = 0 order by main_category_code;";
                            adapter = new NpgsqlDataAdapter(sql_str13, conn);
                            DataTable dt193 = new DataTable();
                            adapter.Fill(dt193);
                            itemComboBox03.DataSource = dt193;
                            itemComboBox03.DisplayMember = "item_name";
                            itemComboBox03.ValueMember = "item_code";
                            itemComboBox03.SelectedValue = itemCode3;
                            #endregion
                            #endregion
                            #region "入力された項目 4行目"
                            this.weightTextBox03.Text = dataRow1["weight"].ToString();
                            this.countTextBox03.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox03.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox03.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox03.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox03.Text = dataRow1["amount"].ToString();
                            moneyTextBox03.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox03.Text, System.Globalization.NumberStyles.Number));
                            this.remarks03.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書5行目"
                        else if (i == 4)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode4 = (int)dataRow1["main_category_m"];
                            int itemCode4 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str14 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            DataTable dt184 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str14, conn);
                            adapter.Fill(dt184);
                            mainCategoryComboBox04.DataSource = dt184;
                            mainCategoryComboBox04.DisplayMember = "main_category_name";
                            mainCategoryComboBox04.ValueMember = "main_category_code";
                            mainCategoryComboBox04.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox04.SelectedValue = itemMainCategoryCode4;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str15 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            DataTable dt194 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str15, conn);
                            adapter.Fill(dt194);
                            itemComboBox04.DataSource = dt194;
                            itemComboBox04.DisplayMember = "item_name";
                            itemComboBox04.ValueMember = "item_code";
                            itemComboBox04.SelectedValue = itemCode4;
                            #endregion
                            #endregion
                            #region "入力された項目 5行目"
                            this.weightTextBox04.Text = dataRow1["weight"].ToString();
                            this.countTextBox04.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox04.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox04.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox04.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox04.Text = dataRow1["amount"].ToString();
                            moneyTextBox04.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox04.Text, System.Globalization.NumberStyles.Number));
                            this.remarks04.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書6行目"
                        else if (i == 5)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode5 = (int)dataRow1["main_category_m"];
                            int itemCode5 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str16 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            DataTable dt185 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str16, conn);
                            adapter.Fill(dt185);
                            mainCategoryComboBox05.DataSource = dt185;
                            mainCategoryComboBox05.DisplayMember = "main_category_name";
                            mainCategoryComboBox05.ValueMember = "main_category_code";
                            mainCategoryComboBox05.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox05.SelectedValue = itemMainCategoryCode5;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str17 = "select * from item_m where invalid = 0 order by main_category_code;";
                            DataTable dt195 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str17, conn);
                            adapter.Fill(dt195);
                            itemComboBox05.DataSource = dt195;
                            itemComboBox05.DisplayMember = "item_name";
                            itemComboBox05.ValueMember = "item_code";
                            itemComboBox05.SelectedValue = itemCode5;
                            #endregion
                            #endregion
                            #region "入力された項目 6行目"
                            this.weightTextBox05.Text = dataRow1["weight"].ToString();
                            this.countTextBox05.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox05.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox05.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox05.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox05.Text = dataRow1["amount"].ToString();
                            moneyTextBox05.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox05.Text, System.Globalization.NumberStyles.Number));
                            this.remarks05.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書7行目"
                        else if (i == 6)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode6 = (int)dataRow1["main_category_m"];
                            int itemCode6 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str18 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            DataTable dt186 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str18, conn);
                            adapter.Fill(dt186);
                            mainCategoryComboBox06.DataSource = dt186;
                            mainCategoryComboBox06.DisplayMember = "main_category_name";
                            mainCategoryComboBox06.ValueMember = "main_category_code";
                            mainCategoryComboBox06.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox06.SelectedValue = itemMainCategoryCode6;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str19 = "select * from item_m where invalid = 0 order by main_category_code;";
                            DataTable dt196 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str19, conn);
                            adapter.Fill(dt196);
                            itemComboBox06.DataSource = dt196;
                            itemComboBox06.DisplayMember = "item_name";
                            itemComboBox06.ValueMember = "item_code";
                            itemComboBox06.SelectedValue = itemCode6;
                            #endregion
                            #endregion
                            #region "入力された項目 7行目"
                            this.weightTextBox06.Text = dataRow1["weight"].ToString();
                            this.countTextBox06.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox06.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox06.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox06.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox06.Text = dataRow1["amount"].ToString();
                            moneyTextBox06.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox06.Text, System.Globalization.NumberStyles.Number));
                            this.remarks06.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書8行目"
                        else if (i == 7)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode7 = (int)dataRow1["main_category_m"];
                            int itemCode7 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str20 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            DataTable dt187 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str20, conn);
                            adapter.Fill(dt187);
                            mainCategoryComboBox07.DataSource = dt187;
                            mainCategoryComboBox07.DisplayMember = "main_category_name";
                            mainCategoryComboBox07.ValueMember = "main_category_code";
                            mainCategoryComboBox07.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox07.SelectedValue = itemMainCategoryCode7;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str21 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            DataTable dt197 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str21, conn);
                            adapter.Fill(dt197);
                            itemComboBox07.DataSource = dt197;
                            itemComboBox07.DisplayMember = "item_name";
                            itemComboBox07.ValueMember = "item_code";
                            itemComboBox07.SelectedValue = itemCode7;
                            #endregion
                            #endregion
                            #region "入力された項目 8行目"
                            this.weightTextBox07.Text = dataRow1["weight"].ToString();
                            this.countTextBox07.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox07.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox07.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox07.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox07.Text = dataRow1["amount"].ToString();
                            moneyTextBox07.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox07.Text, System.Globalization.NumberStyles.Number));
                            this.remarks07.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書9行目"
                        else if (i == 8)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode8 = (int)dataRow1["main_category_m"];
                            int itemCode8 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str22 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            DataTable dt188 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str22, conn);
                            adapter.Fill(dt188);
                            mainCategoryComboBox08.DataSource = dt188;
                            mainCategoryComboBox08.DisplayMember = "main_category_name";
                            mainCategoryComboBox08.ValueMember = "main_category_code";
                            mainCategoryComboBox08.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox08.SelectedValue = itemMainCategoryCode8;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str23 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            DataTable dt198 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str23, conn);
                            adapter.Fill(dt198);
                            itemComboBox08.DataSource = dt198;
                            itemComboBox08.DisplayMember = "item_name";
                            itemComboBox08.ValueMember = "item_code";
                            itemComboBox08.SelectedValue = itemCode8;
                            #endregion
                            #endregion
                            #region "入力された項目 9行目"
                            this.weightTextBox08.Text = dataRow1["weight"].ToString();
                            this.countTextBox08.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox08.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox08.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox08.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox08.Text = dataRow1["amount"].ToString();
                            moneyTextBox08.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox08.Text, System.Globalization.NumberStyles.Number));
                            this.remarks08.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書10行目"
                        else if (i == 9)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode9 = (int)dataRow1["main_category_m"];
                            int itemCode9 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str24 = "select * from main_category_m where invalid = 0 order by main_category_code;";
                            DataTable dt189 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str24, conn);
                            adapter.Fill(dt189);
                            mainCategoryComboBox09.DataSource = dt189;
                            mainCategoryComboBox09.DisplayMember = "main_category_name";
                            mainCategoryComboBox09.ValueMember = "main_category_code";
                            mainCategoryComboBox09.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox09.SelectedValue = itemMainCategoryCode9;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str25 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            DataTable dt199 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str25, conn);
                            adapter.Fill(dt199);
                            itemComboBox09.DataSource = dt199;
                            itemComboBox09.DisplayMember = "item_name";
                            itemComboBox09.ValueMember = "item_code";
                            itemComboBox09.SelectedValue = itemCode9;
                            #endregion
                            #endregion
                            #region "入力された項目 10行目"
                            this.weightTextBox09.Text = dataRow1["weight"].ToString();
                            this.countTextBox09.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox09.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox09.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox09.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox09.Text = dataRow1["amount"].ToString();
                            moneyTextBox09.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox09.Text, System.Globalization.NumberStyles.Number));
                            this.remarks09.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書11行目"
                        else if (i == 10)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode10 = (int)dataRow1["main_category_m"];
                            int itemCode10 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str26 = "select * from main_category_m where invalid = 0 order by main_category_code ;";
                            DataTable dt1810 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str26, conn);
                            adapter.Fill(dt1810);
                            mainCategoryComboBox010.DataSource = dt1810;
                            mainCategoryComboBox010.DisplayMember = "main_category_name";
                            mainCategoryComboBox010.ValueMember = "main_category_code";
                            mainCategoryComboBox010.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox010.SelectedValue = itemMainCategoryCode10;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str27 = "select * from item_m where invalid = 0 order by main_category_code;";
                            DataTable dt1910 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str27, conn);
                            adapter.Fill(dt1910);
                            itemComboBox010.DataSource = dt1910;
                            itemComboBox010.DisplayMember = "item_name";
                            itemComboBox010.ValueMember = "item_code";
                            itemComboBox010.SelectedValue = itemCode10;
                            #endregion
                            #endregion
                            #region "入力された項目 11行目"
                            this.weightTextBox010.Text = dataRow1["weight"].ToString();
                            this.countTextBox010.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox010.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox010.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox010.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox010.Text = dataRow1["amount"].ToString();
                            moneyTextBox010.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox010.Text, System.Globalization.NumberStyles.Number));
                            this.remarks010.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書12行目"
                        else if (i == 11)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode11 = (int)dataRow1["main_category_m"];
                            int itemCode11 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str28 = "select * from main_category_m where invalid = 0 order by main_category_code ;";
                            DataTable dt1811 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str28, conn);
                            adapter.Fill(dt1811);
                            mainCategoryComboBox011.DataSource = dt1811;
                            mainCategoryComboBox011.DisplayMember = "main_category_name";
                            mainCategoryComboBox011.ValueMember = "main_category_code";
                            mainCategoryComboBox011.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox011.SelectedValue = itemMainCategoryCode11;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str29 = "select * from item_m  where invalid = 0 order by main_category_code;";
                            DataTable dt1911 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str29, conn);
                            adapter.Fill(dt1911);
                            itemComboBox011.DataSource = dt1911;
                            itemComboBox011.DisplayMember = "item_name";
                            itemComboBox011.ValueMember = "item_code";
                            itemComboBox011.SelectedValue = itemCode11;
                            #endregion
                            #endregion
                            #region "入力された項目 12行目"
                            this.weightTextBox011.Text = dataRow1["weight"].ToString();
                            this.countTextBox011.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox011.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox011.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox011.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox011.Text = dataRow1["amount"].ToString();
                            moneyTextBox011.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox011.Text, System.Globalization.NumberStyles.Number));
                            this.remarks011.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                        #region "納品書13行目"
                        else if (i == 12)
                        {
                            DataTable dt10 = new DataTable();
                            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + (i + 1) + " and upd_date = '" + date + "';";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode12 = (int)dataRow1["main_category_m"];
                            int itemCode12 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str30 = "select * from main_category_m where invalid = 0 order by main_category_code ;";
                            DataTable dt1812 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str30, conn);
                            adapter.Fill(dt1812);
                            mainCategoryComboBox012.DataSource = dt1812;
                            mainCategoryComboBox012.DisplayMember = "main_category_name";
                            mainCategoryComboBox012.ValueMember = "main_category_code";
                            mainCategoryComboBox012.SelectedIndex = 0;//担当者ごとの初期値設定
                            mainCategoryComboBox012.SelectedValue = itemMainCategoryCode12;
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str31 = "select * from item_m where invalid = 0 order by main_category_code;";
                            DataTable dt1912 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str31, conn);
                            adapter.Fill(dt1912);
                            itemComboBox012.DataSource = dt1912;
                            itemComboBox012.DisplayMember = "item_name";
                            itemComboBox012.ValueMember = "item_code";
                            itemComboBox012.SelectedValue = itemCode12;
                            #endregion
                            #endregion
                            #region "入力された項目 13行目"
                            this.weightTextBox012.Text = dataRow1["weight"].ToString();
                            this.countTextBox012.Text = dataRow1["count"].ToString();
                            this.unitPriceTextBox012.Text = dataRow1["unit_price"].ToString();
                            unitPriceTextBox012.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox012.Text, System.Globalization.NumberStyles.Number));
                            this.moneyTextBox012.Text = dataRow1["amount"].ToString();
                            moneyTextBox012.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox012.Text, System.Globalization.NumberStyles.Number));
                            this.remarks012.Text = dataRow1["remarks"].ToString();
                            #endregion
                        }
                        #endregion
                    }

                }
            }
            #endregion
            if (subSum < 2000000)
            {
                groupBox1.Hide();
            }
            else
            {
                
            }
            //左の入力項目処理
            //string itemDisplay = "item_name";
            //string itemValue = "item_code";
            //地金
            DataTable dt300 = new DataTable();
            string str_sql_metal = "select * from item_m where main_category_code = 100";
            adapter = new NpgsqlDataAdapter(str_sql_metal, conn);
            adapter.Fill(dt300);

            //ダイヤ
            DataTable dt400 = new DataTable();
            string str_sql_diamond = "select * from item_m where main_category_code = 101";

            adapter = new NpgsqlDataAdapter(str_sql_diamond, conn);
            adapter.Fill(dt400);

            //ブランド
            DataTable dt500 = new DataTable();
            string str_sql_brand = "select * from item_m where main_category_code = 102";
            adapter = new NpgsqlDataAdapter(str_sql_brand, conn);
            adapter.Fill(dt500);

            //製品・ジュエリー
            DataTable dt600 = new DataTable();
            string str_sql_jewelry = "select * from item_m where main_category_code = 103";
            adapter = new NpgsqlDataAdapter(str_sql_jewelry, conn);
            adapter.Fill(dt600);

            //その他
            DataTable dt700 = new DataTable();
            string str_sql_other = "select * from item_m where main_category_code = 104";
            adapter = new NpgsqlDataAdapter(str_sql_other, conn);
            adapter.Fill(dt700);
            #region "1行目の入力値"
                //単価の欄に初期表示
            if ((data == "S" || data == "D") && total == 0)
            {

            }
            else if (total == 0 )
            {
                unitPriceTextBox0.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox00.Text = "単価 -> 重量 or 数量";
            }
            #endregion

            //タブのサイズ変更
            tabControl1.ItemSize = new Size(300, 40);

            //デフォルトで税込み表示
            comboBox11.SelectedIndex = 0;

            //デフォルトで円表示
            CoinComboBox.SelectedIndex = 0;
            #region "これから選択する場合"
            if (count != 0)
            {           
                if (type == 0)
                {
                    
                    //顧客情報 法人
                    #region "計算書"
                    DataTable clientDt = new DataTable();
                    string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    //int type = (int)row2["type"];

                    string companyNmae = row2["company_name"].ToString();
                    string shopName = row2["shop_name"].ToString();
                    string Staff_name = row2["staff_name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    this.client_Button.Text = "顧客変更";
                    typeTextBox.Text = "法人";                    //種別
                    companyTextBox.Text = companyNmae;              //会社名   
                    registerDateTextBox2.Text = antique_license;              //古物商許可証
                    shopNameTextBox.Text = shopName;                    //店舗名
                    clientNameTextBox.Text = Staff_name;                //担当名
                    registerDateTextBox.Text = register_date;           //登録日
                    clientRemarksTextBox.Text = remarks;                //備考
                    #endregion
                    #region "納品書"
                    this.client_searchButton1.Text = "顧客変更";
                    typeTextBox2.Text = "法人";                   //種別
                    companyTextBox2.Text = companyNmae;             //会社名
                    shopNameTextBox2.Text = shopName;               //店舗名
                    clientNameTextBox2.Text = Staff_name;           //担当名
                    registerDateTextBox2.Text = register_date;      //登録日
                    clientRemarksTextBox2.Text = remarks;           //備考
                    antiqueLicenceTextBox2.Text = antique_license;                //古物商許可証
                    #endregion

                }
                else if (type == 1)
                {
                    //顧客情報 個人
                    DataTable clientDt = new DataTable();
                    string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];

                    string name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string occupation = row2["occupation"].ToString();
                    string birthday = row2["birthday"].ToString();
                    string antique_license = row2["antique_license"].ToString();

                    #region "計算書"
                    label16.Text = "氏名";
                    label17.Text = "生年月日";
                    label18.Text = "職業";
                    typeTextBox.Text = "個人";
                    companyTextBox.Text = name;
                    shopNameTextBox.Text = birthday;
                    clientNameTextBox.Text = occupation;
                    registerDateTextBox.Text = register_date;
                    clientRemarksTextBox.Text = remarks;
                    registerDateTextBox2.Text = antique_license;
                    label38.Visible = false;
                    registerDateTextBox.Visible = false;
                    #endregion

                    #region "納品書"
                    typeTextBox2.Text = "個人";
                    label75.Text = "氏名";
                    label76.Text = "職業";
                    label77.Text = "生年月日";
                    clientNameTextBox2.Text = occupation;
                    companyTextBox2.Text = name;
                    shopNameTextBox2.Text = birthday;
                    registerDateTextBox2.Text = register_date;
                    clientRemarksTextBox2.Text = remarks;
                    antiqueLicenceTextBox2.Text = antique_license;
                    label36.Visible = false;
                    registerDateTextBox2.Visible = false;
                    #endregion
                }
            }
            #endregion
            #region "１度選択して戻る場合"
            else if (count == 0 && (address != null && client_staff_name != null) && data == null)
            {
                if (type == 0)
                {
                    //顧客情報 法人
                    #region "計算書"
                    DataTable clientDt = new DataTable();
                    string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    //int type = (int)row2["type"];

                    string companyNmae = row2["company_name"].ToString();
                    string shopName = row2["shop_name"].ToString();
                    string Staff_name = row2["staff_name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    this.client_Button.Text = "顧客変更";
                    typeTextBox.Text = "法人";                    //種別
                    companyTextBox.Text = companyNmae;              //会社名   
                    registerDateTextBox2.Text = antique_license;              //古物商許可証
                    shopNameTextBox.Text = shopName;                    //店舗名
                    clientNameTextBox.Text = Staff_name;                //担当名
                    registerDateTextBox.Text = register_date;           //登録日
                    clientRemarksTextBox.Text = remarks;                //備考
                    #endregion
                    #region "納品書"
                    this.client_searchButton1.Text = "顧客変更";
                    typeTextBox2.Text = "法人";                     //種別
                    companyTextBox2.Text = companyNmae;             //会社名
                    shopNameTextBox2.Text = shopName;               //店舗名
                    clientNameTextBox2.Text = Staff_name;           //担当名
                    registerDateTextBox2.Text = register_date;      //登録日
                    clientRemarksTextBox2.Text = remarks;           //備考
                    antiqueLicenceTextBox2.Text = antique_license;                //古物商許可証
                    #endregion

                }
                else if (type == 1)
                {
                    //顧客情報 個人
                    DataTable clientDt = new DataTable();
                    string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];

                    string name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string occupation = row2["occupation"].ToString();
                    string birthday = row2["birthday"].ToString();
                    string antique_license = row2["antique_license"].ToString();

                    #region "計算書"
                    label16.Text = "氏名";
                    label17.Text = "生年月日";
                    label18.Text = "職業";
                    typeTextBox.Text = "個人";
                    companyTextBox.Text = name;
                    shopNameTextBox.Text = birthday;
                    clientNameTextBox.Text = occupation;
                    registerDateTextBox.Text = register_date;
                    clientRemarksTextBox.Text = remarks;
                    registerDateTextBox2.Text = antique_license;
                    label38.Visible = false;
                    registerDateTextBox.Visible = false;
                    #endregion

                    #region "納品書"
                    typeTextBox2.Text = "個人";
                    label75.Text = "氏名";
                    label76.Text = "職業";
                    label77.Text = "生年月日";
                    clientNameTextBox2.Text = occupation;
                    companyTextBox2.Text = name;
                    shopNameTextBox2.Text = birthday;
                    registerDateTextBox2.Text = register_date;
                    clientRemarksTextBox2.Text = remarks;
                    antiqueLicenceTextBox2.Text = antique_license;
                    label36.Visible = false;
                    registerDateTextBox2.Visible = false;
                    #endregion
                }
            }
            #endregion
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }
        //納品書戻る
        private void return2_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }

        #region "計算書　大分類変更"
        #region "1行目"
        private void mainCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e) //大分類選択分岐
        {
            //大分類によって品名変更　1行目
            if (a > 0)
            {
                int codeNum = (int)mainCategoryComboBox0.SelectedValue;
                dt2.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt2);
                itemComboBox0.DataSource = dt2;
                itemComboBox0.DisplayMember = "item_name";
                itemComboBox0.ValueMember = "item_code";

                DataRow row = dt2.Rows[0];
                mainCategoryCode0 = (int)row["main_category_code"];
                itemCode0 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount10 != 0)
            {
                int codeNum = (int)mainCategoryComboBox0.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox0.DataSource = dt17;
                itemComboBox0.DisplayMember = "item_name";
                itemComboBox0.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode0 = (int)row["main_category_code"];
                itemCode0 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "2行目"
        private void mainCategoryComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 2行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox1.SelectedValue;
                dt200.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt200);
                itemComboBox1.DataSource = dt200;
                itemComboBox1.DisplayMember = "item_name";
                itemComboBox1.ValueMember = "item_code";

                DataRow row = dt200.Rows[0];
                mainCategoryCode1 = (int)row["main_category_code"];
                itemCode1 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount11 != 0)
            {
                int codeNum = (int)mainCategoryComboBox1.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox1.DataSource = dt17;
                itemComboBox1.DisplayMember = "item_name";
                itemComboBox1.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode1 = (int)row["main_category_code"];
                itemCode1 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "3行目"
        private void mainCategoryComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 3行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox2.SelectedValue;
                dt201.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt201);
                itemComboBox2.DataSource = dt201;
                itemComboBox2.DisplayMember = "item_name";
                itemComboBox2.ValueMember = "item_code";

                DataRow row = dt201.Rows[0];
                mainCategoryCode2 = (int)row["main_category_code"];
                itemCode2 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount12 != 0)
            {
                int codeNum = (int)mainCategoryComboBox2.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox2.DataSource = dt17;
                itemComboBox2.DisplayMember = "item_name";
                itemComboBox2.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode2 = (int)row["main_category_code"];
                itemCode2 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "4行目"
        private void mainCategoryComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 4行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox3.SelectedValue;
                dt202.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt202);
                itemComboBox3.DataSource = dt202;
                itemComboBox3.DisplayMember = "item_name";
                itemComboBox3.ValueMember = "item_code";

                DataRow row = dt202.Rows[0];
                mainCategoryCode3 = (int)row["main_category_code"];
                itemCode3 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount13 != 0)
            {
                int codeNum = (int)mainCategoryComboBox3.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox3.DataSource = dt17;
                itemComboBox3.DisplayMember = "item_name";
                itemComboBox3.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode3 = (int)row["main_category_code"];
                itemCode3 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "5行目"
        private void mainCategoryComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 5行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox4.SelectedValue;
                dt203.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt203);
                itemComboBox4.DataSource = dt203;
                itemComboBox4.DisplayMember = "item_name";
                itemComboBox4.ValueMember = "item_code";

                DataRow row = dt203.Rows[0];
                mainCategoryCode4 = (int)row["main_category_code"];
                itemCode4 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount14 != 0)
            {
                int codeNum = (int)mainCategoryComboBox4.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox4.DataSource = dt17;
                itemComboBox4.DisplayMember = "item_name";
                itemComboBox4.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode4 = (int)row["main_category_code"];
                itemCode4 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "6行目"
        private void mainCategoryComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 6行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox5.SelectedValue;
                dt204.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt204);
                itemComboBox5.DataSource = dt204;
                itemComboBox5.DisplayMember = "item_name";
                itemComboBox5.ValueMember = "item_code";

                DataRow row = dt204.Rows[0];
                mainCategoryCode5 = (int)row["main_category_code"];
                itemCode5 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount15 != 0)
            {
                int codeNum = (int)mainCategoryComboBox5.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox5.DataSource = dt17;
                itemComboBox5.DisplayMember = "item_name";
                itemComboBox5.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode5 = (int)row["main_category_code"];
                itemCode5 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "7行目"
        private void mainCategoryComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 7行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox6.SelectedValue;
                dt205.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt205);
                itemComboBox6.DataSource = dt205;
                itemComboBox6.DisplayMember = "item_name";
                itemComboBox6.ValueMember = "item_code";

                DataRow row = dt205.Rows[0];
                mainCategoryCode6 = (int)row["main_category_code"];
                itemCode6 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount16 != 0)
            {
                int codeNum = (int)mainCategoryComboBox6.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox6.DataSource = dt17;
                itemComboBox6.DisplayMember = "item_name";
                itemComboBox6.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode6 = (int)row["main_category_code"];
                itemCode6 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "8行目"
        private void mainCategoryComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 8行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox7.SelectedValue;
                dt206.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt206);
                itemComboBox7.DataSource = dt206;
                itemComboBox7.DisplayMember = "item_name";
                itemComboBox7.ValueMember = "item_code";

                DataRow row = dt206.Rows[0];
                mainCategoryCode7 = (int)row["main_category_code"];
                itemCode7 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount17 != 0)
            {
                int codeNum = (int)mainCategoryComboBox7.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox7.DataSource = dt17;
                itemComboBox7.DisplayMember = "item_name";
                itemComboBox7.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode7 = (int)row["main_category_code"];
                itemCode7 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "9行目"
        private void mainCategoryComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 9行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox8.SelectedValue;
                dt207.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt207);
                itemComboBox8.DataSource = dt207;
                itemComboBox8.DisplayMember = "item_name";
                itemComboBox8.ValueMember = "item_code";

                DataRow row = dt207.Rows[0];
                mainCategoryCode8 = (int)row["main_category_code"];
                itemCode8 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount18 != 0)
            {
                int codeNum = (int)mainCategoryComboBox8.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox8.DataSource = dt17;
                itemComboBox8.DisplayMember = "item_name";
                itemComboBox8.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode8 = (int)row["main_category_code"];
                itemCode8 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "10行目"
        private void mainCategoryComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 10行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox9.SelectedValue;
                dt208.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt208);
                itemComboBox9.DataSource = dt208;
                itemComboBox9.DisplayMember = "item_name";
                itemComboBox9.ValueMember = "item_code";

                DataRow row = dt208.Rows[0];
                mainCategoryCode9 = (int)row["main_category_code"];
                itemCode9 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount19 != 0)
            {
                int codeNum = (int)mainCategoryComboBox9.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox9.DataSource = dt17;
                itemComboBox9.DisplayMember = "item_name";
                itemComboBox9.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode9 = (int)row["main_category_code"];
                itemCode9 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "11行目"
        private void mainCategoryComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 11行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox10.SelectedValue;
                dt209.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt209);
                itemComboBox10.DataSource = dt209;
                itemComboBox10.DisplayMember = "item_name";
                itemComboBox10.ValueMember = "item_code";

                DataRow row = dt209.Rows[0];
                mainCategoryCode10 = (int)row["main_category_code"];
                itemCode10 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount110 != 0)
            {
                int codeNum = (int)mainCategoryComboBox10.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox10.DataSource = dt17;
                itemComboBox10.DisplayMember = "item_name";
                itemComboBox10.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode10 = (int)row["main_category_code"];
                itemCode10 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "12行目"
        private void mainCategoryComboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 12行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox11.SelectedValue;
                dt210.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt210);
                itemComboBox11.DataSource = dt210;
                itemComboBox11.DisplayMember = "item_name";
                itemComboBox11.ValueMember = "item_code";

                DataRow row = dt210.Rows[0];
                mainCategoryCode11 = (int)row["main_category_code"];
                itemCode11 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount111 != 0)
            {
                int codeNum = (int)mainCategoryComboBox11.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox11.DataSource = dt17;
                itemComboBox11.DisplayMember = "item_name";
                itemComboBox11.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode11 = (int)row["main_category_code"];
                itemCode11 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "13行目"
        private void mainCategoryComboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 13行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox12.SelectedValue;
                dt211.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt211);
                itemComboBox12.DataSource = dt211;
                itemComboBox12.DisplayMember = "item_name";
                itemComboBox12.ValueMember = "item_code";

                DataRow row = dt211.Rows[0];
                mainCategoryCode12 = (int)row["main_category_code"];
                itemCode12 = (int)row["item_code"];

                conn.Close();
            }
            else if (a > 1 && amount112 != 0)
            {
                int codeNum = (int)mainCategoryComboBox12.SelectedValue;
                dt17.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt17);
                itemComboBox12.DataSource = dt17;
                itemComboBox12.DisplayMember = "item_name";
                itemComboBox12.ValueMember = "item_code";

                DataRow row = dt17.Rows[0];
                mainCategoryCode12 = (int)row["main_category_code"];
                itemCode12 = (int)row["item_code"];

                conn.Close();
            }
            else
            {
                a++;
            }
            
        }
        #endregion
        #endregion

        #region "納品書　大分類変更"
        #region "1行目"
        private void mainCategoryComboBox00_SelectedIndexChanged(object sender, EventArgs e) //大分類選択分岐
        {
            //大分類によって品名変更　1行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox00.SelectedValue;
                deliverydt200.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt200);
                itemComboBox00.DataSource = deliverydt200;
                itemComboBox00.DisplayMember = "item_name";
                itemComboBox00.ValueMember = "item_code";

                DataRow row = deliverydt200.Rows[0];
                mainCategoryCode00 = (int)row["main_category_code"];
                itemCode00 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount00 != 0)
            {
                int codeNum = (int)mainCategoryComboBox00.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox00.DataSource = dt19;
                itemComboBox00.DisplayMember = "item_name";
                itemComboBox00.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode00 = (int)row["main_category_code"];
                itemCode00 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "2行目"
        private void mainCategoryComboBox01_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 2行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox01.SelectedValue;
                deliverydt201.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt201);
                itemComboBox01.DataSource = deliverydt201;
                itemComboBox01.DisplayMember = "item_name";
                itemComboBox01.ValueMember = "item_code";

                DataRow row = deliverydt201.Rows[0];
                mainCategoryCode01 = (int)row["main_category_code"];
                itemCode01 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount01 != 0)
            {
                int codeNum = (int)mainCategoryComboBox01.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox01.DataSource = dt19;
                itemComboBox01.DisplayMember = "item_name";
                itemComboBox01.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode01 = (int)row["main_category_code"];
                itemCode01 = (int)row["item_code"];

                conn.Close();
            }
            else
            {

            }
        }
        #endregion
        #region "3行目"
        private void mainCategoryComboBox02_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 3行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox02.SelectedValue;
                deliverydt202.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt202);
                itemComboBox02.DataSource = deliverydt202;
                itemComboBox02.DisplayMember = "item_name";
                itemComboBox02.ValueMember = "item_code";

                DataRow row = deliverydt202.Rows[0];
                mainCategoryCode02 = (int)row["main_category_code"];
                itemCode02 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount02 != 0)
            {
                int codeNum = (int)mainCategoryComboBox02.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox02.DataSource = dt19;
                itemComboBox02.DisplayMember = "item_name";
                itemComboBox02.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode02 = (int)row["main_category_code"];
                itemCode02 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "4行目"
        private void mainCategoryComboBox03_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 4行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox03.SelectedValue;
                deliverydt203.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt203);
                itemComboBox03.DataSource = deliverydt203;
                itemComboBox03.DisplayMember = "item_name";
                itemComboBox03.ValueMember = "item_code";

                DataRow row = deliverydt203.Rows[0];
                mainCategoryCode03 = (int)row["main_category_code"];
                itemCode03 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount03 != 0)
            {
                int codeNum = (int)mainCategoryComboBox03.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox03.DataSource = dt19;
                itemComboBox03.DisplayMember = "item_name";
                itemComboBox03.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode03 = (int)row["main_category_code"];
                itemCode03 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "5行目"
        private void mainCategoryComboBox04_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 5行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox04.SelectedValue;
                deliverydt204.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt204);
                itemComboBox04.DataSource = deliverydt204;
                itemComboBox04.DisplayMember = "item_name";
                itemComboBox04.ValueMember = "item_code";

                DataRow row = deliverydt204.Rows[0];
                mainCategoryCode04 = (int)row["main_category_code"];
                itemCode04 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount04 != 0)
            {
                int codeNum = (int)mainCategoryComboBox04.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox04.DataSource = dt19;
                itemComboBox04.DisplayMember = "item_name";
                itemComboBox04.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode04 = (int)row["main_category_code"];
                itemCode04 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "6行目"
        private void mainCategoryComboBox05_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 6行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox05.SelectedValue;
                deliverydt205.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt205);
                itemComboBox05.DataSource = deliverydt205;
                itemComboBox05.DisplayMember = "item_name";
                itemComboBox05.ValueMember = "item_code";

                DataRow row = deliverydt205.Rows[0];
                mainCategoryCode05 = (int)row["main_category_code"];
                itemCode05 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount05 != 0)
            {
                int codeNum = (int)mainCategoryComboBox05.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox05.DataSource = dt19;
                itemComboBox05.DisplayMember = "item_name";
                itemComboBox05.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode05 = (int)row["main_category_code"];
                itemCode05 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "7行目"
        private void mainCategoryComboBox06_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 7行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox06.SelectedValue;
                deliverydt206.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt206);
                itemComboBox06.DataSource = deliverydt206;
                itemComboBox06.DisplayMember = "item_name";
                itemComboBox06.ValueMember = "item_code";

                DataRow row = deliverydt206.Rows[0];
                mainCategoryCode06 = (int)row["main_category_code"];
                itemCode06 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount06 != 0)
            {
                int codeNum = (int)mainCategoryComboBox06.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox06.DataSource = dt19;
                itemComboBox06.DisplayMember = "item_name";
                itemComboBox06.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode06 = (int)row["main_category_code"];
                itemCode06 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "8行目"
        private void mainCategoryComboBox07_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 8行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox07.SelectedValue;
                deliverydt207.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt207);
                itemComboBox07.DataSource = deliverydt207;
                itemComboBox07.DisplayMember = "item_name";
                itemComboBox07.ValueMember = "item_code";

                DataRow row = deliverydt207.Rows[0];
                mainCategoryCode07 = (int)row["main_category_code"];
                itemCode07 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount07 != 0)
            {
                int codeNum = (int)mainCategoryComboBox07.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox07.DataSource = dt19;
                itemComboBox07.DisplayMember = "item_name";
                itemComboBox07.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode07 = (int)row["main_category_code"];
                itemCode07 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "9行目"
        private void mainCategoryComboBox08_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 9行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox08.SelectedValue;
                deliverydt208.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt208);
                itemComboBox08.DataSource = deliverydt208;
                itemComboBox08.DisplayMember = "item_name";
                itemComboBox08.ValueMember = "item_code";

                DataRow row = deliverydt208.Rows[0];
                mainCategoryCode08 = (int)row["main_category_code"];
                itemCode08 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount08 != 0)
            {
                int codeNum = (int)mainCategoryComboBox08.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox08.DataSource = dt19;
                itemComboBox08.DisplayMember = "item_name";
                itemComboBox08.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode08 = (int)row["main_category_code"];
                itemCode08 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "10行目"
        private void mainCategoryComboBox09_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 10行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox09.SelectedValue;
                deliverydt209.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt209);
                itemComboBox09.DataSource = deliverydt209;
                itemComboBox09.DisplayMember = "item_name";
                itemComboBox09.ValueMember = "item_code";

                DataRow row = deliverydt209.Rows[0];
                mainCategoryCode09 = (int)row["main_category_code"];
                itemCode09 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount09 != 0)
            {
                int codeNum = (int)mainCategoryComboBox09.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox09.DataSource = dt19;
                itemComboBox09.DisplayMember = "item_name";
                itemComboBox09.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode09 = (int)row["main_category_code"];
                itemCode09 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "11行目"
        private void mainCategoryComboBox010_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 11行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox010.SelectedValue;
                deliverydt210.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt210);
                itemComboBox010.DataSource = deliverydt210;
                itemComboBox010.DisplayMember = "item_name";
                itemComboBox010.ValueMember = "item_code";

                DataRow row = deliverydt210.Rows[0];
                mainCategoryCode010 = (int)row["main_category_code"];
                itemCode010 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount010 != 0)
            {
                int codeNum = (int)mainCategoryComboBox010.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox010.DataSource = dt19;
                itemComboBox010.DisplayMember = "item_name";
                itemComboBox010.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode010 = (int)row["main_category_code"];
                itemCode010 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "12行目"
        private void mainCategoryComboBox011_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 12行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox011.SelectedValue;
                deliverydt211.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt211);
                itemComboBox011.DataSource = deliverydt211;
                itemComboBox011.DisplayMember = "item_name";
                itemComboBox011.ValueMember = "item_code";

                DataRow row = deliverydt211.Rows[0];
                mainCategoryCode011 = (int)row["main_category_code"];
                itemCode011 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount011 != 0)
            {
                int codeNum = (int)mainCategoryComboBox011.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox011.DataSource = dt19;
                itemComboBox011.DisplayMember = "item_name";
                itemComboBox011.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode011 = (int)row["main_category_code"];
                itemCode011 = (int)row["item_code"];

                conn.Close();
            }
        }
        #endregion
        #region "13行目"
        private void mainCategoryComboBox012_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 13行目
            if (b > 0)
            {
                int codeNum = (int)mainCategoryComboBox012.SelectedValue;
                deliverydt212.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt212);
                itemComboBox012.DataSource = deliverydt212;
                itemComboBox012.DisplayMember = "item_name";
                itemComboBox012.ValueMember = "item_code";

                DataRow row = deliverydt212.Rows[0];
                mainCategoryCode012 = (int)row["main_category_code"];
                itemCode012 = (int)row["item_code"];

                conn.Close();
            }
            else if (b > 0 && amount012 != 0)
            {
                int codeNum = (int)mainCategoryComboBox012.SelectedValue;
                dt19.Clear();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt19);
                itemComboBox012.DataSource = dt19;
                itemComboBox012.DisplayMember = "item_name";
                itemComboBox012.ValueMember = "item_code";

                DataRow row = dt19.Rows[0];
                mainCategoryCode012 = (int)row["main_category_code"];
                itemCode012 = (int)row["item_code"];

                conn.Close();
            }
            else if (data == "D")
            {

            }
            else
            {
                b++;
            }
        }
        #endregion
        #endregion

        #region "顧客選択メニュー（計算書）"
        private void client_Button_Click(object sender, EventArgs e)//顧客選択メニュー（計算書）
        {
            #region "コメントアウト"
            /*if (string.IsNullOrEmpty(sumTextBox.Text))
            {
                
            }
            else
            {
                if (!string.IsNullOrEmpty(moneyTextBox0.Text) || amount10 != 0)
                {
                    DialogResult result = MessageBox.Show("データを保持しますか?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        #region "データ一時置き場"
                        string DocumentNumber = documentNumberTextBox.Text;
                        decimal TotalWeight = decimal.Parse(this.totalWeight.Text);
                        decimal SubTotal = Math.Round(subSum, MidpointRounding.AwayFromZero);
                        total = Math.Round(subSum, MidpointRounding.AwayFromZero);
                        //string subTotal = this.subTotal.Text;
                        decimal TotalCount = decimal.Parse(this.totalCount.Text);
                        string TaxAmount = this.taxAmount.Text;
                        DateTime date = DateTime.Now;
                        string dat = date.ToString("yyyy/MM/dd HH:mm:ss");
                        document = DocumentNumber;
                        #region "1行目"
                        int record = 1;     //行数
                        int mainCategory = (int)mainCategoryComboBox0.SelectedValue;
                        int item = (int)itemComboBox0.SelectedValue;
                        string Detail = itemDetail0.Text;
                        decimal Weight = decimal.Parse(weightTextBox0.Text);
                        int Count = int.Parse(countTextBox0.Text);
                        decimal UnitPrice = decimal.Parse(unitPriceTextBox0.Text);
                        decimal amount = money0;
                        string Remarks = remarks0.Text;
                        amount10 = amount;
                        #endregion
                        DataTable dt2 = new DataTable();
                        string sql_str2 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "','" + TaxAmount + "','" + TotalCount + "');";

                        adapter = new NpgsqlDataAdapter(sql_str2, conn);
                        adapter.Fill(dt2);
                        #region "2行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox1.Text) && !(unitPriceTextBox1.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 2;
                            mainCategory = (int)mainCategoryComboBox1.SelectedValue;
                            item = (int)itemComboBox1.SelectedValue;
                            Detail = itemDetail1.Text;
                            Weight = decimal.Parse(weightTextBox1.Text);
                            Count = int.Parse(countTextBox1.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox1.Text);
                            amount = money1;
                            Remarks = remarks1.Text;
                            amount11 = amount;

                            string sql_str4 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str4, conn);
                            adapter.Fill(dt4);
                        }
                        #endregion
                        #region "3行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox2.Text) && !(unitPriceTextBox2.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 3;
                            mainCategory = (int)mainCategoryComboBox2.SelectedValue;
                            item = (int)itemComboBox2.SelectedValue;
                            Detail = itemDetail2.Text;
                            Weight = decimal.Parse(weightTextBox2.Text);
                            Count = int.Parse(countTextBox2.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox2.Text);
                            amount = money2;
                            Remarks = remarks2.Text;
                            amount12 = amount;

                            string sql_str5 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str5, conn);
                            adapter.Fill(dt5);
                        }
                        #endregion
                        #region "4行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox3.Text) && !(unitPriceTextBox3.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 4;
                            mainCategory = (int)mainCategoryComboBox3.SelectedValue;
                            item = (int)itemComboBox3.SelectedValue;
                            Detail = itemDetail3.Text;
                            Weight = decimal.Parse(weightTextBox3.Text);
                            Count = int.Parse(countTextBox3.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox3.Text);
                            amount = money3;
                            Remarks = remarks3.Text;
                            amount13 = amount;

                            string sql_str6 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "' );";

                            adapter = new NpgsqlDataAdapter(sql_str6, conn);
                            adapter.Fill(dt6);
                        }
                        #endregion
                        #region "5行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox4.Text) && !(unitPriceTextBox4.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 5;
                            mainCategory = (int)mainCategoryComboBox4.SelectedValue;
                            item = (int)itemComboBox4.SelectedValue;
                            Detail = itemDetail4.Text;
                            Weight = decimal.Parse(weightTextBox4.Text);
                            Count = int.Parse(countTextBox4.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox4.Text);
                            amount = money4;
                            Remarks = remarks4.Text;
                            amount14 = amount;

                            string sql_str7 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str7, conn);
                            adapter.Fill(dt7);
                        }
                        #endregion
                        #region "6行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox5.Text) && !(unitPriceTextBox5.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 6;
                            mainCategory = (int)mainCategoryComboBox5.SelectedValue;
                            item = (int)itemComboBox5.SelectedValue;
                            Detail = itemDetail5.Text;
                            Weight = decimal.Parse(weightTextBox5.Text);
                            Count = int.Parse(countTextBox5.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox5.Text);
                            amount = money5;
                            Remarks = remarks5.Text;
                            amount15 = amount;

                            string sql_str8 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "' );";

                            adapter = new NpgsqlDataAdapter(sql_str8, conn);
                            adapter.Fill(dt8);
                        }
                        #endregion
                        #region "7行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox6.Text) && !(unitPriceTextBox6.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 7;
                            mainCategory = (int)mainCategoryComboBox6.SelectedValue;
                            item = (int)itemComboBox6.SelectedValue;
                            Detail = itemDetail6.Text;
                            Weight = decimal.Parse(weightTextBox6.Text);
                            Count = int.Parse(countTextBox6.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox6.Text);
                            amount = money6;
                            Remarks = remarks6.Text;
                            amount16 = amount;

                            string sql_str9 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str9, conn);
                            adapter.Fill(dt9);
                        }
                        #endregion
                        #region "8行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox7.Text) && !(unitPriceTextBox7.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 8;
                            mainCategory = (int)mainCategoryComboBox7.SelectedValue;
                            item = (int)itemComboBox7.SelectedValue;
                            Detail = itemDetail7.Text;
                            Weight = decimal.Parse(weightTextBox7.Text);
                            Count = int.Parse(countTextBox7.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox7.Text);
                            amount = money7;
                            Remarks = remarks7.Text;
                            amount17 = amount;

                            string sql_str10 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str10, conn);
                            adapter.Fill(dt10);
                        }
                        #endregion
                        #region "9行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox8.Text) && !(unitPriceTextBox8.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 9;
                            mainCategory = (int)mainCategoryComboBox8.SelectedValue;
                            item = (int)itemComboBox8.SelectedValue;
                            Detail = itemDetail8.Text;
                            Weight = decimal.Parse(weightTextBox8.Text);
                            Count = int.Parse(countTextBox8.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox8.Text);
                            amount = money8;
                            Remarks = remarks8.Text;
                            amount18 = amount;

                            string sql_str11 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str11, conn);
                            adapter.Fill(dt11);
                        }
                        #endregion
                        #region "10行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox9.Text) && !(unitPriceTextBox9.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 10;
                            mainCategory = (int)mainCategoryComboBox9.SelectedValue;
                            item = (int)itemComboBox9.SelectedValue;
                            Detail = itemDetail9.Text;
                            Weight = decimal.Parse(weightTextBox9.Text);
                            Count = int.Parse(countTextBox9.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox9.Text);
                            amount = money9;
                            Remarks = remarks9.Text;
                            amount19 = amount;

                            string sql_str12 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "','" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str12, conn);
                            adapter.Fill(dt12);
                        }
                        #endregion
                        #region "11行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox10.Text) && !(unitPriceTextBox10.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 11;
                            mainCategory = (int)mainCategoryComboBox10.SelectedValue;
                            item = (int)itemComboBox10.SelectedValue;
                            Detail = itemDetail10.Text;
                            Weight = decimal.Parse(weightTextBox10.Text);
                            Count = int.Parse(countTextBox10.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox10.Text);
                            amount = money10;
                            Remarks = remarks10.Text;
                            amount110 = amount;

                            string sql_str13 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str13, conn);
                            adapter.Fill(dt13);
                        }
                        #endregion
                        #region "12行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox11.Text) && !(unitPriceTextBox11.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 12;
                            mainCategory = (int)mainCategoryComboBox11.SelectedValue;
                            item = (int)itemComboBox11.SelectedValue;
                            Detail = itemDetail11.Text;
                            Weight = decimal.Parse(weightTextBox11.Text);
                            Count = int.Parse(countTextBox11.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox11.Text);
                            amount = money11;
                            Remarks = remarks11.Text;
                            amount111 = amount;

                            string sql_str14 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str14, conn);
                            adapter.Fill(dt14);
                        }
                        #endregion
                        #region "13行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox12.Text) && !(unitPriceTextBox12.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 13;
                            mainCategory = (int)mainCategoryComboBox12.SelectedValue;
                            item = (int)itemComboBox12.SelectedValue;
                            Detail = itemDetail12.Text;
                            Weight = decimal.Parse(weightTextBox12.Text);
                            Count = int.Parse(countTextBox12.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox12.Text);
                            amount = money12;
                            Remarks = remarks12.Text;
                            amount112 = amount;

                            string sql_str15 = "Insert into statement_calc_data_if VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "','" + TotalWeight + "','" + SubTotal + "','" + total + "','" + dat + "' );";
                            adapter = new NpgsqlDataAdapter(sql_str15, conn);
                            adapter.Fill(dt15);
                        }
                        #endregion
                        #endregion
                    }
                    else
                    {
                        
                    }

                }
            }*/
            #endregion
            client_search search2 = new client_search(this, staff_id, type, client_staff_name, address, total, number, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);

            Properties.Settings.Default.Save();
            //this.Visible = true;
            search2.ShowDialog();
            #region "これから選択する場合"
            if (count != 0)
            {
                if (type == 0)
                {

                    //顧客情報 法人
                    #region "計算書"
                    DataTable clientDt = new DataTable();
                    string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    //int type = (int)row2["type"];

                    string companyNmae = row2["company_name"].ToString();
                    string shopName = row2["shop_name"].ToString();
                    string Staff_name = row2["staff_name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    this.client_Button.Text = "顧客変更";
                    typeTextBox.Text = "法人";                    //種別
                    companyTextBox.Text = companyNmae;              //会社名   
                    registerDateTextBox2.Text = antique_license;              //古物商許可証
                    shopNameTextBox.Text = shopName;                    //店舗名
                    clientNameTextBox.Text = Staff_name;                //担当名
                    registerDateTextBox.Text = register_date;           //登録日
                    clientRemarksTextBox.Text = remarks;                //備考
                    #endregion
                    #region "納品書"
                    this.client_searchButton1.Text = "顧客変更";
                    typeTextBox2.Text = "法人";                   //種別
                    companyTextBox2.Text = companyNmae;             //会社名
                    shopNameTextBox2.Text = shopName;               //店舗名
                    clientNameTextBox2.Text = Staff_name;           //担当名
                    registerDateTextBox2.Text = register_date;      //登録日
                    clientRemarksTextBox2.Text = remarks;           //備考
                    antiqueLicenceTextBox2.Text = antique_license;                //古物商許可証
                    #endregion

                }
                else if (type == 1)
                {
                    //顧客情報 個人
                    DataTable clientDt = new DataTable();
                    string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];

                    string name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string occupation = row2["occupation"].ToString();
                    string birthday = row2["birthday"].ToString();
                    string antique_license = row2["antique_license"].ToString();

                    #region "計算書"
                    label16.Text = "氏名";
                    label17.Text = "生年月日";
                    label18.Text = "職業";
                    typeTextBox.Text = "個人";
                    companyTextBox.Text = name;
                    shopNameTextBox.Text = birthday;
                    clientNameTextBox.Text = occupation;
                    registerDateTextBox.Text = register_date;
                    clientRemarksTextBox.Text = remarks;
                    registerDateTextBox2.Text = antique_license;
                    label38.Visible = false;
                    registerDateTextBox.Visible = false;
                    this.client_Button.Text = "顧客変更";
                    #endregion

                    #region "納品書"
                    typeTextBox2.Text = "個人";
                    label75.Text = "氏名";
                    label76.Text = "職業";
                    label77.Text = "生年月日";
                    clientNameTextBox2.Text = occupation;
                    companyTextBox2.Text = name;
                    shopNameTextBox2.Text = birthday;
                    registerDateTextBox2.Text = register_date;
                    clientRemarksTextBox2.Text = remarks;
                    antiqueLicenceTextBox2.Text = antique_license;
                    label36.Visible = false;
                    registerDateTextBox2.Visible = false;
                    this.client_searchButton1.Text = "顧客変更";
                    #endregion
                }
            }
            #endregion
            #region "１度選択して戻る場合"
            else if (count == 0 && (address != null && client_staff_name != null) && data == null)
            {
                if (type == 0)
                {
                    //顧客情報 法人
                    #region "計算書"
                    DataTable clientDt = new DataTable();
                    string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    //int type = (int)row2["type"];

                    string companyNmae = row2["company_name"].ToString();
                    string shopName = row2["shop_name"].ToString();
                    string Staff_name = row2["staff_name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    this.client_Button.Text = "顧客変更";
                    typeTextBox.Text = "法人";                    //種別
                    companyTextBox.Text = companyNmae;              //会社名   
                    registerDateTextBox2.Text = antique_license;              //古物商許可証
                    shopNameTextBox.Text = shopName;                    //店舗名
                    clientNameTextBox.Text = Staff_name;                //担当名
                    registerDateTextBox.Text = register_date;           //登録日
                    clientRemarksTextBox.Text = remarks;                //備考
                    #endregion
                    #region "納品書"
                    this.client_searchButton1.Text = "顧客変更";
                    typeTextBox2.Text = "法人";                     //種別
                    companyTextBox2.Text = companyNmae;             //会社名
                    shopNameTextBox2.Text = shopName;               //店舗名
                    clientNameTextBox2.Text = Staff_name;           //担当名
                    registerDateTextBox2.Text = register_date;      //登録日
                    clientRemarksTextBox2.Text = remarks;           //備考
                    antiqueLicenceTextBox2.Text = antique_license;                //古物商許可証
                    #endregion

                }
                else if (type == 1)
                {
                    //顧客情報 個人
                    DataTable clientDt = new DataTable();
                    string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];

                    string name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string occupation = row2["occupation"].ToString();
                    string birthday = row2["birthday"].ToString();
                    string antique_license = row2["antique_license"].ToString();

                    #region "計算書"
                    label16.Text = "氏名";
                    label17.Text = "生年月日";
                    label18.Text = "職業";
                    typeTextBox.Text = "個人";
                    companyTextBox.Text = name;
                    shopNameTextBox.Text = birthday;
                    clientNameTextBox.Text = occupation;
                    registerDateTextBox.Text = register_date;
                    clientRemarksTextBox.Text = remarks;
                    registerDateTextBox2.Text = antique_license;
                    label38.Visible = false;
                    registerDateTextBox.Visible = false;
                    this.client_Button.Text = "顧客変更";
                    #endregion

                    #region "納品書"
                    typeTextBox2.Text = "個人";
                    label75.Text = "氏名";
                    label76.Text = "職業";
                    label77.Text = "生年月日";
                    clientNameTextBox2.Text = occupation;
                    companyTextBox2.Text = name;
                    shopNameTextBox2.Text = birthday;
                    registerDateTextBox2.Text = register_date;
                    clientRemarksTextBox2.Text = remarks;
                    antiqueLicenceTextBox2.Text = antique_license;
                    label36.Visible = false;
                    registerDateTextBox2.Visible = false;
                    this.client_searchButton1.Text = "顧客変更";
                    #endregion
                }
            }
            #endregion

            //Properties.Settings.Default.Upgrade();

            //this.Show();             
        }
        #endregion
        #region "顧客選択メニュー（納品書）"
        private void clientSelectButton_Click(object sender, EventArgs e)//顧客選択メニュー（納品書）
        {
            if (string.IsNullOrEmpty(sumTextBox2.Text))
            {

            }
            using (client_search search2 = new client_search(this, staff_id, type, client_staff_name, address, total, number, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass))
            {
                this.Hide();
                search2.ShowDialog();
            }
        }
        #endregion

        #region"計算書　登録ボタン"
        private void AddButton_Click(object sender, EventArgs e)        //計算書用登録ボタン
        {
            #region "ありうるミス"
            if (paymentMethodsComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("決済方法を選んで下さい。");
                return;
            }
            if (deliveryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("受渡方法を選んで下さい。");
                return;
            }
            #endregion
            DialogResult dr = MessageBox.Show("登録しますか？", "登録確認", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            if (dr == DialogResult.Yes && subSum >= 2000000)
            {
                MessageBox.Show("200万以上の取引です。必要書類に不備がないか確認してください。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                DialogResult dialogResult = MessageBox.Show("入力不備がありましたか？", "入力確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    return;
                }
            }

            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            string DocumentNumber = documentNumberTextBox.Text;
            decimal TotalWeight = weisum;
            int Amount = countsum;
            decimal SubTotal = Math.Round(subSum, MidpointRounding.AwayFromZero);
            decimal Total = Math.Round(subSum, MidpointRounding.AwayFromZero);
            string SettlementDate = settlementBox.Text;
            string DeliveryDate = deliveryDateBox.Text;
            string DeliveryMethod = deliveryComboBox.Text;
            string PaymentMethod = paymentMethodsComboBox.Text;
            string Reason = this.textBox1.Text;
            
            int TYPE = 0;
            AntiqueNumber = 0;
            ID_Number = 0;
            string CompanyName = "";
            string ShopName = "";
            string StaffName = "";
            string Name = "";
            string Birthday = "";
            string TEL = "";
            string Work = "";

            if (typeTextBox.Text == "法人")
            {
                TYPE = 0;
                CompanyName = companyTextBox.Text;
                ShopName = shopNameTextBox.Text;
                StaffName = client_staff_name;

            }
            else if (typeTextBox.Text == "個人")
            {
                TYPE = 1;
                Name = companyTextBox.Text;
                Birthday = shopNameTextBox.Text;
                Work = clientNameTextBox.Text;
            }

            //古物番号、身分証番号、TEL の紐付け
            string NUMBER = "";

            #region"古物番号、身分証番号取得"
            using (transaction = conn.BeginTransaction())     
            {
                if (!string.IsNullOrEmpty(typeTextBox.Text)) {
                    if (typeTextBox.Text == "法人")
                    {
                        NUMBER = @"select antique_number, phone_number from client_m_corporate where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and staff_name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
                        cmd = new NpgsqlCommand(NUMBER, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AntiqueNumber = (int)reader["antique_number"];
                                TEL = reader["phone_number"].ToString();
                            }
                        }
                    }
                    else if (typeTextBox.Text == "個人")
                    {
                        NUMBER = @"select id_number, phone_number from client_m_individual where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
                        cmd = new NpgsqlCommand(NUMBER, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ID_Number = (int)reader["id_number"];
                                TEL = reader["phone_number"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("顧客選択をしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            #endregion

            DataTable dt = new DataTable();
            DataTable revisiondt = new DataTable();
            #region"200万以上"
            if (subSum >= 2000000)
            {
                if (!string.IsNullOrEmpty(articlesTextBox.Text))
                {
                    AolFinancialShareholder = articlesTextBox.Text;

                    using (transaction = conn.BeginTransaction())
                    {
                        if (typeTextBox.Text == "法人")
                        {
                            string SQL_STR = @"update client_m_corporate set aol_financial_shareholder='" + AolFinancialShareholder + "' where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and staff_name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteReader();

                        }
                        else if (typeTextBox.Text == "個人")
                        {
                            string SQL_STR = @"update client_m_individual set aol_financial_shareholder='" + AolFinancialShareholder + "' where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteReader();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(taxCertificateTextBox.Text))
                {
                    TaxCertification = taxCertificateTextBox.Text;
                    using (transaction = conn.BeginTransaction())
                    {
                        if (typeTextBox.Text == "法人")
                        {
                            string SQL_STR = @"update client_m_corporate set tax_certificate='" + TaxCertification + "' where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and staff_name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteReader();
                        }
                        else if (typeTextBox.Text == "個人")
                        {
                            string SQL_STR = @"update client_m_individual set tax_certificate='" + TaxCertification + "'  where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteReader();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(sealCertificationTextBox.Text))
                {
                    SealCertification = sealCertificationTextBox.Text;
                    using (transaction = conn.BeginTransaction())
                    {
                        if (typeTextBox.Text == "法人")
                        {
                            string SQL_STR = @"update client_m_corporate set seal_certification='" + SealCertification + "' where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and staff_name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteReader();
                        }
                        else if (typeTextBox.Text == "個人")
                        {
                            string SQL_STR = @"update client_m_individual set seal_certification='" + SealCertification + "' where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteReader();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(residenceCardTextBox.Text))
                {
                    ResidenceCard = residenceCardTextBox.Text;
                    ResidencePeriod = residencePerioddatetimepicker.Value.ToShortDateString();
                    using (transaction = conn.BeginTransaction())
                    {
                        if (typeTextBox.Text == "法人")
                        {
                            string SQL_STR = @"update client_m_corporate set (residence_card, period_stay) =('" + ResidenceCard + "','" + ResidencePeriod + "') where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and staff_name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteReader();
                        }
                        else if (typeTextBox.Text == "個人")
                        {
                            string SQL_STR = @"update client_m_individual set (residence_card, period_stay) =('" + ResidenceCard + "','" + ResidencePeriod + "')  where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteReader();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(antiqueLicenceTextBox.Text))
                {
                    AntiqueLicence = antiqueLicenceTextBox.Text;

                    using (transaction = conn.BeginTransaction())
                    {
                        if (typeTextBox.Text == "法人")
                        {
                            string SQL_STR = @"update client_m_corporate set antique_license ='" + AntiqueLicence + "' where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and staff_name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteReader();
                        }
                        else if (typeTextBox.Text == "個人")
                        {
                            string SQL_STR = @"update client_m_individual set antique_license ='" + AntiqueLicence + "'  where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteReader();
                        }
                    }
                }

            }
            #endregion
            #region "再登録"
            if (regist1 >= 1 || data == "S")
            {                
                NpgsqlDataAdapter adapterStatement;
                DataTable StatementDt = new DataTable();
                string str_sql_re = "select * from statement_calc_data where document_number = '" + DocumentNumber + "';";
                adapterStatement = new NpgsqlDataAdapter(str_sql_re, conn);
                adapterStatement.Fill(StatementDt);
                Re = StatementDt.Rows.Count;
            }
            else
            {
                Re = 0;
            }

            if (regist1 != 0 && string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("理由を記入して下さい。");
                return;
            }
            else { }
            #endregion
            if (Re == 0)
            {
                DateTime dat1 = DateTime.Now;
                DateTime dtToday = dat1.Date;
                string c = dtToday.ToString("yyyy年MM月dd日");
                sql_str = "Insert into statement_data (antique_number, id_number, staff_code, total_weight, total_amount, sub_total, tax_amount, total, delivery_method, payment_method, settlement_date, delivery_date, document_number, company_name, shop_name, staff_name, name, type, birthday, occupation, address, assessment_date) VALUES ('" + AntiqueNumber + "','" + ID_Number + "' , '" + staff_id + "' , '" + TotalWeight + "' ,  '" + Amount + "' , '" + SubTotal + "', '" + TaxAmount + "' , '" + Total + "' , '" + DeliveryMethod + "' , '" + PaymentMethod + "' , '" + SettlementDate + "' , '" + DeliveryDate + "', '" + DocumentNumber + "','" + CompanyName + "','" + ShopName + "','" + StaffName + "','" + Name + "','" + TYPE + "','" + Birthday + "','" + Work + "', '" + address + "','" + c + "');";
                sql_str3 = "Insert into statement_data_revisions (document_number, antique_number, id_number, staff_code, total_weight, total_amount, sub_total, tax_amount, total, settlement_date, delivery_date, delivery_method, payment_method, registration_date, insert_name ) VALUES ('" + DocumentNumber +  "','" + AntiqueNumber + "','" + ID_Number + "' , '" + staff_id + "' , '" + TotalWeight + "' ,  '" + Amount + "' , '" + SubTotal + "', '" + TaxAmount + "' , '" + Total + "' , '" + SettlementDate + "','" + DeliveryDate  +  "','" + DeliveryMethod + "' , '" + PaymentMethod + "' , '"  + c + "' , "  +  staff_id +  ");";
            }
            else
            {
                DateTime dat1 = DateTime.Now;
                DateTime dtToday = dat1.Date;
                string c = dtToday.ToString("yyyy年MM月dd日");
                sql_str = "UPDATE statement_data SET antique_number = " + AntiqueNumber + ", id_number = "+ ID_Number + ", staff_code = " + staff_id + ", total_weight = " + TotalWeight + " , total_amount = " + Amount + ", sub_total = " + SubTotal + ", tax_amount = " + TaxAmount + ", total = " + Total + ", delivery_method = '" + DeliveryMethod + "', payment_method = '" + PaymentMethod + "', settlement_date = '" + SettlementDate + "', delivery_date = '" + DeliveryDate + "',  company_name = '" + CompanyName + "', shop_name = '" + ShopName + "', staff_name = '" + StaffName + "', name = '" + Name  + "', type = " + TYPE + ", birthday = '" + Birthday + "', occupation = '" + Work  + "', address = '" + address + "', assessment_date = '" + c + "' , reason = '" + Reason + "' Where document_number = '" + DocumentNumber + "';";
            }
            

            conn.Close();
            #region "1行目" 
            int record = 1;     //行数
            //int mainCategory = mainCategoryCode0;
            //int item = itemCode0;
           #region "大分類コード"
           DataTable maindt = new DataTable();
           conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
           string sql_main = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox0.Text + "';";
           adapter = new NpgsqlDataAdapter(sql_main, conn);
           adapter.Fill(maindt);
           DataRow dataRow;
           dataRow = maindt.Rows[0];
           int mainCategory = (int)dataRow["main_category_code"];
           #endregion
           #region "品名コード"
           DataTable itemdt = new DataTable();
           conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
           string sql_item = "select * from item_m where item_name = '" + itemComboBox0.Text + "';";
           adapter = new NpgsqlDataAdapter(sql_item, conn);
           adapter.Fill(itemdt);
           DataRow dataRow1;
           dataRow1 = itemdt.Rows[0];
           int item = (int)dataRow1["item_code"];
           #endregion
            
            string Detail = itemDetail0.Text;
            decimal Weight = decimal.Parse(weightTextBox0.Text);
            int Count = int.Parse(countTextBox0.Text);
            decimal UnitPrice = decimal.Parse(unitPriceTextBox0.Text);
            decimal amount = money0;
            string Remarks = remarks0.Text;
            
            

            DataTable dt2 = new DataTable();
            DataTable revisiondt2 = new DataTable();
            if (regist1 >= 1 || data == "S")
            {
                sql_str2 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "', reason = '" + Reason + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";
            }
            else
            {
                DateTime dat1 = DateTime.Now;
                DateTime dtToday = dat1.Date;
                string c = dtToday.ToString("yyyy年MM月dd日");
                sql_str2 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail )VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "');";
                sql_str4 = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "',"  + staff_id + ", '" + c + "','" + Detail + "');";
            }
            

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            adapter = new NpgsqlDataAdapter(sql_str2, conn);
            adapter.Fill(dt2);
            if (Re == 0)
            {
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(revisiondt);

                adapter = new NpgsqlDataAdapter(sql_str4, conn);
                adapter.Fill(revisiondt2);
            }
            else { }
            
            #endregion
            #region "2行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox1.Text) && !(unitPriceTextBox1.Text == "単価 -> 重量 or 数量"))
            {
                if (Re >= 2)
                {
                    record = 2;
                    mainCategory = mainCategoryCode1;
                    item = itemCode1;
                    Detail = itemDetail1.Text;
                    Weight = decimal.Parse(weightTextBox1.Text);
                    Count = int.Parse(countTextBox1.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox1.Text);
                    amount = money1;
                    Remarks = remarks1.Text;

                    string sql_str4 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

                    adapter = new NpgsqlDataAdapter(sql_str4, conn);
                    adapter.Fill(dt4);
                }
                else
                {
                    record = 2;
                    mainCategory = mainCategoryCode1;
                    item = itemCode1;
                    Detail = itemDetail1.Text;
                    Weight = decimal.Parse(weightTextBox1.Text);
                    Count = int.Parse(countTextBox1.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox1.Text);
                    amount = money1;
                    Remarks = remarks1.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str4 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                    adapter = new NpgsqlDataAdapter(sql_str4, conn);
                    adapter.Fill(dt4);

                    if (Re == 0)
                    {
                        string sql_str4_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str4_re, conn);
                        adapter.Fill(redt4);
                    }
                    else { }
                }
                
            }
            #endregion
            #region "3行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox2.Text) && !(unitPriceTextBox2.Text == "単価 -> 重量 or 数量"))
            {
                if (Re >= 3)
                {
                    record = 3;
                    mainCategory = mainCategoryCode2;
                    item = itemCode2;
                    Detail = itemDetail2.Text;
                    Weight = decimal.Parse(weightTextBox2.Text);
                    Count = int.Parse(countTextBox2.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox2.Text);
                    amount = money2;
                    Remarks = remarks2.Text;

                    string sql_str5 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

                    adapter = new NpgsqlDataAdapter(sql_str5, conn);
                    adapter.Fill(dt5);
                }
                else
                {
                    record = 3;
                    mainCategory = mainCategoryCode2;
                    item = itemCode2;
                    Detail = itemDetail2.Text;
                    Weight = decimal.Parse(weightTextBox2.Text);
                    Count = int.Parse(countTextBox2.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox2.Text);
                    amount = money2;
                    Remarks = remarks2.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str5 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                    adapter = new NpgsqlDataAdapter(sql_str5, conn);
                    adapter.Fill(dt5);

                    if (Re == 0)
                    {
                        string sql_str5_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str5_re, conn);
                        adapter.Fill(redt5);
                    }
                    else { }
                }
                
            }
            #endregion
            #region "4行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox3.Text) && !(unitPriceTextBox3.Text == "単価 -> 重量 or 数量"))
            {
                if (Re >= 4)
                {
                    record = 4;
                    mainCategory = mainCategoryCode3;
                    item = itemCode3;
                    Detail = itemDetail3.Text;
                    Weight = decimal.Parse(weightTextBox3.Text);
                    Count = int.Parse(countTextBox3.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox3.Text);
                    amount = money3;
                    Remarks = remarks3.Text;

                    string sql_str6 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

                    adapter = new NpgsqlDataAdapter(sql_str6, conn);
                    adapter.Fill(dt6);
                }
                else
                {
                    record = 4;
                    mainCategory = mainCategoryCode3;
                    item = itemCode3;
                    Detail = itemDetail3.Text;
                    Weight = decimal.Parse(weightTextBox3.Text);
                    Count = int.Parse(countTextBox3.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox3.Text);
                    amount = money3;
                    Remarks = remarks3.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str6 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                    adapter = new NpgsqlDataAdapter(sql_str6, conn);
                    adapter.Fill(dt6);

                    if (Re == 0)
                    {
                        string sql_str6_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str6_re, conn);
                        adapter.Fill(redt6);
                    }
                    else { }                    
                }
                
            }
            #endregion
            #region "5行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox4.Text) && !(unitPriceTextBox4.Text == "単価 -> 重量 or 数量"))
            {
                if (Re != 0 && Re >= 5)
                {
                    record = 5;
                    mainCategory = mainCategoryCode4;
                    item = itemCode4;
                    Detail = itemDetail4.Text;
                    Weight = decimal.Parse(weightTextBox4.Text);
                    Count = int.Parse(countTextBox4.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox4.Text);
                    amount = money4;
                    Remarks = remarks4.Text;

                    string sql_str7 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

                    adapter = new NpgsqlDataAdapter(sql_str7, conn);
                    adapter.Fill(dt7);
                }
                else
                {
                    record = 5;
                    mainCategory = mainCategoryCode4;
                    item = itemCode4;
                    Detail = itemDetail4.Text;
                    Weight = decimal.Parse(weightTextBox4.Text);
                    Count = int.Parse(countTextBox4.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox4.Text);
                    amount = money4;
                    Remarks = remarks4.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str7 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                    adapter = new NpgsqlDataAdapter(sql_str7, conn);
                    adapter.Fill(dt7);

                    if (Re == 0)
                    {
                        string sql_str7_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str7_re, conn);
                        adapter.Fill(redt7);
                    }
                    else { }                   
                }
                
            }
            #endregion
            #region "6行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox5.Text) && !(unitPriceTextBox5.Text == "単価 -> 重量 or 数量"))
            {
                if (Re >= 6)
                {
                    record = 6;
                    mainCategory = mainCategoryCode5;
                    item = itemCode5;
                    Detail = itemDetail5.Text;
                    Weight = decimal.Parse(weightTextBox5.Text);
                    Count = int.Parse(countTextBox5.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox5.Text);
                    amount = money5;
                    Remarks = remarks5.Text;

                    string sql_str8 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

                    adapter = new NpgsqlDataAdapter(sql_str8, conn);
                    adapter.Fill(dt8);
                }
                else
                {
                    record = 6;
                    mainCategory = mainCategoryCode5;
                    item = itemCode5;
                    Detail = itemDetail5.Text;
                    Weight = decimal.Parse(weightTextBox5.Text);
                    Count = int.Parse(countTextBox5.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox5.Text);
                    amount = money5;
                    Remarks = remarks5.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str8 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                    adapter = new NpgsqlDataAdapter(sql_str8, conn);
                    adapter.Fill(dt8);

                    if (Re == 0)
                    {
                        string sql_str8_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str8_re, conn);
                        adapter.Fill(redt8);
                    }
                    else { }                    
                }                
            }
            #endregion
            #region "7行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox6.Text) && !(unitPriceTextBox6.Text == "単価 -> 重量 or 数量"))
            {
                if (Re >= 7)
                {
                    record = 7;
                    mainCategory = mainCategoryCode6;
                    item = itemCode6;
                    Detail = itemDetail6.Text;
                    Weight = decimal.Parse(weightTextBox6.Text);
                    Count = int.Parse(countTextBox6.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox6.Text);
                    amount = money6;
                    Remarks = remarks6.Text;

                    string sql_str9 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

                    adapter = new NpgsqlDataAdapter(sql_str9, conn);
                    adapter.Fill(dt9);
                }
                else
                {
                    record = 7;
                    mainCategory = mainCategoryCode6;
                    item = itemCode6;
                    Detail = itemDetail6.Text;
                    Weight = decimal.Parse(weightTextBox6.Text);
                    Count = int.Parse(countTextBox6.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox6.Text);
                    amount = money6;
                    Remarks = remarks6.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str9 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                    adapter = new NpgsqlDataAdapter(sql_str9, conn);
                    adapter.Fill(dt9);

                    if (Re == 0)
                    {
                        string sql_str9_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str9_re, conn);
                        adapter.Fill(redt9);
                    }
                    else { }                    
                }
                
            }
            #endregion
            #region "8行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox7.Text) && !(unitPriceTextBox7.Text == "単価 -> 重量 or 数量"))
            {
                if (Re >= 8)
                {
                    record = 8;
                    mainCategory = mainCategoryCode7;
                    item = itemCode7;
                    Detail = itemDetail7.Text;
                    Weight = decimal.Parse(weightTextBox7.Text);
                    Count = int.Parse(countTextBox7.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox7.Text);
                    amount = money7;
                    Remarks = remarks7.Text;

                    string sql_str10 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

                    adapter = new NpgsqlDataAdapter(sql_str10, conn);
                    adapter.Fill(dt10);
                }
                else
                {
                    record = 8;
                    mainCategory = mainCategoryCode7;
                    item = itemCode7;
                    Detail = itemDetail7.Text;
                    Weight = decimal.Parse(weightTextBox7.Text);
                    Count = int.Parse(countTextBox7.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox7.Text);
                    amount = money7;
                    Remarks = remarks7.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str10 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                    adapter = new NpgsqlDataAdapter(sql_str10, conn);
                    adapter.Fill(dt10);

                    if (Re == 0)
                    {
                        string sql_str10_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str10_re, conn);
                        adapter.Fill(redt10);
                    }
                    else { }                    
                }
                
            }
            #endregion
            #region "9行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox8.Text) && !(unitPriceTextBox8.Text == "単価 -> 重量 or 数量"))
            {
                if (Re >= 9)
                {
                    record = 9;
                    mainCategory = mainCategoryCode8;
                    item = itemCode8;
                    Detail = itemDetail8.Text;
                    Weight = decimal.Parse(weightTextBox8.Text);
                    Count = int.Parse(countTextBox8.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox8.Text);
                    amount = money8;
                    Remarks = remarks8.Text;

                    string sql_str11 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

                    adapter = new NpgsqlDataAdapter(sql_str11, conn);
                    adapter.Fill(dt11);
                }
                else
                {
                    record = 9;
                    mainCategory = mainCategoryCode8;
                    item = itemCode8;
                    Detail = itemDetail8.Text;
                    Weight = decimal.Parse(weightTextBox8.Text);
                    Count = int.Parse(countTextBox8.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox8.Text);
                    amount = money8;
                    Remarks = remarks8.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str11 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                    adapter = new NpgsqlDataAdapter(sql_str11, conn);
                    adapter.Fill(dt11);

                    if (Re == 0)
                    {
                        string sql_str11_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str11_re, conn);
                        adapter.Fill(redt11);
                    }
                    else { }
                    
                }               
            }
            #endregion
            #region "10行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox9.Text) && !(unitPriceTextBox9.Text == "単価 -> 重量 or 数量"))
            {
                if (Re >= 10)
                {
                    record = 10;
                    mainCategory = mainCategoryCode9;
                    item = itemCode9;
                    Detail = itemDetail9.Text;
                    Weight = decimal.Parse(weightTextBox9.Text);
                    Count = int.Parse(countTextBox9.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox9.Text);
                    amount = money9;
                    Remarks = remarks9.Text;

                    string sql_str12 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

                    adapter = new NpgsqlDataAdapter(sql_str12, conn);
                    adapter.Fill(dt12);
                }
                else
                {
                    record = 10;
                    mainCategory = mainCategoryCode9;
                    item = itemCode9;
                    Detail = itemDetail9.Text;
                    Weight = decimal.Parse(weightTextBox9.Text);
                    Count = int.Parse(countTextBox9.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox9.Text);
                    amount = money9;
                    Remarks = remarks9.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str12 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "','" + Detail + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                    adapter = new NpgsqlDataAdapter(sql_str12, conn);
                    adapter.Fill(dt12);

                    if (Re == 0)
                    {
                        string sql_str12_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str12_re, conn);
                        adapter.Fill(redt12);
                    }
                    else { }                    
                }                
            }
            #endregion
            #region "11行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox10.Text) && !(unitPriceTextBox10.Text == "単価 -> 重量 or 数量"))
            {
                if (Re >= 11)
                {
                    record = 11;
                    mainCategory = mainCategoryCode10;
                    item = itemCode10;
                    Detail = itemDetail10.Text;
                    Weight = decimal.Parse(weightTextBox10.Text);
                    Count = int.Parse(countTextBox10.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox10.Text);
                    amount = money10;
                    Remarks = remarks10.Text;

                    string sql_str13 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

                    adapter = new NpgsqlDataAdapter(sql_str13, conn);
                    adapter.Fill(dt13);
                }
                else
                {
                    record = 11;
                    mainCategory = mainCategoryCode10;
                    item = itemCode10;
                    Detail = itemDetail10.Text;
                    Weight = decimal.Parse(weightTextBox10.Text);
                    Count = int.Parse(countTextBox10.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox10.Text);
                    amount = money10;
                    Remarks = remarks10.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str13 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                    adapter = new NpgsqlDataAdapter(sql_str13, conn);
                    adapter.Fill(dt13);

                    if (Re == 0)
                    {
                        string sql_str13_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str13_re, conn);
                        adapter.Fill(redt13);
                    }
                    else { }                    
                }                
            }
            #endregion
            #region "12行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox11.Text) && !(unitPriceTextBox11.Text == "単価 -> 重量 or 数量"))
            {
                if (Re >= 12)
                {
                    record = 12;
                    mainCategory = mainCategoryCode11;
                    item = itemCode11;
                    Detail = itemDetail11.Text;
                    Weight = decimal.Parse(weightTextBox11.Text);
                    Count = int.Parse(countTextBox11.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox11.Text);
                    amount = money11;
                    Remarks = remarks11.Text;

                    string sql_str14 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

                    adapter = new NpgsqlDataAdapter(sql_str14, conn);
                    adapter.Fill(dt14);
                }
                else
                {
                    record = 12;
                    mainCategory = mainCategoryCode11;
                    item = itemCode11;
                    Detail = itemDetail11.Text;
                    Weight = decimal.Parse(weightTextBox11.Text);
                    Count = int.Parse(countTextBox11.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox11.Text);
                    amount = money11;
                    Remarks = remarks11.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str14 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                    adapter = new NpgsqlDataAdapter(sql_str14, conn);
                    adapter.Fill(dt14);

                    if (Re == 0)
                    {
                        string sql_str14_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str14_re, conn);
                        adapter.Fill(redt14);
                    }
                    else { }                    
                }                
            }
            #endregion
            #region "13行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox12.Text) && !(unitPriceTextBox12.Text == "単価 -> 重量 or 数量"))
            {
                if (Re >= 13)
                {
                    record = 13;
                    mainCategory = mainCategoryCode12;
                    item = itemCode12;
                    Detail = itemDetail12.Text;
                    Weight = decimal.Parse(weightTextBox12.Text);
                    Count = int.Parse(countTextBox12.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox12.Text);
                    amount = money12;
                    Remarks = remarks12.Text;

                    string sql_str15 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";
                    adapter = new NpgsqlDataAdapter(sql_str15, conn);
                    adapter.Fill(dt15);
                }
                else
                {
                    record = 13;
                    mainCategory = mainCategoryCode12;
                    item = itemCode12;
                    Detail = itemDetail12.Text;
                    Weight = decimal.Parse(weightTextBox12.Text);
                    Count = int.Parse(countTextBox12.Text);
                    UnitPrice = decimal.Parse(unitPriceTextBox12.Text);
                    amount = money12;
                    Remarks = remarks12.Text;
                    DateTime dat1 = DateTime.Now;
                    DateTime dtToday = dat1.Date;
                    string c = dtToday.ToString("yyyy年MM月dd日");

                    string sql_str15 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";
                    adapter = new NpgsqlDataAdapter(sql_str15, conn);
                    adapter.Fill(dt15);

                    if (Re == 0)
                    {
                        string sql_str15_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
                        adapter = new NpgsqlDataAdapter(sql_str15_re, conn);
                        adapter.Fill(redt15);
                    }
                    else { }
                }                
            }
            #endregion
            conn.Close();
            MessageBox.Show("登録しました。");
            this.previewButton.Enabled = true;
            this.RecordListButton.Enabled = true;
            this.label9.Visible = true;
            this.textBox1.Visible = true;
            if (access_auth == "C")
            {
                this.addButton.Enabled = false;
                this.label9.Visible = false;
                this.textBox1.Visible = false;
            }
            regist1++;
        }
        #endregion

        #region"計算書・納品書　計算処理"

        #region　"計算書　単価を入力したら重量、数値入力可"
        #region "1行目"
        private void unitPriceTextBox0_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox0.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox0.Text))
            {
                weightTextBox0.ReadOnly = true;
                countTextBox0.ReadOnly = true;
                unitPriceTextBox1.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox0.Text) && !(unitPriceTextBox0.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox0.ReadOnly = false;
                countTextBox0.ReadOnly = false;
            }
        }
        #endregion
        #region "2行目"
        private void unitPriceTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox1.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox1.Text))
            {
                weightTextBox1.ReadOnly = true;
                countTextBox1.ReadOnly = true;
                unitPriceTextBox2.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox1.Text) && !(unitPriceTextBox1.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox1.ReadOnly = false;
                countTextBox1.ReadOnly = false;
            }
        }
        #endregion
        #region "3行目"
        private void unitPriceTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox2.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox2.Text))
            {
                weightTextBox2.ReadOnly = true;
                countTextBox2.ReadOnly = true;
                unitPriceTextBox3.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox2.Text) && !(unitPriceTextBox2.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox2.ReadOnly = false;
                countTextBox2.ReadOnly = false;
            }
        }
        #endregion
        #region "4行目"
        private void unitPriceTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox3.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox3.Text))
            {
                weightTextBox3.ReadOnly = true;
                countTextBox3.ReadOnly = true;
                unitPriceTextBox4.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox3.Text) && !(unitPriceTextBox3.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox3.ReadOnly = false;
                countTextBox3.ReadOnly = false;
            }
        }
        #endregion
        #region "5行目"
        private void unitPriceTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox4.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox4.Text))
            {
                weightTextBox4.ReadOnly = true;
                countTextBox4.ReadOnly = true;
                unitPriceTextBox5.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox4.Text) && !(unitPriceTextBox4.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox4.ReadOnly = false;
                countTextBox4.ReadOnly = false;
            }
        }
        #endregion
        #region "6行目"
        private void unitPriceTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox5.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox5.Text))
            {
                weightTextBox5.ReadOnly = true;
                countTextBox5.ReadOnly = true;
                unitPriceTextBox6.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox5.Text) && !(unitPriceTextBox5.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox5.ReadOnly = false;
                countTextBox5.ReadOnly = false;
            }
        }
        #endregion
        #region "7行目"
        private void unitPriceTextBox6_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox6.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox6.Text))
            {
                weightTextBox6.ReadOnly = true;
                countTextBox6.ReadOnly = true;
                unitPriceTextBox7.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox6.Text) && !(unitPriceTextBox6.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox6.ReadOnly = false;
                countTextBox6.ReadOnly = false;
            }
        }
        #endregion
        #region "8行目"
        private void unitPriceTextBox7_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox7.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            if (string.IsNullOrEmpty(unitPriceTextBox7.Text))
            {
                weightTextBox7.ReadOnly = true;
                countTextBox7.ReadOnly = true;
                unitPriceTextBox8.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox7.Text) && !(unitPriceTextBox7.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox7.ReadOnly = false;
                countTextBox7.ReadOnly = false;
            }
        }
        #endregion
        #region "9行目"
        private void unitPriceTextBox8_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox8.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox8.Text))
            {
                weightTextBox8.ReadOnly = true;
                countTextBox8.ReadOnly = true;
                unitPriceTextBox9.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox8.Text) && !(unitPriceTextBox8.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox8.ReadOnly = false;
                countTextBox8.ReadOnly = false;
            }
        }
        #endregion
        #region "10行目"
        private void unitPriceTextBox9_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox9.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox9.Text))
            {
                weightTextBox9.ReadOnly = true;
                countTextBox9.ReadOnly = true;
                unitPriceTextBox10.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox9.Text) && !(unitPriceTextBox9.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox9.ReadOnly = false;
                countTextBox9.ReadOnly = false;
            }
        }
        #endregion
        #region "11行目"
        private void unitPriceTextBox10_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox10.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox10.Text))
            {
                weightTextBox10.ReadOnly = true;
                countTextBox10.ReadOnly = true;
                unitPriceTextBox11.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox10.Text) && !(unitPriceTextBox10.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox10.ReadOnly = false;
                countTextBox10.ReadOnly = false;
            }
        }
        #endregion
        #region "12行目"
        private void unitPriceTextBox11_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox11.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox11.Text))
            {
                weightTextBox11.ReadOnly = true;
                countTextBox11.ReadOnly = true;
                unitPriceTextBox12.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox11.Text) && !(unitPriceTextBox11.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox11.ReadOnly = false;
                countTextBox11.ReadOnly = false;
            }
        }
        #endregion
        #region "13行目"
        private void unitPriceTextBox12_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox11.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox12.Text))
            {
                weightTextBox12.ReadOnly = true;
                countTextBox12.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox12.Text) && !(unitPriceTextBox12.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox12.ReadOnly = false;
                countTextBox12.ReadOnly = false;
            }
        }
        #endregion
        #endregion

        #region "計算書　フォーカス時初期状態ならnull"
        #region "1行目"
        private void unitPriceTextBox0_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(typeTextBox.Text))
                {
                    MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    unitPriceTextBox0.Text = "";
                    unitPriceTextBox0.ReadOnly = true;
                    return;
                }
                if (unitPriceTextBox0.Text.ToString() == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox0.Text = "";
                }
            }
            
            
        }
        #endregion
        #region "2行目"
        private void unitPriceTextBox1_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox0.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox0.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox1.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox1.Text = "";
                }
            }
            
        }
        #endregion
        #region "3行目"
        private void unitPriceTextBox2_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox1.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox1.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox2.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox2.Text = "";
                }
            }
        }
        #endregion
        #region "4行目"
        private void unitPriceTextBox3_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox2.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox2.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox3.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox3.Text = "";
                }
            }
        }
        #endregion
        #region "5行目"
        private void unitPriceTextBox4_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox3.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox3.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox4.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox4.Text = "";
                }
            }
        }
        #endregion
        #region "6行目"
        private void unitPriceTextBox5_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox4.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox4.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox5.Text.ToString() == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox5.Text = "";
                }
            }
        }
        #endregion
        #region "7行目"
        private void unitPriceTextBox6_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox5.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox5.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox6.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox6.Text = "";
                }
            }
        }
        #endregion
        #region "8行目"
        private void unitPriceTextBox7_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox6.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox6.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox7.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox7.Text = "";
                }
            }
        }
        #endregion
        #region "9行目"
        private void unitPriceTextBox8_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox7.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox7.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox8.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox8.Text = "";
                }
            }
        }
        #endregion
        #region "10行目"
        private void unitPriceTextBox9_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox8.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox8.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox9.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox9.Text = "";
                }
            }
        }
        #endregion
        #region "11行目"
        private void unitPriceTextBox10_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox9.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox9.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox10.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox10.Text = "";
                }
            }
        }
        #endregion
        #region "12行目"
        private void unitPriceTextBox11_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox10.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox10.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox11.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox11.Text = "";
                }
            }
        }
        #endregion
        #region "13行目"
        private void unitPriceTextBox12_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox10.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox10.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox12.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox12.Text = "";
                }
            }
            
        }
        #endregion
        #endregion

        #region"計算書　単価３桁区切り＋フォーカスが外れた時"
        #region "1行目"
        private void unitPriceTextBox0_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox1.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox0.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox0.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox0.Text))
                {
                    unitPriceTextBox0.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox0.Text)))
                {
                    unitPriceTextBox1.ReadOnly = false;
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox1.Text))
                {
                    unitPriceTextBox0.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox1.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox0.Text) && !(countTextBox0.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
                        sub1 = sub + sub * Tax / 100;
                        money0 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox0.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox0.ReadOnly = true;
                    }
                    else if (!(weightTextBox0.Text == "0") && !string.IsNullOrEmpty(countTextBox0.Text))
                    {
                        sub = decimal.Parse(weightTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
                        sub1 = sub + sub * Tax / 100;
                        money0 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox0.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox0.ReadOnly = true;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox1.ReadOnly = false;
                    unitPriceTextBox1.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox0.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
                }
            }
            
        }
        #endregion
        #region "2行目"
        private void unitPriceTextBox1_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox2.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox0.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox0.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox1.Text))
                {
                    unitPriceTextBox1.Text = "単価 -> 重量 or 数量";
                    
                }
                else if(!string.IsNullOrEmpty(unitPriceTextBox2.Text))
                {
                    unitPriceTextBox1.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox1.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox2.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox1.Text) && !(countTextBox1.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
                        sub1 = sub + sub * Tax / 100;
                        money1 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox1.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox1.ReadOnly = true;
                        money1 = sub;
                    }
                    else if (!(weightTextBox1.Text == "0") && !string.IsNullOrEmpty(countTextBox1.Text))
                    {
                        sub = decimal.Parse(weightTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
                        sub1 = sub + sub * Tax / 100;
                        money1 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox1.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox1.ReadOnly = true;
                        money1 = sub;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox2.ReadOnly = false;
                    unitPriceTextBox2.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox1.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox1.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "3行目"
        private void unitPriceTextBox2_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox3.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox1.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox1.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox2.Text))
                {
                    unitPriceTextBox2.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox3.Text))
                {
                    unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox2.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox3.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox2.Text) && !(countTextBox2.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
                        sub1 = sub + sub * Tax / 100;
                        money2 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox2.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox2.ReadOnly = true;
                        money2 = sub;
                    }
                    else if (!(weightTextBox2.Text == "0") && !string.IsNullOrEmpty(countTextBox2.Text))
                    {
                        sub = decimal.Parse(weightTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
                        sub1 = sub + sub * Tax / 100;
                        money2 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox2.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox2.ReadOnly = true;
                        money2 = sub;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox3.ReadOnly = false;
                    unitPriceTextBox3.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox2.Text, System.Globalization.NumberStyles.Number));
                }
            }
            
        }
        #endregion
        #region "4行目"
        private void unitPriceTextBox3_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox4.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox2.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox2.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox3.Text))
                {
                    unitPriceTextBox3.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox4.Text))
                {
                    unitPriceTextBox3.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox3.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox4.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox3.Text) && !(countTextBox3.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox3.Text) * decimal.Parse(unitPriceTextBox03.Text);
                        sub1 = sub + sub * Tax / 100;
                        money3 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox3.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox3.ReadOnly = true;
                        money3 = sub;
                    }
                    else if (!(weightTextBox3.Text == "0") && !string.IsNullOrEmpty(countTextBox3.Text))
                    {
                        sub = decimal.Parse(weightTextBox3.Text) * decimal.Parse(unitPriceTextBox3.Text);
                        sub1 = sub + sub * Tax / 100;
                        money3 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox3.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox3.ReadOnly = true;
                        money3 = sub;
                    }
                    else
                    {

                    }

                }
                else
                {
                    unitPriceTextBox4.ReadOnly = false;
                    unitPriceTextBox4.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox3.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox3.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "5行目"
        private void unitPriceTextBox4_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox5.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox3.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox3.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox4.Text))
                {
                    unitPriceTextBox4.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox5.Text))
                {
                    unitPriceTextBox4.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox4.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox5.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox4.Text) && !(countTextBox4.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
                        sub1 = sub + sub * Tax / 100;
                        money4 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox4.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox4.ReadOnly = true;
                        money4 = sub;
                    }
                    else if (!(weightTextBox4.Text == "0") && !string.IsNullOrEmpty(countTextBox4.Text))
                    {
                        sub = decimal.Parse(weightTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
                        sub1 = sub + sub * Tax / 100;
                        money4 = Math.Round(sub04, MidpointRounding.AwayFromZero);
                        moneyTextBox4.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox4.ReadOnly = true;
                        money4 = sub;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox5.ReadOnly = false;
                    unitPriceTextBox5.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox4.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox4.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "6行目"
        private void unitPriceTextBox5_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox6.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox4.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox4.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox5.Text))
                {
                    unitPriceTextBox5.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox6.Text))
                {
                    unitPriceTextBox5.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox5.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox6.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox5.Text) && !(countTextBox5.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
                        sub1 = sub + sub * Tax / 100;
                        money5 = Math.Round(sub05, MidpointRounding.AwayFromZero);
                        moneyTextBox5.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox5.ReadOnly = true;
                        money5 = sub;
                    }
                    else if (!(weightTextBox5.Text == "0") && !string.IsNullOrEmpty(countTextBox5.Text))
                    {
                        sub = decimal.Parse(weightTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
                        sub1 = sub + sub * Tax / 100;
                        money5 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox5.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox5.ReadOnly = true;
                        money5 = sub;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox6.ReadOnly = false;
                    unitPriceTextBox6.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox5.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox5.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "7行目"
        private void unitPriceTextBox6_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox7.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox5.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox5.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox6.Text))
                {
                    unitPriceTextBox6.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox7.Text))
                {
                    unitPriceTextBox6.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox6.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox7.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox6.Text) && !(countTextBox6.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
                        sub1 = sub + sub * Tax / 100;
                        money6 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox6.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox6.ReadOnly = true;
                        money6 = sub;
                    }
                    else if (!(weightTextBox6.Text == "0") && !string.IsNullOrEmpty(countTextBox6.Text))
                    {
                        sub = decimal.Parse(weightTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
                        sub1 = sub + sub * Tax / 100;
                        money6 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox6.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox6.ReadOnly = true;
                        money6 = sub;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox7.ReadOnly = false;
                    unitPriceTextBox7.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox6.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox6.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "8行目"
        private void unitPriceTextBox7_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox8.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox6.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox6.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox7.Text))
                {
                    unitPriceTextBox7.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox8.Text))
                {
                    unitPriceTextBox7.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox7.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox8.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox7.Text) && !(countTextBox7.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
                        sub1 = sub + sub * Tax / 100;
                        money7 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox7.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox7.ReadOnly = true;
                        money7 = sub;
                    }
                    else if (!(weightTextBox7.Text == "0") && !string.IsNullOrEmpty(countTextBox7.Text))
                    {
                        sub07 = decimal.Parse(weightTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
                        sub1 = sub + sub * Tax / 100;
                        money7 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox7.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox7.ReadOnly = true;
                        money7 = sub;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox8.ReadOnly = false;
                    unitPriceTextBox8.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox7.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox7.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "9行目"
        private void unitPriceTextBox8_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox9.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox7.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox7.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox8.Text))
                {
                    unitPriceTextBox8.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox9.Text))
                {
                    unitPriceTextBox8.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox8.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox9.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox8.Text) && !(countTextBox8.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
                        sub1 = sub + sub * Tax / 100;
                        money8 = Math.Round(sub08, MidpointRounding.AwayFromZero);
                        moneyTextBox8.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox8.ReadOnly = true;
                        money8 = sub;
                    }
                    else if (!(weightTextBox8.Text == "0") && !string.IsNullOrEmpty(countTextBox8.Text))
                    {
                        sub = decimal.Parse(weightTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
                        sub1 = sub + sub * Tax / 100;
                        money8 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox8.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox8.ReadOnly = true;
                        money8 = sub;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox9.ReadOnly = false;
                    unitPriceTextBox9.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox8.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox8.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "10行目"
        private void unitPriceTextBox9_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox10.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox8.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox8.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox9.Text))
                {
                    unitPriceTextBox9.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox10.Text))
                {
                    unitPriceTextBox9.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox9.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox10.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox9.Text) && !(countTextBox9.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
                        sub1 = sub + sub * Tax / 100;
                        money9 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox9.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox9.ReadOnly = true;
                        money9 = sub;
                    }
                    else if (!(weightTextBox9.Text == "0") && !string.IsNullOrEmpty(countTextBox9.Text))
                    {
                        sub = decimal.Parse(weightTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
                        sub1 = sub + sub * Tax / 100;
                        money9 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox9.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox9.ReadOnly = true;
                        money9 = sub;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox10.ReadOnly = false;
                    unitPriceTextBox10.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox9.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox9.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "11行目"
        private void unitPriceTextBox10_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox11.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox9.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox9.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox10.Text))
                {
                    unitPriceTextBox10.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox11.Text))
                {
                    unitPriceTextBox10.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox10.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox11.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox10.Text) && !(countTextBox010.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
                        sub1 = sub + sub * Tax / 100;
                        money10 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox10.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox10.ReadOnly = true;
                        money10 = sub;
                    }
                    else if (!(weightTextBox10.Text == "0") && !string.IsNullOrEmpty(countTextBox10.Text))
                    {
                        sub = decimal.Parse(weightTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
                        sub1 = sub + sub * Tax / 100;
                        money10 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox10.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox10.ReadOnly = true;
                        money10 = sub;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox11.ReadOnly = false;
                    unitPriceTextBox11.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox10.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox10.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "12行目"
        private void unitPriceTextBox11_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {
                unitPriceTextBox12.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox10.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox10.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox11.Text))
                {
                    unitPriceTextBox11.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox12.Text))
                {
                    unitPriceTextBox11.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox11.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox12.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox11.Text) && !(countTextBox11.Text == "0"))
                    {
                        sub = decimal.Parse(countTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
                        sub1 = sub + sub * Tax / 100;
                        money11 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox11.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        weightTextBox11.ReadOnly = true;
                        money11 = sub;
                    }
                    else if (!(weightTextBox11.Text == "0") && !string.IsNullOrEmpty(countTextBox11.Text))
                    {
                        sub = decimal.Parse(weightTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
                        sub1 = sub + sub * Tax / 100;
                        money11 = Math.Round(sub, MidpointRounding.AwayFromZero);
                        moneyTextBox11.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                        countTextBox11.ReadOnly = true;
                        money11 = sub;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox12.ReadOnly = false;
                    unitPriceTextBox12.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox11.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox11.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "13行目"
        private void unitPriceTextBox12_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox11.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox11.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox12.Text))
                {
                    unitPriceTextBox12.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox12.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox12.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #endregion

        #region"計算書　金額が入力されたら次の単価が入力可＋総重量 or 総数計算、自動計算"
        #region "1行目"
        private void moneyTextBox0_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {
                countsum0 = int.Parse(countTextBox0.Text);
                weisum0 = decimal.Parse(weightTextBox0.Text);
            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox0.Text) && string.IsNullOrEmpty(countTextBox0.Text))
                {
                    countTextBox0.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox0.Text) && string.IsNullOrEmpty(weightTextBox0.Text))
                {
                    weightTextBox0.Text = 0.ToString();
                }
                else
                {

                }
                #region "顧客選択から戻ってきた時"
                if (amount10 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 1 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money0 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum0 = int.Parse(countTextBox0.Text);
                weisum0 = decimal.Parse(weightTextBox0.Text);
                subSum0 = money0;//計算書は税込み
                #region "数量の場合わけ"
                if (countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum0;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum0;
                }
                #endregion

                #region "金額合計の場合わけ"
                if (subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum0;
                }
                #endregion

                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
            
        }
        #endregion
        #region "2行目"
        private void moneyTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox1.Text) && string.IsNullOrEmpty(countTextBox1.Text))
                {
                    countTextBox1.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox1.Text) && string.IsNullOrEmpty(weightTextBox1.Text))
                {
                    weightTextBox1.Text = 0.ToString();
                }
                else
                {

                }
                #region "顧客選択から戻ってきた時"
                if (amount11 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 2 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money1 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum1 = int.Parse(countTextBox1.Text);
                weisum1 = decimal.Parse(weightTextBox1.Text);
                subSum1 = money1;
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum1;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum1;
                }
                #endregion
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum1;
                }
                #endregion
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));


                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
            
        }
        #endregion
        #region "3行目"
        private void moneyTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox2.Text) && string.IsNullOrEmpty(countTextBox2.Text))
                {
                    countTextBox2.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox2.Text) && string.IsNullOrEmpty(weightTextBox2.Text))
                {
                    weightTextBox2.Text = 0.ToString();
                }
                else
                {
                    
                }
                #region "顧客選択から戻ってきた時"
                if (amount12 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 3 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money2 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum2 = int.Parse(countTextBox2.Text);
                weisum2 = decimal.Parse(weightTextBox2.Text);
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum1 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum2;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum1 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum2;
                }
                #endregion
                subSum2 = money2;
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum1 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum2;
                }
                #endregion
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }

        }
        #endregion
        #region "4行目"
        private void moneyTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox3.Text) && string.IsNullOrEmpty(countTextBox3.Text))
                {
                    countTextBox3.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox3.Text) && string.IsNullOrEmpty(weightTextBox3.Text))
                {
                    weightTextBox3.Text = 0.ToString();
                }
                else
                {
                    
                }
                #region "顧客選択から戻ってきた時"
                if (amount13 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 4 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money3 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum3 = int.Parse(countTextBox3.Text);
                weisum3 = decimal.Parse(weightTextBox3.Text);
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum3;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum3;
                }
                #endregion
                subSum3 = money3;     //増やしていく
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum3;
                }
                #endregion
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
 
        }
        #endregion
        #region "5行目"
        private void moneyTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox4.Text) && string.IsNullOrEmpty(countTextBox4.Text))
                {
                    countTextBox4.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox4.Text) && string.IsNullOrEmpty(weightTextBox4.Text))
                {
                    weightTextBox4.Text = 0.ToString();
                }
                else
                {
                    
                }
                #region "顧客選択から戻ってきた時"
                if (amount14 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 5 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money4 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum4 = int.Parse(countTextBox4.Text);
                weisum4 = decimal.Parse(weightTextBox4.Text);
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum4;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum4;
                }
                #endregion
                subSum4 = money4;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum4;
                }
                #endregion
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }

        }
        #endregion
        #region "6行目"
        private void moneyTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox5.Text) && string.IsNullOrEmpty(countTextBox5.Text))
                {
                    countTextBox5.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox5.Text) && string.IsNullOrEmpty(weightTextBox5.Text))
                {
                    weightTextBox5.Text = 0.ToString();
                }
                else
                {
                    
                }
                #region "顧客選択から戻ってきた時"
                if (amount15 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 6 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money5 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum5 = int.Parse(countTextBox5.Text);
                weisum5 = decimal.Parse(weightTextBox5.Text);
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum5;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum5;
                }
                #endregion
                subSum5 = money5;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum5;
                }
                #endregion
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }

        }
        #endregion
        #region "7行目"
        private void moneyTextBox6_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox6.Text) && string.IsNullOrEmpty(countTextBox6.Text))
                {
                    countTextBox6.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox6.Text) && string.IsNullOrEmpty(weightTextBox6.Text))
                {
                    weightTextBox6.Text = 0.ToString();
                }
                else
                {
                    
                }
                #region "顧客選択から戻ってきた時"
                if (amount16 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 7 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money6 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum6 = int.Parse(countTextBox6.Text);
                weisum6 = decimal.Parse(weightTextBox6.Text);
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum6;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum6;
                }
                #endregion
                subSum6 = money6;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum6;
                }
                #endregion
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }

        }
        #endregion
        #region "8行目"
        private void moneyTextBox7_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox7.Text) && string.IsNullOrEmpty(countTextBox7.Text))
                {
                    countTextBox7.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox7.Text) && string.IsNullOrEmpty(weightTextBox7.Text))
                {
                    weightTextBox7.Text = 0.ToString();
                }
                else
                {
                   
                }
                #region "顧客選択から戻ってきた時"
                if (amount17 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 8 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money7 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum7 = int.Parse(countTextBox7.Text);
                weisum7 = decimal.Parse(weightTextBox7.Text);
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum7;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum7;
                }
                #endregion
                subSum7 = money7;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum7;
                }
                #endregion
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
        }
        #endregion
        #region "9行目"
        private void moneyTextBox8_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox8.Text) && string.IsNullOrEmpty(countTextBox8.Text))
                {
                    countTextBox8.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox8.Text) && string.IsNullOrEmpty(weightTextBox8.Text))
                {
                    weightTextBox8.Text = 0.ToString();
                }
                else
                {
                    
                }
                #region "顧客選択から戻ってきた時"
                if (amount18 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 9 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money8 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum8 = int.Parse(countTextBox8.Text);
                weisum8 = decimal.Parse(weightTextBox8.Text);
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum8;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum8;
                }
                #endregion
                subSum8 = money8;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum8;
                }
                #endregion
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
        }
        #endregion
        #region "10行目"
        private void moneyTextBox9_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox9.Text) && string.IsNullOrEmpty(countTextBox9.Text))
                {
                    countTextBox9.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox9.Text) && string.IsNullOrEmpty(weightTextBox9.Text))
                {
                    weightTextBox9.Text = 0.ToString();
                }
                else
                {
                    
                }
                #region "顧客選択から戻ってきた時"
                if (amount19 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 10 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money9 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum9 = int.Parse(countTextBox9.Text);
                weisum9 = decimal.Parse(weightTextBox9.Text);
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum9;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum9;
                }
                #endregion
                subSum9 = money9;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum9;
                }
                #endregion
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }

        }
        #endregion
        #region "11行目"
        private void moneyTextBox10_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox10.Text) && string.IsNullOrEmpty(countTextBox10.Text))
                {
                    countTextBox10.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox10.Text) && string.IsNullOrEmpty(weightTextBox10.Text))
                {
                    weightTextBox10.Text = 0.ToString();
                }
                else
                {
                    
                }
                #region "顧客選択から戻ってきた時"
                if (amount110 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 11 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money10 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum10 = int.Parse(countTextBox10.Text);
                weisum10 = decimal.Parse(weightTextBox10.Text);
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum11 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum10;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum11 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum10;
                }
                #endregion
                subSum10 = money10;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum11 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum10;
                }
                #endregion
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
        }
        #endregion
        #region "12行目"
        private void moneyTextBox11_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox11.Text) && string.IsNullOrEmpty(countTextBox11.Text))
                {
                    countTextBox11.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox11.Text) && string.IsNullOrEmpty(weightTextBox11.Text))
                {
                    weightTextBox11.Text = 0.ToString();
                }
                else
                {
                   
                }
                #region "顧客選択から戻ってきた時"
                if (amount111 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 12 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money11 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum11 = int.Parse(countTextBox11.Text);
                weisum11 = decimal.Parse(weightTextBox11.Text);
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum12 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum11;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum12 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum11;
                }
                #endregion
                subSum11 = money11;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum12 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum11;
                }
                #endregion
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
            
        }
        #endregion
        #region "13行目"
        private void moneyTextBox12_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox12.Text) && string.IsNullOrEmpty(countTextBox12.Text))
                {
                    countTextBox12.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox12.Text) && string.IsNullOrEmpty(weightTextBox12.Text))
                {
                    weightTextBox12.Text = 0.ToString();
                }
                else
                {
                   
                }
                #region "顧客選択から戻ってきた時"
                if (amount112 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from statement_calc_data_if where document_number = '" + document + "' ORDER BY registration_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["registration_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + document + "' and record_number = " + 13 + " and registration_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money12 = decimal.Parse(dataRow1["amount"].ToString());

                }
                #endregion
                countsum12 = int.Parse(countTextBox12.Text);
                weisum12 = decimal.Parse(weightTextBox12.Text);
                #region "数量の場合わけ"
                if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0)
                {
                    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
                }
                else
                {
                    countsum = countsum12;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0)
                {
                    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
                }
                else
                {
                    weisum = weisum12;
                }
                #endregion
                subSum12 = money12;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                #region "金額合計の場合わけ"
                if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0)
                {
                    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
                }
                else
                {
                    subSum = subSum12;
                }
                #endregion
                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
            
        }
        #endregion
        #endregion

        #region　"納品書　単価を入力したら重量、数値入力可"
        #region "1行目"
        private void unitPriceTextBox00_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox00.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox00.Text))
            {
                weightTextBox00.ReadOnly = true;
                countTextBox00.ReadOnly = true;
                unitPriceTextBox01.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox00.Text) && !(unitPriceTextBox00.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox00.ReadOnly = false;
                countTextBox00.ReadOnly = false;
            }
        }
        #endregion
        #region "2行目"
        private void unitPriceTextBox01_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox01.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox01.Text))
            {
                weightTextBox01.ReadOnly = true;
                countTextBox01.ReadOnly = true;
                unitPriceTextBox02.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox01.Text) && !(unitPriceTextBox01.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox01.ReadOnly = false;
                countTextBox01.ReadOnly = false;
            }
        }
        #endregion
        #region "3行目"
        private void unitPriceTextBox02_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox02.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox02.Text))
            {
                weightTextBox02.ReadOnly = true;
                countTextBox02.ReadOnly = true;
                unitPriceTextBox03.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox02.Text) && !(unitPriceTextBox02.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox02.ReadOnly = false;
                countTextBox02.ReadOnly = false;
            }
        }
        #endregion
        #region "4行目"
        private void unitPriceTextBox03_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox03.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox03.Text))
            {
                weightTextBox03.ReadOnly = true;
                countTextBox03.ReadOnly = true;
                unitPriceTextBox04.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox03.Text) && !(unitPriceTextBox03.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox03.ReadOnly = false;
                countTextBox03.ReadOnly = false;
            }
        }
        #endregion
        #region "5行目"
        private void unitPriceTextBox04_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox04.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox04.Text))
            {
                weightTextBox04.ReadOnly = true;
                countTextBox04.ReadOnly = true;
                unitPriceTextBox05.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox04.Text) && !(unitPriceTextBox04.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox04.ReadOnly = false;
                countTextBox04.ReadOnly = false;
            }
        }
        #endregion
        #region "6行目"
        private void unitPriceTextBox05_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox05.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox05.Text))
            {
                weightTextBox05.ReadOnly = true;
                countTextBox05.ReadOnly = true;
                unitPriceTextBox06.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox05.Text) && !(unitPriceTextBox05.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox05.ReadOnly = false;
                countTextBox05.ReadOnly = false;
            }
        }
        #endregion
        #region "7行目"
        private void unitPriceTextBox06_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox06.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox06.Text))
            {
                weightTextBox06.ReadOnly = true;
                countTextBox06.ReadOnly = true;
                unitPriceTextBox07.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox06.Text) && !(unitPriceTextBox06.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox06.ReadOnly = false;
                countTextBox06.ReadOnly = false;
            }
        }
        #endregion
        #region "8行目"
        private void unitPriceTextBox07_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox07.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox07.Text))
            {
                weightTextBox07.ReadOnly = true;
                countTextBox07.ReadOnly = true;
                unitPriceTextBox08.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox07.Text) && !(unitPriceTextBox07.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox07.ReadOnly = false;
                countTextBox07.ReadOnly = false;
            }
        }
        #endregion
        #region "9行目"
        private void unitPriceTextBox08_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox08.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox08.Text))
            {
                weightTextBox08.ReadOnly = true;
                countTextBox08.ReadOnly = true;
                unitPriceTextBox09.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox08.Text) && !(unitPriceTextBox08.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox08.ReadOnly = false;
                countTextBox08.ReadOnly = false;
            }
        }
        #endregion
        #region "10行目"
        private void unitPriceTextBox09_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox09.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox09.Text))
            {
                weightTextBox09.ReadOnly = true;
                countTextBox09.ReadOnly = true;
                unitPriceTextBox010.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox09.Text) && !(unitPriceTextBox09.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox09.ReadOnly = false;
                countTextBox09.ReadOnly = false;
            }
        }
        #endregion
        #region "11行目"
        private void unitPriceTextBox010_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox010.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox010.Text))
            {
                weightTextBox010.ReadOnly = true;
                countTextBox010.ReadOnly = true;
                unitPriceTextBox011.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox010.Text) && !(unitPriceTextBox010.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox010.ReadOnly = false;
                countTextBox010.ReadOnly = false;
            }
        }
        #endregion
        #region "12行目"
        private void unitPriceTextBox011_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox011.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox011.Text))
            {
                weightTextBox011.ReadOnly = true;
                countTextBox011.ReadOnly = true;
                unitPriceTextBox012.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox011.Text) && !(unitPriceTextBox011.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox011.ReadOnly = false;
                countTextBox011.ReadOnly = false;
            }
        }
        #endregion
        #region "13行目"
        private void unitPriceTextBox012_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox012.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(unitPriceTextBox012.Text))
            {
                weightTextBox012.ReadOnly = true;
                countTextBox012.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox012.Text) && !(unitPriceTextBox012.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox012.ReadOnly = false;
                countTextBox012.ReadOnly = false;
            }
        }
        #endregion
        #endregion

        #region"納品書　フォーカス時初期状態ならnull"
        #region "1行目"
        private void unitPriceTextBox00_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(typeTextBox.Text))
                {
                    MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    unitPriceTextBox00.Text = "";
                    unitPriceTextBox00.ReadOnly = true;
                    return;
                }
                if (unitPriceTextBox00.Text.ToString() == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox00.Text = "";
                }
            }
        }
        #endregion
        #region "2行目"
        private void unitPriceTextBox01_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox00.Text, @"^[a-zA-Z]+$"))
                {
                    
                }
                else if (unitPriceTextBox01.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox01.Text = "";
                }
            }
        }
        #endregion
        #region "3行目"
        private void unitPriceTextBox02_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox01.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox01.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox02.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox02.Text = "";
                }
            }
        }
        #endregion
        #region "4行目"
        private void unitPriceTextBox03_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox02.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox02.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox03.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox03.Text = "";
                }
            }
        }
        #endregion
        #region "5行目"
        private void unitPriceTextBox04_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox03.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox03.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox04.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox04.Text = "";
                }
            }
        }
        #endregion
        #region "6行目"
        private void unitPriceTextBox05_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox04.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox04.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox05.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox05.Text = "";
                }
            }
        }
        #endregion
        #region "7行目"
        private void unitPriceTextBox06_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox05.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox05.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox06.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox06.Text = "";
                }
            }
        }
        #endregion
        #region "8行目"
        private void unitPriceTextBox07_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox06.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox06.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox07.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox07.Text = "";
                }
            }
        }
        #endregion
        #region "9行目"
        private void unitPriceTextBox08_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox07.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox07.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox08.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox08.Text = "";
                }
            }
        }
        #endregion
        #region "10行目"
        private void unitPriceTextBox09_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox08.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox08.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox09.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox09.Text = "";
                }
            }
        }
        #endregion
        #region "11行目"
        private void unitPriceTextBox010_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox09.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox09.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox010.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox010.Text = "";
                }
            }
        }
        #endregion
        #region "12行目"
        private void unitPriceTextBox011_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox010.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox010.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox011.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox011.Text = "";
                }
            }
        }
        #endregion
        #region "13行目"
        private void unitPriceTextBox012_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox011.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox011.Text, @"^[a-zA-Z]+$"))
                {

                }
                else if (unitPriceTextBox012.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox012.Text = "";
                }
            }
        }
        #endregion
        #endregion

        #region"納品書　数量を入力したら重量を入力不可"

        #region "1行目"
        private void countTextBox00_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox00.Text))
                {
                    weightTextBox00.ReadOnly = true;
                }
                else
                {
                    weightTextBox00.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "2行目"
        private void countTextBox01_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox01.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox01.Text))
                {
                    weightTextBox01.ReadOnly = true;
                }
                else
                {
                    weightTextBox01.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "3行目"
        private void countTextBox02_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox02.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox02.Text))
                {
                    weightTextBox02.ReadOnly = true;
                }
                else
                {
                    weightTextBox02.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "4行目"
        private void countTextBox03_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox03.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox03.Text))
                {
                    weightTextBox03.ReadOnly = true;
                }
                else
                {
                    weightTextBox03.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "5行目"
        private void countTextBox04_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox04.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox04.Text))
                {
                    weightTextBox04.ReadOnly = true;
                }
                else
                {
                    weightTextBox04.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "6行目"
        private void countTextBox05_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox05.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox05.Text))
                {
                    weightTextBox05.ReadOnly = true;
                }
                else
                {
                    weightTextBox05.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "7行目"
        private void countTextBox06_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox06.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox06.Text))
                {
                    weightTextBox06.ReadOnly = true;
                }
                else
                {
                    weightTextBox06.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "8行目"
        private void countTextBox07_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox07.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox07.Text))
                {
                    weightTextBox07.ReadOnly = true;
                }
                else
                {
                    weightTextBox07.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "9行目"
        private void countTextBox08_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox08.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox08.Text))
                {
                    weightTextBox08.ReadOnly = true;
                }
                else
                {
                    weightTextBox08.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "10行目"
        private void countTextBox09_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox09.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox09.Text))
                {
                    weightTextBox09.ReadOnly = true;
                }
                else
                {
                    weightTextBox09.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "11行目"
        private void countTextBox010_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox010.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox010.Text))
                {
                    weightTextBox010.ReadOnly = true;
                }
                else
                {
                    weightTextBox010.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "12行目"
        private void countTextBox011_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox011.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox011.Text))
                {
                    weightTextBox011.ReadOnly = true;
                }
                else
                {
                    weightTextBox011.ReadOnly = false;
                }
            }
        }
        #endregion
        #region "13行目"
        private void countTextBox012_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox012.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox012.Text))
                {
                    weightTextBox012.ReadOnly = true;
                }
                else
                {
                    weightTextBox012.ReadOnly = false;
                }
            }
        }
        #endregion
        #endregion

        #region"納品書　税込み税抜き選択"

        private void comboBox11_TextChanged(object sender, EventArgs e)
        {
            if (second == false)
            {
                second = true;
                return;
            }
            else
            {

                if (comboBox11.SelectedIndex == 1)      //税抜き選択
                {
                    subSum = decimal.Parse(sub00.ToString()) + decimal.Parse(sub01.ToString()) + decimal.Parse(sub02.ToString()) + decimal.Parse(sub03.ToString()) + decimal.Parse(sub04.ToString())
                            + decimal.Parse(sub05.ToString()) + decimal.Parse(sub06.ToString()) + decimal.Parse(sub07.ToString()) + decimal.Parse(sub08.ToString()) + decimal.Parse(sub09.ToString())
                            + decimal.Parse(sub010.ToString()) + decimal.Parse(sub011.ToString()) + decimal.Parse(sub012.ToString());

                    subTotal2.Text = string.Format("{0:#,0}", Math.Round(subSum, MidpointRounding.AwayFromZero));    //税抜き表記
                }
                else        //税込み選択
                {
                    subSum = decimal.Parse(sub10.ToString()) + decimal.Parse(sub11.ToString()) + decimal.Parse(sub12.ToString()) + decimal.Parse(sub13.ToString()) + decimal.Parse(sub14.ToString())
                            + decimal.Parse(sub15.ToString()) + decimal.Parse(sub16.ToString()) + decimal.Parse(sub17.ToString()) + decimal.Parse(sub18.ToString()) + decimal.Parse(sub19.ToString())
                            + decimal.Parse(sub110.ToString()) + decimal.Parse(sub111.ToString()) + decimal.Parse(sub112.ToString());

                    sum =  subSum ;

                    subTotal2.Text = string.Format("{0:#,0}", Math.Round(sum, MidpointRounding.AwayFromZero));    //税込み表記
                }
                sumTextBox2.Text = string.Format("{0:#,0}", Math.Round(sum, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            }
        }
        #endregion

        #region"納品書　単価３桁区切り＋フォーカスが外れた時"
        #region "1行目"
        private void unitPriceTextBox00_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox01.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox00.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox00.Text))
                {
                    unitPriceTextBox00.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox01.Text))
                {
                    unitPriceTextBox00.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox00.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox01.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox00.Text) && !(countTextBox00.Text == "0"))
                    {
                        sub00 = decimal.Parse(countTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
                        sub10 = sub00 + sub00 * Tax / 100;
                        money0 = Math.Round(sub00, MidpointRounding.AwayFromZero);
                        moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub10, MidpointRounding.AwayFromZero));
                        weightTextBox00.ReadOnly = true;
                    }
                    else if (!(weightTextBox00.Text == "0") && !string.IsNullOrEmpty(countTextBox00.Text))
                    {
                        sub00 = decimal.Parse(weightTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
                        sub10 = sub00 + sub00 * Tax / 100;
                        money0 = Math.Round(sub00, MidpointRounding.AwayFromZero);
                        moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub10, MidpointRounding.AwayFromZero));
                        countTextBox00.ReadOnly = true;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox01.ReadOnly = false;
                    unitPriceTextBox01.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox00.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox00.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "2行目"
        private void unitPriceTextBox01_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox02.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox00.Text, @"^[a-zA-Z]+$"))
            {
                //MessageBox.Show("正しく入力して下さい。");
                //return;
            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox01.Text))
                {
                    unitPriceTextBox01.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox02.Text))
                {
                    unitPriceTextBox01.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox01.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox02.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox01.Text) && !(countTextBox01.Text == "0"))
                    {
                        sub01 = decimal.Parse(countTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
                        sub11 = sub01 + sub01 * Tax / 100;
                        money1 = Math.Round(sub01, MidpointRounding.AwayFromZero);
                        moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub11, MidpointRounding.AwayFromZero));
                        weightTextBox01.ReadOnly = true;
                        money1 = sub01;
                    }
                    else if (!(weightTextBox01.Text == "0") && !string.IsNullOrEmpty(countTextBox01.Text))
                    {
                        sub01 = decimal.Parse(weightTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
                        sub11 = sub01 + sub01 * Tax / 100;
                        money1 = Math.Round(sub01, MidpointRounding.AwayFromZero);
                        moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub11, MidpointRounding.AwayFromZero));
                        countTextBox01.ReadOnly = true;
                        money1 = sub01;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox02.ReadOnly = false;
                    unitPriceTextBox02.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox01.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox01.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "3行目"
        private void unitPriceTextBox02_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox03.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox01.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox01.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox02.Text))
                {
                    unitPriceTextBox02.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox03.Text))
                {
                    unitPriceTextBox02.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox02.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox03.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox02.Text) && !(countTextBox02.Text == "0"))
                    {
                        sub02 = decimal.Parse(countTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
                        sub12 = sub02 + sub02 * Tax / 100;
                        money2 = Math.Round(sub02, MidpointRounding.AwayFromZero);
                        moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub12, MidpointRounding.AwayFromZero));
                        weightTextBox02.ReadOnly = true;
                        money2 = sub02;
                    }
                    else if (!(weightTextBox02.Text == "0") && !string.IsNullOrEmpty(countTextBox02.Text))
                    {
                        sub02 = decimal.Parse(weightTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
                        sub12 = sub02 + sub02 * Tax / 100;
                        money2 = Math.Round(sub02, MidpointRounding.AwayFromZero);
                        moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub12, MidpointRounding.AwayFromZero));
                        countTextBox02.ReadOnly = true;
                        money2 = sub02;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox03.ReadOnly = false;
                    unitPriceTextBox03.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox02.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox02.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "4行目"
        private void unitPriceTextBox03_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox04.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox02.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox02.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox03.Text))
                {
                    unitPriceTextBox03.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox04.Text))
                {
                    unitPriceTextBox03.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox03.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox04.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox03.Text) && !(countTextBox03.Text == "0"))
                    {
                        sub03 = decimal.Parse(countTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
                        sub13 = sub03 + sub03 * Tax / 100;
                        money3 = Math.Round(sub03, MidpointRounding.AwayFromZero);
                        moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub13, MidpointRounding.AwayFromZero));
                        weightTextBox03.ReadOnly = true;
                        money3 = sub03;
                    }
                    else if (!(weightTextBox03.Text == "0") && !string.IsNullOrEmpty(countTextBox03.Text))
                    {
                        sub03 = decimal.Parse(weightTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
                        sub13 = sub03 + sub03 * Tax / 100;
                        money3 = Math.Round(sub03, MidpointRounding.AwayFromZero);
                        moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub13, MidpointRounding.AwayFromZero));
                        countTextBox03.ReadOnly = true;
                        money3 = sub03;
                    }
                    else
                    {

                    }

                }
                else
                {
                    unitPriceTextBox04.ReadOnly = false;
                    unitPriceTextBox04.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox03.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox03.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "5行目"
        private void unitPriceTextBox04_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox05.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox03.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox03.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox04.Text))
                {
                    unitPriceTextBox04.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox05.Text))
                {
                    unitPriceTextBox04.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox04.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox05.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox04.Text) && !(countTextBox04.Text == "0"))
                    {
                        sub04 = decimal.Parse(countTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
                        sub14 = sub04 + sub04 * Tax / 100;
                        money4 = Math.Round(sub04, MidpointRounding.AwayFromZero);
                        moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub14, MidpointRounding.AwayFromZero));
                        weightTextBox04.ReadOnly = true;
                        money4 = sub04;
                    }
                    else if (!(weightTextBox04.Text == "0") && !string.IsNullOrEmpty(countTextBox04.Text))
                    {
                        sub04 = decimal.Parse(weightTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
                        sub14 = sub04 + sub04 * Tax / 100;
                        money4 = Math.Round(sub04, MidpointRounding.AwayFromZero);
                        moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub14, MidpointRounding.AwayFromZero));
                        countTextBox04.ReadOnly = true;
                        money4 = sub04;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox05.ReadOnly = false;
                    unitPriceTextBox05.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox04.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox04.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "6行目"
        private void unitPriceTextBox05_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox06.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox04.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox04.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox05.Text))
                {
                    unitPriceTextBox05.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox06.Text))
                {
                    unitPriceTextBox05.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox05.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox06.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox05.Text) && !(countTextBox05.Text == "0"))
                    {
                        sub05 = decimal.Parse(countTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
                        sub15 = sub05 + sub05 * Tax / 100;
                        money5 = Math.Round(sub05, MidpointRounding.AwayFromZero);
                        moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub15, MidpointRounding.AwayFromZero));
                        weightTextBox05.ReadOnly = true;
                        money5 = sub05;
                    }
                    else if (!(weightTextBox05.Text == "0") && !string.IsNullOrEmpty(countTextBox05.Text))
                    {
                        sub05 = decimal.Parse(weightTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
                        sub15 = sub05 + sub05 * Tax / 100;
                        money5 = Math.Round(sub05, MidpointRounding.AwayFromZero);
                        moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub15, MidpointRounding.AwayFromZero));
                        countTextBox05.ReadOnly = true;
                        money5 = sub05;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox06.ReadOnly = false;
                    unitPriceTextBox06.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox05.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox05.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "7行目"
        private void unitPriceTextBox06_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox07.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox05.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox05.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox06.Text))
                {
                    unitPriceTextBox06.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox07.Text))
                {
                    unitPriceTextBox06.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox06.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox07.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox06.Text) && !(countTextBox06.Text == "0"))
                    {
                        sub06 = decimal.Parse(countTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
                        sub16 = sub06 + sub06 * Tax / 100;
                        money6 = Math.Round(sub06, MidpointRounding.AwayFromZero);
                        moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub16, MidpointRounding.AwayFromZero));
                        weightTextBox06.ReadOnly = true;
                        money6 = sub06;
                    }
                    else if (!(weightTextBox06.Text == "0") && !string.IsNullOrEmpty(countTextBox06.Text))
                    {
                        sub06 = decimal.Parse(weightTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
                        sub16 = sub06 + sub06 * Tax / 100;
                        money6 = Math.Round(sub06, MidpointRounding.AwayFromZero);
                        moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub16, MidpointRounding.AwayFromZero));
                        countTextBox06.ReadOnly = true;
                        money6 = sub06;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox07.ReadOnly = false;
                    unitPriceTextBox07.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox06.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox06.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "8行目"
        private void unitPriceTextBox07_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox08.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox06.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox06.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox07.Text))
                {
                    unitPriceTextBox07.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox08.Text))
                {
                    unitPriceTextBox07.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox07.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox08.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox07.Text) && !(countTextBox07.Text == "0"))
                    {
                        sub07 = decimal.Parse(countTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
                        sub17 = sub07 + sub07 * Tax / 100;
                        money7 = Math.Round(sub07, MidpointRounding.AwayFromZero);
                        moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub17, MidpointRounding.AwayFromZero));
                        weightTextBox07.ReadOnly = true;
                        money7 = sub07;
                    }
                    else if (!(weightTextBox07.Text == "0") && !string.IsNullOrEmpty(countTextBox07.Text))
                    {
                        sub07 = decimal.Parse(weightTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
                        sub17 = sub07 + sub07 * Tax / 100;
                        money7 = Math.Round(sub07, MidpointRounding.AwayFromZero);
                        moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub17, MidpointRounding.AwayFromZero));
                        countTextBox07.ReadOnly = true;
                        money7 = sub07;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox08.ReadOnly = false;
                    unitPriceTextBox08.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox07.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox07.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "9行目"
        private void unitPriceTextBox08_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox09.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox07.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox07.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox08.Text))
                {
                    unitPriceTextBox08.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox09.Text))
                {
                    unitPriceTextBox08.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox08.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox09.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox08.Text) && !(countTextBox08.Text == "0"))
                    {
                        sub08 = decimal.Parse(countTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
                        sub18 = sub08 + sub08 * Tax / 100;
                        money8 = Math.Round(sub08, MidpointRounding.AwayFromZero);
                        moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub18, MidpointRounding.AwayFromZero));
                        weightTextBox08.ReadOnly = true;
                        money8 = sub08;
                    }
                    else if (!(weightTextBox08.Text == "0") && !string.IsNullOrEmpty(countTextBox08.Text))
                    {
                        sub08 = decimal.Parse(weightTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
                        sub18 = sub08 + sub08 * Tax / 100;
                        money8 = Math.Round(sub08, MidpointRounding.AwayFromZero);
                        moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub18, MidpointRounding.AwayFromZero));
                        countTextBox08.ReadOnly = true;
                        money8 = sub08;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox09.ReadOnly = false;
                    unitPriceTextBox09.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox08.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox08.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "10行目"
        private void unitPriceTextBox09_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox10.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox08.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox08.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox09.Text))
                {
                    unitPriceTextBox09.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox010.Text))
                {
                    unitPriceTextBox09.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox09.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox010.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox09.Text) && !(countTextBox09.Text == "0"))
                    {
                        sub09 = decimal.Parse(countTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
                        sub19 = sub09 + sub09 * Tax / 100;
                        money9 = Math.Round(sub09, MidpointRounding.AwayFromZero);
                        moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub19, MidpointRounding.AwayFromZero));
                        weightTextBox09.ReadOnly = true;
                        money9 = sub09;
                    }
                    else if (!(weightTextBox09.Text == "0") && !string.IsNullOrEmpty(countTextBox09.Text))
                    {
                        sub09 = decimal.Parse(weightTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
                        sub19 = sub09 + sub09 * Tax / 100;
                        money9 = Math.Round(sub09, MidpointRounding.AwayFromZero);
                        moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub19, MidpointRounding.AwayFromZero));
                        countTextBox09.ReadOnly = true;
                        money9 = sub09;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox10.ReadOnly = false;
                    unitPriceTextBox10.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox09.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox09.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "11行目"
        private void unitPriceTextBox010_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox011.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox09.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox09.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox010.Text))
                {
                    unitPriceTextBox010.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox011.Text))
                {
                    unitPriceTextBox010.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox010.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox011.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox010.Text) && !(countTextBox010.Text == "0"))
                    {
                        sub010 = decimal.Parse(countTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
                        sub110 = sub010 + sub010 * Tax / 100;
                        money10 = Math.Round(sub010, MidpointRounding.AwayFromZero);
                        moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub110, MidpointRounding.AwayFromZero));
                        weightTextBox010.ReadOnly = true;
                        money10 = sub010;
                    }
                    else if (!(weightTextBox010.Text == "0") && !string.IsNullOrEmpty(countTextBox010.Text))
                    {
                        sub010 = decimal.Parse(weightTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
                        sub110 = sub010 + sub010 * Tax / 100;
                        money10 = Math.Round(sub010, MidpointRounding.AwayFromZero);
                        moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub110, MidpointRounding.AwayFromZero));
                        countTextBox010.ReadOnly = true;
                        money10 = sub010;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox011.ReadOnly = false;
                    unitPriceTextBox011.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox010.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox010.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "12行目"
        private void unitPriceTextBox011_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {
                unitPriceTextBox012.ReadOnly = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox010.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox010.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox011.Text))
                {
                    unitPriceTextBox011.Text = "単価 -> 重量 or 数量";
                }
                else if (!string.IsNullOrEmpty(unitPriceTextBox012.Text))
                {
                    unitPriceTextBox011.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox011.Text, System.Globalization.NumberStyles.Number));
                    unitPriceTextBox012.ReadOnly = false;
                    if (!string.IsNullOrEmpty(weightTextBox011.Text) && !(countTextBox011.Text == "0"))
                    {
                        sub011 = decimal.Parse(countTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
                        sub111 = sub011 + sub011 * Tax / 100;
                        money11 = Math.Round(sub011, MidpointRounding.AwayFromZero);
                        moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub111, MidpointRounding.AwayFromZero));
                        weightTextBox011.ReadOnly = true;
                        money11 = sub011;
                    }
                    else if (!(weightTextBox011.Text == "0") && !string.IsNullOrEmpty(countTextBox011.Text))
                    {
                        sub011 = decimal.Parse(weightTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
                        sub111 = sub011 + sub011 * Tax / 100;
                        money11 = Math.Round(sub011, MidpointRounding.AwayFromZero);
                        moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub111, MidpointRounding.AwayFromZero));
                        countTextBox011.ReadOnly = true;
                        money11 = sub011;
                    }
                    else
                    {

                    }
                }
                else
                {
                    unitPriceTextBox012.ReadOnly = false;
                    unitPriceTextBox012.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox011.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox011.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #region "13行目"
        private void unitPriceTextBox012_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox011.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox011.Text, @"^[a-zA-Z]+$"))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox012.Text))
                {
                    unitPriceTextBox012.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox012.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox012.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }
        #endregion
        #endregion

        #region"計算書　重量×単価"
        #region "1行目"
        private void weightTextBox0_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox0.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox0.Text) && !(weightTextBox0.Text == "0"))
            {
                int j = weightTextBox0.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight0 = Math.Round(decimal.Parse(weightTextBox0.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox0.Text = weight0.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight0 * decimal.Parse(unitPriceTextBox0.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
                }
                countTextBox0.ReadOnly = true;
            }
            else
            {
                countTextBox0.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money0 = Math.Round(sub, MidpointRounding.AwayFromZero);       //計算書は税込み
            moneyTextBox0.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "2行目"
        private void weightTextBox1_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox1.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox1.Text) && !(weightTextBox1.Text == "0"))
            {
                int j = weightTextBox1.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight1 = Math.Round(decimal.Parse(weightTextBox1.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox1.Text = weight1.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight1 * decimal.Parse(unitPriceTextBox1.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
                }
                countTextBox1.ReadOnly = true;
            }
            else
            {
                countTextBox1.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money1 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox1.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "3行目"
        private void weightTextBox2_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox2.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox2.Text) && !(weightTextBox2.Text == "0"))
            {
                int j = weightTextBox2.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight2 = Math.Round(decimal.Parse(weightTextBox2.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox2.Text = weight2.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight2 * decimal.Parse(unitPriceTextBox2.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
                }
                countTextBox2.ReadOnly = true;
            }
            else
            {
                countTextBox2.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money2 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox2.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "4行目"
        private void weightTextBox3_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox3.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox3.Text) && !(weightTextBox3.Text == "0"))
            {
                int j = weightTextBox3.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight3 = Math.Round(decimal.Parse(weightTextBox3.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox3.Text = weight3.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight3 * decimal.Parse(unitPriceTextBox3.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox3.Text) * decimal.Parse(unitPriceTextBox3.Text);
                }
                countTextBox3.ReadOnly = true;
            }
            else
            {
                countTextBox3.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money3 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox3.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "5行目"
        private void weightTextBox4_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox4.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox4.Text) && !(weightTextBox4.Text == "0"))
            {
                int j = weightTextBox4.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight4 = Math.Round(decimal.Parse(weightTextBox4.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox4.Text = weight4.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight4 * decimal.Parse(unitPriceTextBox4.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
                }
                countTextBox4.ReadOnly = true;
            }
            else
            {
                countTextBox4.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money4 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox4.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "6行目"
        private void weightTextBox5_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox5.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox5.Text) && !(weightTextBox5.Text == "0"))
            {
                int j = weightTextBox5.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight5 = Math.Round(decimal.Parse(weightTextBox5.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox5.Text = weight5.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight5 * decimal.Parse(unitPriceTextBox5.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
                }
                countTextBox5.ReadOnly = true;
            }
            else
            {
                countTextBox5.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money5 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox5.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "7行目"
        private void weightTextBox6_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox6.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox6.Text) && !(weightTextBox6.Text == "0"))
            {
                int j = weightTextBox6.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight6 = Math.Round(decimal.Parse(weightTextBox6.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox6.Text = weight6.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight6 * decimal.Parse(unitPriceTextBox6.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
                }
                countTextBox6.ReadOnly = true;
            }
            else
            {
                countTextBox6.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money6 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox6.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "8行目"
        private void weightTextBox7_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox7.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox7.Text) && !(weightTextBox7.Text == "0"))
            {
                int j = weightTextBox7.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight7 = Math.Round(decimal.Parse(weightTextBox7.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox7.Text = weight7.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight7 * decimal.Parse(unitPriceTextBox7.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
                }
                countTextBox7.ReadOnly = true;
            }
            else
            {
                countTextBox7.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money7 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox7.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "9行目"
        private void weightTextBox8_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox8.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox8.Text) && !(weightTextBox8.Text == "0"))
            {
                int j = weightTextBox8.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight8 = Math.Round(decimal.Parse(weightTextBox8.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox8.Text = weight8.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight8 * decimal.Parse(unitPriceTextBox8.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
                }
                countTextBox8.ReadOnly = true;
            }
            else
            {
                countTextBox8.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money8 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox8.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "10行目"
        private void weightTextBox9_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox9.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox9.Text) && !(weightTextBox9.Text == "0"))
            {
                int j = weightTextBox9.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight9 = Math.Round(decimal.Parse(weightTextBox9.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox9.Text = weight9.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight9 * decimal.Parse(unitPriceTextBox9.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
                }
                countTextBox9.ReadOnly = true;
            }
            else
            {
                countTextBox9.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money9 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox9.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "11行目"
        private void weightTextBox10_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox10.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox10.Text) && !(weightTextBox10.Text == "0"))
            {
                int j = weightTextBox10.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight10 = Math.Round(decimal.Parse(weightTextBox10.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox10.Text = weight10.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight10 * decimal.Parse(unitPriceTextBox10.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
                }
                countTextBox10.ReadOnly = true;
            }
            else
            {
                countTextBox10.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money10 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox10.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "12行目"
        private void weightTextBox11_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox11.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox11.Text) && !(weightTextBox11.Text == "0"))
            {
                int j = weightTextBox11.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight11 = Math.Round(decimal.Parse(weightTextBox11.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox11.Text = weight11.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight11 * decimal.Parse(unitPriceTextBox11.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
                }
                countTextBox11.ReadOnly = true;
            }
            else
            {
                countTextBox11.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money11 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox11.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "13行目"
        private void weightTextBox12_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox12.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox12.Text) && !(weightTextBox12.Text == "0"))
            {
                int j = weightTextBox12.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight12 = Math.Round(decimal.Parse(weightTextBox12.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox12.Text = weight12.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight12 * decimal.Parse(unitPriceTextBox12.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox12.Text) * decimal.Parse(unitPriceTextBox12.Text);
                }
                countTextBox12.ReadOnly = true;
            }
            else
            {
                countTextBox12.ReadOnly = false;
            }
            sub += sub * Tax / 100;
            money12 = Math.Round(sub, MidpointRounding.AwayFromZero);
            moneyTextBox12.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        }
        #endregion
        #endregion

        #region"計算書　数量×単価"
        #region "1行目"
        private void countTextBox0_Leave(object sender, EventArgs e)
        {
            int st = countTextBox0.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox0.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox0.Text) && !(countTextBox0.Text == "0"))
            {
                sub = decimal.Parse(countTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
                sub += sub * Tax / 100;
                money0 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox0.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox0.ReadOnly = true;
            }
            else
            {
                weightTextBox0.ReadOnly = false;
            }
        }
        #endregion
        #region "2行目"
        private void countTextBox1_Leave(object sender, EventArgs e)
        {
            int st = countTextBox1.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox1.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox1.Text) && !(countTextBox1.Text == "0"))
            {
                sub = decimal.Parse(countTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
                sub += sub * Tax / 100;
                money1 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox1.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox1.ReadOnly = true;
            }
            else
            {
                weightTextBox1.ReadOnly = false;
            }
        }
        #endregion
        #region "3行目"
        private void countTextBox2_Leave(object sender, EventArgs e)
        {
            int st = countTextBox2.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox2.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox2.Text) && !(countTextBox2.Text == "0"))
            {
                sub = decimal.Parse(countTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
                sub += sub * Tax / 100;
                money2 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox2.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox2.ReadOnly = true;
            }
            else
            {
                weightTextBox2.ReadOnly = false;
            }
        }
        #endregion
        #region "4行目"
        private void countTextBox3_Leave(object sender, EventArgs e)
        {
            int st = countTextBox3.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox3.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox3.Text) && !(countTextBox3.Text == "0"))
            {
                sub = decimal.Parse(countTextBox3.Text) * decimal.Parse(unitPriceTextBox3.Text);
                sub += sub * Tax / 100;
                money3 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox3.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox3.ReadOnly = true;
            }
            else
            {
                weightTextBox3.ReadOnly = false;
            }
        }
        #endregion
        #region "5行目"
        private void countTextBox4_Leave(object sender, EventArgs e)
        {
            int st = countTextBox4.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox4.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox4.Text) && !(countTextBox4.Text == "0"))
            {
                sub = decimal.Parse(countTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
                sub += sub * Tax / 100;
                money4 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox4.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox4.ReadOnly = true;
            }
            else
            {
                weightTextBox4.ReadOnly = false;
            }
        }
        #endregion
        #region "6行目"
        private void countTextBox5_Leave(object sender, EventArgs e)
        {
            int st = countTextBox5.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox5.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox5.Text) && !(countTextBox5.Text == "0"))
            {
                sub = decimal.Parse(countTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
                sub += sub * Tax / 100;
                money5 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox5.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox5.ReadOnly = true;
            }
            else
            {
                weightTextBox5.ReadOnly = false;
            }
        }
        #endregion
        #region "7行目"
        private void countTextBox6_Leave(object sender, EventArgs e)
        {
            int st = countTextBox6.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox6.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox6.Text) && !(countTextBox6.Text == "0"))
            {
                sub = decimal.Parse(countTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
                sub += sub * Tax / 100;
                money6 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox6.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox6.ReadOnly = true;
            }
            else
            {
                weightTextBox6.ReadOnly = false;
            }
        }
        #endregion
        #region "8行目"
        private void countTextBox7_Leave(object sender, EventArgs e)
        {
            int st = countTextBox7.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox7.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox7.Text) && !(countTextBox7.Text == "0"))
            {
                sub = decimal.Parse(countTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
                sub += sub * Tax / 100;
                money7 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox7.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox7.ReadOnly = true;
            }
            else
            {
                weightTextBox7.ReadOnly = false;
            }
        }
        #endregion
        #region "9行目"
        private void countTextBox8_Leave(object sender, EventArgs e)
        {
            int st = countTextBox8.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox8.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox8.Text) && !(countTextBox8.Text == "0"))
            {
                sub = decimal.Parse(countTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
                sub += sub * Tax / 100;
                money8 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox8.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox8.ReadOnly = true;
            }
            else
            {
                weightTextBox8.ReadOnly = false;
            }
        }
        #endregion
        #region "10行目"
        private void countTextBox9_Leave(object sender, EventArgs e)
        {
            int st = countTextBox9.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox9.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox9.Text) && !(countTextBox9.Text == "0"))
            {
                sub = decimal.Parse(countTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
                sub += sub * Tax / 100;
                money9 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox9.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox9.ReadOnly = true;
            }
            else
            {
                weightTextBox9.ReadOnly = false;
            }
        }
        #endregion
        #region "11行目"
        private void countTextBox10_Leave(object sender, EventArgs e)
        {
            int st = countTextBox10.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox10.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox10.Text) && !(countTextBox10.Text == "0"))
            {
                sub = decimal.Parse(countTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
                sub += sub * Tax / 100;
                money10 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox10.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox10.ReadOnly = true;
            }
            else
            {
                weightTextBox10.ReadOnly = false;
            }
        }
        #endregion
        #region "12行目"
        private void countTextBox11_Leave(object sender, EventArgs e)
        {
            int st = countTextBox11.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox11.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            if (!string.IsNullOrEmpty(countTextBox11.Text) && !(countTextBox11.Text == "0"))
            {
                sub = decimal.Parse(countTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
                sub += sub * Tax / 100;
                money11 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox11.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox11.ReadOnly = true;
            }
            else
            {
                weightTextBox11.ReadOnly = false;
            }
        }
        #endregion
        #region "13行目"
        private void countTextBox12_Leave(object sender, EventArgs e)
        {
            int st = countTextBox12.Text.IndexOf(".");
            if (st > -1)
            {
                MessageBox.Show("整数を入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox12.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(countTextBox12.Text) && !(countTextBox12.Text == "0"))
            {
                sub = decimal.Parse(countTextBox12.Text) * decimal.Parse(unitPriceTextBox12.Text);
                sub += sub * Tax / 100;
                money12 = Math.Round(sub, MidpointRounding.AwayFromZero);
                moneyTextBox12.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
                weightTextBox12.ReadOnly = true;
            }
            else
            {
                weightTextBox12.ReadOnly = false;
            }
        }
        #endregion
        #endregion

        #region "納品書　重量×単価"
        #region "重量1行目"
        private void weightTextBox00_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox00.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox00.Text) && !(weightTextBox00.Text == "0"))
            {
                int j = weightTextBox00.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight0 = Math.Round(decimal.Parse(weightTextBox00.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox00.Text = weight0.ToString();

                    //重量×単価の切捨て
                    sub00 = Math.Floor(weight0 * decimal.Parse(unitPriceTextBox00.Text));
                }
                else        //小数点なし
                {
                    sub00 = decimal.Parse(weightTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
                }
                countTextBox00.ReadOnly = true;
            }
            else
            {
                countTextBox00.ReadOnly = false;
            }
            money0 = sub00;
            sub10 = sub00 + sub00 * Tax / 100;
            moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub10, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量2行目"
        private void weightTextBox01_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox01.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox01.Text) && !(weightTextBox01.Text == "0"))
            {
                int j = weightTextBox01.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight1 = Math.Round(decimal.Parse(weightTextBox01.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox01.Text = weight1.ToString();

                    //重量×単価の切捨て
                    sub01 = Math.Floor(weight1 * decimal.Parse(unitPriceTextBox01.Text));
                }
                else        //小数点なし
                {
                    sub01 = decimal.Parse(weightTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
                }
                countTextBox01.ReadOnly = true;
            }
            else
            {
                countTextBox01.ReadOnly = false;
            }
            money1 = sub01;
            sub11 = sub01 + sub01 * Tax / 100;
            moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub11, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量3行目"
        private void weightTextBox02_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox02.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox02.Text) && !(weightTextBox02.Text == "0"))
            {
                int j = weightTextBox02.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight2 = Math.Round(decimal.Parse(weightTextBox02.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox02.Text = weight2.ToString();

                    //重量×単価の切捨て
                    sub02 = Math.Floor(weight2 * decimal.Parse(unitPriceTextBox02.Text));
                }
                else        //小数点なし
                {
                    sub02 = decimal.Parse(weightTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
                }
                countTextBox02.ReadOnly = true;
            }
            else
            {
                countTextBox02.ReadOnly = false;
            }
            money2 = sub02;
            sub12 = sub02 + sub02 * Tax / 100;
            moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub12, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量4行目"
        private void weightTextBox03_Leave(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox03.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (!string.IsNullOrEmpty(weightTextBox03.Text) && !(weightTextBox03.Text == "0"))
            {
                int j = weightTextBox03.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight3 = Math.Round(decimal.Parse(weightTextBox03.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox03.Text = weight3.ToString();

                    //重量×単価の切捨て
                    sub03 = Math.Floor(weight3 * decimal.Parse(unitPriceTextBox03.Text));
                }
                else        //小数点なし
                {
                    sub03 = decimal.Parse(weightTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
                }
                countTextBox03.ReadOnly = true;
            }
            else
            {
                countTextBox03.ReadOnly = false;
            }
            money3 = sub03;
            sub13 = sub03 + sub03 * Tax / 100;
            moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub13, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量5行目"
        private void weightTextBox04_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox04.Text) && !(weightTextBox04.Text == "0"))
            {
                int j = weightTextBox04.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight4 = Math.Round(decimal.Parse(weightTextBox04.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox04.Text = weight4.ToString();

                    //重量×単価の切捨て
                    sub04 = Math.Floor(weight4 * decimal.Parse(unitPriceTextBox04.Text));
                }
                else        //小数点なし
                {
                    sub04 = decimal.Parse(weightTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
                }
                countTextBox04.ReadOnly = true;
            }
            else
            {
                countTextBox04.ReadOnly = false;
            }
            money4 = sub04;
            sub14 = sub04 + sub04 * Tax / 100;
            moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub14, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量6行目"
        private void weightTextBox05_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox05.Text) && !(weightTextBox05.Text == "0"))
            {
                int j = weightTextBox05.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight5 = Math.Round(decimal.Parse(weightTextBox05.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox05.Text = weight5.ToString();

                    //重量×単価の切捨て
                    sub05 = Math.Floor(weight5 * decimal.Parse(unitPriceTextBox05.Text));
                }
                else        //小数点なし
                {
                    sub05 = decimal.Parse(weightTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
                }
                countTextBox05.ReadOnly = true;
            }
            else
            {
                countTextBox05.ReadOnly = false;
            }
            money5 = sub05;
            sub15 = sub05 + sub05 * Tax / 100;
            moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub15, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量7行目"
        private void weightTextBox06_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox06.Text) && !(weightTextBox06.Text == "0"))
            {
                int j = weightTextBox06.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight6 = Math.Round(decimal.Parse(weightTextBox06.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox06.Text = weight6.ToString();

                    //重量×単価の切捨て
                    sub06 = Math.Floor(weight6 * decimal.Parse(unitPriceTextBox06.Text));
                }
                else        //小数点なし
                {
                    sub06 = decimal.Parse(weightTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
                }
                countTextBox06.ReadOnly = true;
            }
            else
            {
                countTextBox06.ReadOnly = false;
            }
            money6 = sub06;
            sub16 = sub06 + sub06 * Tax / 100;
            moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub16, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量8行目"
        private void weightTextBox07_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox07.Text) && !(weightTextBox07.Text == "0"))
            {
                int j = weightTextBox07.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight7 = Math.Round(decimal.Parse(weightTextBox07.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox07.Text = weight7.ToString();

                    //重量×単価の切捨て
                    sub07 = Math.Floor(weight7 * decimal.Parse(unitPriceTextBox07.Text));
                }
                else        //小数点なし
                {
                    sub07 = decimal.Parse(weightTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
                }
                countTextBox07.ReadOnly = true;
            }
            else
            {
                countTextBox07.ReadOnly = false;
            }
            money7 = sub07;
            sub17 = sub07 + sub07 * Tax / 100;
            moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub17, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量9行目"
        private void weightTextBox08_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox08.Text) && !(weightTextBox08.Text == "0"))
            {
                int j = weightTextBox08.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight8 = Math.Round(decimal.Parse(weightTextBox08.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox08.Text = weight8.ToString();

                    //重量×単価の切捨て
                    sub08 = Math.Floor(weight8 * decimal.Parse(unitPriceTextBox08.Text));
                }
                else        //小数点なし
                {
                    sub08 = decimal.Parse(weightTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
                }
                countTextBox08.ReadOnly = true;
            }
            else
            {
                countTextBox08.ReadOnly = false;
            }
            money8 = sub08;
            sub18 = sub08 + sub08 * Tax / 100;
            moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub18, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量10行目"
        private void weightTextBox09_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox09.Text) && !(weightTextBox09.Text == "0"))
            {
                int j = weightTextBox09.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight9 = Math.Round(decimal.Parse(weightTextBox09.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox09.Text = weight9.ToString();

                    //重量×単価の切捨て
                    sub09 = Math.Floor(weight9 * decimal.Parse(unitPriceTextBox09.Text));
                }
                else        //小数点なし
                {
                    sub09 = decimal.Parse(weightTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
                }
                countTextBox09.ReadOnly = true;
            }
            else
            {
                countTextBox09.ReadOnly = false;
            }
            money9 = sub09;
            sub19 = sub09 + sub09 * Tax / 100;
            moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub19, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量11行目"
        private void weightTextBox010_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox010.Text) && !(weightTextBox010.Text == "0"))
            {
                int j = weightTextBox010.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight10 = Math.Round(decimal.Parse(weightTextBox010.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox010.Text = weight10.ToString();

                    //重量×単価の切捨て
                    sub010 = Math.Floor(weight10 * decimal.Parse(unitPriceTextBox010.Text));
                }
                else        //小数点なし
                {
                    sub010 = decimal.Parse(weightTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
                }
                countTextBox010.ReadOnly = true;
            }
            else
            {
                countTextBox010.ReadOnly = false;
            }
            money10 = sub010;
            sub110 = sub010 + sub010 * Tax / 100;
            moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub110, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量12行目"
        private void weightTextBox011_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox011.Text) && !(weightTextBox011.Text == "0"))
            {
                int j = weightTextBox011.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight11 = Math.Round(decimal.Parse(weightTextBox011.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox011.Text = weight11.ToString();

                    //重量×単価の切捨て
                    sub011 = Math.Floor(weight11 * decimal.Parse(unitPriceTextBox011.Text));
                }
                else        //小数点なし
                {
                    sub011 = decimal.Parse(weightTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
                }
                countTextBox011.ReadOnly = true;
            }
            else
            {
                countTextBox011.ReadOnly = false;
            }
            money11 = sub011;
            sub111 = sub011 + sub011 * Tax / 100;
            moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub111, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "重量13行目"
        private void weightTextBox012_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox012.Text) && !(weightTextBox012.Text == "0"))
            {
                int j = weightTextBox012.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight12 = Math.Round(decimal.Parse(weightTextBox012.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox012.Text = weight12.ToString();

                    //重量×単価の切捨て
                    sub012 = Math.Floor(weight12 * decimal.Parse(unitPriceTextBox012.Text));
                }
                else        //小数点なし
                {
                    sub012 = decimal.Parse(weightTextBox012.Text) * decimal.Parse(unitPriceTextBox012.Text);
                }
                countTextBox012.ReadOnly = true;
            }
            else
            {
                countTextBox012.ReadOnly = false;
            }
            money12 = sub012;
            sub112 = sub012 + sub012 * Tax / 100;
            moneyTextBox012.Text = string.Format("{0:C}", Math.Round(sub112, MidpointRounding.AwayFromZero));
        }
        #endregion
        #endregion

        #region　"納品書　数量×単価"
        #region "数量1行目"
        private void countTextBox00_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox00.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox00.Text) && !(countTextBox00.Text == "0"))
                {
                    sub00 = decimal.Parse(countTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
                    weightTextBox00.ReadOnly = true;
                }
                else
                {
                    weightTextBox00.ReadOnly = false;
                }
                money0 = sub00;
                sub10 = sub00 + sub00 * Tax / 100;
                moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub10, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "数量2行目"
        private void countTextBox01_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox01.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox01.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox01.Text) && !(countTextBox01.Text == "0"))
                {
                    sub01 = decimal.Parse(countTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
                    weightTextBox01.ReadOnly = true;
                }
                else
                {
                    weightTextBox01.ReadOnly = false;
                }
                money1 = sub01;
                sub11 = sub01 + sub01 * Tax / 100;
                moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub11, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "数量3行目"
        private void countTextBox02_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox02.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox02.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox02.Text) && !(countTextBox02.Text == "0"))
                {
                    sub02 = decimal.Parse(countTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
                    weightTextBox02.ReadOnly = true;
                }
                else
                {
                    weightTextBox02.ReadOnly = false;
                }
                money2 = sub02;
                sub12 = sub02 + sub02 * Tax / 100;
                moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub12, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "数量4行目"
        private void countTextBox03_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox03.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox03.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox03.Text) && !(countTextBox03.Text == "0"))
                {
                    sub03 = decimal.Parse(countTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
                    weightTextBox03.ReadOnly = true;
                }
                else
                {
                    weightTextBox03.ReadOnly = false;
                }
                money3 = sub03;
                sub13 = sub03 + sub03 * Tax / 100;
                moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub13, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "数量5行目"
        private void countTextBox04_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox04.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox04.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox04.Text) && !(countTextBox04.Text == "0"))
                {
                    sub04 = decimal.Parse(countTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
                    weightTextBox04.ReadOnly = true;
                }
                else
                {
                    weightTextBox04.ReadOnly = false;
                }
                money4 = sub04;
                sub14 = sub04 + sub04 * Tax / 100;
                moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub14, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "数量6行目"
        private void countTextBox05_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox05.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox05.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox05.Text) && !(countTextBox05.Text == "0"))
                {
                    sub05 = decimal.Parse(countTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
                    weightTextBox05.ReadOnly = true;
                }
                else
                {
                    weightTextBox05.ReadOnly = false;
                }
                money5 = sub05;
                sub15 = sub05 + sub05 * Tax / 100;
                moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub15, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "数量7行目"
        private void countTextBox06_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox06.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox06.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox06.Text) && !(countTextBox06.Text == "0"))
                {
                    sub06 = decimal.Parse(countTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
                    weightTextBox06.ReadOnly = true;
                }
                else
                {
                    weightTextBox06.ReadOnly = false;
                }
                money6 = sub06;
                sub16 = sub06 + sub06 * Tax / 100;
                moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub16, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "数量8行目"
        private void countTextBox07_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox07.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox07.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox07.Text) && !(countTextBox07.Text == "0"))
                {
                    sub07 = decimal.Parse(countTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
                    weightTextBox07.ReadOnly = true;
                }
                else
                {
                    weightTextBox07.ReadOnly = false;
                }
                money7 = sub07;
                sub17 = sub07 + sub07 * Tax / 100;
                moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub17, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "数量9行目"
        private void countTextBox08_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox08.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox08.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox08.Text) && !(countTextBox08.Text == "0"))
                {
                    sub08 = decimal.Parse(countTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
                    weightTextBox08.ReadOnly = true;
                }
                else
                {
                    weightTextBox08.ReadOnly = false;
                }
                money8 = sub08;
                sub18 = sub08 + sub08 * Tax / 100;
                moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub18, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "数量10行目"
        private void countTextBox09_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox09.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox09.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox09.Text) && !(countTextBox09.Text == "0"))
                {
                    sub09 = decimal.Parse(countTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
                    weightTextBox09.ReadOnly = true;
                }
                else
                {
                    weightTextBox09.ReadOnly = false;
                }
                money9 = sub09;
                sub19 = sub09 + sub09 * Tax / 100;
                moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub19, MidpointRounding.AwayFromZero));
            }
            
        }
        #endregion
        #region "数量11行目"
        private void countTextBox010_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox010.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox010.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox010.Text) && !(countTextBox010.Text == "0"))
                {
                    sub010 = decimal.Parse(countTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
                    weightTextBox010.ReadOnly = true;
                }
                else
                {
                    weightTextBox010.ReadOnly = false;
                }
                money10 = sub010;
                sub110 = sub010 + sub010 * Tax / 100;
                moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub110, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "数量12行目"
        private void countTextBox011_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox011.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox011.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox011.Text) && !(countTextBox011.Text == "0"))
                {
                    sub011 = decimal.Parse(countTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
                    weightTextBox011.ReadOnly = true;
                }
                else
                {
                    weightTextBox011.ReadOnly = false;
                }
                money11 = sub011;
                sub111 = sub011 + sub011 * Tax / 100;
                moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub111, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "数量13行目"
        private void countTextBox012_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                int co = countTextBox012.Text.IndexOf(".");
                if (co > -1)
                {
                    MessageBox.Show("整数を入力して下さい。");
                    return;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox012.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                else if (!string.IsNullOrEmpty(countTextBox012.Text) && !(countTextBox012.Text == "0"))
                {
                    sub012 = decimal.Parse(countTextBox012.Text) * decimal.Parse(unitPriceTextBox012.Text);
                    weightTextBox012.ReadOnly = true;
                }
                else
                {
                    weightTextBox012.ReadOnly = false;
                }
                money12 = sub012;
                sub112 = sub012 + sub012 * Tax / 100;
                moneyTextBox012.Text = string.Format("{0:C}", Math.Round(sub112, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #endregion

        #region"納品書　金額が入力されたら総重量 or 総数計算、自動計算"
        #region "1行目"
        private void moneyTextBox00_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox00.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                //重量×単価
                else if (!string.IsNullOrEmpty(weightTextBox00.Text) && string.IsNullOrEmpty(countTextBox00.Text))
                {
                    countTextBox00.Text = 0.ToString();

                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox00.Text) && string.IsNullOrEmpty(weightTextBox00.Text))
                {
                    weightTextBox00.Text = 0.ToString();
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    return;
                }
                #region "顧客選択から戻ってきた時"
                if (amount00 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["upd_date"].ToString();
                    #endregion

                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 1 + " and upd_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money0 = (decimal)dataRow1["amount"] / 1.1m;

                }
                #endregion
                countsum00 = int.Parse(countTextBox00.Text);
                weisum00 = decimal.Parse(weightTextBox00.Text);
                subSum00 = money0;
                #region "金額合計の場合わけ"
                if (subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
                {
                    subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
                }
                else
                {
                    subSum = subSum00;
                }
                #endregion
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;
                #region "数量の場合わけ"
                if (countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
                {
                    countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
                }
                else
                {
                    countsum = countsum00;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
                {
                    weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
                }
                else
                {
                    weisum = weisum00;
                }
                #endregion
                
                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "2行目"
        private void moneyTextBox01_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox01.Text) && string.IsNullOrEmpty(countTextBox01.Text))
                {
                    countTextBox01.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox01.Text) && string.IsNullOrEmpty(weightTextBox01.Text))
                {
                    weightTextBox01.Text = 0.ToString();
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    return;
                }
                #region "顧客選択から戻ってきた時"
                if (amount01 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["upd_date"].ToString();
                    #endregion
                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 2 + " and upd_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money1 = (decimal)dataRow1["amount"] / 1.1m;
                }
                #endregion

                countsum01 = int.Parse(countTextBox01.Text);
                weisum01 = decimal.Parse(weightTextBox01.Text);
                subSum01 = money1;       //納品書は税抜き
                #region "金額合計の場合わけ"
                if (subSum00 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
                {
                    subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
                }
                else
                {
                    subSum = subSum01;
                }
                #endregion
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;
                #region "数量の場合わけ"
                if (countsum00 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
                {
                    countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
                }
                else
                {
                    countsum = countsum01;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum00 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
                {
                    weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
                }
                else
                {
                    weisum = weisum01;
                }
                #endregion
                

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            
        }
        #endregion
        #region "3行目"
        private void moneyTextBox02_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox02.Text) && string.IsNullOrEmpty(countTextBox02.Text))
                {
                    countTextBox02.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox02.Text) && string.IsNullOrEmpty(weightTextBox02.Text))
                {
                    weightTextBox02.Text = 0.ToString();
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    return;
                }
                #region "顧客選択から戻ってきた時"
                if (amount02 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["upd_date"].ToString();
                    #endregion
                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 3 + " and upd_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money2 = (decimal)dataRow1["amount"] / 1.1m;
                }
                #endregion
                countsum02 = int.Parse(countTextBox02.Text);
                weisum02 = decimal.Parse(weightTextBox02.Text);
                subSum02 = money2;
                #region "金額合計の場合わけ"
                if (subSum00 != 0 || subSum01 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
                {
                    subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
                }
                else
                {
                    subSum = subSum02;
                }
                #endregion

                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;
                #region "数量の場合わけ"
                if (countsum00 != 0 || countsum01 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
                {
                    countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
                }
                else
                {
                    countsum = countsum02;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum00 != 0 || weisum01 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
                {
                    weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
                }
                else
                {
                    weisum = weisum02;
                }
                #endregion

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            
        }
        #endregion
        #region "4行目"
        private void moneyTextBox03_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox03.Text) && string.IsNullOrEmpty(countTextBox03.Text))
                {
                    countTextBox03.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox03.Text) && string.IsNullOrEmpty(weightTextBox03.Text))
                {
                    weightTextBox03.Text = 0.ToString();
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    return;
                }
                #region "顧客選択から戻ってきた時"
                if (amount03 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["upd_date"].ToString();
                    #endregion
                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 4 + " and upd_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money3 = (decimal)dataRow1["amount"] / 1.1m;
                }
                #endregion
                countsum03 = int.Parse(countTextBox03.Text);
                weisum03 = decimal.Parse(weightTextBox03.Text);
                subSum03 = money3;     //増やしていく
                #region "金額合計の場合わけ"
                if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
                {
                    subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
                }
                else
                {
                    subSum = subSum03;
                }
                #endregion

                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;
                #region "数量の場合わけ"
                if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
                {
                    countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
                }
                else
                {
                    countsum = countsum03;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
                {
                    weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
                }
                else
                {
                    weisum = weisum03;
                }
                #endregion

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "5行目"
        private void moneyTextBox04_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox04.Text) && string.IsNullOrEmpty(countTextBox04.Text))
                {
                    countTextBox04.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox04.Text) && string.IsNullOrEmpty(weightTextBox04.Text))
                {
                    weightTextBox04.Text = 0.ToString();
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    return;
                }
                #region "顧客選択から戻ってきた時"
                if (amount04 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["upd_date"].ToString();
                    #endregion
                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 5 + " and upd_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money4 = (decimal)dataRow1["amount"] / 1.1m;
                }
                #endregion
                countsum04 = int.Parse(countTextBox04.Text);
                weisum04 = decimal.Parse(weightTextBox04.Text);
                subSum04 = money4;     //増やしていく
                #region "金額合計の場合わけ"
                if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
                {
                    subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
                }
                else
                {
                    subSum = subSum04;
                }
                #endregion
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;
                #region "数量の場合わけ"
                if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
                {
                    countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
                }
                else
                {
                    countsum = countsum04;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
                {
                    weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
                }
                else
                {
                    weisum = weisum04;
                }
                #endregion

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "6行目"
        private void moneyTextBox05_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox05.Text) && string.IsNullOrEmpty(countTextBox05.Text))
                {
                    countTextBox05.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox05.Text) && string.IsNullOrEmpty(weightTextBox05.Text))
                {
                    weightTextBox05.Text = 0.ToString();
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    return;
                }
                #region "顧客選択から戻ってきた時"
                if (amount05 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["upd_date"].ToString();
                    #endregion
                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 6 + " and upd_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money5 = (decimal)dataRow1["amount"] / 1.1m;
                }
                #endregion
                countsum05 = int.Parse(countTextBox05.Text);
                weisum05 = decimal.Parse(weightTextBox05.Text);
                subSum05 = money5;     //増やしていく
                #region "金額合計の場合わけ"
                if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
                {
                    subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
                }
                else
                {
                    subSum = subSum05;
                }
                #endregion
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;
                #region "数量の場合わけ"
                if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
                {
                    countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
                }
                else
                {
                    countsum = countsum05;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
                {
                    weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
                }
                else
                {
                    weisum = weisum05;
                }
                #endregion

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "7行目"
        private void moneyTextBox06_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox06.Text) && string.IsNullOrEmpty(countTextBox06.Text))
                {
                    countTextBox06.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox06.Text) && string.IsNullOrEmpty(weightTextBox06.Text))
                {
                    weightTextBox06.Text = 0.ToString();
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    return;
                }
                #region "顧客選択から戻ってきた時"
                if (amount06 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["upd_date"].ToString();
                    #endregion
                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 7 + " and upd_date = '"  + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money6 = (decimal)dataRow1["amount"] / 1.1m;
                }
                #endregion
                countsum06 = int.Parse(countTextBox06.Text);
                weisum06 = decimal.Parse(weightTextBox06.Text);
                subSum06 = money6;     //増やしていく
                #region "金額合計の場合わけ"
                if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
                {
                    subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
                }
                else
                {
                    subSum = subSum06;
                }
                #endregion
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;
                #region "数量の場合わけ"
                if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
                {
                    countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
                }
                else
                {
                    countsum = countsum06;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
                {
                    weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
                }
                else
                {
                    weisum = weisum06;
                }
                #endregion

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "8行目"
        private void moneyTextBox07_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {

            }
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox07.Text) && string.IsNullOrEmpty(countTextBox07.Text))
            {
                countTextBox07.Text = 0.ToString();
            }
            //数量×単価
            else if (!string.IsNullOrEmpty(countTextBox07.Text) && string.IsNullOrEmpty(weightTextBox07.Text))
            {
                weightTextBox07.Text = 0.ToString();
            }
            else
            {
                MessageBox.Show("数値を入力してください");
                return;
            }
            #region "顧客選択から戻ってきた時"
            if (amount07 != 0)
            {
                #region "日付呼び出し"
                DataTable upddt = new DataTable();
                string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                adapter.Fill(upddt);
                DataRow dataRow2;
                dataRow2 = upddt.Rows[0];
                string date = dataRow2["upd_date"].ToString();
                #endregion
                DataTable dt10 = new DataTable();
                string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 8 + " and upd_date = '" + date + "';";
                adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                adapter.Fill(dt10);
                DataRow dataRow1;
                dataRow1 = dt10.Rows[0];
                money7 = (decimal)dataRow1["amount"] / 1.1m;
            }
            #endregion
            countsum07 = int.Parse(countTextBox07.Text);
            weisum07 = decimal.Parse(weightTextBox07.Text);
            subSum07 = money7;     //増やしていく
            #region "金額合計の場合わけ"
            if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
            {
                subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
            }
            else
            {
                subSum = subSum07;
            }
            #endregion
            TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
            sum = subSum + TaxAmount;
            #region "数量の場合わけ"
            if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
            {
                countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
            }
            else
            {
                countsum = countsum07;
            }
            #endregion
            #region "重量の場合わけ"
            if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
            {
                weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
            }
            else
            {
                weisum = weisum07;
            }
            #endregion

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "9行目"
        private void moneyTextBox08_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {

            }
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox08.Text) && string.IsNullOrEmpty(countTextBox08.Text))
            {
                countTextBox08.Text = 0.ToString();
            }
            //数量×単価
            else if (!string.IsNullOrEmpty(countTextBox08.Text) && string.IsNullOrEmpty(weightTextBox08.Text))
            {
                weightTextBox08.Text = 0.ToString();
            }
            else
            {
                MessageBox.Show("数値を入力してください");
                return;
            }
            #region "顧客選択から戻ってきた時"
            if (amount08 != 0)
            {
                #region "日付呼び出し"
                DataTable upddt = new DataTable();
                string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                adapter.Fill(upddt);
                DataRow dataRow2;
                dataRow2 = upddt.Rows[0];
                string date = dataRow2["upd_date"].ToString();
                #endregion
                DataTable dt10 = new DataTable();
                string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 9 + " and upd_date = '" + date + "';";
                adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                adapter.Fill(dt10);
                DataRow dataRow1;
                dataRow1 = dt10.Rows[0];
                money8 = (decimal)dataRow1["amount"] / 1.1m;
            }
            #endregion
            countsum08 = int.Parse(countTextBox08.Text);
            weisum08 = decimal.Parse(weightTextBox08.Text);
            subSum08 = money8;     //増やしていく
            #region "金額合計の場合わけ"
            if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
            {
                subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
            }
            else
            {
                subSum = subSum08;
            }
            #endregion
            TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
            sum = subSum + TaxAmount;
            #region "数量の場合わけ"
            if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
            {
                countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
            }
            else
            {
                countsum = countsum08;
            }
            #endregion
            #region "重量の場合わけ"
            if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
            {
                weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
            }
            else
            {
                weisum = weisum08;
            }
            #endregion

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        #endregion
        #region "10行目"
        private void moneyTextBox09_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox09.Text) && string.IsNullOrEmpty(countTextBox09.Text))
                {
                    countTextBox09.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox09.Text) && string.IsNullOrEmpty(weightTextBox09.Text))
                {
                    weightTextBox09.Text = 0.ToString();
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    return;
                }
                #region "顧客選択から戻ってきた時"
                if (amount09 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["upd_date"].ToString();
                    #endregion
                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 10 + " and upd_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money9 = (decimal)dataRow1["amount"] / 1.1m;
                }
                #endregion
                countsum09 = int.Parse(countTextBox09.Text);
                weisum09 = decimal.Parse(weightTextBox09.Text);
                subSum09 = money9;
                #region "金額合計の場合わけ"
                if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
                {
                    subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
                }
                else
                {
                    subSum = subSum09;
                }
                #endregion
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;
                #region "数量の場合わけ"
                if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
                {
                    countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
                }
                else
                {
                    countsum = countsum09;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
                {
                    weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
                }
                else
                {
                    weisum = weisum09;
                }
                #endregion

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "11行目"
        private void moneyTextBox010_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox010.Text) && string.IsNullOrEmpty(countTextBox010.Text))
                {
                    countTextBox010.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox010.Text) && string.IsNullOrEmpty(weightTextBox010.Text))
                {
                    weightTextBox010.Text = 0.ToString();
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    return;
                }
                #region "顧客選択から戻ってきた時"
                if (amount010 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["upd_date"].ToString();
                    #endregion
                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 11 + " and upd_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money10 = (decimal)dataRow1["amount"] / 1.1m;
                }
                #endregion
                countsum010 = int.Parse(countTextBox010.Text);
                weisum010 = decimal.Parse(weightTextBox010.Text);
                subSum010 = money10;
                #region "金額合計の場合わけ"
                if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum011 != 0 || subSum012 != 0)
                {
                    subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
                }
                else
                {
                    subSum = subSum010;
                }
                #endregion
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;
                #region "数量の場合わけ"
                if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum011 != 0 || countsum012 != 0)
                {
                    countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
                }
                else
                {
                    countsum = countsum010;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum011 != 0 || weisum012 != 0)
                {
                    weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
                }
                else
                {
                    weisum = weisum010;
                }
                #endregion

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "12行目"
        private void moneyTextBox011_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox011.Text) && string.IsNullOrEmpty(countTextBox011.Text))
                {
                    countTextBox011.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox011.Text) && string.IsNullOrEmpty(weightTextBox011.Text))
                {
                    weightTextBox011.Text = 0.ToString();
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    return;
                }
                #region "顧客選択から戻ってきた時"
                if (amount011 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["upd_date"].ToString();
                    #endregion
                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 12 + " and upd_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money11 = (decimal)dataRow1["amount"] / 1.1m;
                }
                #endregion
                countsum011 = int.Parse(countTextBox011.Text);
                weisum011 = decimal.Parse(weightTextBox011.Text);
                subSum011 = money11;     //増やしていく
                #region "金額合計の場合わけ"
                if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum012 != 0)
                {
                    subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
                }
                else
                {
                    subSum = subSum011;
                }
                #endregion
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;
                #region "数量の場合わけ"
                if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum012 != 0)
                {
                    countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
                }
                else
                {
                    countsum = countsum011;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum012 != 0)
                {
                    weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
                }
                else
                {
                    weisum = weisum011;
                }
                #endregion

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #region "13行目"
        private void moneyTextBox012_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox012.Text) && string.IsNullOrEmpty(countTextBox012.Text))
                {
                    countTextBox012.Text = 0.ToString();
                }
                //数量×単価
                else if (!string.IsNullOrEmpty(countTextBox012.Text) && string.IsNullOrEmpty(weightTextBox012.Text))
                {
                    weightTextBox012.Text = 0.ToString();
                }
                else
                {
                    MessageBox.Show("数値を入力してください");
                    return;
                }
                #region "顧客選択から戻ってきた時"
                if (amount012 != 0)
                {
                    #region "日付呼び出し"
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                    DataRow dataRow2;
                    dataRow2 = upddt.Rows[0];
                    string date = dataRow2["upd_date"].ToString();
                    #endregion
                    DataTable dt10 = new DataTable();
                    string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 13 + " and upd_date = '" + date + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                    adapter.Fill(dt10);
                    DataRow dataRow1;
                    dataRow1 = dt10.Rows[0];
                    money12 = (decimal)dataRow1["amount"] / 1.1m;

                }
                #endregion
                countsum012 = int.Parse(countTextBox012.Text);
                weisum012 = decimal.Parse(weightTextBox012.Text);
                subSum012 = money12;     //増やしていく
                #region "金額合計の場合わけ"
                if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0)
                {
                    subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
                }
                else
                {
                    subSum = subSum012;
                }
                #endregion
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;
                #region "数量の場合わけ"
                if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0)
                {
                    countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
                }
                else
                {
                    countsum = countsum012;
                }
                #endregion
                #region "重量の場合わけ"
                if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0)
                {
                    weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
                }
                else
                {
                    weisum = weisum012;
                }
                #endregion

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        #endregion
        #endregion

        #endregion

        #region"右上の×で戻る"
        private void Statement_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan && data == "S")
            {
                document = documentNumberTextBox.Text;
                DataSearchResults dataSearchResults = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, data, pass, document, control, antiqueNumber, documentNumber);
                dataSearchResults.Show();
            }
            else if (screan && data == "D")
            {
                control = int.Parse(documentNumberTextBox2.Text);
                DataSearchResults dataSearchResults = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, data, pass, document, control, antiqueNumber, documentNumber);
                dataSearchResults.Show();
            }
            else if (screan)
            {
                mainMenu.Show();
            }              
        }
        #endregion

        #region"納品書　登録ボタン"
        private void Register_Click(object sender, EventArgs e)
        {
            #region "ありうるミス"
            if (string.IsNullOrEmpty(name.Text))
            {
                MessageBox.Show("宛名を入力して下さい。");
                return;
            }

            if (titleComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("敬称を選んで下さい。");
                return;
            }
            if (typeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("種別を選んで下さい。");
                return;
            }
            if (paymentMethodComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("お支払方法を選んで下さい。");
                return;
            }
            if (paymentMethodComboBox.Text == "振込")
            {
                if (string.IsNullOrEmpty(PayeeTextBox1.Text))
                {
                    MessageBox.Show("振込先を入力して下さい。");
                    return;
                }
            }
            #endregion
            DialogResult dr = MessageBox.Show("登録しますか？", "登録確認", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }
            if (regist >= 1 && access_auth != "C" || data == "D" && access_auth != "C")
            {
                int ControlNumber = number;

                decimal TotalWeight = weisum;
                int Amount = countsum;
                decimal SubTotal = subSum + TaxAmount;
                decimal Total = sum;
                //vat（税込み税抜きどちらか）, vat_rate（税率）
                string vat = this.comboBox11.SelectedItem.ToString();
                int vat_rate = (int)Tax;

                string seaal = "";
                //印鑑印刷
                if (sealY.Checked == true)
                {
                    seaal = "する";
                }
                if (sealN.Checked == true)
                {
                    seaal = "しない";
                }
                #region "再登録用に繋げる"
                NpgsqlConnection connDelivery = new NpgsqlConnection();
                connDelivery.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                NpgsqlDataAdapter adapterDelivery;
                DataTable DeliveryDt = new DataTable();
                string str_sql_re = "select * from delivery_calc where control_number = " + number + ";";
                adapterDelivery = new NpgsqlDataAdapter(str_sql_re, connDelivery);
                adapterDelivery.Fill(DeliveryDt);
                int Re = DeliveryDt.Rows.Count;
                #endregion
                string OrderDate = orderDateTimePicker.Text;
                string DeliveryDate = DeliveryDateTimePicker.Text;
                string SettlementDate = SettlementDateTimePicker.Text;
                string PaymentMethod = paymentMethodComboBox.SelectedItem.ToString();
                string Name = name.Text;                                           //宛名
                string Title = titleComboBox.SelectedItem.ToString();              //敬称
                string Type = typeComboBox.SelectedItem.ToString();                //納品書or請求書
                string payee = PayeeTextBox1.Text;                                 //振り込み先
                string coin = CoinComboBox.SelectedItem.ToString();                //通貨
                string remark = RemarkRegister.Text;
                string Reason = this.textBox2.Text;
                DateTime dat1 = DateTime.Now;
                DateTime dtToday = dat1.Date;
                string c = dtToday.ToString("yyyy年MM月dd日");

                #region "法人"
                if (type == 0)
                {
                    NpgsqlConnection connA = new NpgsqlConnection();
                    connA.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                    NpgsqlDataAdapter adapterA;
                    DataTable clientDt = new DataTable();
                    string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapterA = new NpgsqlDataAdapter(str_sql_corporate, connA);
                    adapterA.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    int antique = (int)row2["antique_number"];

                    NpgsqlConnection conn0 = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter0;
                    conn0.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                    DataTable dt = new DataTable();
                    string sql_str = "UPDATE delivery_m SET staff_code = " + staff_id + ", sub_total = '" + SubTotal + "', vat = '" + vat + "', vat_rate = '" + vat_rate + "', vat_amount = '" + TaxAmount + "', total = '" + Total + "', name = '" + Name + "', honorific_title = '" + Title + "', type = '" + Type + "', order_date = '" + OrderDate + "', delivery_date = '" + DeliveryDate + "', settlement_date = '" + SettlementDate + "', seaal_print = '" + seaal + "', payment_method = '" + PaymentMethod + "', account_payble = '" + payee + "', currency = '" + coin + "', remarks2 = '" + remark + "', total_count = '" + Amount + "', total_weight = '" + TotalWeight + "', types1 = 0, reason = '" + Reason + "', registration_date = '" + c + "' where control_number = " + ControlNumber + " and antique_number = " + antique + ";";
                    adapter0 = new NpgsqlDataAdapter(sql_str, conn0);
                    adapter0.Fill(dt);

                }
                #endregion
                #region "個人"
                if (type == 1)
                {
                    NpgsqlConnection connI = new NpgsqlConnection();
                    connI.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                    NpgsqlDataAdapter adapterI;
                    DataTable clientDt = new DataTable();
                    string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapterI = new NpgsqlDataAdapter(str_sql_individual, connI);
                    adapterI.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    int ID = (int)row2["id_number"];

                    NpgsqlConnection conn1 = new NpgsqlConnection();
                    NpgsqlDataAdapter adapter1;
                    conn1.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                    DataTable dt = new DataTable();
                    string sql_str = "UPDATE delivery_m SET staff_code = " + staff_id + ", sub_total = '" + SubTotal + "', vat = '" + vat + "', vat_rate = '" + vat_rate + "', vat_amount = '" + TaxAmount + "', total = '" + Total + "', name  = '" + Name + "', honorific_title = '" + Title + "', type = '" + Type + "', order_date '" + OrderDate + "', delivery_date = '" + DeliveryDate + "', settlement_date = '" + SettlementDate + "', seaal_print = '" + seaal + "', payment_method = '" + PaymentMethod + "', account_payble  = '" + payee + "', currency = '" + coin + "', remarks2 = '" + remark + "', total_count = '" + Amount + "', total_weight = '" + TotalWeight + "', types1 = 1, reason = '" + Reason + "', registration_date = '" + c + "' where control_number = " + ControlNumber + " and id_number = " + ID + ";";
                    adapter1 = new NpgsqlDataAdapter(sql_str, conn1);
                    adapter1.Fill(dt);
                }
                #endregion

                /*NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;

                DataTable dt = new DataTable();
                string sql_str = "Insert into delivery_m (control_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seaal_print, payment_method, account_payble, currency, remarks2, total_count, total_weight, types1) VALUES ( '" + ControlNumber + "','" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + Amount + "','" + TotalWeight + "'," + type + ");";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);*/
                #region "1行目"
                //管理番号：ControlNumber
                int record = 1;     //行数
                                    //int mainCategory = mainCategoryCode00;
                                    //int item = itemCode00;
                #region "大分類コード"
                DataTable maindt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_main = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox00.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main, conn);
                adapter.Fill(maindt);
                DataRow dataRow;
                dataRow = maindt.Rows[0];
                int mainCategory = (int)dataRow["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable itemdt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_item = "select * from item_m where item_name = '" + itemComboBox00.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item, conn);
                adapter.Fill(itemdt);
                DataRow dataRow1;
                dataRow1 = itemdt.Rows[0];
                int item = (int)dataRow1["item_code"];
                #endregion
                string Detail = itemDetail00.Text;
                decimal Weight = decimal.Parse(weightTextBox00.Text);
                int Count = int.Parse(countTextBox00.Text);
                decimal UnitPrice = decimal.Parse(unitPriceTextBox00.Text);
                decimal amount = money0 + money0 * Tax / 100;
                string Remarks = remarks00.Text;

                DataTable dt2 = new DataTable();
                string sql_str2 = "UPDATE delivery_calc SET item_code = " + item + " ,weight = " + Weight + " , count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = '" + mainCategory + "',detail = '" + Detail + "' , reason = ' " + Reason + "' where control_number = " + ControlNumber +" and record_number = " + record + ";";

                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();
                adapter = new NpgsqlDataAdapter(sql_str2, conn);
                adapter.Fill(dt2);
                #endregion
                #region "2行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox01.Text) && !(unitPriceTextBox01.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 2)
                    {
                        record = 2;
                        //mainCategory = mainCategoryCode01;
                        //item = itemCode01;
                        #region "大分類コード"
                        DataTable main1dt = new DataTable();
                        string sql_main1 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox01.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main1, conn);
                        adapter.Fill(main1dt);
                        DataRow dataRow2;
                        dataRow2 = main1dt.Rows[0];
                        int mainCategory1 = (int)dataRow2["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item1dt = new DataTable();
                        string sql_item1 = "select * from item_m where item_name = '" + itemComboBox01.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item1, conn);
                        adapter.Fill(item1dt);
                        DataRow dataRow3;
                        dataRow3 = item1dt.Rows[0];
                        int item1 = (int)dataRow3["item_code"];
                        #endregion
                        Detail = itemDetail01.Text;
                        Weight = decimal.Parse(weightTextBox01.Text);
                        Count = int.Parse(countTextBox01.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox01.Text);
                        amount = money1 + money1 * Tax / 100;  //decimal.Parse(moneyTextBox01.Text);
                        Remarks = remarks01.Text;

                        DataTable dt4 = new DataTable();
                        string sql_str4 = "UPDATE delivery_calc SET item_code = " + item1 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory1 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

                        adapter = new NpgsqlDataAdapter(sql_str4, conn);
                        adapter.Fill(dt4);
                    }
                    else
                    {
                        record = 2;
                        //mainCategory = mainCategoryCode01;
                        //item = itemCode01;
                        #region "大分類コード"
                        DataTable main1dt = new DataTable();
                        string sql_main1 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox01.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main1, conn);
                        adapter.Fill(main1dt);
                        DataRow dataRow2;
                        dataRow2 = main1dt.Rows[0];
                        int mainCategory1 = (int)dataRow2["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item1dt = new DataTable();
                        string sql_item1 = "select * from item_m where item_name = '" + itemComboBox01.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item1, conn);
                        adapter.Fill(item1dt);
                        DataRow dataRow3;
                        dataRow3 = item1dt.Rows[0];
                        int item1 = (int)dataRow3["item_code"];
                        #endregion
                        Detail = itemDetail01.Text;
                        Weight = decimal.Parse(weightTextBox01.Text);
                        Count = int.Parse(countTextBox01.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox01.Text);
                        amount = money1 + money1 * Tax / 100;  //decimal.Parse(moneyTextBox01.Text);
                        Remarks = remarks01.Text;                        

                        DataTable dt4 = new DataTable();
                        string sql_str4 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item1 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory1 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str4, conn);
                        adapter.Fill(dt4);
                    }
                    
                }
                #endregion
                #region "3行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox02.Text) && !(unitPriceTextBox02.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 3)
                    {
                        record = 3;
                        //mainCategory = mainCategoryCode02;
                        //item = itemCode02;
                        #region "大分類コード"
                        DataTable main2dt = new DataTable();
                        string sql_main2 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox02.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main2, conn);
                        adapter.Fill(main2dt);
                        DataRow dataRow4;
                        dataRow4 = main2dt.Rows[0];
                        int mainCategory2 = (int)dataRow4["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item1dt = new DataTable();
                        string sql_item2 = "select * from item_m where item_name = '" + itemComboBox02.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item2, conn);
                        adapter.Fill(item1dt);
                        DataRow dataRow5;
                        dataRow5 = item1dt.Rows[0];
                        int item2 = (int)dataRow5["item_code"];
                        #endregion
                        Detail = itemDetail02.Text;
                        Weight = decimal.Parse(weightTextBox02.Text);
                        Count = int.Parse(countTextBox02.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox02.Text);
                        amount = money2 + money2 * Tax / 100;  //decimal.Parse(moneyTextBox02.Text);
                        Remarks = remarks02.Text;

                        DataTable dt5 = new DataTable();
                        string sql_str5 = "UPDATE delivery_calc SET item_code = " + item2 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory2 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

                        adapter = new NpgsqlDataAdapter(sql_str5, conn);
                        adapter.Fill(dt5);
                    }
                    else
                    {
                        record = 3;
                        //mainCategory = mainCategoryCode02;
                        //item = itemCode02;
                        #region "大分類コード"
                        DataTable main2dt = new DataTable();
                        string sql_main2 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox02.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main2, conn);
                        adapter.Fill(main2dt);
                        DataRow dataRow4;
                        dataRow4 = main2dt.Rows[0];
                        int mainCategory2 = (int)dataRow4["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item1dt = new DataTable();
                        string sql_item2 = "select * from item_m where item_name = '" + itemComboBox02.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item2, conn);
                        adapter.Fill(item1dt);
                        DataRow dataRow5;
                        dataRow5 = item1dt.Rows[0];
                        int item2 = (int)dataRow5["item_code"];
                        #endregion
                        Detail = itemDetail02.Text;
                        Weight = decimal.Parse(weightTextBox02.Text);
                        Count = int.Parse(countTextBox02.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox02.Text);
                        amount = money2 + money2 * Tax / 100;  //decimal.Parse(moneyTextBox02.Text);
                        Remarks = remarks02.Text;

                        DataTable dt5 = new DataTable();
                        string sql_str5 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item2 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory2 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str5, conn);
                        adapter.Fill(dt5);
                    }
                    
                }
                #endregion
                #region "4行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox03.Text) && !(unitPriceTextBox03.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 4)
                    {
                        record = 4;
                        //mainCategory = mainCategoryCode03;
                        //item = itemCode03;
                        #region "大分類コード"
                        DataTable main3dt = new DataTable();
                        string sql_main3 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox03.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main3, conn);
                        adapter.Fill(main3dt);
                        DataRow dataRow6;
                        dataRow6 = main3dt.Rows[0];
                        int mainCategory3 = (int)dataRow6["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item3dt = new DataTable();
                        string sql_item3 = "select * from item_m where item_name = '" + itemComboBox03.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item3, conn);
                        adapter.Fill(item3dt);
                        DataRow dataRow7;
                        dataRow7 = item3dt.Rows[0];
                        int item3 = (int)dataRow7["item_code"];
                        #endregion
                        Detail = itemDetail03.Text;
                        Weight = decimal.Parse(weightTextBox03.Text);
                        Count = int.Parse(countTextBox03.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox03.Text);
                        amount = money3 + money3 * Tax / 100;  //decimal.Parse(moneyTextBox03.Text);
                        Remarks = remarks03.Text;

                        DataTable dt6 = new DataTable();
                        string sql_str6 = "UPDATE delivery_calc SET item_code = " + item3 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory3 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

                        adapter = new NpgsqlDataAdapter(sql_str6, conn);
                        adapter.Fill(dt6);
                    }
                    else
                    {
                        record = 4;
                        //mainCategory = mainCategoryCode03;
                        //item = itemCode03;
                        #region "大分類コード"
                        DataTable main3dt = new DataTable();
                        string sql_main3 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox03.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main3, conn);
                        adapter.Fill(main3dt);
                        DataRow dataRow6;
                        dataRow6 = main3dt.Rows[0];
                        int mainCategory3 = (int)dataRow6["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item3dt = new DataTable();
                        string sql_item3 = "select * from item_m where item_name = '" + itemComboBox03.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item3, conn);
                        adapter.Fill(item3dt);
                        DataRow dataRow7;
                        dataRow7 = item3dt.Rows[0];
                        int item3 = (int)dataRow7["item_code"];
                        #endregion
                        Detail = itemDetail03.Text;
                        Weight = decimal.Parse(weightTextBox03.Text);
                        Count = int.Parse(countTextBox03.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox03.Text);
                        amount = money3 + money3 * Tax / 100;  //decimal.Parse(moneyTextBox03.Text);
                        Remarks = remarks03.Text;

                        DataTable dt6 = new DataTable();
                        string sql_str6 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item3 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory3 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str6, conn);
                        adapter.Fill(dt6);
                    }
                    
                }
                #endregion
                #region "5行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox04.Text) && !(unitPriceTextBox04.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 5)
                    {
                        record = 5;
                        //mainCategory = mainCategoryCode04;
                        //item = itemCode04;
                        #region "大分類コード"
                        DataTable main4dt = new DataTable();
                        string sql_main4 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox04.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main4, conn);
                        adapter.Fill(main4dt);
                        DataRow dataRow8;
                        dataRow8 = main4dt.Rows[0];
                        int mainCategory4 = (int)dataRow8["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item4dt = new DataTable();
                        string sql_item4 = "select * from item_m where item_name = '" + itemComboBox04.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item4, conn);
                        adapter.Fill(item4dt);
                        DataRow dataRow9;
                        dataRow9 = item4dt.Rows[0];
                        int item4 = (int)dataRow9["item_code"];
                        #endregion
                        Detail = itemDetail04.Text;
                        Weight = decimal.Parse(weightTextBox04.Text);
                        Count = int.Parse(countTextBox04.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox04.Text);
                        amount = money4 + money4 * Tax / 100; //decimal.Parse(moneyTextBox04.Text);
                        Remarks = remarks04.Text;

                        DataTable dt7 = new DataTable();
                        string sql_str7 = "UPDATE delivery_calc SET item_code = " + item4 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory4 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";
                        adapter = new NpgsqlDataAdapter(sql_str7, conn);
                        adapter.Fill(dt7);
                    }
                    else
                    {
                        record = 5;
                        //mainCategory = mainCategoryCode04;
                        //item = itemCode04;
                        #region "大分類コード"
                        DataTable main4dt = new DataTable();
                        string sql_main4 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox04.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main4, conn);
                        adapter.Fill(main4dt);
                        DataRow dataRow8;
                        dataRow8 = main4dt.Rows[0];
                        int mainCategory4 = (int)dataRow8["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item4dt = new DataTable();
                        string sql_item4 = "select * from item_m where item_name = '" + itemComboBox04.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item4, conn);
                        adapter.Fill(item4dt);
                        DataRow dataRow9;
                        dataRow9 = item4dt.Rows[0];
                        int item4 = (int)dataRow9["item_code"];
                        #endregion
                        Detail = itemDetail04.Text;
                        Weight = decimal.Parse(weightTextBox04.Text);
                        Count = int.Parse(countTextBox04.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox04.Text);
                        amount = money4 + money4 * Tax / 100; //decimal.Parse(moneyTextBox04.Text);
                        Remarks = remarks04.Text;

                        DataTable dt7 = new DataTable();
                        string sql_str7 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item4 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory4 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str7, conn);
                        adapter.Fill(dt7);
                    }
                    
                }
                #endregion
                #region "6行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox05.Text) && !(unitPriceTextBox05.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 6)
                    {
                        record = 6;
                        //mainCategory = mainCategoryCode05;
                        //item = itemCode05;
                        #region "大分類コード"
                        DataTable main5dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main5 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox05.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main5, conn);
                        adapter.Fill(main5dt);
                        DataRow dataRow10;
                        dataRow10 = main5dt.Rows[0];
                        int mainCategory5 = (int)dataRow10["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item5dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item5 = "select * from item_m where item_name = '" + itemComboBox05.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item5, conn);
                        adapter.Fill(item5dt);
                        DataRow dataRow11;
                        dataRow11 = item5dt.Rows[0];
                        int item5 = (int)dataRow11["item_code"];
                        #endregion
                        Detail = itemDetail05.Text;
                        Weight = decimal.Parse(weightTextBox05.Text);
                        Count = int.Parse(countTextBox05.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox05.Text);
                        amount = money5 + money5 * Tax / 100; //decimal.Parse(moneyTextBox05.Text);
                        Remarks = remarks05.Text;

                        DataTable dt8 = new DataTable();
                        string sql_str8 = "UPDATE delivery_calc SET item_code = " + item5 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory5 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

                        adapter = new NpgsqlDataAdapter(sql_str8, conn);
                        adapter.Fill(dt8);
                    }
                    else
                    {
                        record = 6;
                        //mainCategory = mainCategoryCode05;
                        //item = itemCode05;
                        #region "大分類コード"
                        DataTable main5dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main5 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox05.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main5, conn);
                        adapter.Fill(main5dt);
                        DataRow dataRow10;
                        dataRow10 = main5dt.Rows[0];
                        int mainCategory5 = (int)dataRow10["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item5dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item5 = "select * from item_m where item_name = '" + itemComboBox05.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item5, conn);
                        adapter.Fill(item5dt);
                        DataRow dataRow11;
                        dataRow11 = item5dt.Rows[0];
                        int item5 = (int)dataRow11["item_code"];
                        #endregion
                        Detail = itemDetail05.Text;
                        Weight = decimal.Parse(weightTextBox05.Text);
                        Count = int.Parse(countTextBox05.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox05.Text);
                        amount = money5 + money5 * Tax / 100; //decimal.Parse(moneyTextBox05.Text);
                        Remarks = remarks05.Text;

                        DataTable dt8 = new DataTable();
                        string sql_str8 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES  ( '" + ControlNumber + "' ,'" + record + "' , " + item5 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory5 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str8, conn);
                        adapter.Fill(dt8);
                    }
                    
                }
                #endregion
                #region "7行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox06.Text) && !(unitPriceTextBox06.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 7)
                    {
                        record = 7;
                        //mainCategory = mainCategoryCode06;
                        //item = itemCode06;
                        #region "大分類コード"
                        DataTable main6dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main6 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox06.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main6, conn);
                        adapter.Fill(main6dt);
                        DataRow dataRow12;
                        dataRow12 = main6dt.Rows[0];
                        int mainCategory6 = (int)dataRow12["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item6dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item6 = "select * from item_m where item_name = '" + itemComboBox06.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item6, conn);
                        adapter.Fill(item6dt);
                        DataRow dataRow13;
                        dataRow13 = item6dt.Rows[0];
                        int item6 = (int)dataRow13["item_code"];
                        #endregion
                        Detail = itemDetail06.Text;
                        Weight = decimal.Parse(weightTextBox06.Text);
                        Count = int.Parse(countTextBox06.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox06.Text);
                        amount = money6 + money6 * Tax / 100; //decimal.Parse(moneyTextBox06.Text);
                        Remarks = remarks06.Text;

                        DataTable dt9 = new DataTable();
                        string sql_str9 = "UPDATE delivery_calc SET item_code = " + item6 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory6 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

                        adapter = new NpgsqlDataAdapter(sql_str9, conn);
                        adapter.Fill(dt9);
                    }
                    else
                    {
                        record = 7;
                        //mainCategory = mainCategoryCode06;
                        //item = itemCode06;
                        #region "大分類コード"
                        DataTable main6dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main6 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox06.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main6, conn);
                        adapter.Fill(main6dt);
                        DataRow dataRow12;
                        dataRow12 = main6dt.Rows[0];
                        int mainCategory6 = (int)dataRow12["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item6dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item6 = "select * from item_m where item_name = '" + itemComboBox06.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item6, conn);
                        adapter.Fill(item6dt);
                        DataRow dataRow13;
                        dataRow13 = item6dt.Rows[0];
                        int item6 = (int)dataRow13["item_code"];
                        #endregion
                        Detail = itemDetail06.Text;
                        Weight = decimal.Parse(weightTextBox06.Text);
                        Count = int.Parse(countTextBox06.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox06.Text);
                        amount = money6 + money6 * Tax / 100; //decimal.Parse(moneyTextBox06.Text);
                        Remarks = remarks06.Text;

                        DataTable dt9 = new DataTable();
                        string sql_str9 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item6 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory6 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str9, conn);
                        adapter.Fill(dt9);
                    }
                    
                }
                #endregion
                #region "8行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox07.Text) && !(unitPriceTextBox07.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 8)
                    {
                        record = 8;
                        //mainCategory = mainCategoryCode07;
                        //item = itemCode07;
                        #region "大分類コード"
                        DataTable main7dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main7 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox07.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main7, conn);
                        adapter.Fill(main7dt);
                        DataRow dataRow13;
                        dataRow13 = main7dt.Rows[0];
                        int mainCategory7 = (int)dataRow13["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item7dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item7 = "select * from item_m where item_name = '" + itemComboBox07.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item7, conn);
                        adapter.Fill(item7dt);
                        DataRow dataRow14;
                        dataRow14 = item7dt.Rows[0];
                        int item7 = (int)dataRow14["item_code"];
                        #endregion
                        Detail = itemDetail07.Text;
                        Weight = decimal.Parse(weightTextBox07.Text);
                        Count = int.Parse(countTextBox07.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox07.Text);
                        amount = money7 + money7 * Tax / 100; //decimal.Parse(moneyTextBox07.Text);
                        Remarks = remarks07.Text;

                        DataTable dt10 = new DataTable();
                        string sql_str10 = "UPDATE delivery_calc SET item_code = " + item7 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory7 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

                        adapter = new NpgsqlDataAdapter(sql_str10, conn);
                        adapter.Fill(dt10);
                    }
                    else
                    {
                        record = 8;
                        //mainCategory = mainCategoryCode07;
                        //item = itemCode07;
                        #region "大分類コード"
                        DataTable main7dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main7 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox07.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main7, conn);
                        adapter.Fill(main7dt);
                        DataRow dataRow13;
                        dataRow13 = main7dt.Rows[0];
                        int mainCategory7 = (int)dataRow13["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item7dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item7 = "select * from item_m where item_name = '" + itemComboBox07.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item7, conn);
                        adapter.Fill(item7dt);
                        DataRow dataRow14;
                        dataRow14 = item7dt.Rows[0];
                        int item7 = (int)dataRow14["item_code"];
                        #endregion
                        Detail = itemDetail07.Text;
                        Weight = decimal.Parse(weightTextBox07.Text);
                        Count = int.Parse(countTextBox07.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox07.Text);
                        amount = money7 + money7 * Tax / 100; //decimal.Parse(moneyTextBox07.Text);
                        Remarks = remarks07.Text;

                        DataTable dt10 = new DataTable();
                        string sql_str10 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item7 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory7 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str10, conn);
                        adapter.Fill(dt10);
                    }
                    
                }
                #endregion
                #region "9行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox08.Text) && !(unitPriceTextBox08.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 9)
                    {
                        record = 9;
                        //mainCategory = mainCategoryCode08;
                        //item = itemCode08;
                        #region "大分類コード"
                        DataTable main8dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main8 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox08.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main8, conn);
                        adapter.Fill(main8dt);
                        DataRow dataRow15;
                        dataRow15 = main8dt.Rows[0];
                        int mainCategory8 = (int)dataRow15["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item8dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item8 = "select * from item_m where item_name = '" + itemComboBox08.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item8, conn);
                        adapter.Fill(item8dt);
                        DataRow dataRow16;
                        dataRow16 = item8dt.Rows[0];
                        int item8 = (int)dataRow16["item_code"];
                        #endregion
                        Detail = itemDetail08.Text;
                        Weight = decimal.Parse(weightTextBox08.Text);
                        Count = int.Parse(countTextBox08.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox08.Text);
                        amount = money8 + money8 * Tax / 100; //decimal.Parse(moneyTextBox08.Text);
                        Remarks = remarks08.Text;

                        DataTable dt11 = new DataTable();
                        string sql_str11 = "UPDATE delivery_calc SET item_code = " + item8 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory8 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

                        adapter = new NpgsqlDataAdapter(sql_str11, conn);
                        adapter.Fill(dt11);
                    }
                    else
                    {
                        record = 9;
                        //mainCategory = mainCategoryCode08;
                        //item = itemCode08;
                        #region "大分類コード"
                        DataTable main8dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main8 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox08.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main8, conn);
                        adapter.Fill(main8dt);
                        DataRow dataRow15;
                        dataRow15 = main8dt.Rows[0];
                        int mainCategory8 = (int)dataRow15["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item8dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item8 = "select * from item_m where item_name = '" + itemComboBox08.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item8, conn);
                        adapter.Fill(item8dt);
                        DataRow dataRow16;
                        dataRow16 = item8dt.Rows[0];
                        int item8 = (int)dataRow16["item_code"];
                        #endregion
                        Detail = itemDetail08.Text;
                        Weight = decimal.Parse(weightTextBox08.Text);
                        Count = int.Parse(countTextBox08.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox08.Text);
                        amount = money8 + money8 * Tax / 100; //decimal.Parse(moneyTextBox08.Text);
                        Remarks = remarks08.Text;

                        DataTable dt11 = new DataTable();
                        string sql_str11 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item8 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory8 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str11, conn);
                        adapter.Fill(dt11);
                    }
                    
                }
                #endregion
                #region "10行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox09.Text) && !(unitPriceTextBox09.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 10)
                    {
                        record = 10;
                        //mainCategory = mainCategoryCode09;
                        //item = itemCode09;
                        #region "大分類コード"
                        DataTable main9dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main9 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox09.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main9, conn);
                        adapter.Fill(main9dt);
                        DataRow dataRow17;
                        dataRow17 = main9dt.Rows[0];
                        int mainCategory9 = (int)dataRow17["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item9dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item9 = "select * from item_m where item_name = '" + itemComboBox09.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item9, conn);
                        adapter.Fill(item9dt);
                        DataRow dataRow18;
                        dataRow18 = item9dt.Rows[0];
                        int item9 = (int)dataRow18["item_code"];
                        #endregion
                        Detail = itemDetail09.Text;
                        Weight = decimal.Parse(weightTextBox09.Text);
                        Count = int.Parse(countTextBox09.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox09.Text);
                        amount = money9 + money9 * Tax / 100; //decimal.Parse(moneyTextBox09.Text);
                        Remarks = remarks09.Text;

                        DataTable dt12 = new DataTable();
                        string sql_str12 = "UPDATE delivery_calc SET item_code = " + item9 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory9 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

                        adapter = new NpgsqlDataAdapter(sql_str12, conn);
                        adapter.Fill(dt12);
                    }
                    else
                    {
                        record = 10;
                        //mainCategory = mainCategoryCode09;
                        //item = itemCode09;
                        #region "大分類コード"
                        DataTable main9dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main9 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox09.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main9, conn);
                        adapter.Fill(main9dt);
                        DataRow dataRow17;
                        dataRow17 = main9dt.Rows[0];
                        int mainCategory9 = (int)dataRow17["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item9dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item9 = "select * from item_m where item_name = '" + itemComboBox09.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item9, conn);
                        adapter.Fill(item9dt);
                        DataRow dataRow18;
                        dataRow18 = item9dt.Rows[0];
                        int item9 = (int)dataRow18["item_code"];
                        #endregion
                        Detail = itemDetail09.Text;
                        Weight = decimal.Parse(weightTextBox09.Text);
                        Count = int.Parse(countTextBox09.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox09.Text);
                        amount = money9 + money9 * Tax / 100; //decimal.Parse(moneyTextBox09.Text);
                        Remarks = remarks09.Text;

                        DataTable dt12 = new DataTable();
                        string sql_str12 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item9 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory9 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str12, conn);
                        adapter.Fill(dt12);
                    }
                    
                }
                #endregion
                #region "11行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox010.Text) && !(unitPriceTextBox010.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 11)
                    {
                        record = 11;
                        //mainCategory = mainCategoryCode010;
                        //item = itemCode010;
                        #region "大分類コード"
                        DataTable main10dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main10 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox010.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main10, conn);
                        adapter.Fill(main10dt);
                        DataRow dataRow19;
                        dataRow19 = main10dt.Rows[0];
                        int mainCategory10 = (int)dataRow19["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item10dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item10 = "select * from item_m where item_name = '" + itemComboBox010.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item10, conn);
                        adapter.Fill(item10dt);
                        DataRow dataRow20;
                        dataRow20 = item10dt.Rows[0];
                        int item10 = (int)dataRow20["item_code"];
                        #endregion
                        Detail = itemDetail010.Text;
                        Weight = decimal.Parse(weightTextBox010.Text);
                        Count = int.Parse(countTextBox010.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox010.Text);
                        amount = money10 + money10 * Tax / 100; //decimal.Parse(moneyTextBox010.Text);
                        Remarks = remarks010.Text;

                        DataTable dt13 = new DataTable();
                        string sql_str13 = "UPDATE delivery_calc SET item_code = " + item10 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory10 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

                        adapter = new NpgsqlDataAdapter(sql_str13, conn);
                        adapter.Fill(dt13);
                    }
                    else
                    {
                        record = 11;
                        //mainCategory = mainCategoryCode010;
                        //item = itemCode010;
                        #region "大分類コード"
                        DataTable main10dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main10 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox010.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main10, conn);
                        adapter.Fill(main10dt);
                        DataRow dataRow19;
                        dataRow19 = main10dt.Rows[0];
                        int mainCategory10 = (int)dataRow19["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item10dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item10 = "select * from item_m where item_name = '" + itemComboBox010.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item10, conn);
                        adapter.Fill(item10dt);
                        DataRow dataRow20;
                        dataRow20 = item10dt.Rows[0];
                        int item10 = (int)dataRow20["item_code"];
                        #endregion
                        Detail = itemDetail010.Text;
                        Weight = decimal.Parse(weightTextBox010.Text);
                        Count = int.Parse(countTextBox010.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox010.Text);
                        amount = money10 + money10 * Tax / 100; //decimal.Parse(moneyTextBox010.Text);
                        Remarks = remarks010.Text;

                        DataTable dt13 = new DataTable();
                        string sql_str13 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item10 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory10 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str13, conn);
                        adapter.Fill(dt13);
                    }
                    
                }
                #endregion
                #region "12行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox011.Text) && !(unitPriceTextBox011.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 12)
                    {
                        record = 12;
                        //mainCategory = mainCategoryCode011;
                        //item = itemCode011;
                        #region "大分類コード"
                        DataTable main11dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main11 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox011.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main11, conn);
                        adapter.Fill(main11dt);
                        DataRow dataRow21;
                        dataRow21 = main11dt.Rows[0];
                        int mainCategory11 = (int)dataRow21["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item11dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item11 = "select * from item_m where item_name = '" + itemComboBox011.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item11, conn);
                        adapter.Fill(item11dt);
                        DataRow dataRow22;
                        dataRow22 = item11dt.Rows[0];
                        int item11 = (int)dataRow22["item_code"];
                        #endregion
                        Detail = itemDetail011.Text;
                        Weight = decimal.Parse(weightTextBox011.Text);
                        Count = int.Parse(countTextBox011.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox011.Text);
                        amount = money11 + money11 * Tax / 100; //decimal.Parse(moneyTextBox011.Text);
                        Remarks = remarks011.Text;

                        DataTable dt14 = new DataTable();
                        string sql_str14 = "UPDATE delivery_calc SET item_code = " + item11 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory11 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

                        adapter = new NpgsqlDataAdapter(sql_str14, conn);
                        adapter.Fill(dt14);
                    }
                    else
                    {
                        record = 12;
                        //mainCategory = mainCategoryCode011;
                        //item = itemCode011;
                        #region "大分類コード"
                        DataTable main11dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main11 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox011.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main11, conn);
                        adapter.Fill(main11dt);
                        DataRow dataRow21;
                        dataRow21 = main11dt.Rows[0];
                        int mainCategory11 = (int)dataRow21["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item11dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item11 = "select * from item_m where item_name = '" + itemComboBox011.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item11, conn);
                        adapter.Fill(item11dt);
                        DataRow dataRow22;
                        dataRow22 = item11dt.Rows[0];
                        int item11 = (int)dataRow22["item_code"];
                        #endregion
                        Detail = itemDetail011.Text;
                        Weight = decimal.Parse(weightTextBox011.Text);
                        Count = int.Parse(countTextBox011.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox011.Text);
                        amount = money11 + money11 * Tax / 100; //decimal.Parse(moneyTextBox011.Text);
                        Remarks = remarks011.Text;

                        DataTable dt14 = new DataTable();
                        string sql_str14 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item11 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory11 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str14, conn);
                        adapter.Fill(dt14);
                    }
                    
                }
                #endregion
                #region "13行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox012.Text) && !(unitPriceTextBox012.Text == "単価 -> 重量 or 数量"))
                {
                    if (Re >= 13)
                    {
                        record = 13;
                        //mainCategory = mainCategoryCode012;
                        //item = itemCode012;
                        #region "大分類コード"
                        DataTable main12dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main12 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox012.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main12, conn);
                        adapter.Fill(main12dt);
                        DataRow dataRow23;
                        dataRow23 = main12dt.Rows[0];
                        int mainCategory12 = (int)dataRow23["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item12dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item12 = "select * from item_m where item_name = '" + itemComboBox012.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item12, conn);
                        adapter.Fill(item12dt);
                        DataRow dataRow24;
                        dataRow24 = item12dt.Rows[0];
                        int item12 = (int)dataRow24["item_code"];
                        #endregion
                        Detail = itemDetail012.Text;
                        Weight = decimal.Parse(weightTextBox012.Text);
                        Count = int.Parse(countTextBox012.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox012.Text);
                        amount = money12 + money12 * Tax / 100; //decimal.Parse(moneyTextBox012.Text);
                        Remarks = remarks012.Text;

                        DataTable dt15 = new DataTable();
                        string sql_str15 = "UPDATE delivery_calc SET item_code = " + item12 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory12 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

                        adapter = new NpgsqlDataAdapter(sql_str15, conn);
                        adapter.Fill(dt15);
                    }
                    else
                    {
                        record = 13;
                        //mainCategory = mainCategoryCode012;
                        //item = itemCode012;
                        #region "大分類コード"
                        DataTable main12dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main12 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox012.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main12, conn);
                        adapter.Fill(main12dt);
                        DataRow dataRow23;
                        dataRow23 = main12dt.Rows[0];
                        int mainCategory12 = (int)dataRow23["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable item12dt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item12 = "select * from item_m where item_name = '" + itemComboBox012.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item12, conn);
                        adapter.Fill(item12dt);
                        DataRow dataRow24;
                        dataRow24 = item12dt.Rows[0];
                        int item12 = (int)dataRow24["item_code"];
                        #endregion
                        Detail = itemDetail012.Text;
                        Weight = decimal.Parse(weightTextBox012.Text);
                        Count = int.Parse(countTextBox012.Text);
                        UnitPrice = decimal.Parse(unitPriceTextBox012.Text);
                        amount = money12 + money12 * Tax / 100; //decimal.Parse(moneyTextBox012.Text);
                        Remarks = remarks012.Text;

                        DataTable dt15 = new DataTable();
                        string sql_str15 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item12 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory12 + ",'" + Detail + "');";

                        adapter = new NpgsqlDataAdapter(sql_str15, conn);
                        adapter.Fill(dt15);
                    }
                    
                }
                #endregion
            }
            #region "新規登録"
            else
            {            
            int ControlNumber = number;

            decimal TotalWeight = weisum;
            //countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
            int Amount = countsum;
            //int Amount = int.Parse(totalCount2.Text);
            decimal SubTotal = subSum + TaxAmount;
            decimal Total = sum;
            //vat（税込み税抜きどちらか）, vat_rate（税率）
            string vat = this.comboBox11.SelectedItem.ToString();
            int vat_rate = (int)Tax;

            string seaal = "";
            //印鑑印刷
            if (sealY.Checked == true)
            {
                seaal = "する";
            }
            if (sealN.Checked == true)
            {
                seaal = "しない";
            }

            string OrderDate = orderDateTimePicker.Text;
            string DeliveryDate = DeliveryDateTimePicker.Text;
            string SettlementDate = SettlementDateTimePicker.Text;
            string PaymentMethod = paymentMethodComboBox.SelectedItem.ToString();
            string Name = name.Text;                                           //宛名
            string Title = titleComboBox.SelectedItem.ToString();              //敬称
            string Type = typeComboBox.SelectedItem.ToString();                //納品書or請求書
            string payee = PayeeTextBox1.Text;                                 //振り込み先
            string coin = CoinComboBox.SelectedItem.ToString();                //通貨
            string remark = RemarkRegister.Text;
            DateTime dat1 = DateTime.Now;
            DateTime dtToday = dat1.Date;
            string c = dtToday.ToString("yyyy年MM月dd日");

                #region "法人"
                if (type == 0)
            {
                NpgsqlConnection connA = new NpgsqlConnection();
                connA.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                NpgsqlDataAdapter adapterA;
                DataTable clientDt = new DataTable();
                string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + client_staff_name + "' and address = '" + address + "';";
                adapterA = new NpgsqlDataAdapter(str_sql_corporate, connA);
                adapterA.Fill(clientDt);

                DataRow row2;
                row2 = clientDt.Rows[0];
                int antique = (int)row2["antique_number"];
                #region "使用する"
                NpgsqlConnection conn0 = new NpgsqlConnection();
                NpgsqlDataAdapter adapter0;
                conn0.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                DataTable dt = new DataTable();
                string sql_str = "Insert into delivery_m (control_number, antique_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seaal_print, payment_method, account_payble, currency, remarks2, total_count, total_weight, types1) VALUES ( '" + ControlNumber + "'," + antique + ",'" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + Amount + "','" + TotalWeight + "'," + 0 + ");";
                adapter0 = new NpgsqlDataAdapter(sql_str, conn0);
                adapter0.Fill(dt);
                #endregion
                #region "履歴"
                DataTable dtre = new DataTable();
                string sql_str_re = "Insert into delivery_m_revisions (control_number, antique_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seal_print, payment_method, account_payable, currency, remarks2, registration_date, insert_name ) VALUES ( '" + ControlNumber + "'," + antique + ",'" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + c + "'," + staff_id + ");";
                adapter0 = new NpgsqlDataAdapter(sql_str_re, conn);
                adapter0.Fill(dtre);
                #endregion
                }
                #endregion
                #region "個人"
                if (type == 1)
            {
                NpgsqlConnection connI = new NpgsqlConnection();
                connI.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                NpgsqlDataAdapter adapterI;
                DataTable clientDt = new DataTable();
                string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
                adapterI = new NpgsqlDataAdapter(str_sql_individual, connI);
                adapterI.Fill(clientDt);

                DataRow row2;
                row2 = clientDt.Rows[0];
                int ID = (int)row2["id_number"];
                #region "使用する"
                NpgsqlConnection conn1 = new NpgsqlConnection();
                NpgsqlDataAdapter adapter1;
                conn1.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                DataTable dt = new DataTable();
                string sql_str = "Insert into delivery_m (control_number, id_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seaal_print, payment_method, account_payble, currency, remarks2, total_count, total_weight, types1) VALUES ( '" + ControlNumber + "','" + ID + "','" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + Amount + "','" + TotalWeight + "'," + 1 + ");";
                adapter1 = new NpgsqlDataAdapter(sql_str, conn1);
                adapter1.Fill(dt);
                    #endregion
                #region "履歴"
                DataTable dtre = new DataTable();
                string sql_str_re = "Insert into delivery_m_revisions (control_number, id_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seal_print, payment_method, account_payable, currency, remarks2, registration_date, insert_name ) VALUES ( '" + ControlNumber + "'," + ID + ",'" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + c + "'," + staff_id + ");";
                adapter1 = new NpgsqlDataAdapter(sql_str_re, conn);
                adapter1.Fill(dtre);
                #endregion
                }
                #endregion

                /*NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;

                DataTable dt = new DataTable();
                string sql_str = "Insert into delivery_m (control_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seaal_print, payment_method, account_payble, currency, remarks2, total_count, total_weight, types1) VALUES ( '" + ControlNumber + "','" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + Amount + "','" + TotalWeight + "'," + type + ");";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);*/

            #region "1行目"
           //管理番号：ControlNumber
            int record = 1;     //行数
            //int mainCategory = mainCategoryCode00;
            //int item = itemCode00;
            #region "大分類コード"
            DataTable maindt = new DataTable();
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            string sql_main = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox00.Text + "';";
            adapter = new NpgsqlDataAdapter(sql_main, conn);
            adapter.Fill(maindt);
            DataRow dataRow;
            dataRow = maindt.Rows[0];
            int mainCategory = (int)dataRow["main_category_code"];
            #endregion
            #region "品名コード"
            DataTable itemdt = new DataTable();
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            string sql_item = "select * from item_m where item_name = '" + itemComboBox00.Text + "';";
            adapter = new NpgsqlDataAdapter(sql_item, conn);
            adapter.Fill(itemdt);
            DataRow dataRow1;
            dataRow1 = itemdt.Rows[0];
            int item = (int)dataRow1["item_code"];
            #endregion
            string Detail = itemDetail00.Text;
            decimal Weight = decimal.Parse(weightTextBox00.Text);
            int Count = int.Parse(countTextBox00.Text);
            decimal UnitPrice = decimal.Parse(unitPriceTextBox00.Text);
            decimal amount = money0 + money0 * Tax / 100;
            string Remarks = remarks00.Text;

            DataTable dt2 = new DataTable();
            string sql_str2 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + mainCategory + "','" + Detail + "');";

            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();
            adapter = new NpgsqlDataAdapter(sql_str2, conn);
            adapter.Fill(dt2);
            #region "履歴"
            DataTable dt2re = new DataTable();
            string sql_str2_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory  + "," +  item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','"  + Detail + "');";

            adapter = new NpgsqlDataAdapter(sql_str2_re, conn);
            adapter.Fill(dt2re);
            #endregion
            #endregion
            #region "2行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox01.Text) && !(unitPriceTextBox01.Text == "単価 -> 重量 or 数量"))
            {
                record = 2;
                //mainCategory = mainCategoryCode01;
                //item = itemCode01;
                #region "大分類コード"
                DataTable main1dt = new DataTable();
                string sql_main1 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox01.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main1, conn);
                adapter.Fill(main1dt);
                DataRow dataRow2;
                dataRow2 = main1dt.Rows[0];
                int mainCategory1 = (int)dataRow2["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item1dt = new DataTable();
                string sql_item1 = "select * from item_m where item_name = '" + itemComboBox01.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item1, conn);
                adapter.Fill(item1dt);
                DataRow dataRow3;
                dataRow3 = item1dt.Rows[0];
                int item1 = (int)dataRow3["item_code"];
                #endregion
                Detail = itemDetail01.Text;
                Weight = decimal.Parse(weightTextBox01.Text);
                Count = int.Parse(countTextBox01.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox01.Text);
                amount = money1 + money1 * Tax / 100;  //decimal.Parse(moneyTextBox01.Text);
                Remarks = remarks01.Text;

                DataTable dt4 = new DataTable();
                string sql_str4 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item1 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory1 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str4, conn);
                adapter.Fill(dt4);

                #region "履歴"
                DataTable dt4re = new DataTable();
                string sql_str4_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

                    
                    adapter = new NpgsqlDataAdapter(sql_str4_re, conn);
                    adapter.Fill(dt4re);
                #endregion
                }
                #endregion
            #region "3行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox02.Text) && !(unitPriceTextBox02.Text == "単価 -> 重量 or 数量"))
            {
                record = 3;
                //mainCategory = mainCategoryCode02;
                //item = itemCode02;
                #region "大分類コード"
                DataTable main2dt = new DataTable();
                string sql_main2 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox02.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main2, conn);
                adapter.Fill(main2dt);
                DataRow dataRow4;
                dataRow4 = main2dt.Rows[0];
                int mainCategory2 = (int)dataRow4["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item1dt = new DataTable();
                string sql_item2 = "select * from item_m where item_name = '" + itemComboBox02.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item2, conn);
                adapter.Fill(item1dt);
                DataRow dataRow5;
                dataRow5 = item1dt.Rows[0];
                int item2 = (int)dataRow5["item_code"];
                #endregion
                Detail = itemDetail02.Text;
                Weight = decimal.Parse(weightTextBox02.Text);
                Count = int.Parse(countTextBox02.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox02.Text);
                amount = money2 + money2 * Tax / 100;  //decimal.Parse(moneyTextBox02.Text);
                Remarks = remarks02.Text;

                DataTable dt5 = new DataTable();
                string sql_str5 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item2 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory2 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str5, conn);
                adapter.Fill(dt5);

                #region "履歴"
                DataTable dt5re = new DataTable();
                string sql_str5_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

                
                adapter = new NpgsqlDataAdapter(sql_str5_re, conn);
                adapter.Fill(dt5re);
                #endregion
                }
                #endregion
            #region "4行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox03.Text) && !(unitPriceTextBox03.Text == "単価 -> 重量 or 数量"))
            {
                record = 4;
                //mainCategory = mainCategoryCode03;
                //item = itemCode03;
                #region "大分類コード"
                DataTable main3dt = new DataTable();
                string sql_main3 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox03.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main3, conn);
                adapter.Fill(main3dt);
                DataRow dataRow6;
                dataRow6 = main3dt.Rows[0];
                int mainCategory3 = (int)dataRow6["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item3dt = new DataTable();
                string sql_item3 = "select * from item_m where item_name = '" + itemComboBox03.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item3, conn);
                adapter.Fill(item3dt);
                DataRow dataRow7;
                dataRow7 = item3dt.Rows[0];
                int item3 = (int)dataRow7["item_code"];
                #endregion
                Detail = itemDetail03.Text;
                Weight = decimal.Parse(weightTextBox03.Text);
                Count = int.Parse(countTextBox03.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox03.Text);
                amount = money3 + money3 * Tax / 100;  //decimal.Parse(moneyTextBox03.Text);
                Remarks = remarks03.Text;

                DataTable dt6 = new DataTable();
                string sql_str6 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item3 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory3 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str6, conn);
                adapter.Fill(dt6);

                #region "履歴"
                DataTable dt6re = new DataTable();
                string sql_str6_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

                
                adapter = new NpgsqlDataAdapter(sql_str6_re, conn);
                adapter.Fill(dt6re);
                #endregion
                }
                #endregion
            #region "5行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox04.Text) && !(unitPriceTextBox04.Text == "単価 -> 重量 or 数量"))
            {
                record = 5;
                //mainCategory = mainCategoryCode04;
                //item = itemCode04;
                #region "大分類コード"
                DataTable main4dt = new DataTable();
                string sql_main4 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox04.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main4, conn);
                adapter.Fill(main4dt);
                DataRow dataRow8;
                dataRow8 = main4dt.Rows[0];
                int mainCategory4 = (int)dataRow8["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item4dt = new DataTable();
                string sql_item4 = "select * from item_m where item_name = '" + itemComboBox04.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item4, conn);
                adapter.Fill(item4dt);
                DataRow dataRow9;
                dataRow9 = item4dt.Rows[0];
                int item4 = (int)dataRow9["item_code"];
                #endregion
                Detail = itemDetail04.Text;
                Weight = decimal.Parse(weightTextBox04.Text);
                Count = int.Parse(countTextBox04.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox04.Text);
                amount = money4 + money4 * Tax / 100; //decimal.Parse(moneyTextBox04.Text);
                Remarks = remarks04.Text;

                DataTable dt7 = new DataTable();
                string sql_str7 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item4 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory4 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str7, conn);
                adapter.Fill(dt7);
                #region "履歴"
                DataTable dt7re = new DataTable();
                string sql_str7_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

                
                adapter = new NpgsqlDataAdapter(sql_str7_re, conn);
                adapter.Fill(dt7re);
                #endregion
                }
                #endregion
            #region "6行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox05.Text) && !(unitPriceTextBox05.Text == "単価 -> 重量 or 数量"))
            {
                record = 6;
                //mainCategory = mainCategoryCode05;
                //item = itemCode05;
                #region "大分類コード"
                DataTable main5dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_main5 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox05.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main5, conn);
                adapter.Fill(main5dt);
                DataRow dataRow10;
                dataRow10 = main5dt.Rows[0];
                int mainCategory5 = (int)dataRow10["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item5dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_item5 = "select * from item_m where item_name = '" + itemComboBox05.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item5, conn);
                adapter.Fill(item5dt);
                DataRow dataRow11;
                dataRow11 = item5dt.Rows[0];
                int item5 = (int)dataRow11["item_code"];
                #endregion
                Detail = itemDetail05.Text;
                Weight = decimal.Parse(weightTextBox05.Text);
                Count = int.Parse(countTextBox05.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox05.Text);
                amount = money5 + money5 * Tax / 100; //decimal.Parse(moneyTextBox05.Text);
                Remarks = remarks05.Text;

                DataTable dt8 = new DataTable();
                string sql_str8 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES  ( '" + ControlNumber + "' ,'" + record + "' , " + item5 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory5 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str8, conn);
                adapter.Fill(dt8);

                #region "履歴"
                 DataTable dt8re = new DataTable();
                 string sql_str8_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

                 
                  adapter = new NpgsqlDataAdapter(sql_str8_re, conn);
                  adapter.Fill(dt8re);
                 #endregion
                }
                #endregion
            #region "7行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox06.Text) && !(unitPriceTextBox06.Text == "単価 -> 重量 or 数量"))
            {
                record = 7;
                //mainCategory = mainCategoryCode06;
                //item = itemCode06;
                #region "大分類コード"
                DataTable main6dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_main6 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox06.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main6, conn);
                adapter.Fill(main6dt);
                DataRow dataRow12;
                dataRow12 = main6dt.Rows[0];
                int mainCategory6 = (int)dataRow12["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item6dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_item6 = "select * from item_m where item_name = '" + itemComboBox06.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item6, conn);
                adapter.Fill(item6dt);
                DataRow dataRow13;
                dataRow13 = item6dt.Rows[0];
                int item6 = (int)dataRow13["item_code"];
                #endregion
                Detail = itemDetail06.Text;
                Weight = decimal.Parse(weightTextBox06.Text);
                Count = int.Parse(countTextBox06.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox06.Text);
                amount = money6 + money6 * Tax / 100; //decimal.Parse(moneyTextBox06.Text);
                Remarks = remarks06.Text;

                DataTable dt9 = new DataTable();
                string sql_str9 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item6 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory6 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str9, conn);
                adapter.Fill(dt9);

                #region "履歴"
                DataTable dt9re = new DataTable();
                string sql_str9_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

                
                adapter = new NpgsqlDataAdapter(sql_str9_re, conn);
                adapter.Fill(dt9re);
                #endregion
                }
                #endregion
            #region "8行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox07.Text) && !(unitPriceTextBox07.Text == "単価 -> 重量 or 数量"))
            {
                record = 8;
                //mainCategory = mainCategoryCode07;
                //item = itemCode07;
                #region "大分類コード"
                DataTable main7dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_main7 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox07.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main7, conn);
                adapter.Fill(main7dt);
                DataRow dataRow13;
                dataRow13 = main7dt.Rows[0];
                int mainCategory7 = (int)dataRow13["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item7dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_item7 = "select * from item_m where item_name = '" + itemComboBox07.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item7, conn);
                adapter.Fill(item7dt);
                DataRow dataRow14;
                dataRow14 = item7dt.Rows[0];
                int item7 = (int)dataRow14["item_code"];
                #endregion
                Detail = itemDetail07.Text;
                Weight = decimal.Parse(weightTextBox07.Text);
                Count = int.Parse(countTextBox07.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox07.Text);
                amount = money7 + money7 * Tax / 100; //decimal.Parse(moneyTextBox07.Text);
                Remarks = remarks07.Text;

                DataTable dt10 = new DataTable();
                string sql_str10 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item7 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory7 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str10, conn);
                adapter.Fill(dt10);

                #region "履歴"
                DataTable dt10re = new DataTable();
                string sql_str10_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

               
                adapter = new NpgsqlDataAdapter(sql_str10_re, conn);
                adapter.Fill(dt10re);
                #endregion
                }
                #endregion
            #region "9行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox08.Text) && !(unitPriceTextBox08.Text == "単価 -> 重量 or 数量"))
            {
                record = 9;
                //mainCategory = mainCategoryCode08;
                //item = itemCode08;
                #region "大分類コード"
                DataTable main8dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_main8 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox08.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main8, conn);
                adapter.Fill(main8dt);
                DataRow dataRow15;
                dataRow15 = main8dt.Rows[0];
                int mainCategory8 = (int)dataRow15["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item8dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_item8 = "select * from item_m where item_name = '" + itemComboBox08.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item8, conn);
                adapter.Fill(item8dt);
                DataRow dataRow16;
                dataRow16 = item8dt.Rows[0];
                int item8 = (int)dataRow16["item_code"];
                #endregion
                Detail = itemDetail08.Text;
                Weight = decimal.Parse(weightTextBox08.Text);
                Count = int.Parse(countTextBox08.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox08.Text);
                amount = money8 + money8 * Tax / 100; //decimal.Parse(moneyTextBox08.Text);
                Remarks = remarks08.Text;

                DataTable dt11 = new DataTable();
                string sql_str11 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item8 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory8 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str11, conn);
                adapter.Fill(dt11);

                #region "履歴"
                DataTable dt11re = new DataTable();
                string sql_str11_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

                
                adapter = new NpgsqlDataAdapter(sql_str11_re, conn);
                adapter.Fill(dt11re);
                #endregion
                }
                #endregion
            #region "10行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox09.Text) && !(unitPriceTextBox09.Text == "単価 -> 重量 or 数量"))
            {
                record = 10;
                //mainCategory = mainCategoryCode09;
                //item = itemCode09;
                #region "大分類コード"
                DataTable main9dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_main9 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox09.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main9, conn);
                adapter.Fill(main9dt);
                DataRow dataRow17;
                dataRow17 = main9dt.Rows[0];
                int mainCategory9 = (int)dataRow17["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item9dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_item9 = "select * from item_m where item_name = '" + itemComboBox09.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item9, conn);
                adapter.Fill(item9dt);
                DataRow dataRow18;
                dataRow18 = item9dt.Rows[0];
                int item9 = (int)dataRow18["item_code"];
                #endregion
                Detail = itemDetail09.Text;
                Weight = decimal.Parse(weightTextBox09.Text);
                Count = int.Parse(countTextBox09.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox09.Text);
                amount = money9 + money9 * Tax / 100; //decimal.Parse(moneyTextBox09.Text);
                Remarks = remarks09.Text;

                DataTable dt12 = new DataTable();
                string sql_str12 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item9 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory9 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str12, conn);
                adapter.Fill(dt12);

                #region "履歴"
                DataTable dt12re = new DataTable();
                string sql_str12_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

                
                adapter = new NpgsqlDataAdapter(sql_str12_re, conn);
                adapter.Fill(dt12re);
                #endregion
                }
                #endregion
            #region "11行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox010.Text) && !(unitPriceTextBox010.Text == "単価 -> 重量 or 数量"))
            {
                record = 11;
                //mainCategory = mainCategoryCode010;
                //item = itemCode010;
                #region "大分類コード"
                DataTable main10dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_main10 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox010.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main10, conn);
                adapter.Fill(main10dt);
                DataRow dataRow19;
                dataRow19 = main10dt.Rows[0];
                int mainCategory10 = (int)dataRow19["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item10dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_item10 = "select * from item_m where item_name = '" + itemComboBox010.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item10, conn);
                adapter.Fill(item10dt);
                DataRow dataRow20;
                dataRow20 = item10dt.Rows[0];
                int item10 = (int)dataRow20["item_code"];
                #endregion
                Detail = itemDetail010.Text;
                Weight = decimal.Parse(weightTextBox010.Text);
                Count = int.Parse(countTextBox010.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox010.Text);
                amount = money10 + money10 * Tax / 100; //decimal.Parse(moneyTextBox010.Text);
                Remarks = remarks010.Text;

                DataTable dt13 = new DataTable();
                string sql_str13 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item10 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory10 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str13, conn);
                adapter.Fill(dt13);

                #region "履歴"
                DataTable dt13re = new DataTable();
                string sql_str13_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

                
                adapter = new NpgsqlDataAdapter(sql_str13_re, conn);
                adapter.Fill(dt13re);
                #endregion
                }
                #endregion
            #region "12行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox011.Text) && !(unitPriceTextBox011.Text == "単価 -> 重量 or 数量"))
            {
                record = 12;
                //mainCategory = mainCategoryCode011;
                //item = itemCode011;
                #region "大分類コード"
                DataTable main11dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_main11 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox011.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main11, conn);
                adapter.Fill(main11dt);
                DataRow dataRow21;
                dataRow21 = main11dt.Rows[0];
                int mainCategory11 = (int)dataRow21["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item11dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_item11 = "select * from item_m where item_name = '" + itemComboBox011.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item11, conn);
                adapter.Fill(item11dt);
                DataRow dataRow22;
                dataRow22 = item11dt.Rows[0];
                int item11 = (int)dataRow22["item_code"];
                #endregion
                Detail = itemDetail011.Text;
                Weight = decimal.Parse(weightTextBox011.Text);
                Count = int.Parse(countTextBox011.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox011.Text);
                amount = money11 + money11 * Tax / 100; //decimal.Parse(moneyTextBox011.Text);
                Remarks = remarks011.Text;

                DataTable dt14 = new DataTable();
                string sql_str14 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item11 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory11 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str14, conn);
                adapter.Fill(dt14);

                #region "履歴"
                DataTable dt14re = new DataTable();
                string sql_str14_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

                
                adapter = new NpgsqlDataAdapter(sql_str14_re, conn);
                adapter.Fill(dt14re);
                    #endregion
                }
                #endregion
            #region "13行目"
                if (!string.IsNullOrEmpty(unitPriceTextBox012.Text) && !(unitPriceTextBox012.Text == "単価 -> 重量 or 数量"))
            {
                record = 13;
                //mainCategory = mainCategoryCode012;
                //item = itemCode012;
                #region "大分類コード"
                DataTable main12dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_main12 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox012.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_main12, conn);
                adapter.Fill(main12dt);
                DataRow dataRow23;
                dataRow23 = main12dt.Rows[0];
                int mainCategory12 = (int)dataRow23["main_category_code"];
                #endregion
                #region "品名コード"
                DataTable item12dt = new DataTable();
                conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                string sql_item12 = "select * from item_m where item_name = '" + itemComboBox012.Text + "';";
                adapter = new NpgsqlDataAdapter(sql_item12, conn);
                adapter.Fill(item12dt);
                DataRow dataRow24;
                dataRow24 = item12dt.Rows[0];
                int item12 = (int)dataRow24["item_code"];
                #endregion
                Detail = itemDetail012.Text;
                Weight = decimal.Parse(weightTextBox012.Text);
                Count = int.Parse(countTextBox012.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox012.Text);
                amount = money12 + money12 * Tax / 100; //decimal.Parse(moneyTextBox012.Text);
                Remarks = remarks012.Text;

                DataTable dt15 = new DataTable();
                string sql_str15 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item12 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory12 + ",'" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str15, conn);
                adapter.Fill(dt15);

                #region "履歴"
                DataTable dt15re = new DataTable();
                string sql_str15_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

               
                adapter = new NpgsqlDataAdapter(sql_str15_re, conn);
                adapter.Fill(dt15re);
                #endregion
                }
                #endregion
            }
            #endregion
            conn.Close();
            MessageBox.Show("登録しました。");
            this.DeliveryPreviewButton.Enabled = true;
            if (access_auth != "C")
            {
                this.textBox2.Visible = true;
                this.label10.Visible = true;
            }
            
            regist++;
        }

        #endregion

        #region"画像選択"
        private void financialButton_Click(object sender, EventArgs e)//ファイルを選択
        {
            if (string.IsNullOrEmpty(typeTextBox.Text))
            {
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                OpenFileDialog op = new OpenFileDialog();

                op.Title = "ファイルを開く";
                op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
                op.Filter = "すべてのファイル(*.*)|*.*";
                op.FilterIndex = 1;

                DialogResult dialog = op.ShowDialog();
                if (dialog == DialogResult.OK)
                {
                    path = op.FileName;

                    articlesTextBox.Text = path;

                }
                else if (dialog == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        private void taxCertificate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(typeTextBox.Text))
            {
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                OpenFileDialog op = new OpenFileDialog();

                op.Title = "ファイルを開く";
                op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
                op.Filter = "すべてのファイル(*.*)|*.*";
                op.FilterIndex = 1;

                DialogResult dialog = op.ShowDialog();
                if (dialog == DialogResult.OK)
                {
                    path = op.FileName;
                    taxCertificateTextBox.Text = path;
                }
                else if (dialog == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        private void sealCertificationButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(typeTextBox.Text))
            {
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                OpenFileDialog op = new OpenFileDialog();

                op.Title = "ファイルを開く";
                op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
                op.Filter = "すべてのファイル(*.*)|*.*";
                op.FilterIndex = 1;

                DialogResult dialog = op.ShowDialog();
                if (dialog == DialogResult.OK)
                {
                    path = op.FileName;
                    sealCertificationTextBox.Text = path;
                }
                else if (dialog == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        private void residenceButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(typeTextBox.Text))
            {
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                OpenFileDialog op = new OpenFileDialog();

                op.Title = "ファイルを開く";
                op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
                op.Filter = "すべてのファイル(*.*)|*.*";
                op.FilterIndex = 1;

                DialogResult dialog = op.ShowDialog();
                if (dialog == DialogResult.OK)
                {
                    path = op.FileName;
                    residenceCardTextBox.Text = path;
                }
                else if (dialog == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        private void AntiqueSelectionButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(typeTextBox.Text))
            {
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                OpenFileDialog op = new OpenFileDialog();

                op.Title = "ファイルを開く";
                op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
                op.Filter = "すべてのファイル(*.*)|*.*";
                op.FilterIndex = 1;

                DialogResult dialog = op.ShowDialog();
                if (dialog == DialogResult.OK)
                {
                    path = op.FileName;
                    antiqueLicenceTextBox.Text = path;
                }
                else if (dialog == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        private void AntiqueSelectionButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(typeTextBox.Text))
            {
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                OpenFileDialog op = new OpenFileDialog();

                op.Title = "ファイルを開く";
                op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
                op.Filter = "すべてのファイル(*.*)|*.*";
                op.FilterIndex = 1;

                DialogResult dialog = op.ShowDialog();
                if (dialog == DialogResult.OK)
                {
                    path = op.FileName;
                    antiqueLicenceTextBox2.Text = path;
                }
                else if (dialog == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        #endregion

        #region"画像確認"
        private void financialCheckButton_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = @articlesTextBox.Text;
        }

        private void taxCertificateCheckButton_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = @taxCertificateTextBox.Text;
        }

        private void sealCertificationCheckButton_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = @sealCertificationTextBox.Text;
        }

        private void residenceCheckButton_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = @residenceCardTextBox.Text;
        }

        private void AntiqueLicenceCheckButton1_Click(object sender, EventArgs e)
        {
            pictureBox2.ImageLocation = @antiqueLicenceTextBox.Text;
        }

        private void AntiqueLicenceCheckButton2_Click(object sender, EventArgs e)
        {
            pictureBox3.ImageLocation = @antiqueLicenceTextBox2.Text;
        }
        #endregion

        private void residenceCardTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(residenceCardTextBox.Text))
            {
                residencePeriod.Visible = true;
                residencePerioddatetimepicker.Visible = true;
            }
        }

        #region"計算書　印刷プレビュー"
        private void previewButton_Click(object sender, EventArgs e)
        {
            //計算書印刷プレビュー
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            //pd.Print();
            printPreviewDialog1.Document = pd;
            DialogResult dr = printPreviewDialog1.ShowDialog();
        }

        //印刷プレビューで表示する項目は品名、品物詳細、重量、単価、数量、金額、備考
        /* 伝票番号で紐付け
         * 左上に表示（左上であれば OK ）
         * 法人           ->会社名/店舗名/担当者名/TEL/FAX
         * 個人事業主     ->氏名/TEL/FAX
         * 一般           ->氏名/住所/TEL/FAX/生年月日/職業
         */

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
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
            int d = 50;
            int d3 = 60;
            int height = 20;
            int h = 4;

            int d2 = 20;    //  調整

            int x1 = 50;           //表の x 座標
            int x2 = x1 + width;
            int x3 = x2 + width;
            int x4 = x3 + width;
            int x5 = x4 + width;
            int x6 = x5 + width;
            int x7 = x6 + width;

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

            #endregion

            #region"法人、個人事業主、一般で共通"
            //一番上

            e.Graphics.DrawString("計算書（お客様控え）", font1, brush, new PointF(280, 30));

            //右上

            e.Graphics.DrawString("株式会社 Flawless", font, brush, new PointF(550, 80));
            e.Graphics.DrawString("〒110-0005", font, brush, new PointF(550, 100));
            e.Graphics.DrawString("東京都台東区上野5-8-5 フロンティア秋葉原３階", font, brush, new PointF(550, 120));
            e.Graphics.DrawString("受け渡し日", font, brush, new PointF(600, 140));
            e.Graphics.DrawString("決済日", font, brush, new PointF(600, 160));
            e.Graphics.DrawString("決済方法", font, brush, new PointF(600, 180));

            e.Graphics.DrawString("：" + deliveryDateBox.Value.ToShortDateString(), font, brush, new PointF(600 + d3, 140));    //受け渡し日
            e.Graphics.DrawString("：" + settlementBox.Value.ToShortDateString(), font, brush, new PointF(600 + d3, 160));    //決済日
            e.Graphics.DrawString("：" + paymentMethodsComboBox.Text, font, brush, new PointF(600 + d3, 180));    //決済方法

            //真ん中

            e.Graphics.DrawString("計算書（兼領収書）", font1, brush, new PointF(280, 530));
            e.Graphics.DrawString("株式会社 Flawless 御中", font2, brush, new PointF(x1, 600));

            //右下

            e.Graphics.DrawString("受け渡し日", font, brush, new PointF(570, 950));
            e.Graphics.DrawString("決済日", font, brush, new PointF(570, 970));
            e.Graphics.DrawString("決済方法", font, brush, new PointF(570, 990));
            e.Graphics.DrawString("上記の通り領収致しました。", font, brush, new PointF(520, 1010));
            e.Graphics.DrawString("署名", font3, brush, new PointF(470, 1050));
            e.Graphics.DrawString("印", font, brush, new PointF(730, 1000));
            e.Graphics.DrawString("署名のないものは無効", font, brush, new PointF(570, 1080));

            e.Graphics.DrawString("：" + deliveryDateBox.Value.ToShortDateString(), font, brush, new PointF(570 + d3, 950));    //受け渡し日
            e.Graphics.DrawString("：" + settlementBox.Value.ToShortDateString(), font, brush, new PointF(570 + d3, 970));    //決済日
            e.Graphics.DrawString("：" + paymentMethodsComboBox.Text, font, brush, new PointF(570 + d3, 990));    //決済方法
            e.Graphics.DrawString("：", font3, brush, new PointF(450 + d, 1050));   //署名

            e.Graphics.DrawRectangle(p, 470, 1070, 250, 0.1f);            //署名下の下線
            e.Graphics.DrawRectangle(p, 730 - d2, 1000 - d2, d2 * 2 + 10, d2 * 2 + 10);         //印の枠
            #endregion

            #region"ページ上のお客様情報"
            //法人の場合
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            if (type == 0)
            {
                DataTable clientDt1 = new DataTable();
                string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + client_staff_name + "' and address = '" + address + "';";
                conn.Open();

                NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str_sql_corporate, conn);
                adapter1.Fill(clientDt1);

                DataRow row2;
                row2 = clientDt1.Rows[0];

                string tel = row2["phone_number"].ToString();
                string fax = row2["fax_number"].ToString();

                conn.Close();

                e.Graphics.DrawString("会社名", font, brush, new PointF(x1, 80));
                e.Graphics.DrawString("店舗名", font, brush, new PointF(x1, 100));
                e.Graphics.DrawString("担当者名", font, brush, new PointF(x1, 120));
                e.Graphics.DrawString("TEL", font, brush, new PointF(x1, 140));
                e.Graphics.DrawString("FAX", font, brush, new PointF(x1, 160));

                e.Graphics.DrawString("：" + companyTextBox.Text, font, brush, new PointF(x1 + d, 80));
                e.Graphics.DrawString("：" + shopNameTextBox.Text, font, brush, new PointF(x1 + d, 100));
                e.Graphics.DrawString("：" + clientNameTextBox.Text + "様", font, brush, new PointF(x1 + d, 120));
                e.Graphics.DrawString("：" + tel, font, brush, new PointF(x1 + d, 140));
                e.Graphics.DrawString("：" + fax, font, brush, new PointF(x1 + d, 160));
            }

            //個人の場合
            else if (type == 1)
            {
                DataTable clientDt2 = new DataTable();
                string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";

                conn.Open();

                NpgsqlDataAdapter adapter2 = new NpgsqlDataAdapter(str_sql_individual, conn);
                adapter2.Fill(clientDt2);

                DataRow row2;
                row2 = clientDt2.Rows[0];
                address = row2["address"].ToString();
                string tel = row2["phone_number"].ToString();
                string fax = row2["fax_number"].ToString();


                conn.Close();

                if (!string.IsNullOrEmpty(registerDateTextBox2.Text))     //個人事業主の場合
                {
                    e.Graphics.DrawString("氏名", font, brush, new PointF(x1, 80));
                    e.Graphics.DrawString("TEL", font, brush, new PointF(x1, 100));
                    e.Graphics.DrawString("FAX", font, brush, new PointF(x1, 120));

                    e.Graphics.DrawString("：" + companyTextBox.Text + "様", font, brush, new PointF(x1 + d, 80));
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

                    e.Graphics.DrawString("：" + companyTextBox.Text + "様", font, brush, new PointF(x1 + d, 80));
                    e.Graphics.DrawString("：" + address, font, brush, new PointF(x1 + d, 100));
                    e.Graphics.DrawString("：" + tel, font, brush, new PointF(x1 + d, 120));
                    e.Graphics.DrawString("：" + fax, font, brush, new PointF(x1 + d, 140));
                    e.Graphics.DrawString("：" + shopNameTextBox.Text, font, brush, new PointF(x1 + d, 160)); ;
                    e.Graphics.DrawString("：" + clientNameTextBox.Text, font, brush, new PointF(x1 + d, 180));
                }
            }
            #endregion

            #region"ページ下のお客様情報"
            //法人の場合

            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            if (type == 0)
            {
                DataTable clientDt1 = new DataTable();
                string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + client_staff_name + "' and address = '" + address + "';";
                conn.Open();

                NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str_sql_corporate, conn);
                adapter1.Fill(clientDt1);

                DataRow row2;
                row2 = clientDt1.Rows[0];

                string tel = row2["phone_number"].ToString();
                string fax = row2["fax_number"].ToString();


                conn.Close();

                e.Graphics.DrawString("会社名", font, brush, new PointF(x1, 950));
                e.Graphics.DrawString("店舗名", font, brush, new PointF(x1, 970));
                e.Graphics.DrawString("担当者名", font, brush, new PointF(x1, 990));
                e.Graphics.DrawString("TEL", font, brush, new PointF(x1, 1010));
                e.Graphics.DrawString("FAX", font, brush, new PointF(x1, 1030));

                e.Graphics.DrawString("：" + companyTextBox.Text, font, brush, new PointF(x1 + d, 950));
                e.Graphics.DrawString("：" + shopNameTextBox.Text, font, brush, new PointF(x1 + d, 970));
                e.Graphics.DrawString("：" + clientNameTextBox.Text, font, brush, new PointF(x1 + d, 990));
                e.Graphics.DrawString("：" + tel, font, brush, new PointF(x1 + d, 1010));
                e.Graphics.DrawString("：" + fax, font, brush, new PointF(x1 + d, 1030));
            }

            else if (type == 1)
            {
                DataTable clientDt2 = new DataTable();
                string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";

                conn.Open();

                NpgsqlDataAdapter adapter2 = new NpgsqlDataAdapter(str_sql_individual, conn);
                adapter2.Fill(clientDt2);

                DataRow row2;
                row2 = clientDt2.Rows[0];
                address = row2["address"].ToString();
                string tel = row2["phone_number"].ToString();
                string fax = row2["fax_number"].ToString();


                conn.Close();

                if (!string.IsNullOrEmpty(registerDateTextBox2.Text))     //個人事業主の場合
                {
                    e.Graphics.DrawString("氏名", font, brush, new PointF(x1, 950));
                    e.Graphics.DrawString("TEL", font, brush, new PointF(x1, 970));
                    e.Graphics.DrawString("FAX", font, brush, new PointF(x1, 990));

                    e.Graphics.DrawString("：" + companyTextBox.Text, font, brush, new PointF(x1 + d, 950));
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

                    e.Graphics.DrawString("：" + companyTextBox.Text, font, brush, new PointF(x1 + d, 950));
                    e.Graphics.DrawString("：" + address, font, brush, new PointF(x1 + d, 970));
                    e.Graphics.DrawString("：" + tel, font, brush, new PointF(x1 + d, 990));
                    e.Graphics.DrawString("：" + fax, font, brush, new PointF(x1 + d, 1010));
                    e.Graphics.DrawString("：" + shopNameTextBox.Text, font, brush, new PointF(x1 + d, 1030)); ;
                    e.Graphics.DrawString("：" + clientNameTextBox.Text, font, brush, new PointF(x1 + d, 1050));
                }
            }
            #endregion

            #region"印刷プレビュー：上の表の中身"

            #region"１行目"
            e.Graphics.DrawString("品目", font3, brush, new RectangleF(x1, y1, width, height), stringFormat);
            e.Graphics.DrawString("品物詳細", font3, brush, new RectangleF(x2, y1, width, height), stringFormat);
            e.Graphics.DrawString("重量", font3, brush, new RectangleF(x3, y1, width, height), stringFormat);
            e.Graphics.DrawString("単価", font3, brush, new RectangleF(x4, y1, width, height), stringFormat);
            e.Graphics.DrawString("数量", font3, brush, new RectangleF(x5, y1, width, height), stringFormat);
            e.Graphics.DrawString("金額", font3, brush, new RectangleF(x6, y1, width, height), stringFormat);
            e.Graphics.DrawString("備考", font3, brush, new RectangleF(x7, y1, width, height), stringFormat);
            #endregion

            #region"２行目"
            e.Graphics.DrawString(itemComboBox0.Text, font, brush, new RectangleF(x1, y12 + h, width, height), stringFormat);
            e.Graphics.DrawString(itemDetail0.Text, font, brush, new RectangleF(x2, y12 + h, width, height), stringFormat);
            e.Graphics.DrawString(weightTextBox0.Text, font, brush, new RectangleF(x3, y12 + h, width, height), stringFormat1);
            e.Graphics.DrawString(unitPriceTextBox0.Text, font, brush, new RectangleF(x4, y12 + h, width, height), stringFormat1);
            e.Graphics.DrawString(countTextBox0.Text, font, brush, new RectangleF(x5, y12 + h, width, height), stringFormat1);
            e.Graphics.DrawString(moneyTextBox0.Text, font, brush, new RectangleF(x6, y12 + h, width, height), stringFormat1);
            e.Graphics.DrawString(remarks0.Text, font, brush, new RectangleF(x7, y12 + h, width, height), stringFormat2);
            #endregion

            #region"３行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox1.Text) && unitPriceTextBox1.Text != "0" && unitPriceTextBox1.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox1.Text, font, brush, new RectangleF(x1, y13 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail1.Text, font, brush, new RectangleF(x2, y13 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox1.Text, font, brush, new RectangleF(x3, y13 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox0.Text, font, brush, new RectangleF(x4, y13 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox1.Text, font, brush, new RectangleF(x5, y13 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox1.Text, font, brush, new RectangleF(x6, y13 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks1.Text, font, brush, new RectangleF(x7, y13 + h, width, height), stringFormat2);
            }
            #endregion

            #region"４行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox2.Text) && unitPriceTextBox2.Text != "0" && unitPriceTextBox2.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox2.Text, font, brush, new RectangleF(x1, y14 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail2.Text, font, brush, new RectangleF(x2, y14 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox2.Text, font, brush, new RectangleF(x3, y14 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox2.Text, font, brush, new RectangleF(x4, y14 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox2.Text, font, brush, new RectangleF(x5, y14 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox2.Text, font, brush, new RectangleF(x6, y14 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks2.Text, font, brush, new RectangleF(x7, y14 + h, width, height), stringFormat2);
            }
            #endregion

            #region"５行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox3.Text) && unitPriceTextBox3.Text != "0" && unitPriceTextBox3.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox3.Text, font, brush, new RectangleF(x1, y15 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail3.Text, font, brush, new RectangleF(x2, y15 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox3.Text, font, brush, new RectangleF(x3, y15 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox3.Text, font, brush, new RectangleF(x4, y15 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox3.Text, font, brush, new RectangleF(x5, y15 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox3.Text, font, brush, new RectangleF(x6, y15 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks3.Text, font, brush, new RectangleF(x7, y15 + h, width, height), stringFormat2);
            }
            #endregion

            #region"６行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox4.Text) && unitPriceTextBox4.Text != "0" && unitPriceTextBox4.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox4.Text, font, brush, new RectangleF(x1, y16 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail4.Text, font, brush, new RectangleF(x2, y16 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox4.Text, font, brush, new RectangleF(x3, y16 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox4.Text, font, brush, new RectangleF(x4, y16 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox4.Text, font, brush, new RectangleF(x5, y16 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox4.Text, font, brush, new RectangleF(x6, y16 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks4.Text, font, brush, new RectangleF(x7, y16 + h, width, height), stringFormat2);
            }
            #endregion

            #region"７行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox5.Text) && unitPriceTextBox5.Text != "0" && unitPriceTextBox5.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox5.Text, font, brush, new RectangleF(x1, y17 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail5.Text, font, brush, new RectangleF(x2, y17 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox5.Text, font, brush, new RectangleF(x3, y17 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox5.Text, font, brush, new RectangleF(x4, y17 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox5.Text, font, brush, new RectangleF(x5, y17 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox5.Text, font, brush, new RectangleF(x6, y17 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks5.Text, font, brush, new RectangleF(x7, y17 + h, width, height), stringFormat2);
            }
            #endregion

            #region"８行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox6.Text) && unitPriceTextBox6.Text != "0" && unitPriceTextBox6.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox6.Text, font, brush, new RectangleF(x1, y18 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail6.Text, font, brush, new RectangleF(x2, y18 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox6.Text, font, brush, new RectangleF(x3, y18 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox6.Text, font, brush, new RectangleF(x4, y18 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox6.Text, font, brush, new RectangleF(x5, y18 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox6.Text, font, brush, new RectangleF(x6, y18 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks6.Text, font, brush, new RectangleF(x7, y18 + h, width, height), stringFormat2);
            }
            #endregion

            #region"９行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox7.Text) && unitPriceTextBox7.Text != "0" && unitPriceTextBox7.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox7.Text, font, brush, new RectangleF(x1, y19 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail7.Text, font, brush, new RectangleF(x2, y19 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox7.Text, font, brush, new RectangleF(x3, y19 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox7.Text, font, brush, new RectangleF(x4, y19 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox7.Text, font, brush, new RectangleF(x5, y19 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox7.Text, font, brush, new RectangleF(x6, y19 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks7.Text, font, brush, new RectangleF(x7, y19 + h, width, height), stringFormat2);
            }
            #endregion

            #region"１０行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox8.Text) && unitPriceTextBox8.Text != "0" && unitPriceTextBox8.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox8.Text, font, brush, new RectangleF(x1, y110 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail8.Text, font, brush, new RectangleF(x2, y110 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox8.Text, font, brush, new RectangleF(x3, y110 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox8.Text, font, brush, new RectangleF(x4, y110 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox8.Text, font, brush, new RectangleF(x5, y110 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox8.Text, font, brush, new RectangleF(x6, y110 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks8.Text, font, brush, new RectangleF(x7, y110 + h, width, height), stringFormat2);
            }
            #endregion

            #region"１１行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox9.Text) && unitPriceTextBox9.Text != "0" && unitPriceTextBox9.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox9.Text, font, brush, new RectangleF(x1, y111 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail9.Text, font, brush, new RectangleF(x2, y111 + h, width, height), stringFormat1);
                e.Graphics.DrawString(weightTextBox9.Text, font, brush, new RectangleF(x3, y111 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox9.Text, font, brush, new RectangleF(x4, y111 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox9.Text, font, brush, new RectangleF(x5, y111 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox9.Text, font, brush, new RectangleF(x6, y111 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks9.Text, font, brush, new RectangleF(x7, y111 + h, width, height), stringFormat2);
            }
            #endregion

            #region"１２行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox10.Text) && unitPriceTextBox10.Text != "0" && unitPriceTextBox10.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox10.Text, font, brush, new RectangleF(x1, y112 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail10.Text, font, brush, new RectangleF(x2, y112 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox10.Text, font, brush, new RectangleF(x3, y112 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox10.Text, font, brush, new RectangleF(x4, y112 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox10.Text, font, brush, new RectangleF(x5, y112 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox10.Text, font, brush, new RectangleF(x6, y112 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks10.Text, font, brush, new RectangleF(x7, y112 + h, width, height), stringFormat2);
            }
            #endregion

            #region"１３行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox11.Text) && unitPriceTextBox11.Text != "0" && unitPriceTextBox11.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox11.Text, font, brush, new RectangleF(x1, y113 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail11.Text, font, brush, new RectangleF(x2, y113 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox11.Text, font, brush, new RectangleF(x3, y113 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox11.Text, font, brush, new RectangleF(x4, y113 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox11.Text, font, brush, new RectangleF(x5, y113 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox11.Text, font, brush, new RectangleF(x6, y113 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks11.Text, font, brush, new RectangleF(x7, y113 + h, width, height), stringFormat2);
            }
            #endregion

            #region"１４行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox12.Text) && unitPriceTextBox12.Text != "0" && unitPriceTextBox12.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox12.Text, font, brush, new RectangleF(x1, y114 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail12.Text, font, brush, new RectangleF(x2, y114 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox12.Text, font, brush, new RectangleF(x3, y114 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox12.Text, font, brush, new RectangleF(x4, y114 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox12.Text, font, brush, new RectangleF(x5, y114 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox12.Text, font, brush, new RectangleF(x6, y114 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks12.Text, font, brush, new RectangleF(x7, y114 + h, width, height), stringFormat2);
            }
            #endregion

            #endregion

            #region"印刷プレビュー：下の表の中身"
            //Rectangle((左上座標),幅,高さ,フォーマット)：フォーマットで位置・形式を指
            #region"１行目"
            e.Graphics.DrawString("品目", font3, brush, new RectangleF(x1, y2, width, height), stringFormat);
            e.Graphics.DrawString("品物詳細", font3, brush, new RectangleF(x2, y2, width, height), stringFormat);
            e.Graphics.DrawString("重量", font3, brush, new RectangleF(x3, y2, width, height), stringFormat);
            e.Graphics.DrawString("単価", font3, brush, new RectangleF(x4, y2, width, height), stringFormat);
            e.Graphics.DrawString("数量", font3, brush, new RectangleF(x5, y2, width, height), stringFormat);
            e.Graphics.DrawString("金額", font3, brush, new RectangleF(x6, y2, width, height), stringFormat);
            e.Graphics.DrawString("備考", font3, brush, new RectangleF(x7, y2, width, height), stringFormat);
            #endregion

            #region"２行目"
            e.Graphics.DrawString(itemComboBox0.Text, font, brush, new RectangleF(x1, y22 + h, width, height), stringFormat);
            e.Graphics.DrawString(itemDetail0.Text, font, brush, new RectangleF(x2, y22 + h, width, height), stringFormat);
            e.Graphics.DrawString(weightTextBox0.Text, font, brush, new RectangleF(x3, y22 + h, width, height), stringFormat1);
            e.Graphics.DrawString(unitPriceTextBox0.Text, font, brush, new RectangleF(x4, y22 + h, width, height), stringFormat1);
            e.Graphics.DrawString(countTextBox0.Text, font, brush, new RectangleF(x5, y22 + h, width, height), stringFormat1);
            e.Graphics.DrawString(moneyTextBox0.Text, font, brush, new RectangleF(x6, y22 + h, width, height), stringFormat1);
            e.Graphics.DrawString(remarks0.Text, font, brush, new RectangleF(x7, y22 + h, width, height), stringFormat2);
            #endregion

            #region"３行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox1.Text) && unitPriceTextBox1.Text != "0" && unitPriceTextBox1.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox1.Text, font, brush, new RectangleF(x1, y23 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail1.Text, font, brush, new RectangleF(x2, y23 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox1.Text, font, brush, new RectangleF(x3, y23 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox0.Text, font, brush, new RectangleF(x4, y23 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox1.Text, font, brush, new RectangleF(x5, y23 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox1.Text, font, brush, new RectangleF(x6, y23 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks1.Text, font, brush, new RectangleF(x7, y23 + h, width, height), stringFormat2);
            }
            #endregion

            #region"４行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox2.Text) && unitPriceTextBox2.Text != "0" && unitPriceTextBox2.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox2.Text, font, brush, new RectangleF(x1, y24 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail2.Text, font, brush, new RectangleF(x2, y24 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox2.Text, font, brush, new RectangleF(x3, y24 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox2.Text, font, brush, new RectangleF(x4, y24 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox2.Text, font, brush, new RectangleF(x5, y24 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox2.Text, font, brush, new RectangleF(x6, y24 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks2.Text, font, brush, new RectangleF(x7, y24 + h, width, height), stringFormat2);
            }
            #endregion

            #region"５行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox3.Text) && unitPriceTextBox3.Text != "0" && unitPriceTextBox3.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox3.Text, font, brush, new RectangleF(x1, y25 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail3.Text, font, brush, new RectangleF(x2, y25 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox3.Text, font, brush, new RectangleF(x3, y25 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox3.Text, font, brush, new RectangleF(x4, y25 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox3.Text, font, brush, new RectangleF(x5, y25 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox3.Text, font, brush, new RectangleF(x6, y25 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks3.Text, font, brush, new RectangleF(x7, y25 + h, width, height), stringFormat2);
            }
            #endregion

            #region"６行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox4.Text) && unitPriceTextBox4.Text != "0" && unitPriceTextBox4.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox4.Text, font, brush, new RectangleF(x1, y26 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail4.Text, font, brush, new RectangleF(x2, y26 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox4.Text, font, brush, new RectangleF(x3, y26 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox4.Text, font, brush, new RectangleF(x4, y26 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox4.Text, font, brush, new RectangleF(x5, y26 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox4.Text, font, brush, new RectangleF(x6, y26 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks4.Text, font, brush, new RectangleF(x7, y26 + h, width, height), stringFormat2);
            }
            #endregion

            #region"７行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox5.Text) && unitPriceTextBox5.Text != "0" && unitPriceTextBox5.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox5.Text, font, brush, new RectangleF(x1, y27 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail5.Text, font, brush, new RectangleF(x2, y27 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox5.Text, font, brush, new RectangleF(x3, y27 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox5.Text, font, brush, new RectangleF(x4, y27 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox5.Text, font, brush, new RectangleF(x5, y27 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox5.Text, font, brush, new RectangleF(x6, y27 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks5.Text, font, brush, new RectangleF(x7, y27 + h, width, height), stringFormat2);
            }
            #endregion

            #region"８行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox6.Text) && unitPriceTextBox6.Text != "0" && unitPriceTextBox6.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox6.Text, font, brush, new RectangleF(x1, y28 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail6.Text, font, brush, new RectangleF(x2, y28 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox6.Text, font, brush, new RectangleF(x3, y28 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox6.Text, font, brush, new RectangleF(x4, y28 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox6.Text, font, brush, new RectangleF(x5, y28 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox6.Text, font, brush, new RectangleF(x6, y28 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks6.Text, font, brush, new RectangleF(x7, y28 + h, width, height), stringFormat2);
            }
            #endregion

            #region"９行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox7.Text) && unitPriceTextBox7.Text != "0" && unitPriceTextBox7.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox7.Text, font, brush, new RectangleF(x1, y29 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail7.Text, font, brush, new RectangleF(x2, y29 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox7.Text, font, brush, new RectangleF(x3, y29 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox7.Text, font, brush, new RectangleF(x4, y29 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox7.Text, font, brush, new RectangleF(x5, y29 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox7.Text, font, brush, new RectangleF(x6, y29 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks7.Text, font, brush, new RectangleF(x7, y29 + h, width, height), stringFormat2);
            }
            #endregion

            #region"１０行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox8.Text) && unitPriceTextBox8.Text != "0" && unitPriceTextBox8.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox8.Text, font, brush, new RectangleF(x1, y210 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail8.Text, font, brush, new RectangleF(x2, y210 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox8.Text, font, brush, new RectangleF(x3, y210 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox8.Text, font, brush, new RectangleF(x4, y210 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox8.Text, font, brush, new RectangleF(x5, y210 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox8.Text, font, brush, new RectangleF(x6, y210 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks8.Text, font, brush, new RectangleF(x7, y210 + h, width, height), stringFormat2);
            }
            #endregion

            #region"１１行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox9.Text) && unitPriceTextBox9.Text != "0" && unitPriceTextBox9.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox9.Text, font, brush, new RectangleF(x1, y211 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail9.Text, font, brush, new RectangleF(x2, y211 + h, width, height), stringFormat1);
                e.Graphics.DrawString(weightTextBox9.Text, font, brush, new RectangleF(x3, y211 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox9.Text, font, brush, new RectangleF(x4, y211 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox9.Text, font, brush, new RectangleF(x5, y211 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox9.Text, font, brush, new RectangleF(x6, y211 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks9.Text, font, brush, new RectangleF(x7, y211 + h, width, height), stringFormat2);
            }
            #endregion

            #region"１２行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox10.Text) && unitPriceTextBox10.Text != "0" && unitPriceTextBox10.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox10.Text, font, brush, new RectangleF(x1, y212 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail10.Text, font, brush, new RectangleF(x2, y212 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox10.Text, font, brush, new RectangleF(x3, y212 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox10.Text, font, brush, new RectangleF(x4, y212 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox10.Text, font, brush, new RectangleF(x5, y212 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox10.Text, font, brush, new RectangleF(x6, y212 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks10.Text, font, brush, new RectangleF(x7, y212 + h, width, height), stringFormat2);
            }
            #endregion

            #region"１３行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox11.Text) && unitPriceTextBox11.Text != "0" && unitPriceTextBox11.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox11.Text, font, brush, new RectangleF(x1, y213 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail11.Text, font, brush, new RectangleF(x2, y213 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox11.Text, font, brush, new RectangleF(x3, y213 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox11.Text, font, brush, new RectangleF(x4, y213 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox11.Text, font, brush, new RectangleF(x5, y213 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox11.Text, font, brush, new RectangleF(x6, y213 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks11.Text, font, brush, new RectangleF(x7, y213 + h, width, height), stringFormat2);
            }
            #endregion

            #region"１４行目"
            if (!string.IsNullOrEmpty(unitPriceTextBox12.Text) && unitPriceTextBox12.Text != "0" && unitPriceTextBox12.Text != "単価 -> 重量 or 数量")
            {
                e.Graphics.DrawString(itemComboBox12.Text, font, brush, new RectangleF(x1, y214 + h, width, height), stringFormat);
                e.Graphics.DrawString(itemDetail12.Text, font, brush, new RectangleF(x2, y214 + h, width, height), stringFormat);
                e.Graphics.DrawString(weightTextBox12.Text, font, brush, new RectangleF(x3, y214 + h, width, height), stringFormat1);
                e.Graphics.DrawString(unitPriceTextBox12.Text, font, brush, new RectangleF(x4, y214 + h, width, height), stringFormat1);
                e.Graphics.DrawString(countTextBox12.Text, font, brush, new RectangleF(x5, y214 + h, width, height), stringFormat1);
                e.Graphics.DrawString(moneyTextBox12.Text, font, brush, new RectangleF(x6, y214 + h, width, height), stringFormat1);
                e.Graphics.DrawString(remarks12.Text, font, brush, new RectangleF(x7, y214 + h, width, height), stringFormat2);
            }
            #endregion

            #endregion

            #region"上の表　枠線"
            //Rectangle(左上隅の x 座標, 左上隅の y 座標, 四角形の幅, 四角形の高さ)
            #region"１行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y1, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y1, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y1, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y1, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y1, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y1, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y1, width, height));
            #endregion

            #region"２行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y12, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y12, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y12, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y12, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y12, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y12, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y12, width, height));
            #endregion

            #region"３行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y13, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y13, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y13, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y13, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y13, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y13, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y13, width, height));
            #endregion

            #region"４行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y14, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y14, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y14, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y14, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y14, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y14, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y14, width, height));
            #endregion

            #region"５行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y15, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y15, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y15, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y15, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y15, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y15, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y15, width, height));
            #endregion

            #region"６行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y16, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y16, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y16, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y16, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y16, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y16, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y16, width, height));
            #endregion

            #region"７行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y17, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y17, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y17, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y17, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y17, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y17, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y17, width, height));
            #endregion

            #region"８行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y18, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y18, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y18, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y18, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y18, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y18, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y18, width, height));
            #endregion

            #region"９行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y19, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y19, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y19, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y19, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y19, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y19, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y19, width, height));
            #endregion

            #region"１０行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y110, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y110, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y110, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y110, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y110, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y110, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y110, width, height));
            #endregion

            #region"１１行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y111, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y111, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y111, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y111, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y111, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y111, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y111, width, height));
            #endregion

            #region"１２行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y112, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y112, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y112, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y112, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y112, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y112, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y112, width, height));
            #endregion

            #region"１３行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y113, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y113, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y113, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y113, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y113, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y113, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y113, width, height));
            #endregion

            #region"１４行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y114, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y114, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y114, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y114, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y114, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y114, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y114, width, height));
            #endregion

            #endregion

            #region"下の表　枠線"

            #region"１行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y2, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y2, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y2, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y2, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y2, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y2, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y2, width, height));
            #endregion

            #region"２行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y22, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y22, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y22, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y22, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y22, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y22, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y22, width, height));
            #endregion

            #region"３行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y23, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y23, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y23, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y23, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y23, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y23, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y23, width, height));
            #endregion

            #region"４行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y24, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y24, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y24, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y24, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y24, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y24, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y24, width, height));
            #endregion

            #region"５行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y25, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y25, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y25, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y25, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y25, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y25, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y25, width, height));
            #endregion

            #region"６行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y26, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y26, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y26, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y26, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y26, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y26, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y26, width, height));
            #endregion

            #region"７行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y27, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y27, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y27, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y27, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y27, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y27, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y27, width, height));
            #endregion

            #region"８行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y28, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y28, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y28, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y28, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y28, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y28, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y28, width, height));
            #endregion

            #region"９行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y29, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y29, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y29, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y29, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y29, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y29, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y29, width, height));
            #endregion

            #region"１０行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y210, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y210, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y210, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y210, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y210, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y210, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y210, width, height));
            #endregion

            #region"１１行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y211, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y211, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y211, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y211, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y211, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y211, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y211, width, height));
            #endregion

            #region"１２行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y212, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y212, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y212, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y212, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y212, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y212, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y212, width, height));
            #endregion

            #region"１３行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y213, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y213, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y213, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y213, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y213, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y213, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y213, width, height));
            #endregion

            #region"１４行目"
            e.Graphics.DrawRectangle(p, new Rectangle(x1, y214, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x2, y214, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x3, y214, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x4, y214, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x5, y214, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x6, y214, width, height));
            e.Graphics.DrawRectangle(p, new Rectangle(x7, y214, width, height));
            #endregion

            #endregion

        }

        #endregion

        #region"計算書　成績入力画面"
        private void RecordListButton_Click(object sender, EventArgs e)
        {
            RecordList recordList = new RecordList(this, staff_id, client_staff_name, type, documentNumberTextBox.Text, Grade, AntiqueNumber, ID_Number, access_auth, pass, NameChange, CarryOver, MonthCatalog);

            this.Hide();
            recordList.Show();
        }

        #endregion

        private void DeliveryPreviewButton_Click(object sender, EventArgs e)
        {
            control = int.Parse(documentNumberTextBox2.Text);
            DeliveryPreview deliveryPreview = new DeliveryPreview(mainMenu, staff_id, type, control, access_auth, pass);
            screan = false;
            this.Close();
            deliveryPreview.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {            
            DataSearchResults dataSearchResults = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, data, pass, document, control, antiqueNumber, documentNumber, access_auth);
            screan = false;
            this.Close();
            mainMenu.Hide();
            dataSearchResults.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {            
            DataSearchResults dataSearchResults = new DataSearchResults(mainMenu, type, staff_id, name1, phoneNumber1, address1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, data, pass, document, control, antiqueNumber, documentNumber);
            screan = false;
            this.Close();
            mainMenu.Hide();
            dataSearchResults.Show();
        }
        #region "納品書　顧客選択"
        private void Client_searchButton1_Click(object sender, EventArgs e)
        {
            #region "仕様変更に伴うコメントアウト"
            /*if (!string.IsNullOrEmpty(moneyTextBox00.Text) || amount00 != 0)
            {
                DialogResult result = MessageBox.Show("データを保持しますか?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    DataTable upddt = new DataTable();
                    string str_sql_if = "select * from delivery_calc_if where control_number = " + control + ";";
                    adapter = new NpgsqlDataAdapter(str_sql_if, conn);
                    adapter.Fill(upddt);
                   #region "データ一時置き場"
                        int ControlNumber = number;
                        total = Math.Round(subSum, MidpointRounding.AwayFromZero);
                        DateTime date = DateTime.Now;
                        string dat = date.ToString("yyyy/MM/dd HH:mm:ss");
                        #region "1行目"
                        //管理番号：ControlNumber
                        int record = 1;     //行数
                                            //int mainCategory = mainCategoryCode00;
                                            //int item = itemCode00;
                        #region "大分類コード"
                        DataTable maindt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_main = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox00.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_main, conn);
                        adapter.Fill(maindt);
                        DataRow dataRow;
                        dataRow = maindt.Rows[0];
                        int mainCategory = (int)dataRow["main_category_code"];
                        #endregion
                        #region "品名コード"
                        DataTable itemdt = new DataTable();
                        conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        string sql_item = "select * from item_m where item_name = '" + itemComboBox00.Text + "';";
                        adapter = new NpgsqlDataAdapter(sql_item, conn);
                        adapter.Fill(itemdt);
                        DataRow dataRow1;
                        dataRow1 = itemdt.Rows[0];
                        int item = (int)dataRow1["item_code"];
                        #endregion
                        string Detail = itemDetail00.Text;
                        decimal Weight = decimal.Parse(weightTextBox00.Text);
                        int Count = int.Parse(countTextBox00.Text);
                        decimal UnitPrice = decimal.Parse(unitPriceTextBox00.Text);
                        decimal amount = money0 + money0 * Tax / 100;
                        string Remarks = remarks00.Text;

                        DataTable dt2 = new DataTable();
                        string sql_str2 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + mainCategory + "','" + Detail + "','" + dat + "');";

                        conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                        conn.Open();
                        adapter = new NpgsqlDataAdapter(sql_str2, conn);
                        adapter.Fill(dt2);
                        amount00 = amount;
                        #endregion
                        #region "2行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox01.Text) && !(unitPriceTextBox01.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 2;
                            //mainCategory = mainCategoryCode01;
                            //item = itemCode01;
                            #region "大分類コード"
                            DataTable main1dt = new DataTable();
                            string sql_main1 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox01.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main1, conn);
                            adapter.Fill(main1dt);
                            DataRow dataRow2;
                            dataRow2 = main1dt.Rows[0];
                            int mainCategory1 = (int)dataRow2["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item1dt = new DataTable();
                            string sql_item1 = "select * from item_m where item_name = '" + itemComboBox01.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item1, conn);
                            adapter.Fill(item1dt);
                            DataRow dataRow3;
                            dataRow3 = item1dt.Rows[0];
                            int item1 = (int)dataRow3["item_code"];
                            #endregion
                            Detail = itemDetail01.Text;
                            Weight = decimal.Parse(weightTextBox01.Text);
                            Count = int.Parse(countTextBox01.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox01.Text);
                            amount = money1 + money1 * Tax / 100;  //decimal.Parse(moneyTextBox01.Text);
                            Remarks = remarks01.Text;

                            DataTable dt4 = new DataTable();
                            string sql_str4 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item1 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory1 + ",'" + Detail + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str4, conn);
                            adapter.Fill(dt4);
                            amount01 = amount;
                        }
                        #endregion
                        #region "3行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox02.Text) && !(unitPriceTextBox02.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 3;
                            //mainCategory = mainCategoryCode02;
                            //item = itemCode02;
                            #region "大分類コード"
                            DataTable main2dt = new DataTable();
                            string sql_main2 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox02.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main2, conn);
                            adapter.Fill(main2dt);
                            DataRow dataRow4;
                            dataRow4 = main2dt.Rows[0];
                            int mainCategory2 = (int)dataRow4["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item1dt = new DataTable();
                            string sql_item2 = "select * from item_m where item_name = '" + itemComboBox02.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item2, conn);
                            adapter.Fill(item1dt);
                            DataRow dataRow5;
                            dataRow5 = item1dt.Rows[0];
                            int item2 = (int)dataRow5["item_code"];
                            #endregion
                            Detail = itemDetail02.Text;
                            Weight = decimal.Parse(weightTextBox02.Text);
                            Count = int.Parse(countTextBox02.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox02.Text);
                            amount = money2 + money2 * Tax / 100;  //decimal.Parse(moneyTextBox02.Text);
                            Remarks = remarks02.Text;

                            DataTable dt5 = new DataTable();
                            string sql_str5 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item2 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory2 + ",'" + Detail + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str5, conn);
                            adapter.Fill(dt5);
                            amount02 = amount;
                        }
                        #endregion
                        #region "4行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox03.Text) && !(unitPriceTextBox03.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 4;
                            //mainCategory = mainCategoryCode03;
                            //item = itemCode03;
                            #region "大分類コード"
                            DataTable main3dt = new DataTable();
                            string sql_main3 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox03.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main3, conn);
                            adapter.Fill(main3dt);
                            DataRow dataRow6;
                            dataRow6 = main3dt.Rows[0];
                            int mainCategory3 = (int)dataRow6["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item3dt = new DataTable();
                            string sql_item3 = "select * from item_m where item_name = '" + itemComboBox03.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item3, conn);
                            adapter.Fill(item3dt);
                            DataRow dataRow7;
                            dataRow7 = item3dt.Rows[0];
                            int item3 = (int)dataRow7["item_code"];
                            #endregion
                            Detail = itemDetail03.Text;
                            Weight = decimal.Parse(weightTextBox03.Text);
                            Count = int.Parse(countTextBox03.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox03.Text);
                            amount = money3 + money3 * Tax / 100;  //decimal.Parse(moneyTextBox03.Text);
                            Remarks = remarks03.Text;

                            DataTable dt6 = new DataTable();
                            string sql_str6 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item3 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory3 + ",'" + Detail + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str6, conn);
                            adapter.Fill(dt6);
                            amount03 = amount;
                        }
                        #endregion
                        #region "5行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox04.Text) && !(unitPriceTextBox04.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 5;
                            //mainCategory = mainCategoryCode04;
                            //item = itemCode04;
                            #region "大分類コード"
                            DataTable main4dt = new DataTable();
                            string sql_main4 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox04.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main4, conn);
                            adapter.Fill(main4dt);
                            DataRow dataRow8;
                            dataRow8 = main4dt.Rows[0];
                            int mainCategory4 = (int)dataRow8["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item4dt = new DataTable();
                            string sql_item4 = "select * from item_m where item_name = '" + itemComboBox04.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item4, conn);
                            adapter.Fill(item4dt);
                            DataRow dataRow9;
                            dataRow9 = item4dt.Rows[0];
                            int item4 = (int)dataRow9["item_code"];
                            #endregion
                            Detail = itemDetail04.Text;
                            Weight = decimal.Parse(weightTextBox04.Text);
                            Count = int.Parse(countTextBox04.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox04.Text);
                            amount = money4 + money4 * Tax / 100; //decimal.Parse(moneyTextBox04.Text);
                            Remarks = remarks04.Text;

                            DataTable dt7 = new DataTable();
                            string sql_str7 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item4 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory4 + ",'" + Detail + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str7, conn);
                            adapter.Fill(dt7);
                            amount04 = amount;
                        }
                        #endregion
                        #region "6行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox05.Text) && !(unitPriceTextBox05.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 6;
                            //mainCategory = mainCategoryCode05;
                            //item = itemCode05;
                            #region "大分類コード"
                            DataTable main5dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_main5 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox05.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main5, conn);
                            adapter.Fill(main5dt);
                            DataRow dataRow10;
                            dataRow10 = main5dt.Rows[0];
                            int mainCategory5 = (int)dataRow10["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item5dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_item5 = "select * from item_m where item_name = '" + itemComboBox05.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item5, conn);
                            adapter.Fill(item5dt);
                            DataRow dataRow11;
                            dataRow11 = item5dt.Rows[0];
                            int item5 = (int)dataRow11["item_code"];
                            #endregion
                            Detail = itemDetail05.Text;
                            Weight = decimal.Parse(weightTextBox05.Text);
                            Count = int.Parse(countTextBox05.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox05.Text);
                            amount = money5 + money5 * Tax / 100; //decimal.Parse(moneyTextBox05.Text);
                            Remarks = remarks05.Text;

                            DataTable dt8 = new DataTable();
                            string sql_str8 = "Insert into delivery_calc_if VALUES  ( '" + ControlNumber + "' ,'" + record + "' , " + item5 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory5 + ",'" + Detail + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str8, conn);
                            adapter.Fill(dt8);
                            amount05 = amount;
                        }
                        #endregion
                        #region "7行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox06.Text) && !(unitPriceTextBox06.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 7;
                            //mainCategory = mainCategoryCode06;
                            //item = itemCode06;
                            #region "大分類コード"
                            DataTable main6dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_main6 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox06.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main6, conn);
                            adapter.Fill(main6dt);
                            DataRow dataRow12;
                            dataRow12 = main6dt.Rows[0];
                            int mainCategory6 = (int)dataRow12["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item6dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_item6 = "select * from item_m where item_name = '" + itemComboBox06.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item6, conn);
                            adapter.Fill(item6dt);
                            DataRow dataRow13;
                            dataRow13 = item6dt.Rows[0];
                            int item6 = (int)dataRow13["item_code"];
                            #endregion
                            Detail = itemDetail06.Text;
                            Weight = decimal.Parse(weightTextBox06.Text);
                            Count = int.Parse(countTextBox06.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox06.Text);
                            amount = money6 + money6 * Tax / 100; //decimal.Parse(moneyTextBox06.Text);
                            Remarks = remarks06.Text;

                            DataTable dt9 = new DataTable();
                            string sql_str9 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item6 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory6 + ",'" + Detail + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str9, conn);
                            adapter.Fill(dt9);
                            amount06 = amount;
                        }
                        #endregion
                        #region "8行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox07.Text) && !(unitPriceTextBox07.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 8;
                            //mainCategory = mainCategoryCode07;
                            //item = itemCode07;
                            #region "大分類コード"
                            DataTable main7dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_main7 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox07.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main7, conn);
                            adapter.Fill(main7dt);
                            DataRow dataRow13;
                            dataRow13 = main7dt.Rows[0];
                            int mainCategory7 = (int)dataRow13["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item7dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_item7 = "select * from item_m where item_name = '" + itemComboBox07.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item7, conn);
                            adapter.Fill(item7dt);
                            DataRow dataRow14;
                            dataRow14 = item7dt.Rows[0];
                            int item7 = (int)dataRow14["item_code"];
                            #endregion
                            Detail = itemDetail07.Text;
                            Weight = decimal.Parse(weightTextBox07.Text);
                            Count = int.Parse(countTextBox07.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox07.Text);
                            amount = money7 + money7 * Tax / 100; //decimal.Parse(moneyTextBox07.Text);
                            Remarks = remarks07.Text;

                            DataTable dt10 = new DataTable();
                            string sql_str10 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item7 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory7 + ",'" + Detail + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str10, conn);
                            adapter.Fill(dt10);
                            amount07 = amount;
                        }
                        #endregion
                        #region "9行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox08.Text) && !(unitPriceTextBox08.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 9;
                            //mainCategory = mainCategoryCode08;
                            //item = itemCode08;
                            #region "大分類コード"
                            DataTable main8dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_main8 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox08.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main8, conn);
                            adapter.Fill(main8dt);
                            DataRow dataRow15;
                            dataRow15 = main8dt.Rows[0];
                            int mainCategory8 = (int)dataRow15["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item8dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_item8 = "select * from item_m where item_name = '" + itemComboBox08.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item8, conn);
                            adapter.Fill(item8dt);
                            DataRow dataRow16;
                            dataRow16 = item8dt.Rows[0];
                            int item8 = (int)dataRow16["item_code"];
                            #endregion
                            Detail = itemDetail08.Text;
                            Weight = decimal.Parse(weightTextBox08.Text);
                            Count = int.Parse(countTextBox08.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox08.Text);
                            amount = money8 + money8 * Tax / 100; //decimal.Parse(moneyTextBox08.Text);
                            Remarks = remarks08.Text;

                            DataTable dt11 = new DataTable();
                            string sql_str11 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item8 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory8 + ",'" + Detail + "','" + dat + ");";

                            adapter = new NpgsqlDataAdapter(sql_str11, conn);
                            adapter.Fill(dt11);
                            amount08 = amount;
                        }
                        #endregion
                        #region "10行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox09.Text) && !(unitPriceTextBox09.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 10;
                            //mainCategory = mainCategoryCode09;
                            //item = itemCode09;
                            #region "大分類コード"
                            DataTable main9dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_main9 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox09.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main9, conn);
                            adapter.Fill(main9dt);
                            DataRow dataRow17;
                            dataRow17 = main9dt.Rows[0];
                            int mainCategory9 = (int)dataRow17["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item9dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_item9 = "select * from item_m where item_name = '" + itemComboBox09.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item9, conn);
                            adapter.Fill(item9dt);
                            DataRow dataRow18;
                            dataRow18 = item9dt.Rows[0];
                            int item9 = (int)dataRow18["item_code"];
                            #endregion
                            Detail = itemDetail09.Text;
                            Weight = decimal.Parse(weightTextBox09.Text);
                            Count = int.Parse(countTextBox09.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox09.Text);
                            amount = money9 + money9 * Tax / 100; //decimal.Parse(moneyTextBox09.Text);
                            Remarks = remarks09.Text;

                            DataTable dt12 = new DataTable();
                            string sql_str12 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item9 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory9 + ",'" + Detail + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str12, conn);
                            adapter.Fill(dt12);
                            amount09 = amount;
                        }
                        #endregion
                        #region "11行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox010.Text) && !(unitPriceTextBox010.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 11;
                            //mainCategory = mainCategoryCode010;
                            //item = itemCode010;
                            #region "大分類コード"
                            DataTable main10dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_main10 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox010.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main10, conn);
                            adapter.Fill(main10dt);
                            DataRow dataRow19;
                            dataRow19 = main10dt.Rows[0];
                            int mainCategory10 = (int)dataRow19["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item10dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_item10 = "select * from item_m where item_name = '" + itemComboBox010.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item10, conn);
                            adapter.Fill(item10dt);
                            DataRow dataRow20;
                            dataRow20 = item10dt.Rows[0];
                            int item10 = (int)dataRow20["item_code"];
                            #endregion
                            Detail = itemDetail010.Text;
                            Weight = decimal.Parse(weightTextBox010.Text);
                            Count = int.Parse(countTextBox010.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox010.Text);
                            amount = money10 + money10 * Tax / 100; //decimal.Parse(moneyTextBox010.Text);
                            Remarks = remarks010.Text;

                            DataTable dt13 = new DataTable();
                            string sql_str13 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item10 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory10 + ",'" + Detail + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str13, conn);
                            adapter.Fill(dt13);
                            amount010 = amount;
                        }
                        #endregion
                        #region "12行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox011.Text) && !(unitPriceTextBox011.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 12;
                            //mainCategory = mainCategoryCode011;
                            //item = itemCode011;
                            #region "大分類コード"
                            DataTable main11dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_main11 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox011.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main11, conn);
                            adapter.Fill(main11dt);
                            DataRow dataRow21;
                            dataRow21 = main11dt.Rows[0];
                            int mainCategory11 = (int)dataRow21["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item11dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_item11 = "select * from item_m where item_name = '" + itemComboBox011.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item11, conn);
                            adapter.Fill(item11dt);
                            DataRow dataRow22;
                            dataRow22 = item11dt.Rows[0];
                            int item11 = (int)dataRow22["item_code"];
                            #endregion
                            Detail = itemDetail011.Text;
                            Weight = decimal.Parse(weightTextBox011.Text);
                            Count = int.Parse(countTextBox011.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox011.Text);
                            amount = money11 + money11 * Tax / 100; //decimal.Parse(moneyTextBox011.Text);
                            Remarks = remarks011.Text;

                            DataTable dt14 = new DataTable();
                            string sql_str14 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item11 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory11 + ",'" + Detail + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str14, conn);
                            adapter.Fill(dt14);
                            amount011 = amount;
                        }
                        #endregion
                        #region "13行目"
                        if (!string.IsNullOrEmpty(unitPriceTextBox012.Text) && !(unitPriceTextBox012.Text == "単価 -> 重量 or 数量"))
                        {
                            record = 13;
                            //mainCategory = mainCategoryCode012;
                            //item = itemCode012;
                            #region "大分類コード"
                            DataTable main12dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_main12 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox012.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_main12, conn);
                            adapter.Fill(main12dt);
                            DataRow dataRow23;
                            dataRow23 = main12dt.Rows[0];
                            int mainCategory12 = (int)dataRow23["main_category_code"];
                            #endregion
                            #region "品名コード"
                            DataTable item12dt = new DataTable();
                            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                            string sql_item12 = "select * from item_m where item_name = '" + itemComboBox012.Text + "';";
                            adapter = new NpgsqlDataAdapter(sql_item12, conn);
                            adapter.Fill(item12dt);
                            DataRow dataRow24;
                            dataRow24 = item12dt.Rows[0];
                            int item12 = (int)dataRow24["item_code"];
                            #endregion
                            Detail = itemDetail012.Text;
                            Weight = decimal.Parse(weightTextBox012.Text);
                            Count = int.Parse(countTextBox012.Text);
                            UnitPrice = decimal.Parse(unitPriceTextBox012.Text);
                            amount = money12 + money12 * Tax / 100; //decimal.Parse(moneyTextBox012.Text);
                            Remarks = remarks012.Text;

                            DataTable dt15 = new DataTable();
                            string sql_str15 = "Insert into delivery_calc_if VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item12 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory12 + ",'" + Detail + "','" + dat + "');";

                            adapter = new NpgsqlDataAdapter(sql_str15, conn);
                            adapter.Fill(dt15);
                            amount012 = amount;
                        }
                        #endregion
                        #endregion
                }
                else
                {

                }}
                        else { }*/
            #endregion
            client_search search2 = new client_search(this, staff_id, type, client_staff_name, address, total, number, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, document, access_auth, pass);
            Properties.Settings.Default.Save();
            //screan = false;
            //this.Close();
            search2.ShowDialog();

            #region "これから選択する場合"
            if (count != 0)
            {
                if (type == 0)
                {

                    //顧客情報 法人
                    #region "計算書"
                    DataTable clientDt = new DataTable();
                    string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    //int type = (int)row2["type"];

                    string companyNmae = row2["company_name"].ToString();
                    string shopName = row2["shop_name"].ToString();
                    string Staff_name = row2["staff_name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    this.client_Button.Text = "顧客変更";
                    typeTextBox.Text = "法人";                    //種別
                    companyTextBox.Text = companyNmae;              //会社名   
                    registerDateTextBox2.Text = antique_license;              //古物商許可証
                    shopNameTextBox.Text = shopName;                    //店舗名
                    clientNameTextBox.Text = Staff_name;                //担当名
                    registerDateTextBox.Text = register_date;           //登録日
                    clientRemarksTextBox.Text = remarks;                //備考
                    #endregion
                    #region "納品書"
                    this.client_searchButton1.Text = "顧客変更";
                    typeTextBox2.Text = "法人";                   //種別
                    companyTextBox2.Text = companyNmae;             //会社名
                    shopNameTextBox2.Text = shopName;               //店舗名
                    clientNameTextBox2.Text = Staff_name;           //担当名
                    registerDateTextBox2.Text = register_date;      //登録日
                    clientRemarksTextBox2.Text = remarks;           //備考
                    antiqueLicenceTextBox2.Text = antique_license;                //古物商許可証
                    #endregion

                }
                else if (type == 1)
                {
                    //顧客情報 個人
                    DataTable clientDt = new DataTable();
                    string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];

                    string name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string occupation = row2["occupation"].ToString();
                    string birthday = row2["birthday"].ToString();
                    string antique_license = row2["antique_license"].ToString();

                    #region "計算書"
                    label16.Text = "氏名";
                    label17.Text = "生年月日";
                    label18.Text = "職業";
                    typeTextBox.Text = "個人";
                    companyTextBox.Text = name;
                    shopNameTextBox.Text = birthday;
                    clientNameTextBox.Text = occupation;
                    registerDateTextBox.Text = register_date;
                    clientRemarksTextBox.Text = remarks;
                    registerDateTextBox2.Text = antique_license;
                    label38.Visible = false;
                    registerDateTextBox.Visible = false;
                    this.client_Button.Text = "顧客変更";
                    #endregion

                    #region "納品書"
                    typeTextBox2.Text = "個人";
                    label75.Text = "氏名";
                    label76.Text = "職業";
                    label77.Text = "生年月日";
                    clientNameTextBox2.Text = occupation;
                    companyTextBox2.Text = name;
                    shopNameTextBox2.Text = birthday;
                    registerDateTextBox2.Text = register_date;
                    clientRemarksTextBox2.Text = remarks;
                    antiqueLicenceTextBox2.Text = antique_license;
                    label36.Visible = false;
                    registerDateTextBox2.Visible = false;
                    this.client_searchButton1.Text = "顧客変更";
                    #endregion
                }
            }
            #endregion
            #region "１度選択して戻る場合"
            else if (count == 0 && (address != null && client_staff_name != null) && data == null)
            {
                if (type == 0)
                {
                    //顧客情報 法人
                    #region "計算書"
                    DataTable clientDt = new DataTable();
                    string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    //int type = (int)row2["type"];

                    string companyNmae = row2["company_name"].ToString();
                    string shopName = row2["shop_name"].ToString();
                    string Staff_name = row2["staff_name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    this.client_Button.Text = "顧客変更";
                    typeTextBox.Text = "法人";                    //種別
                    companyTextBox.Text = companyNmae;              //会社名   
                    registerDateTextBox2.Text = antique_license;              //古物商許可証
                    shopNameTextBox.Text = shopName;                    //店舗名
                    clientNameTextBox.Text = Staff_name;                //担当名
                    registerDateTextBox.Text = register_date;           //登録日
                    clientRemarksTextBox.Text = remarks;                //備考
                    #endregion
                    #region "納品書"
                    this.client_searchButton1.Text = "顧客変更";
                    typeTextBox2.Text = "法人";                     //種別
                    companyTextBox2.Text = companyNmae;             //会社名
                    shopNameTextBox2.Text = shopName;               //店舗名
                    clientNameTextBox2.Text = Staff_name;           //担当名
                    registerDateTextBox2.Text = register_date;      //登録日
                    clientRemarksTextBox2.Text = remarks;           //備考
                    antiqueLicenceTextBox2.Text = antique_license;                //古物商許可証
                    #endregion

                }
                else if (type == 1)
                {
                    //顧客情報 個人
                    DataTable clientDt = new DataTable();
                    string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];

                    string name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string occupation = row2["occupation"].ToString();
                    string birthday = row2["birthday"].ToString();
                    string antique_license = row2["antique_license"].ToString();

                    #region "計算書"
                    label16.Text = "氏名";
                    label17.Text = "生年月日";
                    label18.Text = "職業";
                    typeTextBox.Text = "個人";
                    companyTextBox.Text = name;
                    shopNameTextBox.Text = birthday;
                    clientNameTextBox.Text = occupation;
                    registerDateTextBox.Text = register_date;
                    clientRemarksTextBox.Text = remarks;
                    registerDateTextBox2.Text = antique_license;
                    label38.Visible = false;
                    registerDateTextBox.Visible = false;
                    this.client_Button.Text = "顧客変更";
                    #endregion

                    #region "納品書"
                    typeTextBox2.Text = "個人";
                    label75.Text = "氏名";
                    label76.Text = "職業";
                    label77.Text = "生年月日";
                    clientNameTextBox2.Text = occupation;
                    companyTextBox2.Text = name;
                    shopNameTextBox2.Text = birthday;
                    registerDateTextBox2.Text = register_date;
                    clientRemarksTextBox2.Text = remarks;
                    antiqueLicenceTextBox2.Text = antique_license;
                    label36.Visible = false;
                    registerDateTextBox2.Visible = false;
                    this.client_searchButton1.Text = "顧客変更";
                    #endregion
                }
            }
            #endregion
        }
        #endregion

        private void CountTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
